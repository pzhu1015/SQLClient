using Helper;
using SQLDAL;
using SQLiteDAL.Properties;
using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SQLiteDAL
{
    public partial class SQLiteConnectForm : Form, IConnectionForm
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

        public SQLiteConnectForm()
        {
            InitializeComponent();
        }      

        private void SQLiteConnectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.user = this.txtUser.Text;
            this.password = this.txtPassword.Text;
            this.connectionString = $"Data Source={this.txtDataSource.Text}";
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection($"Data Source={this.txtDataSource.Text}"))
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.cmbOpr.SelectedIndex == 0)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "SQLite Database file(*.db;*.db3;*.sqlite;*.sqlite3)|*.db;*.db3;*.sqlite;*.sqlite3|全部文件(*.*)|*.*";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    this.txtDataSource.Text = openFile.FileName;
                }
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "SQLite Database file(*.db;*.db3;*.sqlite;*.sqlite3)|*.db;*.db3;*.sqlite;*.sqlite3|全部文件(*.*)|*.*";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    SQLiteConnection.CreateFile(saveFile.FileName);
                    this.txtDataSource.Text = saveFile.FileName;
                }
            }
        }

        private void SQLiteConnectForm_Load(object sender, EventArgs e)
        {
            this.cmbOpr.SelectedIndex = 0;
        }

        public bool LoadConnectionInfo(ConnectInfo info)
        {
            this.user = info.User;
            this.txtUser.Text = this.user;
            this.password = info.Password;
            this.txtPassword.Text = this.password;
            SQLiteConnection connection = new SQLiteConnection(info.ConnectionString);
            this.txtDataSource.Text = connection.DataSource;
            return true;
        }
    }
}
