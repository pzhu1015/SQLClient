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
        private TextBox txtEdit;
        public QueryRslt()
        {
            InitializeComponent();
            
        }

        private void txtEdit_Leave(object sender, EventArgs e)
        {
            DataGridViewCell crtCell = (sender as TextBox).Tag as DataGridViewCell;
            string str = (sender as TextBox).Text;
            if (crtCell.Value == DBNull.Value && string.IsNullOrEmpty(str))
            {
                crtCell.Value = null;
            }
            else
            {
                crtCell.Value = str;
            }
            this.txtEdit.Width = 0;
            this.txtEdit.Visible = false;
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
                for (int i = 0; i < this.dgv.Columns.Count; i++)
                {
                    this.dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
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
        private void DisableEditControl()
        {
            this.txtEdit.Visible = false;
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //disable other
                this.DisableEditControl();
                this.txtEdit.Tag = this.dgv.CurrentCell;
                Rectangle rec = this.dgv.GetCellDisplayRectangle(this.dgv.CurrentCell.ColumnIndex, this.dgv.CurrentCell.RowIndex, true);
                this.txtEdit.Left = rec.Left;
                this.txtEdit.Top = rec.Top;
                this.txtEdit.Width = rec.Width;
                this.txtEdit.Text = this.dgv.CurrentCell.Value != DBNull.Value ? this.dgv.CurrentCell.Value.ToString() : "";
                this.txtEdit.BringToFront();
                this.txtEdit.Visible = true;
                this.txtEdit.Focus();
                this.txtEdit.SelectionStart = this.txtEdit.Text.Length;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void QueryRslt_Load(object sender, EventArgs e)
        {
            Type type = this.dgv.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgv, true, null);

            this.txtEdit = new TextBox();
            this.txtEdit.Visible = false;
            this.txtEdit.Width = 0;
            this.txtEdit.Leave += txtEdit_Leave;
            this.dgv.Controls.Add(this.txtEdit);
        }
    }
}
