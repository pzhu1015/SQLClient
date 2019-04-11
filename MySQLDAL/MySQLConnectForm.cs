using Helper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySQLDAL.Properties;
using SQLDAL;
using System.Data.Common;

namespace MySQLDAL
{
    public partial class MySQLConnectForm : Form, IConnectionForm
    {
        private string user;
        private string password;
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

        public MySQLConnectForm()
        {
            InitializeComponent();
        }

        private void MySqlConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.connectionString = $"Database={this.txtDatabase.Text};Data Source={this.txtHost.Text};User Id={this.txtUser.Text};Password={this.txtPassword.Text};port={this.txtPort.Text}";
            this.user = this.txtUser.Text;
            this.password = this.txtPassword.Text;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection connection = new MySqlConnection($"Database={this.txtDatabase.Text};Data Source={this.txtHost.Text};User Id={this.txtUser.Text};Password={this.txtPassword.Text};port={this.txtPort.Text}"))
                {
                    connection.Open();
                    MessageBox.Show(this, Resources.connect_success, Resources.prompt, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    connection.Close();
                }
            }
            catch(Exception ex)
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
            MySqlConnection connection = new MySqlConnection(info.ConnectionString);
            this.txtHost.Text = connection.DataSource;
            this.txtDatabase.Text = connection.Database;
            return true;
        }
    }
}
