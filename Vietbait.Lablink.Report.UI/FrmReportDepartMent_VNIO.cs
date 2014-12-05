using System;
using System.Data;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Microsoft.VisualBasic;
using SubSonic;
using Vietbait.Lablink.Report.Business;
using Vietbait.Lablink.Report.UI.Datasets;
using Vietbait.Lablink.Report.UI.Reports;
using Vietbait.Lablink.Utilities;
using VietBaIT.CommonLibrary;

namespace Vietbait.Lablink.Report.UI

{
    public partial class FrmReportDepartMent_VNIO : Office2007Form
    {

        #region "KHAI BAO THUOC TINH"

        private DataTable _Objecttype = new DataTable();
        private DataTable _Testtype = new DataTable();
        private DataTable _dsReport = new DataTable();
        private string pid;
        public DataSet dsDepartmentDoctor = new DataSet();
        private bool ClickGetData = false;


        #endregion

        public FrmReportDepartMent_VNIO()
        {
            InitializeComponent();
            ReportBusiness.CreateManagementUnit();
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
        private void FrmGeneralReport_Load(object sender, EventArgs e)
        {
            cboDate.SelectedIndex = 0;
            GetAll();
            //FillNOINGOAITRU();
            //cboReportType.SelectedIndex = 0;
            FillTestTypeList();
            FillObjectList();
            FillDepartmentList();
            FillDepartmentListVnio();
            //Gán hàm xử lý sự kiện click chuột phải trên lưới
            grdObjectType.MouseUp += GridRightClick;
            grdTestTypeList.MouseUp += GridRightClick;
            //grdDataControl.MouseUp += GridRightClick;
        }
        //void FillNOINGOAITRU()
        //{
        //    try
        //    {
        //        grdNoiNgoaiTru.AutoGenerateColumns = false;
        //        grdNoiNgoaiTru.DataSource = ReportBusiness.GetAllDepartmentDoctorVnio().Tables[2];

        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
        private void FrmReportDepartMent_VNIO_KeyDown(object sender, KeyEventArgs e)
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
                        //btnOK.PerformClick();
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
        private void btnOK_Click(object sender, EventArgs e)
        {
            //cboReporttype.SelectedIndex = 0;

            string strTestTypeId = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
            //string strTestTypeIdV2 = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
            int strTestTypeIdV3 = SelectedIdToStringForSqlV2(grdTestTypeList, "colX_TestType", "colID_TestType");
            string strDepartment = SelectedIdToStringForSql(grdDepartment, "CHON", "idkhoa");
             int strObjectIdV = SelectedIdToStringForSqlV2(grdObjectType, "colX_Object", "colID_Object");
          
            //string strObjectIdV2 = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
            string reportType = "";
                //short strObjectIdV3 =
                //Convert.ToInt16(SelectedIdToStringForSqlV2(grdObjectType, "colX_Object", "colID_Object"));
            //int noitru = SelectedIdToStringForSqlV2(grdNoiNgoaiTru, "colChon", "colnoitru");
            DateTime fromdate = dtpFromDate.Value;
            DateTime todate = dtpFromDate.Value;
            var dataTable = new DataTable();

            //GetDataReportDepartmentVnio(ref _dsReport, fromdate, todate, strTestTypeId, strObjectIdV, strDepartment);
            InvaliData(_dsReport);
            //grdReport.DataSource = _dsReport;
            //dataTable = ProcessDataCreenTest();
            //     PrintCreenTest(dataTable);
            ClickGetData = true;
        }
        private bool InvaliData(DataTable _dsReport)
        {
            if (_dsReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo");
                return false;
            }
            return true;
        }

        /// <summary>
        /// ham thuc hien lay thong tin cua phan in bao cao hang ngay theo so luong
        /// kieu thu 3 cua phan loai report
        /// </summary>
        /// <param name="_dsReport"></param>
        /// <param name="strTestTypeId"></param>
        /// <param name="strObjectId"></param>
        /// <param name="strDoctorId"></param>

        //private void GetDataReportDepartmentVnio(ref DataTable dtreportvm, DateTime fromdate, DateTime toDate, int strTestTypeId, short strObjectId, int departmentid)
        //{
        //    dtreportvm.Clear();
        //    fromdate = dtpFromDate.Value;
        //    toDate = dtpTodate.Value;
        //    dtreportvm = ReportBusiness.BaocaoLuuKq(fromdate, toDate, strTestTypeId, strObjectId, departmentid);
        //}
        /// <summary>
        /// thuc hien in bao cao theo kieu thu 3 la in bao cao so luong hang ngay
        /// </summary>
        /// <param name="m_dsReport"></param>
        public string Getsday(DateTime pv_date)
        {
            return "Ngày " + pv_date.Day + " tháng " + pv_date.Month + " năm " + pv_date.Year;
        }

        private int intGetTotalTest(ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable dt)
        {
            int Reval = 0;
            string TestType_ID = ",";
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["PrintDetail"] == "1")
                {
                    Reval += Convert.ToInt16(dr["Amount"]);
                }
                else
                {
                    if (Strings.InStr(TestType_ID, "," + dr["TestType_ID"] + ",", CompareMethod.Text) > 0)
                    {
                    }
                    else
                    {
                        TestType_ID += dr["TestType_ID"] + ",";
                        Reval += Convert.ToInt32(dr["NumOfTest"]);
                    }
                }
            }
            return Reval;
        }

