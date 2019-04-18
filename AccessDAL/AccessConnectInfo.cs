using System;
using System.Collections.Generic;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using ADOX;
using AccessDAL.Properties;

namespace AccessDAL
{
    public class AccessConnectInfo : ConnectInfo
    {
        public AccessConnectInfo()
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

        public override bool Create(string name)
        {
            try
            {
                Catalog catalog = new Catalog();
                catalog.Create($"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={name};Jet OLEDB:Engine Type=5");
                return true;
            }
            catch (Exception ex)
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

                    this.AddDataBaseInfo("main");
                    this.isOpen = true;
                    this.OnOpenConnect(new OpenConnectEventArgs(this));
                    return true;
                }
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override DatabaseInfo GetDatabaseInfo()
        {
            return new AccessDatabaseInfo();
        }

        public override Image CloseImage
        {
            get { return Resources.access_close_16; }
        }

        public override Image OpenImage
        {
            get { return Resources.access_open_16; }
        }

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new OleDbConnection(this.connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return null;
            }
        }

        public override Form GetConnectForm()
        {
            return new AccessConnectForm();
        }
    }
}
