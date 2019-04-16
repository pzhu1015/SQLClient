using SQLDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLDAL
{
    #region Select Event
    public delegate void NewSelectEventHandler(object sender, NewSelectEventArgs e);
    public class NewSelectEventArgs : EventArgs
    {
        private DatabaseInfo databaseInfo;
        public NewSelectEventArgs(DatabaseInfo info)
            :
            base()
        {
            this.databaseInfo = info;
        }

        public DatabaseInfo DatabaseInfo
        {
            get
            {
                return databaseInfo;
            }

            set
            {
                databaseInfo = value;
            }
        }
    }

    public delegate void OpenSelectEventHandler(object sender, OpenSelectEventArgs e);
    public class OpenSelectEventArgs : EventArgs
    {
        private SelectInfo info;
        public OpenSelectEventArgs(SelectInfo info)
            :
            base()
        {
            this.info = info;
        }

        public SelectInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }

    #endregion

    #region Table Event
    public delegate void OpenTableEventHandler(object sender, OpenTableEventArgs e);
    public class OpenTableEventArgs : EventArgs
    {
        private TableInfo info;
        public OpenTableEventArgs(TableInfo info)
            :
            base()
        {
            this.info = info;
        }

        public TableInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }

    public delegate void DesignTableEventHandler(object sender, DesignTableEventArgs e);
    public class DesignTableEventArgs : EventArgs
    {
        private TableInfo info;
        public DesignTableEventArgs(TableInfo info)
            :
            base()
        {
            this.info = info;
        }

        public TableInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }

    public delegate void NewTableEventHandler(object sender, NewTableEventArgs e);
    public class NewTableEventArgs : EventArgs
    {
        private DatabaseInfo databaseInfo;
        public NewTableEventArgs(DatabaseInfo databaseInfo)
            :
            base()
        {
            this.databaseInfo = databaseInfo;
        }

        public DatabaseInfo DatabaseInfo
        {
            get
            {
                return databaseInfo;
            }

            set
            {
                databaseInfo = value;
            }
        }
    }

    public delegate void CloseTableEventHandler(object sender, CloseTableEventArgs e);
    public class CloseTableEventArgs : EventArgs
    {
        private TableInfo info;
        public CloseTableEventArgs(TableInfo info)
            :
            base()
        {
            this.info = info;
        }

        public TableInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }

    public delegate void CloseDesignTableEventHandler(object sender, CloseDesignTableEventArgs e);
    public class CloseDesignTableEventArgs : EventArgs
    {
        private TableInfo info;
        public CloseDesignTableEventArgs(TableInfo info)
            :
            base()
        {
            this.info = info;
        }
    }

    #endregion

    #region View Evnet
    public delegate void OpenViewEventHandler(object sender, OpenViewEventArgs e);
    public class OpenViewEventArgs : EventArgs
    {
        private ViewInfo info;
        public OpenViewEventArgs(ViewInfo info)
            :
            base()
        {
            this.info = info;
        }

        public ViewInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }

    public delegate void DesignViewEventHandler(object sender, DesignViewEventArgs e);
    public class DesignViewEventArgs : EventArgs
    {
        private ViewInfo info;
        public DesignViewEventArgs(ViewInfo info)
            :
            base()
        {
            this.info = info;
        }

        public ViewInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }
    #endregion

    #region Database Event
    public delegate void CloseDatabaseEventHandler(object sender, CloseDatabaseEventArgs e);
    public class CloseDatabaseEventArgs : EventArgs
    {
        private DatabaseInfo info;
        public CloseDatabaseEventArgs(DatabaseInfo info)
            :
            base()
        {
            this.info = info;
        }

        public DatabaseInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }

    public delegate void OpenDatabaseEventHandler(object sender, OpenDatabaseEventArgs e);
    public class OpenDatabaseEventArgs : EventArgs
    {
        private DatabaseInfo info;
        public OpenDatabaseEventArgs(DatabaseInfo info)
            :
            base()
        {
            this.info = info;
        }

        public DatabaseInfo Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }
    }
    #endregion

    public delegate void ListViewShowMenuEventHandler(object sender, ListViewShowMenuEventArgs e);
    public class ListViewShowMenuEventArgs: EventArgs
    {
        private DatabaseInfo databaseInfo;
        private TableInfo tableInfo;
        private SelectInfo selectInfo;
        private ViewInfo viewInfo;
        private MouseEventArgs mouseEvent;

        public TableInfo TableInfo
        {
            get
            {
                return tableInfo;
            }

            set
            {
                tableInfo = value;
            }
        }

        public SelectInfo SelectInfo
        {
            get
            {
                return selectInfo;
            }

            set
            {
                selectInfo = value;
            }
        }

        public ViewInfo ViewInfo
        {
            get
            {
                return viewInfo;
            }

            set
            {
                viewInfo = value;
            }
        }

        public MouseEventArgs MouseEvent
        {
            get
            {
                return mouseEvent;
            }

            set
            {
                mouseEvent = value;
            }
        }

        public DatabaseInfo DatabaseInfo
        {
            get
            {
                return databaseInfo;
            }

            set
            {
                databaseInfo = value;
            }
        }

        public ListViewShowMenuEventArgs(TableInfo info, DatabaseInfo databaseInfo, MouseEventArgs e)
            :
            base()
        {
            this.tableInfo = info;
            this.databaseInfo = databaseInfo;
            this.mouseEvent = e;
        }

        public ListViewShowMenuEventArgs(ViewInfo info, DatabaseInfo databaseInfo, MouseEventArgs e)
           :
           base()
        {
            this.viewInfo = info;
            this.databaseInfo = databaseInfo;
            this.mouseEvent = e;
        }

        public ListViewShowMenuEventArgs(SelectInfo info, DatabaseInfo databaseInfo, MouseEventArgs e)
           :
           base()
        {
            this.selectInfo = info;
            this.databaseInfo = databaseInfo;
            this.mouseEvent = e;
        }
    }

    public delegate void ChangeStatusBarEventHandler(object sender, ChangeStatusBarEventArgs e);
    public class ChangeStatusBarEventArgs: EventArgs
    {
        private int fieldCount = 0;
        private bool reset = false;
        private Int64 cost;
        private Int64 crtPage;
        private Int64 rowIndex;
        private Int64 count;
        private string statement;

        public long CrtPage
        {
            get
            {
                return crtPage;
            }

            set
            {
                crtPage = value;
            }
        }

        public long RowIndex
        {
            get
            {
                return rowIndex;
            }

            set
            {
                rowIndex = value;
            }
        }

        public long Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        public string Statement
        {
            get
            {
                return statement;
            }

            set
            {
                statement = value;
            }
        }

        public long Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        public bool Reset
        {
            get
            {
                return reset;
            }

            set
            {
                reset = value;
            }
        }

        public int FieldCount
        {
            get
            {
                return fieldCount;
            }

            set
            {
                fieldCount = value;
            }
        }

        public ChangeStatusBarEventArgs(Int64 crtPage, Int64 rowIndex, Int64 count, string statement)
            :
            base()
        {
            this.crtPage = crtPage;
            this.rowIndex = rowIndex;
            this.count = count;
            this.statement = statement;
        }

        public ChangeStatusBarEventArgs(bool reset)
            :
            base()
        {
            this.reset = reset;
        }

        public ChangeStatusBarEventArgs(Int64 rowIndex, Int64 count, string statement, Int64 cost)
            :
            base()
        {
            this.cost = cost;
            this.rowIndex = rowIndex;
            this.count = count;
            this.statement = statement;
        }

        public ChangeStatusBarEventArgs(int count)
           :
           base()
        {
            this.fieldCount = count;
        }
    }
}
