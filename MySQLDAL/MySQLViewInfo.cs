using System;

using SQLDAL;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Helper;

namespace MySQLDAL
{
    public class MySQLViewInfo : ViewInfo
    {
        public override void Close()
        {
            this.isOpen = false;
        }

        public override void Design()
        {
        }

        public override bool Open(long start, long pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.databaseInfo.ConnectInfo.GetConnection(this.databaseInfo.Name))
                {
                    statement = string.Format(this.databaseInfo.ConnectInfo.OpenView, this.name, start, pageSize);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new MySqlDataAdapter(command as MySqlCommand);
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
