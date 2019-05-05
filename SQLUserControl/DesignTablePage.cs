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
using System.Text;

namespace SQLUserControl
{
    public partial class DesignTablePage : DevExpress.XtraEditors.XtraUserControl
    {
        public event ChangeStatusBarEventHandler ChangeStatusBar;
        public event NewTableEventHandler NewTable;
        private DataTable designTable = new DataTable();
        private TableInfo tableInfo;
        private DatabaseInfo databaseInfo;
        private XtraTabPage page;
        private ComboBox cmbDataTypes = new ComboBox();
        private TextBox txtEdit = new TextBox();

        public DesignTablePage()
        {
            InitializeComponent();

            this.SetBufferedControl();
            this.InitEditControl();
            this.InitEmptyDesignTable();
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

        private void InitEditControl()
        {
            //data types
            this.cmbDataTypes.Visible = false;
            this.cmbDataTypes.Width = 0;
            this.cmbDataTypes.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDataTypes.SelectionChangeCommitted += CmbDataTypes_SelectionChangeCommitted;
            this.cmbDataTypes.Leave += CmbDataTypes_Leave;
            this.dgvFields.Controls.Add(this.cmbDataTypes);

            //text edit
            this.txtEdit.Visible = false;
            this.txtEdit.Width = 0;
            this.txtEdit.Leave += TxtEdit_Leave;

            //date edit
            //TODO

            //Binary edit
            //TODO

            this.dgvFields.Controls.Add(this.txtEdit);
            this.dgvFields.MouseWheel += dgvFields_MouseWheel;
            this.dgvFields.AutoGenerateColumns = false;
        }

        private void SetBufferedControl()
        {
            Type type = this.dgvFields.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvFields, true, null);
            type = this.dgvIndexs.GetType();
            pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dgvIndexs, true, null);
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
            this.OnChangeStatusBar(new ChangeStatusBarEventArgs(this.designTable.Rows.Count));
        }

        private void InitEmptyDesignTable()
        {
            //FIELDNAME, FIELDTYPE, FIELDLENGTH, FIELDSCALE, FIELDISNULL, FIELDPRIMARYKE, FIELDDEFAULT, FIELDCOMMENTS
            this.designTable.Columns.Add(Resources.FieldName);
            this.designTable.Columns.Add(Resources.FieldType);
            this.designTable.Columns.Add(Resources.FieldLength);
            this.designTable.Columns.Add(Resources.FieldScale);
            this.designTable.Columns.Add(Resources.FieldIsNull);
            this.designTable.Columns.Add(Resources.FieldIsPrimayrKey);
            this.designTable.Columns.Add(Resources.FieldDefault);
            this.designTable.Columns.Add(Resources.FieldComments);
        }

