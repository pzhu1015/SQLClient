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
    public partial class ViewListView : DevExpress.XtraEditors.XtraUserControl
    {
        private TreeView tree;
        public event OpenViewEventHandler OpenView;
        public event DesignViewEventHandler DesignView;
        public event ListViewShowMenuEventHandler ListViewShowMenu;
        public ViewListView()
        {
            InitializeComponent();
            Type type = this.lvMain.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.lvMain, true, null);
        }

        private DatabaseInfo databaseInfo;
        private ImageList smallImageList;
        private View view;
        private bool labelEdit;

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
        protected virtual void OnOpenView(OpenViewEventArgs e)
        {
            if (this.OpenView != null)
            {
                this.OpenView(this, e);
            }
        }

        protected virtual void OnDesignView(DesignViewEventArgs e)
        {
            if (this.DesignView != null)
            {
                this.DesignView(this, e);
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

        public void AddItem(ViewInfo info, string image_key)
        {
            ListViewItem item = this.lvMain.Items.Add(info.Name, image_key);
            item.Tag = info;
            info.Item = item;
        }

        public void SelectItem()
        {
            this.tsbtnOpenView.Enabled = true;
            this.tsbtnDesignView.Enabled = true;
            this.tsbtnDeleteView.Enabled = true;
            this.tsbtnNewView.Enabled = true;
        }

        public void UnSelectItem()
        {
            this.tsbtnOpenView.Enabled = false;
            this.tsbtnDesignView.Enabled = false;
            this.tsbtnDeleteView.Enabled = false;
        }

        private ViewInfo GetViewInfo()
        {
            ViewInfo info = null;
            if (this.lvMain.Focused)
            {
                if (this.lvMain.SelectedItems.Count != 0)
                {
                    info = this.lvMain.SelectedItems[0].Tag as ViewInfo;
                }
            }
            else if (this.tree.Focused)
            {
                if (this.tree.SelectedNode != null)
                {
                    NodeTypeInfo nodeTypeInfo = this.tree.SelectedNode.Tag as NodeTypeInfo;
                    info = nodeTypeInfo.ViewInfo;
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
                    this.tsbtnOpenView.Enabled = false;
                    this.tsbtnDesignView.Enabled = false;
                    this.tsbtnDeleteView.Enabled = false;
                }
                else
                {
                    this.tsbtnOpenView.Enabled = true;
                    this.tsbtnDesignView.Enabled = true;
                    this.tsbtnDeleteView.Enabled = true;
                    this.tsbtnNewView.Enabled = true;
                }
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
                ViewInfo info = this.GetViewInfo();
                if (info == null) return;
                this.OnOpenView(new OpenViewEventArgs(info));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnOpenView_Click(object sender, EventArgs e)
        {
            try
            {
                ViewInfo info = this.GetViewInfo();
                if (info == null) return;
                this.OnOpenView(new OpenViewEventArgs(info));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnDesignView_Click(object sender, EventArgs e)
        {
            try
            {
                ViewInfo info = this.GetViewInfo();
                if (info == null) return;
                this.OnDesignView(new DesignViewEventArgs(info));
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
                    ViewInfo info = this.GetViewInfo();
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
