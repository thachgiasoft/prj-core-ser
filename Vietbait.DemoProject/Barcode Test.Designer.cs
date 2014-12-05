namespace Vietbait.DemoProject
{
    partial class Barcode_Test
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
            this.barcode1 = new Mabry.Windows.Forms.Barcode.Barcode();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // barcode1
            // 
            this.barcode1.BackColor = System.Drawing.Color.White;
            this.barcode1.BarColor = System.Drawing.Color.Black;
            this.barcode1.BarRatio = 2F;
            this.barcode1.Data = "05751588731728632584";
            this.barcode1.DataExtension = null;
            this.barcode1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barcode1.Location = new System.Drawing.Point(12, 46);
            this.barcode1.Name = "barcode1";
            this.barcode1.Size = new System.Drawing.Size(524, 246);
            this.barcode1.Symbology = Mabry.Windows.Forms.Barcode.Barcode.BarcodeSymbologies.Code128;
            this.barcode1.TabIndex = 0;
            this.barcode1.Text = "barcode1";
            // 
            // btnClearAll
            // 
            this.button1.Location = new System.Drawing.Point(587, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "btnClearAll";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(469, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Barcode_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 377);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.barcode1);
            this.Name = "Barcode_Test";
            this.Text = "Barcode_Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Mabry.Windows.Forms.Barcode.Barcode barcode1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}