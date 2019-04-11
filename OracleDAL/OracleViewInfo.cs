using System;

using SQLDAL;
using System.Data;
using System.Data.Common;
using Helper;
using Oracle.ManagedDataAccess.Client;

namespace OracleDAL
{
    public class OracleViewInfo : ViewInfo
    {
        public override void Close()
        {
            this.isOpen = false;
        }

        public override void Design()
        {
        }

        public override bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.databaseInfo.ConnectInfo.GetConnection(this.databaseInfo.Name))
                {
                    statement = string.Format(this.databaseInfo.ConnectInfo.OpenView, this.name, this.name, pageSize, start);
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
                LogHelper.Error(ex);;
                return false;
            }
        }
    }
}
