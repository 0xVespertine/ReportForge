using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using ReportForge.Data;
using ReportForge.Entities;
using ReportForge.Rendering;

namespace ReportForge
{
    /// <summary>
    /// Minimal local HTTP surface for deterministic report rendering.
    ///
    /// Endpoints:
    ///   GET  /health
    ///   GET  /reports
    ///   POST /render {reportId, exportType, params}
    /// </summary>
    internal sealed class RenderEndpoint : IDisposable
    {
        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer { MaxJsonLength = int.MaxValue };
        private static readonly string[] AllowedExports = { "Pdf", "Excel", "Image", "Html" };

        private readonly string _prefix;
        private HttpListener _listener;
        private Thread _loop;
        private volatile bool _stopping;

        public string Prefix => _prefix;

        public RenderEndpoint(string prefix)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentException("HTTP endpoint prefix is empty.", nameof(prefix));

            _prefix = prefix.EndsWith("/") ? prefix : prefix + "/";
        }

        public void Start()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(_prefix);
            _listener.Start();

            _loop = new Thread(Loop)
            {
                IsBackground = true,
                Name = "ReportForge render endpoint"
            };
            _loop.Start();
        }

        private void Loop()
        {
            while (!_stopping)
            {
                HttpListenerContext ctx;
                try
                {
                    ctx = _listener.GetContext();
                }
                catch (Exception) when (_stopping)
                {
                    return;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Endpoint accept failed: " + ex.Message);
                    continue;
                }

                try { Handle(ctx); }
                catch (Exception ex) { Console.Error.WriteLine("Endpoint handler crashed: " + FlattenException(ex)); }
            }
        }

        private void Handle(HttpListenerContext ctx)
        {
            var path = (ctx.Request.Url.AbsolutePath ?? string.Empty).TrimEnd('/').ToLowerInvariant();
            var method = ctx.Request.HttpMethod;

            try
            {
                if (path == "/health" && method == "GET")
                {
                    Write(ctx, 200, new { status = "ok" });
                    return;
                }

                if (path == "/reports" && method == "GET")
                {
                    HandleReports(ctx);
                    return;
                }

                if (path == "/render" && method == "POST")
                {
                    HandleRender(ctx);
                    return;
                }

                Write(ctx, 404, new { error = "Unknown endpoint." });
            }
            catch (HttpError he)
            {
                Write(ctx, he.StatusCode, new { error = he.Message });
            }
            catch (Exception ex)
            {
                var detail = FlattenException(ex);
                Console.Error.WriteLine("Request failed: " + detail);
                Write(ctx, 422, new { error = detail });
            }
        }

        private static void HandleReports(HttpListenerContext ctx)
        {
            var reports = Reports.GetRenderable()
                .Select(full =>
                {
                    var parameters = ReportParameterSchema.Parse(full.ParametersSchemaJson)
                        .Select(p => new
                        {
                            name = p.Name,
                            type = p.Type,
                            description = p.Description,
                            required = p.Required,
                            possibleValues = p.PossibleValues
                        })
                        .ToList();

                    return new
                    {
                        reportId = full.Id,
                        name = full.Name,
                        description = full.Description,
                        allowedExports = AllowedExports,
                        parameters
                    };
                })
                .ToList();

            Write(ctx, 200, new
            {
                count = reports.Count,
                reports
            });
        }

        private static void HandleRender(HttpListenerContext ctx)
        {
            var req = ReadJsonObject(ctx.Request);

            var reportId = RequireInt(req, "reportId");
            var exportType = RequireExportType(req);
            var values = ReadParameters(req);

            var rendered = ReportRenderer.Render(reportId, exportType, values);
            if (rendered.Bytes == null || rendered.Bytes.Length == 0)
                throw new HttpError(422, "Renderer produced no file for report '" + rendered.Name + "'.");

            Write(ctx, 200, new
            {
                reportId = rendered.ReportId,
                name = rendered.Name,
                exportType = rendered.Format.ToString(),
                fileName = rendered.FileName,
                mimeType = rendered.Mime,
                base64 = Convert.ToBase64String(rendered.Bytes)
            });
        }

        private static Dictionary<string, object> ReadJsonObject(HttpListenerRequest request)
        {
            var body = ReadBody(request);
            try
            {
                return Json.Deserialize<Dictionary<string, object>>(body)
                    ?? throw new HttpError(400, "Request body is empty.");
            }
            catch (HttpError) { throw; }
            catch (Exception ex)
            {
                throw new HttpError(400, "Invalid JSON body: " + ex.Message);
            }
        }

        private static string ReadBody(HttpListenerRequest request)
        {
            using (var reader = new StreamReader(request.InputStream, request.ContentEncoding ?? Encoding.UTF8))
                return reader.ReadToEnd();
        }

        private static Dictionary<string, string> ReadParameters(IDictionary<string, object> req)
        {
            if (!req.TryGetValue("params", out var raw) || raw == null)
                return new Dictionary<string, string>();

            if (!(raw is Dictionary<string, object> dict))
                throw new HttpError(400, "Field 'params' must be an object.");

            return dict.ToDictionary(kv => kv.Key, kv => StringifyInvariant(kv.Value));
        }

        private static string StringifyInvariant(object value)
        {
            if (value == null) return null;
            if (value is string s) return s;
            if (value is IConvertible c)
                return c.ToString(CultureInfo.InvariantCulture);
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        private static int RequireInt(IDictionary<string, object> req, string key)
        {
            if (req == null || !req.TryGetValue(key, out var raw) || raw == null
                || !int.TryParse(Convert.ToString(raw, CultureInfo.InvariantCulture), out var v))
                throw new HttpError(400, "Field '" + key + "' is required and must be an integer.");

            return v;
        }

        private static ReportExportFormat RequireExportType(IDictionary<string, object> req)
        {
            if (req == null || !req.TryGetValue("exportType", out var raw) || raw == null)
                throw new HttpError(400, "Field 'exportType' is required.");

            var value = Convert.ToString(raw, CultureInfo.InvariantCulture);
            foreach (var allowed in AllowedExports)
                if (allowed.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return (ReportExportFormat)Enum.Parse(typeof(ReportExportFormat), allowed);

            throw new HttpError(400, "Field 'exportType' must be one of: " + string.Join(", ", AllowedExports) + ".");
        }

        private static void Write(HttpListenerContext ctx, int status, object payload)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(Json.Serialize(payload));
                ctx.Response.StatusCode = status;
                ctx.Response.ContentType = "application/json; charset=utf-8";
                ctx.Response.ContentLength64 = bytes.Length;
                ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }
            finally
            {
                try { ctx.Response.OutputStream.Close(); } catch { }
            }
        }

        public void Dispose()
        {
            _stopping = true;
            try { _listener?.Stop(); } catch { }
            try { _listener?.Close(); } catch { }
            try { _loop?.Join(2000); } catch { }
        }

        public static string FlattenException(Exception ex)
        {
            var sb = new StringBuilder();
            var current = ex;
            var depth = 0;

            while (current != null && depth < 8)
            {
                if (depth > 0) sb.Append(" -> ");
                sb.Append('[').Append(current.GetType().Name).Append("] ").Append(current.Message);
                current = current.InnerException;
                depth++;
            }

            return sb.ToString();
        }

        private sealed class HttpError : Exception
        {
            public int StatusCode { get; }
            public HttpError(int statusCode, string message) : base(message) => StatusCode = statusCode;
        }
    }
}
