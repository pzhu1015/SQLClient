using System;
using System.Windows.Forms;
using Helper;

using DevExpress.XtraTab;
using SQLDAL;
using SQLUserControl;
using System.Collections.Generic;
using System.Drawing;
using System.Data;

namespace SQLClient
{

    public partial class SqlClientForm : Form
    {
        private LoadingForm waitForm = new LoadingForm();
        private TreeNode currentDbNode = null;
        private NodeType currentViewListType = NodeType.eTable;
        public SqlClientForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true; 
        }

        /// <summary>
        /// 设置状态栏左右宽度
        /// </summary>
        private void ReSizeStatusBar()
        {
            this.tsObjectBlank.Width = this.tvMain.Width;
            this.tsOpenTableBlank.Width = this.tvMain.Width;
            this.tsNewSelectBlank.Width = this.tvMain.Width;
            this.tsDesignTableBlank.Width = this.tvMain.Width;
            this.tsOpenTableStmt.Width = this.tsOpenTable.Width - this.tsOpenTableBlank.Width - this.tsOpenTablePageInfo.Width - this.tsOpenTableWarn.Width - 100;
            this.tsNewSelectStmt.Width = this.tsNewSelect.Width - this.tsNewSelectBlank.Width - this.tsNewSelectPageInfo.Width - this.tsNewSelectTime.Width - 100;
        }

