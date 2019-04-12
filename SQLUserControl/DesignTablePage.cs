using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SQLDAL;
using System.Reflection;
using System.Drawing;
using Helper;
using System.Data;
using DevExpress.XtraTab;
using SQLUserControl.Properties;

namespace SQLUserControl
{
    public partial class DesignTablePage : DevExpress.XtraEditors.XtraUserControl
    {
        public event ChangeStatusBarEventHandler ChangeStatusBar;
        public NewTableEventHandler NewTable;
        private int rowCount;
        private TableInfo tableInfo;
        private DatabaseInfo databaseInfo;
        private XtraTabPage page;
        private ComboBox cmbDataTypes = new ComboBox();
        private TextBox txtEdit = new TextBox();

        public DesignTablePage()
        {
            InitializeComponent();

            this.dgvFields.MouseWheel += dgvFields_MouseWheel;

            Type type = this.dgvFields.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvFields, true, null);
            type = this.dgvIndexs.GetType();
            pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvIndexs, true, null);

            this.cmbDataTypes.Visible = false;
            this.cmbDataTypes.Width = 0;
            this.cmbDataTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDataTypes.SelectionChangeCommitted += CmbDataTypes_SelectionChangeCommitted;
            this.cmbDataTypes.Leave += CmbDataTypes_Leave;
            this.dgvFields.Controls.Add(this.cmbDataTypes);
            
            this.txtEdit.Visible = false;
            this.txtEdit.Width = 0;
            this.txtEdit.Leave += TxtEdit_Leave;
            this.dgvFields.Controls.Add(this.txtEdit);

            this.dgvFields.AutoGenerateColumns = false;
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

        public TableInfo TableInfo
        {
            get
            {
                return tableInfo;
            }

            set
            {
                tableInfo = value;
            }
        }

        public XtraTabPage Page
        {
            get
            {
                return page;
            }

            set
            {
                page = value;
            }
        }

        protected virtual void OnNewTable(NewTableEventArgs e)
        {
            if (this.NewTable != null)
            {
                this.NewTable(this, e);
            }
        }

        protected virtual void OnChangeStatusBar(ChangeStatusBarEventArgs e)
        {
            if (this.ChangeStatusBar != null)
            {
                this.ChangeStatusBar(this, e);
            }
        }

        private Bitmap GetBlank()
        {
            Bitmap NewBmp = new Bitmap(80, 22);
            Graphics G = Graphics.FromImage(NewBmp);
            G.DrawString("", new Font("宋体", 9), new SolidBrush(Color.Transparent), new PointF(0, 0));
            G.Dispose();
            return NewBmp;
        }

        private Bitmap GetPrimaryKey(int i)
        {
            Bitmap Bmp = Resources.key_16;
            Bitmap NewBmp = new Bitmap(80, 22);
            Graphics G = Graphics.FromImage(NewBmp);
            G.DrawImage(Bmp, 20, 3, Bmp.Width, Bmp.Height);
            string index_str = (i + 1).ToString();
            Font font = new Font("宋体", 9, FontStyle.Regular);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            SizeF sizef = G.MeasureString(index_str, font, PointF.Empty, format);
            RectangleF rect = new RectangleF(20 + Bmp.Width + 2, 3, sizef.Width, sizef.Height);
            SolidBrush brush = new SolidBrush(Color.Black);
            G.DrawString(index_str, font, brush, rect);
            G.Dispose();
            return NewBmp;
        }

        public void SetStatusBar()
        {
            this.OnChangeStatusBar(new ChangeStatusBarEventArgs(this.rowCount));
        }

        public void BindData()
        {
            DataTable dt;
            this.tableInfo.Design(out dt);
            this.dgvFields.DataSource = dt;
            this.rowCount = dt.Rows.Count;
            this.SetStatusBar();
        }

        private void DisableEditControl()
        {
            this.txtEdit.Visible = false;
            this.cmbDataTypes.Visible = false;
        }

