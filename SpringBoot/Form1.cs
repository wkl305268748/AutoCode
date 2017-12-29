using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Maticsoft.DBUtility;
using MySql.Data.MySqlClient;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using SpringBoot.Model;
using SpringBoot.Code;
using SpringBoot.DB;
using SpringBoot.Auto;

namespace SpringBoot
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        int lastDbSelect = -1;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void barBtnConnectDB_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ConnectDB connectDb = new ConnectDB();
            if (connectDb.ShowDialog() == DialogResult.OK)
            {
                DbHelperMySQL.connectionString = connectDb.connectStr;

                try
                {
                    MySqlDataReader reader = DbHelperMySQL.ExecuteReader("show databases;");

                    imageListBoxControl.Items.Clear();
                    while (reader.Read())
                    {
                        imageListBoxControl.Items.Add(reader.GetString(0), 0);
                    }
                    reader.Close();
                }
                catch (Exception ee) {
                    MessageBox.Show("数据库连接失败" + ee.Message);
                }

            }
        }

        private void imageListBoxControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string dbName = imageListBoxControl.Items[imageListBoxControl.SelectedIndex].Value.ToString();
            imageListBoxControl.Items[imageListBoxControl.SelectedIndex].ImageIndex = 1;
            if (lastDbSelect != -1){
                imageListBoxControl.Items[lastDbSelect].ImageIndex = 0;
            }
            lastDbSelect = imageListBoxControl.SelectedIndex;
            MySqlDataReader reader = DbHelperMySQL.ExecuteReader(string.Format("show tables from {0};",dbName));

            checkedListBoxControl.Items.Clear();
            while (reader.Read())
            {
                checkedListBoxControl.Items.Add(reader.GetString(0), true);
            }
            reader.Close();
        }

        private void BtnBegin_Click(object sender, EventArgs e)
        {
            if (imageListBoxControl.Items.Count == 0)
            {
                MessageBox.Show("请连接数据库");
                return;
            }
            if (checkedListBoxControl.Items.Count == 0)
            {
                MessageBox.Show("请选择表");
                return;
            }
            if (textPackage.Text.Equals(""))
            {
                MessageBox.Show("请输入包名");
                return;
            }
            if (buttonPath.Text.Equals(""))
            {
                MessageBox.Show("请选择输出路径");
                return;
            }


            if (!backgroundWorker.IsBusy)
            {
                //显示控件
                progressBar.Visible = true;
                labprograss.Visible = true;
                progressBar.Properties.Maximum = checkedListBoxControl.Items.Count;

                backgroundWorker.RunWorkerAsync();
            }
        }



        private void buttonPath_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK) {
                buttonPath.Text = folderBrowserDialog.SelectedPath;
            }
        }
        

        private void checkedListBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            string dbName = imageListBoxControl.Items[imageListBoxControl.SelectedIndex].Value.ToString();
            string tableName = checkedListBoxControl.Items[checkedListBoxControl.SelectedIndex].Value.ToString();

            string sql = "select column_name,data_type,column_key,character_maximum_length,column_comment from information_schema.COLUMNS where table_name = '{0}' and table_schema = '{1}';";
            MySqlDataReader reader = DbHelperMySQL.ExecuteReader(string.Format(sql,tableName,dbName));
            
            while (reader.Read())
            {
                Console.WriteLine(reader.GetString(0)+"  "+ reader.GetString(1)+"  "+ reader.GetString(2));
            }*/
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //遍历表
            int index = 0;
            foreach (CheckedListBoxItem item in checkedListBoxControl.Items)
            {
                index++;
                if (item.CheckState == CheckState.Checked)
                {
                    //UI刷新
                    ((BackgroundWorker)sender).ReportProgress(index,item.Value);
                    DbTable dbTalbe = new DbTable();
                    dbTalbe.Name = item.Value.ToString();
                    
                    //查询字段
                    string dbName = imageListBoxControl.Items[imageListBoxControl.SelectedIndex].Value.ToString();
                    string tableName = item.Value.ToString();

                    string sql0 = string.Format("SHOW TABLE STATUS FROM {0} WHERE Name='{1}';", dbName,tableName);
                    MySqlDataReader reader0 = DbHelperMySQL.ExecuteReader(sql0);
                    while (reader0.Read())
                    {
                        dbTalbe.Notes = reader0.GetString("Comment");
                    }
                    reader0.Close();

                    //查询字段属性
                    string sql = "select * from information_schema.COLUMNS where table_name = '{0}' and table_schema = '{1}';";
                    MySqlDataReader reader = DbHelperMySQL.ExecuteReader(string.Format(sql, tableName, dbName));

                    while (reader.Read())
                    {
                        DbColumn column = new DbColumn();
                        column.Name = reader.GetString("COLUMN_NAME");
                        column.Type = reader.GetString("DATA_TYPE");
                        column.IsKey = reader.GetString("COLUMN_KEY").Equals("PRI") ? true : false;
                        column.Notes = reader.GetString("COLUMN_COMMENT");
                        column.IsNotNull = reader.GetString("IS_NULLABLE").Equals("YES") ? false : true;
                        column.IsFkey = false;
                        dbTalbe.Column.Add(column);
                    }
                    reader.Close();

                    //查询外键关系
                    string sql1 = "select * from information_schema.KEY_COLUMN_USAGE where table_name = '{0}' and table_schema = '{1}';";
                    MySqlDataReader reader1 = DbHelperMySQL.ExecuteReader(string.Format(sql1, tableName, dbName));

                    while (reader1.Read())
                    {
                        string fkeyName = reader1.GetString("CONSTRAINT_NAME");
                        if (!fkeyName.Equals("PRIMARY")) {
                            string columnName = reader1.GetString("COLUMN_NAME");
                            foreach (DbColumn items in dbTalbe.Column) {
                                if (items.Name.Equals(columnName)) {
                                    items.IsFkey = true;
                                    items.FkTable = reader1.GetString("REFERENCED_TABLE_NAME");
                                    items.FkColumn = reader1.GetString("REFERENCED_COLUMN_NAME");
                                }
                            }
                        }
                    }
                    reader1.Close();


                    //创建Model
                    if (checkModel.Checked)
                    {
                        AutoModel.CreateModel(dbTalbe, textPackage.Text, buttonPath.Text);
                        AutoModelSwagger.CreateModel(dbTalbe, textPackage.Text, buttonPath.Text);
                    }
                    //创建Mapper
                    if (checkMapper.Checked)
                    {
                        AutoMapper.CreateMapper(dbTalbe, textPackage.Text, buttonPath.Text);
                    }
                    //创建Service
                    if (checkService.Checked)
                    {
                        AutoService.CreateService(dbTalbe, textPackage.Text, buttonPath.Text);
                    }
                    //创建Controller
                    if (checkController.Checked)
                    {
                        AutoController.CreateController(dbTalbe, textPackage.Text, buttonPath.Text);
                        AutoFegin.CreateFeign(dbTalbe, textPackage.Text, buttonPath.Text);
                    }
                }
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Position = e.ProgressPercentage;
            labprograss.Text = "正在生成表：" + (string)e.UserState;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Visible = false;
            labprograss.Visible = false;
            MessageBox.Show("生成成功！");
            System.Diagnostics.Process.Start("explorer.exe", @buttonPath.Text);

        }
    }
}
