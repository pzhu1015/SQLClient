using Helper;
using SQLClient.Properties;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLClient
{
    public partial class UpLoadDriverForm : Form
    {
        private ConnectInfo connectInfo;

        public ConnectInfo ConnectInfo
        {
            get
            {
                return connectInfo;
            }

            set
            {
                connectInfo = value;
            }
        }

        public UpLoadDriverForm()
        {
            InitializeComponent();
        }

        private void UpLoadDriverForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.txtDriverName.Text == "" || this.txtAssemblyName.Text == "" || this.txtNameSpace.Text == "" || this.txtClassName.Text == "")
                {
                    this.tipMessage.Show("driverName, assemblyName, nameSpace, className不能为空", this, this.Location, 2000);
                    e.Cancel = true;
                }
                else
                {
                    //TODO check the DAL and driver invalid, if not return error, otherwise add to driver list
                    try
                    {
                        ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(this.txtAssemblyName.Text, this.txtNameSpace.Text, this.txtClassName.Text);
                        if (info == null)
                        {
                            e.Cancel = true;
                            this.tipMessage.Show("驱动验证失败", this, this.Location, 2000);
                            return;
                        }
                        else
                        {
                            this.connectInfo = info;
                            this.connectInfo.AssemblyName = this.txtAssemblyName.Text;
                            this.connectInfo.NamespaceName = this.txtNameSpace.Text;
                            this.connectInfo.ClassName = this.txtClassName.Text;
                            this.connectInfo.DriverName = this.txtDriverName.Text;
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        LogHelper.Error(ex);
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }


        private void btnDriverPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = Resources.select_driver;
                ofd.Filter = $"{Resources.dynamic_libaray}(*.dll)|*.dll";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filename = ofd.FileName;
                    FileInfo info = new FileInfo(filename);
                    //info.CopyTo($@"{Application.StartupPath}\{info.Name}");
                    info.CopyTo($@"{Application.StartupPath}\{info.Name}", true);
                    this.btnDriverPath.Text = filename;
                    this.tipMessage.Show("驱动上传成功", this.btnDriverPath, this.btnDriverPath.Location, 2000);
                }
            }
            catch (Exception ex)
            {
                this.tipMessage.Show("驱动上传失败", this.btnDriverPath, this.btnDriverPath.Location, 2000);
                LogHelper.Error(ex);
            }
        }

        private void btnDALPath_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = Resources.select_driver;
                ofd.Filter = $"{Resources.dynamic_libaray}(*.dll)|*.dll";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filename = ofd.FileName;
                    FileInfo info = new FileInfo(filename);
                    //info.CopyTo($@"{Application.StartupPath}\{info.Name}");
                    info.CopyTo($@"{Application.StartupPath}\{info.Name}", true);
                    this.btnDALPath.Text = filename;
                    this.tipMessage.Show("数据访问层上传成功", this.btnDALPath, this.btnDALPath.Location, 2000);
                }
            }
            catch (Exception ex)
            {
                this.tipMessage.Show("数据访问层上传失败", this.btnDALPath, this.btnDALPath.Location, 2000);
                LogHelper.Error(ex);
            }
        }
    }
}
