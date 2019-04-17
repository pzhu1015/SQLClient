﻿using System;
using System.Collections.Generic;

using Helper;
using System.Data;
using System.Data.Common;
using SQLDAL;
using System.Data.SqlClient;
using System.Diagnostics;
using gudusoft.gsqlparser;

namespace SQLServerDAL
{
    public class SQLServerDatabaseInfo : DatabaseInfo
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
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = string.Format(this.connectInfo.LoadTable, this.name);
                    DbDataAdapter da = new SqlDataAdapter(command as SqlCommand);
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public override DataTable LoadView()
        {
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = string.Format(this.connectInfo.LoadView, this.name);
                    DbDataAdapter da = new SqlDataAdapter(command as SqlCommand);
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

        public override TableInfo CreateTable(string name, string script)
        {
            return null;
        }

        public override ViewInfo CreateView(string name, string script)
        {
            return null;
        }

        public override void AlterTable(string name, string script)
        {
            
        }

        public override void AlterView(string name, string script)
        {
            
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
                    DbDataAdapter da = new SqlDataAdapter(command as SqlCommand);
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
            return new SQLServerTableInfo();
        }

        public override ViewInfo GetViewInfo()
        {
            return new SQLServerViewInfo();
        }

        public override bool Parse(string sql, out List<StatementObj> statements)
        {
            statements = new List<StatementObj>();
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVMssql);
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
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVMssql);
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
