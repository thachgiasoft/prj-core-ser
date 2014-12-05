using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;

using Vietbait.Lablink.Report.Business;
using Vietbait.Lablink.Report.UI.Reports;
using VietBaIT.CommonLibrary;

namespace Vietbait.Lablink.Report.UI

{
    public partial class FrmReportDailyTest_VNIO : Office2007Form
    {
        #region "KHAI BAO THUOC TINH"

        private DataTable _Objecttype = new DataTable();
        private DataTable _Testtype = new DataTable();
        private DataTable _dsReport = new DataTable();
        public DataSet dsDepartmentDoctor = new DataSet();
        private string pid;

        #endregion

        public FrmReportDailyTest_VNIO()
        {
            InitializeComponent();
            ReportBusiness.CreateManagementUnit();
        }

        private void FrmGeneralReport_Load(object sender, EventArgs e)
        {
            cboDate.SelectedIndex = 0;
            //cboReportType.SelectedIndex = 0;
            FillTestTypeList();
            FillObjectList();
            GetAll();
            FillDepartmentListVnio();
            FillDoctorVnio();
            //Gán hàm xử lý sự kiện click chuột phải trên lưới
            grdObjectType.MouseUp += GridRightClick;
            grdTestTypeList.MouseUp += GridRightClick;
            //grdDataControl.MouseUp += GridRightClick;
            
        }

