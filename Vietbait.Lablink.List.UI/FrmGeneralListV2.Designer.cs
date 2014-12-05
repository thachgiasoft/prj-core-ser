namespace Vietbait.Lablink.List.UI
{
    partial class FrmGeneralListV2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGeneralListV2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.navigationPane1 = new DevComponents.DotNetBar.NavigationPane();
            this.navigationPanePanel1 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.groupPanel6 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cboDeviceNameSpace = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtDeviceLibPath = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.cboDeviceClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnDeviceLibrary = new System.Windows.Forms.Button();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel5 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.rdUsed = new System.Windows.Forms.RadioButton();
            this.rdUnused = new System.Windows.Forms.RadioButton();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.rdTwo = new System.Windows.Forms.RadioButton();
            this.rdOne = new System.Windows.Forms.RadioButton();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboPort = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.rdR232 = new System.Windows.Forms.RadioButton();
            this.rdTCPIP = new System.Windows.Forms.RadioButton();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cboTest = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboManufacturer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnEquip = new DevComponents.DotNetBar.ButtonItem();
            this.navigationPanePanel5 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.btnManufacturer = new DevComponents.DotNetBar.ButtonItem();
            this.navigationPanePanel4 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.btnRS232 = new DevComponents.DotNetBar.ButtonItem();
            this.btnTCPIP = new DevComponents.DotNetBar.ButtonItem();
            this.navigationPanePanel2 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.grdDatacontrol = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Device_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Device_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCodeToCtrlEquip = new DevComponents.DotNetBar.ButtonItem();
            this.navigationPanePanel3 = new DevComponents.DotNetBar.NavigationPanePanel();
            this.btnTestType = new DevComponents.DotNetBar.ButtonItem();
            this.grdAll = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.grbAction = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.warmingboxTimer = new System.Windows.Forms.Timer(this.components);
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.warningBox1 = new DevComponents.DotNetBar.Controls.WarningBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.navigationPane1.SuspendLayout();
            this.navigationPanePanel1.SuspendLayout();
            this.groupPanel6.SuspendLayout();
            this.groupPanel5.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            this.groupPanel3.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            this.navigationPanePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDatacontrol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAll)).BeginInit();
            this.grbAction.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // navigationPane1
            // 
            this.navigationPane1.CanCollapse = true;
            this.navigationPane1.ConfigureItemVisible = false;
            this.navigationPane1.Controls.Add(this.navigationPanePanel2);
            this.navigationPane1.Controls.Add(this.navigationPanePanel5);
            this.navigationPane1.Controls.Add(this.navigationPanePanel4);
            this.navigationPane1.Controls.Add(this.navigationPanePanel3);
            this.navigationPane1.Controls.Add(this.navigationPanePanel1);
            this.navigationPane1.Dock = System.Windows.Forms.DockStyle.Left;
            this.navigationPane1.ItemPaddingBottom = 2;
            this.navigationPane1.ItemPaddingTop = 2;
            this.navigationPane1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnEquip,
            this.btnCodeToCtrlEquip,
            this.btnTestType,
            this.buttonItem4,
            this.btnManufacturer});
            this.navigationPane1.Location = new System.Drawing.Point(0, 0);
            this.navigationPane1.Name = "navigationPane1";
            this.navigationPane1.NavigationBarHeight = 207;
            this.navigationPane1.Padding = new System.Windows.Forms.Padding(1);
            this.navigationPane1.Size = new System.Drawing.Size(218, 523);
            this.navigationPane1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.navigationPane1.TabIndex = 3;
            // 
            // 
            // 
            this.navigationPane1.TitlePanel.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.navigationPane1.TitlePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationPane1.TitlePanel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.navigationPane1.TitlePanel.Location = new System.Drawing.Point(1, 1);
            this.navigationPane1.TitlePanel.Name = "panelTitle";
            this.navigationPane1.TitlePanel.Size = new System.Drawing.Size(216, 24);
            this.navigationPane1.TitlePanel.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.navigationPane1.TitlePanel.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.navigationPane1.TitlePanel.Style.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.navigationPane1.TitlePanel.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPane1.TitlePanel.Style.BorderSide = DevComponents.DotNetBar.eBorderSide.Bottom;
            this.navigationPane1.TitlePanel.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.navigationPane1.TitlePanel.Style.GradientAngle = 90;
            this.navigationPane1.TitlePanel.Style.MarginLeft = 4;
            this.navigationPane1.TitlePanel.TabIndex = 0;
            this.navigationPane1.TitlePanel.Text = "MÃ ĐIỀU KHIỂN THIẾT BỊ";
            // 
            // navigationPanePanel1
            // 
            this.navigationPanePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.navigationPanePanel1.Controls.Add(this.groupPanel6);
            this.navigationPanePanel1.Controls.Add(this.btnAdd);
            this.navigationPanePanel1.Controls.Add(this.groupPanel5);
            this.navigationPanePanel1.Controls.Add(this.groupPanel4);
            this.navigationPanePanel1.Controls.Add(this.groupPanel3);
            this.navigationPanePanel1.Controls.Add(this.groupPanel1);
            this.navigationPanePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel1.Location = new System.Drawing.Point(1, 1);
            this.navigationPanePanel1.Name = "navigationPanePanel1";
            this.navigationPanePanel1.ParentItem = this.btnEquip;
            this.navigationPanePanel1.Size = new System.Drawing.Size(216, 521);
            this.navigationPanePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.navigationPanePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.navigationPanePanel1.Style.GradientAngle = 90;
            this.navigationPanePanel1.TabIndex = 2;
            // 
            // groupPanel6
            // 
            this.groupPanel6.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.labelX6);
            this.groupPanel6.Controls.Add(this.cboDeviceNameSpace);
            this.groupPanel6.Controls.Add(this.labelX7);
            this.groupPanel6.Controls.Add(this.labelX8);
            this.groupPanel6.Controls.Add(this.txtDeviceLibPath);
            this.groupPanel6.Controls.Add(this.cboDeviceClass);
            this.groupPanel6.Controls.Add(this.btnDeviceLibrary);
            this.groupPanel6.Location = new System.Drawing.Point(4, 233);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new System.Drawing.Size(207, 95);
            // 
            // 
            // 
            this.groupPanel6.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel6.Style.BackColorGradientAngle = 90;
            this.groupPanel6.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel6.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderBottomWidth = 1;
            this.groupPanel6.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel6.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderLeftWidth = 1;
            this.groupPanel6.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderRightWidth = 1;
            this.groupPanel6.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderTopWidth = 1;
            this.groupPanel6.Style.Class = "";
            this.groupPanel6.Style.CornerDiameter = 4;
            this.groupPanel6.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel6.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel6.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel6.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel6.StyleMouseDown.Class = "";
            this.groupPanel6.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel6.StyleMouseOver.Class = "";
            this.groupPanel6.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel6.TabIndex = 64;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(3, 3);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(39, 23);
            this.labelX6.TabIndex = 44;
            this.labelX6.Text = "Library";
            // 
            // cboDeviceNameSpace
            // 
            this.cboDeviceNameSpace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDeviceNameSpace.DisplayMember = "Text";
            this.cboDeviceNameSpace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDeviceNameSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeviceNameSpace.DropDownWidth = 300;
            this.cboDeviceNameSpace.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDeviceNameSpace.FormattingEnabled = true;
            this.cboDeviceNameSpace.ItemHeight = 15;
            this.cboDeviceNameSpace.Location = new System.Drawing.Point(85, 35);
            this.cboDeviceNameSpace.Name = "cboDeviceNameSpace";
            this.cboDeviceNameSpace.Size = new System.Drawing.Size(114, 21);
            this.cboDeviceNameSpace.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDeviceNameSpace.TabIndex = 40;
            this.cboDeviceNameSpace.SelectedIndexChanged += new System.EventHandler(this.cboDeviceNameSpace_SelectedIndexChanged);
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(4, 32);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(75, 23);
            this.labelX7.TabIndex = 45;
            this.labelX7.Text = "Name Space";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(3, 61);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(75, 23);
            this.labelX8.TabIndex = 47;
            this.labelX8.Text = "Device Class";
            // 
            // txtDeviceLibPath
            // 
            this.txtDeviceLibPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDeviceLibPath.Border.Class = "TextBoxBorder";
            this.txtDeviceLibPath.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDeviceLibPath.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceLibPath.Location = new System.Drawing.Point(85, 6);
            this.txtDeviceLibPath.Name = "txtDeviceLibPath";
            this.txtDeviceLibPath.ReadOnly = true;
            this.txtDeviceLibPath.Size = new System.Drawing.Size(114, 21);
            this.txtDeviceLibPath.TabIndex = 38;
            this.txtDeviceLibPath.TextChanged += new System.EventHandler(this.txtDeviceLib_TextChanged);
            // 
            // cboDeviceClass
            // 
            this.cboDeviceClass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDeviceClass.DisplayMember = "Text";
            this.cboDeviceClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDeviceClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeviceClass.DropDownWidth = 300;
            this.cboDeviceClass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDeviceClass.FormattingEnabled = true;
            this.cboDeviceClass.ItemHeight = 15;
            this.cboDeviceClass.Location = new System.Drawing.Point(85, 63);
            this.cboDeviceClass.Name = "cboDeviceClass";
            this.cboDeviceClass.Size = new System.Drawing.Size(114, 21);
            this.cboDeviceClass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDeviceClass.TabIndex = 42;
            // 
            // btnDeviceLibrary
            // 
            this.btnDeviceLibrary.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDeviceLibrary.Image = ((System.Drawing.Image)(resources.GetObject("btnDeviceLibrary.Image")));
            this.btnDeviceLibrary.Location = new System.Drawing.Point(48, 4);
            this.btnDeviceLibrary.Name = "btnDeviceLibrary";
            this.btnDeviceLibrary.Size = new System.Drawing.Size(30, 23);
            this.btnDeviceLibrary.TabIndex = 58;
            this.btnDeviceLibrary.TabStop = false;
            this.btnDeviceLibrary.UseVisualStyleBackColor = true;
            this.btnDeviceLibrary.Click += new System.EventHandler(this.btnDeviceLibrary_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Enabled = false;
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::Vietbait.Lablink.List.UI.Properties.Resources._2rightarrow;
            this.btnAdd.Location = new System.Drawing.Point(54, 334);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 29);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 62;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupPanel5
            // 
            this.groupPanel5.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel5.Controls.Add(this.labelX5);
            this.groupPanel5.Controls.Add(this.rdUsed);
            this.groupPanel5.Controls.Add(this.rdUnused);
            this.groupPanel5.Location = new System.Drawing.Point(4, 193);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.Size = new System.Drawing.Size(208, 34);
            // 
            // 
            // 
            this.groupPanel5.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel5.Style.BackColorGradientAngle = 90;
            this.groupPanel5.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel5.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderBottomWidth = 1;
            this.groupPanel5.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel5.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderLeftWidth = 1;
            this.groupPanel5.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderRightWidth = 1;
            this.groupPanel5.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderTopWidth = 1;
            this.groupPanel5.Style.Class = "";
            this.groupPanel5.Style.CornerDiameter = 4;
            this.groupPanel5.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel5.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel5.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel5.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseDown.Class = "";
            this.groupPanel5.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseOver.Class = "";
            this.groupPanel5.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel5.TabIndex = 61;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(3, 3);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(50, 23);
            this.labelX5.TabIndex = 55;
            this.labelX5.Text = "Status";
            // 
            // rdUsed
            // 
            this.rdUsed.AutoSize = true;
            this.rdUsed.Checked = true;
            this.rdUsed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdUsed.Location = new System.Drawing.Point(71, 6);
            this.rdUsed.Name = "rdUsed";
            this.rdUsed.Size = new System.Drawing.Size(50, 17);
            this.rdUsed.TabIndex = 36;
            this.rdUsed.TabStop = true;
            this.rdUsed.Text = "Used";
            this.rdUsed.UseVisualStyleBackColor = true;
            // 
            // rdUnused
            // 
            this.rdUnused.AutoSize = true;
            this.rdUnused.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdUnused.Location = new System.Drawing.Point(136, 6);
            this.rdUnused.Name = "rdUnused";
            this.rdUnused.Size = new System.Drawing.Size(62, 17);
            this.rdUnused.TabIndex = 37;
            this.rdUnused.Text = "Unused";
            this.rdUnused.UseVisualStyleBackColor = true;
            // 
            // groupPanel4
            // 
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.labelX10);
            this.groupPanel4.Controls.Add(this.rdTwo);
            this.groupPanel4.Controls.Add(this.rdOne);
            this.groupPanel4.Location = new System.Drawing.Point(4, 146);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(208, 40);
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
            this.groupPanel4.TabIndex = 60;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(3, 5);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(50, 23);
            this.labelX10.TabIndex = 53;
            this.labelX10.Text = "Protocol";
            // 
            // rdTwo
            // 
            this.rdTwo.AutoSize = true;
            this.rdTwo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdTwo.Location = new System.Drawing.Point(136, 8);
            this.rdTwo.Name = "rdTwo";
            this.rdTwo.Size = new System.Drawing.Size(61, 17);
            this.rdTwo.TabIndex = 35;
            this.rdTwo.Text = "2-Ways";
            this.rdTwo.UseVisualStyleBackColor = true;
            // 
            // rdOne
            // 
            this.rdOne.AutoSize = true;
            this.rdOne.Checked = true;
            this.rdOne.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdOne.Location = new System.Drawing.Point(71, 8);
            this.rdOne.Name = "rdOne";
            this.rdOne.Size = new System.Drawing.Size(56, 17);
            this.rdOne.TabIndex = 33;
            this.rdOne.TabStop = true;
            this.rdOne.Text = "1-Way";
            this.rdOne.UseVisualStyleBackColor = true;
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.labelX3);
            this.groupPanel3.Controls.Add(this.cboPort);
            this.groupPanel3.Controls.Add(this.labelX11);
            this.groupPanel3.Controls.Add(this.rdR232);
            this.groupPanel3.Controls.Add(this.rdTCPIP);
            this.groupPanel3.Location = new System.Drawing.Point(4, 75);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(208, 65);
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
            this.groupPanel3.TabIndex = 59;
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(1, 32);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(35, 23);
            this.labelX3.TabIndex = 43;
            this.labelX3.Text = "Port";
            // 
            // cboPort
            // 
            this.cboPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPort.DisplayMember = "Text";
            this.cboPort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPort.FormattingEnabled = true;
            this.cboPort.ItemHeight = 15;
            this.cboPort.Location = new System.Drawing.Point(71, 32);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(126, 21);
            this.cboPort.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboPort.TabIndex = 49;
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.Class = "";
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(0, 3);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(65, 23);
            this.labelX11.TabIndex = 51;
            this.labelX11.Text = "Connector";
            // 
            // rdR232
            // 
            this.rdR232.AutoSize = true;
            this.rdR232.Checked = true;
            this.rdR232.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdR232.Location = new System.Drawing.Point(71, 6);
            this.rdR232.Name = "rdR232";
            this.rdR232.Size = new System.Drawing.Size(58, 17);
            this.rdR232.TabIndex = 32;
            this.rdR232.TabStop = true;
            this.rdR232.Text = "RS232";
            this.rdR232.UseVisualStyleBackColor = true;
            this.rdR232.CheckedChanged += new System.EventHandler(this.rdR232_CheckedChanged);
            // 
            // rdTCPIP
            // 
            this.rdTCPIP.AutoSize = true;
            this.rdTCPIP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdTCPIP.Location = new System.Drawing.Point(136, 6);
            this.rdTCPIP.Name = "rdTCPIP";
            this.rdTCPIP.Size = new System.Drawing.Size(61, 17);
            this.rdTCPIP.TabIndex = 34;
            this.rdTCPIP.Text = "TCP/IP";
            this.rdTCPIP.UseVisualStyleBackColor = true;
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.cboTest);
            this.groupPanel1.Controls.Add(this.cboManufacturer);
            this.groupPanel1.Location = new System.Drawing.Point(4, 5);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(207, 64);
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
            this.groupPanel1.TabIndex = 54;
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(3, 3);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 41;
            this.labelX2.Text = "Test Type";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(2, 32);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 23);
            this.labelX4.TabIndex = 46;
            this.labelX4.Text = "Manufacturer";
            // 
            // cboTest
            // 
            this.cboTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTest.DisplayMember = "Text";
            this.cboTest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTest.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTest.FormattingEnabled = true;
            this.cboTest.ItemHeight = 15;
            this.cboTest.Location = new System.Drawing.Point(83, 5);
            this.cboTest.Name = "cboTest";
            this.cboTest.Size = new System.Drawing.Size(114, 21);
            this.cboTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTest.TabIndex = 48;
            // 
            // cboManufacturer
            // 
            this.cboManufacturer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboManufacturer.DisplayMember = "Text";
            this.cboManufacturer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManufacturer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboManufacturer.FormattingEnabled = true;
            this.cboManufacturer.ItemHeight = 15;
            this.cboManufacturer.Location = new System.Drawing.Point(83, 32);
            this.cboManufacturer.Name = "cboManufacturer";
            this.cboManufacturer.Size = new System.Drawing.Size(115, 21);
            this.cboManufacturer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboManufacturer.TabIndex = 52;
            // 
            // btnEquip
            // 
            this.btnEquip.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnEquip.Image = ((System.Drawing.Image)(resources.GetObject("btnEquip.Image")));
            this.btnEquip.Name = "btnEquip";
            this.btnEquip.OptionGroup = "navBar";
            this.btnEquip.Tag = "thietbixetnghiem";
            this.btnEquip.Text = "THIẾT BỊ XÉT NGHIỆM";
            this.btnEquip.Click += new System.EventHandler(this.btnEquip_Click);
            // 
            // navigationPanePanel5
            // 
            this.navigationPanePanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.navigationPanePanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel5.Location = new System.Drawing.Point(1, 1);
            this.navigationPanePanel5.Name = "navigationPanePanel5";
            this.navigationPanePanel5.ParentItem = this.btnManufacturer;
            this.navigationPanePanel5.Size = new System.Drawing.Size(216, 314);
            this.navigationPanePanel5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.navigationPanePanel5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.navigationPanePanel5.Style.GradientAngle = 90;
            this.navigationPanePanel5.TabIndex = 6;
            // 
            // btnManufacturer
            // 
            this.btnManufacturer.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnManufacturer.Image = ((System.Drawing.Image)(resources.GetObject("btnManufacturer.Image")));
            this.btnManufacturer.Name = "btnManufacturer";
            this.btnManufacturer.OptionGroup = "navBar";
            this.btnManufacturer.Tag = "hangsanxuat";
            this.btnManufacturer.Text = "HÃNG SẢN XUẤT";
            this.btnManufacturer.Click += new System.EventHandler(this.btnManufacturer_Click);
            // 
            // navigationPanePanel4
            // 
            this.navigationPanePanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.navigationPanePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel4.Location = new System.Drawing.Point(1, 1);
            this.navigationPanePanel4.Name = "navigationPanePanel4";
            this.navigationPanePanel4.ParentItem = this.buttonItem4;
            this.navigationPanePanel4.Size = new System.Drawing.Size(216, 314);
            this.navigationPanePanel4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.navigationPanePanel4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.navigationPanePanel4.Style.GradientAngle = 90;
            this.navigationPanePanel4.TabIndex = 5;
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem4.Image")));
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.OptionGroup = "navBar";
            this.buttonItem4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRS232,
            this.btnTCPIP});
            this.buttonItem4.Tag = "congketnoi";
            this.buttonItem4.Text = "CỔNG KẾT NỐI";
            // 
            // btnRS232
            // 
            this.btnRS232.Name = "btnRS232";
            this.btnRS232.Tag = "congrs232";
            this.btnRS232.Text = "RS232";
            this.btnRS232.Click += new System.EventHandler(this.btnRS232_Click);
            // 
            // btnTCPIP
            // 
            this.btnTCPIP.Name = "btnTCPIP";
            this.btnTCPIP.Tag = "congtcpip";
            this.btnTCPIP.Text = "TCP/IP";
            this.btnTCPIP.Click += new System.EventHandler(this.btnTCPIP_Click);
            // 
            // navigationPanePanel2
            // 
            this.navigationPanePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.navigationPanePanel2.Controls.Add(this.grdDatacontrol);
            this.navigationPanePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel2.Location = new System.Drawing.Point(1, 25);
            this.navigationPanePanel2.Name = "navigationPanePanel2";
            this.navigationPanePanel2.ParentItem = this.btnCodeToCtrlEquip;
            this.navigationPanePanel2.Size = new System.Drawing.Size(216, 290);
            this.navigationPanePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.navigationPanePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.navigationPanePanel2.Style.GradientAngle = 90;
            this.navigationPanePanel2.TabIndex = 3;
            // 
            // grdDatacontrol
            // 
            this.grdDatacontrol.AllowUserToAddRows = false;
            this.grdDatacontrol.AllowUserToDeleteRows = false;
            this.grdDatacontrol.BackgroundColor = System.Drawing.Color.White;
            this.grdDatacontrol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDatacontrol.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Device_ID,
            this.Device_Name});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDatacontrol.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdDatacontrol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDatacontrol.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdDatacontrol.Location = new System.Drawing.Point(0, 0);
            this.grdDatacontrol.Name = "grdDatacontrol";
            this.grdDatacontrol.ReadOnly = true;
            this.grdDatacontrol.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDatacontrol.Size = new System.Drawing.Size(216, 290);
            this.grdDatacontrol.TabIndex = 7;
            this.grdDatacontrol.Tag = "dieukhienthietbi";
            this.grdDatacontrol.SelectionChanged += new System.EventHandler(this.grdDatacontrol_SelectionChanged);
            this.grdDatacontrol.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdDatacontrol_KeyUp);
            // 
            // Device_ID
            // 
            this.Device_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Device_ID.DataPropertyName = "Device_ID";
            this.Device_ID.HeaderText = "ID";
            this.Device_ID.Name = "Device_ID";
            this.Device_ID.ReadOnly = true;
            this.Device_ID.Width = 43;
            // 
            // Device_Name
            // 
            this.Device_Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Device_Name.DataPropertyName = "Device_Name";
            this.Device_Name.HeaderText = "Tên thiết bị";
            this.Device_Name.Name = "Device_Name";
            this.Device_Name.ReadOnly = true;
            this.Device_Name.Width = 85;
            // 
            // btnCodeToCtrlEquip
            // 
            this.btnCodeToCtrlEquip.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnCodeToCtrlEquip.Checked = true;
            this.btnCodeToCtrlEquip.Image = ((System.Drawing.Image)(resources.GetObject("btnCodeToCtrlEquip.Image")));
            this.btnCodeToCtrlEquip.Name = "btnCodeToCtrlEquip";
            this.btnCodeToCtrlEquip.OptionGroup = "navBar";
            this.btnCodeToCtrlEquip.Tag = "madieukhienthietbi";
            this.btnCodeToCtrlEquip.Text = "MÃ ĐIỀU KHIỂN THIẾT BỊ";
            this.btnCodeToCtrlEquip.Click += new System.EventHandler(this.btnCodeToCtrlEquip_Click);
            // 
            // navigationPanePanel3
            // 
            this.navigationPanePanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.navigationPanePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationPanePanel3.Location = new System.Drawing.Point(1, 1);
            this.navigationPanePanel3.Name = "navigationPanePanel3";
            this.navigationPanePanel3.ParentItem = this.btnTestType;
            this.navigationPanePanel3.Size = new System.Drawing.Size(216, 314);
            this.navigationPanePanel3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.navigationPanePanel3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.navigationPanePanel3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.navigationPanePanel3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.navigationPanePanel3.Style.GradientAngle = 90;
            this.navigationPanePanel3.TabIndex = 4;
            // 
            // btnTestType
            // 
            this.btnTestType.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btnTestType.Image = ((System.Drawing.Image)(resources.GetObject("btnTestType.Image")));
            this.btnTestType.Name = "btnTestType";
            this.btnTestType.OptionGroup = "navBar";
            this.btnTestType.Tag = "loaixetnghiem";
            this.btnTestType.Text = "LOẠI XÉT NGHIỆM";
            this.btnTestType.Click += new System.EventHandler(this.btnTestType_Click);
            // 
            // grdAll
            // 
            this.grdAll.AllowUserToDeleteRows = false;
            this.grdAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAll.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAll.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdAll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdAll.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdAll.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdAll.Location = new System.Drawing.Point(3, 3);
            this.grdAll.Name = "grdAll";
            this.grdAll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdAll.Size = new System.Drawing.Size(462, 419);
            this.grdAll.TabIndex = 4;
            this.grdAll.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.grdAll_DataError);
            this.grdAll.SelectionChanged += new System.EventHandler(this.grdAll_SelectionChanged);
            this.grdAll.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdAll_KeyUp);
            // 
            // grbAction
            // 
            this.grbAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbAction.CanvasColor = System.Drawing.SystemColors.Control;
            this.grbAction.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grbAction.Controls.Add(this.btnExit);
            this.grbAction.Controls.Add(this.btnSave);
            this.grbAction.Location = new System.Drawing.Point(3, 428);
            this.grbAction.Name = "grbAction";
            this.grbAction.Size = new System.Drawing.Size(462, 52);
            // 
            // 
            // 
            this.grbAction.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbAction.Style.BackColorGradientAngle = 90;
            this.grbAction.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbAction.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbAction.Style.BorderBottomWidth = 1;
            this.grbAction.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbAction.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbAction.Style.BorderLeftWidth = 1;
            this.grbAction.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbAction.Style.BorderRightWidth = 1;
            this.grbAction.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbAction.Style.BorderTopWidth = 1;
            this.grbAction.Style.Class = "";
            this.grbAction.Style.CornerDiameter = 4;
            this.grbAction.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbAction.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbAction.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbAction.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbAction.StyleMouseDown.Class = "";
            this.grbAction.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbAction.StyleMouseOver.Class = "";
            this.grbAction.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbAction.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::Vietbait.Lablink.List.UI.Properties.Resources.fileclose;
            this.btnExit.Location = new System.Drawing.Point(356, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 41);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "&Thoát";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(97, 41);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Lưu";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // warmingboxTimer
            // 
            this.warmingboxTimer.Interval = 5000;
            this.warmingboxTimer.Tick += new System.EventHandler(this.warmingboxTimer_Tick);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.grdAll);
            this.panelEx1.Controls.Add(this.warningBox1);
            this.panelEx1.Controls.Add(this.grbAction);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(218, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(468, 523);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            this.panelEx1.Text = "panelEx1";
            // 
            // warningBox1
            // 
            this.warningBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.warningBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(219)))), ((int)(((byte)(249)))));
            this.warningBox1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.warningBox1.ForeColor = System.Drawing.Color.Red;
            this.warningBox1.Image = ((System.Drawing.Image)(resources.GetObject("warningBox1.Image")));
            this.warningBox1.Location = new System.Drawing.Point(3, 486);
            this.warningBox1.Name = "warningBox1";
            this.warningBox1.OptionsButtonVisible = false;
            this.warningBox1.OptionsText = "";
            this.warningBox1.Size = new System.Drawing.Size(462, 34);
            this.warningBox1.TabIndex = 6;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmGeneralListV2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 523);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.navigationPane1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmGeneralListV2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DANH MỤC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmGeneralList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGeneralList_KeyDown);
            this.navigationPane1.ResumeLayout(false);
            this.navigationPanePanel1.ResumeLayout(false);
            this.groupPanel6.ResumeLayout(false);
            this.groupPanel5.ResumeLayout(false);
            this.groupPanel5.PerformLayout();
            this.groupPanel4.ResumeLayout(false);
            this.groupPanel4.PerformLayout();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.navigationPanePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDatacontrol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAll)).EndInit();
            this.grbAction.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.NavigationPane navigationPane1;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel1;
        private DevComponents.DotNetBar.ButtonItem btnEquip;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel2;
        private DevComponents.DotNetBar.ButtonItem btnCodeToCtrlEquip;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel3;
        private DevComponents.DotNetBar.ButtonItem btnTestType;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel4;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.NavigationPanePanel navigationPanePanel5;
        private DevComponents.DotNetBar.ButtonItem btnManufacturer;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdAll;
        private DevComponents.DotNetBar.ButtonItem btnRS232;
        private DevComponents.DotNetBar.ButtonItem btnTCPIP;
        private DevComponents.DotNetBar.Controls.GroupPanel grbAction;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.Controls.WarningBox warningBox1;
        private System.Windows.Forms.Timer warmingboxTimer;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdDatacontrol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Device_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Device_Name;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RadioButton rdUnused;
        private System.Windows.Forms.RadioButton rdTwo;
        private System.Windows.Forms.RadioButton rdUsed;
        private System.Windows.Forms.RadioButton rdOne;
        private System.Windows.Forms.RadioButton rdTCPIP;
        private System.Windows.Forms.RadioButton rdR232;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboManufacturer;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDeviceClass;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboPort;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDeviceNameSpace;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTest;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDeviceLibPath;
        internal System.Windows.Forms.Button btnDeviceLibrary;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel6;


    }
}