using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.ComponentModel;

namespace SQLDAL
{
    public abstract class ConnectInfo : IConnectInfo
    {
        public static string LocalConnectionString = $@"Data Source = {Application.StartupPath}\AppData\SQL.db";
        public event CloseConnectEventHandler CloseConnect;
        public event OpenConnectEventHandler OpenConnect;
        protected string driverName;
        protected string name;
        protected string user;
        protected string password;
        protected string assemblyName;
        protected string namespaceName;
        protected string className;
        protected string connectionString;
        protected string designTable;
        protected string openTable;
        protected string openView;
        protected string[] dataTypes;
        protected string message;
        protected TreeNode node;
        protected bool isOpen = false;
        
        protected List<DatabaseInfo> databases = new List<DatabaseInfo>();

        [Browsable(false)]
        public List<DatabaseInfo> Databases
        {
            get
            {
                return databases;
            }

            set
            {
                databases = value;
            }
        }

        [Browsable(false)]
        public bool IsOpen
        {
            get
            {
                return isOpen;
            }

            set
            {
                isOpen = value;
            }
        }

        [Browsable(false)]
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        [Browsable(false)]
        [Description("连接字符串")]
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

        [Browsable(false)]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        [Browsable(true)]
        [Category("基本")]
        [Description("驱动程序集名称")]
        public string AssemblyName
        {
            get
            {
                return assemblyName;
            }

            set
            {
                assemblyName = value;
            }
        }

        [Browsable(true)]
        [Category("基本")]
        [Description("驱动程序命令空间")]
        public string NamespaceName
        {
            get
            {
                return namespaceName;
            }

            set
            {
                namespaceName = value;
            }
        }

        [Browsable(true)]
        [Category("基本")]
        [Description("驱动程序连接信息类名")]
        public string ClassName
        {
            get
            {
                return className;
            }

            set
            {
                className = value;
            }
        }

        [Browsable(false)]
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

        [Browsable(false)]
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

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库查看表设计的脚本模版")]
        public string DesignTable
        {
            get
            {
                return designTable;
            }

            set
            {
                designTable = value;
            }
        }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库打开表的脚本模版")]
        public string OpenTable
        {
            get
            {
                return openTable;
            }

            set
            {
                openTable = value;
            }
        }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库支持的所有数据类型集合")]
        public string[] DataTypes
        {
            get
            {
                return dataTypes;
            }

            set
            {
                dataTypes = value;
            }
        }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库打开视图的脚本模版")]
        public string OpenView
        {
            get
            {
                return openView;
            }

            set
            {
                openView = value;
            }
        }

        [Browsable(false)]
        public TreeNode Node
        {
            get
            {
                return node;
            }

            set
            {
                node = value;
            }
        }

        [Browsable(true)]
        [Category("基本")]
        [Description("当前数据库的驱动程序名称")]
        public string DriverName
        {
            get
            {
                return driverName;
            }

            set
            {
                driverName = value;
            }
        }

        protected virtual void OnCloseConnect(CloseConnectEventArgs e)
        {
            if (this.CloseConnect != null)
            {
                this.CloseConnect(this, e);
            }
        }

        protected virtual void OnOpenConnect(OpenConnectEventArgs e)
        {
            if (this.OpenConnect != null)
            {
                this.OpenConnect(this, e);
            }
        }

        public abstract bool Open();
        public abstract void Create(string name);
        public abstract void Drop(string name);
        public abstract Form GetConnectForm();
        public abstract Image CloseImage();
        public abstract Image OpenImage();
        public abstract DatabaseInfo GetDatabaseInfo();
        public abstract DbConnection GetConnection(string database);

        public bool Refresh()
        {
            if (!this.isOpen)
            {
                return true;
            }
            this.databases.Clear();
            return this.Open();
        }

        public bool Close()
        {
            try
            {
                foreach (DatabaseInfo info in this.databases)
                {
                    info.Close();
                }
                this.databases.Clear();
                this.isOpen = false;
                this.OnCloseConnect(new CloseConnectEventArgs(this));
                return true;
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public DatabaseInfo AddDataBaseInfo(string name)
        {
            DatabaseInfo info = this.GetDatabaseInfo();
            info.Name = name;
            info.ConnectInfo = this;
            this.databases.Add(info);
            return info;
        }

        public static bool AddConnection(ConnectInfo info)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"INSERT INTO TB_CONNECTION VALUES('{info.Name}', '{info.User}', '{info.Password}', '{info.ConnectionString}', '{info.AssemblyName}', '{info.NamespaceName}', '{info.ClassName}')";
                    int ret = command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }

        public static bool RemoveConnection(string name)
        {
            try
            {
                DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString);
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM TB_CONNECTION WHERE NAME='{name}'";
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex); ;
                return false;
            }
        }

        public static bool UpdateConnection(ConnectInfo info)
        {
            try
            {
                DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString);
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = $"UPDATE TB_CONNECTION SET connectionString={info.connectionString}, user={info.user}, password={info.password} WHERE NAME='{info.name}'";
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }

        public static List<ConnectInfo> LoadConnection()
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    List<ConnectInfo> list = new List<ConnectInfo>();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText =
                    "SELECT " +
                        "tb_connection.name, " +
                        "tb_connection.user, " +
                        "tb_connection.password, " +
                        "tb_connection.connectionString, " +
                        "tb_config.assemblyName, " +
                        "tb_config.namespaceName, " +
                        "tb_config.className, " +
                        "tb_config.designTable, " +
                        "tb_config.openTable, " +
                        "tb_config.openView, " +
                        "tb_config.dataTypes " +
                    "FROM " +
                        "tb_connection, " +

                        "tb_config " +
                    "WHERE " +
                        "tb_connection.driverName = tb_config.name;";
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(
                            dr["assemblyName"].ToString(),
                            dr["namespaceName"].ToString(),
                            dr["className"].ToString());

                        info.Name = dr["name"].ToString();
                        info.User = dr["user"].ToString();
                        info.Password = dr["password"].ToString();
                        info.ConnectionString = dr["connectionstring"].ToString();
                        info.AssemblyName = dr["assemblyName"].ToString();
                        info.NamespaceName = dr["namespaceName"].ToString();
                        info.ClassName = dr["className"].ToString();
                        info.DesignTable = dr["designTable"].ToString();
                        info.OpenTable = dr["openTable"].ToString();
                        info.OpenView = dr["openView"].ToString();
                        info.DataTypes = dr["dataTypes"].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                        list.Add(info);
                    }
                    return list;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex); ;
                return null;
            }
        }

        public static DataTable LoadConfig()
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM tb_config";
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
    }

    public delegate void CloseConnectEventHandler(object sender, CloseConnectEventArgs e);

    public class CloseConnectEventArgs: EventArgs
    {
        private ConnectInfo info;
        public CloseConnectEventArgs(ConnectInfo info)
            :
            base()
        {
            this.info = info;
        }

        public ConnectInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }

    public delegate void OpenConnectEventHandler(object sender, OpenConnectEventArgs e);
    public class OpenConnectEventArgs:EventArgs
    {
        private ConnectInfo info;
        public OpenConnectEventArgs(ConnectInfo info)
            :
            base()
        {
            this.info = info;
        }

        public ConnectInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }
}
