using Helper;
using SQLDAL.Properties;
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
        public event OpenTablesEventHandler OpenTables;
        public event OpenViewsEventHandler OpenViews;
        public event OpenSelectsEventHandler OpenSelects;
        public event CloseTablesEventHandler CloseTables;
        public event CloseViewsEventHandler CloseViews;
        public event CloseSelectsEventHandler CloseSelects;
        public event RefreshTablesEventHandler RefreshTables;
        public event RefreshViewsEventHandler RefreshViews;
        public event RefreshSelectEventHandler RefreshSelects;
        private TreeNode node;
        private string name;
        private string message;
        private bool isOpen = false;
        private ConnectInfo connectInfo;
        private List<TableInfo> tables = new List<TableInfo>();
        private List<ViewInfo> views = new List<ViewInfo>();
        private List<SelectInfo> selects = new List<SelectInfo>();
        private Dictionary<string, TableInfo> tableMaps = new Dictionary<string, TableInfo>();
        private Dictionary<string, ViewInfo> viewMaps = new Dictionary<string, ViewInfo>();
        private Dictionary<string, SelectInfo> selectMaps = new Dictionary<string, SelectInfo>();
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

        private void OnOpenTables(OpenTablesEventArgs e)
        {
            if (this.OpenTables != null)
            {
                this.OpenTables(this, e);
            }
        }

        private void OnOpenViews(OpenViewsEventArgs e)
        {
            if (this.OpenViews != null)
            {
                this.OpenViews(this, e);
            }
        }

        private void OnOpenSelects(OpenSelectsEventArgs e)
        {
            if (this.OpenSelects != null)
            {
                this.OpenSelects(this, e);
            }
        }

        private void OnCloseTables(CloseTablesEventArgs e)
        {
            if (this.CloseTables != null)
            {
                this.CloseTables(this, e);
            }
        }

        private void OnCloseViews(CloseViewsEventArgs e)
        {
            if (this.CloseViews != null)
            {
                this.CloseViews(this, e);
            }
        }

        private void OnCloseSelects(CloseSelectsEventArgs e)
        {
            if (this.CloseSelects != null)
            {
                this.CloseSelects(this, e);
            }
        }

        private void OnRefreshTables(RefreshTablesEventArgs e)
        {
            if (this.RefreshTables != null)
            {
                this.RefreshTables(this, e);
            }
        }

        private void OnRefreshViews(RefreshViewsEventArgs e)
        {
            if (this.RefreshViews != null)
            {
                this.RefreshViews(this, e);
            }
        }

        private void OnRefreshSelects(RefreshSelectsEventArgs e)
        {
            if (this.RefreshSelects != null)
            {
                this.RefreshSelects(this, e);
            }
        }

        public TableInfo AddTableInfo(string name)
        {
            TableInfo info = new TableInfo();
            info.Name = name;
            info.DatabaseInfo = this;
            this.tables.Add(info);
            this.tableMaps.Add(info.Name, info);
            return info;
        }

        public ViewInfo AddViewInfo(string name)
        {
            ViewInfo info = new ViewInfo();
            info.Name = name;
            info.DatabaseInfo = this;
            this.views.Add(info);
            this.viewMaps.Add(info.Name, info);
            return info;
        }

        public SelectInfo AddSelectInfo(string name)
        {
            SelectInfo info = new SelectInfo();
            info.Name = name;
            info.DatabaseInfo = this;
            this.selects.Add(info);
            this.selectMaps.Add(info.Name, info);
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
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.InsertSelectScript;
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

        public bool LoadTable()
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
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        this.AddTableInfo(dr[0].ToString());
                    }
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

        public bool LoadView()
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
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        this.AddViewInfo(dr[0].ToString());
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }

        public bool LoadSelect()
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.LoadSelectScript;
                    command.Parameters.Add(new SQLiteParameter("@database", this.name));
                    command.Parameters.Add(new SQLiteParameter("@connect", this.connectInfo.Name));
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        this.AddSelectInfo(dr[0].ToString());
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return false;
            }
        }

        public bool UnLoadTable()
        {
            foreach (TableInfo info in this.tables)
            {
                info.Close();
            }
            this.tables.Clear();
            this.tableMaps.Clear();
            return true;
        }

        public bool UnLoadView()
        {
            foreach (ViewInfo info in this.views)
            {
                info.Close();
            }
            this.views.Clear();
            this.viewMaps.Clear();
            return true;
        }

        public bool UnLoadSelect()
        {
            foreach (SelectInfo info in this.selects)
            {
                info.Close();
            }
            this.selects.Clear();
            this.selectMaps.Clear();
            return true;
        }

        public void AlterSelect(string selectName, string script)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.UpdateSelectScript;
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

                List<TableInfo> openTables = new List<TableInfo>();
                List<TableInfo> designTables = new List<TableInfo>();
                List<TableInfo> newTables = new List<TableInfo>();
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = this.connectInfo.GetLoadTableScript(this.name);
                    DbDataAdapter da = this.connectInfo.GetDataAdapter(command);
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string tableName = dr[0].ToString();
                        if (this.tableMaps.ContainsKey(tableName))
                        {
                            TableInfo tableInfo = this.tableMaps[tableName];
                            if (tableInfo.IsOpen)
                            {
                                openTables.Add(tableInfo);
                            }
                            if (tableInfo.IsDesign)
                            {
                                designTables.Add(tableInfo);
                            }
                        }
                        else
                        {
                            TableInfo tableInfo = this.AddTableInfo(tableName);
                            newTables.Add(tableInfo);
                        }
                    }
                }
                this.OnRefreshTables(new RefreshTablesEventArgs(this, newTables, openTables, designTables));
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
                List<ViewInfo> newViews = new List<ViewInfo>();
                List<ViewInfo> openViews = new List<ViewInfo>();
                List<ViewInfo> designViews = new List<ViewInfo>();
                using (DbConnection connection = this.connectInfo.GetConnection(this.name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = this.connectInfo.GetLoadViewScript(this.name);
                    DbDataAdapter da = this.connectInfo.GetDataAdapter(command);
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string viewName = dr[0].ToString();
                        if (this.viewMaps.ContainsKey(viewName))
                        {
                            ViewInfo viewInfo = this.viewMaps[viewName];
                            if (viewInfo.IsOpen)
                            {
                                openViews.Add(viewInfo);
                            }
                            if (viewInfo.IsDesign)
                            {
                                designViews.Add(viewInfo);
                            }
                        }
                        else
                        {
                            ViewInfo viewInfo = this.AddViewInfo(viewName);
                            newViews.Add(viewInfo);
                        }
                    }
                }
                this.OnRefreshViews(new RefreshViewsEventArgs(this, newViews, openViews, designViews));
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
                };
                List<SelectInfo> newSelects = new List<SelectInfo>();
                List<SelectInfo> openSelects = new List<SelectInfo>();
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = Resources.LoadSelectScript;
                    command.Parameters.Add(new SQLiteParameter("@database", this.name));
                    command.Parameters.Add(new SQLiteParameter("@connect", this.connectInfo.Name));
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string selectName = dr[0].ToString();
                        if (this.selectMaps.ContainsKey(selectName))
                        {
                            SelectInfo selectInfo = this.selectMaps[selectName];
                            if (selectInfo.IsOpen)
                            {
                                openSelects.Add(selectInfo);
                            }
                        }
                        else
                        {
                            SelectInfo selectInfo = this.AddSelectInfo(selectName);
                            newSelects.Add(selectInfo);
                        }
                    }
                }
                this.OnRefreshSelects(new RefreshSelectsEventArgs(this, newSelects, openSelects));
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

                this.LoadTable();
                this.OnOpenTables(new OpenTablesEventArgs(this));

                this.LoadView();
                this.OnOpenViews(new OpenViewsEventArgs(this));

                this.LoadSelect();
                this.OnOpenSelects(new OpenSelectsEventArgs(this));

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
            this.RefreshTable();
            this.RefreshView();
            this.RefreshSelect();
        }

        public bool Close()
        {
            try
            {
                if (!this.isOpen)
                {
                    return true;
                }
                this.UnLoadTable();
                this.OnCloseTables(new CloseTablesEventArgs(this));
                this.UnLoadView();
                this.OnCloseViews(new CloseViewsEventArgs(this));
                this.UnLoadSelect();
                this.OnCloseSelects(new CloseSelectsEventArgs(this));
                this.isOpen = false;
                this.OnCloseDatabase(new CloseDatabaseEventArgs(this));
                return true;
            }
            catch (Exception ex)
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
