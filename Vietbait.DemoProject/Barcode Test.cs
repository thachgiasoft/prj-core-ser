using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vietbait.DemoProject
{
    public partial class Barcode_Test : Form
    {
        public Barcode_Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            barcode1.Image().Save("C:\\barcode.bmp");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            barcode1.Data = textBox1.Text.Trim();
        }
    }
}
