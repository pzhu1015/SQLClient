using System;

using Helper;
using SQLDAL;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SQLServerDAL
{
    public class SQLServerTableInfo : TableInfo
    {
        /// <summary>
        /// Table Headers: FIELDNAME, FIELDTYPE, FIELDLENGTH, FIELDSCALE, FIELDISNULL, FIELDDEFAULT, FIELDPRIMARYKE, FIELDCOMMENTS
        /// </summary>
        /// <returns></returns>
        public override bool Design(out DataTable table)
        {
            try
            {
                using (DbConnection connection = this.databaseInfo.ConnectInfo.GetConnection(this.databaseInfo.Name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = this.databaseInfo.ConnectInfo.DesignTable;
                    command.Parameters.Add(new SqlParameter("@table", this.name));
                    DbDataAdapter da = new SqlDataAdapter(command as SqlCommand);
                    da.Fill(ds);
                    this.isDesign = true;
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

        public override bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.databaseInfo.ConnectInfo.GetConnection(this.databaseInfo.Name))
                {
                    statement = string.Format(this.databaseInfo.ConnectInfo.OpenTable, this.name, start, pageSize);
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new SqlDataAdapter(command as SqlCommand);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    this.isOpen = true;
                    datatable = ds.Tables[0];
                    return true;
                }
            }
            catch(Exception ex)
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
