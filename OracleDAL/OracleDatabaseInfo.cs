using System;
using System.Collections.Generic;
using Helper;
using System.Data;
using System.Data.Common;
using SQLDAL;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;

namespace OracleDAL
{
    public class OracleDatabaseInfo : DatabaseInfo
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
                    command.CommandText = "select * from user_tables";
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
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
                    command.CommandText = "select * from user_views";
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
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
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = script;
                    command.ExecuteNonQuery();
                    return this.AddTableInfo(name);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
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

        public override SqlStatementType GetStatementType(string statement, string first_word)
        {
            if (first_word == "CREATE" ||
                first_word == "ALTER" ||
                first_word == "DELETE" ||
                first_word == "UPDATE" ||
                first_word == "INSERT" ||
                first_word == "DROP" ||
                first_word == "TRUNCATE")
            {
                return SqlStatementType.eMsg;
            }
            else if (first_word == "SELECT" || first_word == "SHOW")
            {
                return SqlStatementType.eTable;
            }
            else
            {
                return SqlStatementType.eMsg;
            }
        }

        public override bool ExecueNonQuery(string sql, out int count, out string error, out Int64 cost)
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
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                count = 0;
                error = ex.Message;
                cost = 0;
                return false;
            }
        }

        public override bool ExecuteQuery(string sql, out DataTable table, out int count, out string error, out Int64 cost)
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
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
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
            return new OracleTableInfo();
        }

        public override ViewInfo GetViewInfo()
        {
            return new OracleViewInfo();
        }
    }
}
