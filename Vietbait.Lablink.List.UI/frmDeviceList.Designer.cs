namespace Vietbait.Lablink.List.UI
{
    partial class frmDeviceList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnExit = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnEdit = new DevComponents.DotNetBar.ButtonX();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.grpInfor = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grDeviceConnectorType = new System.Windows.Forms.GroupBox();
            this.rdTCPIP = new System.Windows.Forms.RadioButton();
            this.rdR232 = new System.Windows.Forms.RadioButton();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.grProtocol = new System.Windows.Forms.GroupBox();
            this.rdTwo = new System.Windows.Forms.RadioButton();
            this.rdOne = new System.Windows.Forms.RadioButton();
            this.grStatus = new System.Windows.Forms.GroupBox();
            this.rdUnused = new System.Windows.Forms.RadioButton();
            this.rdUsed = new System.Windows.Forms.RadioButton();
            this.cboManufacturer = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboPort = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboTest = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtDesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDeviceName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtDeviceID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grdDevices = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.clDevice_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTestType_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDevice_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clManufacture_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clManufacture_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clValid = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clProtocol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clDeviceLibrary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDeviceNameSpace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDeviceClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clComputerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDeviceConnectorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clConnectorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeviceLibrary = new DevComponents.DotNetBar.ButtonX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cboDeviceClass = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboDeviceNameSpace = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtComputerName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtConnectorId = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDeviceLib = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.grpInfor.SuspendLayout();
            this.grDeviceConnectorType.SuspendLayout();
            this.grProtocol.SuspendLayout();
            this.grStatus.SuspendLayout();
            this.groupPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDevices)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExit.Location = new System.Drawing.Point(1073, 524);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDelete.Location = new System.Drawing.Point(911, 524);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnEdit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnEdit.Location = new System.Drawing.Point(831, 524);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAdd.Location = new System.Drawing.Point(750, 524);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpInfor
            // 
            this.grpInfor.BackColor = System.Drawing.Color.Transparent;
            this.grpInfor.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpInfor.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grpInfor.Controls.Add(this.grDeviceConnectorType);
            this.grpInfor.Controls.Add(this.labelX11);
            this.grpInfor.Controls.Add(this.grProtocol);
            this.grpInfor.Controls.Add(this.grStatus);
            this.grpInfor.Controls.Add(this.cboManufacturer);
            this.grpInfor.Controls.Add(this.cboPort);
            this.grpInfor.Controls.Add(this.cboTest);
            this.grpInfor.Controls.Add(this.txtDesc);
            this.grpInfor.Controls.Add(this.txtDeviceName);
            this.grpInfor.Controls.Add(this.labelX5);
            this.grpInfor.Controls.Add(this.txtDeviceID);
            this.grpInfor.Controls.Add(this.labelX4);
            this.grpInfor.Controls.Add(this.labelX3);
            this.grpInfor.Controls.Add(this.labelX2);
            this.grpInfor.Controls.Add(this.labelX1);
            this.grpInfor.Location = new System.Drawing.Point(2, 0);
            this.grpInfor.Name = "grpInfor";
            this.grpInfor.Size = new System.Drawing.Size(1158, 198);
            // 
            // 
            // 
            this.grpInfor.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grpInfor.Style.BackColorGradientAngle = 90;
            this.grpInfor.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grpInfor.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInfor.Style.BorderBottomWidth = 1;
            this.grpInfor.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grpInfor.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInfor.Style.BorderLeftWidth = 1;
            this.grpInfor.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInfor.Style.BorderRightWidth = 1;
            this.grpInfor.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpInfor.Style.BorderTopWidth = 1;
            this.grpInfor.Style.Class = "";
            this.grpInfor.Style.CornerDiameter = 4;
            this.grpInfor.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpInfor.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grpInfor.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grpInfor.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpInfor.StyleMouseDown.Class = "";
            this.grpInfor.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpInfor.StyleMouseOver.Class = "";
            this.grpInfor.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpInfor.TabIndex = 19;
            this.grpInfor.Text = "Information Device";
            // 
            // grDeviceConnectorType
            // 
            this.grDeviceConnectorType.BackColor = System.Drawing.Color.Transparent;
            this.grDeviceConnectorType.Controls.Add(this.rdTCPIP);
            this.grDeviceConnectorType.Controls.Add(this.rdR232);
            this.grDeviceConnectorType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.grDeviceConnectorType.Location = new System.Drawing.Point(708, 0);
            this.grDeviceConnectorType.Name = "grDeviceConnectorType";
            this.grDeviceConnectorType.Size = new System.Drawing.Size(332, 48);
            this.grDeviceConnectorType.TabIndex = 25;
            this.grDeviceConnectorType.TabStop = false;
            this.grDeviceConnectorType.Text = "Device Connector Type";
            // 
            // rdTCPIP
            // 
            this.rdTCPIP.AutoSize = true;
            this.rdTCPIP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdTCPIP.Location = new System.Drawing.Point(217, 20);
            this.rdTCPIP.Name = "rdTCPIP";
            this.rdTCPIP.Size = new System.Drawing.Size(61, 17);
            this.rdTCPIP.TabIndex = 5;
            this.rdTCPIP.Text = "TCP/IP";
            this.rdTCPIP.UseVisualStyleBackColor = true;
            // 
            // rdR232
            // 
            this.rdR232.AutoSize = true;
            this.rdR232.Checked = true;
            this.rdR232.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdR232.Location = new System.Drawing.Point(54, 20);
            this.rdR232.Name = "rdR232";
            this.rdR232.Size = new System.Drawing.Size(58, 17);
            this.rdR232.TabIndex = 4;
            this.rdR232.TabStop = true;
            this.rdR232.Text = "RS232";
            this.rdR232.UseVisualStyleBackColor = true;
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.Class = "";
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(9, 6);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(75, 23);
            this.labelX11.TabIndex = 30;
            this.labelX11.Text = "Device ID";
            // 
            // grProtocol
            // 
            this.grProtocol.BackColor = System.Drawing.Color.Transparent;
            this.grProtocol.Controls.Add(this.rdTwo);
            this.grProtocol.Controls.Add(this.rdOne);
            this.grProtocol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.grProtocol.Location = new System.Drawing.Point(708, 54);
            this.grProtocol.Name = "grProtocol";
            this.grProtocol.Size = new System.Drawing.Size(332, 48);
            this.grProtocol.TabIndex = 26;
            this.grProtocol.TabStop = false;
            this.grProtocol.Text = "Protocol";
            // 
            // rdTwo
            // 
            this.rdTwo.AutoSize = true;
            this.rdTwo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdTwo.Location = new System.Drawing.Point(217, 19);
            this.rdTwo.Name = "rdTwo";
            this.rdTwo.Size = new System.Drawing.Size(68, 17);
            this.rdTwo.TabIndex = 3;
            this.rdTwo.Text = "Two-way";
            this.rdTwo.UseVisualStyleBackColor = true;
            // 
            // rdOne
            // 
            this.rdOne.AutoSize = true;
            this.rdOne.Checked = true;
            this.rdOne.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdOne.Location = new System.Drawing.Point(54, 19);
            this.rdOne.Name = "rdOne";
            this.rdOne.Size = new System.Drawing.Size(67, 17);
            this.rdOne.TabIndex = 2;
            this.rdOne.TabStop = true;
            this.rdOne.Text = "One-way";
            this.rdOne.UseVisualStyleBackColor = true;
            // 
            // grStatus
            // 
            this.grStatus.BackColor = System.Drawing.Color.Transparent;
            this.grStatus.Controls.Add(this.rdUnused);
            this.grStatus.Controls.Add(this.rdUsed);
            this.grStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.grStatus.Location = new System.Drawing.Point(708, 102);
            this.grStatus.Name = "grStatus";
            this.grStatus.Size = new System.Drawing.Size(332, 48);
            this.grStatus.TabIndex = 24;
            this.grStatus.TabStop = false;
            this.grStatus.Text = "Status";
            // 
            // rdUnused
            // 
            this.rdUnused.AutoSize = true;
            this.rdUnused.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdUnused.Location = new System.Drawing.Point(217, 19);
            this.rdUnused.Name = "rdUnused";
            this.rdUnused.Size = new System.Drawing.Size(62, 17);
            this.rdUnused.TabIndex = 5;
            this.rdUnused.Text = "Unused";
            this.rdUnused.UseVisualStyleBackColor = true;
            // 
            // rdUsed
            // 
            this.rdUsed.AutoSize = true;
            this.rdUsed.Checked = true;
            this.rdUsed.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rdUsed.Location = new System.Drawing.Point(54, 19);
            this.rdUsed.Name = "rdUsed";
            this.rdUsed.Size = new System.Drawing.Size(50, 17);
            this.rdUsed.TabIndex = 4;
            this.rdUsed.TabStop = true;
            this.rdUsed.Text = "Used";
            this.rdUsed.UseVisualStyleBackColor = true;
            // 
            // cboManufacturer
            // 
            this.cboManufacturer.DisplayMember = "Text";
            this.cboManufacturer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboManufacturer.FormattingEnabled = true;
            this.cboManufacturer.ItemHeight = 14;
            this.cboManufacturer.Location = new System.Drawing.Point(90, 125);
            this.cboManufacturer.Name = "cboManufacturer";
            this.cboManufacturer.Size = new System.Drawing.Size(374, 20);
            this.cboManufacturer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboManufacturer.TabIndex = 23;
            // 
            // cboPort
            // 
            this.cboPort.DisplayMember = "Text";
            this.cboPort.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort.FormattingEnabled = true;
            this.cboPort.ItemHeight = 14;
            this.cboPort.Location = new System.Drawing.Point(90, 96);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(374, 20);
            this.cboPort.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboPort.TabIndex = 22;
            // 
            // cboTest
            // 
            this.cboTest.DisplayMember = "Text";
            this.cboTest.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTest.FormattingEnabled = true;
            this.cboTest.ItemHeight = 14;
            this.cboTest.Location = new System.Drawing.Point(90, 67);
            this.cboTest.Name = "cboTest";
            this.cboTest.Size = new System.Drawing.Size(374, 20);
            this.cboTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTest.TabIndex = 21;
            // 
            // txtDesc
            // 
            // 
            // 
            // 
            this.txtDesc.Border.Class = "TextBoxBorder";
            this.txtDesc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDesc.Location = new System.Drawing.Point(90, 154);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(1039, 20);
            this.txtDesc.TabIndex = 20;
            // 
            // txtDeviceName
            // 
            // 
            // 
            // 
            this.txtDeviceName.Border.Class = "TextBoxBorder";
            this.txtDeviceName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDeviceName.Location = new System.Drawing.Point(90, 38);
            this.txtDeviceName.Name = "txtDeviceName";
            this.txtDeviceName.Size = new System.Drawing.Size(374, 20);
            this.txtDeviceName.TabIndex = 19;
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.Class = "";
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(9, 149);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(75, 23);
            this.labelX5.TabIndex = 18;
            this.labelX5.Text = "Decription";
            // 
            // txtDeviceID
            // 
            // 
            // 
            // 
            this.txtDeviceID.Border.Class = "TextBoxBorder";
            this.txtDeviceID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDeviceID.Location = new System.Drawing.Point(90, 9);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(374, 20);
            this.txtDeviceID.TabIndex = 21;
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(9, 125);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 23);
            this.labelX4.TabIndex = 17;
            this.labelX4.Text = "Manufacturer";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.Class = "";
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(9, 96);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(75, 23);
            this.labelX3.TabIndex = 16;
            this.labelX3.Text = "Port";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(9, 64);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 15;
            this.labelX2.Text = "Test Type";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelX1.Location = new System.Drawing.Point(7, 35);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 14;
            this.labelX1.Text = "Device Name";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.grdDevices);
            this.groupPanel2.Location = new System.Drawing.Point(319, 204);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(841, 314);
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
            this.groupPanel2.TabIndex = 20;
            this.groupPanel2.Text = "Device List";
            // 
            // grdDevices
            // 
            this.grdDevices.AllowUserToAddRows = false;
            this.grdDevices.AllowUserToDeleteRows = false;
            this.grdDevices.BackgroundColor = System.Drawing.Color.White;
            this.grdDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDevices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clDevice_ID,
            this.clTestType_ID,
            this.clTestName,
            this.clDevice_Name,
            this.clManufacture_ID,
            this.clManufacture_Name,
            this.clValid,
            this.clDescription,
            this.clProtocol,
            this.clDeviceLibrary,
            this.clDeviceNameSpace,
            this.clDeviceClass,
            this.clComputerName,
            this.clDeviceConnectorType,
            this.clConnectorID});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdDevices.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDevices.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdDevices.Location = new System.Drawing.Point(0, 0);
            this.grdDevices.Name = "grdDevices";
            this.grdDevices.ReadOnly = true;
            this.grdDevices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdDevices.Size = new System.Drawing.Size(835, 293);
            this.grdDevices.TabIndex = 2;
            this.grdDevices.SelectionChanged += new System.EventHandler(this.grdDevices_SelectionChanged);
            // 
            // clDevice_ID
            // 
            this.clDevice_ID.DataPropertyName = "Device_ID";
            this.clDevice_ID.HeaderText = "ID";
            this.clDevice_ID.Name = "clDevice_ID";
            this.clDevice_ID.ReadOnly = true;
            this.clDevice_ID.Visible = false;
            // 
            // clTestType_ID
            // 
            this.clTestType_ID.DataPropertyName = "TestType_ID";
            this.clTestType_ID.HeaderText = "Test Type ID";
            this.clTestType_ID.Name = "clTestType_ID";
            this.clTestType_ID.ReadOnly = true;
            this.clTestType_ID.Visible = false;
            // 
            // clTestName
            // 
            this.clTestName.DataPropertyName = "TestType_Name";
            this.clTestName.HeaderText = "Test Type Name";
            this.clTestName.Name = "clTestName";
            this.clTestName.ReadOnly = true;
            // 
            // clDevice_Name
            // 
            this.clDevice_Name.DataPropertyName = "Device_Name";
            this.clDevice_Name.HeaderText = "Device Name";
            this.clDevice_Name.Name = "clDevice_Name";
            this.clDevice_Name.ReadOnly = true;
            // 
            // clManufacture_ID
            // 
            this.clManufacture_ID.DataPropertyName = "Manufacture_ID";
            this.clManufacture_ID.HeaderText = "Manufacturer ID";
            this.clManufacture_ID.Name = "clManufacture_ID";
            this.clManufacture_ID.ReadOnly = true;
            this.clManufacture_ID.Visible = false;
            // 
            // clManufacture_Name
            // 
            this.clManufacture_Name.DataPropertyName = "Manufacture_Name";
            this.clManufacture_Name.HeaderText = "Manufacturer Name";
            this.clManufacture_Name.Name = "clManufacture_Name";
            this.clManufacture_Name.ReadOnly = true;
            // 
            // clValid
            // 
            this.clValid.DataPropertyName = "Valid";
            this.clValid.HeaderText = "Use";
            this.clValid.Name = "clValid";
            this.clValid.ReadOnly = true;
            this.clValid.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clDescription
            // 
            this.clDescription.DataPropertyName = "Description";
            this.clDescription.HeaderText = "Description";
            this.clDescription.Name = "clDescription";
            this.clDescription.ReadOnly = true;
            // 
            // clProtocol
            // 
            this.clProtocol.DataPropertyName = "Protocol";
            this.clProtocol.HeaderText = "Protocol";
            this.clProtocol.Name = "clProtocol";
            this.clProtocol.ReadOnly = true;
            // 
            // clDeviceLibrary
            // 
            this.clDeviceLibrary.DataPropertyName = "DeviceLibrary";
            this.clDeviceLibrary.HeaderText = "Device Library";
            this.clDeviceLibrary.Name = "clDeviceLibrary";
            this.clDeviceLibrary.ReadOnly = true;
            // 
            // clDeviceNameSpace
            // 
            this.clDeviceNameSpace.DataPropertyName = "DeviceNameSpace";
            this.clDeviceNameSpace.HeaderText = "Device Name Space";
            this.clDeviceNameSpace.Name = "clDeviceNameSpace";
            this.clDeviceNameSpace.ReadOnly = true;
            // 
            // clDeviceClass
            // 
            this.clDeviceClass.DataPropertyName = "DeviceClass";
            this.clDeviceClass.HeaderText = "Device Class";
            this.clDeviceClass.Name = "clDeviceClass";
            this.clDeviceClass.ReadOnly = true;
            // 
            // clComputerName
            // 
            this.clComputerName.DataPropertyName = "ComputerName";
            this.clComputerName.HeaderText = "Computer Name";
            this.clComputerName.Name = "clComputerName";
            this.clComputerName.ReadOnly = true;
            // 
            // clDeviceConnectorType
            // 
            this.clDeviceConnectorType.DataPropertyName = "DeviceConnectorType";
            this.clDeviceConnectorType.HeaderText = "Device Connector Type";
            this.clDeviceConnectorType.Name = "clDeviceConnectorType";
            this.clDeviceConnectorType.ReadOnly = true;
            // 
            // clConnectorID
            // 
            this.clConnectorID.DataPropertyName = "ConnectorID";
            this.clConnectorID.HeaderText = "Port";
            this.clConnectorID.Name = "clConnectorID";
            this.clConnectorID.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeviceLibrary);
            this.groupBox1.Controls.Add(this.labelX10);
            this.groupBox1.Controls.Add(this.labelX9);
            this.groupBox1.Controls.Add(this.labelX8);
            this.groupBox1.Controls.Add(this.labelX7);
            this.groupBox1.Controls.Add(this.labelX6);
            this.groupBox1.Controls.Add(this.cboDeviceClass);
            this.groupBox1.Controls.Add(this.cboDeviceNameSpace);
            this.groupBox1.Controls.Add(this.txtComputerName);
            this.groupBox1.Controls.Add(this.txtConnectorId);
            this.groupBox1.Controls.Add(this.txtDeviceLib);
            this.groupBox1.Location = new System.Drawing.Point(2, 204);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 336);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // btnDeviceLibrary
            // 
            this.btnDeviceLibrary.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeviceLibrary.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDeviceLibrary.Location = new System.Drawing.Point(197, 10);
            this.btnDeviceLibrary.Name = "btnDeviceLibrary";
            this.btnDeviceLibrary.Size = new System.Drawing.Size(84, 29);
            this.btnDeviceLibrary.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDeviceLibrary.TabIndex = 31;
            this.btnDeviceLibrary.Text = "Open";
            this.btnDeviceLibrary.Click += new System.EventHandler(this.btnDeviceLibrary_Click);
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.Class = "";
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(8, 224);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(75, 23);
            this.labelX10.TabIndex = 29;
            this.labelX10.Text = "Connector ID";
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.Class = "";
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(8, 170);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(112, 23);
            this.labelX9.TabIndex = 28;
            this.labelX9.Text = "Computer Name";
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.Class = "";
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(8, 119);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(75, 23);
            this.labelX8.TabIndex = 27;
            this.labelX8.Text = "Device Class";
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.Class = "";
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(8, 68);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(112, 23);
            this.labelX7.TabIndex = 26;
            this.labelX7.Text = "Device Name Space";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.Class = "";
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(8, 16);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(75, 23);
            this.labelX6.TabIndex = 25;
            this.labelX6.Text = "Device Library";
            // 
            // cboDeviceClass
            // 
            this.cboDeviceClass.DisplayMember = "Text";
            this.cboDeviceClass.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDeviceClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeviceClass.FormattingEnabled = true;
            this.cboDeviceClass.ItemHeight = 14;
            this.cboDeviceClass.Location = new System.Drawing.Point(28, 144);
            this.cboDeviceClass.Name = "cboDeviceClass";
            this.cboDeviceClass.Size = new System.Drawing.Size(266, 20);
            this.cboDeviceClass.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDeviceClass.TabIndex = 24;
            // 
            // cboDeviceNameSpace
            // 
            this.cboDeviceNameSpace.DisplayMember = "Text";
            this.cboDeviceNameSpace.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDeviceNameSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeviceNameSpace.FormattingEnabled = true;
            this.cboDeviceNameSpace.ItemHeight = 14;
            this.cboDeviceNameSpace.Location = new System.Drawing.Point(28, 93);
            this.cboDeviceNameSpace.Name = "cboDeviceNameSpace";
            this.cboDeviceNameSpace.Size = new System.Drawing.Size(266, 20);
            this.cboDeviceNameSpace.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDeviceNameSpace.TabIndex = 23;
            // 
            // txtComputerName
            // 
            // 
            // 
            // 
            this.txtComputerName.Border.Class = "TextBoxBorder";
            this.txtComputerName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtComputerName.Location = new System.Drawing.Point(30, 198);
            this.txtComputerName.Name = "txtComputerName";
            this.txtComputerName.Size = new System.Drawing.Size(251, 20);
            this.txtComputerName.TabIndex = 22;
            // 
            // txtConnectorId
            // 
            // 
            // 
            // 
            this.txtConnectorId.Border.Class = "TextBoxBorder";
            this.txtConnectorId.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtConnectorId.Location = new System.Drawing.Point(29, 252);
            this.txtConnectorId.Name = "txtConnectorId";
            this.txtConnectorId.Size = new System.Drawing.Size(252, 20);
            this.txtConnectorId.TabIndex = 20;
            // 
            // txtDeviceLib
            // 
            // 
            // 
            // 
            this.txtDeviceLib.Border.Class = "TextBoxBorder";
            this.txtDeviceLib.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDeviceLib.Location = new System.Drawing.Point(29, 42);
            this.txtDeviceLib.Name = "txtDeviceLib";
            this.txtDeviceLib.Size = new System.Drawing.Size(252, 20);
            this.txtDeviceLib.TabIndex = 19;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(992, 524);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 24;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmDeviceList
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(217)))), ((int)(((byte)(247)))));
            this.ClientSize = new System.Drawing.Size(1160, 552);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupPanel2);
            this.Controls.Add(this.grpInfor);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnExit);
            this.DoubleBuffered = true;
            this.Name = "frmDeviceList";
            this.Text = "frmDeviceList";
            this.Load += new System.EventHandler(this.frmDeviceList_Load);
            this.grpInfor.ResumeLayout(false);
            this.grDeviceConnectorType.ResumeLayout(false);
            this.grDeviceConnectorType.PerformLayout();
            this.grProtocol.ResumeLayout(false);
            this.grProtocol.PerformLayout();
            this.grStatus.ResumeLayout(false);
            this.grStatus.PerformLayout();
            this.groupPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDevices)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnExit;
        private DevComponents.DotNetBar.ButtonX btnDelete;
        private DevComponents.DotNetBar.ButtonX btnEdit;
        private DevComponents.DotNetBar.ButtonX btnAdd;
        private DevComponents.DotNetBar.Controls.GroupPanel grpInfor;
        private System.Windows.Forms.GroupBox grDeviceConnectorType;
        private System.Windows.Forms.RadioButton rdTCPIP;
        private System.Windows.Forms.RadioButton rdR232;
        private System.Windows.Forms.GroupBox grProtocol;
        private System.Windows.Forms.RadioButton rdTwo;
        private System.Windows.Forms.RadioButton rdOne;
        private System.Windows.Forms.GroupBox grStatus;
        private System.Windows.Forms.RadioButton rdUnused;
        private System.Windows.Forms.RadioButton rdUsed;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboManufacturer;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboPort;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTest;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDesc;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDeviceName;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdDevices;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.ButtonX btnDeviceLibrary;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDeviceClass;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDeviceNameSpace;
        private DevComponents.DotNetBar.Controls.TextBoxX txtComputerName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDeviceID;
        private DevComponents.DotNetBar.Controls.TextBoxX txtConnectorId;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDeviceLib;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDevice_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTestType_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTestName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDevice_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn clManufacture_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn clManufacture_Name;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clValid;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clProtocol;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDeviceLibrary;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDeviceNameSpace;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDeviceClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn clComputerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDeviceConnectorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn clConnectorID;
    }
}