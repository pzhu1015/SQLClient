using System;
using System.Data;
using System.Windows.Forms;

namespace SQLDAL
{
    public sealed class TableInfo : ITableInfo
    {
        private string name;
        private string message;
        private bool isOpen = false;
        private bool isDesign = false;
        private DatabaseInfo databaseInfo;
        private TreeNode node;
        private ListViewItem item;
        private object openTablePage;
        private object designTablePage;

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

        public bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            bool rslt = this.databaseInfo.ConnectInfo.OpenTable(this.databaseInfo.Name, this.name, start, pageSize, out datatable, out statement);
            if (!rslt)
            {
                this.message = this.databaseInfo.ConnectInfo.Message;
                return false;
            }
            this.isOpen = true;
            return true;
        }
        public bool Design(out DataTable table)
        {
            bool rslt = this.databaseInfo.ConnectInfo.DesignTable(this.databaseInfo.Name, this.name, out table);
            if (!rslt)
            {
                this.message = this.databaseInfo.ConnectInfo.Message;
                return false;
            }
            this.isDesign = true;
            return true;
        }
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
