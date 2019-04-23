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
using System.Diagnostics;
using gudusoft.gsqlparser;

namespace AccessDAL
{
    public sealed class AccessConnectInfo : ConnectInfo
    {
        public AccessConnectInfo()
        {

        }

        public override Image CloseImage
        {
            get { return Resources.access_close_16; }
        }

        public override Image OpenImage
        {
            get { return Resources.access_open_16; }
        }

        public override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new OleDbDataAdapter(command as OleDbCommand);
        }

        public override string GetLoadTableScript(string database)
        {
            throw new NotImplementedException();
        }

        public override string GetLoadViewScript(string database)
        {
            throw new NotImplementedException();
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

        public override Form ConnectForm
        {
            get { return new AccessConnectForm(); }
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

        public override bool Parse(string sql, out List<StatementObj> statements)
        {
            statements = new List<StatementObj>();
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVAccess);
                sqlparser.SqlText.Text = sql;
                int ret = sqlparser.Parse();
                if (ret != 0)
                {
                    this.message = sqlparser.ErrorMessages;
                    return false;
                }
                foreach (var statement in sqlparser.SqlStatements)
                {
                    StatementObj obj = new StatementObj();
                    obj.SqlText = statement.RawSqlText;
                    switch (statement.SqlStatementType)
                    {
                        case TSqlStatementType.sstSelect:
                            obj.SqlType = SqlType.eTable;
                            break;
                        default:
                            obj.SqlType = SqlType.eMsg;
                            break;
                    }
                    statements.Add(obj);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool Format(string sql, out string formatSql)
        {
            formatSql = sql;
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVAccess);
                sqlparser.SqlText.Text = sql;
                int ret = sqlparser.PrettyPrint();
                if (ret != 0)
                {
                    this.message = sqlparser.ErrorMessages;
                    return false;
                }
                formatSql = sqlparser.FormattedSqlText.Text;
                return true;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool DesignTable(string database, string tablename, out DataTable table)
        {
            throw new NotImplementedException();
        }

        public override bool OpenTable(string database, string tablename, long start, long pageSize, out DataTable datatable, out string statement)
        {
            throw new NotImplementedException();
        }

        public override bool DesignView(string database, string viewname, out DataTable table)
        {
            throw new NotImplementedException();
        }

        public override bool OpenView(string dataase, string viewname, long start, long pageSize, out DataTable datatable, out string statement)
        {
            throw new NotImplementedException();
        }
    }
}
