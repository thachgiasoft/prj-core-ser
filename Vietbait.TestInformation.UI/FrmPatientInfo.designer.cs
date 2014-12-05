namespace Vietbait.TestInformation.UI
{
    partial class FrmPatientInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPatientInfo));
            this.grbInformation = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grpInformationFK = new System.Windows.Forms.GroupBox();
            this.txtDiagnose = new System.Windows.Forms.TextBox();
            this.cboDepartment = new System.Windows.Forms.ComboBox();
            this.txtBed = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRoom = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtInvoiceNumber = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.grpInformationPK = new System.Windows.Forms.GroupBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInsurance = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cboObject = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboSex = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPID = new System.Windows.Forms.TextBox();
            this.txtYearOfBirth = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmdExit = new DevComponents.DotNetBar.ButtonX();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.grbInformation.SuspendLayout();
            this.grpInformationFK.SuspendLayout();
            this.grpInformationPK.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // grbInformation
            // 
            this.grbInformation.BackColor = System.Drawing.Color.Transparent;
            this.grbInformation.CanvasColor = System.Drawing.SystemColors.ActiveCaption;
            this.grbInformation.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.grbInformation.Controls.Add(this.grpInformationFK);
            this.grbInformation.Controls.Add(this.grpInformationPK);
            this.grbInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.grbInformation.Location = new System.Drawing.Point(0, 0);
            this.grbInformation.Margin = new System.Windows.Forms.Padding(4);
            this.grbInformation.Name = "grbInformation";
            this.grbInformation.Size = new System.Drawing.Size(873, 365);
            // 
            // 
            // 
            this.grbInformation.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.grbInformation.Style.BackColorGradientAngle = 90;
            this.grbInformation.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.grbInformation.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInformation.Style.BorderBottomWidth = 1;
            this.grbInformation.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.grbInformation.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInformation.Style.BorderLeftWidth = 1;
            this.grbInformation.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInformation.Style.BorderRightWidth = 1;
            this.grbInformation.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grbInformation.Style.BorderTopWidth = 1;
            this.grbInformation.Style.Class = "";
            this.grbInformation.Style.CornerDiameter = 4;
            this.grbInformation.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grbInformation.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grbInformation.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.grbInformation.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grbInformation.StyleMouseDown.Class = "";
            this.grbInformation.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grbInformation.StyleMouseOver.Class = "";
            this.grbInformation.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grbInformation.TabIndex = 0;
            this.grbInformation.Text = "Thông tin bệnh nhân";
            // 
            // grpInformationFK
            // 
            this.grpInformationFK.Controls.Add(this.txtDiagnose);
            this.grpInformationFK.Controls.Add(this.cboDepartment);
            this.grpInformationFK.Controls.Add(this.txtBed);
            this.grpInformationFK.Controls.Add(this.label14);
            this.grpInformationFK.Controls.Add(this.txtRoom);
            this.grpInformationFK.Controls.Add(this.label15);
            this.grpInformationFK.Controls.Add(this.label13);
            this.grpInformationFK.Controls.Add(this.label12);
            this.grpInformationFK.Controls.Add(this.txtInvoiceNumber);
            this.grpInformationFK.Controls.Add(this.label6);
            this.grpInformationFK.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInformationFK.Location = new System.Drawing.Point(433, 3);
            this.grpInformationFK.Name = "grpInformationFK";
            this.grpInformationFK.Size = new System.Drawing.Size(422, 321);
            this.grpInformationFK.TabIndex = 32;
            this.grpInformationFK.TabStop = false;
            this.grpInformationFK.Text = "&Thông tin thêm";
            // 
            // txtDiagnose
            // 
            this.txtDiagnose.Location = new System.Drawing.Point(117, 166);
            this.txtDiagnose.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiagnose.Multiline = true;
            this.txtDiagnose.Name = "txtDiagnose";
            this.txtDiagnose.Size = new System.Drawing.Size(298, 61);
            this.txtDiagnose.TabIndex = 4;
            // 
            // cboDepartment
            // 
            this.cboDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.Location = new System.Drawing.Point(117, 64);
            this.cboDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.Size = new System.Drawing.Size(298, 24);
            this.cboDepartment.TabIndex = 1;
            // 
            // txtBed
            // 
            this.txtBed.Location = new System.Drawing.Point(117, 135);
            this.txtBed.Margin = new System.Windows.Forms.Padding(4);
            this.txtBed.Name = "txtBed";
            this.txtBed.Size = new System.Drawing.Size(298, 24);
            this.txtBed.TabIndex = 3;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(48, 138);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 17);
            this.label14.TabIndex = 32;
            this.label14.Text = "Giường:";
            // 
            // txtRoom
            // 
            this.txtRoom.Location = new System.Drawing.Point(117, 99);
            this.txtRoom.Margin = new System.Windows.Forms.Padding(4);
            this.txtRoom.Name = "txtRoom";
            this.txtRoom.Size = new System.Drawing.Size(298, 24);
            this.txtRoom.TabIndex = 2;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(23, 169);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(88, 17);
            this.label15.TabIndex = 26;
            this.label15.Text = "Chẩn đoán:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(52, 102);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 17);
            this.label13.TabIndex = 31;
            this.label13.Text = "Buồng:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(62, 67);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 17);
            this.label12.TabIndex = 30;
            this.label12.Text = "Khoa:";
            // 
            // txtInvoiceNumber
            // 
            this.txtInvoiceNumber.Location = new System.Drawing.Point(117, 29);
            this.txtInvoiceNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtInvoiceNumber.Name = "txtInvoiceNumber";
            this.txtInvoiceNumber.Size = new System.Drawing.Size(298, 24);
            this.txtInvoiceNumber.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(45, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Số CMT:";
            // 
            // grpInformationPK
            // 
            this.grpInformationPK.Controls.Add(this.dtpDate);
            this.grpInformationPK.Controls.Add(this.label10);
            this.grpInformationPK.Controls.Add(this.txtAddress);
            this.grpInformationPK.Controls.Add(this.label7);
            this.grpInformationPK.Controls.Add(this.txtInsurance);
            this.grpInformationPK.Controls.Add(this.label11);
            this.grpInformationPK.Controls.Add(this.cboObject);
            this.grpInformationPK.Controls.Add(this.label3);
            this.grpInformationPK.Controls.Add(this.cboSex);
            this.grpInformationPK.Controls.Add(this.label9);
            this.grpInformationPK.Controls.Add(this.txtAge);
            this.grpInformationPK.Controls.Add(this.label8);
            this.grpInformationPK.Controls.Add(this.txtName);
            this.grpInformationPK.Controls.Add(this.label4);
            this.grpInformationPK.Controls.Add(this.txtPID);
            this.grpInformationPK.Controls.Add(this.txtYearOfBirth);
            this.grpInformationPK.Controls.Add(this.label5);
            this.grpInformationPK.Controls.Add(this.label2);
            this.grpInformationPK.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInformationPK.Location = new System.Drawing.Point(3, 3);
            this.grpInformationPK.Name = "grpInformationPK";
            this.grpInformationPK.Size = new System.Drawing.Size(424, 321);
            this.grpInformationPK.TabIndex = 31;
            this.grpInformationPK.TabStop = false;
            this.grpInformationPK.Text = "&Thông tin chính";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Enabled = false;
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(143, 36);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(156, 24);
            this.dtpDate.TabIndex = 50;
            this.dtpDate.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 42);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 17);
            this.label10.TabIndex = 49;
            this.label10.Text = "Ngày nhập:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(143, 212);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(265, 24);
            this.txtAddress.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 215);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 17);
            this.label7.TabIndex = 48;
            this.label7.Text = "Địa chỉ:";
            // 
            // txtInsurance
            // 
            this.txtInsurance.Location = new System.Drawing.Point(143, 281);
            this.txtInsurance.Margin = new System.Windows.Forms.Padding(4);
            this.txtInsurance.Name = "txtInsurance";
            this.txtInsurance.Size = new System.Drawing.Size(265, 24);
            this.txtInsurance.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(53, 284);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 17);
            this.label11.TabIndex = 46;
            this.label11.Text = "Thẻ BHYT:";
            // 
            // cboObject
            // 
            this.cboObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboObject.FormattingEnabled = true;
            this.cboObject.Location = new System.Drawing.Point(143, 246);
            this.cboObject.Margin = new System.Windows.Forms.Padding(4);
            this.cboObject.Name = "cboObject";
            this.cboObject.Size = new System.Drawing.Size(265, 24);
            this.cboObject.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 249);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 17);
            this.label3.TabIndex = 45;
            this.label3.Text = "Đối tượng:";
            // 
            // cboSex
            // 
            this.cboSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSex.FormattingEnabled = true;
            this.cboSex.Items.AddRange(new object[] {
            "Nam",
            "Nữ"});
            this.cboSex.Location = new System.Drawing.Point(143, 142);
            this.cboSex.Margin = new System.Windows.Forms.Padding(4);
            this.cboSex.Name = "cboSex";
            this.cboSex.Size = new System.Drawing.Size(74, 24);
            this.cboSex.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(66, 145);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 17);
            this.label9.TabIndex = 42;
            this.label9.Text = "Giới tính:";
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(143, 176);
            this.txtAge.Margin = new System.Windows.Forms.Padding(4);
            this.txtAge.MaxLength = 3;
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(74, 24);
            this.txtAge.TabIndex = 2;
            this.txtAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAge_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(92, 179);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 17);
            this.label8.TabIndex = 41;
            this.label8.Text = "Tuổi:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(143, 106);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(265, 24);
            this.txtName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 17);
            this.label4.TabIndex = 40;
            this.label4.Text = "Tên bệnh nhân:";
            // 
            // txtPID
            // 
            this.txtPID.Enabled = false;
            this.txtPID.Location = new System.Drawing.Point(142, 71);
            this.txtPID.Margin = new System.Windows.Forms.Padding(4);
            this.txtPID.Name = "txtPID";
            this.txtPID.Size = new System.Drawing.Size(157, 24);
            this.txtPID.TabIndex = 7;
            this.txtPID.TabStop = false;
            // 
            // txtYearOfBirth
            // 
            this.txtYearOfBirth.Location = new System.Drawing.Point(297, 176);
            this.txtYearOfBirth.Margin = new System.Windows.Forms.Padding(4);
            this.txtYearOfBirth.MaxLength = 4;
            this.txtYearOfBirth.Name = "txtYearOfBirth";
            this.txtYearOfBirth.Size = new System.Drawing.Size(111, 24);
            this.txtYearOfBirth.TabIndex = 3;
            this.txtYearOfBirth.TextChanged += new System.EventHandler(this.txtYearOfBirth_TextChanged);
            this.txtYearOfBirth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtYearOfBirth_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(222, 179);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Năm sinh:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(96, 74);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "PID:";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(545, 373);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(186, 39);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Lưu thông tin (Ctrl+S)";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.cmdExit);
            this.groupPanel1.Controls.Add(this.checkBox1);
            this.groupPanel1.Controls.Add(this.grbInformation);
            this.groupPanel1.Controls.Add(this.btnSave);
            this.groupPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupPanel1.Location = new System.Drawing.Point(0, 0);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(879, 420);
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
            this.groupPanel1.TabIndex = 3;
            // 
            // cmdExit
            // 
            this.cmdExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cmdExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.cmdExit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExit.Image = ((System.Drawing.Image)(resources.GetObject("cmdExit.Image")));
            this.cmdExit.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.cmdExit.Location = new System.Drawing.Point(739, 373);
            this.cmdExit.Margin = new System.Windows.Forms.Padding(4);
            this.cmdExit.Name = "cmdExit";
            this.cmdExit.Size = new System.Drawing.Size(130, 39);
            this.cmdExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmdExit.TabIndex = 1;
            this.cmdExit.Text = "&Thoát(Esc)";
            this.cmdExit.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(6, 380);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(221, 23);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.TabStop = false;
            this.checkBox1.Text = "Thoát sau khi thêm mới";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // FrmPatientInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 420);
            this.Controls.Add(this.groupPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FrmPatientInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "THÔNG TIN BỆNH NHÂN";
            this.Load += new System.EventHandler(this.FrmPatientInfor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPatientInfo_KeyDown);
            this.grbInformation.ResumeLayout(false);
            this.grpInformationFK.ResumeLayout(false);
            this.grpInformationFK.PerformLayout();
            this.grpInformationPK.ResumeLayout(false);
            this.grpInformationPK.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            this.groupPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel grbInformation;
        private System.Windows.Forms.TextBox txtPID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYearOfBirth;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label15;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private System.Windows.Forms.CheckBox checkBox1;
        private DevComponents.DotNetBar.ButtonX cmdExit;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox grpInformationPK;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInsurance;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboObject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboSex;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpInformationFK;
        private System.Windows.Forms.TextBox txtDiagnose;
        private System.Windows.Forms.ComboBox cboDepartment;
        private System.Windows.Forms.TextBox txtBed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRoom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtInvoiceNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label10;
    }
}