using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ReportForge.Data;
using ReportForge.Entities;
using ReportForge.Views;

namespace ReportForge
{
    public partial class Home : XtraForm
    {

        /// <summary>Design-time only. Runtime entry is <see cref="Home()"/>.</summary>
        public Home()
        {
            InitializeComponent();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            ConfigureToolbarState();
            LoadCatalog();
        }

        private void ConfigureToolbarState()
        {
            UpdateGlobalButtonsEnabled();
        }

        /// <summary>Hides any auto-generated column whose field name is Id or ends with Id.</summary>
        private static void HideIdColumns(GridView view)
        {
            foreach (GridColumn col in view.Columns)
            {
                var name = col.FieldName ?? string.Empty;
                if (name == "Id" || name.EndsWith("Id"))
                    col.Visible = false;
                else switch (name)
                {
                    case "IsActive":
                        col.MaxWidth = 50;
                        break;
                }
            }
        }

        // -- Reports CRUD ---------------------------------------------------

        private void RefreshReports()
        {
            ReportsGridControl.DataSource = Reports.GetAll();
            HideIdColumns(ReportsGridView);
            UpdateGlobalButtonsEnabled();
        }

        private void AddReport()
        {
            using (var dlg = new ReportForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    RefreshReports();
            }
        }

        private void ReportsGridView_DoubleClick(object sender, EventArgs e) => EditFocusedReport();

        private void DeleteReport()
        {
            if (!(ReportsGridView.GetFocusedRow() is Report report)) return;

            var answer = XtraMessageBox.Show(
                this,
                $"Delete report '{report.Name}'?",
                "Delete report",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes) return;

            try
            {
                Reports.Delete(report.Id);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                XtraMessageBox.Show(
                    this,
                    $"Cannot delete '{report.Name}' because it is still referenced by another record.",
                    "Delete blocked",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            RefreshReports();
        }

        private void EditFocusedReport()
        {
            if (!(ReportsGridView.GetFocusedRow() is Report report)) return;

            using (var dlg = new ReportForm(report))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    RefreshReports();
            }
        }

        private void CustomizeReport()
        {
            if (!(ReportsGridView.GetFocusedRow() is Report report)) return;

            using (var designer = new ReportDesignerForm(report)) designer.ShowDialog(this);
        }

        private void ReportsGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) => UpdateGlobalButtonsEnabled();

        // -- Sources CRUD --------------------------------------------------

        private void RefreshSources()
        {
            SourcesGridControl.DataSource = Sources.GetAll();
            HideIdColumns(SourcesGridView);

            var passwordCol = SourcesGridView.Columns.ColumnByFieldName("Password");
            if (passwordCol != null) passwordCol.Visible = false;

            UpdateGlobalButtonsEnabled();
        }

        private void EditFocusedSource()
        {
            if (!(SourcesGridView.GetFocusedRow() is Source source)) return;

            var fullSource = Sources.GetById(source.Id);
            if (fullSource == null)
            {
                RefreshSources();
                return;
            }

            using (var dlg = new SourceForm(fullSource))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    RefreshSources();
            }
        }

        private void LoadCatalog()
        {
            xtraTabControl1.Enabled = true;
            Text = @"ReportForge";
            RefreshReports();
            RefreshSources();
            UpdateGlobalButtonsEnabled();
        }

        private void DuplicateReport()
        {
            if (!(ReportsGridView.GetFocusedRow() is Report report)) return;

            var answer = XtraMessageBox.Show(
                this,
                $"Duplicate report '{report.Name}'?",
                "Duplicate report",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes) return;

            var newId = Reports.Duplicate(report.Id);
            RefreshReports();

            // Focus the new row so the operator can see (and rename) the copy.
            if (newId == 0) return;
            for (var i = 0; i < ReportsGridView.RowCount; i++)
            {
                if (ReportsGridView.GetRow(i) is Report r && r.Id == newId)
                {
                    ReportsGridView.FocusedRowHandle = i;
                    break;
                }
            }
        }

        private void SourcesGridView_DoubleClick(object sender, EventArgs e) => EditFocusedSource();

