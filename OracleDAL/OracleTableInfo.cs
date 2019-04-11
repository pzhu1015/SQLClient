using System;

using Helper;
using SQLDAL;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;

namespace OracleDAL
{
    public class OracleTableInfo : TableInfo
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
                    command.CommandText = string.Format(this.databaseInfo.ConnectInfo.DesignTable, this.name);
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
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
                    statement = string.Format(this.databaseInfo.ConnectInfo.OpenTable, this.name, this.name, pageSize, start);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
                    da.Fill(ds);
                    this.isOpen = true;
                    ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.Count - 1);
                    ds.Tables[0].Columns.RemoveAt(ds.Tables[0].Columns.Count - 1);
                    datatable = ds.Tables[0];
                    return true;
                }
            }
            catch(Exception ex)
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