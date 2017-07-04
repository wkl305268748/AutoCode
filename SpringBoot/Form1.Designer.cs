namespace SpringBoot
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtnConnectDB = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.imageListBoxControl = new DevExpress.XtraEditors.ImageListBoxControl();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.checkedListBoxControl = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.panelControl = new DevExpress.XtraEditors.PanelControl();
            this.labprograss = new DevExpress.XtraEditors.LabelControl();
            this.progressBar = new DevExpress.XtraEditors.ProgressBarControl();
            this.BtnBegin = new DevExpress.XtraEditors.SimpleButton();
            this.checkController = new DevExpress.XtraEditors.CheckEdit();
            this.checkService = new DevExpress.XtraEditors.CheckEdit();
            this.checkMapper = new DevExpress.XtraEditors.CheckEdit();
            this.checkModel = new DevExpress.XtraEditors.CheckEdit();
            this.buttonPath = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textPackage = new DevExpress.XtraEditors.TextEdit();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).BeginInit();
            this.panelControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkController.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkService.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkMapper.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkModel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPackage.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.barBtnConnectDB});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(925, 147);
            // 
            // barBtnConnectDB
            // 
            this.barBtnConnectDB.Caption = "连接数据库";
            this.barBtnConnectDB.Id = 1;
            this.barBtnConnectDB.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barBtnConnectDB.LargeGlyph")));
            this.barBtnConnectDB.Name = "barBtnConnectDB";
            this.barBtnConnectDB.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText)
                        | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.barBtnConnectDB.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnConnectDB_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "设置";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barBtnConnectDB);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "数据库";
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "db.png");
            this.imageList.Images.SetKeyName(1, "db2.png");
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 147);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.imageListBoxControl);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(925, 480);
            this.splitContainerControl1.SplitterPosition = 200;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // imageListBoxControl
            // 
            this.imageListBoxControl.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.imageListBoxControl.Appearance.Options.UseFont = true;
            this.imageListBoxControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.imageListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListBoxControl.ImageList = this.imageList;
            this.imageListBoxControl.ItemHeight = 25;
            this.imageListBoxControl.Location = new System.Drawing.Point(0, 0);
            this.imageListBoxControl.Name = "imageListBoxControl";
            this.imageListBoxControl.Size = new System.Drawing.Size(200, 480);
            this.imageListBoxControl.TabIndex = 0;
            this.imageListBoxControl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.imageListBoxControl_MouseDoubleClick);
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.checkedListBoxControl);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(720, 480);
            this.splitContainerControl2.SplitterPosition = 200;
            this.splitContainerControl2.TabIndex = 0;
            this.splitContainerControl2.Text = "splitContainerControl2";
            // 
            // checkedListBoxControl
            // 
            this.checkedListBoxControl.Appearance.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkedListBoxControl.Appearance.Options.UseFont = true;
            this.checkedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxControl.ItemHeight = 25;
            this.checkedListBoxControl.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxControl.Name = "checkedListBoxControl";
            this.checkedListBoxControl.Size = new System.Drawing.Size(200, 480);
            this.checkedListBoxControl.TabIndex = 0;
            this.checkedListBoxControl.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxControl_SelectedIndexChanged);
            // 
            // panelControl
            // 
            this.panelControl.Appearance.BackColor = System.Drawing.Color.White;
            this.panelControl.Appearance.Options.UseBackColor = true;
            this.panelControl.Controls.Add(this.labprograss);
            this.panelControl.Controls.Add(this.progressBar);
            this.panelControl.Controls.Add(this.BtnBegin);
            this.panelControl.Controls.Add(this.checkController);
            this.panelControl.Controls.Add(this.checkService);
            this.panelControl.Controls.Add(this.checkMapper);
            this.panelControl.Controls.Add(this.checkModel);
            this.panelControl.Controls.Add(this.buttonPath);
            this.panelControl.Controls.Add(this.labelControl2);
            this.panelControl.Controls.Add(this.labelControl1);
            this.panelControl.Controls.Add(this.textPackage);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl.Location = new System.Drawing.Point(0, 0);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(515, 480);
            this.panelControl.TabIndex = 0;
            // 
            // labprograss
            // 
            this.labprograss.Location = new System.Drawing.Point(92, 438);
            this.labprograss.Name = "labprograss";
            this.labprograss.Size = new System.Drawing.Size(70, 14);
            this.labprograss.TabIndex = 10;
            this.labprograss.Text = "labelControl3";
            this.labprograss.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(92, 414);
            this.progressBar.MenuManager = this.ribbonControl1;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(344, 18);
            this.progressBar.TabIndex = 9;
            this.progressBar.Visible = false;
            // 
            // BtnBegin
            // 
            this.BtnBegin.Image = ((System.Drawing.Image)(resources.GetObject("BtnBegin.Image")));
            this.BtnBegin.Location = new System.Drawing.Point(193, 355);
            this.BtnBegin.Name = "BtnBegin";
            this.BtnBegin.Size = new System.Drawing.Size(127, 39);
            this.BtnBegin.TabIndex = 8;
            this.BtnBegin.Text = "开始生成";
            this.BtnBegin.Click += new System.EventHandler(this.BtnBegin_Click);
            // 
            // checkController
            // 
            this.checkController.EditValue = true;
            this.checkController.Location = new System.Drawing.Point(193, 295);
            this.checkController.MenuManager = this.ribbonControl1;
            this.checkController.Name = "checkController";
            this.checkController.Properties.Caption = "生成Controller接口层";
            this.checkController.Size = new System.Drawing.Size(151, 19);
            this.checkController.TabIndex = 7;
            // 
            // checkService
            // 
            this.checkService.EditValue = true;
            this.checkService.Location = new System.Drawing.Point(193, 255);
            this.checkService.MenuManager = this.ribbonControl1;
            this.checkService.Name = "checkService";
            this.checkService.Properties.Caption = "生成Service层";
            this.checkService.Size = new System.Drawing.Size(151, 19);
            this.checkService.TabIndex = 6;
            // 
            // checkMapper
            // 
            this.checkMapper.EditValue = true;
            this.checkMapper.Location = new System.Drawing.Point(193, 216);
            this.checkMapper.MenuManager = this.ribbonControl1;
            this.checkMapper.Name = "checkMapper";
            this.checkMapper.Properties.Caption = "生成Mapper层";
            this.checkMapper.Size = new System.Drawing.Size(151, 19);
            this.checkMapper.TabIndex = 5;
            // 
            // checkModel
            // 
            this.checkModel.EditValue = true;
            this.checkModel.Location = new System.Drawing.Point(193, 178);
            this.checkModel.MenuManager = this.ribbonControl1;
            this.checkModel.Name = "checkModel";
            this.checkModel.Properties.Caption = "生成Model层";
            this.checkModel.Size = new System.Drawing.Size(151, 19);
            this.checkModel.TabIndex = 4;
            // 
            // buttonPath
            // 
            this.buttonPath.Location = new System.Drawing.Point(193, 130);
            this.buttonPath.MenuManager = this.ribbonControl1;
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonPath.Size = new System.Drawing.Size(243, 20);
            this.buttonPath.TabIndex = 3;
            this.buttonPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonPath_ButtonClick);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(92, 133);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "生成路径：";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(116, 82);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "包名：";
            // 
            // textPackage
            // 
            this.textPackage.EditValue = "com.avatarcn";
            this.textPackage.Location = new System.Drawing.Point(193, 79);
            this.textPackage.MenuManager = this.ribbonControl1;
            this.textPackage.Name = "textPackage";
            this.textPackage.Size = new System.Drawing.Size(243, 20);
            this.textPackage.TabIndex = 0;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AllowFormGlass = DevExpress.Utils.DefaultBoolean.False;
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 627);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "Form1";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SpringBoot代码生成工具 by:kenny";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageListBoxControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl)).EndInit();
            this.panelControl.ResumeLayout(false);
            this.panelControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkController.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkService.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkMapper.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkModel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textPackage.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraEditors.ImageListBoxControl imageListBoxControl;
        private System.Windows.Forms.ImageList imageList;
        private DevExpress.XtraBars.BarButtonItem barBtnConnectDB;
        private DevExpress.XtraEditors.CheckedListBoxControl checkedListBoxControl;
        private DevExpress.XtraEditors.PanelControl panelControl;
        private DevExpress.XtraEditors.ButtonEdit buttonPath;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit textPackage;
        private DevExpress.XtraEditors.CheckEdit checkController;
        private DevExpress.XtraEditors.CheckEdit checkService;
        private DevExpress.XtraEditors.CheckEdit checkMapper;
        private DevExpress.XtraEditors.CheckEdit checkModel;
        private DevExpress.XtraEditors.SimpleButton BtnBegin;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private DevExpress.XtraEditors.ProgressBarControl progressBar;
        private DevExpress.XtraEditors.LabelControl labprograss;
    }
}

