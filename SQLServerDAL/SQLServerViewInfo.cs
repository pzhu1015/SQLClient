﻿using System;

using SQLDAL;
using System.Data;
using System.Data.Common;
using Helper;
using System.Data.SqlClient;

namespace SQLServerDAL
{
    public class SQLServerViewInfo : ViewInfo
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
                    statement = string.Format(this.databaseInfo.ConnectInfo.OpenView, this.name, start, pageSize);
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
