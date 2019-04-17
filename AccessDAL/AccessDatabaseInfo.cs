using System;
using System.Collections.Generic;
using Helper;
using System.Data;
using System.Data.Common;
using SQLDAL;
using System.Data.OleDb;

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
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = this.connectInfo.LoadTable;
                    DbDataAdapter da = new OleDbDataAdapter(command as OleDbCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
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
                using (DbConnection connection = this.connectInfo.GetConnection(""))
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = this.connectInfo.LoadView;
                    DbDataAdapter da = new OleDbDataAdapter(command as OleDbCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    return ds.Tables[0];
                }
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
            throw new NotImplementedException();
        }

        public override bool ExecuteQuery(string sql, out DataTable table, out int count, out string error, out long cost)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override bool Format(string sql, out string formatSql)
        {
            throw new NotImplementedException();
        }
    }
}