        private void SourcesGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e) => UpdateGlobalButtonsEnabled();

        private void AddSource()
        {
            using (var dlg = new SourceForm())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    RefreshSources();
            }
        }

        private void DeleteSource()
        {
            if (!(SourcesGridView.GetFocusedRow() is Source source)) return;

            var answer = XtraMessageBox.Show(
                this,
                $"Delete source '{source.Name}'?",
                "Delete source",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes) return;

            Sources.Delete(source.Id);
            RefreshSources();
        }

        private void AddForActiveTab()
        {
            if (xtraTabControl1.SelectedTabPage == ReportsTabPage)
                AddReport();
            else if (xtraTabControl1.SelectedTabPage == SourcesTabPage)
                AddSource();
        }

        private void EditForActiveTab()
        {
            if (xtraTabControl1.SelectedTabPage == ReportsTabPage)
                EditFocusedReport();
            else if (xtraTabControl1.SelectedTabPage == SourcesTabPage)
                EditFocusedSource();
        }

        private void DeleteForActiveTab()
        {
            if (xtraTabControl1.SelectedTabPage == ReportsTabPage)
                DeleteReport();
            else if (xtraTabControl1.SelectedTabPage == SourcesTabPage)
                DeleteSource();
        }

        private void CustomizeForActiveTab()
        {
            if (xtraTabControl1.SelectedTabPage != ReportsTabPage) return;
            CustomizeReport();
        }

        private void DuplicateForActiveTab()
        {
            if (xtraTabControl1.SelectedTabPage != ReportsTabPage) return;
            DuplicateReport();
        }

        private void UpdateGlobalButtonsEnabled()
        {
            ConfigurePopupContextMenuItems();
        }

        private bool HasFocusedRecordForActiveTab()
        {
            if (xtraTabControl1.SelectedTabPage == ReportsTabPage)
                return ReportsGridView.GetFocusedRow() is Report;
            if (xtraTabControl1.SelectedTabPage == SourcesTabPage)
                return SourcesGridView.GetFocusedRow() is Source;

            return false;
        }

        private bool CanDeleteFocusedRecordForActiveTab()
        {
            return HasFocusedRecordForActiveTab();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e) => UpdateGlobalButtonsEnabled();

        private void BarBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (e.Item == null || !e.Item.Enabled) return;

            if (e.Item == BarBtnAdd)
                AddForActiveTab();
            else if (e.Item == BarBtnEdit)
                EditForActiveTab();
            else if (e.Item == BarBtnDelete)
                DeleteForActiveTab();
            else if (e.Item == BarBtnCustomize)
                CustomizeForActiveTab();
            else if (e.Item == BarBtnDuplicate)
                DuplicateForActiveTab();
        }

        private void GridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (!(sender is GridView view)) return;
            if (e.MenuType != GridMenuType.Row && e.MenuType != GridMenuType.User) return;

            if (e.HitInfo.RowHandle >= 0)
                view.FocusedRowHandle = e.HitInfo.RowHandle;
            else
                view.FocusedRowHandle = DevExpress.XtraGrid.GridControl.InvalidRowHandle;

            UpdateGlobalButtonsEnabled();
            ConfigurePopupContextMenuItems();

            e.Allow = false;
            e.Menu = null;
            PopupContextMenu.ShowPopup(e.ScreenPoint);
        }

        private void ConfigurePopupContextMenuItems()
        {
            var selectedPage = xtraTabControl1.SelectedTabPage;
            var onReports = selectedPage == ReportsTabPage;
            var hasRecord = HasFocusedRecordForActiveTab();

            BarBtnAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            BarBtnEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            BarBtnDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            BarBtnCustomize.Visibility = onReports
                ? DevExpress.XtraBars.BarItemVisibility.Always
                : DevExpress.XtraBars.BarItemVisibility.Never;
            BarBtnDuplicate.Visibility = onReports
                ? DevExpress.XtraBars.BarItemVisibility.Always
                : DevExpress.XtraBars.BarItemVisibility.Never;

            BarBtnAdd.Enabled = true;
            BarBtnEdit.Enabled = hasRecord;
            BarBtnDelete.Enabled = CanDeleteFocusedRecordForActiveTab();
            BarBtnCustomize.Enabled = onReports && ReportsGridView.GetFocusedRow() is Report;
            BarBtnDuplicate.Enabled = BarBtnCustomize.Enabled;
        }
    }
}
