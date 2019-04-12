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
                DataTable dt = ConnectInfo.LoadConfig();
                foreach(DataRow dr in dt.Rows)
                {
                    AccordionControlElement element = new AccordionControlElement(ElementStyle.Item);
                    ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(dr["assemblyName"].ToString(), dr["namespaceName"].ToString(), dr["className"].ToString());
                    info.DriverName = dr["name"].ToString();
                    info.AssemblyName = dr["assemblyName"].ToString();
                    info.ClassName = dr["className"].ToString();
                    info.NamespaceName = dr["namespaceName"].ToString();
                    info.DesignTable = dr["designTable"].ToString();
                    info.OpenTable = dr["openTable"].ToString();
                    info.OpenView = dr["openView"].ToString();
                    info.DataTypes = dr["dataTypes"].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                    element.Text = info.DriverName;
                    element.Image = info.OpenImage;
                    element.Tag = info;
                    this.accDataSource.Elements[0].Elements.Add(element);
                }
                this.accDataSource.SelectedElement = this.accDataSource.Elements[0].Elements[0];

                if (this.accDataSource.SelectedElement != null)
                {
                    this.btnAdvance.Enabled = true;
                    this.btnOK.Enabled = true;
                }
                else
                {
                    this.btnAdvance.Enabled = false;
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

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.accDataSource.SelectedElement == null) return;
                ConnectInfo info = this.accDataSource.SelectedElement.Tag as ConnectInfo;
                Form form = info.GetConnectForm();
                IConnectionForm connInfo = form as IConnectionForm;
                connInfo.LoadConnectionInfo(info);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    info.ConnectionString = connInfo.ConnectionString;
                    info.User = connInfo.User;
                    info.Password = connInfo.Password;
                    this.connectionInfo = info;
                    this.txtConnectionString.Text = connInfo.ConnectionString;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void accDataSource_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (this.accDataSource.SelectedElement != null)
            {
                this.btnAdvance.Enabled = true;
                this.btnOK.Enabled = true;
                ConnectInfo info = this.accDataSource.SelectedElement.Tag as ConnectInfo;
                if (this.connectionInfo != null && this.connectionInfo.DriverName != info.DriverName)
                {
                    this.connectionInfo = null;
                }
                this.pg.SelectedObject = info;
            }
            else
            {
                this.btnAdvance.Enabled = false;
                this.btnOK.Enabled = false;
            }
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            UpLoadDriverForm form = new UpLoadDriverForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                //TODO get the DAL and Driver info then to insert driver list
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
    }
}