using System;
using System.Collections.Generic;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing;
using SQLiteDAL.Properties;
using System.Diagnostics;
using gudusoft.gsqlparser;
using System.Reflection;

namespace SQLiteDAL
{
    public sealed class SQLiteConnectInfo : ConnectInfo
    {
        public override Image CloseImage
        {
            get { return Resources.sqlite_close_16; }
        }

        public override Image OpenImage
        {
            get { return Resources.sqlite_open_16; }
        }

        public override Form ConnectForm
        {
            get { return new SQLiteConnectForm(); }
        }

        public override string DriverName
        {
            get
            {
                return "SQLite";
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
                return "";
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
                DbConnection connection = new SQLiteConnection(this.connectionString);
                connection.Open();
                return connection;
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return null;
            }
        }

        public override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new SQLiteDataAdapter(command as SQLiteCommand);
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
                SQLiteConnection.CreateFile(name);
                return true;
            }
            catch (Exception ex)
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

                    this.AddDataBaseInfo("main");
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
            table = new DataTable("Design");
            table.Columns.Add(Resources.FieldName);
            table.Columns.Add(Resources.FieldType);
            table.Columns.Add(Resources.FieldLength);
            table.Columns.Add(Resources.FieldScale);
            table.Columns.Add(Resources.FieldIsNull);
            table.Columns.Add(Resources.FieldIsPrimayrKey);
            table.Columns.Add(Resources.FieldDefault);
            table.Columns.Add(Resources.FieldComments);
            string field_name = "", field_type = "", field_length = "", field_precision = "", field_isnull = "", field_primarykey = "";
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = string.Format(this.DesignTableScript, tablename);
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        field_name = dr[1].ToString();
                        string str = dr[2].ToString();
                        int first_bracket = str.IndexOf("(");
                        int last_bracket = str.IndexOf(")");
                        if (first_bracket > 0 && last_bracket > 0)
                        {
                            field_type = str.Substring(0, first_bracket);
                            int comma = str.IndexOf(",");
                            if (comma > 0)
                            {
                                field_length = str.Substring(first_bracket + 1, comma - first_bracket - 1);
                                field_precision = str.Substring(comma + 1, last_bracket - comma - 1);
                            }
                            else
                            {
                                field_length = str.Substring(first_bracket + 1, last_bracket - first_bracket - 1);
                                field_precision = "0";
                            }
                        }
                        else
                        {
                            field_type = str;
                            field_length = "0";
                            field_precision = "0";
                        }
                        field_isnull = dr[3].ToString() == "0" ? "False" : "True";
                        field_primarykey = dr[5].ToString() != "0" ? "True" : "False";
                        object field_default = dr[4];
                        table.Rows.Add(new object[] { field_name, field_type, field_length, field_precision, field_isnull, field_primarykey, field_default });
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
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
                    statement = string.Format(this.OpenTableScript, tablename, pageSize, start);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
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

        public override bool OpenView(string database, string viewname, Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(database))
                {
                    statement = string.Format(this.OpenViewScript, viewname, pageSize, start);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
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
    }
}
