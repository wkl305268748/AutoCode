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
                MySqlDataReader reader = DbHelperMySQL.ExecuteReader("show databases;");

                imageListBoxControl.Items.Clear();
                while (reader.Read())
                {
                    imageListBoxControl.Items.Add(reader.GetString(0), 0);
                }
                reader.Close();

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

                    List <Field> fieldList = new List<Field>();
                    //查询字段
                    string dbName = imageListBoxControl.Items[imageListBoxControl.SelectedIndex].Value.ToString();
                    string tableName = item.Value.ToString();

                    string sql = "select column_name,data_type,column_key,character_maximum_length,column_comment from information_schema.COLUMNS where table_name = '{0}' and table_schema = '{1}';";
                    MySqlDataReader reader = DbHelperMySQL.ExecuteReader(string.Format(sql, tableName, dbName));

                    while (reader.Read())
                    {
                        Field field = new Field();
                        field.Name = reader.GetString("column_name");
                        field.Type = reader.GetString("data_type");
                        if (reader.GetString("column_key").Equals("PRI"))
                            field.IsKey = true;
                        else
                            field.IsKey = false;


                        field.Notes = reader.GetString("column_comment");
                        fieldList.Add(field);
                    }
                    //创建Model
                    if (checkModel.Checked)
                    {
                        ModelHelp.CreateModel(buttonPath.Text, textPackage.Text, tableName, fieldList);
                    }
                    //创建Mapper
                    if (checkMapper.Checked)
                    {
                        MapperHelp.CreateMapper(buttonPath.Text, textPackage.Text, tableName, fieldList);
                    }
                    //创建Service
                    if (checkService.Checked)
                    {
                        ServiceHelp.CreateService(buttonPath.Text, textPackage.Text, tableName, fieldList);
                    }
                    //创建Controller
                    if (checkController.Checked)
                    {
                        ControllerHelp.CreateController(buttonPath.Text, textPackage.Text, tableName, fieldList);
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
