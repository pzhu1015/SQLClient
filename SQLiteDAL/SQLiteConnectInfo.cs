using System;
using System.Collections.Generic;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Drawing;
using SQLiteDAL.Properties;

namespace SQLiteDAL
{
    public class SQLiteConnectInfo : ConnectInfo
    {
        public SQLiteConnectInfo()
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
                SQLiteConnection.CreateFile(name);
            }
            catch (Exception ex)
            {
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

                    this.AddDataBaseInfo("main");
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
            return new SQLiteDatabaseInfo();
        }

        public override Form GetConnectForm()
        {
            return new SQLiteConnectForm();
        }

        public override Image CloseImage()
        {
            return Resources.sqlite_close_16;
        }

        public override Image OpenImage()
        {
            return Resources.sqlite_open_16;
        }

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new SQLiteConnection(this.connectionString);
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
