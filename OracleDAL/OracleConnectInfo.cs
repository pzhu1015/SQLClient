using System;
using System.Collections.Generic;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
using System.Drawing;
using OracleDAL.Properties;
using System.Diagnostics;
using gudusoft.gsqlparser;
using System.Reflection;

namespace OracleDAL
{
    public sealed class OracleConnectInfo : ConnectInfo
    {
        public override Image CloseImage
        {
            get { return Resources.oracle_close_16; }
        }

        public override Image OpenImage
        {
            get { return Resources.oracle_open_16; }
        }
        
        public override Form ConnectForm
        {
            get { return new OracleConnectForm(); }
        }

        public override string DriverName
        {
            get
            {
                return "Oracle";
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

        public override string DefaultPort
        {
            get
            {
                return "1521";
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

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new OracleConnection(this.connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return null;
            }
        }

        public override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new OracleDataAdapter(command as OracleCommand);
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
                return true;
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
                    command.CommandText = $"select * from all_users where username = '{this.user.ToUpper()}' order by created desc";
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
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
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVOracle);
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
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVOracle);
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
                    command.CommandText = string.Format(this.DesignTableScript, tablename);
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
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

        public override bool OpenTable(string database, string tablename, Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    statement = string.Format(this.OpenTableScript, tablename, tablename, pageSize, start);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
                    da.Fill(ds);
                    ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.Count - 1);
                    ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.Count - 1);
                    datatable = ds.Tables[0];
                    return true;
                }
            }
            catch (Exception ex)
            {
                datatable = null;
                statement = "";
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool DesignView(string database, string viewname, out DataTable table)
        {
            throw new NotImplementedException();
        }

        public override bool OpenView(string database, string viewname, Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    statement = string.Format(this.OpenViewScript, viewname, viewname, pageSize, start);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
                    da.Fill(ds);
                    this.isOpen = true;
                    datatable = ds.Tables[0];
                    ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.Count - 1);
                    ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.Count - 1);
                    return true;
                }
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                datatable = null;
                statement = "";
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }
    }
}
