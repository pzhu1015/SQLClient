using System;
using System.Data;
using System.Windows.Forms;

namespace SQLDAL
{
    public abstract class TableInfo : ITableInfo
    {
        protected string name;
        protected string message;
        protected bool isOpen = false;
        protected bool isDesign = false;
        protected DatabaseInfo databaseInfo;
        protected TreeNode node;
        protected ListViewItem item;
        protected object openTablePage;
        protected object designTablePage;

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

        public bool IsDesign
        {
            get
            {
                return isDesign;
            }

            set
            {
                isDesign = value;
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

        public object OpenTablePage
        {
            get
            {
                return openTablePage;
            }

            set
            {
                openTablePage = value;
            }
        }

        public object DesignTablePage
        {
            get
            {
                return designTablePage;
            }

            set
            {
                designTablePage = value;
            }
        }

        public abstract bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement);
        public abstract bool Design(out DataTable table);
        public void Close()
        {
            this.isOpen = false;
        }

        public void CloseDesign()
        {
            this.isDesign = false;
        }
    }

   
}
