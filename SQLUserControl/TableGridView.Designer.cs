namespace SQLUserControl
{
    partial class TableGridView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.tsNavigate = new System.Windows.Forms.ToolStrip();
            this.tsbtnSetting = new System.Windows.Forms.ToolStripButton();
            this.tsbtnLastPage = new System.Windows.Forms.ToolStripButton();
            this.tsbtnNextPage = new System.Windows.Forms.ToolStripButton();
            this.tstxtCurentPage = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnPrevPage = new System.Windows.Forms.ToolStripButton();
            this.tsbtnFristPage = new System.Windows.Forms.ToolStripButton();
            this.tslblRecordPerPage = new System.Windows.Forms.ToolStripLabel();
            this.tstxtPageSize = new System.Windows.Forms.ToolStripTextBox();
            this.tsbtnAddRecord = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDeleteRecord = new System.Windows.Forms.ToolStripButton();
            this.tsbtnApplyChange = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCancleChange = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnStop = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tsNavigate.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv.BackgroundColor = System.Drawing.Color.White;
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Tahoma", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle3.NullValue = "(Null)";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.GridColor = System.Drawing.SystemColors.Control;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Margin = new System.Windows.Forms.Padding(0);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersWidth = 24;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv.RowTemplate.Height = 23;
            this.dgv.Size = new System.Drawing.Size(726, 431);
            this.dgv.TabIndex = 4;
            this.dgv.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellClick);
            this.dgv.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_CellFormatting);
            this.dgv.CurrentCellChanged += new System.EventHandler(this.dgv_CurrentCellChanged);
            this.dgv.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgv_CurrentCellDirtyStateChanged);
            this.dgv.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_DataError);
            this.dgv.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgv_Scroll);
            // 
            // tsNavigate
            // 
            this.tsNavigate.BackColor = System.Drawing.SystemColors.Control;
            this.tsNavigate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tsNavigate.GripMargin = new System.Windows.Forms.Padding(0);
            this.tsNavigate.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsNavigate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSetting,
            this.tsbtnLastPage,
            this.tsbtnNextPage,
            this.tstxtCurentPage,
            this.tsbtnPrevPage,
            this.tsbtnFristPage,
            this.tslblRecordPerPage,
            this.tstxtPageSize,
            this.tsbtnAddRecord,
            this.tsbtnDeleteRecord,
            this.tsbtnApplyChange,
            this.tsbtnCancleChange,
            this.tsbtnRefresh,
            this.tsbtnStop});
            this.tsNavigate.Location = new System.Drawing.Point(0, 431);
            this.tsNavigate.Name = "tsNavigate";
            this.tsNavigate.Padding = new System.Windows.Forms.Padding(0);
            this.tsNavigate.Size = new System.Drawing.Size(726, 25);
            this.tsNavigate.TabIndex = 3;
            this.tsNavigate.Text = "toolStrip2";
            // 
            // tsbtnSetting
            // 
            this.tsbtnSetting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnSetting.Image = global::SQLUserControl.Properties.Resources.page_setting_16;
            this.tsbtnSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSetting.Name = "tsbtnSetting";
            this.tsbtnSetting.Size = new System.Drawing.Size(23, 22);
            this.tsbtnSetting.Text = "限制记录设置";
            this.tsbtnSetting.Click += new System.EventHandler(this.tsbtnSetting_Click);
            // 
            // tsbtnLastPage
            // 
            this.tsbtnLastPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnLastPage.Image = global::SQLUserControl.Properties.Resources.last_page_16;
            this.tsbtnLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnLastPage.Name = "tsbtnLastPage";
            this.tsbtnLastPage.Size = new System.Drawing.Size(23, 22);
            this.tsbtnLastPage.Text = "最后一页";
            this.tsbtnLastPage.Click += new System.EventHandler(this.tsbtnLastPage_Click);
            // 
            // tsbtnNextPage
            // 
            this.tsbtnNextPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnNextPage.Image = global::SQLUserControl.Properties.Resources.next_page_16;
            this.tsbtnNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnNextPage.Name = "tsbtnNextPage";
            this.tsbtnNextPage.Size = new System.Drawing.Size(23, 22);
            this.tsbtnNextPage.Text = "后一页";
            this.tsbtnNextPage.Click += new System.EventHandler(this.tsbtnNextPage_Click);
            // 
            // tstxtCurentPage
            // 
            this.tstxtCurentPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tstxtCurentPage.Name = "tstxtCurentPage";
            this.tstxtCurentPage.Size = new System.Drawing.Size(25, 25);
            this.tstxtCurentPage.Text = "1";
            // 
            // tsbtnPrevPage
            // 
            this.tsbtnPrevPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnPrevPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnPrevPage.Image = global::SQLUserControl.Properties.Resources.prev_page_16;
            this.tsbtnPrevPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnPrevPage.Name = "tsbtnPrevPage";
            this.tsbtnPrevPage.Size = new System.Drawing.Size(23, 22);
            this.tsbtnPrevPage.Text = "前一页";
            this.tsbtnPrevPage.Click += new System.EventHandler(this.tsbtnPrevPage_Click);
            // 
            // tsbtnFristPage
            // 
            this.tsbtnFristPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnFristPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnFristPage.Image = global::SQLUserControl.Properties.Resources.first_page_16;
            this.tsbtnFristPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFristPage.Name = "tsbtnFristPage";
            this.tsbtnFristPage.Size = new System.Drawing.Size(23, 22);
            this.tsbtnFristPage.Text = "第一页";
            this.tsbtnFristPage.Click += new System.EventHandler(this.tsbtnFristPage_Click);
            // 
            // tslblRecordPerPage
            // 
            this.tslblRecordPerPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslblRecordPerPage.Name = "tslblRecordPerPage";
            this.tslblRecordPerPage.Size = new System.Drawing.Size(76, 22);
            this.tslblRecordPerPage.Text = "条记录(每页)";
            this.tslblRecordPerPage.Visible = false;
            // 
            // tstxtPageSize
            // 
            this.tstxtPageSize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tstxtPageSize.Name = "tstxtPageSize";
            this.tstxtPageSize.Size = new System.Drawing.Size(25, 25);
            this.tstxtPageSize.Text = "10";
            this.tstxtPageSize.Visible = false;
            this.tstxtPageSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tstxtPageSize_KeyDown);
            // 
            // tsbtnAddRecord
            // 
            this.tsbtnAddRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAddRecord.Image = global::SQLUserControl.Properties.Resources.add_record_16;
            this.tsbtnAddRecord.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnAddRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAddRecord.Name = "tsbtnAddRecord";
            this.tsbtnAddRecord.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAddRecord.Text = "新建记录";
            // 
            // tsbtnDeleteRecord
            // 
            this.tsbtnDeleteRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDeleteRecord.Image = global::SQLUserControl.Properties.Resources.delete_record_16;
            this.tsbtnDeleteRecord.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnDeleteRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDeleteRecord.Name = "tsbtnDeleteRecord";
            this.tsbtnDeleteRecord.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDeleteRecord.Text = "删除记录";
            // 
            // tsbtnApplyChange
            // 
            this.tsbtnApplyChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnApplyChange.Image = global::SQLUserControl.Properties.Resources.apply_change_16;
            this.tsbtnApplyChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnApplyChange.Name = "tsbtnApplyChange";
            this.tsbtnApplyChange.Size = new System.Drawing.Size(23, 22);
            this.tsbtnApplyChange.Text = "应用改变";
            // 
            // tsbtnCancleChange
            // 
            this.tsbtnCancleChange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnCancleChange.Image = global::SQLUserControl.Properties.Resources.cancle_change_16;
            this.tsbtnCancleChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCancleChange.Name = "tsbtnCancleChange";
            this.tsbtnCancleChange.Size = new System.Drawing.Size(23, 22);
            this.tsbtnCancleChange.Text = "取消改变";
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = global::SQLUserControl.Properties.Resources.refresh_16;
            this.tsbtnRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefresh.Text = "刷新";
            // 
            // tsbtnStop
            // 
            this.tsbtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnStop.Image = global::SQLUserControl.Properties.Resources.stop_update_16;
            this.tsbtnStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnStop.Name = "tsbtnStop";
            this.tsbtnStop.Size = new System.Drawing.Size(23, 22);
            this.tsbtnStop.Text = "停止";
            // 
            // TableGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.tsNavigate);
            this.Name = "TableGridView";
            this.Size = new System.Drawing.Size(726, 456);
            this.Load += new System.EventHandler(this.TableGridView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tsNavigate.ResumeLayout(false);
            this.tsNavigate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ToolStrip tsNavigate;
        private System.Windows.Forms.ToolStripButton tsbtnSetting;
        private System.Windows.Forms.ToolStripButton tsbtnLastPage;
        private System.Windows.Forms.ToolStripButton tsbtnNextPage;
        private System.Windows.Forms.ToolStripTextBox tstxtCurentPage;
        private System.Windows.Forms.ToolStripButton tsbtnPrevPage;
        private System.Windows.Forms.ToolStripButton tsbtnFristPage;
        private System.Windows.Forms.ToolStripLabel tslblRecordPerPage;
        private System.Windows.Forms.ToolStripTextBox tstxtPageSize;
        private System.Windows.Forms.ToolStripButton tsbtnAddRecord;
        private System.Windows.Forms.ToolStripButton tsbtnDeleteRecord;
        private System.Windows.Forms.ToolStripButton tsbtnApplyChange;
        private System.Windows.Forms.ToolStripButton tsbtnCancleChange;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ToolStripButton tsbtnStop;
    }
}
