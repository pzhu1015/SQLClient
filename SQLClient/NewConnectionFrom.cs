using System;
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
                DataTable dt = ConnectInfo.LoadDriver();
                foreach(DataRow dr in dt.Rows)
                {
                    AccordionControlElement element = new AccordionControlElement(ElementStyle.Item);
                    ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(dr["assemblyName"].ToString(), dr["namespaceName"].ToString(), dr["className"].ToString());
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

       
        private void txtConnectionString_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (this.accDataSource.SelectedElement == null) return;
                Form form = this.connectionInfo.ConnectForm;
                IConnectionForm connInfo = form as IConnectionForm;
                connInfo.LoadConnectionInfo(this.connectionInfo);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.connectionInfo.ConnectionString = connInfo.ConnectionString;
                    this.connectionInfo.User = connInfo.User;
                    this.connectionInfo.Password = connInfo.Password;
                    this.connectionInfo.File = connInfo.File;
                    this.connectionInfo.Host = connInfo.Host;
                    this.connectionInfo.Port = connInfo.Port;
                    this.txtConnectionString.Text = connInfo.ConnectionString;
                }
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
                this.connectionInfo = info.Clone();
                this.txtConnectionString.Text = "";
                this.pg.SelectedObject = this.connectionInfo;
            }
            else
            {
                this.btnOK.Enabled = false;
            }
        }

        private void tsbtnAddDriver_Click(object sender, EventArgs e)
        {
            try
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
                        this.accDataSource.SelectedElement = element;
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnUpdateDriver_Click(object sender, EventArgs e)
        {
        }

        private void tsbtnDeleteDriver_Click(object sender, EventArgs e)
        {
            if (this.accDataSource.SelectedElement != null)
            {
                ConnectInfo info = this.pg.SelectedObject as ConnectInfo;
                this.tsbtnDeleteDriver.Enabled = false;
                if (MessageBox.Show(this, Resources.is_delete_driver, Resources.prompt, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    bool rslt = ConnectInfo.RemoveDriver(info.DriverName);
                    if (rslt)
                    {
                        this.accDataSource.Elements[0].Elements.Remove(this.accDataSource.SelectedElement);
                        if (this.accDataSource.Elements[0].Elements.Count > 0)
                        {
                            this.accDataSource.SelectedElement = this.accDataSource.Elements[0].Elements[0];
                        }
                    }
                }
                this.tsbtnDeleteDriver.Enabled = true;
            }
        }
    }
}