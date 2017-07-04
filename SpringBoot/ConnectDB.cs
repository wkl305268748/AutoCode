using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace SpringBoot
{
    public partial class ConnectDB : DevExpress.XtraEditors.XtraForm
    {
        public string connectStr = "";

        public ConnectDB()
        {
            InitializeComponent();
        }

        private void BtnSuccess_Click(object sender, EventArgs e)
        {
            //connectStr = String.Format("server={0};user id={1};password={2};port={3};", texthost.Text, textuser.Text, textpass.Text,textport.Text);
            connectStr = String.Format("server={0};user id={1};password={2};port={3};", "rm-bp1d79mlswd465ue0o.mysql.rds.aliyuncs.com", textuser.Text, "Avatar123", textport.Text);
            
        }


    }
}