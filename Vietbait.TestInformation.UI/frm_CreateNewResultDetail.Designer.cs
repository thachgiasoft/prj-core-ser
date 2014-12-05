namespace Vietbait.TestInformation.UI
{
    partial class frm_CreateNewResultDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_CreateNewResultDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtFilter = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chkRearve = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cmdAccept = new DevComponents.DotNetBar.ButtonX();
            this.cmdExit = new DevComponents.DotNetBar.ButtonX();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.grdList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colDataControl_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCHON = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Para_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colData_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNormal_Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNormal_LevelW = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMeasure_Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAlias_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Controls.Add(this.txtFilter);
            this.groupBox1.Controls.Add(this.chkRearve);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(924, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm nhanh";
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(198, 11);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(117, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "&Lọc thông tin nhanh";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.txtFilter.Border.Class = "TextBoxBorder";
            this.txtFilter.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtFilter.Location = new System.Drawing.Point(321, 13);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(468, 20);
            this.txtFilter.TabIndex = 4;
            this.txtFilter.TabStop = false;
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // chkRearve
            // 
            this.chkRearve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.chkRearve.BackgroundStyle.Class = "";
            this.chkRearve.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkRearve.Location = new System.Drawing.Point(117, 11);
            this.chkRearve.Name = "chkRearve";
            this.chkRearve.Size = new System.Drawing.Size(100, 23);
            this.chkRearve.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkRearve.TabIndex = 1;
            this.chkRearve.TabStop = false;
            this.chkRearve.Text = "&Đảo chọn";
            this.chkRearve.CheckedChanged += new System.EventHandler(this.chkRearve_CheckedChanged);
            // 
            // chkAll
            // 
            this.chkAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.chkAll.BackgroundStyle.Class = "";
            this.chkAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkAll.Location = new System.Drawing.Point(2, 11);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(115, 23);
            this.chkAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkAll.TabIndex = 0;
            this.chkAll.TabStop = false;
            this.chkAll.Text = "&Chọn tất/bỏ chọn";
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelX5);
            this.groupBox2.Controls.Add(this.labelX4);
            this.groupBox2.Controls.Add(this.labelX3);
            this.groupBox2.Controls.Add(this.labelX1);
            this.groupBox2.Controls.Add(this.cmdAccept);
            this.groupBox2.Controls.Add(this.cmdExit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 462);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(924, 45);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "&Chức năng";
            // 
            // labelX5
            // 
            this.labelX5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.Location = new System.Drawing.Point(407, 15);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(168, 23);
            this.labelX5.TabIndex = 9;
            this.labelX5.Text = "Alt+L->Lọc thông tin XN";
            // 
            // labelX4
            // 
            this.labelX4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(267, 16);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(117, 23);
            this.labelX4.TabIndex = 8;
            this.labelX4.Text = "Alt+A->Chấp nhận";
            // 
            // labelX3
            // 
            this.labelX3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(129, 16);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(117, 23);
            this.labelX3.TabIndex = 7;
            this.labelX3.Text = "Alt+C->Chọn tất cả";
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(6, 16);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(117, 23);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "Escape ->Thoát";
            // 
            // cmdAccept
            // 
            this.cmdAccept.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAccept.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Image = ((System.Drawing.Image)(resources.GetObject("cmdAccept.Image")));
            this.cmdAccept.Location = new System.Drawing.Point(721, 15);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(105, 23);
            this.cmdAccept.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdAccept.TabIndex = 1;
            this.cmdAccept.Text = "&Chấp nhận";
            this.cmdAccept.TextAlignment = DevComponents.DotNetBar.eButtonTextAlignment.Left;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdExit
            // 
            this.cmdExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.Location = new System.Drawing.Point(843, 15);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(75, 23);
            this.cmdExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExit.TabIndex = 0;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.grdList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 44);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(924, 418);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "&Thông số ";
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdList.BackgroundColor = System.Drawing.Color.White;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDataControl_ID,
            this.colCHON,
            this.Para_Name,
            this.colData_Name,
            this.colNormal_Level,
            this.colNormal_LevelW,
            this.colMeasure_Unit,
            this.colAlias_Name});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdList.Location = new System.Drawing.Point(3, 16);
            this.grdList.Name = "grdList";
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdList.Size = new System.Drawing.Size(918, 399);
            this.grdList.TabIndex = 2;
            // 
            // colDataControl_ID
            // 
            this.colDataControl_ID.DataPropertyName = "DataControl_ID";
            this.colDataControl_ID.HeaderText = "ID";
            this.colDataControl_ID.Name = "colDataControl_ID";
            this.colDataControl_ID.ToolTipText = "H";
            this.colDataControl_ID.Visible = false;
            // 
            // colCHON
            // 
            this.colCHON.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colCHON.DataPropertyName = "CHON";
            this.colCHON.FalseValue = "0";
            this.colCHON.HeaderText = "Chọn";
            this.colCHON.MinimumWidth = 40;
            this.colCHON.Name = "colCHON";
            this.colCHON.TrueValue = "1";
            this.colCHON.Width = 40;
            // 
            // Para_Name
            // 
            this.Para_Name.DataPropertyName = "Para_Name";
            this.Para_Name.HeaderText = "Thông số xét nghiệm";
            this.Para_Name.Name = "Para_Name";
            this.Para_Name.ReadOnly = true;
            // 
            // colData_Name
            // 
            this.colData_Name.DataPropertyName = "Data_Name";
            this.colData_Name.HeaderText = "Thông số xét nghiệm";
            this.colData_Name.MinimumWidth = 200;
            this.colData_Name.Name = "colData_Name";
            this.colData_Name.ReadOnly = true;
            this.colData_Name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colData_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colData_Name.Visible = false;
            // 
            // colNormal_Level
            // 
            this.colNormal_Level.DataPropertyName = "Normal_Level";
            this.colNormal_Level.HeaderText = "TB Nam";
            this.colNormal_Level.MinimumWidth = 200;
            this.colNormal_Level.Name = "colNormal_Level";
            this.colNormal_Level.ReadOnly = true;
            this.colNormal_Level.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colNormal_Level.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colNormal_LevelW
            // 
            this.colNormal_LevelW.DataPropertyName = "Normal_LevelW";
            this.colNormal_LevelW.HeaderText = "TB nữ";
            this.colNormal_LevelW.MinimumWidth = 200;
            this.colNormal_LevelW.Name = "colNormal_LevelW";
            this.colNormal_LevelW.ReadOnly = true;
            this.colNormal_LevelW.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colNormal_LevelW.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colMeasure_Unit
            // 
            this.colMeasure_Unit.DataPropertyName = "Measure_Unit";
            this.colMeasure_Unit.HeaderText = "Đơn vị";
            this.colMeasure_Unit.MinimumWidth = 200;
            this.colMeasure_Unit.Name = "colMeasure_Unit";
            this.colMeasure_Unit.ReadOnly = true;
            this.colMeasure_Unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colMeasure_Unit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colAlias_Name
            // 
            this.colAlias_Name.DataPropertyName = "Alias_Name";
            this.colAlias_Name.HeaderText = "Alias_Name";
            this.colAlias_Name.Name = "colAlias_Name";
            this.colAlias_Name.ReadOnly = true;
            this.colAlias_Name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAlias_Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAlias_Name.ToolTipText = "H";
            this.colAlias_Name.Visible = false;
            // 
            // frm_CreateNewResultDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 507);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frm_CreateNewResultDetail";
            this.ShowInTaskbar = false;
            this.Text = "Danh sách thông số";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_CreateNewResultDetail_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_CreateNewResultDetail_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkRearve;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkAll;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtFilter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdList;
        private DevComponents.DotNetBar.ButtonX cmdAccept;
        private DevComponents.DotNetBar.ButtonX cmdExit;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX5;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataControl_ID;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCHON;
        private System.Windows.Forms.DataGridViewTextBoxColumn Para_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colData_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNormal_Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNormal_LevelW;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMeasure_Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlias_Name;
    }
}