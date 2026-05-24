using System;
using ReportForge.Data;
using ReportForge.Entities;

namespace ReportForge.Views
{
    public partial class ReportForm : DevExpress.XtraEditors.XtraForm
    {
        private readonly Report _existing;  // null = Add mode, non-null = Edit mode

        /// <summary>Saved report (with generated Id on Insert) — populated after a successful OK.</summary>
        public Report SavedReport { get; private set; }

        /// <summary>Design-time only. Runtime callers must use one of the overloads.</summary>
        public ReportForm()
        {
            InitializeComponent();
        }

        public ReportForm(Report report) : this()
        {
            _existing = report;
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            if (_existing != null)
            {
                Text = @"Edit report";
                NameField.Text = _existing.Name;
                DescriptionField.Text = _existing.Description ?? string.Empty;
            }
            else
            {
                Text = @"Add report";
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!validationProvider.Validate()) return;

            var report = new Report
            {
                Id          = _existing?.Id ?? 0,
                Name        = NameField.Text.Trim(),
                Description = string.IsNullOrWhiteSpace(DescriptionField.Text) ? null : DescriptionField.Text.Trim()
            };

            if (_existing == null)
                report.Id = Reports.Insert(report);
            else
                Reports.Update(report);

            SavedReport = report;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
