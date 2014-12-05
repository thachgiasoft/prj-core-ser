namespace Vietbait.DemoProject
{
    partial class TestDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestDataForm));
            this.com = new System.IO.Ports.SerialPort(this.components);
            this.btnketnoi = new System.Windows.Forms.Button();
            this.grbconnection_setting = new System.Windows.Forms.GroupBox();
            this.txtTcpLog = new System.Windows.Forms.TextBox();
            this.tabConnectionType = new System.Windows.Forms.TabControl();
            this.tabCom = new System.Windows.Forms.TabPage();
            this.grbComPort = new System.Windows.Forms.GroupBox();
            this.btnRefreshPort = new System.Windows.Forms.Button();
            this.lbsCom = new System.Windows.Forms.ComboBox();
            this.grbBaudRate = new System.Windows.Forms.GroupBox();
            this.cboBaudRate = new System.Windows.Forms.ComboBox();
            this.grbStopbit = new System.Windows.Forms.GroupBox();
            this.cboStopBit = new System.Windows.Forms.ComboBox();
            this.grbParity = new System.Windows.Forms.GroupBox();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.grbhandshacking = new System.Windows.Forms.GroupBox();
            this.cboHandShacking = new System.Windows.Forms.ComboBox();
            this.grbDataBits = new System.Windows.Forms.GroupBox();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.tabTpcIpServer = new System.Windows.Forms.TabPage();
            this.btnShowHideTcpLog = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboIpAddress = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.cbxShowSpace = new System.Windows.Forms.CheckBox();
            this.cbxShowAllCharacter = new System.Windows.Forms.CheckBox();
            this.cbxWordWrap = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtLF = new System.Windows.Forms.RadioButton();
            this.rbtCr = new System.Windows.Forms.RadioButton();
            this.rbtCrlf = new System.Windows.Forms.RadioButton();
            this.chkTopMode = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLengthAndLines = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentPosition = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.grbDulieunhan = new System.Windows.Forms.GroupBox();
            this.txtDlNhan = new ScintillaNET.Scintilla();
            this.grbDulieugui = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtGui = new ScintillaNET.Scintilla();
            this.chkAstm = new System.Windows.Forms.CheckBox();
            this.btnEtb = new System.Windows.Forms.Button();
            this.btnLf = new System.Windows.Forms.Button();
            this.btnCr = new System.Windows.Forms.Button();
            this.btnEOT = new System.Windows.Forms.Button();
            this.btnEnq = new System.Windows.Forms.Button();
            this.btnGui = new System.Windows.Forms.Button();
            this.btnNak = new System.Windows.Forms.Button();
            this.btnAck = new System.Windows.Forms.Button();
            this.txtDlGui = new ScintillaNET.Scintilla();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tolButton = new System.Windows.Forms.ToolTip(this.components);
            this.grbconnection_setting.SuspendLayout();
            this.tabConnectionType.SuspendLayout();
            this.tabCom.SuspendLayout();
            this.grbComPort.SuspendLayout();
            this.grbBaudRate.SuspendLayout();
            this.grbStopbit.SuspendLayout();
            this.grbParity.SuspendLayout();
            this.grbhandshacking.SuspendLayout();
            this.grbDataBits.SuspendLayout();
            this.tabTpcIpServer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grbDulieunhan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDlNhan)).BeginInit();
            this.grbDulieugui.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGui)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDlGui)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // com
            // 
            this.com.RtsEnable = true;
            this.com.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.ComDataReceived);
            // 
            // btnketnoi
            // 
            this.btnketnoi.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnketnoi.Location = new System.Drawing.Point(562, 38);
            this.btnketnoi.Name = "btnketnoi";
            this.btnketnoi.Size = new System.Drawing.Size(73, 31);
            this.btnketnoi.TabIndex = 3;
            this.btnketnoi.Tag = "0";
            this.btnketnoi.Text = "&Kết Nối";
            this.btnketnoi.UseVisualStyleBackColor = true;
            this.btnketnoi.Click += new System.EventHandler(this.BtnketnoiClick);
            // 
            // grbconnection_setting
            // 
            this.grbconnection_setting.BackColor = System.Drawing.Color.Transparent;
            this.grbconnection_setting.Controls.Add(this.txtTcpLog);
            this.grbconnection_setting.Controls.Add(this.tabConnectionType);
            this.grbconnection_setting.Controls.Add(this.btnClearAll);
            this.grbconnection_setting.Controls.Add(this.btnketnoi);
            this.grbconnection_setting.Controls.Add(this.cbxShowSpace);
            this.grbconnection_setting.Controls.Add(this.cbxShowAllCharacter);
            this.grbconnection_setting.Controls.Add(this.cbxWordWrap);
            this.grbconnection_setting.Controls.Add(this.groupBox1);
            this.grbconnection_setting.Controls.Add(this.chkTopMode);
            this.grbconnection_setting.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbconnection_setting.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbconnection_setting.Location = new System.Drawing.Point(0, 0);
            this.grbconnection_setting.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbconnection_setting.Name = "grbconnection_setting";
            this.grbconnection_setting.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grbconnection_setting.Size = new System.Drawing.Size(1008, 122);
            this.grbconnection_setting.TabIndex = 2;
            this.grbconnection_setting.TabStop = false;
            this.grbconnection_setting.Text = "Thiết lập cấu hình kết nối";
            // 
            // txtTcpLog
            // 
            this.txtTcpLog.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTcpLog.Location = new System.Drawing.Point(558, 12);
            this.txtTcpLog.Multiline = true;
            this.txtTcpLog.Name = "txtTcpLog";
            this.txtTcpLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTcpLog.Size = new System.Drawing.Size(444, 105);
            this.txtTcpLog.TabIndex = 5;
            this.txtTcpLog.Visible = false;
            // 
            // tabConnectionType
            // 
            this.tabConnectionType.Controls.Add(this.tabCom);
            this.tabConnectionType.Controls.Add(this.tabTpcIpServer);
            this.tabConnectionType.Location = new System.Drawing.Point(7, 17);
            this.tabConnectionType.Name = "tabConnectionType";
            this.tabConnectionType.SelectedIndex = 0;
            this.tabConnectionType.Size = new System.Drawing.Size(549, 95);
            this.tabConnectionType.TabIndex = 8;
            // 
            // tabCom
            // 
            this.tabCom.BackColor = System.Drawing.SystemColors.Control;
            this.tabCom.Controls.Add(this.grbComPort);
            this.tabCom.Controls.Add(this.grbBaudRate);
            this.tabCom.Controls.Add(this.grbStopbit);
            this.tabCom.Controls.Add(this.grbParity);
            this.tabCom.Controls.Add(this.grbhandshacking);
            this.tabCom.Controls.Add(this.grbDataBits);
            this.tabCom.Location = new System.Drawing.Point(4, 23);
            this.tabCom.Name = "tabCom";
            this.tabCom.Padding = new System.Windows.Forms.Padding(3);
            this.tabCom.Size = new System.Drawing.Size(541, 68);
            this.tabCom.TabIndex = 0;
            this.tabCom.Text = "COM";
            // 
            // grbComPort
            // 
            this.grbComPort.BackColor = System.Drawing.Color.Transparent;
            this.grbComPort.Controls.Add(this.btnRefreshPort);
            this.grbComPort.Controls.Add(this.lbsCom);
            this.grbComPort.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbComPort.Location = new System.Drawing.Point(6, 6);
            this.grbComPort.Name = "grbComPort";
            this.grbComPort.Size = new System.Drawing.Size(100, 49);
            this.grbComPort.TabIndex = 0;
            this.grbComPort.TabStop = false;
            this.grbComPort.Text = "Com Port";
            // 
            // btnRefreshPort
            // 
            this.btnRefreshPort.Location = new System.Drawing.Point(64, 0);
            this.btnRefreshPort.Name = "btnRefreshPort";
            this.btnRefreshPort.Size = new System.Drawing.Size(34, 20);
            this.btnRefreshPort.TabIndex = 9;
            this.btnRefreshPort.Text = "(...)";
            this.btnRefreshPort.UseVisualStyleBackColor = true;
            this.btnRefreshPort.Click += new System.EventHandler(this.btnRefreshPort_Click);
            // 
            // lbsCom
            // 
            this.lbsCom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbsCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lbsCom.FormattingEnabled = true;
            this.lbsCom.Location = new System.Drawing.Point(3, 16);
            this.lbsCom.Name = "lbsCom";
            this.lbsCom.Size = new System.Drawing.Size(94, 22);
            this.lbsCom.TabIndex = 0;
            // 
            // grbBaudRate
            // 
            this.grbBaudRate.BackColor = System.Drawing.Color.Transparent;
            this.grbBaudRate.Controls.Add(this.cboBaudRate);
            this.grbBaudRate.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbBaudRate.Location = new System.Drawing.Point(112, 6);
            this.grbBaudRate.Name = "grbBaudRate";
            this.grbBaudRate.Size = new System.Drawing.Size(79, 49);
            this.grbBaudRate.TabIndex = 0;
            this.grbBaudRate.TabStop = false;
            this.grbBaudRate.Text = "Baud Rate";
            // 
            // cboBaudRate
            // 
            this.cboBaudRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboBaudRate.FormattingEnabled = true;
            this.cboBaudRate.Items.AddRange(new object[] {
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "56000",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.cboBaudRate.Location = new System.Drawing.Point(3, 16);
            this.cboBaudRate.Name = "cboBaudRate";
            this.cboBaudRate.Size = new System.Drawing.Size(73, 22);
            this.cboBaudRate.TabIndex = 0;
            // 
            // grbStopbit
            // 
            this.grbStopbit.BackColor = System.Drawing.Color.Transparent;
            this.grbStopbit.Controls.Add(this.cboStopBit);
            this.grbStopbit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbStopbit.Location = new System.Drawing.Point(344, 6);
            this.grbStopbit.Name = "grbStopbit";
            this.grbStopbit.Size = new System.Drawing.Size(78, 49);
            this.grbStopbit.TabIndex = 0;
            this.grbStopbit.TabStop = false;
            this.grbStopbit.Text = "Stops Bits";
            // 
            // cboStopBit
            // 
            this.cboStopBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStopBit.FormattingEnabled = true;
            this.cboStopBit.Location = new System.Drawing.Point(3, 16);
            this.cboStopBit.Name = "cboStopBit";
            this.cboStopBit.Size = new System.Drawing.Size(72, 22);
            this.cboStopBit.TabIndex = 1;
            // 
            // grbParity
            // 
            this.grbParity.BackColor = System.Drawing.Color.Transparent;
            this.grbParity.Controls.Add(this.cboParity);
            this.grbParity.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbParity.Location = new System.Drawing.Point(275, 6);
            this.grbParity.Name = "grbParity";
            this.grbParity.Size = new System.Drawing.Size(63, 49);
            this.grbParity.TabIndex = 0;
            this.grbParity.TabStop = false;
            this.grbParity.Text = "Parity";
            // 
            // cboParity
            // 
            this.cboParity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParity.FormattingEnabled = true;
            this.cboParity.Location = new System.Drawing.Point(3, 16);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(57, 22);
            this.cboParity.TabIndex = 1;
            // 
            // grbhandshacking
            // 
            this.grbhandshacking.BackColor = System.Drawing.Color.Transparent;
            this.grbhandshacking.Controls.Add(this.cboHandShacking);
            this.grbhandshacking.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbhandshacking.Location = new System.Drawing.Point(428, 6);
            this.grbhandshacking.Name = "grbhandshacking";
            this.grbhandshacking.Size = new System.Drawing.Size(104, 49);
            this.grbhandshacking.TabIndex = 0;
            this.grbhandshacking.TabStop = false;
            this.grbhandshacking.Text = "HandShacking";
            // 
            // cboHandShacking
            // 
            this.cboHandShacking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboHandShacking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHandShacking.FormattingEnabled = true;
            this.cboHandShacking.Items.AddRange(new object[] {
            "None",
            "RTS/CTR",
            "XON/XOFF",
            "RTS/CTRS + XON/XOFF",
            "RTS on XT"});
            this.cboHandShacking.Location = new System.Drawing.Point(3, 16);
            this.cboHandShacking.Name = "cboHandShacking";
            this.cboHandShacking.Size = new System.Drawing.Size(98, 22);
            this.cboHandShacking.TabIndex = 1;
            // 
            // grbDataBits
            // 
            this.grbDataBits.BackColor = System.Drawing.Color.Transparent;
            this.grbDataBits.Controls.Add(this.cboDataBits);
            this.grbDataBits.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDataBits.Location = new System.Drawing.Point(197, 6);
            this.grbDataBits.Name = "grbDataBits";
            this.grbDataBits.Size = new System.Drawing.Size(72, 49);
            this.grbDataBits.TabIndex = 1;
            this.grbDataBits.TabStop = false;
            this.grbDataBits.Text = "Data Bits";
            // 
            // cboDataBits
            // 
            this.cboDataBits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBits.FormattingEnabled = true;
            this.cboDataBits.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cboDataBits.Location = new System.Drawing.Point(3, 16);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(66, 22);
            this.cboDataBits.TabIndex = 1;
            // 
            // tabTpcIpServer
            // 
            this.tabTpcIpServer.BackColor = System.Drawing.SystemColors.Control;
            this.tabTpcIpServer.Controls.Add(this.btnShowHideTcpLog);
            this.tabTpcIpServer.Controls.Add(this.txtPort);
            this.tabTpcIpServer.Controls.Add(this.label2);
            this.tabTpcIpServer.Controls.Add(this.cboIpAddress);
            this.tabTpcIpServer.Controls.Add(this.label1);
            this.tabTpcIpServer.Location = new System.Drawing.Point(4, 23);
            this.tabTpcIpServer.Name = "tabTpcIpServer";
            this.tabTpcIpServer.Padding = new System.Windows.Forms.Padding(3);
            this.tabTpcIpServer.Size = new System.Drawing.Size(541, 68);
            this.tabTpcIpServer.TabIndex = 1;
            this.tabTpcIpServer.Text = "TCP/IP Server";
            this.tabTpcIpServer.Enter += new System.EventHandler(this.tabTpcIpServer_Enter);
            // 
            // btnShowHideTcpLog
            // 
            this.btnShowHideTcpLog.Location = new System.Drawing.Point(352, 42);
            this.btnShowHideTcpLog.Name = "btnShowHideTcpLog";
            this.btnShowHideTcpLog.Size = new System.Drawing.Size(183, 23);
            this.btnShowHideTcpLog.TabIndex = 4;
            this.btnShowHideTcpLog.Text = "Show / Hide TCP Log";
            this.btnShowHideTcpLog.UseVisualStyleBackColor = true;
            this.btnShowHideTcpLog.Click += new System.EventHandler(this.btnShowHideTcpLog_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(275, 11);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(71, 20);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "1000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // cboIpAddress
            // 
            this.cboIpAddress.FormattingEnabled = true;
            this.cboIpAddress.Location = new System.Drawing.Point(72, 9);
            this.cboIpAddress.Name = "cboIpAddress";
            this.cboIpAddress.Size = new System.Drawing.Size(162, 22);
            this.cboIpAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearAll.Location = new System.Drawing.Point(562, 75);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(73, 33);
            this.btnClearAll.TabIndex = 9;
            this.btnClearAll.Tag = "0";
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // cbxShowSpace
            // 
            this.cbxShowSpace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxShowSpace.AutoSize = true;
            this.cbxShowSpace.Checked = true;
            this.cbxShowSpace.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxShowSpace.Location = new System.Drawing.Point(886, 31);
            this.cbxShowSpace.Name = "cbxShowSpace";
            this.cbxShowSpace.Size = new System.Drawing.Size(88, 18);
            this.cbxShowSpace.TabIndex = 8;
            this.cbxShowSpace.Text = "Show space";
            this.cbxShowSpace.UseVisualStyleBackColor = true;
            this.cbxShowSpace.CheckedChanged += new System.EventHandler(this.cbxShowSpace_CheckedChanged);
            // 
            // cbxShowAllCharacter
            // 
            this.cbxShowAllCharacter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxShowAllCharacter.AutoSize = true;
            this.cbxShowAllCharacter.Checked = true;
            this.cbxShowAllCharacter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxShowAllCharacter.Location = new System.Drawing.Point(886, 58);
            this.cbxShowAllCharacter.Name = "cbxShowAllCharacter";
            this.cbxShowAllCharacter.Size = new System.Drawing.Size(118, 18);
            this.cbxShowAllCharacter.TabIndex = 7;
            this.cbxShowAllCharacter.Text = "Show all character";
            this.cbxShowAllCharacter.UseVisualStyleBackColor = true;
            this.cbxShowAllCharacter.CheckedChanged += new System.EventHandler(this.CbxShowAllCharacterCheckedChanged);
            // 
            // cbxWordWrap
            // 
            this.cbxWordWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbxWordWrap.AutoSize = true;
            this.cbxWordWrap.Location = new System.Drawing.Point(796, 58);
            this.cbxWordWrap.Name = "cbxWordWrap";
            this.cbxWordWrap.Size = new System.Drawing.Size(81, 18);
            this.cbxWordWrap.TabIndex = 6;
            this.cbxWordWrap.Text = "Word wrap";
            this.cbxWordWrap.UseVisualStyleBackColor = true;
            this.cbxWordWrap.CheckedChanged += new System.EventHandler(this.cbxWordWrapCheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.rbtLF);
            this.groupBox1.Controls.Add(this.rbtCr);
            this.groupBox1.Controls.Add(this.rbtCrlf);
            this.groupBox1.Location = new System.Drawing.Point(641, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(65, 96);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EOL";
            // 
            // rbtLF
            // 
            this.rbtLF.AutoSize = true;
            this.rbtLF.Location = new System.Drawing.Point(6, 64);
            this.rbtLF.Name = "rbtLF";
            this.rbtLF.Size = new System.Drawing.Size(37, 18);
            this.rbtLF.TabIndex = 0;
            this.rbtLF.Text = "LF";
            this.rbtLF.UseVisualStyleBackColor = true;
            // 
            // rbtCr
            // 
            this.rbtCr.AutoSize = true;
            this.rbtCr.Location = new System.Drawing.Point(6, 40);
            this.rbtCr.Name = "rbtCr";
            this.rbtCr.Size = new System.Drawing.Size(39, 18);
            this.rbtCr.TabIndex = 0;
            this.rbtCr.Text = "CR";
            this.rbtCr.UseVisualStyleBackColor = true;
            // 
            // rbtCrlf
            // 
            this.rbtCrlf.AutoSize = true;
            this.rbtCrlf.Checked = true;
            this.rbtCrlf.Location = new System.Drawing.Point(6, 16);
            this.rbtCrlf.Name = "rbtCrlf";
            this.rbtCrlf.Size = new System.Drawing.Size(51, 18);
            this.rbtCrlf.TabIndex = 0;
            this.rbtCrlf.TabStop = true;
            this.rbtCrlf.Text = "CRLF";
            this.rbtCrlf.UseVisualStyleBackColor = true;
            // 
            // chkTopMode
            // 
            this.chkTopMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTopMode.AutoSize = true;
            this.chkTopMode.Location = new System.Drawing.Point(796, 31);
            this.chkTopMode.Name = "chkTopMode";
            this.chkTopMode.Size = new System.Drawing.Size(72, 18);
            this.chkTopMode.TabIndex = 4;
            this.chkTopMode.Text = "Top Mode";
            this.chkTopMode.UseVisualStyleBackColor = true;
            this.chkTopMode.CheckedChanged += new System.EventHandler(this.ChkTopModeCheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripStatusLabel1,
            this.lblLengthAndLines,
            this.toolStripStatusLabel2,
            this.lblCurrentPosition,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(261, 17);
            this.lblStatus.Text = "Nhấn nút kết nối để bắt đầu truyền nhận dữ liệu";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel1.Text = "     |";
            // 
            // lblLengthAndLines
            // 
            this.lblLengthAndLines.Name = "lblLengthAndLines";
            this.lblLengthAndLines.Size = new System.Drawing.Size(110, 17);
            this.lblLengthAndLines.Text = "Total Lines: Length:";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel2.Text = "     |";
            // 
            // lblCurrentPosition
            // 
            this.lblCurrentPosition.Name = "lblCurrentPosition";
            this.lblCurrentPosition.Size = new System.Drawing.Size(96, 17);
            this.lblCurrentPosition.Text = "Current position:";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(25, 17);
            this.toolStripStatusLabel3.Text = "     |";
            // 
            // grbDulieunhan
            // 
            this.grbDulieunhan.Controls.Add(this.txtDlNhan);
            this.grbDulieunhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDulieunhan.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grbDulieunhan.Location = new System.Drawing.Point(0, 0);
            this.grbDulieunhan.Name = "grbDulieunhan";
            this.grbDulieunhan.Size = new System.Drawing.Size(347, 476);
            this.grbDulieunhan.TabIndex = 5;
            this.grbDulieunhan.TabStop = false;
            this.grbDulieunhan.Text = "Dữ liệu nhận";
            this.grbDulieunhan.UseCompatibleTextRendering = true;
            // 
            // txtDlNhan
            // 
            this.txtDlNhan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDlNhan.EndOfLine.IsVisible = true;
            this.txtDlNhan.Folding.IsEnabled = false;
            this.txtDlNhan.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDlNhan.Location = new System.Drawing.Point(3, 16);
            this.txtDlNhan.Margins.Margin0.Width = 35;
            this.txtDlNhan.Margins.Margin2.IsClickable = false;
            this.txtDlNhan.Margins.Margin2.IsFoldMargin = false;
            this.txtDlNhan.Name = "txtDlNhan";
            this.txtDlNhan.Scrolling.HorizontalWidth = 100;
            this.txtDlNhan.Size = new System.Drawing.Size(341, 454);
            this.txtDlNhan.Styles.Bits = 5;
            this.txtDlNhan.Styles.BraceBad.Size = 9F;
            this.txtDlNhan.Styles.BraceLight.Size = 9F;
            this.txtDlNhan.Styles.ControlChar.Size = 9F;
            this.txtDlNhan.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.txtDlNhan.Styles.Default.Size = 9F;
            this.txtDlNhan.Styles.IndentGuide.Size = 9F;
            this.txtDlNhan.Styles.LastPredefined.Size = 9F;
            this.txtDlNhan.Styles.LineNumber.CharacterSet = ScintillaNET.CharacterSet.Ansi;
            this.txtDlNhan.Styles.LineNumber.Size = 8.25F;
            this.txtDlNhan.Styles.Max.Size = 9F;
            this.txtDlNhan.TabIndex = 8;
            this.txtDlNhan.Whitespace.Mode = ScintillaNET.WhitespaceMode.VisibleAlways;
            this.txtDlNhan.Enter += new System.EventHandler(this.EditorEnter);
            // 
            // grbDulieugui
            // 
            this.grbDulieugui.BackColor = System.Drawing.Color.Transparent;
            this.grbDulieugui.Controls.Add(this.splitContainer2);
            this.grbDulieugui.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbDulieugui.Enabled = false;
            this.grbDulieugui.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbDulieugui.Location = new System.Drawing.Point(0, 0);
            this.grbDulieugui.Name = "grbDulieugui";
            this.grbDulieugui.Size = new System.Drawing.Size(654, 476);
            this.grbDulieugui.TabIndex = 6;
            this.grbDulieugui.TabStop = false;
            this.grbDulieugui.Text = "Dữ liệu gửi";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtGui);
            this.splitContainer2.Panel1.Controls.Add(this.chkAstm);
            this.splitContainer2.Panel1.Controls.Add(this.btnEtb);
            this.splitContainer2.Panel1.Controls.Add(this.btnLf);
            this.splitContainer2.Panel1.Controls.Add(this.btnCr);
            this.splitContainer2.Panel1.Controls.Add(this.btnEOT);
            this.splitContainer2.Panel1.Controls.Add(this.btnEnq);
            this.splitContainer2.Panel1.Controls.Add(this.btnGui);
            this.splitContainer2.Panel1.Controls.Add(this.btnNak);
            this.splitContainer2.Panel1.Controls.Add(this.btnAck);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtDlGui);
            this.splitContainer2.Size = new System.Drawing.Size(648, 457);
            this.splitContainer2.SplitterDistance = 115;
            this.splitContainer2.TabIndex = 8;
            // 
            // txtGui
            // 
            this.txtGui.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGui.EndOfLine.IsVisible = true;
            this.txtGui.Folding.IsEnabled = false;
            this.txtGui.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGui.Location = new System.Drawing.Point(0, 27);
            this.txtGui.Margins.Margin0.Width = 35;
            this.txtGui.Margins.Margin2.IsClickable = false;
            this.txtGui.Margins.Margin2.IsFoldMargin = false;
            this.txtGui.Name = "txtGui";
            this.txtGui.Scrolling.HorizontalWidth = 100;
            this.txtGui.Size = new System.Drawing.Size(645, 85);
            this.txtGui.Styles.Bits = 5;
            this.txtGui.Styles.BraceBad.Size = 9F;
            this.txtGui.Styles.BraceLight.Size = 9F;
            this.txtGui.Styles.ControlChar.Size = 9F;
            this.txtGui.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.txtGui.Styles.Default.Size = 9F;
            this.txtGui.Styles.IndentGuide.Size = 9F;
            this.txtGui.Styles.LastPredefined.Size = 9F;
            this.txtGui.Styles.LineNumber.CharacterSet = ScintillaNET.CharacterSet.Ansi;
            this.txtGui.Styles.LineNumber.Size = 8.25F;
            this.txtGui.Styles.Max.Size = 9F;
            this.txtGui.TabIndex = 8;
            this.txtGui.Whitespace.Mode = ScintillaNET.WhitespaceMode.VisibleAlways;
            this.txtGui.Enter += new System.EventHandler(this.EditorEnter);
            this.txtGui.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TxtGuiKeyUp);
            // 
            // chkAstm
            // 
            this.chkAstm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAstm.AutoSize = true;
            this.chkAstm.Location = new System.Drawing.Point(514, 6);
            this.chkAstm.Name = "chkAstm";
            this.chkAstm.Size = new System.Drawing.Size(80, 18);
            this.chkAstm.TabIndex = 7;
            this.chkAstm.Text = "ASTM Data";
            this.chkAstm.UseVisualStyleBackColor = true;
            // 
            // btnEtb
            // 
            this.btnEtb.Location = new System.Drawing.Point(165, 3);
            this.btnEtb.Name = "btnEtb";
            this.btnEtb.Size = new System.Drawing.Size(47, 23);
            this.btnEtb.TabIndex = 6;
            this.btnEtb.Tag = "23";
            this.btnEtb.Text = "ETB";
            this.tolButton.SetToolTip(this.btnEtb, "ETB");
            this.btnEtb.UseVisualStyleBackColor = true;
            // 
            // btnLf
            // 
            this.btnLf.Location = new System.Drawing.Point(321, 3);
            this.btnLf.Name = "btnLf";
            this.btnLf.Size = new System.Drawing.Size(47, 23);
            this.btnLf.TabIndex = 6;
            this.btnLf.Tag = "10";
            this.btnLf.Text = "LF";
            this.tolButton.SetToolTip(this.btnLf, "LF");
            this.btnLf.UseVisualStyleBackColor = true;
            // 
            // btnCr
            // 
            this.btnCr.Location = new System.Drawing.Point(268, 3);
            this.btnCr.Name = "btnCr";
            this.btnCr.Size = new System.Drawing.Size(47, 23);
            this.btnCr.TabIndex = 6;
            this.btnCr.Tag = "13";
            this.btnCr.Text = "CR";
            this.tolButton.SetToolTip(this.btnCr, "CR");
            this.btnCr.UseVisualStyleBackColor = true;
            // 
            // btnEOT
            // 
            this.btnEOT.Location = new System.Drawing.Point(218, 3);
            this.btnEOT.Name = "btnEOT";
            this.btnEOT.Size = new System.Drawing.Size(47, 23);
            this.btnEOT.TabIndex = 6;
            this.btnEOT.Tag = "4";
            this.btnEOT.Text = "EOT";
            this.tolButton.SetToolTip(this.btnEOT, "EOT");
            this.btnEOT.UseVisualStyleBackColor = true;
            // 
            // btnEnq
            // 
            this.btnEnq.Location = new System.Drawing.Point(6, 3);
            this.btnEnq.Name = "btnEnq";
            this.btnEnq.Size = new System.Drawing.Size(47, 23);
            this.btnEnq.TabIndex = 6;
            this.btnEnq.Tag = "5";
            this.btnEnq.Text = "ENQ";
            this.tolButton.SetToolTip(this.btnEnq, "ENQ");
            this.btnEnq.UseVisualStyleBackColor = true;
            // 
            // btnGui
            // 
            this.btnGui.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGui.Location = new System.Drawing.Point(594, 3);
            this.btnGui.Name = "btnGui";
            this.btnGui.Size = new System.Drawing.Size(51, 23);
            this.btnGui.TabIndex = 1;
            this.btnGui.Tag = "0";
            this.btnGui.Text = "&Gửi";
            this.btnGui.UseVisualStyleBackColor = true;
            this.btnGui.Click += new System.EventHandler(this.BtnGuiClick);
            // 
            // btnNak
            // 
            this.btnNak.Location = new System.Drawing.Point(112, 3);
            this.btnNak.Name = "btnNak";
            this.btnNak.Size = new System.Drawing.Size(47, 23);
            this.btnNak.TabIndex = 6;
            this.btnNak.Tag = "21";
            this.btnNak.Text = "NAK";
            this.tolButton.SetToolTip(this.btnNak, "NAK");
            this.btnNak.UseVisualStyleBackColor = true;
            // 
            // btnAck
            // 
            this.btnAck.Location = new System.Drawing.Point(59, 3);
            this.btnAck.Name = "btnAck";
            this.btnAck.Size = new System.Drawing.Size(47, 23);
            this.btnAck.TabIndex = 6;
            this.btnAck.Tag = "6";
            this.btnAck.Text = "ACK";
            this.tolButton.SetToolTip(this.btnAck, "ACK");
            this.btnAck.UseVisualStyleBackColor = true;
            // 
            // txtDlGui
            // 
            this.txtDlGui.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDlGui.EndOfLine.IsVisible = true;
            this.txtDlGui.Folding.IsEnabled = false;
            this.txtDlGui.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDlGui.Location = new System.Drawing.Point(0, 0);
            this.txtDlGui.Margins.Margin0.Width = 35;
            this.txtDlGui.Margins.Margin2.IsClickable = false;
            this.txtDlGui.Margins.Margin2.IsFoldMargin = false;
            this.txtDlGui.Name = "txtDlGui";
            this.txtDlGui.Scrolling.HorizontalWidth = 100;
            this.txtDlGui.Size = new System.Drawing.Size(648, 338);
            this.txtDlGui.Styles.Bits = 5;
            this.txtDlGui.Styles.BraceBad.Size = 9F;
            this.txtDlGui.Styles.BraceLight.Size = 9F;
            this.txtDlGui.Styles.ControlChar.Size = 9F;
            this.txtDlGui.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.txtDlGui.Styles.Default.Size = 9F;
            this.txtDlGui.Styles.IndentGuide.Size = 9F;
            this.txtDlGui.Styles.LastPredefined.Size = 9F;
            this.txtDlGui.Styles.LineNumber.CharacterSet = ScintillaNET.CharacterSet.Ansi;
            this.txtDlGui.Styles.LineNumber.Size = 8.25F;
            this.txtDlGui.Styles.Max.Size = 9F;
            this.txtDlGui.TabIndex = 7;
            this.txtDlGui.Whitespace.Mode = ScintillaNET.WhitespaceMode.VisibleAlways;
            this.txtDlGui.Enter += new System.EventHandler(this.EditorEnter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 127);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grbDulieugui);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grbDulieunhan);
            this.splitContainer1.Size = new System.Drawing.Size(1005, 476);
            this.splitContainer1.SplitterDistance = 654;
            this.splitContainer1.TabIndex = 7;
            // 
            // tolButton
            // 
            this.tolButton.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.tolButton.ToolTipTitle = "Nhấn CTRL để thêm vào ô gửi DL";
            // 
            // TestDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 628);
            this.Controls.Add(this.grbconnection_setting);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestDataForm";
            this.Load += new System.EventHandler(this.TestDataFormLoad);
            this.grbconnection_setting.ResumeLayout(false);
            this.grbconnection_setting.PerformLayout();
            this.tabConnectionType.ResumeLayout(false);
            this.tabCom.ResumeLayout(false);
            this.grbComPort.ResumeLayout(false);
            this.grbBaudRate.ResumeLayout(false);
            this.grbStopbit.ResumeLayout(false);
            this.grbParity.ResumeLayout(false);
            this.grbhandshacking.ResumeLayout(false);
            this.grbDataBits.ResumeLayout(false);
            this.tabTpcIpServer.ResumeLayout(false);
            this.tabTpcIpServer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grbDulieunhan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtDlNhan)).EndInit();
            this.grbDulieugui.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtGui)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDlGui)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.IO.Ports.SerialPort com;
        internal System.Windows.Forms.Button btnketnoi;
        internal System.Windows.Forms.GroupBox grbconnection_setting;
        internal System.Windows.Forms.GroupBox grbDataBits;
        internal System.Windows.Forms.ComboBox cboDataBits;
        internal System.Windows.Forms.GroupBox grbhandshacking;
        internal System.Windows.Forms.ComboBox cboHandShacking;
        internal System.Windows.Forms.GroupBox grbParity;
        internal System.Windows.Forms.ComboBox cboParity;
        internal System.Windows.Forms.GroupBox grbStopbit;
        internal System.Windows.Forms.ComboBox cboStopBit;
        internal System.Windows.Forms.GroupBox grbBaudRate;
        internal System.Windows.Forms.ComboBox cboBaudRate;
        internal System.Windows.Forms.GroupBox grbComPort;
        internal System.Windows.Forms.ComboBox lbsCom;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.CheckBox chkTopMode;
        internal System.Windows.Forms.GroupBox grbDulieunhan;
        private ScintillaNET.Scintilla txtDlNhan;
        internal System.Windows.Forms.GroupBox grbDulieugui;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ScintillaNET.Scintilla txtGui;
        private System.Windows.Forms.CheckBox chkAstm;
        private System.Windows.Forms.Button btnEtb;
        private System.Windows.Forms.Button btnLf;
        private System.Windows.Forms.Button btnCr;
        private System.Windows.Forms.Button btnEOT;
        private System.Windows.Forms.Button btnEnq;
        internal System.Windows.Forms.Button btnGui;
        private System.Windows.Forms.Button btnNak;
        private System.Windows.Forms.Button btnAck;
        private ScintillaNET.Scintilla txtDlGui;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip tolButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbxWordWrap;
        private System.Windows.Forms.RadioButton rbtCrlf;
        private System.Windows.Forms.RadioButton rbtCr;
        private System.Windows.Forms.RadioButton rbtLF;
        private System.Windows.Forms.CheckBox cbxShowAllCharacter;
        private System.Windows.Forms.CheckBox cbxShowSpace;
        private System.Windows.Forms.ToolStripStatusLabel lblLengthAndLines;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentPosition;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.Button btnRefreshPort;
        internal System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.TabControl tabConnectionType;
        private System.Windows.Forms.TabPage tabCom;
        private System.Windows.Forms.TabPage tabTpcIpServer;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboIpAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShowHideTcpLog;
        private System.Windows.Forms.TextBox txtTcpLog;
    }
}