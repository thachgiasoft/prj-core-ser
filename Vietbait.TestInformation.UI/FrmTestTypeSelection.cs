using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmTestTypeSelection : Office2007Form
    {
        public DataTable dtTestTypeCLB;
        public string strSelectedTestType;
        public bool vCancel;

        public FrmTestTypeSelection()
        {
            InitializeComponent();

            grdTestType.AutoGenerateColumns = false;
        }

        private void FrmTestTypeSelection_Load(object sender, EventArgs e)
        {
            if (!dtTestTypeCLB.Columns.Contains("CheckState")) dtTestTypeCLB.Columns.Add("CheckState", typeof (bool));

            foreach (DataRow dr in dtTestTypeCLB.Rows)
            {
                dr["CheckState"] = true;
            }
            dtTestTypeCLB.AcceptChanges();
            grdTestType.DataSource = dtTestTypeCLB;
        }

        private void FrmTestTypeSelection_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    vCancel = true;
                    Close();
                    break;
                case Keys.Enter:
                    strSelectedTestType = Utility.GetCheckedID(dtTestTypeCLB, "CheckState",
                                                               TTestTypeList.Columns.TestTypeId);
                    Close();
                    break;
            }
        }

        private void grdTestType_MouseUp(object sender, MouseEventArgs e)
        {
            if (grdTestType.CurrentRow != null)
            {
                int rowIndex = grdTestType.CurrentRow.Index;
                dtTestTypeCLB.Rows[rowIndex]["CheckState"] =
                    Convert.ToBoolean(dtTestTypeCLB.Rows[rowIndex]["CheckState"]) ? false : true;
                dtTestTypeCLB.AcceptChanges();
            }
        }

        private void grdTestType_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) grdTestType_MouseUp(null, null);
        }
    }
}