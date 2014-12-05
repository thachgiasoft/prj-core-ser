namespace VietBaIT.CommonLibrary
{
    partial class frm_SignInfor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SignInfor));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.chkGhiLai = new System.Windows.Forms.CheckBox();
            this.txtNoiDungKy = new System.Windows.Forms.TextBox();
            this.cboFontSize = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.cboFontStyle = new System.Windows.Forms.ComboBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtBaoCao = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdQuit = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.chkGhiLai);
            this.GroupBox1.Controls.Add(this.txtNoiDungKy);
            this.GroupBox1.Controls.Add(this.cboFontSize);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.cboFontStyle);
            this.GroupBox1.Controls.Add(this.cboFontName);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.txtBaoCao);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.Location = new System.Drawing.Point(3, 56);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(573, 272);
            this.GroupBox1.TabIndex = 6;
            this.GroupBox1.TabStop = false;
            // 
            // chkGhiLai
            // 
            this.chkGhiLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkGhiLai.AutoSize = true;
            this.chkGhiLai.Checked = true;
            this.chkGhiLai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGhiLai.Location = new System.Drawing.Point(105, 247);
            this.chkGhiLai.Name = "chkGhiLai";
            this.chkGhiLai.Size = new System.Drawing.Size(166, 19);
            this.chkGhiLai.TabIndex = 1;
            this.chkGhiLai.TabStop = false;
            this.chkGhiLai.Text = "Ghi lại cho lần dùng sau?";
            this.chkGhiLai.UseVisualStyleBackColor = true;
            // 
            // txtNoiDungKy
            // 
            this.txtNoiDungKy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoiDungKy.Location = new System.Drawing.Point(105, 102);
            this.txtNoiDungKy.Multiline = true;
            this.txtNoiDungKy.Name = "txtNoiDungKy";
            this.txtNoiDungKy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNoiDungKy.Size = new System.Drawing.Size(443, 139);
            this.txtNoiDungKy.TabIndex = 4;
            this.txtNoiDungKy.WordWrap = false;
            // 
            // cboFontSize
            // 
            this.cboFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontSize.FormattingEnabled = true;
            this.cboFontSize.Location = new System.Drawing.Point(377, 71);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(171, 23);
            this.cboFontSize.TabIndex = 3;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(323, 79);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(48, 15);
            this.Label5.TabIndex = 7;
            this.Label5.Text = "Cỡ chữ";
            // 
            // cboFontStyle
            // 
            this.cboFontStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontStyle.FormattingEnabled = true;
            this.cboFontStyle.Items.AddRange(new object[] {
            "Chữ đậm",
            "Chữ nghiêng",
            "Bình thường"});
            this.cboFontStyle.Location = new System.Drawing.Point(105, 71);
            this.cboFontStyle.Name = "cboFontStyle";
            this.cboFontStyle.Size = new System.Drawing.Size(212, 23);
            this.cboFontStyle.TabIndex = 2;
            // 
            // cboFontName
            // 
            this.cboFontName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontName.FormattingEnabled = true;
            this.cboFontName.Location = new System.Drawing.Point(105, 40);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new System.Drawing.Size(443, 23);
            this.cboFontName.TabIndex = 1;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(22, 102);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 15);
            this.Label4.TabIndex = 4;
            this.Label4.Text = "Tên báo cáo";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(22, 71);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(56, 15);
            this.Label3.TabIndex = 3;
            this.Label3.Text = "Kiểu chữ";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(22, 44);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(55, 15);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Font chữ";
            // 
            // txtBaoCao
            // 
            this.txtBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaoCao.Location = new System.Drawing.Point(105, 13);
            this.txtBaoCao.Name = "txtBaoCao";
            this.txtBaoCao.Size = new System.Drawing.Size(443, 21);
            this.txtBaoCao.TabIndex = 0;
            this.txtBaoCao.TabStop = false;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(22, 16);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 15);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Tên báo cáo";
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.Location = new System.Drawing.Point(190, 343);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(102, 30);
            this.cmdOK.TabIndex = 10;
            this.cmdOK.Text = "Chấp nhận";
            this.cmdOK.UseVisualStyleBackColor = true;
            // 
            // cmdQuit
            // 
            this.cmdQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdQuit.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdQuit.Location = new System.Drawing.Point(298, 343);
            this.cmdQuit.Name = "cmdQuit";
            this.cmdQuit.Size = new System.Drawing.Size(102, 30);
            this.cmdQuit.TabIndex = 9;
            this.cmdQuit.Text = "Thoát";
            this.cmdQuit.UseVisualStyleBackColor = true;
            // 
            // Label6
            // 
            this.Label6.Font = new System.Drawing.Font("Arial", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.Color.Maroon;
            this.Label6.Location = new System.Drawing.Point(66, 9);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(510, 49);
            this.Label6.TabIndex = 8;
            this.Label6.Text = "TÙY BIẾN TRÌNH KÝ CHO CÁC BÁO CÁO";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(12, 8);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(68, 50);
            this.PictureBox1.TabIndex = 7;
            this.PictureBox1.TabStop = false;
            // 
            // frm_SignInfor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 381);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdQuit);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.PictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SignInfor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VietBaIT JSC-HIS.Thông tin trình ký";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.CheckBox chkGhiLai;
        internal System.Windows.Forms.TextBox txtNoiDungKy;
        internal System.Windows.Forms.ComboBox cboFontSize;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cboFontStyle;
        internal System.Windows.Forms.ComboBox cboFontName;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtBaoCao;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button cmdOK;
        internal System.Windows.Forms.Button cmdQuit;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.PictureBox PictureBox1;
    }
}