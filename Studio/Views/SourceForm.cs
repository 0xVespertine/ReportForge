using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ReportForge.Data;
using ReportForge.Entities;

namespace ReportForge.Views
{
    public partial class SourceForm : XtraForm
    {
        private readonly Source _existing;

        public Source SavedSource { get; private set; }

        public SourceForm()
        {
            InitializeComponent();
        }

        public SourceForm(Source source) : this()
        {
            _existing = source;
        }

        private void SourceForm_Load(object sender, EventArgs e)
        {
            if (_existing != null)
            {
                Text = @"Edit source";
                NameField.Text = _existing.Name ?? string.Empty;
                ServerField.Text = _existing.Server ?? string.Empty;
                DatabaseField.Text = _existing.Database ?? string.Empty;
                UsernameField.Text = _existing.Username ?? string.Empty;
                PasswordField.Text = _existing.Password ?? string.Empty;
                EncryptField.Checked = _existing.Encrypt;
                TrustServerCertificateField.Checked = _existing.TrustServerCertificate;
            }
            else
            {
                Text = @"Add source";
                EncryptField.Checked = true;
                TrustServerCertificateField.Checked = true;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (!validationProvider.Validate()) return;

            var source = new Source
            {
                Id = _existing?.Id ?? 0,
                Name = NameField.Text.Trim(),
                Server = ServerField.Text.Trim(),
                Database = DatabaseField.Text.Trim(),
                Username = UsernameField.Text.Trim(),
                Password = PasswordField.Text ?? string.Empty,
                Encrypt = EncryptField.Checked,
                TrustServerCertificate = TrustServerCertificateField.Checked
            };

            try
            {
                Sources.TestConnectivity(source);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    this,
                    "Could not connect to the source. The source was not saved.\n\n" + ex.Message,
                    "Source",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (_existing == null)
                    source.Id = Sources.Insert(source);
                else
                    Sources.Update(source);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    this,
                    "Connection succeeded, but the source could not be saved.\n\n" + ex.Message,
                    "Source",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            SavedSource = source;
            DialogResult = DialogResult.OK;
        }

        private void BtnCopyPass_Click(object sender, EventArgs e)
        {
            var password = PasswordField.Text ?? string.Empty;
            if (password.Length == 0) return;

            try
            {
                Clipboard.SetText(password);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    this,
                    "Failed to copy password.\n\n" + ex.Message,
                    "Copy password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
