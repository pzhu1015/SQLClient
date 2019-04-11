using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using SQLDAL;
using Helper;

namespace SQLUserControl
{
    public partial class TableListView : DevExpress.XtraEditors.XtraUserControl
    {
        private TreeView tree;
        private DatabaseInfo databaseInfo;
        private ImageList smallImageList;
        private View view;
        private bool labelEdit;
        public event OpenTableEventHandler OpenTable;
        public event DesignTableEventHandler DesignTable;
        public event NewTableEventHandler NewTable;
        public event ListViewShowMenuEventHandler ListViewShowMenu;

        public TableListView()
        {
            InitializeComponent();
            Type type = this.lvMain.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.lvMain, true, null);
        }

        public ImageList SmallImageList
        {
            get
            {
                return smallImageList;
            }

            set
            {
                smallImageList = value;
                this.lvMain.SmallImageList = this.smallImageList;
            }
        }

        public View View
        {
            get
            {
                return view;
            }

            set
            {
                view = value;
                this.lvMain.View = view;
            }
        }

        public bool LabelEdit
        {
            get
            {
                return labelEdit;
            }

            set
            {
                labelEdit = value;
                this.lvMain.LabelEdit = labelEdit;
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

        public TreeView Tree
        {
            get
            {
                return tree;
            }

            set
            {
                tree = value;
            }
        }

        #region event
        protected virtual void OnOpenTable(OpenTableEventArgs e)
        {
            if (this.OpenTable != null)
            {
                this.OpenTable(this, e);
            }
        }

        protected virtual void OnDesignTable(DesignTableEventArgs e)
        {
            if (this.DesignTable != null)
            {
                this.DesignTable(this, e);
            }
        }

        protected virtual void OnNewTable(NewTableEventArgs e)
        {
            if (this.NewTable != null)
            {
                this.NewTable(this, e);
            }
        }

        protected virtual void OnListViewShowMenu(ListViewShowMenuEventArgs e)
        {
            if (this.ListViewShowMenu != null)
            {
                this.ListViewShowMenu(this.lvMain, e);
            }
        }
        #endregion

        public void Clear()
        {
            this.lvMain.Clear();
        }

        public void AddItem(TableInfo info, string image_key)
        {
            ListViewItem item = this.lvMain.Items.Add(info.Name, image_key);
            item.Tag = info;
            info.Item = item;
        }

        public void SelectItem()
        {
            this.tsbtnOpenTable.Enabled = true;
            this.tsbtnDesignTable.Enabled = true;
            this.tsbtnDeleteTable.Enabled = true;
            this.tsbtnNewTable.Enabled = true;
        }

        public void UnSelectItem()
        {
            this.tsbtnOpenTable.Enabled = false;
            this.tsbtnDesignTable.Enabled = false;
            this.tsbtnDeleteTable.Enabled = false;
        }

        private TableInfo GetTableInfo()
        {
            TableInfo info = null;
            if (this.lvMain.Focused)
            {
                if (this.lvMain.SelectedItems.Count != 0)
                {
                    info = this.lvMain.SelectedItems[0].Tag as TableInfo;
                }
            }
            else if (this.tree.Focused)
            {
                if (this.tree.SelectedNode != null)
                {
                    NodeTypeInfo nodeTypeInfo = this.tree.SelectedNode.Tag as NodeTypeInfo;
                    info = nodeTypeInfo.TableInfo;
                }
            }
            return info;
        }

        private void lvMain_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            try
            {
                if ((sender as ListView).SelectedIndices.Count == 0)
                {
                    this.tsbtnOpenTable.Enabled = false;
                    this.tsbtnDesignTable.Enabled = false;
                    this.tsbtnDeleteTable.Enabled = false;
                }
                else
                {
                    this.tsbtnOpenTable.Enabled = true;
                    this.tsbtnDesignTable.Enabled = true;
                    this.tsbtnDeleteTable.Enabled = true;
                    this.tsbtnNewTable.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnDesignTable_Click(object sender, EventArgs e)
        {
            try
            {
                TableInfo info = this.GetTableInfo();
                if (info == null) return;
                this.OnDesignTable(new DesignTableEventArgs(info));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void lvMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                TableInfo info = this.GetTableInfo();
                if (info == null) return;
                this.OnOpenTable(new OpenTableEventArgs(info));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnOpenTable_Click(object sender, EventArgs e)
        {
            try
            {
                TableInfo info = this.GetTableInfo();
                if (info == null) return;
                this.OnOpenTable(new OpenTableEventArgs(info));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnNewTable_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnNewTable(new NewTableEventArgs(this.databaseInfo));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void lvMain_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    TableInfo info = this.GetTableInfo();
                    this.OnListViewShowMenu(new ListViewShowMenuEventArgs(info, this.databaseInfo, e));
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }

    
}
