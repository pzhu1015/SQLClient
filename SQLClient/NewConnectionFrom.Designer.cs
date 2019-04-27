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
            this.tsTool = new System.Windows.Forms.ToolStrip();
            this.tsbtnAddDriver = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDeleteDriver = new System.Windows.Forms.ToolStripButton();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pg = new System.Windows.Forms.PropertyGrid();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtConnectionString = new DevExpress.XtraEditors.ButtonEdit();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtConnectName = new System.Windows.Forms.TextBox();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
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
            this.tsTool.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtConnectionString.Properties)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.btnOK);
            this.splitContainer1.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer1.Size = new System.Drawing.Size(620, 466);
            this.splitContainer1.SplitterDistance = 413;
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
            this.splitContainer2.Size = new System.Drawing.Size(620, 413);
            this.splitContainer2.SplitterDistance = 277;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // accDataSource
            // 
            this.accDataSource.AllowItemSelection = true;
            this.accDataSource.Controls.Add(this.tsTool);
            this.accDataSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accDataSource.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accDataSource.ExpandGroupOnHeaderClick = false;
            this.accDataSource.Location = new System.Drawing.Point(0, 0);
            this.accDataSource.Name = "accDataSource";
            this.accDataSource.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.accDataSource.ShowGroupExpandButtons = false;
            this.accDataSource.Size = new System.Drawing.Size(277, 413);
            this.accDataSource.TabIndex = 17;
            this.accDataSource.Text = "accordionControl1";
            this.accDataSource.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.accDataSource_SelectedElementChanged);
            // 
            // tsTool
            // 
            this.tsTool.BackColor = System.Drawing.Color.Transparent;
            this.tsTool.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tsTool.CanOverflow = false;
            this.tsTool.Dock = System.Windows.Forms.DockStyle.None;
            this.tsTool.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAddDriver,
            this.tsbtnDeleteDriver});
            this.tsTool.Location = new System.Drawing.Point(199, 7);
            this.tsTool.Name = "tsTool";
            this.tsTool.Size = new System.Drawing.Size(49, 25);
            this.tsTool.TabIndex = 16;
            this.tsTool.Text = "toolStrip1";
            // 
            // tsbtnAddDriver
            // 
            this.tsbtnAddDriver.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnAddDriver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddDriver.Image = global::SQLClient.Properties.Resources.add_datasource_16;
            this.tsbtnAddDriver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddDriver.Margin = new System.Windows.Forms.Padding(0);
            this.tsbtnAddDriver.Name = "tsbtnAddDriver";
            this.tsbtnAddDriver.Size = new System.Drawing.Size(23, 25);
            this.tsbtnAddDriver.Text = "添加驱动";
            this.tsbtnAddDriver.Click += new System.EventHandler(this.tsbtnAddDriver_Click);
            // 
            // tsbtnDeleteDriver
            // 
            this.tsbtnDeleteDriver.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnDeleteDriver.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDeleteDriver.Image = global::SQLClient.Properties.Resources.delete_driver_16;
            this.tsbtnDeleteDriver.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnDeleteDriver.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeleteDriver.Name = "tsbtnDeleteDriver";
            this.tsbtnDeleteDriver.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDeleteDriver.Text = "删除驱动";
            this.tsbtnDeleteDriver.Click += new System.EventHandler(this.tsbtnDeleteDriver_Click);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.HeaderControl = this.tsTool;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "数据源";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(18, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 322);
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
            this.pg.Size = new System.Drawing.Size(318, 302);
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
            this.txtConnectionString.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Office2003;
            this.txtConnectionString.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtConnectionString.Properties.ReadOnly = true;
            this.txtConnectionString.Size = new System.Drawing.Size(318, 20);
            this.txtConnectionString.TabIndex = 0;
            this.txtConnectionString.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtConnectionString_ButtonClick);
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
            // 
            // separatorControl2
            // 
            this.separatorControl2.AutoSizeMode = true;
            this.separatorControl2.Dock = System.Windows.Forms.DockStyle.Left;
            this.separatorControl2.LineOrientation = System.Windows.Forms.Orientation.Vertical;
            this.separatorControl2.Location = new System.Drawing.Point(0, 0);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Padding = new System.Windows.Forms.Padding(8);
            this.separatorControl2.Size = new System.Drawing.Size(18, 413);
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
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(527, 25);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(68, 20);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "添加";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(441, 25);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 20);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            // 
            // NewConnectionFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 486);
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
            this.accDataSource.PerformLayout();
            this.tsTool.ResumeLayout(false);
            this.tsTool.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtConnectionString.Properties)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraBars.Navigation.AccordionControl accDataSource;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtConnectName;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private System.Windows.Forms.PropertyGrid pg;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.ButtonEdit txtConnectionString;
        private System.Windows.Forms.ToolStrip tsTool;
        private System.Windows.Forms.ToolStripButton tsbtnAddDriver;
        private System.Windows.Forms.ToolStripButton tsbtnDeleteDriver;
    }
}