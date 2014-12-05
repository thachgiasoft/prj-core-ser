namespace Vietbait.Lablink.List.UI
{
    partial class frm_PORT
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.btnSaveRs232 = new DevComponents.DotNetBar.ButtonX();
            this.grdRs232 = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.tbRs232 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel2 = new DevComponents.DotNetBar.TabControlPanel();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.grdTcpip = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colTcpId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTcpIpadd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTcpDes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbTcpip = new DevComponents.DotNetBar.TabItem(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.colRs232Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRs232Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBaudRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataBits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStopBits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colParity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDTR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRTS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRs232)).BeginInit();
            this.tabControlPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTcpip)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabControl1.CanReorderTabs = true;
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Controls.Add(this.tabControlPanel2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(934, 452);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox;
            this.tabControl1.Tabs.Add(this.tbRs232);
            this.tabControl1.Tabs.Add(this.tbTcpip);
            this.tabControl1.Text = "tabControl1";
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.btnSaveRs232);
            this.tabControlPanel1.Controls.Add(this.grdRs232);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(934, 426);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tbRs232;
            // 
            // btnSaveRs232
            // 
            this.btnSaveRs232.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveRs232.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveRs232.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRs232.ForeColor = System.Drawing.Color.Black;
            this.btnSaveRs232.Image = global::Vietbait.Lablink.List.UI.Properties.Resources.Save;
            this.btnSaveRs232.Location = new System.Drawing.Point(815, 379);
            this.btnSaveRs232.Name = "btnSaveRs232";
            this.btnSaveRs232.Size = new System.Drawing.Size(102, 43);
            this.btnSaveRs232.TabIndex = 2;
            this.btnSaveRs232.Text = "&Save";
            this.btnSaveRs232.Click += new System.EventHandler(this.btnSaveRs232_Click);
            // 
            // grdRs232
            // 
            this.grdRs232.AllowUserToDeleteRows = false;
            this.grdRs232.AllowUserToResizeColumns = false;
            this.grdRs232.AllowUserToResizeRows = false;
            this.grdRs232.BackgroundColor = System.Drawing.Color.White;
            this.grdRs232.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRs232.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRs232Id,
            this.colRs232Name,
            this.colBaudRate,
            this.colDataBits,
            this.colStopBits,
            this.colParity,
            this.colDTR,
            this.colRTS,
            this.colDescription});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdRs232.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdRs232.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdRs232.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdRs232.Location = new System.Drawing.Point(1, 1);
            this.grdRs232.Name = "grdRs232";
            this.grdRs232.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdRs232.Size = new System.Drawing.Size(932, 372);
            this.grdRs232.TabIndex = 0;
            this.grdRs232.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.grdRs232_CellValidating);
            this.grdRs232.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdRs232_KeyUp);
            // 
            // tbRs232
            // 
            this.tbRs232.AttachedControl = this.tabControlPanel1;
            this.tbRs232.Name = "tbRs232";
            this.tbRs232.Text = "RS232";
            // 
            // tabControlPanel2
            // 
            this.tabControlPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlPanel2.Controls.Add(this.buttonX1);
            this.tabControlPanel2.Controls.Add(this.grdTcpip);
            this.tabControlPanel2.Location = new System.Drawing.Point(0, 26);
            this.tabControlPanel2.Name = "tabControlPanel2";
            this.tabControlPanel2.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel2.Size = new System.Drawing.Size(934, 456);
            this.tabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(179)))), ((int)(((byte)(231)))));
            this.tabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(237)))), ((int)(((byte)(254)))));
            this.tabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.tabControlPanel2.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right)
                        | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel2.Style.GradientAngle = 90;
            this.tabControlPanel2.TabIndex = 2;
            this.tabControlPanel2.TabItem = this.tbTcpip;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.ForeColor = System.Drawing.Color.Black;
            this.buttonX1.Image = global::Vietbait.Lablink.List.UI.Properties.Resources.Save;
            this.buttonX1.Location = new System.Drawing.Point(821, 387);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(96, 38);
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "&Save";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // grdTcpip
            // 
            this.grdTcpip.BackgroundColor = System.Drawing.Color.White;
            this.grdTcpip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTcpip.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTcpId,
            this.colTcpIpadd,
            this.colTcpDes});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTcpip.DefaultCellStyle = dataGridViewCellStyle3;
            this.grdTcpip.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdTcpip.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdTcpip.Location = new System.Drawing.Point(1, 1);
            this.grdTcpip.Name = "grdTcpip";
            this.grdTcpip.Size = new System.Drawing.Size(932, 380);
            this.grdTcpip.TabIndex = 0;
            this.grdTcpip.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdTcpip_KeyUp);
            // 
            // colTcpId
            // 
            this.colTcpId.DataPropertyName = "ID";
            this.colTcpId.HeaderText = "ID";
            this.colTcpId.Name = "colTcpId";
            this.colTcpId.ReadOnly = true;
            this.colTcpId.Width = 50;
            // 
            // colTcpIpadd
            // 
            this.colTcpIpadd.DataPropertyName = "IPAddress";
            this.colTcpIpadd.HeaderText = "IP Address";
            this.colTcpIpadd.Name = "colTcpIpadd";
            this.colTcpIpadd.Width = 200;
            // 
            // colTcpDes
            // 
            this.colTcpDes.DataPropertyName = "Description";
            this.colTcpDes.HeaderText = "Desciption";
            this.colTcpDes.Name = "colTcpDes";
            this.colTcpDes.Width = 400;
            // 
            // tbTcpip
            // 
            this.tbTcpip.AttachedControl = this.tabControlPanel2;
            this.tbTcpip.Name = "tbTcpip";
            this.tbTcpip.Text = "TCP / IP";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(934, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(172, 17);
            this.toolStripStatusLabel1.Text = "Sử dụng phím tắt để thao tác";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(330, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "Ctr + S -> Save";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(330, 17);
            this.toolStripStatusLabel3.Spring = true;
            this.toolStripStatusLabel3.Text = "Phím Delete: Xóa";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(86, 17);
            this.toolStripStatusLabel4.Text = "Escape: Thoát";
            // 
            // colRs232Id
            // 
            this.colRs232Id.DataPropertyName = "ID";
            this.colRs232Id.HeaderText = "ID";
            this.colRs232Id.Name = "colRs232Id";
            this.colRs232Id.ReadOnly = true;
            this.colRs232Id.Width = 50;
            // 
            // colRs232Name
            // 
            this.colRs232Name.DataPropertyName = "Name";
            this.colRs232Name.HeaderText = "Name";
            this.colRs232Name.Name = "colRs232Name";
            // 
            // colBaudRate
            // 
            this.colBaudRate.DataPropertyName = "BaudRate";
            this.colBaudRate.HeaderText = "BaudRate";
            this.colBaudRate.Name = "colBaudRate";
            // 
            // colDataBits
            // 
            this.colDataBits.DataPropertyName = "DataBits";
            dataGridViewCellStyle1.NullValue = null;
            this.colDataBits.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDataBits.HeaderText = "DataBits";
            this.colDataBits.Name = "colDataBits";
            // 
            // colStopBits
            // 
            this.colStopBits.DataPropertyName = "StopBits";
            this.colStopBits.HeaderText = "StopBits";
            this.colStopBits.Name = "colStopBits";
            // 
            // colParity
            // 
            this.colParity.DataPropertyName = "Parity";
            this.colParity.HeaderText = "Parity";
            this.colParity.Name = "colParity";
            // 
            // colDTR
            // 
            this.colDTR.DataPropertyName = "DTR";
            this.colDTR.HeaderText = "DTR";
            this.colDTR.Name = "colDTR";
            // 
            // colRTS
            // 
            this.colRTS.DataPropertyName = "RTS";
            this.colRTS.HeaderText = "RTS";
            this.colRTS.Name = "colRTS";
            // 
            // colDescription
            // 
            this.colDescription.DataPropertyName = "Description";
            this.colDescription.HeaderText = "Description";
            this.colDescription.Name = "colDescription";
            this.colDescription.Width = 130;
            // 
            // frm_PORT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(934, 477);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frm_PORT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh mục cổng kết nối";
            this.Load += new System.EventHandler(this.frm_PORT_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_PORT_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRs232)).EndInit();
            this.tabControlPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTcpip)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tbRs232;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel2;
        private DevComponents.DotNetBar.TabItem tbTcpip;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdRs232;
        private DevComponents.DotNetBar.Controls.DataGridViewX grdTcpip;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX btnSaveRs232;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTcpId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTcpIpadd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTcpDes;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRs232Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRs232Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBaudRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataBits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStopBits;
        private System.Windows.Forms.DataGridViewTextBoxColumn colParity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDTR;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRTS;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
    }
}