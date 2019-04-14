namespace SQLClient
{
    partial class NewConnectionFrom
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.accDataSource = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.btnUpLoad = new DevExpress.XtraEditors.SimpleButton();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtConnectName = new System.Windows.Forms.TextBox();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.btnAdvance = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accDataSource)).BeginInit();
            this.accDataSource.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(10, 10);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.separatorControl1);
            this.splitContainer1.Panel2.Controls.Add(this.btnAdvance);
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Size = new System.Drawing.Size(620, 504);
            this.splitContainer1.SplitterDistance = 448;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 21;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.accDataSource);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer2.Panel2.Controls.Add(this.groupBox7);
            this.splitContainer2.Panel2.Controls.Add(this.separatorControl2);
            this.splitContainer2.Size = new System.Drawing.Size(620, 448);
            this.splitContainer2.SplitterDistance = 277;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // accDataSource
            // 
            this.accDataSource.AllowItemSelection = true;
            this.accDataSource.Controls.Add(this.btnUpLoad);
            this.accDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accDataSource.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accDataSource.ExpandGroupOnHeaderClick = false;
            this.accDataSource.Location = new System.Drawing.Point(0, 0);
            this.accDataSource.Name = "accDataSource";
            this.accDataSource.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.accDataSource.ShowGroupExpandButtons = false;
            this.accDataSource.Size = new System.Drawing.Size(277, 448);
            this.accDataSource.TabIndex = 17;
            this.accDataSource.Text = "accordionControl1";
            this.accDataSource.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.accDataSource_SelectedElementChanged);
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUpLoad.Image = global::SQLClient.Properties.Resources.add_datasource_16;
            this.btnUpLoad.Location = new System.Drawing.Point(222, 10);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.False;
            this.btnUpLoad.Size = new System.Drawing.Size(26, 20);
            this.btnUpLoad.TabIndex = 20;
            this.btnUpLoad.ToolTip = "添加新的数据源";
            this.btnUpLoad.Click += new System.EventHandler(this.btnUpLoad_Click);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.HeaderControl = this.btnUpLoad;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "数据源";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(18, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 357);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "驱动属性";
            // 
            // pg
            // 
            this.pg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pg.Location = new System.Drawing.Point(3, 17);
            this.pg.Name = "pg";
            this.pg.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.pg.Size = new System.Drawing.Size(318, 337);
            this.pg.TabIndex = 19;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtConnectionString);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(18, 51);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 40);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "输入连接字符串";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectionString.Location = new System.Drawing.Point(3, 17);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(318, 21);
            this.txtConnectionString.TabIndex = 1;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtConnectName);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox7.Location = new System.Drawing.Point(18, 0);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(324, 51);
            this.groupBox7.TabIndex = 18;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "连接名称";
            // 
            // txtConnectName
            // 
            this.txtConnectName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectName.Location = new System.Drawing.Point(3, 17);
            this.txtConnectName.Name = "txtConnectName";
            this.txtConnectName.Size = new System.Drawing.Size(318, 21);
            this.txtConnectName.TabIndex = 0;
            this.txtConnectName.Text = "mysql_test";
            // 
            // separatorControl2
            // 
            this.separatorControl2.AutoSizeMode = true;
            this.separatorControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.separatorControl2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl2.Location = new System.Drawing.Point(0, 0);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(8);
            this.separatorControl2.Size = new System.Drawing.Size(18, 448);
            this.separatorControl2.TabIndex = 16;
            // 
            // separatorControl1
            // 
            this.separatorControl1.AutoSizeMode = true;
            this.separatorControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.separatorControl1.Location = new System.Drawing.Point(0, 0);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(8);
            this.separatorControl1.Size = new System.Drawing.Size(620, 18);
            this.separatorControl1.TabIndex = 15;
            // 
            // btnAdvance
            // 
            this.btnAdvance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdvance.Enabled = false;
            this.btnAdvance.Location = new System.Drawing.Point(530, 21);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(68, 20);
            this.btnAdvance.TabIndex = 14;
            this.btnAdvance.Text = "高级";
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(352, 21);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 20);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "添加";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(441, 21);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 20);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            // 
            // NewConnectionFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 524);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewConnectionFrom";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建连接";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewConnectionFrom_FormClosing);
            this.Load += new System.EventHandler(this.NewConnectionFrom_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accDataSource)).EndInit();
            this.accDataSource.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraBars.Navigation.AccordionControl accDataSource;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraEditors.SimpleButton btnAdvance;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtConnectName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private System.Windows.Forms.PropertyGrid pg;
        private DevExpress.XtraEditors.SimpleButton btnUpLoad;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}