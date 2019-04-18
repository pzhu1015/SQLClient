using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SQLDAL;
using System.Data.OleDb;
using System.Data.Common;
using AccessDAL.Properties;
using Helper;
using ADOX;

namespace AccessDAL
{
    public partial class AccessConnectForm : Form, IConnectionForm
    {
        private string user;
        private string password;
        private string connectionString;
        public AccessConnectForm()
        {
            InitializeComponent();
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                connectionString = value;
            }
        }

        public string User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        private void AccessConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.user = this.txtUser.Text;
            this.password = this.txtPassword.Text;
            this.connectionString = $"Provider=Microsoft.Jet.OLEDB.4.0; Data Source={this.txtDataSource.Text}; Jet OLEDB:Database Password={this.password}";
        }

        public bool LoadConnectionInfo(ConnectInfo info)
        {
            this.user = info.User;
            this.txtUser.Text = this.user;
            this.password = info.Password;
            this.txtPassword.Text = this.password;
            OleDbConnection connection = new OleDbConnection(info.ConnectionString);
            this.txtDataSource.Text = connection.DataSource;
            return true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //using (DbConnection connection = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0; Data Source={this.txtDataSource.Text}; Jet OLEDB:Database Password={this.txtPassword.Text}"))
                using (DbConnection connection = new OleDbConnection($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={this.txtDataSource.Text};Jet OLEDB:Database Password={this.txtPassword.Text}"))
                {
                    connection.Open();
                    MessageBox.Show(this, Resources.connect_success, Resources.prompt, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                MessageBox.Show(this, ex.Message, Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AccessConnectForm_Load(object sender, EventArgs e)
        {
            this.cmbOpr.SelectedIndex = 0;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.cmbOpr.SelectedIndex == 0)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "SQLite Database file(*.mdb;*.accdb)|*.mdb;*.accdb|全部文件(*.*)|*.*";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    this.txtDataSource.Text = openFile.FileName;
                }
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "SQLite Database file(*.mdb;*.accdb)|*.mdb;*.accdb|全部文件(*.*)|*.*";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    Catalog catalog = new Catalog();
                    catalog.Create($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={saveFile.FileName};Jet OLEDB:Engine Type=5");
                    this.txtDataSource.Text = saveFile.FileName;
                }
            }
        }
    }
}
