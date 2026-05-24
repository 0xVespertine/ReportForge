namespace ReportForge
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.ReportsTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.ReportsGridControl = new DevExpress.XtraGrid.GridControl();
            this.ReportsGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.SourcesTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.SourcesGridControl = new DevExpress.XtraGrid.GridControl();
            this.SourcesGridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.PopupContextMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.BarBtnAdd = new DevExpress.XtraBars.BarButtonItem();
            this.BarBtnEdit = new DevExpress.XtraBars.BarButtonItem();
            this.BarBtnDelete = new DevExpress.XtraBars.BarButtonItem();
            this.BarBtnCustomize = new DevExpress.XtraBars.BarButtonItem();
            this.BarBtnDuplicate = new DevExpress.XtraBars.BarButtonItem();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.ReportsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ReportsGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportsGridView)).BeginInit();
            this.SourcesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SourcesGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupContextMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Enabled = false;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.ReportsTabPage;
            this.xtraTabControl1.Size = new System.Drawing.Size(1076, 598);
            this.xtraTabControl1.TabIndex = 1;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.ReportsTabPage,
            this.SourcesTabPage});
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // ReportsTabPage
            // 
            this.ReportsTabPage.Controls.Add(this.ReportsGridControl);
            this.ReportsTabPage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ReportsTabPage.ImageOptions.Image")));
            this.ReportsTabPage.Name = "ReportsTabPage";
            this.ReportsTabPage.Size = new System.Drawing.Size(1074, 554);
            this.ReportsTabPage.Text = "Reports";
            // 
            // ReportsGridControl
            // 
            this.ReportsGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportsGridControl.Location = new System.Drawing.Point(0, 0);
            this.ReportsGridControl.MainView = this.ReportsGridView;
            this.ReportsGridControl.Name = "ReportsGridControl";
            this.ReportsGridControl.Size = new System.Drawing.Size(1074, 554);
            this.ReportsGridControl.TabIndex = 0;
            this.ReportsGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ReportsGridView});
            // 
            // ReportsGridView
            // 
            this.ReportsGridView.GridControl = this.ReportsGridControl;
            this.ReportsGridView.Name = "ReportsGridView";
            this.ReportsGridView.OptionsBehavior.Editable = false;
            this.ReportsGridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.ReportsGridView.OptionsView.ShowAutoFilterRow = true;
            this.ReportsGridView.OptionsView.ShowGroupPanel = false;
            this.ReportsGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.GridView_PopupMenuShowing);
            this.ReportsGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.ReportsGridView_FocusedRowChanged);
            this.ReportsGridView.DoubleClick += new System.EventHandler(this.ReportsGridView_DoubleClick);
            // 
            // SourcesTabPage
            // 
            this.SourcesTabPage.Controls.Add(this.SourcesGridControl);
            this.SourcesTabPage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("SourcesTabPage.ImageOptions.Image")));
            this.SourcesTabPage.Name = "SourcesTabPage";
            this.SourcesTabPage.Size = new System.Drawing.Size(1074, 558);
            this.SourcesTabPage.Text = "Sources";
            // 
            // SourcesGridControl
            // 
            this.SourcesGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourcesGridControl.Location = new System.Drawing.Point(0, 0);
            this.SourcesGridControl.MainView = this.SourcesGridView;
            this.SourcesGridControl.Name = "SourcesGridControl";
            this.SourcesGridControl.Size = new System.Drawing.Size(1074, 558);
            this.SourcesGridControl.TabIndex = 0;
            this.SourcesGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.SourcesGridView});
            // 
            // SourcesGridView
            // 
            this.SourcesGridView.GridControl = this.SourcesGridControl;
            this.SourcesGridView.Name = "SourcesGridView";
            this.SourcesGridView.OptionsBehavior.Editable = false;
            this.SourcesGridView.OptionsView.ShowGroupPanel = false;
            this.SourcesGridView.PopupMenuShowing += new DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventHandler(this.GridView_PopupMenuShowing);
            this.SourcesGridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.SourcesGridView_FocusedRowChanged);
            this.SourcesGridView.DoubleClick += new System.EventHandler(this.SourcesGridView_DoubleClick);
            // 
            // PopupContextMenu
            // 
            this.PopupContextMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BarBtnAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarBtnEdit),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarBtnDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarBtnCustomize, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarBtnDuplicate, true)});
            this.PopupContextMenu.Manager = this.barManager1;
            this.PopupContextMenu.Name = "PopupContextMenu";
            // 
            // BarBtnAdd
            // 
            this.BarBtnAdd.Caption = "Add";
            this.BarBtnAdd.Id = 0;
            this.BarBtnAdd.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarBtnAdd.ImageOptions.Image")));
            this.BarBtnAdd.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BarBtnAdd.ImageOptions.LargeImage")));
            this.BarBtnAdd.Name = "BarBtnAdd";
            this.BarBtnAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_ItemClick);
            // 
            // BarBtnEdit
            // 
            this.BarBtnEdit.Caption = "Edit";
            this.BarBtnEdit.Enabled = false;
            this.BarBtnEdit.Id = 1;
            this.BarBtnEdit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarBtnEdit.ImageOptions.Image")));
            this.BarBtnEdit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BarBtnEdit.ImageOptions.LargeImage")));
            this.BarBtnEdit.Name = "BarBtnEdit";
            this.BarBtnEdit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_ItemClick);
            // 
            // BarBtnDelete
            // 
            this.BarBtnDelete.Caption = "Delete";
            this.BarBtnDelete.Enabled = false;
            this.BarBtnDelete.Id = 2;
            this.BarBtnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarBtnDelete.ImageOptions.Image")));
            this.BarBtnDelete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BarBtnDelete.ImageOptions.LargeImage")));
            this.BarBtnDelete.Name = "BarBtnDelete";
            this.BarBtnDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_ItemClick);
            // 
            // BarBtnCustomize
            // 
            this.BarBtnCustomize.Caption = "Customize";
            this.BarBtnCustomize.Enabled = false;
            this.BarBtnCustomize.Id = 4;
            this.BarBtnCustomize.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarBtnCustomize.ImageOptions.Image")));
            this.BarBtnCustomize.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BarBtnCustomize.ImageOptions.LargeImage")));
            this.BarBtnCustomize.Name = "BarBtnCustomize";
            this.BarBtnCustomize.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.BarBtnCustomize.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_ItemClick);
            // 
            // BarBtnDuplicate
            // 
            this.BarBtnDuplicate.Caption = "Duplicate";
            this.BarBtnDuplicate.Enabled = false;
            this.BarBtnDuplicate.Id = 7;
            this.BarBtnDuplicate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarBtnDuplicate.ImageOptions.Image")));
            this.BarBtnDuplicate.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BarBtnDuplicate.ImageOptions.LargeImage")));
            this.BarBtnDuplicate.Name = "BarBtnDuplicate";
            this.BarBtnDuplicate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.BarBtnDuplicate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarBtn_ItemClick);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BarBtnAdd,
            this.BarBtnEdit,
            this.BarBtnDelete,
            this.BarBtnCustomize,
            this.BarBtnDuplicate});
            this.barManager1.MaxItemId = 9;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1076, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 598);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1076, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 598);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1076, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 598);
            // 
            // Home
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 598);
            this.Controls.Add(this.xtraTabControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.Image = global::ReportForge.Properties.Resources.icon;
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportForge";
            this.Load += new System.EventHandler(this.Home_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ReportsTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ReportsGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportsGridView)).EndInit();
            this.SourcesTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SourcesGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PopupContextMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage ReportsTabPage;
        private DevExpress.XtraGrid.GridControl ReportsGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView ReportsGridView;
        private DevExpress.XtraTab.XtraTabPage SourcesTabPage;
        private DevExpress.XtraGrid.GridControl SourcesGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView SourcesGridView;
        private DevExpress.XtraBars.PopupMenu PopupContextMenu;
        private DevExpress.XtraBars.BarButtonItem BarBtnAdd;
        private DevExpress.XtraBars.BarButtonItem BarBtnEdit;
        private DevExpress.XtraBars.BarButtonItem BarBtnDelete;
        private DevExpress.XtraBars.BarButtonItem BarBtnCustomize;
        private DevExpress.XtraBars.BarButtonItem BarBtnDuplicate;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
