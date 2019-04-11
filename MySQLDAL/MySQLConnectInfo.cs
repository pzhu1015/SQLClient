using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using MySQLDAL.Properties;

namespace MySQLDAL
{
    public class MySQLConnectInfo : ConnectInfo
    {
        public MySQLConnectInfo()
        {
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

        public override void Create(string name)
        {
            try
            {
                using (DbConnection connection = this.GetConnection(""))
                {
                    if (!this.isOpen)
                    {
                        return;
                    }
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"CREATE DATABASE {name};";
                    command.ExecuteNonQuery();
                    if (this.databases != null)
                    {
                        DatabaseInfo info = new MySQLDatabaseInfo();
                        info.Name = name;
                        info.ConnectInfo = this;
                        this.databases.Add(info);
                    }
                }
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);;
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
                    command.CommandText = "SHOW DATABASES;";
                    DbDataAdapter da = new MySqlDataAdapter(command as MySqlCommand);
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

        public override Form GetConnectForm()
        {
            return new MySQLConnectForm();
        }

        public override Image CloseImage()
        {
            return Resources.mysql_close_16;
        }

        public override Image OpenImage()
        {
            return Resources.mysql_open_16;
        }

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new MySqlConnection(this.connectionString);
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

        public override DatabaseInfo GetDatabaseInfo()
        {
            return new MySQLDatabaseInfo();
        }
    }
}
