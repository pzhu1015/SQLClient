using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Reflection;
using Helper;
using SQLDAL;

namespace SQLUserControl
{
    public partial class TableGridView : DevExpress.XtraEditors.XtraUserControl
    {
        private int pageSize = 1000;
        private object info;
        private Int64 crtPage;
        private Int64 rowIndex;
        private Int64 rowCount;
        private string statement;
        private TextBox txtEdit = null;
        
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

        public long CrtPage
        {
            get
            {
                return crtPage;
            }

            set
            {
                crtPage = value;
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

        public long RowCount
        {
            get
            {
                return rowCount;
            }

            set
            {
                rowCount = value;
            }
        }

        public object Info
        {
            get
            {
                return info;
            }

            set
            {
                info = value;
            }
        }

        public event ChangeStatusBarEventHandler ChangeStatusBar;

        public TableGridView()
        {
            InitializeComponent();
        }

        private void DisableEditControl()
        {
            this.txtEdit.Visible = false;
        }

        public void BindData(object info, Int64 start)
        {
            try
            {
                DataTable dt = null;
                if (info is TableInfo)
                {
                    bool rslt = (info as TableInfo).Open(start, this.pageSize, out dt, out this.statement);
                    if (!rslt)
                    {
                        MessageBox.Show((info as TableInfo).Message);
                        return;
                    }
                }
                else if (info is ViewInfo)
                {
                    bool rslt = (info as ViewInfo).Open(start, this.pageSize, out dt, out this.statement);
                    if (!rslt)
                    {
                        MessageBox.Show((info as ViewInfo).Message);
                        return;
                    }
                }
                this.dgv.DataSource = dt;
                this.rowCount = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        protected virtual void OnChangeStatusBar(ChangeStatusBarEventArgs e)
        {
            if (this.ChangeStatusBar != null)
            {

                this.tstxtCurentPage.Text = e.CrtPage.ToString();
                this.ChangeStatusBar(this, e);
            }
        }

        private void TableGridView_Load(object sender, EventArgs e)
        {
            Type type = this.dgv.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgv, true, null);

            this.dgv.MouseWheel += dgv_MouseWheel;
            this.txtEdit = new TextBox();
            this.txtEdit.Visible = false;
            this.txtEdit.Width = 0;
            this.txtEdit.Leave += txtEdit_Leave;
            this.dgv.Controls.Add(this.txtEdit);
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

        private void dgv_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                this.DisableEditControl();
                if (this.dgv.CurrentCell != null)
                {
                    DataGridViewCell cell = this.dgv.CurrentCell;
                    int ridx = cell.RowIndex;
                    int cidx = cell.ColumnIndex;
                    if (e.Delta > 0)
                    {
                        if (ridx > 0)
                        {
                            cell = this.dgv.Rows[ridx - 1].Cells[cidx];
                            this.dgv.CurrentCell = cell;
                        }
                    }
                    else
                    {
                        if (ridx < this.dgv.Rows.Count)
                        {
                            cell = this.dgv.Rows[ridx + 1].Cells[cidx];
                            this.dgv.CurrentCell = cell;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgv_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                this.crtPage = Convert.ToInt64(this.tstxtCurentPage.Text);
                int rowIdx = 0;
                if (this.dgv.CurrentCell != null)
                {
                    rowIdx = this.dgv.CurrentRow.Index;
                }
                rowIdx += 1;
                if (rowIdx != this.rowIndex)
                {
                    this.rowIndex = rowIdx;
                    this.OnChangeStatusBar(new ChangeStatusBarEventArgs(this.crtPage, this.rowIndex, this.rowCount, this.statement));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
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

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.DisableEditControl();
                //if (e.NewValue >= this.dgv.CurrentCell.RowIndex)
                //{
                //    return;
                //}
                //this.dgv.FirstDisplayedScrollingRowIndex = e.NewValue;
                //this.dgv.CurrentCell = this.dgv.Rows[e.NewValue].Cells[this.dgv.CurrentCell.ColumnIndex];
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnFristPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                this.crtPage = Convert.ToInt64(this.tstxtCurentPage.Text);
                if (this.crtPage == 1)
                {
                    return;
                }
                Int64 start = 0 * this.pageSize;
                this.BindData(this.info, start);
                this.rowIndex = 0;
                if (this.dgv.CurrentCell != null)
                {
                    this.rowIndex = this.dgv.CurrentCell.RowIndex;
                }
                rowIndex += 1;
                this.crtPage = 1;
                this.OnChangeStatusBar(new ChangeStatusBarEventArgs(this.crtPage, this.rowIndex, this.rowCount, this.statement));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnPrevPage_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                this.crtPage = Convert.ToInt64(this.tstxtCurentPage.Text);
                if (this.crtPage == 1)
                {
                    return;
                }
                Int64 start = (this.crtPage - 2) * this.pageSize;
                this.BindData(this.info, start);
                this.rowIndex = 0;
                if (this.dgv.CurrentCell != null)
                {
                    this.rowIndex = this.dgv.CurrentCell.RowIndex;
                }
                rowIndex += 1;
                this.crtPage -= 1;
                this.OnChangeStatusBar(new ChangeStatusBarEventArgs(this.crtPage, this.rowIndex, this.rowCount, this.statement));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnNextPage_Click(object sender, EventArgs e)
        {
            this.DisableEditControl();
            this.crtPage = Convert.ToInt64(this.tstxtCurentPage.Text);
            Int64 start = this.crtPage * this.pageSize;
            this.BindData(this.info, start);
            this.rowIndex = 0;
            if (this.dgv.CurrentCell != null)
            {
                this.rowIndex = this.dgv.CurrentCell.RowIndex;
            }
            this.rowIndex += 1;
            this.crtPage += 1;
            this.OnChangeStatusBar(new ChangeStatusBarEventArgs(this.crtPage, this.rowIndex, this.rowCount, this.statement));
        }

        private void tsbtnLastPage_Click(object sender, EventArgs e)
        {

        }

        private void tsbtnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                if (!this.tstxtPageSize.Visible)
                {
                    this.tstxtPageSize.Visible = true;
                    this.tstxtPageSize.Text = this.pageSize.ToString();
                    this.tslblRecordPerPage.Visible = true;

                    this.tsbtnFristPage.Visible = false;
                    this.tsbtnPrevPage.Visible = false;
                    this.tstxtCurentPage.Visible = false;
                    this.tsbtnNextPage.Visible = false;
                    this.tsbtnLastPage.Visible = false;
                }
                else
                {
                    this.tstxtPageSize.Visible = false;
                    this.pageSize = Convert.ToInt32(this.tstxtPageSize.Text);
                    this.tslblRecordPerPage.Visible = false;

                    this.tsbtnFristPage.Visible = true;
                    this.tsbtnPrevPage.Visible = true;
                    this.tstxtCurentPage.Visible = true;
                    this.tsbtnNextPage.Visible = true;
                    this.tsbtnLastPage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tstxtPageSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DisableEditControl();
                this.pageSize = Convert.ToInt32(this.tstxtPageSize.Text);
                Int64 crtPage = Convert.ToInt64(this.tstxtCurentPage.Text);
                Int64 start = crtPage * this.pageSize;
                this.BindData(this.info, start);
                int rowIndex = 0;
                if (this.dgv.CurrentCell != null)
                {
                    rowIndex = this.dgv.CurrentCell.RowIndex;
                }
                rowIndex += 1;
                this.OnChangeStatusBar(new ChangeStatusBarEventArgs(crtPage, rowIndex, this.dgv.RowCount, this.statement));
            }
        }
    }
}
