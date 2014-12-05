namespace Vietbait.TestInformation.UI
{
    partial class FrmTestDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTestDetail));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.barcode1 = new Mabry.Windows.Forms.Barcode.Barcode();
            this.clbTestType = new System.Windows.Forms.CheckedListBox();
            this.reflectionLabel1 = new DevComponents.DotNetBar.Controls.ReflectionLabel();
            this.grbDateSelector = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grbDate = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.dtpTodate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtpFromDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblDen = new System.Windows.Forms.Label();
            this.lbltu = new System.Windows.Forms.Label();
            this.cboDate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.grbLoaiXetNghiem = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grpStatus = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cboChuaCoKetQua = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cboTatca = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cboCoKetQua = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtPID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.cboSex = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.btnXoaThongTinBenhNhanVaTimKiem = new DevComponents.DotNetBar.ButtonItem();
            this.grbInfo = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.warningBox1 = new DevComponents.DotNetBar.Controls.WarningBox();
            this.grbThongtinbenhnhan = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.circularProgress1 = new DevComponents.DotNetBar.Controls.CircularProgress();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.grbPatients = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grdPatients = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Patient_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPatientName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnThoat = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnPrintAll = new DevComponents.DotNetBar.ButtonX();
            this.btnPrintByPatient = new DevComponents.DotNetBar.ButtonX();
            this.btnPrintByTypeTest = new DevComponents.DotNetBar.ButtonX();
            this.cboStyle = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnTest = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.WarningBoxTimer = new System.Windows.Forms.Timer(this.components);
            this.grbDateSelector.SuspendLayout();
            this.grbDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).BeginInit();
            this.grbLoaiXetNghiem.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.grbInfo.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.grbThongtinbenhnhan.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.grbPatients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPatients)).BeginInit();
            this.groupPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // barcode1
            // 
            this.barcode1.BackColor = System.Drawing.Color.White;
            this.barcode1.BarColor = System.Drawing.Color.Black;
            this.barcode1.BarRatio = 2F;
            this.barcode1.Data = "0000000000";
            this.barcode1.DataExtension = null;
            this.barcode1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barcode1.Location = new System.Drawing.Point(0, 0);
            this.barcode1.Name = "barcode1";
            this.barcode1.Size = new System.Drawing.Size(188, 59);
            this.barcode1.Symbology = Mabry.Windows.Forms.Barcode.Barcode.BarcodeSymbologies.Code128;
            this.barcode1.TabIndex = 0;
            // 
            // clbTestType
            // 
            this.clbTestType.CheckOnClick = true;
            this.clbTestType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbTestType.Location = new System.Drawing.Point(0, 0);
            this.clbTestType.MultiColumn = true;
            this.clbTestType.Name = "clbTestType";
            this.clbTestType.Size = new System.Drawing.Size(274, 109);
            this.clbTestType.TabIndex = 1;
            this.clbTestType.ThreeDCheckBoxes = true;
            this.clbTestType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.clbTestType_MouseUp);
            this.clbTestType.SelectedIndexChanged += new System.EventHandler(this.clbTestType_SelectedIndexChanged);
            // 
            // reflectionLabel1
            // 
            // 
            // 
            // 
            this.reflectionLabel1.BackgroundStyle.Class = "";
            this.reflectionLabel1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.reflectionLabel1.Location = new System.Drawing.Point(0, 0);
            this.reflectionLabel1.Name = "reflectionLabel1";
            this.reflectionLabel1.Size = new System.Drawing.Size(175, 70);
            this.reflectionLabel1.TabIndex = 0;
            // 
            // grbDateSelector
            // 
            this.grbDateSelector.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbDateSelector.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grbDateSelector.Controls.Add(this.grbDate);
            this.grbDateSelector.Controls.Add(this.cboDate);
            this.grbDateSelector.Location = new System.Drawing.Point(4, 12);
            this.grbDateSelector.Name = "grbDateSelector";
            this.grbDateSelector.Size = new System.Drawing.Size(151, 135);
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
            this.grbDateSelector.TabIndex = 61;
            this.grbDateSelector.Text = "Ngày";
            // 
            // grbDate
            // 
            this.grbDate.BackColor = System.Drawing.Color.Transparent;
            this.grbDate.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbDate.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grbDate.Controls.Add(this.dtpTodate);
            this.grbDate.Controls.Add(this.dtpFromDate);
            this.grbDate.Controls.Add(this.lblDen);
            this.grbDate.Controls.Add(this.lbltu);
            this.grbDate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grbDate.Location = new System.Drawing.Point(0, 31);
            this.grbDate.Name = "grbDate";
            this.grbDate.Size = new System.Drawing.Size(145, 83);
            // 
            // 
            // 
            this.grbDate.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbDate.Style.BackColorGradientAngle = 90;
            this.grbDate.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbDate.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDate.Style.BorderBottomWidth = 1;
            this.grbDate.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbDate.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDate.Style.BorderLeftWidth = 1;
            this.grbDate.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDate.Style.BorderRightWidth = 1;
            this.grbDate.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbDate.Style.BorderTopWidth = 1;
            this.grbDate.Style.Class = "";
            this.grbDate.Style.CornerDiameter = 4;
            this.grbDate.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbDate.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbDate.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbDate.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbDate.StyleMouseDown.Class = "";
            this.grbDate.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbDate.StyleMouseOver.Class = "";
            this.grbDate.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbDate.TabIndex = 1;
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
            this.dtpTodate.Location = new System.Drawing.Point(25, 28);
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
            this.dtpTodate.Size = new System.Drawing.Size(111, 20);
            this.dtpTodate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpTodate.TabIndex = 1;
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
            this.dtpFromDate.Location = new System.Drawing.Point(25, 3);
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
            this.dtpFromDate.Size = new System.Drawing.Size(111, 20);
            this.dtpFromDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpFromDate.TabIndex = 1;
            // 
            // lblDen
            // 
            this.lblDen.AutoSize = true;
            this.lblDen.Location = new System.Drawing.Point(-4, 31);
            this.lblDen.Name = "lblDen";
            this.lblDen.Size = new System.Drawing.Size(30, 14);
            this.lblDen.TabIndex = 0;
            this.lblDen.Text = "Đến:";
            // 
            // lbltu
            // 
            this.lbltu.AutoSize = true;
            this.lbltu.Location = new System.Drawing.Point(3, 4);
            this.lbltu.Name = "lbltu";
            this.lbltu.Size = new System.Drawing.Size(23, 14);
            this.lbltu.TabIndex = 0;
            this.lbltu.Text = "Từ:";
            // 
            // cboDate
            // 
            this.cboDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDate.DisplayMember = "Text";
            this.cboDate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDate.FormattingEnabled = true;
            this.cboDate.ItemHeight = 14;
            this.cboDate.Location = new System.Drawing.Point(5, 5);
            this.cboDate.Name = "cboDate";
            this.cboDate.Size = new System.Drawing.Size(131, 20);
            this.cboDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDate.TabIndex = 0;
            this.cboDate.SelectedIndexChanged += new System.EventHandler(this.cboDate_SelectedIndexChanged);
            // 
            // grbLoaiXetNghiem
            // 
            this.grbLoaiXetNghiem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbLoaiXetNghiem.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbLoaiXetNghiem.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grbLoaiXetNghiem.Controls.Add(this.clbTestType);
            this.grbLoaiXetNghiem.Location = new System.Drawing.Point(161, 12);
            this.grbLoaiXetNghiem.Name = "grbLoaiXetNghiem";
            this.grbLoaiXetNghiem.Size = new System.Drawing.Size(280, 137);
            // 
            // 
            // 
            this.grbLoaiXetNghiem.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbLoaiXetNghiem.Style.BackColorGradientAngle = 90;
            this.grbLoaiXetNghiem.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbLoaiXetNghiem.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbLoaiXetNghiem.Style.BorderBottomWidth = 1;
            this.grbLoaiXetNghiem.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbLoaiXetNghiem.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbLoaiXetNghiem.Style.BorderLeftWidth = 1;
            this.grbLoaiXetNghiem.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbLoaiXetNghiem.Style.BorderRightWidth = 1;
            this.grbLoaiXetNghiem.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbLoaiXetNghiem.Style.BorderTopWidth = 1;
            this.grbLoaiXetNghiem.Style.Class = "";
            this.grbLoaiXetNghiem.Style.CornerDiameter = 4;
            this.grbLoaiXetNghiem.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbLoaiXetNghiem.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbLoaiXetNghiem.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbLoaiXetNghiem.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbLoaiXetNghiem.StyleMouseDown.Class = "";
            this.grbLoaiXetNghiem.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbLoaiXetNghiem.StyleMouseOver.Class = "";
            this.grbLoaiXetNghiem.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbLoaiXetNghiem.TabIndex = 62;
            this.grbLoaiXetNghiem.Text = "Loại xét nghiệm";
            // 
            // grpStatus
            // 
            this.grpStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpStatus.BackColor = System.Drawing.Color.Transparent;
            this.grpStatus.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpStatus.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grpStatus.Controls.Add(this.cboChuaCoKetQua);
            this.grpStatus.Controls.Add(this.cboTatca);
            this.grpStatus.Controls.Add(this.cboCoKetQua);
            this.grpStatus.Location = new System.Drawing.Point(444, 12);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(119, 138);
            // 
            // 
            // 
            this.grpStatus.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpStatus.Style.BackColorGradientAngle = 90;
            this.grpStatus.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpStatus.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpStatus.Style.BorderBottomWidth = 1;
            this.grpStatus.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpStatus.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpStatus.Style.BorderLeftWidth = 1;
            this.grpStatus.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpStatus.Style.BorderRightWidth = 1;
            this.grpStatus.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpStatus.Style.BorderTopWidth = 1;
            this.grpStatus.Style.Class = "";
            this.grpStatus.Style.CornerDiameter = 4;
            this.grpStatus.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpStatus.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grpStatus.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpStatus.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpStatus.StyleMouseDown.Class = "";
            this.grpStatus.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpStatus.StyleMouseOver.Class = "";
            this.grpStatus.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpStatus.TabIndex = 62;
            this.grpStatus.Text = "Trạng thái";
            // 
            // cboChuaCoKetQua
            // 
            this.cboChuaCoKetQua.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cboChuaCoKetQua.BackgroundStyle.Class = "";
            this.cboChuaCoKetQua.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cboChuaCoKetQua.Location = new System.Drawing.Point(3, 56);
            this.cboChuaCoKetQua.Name = "cboChuaCoKetQua";
            this.cboChuaCoKetQua.Size = new System.Drawing.Size(106, 16);
            this.cboChuaCoKetQua.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboChuaCoKetQua.TabIndex = 2;
            this.cboChuaCoKetQua.Text = "Chưa có kết quả";
            // 
            // cboTatca
            // 
            this.cboTatca.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cboTatca.BackgroundStyle.Class = "";
            this.cboTatca.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cboTatca.Checked = true;
            this.cboTatca.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboTatca.CheckValue = "Y";
            this.cboTatca.Location = new System.Drawing.Point(3, 9);
            this.cboTatca.Name = "cboTatca";
            this.cboTatca.Size = new System.Drawing.Size(60, 19);
            this.cboTatca.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTatca.TabIndex = 0;
            this.cboTatca.Text = "Tất cả";
            this.cboTatca.CheckedChanged += new System.EventHandler(this.CboTatcaCheckedChanged);
            // 
            // cboCoKetQua
            // 
            this.cboCoKetQua.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cboCoKetQua.BackgroundStyle.Class = "";
            this.cboCoKetQua.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cboCoKetQua.Location = new System.Drawing.Point(3, 34);
            this.cboCoKetQua.Name = "cboCoKetQua";
            this.cboCoKetQua.Size = new System.Drawing.Size(106, 16);
            this.cboCoKetQua.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboCoKetQua.TabIndex = 1;
            this.cboCoKetQua.Text = "Có kết quả";
            // 
            // txtPID
            // 
            this.txtPID.Location = new System.Drawing.Point(49, 61);
            this.txtPID.Name = "txtPID";
            this.txtPID.Size = new System.Drawing.Size(161, 20);
            this.txtPID.TabIndex = 19;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(49, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(161, 20);
            this.txtName.TabIndex = 11;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(49, 90);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(161, 20);
            this.txtBarcode.TabIndex = 17;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(48, 32);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(36, 20);
            this.txtAge.TabIndex = 16;
            // 
            // cboSex
            // 
            this.cboSex.DisplayMember = "Text";
            this.cboSex.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSex.FormattingEnabled = true;
            this.cboSex.ItemHeight = 14;
            this.cboSex.Location = new System.Drawing.Point(132, 33);
            this.cboSex.Name = "cboSex";
            this.cboSex.Size = new System.Drawing.Size(78, 20);
            this.cboSex.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSex.TabIndex = 20;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSearch.Image = global::Vietbait.TestInformation.UI.Properties.Resources.search;
            this.btnSearch.Location = new System.Drawing.Point(0, 69);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(188, 54);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnXoaThongTinBenhNhanVaTimKiem});
            this.btnSearch.SubItemsExpandWidth = 50;
            this.btnSearch.TabIndex = 65;
            this.btnSearch.Text = "Tìm Kiếm";
            this.btnSearch.Click += new System.EventHandler(this.BtnSearchClick);
            // 
            // btnXoaThongTinBenhNhanVaTimKiem
            // 
            this.btnXoaThongTinBenhNhanVaTimKiem.GlobalItem = false;
            this.btnXoaThongTinBenhNhanVaTimKiem.Name = "btnXoaThongTinBenhNhanVaTimKiem";
            this.btnXoaThongTinBenhNhanVaTimKiem.Text = "Xóa Thông Tin Bệnh Nhân Và Tìm Kiếm";
            this.btnXoaThongTinBenhNhanVaTimKiem.Click += new System.EventHandler(this.btnXoaThongTinBenhNhanVaTimKiem_Click);
            // 
            // grbInfo
            // 
            this.grbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbInfo.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbInfo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grbInfo.Controls.Add(this.barcode1);
            this.grbInfo.Controls.Add(this.btnSearch);
            this.grbInfo.Location = new System.Drawing.Point(786, 21);
            this.grbInfo.Name = "grbInfo";
            this.grbInfo.Size = new System.Drawing.Size(194, 129);
            // 
            // 
            // 
            this.grbInfo.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbInfo.Style.BackColorGradientAngle = 90;
            this.grbInfo.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbInfo.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInfo.Style.BorderBottomWidth = 1;
            this.grbInfo.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbInfo.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInfo.Style.BorderLeftWidth = 1;
            this.grbInfo.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInfo.Style.BorderRightWidth = 1;
            this.grbInfo.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInfo.Style.BorderTopWidth = 1;
            this.grbInfo.Style.Class = "";
            this.grbInfo.Style.CornerDiameter = 4;
            this.grbInfo.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbInfo.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbInfo.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbInfo.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbInfo.StyleMouseDown.Class = "";
            this.grbInfo.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbInfo.StyleMouseOver.Class = "";
            this.grbInfo.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbInfo.TabIndex = 64;
            // 
            // groupPanel4
            // 
            this.groupPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel4.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.warningBox1);
            this.groupPanel4.Location = new System.Drawing.Point(4, 156);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(975, 47);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.Class = "";
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseDown.Class = "";
            this.groupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseOver.Class = "";
            this.groupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel4.TabIndex = 65;
            // 
            // warningBox1
            // 
            this.warningBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(219)))), ((int)(((byte)(249)))));
            this.warningBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.warningBox1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warningBox1.Image = ((System.Drawing.Image)(resources.GetObject("warningBox1.Image")));
            this.warningBox1.Location = new System.Drawing.Point(0, 0);
            this.warningBox1.Name = "warningBox1";
            this.warningBox1.OptionsText = "OK...";
            this.warningBox1.Size = new System.Drawing.Size(969, 41);
            this.warningBox1.TabIndex = 1;
            this.warningBox1.OptionsClick += new System.EventHandler(this.warningBox1_OptionsClick);
            // 
            // grbThongtinbenhnhan
            // 
            this.grbThongtinbenhnhan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grbThongtinbenhnhan.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbThongtinbenhnhan.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grbThongtinbenhnhan.Controls.Add(this.cboSex);
            this.grbThongtinbenhnhan.Controls.Add(this.txtPID);
            this.grbThongtinbenhnhan.Controls.Add(this.txtName);
            this.grbThongtinbenhnhan.Controls.Add(this.txtBarcode);
            this.grbThongtinbenhnhan.Controls.Add(this.Label5);
            this.grbThongtinbenhnhan.Controls.Add(this.txtAge);
            this.grbThongtinbenhnhan.Controls.Add(this.Label3);
            this.grbThongtinbenhnhan.Controls.Add(this.Label7);
            this.grbThongtinbenhnhan.Controls.Add(this.Label4);
            this.grbThongtinbenhnhan.Controls.Add(this.Label6);
            this.grbThongtinbenhnhan.Location = new System.Drawing.Point(565, 12);
            this.grbThongtinbenhnhan.Name = "grbThongtinbenhnhan";
            this.grbThongtinbenhnhan.Size = new System.Drawing.Size(218, 137);
            // 
            // 
            // 
            this.grbThongtinbenhnhan.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbThongtinbenhnhan.Style.BackColorGradientAngle = 90;
            this.grbThongtinbenhnhan.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbThongtinbenhnhan.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbThongtinbenhnhan.Style.BorderBottomWidth = 1;
            this.grbThongtinbenhnhan.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbThongtinbenhnhan.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbThongtinbenhnhan.Style.BorderLeftWidth = 1;
            this.grbThongtinbenhnhan.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbThongtinbenhnhan.Style.BorderRightWidth = 1;
            this.grbThongtinbenhnhan.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbThongtinbenhnhan.Style.BorderTopWidth = 1;
            this.grbThongtinbenhnhan.Style.Class = "";
            this.grbThongtinbenhnhan.Style.CornerDiameter = 4;
            this.grbThongtinbenhnhan.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbThongtinbenhnhan.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbThongtinbenhnhan.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbThongtinbenhnhan.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbThongtinbenhnhan.StyleMouseDown.Class = "";
            this.grbThongtinbenhnhan.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbThongtinbenhnhan.StyleMouseOver.Class = "";
            this.grbThongtinbenhnhan.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbThongtinbenhnhan.TabIndex = 66;
            this.grbThongtinbenhnhan.Text = "Thông tin bệnh nhân";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Location = new System.Drawing.Point(85, 35);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(49, 14);
            this.Label5.TabIndex = 14;
            this.Label5.Text = "GIới tính:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label3.Location = new System.Drawing.Point(24, 5);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(28, 14);
            this.Label3.TabIndex = 12;
            this.Label3.Text = "Tên:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label7.Location = new System.Drawing.Point(27, 64);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(25, 14);
            this.Label7.TabIndex = 18;
            this.Label7.Text = "PID:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label4.Location = new System.Drawing.Point(22, 37);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(30, 14);
            this.Label4.TabIndex = 13;
            this.Label4.Text = "Tuổi:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Label6.Location = new System.Drawing.Point(1, 93);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(51, 14);
            this.Label6.TabIndex = 15;
            this.Label6.Text = "Barcode:";
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.circularProgress1);
            this.groupPanel1.Controls.Add(this.groupPanel3);
            this.groupPanel1.Controls.Add(this.grbPatients);
            this.groupPanel1.Location = new System.Drawing.Point(4, 209);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(976, 297);
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
            this.groupPanel1.TabIndex = 67;
            // 
            // circularProgress1
            // 
            // 
            // 
            // 
            this.circularProgress1.BackgroundStyle.Class = "";
            this.circularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.circularProgress1.Location = new System.Drawing.Point(426, 83);
            this.circularProgress1.Name = "circularProgress1";
            this.circularProgress1.Size = new System.Drawing.Size(73, 75);
            this.circularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
            this.circularProgress1.TabIndex = 2;
            // 
            // groupPanel3
            // 
            this.groupPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.groupPanel3.Controls.Add(this.superTabControl1);
            this.groupPanel3.Location = new System.Drawing.Point(505, 3);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(464, 285);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.Class = "";
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.Class = "";
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.Class = "";
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 2;
            this.groupPanel3.Text = "Kết quả";
            // 
            // superTabControl1
            // 
            this.superTabControl1.AntiAlias = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.Location = new System.Drawing.Point(0, 0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = false;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = -1;
            this.superTabControl1.Size = new System.Drawing.Size(458, 264);
            this.superTabControl1.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
            this.superTabControl1.TabFont = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.TabHorizontalSpacing = 10;
            this.superTabControl1.TabIndex = 0;
            this.superTabControl1.TabLayoutType = DevComponents.DotNetBar.eSuperTabLayoutType.MultiLine;
            this.superTabControl1.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
            this.superTabControl1.Text = "superTabControl1";
            // 
            // grbPatients
            // 
            this.grbPatients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grbPatients.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbPatients.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.grbPatients.Controls.Add(this.grdPatients);
            this.grbPatients.Location = new System.Drawing.Point(9, 3);
            this.grbPatients.Name = "grbPatients";
            this.grbPatients.Size = new System.Drawing.Size(411, 285);
            // 
            // 
            // 
            this.grbPatients.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbPatients.Style.BackColorGradientAngle = 90;
            this.grbPatients.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbPatients.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbPatients.Style.BorderBottomWidth = 1;
            this.grbPatients.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbPatients.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbPatients.Style.BorderLeftWidth = 1;
            this.grbPatients.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbPatients.Style.BorderRightWidth = 1;
            this.grbPatients.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbPatients.Style.BorderTopWidth = 1;
            this.grbPatients.Style.Class = "";
            this.grbPatients.Style.CornerDiameter = 4;
            this.grbPatients.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbPatients.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbPatients.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbPatients.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbPatients.StyleMouseDown.Class = "";
            this.grbPatients.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbPatients.StyleMouseOver.Class = "";
            this.grbPatients.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbPatients.TabIndex = 1;
            this.grbPatients.Text = "Danh sách bệnh nhân";
            // 
            // grdPatients
            // 
            this.grdPatients.AllowUserToAddRows = false;
            this.grdPatients.AllowUserToDeleteRows = false;
            this.grdPatients.AllowUserToResizeColumns = false;
            this.grdPatients.AllowUserToResizeRows = false;
            this.grdPatients.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdPatients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdPatients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPatients.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBarcode,
            this.Patient_ID,
            this.colPatientName,
            this.colPID,
            this.colAge,
            this.clDate});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdPatients.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPatients.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdPatients.Location = new System.Drawing.Point(0, 0);
            this.grdPatients.MultiSelect = false;
            this.grdPatients.Name = "grdPatients";
            this.grdPatients.ReadOnly = true;
            this.grdPatients.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.grdPatients.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdPatients.Size = new System.Drawing.Size(405, 264);
            this.grdPatients.TabIndex = 0;
            this.grdPatients.SelectionChanged += new System.EventHandler(this.GrdPatientsSelectionChanged);
            // 
            // colBarcode
            // 
            this.colBarcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colBarcode.DataPropertyName = "Barcode";
            this.colBarcode.HeaderText = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.ReadOnly = true;
            this.colBarcode.Width = 73;
            // 
            // Patient_ID
            // 
            this.Patient_ID.DataPropertyName = "Patient_ID";
            this.Patient_ID.HeaderText = "colPatientId";
            this.Patient_ID.Name = "Patient_ID";
            this.Patient_ID.ReadOnly = true;
            this.Patient_ID.Visible = false;
            // 
            // colPatientName
            // 
            this.colPatientName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colPatientName.DataPropertyName = "Patient_Name";
            this.colPatientName.FillWeight = 150F;
            this.colPatientName.HeaderText = "Tên Bệnh Nhân";
            this.colPatientName.Name = "colPatientName";
            this.colPatientName.ReadOnly = true;
            this.colPatientName.Width = 106;
            // 
            // colPID
            // 
            this.colPID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colPID.DataPropertyName = "PID";
            this.colPID.FillWeight = 50F;
            this.colPID.HeaderText = "PID";
            this.colPID.Name = "colPID";
            this.colPID.ReadOnly = true;
            this.colPID.Width = 47;
            // 
            // colAge
            // 
            this.colAge.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.colAge.DataPropertyName = "Age";
            this.colAge.HeaderText = "Tuổi";
            this.colAge.Name = "colAge";
            this.colAge.ReadOnly = true;
            this.colAge.Width = 52;
            // 
            // clDate
            // 
            this.clDate.DataPropertyName = "DATEUPDATE";
            this.clDate.HeaderText = "Ngày";
            this.clDate.Name = "clDate";
            this.clDate.ReadOnly = true;
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThoat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThoat.Location = new System.Drawing.Point(894, 3);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(75, 23);
            this.btnThoat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThoat.TabIndex = 54;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.BtnThoatClick);
            // 
            // groupPanel2
            // 
            this.groupPanel2.BackColor = System.Drawing.Color.Transparent;
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.btnPrintAll);
            this.groupPanel2.Controls.Add(this.btnPrintByPatient);
            this.groupPanel2.Controls.Add(this.btnPrintByTypeTest);
            this.groupPanel2.Controls.Add(this.cboStyle);
            this.groupPanel2.Controls.Add(this.btnTest);
            this.groupPanel2.Controls.Add(this.btnThoat);
            this.groupPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupPanel2.Location = new System.Drawing.Point(0, 512);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(984, 35);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.Class = "";
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.Class = "";
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.Class = "";
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 68;
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrintAll.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrintAll.Location = new System.Drawing.Point(161, 3);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(75, 23);
            this.btnPrintAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrintAll.TabIndex = 58;
            this.btnPrintAll.Text = "In Tất";
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // btnPrintByPatient
            // 
            this.btnPrintByPatient.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrintByPatient.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrintByPatient.Location = new System.Drawing.Point(462, 3);
            this.btnPrintByPatient.Name = "btnPrintByPatient";
            this.btnPrintByPatient.Size = new System.Drawing.Size(159, 23);
            this.btnPrintByPatient.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrintByPatient.TabIndex = 57;
            this.btnPrintByPatient.Text = "In kết quả xét nghiệm BN";
            this.btnPrintByPatient.Click += new System.EventHandler(this.btnPrintByPatient_Click);
            // 
            // btnPrintByTypeTest
            // 
            this.btnPrintByTypeTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrintByTypeTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPrintByTypeTest.Location = new System.Drawing.Point(627, 3);
            this.btnPrintByTypeTest.Name = "btnPrintByTypeTest";
            this.btnPrintByTypeTest.Size = new System.Drawing.Size(159, 23);
            this.btnPrintByTypeTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrintByTypeTest.TabIndex = 56;
            this.btnPrintByTypeTest.Text = "In kết quả theo loại XN";
            // 
            // cboStyle
            // 
            this.cboStyle.DisplayMember = "Text";
            this.cboStyle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboStyle.FormattingEnabled = true;
            this.cboStyle.ItemHeight = 14;
            this.cboStyle.Location = new System.Drawing.Point(6, 6);
            this.cboStyle.Name = "cboStyle";
            this.cboStyle.Size = new System.Drawing.Size(121, 20);
            this.cboStyle.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboStyle.TabIndex = 55;
            this.cboStyle.SelectedValueChanged += new System.EventHandler(this.cboStyle_SelectedValueChanged);
            // 
            // btnTest
            // 
            this.btnTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTest.Location = new System.Drawing.Point(792, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTest.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem2,
            this.buttonItem3});
            this.btnTest.TabIndex = 54;
            this.btnTest.Text = "Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "BS380";
            // 
            // buttonItem2
            // 
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "BS400";
            // 
            // buttonItem3
            // 
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "Express plus";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            // 
            // WarningBoxTimer
            // 
            this.WarningBoxTimer.Interval = 1000;
            this.WarningBoxTimer.Tick += new System.EventHandler(this.WarningBoxTimer_Tick);
            // 
            // FrmTestDetail
            // 
            this.AcceptButton = this.btnSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(984, 547);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.grbThongtinbenhnhan);
            this.Controls.Add(this.groupPanel4);
            this.Controls.Add(this.grbInfo);
            this.Controls.Add(this.grbLoaiXetNghiem);
            this.Controls.Add(this.grbDateSelector);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmTestDetail";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmTestDetail_Load);
            this.grbDateSelector.ResumeLayout(false);
            this.grbDate.ResumeLayout(false);
            this.grbDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTodate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFromDate)).EndInit();
            this.grbLoaiXetNghiem.ResumeLayout(false);
            this.grpStatus.ResumeLayout(false);
            this.grbInfo.ResumeLayout(false);
            this.groupPanel4.ResumeLayout(false);
            this.grbThongtinbenhnhan.ResumeLayout(false);
            this.grbThongtinbenhnhan.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.grbPatients.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPatients)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.CheckedListBox clbTestType;
        private Mabry.Windows.Forms.Barcode.Barcode barcode1;
        private DevComponents.DotNetBar.Controls.ReflectionLabel reflectionLabel1;
        private DevComponents.DotNetBar.Controls.GroupPanel grbDateSelector;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpFromDate;
        private DevComponents.DotNetBar.Controls.GroupPanel grbDate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpTodate;
        private System.Windows.Forms.Label lblDen;
        private System.Windows.Forms.Label lbltu;
        private DevComponents.DotNetBar.Controls.GroupPanel grpStatus;
        private DevComponents.DotNetBar.Controls.CheckBoxX cboTatca;
        private DevComponents.DotNetBar.Controls.GroupPanel grbInfo;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        private DevComponents.DotNetBar.Controls.CheckBoxX cboChuaCoKetQua;
        private DevComponents.DotNetBar.Controls.CheckBoxX cboCoKetQua;
        private DevComponents.DotNetBar.Controls.GroupPanel grbThongtinbenhnhan;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox txtPID;
        internal System.Windows.Forms.TextBox txtName;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox txtBarcode;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtAge;
        internal System.Windows.Forms.Label Label6;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.ButtonX btnThoat;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSex;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX btnTest;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdPatients;
        private DevComponents.DotNetBar.Controls.GroupPanel grbPatients;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.Controls.WarningBox warningBox1;
        private System.Windows.Forms.Timer WarningBoxTimer;
        private DevComponents.DotNetBar.ButtonItem btnXoaThongTinBenhNhanVaTimKiem;
        private DevComponents.DotNetBar.Controls.GroupPanel grbLoaiXetNghiem;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboStyle;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress1;
        private DevComponents.DotNetBar.ButtonX btnPrintAll;
        private DevComponents.DotNetBar.ButtonX btnPrintByPatient;
        private DevComponents.DotNetBar.ButtonX btnPrintByTypeTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Patient_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPatientName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDate;
    }
}