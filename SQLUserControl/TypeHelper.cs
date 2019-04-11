using System.Windows.Forms;

using SQLDAL;

namespace SQLUserControl
{
    public enum NodeType
    {
        eConnect,
        eDatabase,
        eTableGroup,
        eTable,
        eViewGroup,
        eView,
        eSelectGroup,
        eSelect
    }

    public class NodeTypeInfo
    {
        public NodeTypeInfo(NodeType type, TreeNode node)
        {
            this.NodeType = type;
            this.Node = node;
        }

        public NodeTypeInfo(NodeType type, TreeNode node, ConnectInfo info)
        {
            this.NodeType = type;
            this.Node = node;
            this.ConnectionInfo = info;
        }

        public NodeTypeInfo(NodeType type, TreeNode node, DatabaseInfo info)
        {
            this.NodeType = type;
            this.Node = node;
            this.DatabaseInfo = info;
        }

        public NodeTypeInfo(NodeType type, TreeNode node, TableInfo info)
        {
            this.NodeType = type;
            this.Node = node;
            this.TableInfo = info;
        }

        public NodeTypeInfo(NodeType type, TreeNode node, ViewInfo info)
        {
            this.NodeType = type;
            this.Node = node;
            this.ViewInfo = info;
        }

        public NodeTypeInfo(NodeType type, TreeNode node, SelectInfo info)
        {
            this.NodeType = type;
            this.Node = node;
            this.SelectInfo = info;
        }

        public NodeType NodeType { get; set; }
        public ConnectInfo ConnectionInfo { get; set; }
        public DatabaseInfo DatabaseInfo { get; set; }
        public TableInfo TableInfo { get; set; }
        public ViewInfo ViewInfo { get; set; }
        public SelectInfo SelectInfo { get; set; }
        public TreeNode Node { get; set; }
        public TableListView TableList { get; set; }
        public ViewListView ViewList { get; set; }
        public SelectListView SelectList { get; set; }     
    }

    public enum TabPageType
    {
        eObject,
        eOpenTable,
        eNewTable,
        eDesignTable,
        eOpenView,
        eNewView,
        eDesignView,
        eOpenSelect,
        eNewSelect
    }

    public class TabPageTypeInfo
    {
        public TabPageTypeInfo(TabPageType type, object info)
        {
            this.PageType = type;
            this.Info = info;
        }
        public TabPageType PageType { get; set; }
        public object Info { get; set; }
        public OpenTablePage OpenTablePage { get; set; }
        public DesignTablePage DesignTablePage { get; set; }
        public OpenViewPage OpenViewPage { get; set; }
        public NewSelectPage NewSelectPage { get; set; }

    }
}