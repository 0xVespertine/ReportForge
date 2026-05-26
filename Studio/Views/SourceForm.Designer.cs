namespace ReportForge.Views
{
    partial class SourceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceForm));
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule1 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule2 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule3 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule4 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule conditionValidationRule5 = new DevExpress.XtraEditors.DXErrorProvider.ConditionValidationRule();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.BtnOk = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.NameField = new DevExpress.XtraEditors.TextEdit();
            this.ServerField = new DevExpress.XtraEditors.TextEdit();
            this.DatabaseField = new DevExpress.XtraEditors.TextEdit();
            this.UsernameField = new DevExpress.XtraEditors.TextEdit();
            this.EncryptField = new DevExpress.XtraEditors.CheckEdit();
            this.TrustServerCertificateField = new DevExpress.XtraEditors.CheckEdit();
            this.PasswordField = new DevExpress.XtraEditors.TextEdit();
            this.BtnCopyPass = new DevExpress.XtraEditors.SimpleButton();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.validationProvider = new DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NameField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatabaseField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EncryptField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrustServerCertificateField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordField.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.validationProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.BtnOk);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl1.Location = new System.Drawing.Point(0, 247);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new System.Drawing.Size(355, 32);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "groupControl1";
            // 
            // BtnOk
            // 
            this.BtnOk.Dock = System.Windows.Forms.DockStyle.Right;
            this.BtnOk.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnOk.ImageOptions.Image")));
            this.BtnOk.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnOk.Location = new System.Drawing.Point(306, 2);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(47, 28);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "Ok";
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.NameField);
            this.layoutControl1.Controls.Add(this.ServerField);
            this.layoutControl1.Controls.Add(this.DatabaseField);
            this.layoutControl1.Controls.Add(this.UsernameField);
            this.layoutControl1.Controls.Add(this.EncryptField);
            this.layoutControl1.Controls.Add(this.TrustServerCertificateField);
            this.layoutControl1.Controls.Add(this.PasswordField);
            this.layoutControl1.Controls.Add(this.BtnCopyPass);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(355, 247);
            this.layoutControl1.TabIndex = 5;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // NameField
            // 
            this.NameField.Location = new System.Drawing.Point(100, 16);
            this.NameField.Name = "NameField";
            this.NameField.Size = new System.Drawing.Size(239, 28);
            this.NameField.StyleController = this.layoutControl1;
            this.NameField.TabIndex = 4;
            conditionValidationRule1.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule1.ErrorText = "This value is not valid";
            conditionValidationRule1.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
            this.validationProvider.SetValidationRule(this.NameField, conditionValidationRule1);
            // 
            // ServerField
            // 
            this.ServerField.Location = new System.Drawing.Point(100, 50);
            this.ServerField.Name = "ServerField";
            this.ServerField.Size = new System.Drawing.Size(239, 28);
            this.ServerField.StyleController = this.layoutControl1;
            this.ServerField.TabIndex = 5;
            conditionValidationRule2.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule2.ErrorText = "This value is not valid";
            conditionValidationRule2.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
            this.validationProvider.SetValidationRule(this.ServerField, conditionValidationRule2);
            // 
            // DatabaseField
            // 
            this.DatabaseField.Location = new System.Drawing.Point(100, 84);
            this.DatabaseField.Name = "DatabaseField";
            this.DatabaseField.Size = new System.Drawing.Size(239, 28);
            this.DatabaseField.StyleController = this.layoutControl1;
            this.DatabaseField.TabIndex = 6;
            conditionValidationRule3.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule3.ErrorText = "This value is not valid";
            conditionValidationRule3.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
            this.validationProvider.SetValidationRule(this.DatabaseField, conditionValidationRule3);
            // 
            // UsernameField
            // 
            this.UsernameField.Location = new System.Drawing.Point(100, 118);
            this.UsernameField.Name = "UsernameField";
            this.UsernameField.Size = new System.Drawing.Size(239, 28);
            this.UsernameField.StyleController = this.layoutControl1;
            this.UsernameField.TabIndex = 7;
            conditionValidationRule4.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule4.ErrorText = "This value is not valid";
            conditionValidationRule4.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
            this.validationProvider.SetValidationRule(this.UsernameField, conditionValidationRule4);
            // 
            // EncryptField
            // 
            this.EncryptField.EditValue = true;
            this.EncryptField.Location = new System.Drawing.Point(16, 186);
            this.EncryptField.Name = "EncryptField";
            this.EncryptField.Properties.Caption = "Encrypt";
            this.EncryptField.Size = new System.Drawing.Size(158, 22);
            this.EncryptField.StyleController = this.layoutControl1;
            this.EncryptField.TabIndex = 8;
            // 
            // TrustServerCertificateField
            // 
            this.TrustServerCertificateField.EditValue = true;
            this.TrustServerCertificateField.Location = new System.Drawing.Point(180, 186);
            this.TrustServerCertificateField.Name = "TrustServerCertificateField";
            this.TrustServerCertificateField.Properties.Caption = "Trust server certificate";
            this.TrustServerCertificateField.Size = new System.Drawing.Size(159, 22);
            this.TrustServerCertificateField.StyleController = this.layoutControl1;
            this.TrustServerCertificateField.TabIndex = 9;
            // 
            // PasswordField
            // 
            this.PasswordField.Location = new System.Drawing.Point(100, 152);
            this.PasswordField.Name = "PasswordField";
            this.PasswordField.Properties.UseSystemPasswordChar = true;
            this.PasswordField.Size = new System.Drawing.Size(207, 28);
            this.PasswordField.StyleController = this.layoutControl1;
            this.PasswordField.TabIndex = 10;
            conditionValidationRule5.ConditionOperator = DevExpress.XtraEditors.DXErrorProvider.ConditionOperator.IsNotBlank;
            conditionValidationRule5.ErrorText = "This value is not valid";
            conditionValidationRule5.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
            this.validationProvider.SetValidationRule(this.PasswordField, conditionValidationRule5);
            // 
            // BtnCopyPass
            // 
            this.BtnCopyPass.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnCopyPass.ImageOptions.Image")));
            this.BtnCopyPass.Location = new System.Drawing.Point(313, 152);
            this.BtnCopyPass.Name = "BtnCopyPass";
            this.BtnCopyPass.Size = new System.Drawing.Size(26, 28);
            this.BtnCopyPass.StyleController = this.layoutControl1;
            this.BtnCopyPass.TabIndex = 11;
            this.BtnCopyPass.Click += new System.EventHandler(this.BtnCopyPass_Click);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(355, 247);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem1.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem1.Control = this.NameField;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(329, 34);
            this.layoutControlItem1.Text = "Name (*):";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem2.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem2.Control = this.ServerField;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 34);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(329, 34);
            this.layoutControlItem2.Text = "Server (*):";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem3.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem3.Control = this.DatabaseField;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 68);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(329, 34);
            this.layoutControlItem3.Text = "Database (*):";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem4.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem4.Control = this.UsernameField;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 102);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(329, 34);
            this.layoutControlItem4.Text = "Username (*):";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem7.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.layoutControlItem7.Control = this.PasswordField;
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 136);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(297, 34);
            this.layoutControlItem7.Text = "Password (*):";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(68, 13);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.BtnCopyPass;
            this.layoutControlItem8.Location = new System.Drawing.Point(297, 136);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(32, 34);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.EncryptField;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 170);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(164, 51);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.TrustServerCertificateField;
            this.layoutControlItem6.Location = new System.Drawing.Point(164, 170);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(165, 51);
            this.layoutControlItem6.TextVisible = false;
            // 
            // SourceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 279);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SourceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Source";
            this.Load += new System.EventHandler(this.SourceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NameField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServerField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatabaseField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UsernameField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EncryptField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrustServerCertificateField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PasswordField.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.validationProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton BtnOk;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit NameField;
        private DevExpress.XtraEditors.TextEdit ServerField;
        private DevExpress.XtraEditors.TextEdit DatabaseField;
        private DevExpress.XtraEditors.TextEdit UsernameField;
        private DevExpress.XtraEditors.CheckEdit EncryptField;
        private DevExpress.XtraEditors.CheckEdit TrustServerCertificateField;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit PasswordField;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.DXErrorProvider.DXValidationProvider validationProvider;
        private DevExpress.XtraEditors.SimpleButton BtnCopyPass;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
    }
}
