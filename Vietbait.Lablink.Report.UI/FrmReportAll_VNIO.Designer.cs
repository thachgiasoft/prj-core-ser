namespace Vietbait.Lablink.Report.UI
{
    partial class FrmReportAll_VNIO
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
            Janus.Windows.GridEX.GridEXLayout grdReport_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReportAll_VNIO));
            this.cmsGrd = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDeSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmReverseSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.grbDateSelector = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grdObjectType = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX_Object = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colObjectType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_Object = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grdNoiNgoaiTru = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colChon = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colNOINGOATRU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colnoitru = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboAoThat = new System.Windows.Forms.ComboBox();
            this.grdTestTypeList = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX_TestType = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID_TestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpTodate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cboDate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lbltu = new System.Windows.Forms.Label();
            this.dtpFromDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblDen = new System.Windows.Forms.Label();
            this.btnOK = new DevComponents.DotNetBar.ButtonX();
            this.grdReport = new Janus.Windows.GridEX.GridEX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.cmsGrd.SuspendLayout();
            this.grbDateSelector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNoiNgoaiTru)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).BeginInit();
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
            this.grbDateSelector.Controls.Add(this.grdObjectType);
            this.grbDateSelector.Controls.Add(this.grdNoiNgoaiTru);
            this.grbDateSelector.Controls.Add(this.cboAoThat);
            this.grbDateSelector.Controls.Add(this.grdTestTypeList);
            this.grbDateSelector.Controls.Add(this.dtpTodate);
            this.grbDateSelector.Controls.Add(this.cboDate);
            this.grbDateSelector.Controls.Add(this.label1);
            this.grbDateSelector.Controls.Add(this.lbltu);
            this.grbDateSelector.Controls.Add(this.dtpFromDate);
            this.grbDateSelector.Controls.Add(this.lblDen);
            this.grbDateSelector.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDateSelector.Location = new System.Drawing.Point(12, 2);
            this.grbDateSelector.Name = "grbDateSelector";
            this.grbDateSelector.Size = new System.Drawing.Size(284, 498);
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
            // 
            // grdObjectType
            // 
            this.grdObjectType.AllowUserToAddRows = false;
            this.grdObjectType.AllowUserToDeleteRows = false;
            this.grdObjectType.AllowUserToResizeRows = false;
            this.grdObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdObjectType.BackgroundColor = System.Drawing.Color.White;
            this.grdObjectType.CausesValidation = false;
            this.grdObjectType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdObjectType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX_Object,
            this.colObjectType,
            this.colID_Object});
            this.grdObjectType.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdObjectType.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdObjectType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdObjectType.Location = new System.Drawing.Point(0, 330);
            this.grdObjectType.MultiSelect = false;
            this.grdObjectType.Name = "grdObjectType";
            this.grdObjectType.ReadOnly = true;
            this.grdObjectType.RowHeadersVisible = false;
            this.grdObjectType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdObjectType.Size = new System.Drawing.Size(281, 141);
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
            this.colX_Object.TrueValue = "1";
            this.colX_Object.Width = 25;
            // 
            // colObjectType
            // 
            this.colObjectType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colObjectType.DataPropertyName = "sName";
            this.colObjectType.HeaderText = "Đối tượng";
            this.colObjectType.Name = "colObjectType";
            this.colObjectType.ReadOnly = true;
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
            // grdNoiNgoaiTru
            // 
            this.grdNoiNgoaiTru.AllowUserToAddRows = false;
            this.grdNoiNgoaiTru.AllowUserToDeleteRows = false;
            this.grdNoiNgoaiTru.AllowUserToResizeRows = false;
            this.grdNoiNgoaiTru.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdNoiNgoaiTru.BackgroundColor = System.Drawing.Color.White;
            this.grdNoiNgoaiTru.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdNoiNgoaiTru.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colChon,
            this.colNOINGOATRU,
            this.colnoitru});
            this.grdNoiNgoaiTru.ContextMenuStrip = this.cmsGrd;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdNoiNgoaiTru.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdNoiNgoaiTru.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdNoiNgoaiTru.Location = new System.Drawing.Point(0, 228);
            this.grdNoiNgoaiTru.Name = "grdNoiNgoaiTru";
            this.grdNoiNgoaiTru.RowHeadersVisible = false;
            this.grdNoiNgoaiTru.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdNoiNgoaiTru.Size = new System.Drawing.Size(281, 96);
            this.grdNoiNgoaiTru.TabIndex = 71;
            // 
            // colChon
            // 
            this.colChon.DataPropertyName = "CHON";
            this.colChon.FalseValue = "0";
            this.colChon.HeaderText = "X";
            this.colChon.Name = "colChon";
            this.colChon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colChon.TrueValue = "1";
            this.colChon.Width = 25;
            // 
            // colNOINGOATRU
            // 
            this.colNOINGOATRU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colNOINGOATRU.DataPropertyName = "NOINGOATRU";
            this.colNOINGOATRU.HeaderText = "Chuyên Khoa";
            this.colNOINGOATRU.Name = "colNOINGOATRU";
            this.colNOINGOATRU.ReadOnly = true;
            // 
            // colnoitru
            // 
            this.colnoitru.DataPropertyName = "noitru";
            this.colnoitru.HeaderText = "ID";
            this.colnoitru.Name = "colnoitru";
            this.colnoitru.ReadOnly = true;
            this.colnoitru.Visible = false;
            this.colnoitru.Width = 40;
            // 
            // cboAoThat
            // 
            this.cboAoThat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cboAoThat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAoThat.FormattingEnabled = true;
            this.cboAoThat.Location = new System.Drawing.Point(0, 469);
            this.cboAoThat.Name = "cboAoThat";
            this.cboAoThat.Size = new System.Drawing.Size(278, 23);
            this.cboAoThat.TabIndex = 73;
            // 
            // grdTestTypeList
            // 
            this.grdTestTypeList.AllowUserToAddRows = false;
            this.grdTestTypeList.AllowUserToDeleteRows = false;
            this.grdTestTypeList.AllowUserToResizeRows = false;
            this.grdTestTypeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.grdTestTypeList.Location = new System.Drawing.Point(0, 58);
            this.grdTestTypeList.Name = "grdTestTypeList";
            this.grdTestTypeList.RowHeadersVisible = false;
            this.grdTestTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTestTypeList.Size = new System.Drawing.Size(281, 164);
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
            this.colTestType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTestType.DataPropertyName = "TestType_Name";
            this.colTestType.HeaderText = "Loại Xét Nghiệm";
            this.colTestType.Name = "colTestType";
            this.colTestType.ReadOnly = true;
            // 
            // colID_TestType
            // 
            this.colID_TestType.DataPropertyName = "TestType_ID";
            this.colID_TestType.HeaderText = "ID";
            this.colID_TestType.Name = "colID_TestType";
            this.colID_TestType.ReadOnly = true;
            this.colID_TestType.Visible = false;
            this.colID_TestType.Width = 40;
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
            this.dtpTodate.Location = new System.Drawing.Point(176, 31);
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
            this.dtpTodate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
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
            this.dtpTodate.Size = new System.Drawing.Size(99, 21);
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
            this.cboDate.Location = new System.Drawing.Point(47, 5);
            this.cboDate.Name = "cboDate";
            this.cboDate.Size = new System.Drawing.Size(228, 21);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày:";
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
            this.dtpFromDate.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
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
            this.dtpFromDate.Size = new System.Drawing.Size(98, 21);
            this.dtpFromDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpFromDate.TabIndex = 1;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.BackColor = System.Drawing.Color.Transparent;
            this.lblDen.Location = new System.Drawing.Point(141, 35);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(33, 15);
            this.lblDen.TabIndex = 0;
            this.lblDen.Text = "Đến:";
            // 
            // btnOK
            // 
            this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(105, 506);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(70, 45);
            this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOK.TabIndex = 68;
            this.btnOK.Text = "Xem Báo Cáo";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grdReport
            // 
            this.grdReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            grdReport_DesignTimeLayout.LayoutString = resources.GetString("grdReport_DesignTimeLayout.LayoutString");
            this.grdReport.DesignTimeLayout = grdReport_DesignTimeLayout;
            this.grdReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.grdReport.GroupByBoxVisible = false;
            this.grdReport.Location = new System.Drawing.Point(302, 2);
            this.grdReport.Name = "grdReport";
            this.grdReport.Size = new System.Drawing.Size(686, 549);
            this.grdReport.TabIndex = 69;
            this.grdReport.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2003;
            this.grdReport.CellUpdated += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdReport_CellUpdated);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(12, 506);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(73, 45);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 72;
            this.btnPrint.Text = "In báo cáo (F4)";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(201, 506);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(57, 45);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 74;
            this.buttonX1.Text = "Thoát";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // FrmReportAll_VNIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 552);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.grbDateSelector);
            this.Controls.Add(this.grdReport);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmReportAll_VNIO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THÔNG KÊ TỔNG HỢP XÉT NGHIỆM";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmGeneralReport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGeneralReport_KeyDown);
            this.cmsGrd.ResumeLayout(false);
            this.grbDateSelector.ResumeLayout(false);
            this.grbDateSelector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdObjectType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdNoiNgoaiTru)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestTypeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReport)).EndInit();
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
        private DevComponents.DotNetBar.ButtonX btnOK;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdObjectType;
        private System.Windows.Forms.ContextMenuStrip cmsGrd;
        private System.Windows.Forms.ToolStripMenuItem tsmSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmDeSelectAll;
        private System.Windows.Forms.ToolStripMenuItem tsmReverseSelection;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdTestTypeList;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdNoiNgoaiTru;
        private Janus.Windows.GridEX.GridEX grdReport;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private System.Windows.Forms.ComboBox cboAoThat;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colChon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNOINGOATRU;
        private System.Windows.Forms.DataGridViewTextBoxColumn colnoitru;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_TestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_TestType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX_Object;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID_Object;
    }
}