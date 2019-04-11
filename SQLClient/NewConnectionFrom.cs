using System;
using System.Windows.Forms;
using DevExpress.XtraBars.Navigation;
using Helper;
using SQLDAL;
using System.IO;
using System.Data;

namespace SQLClient
{
    public partial class NewConnectionFrom : DevExpress.XtraEditors.XtraForm
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
                    string name = dr["name"].ToString();
                    element.Text = name;
                    element.Tag = dr;
                    this.accDataSource.Elements[0].Elements.Add(element);
                    TreeNode dbNodeEdit = new TreeNode(name);
                    dbNodeEdit.Tag = dr;
                    this.tvDataSourceEdit.Nodes.Add(dbNodeEdit);
                }

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

        private void btnUpLoad_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = Resource.select_driver;
            ofd.Filter = $"{Resource.dynamic_libaray}(*.dll)|*.dll";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                FileInfo info = new FileInfo(filename);
                info.CopyTo($@"{Application.StartupPath}\{info.Name}");
            }
        }

        private void btnAdvance_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.accDataSource.SelectedElement == null) return;
                DataRow dr = this.accDataSource.SelectedElement.Tag as DataRow;
                ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(dr["assemblyName"].ToString(), dr["namespaceName"].ToString(), dr["className"].ToString());
                info.AssemblyName = dr["assemblyName"].ToString();
                info.ClassName = dr["className"].ToString();
                info.NamespaceName = dr["namespaceName"].ToString();
                Form form = info.GetConnectForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    IConnectionForm connInfo = form as IConnectionForm;
                    info.ConnectionString = connInfo.ConnectionString;
                    info.User = connInfo.User;
                    info.Password = connInfo.Password;
                    this.connectionInfo = info;
                    this.txtConnectionString.Text = this.connectionInfo.ConnectionString;
                    info.DesignTable = dr["designTable"].ToString();
                    info.OpenTable = dr["openTable"].ToString();
                    info.OpenView = dr["openView"].ToString();
                    info.DataTypes = dr["dataTypes"].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
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
                DataRow dr = this.accDataSource.SelectedElement.Tag as DataRow;
                if (this.connectionInfo != null && this.connectionInfo.ClassName != dr["className"].ToString())
                {
                    this.connectionInfo = null;
                }
            }
            else
            {
                this.btnAdvance.Enabled = false;
                this.btnOK.Enabled = false;
            }
        }
    }
}