        private void PrintReportDetailTotalTest(DataTable m_dsReport)
        {
            var DT = new ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable();
            string sTungayDenNgay = Getsday(Convert.ToDateTime(dtpFromDate.Text)) + " --- đến --- " +
                                    Getsday(Convert.ToDateTime(dtpTodate.Text));

            if (!InvaliData(_dsReport)) return;
            var crpt = new VD_1C_crpt_DailyParamTestReport1();
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
                crpt.SetParameterValue("sCondition", sTungayDenNgay);
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


       
        /// <summary>
        /// hàm thực hiện lấy tổng test
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int GetTotalTest(DataTable dt)
        {
            int Reval = 0;
            string TestType_ID = ",";
            if (dt.Rows != null)
            {
                Reval = Utility.Int32Dbnull(dt.Rows[0]["Amount"], 0);
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TESTTYPE_ID"].ToString() != dt.Rows[i - 1]["TESTTYPE_ID"].ToString())
                    {
                        Reval += Utility.Int32Dbnull(dt.Rows[i]["NumOfTest"]);
                    }
                }
                //foreach (DataRow dr in dt.Rows)
                //{
                //    if (dr["PrintDetail"].ToString() == "1")
                //    {
                //        Reval += Utility.Int32Dbnull(dr["Amount"], 0);
                //    }
                //    else
                //    {
                //        if (Strings.InStr(TestType_ID, "," + dr["TestType_ID"] + ",", CompareMethod.Text) > 0)
                //        {
                //        }
                //        else
                //        {
                //            TestType_ID += dr["TestType_ID"] + ",";
                //            Reval += Utility.Int32Dbnull(dr["NumOfTest"]);
                //        }
                //    }
                //}
            }
            return Reval;
        }

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

            int strDoctor =0;
            Int32 count = 0;

            for (int i = 0; i < grd.Rows.Count; i++)
            {
                if (grd.Rows[i].Cells[colXName].Value.ToString() == "1")
                {
                    strDoctor += Utility.Int32Dbnull(grd.Rows[i].Cells[colIDName].Value, -1);
                    count += 1;
                }
            }
            if ((count == 0) || (count == grd.Rows.Count))
            {
                return Convert.ToInt32("-1");
            }
            return Convert.ToInt32(strDoctor);
            // else return Convert.ToInt32(strDoctor.ToString());
        }

        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdDataControl.Enabled = cboReportType.SelectedIndex>= 3;
            //grdObjectType.Enabled = cboReportType.SelectedIndex < 5;
            //grdTestTypeList.Enabled = cboReportType.SelectedIndex < 5;
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

        private void FillDepartmentList()
        {
            try
            {
                //grdDataControl.AutoGenerateColumns = false;
                //grdDataControl.DataSource = ReportBusiness.GetAllDepartment();
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

        private void chkThucAo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            string strTestTypeId = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
            string strDepartment = SelectedIdToStringForSql(grdDepartment, "CHON", "idkhoa");
          
                //string strObjectId = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
            int strObjectIdV = SelectedIdToStringForSqlV2(grdObjectType, "colX_Object", "colID_Object");
          //int noitru = SelectedIdToStringForSqlV2(grdNoiNgoaiTru, "colChon", "colnoitru");
            DateTime fromdate = dtpFromDate.Value;
            DateTime todate = dtpFromDate.Value;
            var dataTable = new DataTable();
            if(!ClickGetData)
                //GetDataReportVienmat(ref _dsReport, fromdate, todate, noitru, strObjectIdV, strTestTypeId, chkThucAo.Checked);
                //GetDataReportDepartmentVnio(ref _dsReport, fromdate, todate, strTestTypeId, strObjectIdV, strDepartment);
            //InvaliData(_dsReport);
            PrintReportDetailTotalTest(_dsReport);
            //ClickGetData = false;

        }

        private void grdReport_CellUpdated(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            //grdReport.UpdateData();
        }
    }
}