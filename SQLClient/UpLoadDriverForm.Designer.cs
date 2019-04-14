namespace SQLClient
{
    partial class UpLoadDriverForm
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
            this.btnDriverPath = new DevExpress.XtraEditors.ButtonEdit();
            this.btnDALPath = new DevExpress.XtraEditors.ButtonEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDriverName = new System.Windows.Forms.TextBox();
            this.txtAssemblyName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tipMessage = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.btnDriverPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDALPath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDriverPath
            // 
            this.btnDriverPath.Location = new System.Drawing.Point(119, 118);
            this.btnDriverPath.Name = "btnDriverPath";
            this.btnDriverPath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnDriverPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnDriverPath.Size = new System.Drawing.Size(308, 20);
            this.btnDriverPath.TabIndex = 4;
            this.btnDriverPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDriverPath_ButtonClick);
            // 
            // btnDALPath
            // 
            this.btnDALPath.Location = new System.Drawing.Point(119, 144);
            this.btnDALPath.Name = "btnDALPath";
            this.btnDALPath.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.btnDALPath.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.btnDALPath.Size = new System.Drawing.Size(308, 20);
            this.btnDALPath.TabIndex = 5;
            this.btnDALPath.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.btnDALPath_ButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "数据库驱动：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "标准数据访问层：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "驱动名称：";
            // 
            // txtDriverName
            // 
            this.txtDriverName.Location = new System.Drawing.Point(119, 10);
            this.txtDriverName.Name = "txtDriverName";
            this.txtDriverName.Size = new System.Drawing.Size(308, 21);
            this.txtDriverName.TabIndex = 0;
            // 
            // txtAssemblyName
            // 
            this.txtAssemblyName.Location = new System.Drawing.Point(119, 37);
            this.txtAssemblyName.Name = "txtAssemblyName";
            this.txtAssemblyName.Size = new System.Drawing.Size(308, 21);
            this.txtAssemblyName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "程序集名称：";
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(119, 64);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(308, 21);
            this.txtNameSpace.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "名称空间：";
            // 
            // separatorControl1
            // 
            this.separatorControl1.AutoSizeMode = true;
            this.separatorControl1.Location = new System.Drawing.Point(5, 168);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Padding = new System.Windows.Forms.Padding(8);
            this.separatorControl1.Size = new System.Drawing.Size(430, 18);
            this.separatorControl1.TabIndex = 16;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(352, 192);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 17;
            this.btnOk.Text = "添加";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(257, 192);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(119, 91);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(308, 21);
            this.txtClassName.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(63, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "类名：";
            // 
            // UpLoadDriverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 227);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.txtNameSpace);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtAssemblyName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDriverName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDALPath);
            this.Controls.Add(this.btnDriverPath);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpLoadDriverForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加新数据源";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpLoadDriverForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.btnDriverPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDALPath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ButtonEdit btnDriverPath;
        private DevExpress.XtraEditors.ButtonEdit btnDALPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDriverName;
        private System.Windows.Forms.TextBox txtAssemblyName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip tipMessage;
    }
}