        private void FrmGeneralReport_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
                case Keys.F4:
                    {
                        btnOK.PerformClick();
                        break;
                    }
            }
        }

        private void cboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cboDate.SelectedIndex;

                dtpFromDate.Enabled = index == cboDate.Items.Count - 1;
                dtpTodate.Enabled = index == cboDate.Items.Count - 1;

                if (index == 0)
                {
                    dtpFromDate.Value = DateTime.Now;
                    dtpTodate.Value = dtpFromDate.Value;
                }
                else if (index == 1)
                {
                    dtpFromDate.Value = DateTime.Now.AddDays(-1);
                    dtpTodate.Value = dtpFromDate.Value;
                }
                else
                {
                    dtpFromDate.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Hàm xử lý sự kiện click chuột phải trên Grid, focus vào lưới đc MouseRight
        private void GridRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (sender != null) (sender as DataGridViewX).Focus();
            }
        }

        //Xử lý khi click vào dòng của mỗi lưới

        /// <summary>
        /// hàm thực hiện in báo cáo danh sách lên báo cáo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        private bool InvaliData(DataTable _dsReport)
        {
            if (_dsReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo");
                return false;
            }
            return true;
        }

        private void GetDataReportDailyDepartmentVnio(ref DataTable dtreportvm, DateTime fromdate, DateTime toDate,
                                                      string strDepartment,string strTestTypeId)
        {
            try
            {
                dtreportvm.Clear();
                fromdate = dtpFromDate.Value;
                toDate = dtpTodate.Value;
                dtreportvm = ReportBusiness.GetDataReportDailyDepartMentVnio(fromdate, toDate, strDepartment, strTestTypeId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Getsday(DateTime pv_date)
        {
            return "Ngày " + pv_date.Day + " tháng " + pv_date.Month + " năm " + pv_date.Year;
        }

        private void ReportDepartmentVnio(DataTable m_dsReport)
        {
            //ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable DT = new ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable();
            string sTungayDenNgay = Getsday(Convert.ToDateTime(dtpFromDate.Text)) + " --- đến --- " +
                                    Getsday(Convert.ToDateTime(dtpTodate.Text));

            if (!InvaliData(_dsReport)) return;
            var crpt = new crpt_DailyTestReportDepartment();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dsReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                //                                                      "    Nhân viên                                                                   "
                //                                                          .Replace("#$X$#",
                //                                                                   Strings.Chr(34) + "&Chr(13)&" +
                //                                                                   Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                //crpt.SetParameterValue("ShowGroup", "1");
                crpt.SetParameterValue("sTungayDenNgay", sTungayDenNgay);
                // crpt.SetParameterValue("TotalTest", intGetTotalTest( DT));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }

        /// <summary>
        /// hàm thực hiện lấy tổng test
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string SelectedIdToStringForSql(DataGridViewX grd, string colXName, string colIDName)
        {
            if (grd == null) return "-1";

            string strDoctor = "-1";
            Int32 count = 0;

            for (int i = 0; i < grd.Rows.Count; i++)
            {
                if (grd.Rows[i].Cells[colXName].Value.ToString() == "1")
                {
                    strDoctor += "," + Utility.Int32Dbnull(grd.Rows[i].Cells[colIDName].Value, -1);
                    count += 1;
                }
            }
            if ((count == 0) || (count == grd.Rows.Count))
            {
                return "-1";
            }
            else return strDoctor;
        }

        private int SelectedIdToStringForSqlV2(DataGridViewX grd, string colXName, string colIDName)
        {
            if (grd == null) return Convert.ToInt32("-1");

            string strDoctor = "";
            Int32 count = 0;

            for (int i = 0; i < grd.Rows.Count; i++)
            {
                if (grd.Rows[i].Cells[colXName].Value.ToString() == "1")
                {
                    strDoctor += "," + Utility.Int32Dbnull(grd.Rows[i].Cells[colIDName].Value, -1);
                    count += 1;
                }
            }
            if ((count == 0) || (count == grd.Rows.Count))
            {
                return Convert.ToInt32("-1");
            }
            return Convert.ToInt32(strDoctor = "-1");
            // else return Convert.ToInt32(strDoctor.ToString());
        }

        private void chkChon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView dr in dsDepartmentDoctor.Tables[0].DefaultView)
                {
                    dr["CHON"] = chkChon.Checked ? 1 : 0;
                }
                dsDepartmentDoctor.Tables[0].AcceptChanges();
            }
            catch (Exception ex)
            {
            }
        }

        private void chkUncheck_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView dr in dsDepartmentDoctor.Tables[0].DefaultView)
                {
                    dr["CHON"] = dr["CHON"].ToString() == "0" ? 1 : 0;
                }
                dsDepartmentDoctor.Tables[0].AcceptChanges();
            }
            catch (Exception ex)
            {
            }
        }

        #region FillGrid

        private void FillObjectList()
        {
            try
            {
                grdObjectType.AutoGenerateColumns = false;
                grdObjectType.DataSource = ReportBusiness.GetAllObjectList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FillDoctorVnio()
        {
            try
            {
                grdDoctor.AutoGenerateColumns = false;
                grdDoctor.DataSource = ReportBusiness.GetAllDoctorVnio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GetAll()
        {
            try
            {
                dsDepartmentDoctor = ReportBusiness.GetAllDepartmentDoctorVnio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FillDepartmentListVnio()
        {
            try
            {
                grdDepartment.AutoGenerateColumns = false;
                grdDepartment.DataSource = dsDepartmentDoctor.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void FillTestTypeList()
        {
            try
            {
                grdTestTypeList.AutoGenerateColumns = false;
                _Testtype = ReportBusiness.GetAllTestTypeList();
                grdTestTypeList.DataSource = _Testtype;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region ColX_Selection

        private void grdObjectType_Click(object sender, EventArgs e)
        {
            grdObjectType.CurrentRow.Cells["colX_Object"].Value =
                grdObjectType.CurrentRow.Cells["colX_Object"].Value.ToString() == "1" ? 0 : 1;
        }

        private void grdObjectType_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                grdObjectType.CurrentRow.Cells["colX_Object"].Value =
                    grdObjectType.CurrentRow.Cells["colX_Object"].Value.ToString() == "1" ? 0 : 1;
            }
        }

        #endregion

        #region ContextStripMenu_Selection

        private void tsmSelectAll_Click(object sender, EventArgs e)
        {
            if (grdObjectType != null && grdObjectType.Focused)
            {
                foreach (DataGridViewRow dr in grdObjectType.Rows)
                {
                    dr.Cells[0].Value = 1;
                }
            }

            //if (grdDataControl != null && grdDataControl.Focused)
            //{
            //    foreach (DataGridViewRow dr in grdDataControl.Rows)
            //    {
            //        dr.Cells[0].Value = 1;
            //    }
            //}
        }

        private void tsmDeSelectAll_Click(object sender, EventArgs e)
        {
            if (grdTestTypeList != null && grdTestTypeList.Focused)
            {
                foreach (DataGridViewRow dr in grdTestTypeList.Rows)
                {
                    dr.Cells[0].Value = 0;
                }
            }
            if (grdObjectType != null && grdObjectType.Focused)
            {
                foreach (DataGridViewRow dr in grdObjectType.Rows)
                {
                    dr.Cells[0].Value = 0;
                }
            }
        }

        private void tsmReverseSelection_Click(object sender, EventArgs e)
        {
            if (grdTestTypeList != null && grdTestTypeList.Focused)
            {
                foreach (DataGridViewRow dr in grdTestTypeList.Rows)
                {
                    dr.Cells[0].Value = dr.Cells[0].Value.ToString() == "0" ? 1 : 0;
                }
            }
            if (grdObjectType != null && grdObjectType.Focused)
            {
                foreach (DataGridViewRow dr in grdObjectType.Rows)
                {
                    dr.Cells[0].Value = dr.Cells[0].Value.ToString() == "0" ? 1 : 0;
                }
            }
        }

        #endregion

        private void btnOK_Click_1(object sender, EventArgs e)
        {
            try
            {
                string strTestTypeId = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
                //string strTestTypeIdV2 = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
                //int strTestTypeIdV3 = SelectedIdToStringForSqlV2(grdTestTypeList, "colX_TestType", "colID_TestType");
                //string strObjectId = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
                //string strObjectIdV2 = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
                string strDoctor = SelectedIdToStringForSql(grdDepartment, "CHON", "idkhoa");
                //short strObjectIdV3 =
                //    Convert.ToInt16(SelectedIdToStringForSqlV2(grdObjectType, "colX_Object", "colID_Object"));
                DateTime fromdate = dtpFromDate.Value;
                DateTime todate = dtpFromDate.Value;
                var dataTable = new DataTable();
                GetDataReportDailyDepartmentVnio(ref _dsReport, fromdate, todate, strDoctor, strTestTypeId);
                ReportDepartmentVnio(_dsReport);


            }
            catch (Exception)
            {
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}