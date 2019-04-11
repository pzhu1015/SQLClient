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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtConnectName = new System.Windows.Forms.TextBox();
            this.accDataSource = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnAdvance = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonEdit1 = new DevExpress.XtraEditors.ButtonEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpLoad = new DevExpress.XtraEditors.ButtonEdit();
            this.tvDataSourceEdit = new System.Windows.Forms.TreeView();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtProviderName = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtInvariant = new System.Windows.Forms.TextBox();
            this.separatorControl2 = new DevExpress.XtraEditors.SeparatorControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accDataSource)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpLoad.Properties)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(695, 338);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox7);
            this.tabPage1.Controls.Add(this.accDataSource);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.btnAdvance);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.btnCancel);
            this.tabPage1.Controls.Add(this.btnOK);
            this.tabPage1.Controls.Add(this.separatorControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(687, 311);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "选择数据源";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtConnectName);
            this.groupBox7.Location = new System.Drawing.Point(263, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(416, 47);
            this.groupBox7.TabIndex = 18;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "连接名称";
            // 
            // txtConnectName
            // 
            this.txtConnectName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectName.Location = new System.Drawing.Point(3, 18);
            this.txtConnectName.Name = "txtConnectName";
            this.txtConnectName.Size = new System.Drawing.Size(410, 22);
            this.txtConnectName.TabIndex = 4;
            this.txtConnectName.Text = "mysql_test";
            // 
            // accDataSource
            // 
            this.accDataSource.AllowItemSelection = true;
            this.accDataSource.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accDataSource.ExpandGroupOnHeaderClick = false;
            this.accDataSource.Location = new System.Drawing.Point(8, 3);
            this.accDataSource.Name = "accDataSource";
            this.accDataSource.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Auto;
            this.accDataSource.ShowGroupExpandButtons = false;
            this.accDataSource.Size = new System.Drawing.Size(248, 241);
            this.accDataSource.TabIndex = 17;
            this.accDataSource.Text = "accordionControl1";
            this.accDataSource.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.accDataSource_SelectedElementChanged);
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Expanded = true;
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "数据源";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(263, 138);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(413, 106);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "描述";
            // 
            // btnAdvance
            // 
            this.btnAdvance.Enabled = false;
            this.btnAdvance.Location = new System.Drawing.Point(601, 109);
            this.btnAdvance.Name = "btnAdvance";
            this.btnAdvance.Size = new System.Drawing.Size(75, 23);
            this.btnAdvance.TabIndex = 14;
            this.btnAdvance.Text = "高级";
            this.btnAdvance.Click += new System.EventHandler(this.btnAdvance_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtConnectionString);
            this.groupBox3.Location = new System.Drawing.Point(263, 56);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(416, 47);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "输入连接字符串";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConnectionString.Location = new System.Drawing.Point(3, 18);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(410, 22);
            this.txtConnectionString.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(499, 276);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(601, 276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "添加";
            // 
            // separatorControl1
            // 
            this.separatorControl1.Location = new System.Drawing.Point(10, 250);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(660, 20);
            this.separatorControl1.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.tvDataSourceEdit);
            this.tabPage2.Controls.Add(this.simpleButton2);
            this.tabPage2.Controls.Add(this.simpleButton1);
            this.tabPage2.Controls.Add(this.btnAdd);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.separatorControl2);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(687, 311);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "添加数据驱动";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonEdit1);
            this.groupBox2.Location = new System.Drawing.Point(267, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(406, 44);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "上传数据源客户端";
            // 
            // buttonEdit1
            // 
            this.buttonEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonEdit1.Location = new System.Drawing.Point(3, 18);
            this.buttonEdit1.Name = "buttonEdit1";
            this.buttonEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.buttonEdit1.Size = new System.Drawing.Size(400, 20);
            this.buttonEdit1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpLoad);
            this.groupBox1.Location = new System.Drawing.Point(267, 196);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 44);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "上传驱动";
            // 
            // btnUpLoad
            // 
            this.btnUpLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpLoad.Location = new System.Drawing.Point(3, 18);
            this.btnUpLoad.Name = "btnUpLoad";
            this.btnUpLoad.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnUpLoad.Size = new System.Drawing.Size(400, 20);
            this.btnUpLoad.TabIndex = 7;
            this.btnUpLoad.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnUpLoad_ButtonClick);
            // 
            // tvDataSourceEdit
            // 
            this.tvDataSourceEdit.BackColor = System.Drawing.Color.White;
            this.tvDataSourceEdit.FullRowSelect = true;
            this.tvDataSourceEdit.ItemHeight = 20;
            this.tvDataSourceEdit.Location = new System.Drawing.Point(0, 0);
            this.tvDataSourceEdit.Margin = new System.Windows.Forms.Padding(0);
            this.tvDataSourceEdit.Name = "tvDataSourceEdit";
            this.tvDataSourceEdit.ShowLines = false;
            this.tvDataSourceEdit.ShowNodeToolTips = true;
            this.tvDataSourceEdit.ShowPlusMinus = false;
            this.tvDataSourceEdit.ShowRootLines = false;
            this.tvDataSourceEdit.Size = new System.Drawing.Size(260, 240);
            this.tvDataSourceEdit.TabIndex = 13;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Location = new System.Drawing.Point(335, 276);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(85, 23);
            this.simpleButton2.TabIndex = 12;
            this.simpleButton2.Text = "删除";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(461, 276);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(85, 23);
            this.simpleButton1.TabIndex = 11;
            this.simpleButton1.Text = "修改";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(587, 276);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 23);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "添加";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.txtType);
            this.groupBox8.Location = new System.Drawing.Point(267, 97);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(406, 44);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "类名";
            // 
            // txtType
            // 
            this.txtType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtType.Location = new System.Drawing.Point(3, 18);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(400, 22);
            this.txtType.TabIndex = 1;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtProviderName);
            this.groupBox6.Location = new System.Drawing.Point(267, 1);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(406, 44);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "程序集名称";
            // 
            // txtProviderName
            // 
            this.txtProviderName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProviderName.Location = new System.Drawing.Point(3, 18);
            this.txtProviderName.Name = "txtProviderName";
            this.txtProviderName.Size = new System.Drawing.Size(400, 22);
            this.txtProviderName.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtInvariant);
            this.groupBox5.Location = new System.Drawing.Point(267, 49);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(406, 44);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "命名空间";
            // 
            // txtInvariant
            // 
            this.txtInvariant.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInvariant.Location = new System.Drawing.Point(3, 18);
            this.txtInvariant.Name = "txtInvariant";
            this.txtInvariant.Size = new System.Drawing.Size(400, 22);
            this.txtInvariant.TabIndex = 1;
            // 
            // separatorControl2
            // 
            this.separatorControl2.Location = new System.Drawing.Point(10, 250);
            this.separatorControl2.Name = "separatorControl2";
            this.separatorControl2.Size = new System.Drawing.Size(660, 20);
            this.separatorControl2.TabIndex = 16;
            // 
            // NewConnectionFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(695, 338);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewConnectionFrom";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "新建连接";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewConnectionFrom_FormClosing);
            this.Load += new System.EventHandler(this.NewConnectionFrom_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accDataSource)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit1.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnUpLoad.Properties)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtProviderName;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtInvariant;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.TextBox txtType;
        private DevExpress.XtraEditors.ButtonEdit btnUpLoad;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.TreeView tvDataSourceEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraBars.Navigation.AccordionControl accDataSource;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private System.Windows.Forms.GroupBox groupBox4;
        private DevExpress.XtraEditors.SimpleButton btnAdvance;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraEditors.ButtonEdit buttonEdit1;
        private DevExpress.XtraEditors.SeparatorControl separatorControl2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtConnectName;
    }
}