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
using Helper;

namespace SQLUserControl
{
    public partial class QueryRslt : DevExpress.XtraEditors.XtraUserControl
    {
        public event EventHandler CurrentCellChange;
        private Int64 cost;
        private string statement;
        private Int64 rowIndex;
        private Int64 count;
        public QueryRslt()
        {
            InitializeComponent();
            Type type = this.dgv.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgv, true, null);
        }

        public string Statement
        {
            get
            {
                return statement;
            }

            set
            {
                statement = value;
            }
        }

        public long Cost
        {
            get
            {
                return cost;
            }

            set
            {
                cost = value;
            }
        }

        public long RowIndex
        {
            get
            {
                return rowIndex;
            }

            set
            {
                rowIndex = value;
            }
        }

        public long Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        protected virtual void OnCurrentCellChange(EventArgs e)
        {
            if (this.CurrentCellChange != null)
            {
                this.CurrentCellChange(this, e);
            }
        }

        public void BindData(DataTable dt)
        {
            if (dt != null)
            {
                this.dgv.DataSource = dt;
                this.count = dt.Rows.Count;
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            this.rowIndex = 0;
            if (this.dgv.CurrentCell != null)
            {
                this.rowIndex = this.dgv.CurrentCell.RowIndex;
            }
            this.rowIndex += 1;
            this.OnCurrentCellChange(e);
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.Value == null || Convert.ToString(e.Value) == "")
                {
                    this.dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }
}
