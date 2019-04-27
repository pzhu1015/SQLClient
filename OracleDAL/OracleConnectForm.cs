using Helper;
using Oracle.ManagedDataAccess.Client;
using OracleDAL.Properties;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OracleDAL
{
    internal sealed partial class OracleConnectForm : Form, IConnectionForm
    {
        private string user;
        private string password;
        private string file;
        private string host;
        private string port;
        private string connectionString;

        public string User
        {
            get
            {
                return this.user;
            }

            set
            {
                this.user = value;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }

            set
            {
                this.connectionString = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
            }
        }

        public string File
        {
            get
            {
                return file;
            }

            set
            {
                file = value;
            }
        }

        public string Host
        {
            get
            {
                return host;
            }

            set
            {
                host = value;
            }
        }

        public string Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }

        public OracleConnectForm()
        {
            InitializeComponent();
        }

        private void OracleConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.connectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={this.txtHost.Text})(PORT={this.txtPort.Text}))(CONNECT_DATA=(SERVICE_NAME={this.txtSeviceName.Text})));Persist Security Info=True;User ID={this.txtUser.Text};Password={this.txtPassword.Text};";
            this.user = this.txtUser.Text;
            this.password = this.txtPassword.Text;
            this.host = this.txtHost.Text;
            this.port = this.txtPort.Text;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection connection = new OracleConnection($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={this.txtHost.Text})(PORT={this.txtPort.Text}))(CONNECT_DATA=(SERVICE_NAME={this.txtSeviceName.Text})));Persist Security Info=True;User ID={this.txtUser.Text};Password={this.txtPassword.Text};"))
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

        public bool LoadConnectionInfo(ConnectInfo info)
        {
            this.user = info.User;
            this.txtUser.Text = this.user;
            this.password = info.Password;
            this.txtPassword.Text = this.password;
            this.txtHost.Text = info.Host;
            this.txtPort.Text = string.IsNullOrEmpty(info.Port) ? info.DefaultPort : info.Port;
            return true;
        }
    }
}
