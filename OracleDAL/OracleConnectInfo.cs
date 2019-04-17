using System;
using System.Collections.Generic;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
using System.Drawing;
using OracleDAL.Properties;

namespace OracleDAL
{
    public class OracleConnectInfo : ConnectInfo
    {
        public OracleConnectInfo()
        {
        }

        public override Image CloseImage
        {
            get { return Resources.oracle_close_16; }
        }

        public override Image OpenImage
        {
            get { return Resources.oracle_open_16; }
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
                    command.CommandText = $"select * from all_users where username = '{this.user.ToUpper()}' order by created desc";
                    DbDataAdapter da = new OracleDataAdapter(command as OracleCommand);
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
            return new OracleDatabaseInfo();
        }

        public override Form GetConnectForm()
        {
            return new OracleConnectForm();
        }

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new OracleConnection(this.connectionString);
                connection.Open();
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
