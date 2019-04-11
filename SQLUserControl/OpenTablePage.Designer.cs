namespace SQLUserControl
{
    partial class OpenTablePage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tsOpenTable = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.tgv = new SQLUserControl.TableGridView();
            this.tsOpenTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsOpenTable
            // 
            this.tsOpenTable.BackColor = System.Drawing.SystemColors.Control;
            this.tsOpenTable.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsOpenTable.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsOpenTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripButton1,
            this.toolStripButton2});
            this.tsOpenTable.Location = new System.Drawing.Point(0, 0);
            this.tsOpenTable.Name = "tsOpenTable";
            this.tsOpenTable.Padding = new System.Windows.Forms.Padding(0);
            this.tsOpenTable.Size = new System.Drawing.Size(700, 25);
            this.tsOpenTable.TabIndex = 0;
            this.tsOpenTable.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = global::SQLUserControl.Resource.menu_16;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::SQLUserControl.Resource.import_table_16;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton1.Text = "导入";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::SQLUserControl.Resource.export_table_16;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton2.Text = "导出";
            // 
            // tgv
            // 
            this.tgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tgv.Location = new System.Drawing.Point(0, 25);
            this.tgv.Name = "tgv";
            this.tgv.Size = new System.Drawing.Size(700, 375);
            this.tgv.TabIndex = 1;
            this.tgv.ChangeStatusBar += new SQLDAL.ChangeStatusBarEventHandler(this.tgv_ChangeStatusBar);
            // 
            // OpenTablePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tgv);
            this.Controls.Add(this.tsOpenTable);
            this.DoubleBuffered = true;
            this.Name = "OpenTablePage";
            this.Size = new System.Drawing.Size(700, 400);
            this.tsOpenTable.ResumeLayout(false);
            this.tsOpenTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsOpenTable;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private TableGridView tgv;
    }
}
