namespace Vietbait.Lablink.Report.UI
{
    partial class FrmReportDepartMent_VNIO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportDepartMent_VNIO));
            this.cmsGrd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReverseSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.grbDateSelector = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grdDepartment = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.CHON = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idkhoa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTodate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cboDate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.grdObjectType = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX_Object = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colObjectType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_Object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdTestTypeList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX_TestType = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_TestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbltu = new System.Windows.Forms.Label();
            this.dtpFromDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblDen = new System.Windows.Forms.Label();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.cmsGrd.SuspendLayout();
            this.grbDateSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
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
            this.grbDateSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grbDateSelector.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbDateSelector.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grbDateSelector.Controls.Add(this.grdDepartment);
            this.grbDateSelector.Controls.Add(this.dtpTodate);
            this.grbDateSelector.Controls.Add(this.cboDate);
            this.grbDateSelector.Controls.Add(this.grdObjectType);
            this.grbDateSelector.Controls.Add(this.grdTestTypeList);
            this.grbDateSelector.Controls.Add(this.lbltu);
            this.grbDateSelector.Controls.Add(this.dtpFromDate);
            this.grbDateSelector.Controls.Add(this.lblDen);
            this.grbDateSelector.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDateSelector.Location = new System.Drawing.Point(0, 1);
            this.grbDateSelector.Name = "grbDateSelector";
            this.grbDateSelector.Size = new System.Drawing.Size(275, 494);
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
            // grdDepartment
            // 
            this.grdDepartment.AllowUserToAddRows = false;
            this.grdDepartment.AllowUserToDeleteRows = false;
            this.grdDepartment.AllowUserToResizeRows = false;
            this.grdDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdDepartment.BackgroundColor = System.Drawing.Color.White;
            this.grdDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHON,
            this.dataGridViewTextBoxColumn3,
            this.idkhoa});
            this.grdDepartment.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDepartment.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdDepartment.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdDepartment.Location = new System.Drawing.Point(-9, 316);
            this.grdDepartment.Name = "grdDepartment";
            this.grdDepartment.RowHeadersVisible = false;
            this.grdDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDepartment.Size = new System.Drawing.Size(275, 159);
            this.grdDepartment.TabIndex = 73;
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
            this.dtpTodate.Size = new System.Drawing.Size(229, 21);
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
            this.cboDate.Size = new System.Drawing.Size(261, 21);
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
            // grdObjectType
            // 
            this.grdObjectType.AllowUserToAddRows = false;
            this.grdObjectType.AllowUserToDeleteRows = false;
            this.grdObjectType.AllowUserToResizeRows = false;
            this.grdObjectType.BackgroundColor = System.Drawing.Color.White;
            this.grdObjectType.CausesValidation = false;
            this.grdObjectType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdObjectType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_Object,
            this.colObjectType,
            this.colID_Object});
            this.grdObjectType.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdObjectType.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdObjectType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdObjectType.Location = new System.Drawing.Point(0, 92);
            this.grdObjectType.MultiSelect = false;
            this.grdObjectType.Name = "grdObjectType";
            this.grdObjectType.ReadOnly = true;
            this.grdObjectType.RowHeadersVisible = false;
            this.grdObjectType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdObjectType.Size = new System.Drawing.Size(266, 92);
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
            // grdTestTypeList
            // 
            this.grdTestTypeList.AllowUserToAddRows = false;
            this.grdTestTypeList.AllowUserToDeleteRows = false;
            this.grdTestTypeList.AllowUserToResizeRows = false;
            this.grdTestTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grdTestTypeList.BackgroundColor = System.Drawing.Color.White;
            this.grdTestTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTestTypeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_TestType,
            this.colTestType,
            this.colID_TestType});
            this.grdTestTypeList.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTestTypeList.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdTestTypeList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdTestTypeList.Location = new System.Drawing.Point(-3, 185);
            this.grdTestTypeList.Name = "grdTestTypeList";
            this.grdTestTypeList.RowHeadersVisible = false;
            this.grdTestTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTestTypeList.Size = new System.Drawing.Size(269, 161);
            this.grdTestTypeList.TabIndex = 70;
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
            this.dtpFromDate.Size = new System.Drawing.Size(229, 21);
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
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(12, 501);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(109, 45);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 72;
            this.btnPrint.Text = "In báo cáo (F4)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(127, 501);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(126, 45);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 77;
            this.btnExit.Text = "Thoát";
            // 
            // FrmReportDepartMent_VNIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 570);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.grbDateSelector);
            this.Controls.Add(this.btnPrint);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "FrmReportDepartMent_VNIO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THÔNG KÊ LƯU KẾT QUẢ XN";
            this.Load += new System.EventHandler(this.FrmGeneralReport_Load);
            this.cmsGrd.ResumeLayout(false);
            this.grbDateSelector.ResumeLayout(false);
            this.grbDateSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
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
        private DevComponents.DotNetBar.Controls.DataGridViewX grdTestTypeList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_TestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_TestType;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdDepartment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CHON;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn idkhoa;
        private DevComponents.DotNetBar.ButtonX btnExit;
    }
}