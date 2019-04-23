using Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SQLDAL
{
    public sealed class DatabaseInfo : IDatabaseInfo
    {
        public event CloseDatabaseEventHandler CloseDatabase;
        public event OpenDatabaseEventHandler OpenDatabase;
        private TreeNode node;
        private string name;
        private string message;
        private bool isOpen = false;
        private ConnectInfo connectInfo;
        private List<TableInfo> tables = new List<TableInfo>();
        private List<ViewInfo> views = new List<ViewInfo>();
        private List<SelectInfo> selects = new List<SelectInfo>();
        private List<object> newSelectPages = new List<object>();
        private List<object> newTablePages = new List<object>();

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public ConnectInfo ConnectInfo
        {
            get
            {
                return connectInfo;
            }

            set
            {
                connectInfo = value;
            }
        }

        public List<TableInfo> Tables
        {
            get
            {
                return tables;
            }

            set
            {
                tables = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return isOpen;
            }

            set
            {
                isOpen = value;
            }
        }

        public List<ViewInfo> Views
        {
            get
            {
                return views;
            }

            set
            {
                views = value;
            }
        }

        public List<SelectInfo> Selects
        {
            get
            {
                return selects;
            }

            set
            {
                selects = value;
            }
        }

        public TreeNode Node
        {
            get
            {
                return node;
            }

            set
            {
                node = value;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public List<object> NewSelectPages
        {
            get
            {
                return newSelectPages;
            }

            set
            {
                newSelectPages = value;
            }
        }

        public List<object> NewTablePages
        {
            get
            {
                return newTablePages;
            }

            set
            {
                newTablePages = value;
            }
        }

        private void OnCloseDatabase(CloseDatabaseEventArgs e)
        {
            if (this.CloseDatabase != null)
            {
                this.CloseDatabase(this, e);
            }
        }

        private void OnOpenDatabase(OpenDatabaseEventArgs e)
        {
            if (this.OpenDatabase != null)
            {
                this.OpenDatabase(this, e);
            }
        }

        public TableInfo AddTableInfo(string name)
        {
            TableInfo info = new TableInfo();
            info.Name = name;
            info.DatabaseInfo = this;
            this.tables.Add(info);
            return info;
        }
        public ViewInfo AddViewInfo(string name)
        {
            ViewInfo info = new ViewInfo();
            info.Name = name;
            info.DatabaseInfo = this;
            this.views.Add(info);
            return info;
        }
        public SelectInfo AddSelectInfo(string name)
        {
            SelectInfo info = new SelectInfo();
            info.Name = name;
            info.DatabaseInfo = this;
            this.selects.Add(info);
            return info;
        }
        public TableInfo CreateTable(string name, string script)
        {
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = script;
                    command.ExecuteNonQuery();
                    return this.AddTableInfo(name);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }
        public SelectInfo CreateSelect(string selectName, string script)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand(); command.CommandText = $"INSERT INTO TB_SELECT VALUES(@selectName, @script, @connectName, @databaseName)";
                    command.Parameters.Add(new SQLiteParameter("selectName", selectName));
                    command.Parameters.Add(new SQLiteParameter("script", script));
                    command.Parameters.Add(new SQLiteParameter("connectName", this.connectInfo.Name));
                    command.Parameters.Add(new SQLiteParameter("databaseName", this.name));
                    int ret = command.ExecuteNonQuery();
                }
                return this.AddSelectInfo(selectName);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public DataTable LoadTable()
        {
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = this.connectInfo.GetLoadTableScript(this.name);
                    DbDataAdapter da = this.connectInfo.GetDataAdapter(command);
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public DataTable LoadView()
        {
            try
            {
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = this.connectInfo.GetLoadViewScript(this.name);
                    DbDataAdapter da = this.connectInfo.GetDataAdapter(command);
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public DataTable LoadSelect()
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM TB_SELECT WHERE DATABASE='{this.name}' AND CONNECT='{this.connectInfo.Name}'";
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return null;
            }
        }

        public void AlterSelect(string selectName, string script)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();command.CommandText = $"UPDATE TB_SELECT SET CONTENTS=@script WHERE NAME=@selectName AND CONNECT=@connectName AND DATABASE=@databaseName";
                    command.Parameters.Add(new SQLiteParameter("@script", script));
                    command.Parameters.Add(new SQLiteParameter("@selectName", selectName));
                    command.Parameters.Add(new SQLiteParameter("@connectName", this.connectInfo.Name));
                    command.Parameters.Add(new SQLiteParameter("@databaseName", this.name));
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
        
        public void RefreshTable()
        {
            try
            {
                if (!this.isOpen)
                {
                    return;
                }
                this.tables.Clear();
                DataTable dt = this.LoadTable();
                foreach (DataRow dr in dt.Rows)
                {
                    this.AddTableInfo(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public void RefreshView()
        {
            try
            {
                if (!this.isOpen)
                {
                    return;
                }
                this.views.Clear();
                DataTable dt = this.LoadView();
                foreach (DataRow dr in dt.Rows)
                {
                    this.AddViewInfo(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public void RefreshSelect()
        {
            try
            {
                if (!this.isOpen)
                {
                    return;
                }
                this.selects.Clear();
                DataTable dt = this.LoadSelect();
                foreach (DataRow dr in dt.Rows)
                {
                    this.AddSelectInfo(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        public bool Open()
        {
            try
            {
                if (this.isOpen)
                {
                    return true;
                }

                DataTable dt = this.LoadTable();
                if (dt == null)
                {
                    return false;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    this.AddTableInfo(dr[0].ToString());
                }

                dt = this.LoadView();
                if (dt == null)
                {
                    return false;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    this.AddViewInfo(dr[0].ToString());
                }

                dt = this.LoadSelect();
                if (dt == null)
                {
                    return false;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    this.AddSelectInfo(dr[0].ToString());
                }

                this.isOpen = true;
                this.OnOpenDatabase(new OpenDatabaseEventArgs(this));
                return true;
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public void Refresh()
        {
            if (!this.isOpen)
            {
                return;
            }
            this.tables.Clear();
            this.Views.Clear();
            this.Selects.Clear();
            this.Open();
        }

        public bool Close()
        {
            try
            {
                if (this.isOpen)
                {
                    foreach(TableInfo info in this.tables)
                    {
                        info.Close();
                    }
                    this.tables.Clear();
                    foreach(ViewInfo info in this.views)
                    {
                        info.Close();
                    }
                    this.views.Clear();
                    foreach(SelectInfo info in this.selects)
                    {
                        info.Close();
                    }
                    this.selects.Clear();
                    this.isOpen = false;
                    this.OnCloseDatabase(new CloseDatabaseEventArgs(this));
                }
                return true;
            }
            catch(Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }
        
        public bool Parse(string sql, out List<StatementObj> statements)
        {
            return this.connectInfo.Parse(sql, out statements);
        }
        public bool Format(string sql, out string formatSql)
        {
            return this.connectInfo.Format(sql, out formatSql);
        }
        public bool ExecueNonQuery(string sql, out int count, out string error, out long cost)
        {
            return this.connectInfo.ExecueNonQuery(this.name, sql, out count, out error, out cost);
        }
        public bool ExecuteQuery(string sql, out DataTable table, out int count, out string error, out long cost)
        {
            return this.connectInfo.ExecuteQuery(this.name, sql, out table, out count, out error, out cost);
        }

        public ViewInfo CreateView(string name, string script)
        {
            throw new NotImplementedException();
        }

        public void AlterTable(string name, string script)
        {
            throw new NotImplementedException();
        }

        public void AlterView(string name, string script)
        {
            throw new NotImplementedException();
        }

        public void DropTable(string name)
        {
            throw new NotImplementedException();
        }

        public void DropView(string name)
        {
            throw new NotImplementedException();
        }

        public void DropSelect(string name)
        {
            throw new NotImplementedException();
        }

        public void CloseTable()
        {
            throw new NotImplementedException();
        }

        public void CloseDesignTable()
        {
            throw new NotImplementedException();
        }

        public bool OpenTable(long start, long pageSize, out DataTable datatable, out string statement)
        {
            throw new NotImplementedException();
        }

        public bool DesignTable(out DataTable table)
        {
            throw new NotImplementedException();
        }
    }
}
