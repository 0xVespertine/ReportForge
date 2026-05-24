using DevExpress.DataAccess.Sql;
using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using DevExpress.XtraReports.Security;
using ReportForge.Data;

namespace ReportForge
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            WindowsFormsSettings.AllowRoundedWindowCorners = DevExpress.Utils.DefaultBoolean.True;

            WindowsFormsSettings.EnableMdiFormSkins();

            ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);

            SqlDataSource.DisableCustomQueryValidation = true;
            SqlDataSource.AllowCustomSqlQueries = true;

            try
            {
                Schema.EnsureCreated();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(
                    "Failed to initialize ReportForge database tables.\n\n" + ex.Message,
                    "ReportForge",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            Application.Run(new Home());
        }
    }
}
