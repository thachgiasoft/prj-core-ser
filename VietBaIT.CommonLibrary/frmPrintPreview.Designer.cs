namespace VietBaIT.CommonLibrary
{
    partial class frmPrintPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintPreview));
            this.cmdExcel = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdTrinhKy = new System.Windows.Forms.Button();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbr = new System.Windows.Forms.StatusStrip();
            this.crptViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.stbr.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdExcel
            // 
            this.cmdExcel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.cmdExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExcel.Image")));
            this.cmdExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdExcel.Location = new System.Drawing.Point(482, 2);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(103, 25);
            this.cmdExcel.TabIndex = 9;
            this.cmdExcel.Text = "&Xuất Excel";
            this.cmdExcel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdExcel.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Navy;
            this.Label1.Location = new System.Drawing.Point(599, 7);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(201, 15);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Nhấn P hoặc Ctrl+P để in ra máy in";
            // 
            // cmdTrinhKy
            // 
            this.cmdTrinhKy.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTrinhKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.cmdTrinhKy.Image = ((System.Drawing.Image)(resources.GetObject("cmdTrinhKy.Image")));
            this.cmdTrinhKy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdTrinhKy.Location = new System.Drawing.Point(373, 2);
            this.cmdTrinhKy.Name = "cmdTrinhKy";
            this.cmdTrinhKy.Size = new System.Drawing.Size(103, 25);
            this.cmdTrinhKy.TabIndex = 6;
            this.cmdTrinhKy.Text = "&Trình ký";
            this.cmdTrinhKy.UseVisualStyleBackColor = true;
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(442, 17);
            this.ToolStripStatusLabel2.Spring = true;
            this.ToolStripStatusLabel2.Text = "Nhấn S hoặc Ctrl+S để lưu dữ liệu in ra file Excel";
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(442, 17);
            this.ToolStripStatusLabel1.Spring = true;
            this.ToolStripStatusLabel1.Text = "Nhấn P hoặc Ctrl+P để in ra máy in";
            // 
            // stbr
            // 
            this.stbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1,
            this.ToolStripStatusLabel2});
            this.stbr.Location = new System.Drawing.Point(0, 458);
            this.stbr.Name = "stbr";
            this.stbr.Size = new System.Drawing.Size(900, 22);
            this.stbr.TabIndex = 7;
            this.stbr.Text = "StatusStrip1";
            // 
            // crptViewer
            // 
            this.crptViewer.ActiveViewIndex = -1;
            this.crptViewer.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.crptViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crptViewer.DisplayGroupTree = false;
            this.crptViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crptViewer.EnableDrillDown = false;
            this.crptViewer.Location = new System.Drawing.Point(0, 0);
            this.crptViewer.Name = "crptViewer";
            this.crptViewer.SelectionFormula = "";
            this.crptViewer.Size = new System.Drawing.Size(900, 480);
            this.crptViewer.TabIndex = 10;
            this.crptViewer.ViewTimeSelectionFormula = "";
            // 
            // frmPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 480);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cmdTrinhKy);
            this.Controls.Add(this.stbr);
            this.Controls.Add(this.crptViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "frmPrintPreview";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintPreview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.stbr.ResumeLayout(false);
            this.stbr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button cmdExcel;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button cmdTrinhKy;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel2;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        internal System.Windows.Forms.StatusStrip stbr;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crptViewer;

    }
}