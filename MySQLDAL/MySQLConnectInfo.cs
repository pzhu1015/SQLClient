using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using MySQLDAL.Properties;
using System.Diagnostics;
using gudusoft.gsqlparser;
using System.Reflection;

namespace MySQLDAL
{
    public sealed class MySQLConnectInfo : ConnectInfo
    {
        public override string DefaultPort
        {
            get
            {
                return "3306";
            }
        }

        public override Image CloseImage
        {
            get { return Resources.mysql_close_16; }
        }

        public override Image OpenImage
        {
            get { return Resources.mysql_open_16; }
        }

        public override Form ConnectForm
        {
            get
            {
                return new MySQLConnectForm();
            }
        }

        public override string DriverName
        {
            get
            {
                return "MySQL";
            }
        }

        public override string AssemblyName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        public override string NamespaceName
        {
            get
            {
                return this.GetType().Namespace;
            }
        }

        public override string ClassName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public override string DesignTableScript
        {
            get
            {
                return Resources.designTableScript;
            }
        }

        public override string OpenTableScript
        {
            get
            {
                return Resources.openTableScript;
            }
        }

        public override string OpenViewScript
        {
            get
            {
                return Resources.openViewScript;
            }
        }

        public override string LoadTableScript
        {
            get
            {
                return Resources.loadTableScript;
            }
        }

        public override string LoadViewScript
        {
            get
            {
                return Resources.loadViewScript;
            }
        }

        public override string[] DataTypes
        {
            get
            {
                return Resources.dataTypes.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            }
        }

        public override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new MySqlDataAdapter(command as MySqlCommand);
        }

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new MySqlConnection(this.connectionString);
                connection.Open();
                if (database != "")
                {
                    connection.ChangeDatabase(database);
                }
                return connection;
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return null;
            }
        }

        public override string GetLoadTableScript(string database)
        {
            return Resources.loadTableScript;
        }

        public override string GetLoadViewScript(string database)
        {
            return Resources.loadViewScript;
        }

        public override void Drop(string name)
        {
            try
            {
               
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);;
            }
        }

        public override bool Create(string name)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(""))
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"CREATE DATABASE {name};";
                    command.ExecuteNonQuery();
                    if (this.databases != null)
                    {
                        DatabaseInfo info = new DatabaseInfo();
                        info.Name = name;
                        info.ConnectInfo = this;
                        this.databases.Add(info);
                    }
                    return true;
                }
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool Open()
        {
            try
            {
                using (DbConnection connection = this.GetConnection(""))
                {
                    if (connection == null)
                    {
                        return false;
                    }

                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = "SHOW DATABASES;";
                    DbDataAdapter da = this.GetDataAdapter(command);
                    da.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.AddDataBaseInfo(dr[0].ToString());
                    }
                    this.isOpen = true;
                    this.OnOpenConnect(new OpenConnectEventArgs(this));
                    return true;
                }
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);;
                return false;
            }
        }

        public override bool Parse(string sql, out List<StatementObj> statements)
        {
            statements = new List<StatementObj>();
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVMysql);
                sqlparser.SqlText.Text = sql;
                int ret = sqlparser.Parse();
                if (ret != 0)
                {
                    this.message = sqlparser.ErrorMessages;
                    return false;
                }
                foreach (var statement in sqlparser.SqlStatements)
                {
                    StatementObj obj = new StatementObj();
                    obj.SqlText = statement.RawSqlText;
                    switch (statement.SqlStatementType)
                    {
                        case TSqlStatementType.sstSelect:
                        case TSqlStatementType.sstMySQLDescribe:
                        case TSqlStatementType.sstMySQLShow:
                            obj.SqlType = SqlType.eTable;
                            break;
                        default:
                            obj.SqlType = SqlType.eMsg;
                            break;
                    }
                    statements.Add(obj);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool Format(string sql, out string formatSql)
        {
            formatSql = sql;
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVMysql);
                sqlparser.SqlText.Text = sql;
                int ret = sqlparser.PrettyPrint();
                if (ret != 0)
                {
                    this.message = sqlparser.ErrorMessages;
                    return false;
                }
                formatSql = sqlparser.FormattedSqlText.Text;
                return true;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool DesignTable(string database, string tablename, out DataTable table)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.designTableScript;
                    command.Parameters.Add(new MySqlParameter("@database", database));
                    command.Parameters.Add(new MySqlParameter("@table", tablename));
                    DbDataAdapter da = new MySqlDataAdapter(command as MySqlCommand);
                    da.Fill(ds);
                    table = ds.Tables[0];
                    return true;
                }
            }
            catch (Exception ex)
            {
                table = null;
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool OpenTable(string database, string tablename, long start, long pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    statement = string.Format(Resources.openTableScript, tablename, start, pageSize);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = this.GetDataAdapter(command);
                    da.Fill(ds);
                    datatable = ds.Tables[0];
                    return true;
                }
            }
            catch (Exception ex)
            {
                datatable = null;
                statement = "";
                this.message = ex.Message;
                LogHelper.Error(ex); ;
                return false;
            }
        }

        public override bool OpenView(string dataase, string viewname, long start, long pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(dataase))
                {
                    statement = string.Format(Resources.openViewScript, viewname, start, pageSize);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = this.GetDataAdapter(command);
                    da.Fill(ds);
                    this.isOpen = true;
                    datatable = ds.Tables[0];
                    return true;
                }
            }
            catch (Exception ex)
            {
                datatable = null;
                statement = "";
                this.message = ex.Message;
                LogHelper.Error(ex); ;
                return false;
            }
        }

        public override bool DesignView(string database, string viewname, out DataTable table)
        {
            throw new NotImplementedException();
        }
    }
}
