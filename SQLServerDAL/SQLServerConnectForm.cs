﻿using Helper;
using SQLDAL;
using SQLServerDAL.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLServerDAL
{
    public partial class SQLServerConnectForm : Form, IConnectionForm
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

        public SQLServerConnectForm()
        {
            InitializeComponent();
        }

        private void SqlServerConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.user = this.txtUser.Text;
            this.password = this.txtPassword.Text;
            this.connectionString = $"server={this.txtHost.Text};uid={this.txtUser.Text};pwd={this.txtPassword.Text}";
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection connection = new SqlConnection($"server={this.txtHost.Text};uid={this.txtUser.Text};pwd={this.txtPassword.Text}"))
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
            SqlConnection connection = new SqlConnection(info.ConnectionString);
            this.txtHost.Text = connection.DataSource;
            return true;
        }
    }
}