using System;
using System.Data;
using System.Windows.Forms;

namespace SQLDAL
{
    public abstract class ViewInfo : IViewInfo
    {
        protected string name;
        protected string message;
        protected bool isOpen = false;
        protected DatabaseInfo databaseInfo;
        protected TreeNode node;
        protected ListViewItem item;
        protected object openViewPage;

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

        public ListViewItem Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
            }
        }

        public object OpenViewPage
        {
            get
            {
                return openViewPage;
            }

            set
            {
                openViewPage = value;
            }
        }

        public abstract void Close();
        public abstract bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement);
        public abstract void Design();
    }
}
