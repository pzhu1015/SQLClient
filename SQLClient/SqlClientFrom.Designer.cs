namespace SQLClient
{
    partial class SqlClientForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlClientForm));
            this.tvMain = new System.Windows.Forms.TreeView();
            this.imgListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.tcMain = new DevExpress.XtraTab.XtraTabControl();
            this.tpObject = new DevExpress.XtraTab.XtraTabPage();
            this.imgListListView = new System.Windows.Forms.ImageList(this.components);
            this.tsSystemMain = new System.Windows.Forms.ToolStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuOpenConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMenuCloseConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiImportConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTool = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOption = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDropDownHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbtnConnect = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnTable = new System.Windows.Forms.ToolStripButton();
            this.tsbtnView = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSelect = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsObject = new System.Windows.Forms.ToolStrip();
            this.tsObjectBlank = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsObjectOwner = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDesignTable = new System.Windows.Forms.ToolStrip();
            this.tsDesignTableBlank = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsFields = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.tsIndexs = new System.Windows.Forms.ToolStripLabel();
            this.tsPrimaryKeys = new System.Windows.Forms.ToolStripLabel();
            this.tsTriggers = new System.Windows.Forms.ToolStripLabel();
            this.tsNewSelect = new System.Windows.Forms.ToolStrip();
            this.tsNewSelectBlank = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsNewSelectStmt = new System.Windows.Forms.ToolStripTextBox();
            this.tsNewSelectPageInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsNewSelectTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOpenTable = new System.Windows.Forms.ToolStrip();
            this.tsOpenTableBlank = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOpenTableStmt = new System.Windows.Forms.ToolStripTextBox();
            this.tsOpenTablePageInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator30 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOpenTableWarn = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.spStatusBar = new System.Windows.Forms.SplitContainer();
            this.spMain = new System.Windows.Forms.SplitContainer();
            this.cmsTableGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiGroupNewTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiImportTableGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportTableGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshTableGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsViewGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiGroupNewView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiGroupExprotView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshViewGruop = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSelectGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiGrupNewSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshSelectGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsConnect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNewConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConnectProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDatabase = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCloseDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDatabaseNewTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDatabaseNewView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDatabaseNewSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator20 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNewDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshDatabase = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesignTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTableNewTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiImportTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExportTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshTable = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesignView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewNewView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator26 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExportView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator27 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshView = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsSelect = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDesignSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExprotSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiRefreshSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCheckUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).BeginInit();
            this.tcMain.SuspendLayout();
            this.tsSystemMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsObject.SuspendLayout();
            this.tsDesignTable.SuspendLayout();
            this.tsNewSelect.SuspendLayout();
            this.tsOpenTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spStatusBar)).BeginInit();
            this.spStatusBar.Panel1.SuspendLayout();
            this.spStatusBar.Panel2.SuspendLayout();
            this.spStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).BeginInit();
            this.spMain.Panel1.SuspendLayout();
            this.spMain.Panel2.SuspendLayout();
            this.spMain.SuspendLayout();
            this.cmsTableGroup.SuspendLayout();
            this.cmsViewGroup.SuspendLayout();
            this.cmsSelectGroup.SuspendLayout();
            this.cmsConnect.SuspendLayout();
            this.cmsDatabase.SuspendLayout();
            this.cmsTable.SuspendLayout();
            this.cmsView.SuspendLayout();
            this.cmsSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvMain
            // 
            this.tvMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMain.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvMain.FullRowSelect = true;
            this.tvMain.HideSelection = false;
            this.tvMain.ImageIndex = 0;
            this.tvMain.ImageList = this.imgListTreeView;
            this.tvMain.Location = new System.Drawing.Point(0, 0);
            this.tvMain.Margin = new System.Windows.Forms.Padding(0);
            this.tvMain.Name = "tvMain";
            this.tvMain.SelectedImageIndex = 0;
            this.tvMain.ShowLines = false;
            this.tvMain.Size = new System.Drawing.Size(260, 400);
            this.tvMain.TabIndex = 0;
            this.tvMain.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMain_NodeMouseDoubleClick);
            this.tvMain.Leave += new System.EventHandler(this.tvMain_Leave);
            this.tvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMain_MouseDown);
            // 
            // imgListTreeView
            // 
            this.imgListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListTreeView.ImageStream")));
            this.imgListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListTreeView.Images.SetKeyName(0, "database_close");
            this.imgListTreeView.Images.SetKeyName(1, "database_open");
            this.imgListTreeView.Images.SetKeyName(2, "select");
            this.imgListTreeView.Images.SetKeyName(3, "table");
            this.imgListTreeView.Images.SetKeyName(4, "view");
            // 
            // tcMain
            // 
            this.tcMain.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.tcMain.Appearance.Options.UseBackColor = true;
            this.tcMain.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tcMain.BorderStylePage = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.tcMain.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedTabPage = this.tpObject;
            this.tcMain.Size = new System.Drawing.Size(862, 400);
            this.tcMain.TabIndex = 0;
            this.tcMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tpObject});
            this.tcMain.CloseButtonClick += new System.EventHandler(this.tcMain_CloseButtonClick);
            this.tcMain.Selected += new DevExpress.XtraTab.TabPageEventHandler(this.tcMain_Selected);
            // 
            // tpObject
            // 
            this.tpObject.Appearance.PageClient.BackColor = System.Drawing.Color.White;
            this.tpObject.Appearance.PageClient.Options.UseBackColor = true;
            this.tpObject.Margin = new System.Windows.Forms.Padding(0);
            this.tpObject.Name = "tpObject";
            this.tpObject.ShowCloseButton = DevExpress.Utils.DefaultBoolean.False;
            this.tpObject.Size = new System.Drawing.Size(856, 371);
            this.tpObject.Text = "对象";
            // 
            // imgListListView
            // 
            this.imgListListView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListListView.ImageStream")));
            this.imgListListView.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListListView.Images.SetKeyName(0, "select");
            this.imgListListView.Images.SetKeyName(1, "table");
            this.imgListListView.Images.SetKeyName(2, "view");
            // 
            // tsSystemMain
            // 
            this.tsSystemMain.BackColor = System.Drawing.SystemColors.Control;
            this.tsSystemMain.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsSystemMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsSystemMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiView,
            this.tsmiStore,
            this.tsmiTool,
            this.tsmiWindow,
            this.tsmiDropDownHelp});
            this.tsSystemMain.Location = new System.Drawing.Point(0, 0);
            this.tsSystemMain.Name = "tsSystemMain";
            this.tsSystemMain.Padding = new System.Windows.Forms.Padding(0);
            this.tsSystemMain.Size = new System.Drawing.Size(1125, 25);
            this.tsSystemMain.TabIndex = 10;
            this.tsSystemMain.Text = "toolStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConnect,
            this.tsmiMenuOpenConnect,
            this.tsmiMenuCloseConnect,
            this.toolStripSeparator9,
            this.tsmiExportConnect,
            this.tsmiImportConnect});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(44, 25);
            this.tsmiFile.Text = "文件";
            this.tsmiFile.DropDownOpening += new System.EventHandler(this.tsmiFile_DropDownOpening);
            // 
            // tsmiConnect
            // 
            this.tsmiConnect.Name = "tsmiConnect";
            this.tsmiConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiConnect.Text = "新建连接";
            this.tsmiConnect.Click += new System.EventHandler(this.tsbtnConnect_Click);
            // 
            // tsmiMenuOpenConnect
            // 
            this.tsmiMenuOpenConnect.Name = "tsmiMenuOpenConnect";
            this.tsmiMenuOpenConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiMenuOpenConnect.Text = "打开连接";
            this.tsmiMenuOpenConnect.Click += new System.EventHandler(this.tsmiMenuOpenConnect_Click);
            // 
            // tsmiMenuCloseConnect
            // 
            this.tsmiMenuCloseConnect.Name = "tsmiMenuCloseConnect";
            this.tsmiMenuCloseConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiMenuCloseConnect.Text = "关闭连接";
            this.tsmiMenuCloseConnect.Click += new System.EventHandler(this.tsmiCloseConnect_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiExportConnect
            // 
            this.tsmiExportConnect.Name = "tsmiExportConnect";
            this.tsmiExportConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiExportConnect.Text = "导出连接";
            // 
            // tsmiImportConnect
            // 
            this.tsmiImportConnect.Name = "tsmiImportConnect";
            this.tsmiImportConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiImportConnect.Text = "导入连接";
            // 
            // tsmiView
            // 
            this.tsmiView.Name = "tsmiView";
            this.tsmiView.Size = new System.Drawing.Size(44, 25);
            this.tsmiView.Text = "查看";
            // 
            // tsmiStore
            // 
            this.tsmiStore.Name = "tsmiStore";
            this.tsmiStore.Size = new System.Drawing.Size(56, 25);
            this.tsmiStore.Text = "收藏夹";
            // 
            // tsmiTool
            // 
            this.tsmiTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCommand,
            this.tsmiOption});
            this.tsmiTool.Name = "tsmiTool";
            this.tsmiTool.Size = new System.Drawing.Size(44, 25);
            this.tsmiTool.Text = "工具";
            // 
            // tsmiCommand
            // 
            this.tsmiCommand.Image = global::SQLClient.Properties.Resources.command_line_16;
            this.tsmiCommand.Name = "tsmiCommand";
            this.tsmiCommand.Size = new System.Drawing.Size(152, 22);
            this.tsmiCommand.Text = "命令界面";
            // 
            // tsmiOption
            // 
            this.tsmiOption.Name = "tsmiOption";
            this.tsmiOption.Size = new System.Drawing.Size(152, 22);
            this.tsmiOption.Text = "选项";
            // 
            // tsmiWindow
            // 
            this.tsmiWindow.Name = "tsmiWindow";
            this.tsmiWindow.Size = new System.Drawing.Size(44, 25);
            this.tsmiWindow.Text = "窗口";
            // 
            // tsmiDropDownHelp
            // 
            this.tsmiDropDownHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelp,
            this.tsmiCheckUpdate,
            this.toolStripSeparator12,
            this.tsmiAbout});
            this.tsmiDropDownHelp.Name = "tsmiDropDownHelp";
            this.tsmiDropDownHelp.Size = new System.Drawing.Size(44, 25);
            this.tsmiDropDownHelp.Text = "帮助";
            // 
            // tsbtnConnect
            // 
            this.tsbtnConnect.Image = global::SQLClient.Properties.Resources.CONNECT_32;
            this.tsbtnConnect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnConnect.Margin = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.tsbtnConnect.Name = "tsbtnConnect";
            this.tsbtnConnect.Padding = new System.Windows.Forms.Padding(20, 20, 20, 1);
            this.tsbtnConnect.Size = new System.Drawing.Size(76, 74);
            this.tsbtnConnect.Text = "连接";
            this.tsbtnConnect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnConnect.Click += new System.EventHandler(this.tsbtnConnect_Click);
            // 
            // tsbtnUser
            // 
            this.tsbtnUser.Image = global::SQLClient.Properties.Resources.USER_32;
            this.tsbtnUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUser.Margin = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.tsbtnUser.Name = "tsbtnUser";
            this.tsbtnUser.Padding = new System.Windows.Forms.Padding(20, 20, 20, 1);
            this.tsbtnUser.Size = new System.Drawing.Size(76, 74);
            this.tsbtnUser.Text = "用户";
            this.tsbtnUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 76);
            // 
            // tsbtnTable
            // 
            this.tsbtnTable.Image = global::SQLClient.Properties.Resources.TABLE_32;
            this.tsbtnTable.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnTable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnTable.Margin = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.tsbtnTable.Name = "tsbtnTable";
            this.tsbtnTable.Padding = new System.Windows.Forms.Padding(20, 20, 20, 1);
            this.tsbtnTable.Size = new System.Drawing.Size(76, 74);
            this.tsbtnTable.Text = "表";
            this.tsbtnTable.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnTable.Click += new System.EventHandler(this.tsbtnTable_Click);
            // 
            // tsbtnView
            // 
            this.tsbtnView.Image = global::SQLClient.Properties.Resources.VIEW_32;
            this.tsbtnView.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnView.Margin = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.tsbtnView.Name = "tsbtnView";
            this.tsbtnView.Padding = new System.Windows.Forms.Padding(20, 20, 20, 1);
            this.tsbtnView.Size = new System.Drawing.Size(76, 74);
            this.tsbtnView.Text = "视图";
            this.tsbtnView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnView.Click += new System.EventHandler(this.tsbtnView_Click);
            // 
            // tsbtnSelect
            // 
            this.tsbtnSelect.Image = global::SQLClient.Properties.Resources.SELECT_32;
            this.tsbtnSelect.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSelect.Margin = new System.Windows.Forms.Padding(5, 1, 0, 1);
            this.tsbtnSelect.Name = "tsbtnSelect";
            this.tsbtnSelect.Padding = new System.Windows.Forms.Padding(20, 20, 20, 1);
            this.tsbtnSelect.Size = new System.Drawing.Size(76, 74);
            this.tsbtnSelect.Text = "查询";
            this.tsbtnSelect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnSelect.Click += new System.EventHandler(this.tsbtnSelect_Click);
            // 
            // msMain
            // 
            this.msMain.BackColor = System.Drawing.SystemColors.Control;
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnConnect,
            this.tsbtnUser,
            this.toolStripSeparator8,
            this.tsbtnTable,
            this.tsbtnView,
            this.tsbtnSelect});
            this.msMain.Location = new System.Drawing.Point(0, 25);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(1125, 80);
            this.msMain.TabIndex = 16;
            this.msMain.Text = "menuStrip1";
            // 
            // tsObject
            // 
            this.tsObject.BackColor = System.Drawing.SystemColors.Control;
            this.tsObject.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsObject.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsObject.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsObjectBlank,
            this.toolStripSeparator13,
            this.tsObjectOwner,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator14});
            this.tsObject.Location = new System.Drawing.Point(0, 98);
            this.tsObject.Name = "tsObject";
            this.tsObject.Padding = new System.Windows.Forms.Padding(0);
            this.tsObject.Size = new System.Drawing.Size(1125, 25);
            this.tsObject.TabIndex = 32;
            this.tsObject.Text = "toolStrip1";
            // 
            // tsObjectBlank
            // 
            this.tsObjectBlank.AutoSize = false;
            this.tsObjectBlank.Name = "tsObjectBlank";
            this.tsObjectBlank.Size = new System.Drawing.Size(261, 22);
            this.tsObjectBlank.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(6, 25);
            // 
            // tsObjectOwner
            // 
            this.tsObjectOwner.Name = "tsObjectOwner";
            this.tsObjectOwner.Size = new System.Drawing.Size(78, 22);
            this.tsObjectOwner.Text = "数据库: 用户:";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "详细信息";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 22);
            this.toolStripButton2.Text = "列表";
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(6, 25);
            // 
            // tsDesignTable
            // 
            this.tsDesignTable.BackColor = System.Drawing.SystemColors.Control;
            this.tsDesignTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsDesignTable.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsDesignTable.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDesignTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDesignTableBlank,
            this.toolStripSeparator15,
            this.tsFields,
            this.toolStripSeparator16,
            this.tsIndexs,
            this.tsPrimaryKeys,
            this.tsTriggers});
            this.tsDesignTable.Location = new System.Drawing.Point(0, 73);
            this.tsDesignTable.Name = "tsDesignTable";
            this.tsDesignTable.Padding = new System.Windows.Forms.Padding(0);
            this.tsDesignTable.Size = new System.Drawing.Size(1125, 25);
            this.tsDesignTable.TabIndex = 33;
            this.tsDesignTable.Text = "toolStrip1";
            // 
            // tsDesignTableBlank
            // 
            this.tsDesignTableBlank.AutoSize = false;
            this.tsDesignTableBlank.Name = "tsDesignTableBlank";
            this.tsDesignTableBlank.Size = new System.Drawing.Size(261, 22);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // tsFields
            // 
            this.tsFields.Name = "tsFields";
            this.tsFields.Size = new System.Drawing.Size(54, 22);
            this.tsFields.Text = "栏位数:0";
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(6, 25);
            // 
            // tsIndexs
            // 
            this.tsIndexs.Name = "tsIndexs";
            this.tsIndexs.Size = new System.Drawing.Size(54, 22);
            this.tsIndexs.Text = "索引数:0";
            // 
            // tsPrimaryKeys
            // 
            this.tsPrimaryKeys.Name = "tsPrimaryKeys";
            this.tsPrimaryKeys.Size = new System.Drawing.Size(54, 22);
            this.tsPrimaryKeys.Text = "外键数:0";
            // 
            // tsTriggers
            // 
            this.tsTriggers.Name = "tsTriggers";
            this.tsTriggers.Size = new System.Drawing.Size(66, 22);
            this.tsTriggers.Text = "触发器数:0";
            // 
            // tsNewSelect
            // 
            this.tsNewSelect.BackColor = System.Drawing.SystemColors.Control;
            this.tsNewSelect.CanOverflow = false;
            this.tsNewSelect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsNewSelect.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsNewSelect.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsNewSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNewSelectBlank,
            this.toolStripSeparator4,
            this.tsNewSelectStmt,
            this.tsNewSelectPageInfo,
            this.toolStripSeparator5,
            this.tsNewSelectTime,
            this.toolStripSeparator7,
            this.toolStripLabel5,
            this.toolStripSeparator10});
            this.tsNewSelect.Location = new System.Drawing.Point(0, 48);
            this.tsNewSelect.Name = "tsNewSelect";
            this.tsNewSelect.Padding = new System.Windows.Forms.Padding(0);
            this.tsNewSelect.Size = new System.Drawing.Size(1125, 25);
            this.tsNewSelect.TabIndex = 31;
            this.tsNewSelect.Text = "toolStrip1";
            // 
            // tsNewSelectBlank
            // 
            this.tsNewSelectBlank.AutoSize = false;
            this.tsNewSelectBlank.Name = "tsNewSelectBlank";
            this.tsNewSelectBlank.Size = new System.Drawing.Size(261, 22);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsNewSelectStmt
            // 
            this.tsNewSelectStmt.AutoSize = false;
            this.tsNewSelectStmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tsNewSelectStmt.Name = "tsNewSelectStmt";
            this.tsNewSelectStmt.ReadOnly = true;
            this.tsNewSelectStmt.Size = new System.Drawing.Size(400, 23);
            this.tsNewSelectStmt.Text = "自动完成代码就绪";
            this.tsNewSelectStmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsNewSelectPageInfo
            // 
            this.tsNewSelectPageInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsNewSelectPageInfo.AutoSize = false;
            this.tsNewSelectPageInfo.Name = "tsNewSelectPageInfo";
            this.tsNewSelectPageInfo.Size = new System.Drawing.Size(250, 22);
            this.tsNewSelectPageInfo.Text = "第0条记录(共0条)";
            this.tsNewSelectPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsNewSelectTime
            // 
            this.tsNewSelectTime.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsNewSelectTime.AutoSize = false;
            this.tsNewSelectTime.Name = "tsNewSelectTime";
            this.tsNewSelectTime.Size = new System.Drawing.Size(120, 22);
            this.tsNewSelectTime.Text = "查询时间:0.000s";
            this.tsNewSelectTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel5.Text = "只读";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 25);
            // 
            // tsOpenTable
            // 
            this.tsOpenTable.BackColor = System.Drawing.SystemColors.Control;
            this.tsOpenTable.CanOverflow = false;
            this.tsOpenTable.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsOpenTable.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsOpenTable.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsOpenTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOpenTableBlank,
            this.toolStripSeparator6,
            this.tsOpenTableStmt,
            this.tsOpenTablePageInfo,
            this.toolStripSeparator30,
            this.tsOpenTableWarn,
            this.toolStripSeparator11});
            this.tsOpenTable.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsOpenTable.Location = new System.Drawing.Point(0, 23);
            this.tsOpenTable.Name = "tsOpenTable";
            this.tsOpenTable.Padding = new System.Windows.Forms.Padding(0);
            this.tsOpenTable.Size = new System.Drawing.Size(1125, 25);
            this.tsOpenTable.TabIndex = 29;
            this.tsOpenTable.Text = "toolStrip1";
            // 
            // tsOpenTableBlank
            // 
            this.tsOpenTableBlank.AutoSize = false;
            this.tsOpenTableBlank.Name = "tsOpenTableBlank";
            this.tsOpenTableBlank.Size = new System.Drawing.Size(261, 22);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsOpenTableStmt
            // 
            this.tsOpenTableStmt.AutoSize = false;
            this.tsOpenTableStmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tsOpenTableStmt.Name = "tsOpenTableStmt";
            this.tsOpenTableStmt.ReadOnly = true;
            this.tsOpenTableStmt.Size = new System.Drawing.Size(400, 25);
            this.tsOpenTableStmt.Text = "SELEC * FROM TABLE";
            this.tsOpenTableStmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsOpenTablePageInfo
            // 
            this.tsOpenTablePageInfo.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsOpenTablePageInfo.AutoSize = false;
            this.tsOpenTablePageInfo.Name = "tsOpenTablePageInfo";
            this.tsOpenTablePageInfo.Size = new System.Drawing.Size(250, 22);
            this.tsOpenTablePageInfo.Text = "第0条记录(共0条)于第0页";
            this.tsOpenTablePageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator30
            // 
            this.toolStripSeparator30.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator30.Name = "toolStripSeparator30";
            this.toolStripSeparator30.Size = new System.Drawing.Size(6, 25);
            // 
            // tsOpenTableWarn
            // 
            this.tsOpenTableWarn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsOpenTableWarn.AutoSize = false;
            this.tsOpenTableWarn.Name = "tsOpenTableWarn";
            this.tsOpenTableWarn.Size = new System.Drawing.Size(50, 22);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 25);
            // 
            // spStatusBar
            // 
            this.spStatusBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spStatusBar.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spStatusBar.IsSplitterFixed = true;
            this.spStatusBar.Location = new System.Drawing.Point(0, 105);
            this.spStatusBar.Margin = new System.Windows.Forms.Padding(0);
            this.spStatusBar.Name = "spStatusBar";
            this.spStatusBar.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spStatusBar.Panel1
            // 
            this.spStatusBar.Panel1.Controls.Add(this.spMain);
            // 
            // spStatusBar.Panel2
            // 
            this.spStatusBar.Panel2.Controls.Add(this.tsOpenTable);
            this.spStatusBar.Panel2.Controls.Add(this.tsNewSelect);
            this.spStatusBar.Panel2.Controls.Add(this.tsDesignTable);
            this.spStatusBar.Panel2.Controls.Add(this.tsObject);
            this.spStatusBar.Size = new System.Drawing.Size(1125, 524);
            this.spStatusBar.SplitterDistance = 400;
            this.spStatusBar.SplitterWidth = 1;
            this.spStatusBar.TabIndex = 32;
            // 
            // spMain
            // 
            this.spMain.BackColor = System.Drawing.SystemColors.Control;
            this.spMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spMain.Location = new System.Drawing.Point(0, 0);
            this.spMain.Margin = new System.Windows.Forms.Padding(0);
            this.spMain.Name = "spMain";
            // 
            // spMain.Panel1
            // 
            this.spMain.Panel1.Controls.Add(this.tvMain);
            // 
            // spMain.Panel2
            // 
            this.spMain.Panel2.Controls.Add(this.tcMain);
            this.spMain.Size = new System.Drawing.Size(1125, 400);
            this.spMain.SplitterDistance = 260;
            this.spMain.SplitterWidth = 3;
            this.spMain.TabIndex = 5;
            this.spMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.spMain_SplitterMoved);
            // 
            // cmsTableGroup
            // 
            this.cmsTableGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGroupNewTable,
            this.toolStripSeparator1,
            this.tsmiImportTableGroup,
            this.tsmiExportTableGroup,
            this.toolStripSeparator2,
            this.tsmiRefreshTableGroup});
            this.cmsTableGroup.Name = "cmsTableGroup";
            this.cmsTableGroup.Size = new System.Drawing.Size(125, 104);
            // 
            // tsmiGroupNewTable
            // 
            this.tsmiGroupNewTable.Image = global::SQLClient.Properties.Resources.new_table_16;
            this.tsmiGroupNewTable.Name = "tsmiGroupNewTable";
            this.tsmiGroupNewTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiGroupNewTable.Text = "新建表";
            this.tsmiGroupNewTable.Click += new System.EventHandler(this.tsmiGroupNewTable_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiImportTableGroup
            // 
            this.tsmiImportTableGroup.Image = global::SQLClient.Properties.Resources.import_table_16;
            this.tsmiImportTableGroup.Name = "tsmiImportTableGroup";
            this.tsmiImportTableGroup.Size = new System.Drawing.Size(124, 22);
            this.tsmiImportTableGroup.Text = "导入向导";
            // 
            // tsmiExportTableGroup
            // 
            this.tsmiExportTableGroup.Image = global::SQLClient.Properties.Resources.export_table_16;
            this.tsmiExportTableGroup.Name = "tsmiExportTableGroup";
            this.tsmiExportTableGroup.Size = new System.Drawing.Size(124, 22);
            this.tsmiExportTableGroup.Text = "导出向导";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiRefreshTableGroup
            // 
            this.tsmiRefreshTableGroup.Name = "tsmiRefreshTableGroup";
            this.tsmiRefreshTableGroup.Size = new System.Drawing.Size(124, 22);
            this.tsmiRefreshTableGroup.Text = "刷新";
            // 
            // cmsViewGroup
            // 
            this.cmsViewGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGroupNewView,
            this.toolStripSeparator3,
            this.tsmiGroupExprotView,
            this.toolStripSeparator17,
            this.tsmiRefreshViewGruop});
            this.cmsViewGroup.Name = "contextMenuStrip1";
            this.cmsViewGroup.Size = new System.Drawing.Size(125, 82);
            // 
            // tsmiGroupNewView
            // 
            this.tsmiGroupNewView.Image = global::SQLClient.Properties.Resources.new_view_16;
            this.tsmiGroupNewView.Name = "tsmiGroupNewView";
            this.tsmiGroupNewView.Size = new System.Drawing.Size(124, 22);
            this.tsmiGroupNewView.Text = "新建视图";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiGroupExprotView
            // 
            this.tsmiGroupExprotView.Image = global::SQLClient.Properties.Resources.export_view_16;
            this.tsmiGroupExprotView.Name = "tsmiGroupExprotView";
            this.tsmiGroupExprotView.Size = new System.Drawing.Size(124, 22);
            this.tsmiGroupExprotView.Text = "导出向导";
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiRefreshViewGruop
            // 
            this.tsmiRefreshViewGruop.Name = "tsmiRefreshViewGruop";
            this.tsmiRefreshViewGruop.Size = new System.Drawing.Size(124, 22);
            this.tsmiRefreshViewGruop.Text = "刷新";
            // 
            // cmsSelectGroup
            // 
            this.cmsSelectGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGrupNewSelect,
            this.toolStripSeparator18,
            this.tsmiRefreshSelectGroup});
            this.cmsSelectGroup.Name = "cmsSelectGroup";
            this.cmsSelectGroup.Size = new System.Drawing.Size(125, 54);
            // 
            // tsmiGrupNewSelect
            // 
            this.tsmiGrupNewSelect.Image = global::SQLClient.Properties.Resources.new_select_16;
            this.tsmiGrupNewSelect.Name = "tsmiGrupNewSelect";
            this.tsmiGrupNewSelect.Size = new System.Drawing.Size(124, 22);
            this.tsmiGrupNewSelect.Text = "新建查询";
            this.tsmiGrupNewSelect.Click += new System.EventHandler(this.tsmiGrupNewSelect_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiRefreshSelectGroup
            // 
            this.tsmiRefreshSelectGroup.Name = "tsmiRefreshSelectGroup";
            this.tsmiRefreshSelectGroup.Size = new System.Drawing.Size(124, 22);
            this.tsmiRefreshSelectGroup.Text = "刷新";
            // 
            // cmsConnect
            // 
            this.cmsConnect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenConnect,
            this.tsmiCloseConnect,
            this.toolStripSeparator22,
            this.tsmiNewConnect,
            this.tsmiDeleteConnect,
            this.toolStripSeparator23,
            this.tsmiRefreshConnect,
            this.tsmiConnectProperty});
            this.cmsConnect.Name = "cmsConnect";
            this.cmsConnect.Size = new System.Drawing.Size(125, 148);
            // 
            // tsmiOpenConnect
            // 
            this.tsmiOpenConnect.Name = "tsmiOpenConnect";
            this.tsmiOpenConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiOpenConnect.Text = "打开连接";
            this.tsmiOpenConnect.Click += new System.EventHandler(this.tsmiOpenConnect_Click);
            // 
            // tsmiCloseConnect
            // 
            this.tsmiCloseConnect.Name = "tsmiCloseConnect";
            this.tsmiCloseConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiCloseConnect.Text = "关闭连接";
            this.tsmiCloseConnect.Click += new System.EventHandler(this.tsmiCloseConnect_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiNewConnect
            // 
            this.tsmiNewConnect.Name = "tsmiNewConnect";
            this.tsmiNewConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiNewConnect.Text = "新建连接";
            this.tsmiNewConnect.Click += new System.EventHandler(this.tsbtnConnect_Click);
            // 
            // tsmiDeleteConnect
            // 
            this.tsmiDeleteConnect.Name = "tsmiDeleteConnect";
            this.tsmiDeleteConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteConnect.Text = "删除连接";
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiRefreshConnect
            // 
            this.tsmiRefreshConnect.Enabled = false;
            this.tsmiRefreshConnect.Name = "tsmiRefreshConnect";
            this.tsmiRefreshConnect.Size = new System.Drawing.Size(124, 22);
            this.tsmiRefreshConnect.Text = "刷新";
            // 
            // tsmiConnectProperty
            // 
            this.tsmiConnectProperty.Name = "tsmiConnectProperty";
            this.tsmiConnectProperty.Size = new System.Drawing.Size(124, 22);
            this.tsmiConnectProperty.Text = "连接信息";
            this.tsmiConnectProperty.Click += new System.EventHandler(this.tsmiConnectProperty_Click);
            // 
            // cmsDatabase
            // 
            this.cmsDatabase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenDatabase,
            this.tsmiCloseDatabase,
            this.toolStripSeparator19,
            this.tsmiDatabaseNewTable,
            this.tsmiDatabaseNewView,
            this.tsmiDatabaseNewSelect,
            this.toolStripSeparator20,
            this.tsmiNewDatabase,
            this.tsmiDeleteDatabase,
            this.toolStripSeparator21,
            this.tsmiRefreshDatabase});
            this.cmsDatabase.Name = "cmsDatabase";
            this.cmsDatabase.Size = new System.Drawing.Size(137, 198);
            // 
            // tsmiOpenDatabase
            // 
            this.tsmiOpenDatabase.Name = "tsmiOpenDatabase";
            this.tsmiOpenDatabase.Size = new System.Drawing.Size(136, 22);
            this.tsmiOpenDatabase.Text = "打开数据库";
            this.tsmiOpenDatabase.Click += new System.EventHandler(this.tsmiOpenDatabase_Click);
            // 
            // tsmiCloseDatabase
            // 
            this.tsmiCloseDatabase.Name = "tsmiCloseDatabase";
            this.tsmiCloseDatabase.Size = new System.Drawing.Size(136, 22);
            this.tsmiCloseDatabase.Text = "关闭数据库";
            this.tsmiCloseDatabase.Click += new System.EventHandler(this.tsmiCloseDatabase_Click);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(133, 6);
            // 
            // tsmiDatabaseNewTable
            // 
            this.tsmiDatabaseNewTable.Image = global::SQLClient.Properties.Resources.new_table_16;
            this.tsmiDatabaseNewTable.Name = "tsmiDatabaseNewTable";
            this.tsmiDatabaseNewTable.Size = new System.Drawing.Size(136, 22);
            this.tsmiDatabaseNewTable.Text = "新建表";
            this.tsmiDatabaseNewTable.Click += new System.EventHandler(this.tsmiDatabaseNewTable_Click);
            // 
            // tsmiDatabaseNewView
            // 
            this.tsmiDatabaseNewView.Image = global::SQLClient.Properties.Resources.new_view_16;
            this.tsmiDatabaseNewView.Name = "tsmiDatabaseNewView";
            this.tsmiDatabaseNewView.Size = new System.Drawing.Size(136, 22);
            this.tsmiDatabaseNewView.Text = "新建视图";
            this.tsmiDatabaseNewView.Click += new System.EventHandler(this.tsmiDatabaseNewView_Click);
            // 
            // tsmiDatabaseNewSelect
            // 
            this.tsmiDatabaseNewSelect.Image = global::SQLClient.Properties.Resources.new_select_16;
            this.tsmiDatabaseNewSelect.Name = "tsmiDatabaseNewSelect";
            this.tsmiDatabaseNewSelect.Size = new System.Drawing.Size(136, 22);
            this.tsmiDatabaseNewSelect.Text = "新建查询";
            this.tsmiDatabaseNewSelect.Click += new System.EventHandler(this.tsmiDatabaseNewSelect_Click);
            // 
            // toolStripSeparator20
            // 
            this.toolStripSeparator20.Name = "toolStripSeparator20";
            this.toolStripSeparator20.Size = new System.Drawing.Size(133, 6);
            // 
            // tsmiNewDatabase
            // 
            this.tsmiNewDatabase.Name = "tsmiNewDatabase";
            this.tsmiNewDatabase.Size = new System.Drawing.Size(136, 22);
            this.tsmiNewDatabase.Text = "新建数据库";
            // 
            // tsmiDeleteDatabase
            // 
            this.tsmiDeleteDatabase.Name = "tsmiDeleteDatabase";
            this.tsmiDeleteDatabase.Size = new System.Drawing.Size(136, 22);
            this.tsmiDeleteDatabase.Text = "删除数据库";
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(133, 6);
            // 
            // tsmiRefreshDatabase
            // 
            this.tsmiRefreshDatabase.Name = "tsmiRefreshDatabase";
            this.tsmiRefreshDatabase.Size = new System.Drawing.Size(136, 22);
            this.tsmiRefreshDatabase.Text = "刷新";
            // 
            // cmsTable
            // 
            this.cmsTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenTable,
            this.tsmiDesignTable,
            this.tsmiTableNewTable,
            this.tsmiDeleteTable,
            this.toolStripSeparator24,
            this.tsmiImportTable,
            this.tsmiExportTable,
            this.toolStripSeparator25,
            this.tsmiRefreshTable});
            this.cmsTable.Name = "cmsTable";
            this.cmsTable.Size = new System.Drawing.Size(125, 170);
            // 
            // tsmiOpenTable
            // 
            this.tsmiOpenTable.Image = global::SQLClient.Properties.Resources.open_table_16;
            this.tsmiOpenTable.Name = "tsmiOpenTable";
            this.tsmiOpenTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiOpenTable.Text = "打开表";
            this.tsmiOpenTable.Click += new System.EventHandler(this.tsmiOpenTable_Click);
            // 
            // tsmiDesignTable
            // 
            this.tsmiDesignTable.Image = global::SQLClient.Properties.Resources.design_table_16;
            this.tsmiDesignTable.Name = "tsmiDesignTable";
            this.tsmiDesignTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiDesignTable.Text = "设计表";
            this.tsmiDesignTable.Click += new System.EventHandler(this.tsmiDesignTable_Click);
            // 
            // tsmiTableNewTable
            // 
            this.tsmiTableNewTable.Image = global::SQLClient.Properties.Resources.new_table_16;
            this.tsmiTableNewTable.Name = "tsmiTableNewTable";
            this.tsmiTableNewTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiTableNewTable.Text = "新建表";
            this.tsmiTableNewTable.Click += new System.EventHandler(this.tsmiTableNewTable_Click);
            // 
            // tsmiDeleteTable
            // 
            this.tsmiDeleteTable.Image = global::SQLClient.Properties.Resources.delete_table_16;
            this.tsmiDeleteTable.Name = "tsmiDeleteTable";
            this.tsmiDeleteTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteTable.Text = "删除表";
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiImportTable
            // 
            this.tsmiImportTable.Image = global::SQLClient.Properties.Resources.import_table_16;
            this.tsmiImportTable.Name = "tsmiImportTable";
            this.tsmiImportTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiImportTable.Text = "导入向导";
            // 
            // tsmiExportTable
            // 
            this.tsmiExportTable.Image = global::SQLClient.Properties.Resources.export_table_16;
            this.tsmiExportTable.Name = "tsmiExportTable";
            this.tsmiExportTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiExportTable.Text = "导出向导";
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiRefreshTable
            // 
            this.tsmiRefreshTable.Name = "tsmiRefreshTable";
            this.tsmiRefreshTable.Size = new System.Drawing.Size(124, 22);
            this.tsmiRefreshTable.Text = "刷新";
            // 
            // cmsView
            // 
            this.cmsView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenView,
            this.tsmiDesignView,
            this.tsmiViewNewView,
            this.tsmiDeleteView,
            this.toolStripSeparator26,
            this.tsmiExportView,
            this.toolStripSeparator27,
            this.tsmiRefreshView});
            this.cmsView.Name = "cmsView";
            this.cmsView.Size = new System.Drawing.Size(125, 148);
            // 
            // tsmiOpenView
            // 
            this.tsmiOpenView.Image = global::SQLClient.Properties.Resources.open_view_16;
            this.tsmiOpenView.Name = "tsmiOpenView";
            this.tsmiOpenView.Size = new System.Drawing.Size(124, 22);
            this.tsmiOpenView.Text = "打开视图";
            this.tsmiOpenView.Click += new System.EventHandler(this.tsmiOpenView_Click);
            // 
            // tsmiDesignView
            // 
            this.tsmiDesignView.Image = global::SQLClient.Properties.Resources.design_view_16;
            this.tsmiDesignView.Name = "tsmiDesignView";
            this.tsmiDesignView.Size = new System.Drawing.Size(124, 22);
            this.tsmiDesignView.Text = "视计视图";
            // 
            // tsmiViewNewView
            // 
            this.tsmiViewNewView.Image = global::SQLClient.Properties.Resources.new_view_16;
            this.tsmiViewNewView.Name = "tsmiViewNewView";
            this.tsmiViewNewView.Size = new System.Drawing.Size(124, 22);
            this.tsmiViewNewView.Text = "新建视图";
            // 
            // tsmiDeleteView
            // 
            this.tsmiDeleteView.Image = global::SQLClient.Properties.Resources.delete_view_16;
            this.tsmiDeleteView.Name = "tsmiDeleteView";
            this.tsmiDeleteView.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteView.Text = "删除视图";
            // 
            // toolStripSeparator26
            // 
            this.toolStripSeparator26.Name = "toolStripSeparator26";
            this.toolStripSeparator26.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiExportView
            // 
            this.tsmiExportView.Image = global::SQLClient.Properties.Resources.export_view_16;
            this.tsmiExportView.Name = "tsmiExportView";
            this.tsmiExportView.Size = new System.Drawing.Size(124, 22);
            this.tsmiExportView.Text = "导出视图";
            // 
            // toolStripSeparator27
            // 
            this.toolStripSeparator27.Name = "toolStripSeparator27";
            this.toolStripSeparator27.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiRefreshView
            // 
            this.tsmiRefreshView.Name = "tsmiRefreshView";
            this.tsmiRefreshView.Size = new System.Drawing.Size(124, 22);
            this.tsmiRefreshView.Text = "刷新";
            // 
            // cmsSelect
            // 
            this.cmsSelect.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenSelect,
            this.tsmiDesignSelect,
            this.tsmiNewSelect,
            this.tsmiDeleteSelect,
            this.toolStripSeparator28,
            this.tsmiExprotSelect,
            this.toolStripSeparator29,
            this.tsmiRefreshSelect});
            this.cmsSelect.Name = "cmsSelect";
            this.cmsSelect.Size = new System.Drawing.Size(125, 148);
            // 
            // tsmiOpenSelect
            // 
            this.tsmiOpenSelect.Image = global::SQLClient.Properties.Resources.open_select_16;
            this.tsmiOpenSelect.Name = "tsmiOpenSelect";
            this.tsmiOpenSelect.Size = new System.Drawing.Size(124, 22);
            this.tsmiOpenSelect.Text = "打开查询";
            this.tsmiOpenSelect.Click += new System.EventHandler(this.tsmiOpenSelect_Click);
            // 
            // tsmiDesignSelect
            // 
            this.tsmiDesignSelect.Image = global::SQLClient.Properties.Resources.design_select_16;
            this.tsmiDesignSelect.Name = "tsmiDesignSelect";
            this.tsmiDesignSelect.Size = new System.Drawing.Size(124, 22);
            this.tsmiDesignSelect.Text = "设计查询";
            // 
            // tsmiNewSelect
            // 
            this.tsmiNewSelect.Image = global::SQLClient.Properties.Resources.new_select_16;
            this.tsmiNewSelect.Name = "tsmiNewSelect";
            this.tsmiNewSelect.Size = new System.Drawing.Size(124, 22);
            this.tsmiNewSelect.Text = "新建查询";
            this.tsmiNewSelect.Click += new System.EventHandler(this.tsmiNewSelect_Click);
            // 
            // tsmiDeleteSelect
            // 
            this.tsmiDeleteSelect.Image = global::SQLClient.Properties.Resources.delete_select_16;
            this.tsmiDeleteSelect.Name = "tsmiDeleteSelect";
            this.tsmiDeleteSelect.Size = new System.Drawing.Size(124, 22);
            this.tsmiDeleteSelect.Text = "删除查询";
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiExprotSelect
            // 
            this.tsmiExprotSelect.Image = global::SQLClient.Properties.Resources.export_select_16;
            this.tsmiExprotSelect.Name = "tsmiExprotSelect";
            this.tsmiExprotSelect.Size = new System.Drawing.Size(124, 22);
            this.tsmiExprotSelect.Text = "导出查询";
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(121, 6);
            // 
            // tsmiRefreshSelect
            // 
            this.tsmiRefreshSelect.Name = "tsmiRefreshSelect";
            this.tsmiRefreshSelect.Size = new System.Drawing.Size(124, 22);
            this.tsmiRefreshSelect.Text = "刷新";
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(152, 22);
            this.tsmiHelp.Text = "帮助";
            // 
            // tsmiCheckUpdate
            // 
            this.tsmiCheckUpdate.Name = "tsmiCheckUpdate";
            this.tsmiCheckUpdate.Size = new System.Drawing.Size(152, 22);
            this.tsmiCheckUpdate.Text = "检查更新";
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(149, 6);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(152, 22);
            this.tsmiAbout.Text = "关于";
            // 
            // SqlClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 629);
            this.Controls.Add(this.spStatusBar);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.tsSystemMain);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "SqlClientForm";
            this.Text = "SQLClient";
            this.Load += new System.EventHandler(this.SQLClientForm_Load);
            this.SizeChanged += new System.EventHandler(this.SQLClientForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.tsSystemMain.ResumeLayout(false);
            this.tsSystemMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsObject.ResumeLayout(false);
            this.tsObject.PerformLayout();
            this.tsDesignTable.ResumeLayout(false);
            this.tsDesignTable.PerformLayout();
            this.tsNewSelect.ResumeLayout(false);
            this.tsNewSelect.PerformLayout();
            this.tsOpenTable.ResumeLayout(false);
            this.tsOpenTable.PerformLayout();
            this.spStatusBar.Panel1.ResumeLayout(false);
            this.spStatusBar.Panel2.ResumeLayout(false);
            this.spStatusBar.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spStatusBar)).EndInit();
            this.spStatusBar.ResumeLayout(false);
            this.spMain.Panel1.ResumeLayout(false);
            this.spMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spMain)).EndInit();
            this.spMain.ResumeLayout(false);
            this.cmsTableGroup.ResumeLayout(false);
            this.cmsViewGroup.ResumeLayout(false);
            this.cmsSelectGroup.ResumeLayout(false);
            this.cmsConnect.ResumeLayout(false);
            this.cmsDatabase.ResumeLayout(false);
            this.cmsTable.ResumeLayout(false);
            this.cmsView.ResumeLayout(false);
            this.cmsSelect.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraTab.XtraTabControl tcMain;
        private DevExpress.XtraTab.XtraTabPage tpObject;
        private System.Windows.Forms.TreeView tvMain;
        private System.Windows.Forms.ImageList imgListTreeView;
        private System.Windows.Forms.ImageList imgListListView;
        private System.Windows.Forms.ToolStrip tsSystemMain;
        private System.Windows.Forms.ToolStripButton tsbtnUser;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbtnTable;
        private System.Windows.Forms.ToolStripButton tsbtnView;
        private System.Windows.Forms.ToolStripButton tsbtnSelect;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuOpenConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiMenuCloseConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiView;
        private System.Windows.Forms.ToolStripMenuItem tsmiStore;
        private System.Windows.Forms.ToolStripMenuItem tsmiTool;
        private System.Windows.Forms.ToolStripMenuItem tsmiCommand;
        private System.Windows.Forms.ToolStripMenuItem tsmiOption;
        private System.Windows.Forms.ToolStripMenuItem tsmiWindow;
        private System.Windows.Forms.ToolStripMenuItem tsmiDropDownHelp;
        private System.Windows.Forms.ToolStrip tsOpenTable;
        private System.Windows.Forms.ToolStrip tsNewSelect;
        private System.Windows.Forms.ToolStripLabel tsNewSelectBlank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel tsOpenTableBlank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStrip tsObject;
        private System.Windows.Forms.ToolStripLabel tsNewSelectPageInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel tsNewSelectTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripLabel tsOpenTablePageInfo;
        private System.Windows.Forms.ToolStripLabel tsObjectBlank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripLabel tsObjectOwner;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStrip tsDesignTable;
        private System.Windows.Forms.ToolStripLabel tsDesignTableBlank;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripLabel tsFields;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripLabel tsIndexs;
        private System.Windows.Forms.ToolStripLabel tsPrimaryKeys;
        private System.Windows.Forms.ToolStripLabel tsTriggers;
        private System.Windows.Forms.SplitContainer spStatusBar;
        private System.Windows.Forms.SplitContainer spMain;
        private System.Windows.Forms.ToolStripButton tsbtnConnect;
        private System.Windows.Forms.ContextMenuStrip cmsTableGroup;
        private System.Windows.Forms.ContextMenuStrip cmsViewGroup;
        private System.Windows.Forms.ContextMenuStrip cmsSelectGroup;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupNewTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportTableGroup;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportTableGroup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshTableGroup;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupNewView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiGroupExprotView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshViewGruop;
        private System.Windows.Forms.ToolStripMenuItem tsmiGrupNewSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshSelectGroup;
        private System.Windows.Forms.ContextMenuStrip cmsConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiConnectProperty;
        private System.Windows.Forms.ContextMenuStrip cmsDatabase;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenDatabase;
        private System.Windows.Forms.ToolStripMenuItem tsmiCloseDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripMenuItem tsmiDatabaseNewSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator20;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewDatabase;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshDatabase;
        private System.Windows.Forms.ContextMenuStrip cmsTable;
        private System.Windows.Forms.ContextMenuStrip cmsView;
        private System.Windows.Forms.ContextMenuStrip cmsSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenTable;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesignTable;
        private System.Windows.Forms.ToolStripMenuItem tsmiTableNewTable;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripMenuItem tsmiImportTable;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportTable;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshTable;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenView;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesignView;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewNewView;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator26;
        private System.Windows.Forms.ToolStripMenuItem tsmiExportView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator27;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshView;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiDesignSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewSelect;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripMenuItem tsmiExprotSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator29;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefreshSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator30;
        private System.Windows.Forms.ToolStripLabel tsOpenTableWarn;
        private System.Windows.Forms.ToolStripTextBox tsOpenTableStmt;
        private System.Windows.Forms.ToolStripMenuItem tsmiDatabaseNewTable;
        private System.Windows.Forms.ToolStripMenuItem tsmiDatabaseNewView;
        private System.Windows.Forms.ToolStripTextBox tsNewSelectStmt;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiCheckUpdate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
    }
}

