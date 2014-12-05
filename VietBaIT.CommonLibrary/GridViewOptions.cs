using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace GridViewUtils
{
    public partial class GridviewOption : Form
    {
        public ArrayList InvisibleCols = new ArrayList();
        public ArrayList AllCols = new ArrayList();
        public bool _Cancel = true;
        public GridviewOption(ArrayList AllCols, ArrayList InvisibleCols)
        {
            InitializeComponent();
            this.AllCols = AllCols;
            this.InvisibleCols = InvisibleCols;

        }

        private void LoadColumns()
        {
            if (AllCols == null) return;
            for (int i = 0; i <= AllCols.Count - 1; i++)
            {
                if (InvisibleCols.Contains(AllCols[i]))
                    chkColList.Items.Add(AllCols[i], true);
                else
                    chkColList.Items.Add(AllCols[i], false);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadColumns();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            InvisibleCols.Clear();
            foreach (object obj in chkColList.CheckedItems)
            {
                InvisibleCols.Add(obj.ToString());
            }
            _Cancel = false;
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            for(int i=0;i<=chkColList.Items.Count-1;i++)
            {
                chkColList.SetItemChecked(i, chkCheckAll.Checked);  
            }  
        }

        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= chkColList.Items.Count - 1; i++)
            {
                if (chkColList.GetItemChecked(i))
                    chkColList.SetItemChecked(i, false);
                else
                    chkColList.SetItemChecked(i, true);
              }  
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

    }
}
