using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SQLDAL;
using System.Reflection;
using Helper;

namespace SQLUserControl
{
    public partial class SelectListView : DevExpress.XtraEditors.XtraUserControl
    {
        public event NewSelectEventHandler NewSelect;
        public event OpenSelectEventHandler OpenSelect;
        public event ListViewShowMenuEventHandler ListViewShowMenu;
        private TreeView tree;
        private DatabaseInfo databaseInfo;
        private ImageList smallImageList;
        private View view;
        private bool labelEdit;

        public SelectListView()
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

        #region Event
        protected virtual void OnNewSelect(NewSelectEventArgs e)
        {
            if (this.NewSelect != null)
            {
                this.NewSelect(this, e);
            }
        }

        protected virtual void OnOpenSelect(OpenSelectEventArgs e)
        {
            if (this.OpenSelect != null)
            {
                this.OpenSelect(this, e);
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

        public void SelectItem()
        {
            this.tsbtnOpenSelect.Enabled = true;
            this.tsbtnDesignSelect.Enabled = true;
            this.tsbtnDeleteSelect.Enabled = true;
            this.tsbtnNewSelect.Enabled = true;
        }

        public void UnSelectItem()
        {
            this.tsbtnOpenSelect.Enabled = false;
            this.tsbtnDesignSelect.Enabled = false;
            this.tsbtnDeleteSelect.Enabled = false;
        }

        public void AddItem(SelectInfo info, string image_key)
        {
            ListViewItem item =  this.lvMain.Items.Add(info.Name, image_key);
            item.Tag = info;
            info.Item = item;
        }

        private SelectInfo GetSelecctInfo()
        {
            SelectInfo info = null;
            if (this.lvMain.Focused)
            {
                if (this.lvMain.SelectedItems.Count != 0)
                {
                    info = this.lvMain.SelectedItems[0].Tag as SelectInfo;
                }
            }
            else if (this.tree.Focused)
            {
                if (this.tree.SelectedNode != null)
                {
                    NodeTypeInfo nodeTypeInfo = this.tree.SelectedNode.Tag as NodeTypeInfo;
                    info = nodeTypeInfo.SelectInfo;
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
                    this.tsbtnOpenSelect.Enabled = false;
                    this.tsbtnDesignSelect.Enabled = false;
                    this.tsbtnDeleteSelect.Enabled = false;
                }
                else
                {
                    this.tsbtnOpenSelect.Enabled = true;
                    this.tsbtnDesignSelect.Enabled = true;
                    this.tsbtnDeleteSelect.Enabled = true;
                    this.tsbtnNewSelect.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnNewSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnNewSelect(new NewSelectEventArgs(this.databaseInfo));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnOpenSelect_Click(object sender, EventArgs e)
        {
            try
            {
                SelectInfo info = this.GetSelecctInfo();
                if (info == null) return;
                this.OnOpenSelect(new OpenSelectEventArgs(info));
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
                SelectInfo info = this.GetSelecctInfo();
                if (info == null) return;
                this.OnOpenSelect(new OpenSelectEventArgs(info));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void lvMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SelectInfo info = this.GetSelecctInfo();
                this.OnListViewShowMenu(new ListViewShowMenuEventArgs(info, this.databaseInfo, e));
            }
        }
    }
   
}
