using System;
using System.Data;
using System.Windows.Forms;

namespace SQLDAL
{
    public sealed class ViewInfo : IViewInfo
    {
        private string name;
        private string message;
        private bool isOpen = false;
        private bool isDesign = false;
        private DatabaseInfo databaseInfo;
        private TreeNode node;
        private ListViewItem item;
        private object openViewPage;

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

        public void Close()
        {
            this.isOpen = false;
        }
        public bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            bool rslt = this.databaseInfo.ConnectInfo.OpenView(this.databaseInfo.Name, this.name, start, pageSize, out datatable, out statement);
            if (!rslt)
            {
                this.message = this.databaseInfo.ConnectInfo.Message;
                return false;
            }
            this.IsOpen = true;
            return true;
        }

        public bool Design(string database, string viewname, out DataTable table)
        {
            bool rslt = this.databaseInfo.ConnectInfo.DesignView(this.databaseInfo.Name, this.name, out table);
            if (!rslt)
            {
                this.message = this.databaseInfo.ConnectInfo.Message;
                return false;
            }
            this.isDesign = true;
            return true;
        }
    }
}
