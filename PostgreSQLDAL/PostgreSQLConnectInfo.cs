﻿using SQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;
using PostgreSQLDAL.Properties;
using Helper;
using Npgsql;
using gudusoft.gsqlparser;
using System.Reflection;

namespace PostgreSQLDAL
{
    public sealed class PostgreSQLConnectInfo : ConnectInfo
    {
        public override string DriverName
        {
            get
            {
                return "PostgreSQL";
            }
        }

        public override string AssemblyName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }

        public override string NamespaceName
        {
            get
            {
                return this.GetType().Namespace;
            }
        }

        public override string ClassName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public override Image CloseImage
        {
            get
            {
                return Resources.postgresql_close_16;
            }
        }

        public override Form ConnectForm
        {
            get
            {
                return new PostgreSQLConnectForm();
            }
        }

        public override Image OpenImage
        {
            get
            {
                return Resources.postgresql_open_16;
            }
        }

        public override string DefaultPort
        {
            get
            {
                return "5432";
            }
        }

        public override bool Create(string name)
        {
            throw new NotImplementedException();
        }

        public override bool DesignTable(string database, string tablename, out DataTable table)
        {
            throw new NotImplementedException();
        }

        public override bool DesignView(string database, string viewname, out DataTable table)
        {
            throw new NotImplementedException();
        }

        public override void Drop(string name)
        {
            throw new NotImplementedException();
        }

        public override bool Format(string sql, out string formatSql)
        {
            formatSql = sql;
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVPostgresql);
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

        public override DbConnection GetConnection(string database)
        {
            try
            {
                DbConnection connection = new NpgsqlConnection(this.connectionString);
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

        public override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new NpgsqlDataAdapter(command as NpgsqlCommand);
        }

        public override string GetLoadTableScript(string database)
        {
            throw new NotImplementedException();
        }

        public override string GetLoadViewScript(string database)
        {
            throw new NotImplementedException();
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
                    command.CommandText = "SELECT * FROM pg_database;";
                    DbDataAdapter da = this.GetDataAdapter(command);
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
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex); ;
                return false;
            }
        }

        public override bool OpenTable(string database, string tablename, long start, long pageSize, out DataTable datatable, out string statement)
        {
            throw new NotImplementedException();
        }

        public override bool OpenView(string dataase, string viewname, long start, long pageSize, out DataTable datatable, out string statement)
        {
            throw new NotImplementedException();
        }

        public override bool Parse(string sql, out List<StatementObj> statements)
        {
            statements = new List<StatementObj>();
            try
            {
                TGSqlParser sqlparser = new TGSqlParser(TDbVendor.DbVPostgresql);
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
    }
}