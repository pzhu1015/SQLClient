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
using ICSharpCode.TextEditor.Document;
using DevExpress.XtraTab;
using System.IO;
using System.Reflection;
using SQLParser;
using System.Threading;

namespace SQLUserControl
{
    public partial class NewSelectPage : DevExpress.XtraEditors.XtraUserControl
    {
        public event ChangeStatusBarEventHandler ChangeStatusBar;
        public NewSelectEventHandler NewSelect;
        private SelectInfo selectInfo;
        private DatabaseInfo databaseInfo;
        private XtraTabPage page;

        public NewSelectPage()
        {
            InitializeComponent();
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

        public SelectInfo SelectInfo
        {
            get
            {
                return selectInfo;
            }

            set
            {
                selectInfo = value;
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

        protected virtual void OnNewSelect(NewSelectEventArgs e)
        {
            if (this.NewSelect != null)
            {
                this.NewSelect(this, e);
            }
        }

        protected virtual void OnChangeStatusBar(ChangeStatusBarEventArgs e)
        {
            if (this.ChangeStatusBar != null)
            {
                this.ChangeStatusBar(this, e);
            }
        }

        public void SetStatusBar()
        {
            TabPage page = this.tcRslt.SelectedTab;
            QueryRslt queryRslt = page.Tag as QueryRslt;
            this.SetStatusBar(queryRslt);
        }

        private void SetStatusBar(QueryRslt queryRslt)
        {
            if (queryRslt == null)
            {
                this.OnChangeStatusBar(new ChangeStatusBarEventArgs(true));
            }
            else
            {
                this.OnChangeStatusBar(new ChangeStatusBarEventArgs(queryRslt.RowIndex, queryRslt.Count, queryRslt.Statement, queryRslt.Cost));
            }
        }

        public void BindData()
        {
            this.txtMain.Text = this.selectInfo.Open();
        }

        private void RunSql(object args)
        {
            Thread.Sleep(2000);
            string str = args as string;
            try
            {
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        this.splitMain.Panel2Collapsed = false;
                        while (this.tcRslt.TabPages.Count > 1)
                        {
                            this.tcRslt.TabPages.RemoveAt(this.tcRslt.TabPages.Count - 1);
                        }
                        this.txtMsg.Clear();
                        SQLParser.SQLParser parser = new SQLParser.SQLParser();
                        parser.Parse(str);
                        if (parser.IsComplete())
                        {
                            List<StatementObj> statements = parser.GetStatements();
                            foreach (var statement in statements)
                            {
                                string sql = statement.GetStatement();
                                sql = sql.Remove(sql.Length - 1);
                                string first_word = statement.GetFirstWord();
                                SqlStatementType type = this.databaseInfo.GetStatementType(sql, first_word.ToUpper());
                                bool rslt = false;
                                int count = 0;
                                string error = "";
                                Int64 cost = 0;
                                if (type == SqlStatementType.eMsg)
                                {
                                    rslt = this.databaseInfo.ExecueNonQuery(sql, out count, out error, out cost);
                                }
                                else
                                {
                                    DataTable table;
                                    rslt = this.databaseInfo.ExecuteQuery(sql, out table, out count, out error, out cost);
                                    if (rslt)
                                    {
                                        TabPage page = new TabPage();
                                        page.Text = $"结果-{this.tcRslt.TabPages.Count}";
                                        QueryRslt queryRslt = new QueryRslt();
                                        queryRslt.Dock = DockStyle.Fill;
                                        queryRslt.CurrentCellChange += QueryRslt_CurrentCellChange;
                                        queryRslt.Cost = cost;
                                        queryRslt.Statement = statement.GetStatement();
                                        queryRslt.BindData(table);
                                        page.Tag = queryRslt;
                                        page.Controls.Add(queryRslt);
                                        this.tcRslt.TabPages.Add(page);
                                        this.tcRslt.SelectedTab = page;
                                    }
                                }
                                if (rslt)
                                {
                                    string success_msg = $"[SQL] {sql}\r\n 受影响的行:{count}, 时间: {cost / 1000.0}s\r\n\r\n";
                                    this.txtMsg.AppendText(success_msg);
                                }
                                else
                                {
                                    this.txtMsg.AppendText($"[ERROR] {error}\r\n\r\n");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                    }
                    finally
                    {
                        this.EnableRun();
                    }
                }));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void DisableRun()
        {
            this.tsbtnRun.Enabled = false;
            this.tsbtnRunSelect.Enabled = false;
            this.tsmiRunSelect.Enabled = false;
            this.tsmiRunOneSelect.Enabled = false;
            this.tsbtnStop.Enabled = true;
        }

        private void EnableRun()
        {
            this.tsbtnRun.Enabled = true;
            this.tsbtnRunSelect.Enabled = true;
            this.tsmiRunSelect.Enabled = true;
            this.tsmiRunOneSelect.Enabled = true;
            this.tsbtnStop.Enabled = false;
        }

        private void NewSelectPage_Load(object sender, EventArgs e)
        {
            Type type = this.txtMain.GetType();
            PropertyInfo pi = type.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.txtMain, true, null);
            this.txtMain.ShowEOLMarkers = false;
            this.txtMain.ShowHRuler = false;
            this.txtMain.ShowInvalidLines = false;
            this.txtMain.ShowMatchingBracket = true;
            this.txtMain.ShowSpaces = false;
            this.txtMain.ShowTabs = false;
            this.txtMain.ShowVRuler = false;
            this.txtMain.AllowCaretBeyondEOL = false;
            this.txtMain.IsIconBarVisible = false;
            this.txtMain.HorizontalScroll.Visible = false;
            this.txtMain.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("TSQL");
            this.txtMain.Document.FoldingManager.FoldingStrategy = new SQLFolding();
            this.splitMain.Panel2Collapsed = true;
        }

        #region ToolBar Event
        private void tsbtnNew_Click(object sender, EventArgs e)
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

        private void tsbtnSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "SQL file(*.sql;*.txt)|*.sql;*.txt|全部文件(*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.txtMain.Text = File.ReadAllText(openFileDialog.FileName);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.selectInfo == null)
                {
                    SaveForm saveForm = new SaveForm();
                    DialogResult rslt = saveForm.ShowDialog();
                    if (rslt == DialogResult.OK)
                    {
                        this.selectInfo = this.databaseInfo.CreateSelect(saveForm.SaveName, this.txtMain.Text);
                        if (this.selectInfo != null)
                        {
                            this.selectInfo.IsOpen = true;
                            TreeNode dbNode = this.databaseInfo.Node;
                            TreeNode sgNode = dbNode.Nodes[2];
                            NodeTypeInfo sgNodeTypeInfo = sgNode.Tag as NodeTypeInfo;
                            TreeNode sNode = new TreeNode();
                            sNode.Text = this.selectInfo.Name;
                            sNode.ImageKey = Resource.image_key_select;
                            sNode.SelectedImageKey = Resource.image_key_select;
                            sNode.Tag = new NodeTypeInfo(NodeType.eSelect, sNode, this.selectInfo);
                            sgNode.Nodes.Add(sNode);
                            sgNodeTypeInfo.SelectList.AddItem(this.selectInfo, Resource.image_key_select);
                            this.page.Text = $"{this.selectInfo.Name}@{this.databaseInfo.Name}({this.databaseInfo.ConnectInfo.Name})-查询";
                            TabPageTypeInfo pageTypeInfo = this.page.Tag as TabPageTypeInfo;
                            pageTypeInfo.PageType = TabPageType.eOpenSelect;
                            pageTypeInfo.Info = this.selectInfo;
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }
                    }
                }
                else
                {
                    this.databaseInfo.AlterSelect(this.selectInfo.Name, this.txtMain.Text);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                this.tsbtnSave.Enabled = true;
            }
        }

        private void tsbtnRun_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                this.DisableRun();
                string str = this.txtMain.Text;
                //AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(RunSql);
                //ah.BeginInvoke(str, null, null);
                Thread thread = new Thread(new ParameterizedThreadStart(RunSql));
                thread.Start(str);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnRunSelect_Click(object sender, EventArgs e)
        {
            try
            {
                this.DisableRun();
                string str = this.txtMain.ActiveTextAreaControl.SelectionManager.SelectedText;
                //AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(RunSql);
                //ah.BeginInvoke(str, null, null);
                Thread thread = new Thread(new ParameterizedThreadStart(RunSql));
                thread.Start(str);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnFormat_Click(object sender, EventArgs e)
        {
            this.txtMain.Document.FoldingManager.UpdateFoldings(String.Empty, null);
            this.txtMain.ActiveTextAreaControl.TextArea.Refresh();
        }
        #endregion

        #region Command Rslt Event
        private void QueryRslt_CurrentCellChange(object sender, EventArgs e)
        {
            QueryRslt queryRslt = sender as QueryRslt;
            this.SetStatusBar(queryRslt);
        }

        private void tcRslt_Selected(object sender, TabControlEventArgs e)
        {
            TabPage page = e.TabPage;
            QueryRslt queryRslt = page.Tag as QueryRslt;
            this.SetStatusBar(queryRslt);
        }
        #endregion

        #region Right Key Menu Event
        private void tsmiUndo_Click(object sender, EventArgs e)
        {
            this.txtMain.Undo();
        }

        private void tsmiRedo_Click(object sender, EventArgs e)
        {
            this.txtMain.Redo();
        }

        private void tsmiCut_Click(object sender, EventArgs e)
        {
            string str = this.txtMain.ActiveTextAreaControl.SelectionManager.SelectedText;
            if (str == "") return;
            Clipboard.SetText(str);
            this.txtMain.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
           
        }

        private void tsmiCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.txtMain.ActiveTextAreaControl.SelectionManager.SelectedText);
        }

        private void tsmiPaste_Click(object sender, EventArgs e)
        {
            string str = Clipboard.GetText();
            this.txtMain.ActiveTextAreaControl.TextArea.InsertString(str);
            if (this.txtMain.ActiveTextAreaControl.SelectionManager.HasSomethingSelected)
            {
                this.txtMain.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            }
        }
        #endregion
    }
}