        /// <summary>
        /// 根据当前显示的TabPage类型，显示相对应的状态栏
        /// </summary>
        /// <param name="pageTypeInfo">TabPage类型数据信息</param>
        private void ShowStatusBar(TabPageTypeInfo pageTypeInfo)
        {
            switch (pageTypeInfo.PageType)
            {
                case TabPageType.eObject:
                    {
                        this.tsObject.Visible = true;
                        this.tsOpenTable.Visible = false;
                        this.tsDesignTable.Visible = false;
                        this.tsNewSelect.Visible = false;
                        TreeNode selectNode = this.tvMain.SelectedNode;
                        if (selectNode == null)
                        {
                            return;
                        }
                        this.SelectNode(selectNode);
                        break;
                    }
                case TabPageType.eOpenTable:
                    {
                        this.tsObject.Visible = false;
                        this.tsOpenTable.Visible = true;
                        this.tsDesignTable.Visible = false;
                        this.tsNewSelect.Visible = false;
                        pageTypeInfo.OpenTablePage.SetStatusBar();
                        break;
                    }
                case TabPageType.eOpenView:
                    {
                        this.tsObject.Visible = false;
                        this.tsOpenTable.Visible = true;
                        this.tsDesignTable.Visible = false;
                        this.tsNewSelect.Visible = false;

                        pageTypeInfo.OpenViewPage.SetStatusBar();
                        break;
                    }
                case TabPageType.eOpenSelect:
                    {
                        this.tsObject.Visible = false;
                        this.tsOpenTable.Visible = false;
                        this.tsDesignTable.Visible = false;
                        this.tsNewSelect.Visible = true;
                        pageTypeInfo.NewSelectPage.SetStatusBar();
                        break;
                    }
                case TabPageType.eNewTable:
                    {
                        this.tsObject.Visible = false;
                        this.tsOpenTable.Visible = false;
                        this.tsDesignTable.Visible = true;
                        this.tsNewSelect.Visible = false;
                        pageTypeInfo.DesignTablePage.SetStatusBar();
                        break;
                    }
                case TabPageType.eNewView:
                    {
                        break;
                    }
                case TabPageType.eNewSelect:
                    {
                        this.tsObject.Visible = false;
                        this.tsOpenTable.Visible = false;
                        this.tsDesignTable.Visible = false;
                        this.tsNewSelect.Visible = true;
                        pageTypeInfo.NewSelectPage.SetStatusBar();
                        break;
                    }
                case TabPageType.eDesignTable:
                    {
                        this.tsObject.Visible = false;
                        this.tsOpenTable.Visible = false;
                        this.tsDesignTable.Visible = true;
                        this.tsNewSelect.Visible = false;
                        pageTypeInfo.DesignTablePage.SetStatusBar();
                        break;
                    }
                case TabPageType.eDesignView:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 显示连接配置对话框
        /// </summary>
        private void OpenNewConnectionForm()
        {
            NewConnectionFrom newConnectionForm = new NewConnectionFrom();
            if (newConnectionForm.ShowDialog() == DialogResult.OK)
            {
                this.AddConnection(newConnectionForm.ConnectionInfo);
            }
        }

        /// <summary>
        /// 添加新的连接信息
        /// </summary>
        /// <param name="info">连接对象的具体信息</param>
        private void AddConnection(ConnectInfo info)
        {
            info.CloseConnect += Info_CloseConnect;
            info.OpenConnect += Info_OpenConnect;
            TreeNode node = new TreeNode();
            node.Text = info.Name;
            node.ImageKey = info.ClassName + "_close";
            node.SelectedImageKey = info.ClassName + "_close";
            node.Tag = new NodeTypeInfo(NodeType.eConnect, node, info);
            ConnectInfo.AddConnection(info);
            info.Node = node;
            this.tvMain.Nodes.Add(node);
            this.tsObjectBlank.Text = $"{this.tvMain.Nodes.Count}服务器";
            this.tsObjectOwner.Text = $"{info.Name} 用户: {info.User}";
            this.tsObjectOwner.Image = info.CloseImage();
        }

        /// <summary>
        /// 异步打开连接
        /// </summary>
        /// <param name="args">选中连接的具体信息</param>
        private void OpenConnect(object args)
        {
            try
            {
                NodeTypeInfo nodeTypeInfo = args as NodeTypeInfo;
                ConnectInfo info = nodeTypeInfo.ConnectionInfo;
                TreeNode node = nodeTypeInfo.Node;
                bool rslt = info.Open();
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        if (!rslt)
                        {
                            MessageBox.Show(info.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        LogHelper.Error(ex);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 异步关闭连接
        /// </summary>
        /// <param name="args">选中连接的具体信息</param>
        private void CloseConnect(object args)
        {
            try
            {
                NodeTypeInfo nodeTypeInfo = args as NodeTypeInfo;
                ConnectInfo info = nodeTypeInfo.ConnectionInfo;
                bool rslt = info.Close();
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        if (!rslt)
                        {
                            MessageBox.Show(info.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        LogHelper.Error(ex);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 异步关闭数据库
        /// </summary>
        /// <param name="args">选中数据库的具体信息</param>
        private void CloseDatabase(object args)
        {
            try
            {
                NodeTypeInfo nodeTypeInfo = args as NodeTypeInfo;
                DatabaseInfo info = nodeTypeInfo.DatabaseInfo;
                TreeNode node = nodeTypeInfo.Node;
                bool rslt = info.Close();
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        if (!rslt)
                        {
                            MessageBox.Show(info.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        LogHelper.Error(ex);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 异步打开数据库
        /// </summary>
        /// <param name="args">选中数据库的具体信息</param>
        private void OpenDatabase(object args)
        {
            try
            {
                NodeTypeInfo nodeTypeInfo = args as NodeTypeInfo;
                DatabaseInfo info = nodeTypeInfo.DatabaseInfo;
                TreeNode node = nodeTypeInfo.Node;
                bool rslt = info.Open();
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        if (!rslt)
                        {
                            MessageBox.Show(info.Message);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        LogHelper.Error(ex);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 异步打开表的设计
        /// </summary>
        /// <param name="args">表的具体信息</param>
        private void DesignTable(object args)
        {
            try
            {
                TableInfo info = args as TableInfo;
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        XtraTabPage tabPage = new XtraTabPage();
                        tabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
                        tabPage.Text = $"{info.Name}@{info.DatabaseInfo.Name}({info.DatabaseInfo.ConnectInfo.Name}) - 表";
                        tabPage.Tooltip = tabPage.Text;
                        tabPage.MaxTabPageWidth = 200;
                        tabPage.Image = Resource.design_table_16;

                        //new DesignTableTabPage
                        DesignTablePage designTablePage = new DesignTablePage();
                        designTablePage.NewTable += TableList_NewTable;
                        designTablePage.Dock = DockStyle.Fill;
                        designTablePage.TableInfo = info;
                        designTablePage.DatabaseInfo = info.DatabaseInfo;
                        designTablePage.ChangeStatusBar += DesignTablePage_ChangeStatusBar;
                        designTablePage.BindData();

                        TabPageTypeInfo tpInfo = new TabPageTypeInfo(TabPageType.eDesignTable, info);
                        tpInfo.DesignTablePage = designTablePage;
                        tabPage.Tag = tpInfo;

                        tabPage.Controls.Add(designTablePage);
                        info.DesignTablePage = tabPage;
                        this.tcMain.TabPages.Add(tabPage);
                        this.tcMain.SelectedTabPage = tabPage;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                        MessageBox.Show(info.Message);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 异步打开表
        /// </summary>
        /// <param name="args">表的具体信息</param>
        private void OpenTable(object args)
        {
            try
            {
                TableInfo info = args as TableInfo;
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        //add TabPage
                        XtraTabPage tabPage = new XtraTabPage();
                        tabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
                        tabPage.Text = $"{info.Name}@{info.DatabaseInfo.Name}({info.DatabaseInfo.ConnectInfo.Name}) - 表";
                        tabPage.Tooltip = tabPage.Text;
                        tabPage.MaxTabPageWidth = 200;
                        //new OpenTableTabPage
                        OpenTablePage openTalePage = new OpenTablePage();
                        openTalePage.Dock = DockStyle.Fill;
                        openTalePage.TableInfo = info;
                        openTalePage.DatabaseInfo = info.DatabaseInfo;
                        openTalePage.ChangeStatusBar += OpenTalePage_ChangeStatusBar;
                        openTalePage.BindData(0);

                        TabPageTypeInfo tpInfo = new TabPageTypeInfo(TabPageType.eOpenTable, info);
                        tpInfo.OpenTablePage = openTalePage;
                        tabPage.Tag = tpInfo;
                        tabPage.Image = Resource.table_16;
                        tabPage.Controls.Add(openTalePage);
                        info.OpenTablePage = tabPage;
                        this.tcMain.TabPages.Add(tabPage);
                        this.tcMain.SelectedTabPage = tabPage;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                        MessageBox.Show(info.Message);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 打开查询脚本
        /// </summary>
        /// <param name="args">查询的具体信息</param>
        private void OpenSelect(object args)
        {
            try
            {
                SelectInfo info = args as SelectInfo;
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        //add TabPage
                        XtraTabPage tabPage = new XtraTabPage();
                        tabPage.PageVisible = false;
                        tabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
                        tabPage.Text = $"{info.Name}@{info.DatabaseInfo.Name}({info.DatabaseInfo.ConnectInfo.Name}) - 查询";
                        tabPage.Tooltip = tabPage.Text;
                        tabPage.MaxTabPageWidth = 200;
                        //new OpenTableTabPage
                        NewSelectPage newSelectPage = new NewSelectPage();
                        newSelectPage.NewSelect += SelectList_NewSelect;
                        newSelectPage.Dock = DockStyle.Fill;
                        newSelectPage.SelectInfo = info;
                        newSelectPage.DatabaseInfo = info.DatabaseInfo;
                        newSelectPage.ChangeStatusBar += NewSelectPage_ChangeStatusBar;
                        newSelectPage.Page = tabPage;
                        newSelectPage.BindData();

                        TabPageTypeInfo tpInfo = new TabPageTypeInfo(TabPageType.eOpenSelect, info);
                        tpInfo.NewSelectPage = newSelectPage;

                        tabPage.Tag = tpInfo;
                        tabPage.Image = Resource.select_16;
                        tabPage.Controls.Add(newSelectPage);
                        info.OpenSelectPage = tabPage;
                        this.tcMain.TabPages.Add(tabPage);
                        tabPage.PageVisible = true;
                        this.tcMain.SelectedTabPage = tabPage;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                        MessageBox.Show(info.Message);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 打开视图
        /// </summary>
        /// <param name="args">视图的具体信息</param>
        private void OpenView(object args)
        {
            try
            {
                ViewInfo info = args as ViewInfo;
                this.Invoke(new MethodInvoker(delegate ()
                {
                    try
                    {
                        //add TabPage
                        XtraTabPage tabPage = new XtraTabPage();
                        tabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
                        tabPage.Text = $"{info.Name}@{info.DatabaseInfo.Name}({info.DatabaseInfo.ConnectInfo.Name}) - 视图";
                        tabPage.Tooltip = tabPage.Text;
                        tabPage.MaxTabPageWidth = 200;
                        //new OpenTableTabPage
                        OpenViewPage openViewPage = new OpenViewPage();
                        openViewPage.Dock = DockStyle.Fill;
                        openViewPage.ViewInfo = info;
                        openViewPage.DatabaseInfo = info.DatabaseInfo;
                        openViewPage.ChangeStatusBar += OpenViewPage_ChangeStatusBar;
                        openViewPage.BindData(0);

                        TabPageTypeInfo tpInfo = new TabPageTypeInfo(TabPageType.eOpenView, info);
                        tpInfo.OpenViewPage = openViewPage;
                        tabPage.Tag = tpInfo;
                        tabPage.Image = Resource.view_16;
                        tabPage.Controls.Add(openViewPage);
                        info.OpenViewPage = tabPage;
                        this.tcMain.TabPages.Add(tabPage);
                        this.tcMain.SelectedTabPage = tabPage;
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Error(ex);
                        MessageBox.Show(info.Message);
                    }
                    finally
                    {
                        this.waitForm.Close();
                    }
                }));
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        /// <summary>
        /// 新建表
        /// </summary>
        /// <param name="databaseInfo">当前数据库的信息</param>
        private void NewTable(DatabaseInfo databaseInfo)
        {
            XtraTabPage tabPage = new XtraTabPage();
            tabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            tabPage.Text = $"{Resource.no_title}@{databaseInfo.Name}({databaseInfo.ConnectInfo.Name}) - 表";
            tabPage.Tooltip = tabPage.Text;
            tabPage.MaxTabPageWidth = 200;
            tabPage.Image = Resource.design_table_16;

            //new DesignTableTabPage
            DesignTablePage designTablePage = new DesignTablePage();
            designTablePage.NewTable += TableList_NewTable;
            designTablePage.Dock = DockStyle.Fill;
            designTablePage.DatabaseInfo = databaseInfo;
            designTablePage.ChangeStatusBar += DesignTablePage_ChangeStatusBar;
            designTablePage.Page = tabPage;

            TabPageTypeInfo tpInfo = new TabPageTypeInfo(TabPageType.eNewTable, databaseInfo);
            tpInfo.DesignTablePage = designTablePage;
            tabPage.Tag = tpInfo;
            databaseInfo.NewTablePages.Add(tabPage);
            tabPage.Controls.Add(designTablePage);

            this.tcMain.TabPages.Add(tabPage);
            this.tcMain.SelectedTabPage = tabPage;
        }

        /// <summary>
        /// 新建查询脚本
        /// </summary>
        /// <param name="databaseInfo">当前数据库的信息</param>
        private void NewSelect(DatabaseInfo databaseInfo)
        {
            XtraTabPage tabPage = new XtraTabPage();
            tabPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;
            tabPage.Text = $"{Resource.no_title}@{databaseInfo.Name}({databaseInfo.ConnectInfo.Name}) - 查询";
            tabPage.Tooltip = tabPage.Text;
            tabPage.MaxTabPageWidth = 200;
            tabPage.Image = Resource.select_16;

            //new NewSelectTabPage
            NewSelectPage newSelectPage = new NewSelectPage();
            newSelectPage.NewSelect += SelectList_NewSelect;
            newSelectPage.Dock = DockStyle.Fill;
            newSelectPage.DatabaseInfo = databaseInfo;
            newSelectPage.ChangeStatusBar += NewSelectPage_ChangeStatusBar;
            newSelectPage.Page = tabPage;

            TabPageTypeInfo tpInfo = new TabPageTypeInfo(TabPageType.eNewSelect, databaseInfo);
            tpInfo.NewSelectPage = newSelectPage;
            tabPage.Tag = tpInfo;

            databaseInfo.NewSelectPages.Add(tabPage);
            tabPage.Controls.Add(newSelectPage);

            this.tcMain.TabPages.Add(tabPage);
            this.tcMain.SelectedTabPage = tabPage;
        }

        /// <summary>
        /// 设置选中工具栏按钮时的状态
        /// </summary>
        private void SelectToolBar()
        {
            switch (this.currentViewListType)
            {
                case NodeType.eTable:
                    {
                        this.tsbtnTable.Checked = true;
                        this.tsbtnView.Checked = false;
                        this.tsbtnSelect.Checked = false;
                        break;
                    }
                case NodeType.eView:
                    {
                        this.tsbtnTable.Checked = false;
                        this.tsbtnView.Checked = true;
                        this.tsbtnSelect.Checked = false;
                        break;
                    }
                case NodeType.eSelect:
                    {
                        this.tsbtnTable.Checked = false;
                        this.tsbtnView.Checked = false;
                        this.tsbtnSelect.Checked = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        /// <summary>
        /// 设置当前选中节点时的状态
        /// </summary>
        /// <param name="node">当前选中节点</param>
        private void SelectNode(TreeNode node)
        {
            NodeTypeInfo nodeTypeInfo = node.Tag as NodeTypeInfo;
            switch (nodeTypeInfo.NodeType)
            {
                case NodeType.eConnect:
                    {
                        this.currentDbNode = null;
                        ConnectInfo connectInfo = nodeTypeInfo.ConnectionInfo;
                        if (!connectInfo.IsOpen)
                        {
                            this.tsObjectBlank.Text = $"{this.tvMain.Nodes.Count}服务器";
                            this.tsObjectOwner.Text = $"{connectInfo.Name} 用户: {connectInfo.User}";
                            this.tsObjectOwner.Image = connectInfo.CloseImage();
                            return;
                        }
                        foreach (Control c in this.tpObject.Controls)
                        {
                            if (c is TableListView || c is SelectListView || c is ViewListView)
                            {
                                c.Visible = false;
                            }
                            if (c is ToolStrip)
                            {
                                c.Enabled = false;
                            }
                        }
                        this.tsObjectBlank.Text = $"{connectInfo.Databases.Count}数据据库";
                        this.tsObjectOwner.Text = $"{connectInfo.Name} 用户: {connectInfo.User}";
                        this.tsObjectOwner.Image = connectInfo.OpenImage();
                        break;
                    }
                case NodeType.eDatabase:
                    {
                        this.SelectDbNode(node, this.currentViewListType);
                        DatabaseInfo databaseInfo = nodeTypeInfo.DatabaseInfo;
                        if (!databaseInfo.IsOpen)
                        {
                            ConnectInfo connectInfo = databaseInfo.ConnectInfo;
                            this.tsObjectBlank.Text = $"{connectInfo.Databases.Count}数据库";
                            this.tsObjectOwner.Text = $"{connectInfo.Name} 用户: {connectInfo.User} 数据库:{databaseInfo.Name}";
                            this.tsObjectOwner.Image = connectInfo.OpenImage();
                            return;
                        }
                        nodeTypeInfo.TableList.UnSelectItem();
                        nodeTypeInfo.ViewList.UnSelectItem();
                        nodeTypeInfo.SelectList.UnSelectItem();
                        //handle select tool bar
                        this.SelectToolBar();

                        //handle left status bar
                        this.tsObjectBlank.Text = $"{databaseInfo.Tables.Count}表({databaseInfo.Tables.Count}位于当前的组)";
                        this.tsObjectOwner.Text = $"{databaseInfo.ConnectInfo.Name} 用户:{databaseInfo.ConnectInfo.User} 数据库:{databaseInfo.Name}";
                        this.tsObjectOwner.Image = databaseInfo.ConnectInfo.OpenImage();
                        break;
                    }
                case NodeType.eTableGroup:
                    {
                        //show table view list
                        this.SelectDbNode(node.Parent, NodeType.eTable);

                        //handle selectt tool bar
                        this.SelectToolBar();

                        //handle left status bar
                        NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                        dbNodeTypeInfo.TableList.UnSelectItem();
                        DatabaseInfo databaseInfo = dbNodeTypeInfo.DatabaseInfo;
                        this.tsObjectBlank.Text = $"{databaseInfo.Tables.Count}表({databaseInfo.Tables.Count}位于当前的组)";
                        this.tsObjectOwner.Text = $"{databaseInfo.ConnectInfo.Name} 用户:{databaseInfo.ConnectInfo.User} 数据库:{databaseInfo.Name}";
                        this.tsObjectOwner.Image = databaseInfo.ConnectInfo.OpenImage();
                        break;
                    }
                case NodeType.eTable:
                    {
                        //show table view list
                        this.SelectDbNode(node.Parent.Parent, NodeType.eTable);

                        //handle select tool bar
                        this.SelectToolBar();
                        //handle left status bar
                        NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                        dbNodeTypeInfo.TableList.SelectItem();
                        DatabaseInfo databaseInfo = dbNodeTypeInfo.DatabaseInfo;
                        this.tsObjectBlank.Text = $"{databaseInfo.Tables.Count}表({databaseInfo.Tables.Count}位于当前的组)";
                        this.tsObjectOwner.Text = $"{databaseInfo.ConnectInfo.Name} 用户:{databaseInfo.ConnectInfo.User} 数据库:{databaseInfo.Name}";
                        this.tsObjectOwner.Image = databaseInfo.ConnectInfo.OpenImage();
                        break;
                    }
                case NodeType.eViewGroup:
                    {
                        //show view view list
                        this.SelectDbNode(node.Parent, NodeType.eView);

                        //handle select tool bar
                        this.SelectToolBar();
                        //handle left status bar
                        NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                        dbNodeTypeInfo.ViewList.UnSelectItem();
                        DatabaseInfo databaseInfo = dbNodeTypeInfo.DatabaseInfo;
                        this.tsObjectBlank.Text = $"{databaseInfo.Views.Count}视图({databaseInfo.Views.Count}位于当前的组)";
                        this.tsObjectOwner.Text = $"{databaseInfo.ConnectInfo.Name} 用户:{databaseInfo.ConnectInfo.User} 数据库:{databaseInfo.Name}";
                        this.tsObjectOwner.Image = databaseInfo.ConnectInfo.OpenImage();
                        break;
                    }
                case NodeType.eView:
                    {
                        //show view view list
                        this.SelectDbNode(node.Parent.Parent, NodeType.eView);

                        //handle select tool bar
                        this.SelectToolBar();
                        //handle left status bar
                        NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                        dbNodeTypeInfo.ViewList.SelectItem();
                        DatabaseInfo databaseInfo = dbNodeTypeInfo.DatabaseInfo;
                        this.tsObjectBlank.Text = $"{databaseInfo.Views.Count}视图({databaseInfo.Views.Count}位于当前的组)";
                        this.tsObjectOwner.Text = $"{databaseInfo.ConnectInfo.Name} 用户:{databaseInfo.ConnectInfo.User} 数据库:{databaseInfo.Name}";
                        this.tsObjectOwner.Image = databaseInfo.ConnectInfo.OpenImage();
                        break;
                    }
                case NodeType.eSelectGroup:
                    {
                        //show select view list
                        this.SelectDbNode(node.Parent, NodeType.eSelect);

                        //handle select tool bar
                        this.SelectToolBar();
                        //handle left status bar
                        NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                        dbNodeTypeInfo.SelectList.UnSelectItem();
                        DatabaseInfo databaseInfo = dbNodeTypeInfo.DatabaseInfo;
                        this.tsObjectBlank.Text = $"{databaseInfo.Selects.Count}查询({databaseInfo.Selects.Count}位于当前的组)";
                        this.tsObjectOwner.Text = $"{databaseInfo.ConnectInfo.Name} 用户:{databaseInfo.ConnectInfo.User} 数据库:{databaseInfo.Name}";
                        this.tsObjectOwner.Image = databaseInfo.ConnectInfo.OpenImage();

                        break;
                    }
                case NodeType.eSelect:
                    {
                        //show select view list
                        this.SelectDbNode(node.Parent.Parent, NodeType.eSelect);

                        //handle select tool bar
                        this.SelectToolBar();

                        //handle left status bar
                        NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                        dbNodeTypeInfo.SelectList.SelectItem();
                        DatabaseInfo databaseInfo = dbNodeTypeInfo.DatabaseInfo;
                        this.tsObjectBlank.Text = $"{databaseInfo.Selects.Count}查询({databaseInfo.Selects.Count}位于当前的组)";
                        this.tsObjectOwner.Text = $"{databaseInfo.ConnectInfo.Name} 用户:{databaseInfo.ConnectInfo.User} 数据库:{databaseInfo.Name}";
                        this.tsObjectOwner.Image = databaseInfo.ConnectInfo.OpenImage();

                        break;
                    }
            }
        }

        /// <summary>
        /// 设置当前选中数据库节点的状态
        /// </summary>
        /// <param name="node">当前数据库节点</param>
        /// <param name="type">当前节点类型</param>
        /// <param name="isopen">是否第一次打开数据库</param>
        private void SelectDbNode(TreeNode node, NodeType type, bool isopen = false)
        {
            if (this.currentDbNode == null ||
                (this.currentDbNode != null && this.currentDbNode.Text != node.Text) ||
                this.currentViewListType != type || isopen)
            {
                this.currentDbNode = node;
                this.currentViewListType = type;
                switch (this.currentViewListType)
                {
                    case NodeType.eTable:
                        {
                            //this.currentDbNode.Nodes.Count check weather database is open or not
                            if (this.currentDbNode != null && this.currentDbNode.Nodes.Count > 0)
                            {
                                //this.tvMain.SelectedNode = this.currentDbNode.Nodes[0];
                                NodeTypeInfo nodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                                foreach (Control c in this.tpObject.Controls)
                                {
                                    if (c is TableListView || c is SelectListView || c is ViewListView)
                                    {
                                        c.Visible = false;
                                    }
                                }
                                nodeTypeInfo.TableList.Visible = true;
                                nodeTypeInfo.TableList.BringToFront();
                            }
                            break;
                        }
                    case NodeType.eView:
                        {
                            if (this.currentDbNode != null && this.currentDbNode.Nodes.Count > 0)
                            {
                                //this.tvMain.SelectedNode = this.currentDbNode.Nodes[1];
                                NodeTypeInfo nodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                                foreach (Control c in this.tpObject.Controls)
                                {
                                    if (c is TableListView || c is SelectListView || c is ViewListView)
                                    {
                                        c.Visible = false;
                                    }
                                }
                                nodeTypeInfo.ViewList.Visible = true;
                                nodeTypeInfo.ViewList.BringToFront();
                            }
                            break;
                        }
                    case NodeType.eSelect:
                        {
                            if (this.currentDbNode != null && this.currentDbNode.Nodes.Count > 0)
                            {
                                //this.tvMain.SelectedNode = this.currentDbNode.Nodes[2];
                                NodeTypeInfo nodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                                foreach (Control c in this.tpObject.Controls)
                                {
                                    if (c is TableListView || c is SelectListView || c is ViewListView)
                                    {
                                        c.Visible = false;
                                    }
                                }
                                nodeTypeInfo.SelectList.Visible = true;
                                nodeTypeInfo.SelectList.BringToFront();
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        /// <summary>
        /// 显示选中TreeView不同节点，展示的右键菜单
        /// </summary>
        /// <param name="node">选中当前节点</param>
        /// <param name="e">当前触发的鼠标事件</param>
        private void ShowTreeViewContextMenu(TreeNode node, MouseEventArgs e)
        {
            NodeTypeInfo nodeTypeInfo = node.Tag as NodeTypeInfo;
            switch (nodeTypeInfo.NodeType)
            {
                case NodeType.eConnect:
                    {
                        this.cmsConnect.Tag = nodeTypeInfo.ConnectionInfo;
                        if (!nodeTypeInfo.ConnectionInfo.IsOpen)
                        {
                            this.tsmiOpenConnect.Enabled = true;
                            this.tsmiCloseConnect.Enabled = false;
                            this.tsmiRefreshConnect.Enabled = false;
                        }
                        else
                        {
                            this.tsmiOpenConnect.Enabled = false;
                            this.tsmiCloseConnect.Enabled = true;
                            this.tsmiRefreshConnect.Enabled = true;
                        }
                        this.cmsConnect.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
                case NodeType.eDatabase:
                    {
                        this.cmsDatabase.Tag = nodeTypeInfo.DatabaseInfo;
                        if (!nodeTypeInfo.DatabaseInfo.IsOpen)
                        {
                            this.tsmiOpenDatabase.Enabled = true;
                            this.tsmiCloseDatabase.Enabled = false;
                            this.tsmiDatabaseNewTable.Enabled = false;
                            this.tsmiDatabaseNewView.Enabled = false;
                            this.tsmiDatabaseNewSelect.Enabled = false;
                        }
                        else
                        {
                            this.tsmiOpenDatabase.Enabled = false;
                            this.tsmiCloseDatabase.Enabled = true;
                            this.tsmiDatabaseNewTable.Enabled = true;
                            this.tsmiDatabaseNewView.Enabled = true;
                            this.tsmiDatabaseNewSelect.Enabled = true;
                        }
                        this.cmsDatabase.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
                case NodeType.eTableGroup:
                    {
                        this.cmsTableGroup.Tag = nodeTypeInfo.DatabaseInfo;
                        this.cmsTableGroup.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
                case NodeType.eTable:
                    {
                        this.cmsTable.Tag = nodeTypeInfo.TableInfo;
                        this.cmsTable.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
                case NodeType.eViewGroup:
                    {
                        this.cmsViewGroup.Tag = nodeTypeInfo.DatabaseInfo;
                        this.cmsViewGroup.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
                case NodeType.eView:
                    {
                        this.cmsView.Tag = nodeTypeInfo.ViewInfo;
                        this.cmsView.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
                case NodeType.eSelectGroup:
                    {
                        this.cmsSelectGroup.Tag = nodeTypeInfo.DatabaseInfo;
                        this.cmsSelectGroup.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
                case NodeType.eSelect:
                    {
                        this.cmsSelect.Tag = nodeTypeInfo.SelectInfo;
                        this.cmsSelect.Show(this.tvMain, new Point(e.X, e.Y));
                        break;
                    }
            }
        }

        /// <summary>
        /// 选中最后一个页面
        /// </summary>
        private void SelectPage()
        {
            this.tcMain.SelectedTabPage = this.tcMain.TabPages[this.tcMain.TabPages.Count - 1];
            if (this.tcMain.SelectedTabPageIndex == 0)
            {
                TabPageTypeInfo pageTypeInfo = this.tcMain.SelectedTabPage.Tag as TabPageTypeInfo;
                this.ShowStatusBar(pageTypeInfo);
                this.Text = "SQLClient";
            }
        }

        #region FormLoad Event
        private void SQLClientForm_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ConnectInfo.LoadConfig();
                foreach (DataRow dr in dt.Rows)
                {
                    ConnectInfo info = ReflectionHelper.CreateInstance<ConnectInfo>(dr["assemblyName"].ToString(), dr["namespaceName"].ToString(), dr["className"].ToString());
                    this.imgListTreeView.Images.Add(dr["className"].ToString() + "_close", info.CloseImage());
                    this.imgListTreeView.Images.Add(dr["className"].ToString() + "_open", info.OpenImage());
                }
                this.spStatusBar.SplitterDistance = this.spStatusBar.Height - 25;
                TabPageTypeInfo pageTypeInfo = new TabPageTypeInfo(TabPageType.eObject, null);
                this.tpObject.Tag = pageTypeInfo;
                this.ShowStatusBar(pageTypeInfo);
                List<ConnectInfo> list = ConnectInfo.LoadConnection();
                foreach (ConnectInfo info in list)
                {
                    info.CloseConnect += Info_CloseConnect;
                    info.OpenConnect += Info_OpenConnect;
                    TreeNode node = new TreeNode();
                    node.Text = info.Name;
                    node.ImageKey = info.ClassName + "_close";
                    node.SelectedImageKey = info.ClassName + "_close";
                    node.Tag = new NodeTypeInfo(NodeType.eConnect, node, info);
                    info.Node = node;
                    this.tvMain.Nodes.Add(node);
                    this.tsObjectBlank.Text = $"{this.tvMain.Nodes.Count}服务器";
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void Info_OpenConnect(object sender, OpenConnectEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    ConnectInfo info = e.Info;
                    TreeNode node = info.Node;
                    this.tvMain.BeginUpdate();
                    foreach (DatabaseInfo dbInfo in info.Databases)
                    {
                        dbInfo.OpenDatabase += DbInfo_OpenDatabase;
                        dbInfo.CloseDatabase += DbInfo_CloseDatabase;
                        TreeNode dbNode = new TreeNode();
                        dbNode.Text = dbInfo.Name;
                        dbNode.ImageKey = Resource.image_key_database_close;
                        dbNode.SelectedImageKey = Resource.image_key_database_close;
                        dbNode.Tag = new NodeTypeInfo(NodeType.eDatabase, dbNode, dbInfo);
                        dbInfo.Node = dbNode;
                        node.Nodes.Add(dbNode);
                    }
                    this.tvMain.EndUpdate();
                    node.ImageKey = info.ClassName + "_open";
                    node.SelectedImageKey = info.ClassName + "_open";
                    node.Expand();
                    this.tsObjectBlank.Text = $"{info.Databases.Count}数据据库";
                    this.tsObjectOwner.Text = $"{info.Name} 用户: {info.User}";
                    this.tsObjectOwner.Image = info.OpenImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    LogHelper.Error(ex);
                }
                finally
                {
                    this.waitForm.Close();
                }
            }));
        }

        private void DbInfo_CloseDatabase(object sender, CloseDatabaseEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    DatabaseInfo info = e.Info;
                    TreeNode node = info.Node;
                    NodeTypeInfo nodeTypeInfo = node.Tag as NodeTypeInfo;
                    TreeNode tgNode = node.Nodes[0];
                    foreach (TreeNode tbNode in tgNode.Nodes)
                    {
                        NodeTypeInfo tbNodeTypeInfo = tbNode.Tag as NodeTypeInfo;
                        TableInfo tbInfo = tbNodeTypeInfo.TableInfo;
                        XtraTabPage openTablePage = tbInfo.OpenTablePage as XtraTabPage;
                        if (openTablePage != null)
                        {
                            //TODO close page
                            this.tcMain.TabPages.Remove(openTablePage);
                            this.SelectPage();
                        }

                        XtraTabPage designTablePage = tbInfo.DesignTablePage as XtraTabPage;
                        if (designTablePage != null)
                        {
                            //TODO close page
                            this.tcMain.TabPages.Remove(designTablePage);
                            this.SelectPage();
                        }
                    }

                    TreeNode vgNode = node.Nodes[1];
                    foreach (TreeNode vNode in vgNode.Nodes)
                    {
                        NodeTypeInfo vNodeTypeInfo = vNode.Tag as NodeTypeInfo;
                        ViewInfo vInfo = vNodeTypeInfo.ViewInfo;
                        XtraTabPage openViewPage = vInfo.OpenViewPage as XtraTabPage;
                        if (openViewPage != null)
                        {
                            //TODO close page
                            this.tcMain.TabPages.Remove(openViewPage);
                            this.SelectPage();
                        }
                    }

                    TreeNode sgNode = node.Nodes[2];
                    foreach (TreeNode sNode in sgNode.Nodes)
                    {
                        NodeTypeInfo sNodeTypeInfo = sNode.Tag as NodeTypeInfo;
                        SelectInfo sInfo = sNodeTypeInfo.SelectInfo;
                        XtraTabPage openSelectPage = sInfo.OpenSelectPage as XtraTabPage;
                        if (openSelectPage != null)
                        {
                            //TODO close page
                            this.tcMain.TabPages.Remove(openSelectPage);
                            this.SelectPage();
                        }
                    }

                    foreach(object o in info.NewSelectPages)
                    {
                        XtraTabPage page = o as XtraTabPage;
                        this.tcMain.TabPages.Remove(page);
                        this.SelectPage();
                    }

                    foreach (object o in info.NewTablePages)
                    {
                        XtraTabPage page = o as XtraTabPage;
                        this.tcMain.TabPages.Remove(page);
                        this.SelectPage();
                    }
                    info.NewTablePages.Clear();
                    info.NewSelectPages.Clear();
                    tgNode.Remove();
                    vgNode.Remove();
                    sgNode.Remove();
                    nodeTypeInfo.TableList.Clear();
                    nodeTypeInfo.ViewList.Clear();
                    nodeTypeInfo.SelectList.Clear();
                    this.tpObject.Controls.Remove(nodeTypeInfo.TableList);
                    this.tpObject.Controls.Remove(nodeTypeInfo.ViewList);
                    this.tpObject.Controls.Remove(nodeTypeInfo.SelectList);

                    node.ImageKey = Resource.image_key_database_close;
                    node.SelectedImageKey = Resource.image_key_database_close;
                    this.tsObjectBlank.Text = $"{info.ConnectInfo.Databases.Count}数据据库";
                    this.tsObjectOwner.Text = $"{info.ConnectInfo.Name} 用户: {info.ConnectInfo.User} 数据库: {info.Name}";
                    this.tsObjectOwner.Image = info.ConnectInfo.CloseImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    LogHelper.Error(ex);
                }
                finally
                {
                    this.waitForm.Close();
                }
            }));
        }

        private void DbInfo_OpenDatabase(object sender, OpenDatabaseEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    DatabaseInfo info = e.Info;
                    TreeNode node = info.Node;
                    NodeTypeInfo nodeTypeInfo = node.Tag as NodeTypeInfo;
                    this.tvMain.BeginUpdate();
                    //add table group node
                    TreeNode tgNode = new TreeNode();
                    NodeTypeInfo tgNodeTypeInfo = new NodeTypeInfo(NodeType.eTableGroup, tgNode, info);
                    tgNode.Text = Resource.node_name_table;
                    tgNode.ImageKey = Resource.image_key_table;
                    tgNode.SelectedImageKey = Resource.image_key_table;
                    tgNode.Tag = tgNodeTypeInfo;
                    node.Nodes.Add(tgNode);

                    //add table node and add table listview
                    tgNodeTypeInfo.TableList = new TableListView();
                    tgNodeTypeInfo.TableList.OpenTable += TableList_OpenTable;
                    tgNodeTypeInfo.TableList.DesignTable += TableList_DesignTable;
                    tgNodeTypeInfo.TableList.NewTable += TableList_NewTable;
                    tgNodeTypeInfo.TableList.ListViewShowMenu += TableList_ListViewShowMenu;
                    tgNodeTypeInfo.TableList.SmallImageList = this.imgListListView;
                    tgNodeTypeInfo.TableList.Dock = DockStyle.Fill;
                    tgNodeTypeInfo.TableList.View = View.List;
                    tgNodeTypeInfo.TableList.Visible = false;
                    tgNodeTypeInfo.TableList.Tag = tgNodeTypeInfo;
                    tgNodeTypeInfo.TableList.DatabaseInfo = info;
                    tgNodeTypeInfo.TableList.Tree = this.tvMain;
                    this.tpObject.Controls.Add(tgNodeTypeInfo.TableList);
                    foreach (TableInfo tbInfo in info.Tables)
                    {
                        TreeNode tbNode = new TreeNode();
                        tbNode.Text = tbInfo.Name;
                        tbNode.ImageKey = Resource.image_key_table;
                        tbNode.SelectedImageKey = Resource.image_key_table;
                        tbNode.Tag = new NodeTypeInfo(NodeType.eTable, tbNode, tbInfo);
                        tgNode.Nodes.Add(tbNode);
                        tbInfo.Node = tbNode;
                        tgNodeTypeInfo.TableList.AddItem(tbInfo, Resource.image_key_table);
                    }

                    //add view group node
                    TreeNode vgpNode = new TreeNode();
                    NodeTypeInfo vgNodeTypeInfo = new NodeTypeInfo(NodeType.eViewGroup, vgpNode, info);
                    vgpNode.Text = Resource.node_name_view;
                    vgpNode.ImageKey = Resource.image_key_view;
                    vgpNode.SelectedImageKey = Resource.image_key_view;
                    vgpNode.Tag = vgNodeTypeInfo;
                    node.Nodes.Add(vgpNode);

                    //add view node and view listview
                    vgNodeTypeInfo.ViewList = new ViewListView();
                    vgNodeTypeInfo.ViewList.OpenView += ViewList_OpenView;
                    vgNodeTypeInfo.ViewList.DesignView += ViewList_DesignView;
                    vgNodeTypeInfo.ViewList.ListViewShowMenu += ViewList_ListViewShowMenu;
                    vgNodeTypeInfo.ViewList.SmallImageList = this.imgListListView;
                    vgNodeTypeInfo.ViewList.Dock = DockStyle.Fill;
                    vgNodeTypeInfo.ViewList.View = View.List;
                    vgNodeTypeInfo.ViewList.Visible = false;
                    vgNodeTypeInfo.ViewList.Tag = vgNodeTypeInfo;
                    vgNodeTypeInfo.ViewList.DatabaseInfo = info;
                    vgNodeTypeInfo.ViewList.Tree = this.tvMain;
                    this.tpObject.Controls.Add(vgNodeTypeInfo.ViewList);
                    foreach (ViewInfo vInfo in info.Views)
                    {
                        TreeNode vNode = new TreeNode();
                        vNode.Text = vInfo.Name;
                        vNode.ImageKey = Resource.image_key_view;
                        vNode.SelectedImageKey = Resource.image_key_view;
                        vNode.Tag = new NodeTypeInfo(NodeType.eView, vNode, vInfo);
                        vInfo.Node = vNode;
                        vgpNode.Nodes.Add(vNode);
                        vgNodeTypeInfo.ViewList.AddItem(vInfo, Resource.image_key_view);
                    }

                    //add select group
                    TreeNode sgNode = new TreeNode();
                    NodeTypeInfo sgNodeTypeInfo = new NodeTypeInfo(NodeType.eSelectGroup, sgNode, info);
                    sgNode.Text = Resource.node_name_select;
                    sgNode.ImageKey = Resource.image_key_select;
                    sgNode.SelectedImageKey = Resource.image_key_select;
                    sgNode.Tag = sgNodeTypeInfo;
                    node.Nodes.Add(sgNode);

                    //add select node and select listview
                    sgNodeTypeInfo.SelectList = new SelectListView();
                    sgNodeTypeInfo.SelectList.NewSelect += SelectList_NewSelect;
                    sgNodeTypeInfo.SelectList.OpenSelect += SelectList_OpenSelect;
                    sgNodeTypeInfo.SelectList.ListViewShowMenu += SelectList_ListViewShowMenu;
                    sgNodeTypeInfo.SelectList.SmallImageList = this.imgListListView;
                    sgNodeTypeInfo.SelectList.Dock = DockStyle.Fill;
                    sgNodeTypeInfo.SelectList.View = View.List;
                    sgNodeTypeInfo.SelectList.Visible = false;
                    sgNodeTypeInfo.SelectList.Tag = sgNodeTypeInfo;
                    sgNodeTypeInfo.SelectList.DatabaseInfo = info;
                    sgNodeTypeInfo.SelectList.Tree = this.tvMain;
                    this.tpObject.Controls.Add(sgNodeTypeInfo.SelectList);
                    foreach (SelectInfo sInfo in info.Selects)
                    {
                        TreeNode sNode = new TreeNode();
                        sNode.Text = sInfo.Name;
                        sNode.ImageKey = Resource.image_key_select;
                        sNode.SelectedImageKey = Resource.image_key_select;
                        sNode.Tag = new NodeTypeInfo(NodeType.eSelect, sNode, sInfo);
                        sInfo.Node = sNode;
                        sgNode.Nodes.Add(sNode);
                        sgNodeTypeInfo.SelectList.AddItem(sInfo, Resource.image_key_select);
                    }
                    this.tvMain.EndUpdate();
                    node.ImageKey = Resource.image_key_database_open;
                    node.SelectedImageKey = Resource.image_key_database_open;
                    node.Expand();
                    nodeTypeInfo.TableList = tgNodeTypeInfo.TableList;
                    nodeTypeInfo.ViewList = vgNodeTypeInfo.ViewList;
                    nodeTypeInfo.SelectList = sgNodeTypeInfo.SelectList;

                    this.SelectDbNode(node, this.currentViewListType, true);
                    this.SelectToolBar();
                    nodeTypeInfo.TableList.UnSelectItem();
                    nodeTypeInfo.ViewList.UnSelectItem();
                    nodeTypeInfo.SelectList.UnSelectItem();
                    this.tsObjectBlank.Text = $"{info.Tables.Count}表({info.Tables.Count}位于当前的组)";
                    this.tsObjectOwner.Text = $"{info.ConnectInfo.Name} 用户:{info.ConnectInfo.User} 数据库:{info.Name}";
                    this.tsObjectOwner.Image = info.ConnectInfo.OpenImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    LogHelper.Error(ex);
                }
                finally
                {
                    this.waitForm.Close();
                }
            }));
        }

        private void Info_CloseConnect(object sender, CloseConnectEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                try
                {
                    ConnectInfo info = e.Info;
                    TreeNode node = info.Node;
                    while(node.Nodes.Count > 0)
                    {
                        node.Nodes[0].Remove();
                    }
                    node.ImageKey = info.ClassName + "_close";
                    node.SelectedImageKey = info.ClassName + "_close";
                    this.tsObjectBlank.Text = $"{info.Databases.Count}数据据库";
                    this.tsObjectOwner.Text = $"{info.Name} 用户: {info.User}";
                    this.tsObjectOwner.Image = info.CloseImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    LogHelper.Error(ex);
                }
                finally
                {
                    this.waitForm.Close();
                }
            }));
        }
        #endregion

        #region ListView Event

        private void ViewList_DesignView(object sender, DesignViewEventArgs e)
        {
            MessageBox.Show("Not Support");
        }

        private void ViewList_OpenView(object sender, OpenViewEventArgs e)
        {
            try
            {
                ViewInfo info = e.Info;
                if (info.IsOpen)
                {
                    return;
                }

                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenView);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void TableList_OpenTable(object sender, OpenTableEventArgs e)
        {
            try
            {
                TableInfo info = e.Info;
                if (info.IsOpen)
                {
                    return;
                }

                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenTable);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void TableList_DesignTable(object sender, DesignTableEventArgs e)
        {
            try
            {
                TableInfo info = e.Info;
                if (info.IsDesign)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(DesignTable);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void TableList_NewTable(object sender, NewTableEventArgs e)
        {
            try
            {
                DatabaseInfo databaseInfo = e.DatabaseInfo;
                this.NewTable(databaseInfo);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void SelectList_NewSelect(object sender, NewSelectEventArgs e)
        {
            try
            {
                DatabaseInfo databaseInfo = e.DatabaseInfo;
                this.NewSelect(databaseInfo);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void SelectList_OpenSelect(object sender, OpenSelectEventArgs e)
        {
            try
            {
                SelectInfo info = e.Info;
                if (info.IsOpen)
                {
                    return;
                }

                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenSelect);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion

        #region XtraTabPage Event
        private void tcMain_CloseButtonClick(object sender, EventArgs e)
        {
            try
            {
                DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs args = e as DevExpress.XtraTab.ViewInfo.ClosePageButtonEventArgs;
                foreach (XtraTabPage page in this.tcMain.TabPages)
                {
                    if (args.Page.Text == page.Text)
                    {
                        TabPageTypeInfo pageTypeInfo = page.Tag as TabPageTypeInfo;
                        switch (pageTypeInfo.PageType)
                        {
                            case TabPageType.eOpenTable:
                                {
                                    TableInfo info = pageTypeInfo.Info as TableInfo;
                                    info.Close();
                                    break;
                                }
                            case TabPageType.eOpenView:
                                {
                                    ViewInfo info = pageTypeInfo.Info as ViewInfo;
                                    info.Close();
                                    break;
                                }
                            case TabPageType.eOpenSelect:
                                {
                                    SelectInfo info = pageTypeInfo.Info as SelectInfo;
                                    if (info != null)
                                    {
                                        info.Close();
                                    }
                                    break;
                                }
                            case TabPageType.eDesignTable:
                                {
                                    TableInfo info = pageTypeInfo.Info as TableInfo;
                                    if (info != null)
                                    {
                                        info.CloseDesign();
                                    }
                                    break;
                                }
                            case TabPageType.eDesignView:
                                {
                                    break;
                                }
                            case TabPageType.eNewTable:
                                {
                                    DesignTablePage designTablePage = pageTypeInfo.DesignTablePage;
                                    TableInfo info = designTablePage.TableInfo;
                                    if (info != null)
                                    {
                                        info.CloseDesign();
                                        info.DatabaseInfo.NewTablePages.Remove(page);
                                    }
                                    break;
                                }
                            case TabPageType.eNewView:
                                {

                                    break;
                                }
                            case TabPageType.eNewSelect:
                                {
                                    NewSelectPage newSelectPage = pageTypeInfo.NewSelectPage;
                                    SelectInfo info = newSelectPage.SelectInfo;
                                    if (info != null)
                                    {
                                        info.Close();
                                        info.DatabaseInfo.NewSelectPages.Remove(page);
                                    }
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        this.tcMain.TabPages.Remove(page);
                        break;
                    }
                }
                this.tcMain.SelectedTabPage = this.tcMain.TabPages[this.tcMain.TabPages.Count - 1];
                if (this.tcMain.SelectedTabPageIndex == 0)
                {
                    TabPageTypeInfo pageTypeInfo = this.tcMain.SelectedTabPage.Tag as TabPageTypeInfo;
                    this.ShowStatusBar(pageTypeInfo);
                    this.Text = "SQLClient";
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tcMain_Selected(object sender, TabPageEventArgs e)
        {
            try
            {
                TabPageTypeInfo pageTypeInfo = e.Page.Tag as TabPageTypeInfo;
                if (pageTypeInfo.PageType == TabPageType.eObject)
                {
                    this.Text = "SQLClient";
                }
                else
                {
                    this.Text = e.Page.Text + " - SQLClient";
                }
                this.ShowStatusBar(pageTypeInfo);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion

        #region TreeView Event
        private void tvMain_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                TreeNode node = this.tvMain.GetNodeAt(e.X, e.Y);
                if (node == null)
                {
                    return;
                }

                this.tvMain.SelectedNode = node;
                this.SelectNode(node);
                if (e.Button == MouseButtons.Right)
                {
                    this.ShowTreeViewContextMenu(node, e);
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tvMain_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.tvMain.SelectedNode == null)
                {
                    return;
                }
                TreeNode node = this.tvMain.SelectedNode;
                NodeTypeInfo nodeTypeInfo = node.Tag as NodeTypeInfo;
                switch (nodeTypeInfo.NodeType)
                {
                    case NodeType.eConnect:
                        {
                            break;
                        }
                    case NodeType.eDatabase:
                        {
                            break;
                        }
                    case NodeType.eSelect:
                        {
                            NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                            dbNodeTypeInfo.SelectList.UnSelectItem();
                            break;
                        }
                    case NodeType.eSelectGroup:
                        {
                            break;
                        }
                    case NodeType.eTable:
                        {
                            NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                            dbNodeTypeInfo.TableList.UnSelectItem();
                            break;
                        }
                    case NodeType.eTableGroup:
                        {

                            break;
                        }
                    case NodeType.eView:
                        {
                            NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                            dbNodeTypeInfo.ViewList.UnSelectItem();
                            break;
                        }
                    case NodeType.eViewGroup:
                        {
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tvMain_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode node = this.tvMain.SelectedNode;
                NodeTypeInfo nodeTypeInfo = node.Tag as NodeTypeInfo;
                switch (nodeTypeInfo.NodeType)
                {
                    case NodeType.eConnect:
                        {
                            ConnectInfo info = nodeTypeInfo.ConnectionInfo;
                            if (info.IsOpen)
                            {
                                return;
                            }
                            AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenConnect);
                            ah.BeginInvoke(nodeTypeInfo, null, null);
                            this.waitForm.ShowDialog();
                            break;
                        }
                    case NodeType.eDatabase:
                        {
                            DatabaseInfo info = nodeTypeInfo.DatabaseInfo;
                            if (info.IsOpen)
                            {
                                return;
                            }
                            AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenDatabase);
                            ah.BeginInvoke(nodeTypeInfo, null, null);
                            this.waitForm.ShowDialog();
                            break;
                        }
                    case NodeType.eTableGroup:
                        {
                            this.currentDbNode = node.Parent;
                            break;
                        }
                    case NodeType.eViewGroup:
                        {
                            this.currentDbNode = node.Parent;
                            break;
                        }
                    case NodeType.eSelectGroup:
                        {
                            this.currentDbNode = node.Parent;
                            break;
                        }
                    case NodeType.eTable:
                        {
                            TableInfo info = nodeTypeInfo.TableInfo;
                            if (info.IsOpen)
                            {
                                return;
                            }
                            AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenTable);
                            ah.BeginInvoke(info, null, null);
                            this.waitForm.ShowDialog();
                            break;
                        }
                    case NodeType.eView:
                        {
                            ViewInfo info = nodeTypeInfo.ViewInfo;
                            if (info.IsOpen)
                            {
                                return;
                            }
                            AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenView);
                            ah.BeginInvoke(info, null, null);
                            this.waitForm.ShowDialog();
                            break;
                        }
                    case NodeType.eSelect:
                        {
                            SelectInfo info = nodeTypeInfo.SelectInfo;
                            if (info.IsOpen)
                            {
                                return;
                            }
                            AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenSelect);
                            ah.BeginInvoke(info, null, null);
                            this.waitForm.ShowDialog();
                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        #endregion

        #region ToolBar Event

        private void tsbtnTable_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentDbNode == null)
                {
                    return;
                }
                NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                if (dbNodeTypeInfo == null)
                {
                    return;
                }
                this.tvMain.SelectedNode = this.currentDbNode.Nodes[0];
                this.SelectDbNode(this.currentDbNode, NodeType.eTable);
                this.currentViewListType = NodeType.eTable;
                this.SelectToolBar();
                dbNodeTypeInfo.TableList.UnSelectItem();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentDbNode == null)
                {
                    return;
                }
                NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                if (dbNodeTypeInfo == null)
                {
                    return;
                }
                this.tvMain.SelectedNode = this.currentDbNode.Nodes[1];
                this.SelectDbNode(this.currentDbNode, NodeType.eView);
                this.currentViewListType = NodeType.eView;
                this.SelectToolBar();

                dbNodeTypeInfo.ViewList.UnSelectItem();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.currentDbNode == null)
                {
                    return;
                }
                NodeTypeInfo dbNodeTypeInfo = this.currentDbNode.Tag as NodeTypeInfo;
                if (dbNodeTypeInfo == null)
                {
                    return;
                }
                this.tvMain.SelectedNode = this.currentDbNode.Nodes[2];
                this.SelectDbNode(this.currentDbNode, NodeType.eSelect);
                this.currentViewListType = NodeType.eSelect;
                this.SelectToolBar();

                dbNodeTypeInfo.SelectList.UnSelectItem();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsbtnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.OpenNewConnectionForm();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
        #endregion

        #region FormResize Event
        private void spMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                this.ReSizeStatusBar();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void SQLClientForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.ReSizeStatusBar();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
        #endregion

        #region Status Bar Event

        private void DesignTablePage_ChangeStatusBar(object sender, ChangeStatusBarEventArgs e)
        {
            this.tsFields.Text = $"字段数: {e.FieldCount}";
        }

        private void OpenTalePage_ChangeStatusBar(object sender, ChangeStatusBarEventArgs e)
        {
            this.tsOpenTableStmt.Text = e.Statement;
            this.tsOpenTablePageInfo.Text = $"第{e.RowIndex}条记录(共{e.Count}条)于第{e.CrtPage}页";
            this.ReSizeStatusBar();
        }

        private void NewSelectPage_ChangeStatusBar(object sender, ChangeStatusBarEventArgs e)
        {
            if (e.Reset)
            {
                this.tsNewSelectStmt.Text = "自动完成代码就绪";
                this.tsNewSelectTime.Text = "查询时间:0.000s";
                this.tsNewSelectPageInfo.Text = "第0条记录(共0条)";
            }
            else
            {
                this.tsNewSelectStmt.Text = e.Statement;
                this.tsNewSelectTime.Text = $"查询时间: {e.Cost / 1000.0}s";
                this.tsNewSelectPageInfo.Text = $"第{e.RowIndex}条记录 (共{e.Count}条)";
            }
            this.ReSizeStatusBar();
        }

        private void OpenViewPage_ChangeStatusBar(object sender, ChangeStatusBarEventArgs e)
        {
            this.tsOpenTableStmt.Text = e.Statement;
            this.tsOpenTablePageInfo.Text = $"第{e.RowIndex}条记录(共{e.Count}条)于第{e.CrtPage}页";
            this.ReSizeStatusBar();
        }

        #endregion

        #region ContextMenu Event

        private void SelectList_ListViewShowMenu(object sender, ListViewShowMenuEventArgs e)
        {
            try
            {
                if (e.SelectInfo != null)
                {
                    this.cmsSelect.Tag = e.SelectInfo;
                    this.cmsSelect.Show((sender as ListView), new Point(e.MouseEvent.X, e.MouseEvent.Y));
                }
                else
                {
                    this.cmsSelectGroup.Tag = e.DatabaseInfo;
                    this.cmsSelectGroup.Show((sender as ListView), new Point(e.MouseEvent.X, e.MouseEvent.Y));
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void ViewList_ListViewShowMenu(object sender, ListViewShowMenuEventArgs e)
        {
            try
            {
                if (e.ViewInfo != null)
                {
                    this.cmsView.Tag = e.ViewInfo;
                    this.cmsView.Show((sender as ListView), new Point(e.MouseEvent.X, e.MouseEvent.Y));
                }
                else
                {
                    this.cmsViewGroup.Tag = e.DatabaseInfo;
                    this.cmsViewGroup.Show((sender as ListView), new Point(e.MouseEvent.X, e.MouseEvent.Y));
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void TableList_ListViewShowMenu(object sender, ListViewShowMenuEventArgs e)
        {
            try
            {
                if (e.TableInfo != null)
                {
                    this.cmsTable.Tag = e.TableInfo;
                    this.cmsTable.Show((sender as ListView), new Point(e.MouseEvent.X, e.MouseEvent.Y));
                }
                else
                {
                    this.cmsTableGroup.Tag = e.DatabaseInfo;
                    this.cmsTableGroup.Show((sender as ListView), new Point(e.MouseEvent.X, e.MouseEvent.Y));
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiOpenConnect_Click(object sender, EventArgs e)
        {
            try
            {
                NodeTypeInfo nodeTypeInfo = this.tvMain.SelectedNode.Tag as NodeTypeInfo;
                ConnectInfo info = nodeTypeInfo.ConnectionInfo;
                if (info.IsOpen)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenConnect);
                ah.BeginInvoke(nodeTypeInfo, null, null);
                this.waitForm.ShowDialog();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiOpenDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                NodeTypeInfo nodeTypeInfo = this.tvMain.SelectedNode.Tag as NodeTypeInfo;
                DatabaseInfo info = nodeTypeInfo.DatabaseInfo;
                if (info.IsOpen)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenDatabase);
                ah.BeginInvoke(nodeTypeInfo, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiGroupNewTable_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseInfo databaseInfo = this.cmsTableGroup.Tag as DatabaseInfo;
                this.NewTable(databaseInfo);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiTableNewTable_Click(object sender, EventArgs e)
        {
            try
            {
                TableInfo tableInfo = this.cmsTable.Tag as TableInfo;
                this.NewTable(tableInfo.DatabaseInfo);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiOpenTable_Click(object sender, EventArgs e)
        {
            try
            {
                TableInfo info = this.cmsTable.Tag as TableInfo;
                if (info.IsOpen)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenTable);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiDesignTable_Click(object sender, EventArgs e)
        {
            try
            {
                TableInfo info = this.cmsTable.Tag as TableInfo;
                if (info.IsDesign)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(DesignTable);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiOpenView_Click(object sender, EventArgs e)
        {
            try
            {
                ViewInfo info = this.cmsView.Tag as ViewInfo;
                if (info.IsOpen)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenView);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiGrupNewSelect_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseInfo databaseInfo = this.cmsSelectGroup.Tag as DatabaseInfo;
                this.NewSelect(databaseInfo);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiOpenSelect_Click(object sender, EventArgs e)
        {
            try
            {
                SelectInfo info = this.cmsSelect.Tag as SelectInfo;
                if (info.IsOpen)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenSelect);
                ah.BeginInvoke(info, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiNewSelect_Click(object sender, EventArgs e)
        {
            try
            {
                SelectInfo selectInfo = this.cmsSelect.Tag as SelectInfo;
                this.NewSelect(selectInfo.DatabaseInfo);
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiMenuOpenConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tvMain.SelectedNode == null) return;
                NodeTypeInfo nodeTypeInfo = this.tvMain.SelectedNode.Tag as NodeTypeInfo;
                if (nodeTypeInfo.NodeType != NodeType.eConnect) return;
                ConnectInfo info = nodeTypeInfo.ConnectionInfo;
                if (info.IsOpen)
                {
                    return;
                }
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(OpenConnect);
                ah.BeginInvoke(nodeTypeInfo, null, null);
                this.waitForm.ShowDialog();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiCloseConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tvMain.SelectedNode == null) return;
                NodeTypeInfo nodeTypeInfo = this.tvMain.SelectedNode.Tag as NodeTypeInfo;
                if (nodeTypeInfo.NodeType != NodeType.eConnect) return;
                ConnectInfo info = nodeTypeInfo.ConnectionInfo;
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(CloseConnect);
                ah.BeginInvoke(nodeTypeInfo, null, null);
                this.waitForm.ShowDialog();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiDatabaseNewSelect_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseInfo databaseInfo = this.cmsDatabase.Tag as DatabaseInfo;
                this.NewSelect(databaseInfo);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiDatabaseNewTable_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseInfo databaseInfo = this.cmsDatabase.Tag as DatabaseInfo;
                this.NewTable(databaseInfo);
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiDatabaseNewView_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseInfo databaseInfo = this.cmsDatabase.Tag as DatabaseInfo;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiCloseDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.tvMain.SelectedNode == null) return;
                NodeTypeInfo nodeTypeInfo = this.tvMain.SelectedNode.Tag as NodeTypeInfo;
                if (nodeTypeInfo.NodeType != NodeType.eDatabase) return;
                DatabaseInfo info = nodeTypeInfo.DatabaseInfo;
                AsyncHelper.AsyncHandlerArgs ah = new AsyncHelper.AsyncHandlerArgs(CloseDatabase);
                ah.BeginInvoke(nodeTypeInfo, null, null);
                this.waitForm.ShowDialog();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void tsmiConnectProperty_Click(object sender, EventArgs e)
        {
            ConnectInfo connectionInfo = this.cmsConnect.Tag as ConnectInfo;
            Form form = connectionInfo.GetConnectForm();
            IConnectionForm iconnectForm = form as IConnectionForm;
            iconnectForm.LoadConnectionInfo(connectionInfo);
            if (form.ShowDialog() == DialogResult.OK)
            {
                connectionInfo.ConnectionString = iconnectForm.ConnectionString;
                connectionInfo.User = iconnectForm.User;
                connectionInfo.Password = iconnectForm.Password;
                ConnectInfo.UpdateConnection(connectionInfo);
            }
        }

        private void tsmiFile_DropDownOpening(object sender, EventArgs e)
        {
            if (this.tvMain.SelectedNode == null) return;
            NodeTypeInfo nodeTypeInfo = this.tvMain.SelectedNode.Tag as NodeTypeInfo;
            if (nodeTypeInfo.NodeType != NodeType.eConnect)
            {
                this.tsmiMenuCloseConnect.Enabled = false;
            }
            else
            {
                ConnectInfo info = nodeTypeInfo.ConnectionInfo;
                if (info.IsOpen)
                {
                    this.tsmiMenuCloseConnect.Enabled = true;
                }
                else
                {
                    this.tsmiMenuCloseConnect.Enabled = false;
                }
            }
        }
        #endregion

    }
}
