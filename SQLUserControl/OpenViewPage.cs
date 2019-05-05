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
    public partial class OpenViewPage : DevExpress.XtraEditors.XtraUserControl
    {
        public event ChangeStatusBarEventHandler ChangeStatusBar;
        private ViewInfo viewInfo;
        private DatabaseInfo databaseInfo;
        //private ToolStripTextBox statement;
        //private ToolStripLabel pageInfo;
        public OpenViewPage()
        {
            InitializeComponent();
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
                this.tgv.Info = viewInfo;
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

        //public ToolStripTextBox Statement
        //{
        //    get
        //    {
        //        return statement;
        //    }

        //    set
        //    {
        //        statement = value;
        //    }
        //}

        //public ToolStripLabel PageInfo
        //{
        //    get
        //    {
        //        return pageInfo;
        //    }

        //    set
        //    {
        //        pageInfo = value;
        //    }
        //}

        protected virtual void OnChangeStatusBar(ChangeStatusBarEventArgs e)
        {
            if (this.ChangeStatusBar != null)
            {
                this.ChangeStatusBar(this, e);
            }
        }

        public void BindData(Int64 start)
        {
            this.tgv.BindData(this.viewInfo, start);
        }

        public void SetStatusBar()
        {
            this.SetStatusBar(this.tgv.CrtPage, this.tgv.Statement, this.tgv.RowIndex, this.tgv.RowCount);
        }

        public void SetStatusBar(Int64 crtPage, string statement, Int64 rowIndex, Int64 count)
        {
            //this.statement.Text = statement;
            //this.pageInfo.Text = $"第{rowIndex}条记录(共{count}条)于第{crt_page}页";
            this.OnChangeStatusBar(new ChangeStatusBarEventArgs(crtPage, rowIndex, count, statement));
        }

        private void tgv_ChangeStatusBar(object sender, ChangeStatusBarEventArgs e)
        {
            this.SetStatusBar(e.CrtPage, e.Statement, e.RowIndex, e.Count);
        }

        public void RefreshView()
        {
            throw new NotImplementedException();
        }

        public void RefreshDesignView()
        {
            throw new NotImplementedException();
        }
    }
}
