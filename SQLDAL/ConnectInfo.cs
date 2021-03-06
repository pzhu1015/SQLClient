﻿using Helper;
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
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;

/*
 * MySql: root@12345
 * Oracle: root@123456
 * SqlServer: sa@12345
 * Postgresql: postgres@12345
 */

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
        protected string file;
        protected string host;
        protected string port;
        protected string assemblyName;
        protected string namespaceName;
        protected string className;
        protected string connectionString;
        protected string loginUser;
        protected Form connectForm;
        protected string message;
        protected TreeNode node;
        protected bool isOpen = false;

        #region 属性
        [Browsable(true)]
        [Category("基本")]
        [Description("当前数据库的驱动程序名称")]
        public abstract string DriverName { get; }

        [Browsable(true)]
        [Category("基本")]
        [Description("驱动程序集名称")]
        public abstract string AssemblyName { get; }

        [Browsable(true)]
        [Category("基本")]
        [Description("驱动程序命令空间")]
        public abstract string NamespaceName { get; }

        [Browsable(true)]
        [Category("基本")]
        [Description("驱动程序连接信息类名")]
        public abstract string ClassName { get; }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库查看表设计的脚本模版")]
        public abstract string DesignTableScript { get; }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库打开表的脚本模版")]
        public abstract string OpenTableScript { get; }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库打开视图的脚本模版")]
        public abstract string OpenViewScript { get; }

        [Browsable(true)]
        [Category("脚本")]
        [Description("加载当前数据库中所有表的脚本模版")]
        public abstract string LoadTableScript { get; }

        [Browsable(true)]
        [Category("脚本")]
        [Description("加载当前数据库中所有视图的脚本模版")]
        public abstract string LoadViewScript { get; }

        [Browsable(true)]
        [Category("脚本")]
        [Description("当前数据库支持的所有数据类型集合")]
        public abstract string[] DataTypes { get; }
 
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
        public string LoginUser
        {
            get
            {
                return loginUser;
            }

            set
            {
                loginUser = value;
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

        [Browsable(false)]
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

        [Browsable(false)]
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

        [Browsable(false)]
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

        [Browsable(false)]
        public abstract string DefaultPort { get; }

        

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

        public ConnectInfo Clone()
        {
            ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(this.AssemblyName, this.NamespaceName, this.ClassName);
            info.IsOpen = this.IsOpen;
            info.Message = this.Message;
            info.ConnectionString = this.ConnectionString;
            info.Name = this.Name;
            info.User = this.User;
            info.Password = this.Password;
            info.Host = this.Host;
            info.Port = this.Port;
            info.File = this.File;
            info.LoginUser = this.LoginUser;
            return info;
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

        public static bool Login(string user, string password, out string error)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"select password from tb_user where user='{user}'";
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {
                        error = $"当前用户'{user}'不存在，请联系管理员注册登录用户";
                        return false;
                    }
                    string en_password = EncryptHelper.MD5Encrypt64(password);
                    string db_password = dt.Rows[0]["password"].ToString();
                    if (en_password != db_password)
                    {
                        error = $"当前用户'{user}'密码不正确";
                        return false;
                    }
                }
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                error = ex.Message;
                return false;
            }
        }

        public static bool Regist(string user, string password, string phone, string email, string age, string gender, byte[] face, byte[] faceresult, out string error)
        {
            try
            {
                if (user == "")
                {
                    error = "用户名不能为空";
                    return false;
                }
                if (password == "")
                {
                    error = "密码不能为空";
                    return false;
                }
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"select password from tb_user where user='{user}'";
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        error = $"当前用户'{user}'已存在，请选择其他用户名注册";
                        return false;
                    }
                    string en_password = EncryptHelper.MD5Encrypt64(password);
                    command.CommandText = "insert into tb_user values(@user, @password, @phone, @email, @age, @gender, @face, @faceresult, @permission)";
                    command.Parameters.Add(new SQLiteParameter("@user", user));
                    command.Parameters.Add(new SQLiteParameter("@password", en_password));
                    command.Parameters.Add(new SQLiteParameter("@phone", phone));
                    command.Parameters.Add(new SQLiteParameter("@email", email));
                    command.Parameters.Add(new SQLiteParameter("@age", age));
                    command.Parameters.Add(new SQLiteParameter("@gender", gender));
                    command.Parameters.Add(new SQLiteParameter("@face", face));
                    command.Parameters.Add(new SQLiteParameter("@faceresult", faceresult));
                    command.Parameters.Add(new SQLiteParameter("@permission", 1));
                    command.ExecuteNonQuery();
                }
                error = "";
                return true;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                error = ex.Message;
                return false;
            }
        }

        public static bool LoadUser(out DataTable dt, out string error)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"select * from tb_user";
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    dt = new DataTable();
                    da.Fill(dt);
                }
                error = "";
                return true;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                error = ex.Message;
                dt = null;
                return false;
            }
        }

        public static int GetPermission(string user)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"select permission from tb_user where user='{user}'";
                    int permission = Convert.ToInt32(command.ExecuteScalar());
                    return permission;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return -1;
            }
        }

        public static bool AddConnection(ConnectInfo info)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.InsertConnectScript;
                    command.Parameters.Add(new SQLiteParameter("@connectName", info.Name));
                    command.Parameters.Add(new SQLiteParameter("@user", info.User));
                    command.Parameters.Add(new SQLiteParameter("@file", info.File));
                    command.Parameters.Add(new SQLiteParameter("@host", info.Host));
                    command.Parameters.Add(new SQLiteParameter("@port", info.Port));
                    command.Parameters.Add(new SQLiteParameter("@password", info.Password));
                    command.Parameters.Add(new SQLiteParameter("@connectionString", info.ConnectionString));
                    command.Parameters.Add(new SQLiteParameter("@driverName", info.DriverName));
                    command.Parameters.Add(new SQLiteParameter("@owner", info.LoginUser));

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
                    command.CommandText = Resources.DeleteConnectScript;
                    command.Parameters.Add(new SQLiteParameter("@name", name));
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
                    command.CommandText = Resources.UpdateConnectScript;
                    command.Parameters.Add(new SQLiteParameter("@user", info.User));
                    command.Parameters.Add(new SQLiteParameter("@password", info.Password));
                    command.Parameters.Add(new SQLiteParameter("@file", info.File));
                    command.Parameters.Add(new SQLiteParameter("@host", info.Host));
                    command.Parameters.Add(new SQLiteParameter("@port", info.Port));
                    command.Parameters.Add(new SQLiteParameter("@connectionString", info.ConnectionString));
                    command.Parameters.Add(new SQLiteParameter("@driverName", info.DriverName));
                    command.Parameters.Add(new SQLiteParameter("@name", info.Name));
                    command.Parameters.Add(new SQLiteParameter("@owner", info.LoginUser));

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

        public static List<ConnectInfo> LoadConnection(string owner)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    List<ConnectInfo> list = new List<ConnectInfo>();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.LoadConnectScript;
                    command.Parameters.Add(new SQLiteParameter("@owner", owner));
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
                        info.File = dr["file"].ToString();
                        info.Host = dr["host"].ToString();
                        info.Port = dr["port"].ToString();
                        info.ConnectionString = dr["connectionstring"].ToString();
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

        public static DataTable LoadDriver()
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.LoadDriverScript;
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
                    command.CommandText = Resources.InsertDriverScript;
                    command.Parameters.Add(new SQLiteParameter("@name", info.DriverName));
                    command.Parameters.Add(new SQLiteParameter("@assemblyName", info.AssemblyName));
                    command.Parameters.Add(new SQLiteParameter("@namespaceName", info.NamespaceName));
                    command.Parameters.Add(new SQLiteParameter("@className", info.ClassName));
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

        public static bool RemoveDriver(string name)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.DeleteDriverScript;
                    command.Parameters.Add(new SQLiteParameter("@name", name));
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
