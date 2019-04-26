﻿using System;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using Helper;
using SQLDAL;
using System.IO;
using System.Data;
using SQLClient.Properties;

namespace SQLClient
{
    public partial class NewConnectionFrom : Form
    {
        private ConnectInfo connectionInfo;

        public ConnectInfo ConnectionInfo
        {
            get
            {
                return connectionInfo;
            }

            set
            {
                connectionInfo = value;
            }
        }

        public NewConnectionFrom()
        {
            InitializeComponent();
           
        }

        private void NewConnectionFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                if (this.txtConnectName.Text != "" && this.connectionInfo != null)
                {
                    this.connectionInfo.Name = this.txtConnectName.Text;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void NewConnectionFrom_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ConnectInfo.LoadConfig();
                foreach(DataRow dr in dt.Rows)
                {
                    AccordionControlElement element = new AccordionControlElement(ElementStyle.Item);
                    ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(dr["assemblyName"].ToString(), dr["namespaceName"].ToString(), dr["className"].ToString());
                    info.DesignTableScript = dr["designTable"].ToString();
                    info.OpenTableScript = dr["openTable"].ToString();
                    info.OpenViewScript = dr["openView"].ToString();
                    info.LoadTableScript = dr["loadTable"].ToString();
                    info.LoadViewScript = dr["loadView"].ToString();
                    info.DataTypes = dr["dataTypes"].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    element.Text = info.DriverName;
                    element.Image = info.OpenImage;
                    element.Tag = info;
                    this.accDataSource.Elements[0].Elements.Add(element);
                }
                this.accDataSource.SelectedElement = this.accDataSource.Elements[0].Elements[0];

                if (this.accDataSource.SelectedElement != null)
                {
                    this.btnOK.Enabled = true;
                }
                else
                {
                    this.btnOK.Enabled = false;
                }

                this.txtConnectName.Text = "";
                this.txtConnectionString.Text = "";
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void accDataSource_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (this.accDataSource.SelectedElement != null)
            {
                this.btnOK.Enabled = true;
                ConnectInfo info = this.accDataSource.SelectedElement.Tag as ConnectInfo;
                if (this.connectionInfo != null && this.connectionInfo.DriverName != info.DriverName)
                {
                    this.connectionInfo = null;
                    this.txtConnectionString.Text = "";
                }
                this.pg.SelectedObject = info;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            UpLoadDriverForm form = new UpLoadDriverForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                bool rslt = ConnectInfo.AddDriver(form.ConnectInfo);
                if (rslt)
                {
                    AccordionControlElement element = new AccordionControlElement(ElementStyle.Item);
                    element.Text = form.ConnectInfo.DriverName;
                    element.Image = form.ConnectInfo.OpenImage;
                    element.Tag = form.ConnectInfo;
                    this.accDataSource.Elements[0].Elements.Add(element);
                }
            }
        }

        private void txtConnectionString_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (this.accDataSource.SelectedElement == null) return;
                ConnectInfo info = null;
                if (this.connectionInfo == null)
                {
                    info = this.accDataSource.SelectedElement.Tag as ConnectInfo;
                    info = ReflectionHelper.CreateInstance<ConnectInfo>(info.AssemblyName, info.NamespaceName, info.ClassName);
                    info.Port = info.DefaultPort;
                }
                else
                {
                    info = this.connectionInfo;
                }
                Form form = info.ConnectForm;
                IConnectionForm connInfo = form as IConnectionForm;
                connInfo.LoadConnectionInfo(info);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    info.ConnectionString = connInfo.ConnectionString;
                    info.User = connInfo.User;
                    info.Password = connInfo.Password;
                    info.File = connInfo.File;
                    info.Host = connInfo.Host;
                    info.Port = connInfo.Port;
                    this.connectionInfo = info;
                    this.txtConnectionString.Text = connInfo.ConnectionString;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }
}