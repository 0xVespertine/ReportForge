using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;

namespace ReportForge.Rendering
{
    /// <summary>One declared DevExpress parameter of a report template.</summary>
    public sealed class ReportParameter
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; } = true;

        /// <summary>
        /// Snapshot of allowed values from the parameter's DevExpress lookup
        /// settings at save time. Null means free input.
        /// </summary>
        public List<string> PossibleValues { get; set; }
    }

    /// <summary>
    /// The persisted contract for report parameters. The designer save flow
    /// refreshes it, and render callers validate against it before injecting
    /// values into a report.
    /// </summary>
    public static class ReportParameterSchema
    {
        public const string PeriodFromParameter = "pFrom";
        public const string PeriodToParameter = "pTo";

        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer();

        public static string FromReport(XtraReport report)
        {
            if (report == null) throw new ArgumentNullException(nameof(report));

            var list = report.Parameters
                .Cast<Parameter>()
                .Select(p => new ReportParameter
                {
                    Name = p.Name,
                    Type = p.Type?.FullName ?? "System.String",
                    Description = p.Description,
                    Required = IsRequired(p),
                    PossibleValues = ExtractLookupValues(p)
                })
                .ToList();

            return Serialize(list);
        }

        private static List<string> ExtractLookupValues(Parameter parameter)
        {
            var settings = parameter.LookUpSettings;
            if (settings == null) return null;

            if (settings is StaticListLookUpSettings staticSettings)
                return staticSettings.LookUpValues
                    .Select(v => Convert.ToString(v.Value, CultureInfo.InvariantCulture))
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Distinct(StringComparer.Ordinal)
                    .ToList();

            if (settings is DynamicListLookUpSettings dynamicSettings)
                return ExtractDynamicLookupValues(dynamicSettings, parameter.Name);

            return null;
        }

        private static List<string> ExtractDynamicLookupValues(DynamicListLookUpSettings settings, string parameterName)
        {
            if (!(settings.DataSource is SqlDataSource sqlSource)) return null;

            try { sqlSource.Fill(); }
            catch { return null; }

            var resultSet = sqlSource.Result as DevExpress.DataAccess.Native.Sql.ResultSet;
            if (resultSet == null) return null;

            var table = resultSet.Tables.SingleOrDefault(t => t.TableName == settings.DataMember);
            if (table == null) return null;

            if (table.Columns.Count() > 1)
                throw new InvalidOperationException(
                    "Lookup query for parameter '" + parameterName + "' returned " +
                    table.Columns.Count() + " columns; project only the value column.");

            var values = new List<string>();
            foreach (var row in table)
            {
                var cell = row.First();
                if (cell == null || cell == DBNull.Value) continue;
                var s = Convert.ToString(cell, CultureInfo.InvariantCulture);
                if (!string.IsNullOrWhiteSpace(s)) values.Add(s);
            }

            return values.Distinct(StringComparer.Ordinal).ToList();
        }

        public static string Serialize(IEnumerable<ReportParameter> parameters)
            => Json.Serialize((parameters ?? Enumerable.Empty<ReportParameter>()).ToList());

        public static List<ReportParameter> Parse(string schemaJson)
        {
            if (string.IsNullOrWhiteSpace(schemaJson))
                return new List<ReportParameter>();

            return Json.Deserialize<List<ReportParameter>>(schemaJson) ?? new List<ReportParameter>();
        }

        private static bool IsRequired(Parameter parameter)
        {
            var allowNull = parameter.GetType().GetProperty("AllowNull");
            if (allowNull != null && allowNull.PropertyType == typeof(bool))
            {
                var value = allowNull.GetValue(parameter, null);
                if (value is bool allowed) return !allowed;
            }

            return true;
        }
    }

    public static class ReportParameterValues
    {
        private static readonly JavaScriptSerializer Json = new JavaScriptSerializer();

        public static string Serialize(IDictionary<string, string> values)
            => Json.Serialize(values ?? new Dictionary<string, string>());

        public static Dictionary<string, string> Parse(string parametersJson)
        {
            if (string.IsNullOrWhiteSpace(parametersJson))
                return new Dictionary<string, string>();

            var raw = Json.Deserialize<Dictionary<string, object>>(parametersJson)
                      ?? new Dictionary<string, object>();

            return raw.ToDictionary(
                kv => kv.Key,
                kv => kv.Value?.ToString());
        }

        public static object ConvertValue(string raw, string declaredType)
        {
            var t = Type.GetType(declaredType) ?? typeof(string);
            return ConvertTo(raw, t);
        }

        private static object ConvertTo(string raw, Type t)
        {
            if (t == typeof(string)) return raw ?? string.Empty;
            if (raw == null) return null;

            var underlying = Nullable.GetUnderlyingType(t) ?? t;
            if (underlying == typeof(DateTime))
                return DateTime.Parse(raw, CultureInfo.InvariantCulture);
            if (underlying == typeof(Guid))
                return Guid.Parse(raw);
            if (underlying.IsEnum)
                return Enum.Parse(underlying, raw, true);

            return Convert.ChangeType(raw, underlying, CultureInfo.InvariantCulture);
        }
    }
}
