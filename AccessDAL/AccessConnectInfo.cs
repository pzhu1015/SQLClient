using System;
using System.Collections.Generic;
using Helper;
using SQLDAL;
using System.Data.Common;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;

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

        public override void Create(string name)
        {
            try
            {
                
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
                if (this.databases == null)
                {
                    this.databases = new List<DatabaseInfo>();
                }

                this.AddDataBaseInfo("main");
                this.isOpen = true;
                return true;
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
            return new AccessDatabaseInfo();
        }

        public override Image CloseImage
        {
            get { throw new NotImplementedException(); }
        }

        public override Image OpenImage
        {
            get { throw new NotImplementedException(); }
        }

        public override DbConnection GetConnection(string database)
        {
            throw new NotImplementedException();
        }

        public override Form GetConnectForm()
        {
            return new AccessConnectForm();
        }
    }
}
