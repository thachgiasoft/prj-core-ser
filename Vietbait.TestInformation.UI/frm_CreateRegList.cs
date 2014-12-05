using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;

namespace Vietbait.TestInformation.UI
{
    public partial class frm_CreateRegList : Office2007Form
    {
        #region "KHAI BÁO THUỘC TÍNH"

        public bool b_Status;
        public action em_Action = action.doNothing;
        public DataTable g_dtResultDetail = new DataTable();
        public DataTable g_dtTestInfo = new DataTable();
        private DataTable m_dtDataControl = new DataTable();
        public string v_Barcode = "";
        public int v_Device_Id = -1;
        public int v_Patient_ID = -1;
        public int v_TestType_Id = -1;
        public int v_Test_ID = -1;
        // public DataTable g_dtResultDetail = new DataTable();

        #endregion

        #region "HÀM KHỞI TẠO"

        /// <summary>
        /// hàm thực hiện khởi tạo form
        /// </summary>
        public frm_CreateRegList()
        {
            InitializeComponent();
            KeyPreview = true;
        }

        #endregion

        #region "KHAI BÁO SỰ KIỆN CỦA FORM"

        /// <summary>
        /// hàm thực hiện chọn toàn bộ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRowView drv in m_dtDataControl.DefaultView)
            {
                drv["CHON"] = chkAll.Checked ? 1 : 0;
            }
            m_dtDataControl.AcceptChanges();
        }

        /// <summary>
        /// hàm thực hiện đảo chọn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRearve_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRowView drv in m_dtDataControl.DefaultView)
            {
                drv["CHON"] = drv["CHON"].ToString() == "0" ? 1 : 0;
            }
            m_dtDataControl.AcceptChanges();
        }

        /// <summary>
        /// hàm thực hiện load thông tin data của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_CreateNewResultDetail_Load(object sender, EventArgs e)
        {
            //v_TestType_Id = Utility.Int32Dbnull(cboTestType_ID.SelectedValue, -1);
            GetData();
            grdList.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            Location=new Point(0,0);
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            grdList.Focus();
        }

        #endregion

        #region "HÀM THỰC HIỆN DÙNG CHUNG"

        /// <summary>
        /// hàm thực hiện getdata
        /// </summary>
        private void GetData()
        {
            //m_dtDataControl = SPs.SpDataControlLoadData(v_TestType_Id,v_Device_Id).GetDataSet().Tables[0];
            // m_dtDataControl = SPs.SpQuickInputDetailCorrectVersion(v_Patient_ID, v_TestType_Id, globalVariables.SysDate.ToString()).GetDataSet().Tables[0];
            m_dtDataControl = SPs.SpQuickInputDetailV2(v_Patient_ID, v_TestType_Id).GetDataSet().Tables[0];

            if (!m_dtDataControl.Columns.Contains("CHON")) m_dtDataControl.Columns.Add("CHON", typeof (int));
            foreach (DataRow dr in m_dtDataControl.Rows)
            {
                dr["CHON"] = 0;
            }
            m_dtDataControl.AcceptChanges();
            foreach (DataRowView drv in g_dtResultDetail.DefaultView)
            {
                DataRow[] arrDr = m_dtDataControl.Select("Para_Name='" + drv[TResultDetail.Columns.ParaName] + "'");
                // DataRow[] arrDr = m_dtDataControl.Select("Data_Name='" + drv[TResultDetail.Columns.ParaName] + "'");
                if (arrDr.GetLength(0) > 0)
                {
                    arrDr[0].Delete();
                }
            }
            m_dtDataControl.AcceptChanges();
            Utility.SetDataSourceForDataGridView(grdList, m_dtDataControl, false, true, "", "");
            grdList.Focus();
            // DataBinding.BindData(cboTestType_ID, g_dtTestInfo,TTestTypeList.Columns.TestTypeId,TTestTypeList.Columns.TestTypeName);
        }

        #endregion

        private string _rowFilter = "1=1";
        public string barcode = "";

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _rowFilter = "1=1";
            if (!string.IsNullOrEmpty(txtFilter.Text.Trim()))
            {
                _rowFilter = "Data_Name like '%" + txtFilter.Text.Trim() + "%'";
            }
            m_dtDataControl.DefaultView.RowFilter = _rowFilter;
            m_dtDataControl.AcceptChanges();
        }

        /// <summary>
        /// hàm thực hiện phím tắt của lọc dữ liệu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                grdList.Focus();
        }

        /// <summary>
        /// hàm thực hiện phím tắt của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_CreateNewResultDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt & e.KeyCode == Keys.L)
            {
                txtFilter.Focus();
            }
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Alt & e.KeyCode == Keys.A)
            {
                cmdAccept.PerformClick();
            }
            if(e.KeyCode==Keys.Enter)
            {
                cmdAccept.Focus();
            }
            if (e.KeyCode == Keys.Tab)
               
            {grdList.Focus();}
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            em_Action = action.doNothing;
            Close();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            foreach (DataRow dr in m_dtDataControl.Rows)
            {
                if (dr["CHON"].ToString() == "1")
                {
                DataRow newDr = g_dtResultDetail.NewRow();
                    newDr[TRegList.Columns.Barcode] = dr["barcode"];
                    newDr[TRegList.Columns.ParaName] = dr[DDataControl.Columns.DataName];
                    newDr[TRegList.Columns.TestId] = v_Test_ID;
                    newDr[TRegList.Columns.DeviceId] = dr[DDataControl.Columns.DeviceId];
                    newDr[TRegList.Columns.AliasName] = dr[DDataControl.Columns.AliasName];
                    g_dtResultDetail.Rows.Add(newDr);
                }
            }
            em_Action = action.ConfirmData;
            b_Status = true;
            Close();
          
        }

        private void grdList_Enter(object sender, EventArgs e)
        {
           // cmdAccept.Focus();
        }

        private void frm_CreateRegList_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

       }
}