using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using SQLServerDAL.Properties;

namespace SQLServerDAL
{
    public class SQLServerConnectInfo : ConnectInfo
    {
        public SQLServerConnectInfo()
        {

        }

        public override Image CloseImage
        {
            get { return Resources.sqlserver_close_16; }
        }

        public override Image OpenImage
        {
            get { return Resources.sqlserver_open_16; }
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
                return true;
            }
            catch(Exception ex)
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

                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = "SELECT NAME FROM SYSDATABASES";
                    DbDataAdapter da = new SqlDataAdapter(command as SqlCommand);
                    da.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.AddDataBaseInfo(dr[0].ToString());
                    }
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

        public override DatabaseInfo GetDatabaseInfo()
        {
            return new SQLServerDatabaseInfo();
        }

        public override Form GetConnectForm()
        {
            return new SQLServerConnectForm();
        }

        

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new SqlConnection(this.connectionString);
                connection.Open();
                if (database != "")
                {
                    connection.ChangeDatabase(database);
                }
                return connection;
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return null;
            }
        }
    }
}
