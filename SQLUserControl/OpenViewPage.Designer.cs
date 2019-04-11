namespace SQLUserControl
{
    partial class OpenViewPage
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
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.设计表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置为NULLNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置为空白字符串ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除记录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.关闭CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripButton2});
            this.tsOpenTable.Location = new System.Drawing.Point(0, 0);
            this.tsOpenTable.Name = "tsOpenTable";
            this.tsOpenTable.Padding = new System.Windows.Forms.Padding(0);
            this.tsOpenTable.Size = new System.Drawing.Size(700, 25);
            this.tsOpenTable.TabIndex = 3;
            this.tsOpenTable.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.查看VToolStripMenuItem,
            this.toolStripSeparator1,
            this.关闭CToolStripMenuItem});
            this.toolStripDropDownButton1.Image = global::SQLUserControl.Resource.menu_16;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出ToolStripMenuItem,
            this.toolStripSeparator2,
            this.设计表ToolStripMenuItem,
            this.查询表ToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Image = global::SQLUserControl.Resource.export_view_16;
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.导出ToolStripMenuItem.Text = "导出(&X)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // 设计表ToolStripMenuItem
            // 
            this.设计表ToolStripMenuItem.Image = global::SQLUserControl.Resource.design_view_16;
            this.设计表ToolStripMenuItem.Name = "设计表ToolStripMenuItem";
            this.设计表ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.设计表ToolStripMenuItem.Text = "设计表(&Y) Ctrl+D";
            // 
            // 查询表ToolStripMenuItem
            // 
            this.查询表ToolStripMenuItem.Image = global::SQLUserControl.Resource.new_select_16;
            this.查询表ToolStripMenuItem.Name = "查询表ToolStripMenuItem";
            this.查询表ToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.查询表ToolStripMenuItem.Text = "查询表(&Z) Ctrl+Q";
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置为NULLNToolStripMenuItem,
            this.设置为空白字符串ToolStripMenuItem,
            this.toolStripSeparator3,
            this.复制ToolStripMenuItem,
            this.粘贴ToolStripMenuItem,
            this.删除记录ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.编辑ToolStripMenuItem.Text = "编辑(&E)";
            // 
            // 设置为NULLNToolStripMenuItem
            // 
            this.设置为NULLNToolStripMenuItem.Name = "设置为NULLNToolStripMenuItem";
            this.设置为NULLNToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.设置为NULLNToolStripMenuItem.Text = "设置为NULL(&N)";
            // 
            // 设置为空白字符串ToolStripMenuItem
            // 
            this.设置为空白字符串ToolStripMenuItem.Name = "设置为空白字符串ToolStripMenuItem";
            this.设置为空白字符串ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.设置为空白字符串ToolStripMenuItem.Text = "设置为空白字符串(&R)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(185, 6);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            // 
            // 删除记录ToolStripMenuItem
            // 
            this.删除记录ToolStripMenuItem.Name = "删除记录ToolStripMenuItem";
            this.删除记录ToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.删除记录ToolStripMenuItem.Text = "删除记录";
            // 
            // 查看VToolStripMenuItem
            // 
            this.查看VToolStripMenuItem.Name = "查看VToolStripMenuItem";
            this.查看VToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.查看VToolStripMenuItem.Text = "查看(&V)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // 关闭CToolStripMenuItem
            // 
            this.关闭CToolStripMenuItem.Name = "关闭CToolStripMenuItem";
            this.关闭CToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.关闭CToolStripMenuItem.Text = "关闭(&C)";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::SQLUserControl.Resource.export_view_16;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(52, 22);
            this.toolStripButton2.Text = "导出";
            // 
            // tgv
            // 
            this.tgv.CrtPage = ((long)(0));
            this.tgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tgv.Location = new System.Drawing.Point(0, 25);
            this.tgv.Name = "tgv";
            this.tgv.RowCount = ((long)(0));
            this.tgv.RowIndex = ((long)(0));
            this.tgv.Size = new System.Drawing.Size(700, 375);
            this.tgv.Statement = null;
            this.tgv.TabIndex = 4;
            this.tgv.ChangeStatusBar += new SQLDAL.ChangeStatusBarEventHandler(this.tgv_ChangeStatusBar);
            // 
            // OpenViewPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tgv);
            this.Controls.Add(this.tsOpenTable);
            this.Name = "OpenViewPage";
            this.Size = new System.Drawing.Size(700, 400);
            this.tsOpenTable.ResumeLayout(false);
            this.tsOpenTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsOpenTable;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 设计表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查询表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置为NULLNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置为空白字符串ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除记录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看VToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 关闭CToolStripMenuItem;
        private TableGridView tgv;
    }
}
