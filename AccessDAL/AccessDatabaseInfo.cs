using System;
using System.Collections.Generic;
using Helper;
using System.Data;
using System.Data.Common;
using SQLDAL;
using System.Data.OleDb;
using System.Diagnostics;
using gudusoft.gsqlparser;

namespace AccessDAL
{
    public class AccessDatabaseInfo : DatabaseInfo
    {
        public override void DropTable(string name)
        {
            try
            {
               
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);;
            }
        }

        public override void DropView(string name)
        {
            throw new NotImplementedException();
        }

       

        public override DataTable LoadTable()
        {
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(""))
                {
                    DataTable dt = (connection as OleDbConnection).GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    return dt;
                }
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return null;
            }
        }

        public override DataTable LoadView()
        {
            try
            {
                return new DataTable();
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return null;
            }
        }


        public override TableInfo CreateTable(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override ViewInfo CreateView(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override void AlterTable(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override void AlterView(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override bool ExecueNonQuery(string sql, out int count, out string error, out long cost)
        {
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
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

        public override bool ExecuteQuery(string sql, out DataTable table, out int count, out string error, out long cost)
        {
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    DataSet ds = new DataSet();
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    DbDataAdapter da = new OleDbDataAdapter(command as OleDbCommand);
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

        public override TableInfo GetTableInfo()
        {
            return new AccessTableInfo();
        }

        public override ViewInfo GetViewInfo()
        {
            return new AccessViewInfo();
        }

        public override bool Parse(string sql, out List<StatementObj> statements)
        {
            statements = new List<StatementObj>();
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVAccess);
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
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVAccess);
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
    }
}