        public void BindData()
        {
            this.tableInfo.Design(out this.designTable);
            this.dgvFields.DataSource = this.designTable;
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
                    bool isPrimaryKey = !Convert.ToBoolean(this.dgvFields.CurrentCell.Value);
                    this.dgvFields.CurrentCell.Value = isPrimaryKey;
                    if (isPrimaryKey)
                    {
                        this.DisableEditControl();
                        this.dgvFields.CurrentRow.Cells[idx - 1].Value = isPrimaryKey;
                    }
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

        private DataRow GetNewRow()
        {
            DataRow dr = this.designTable.NewRow();
            dr[Resources.FieldName] = "";
            dr[Resources.FieldType] = "";
            dr[Resources.FieldLength] = 0;
            dr[Resources.FieldScale] = 0;
            dr[Resources.FieldIsNull] = false;
            dr[Resources.FieldIsPrimayrKey] = false;
            dr[Resources.FieldDefault] = null;
            dr[Resources.FieldComments] = null;
            return dr;
        }
        private void tsbtnAddField_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableEditControl();
                this.designTable.Rows.Add(this.GetNewRow());
                this.dgvFields.DataSource = this.designTable;
                if (this.tableInfo != null)
                {
                    //TODO add field
                }
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
                    this.designTable.Rows.InsertAt(this.GetNewRow(), this.dgvFields.CurrentRow.Index);
                    this.dgvFields.DataSource = this.designTable;
                    if (this.tableInfo != null)
                    {
                        //TODO insert field
                    }
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
                    this.designTable.Rows.RemoveAt(this.dgvFields.CurrentRow.Index);
                    this.dgvFields.DataSource = this.designTable;
                    if (this.tableInfo != null)
                    {
                        //TODO delete field
                    }
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
                    this.designTable.Rows[this.dgvFields.CurrentRow.Index][Resources.FieldIsPrimayrKey] = !(Convert.ToBoolean(this.designTable.Rows[this.dgvFields.CurrentRow.Index]["FIELDPRIMARYKE"]));
                    this.dgvFields.DataSource = this.designTable;
                    if (this.tableInfo != null)
                    {
                        //TODO alter primary key
                    }
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
                    DataRow pre_row = this.designTable.NewRow();
                    pre_row.ItemArray = this.designTable.Rows[index - 1].ItemArray;
                    this.designTable.Rows.RemoveAt(index - 1);
                    this.designTable.Rows.InsertAt(pre_row, index);
                    this.designTable.AcceptChanges();
                    this.dgvFields.DataSource = this.designTable;
                    if (this.tableInfo != null)
                    {
                        //TODO alter field index
                    }
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
                    DataRow crt_row = this.designTable.NewRow();
                    crt_row.ItemArray = this.designTable.Rows[index].ItemArray;
                    this.designTable.Rows.RemoveAt(index);
                    this.designTable.Rows.InsertAt(crt_row, index + 1);
                    this.designTable.AcceptChanges();
                    this.dgvFields.DataSource = this.designTable;
                    this.dgvFields.CurrentCell = crt_cell;
                    if (this.tableInfo != null)
                    {
                        //TODO alter field index
                    }
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
                        StringBuilder tb = new StringBuilder();
                        tb.Append($"CREATE TABLE {saveForm.SaveName} \n(\n");
                        StringBuilder pk = new StringBuilder();
                        foreach(DataRow dr in this.designTable.Rows)
                        {
                            tb.Append($" {dr[Resources.FieldName]} {dr[Resources.FieldType]}");
                            if (dr[Resources.FieldLength].ToString() != "0")
                            {
                                tb.Append($" ({dr[Resources.FieldLength]}");
                                if (dr[Resources.FieldScale].ToString() != "0")
                                {
                                    tb.Append($",{dr[Resources.FieldScale]}");
                                }
                                tb.Append(")");
                            }
                            if (Convert.ToBoolean(dr[Resources.FieldIsNull]))
                            {
                                tb.Append(" NOT NULL");
                            }
                            if (Convert.ToBoolean(dr[Resources.FieldIsPrimayrKey]))
                            {
                                if (pk.Length == 0)
                                {
                                    pk.Append("PRIMARY KEY(");
                                }
                                pk.Append($"{dr[Resources.FieldName]},");
                            }
                            if (dr[Resources.FieldDefault].ToString() != "")
                            {
                                tb.Append($" DEFAULT {dr[Resources.FieldDefault]}");
                            }
                            else
                            {
                                if (!Convert.ToBoolean(dr[Resources.FieldIsPrimayrKey]) && !Convert.ToBoolean(dr[Resources.FieldIsNull]))
                                {
                                    tb.Append(" DEFAULT NULL");
                                }
                            }
                            tb.Append(",\n");
                        }
                        if (pk.Length != 0)
                        {
                            pk.Remove(pk.Length - 1, 1);
                            pk.Append(")\n");
                            tb.Append(pk);
                            tb.Append(")\n");
                        }
                        else
                        {
                            tb.Remove(tb.Length - 2, 2);
                            tb.Append(")\n");
                        }
                        this.tableInfo = this.databaseInfo.CreateTable(saveForm.SaveName, tb.ToString());
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

        private void tsbtnSaveAs_Click(object sender, EventArgs e)
        {

        }

        private void DesignTablePage_Load(object sender, EventArgs e)
        {
            this.spMain.Panel2Collapsed = true;
        }

        public void RefreshDesignTable()
        {
            this.BindData();
        }
    }
}
