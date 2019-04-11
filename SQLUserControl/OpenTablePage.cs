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
using Helper;
using System.Reflection;

namespace SQLUserControl
{
    public partial class OpenTablePage : DevExpress.XtraEditors.XtraUserControl
    {
        public event ChangeStatusBarEventHandler ChangeStatusBar;
        private TableInfo tableInfo;
        private DatabaseInfo databaseInfo;
        public OpenTablePage()
        {
            InitializeComponent();
        }

        public TableInfo TableInfo
        {
            get
            {
                return tableInfo;
            }

            set
            {
                tableInfo = value;
                this.tgv.Info = this.tableInfo;
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

        protected virtual void OnChangeStatusBar(ChangeStatusBarEventArgs e)
        {
            if (this.ChangeStatusBar != null)
            {
                this.ChangeStatusBar(this, e);
            }
        }

        public void BindData(Int64 start)
        {
            this.tgv.BindData(this.tableInfo, start);
        }

        public void SetStatusBar()
        {
            this.SetStatusBar(this.tgv.CrtPage, this.tgv.Statement, this.tgv.RowIndex, this.tgv.RowCount);
        }

        public void SetStatusBar(Int64 crtPage, string statement, Int64 rowIndex, Int64 count)
        {
            this.OnChangeStatusBar(new ChangeStatusBarEventArgs(crtPage, rowIndex, count, statement));
        }

        private void tgv_ChangeStatusBar(object sender, ChangeStatusBarEventArgs e)
        {
            this.SetStatusBar(e.CrtPage, e.Statement, e.RowIndex, e.Count);
        }
    }
}
