using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using DevExpress.XtraReports.UI;
using ReportForge.Data;
using ReportForge.Entities;

namespace ReportForge.Rendering
{
    internal static class ReportRenderGate
    {
        private static readonly SemaphoreSlim Gate = new SemaphoreSlim(1, 1);

        public static void Wait() => Gate.Wait();
        public static void Release() => Gate.Release();
    }

    public sealed class RenderedReport
    {
        public int ReportId { get; set; }
        public string Name { get; set; }
        public ReportExportFormat Format { get; set; }
        public byte[] Bytes { get; set; }
        public string FileName { get; set; }
        public string Mime { get; set; }
    }

    /// <summary>
    /// Renders a saved report template from caller-supplied, schema-validated
    /// parameter values. The caller chooses values and export format, not the
    /// layout or query shape.
    /// </summary>
    public static class ReportRenderer
    {
        private static readonly Regex SafeNameRegex = new Regex(@"[^\w\-\. ]+", RegexOptions.Compiled);

        public static RenderedReport Render(
            int reportId,
            ReportExportFormat exportFormat,
            IDictionary<string, string> values)
        {
            var report = Reports.GetById(reportId);
            if (report == null)
                throw new InvalidOperationException("Report #" + reportId + " does not exist.");

            return Render(report, exportFormat, values);
        }

        public static RenderedReport Render(
            Report report,
            ReportExportFormat exportFormat,
            IDictionary<string, string> values)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            ReportRenderGate.Wait();
            try
            {
                var templateXml = Reports.GetTemplateXml(report.Id);
                if (templateXml == null || templateXml.Length == 0)
                    throw new InvalidOperationException(
                        $"Report '{report.Name}' has no saved layout - open the designer and save it first.");

                var declared = ReportParameterSchema.Parse(Reports.GetParametersSchemaJson(report.Id));
                var declaredBy = declared.ToDictionary(p => p.Name, StringComparer.Ordinal);
                values = values ?? new Dictionary<string, string>();

                foreach (var key in values.Keys)
                    if (!declaredBy.ContainsKey(key))
                        throw new InvalidOperationException(
                            $"Report '{report.Name}' does not declare parameter '{key}'.");

                using (var xtraReport = new XtraReport())
                using (var source = new MemoryStream(templateXml))
                {
                    xtraReport.LoadLayoutFromXml(source);
                    ValidateTemplateMatchesSchema(xtraReport, report, declaredBy);
                    xtraReport.RequestParameters = false;

                    foreach (var parameter in declared)
                        ApplyParameter(xtraReport, report, parameter, values);

                    ValidatePeriodRange(xtraReport, report);
                    return Export(xtraReport, report, exportFormat);
                }
            }
            finally
            {
                ReportRenderGate.Release();
            }
        }

        private static void ValidateTemplateMatchesSchema(
            XtraReport xtraReport,
            Report report,
            IDictionary<string, ReportParameter> declaredBy)
        {
            foreach (DevExpress.XtraReports.Parameters.Parameter actual in xtraReport.Parameters)
            {
                if (!declaredBy.TryGetValue(actual.Name, out var declared))
                    throw new InvalidOperationException(
                        $"Report '{report.Name}' has template parameter '{actual.Name}' " +
                        "that is missing from ParametersSchemaJson - re-save the template before rendering.");

                var actualType = actual.Type?.FullName ?? "System.String";
                var declaredType = string.IsNullOrWhiteSpace(declared.Type) ? "System.String" : declared.Type;
                if (!StringComparer.Ordinal.Equals(actualType, declaredType))
                    throw new InvalidOperationException(
                        $"Report '{report.Name}' parameter '{actual.Name}' changed type " +
                        $"from '{declaredType}' to '{actualType}' - re-save the template before rendering.");
            }
        }

        private static void ApplyParameter(
            XtraReport xtraReport,
            Report report,
            ReportParameter parameter,
            IDictionary<string, string> values)
        {
            var actual = xtraReport.Parameters[parameter.Name];
            if (actual == null)
                throw new InvalidOperationException(
                    $"Template parameter '{parameter.Name}' is declared in the schema " +
                    $"but missing from report '{report.Name}' - re-save the template.");

            values.TryGetValue(parameter.Name, out var raw);
            if (string.IsNullOrWhiteSpace(raw))
            {
                if (parameter.Required)
                    throw new InvalidOperationException(
                        $"Report '{report.Name}' requires parameter '{parameter.Name}'.");

                actual.Value = null;
                return;
            }

            object converted;
            try
            {
                converted = ReportParameterValues.ConvertValue(raw.Trim(), actual.Type?.FullName ?? parameter.Type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Report '{report.Name}': value '{raw}' for parameter '{parameter.Name}' " +
                    $"is not a valid {parameter.Type}. {ex.Message}");
            }

            if (parameter.Name == ReportParameterSchema.PeriodToParameter && converted is DateTime to && to.TimeOfDay == TimeSpan.Zero)
                converted = to.Date.AddDays(1).AddSeconds(-1);

            actual.Value = converted;
        }

        private static void ValidatePeriodRange(XtraReport xtraReport, Report report)
        {
            var from = xtraReport.Parameters[ReportParameterSchema.PeriodFromParameter]?.Value;
            var to = xtraReport.Parameters[ReportParameterSchema.PeriodToParameter]?.Value;

            if (from is DateTime fromDate && to is DateTime toDate && toDate < fromDate)
                throw new InvalidOperationException(
                    $"Report '{report.Name}' has an invalid period: pTo is earlier than pFrom.");
        }

        private static RenderedReport Export(
            XtraReport report,
            Report meta,
            ReportExportFormat exportFormat)
        {
            using (var dest = new MemoryStream())
            {
                string ext, mime;
                switch (exportFormat)
                {
                    case ReportExportFormat.Pdf:
                        report.ExportToPdf(dest); ext = ".pdf"; mime = "application/pdf"; break;
                    case ReportExportFormat.Excel:
                        report.ExportToXlsx(dest); ext = ".xlsx"; mime = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; break;
                    case ReportExportFormat.Image:
                        report.ExportToImage(dest); ext = ".png"; mime = "image/png"; break;
                    case ReportExportFormat.Html:
                        report.ExportToHtml(dest); ext = ".html"; mime = "text/html"; break;
                    default:
                        throw new InvalidOperationException($"Unsupported export format {exportFormat}.");
                }

                return new RenderedReport
                {
                    ReportId = meta.Id,
                    Name = meta.Name,
                    Format = exportFormat,
                    Bytes = dest.ToArray(),
                    FileName = SafeFileName(meta.Name) + ext,
                    Mime = mime
                };
            }
        }

        private static string SafeFileName(string name)
        {
            var safe = SafeNameRegex.Replace(name ?? string.Empty, "_").Trim();
            return string.IsNullOrWhiteSpace(safe) ? "report" : safe;
        }
    }
}
