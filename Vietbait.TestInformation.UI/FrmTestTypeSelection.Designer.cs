namespace Vietbait.TestInformation.UI
{
    partial class FrmTestTypeSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdTestType = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.colX = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestType_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTestType_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            ((System.ComponentModel.ISupportInitialize)(this.grdTestType)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTestType
            // 
            this.grdTestType.AllowUserToAddRows = false;
            this.grdTestType.AllowUserToDeleteRows = false;
            this.grdTestType.AllowUserToResizeRows = false;
            this.grdTestType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTestType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdTestType.BackgroundColor = System.Drawing.Color.White;
            this.grdTestType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTestType.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colX,
            this.colTestType_Name,
            this.colTestType_ID});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTestType.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdTestType.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grdTestType.Location = new System.Drawing.Point(12, 12);
            this.grdTestType.MultiSelect = false;
            this.grdTestType.Name = "grdTestType";
            this.grdTestType.RowHeadersVisible = false;
            this.grdTestType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdTestType.Size = new System.Drawing.Size(286, 333);
            this.grdTestType.TabIndex = 0;
            this.grdTestType.MouseUp += new System.Windows.Forms.MouseEventHandler(this.grdTestType_MouseUp);
            this.grdTestType.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdTestType_KeyUp);
            // 
            // colX
            // 
            this.colX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.colX.DataPropertyName = "CheckState";
            this.colX.FillWeight = 20F;
            this.colX.HeaderText = "X";
            this.colX.MinimumWidth = 20;
            this.colX.Name = "colX";
            this.colX.ReadOnly = true;
            this.colX.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colX.Width = 25;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TestType_Name";
            this.dataGridViewTextBoxColumn1.FillWeight = 140F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Tên Xét Nghiệm";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 173;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "TestType_ID";
            this.dataGridViewTextBoxColumn2.HeaderText = "TestType_ID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // colTestType_Name
            // 
            this.colTestType_Name.DataPropertyName = "TestType_Name";
            this.colTestType_Name.FillWeight = 140F;
            this.colTestType_Name.HeaderText = "Tên Xét Nghiệm";
            this.colTestType_Name.Name = "colTestType_Name";
            this.colTestType_Name.ReadOnly = true;
            // 
            // colTestType_ID
            // 
            this.colTestType_ID.DataPropertyName = "TestType_ID";
            this.colTestType_ID.HeaderText = "TestType_ID";
            this.colTestType_ID.Name = "colTestType_ID";
            this.colTestType_ID.Visible = false;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(44, 350);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(223, 23);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "Ấn Enter để chập nhận lựa chọn";
            // 
            // FrmTestTypeSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 378);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.grdTestType);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmTestTypeSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CHỌN LOẠI XÉT NGHIỆM ĐỂ IN";
            this.Load += new System.EventHandler(this.FrmTestTypeSelection_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTestTypeSelection_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grdTestType)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX grdTestType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colX;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestType_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTestType_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DevComponents.DotNetBar.LabelX labelX1;

    }
}