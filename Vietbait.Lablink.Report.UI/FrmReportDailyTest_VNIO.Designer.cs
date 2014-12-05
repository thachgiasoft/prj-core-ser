namespace Vietbait.Lablink.Report.UI
{
    partial class FrmReportDailyTest_VNIO
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportDailyTest_VNIO));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmsGrd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReverseSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.grbDateSelector = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chkUncheck = new System.Windows.Forms.CheckBox();
            this.chkChon = new System.Windows.Forms.CheckBox();
            this.dtpTodate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cboDate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.lbltu = new System.Windows.Forms.Label();
            this.dtpFromDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblDen = new System.Windows.Forms.Label();
            this.grdObjectType = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX_Object = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colObjectType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_Object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdDoctor = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdTestTypeList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX_TestType = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_TestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.grdDepartment = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.CHON = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idkhoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.cmsGrd.SuspendLayout();
            this.grbDateSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoctor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDepartment)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsGrd
            // 
            this.cmsGrd.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSelectAll,
            this.tsmDeSelectAll,
            this.tsmReverseSelection});
            this.cmsGrd.Name = "cmsGrd";
            this.cmsGrd.Size = new System.Drawing.Size(141, 70);
            this.cmsGrd.Text = "Đánh dấu";
            // 
            // tsmSelectAll
            // 
            this.tsmSelectAll.Name = "tsmSelectAll";
            this.tsmSelectAll.Size = new System.Drawing.Size(140, 22);
            this.tsmSelectAll.Text = "Chọn Tất Cả";
            this.tsmSelectAll.Click += new System.EventHandler(this.tsmSelectAll_Click);
            // 
            // tsmDeSelectAll
            // 
            this.tsmDeSelectAll.Name = "tsmDeSelectAll";
            this.tsmDeSelectAll.Size = new System.Drawing.Size(140, 22);
            this.tsmDeSelectAll.Text = "Hủy Chọn";
            this.tsmDeSelectAll.Click += new System.EventHandler(this.tsmDeSelectAll_Click);
            // 
            // tsmReverseSelection
            // 
            this.tsmReverseSelection.Name = "tsmReverseSelection";
            this.tsmReverseSelection.Size = new System.Drawing.Size(140, 22);
            this.tsmReverseSelection.Text = "Đảo Chọn";
            this.tsmReverseSelection.Click += new System.EventHandler(this.tsmReverseSelection_Click);
            // 
            // grbDateSelector
            // 
            this.grbDateSelector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grbDateSelector.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbDateSelector.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grbDateSelector.Controls.Add(this.chkUncheck);
            this.grbDateSelector.Controls.Add(this.chkChon);
            this.grbDateSelector.Controls.Add(this.dtpTodate);
            this.grbDateSelector.Controls.Add(this.cboDate);
            this.grbDateSelector.Controls.Add(this.lbltu);
            this.grbDateSelector.Controls.Add(this.dtpFromDate);
            this.grbDateSelector.Controls.Add(this.lblDen);
            this.grbDateSelector.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDateSelector.Location = new System.Drawing.Point(12, 2);
            this.grbDateSelector.Name = "grbDateSelector";
            this.grbDateSelector.Size = new System.Drawing.Size(268, 132);
            // 
            // 
            // 
            this.grbDateSelector.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbDateSelector.Style.BackColorGradientAngle = 90;
            this.grbDateSelector.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbDateSelector.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDateSelector.Style.BorderBottomWidth = 1;
            this.grbDateSelector.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbDateSelector.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDateSelector.Style.BorderLeftWidth = 1;
            this.grbDateSelector.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDateSelector.Style.BorderRightWidth = 1;
            this.grbDateSelector.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDateSelector.Style.BorderTopWidth = 1;
            this.grbDateSelector.Style.Class = "";
            this.grbDateSelector.Style.CornerDiameter = 4;
            this.grbDateSelector.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbDateSelector.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbDateSelector.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbDateSelector.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbDateSelector.StyleMouseDown.Class = "";
            this.grbDateSelector.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbDateSelector.StyleMouseOver.Class = "";
            this.grbDateSelector.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbDateSelector.TabIndex = 62;
            this.grbDateSelector.Text = "Ngày";
            // 
            // chkUncheck
            // 
            this.chkUncheck.AutoSize = true;
            this.chkUncheck.Location = new System.Drawing.Point(94, 94);
            this.chkUncheck.Name = "chkUncheck";
            this.chkUncheck.Size = new System.Drawing.Size(80, 19);
            this.chkUncheck.TabIndex = 75;
            this.chkUncheck.Text = "Đảo chọn";
            this.chkUncheck.UseVisualStyleBackColor = true;
            this.chkUncheck.CheckedChanged += new System.EventHandler(this.chkUncheck_CheckedChanged);
            // 
            // chkChon
            // 
            this.chkChon.AutoSize = true;
            this.chkChon.Location = new System.Drawing.Point(3, 94);
            this.chkChon.Name = "chkChon";
            this.chkChon.Size = new System.Drawing.Size(90, 19);
            this.chkChon.TabIndex = 74;
            this.chkChon.Text = "Chọn tất cả";
            this.chkChon.UseVisualStyleBackColor = true;
            this.chkChon.CheckedChanged += new System.EventHandler(this.chkChon_CheckedChanged);
            // 
            // dtpTodate
            // 
            this.dtpTodate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dtpTodate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpTodate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpTodate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpTodate.ButtonDropDown.Visible = true;
            this.dtpTodate.CustomFormat = "dd/MM/yyyy";
            this.dtpTodate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpTodate.IsPopupCalendarOpen = false;
            this.dtpTodate.Location = new System.Drawing.Point(37, 57);
            // 
            // 
            // 
            this.dtpTodate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpTodate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtpTodate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtpTodate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpTodate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtpTodate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpTodate.MonthCalendar.DisplayMonth = new System.DateTime(2011, 4, 1, 0, 0, 0, 0);
            this.dtpTodate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpTodate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpTodate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpTodate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpTodate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpTodate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtpTodate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpTodate.MonthCalendar.TodayButtonVisible = true;
            this.dtpTodate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpTodate.Name = "dtpTodate";
            this.dtpTodate.Size = new System.Drawing.Size(222, 21);
            this.dtpTodate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpTodate.TabIndex = 1;
            // 
            // cboDate
            // 
            this.cboDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDate.DisplayMember = "Text";
            this.cboDate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDate.FormattingEnabled = true;
            this.cboDate.ItemHeight = 15;
            this.cboDate.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3});
            this.cboDate.Location = new System.Drawing.Point(5, 5);
            this.cboDate.Name = "cboDate";
            this.cboDate.Size = new System.Drawing.Size(254, 21);
            this.cboDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDate.TabIndex = 0;
            this.cboDate.SelectedIndexChanged += new System.EventHandler(this.cboDate_SelectedIndexChanged);
            // 
            // comboItem1
            // 
            this.comboItem1.FontStyle = System.Drawing.FontStyle.Bold;
            this.comboItem1.Text = "Hôm nay";
            // 
            // comboItem2
            // 
            this.comboItem2.FontStyle = System.Drawing.FontStyle.Bold;
            this.comboItem2.Text = "Hôm qua";
            // 
            // comboItem3
            // 
            this.comboItem3.FontStyle = System.Drawing.FontStyle.Bold;
            this.comboItem3.Text = "Tùy chọn";
            // 
            // lbltu
            // 
            this.lbltu.AutoSize = true;
            this.lbltu.BackColor = System.Drawing.Color.Transparent;
            this.lbltu.Location = new System.Drawing.Point(3, 35);
            this.lbltu.Name = "lbltu";
            this.lbltu.Size = new System.Drawing.Size(26, 15);
            this.lbltu.TabIndex = 0;
            this.lbltu.Text = "Từ:";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dtpFromDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpFromDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpFromDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpFromDate.ButtonDropDown.Visible = true;
            this.dtpFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtpFromDate.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtpFromDate.IsPopupCalendarOpen = false;
            this.dtpFromDate.Location = new System.Drawing.Point(37, 31);
            // 
            // 
            // 
            this.dtpFromDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpFromDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtpFromDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtpFromDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpFromDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtpFromDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpFromDate.MonthCalendar.DisplayMonth = new System.DateTime(2011, 4, 1, 0, 0, 0, 0);
            this.dtpFromDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpFromDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpFromDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpFromDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpFromDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpFromDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtpFromDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpFromDate.MonthCalendar.TodayButtonVisible = true;
            this.dtpFromDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(222, 21);
            this.dtpFromDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpFromDate.TabIndex = 1;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.BackColor = System.Drawing.Color.Transparent;
            this.lblDen.Location = new System.Drawing.Point(2, 61);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(33, 15);
            this.lblDen.TabIndex = 0;
            this.lblDen.Text = "Đến:";
            // 
            // grdObjectType
            // 
            this.grdObjectType.AllowUserToAddRows = false;
            this.grdObjectType.AllowUserToDeleteRows = false;
            this.grdObjectType.AllowUserToResizeRows = false;
            this.grdObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdObjectType.BackgroundColor = System.Drawing.Color.White;
            this.grdObjectType.CausesValidation = false;
            this.grdObjectType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdObjectType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_Object,
            this.colObjectType,
            this.colID_Object});
            this.grdObjectType.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdObjectType.DefaultCellStyle = dataGridViewCellStyle9;
            this.grdObjectType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdObjectType.Location = new System.Drawing.Point(32, 196);
            this.grdObjectType.MultiSelect = false;
            this.grdObjectType.Name = "grdObjectType";
            this.grdObjectType.ReadOnly = true;
            this.grdObjectType.RowHeadersVisible = false;
            this.grdObjectType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdObjectType.Size = new System.Drawing.Size(187, 279);
            this.grdObjectType.TabIndex = 64;
            this.grdObjectType.Click += new System.EventHandler(this.grdObjectType_Click);
            this.grdObjectType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdObjectType_KeyUp);
            // 
            // colX_Object
            // 
            this.colX_Object.DataPropertyName = "Checked";
            this.colX_Object.FalseValue = "0";
            this.colX_Object.HeaderText = "X";
            this.colX_Object.Name = "colX_Object";
            this.colX_Object.ReadOnly = true;
            this.colX_Object.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colX_Object.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colX_Object.TrueValue = "1";
            this.colX_Object.Width = 25;
            // 
            // colObjectType
            // 
            this.colObjectType.DataPropertyName = "sName";
            this.colObjectType.HeaderText = "Đối tượng";
            this.colObjectType.Name = "colObjectType";
            this.colObjectType.ReadOnly = true;
            this.colObjectType.Width = 120;
            // 
            // colID_Object
            // 
            this.colID_Object.DataPropertyName = "ID";
            this.colID_Object.HeaderText = "ID";
            this.colID_Object.Name = "colID_Object";
            this.colID_Object.ReadOnly = true;
            this.colID_Object.Visible = false;
            this.colID_Object.Width = 40;
            // 
            // grdDoctor
            // 
            this.grdDoctor.AllowUserToAddRows = false;
            this.grdDoctor.AllowUserToDeleteRows = false;
            this.grdDoctor.AllowUserToResizeRows = false;
            this.grdDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDoctor.BackgroundColor = System.Drawing.Color.White;
            this.grdDoctor.CausesValidation = false;
            this.grdDoctor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDoctor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.grdDoctor.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDoctor.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdDoctor.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdDoctor.Location = new System.Drawing.Point(477, 12);
            this.grdDoctor.MultiSelect = false;
            this.grdDoctor.Name = "grdDoctor";
            this.grdDoctor.ReadOnly = true;
            this.grdDoctor.RowHeadersVisible = false;
            this.grdDoctor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDoctor.Size = new System.Drawing.Size(250, 565);
            this.grdDoctor.TabIndex = 71;
            this.grdDoctor.Visible = false;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "Checked";
            this.dataGridViewCheckBoxColumn1.FalseValue = "0";
            this.dataGridViewCheckBoxColumn1.HeaderText = "X";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewCheckBoxColumn1.TrueValue = "1";
            this.dataGridViewCheckBoxColumn1.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "bacSyDieuTri";
            this.dataGridViewTextBoxColumn1.HeaderText = "Bác Sỹ điều trị";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "idbacSyDieuTri";
            this.dataGridViewTextBoxColumn2.HeaderText = "ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 40;
            // 
            // grdTestTypeList
            // 
            this.grdTestTypeList.AllowUserToAddRows = false;
            this.grdTestTypeList.AllowUserToDeleteRows = false;
            this.grdTestTypeList.AllowUserToResizeRows = false;
            this.grdTestTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grdTestTypeList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTestTypeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdTestTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTestTypeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_TestType,
            this.colTestType,
            this.colID_TestType});
            this.grdTestTypeList.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTestTypeList.DefaultCellStyle = dataGridViewCellStyle10;
            this.grdTestTypeList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdTestTypeList.Location = new System.Drawing.Point(6, 336);
            this.grdTestTypeList.Name = "grdTestTypeList";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTestTypeList.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.grdTestTypeList.RowHeadersVisible = false;
            this.grdTestTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTestTypeList.Size = new System.Drawing.Size(274, 193);
            this.grdTestTypeList.TabIndex = 73;
            // 
            // colX_TestType
            // 
            this.colX_TestType.DataPropertyName = "Checked";
            this.colX_TestType.FalseValue = "0";
            this.colX_TestType.HeaderText = "X";
            this.colX_TestType.Name = "colX_TestType";
            this.colX_TestType.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colX_TestType.TrueValue = "1";
            this.colX_TestType.Width = 25;
            // 
            // colTestType
            // 
            this.colTestType.DataPropertyName = "TestType_Name";
            this.colTestType.HeaderText = "Loại Xét Nghiệm";
            this.colTestType.Name = "colTestType";
            this.colTestType.ReadOnly = true;
            this.colTestType.Width = 200;
            // 
            // colID_TestType
            // 
            this.colID_TestType.DataPropertyName = "TestType_ID";
            this.colID_TestType.HeaderText = "ID";
            this.colID_TestType.Name = "colID_TestType";
            this.colID_TestType.ReadOnly = true;
            this.colID_TestType.Width = 40;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(13, 535);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(126, 45);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 74;
            this.btnOK.Text = "In báo cáo (F4)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // grdDepartment
            // 
            this.grdDepartment.AllowUserToAddRows = false;
            this.grdDepartment.AllowUserToDeleteRows = false;
            this.grdDepartment.AllowUserToResizeRows = false;
            this.grdDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDepartment.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDepartment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHON,
            this.dataGridViewTextBoxColumn3,
            this.idkhoa});
            this.grdDepartment.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDepartment.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdDepartment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdDepartment.Location = new System.Drawing.Point(6, 140);
            this.grdDepartment.Name = "grdDepartment";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDepartment.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grdDepartment.RowHeadersVisible = false;
            this.grdDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDepartment.Size = new System.Drawing.Size(274, 198);
            this.grdDepartment.TabIndex = 75;
            // 
            // CHON
            // 
            this.CHON.DataPropertyName = "CHON";
            this.CHON.FalseValue = "0";
            this.CHON.HeaderText = "X";
            this.CHON.Name = "CHON";
            this.CHON.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CHON.TrueValue = "1";
            this.CHON.Width = 25;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "khoa";
            this.dataGridViewTextBoxColumn3.HeaderText = "Khoa";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 200;
            // 
            // idkhoa
            // 
            this.idkhoa.DataPropertyName = "idkhoa";
            this.idkhoa.HeaderText = "IdKhoa";
            this.idkhoa.Name = "idkhoa";
            this.idkhoa.ReadOnly = true;
            this.idkhoa.Width = 40;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(145, 535);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(126, 45);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 76;
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // FrmReportDailyTest_VNIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 589);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.grdDepartment);
            this.Controls.Add(this.grdDoctor);
            this.Controls.Add(this.grbDateSelector);
            this.Controls.Add(this.grdTestTypeList);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grdObjectType);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmReportDailyTest_VNIO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THÔNG KÊ THEO KHOA LÂM SÀNG";
            this.Load += new System.EventHandler(this.FrmGeneralReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGeneralReport_KeyDown);
            this.cmsGrd.ResumeLayout(false);
            this.grbDateSelector.ResumeLayout(false);
            this.grbDateSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoctor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDepartment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel grbDateSelector;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpTodate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpFromDate;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.Label lbltu;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDate;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdObjectType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_Object;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_Object;
        private System.Windows.Forms.ContextMenuStrip cmsGrd;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmDeSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmReverseSelection;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdDoctor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdTestTypeList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_TestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_TestType;
        private System.Windows.Forms.CheckBox chkUncheck;
        private System.Windows.Forms.CheckBox chkChon;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdDepartment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHON;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn idkhoa;
        private DevComponents.DotNetBar.ButtonX btnExit;
    }
}