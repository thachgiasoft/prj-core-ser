namespace VietBaIT.CommonLibrary
{
    partial class SearchOnGridView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchOnGridView));
            this.chkCaption = new System.Windows.Forms.CheckBox();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.chkFirstRowFirstCol = new System.Windows.Forms.CheckBox();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCol = new System.Windows.Forms.CheckBox();
            this.cboValue = new System.Windows.Forms.ComboBox();
            this.cboColName = new System.Windows.Forms.ComboBox();
            this.chkLike = new System.Windows.Forms.CheckBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.StatusStrip1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkCaption
            // 
            this.chkCaption.AutoSize = true;
            this.chkCaption.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCaption.Location = new System.Drawing.Point(109, 97);
            this.chkCaption.Name = "chkCaption";
            this.chkCaption.Size = new System.Drawing.Size(196, 19);
            this.chkCaption.TabIndex = 4;
            this.chkCaption.Text = "Phân biệt chữ hoa, chữ thường";
            this.chkCaption.UseVisualStyleBackColor = true;
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PResult});
            this.StatusStrip1.Location = new System.Drawing.Point(0, 245);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(407, 22);
            this.StatusStrip1.TabIndex = 63;
            this.StatusStrip1.Text = "StatusStrip1";
            // 
            // PResult
            // 
            this.PResult.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PResult.Name = "PResult";
            this.PResult.Size = new System.Drawing.Size(392, 17);
            this.PResult.Spring = true;
            this.PResult.Text = "Kết quả";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(120, 208);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(90, 28);
            this.cmdSearch.TabIndex = 59;
            this.cmdSearch.Text = "Tìm kiếm";
            this.cmdSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // chkFirstRowFirstCol
            // 
            this.chkFirstRowFirstCol.AutoSize = true;
            this.chkFirstRowFirstCol.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFirstRowFirstCol.Location = new System.Drawing.Point(109, 122);
            this.chkFirstRowFirstCol.Name = "chkFirstRowFirstCol";
            this.chkFirstRowFirstCol.Size = new System.Drawing.Size(196, 19);
            this.chkFirstRowFirstCol.TabIndex = 5;
            this.chkFirstRowFirstCol.Text = "Luôn tìm kiếm từ hàng đầu tiên";
            this.chkFirstRowFirstCol.UseVisualStyleBackColor = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.chkFirstRowFirstCol);
            this.GroupBox2.Controls.Add(this.chkCaption);
            this.GroupBox2.Controls.Add(this.chkCol);
            this.GroupBox2.Controls.Add(this.cboValue);
            this.GroupBox2.Controls.Add(this.cboColName);
            this.GroupBox2.Controls.Add(this.chkLike);
            this.GroupBox2.Controls.Add(this.Label16);
            this.GroupBox2.Location = new System.Drawing.Point(0, 53);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(409, 147);
            this.GroupBox2.TabIndex = 58;
            this.GroupBox2.TabStop = false;
            // 
            // chkCol
            // 
            this.chkCol.AutoSize = true;
            this.chkCol.Location = new System.Drawing.Point(15, 44);
            this.chkCol.Name = "chkCol";
            this.chkCol.Size = new System.Drawing.Size(69, 17);
            this.chkCol.TabIndex = 60;
            this.chkCol.Text = "Theo cột";
            this.chkCol.UseVisualStyleBackColor = true;
            // 
            // cboValue
            // 
            this.cboValue.FormattingEnabled = true;
            this.cboValue.Location = new System.Drawing.Point(109, 15);
            this.cboValue.Name = "cboValue";
            this.cboValue.Size = new System.Drawing.Size(283, 21);
            this.cboValue.TabIndex = 1;
            // 
            // cboColName
            // 
            this.cboColName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColName.FormattingEnabled = true;
            this.cboColName.Location = new System.Drawing.Point(109, 42);
            this.cboColName.Name = "cboColName";
            this.cboColName.Size = new System.Drawing.Size(283, 21);
            this.cboColName.TabIndex = 2;
            // 
            // chkLike
            // 
            this.chkLike.AutoSize = true;
            this.chkLike.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLike.Location = new System.Drawing.Point(109, 72);
            this.chkLike.Name = "chkLike";
            this.chkLike.Size = new System.Drawing.Size(131, 19);
            this.chkLike.TabIndex = 3;
            this.chkLike.Text = "Tìm kiếm chính xác";
            this.chkLike.UseVisualStyleBackColor = true;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.ForeColor = System.Drawing.Color.Black;
            this.Label16.Location = new System.Drawing.Point(12, 18);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(59, 15);
            this.Label16.TabIndex = 54;
            this.Label16.Text = "Giá trị tìm";
            // 
            // Label2
            // 
            this.Label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.Navy;
            this.Label2.Location = new System.Drawing.Point(67, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(330, 44);
            this.Label2.TabIndex = 62;
            this.Label2.Text = "NHẬP THÔNG TIN TÌM KIẾM VÀ NHẤN ENTER";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdClose
            // 
            this.cmdClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(225, 208);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(90, 28);
            this.cmdClose.TabIndex = 60;
            this.cmdClose.Text = "Thoát";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(7, 3);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(54, 54);
            this.PictureBox1.TabIndex = 61;
            this.PictureBox1.TabStop = false;
            // 
            // SearchOnGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 267);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.cmdSearch);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.cmdClose);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchOnGridView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tìm kiếm thông tin trên lưới dữ liệu";
            this.Load += new System.EventHandler(this.SearchOnGridView_Load);
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox chkCaption;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel PResult;
        internal System.Windows.Forms.Button cmdSearch;
        internal System.Windows.Forms.CheckBox chkFirstRowFirstCol;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.CheckBox chkCol;
        internal System.Windows.Forms.ComboBox cboValue;
        internal System.Windows.Forms.ComboBox cboColName;
        internal System.Windows.Forms.CheckBox chkLike;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button cmdClose;
        internal System.Windows.Forms.PictureBox PictureBox1;
    }
}