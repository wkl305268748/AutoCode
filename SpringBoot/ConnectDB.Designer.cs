namespace SpringBoot
{
    partial class ConnectDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectDB));
            this.BtnSuccess = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.texthost = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textport = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textuser = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.textpass = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.texthost.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textport.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textuser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textpass.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSuccess
            // 
            this.BtnSuccess.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.BtnSuccess.Appearance.Options.UseFont = true;
            this.BtnSuccess.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnSuccess.Image = ((System.Drawing.Image)(resources.GetObject("BtnSuccess.Image")));
            this.BtnSuccess.Location = new System.Drawing.Point(238, 461);
            this.BtnSuccess.Name = "BtnSuccess";
            this.BtnSuccess.Size = new System.Drawing.Size(96, 41);
            this.BtnSuccess.TabIndex = 0;
            this.BtnSuccess.Text = "连接";
            this.BtnSuccess.Click += new System.EventHandler(this.BtnSuccess_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancel.Image")));
            this.BtnCancel.Location = new System.Drawing.Point(360, 461);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(96, 41);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "取消";
            // 
            // texthost
            // 
            this.texthost.EditValue = "rm-bp1d79mlswd465ue0o.mysql.rds.aliyuncs.com";
            this.texthost.Location = new System.Drawing.Point(220, 77);
            this.texthost.Name = "texthost";
            this.texthost.Size = new System.Drawing.Size(221, 20);
            this.texthost.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(46, 80);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(107, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "主机名或者IP地址：";
            // 
            // textport
            // 
            this.textport.EditValue = "3306";
            this.textport.Location = new System.Drawing.Point(220, 127);
            this.textport.Name = "textport";
            this.textport.Properties.Mask.EditMask = "d";
            this.textport.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.textport.Size = new System.Drawing.Size(96, 20);
            this.textport.TabIndex = 7;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(46, 130);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "端口号：";
            // 
            // textuser
            // 
            this.textuser.EditValue = "root";
            this.textuser.Location = new System.Drawing.Point(220, 173);
            this.textuser.Name = "textuser";
            this.textuser.Size = new System.Drawing.Size(96, 20);
            this.textuser.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(46, 176);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(48, 14);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "用户名：";
            // 
            // textpass
            // 
            this.textpass.EditValue = "Avatar123";
            this.textpass.Location = new System.Drawing.Point(220, 218);
            this.textpass.Name = "textpass";
            this.textpass.Size = new System.Drawing.Size(96, 20);
            this.textpass.TabIndex = 11;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(46, 221);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(36, 14);
            this.labelControl5.TabIndex = 10;
            this.labelControl5.Text = "密码：";
            // 
            // ConnectDB
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 518);
            this.Controls.Add(this.textpass);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.textuser);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.textport);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.texthost);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnSuccess);
            this.Name = "ConnectDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConnectDB";
            ((System.ComponentModel.ISupportInitialize)(this.texthost.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textport.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textuser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textpass.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BtnSuccess;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.TextEdit texthost;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textport;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textuser;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit textpass;
        private DevExpress.XtraEditors.LabelControl labelControl5;
    }
}