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
using SQLDAL.Properties;
using System.Diagnostics;

namespace SQLDAL
{
    public abstract class ConnectInfo : IConnectInfo
    {
        public static string LocalConnectionString = string.Format(Resources.AppSQL, Application.StartupPath);
        public event CloseConnectEventHandler CloseConnect;
        public event OpenConnectEventHandler OpenConnect;
        protected List<DatabaseInfo> databases = new List<DatabaseInfo>();
        protected string driverName;
        protected string name;
        protected string user;
        protected string password;
        protected string assemblyName;
        protected string namespaceName;
        protected string className;
        protected string connectionString;
        protected string designTableScript;
        protected string openTableScript;
        protected string openViewScript;
        protected string loadTableScript;
        protected string loadViewScript;
        protected string[] dataTypes;
        protected Form connectForm;
        protected string message;
        protected TreeNode node;
        protected bool isOpen = false;

        #region 属性
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

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库查看表设计的脚本模版")]
        public string DesignTableScript
        {
            get
            {
                return designTableScript;
            }

            set
            {
                designTableScript = value;
            }
        }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库打开表的脚本模版")]
        public string OpenTableScript
        {
            get
            {
                return openTableScript;
            }

            set
            {
                openTableScript = value;
            }
        }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库打开视图的脚本模版")]
        public string OpenViewScript
        {
            get
            {
                return openViewScript;
            }

            set
            {
                openViewScript = value;
            }
        }

        [Browsable(true)]
        [Category("脚本")]
        [Description("加载当前数据库中所有表的脚本模版")]
        public string LoadTableScript
        {
            get
            {
                return loadTableScript;
            }

            set
            {
                loadTableScript = value;
            }
        }

        [Browsable(true)]
        [Category("脚本")]
        [Description("加载当前数据库中所有视图的脚本模版")]
        public string LoadViewScript
        {
            get
            {
                return loadViewScript;
            }

            set
            {
                loadViewScript = value;
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

        [Browsable(false)]
        public abstract Image CloseImage { get; }

        [Browsable(false)]
        public abstract Image OpenImage { get; }

        [Browsable(false)]
        public abstract Form ConnectForm { get; }

        #endregion

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
            DatabaseInfo info = new DatabaseInfo();
            info.Name = name;
            info.ConnectInfo = this;
            this.databases.Add(info);
            return info;
        }

        public bool ExecueNonQuery(string database, string sql, out int count, out string error, out Int64 cost)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    count = command.ExecuteNonQuery();
                    watch.Stop();
                    cost = watch.ElapsedMilliseconds;
                    error = "";
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                count = 0;
                error = ex.Message;
                cost = 0;
                return false;
            }
        }

        public bool ExecuteQuery(string database, string sql, out DataTable table, out int count, out string error, out Int64 cost)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    DataSet ds = new DataSet();
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    DbDataAdapter da = this.GetDataAdapter(command);
                    da.Fill(ds);
                    table = ds.Tables[0];
                    watch.Stop();
                    count = table.Rows.Count;
                    cost = watch.ElapsedMilliseconds;
                    error = "";
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                table = null;
                count = 0;
                cost = 0;
                error = ex.Message;
                return false;
            }
        }

        #region 驱动数据访问层管理静态方法
        public static bool AddConnection(ConnectInfo info)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"INSERT INTO TB_CONNECTION VALUES('{info.Name}', '{info.User}', '{info.Password}', '{info.ConnectionString}',  '{info.DriverName}')";
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
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"DELETE FROM TB_CONNECTION WHERE NAME='{name}'";
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
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
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"UPDATE TB_CONNECTION SET connectionString={info.connectionString}, user={info.user}, password={info.password} WHERE NAME='{info.name}'";
                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
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
                        "tb_config.loadTable, " +
                        "tb_config.loadView, " +
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
                        info.DesignTableScript = dr["designTable"].ToString();
                        info.OpenTableScript = dr["openTable"].ToString();
                        info.OpenViewScript = dr["openView"].ToString();
                        info.LoadTableScript = dr["loadTable"].ToString();
                        info.LoadViewScript = dr["loadView"].ToString();
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

        public static bool AddDriver(ConnectInfo info)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"INSERT INTO TB_CONFIG (name, assemblyName, namespaceName, className) VALUES('{info.driverName}', '{info.assemblyName}', '{info.namespaceName}', '{info.className}')";
                    int ret = command.ExecuteNonQuery();
                    return true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }
        #endregion

        public abstract bool Open();
        public abstract bool Create(string name);
        public abstract void Drop(string name);
        public abstract DbConnection GetConnection(string database);
        public abstract DbDataAdapter GetDataAdapter(DbCommand command);
        public abstract string GetLoadTableScript(string database);
        public abstract string GetLoadViewScript(string database);
        public abstract bool Format(string sql, out string formatSql);
        public abstract bool Parse(string sql, out List<StatementObj> statements);
        public abstract bool DesignTable(string database, string tablename, out DataTable table);
        public abstract bool OpenTable(string database, string tablename, long start, long pageSize, out DataTable datatable, out string statement);
        public abstract bool DesignView(string database, string viewname, out DataTable table);
        public abstract bool OpenView(string dataase, string viewname, long start, long pageSize, out DataTable datatable, out string statement);
    }

    
}
