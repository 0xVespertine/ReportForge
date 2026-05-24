using System.ComponentModel;

namespace ReportForge.Entities
{
    /// <summary>
    /// A reusable DevExpress .repx template managed by the personal report
    /// catalog. Export format and parameter values are supplied at render time.
    /// </summary>
    public class Report
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Browsable(false)]
        public byte[] TemplateXml { get; set; }

        /// <summary>
        /// Snapshot of the template's declared DevExpress parameters (name +
        /// type) as JSON, refreshed whenever <see cref="TemplateXml"/> is saved.
        /// Lets UI and endpoint callers validate inputs without re-parsing the
        /// .repx. Null until a template has been saved.
        /// </summary>
        [Browsable(false)]
        public string ParametersSchemaJson { get; set; }
    }
}
