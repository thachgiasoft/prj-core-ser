namespace Vietbait.Lablink.Report.UI
{
    partial class FrmGeneralReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdTestTypeList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX_TestType = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_TestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmsGrd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReverseSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.grbDateSelector = new DevComponents.DotNetBar.Controls.GroupPanel();
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
            this.colX_Doctor = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDoctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_Doctor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboReportType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).BeginInit();
            this.cmsGrd.SuspendLayout();
            this.grbDateSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoctor)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdTestTypeList
            // 
            this.grdTestTypeList.AllowUserToAddRows = false;
            this.grdTestTypeList.AllowUserToDeleteRows = false;
            this.grdTestTypeList.AllowUserToResizeRows = false;
            this.grdTestTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.grdTestTypeList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTestTypeList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdTestTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTestTypeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_TestType,
            this.colTestType,
            this.colID_TestType});
            this.grdTestTypeList.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTestTypeList.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdTestTypeList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdTestTypeList.Location = new System.Drawing.Point(169, 22);
            this.grdTestTypeList.Name = "grdTestTypeList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTestTypeList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdTestTypeList.RowHeadersVisible = false;
            this.grdTestTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTestTypeList.Size = new System.Drawing.Size(270, 343);
            this.grdTestTypeList.TabIndex = 0;
            this.grdTestTypeList.Click += new System.EventHandler(this.grdTestTypeList_Click);
            this.grdTestTypeList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdTestTypeList_KeyUp);
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
            this.grbDateSelector.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbDateSelector.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbDateSelector.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grbDateSelector.Controls.Add(this.dtpTodate);
            this.grbDateSelector.Controls.Add(this.cboDate);
            this.grbDateSelector.Controls.Add(this.lbltu);
            this.grbDateSelector.Controls.Add(this.dtpFromDate);
            this.grbDateSelector.Controls.Add(this.lblDen);
            this.grbDateSelector.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDateSelector.Location = new System.Drawing.Point(12, 88);
            this.grbDateSelector.Name = "grbDateSelector";
            this.grbDateSelector.Size = new System.Drawing.Size(151, 107);
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
            this.dtpTodate.Size = new System.Drawing.Size(105, 21);
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
            this.cboDate.Size = new System.Drawing.Size(137, 21);
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
            this.dtpFromDate.Size = new System.Drawing.Size(105, 21);
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
            this.grdObjectType.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grdObjectType.BackgroundColor = System.Drawing.Color.White;
            this.grdObjectType.CausesValidation = false;
            this.grdObjectType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdObjectType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_Object,
            this.colObjectType,
            this.colID_Object});
            this.grdObjectType.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdObjectType.DefaultCellStyle = dataGridViewCellStyle4;
            this.grdObjectType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdObjectType.Location = new System.Drawing.Point(12, 201);
            this.grdObjectType.MultiSelect = false;
            this.grdObjectType.Name = "grdObjectType";
            this.grdObjectType.ReadOnly = true;
            this.grdObjectType.RowHeadersVisible = false;
            this.grdObjectType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdObjectType.Size = new System.Drawing.Size(151, 101);
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
            this.grdDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.grdDoctor.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdDoctor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grdDoctor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDoctor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_Doctor,
            this.colDoctor,
            this.colID_Doctor});
            this.grdDoctor.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDoctor.DefaultCellStyle = dataGridViewCellStyle6;
            this.grdDoctor.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdDoctor.Location = new System.Drawing.Point(445, 22);
            this.grdDoctor.Name = "grdDoctor";
            this.grdDoctor.RowHeadersVisible = false;
            this.grdDoctor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDoctor.Size = new System.Drawing.Size(270, 343);
            this.grdDoctor.TabIndex = 66;
            this.grdDoctor.Click += new System.EventHandler(this.grdDoctor_Click);
            this.grdDoctor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdDoctor_KeyUp);
            // 
            // colX_Doctor
            // 
            this.colX_Doctor.DataPropertyName = "Checked";
            this.colX_Doctor.FalseValue = "0";
            this.colX_Doctor.HeaderText = "X";
            this.colX_Doctor.Name = "colX_Doctor";
            this.colX_Doctor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colX_Doctor.TrueValue = "1";
            this.colX_Doctor.Width = 25;
            // 
            // colDoctor
            // 
            this.colDoctor.DataPropertyName = "User_Name";
            this.colDoctor.HeaderText = "Bác sỹ";
            this.colDoctor.Name = "colDoctor";
            this.colDoctor.ReadOnly = true;
            this.colDoctor.Width = 200;
            // 
            // colID_Doctor
            // 
            this.colID_Doctor.DataPropertyName = "User_ID";
            this.colID_Doctor.HeaderText = "ID";
            this.colID_Doctor.Name = "colID_Doctor";
            this.colID_Doctor.ReadOnly = true;
            this.colID_Doctor.Width = 40;
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(12, 308);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(151, 57);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 68;
            this.btnOK.Text = "In báo cáo (F4)";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel1.Controls.Add(this.cboReportType);
            this.groupPanel1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupPanel1.Location = new System.Drawing.Point(12, 22);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(151, 60);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.Class = "";
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.Class = "";
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.Class = "";
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 63;
            this.groupPanel1.Text = "Loại báo cáo";
            // 
            // cboReportType
            // 
            this.cboReportType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReportType.DisplayMember = "Text";
            this.cboReportType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportType.DropDownWidth = 131;
            this.cboReportType.FormattingEnabled = true;
            this.cboReportType.ItemHeight = 15;
            this.cboReportType.Items.AddRange(new object[] {
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem4,
            this.comboItem6});
            this.cboReportType.Location = new System.Drawing.Point(5, 5);
            this.cboReportType.Name = "cboReportType";
            this.cboReportType.Size = new System.Drawing.Size(131, 21);
            this.cboReportType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboReportType.TabIndex = 0;
            this.cboReportType.SelectedIndexChanged += new System.EventHandler(this.cboReportType_SelectedIndexChanged);
            // 
            // comboItem7
            // 
            this.comboItem7.FontStyle = System.Drawing.FontStyle.Bold;
            this.comboItem7.Text = "SL Mỗi Tham Số";
            // 
            // comboItem8
            // 
            this.comboItem8.FontStyle = System.Drawing.FontStyle.Bold;
            this.comboItem8.Text = "Kết Quả BN";
            // 
            // comboItem9
            // 
            this.comboItem9.FontStyle = System.Drawing.FontStyle.Bold;
            this.comboItem9.Text = "Loại Xét Nghiệm";
            // 
            // comboItem4
            // 
            this.comboItem4.FontStyle = System.Drawing.FontStyle.Bold;
            this.comboItem4.Text = "Sàng Lọc";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "Chi tiết kết quả BN";
            // 
            // FrmGeneralReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 379);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.grdObjectType);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grdDoctor);
            this.Controls.Add(this.grbDateSelector);
            this.Controls.Add(this.grdTestTypeList);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "FrmGeneralReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THÔNG KÊ KẾT QUẢ XÉT NGHIỆM";
            this.Load += new System.EventHandler(this.FrmGeneralReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGeneralReport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).EndInit();
            this.cmsGrd.ResumeLayout(false);
            this.grbDateSelector.ResumeLayout(false);
            this.grbDateSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDoctor)).EndInit();
            this.groupPanel1.ResumeLayout(false);
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
        private DevComponents.DotNetBar.Controls.DataGridViewX grdTestTypeList;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdDoctor;
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdObjectType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_TestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_TestType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_Object;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_Object;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboReportType;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private System.Windows.Forms.ContextMenuStrip cmsGrd;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmDeSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmReverseSelection;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_Doctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDoctor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_Doctor;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem6;
    }
}