        #region EditControl Event
        private void CmbDataTypes_Leave(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell crtCell = (sender as ComboBox).Tag as DataGridViewCell;
                crtCell.Value = (sender as ComboBox).Text.ToString();
                this.cmbDataTypes.Width = 0;
                this.cmbDataTypes.Visible = false;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvFields_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                this.DisableEditControl();
                if (this.dgvFields.CurrentCell != null)
                {
                    DataGridViewCell cell = this.dgvFields.CurrentCell;
                    int ridx = cell.RowIndex;
                    int cidx = cell.ColumnIndex;
                    if (e.Delta > 0)
                    {
                        if (ridx > 0)
                        {
                            cell = this.dgvFields.Rows[ridx - 1].Cells[cidx];
                            this.dgvFields.CurrentCell = cell;
                        }
                    }
                    else
                    {
                        if (ridx < this.dgvFields.Rows.Count)
                        {
                            cell = this.dgvFields.Rows[ridx + 1].Cells[cidx];
                            this.dgvFields.CurrentCell = cell;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void TxtEdit_Leave(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell crtCell = (sender as TextBox).Tag as DataGridViewCell;
                crtCell.Value = (sender as TextBox).Text.ToString();
                this.txtEdit.Width = 0;
                this.txtEdit.Visible = false;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void CmbDataTypes_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell crtCell = (sender as ComboBox).Tag as DataGridViewCell;
                crtCell.Value = (sender as ComboBox).SelectedItem.ToString();
                this.cmbDataTypes.Width = 0;
                this.cmbDataTypes.Visible = false;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
        #endregion

        #region Fields DataGridView Event

        private void dgvFields_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                this.dgvFields.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvFields_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (Convert.ToBoolean(e.Value))
                    {

                        e.Value = this.GetPrimaryKey(e.RowIndex);
                    }
                    else
                    {
                        e.Value = this.GetBlank();
                    }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvFields_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int idx = this.dgvFields.CurrentCell.ColumnIndex;
                if (idx == 1)
                {
                    //disable other
                    this.DisableEditControl();

                    this.cmbDataTypes.Tag = this.dgvFields.CurrentCell;
                    string[] types = null;
                    if (this.tableInfo != null)
                    {
                        types = this.tableInfo.DatabaseInfo.ConnectInfo.DataTypes;
                    }
                    else if (this.databaseInfo != null)
                    {
                        types = this.databaseInfo.ConnectInfo.DataTypes;
                    }
                    Rectangle rec = this.dgvFields.GetCellDisplayRectangle(this.dgvFields.CurrentCell.ColumnIndex, this.dgvFields.CurrentCell.RowIndex, true);
                    this.cmbDataTypes.Left = rec.Left;
                    this.cmbDataTypes.Top = rec.Top;
                    this.cmbDataTypes.Width = rec.Width;
                    this.cmbDataTypes.DataSource = types;
                    this.cmbDataTypes.Text = this.dgvFields.CurrentCell.Value != null ? this.dgvFields.CurrentCell.Value.ToString() : "";
                    this.cmbDataTypes.Visible = true;
                    this.cmbDataTypes.Focus();
                }
                else if (idx == 0 || idx == 2 || idx == 3 || idx == 6)
                {
                    //disable other
                    this.DisableEditControl();

                    this.txtEdit.Tag = this.dgvFields.CurrentCell;
                    Rectangle rec = this.dgvFields.GetCellDisplayRectangle(this.dgvFields.CurrentCell.ColumnIndex, this.dgvFields.CurrentCell.RowIndex, true);
                    this.txtEdit.Left = rec.Left;
                    this.txtEdit.Top = rec.Top;
                    this.txtEdit.Width = rec.Width;
                    this.txtEdit.Text = this.dgvFields.CurrentCell.Value != null ? this.dgvFields.CurrentCell.Value.ToString() : "";
                    this.txtEdit.BringToFront();
                    this.txtEdit.Visible = true;
                    this.txtEdit.Focus();
                    this.txtEdit.SelectionStart = this.txtEdit.Text.Length;
                }
                else if (idx == 4)
                {
                    this.DisableEditControl();
                    this.dgvFields.CurrentCell.Value = !(Convert.ToBoolean(this.dgvFields.CurrentCell.Value));
                }
                else if (idx == 5)
                {
                    this.DisableEditControl();
                    this.dgvFields.CurrentCell.Value = !Convert.ToBoolean(this.dgvFields.CurrentCell.Value);
                }
                else
                {
                    this.DisableEditControl();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvFields_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                this.DisableEditControl();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvFields_Leave(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvFields_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
                e.Cancel = true;
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
        #endregion

        #region ToolBar Event
        private void tsbtnAddField_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                this.dgvFields.Rows.Add(new DataGridViewRow());
                this.SetStatusBar();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnInsertFields_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                if (this.dgvFields.CurrentRow != null)
                {
                    this.dgvFields.Rows.Insert(this.dgvFields.CurrentRow.Index + 1, new DataGridViewRow());
                    this.SetStatusBar();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnDeleteField_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                if (this.dgvFields.CurrentRow != null)
                {
                    this.dgvFields.Rows.RemoveAt(this.dgvFields.CurrentRow.Index);
                    this.SetStatusBar();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnPrimaryKey_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvFields.CurrentRow != null)
                {
                    this.dgvFields.Rows[this.dgvFields.CurrentRow.Index].Cells[5].Value = !(Convert.ToBoolean(this.dgvFields.Rows[this.dgvFields.CurrentRow.Index].Cells[5].Value));
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnMoveUp_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                if (this.dgvFields.CurrentRow != null && this.dgvFields.CurrentRow.Index > 0)
                {
                    int index = this.dgvFields.CurrentRow.Index;
                    DataGridViewRow pre_row = this.dgvFields.Rows[index - 1];
                    this.dgvFields.Rows.RemoveAt(index - 1);
                    this.dgvFields.Rows.Insert(index, pre_row);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                if (this.dgvFields.CurrentRow != null && this.dgvFields.CurrentRow.Index < this.dgvFields.Rows.Count - 1)
                {
                    DataGridViewCell crt_cell = this.dgvFields.CurrentCell;
                    int index = this.dgvFields.CurrentRow.Index;
                    DataGridViewRow crt_row = this.dgvFields.Rows[index];
                    this.dgvFields.Rows.RemoveAt(index);
                    this.dgvFields.Rows.Insert(index + 1, crt_row);
                    this.dgvFields.CurrentCell = crt_cell;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                this.OnNewTable(new NewTableEventArgs(this.databaseInfo));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvFields.Rows.Count == 0)
                {
                    return;
                }
                if (this.tableInfo == null)
                {
                    SaveForm saveForm = new SaveForm();
                    if (saveForm.ShowDialog() == DialogResult.OK)
                    {
                        //generate create table script
                        string primarykey = "";
                        string str = $"CREATE TABLE {saveForm.SaveName}\n(\n";
                        foreach (DataGridViewRow row in this.dgvFields.Rows)
                        {
                            str += $" {row.Cells[0].Value.ToString()}";
                            str += $" {row.Cells[1].Value.ToString()}";
                            if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "0")
                            {
                                str += $"({row.Cells[2].Value.ToString()}";
                                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "0")
                                {
                                    str += $",{row.Cells[3].Value.ToString()}";
                                }
                                str += ")";
                            }
                            if (row.Cells[4].Value != null && Convert.ToBoolean(row.Cells[4].Value))
                            {
                                str += $" NOT NULL";
                            }
                            if (row.Cells[5].Value != null && Convert.ToBoolean(row.Cells[5].Value))
                            {
                                if (primarykey == "")
                                {
                                    primarykey += "PRIMARY KEY(";
                                }
                                primarykey += $"{row.Cells[0].Value.ToString()},";
                            }
                            if (row.Cells[6].Value != null)
                            {
                                str += $" DEFAULT '{row.Cells[6].Value.ToString()}'";
                            }
                            else
                            {
                                str += $" DEFAULT NULL";
                            }
                            str += ",\n";
                        }
                        if (primarykey != "")
                        {
                            primarykey = primarykey.Remove(primarykey.Length - 1);
                            primarykey += "),\n";
                            str += primarykey;
                        }
                        str = str.Remove(str.Length - 2);
                        str += "\n)";
                        this.tableInfo = this.databaseInfo.CreateTable(saveForm.SaveName, str);
                        if (this.tableInfo != null)
                        {
                            this.tableInfo.IsDesign = true;
                            this.BindData();
                            TreeNode dbNode = this.databaseInfo.Node;
                            TreeNode tgNode = dbNode.Nodes[0];
                            NodeTypeInfo tgNodeTypeInfo = tgNode.Tag as NodeTypeInfo;
                            TreeNode tbNode = new TreeNode();
                            tbNode.Text = this.tableInfo.Name;
                            tbNode.ImageKey = Resources.image_key_table;
                            tbNode.SelectedImageKey = Resources.image_key_table;
                            tbNode.Tag = new NodeTypeInfo(NodeType.eTable, tbNode, this.tableInfo);
                            tgNode.Nodes.Add(tbNode);
                            tgNodeTypeInfo.TableList.AddItem(this.tableInfo, Resources.image_key_table);
                            this.page.Text = $"{this.tableInfo.Name}@{this.databaseInfo.Name}({this.databaseInfo.ConnectInfo.Name})-表";
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
                else
                {
                    //TODO alter table
                    string str = "";
                    this.databaseInfo.AlterTable(this.tableInfo.Name, str);
                    this.BindData();
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion
    }
}
