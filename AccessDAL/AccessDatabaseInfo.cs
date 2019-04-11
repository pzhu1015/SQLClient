using System;
using System.Collections.Generic;
using Helper;
using System.Data;
using System.Data.Common;
using SQLDAL;
using System.Data.OleDb;

namespace AccessDAL
{
    public class AccessDatabaseInfo : DatabaseInfo
    {
        public override void DropTable(string name)
        {
            try
            {
               
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);;
            }
        }

        public override void DropView(string name)
        {
            throw new NotImplementedException();
        }

       

        public override DataTable LoadTable()
        {
            try
            {
                return null;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public override DataTable LoadView()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }


        public override TableInfo CreateTable(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override ViewInfo CreateView(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override void AlterTable(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override void AlterView(string name, string script)
        {
            throw new NotImplementedException();
        }

        public override SqlStatementType GetStatementType(string statement, string first_word)
        {
            throw new NotImplementedException();
        }

        public override bool ExecueNonQuery(string sql, out int count, out string error, out long cost)
        {
            throw new NotImplementedException();
        }

        public override bool ExecuteQuery(string sql, out DataTable table, out int count, out string error, out long cost)
        {
            throw new NotImplementedException();
        }

        public override TableInfo GetTableInfo()
        {
            return new AccessTableInfo();
        }

        public override ViewInfo GetViewInfo()
        {
            return new AccessViewInfo();
        }
    }
}
