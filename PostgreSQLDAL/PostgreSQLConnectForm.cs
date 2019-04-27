using Helper;
using Npgsql;
using PostgreSQLDAL.Properties;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostgreSQLDAL
{
    internal sealed partial class PostgreSQLConnectForm : Form, IConnectionForm
    {
        private string password;
        private string user;
        private string file;
        private string host;
        private string port;
        private string connectionString;

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

        public PostgreSQLConnectForm()
        {
            InitializeComponent();
        }

        

        public bool LoadConnectionInfo(ConnectInfo info)
        {
            this.user = info.User;
            this.txtUser.Text = this.user;
            this.password = info.Password;
            this.txtPassword.Text = this.password;
            NpgsqlConnection connection = new NpgsqlConnection(info.ConnectionString);
            this.txtHost.Text = info.Host;
            this.txtPort.Text = string.IsNullOrEmpty(info.Port) ? info.DefaultPort : info.Port;
            this.txtDatabase.Text = connection.Database;
            return true;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection connection = new NpgsqlConnection($"PORT={this.txtPort.Text};DATABASE={this.txtDatabase.Text};HOST={this.txtHost.Text};PASSWORD={this.txtPassword.Text};USER ID={this.txtUser.Text}"))
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

        private void PostgreSQLConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.connectionString = $"PORT={this.txtPort.Text};DATABASE={this.txtDatabase.Text};HOST={this.txtHost.Text};PASSWORD={this.txtPassword.Text};USER ID={this.txtUser.Text}";
            this.user = this.txtUser.Text;
            this.password = this.txtPassword.Text;
            this.host = this.txtHost.Text;
            this.port = this.txtPort.Text;
        }
    }
}
