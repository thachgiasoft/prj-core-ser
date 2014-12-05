using System;
using System.Data;
using System.Windows.Forms;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;

namespace Vietbait.TestInformation.UI
{
    public partial class frm_QuickSearch : Form
    {
        private string _rowFiler = "1=1";
        public DataTable m_dtPatientInfo = new DataTable();
        public int p_PatientID = -2;
        public DataTable p_dtResultDetail = new DataTable();
        public DataTable p_dtTestInfo = new DataTable();
        public int v_TestInfo = -1;
        public int v_TestTypeInfo = -1;
        private int v_patientId = -1;

        public frm_QuickSearch()
        {
            InitializeComponent();
            KeyPreview = true;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
        }

        private void frm_QuickSearch_Load(object sender, EventArgs e)
        {
            grdPatients.AutoGenerateColumns = false;
            grdPatients.DataSource = m_dtPatientInfo;
            ModifyCommand();
        }

        private void ModifyCommand()
        {
            txtFilter.Visible = m_dtPatientInfo.Rows.Count > 0;
            lblMessage.Visible = m_dtPatientInfo.Rows.Count <= 0;
            lblMessage.Text = "Hiện  không tìm thấy thông tin bệnh nhân nào";
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _rowFiler = "1=1";
            if (!string.IsNullOrEmpty(txtFilter.Text))
            {
                _rowFiler = "Patient_Name like '%" + txtFilter.Text.Trim() + "%' or Barcode like '%" +
                            txtFilter.Text.Trim() + "%'";
            }
            m_dtPatientInfo.DefaultView.RowFilter = _rowFiler;
            m_dtPatientInfo.AcceptChanges();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                grdPatients.Focus();
            }
        }

        private void grdPatients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdAccept.Focus();
            }
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if (InValiData())
            {
                if (Utility.AcceptQuestion(
                    "Hiện thời bệnh nhân đã có xét nghiệm,Bạn có muốn thêm một bản ghi nữa không", "Thông báo", true))
                {
                    goto _goto;
                }
                else
                {
                    return;
                }
            }

            _goto:
            int record = new Update(TTestInfo.Schema)
                .Set(TTestInfo.Columns.PatientId).EqualTo(v_patientId)
                .Where(TTestInfo.Columns.TestId).IsEqualTo(v_TestInfo).Execute();
            int record2 = new Update(TResultDetail.Schema)
                .Set(TResultDetail.Columns.PatientId).EqualTo(v_patientId)
                .Where(TResultDetail.Columns.TestId).IsEqualTo(v_TestInfo).Execute();
               
            if (record > 0 && record2 > 0)
            {
                DataRow[] arrDr = p_dtTestInfo.Select("Test_ID=" + v_TestInfo);
                DataRow[] arrResultDetail = p_dtResultDetail.Select("Test_ID=" + v_TestInfo);
                if (arrDr.GetLength(0) > 0)
                {
                    foreach (DataRow row in arrDr)
                    {
                        row["Patient_Id"] = v_patientId;
                    }
                    p_dtTestInfo.AcceptChanges();
                    foreach (DataRow dataRow in arrResultDetail)
                    {
                        dataRow["Patient_Id"] = v_patientId;
                    }
                    p_dtResultDetail.AcceptChanges();
                }
                Utility.ShowMsg("Bạn thực hiện thành công");
                cmdExit.PerformClick();
            }
            else
            {
                Utility.ShowMsg("Có lỗi trong quá trình cập nhật dữ liệu");
            }
        }

        private bool InValiData()
        {
            v_patientId = Utility.Int32Dbnull(grdPatients.CurrentRow.Cells["colPatient_ID"].Value, -1);

            TTestInfoCollection testInfoCollection =
                new TTestInfoController().FetchByQuery(TTestInfo.CreateQuery().AND(TTestInfo.Columns.TestTypeId,
                                                                                   Comparison.Equals, v_TestTypeInfo).AND(
                                                                                       TTestInfo.Columns.PatientId,
                                                                                       Comparison.Equals, v_patientId));

            if (testInfoCollection.Count > 0)
            {
                return true;
            }
            return false;
        }

        private void frm_QuickSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// hàm thực hiện dùng phím tắt của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_QuickSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Alt && e.KeyCode == Keys.T) txtFilter.Focus();
            if (e.Control && e.KeyCode == Keys.S) cmdAccept.PerformClick();
        }

        private void grdPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (grdPatients.CurrentRow != null)
            {
                v_patientId = Utility.Int32Dbnull(grdPatients.CurrentRow.Cells["colPatient_ID"].Value, -1);
                cmdAccept.Enabled = grdPatients.RowCount > 0 && v_patientId != p_PatientID;
            }
        }

        private void frm_QuickSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_dtPatientInfo.DefaultView.RowFilter = "1=1";
            m_dtPatientInfo.AcceptChanges();
        }
    }
}