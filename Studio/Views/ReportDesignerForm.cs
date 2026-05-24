using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.UserDesigner;
using System;
using System.IO;
using ReportForge.Data;
using ReportForge.Entities;

namespace ReportForge.Views
{
    public partial class ReportDesignerForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly Report _report;

        /// <summary>Design-time only. Runtime callers must use the overload.</summary>
        public ReportDesignerForm()
        {
            InitializeComponent();
        }

        public ReportDesignerForm(Report report) : this()
        {
            _report = report;

            var handler = new DbSaveCommandHandler(reportDesigner1, report.Id);
            reportDesigner1.AddCommandHandler(handler);

            var xtraReport = new XtraReport { Name = report.Name };

            var bytes = Reports.GetTemplateXml(report.Id);
            if (bytes != null && bytes.Length > 0)
            {
                using (var ms = new MemoryStream(bytes))
                    xtraReport.LoadLayoutFromXml(ms);
            }

            reportDesigner1.OpenReport(xtraReport);

            reportDesigner1.ActiveDesignPanel?.AddCommandHandler(handler);
        }

        private void ReportDesignerForm_Load(object sender, EventArgs e)
        {
            Text = $@"Designer — {_report.Name}";
        }

        private sealed class DbSaveCommandHandler : ICommandHandler
        {
            private readonly XRDesignMdiController _controller;
            private readonly int _reportId;

            public DbSaveCommandHandler(XRDesignMdiController controller, int reportId)
            {
                _controller = controller;
                _reportId = reportId;
            }

            public bool CanHandleCommand(ReportCommand command, ref bool useNextHandler)
            {
                useNextHandler = command != ReportCommand.SaveFile && /*command != ReportCommand.SaveFileAs &&*/ command != ReportCommand.SaveAll;
                return !useNextHandler;
            }

            public void HandleCommand(ReportCommand command, object[] args)
            {
                var panel = _controller.ActiveDesignPanel;
                if (panel?.Report == null) return;

                try
                {
                    using (var ms = new MemoryStream())
                    {
                        panel.Report.SaveLayoutToXml(ms);
                        var schemaJson = Rendering.ReportParameterSchema.FromReport(panel.Report);
                        Reports.SaveTemplateXml(_reportId, ms.ToArray(), schemaJson);
                    }

                    panel.ReportState = ReportState.Saved;
                }
                catch (Exception ex)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Failed to save the report:\n\n" + ex.Message,
                        "Save failed",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Error);
                    // ReportState left as Changed so the designer still considers it dirty.
                }
            }
        }
    }
}
