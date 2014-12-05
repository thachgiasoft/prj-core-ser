using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using Janus.Windows.GridEX;
using Microsoft.VisualBasic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Report.Business;
using Vietbait.Lablink.Report.UI.Datasets;
using Vietbait.Lablink.Report.UI.Reports;

namespace Vietbait.Lablink.Report.UI

{
    public partial class FrmReportAll_VNIO : Office2007Form
    {
        #region "KHAI BAO THUOC TINH"

        private bool ClickGetData;
        private DataTable _Objecttype = new DataTable();
        private DataTable _Testtype = new DataTable();
        private DataTable _dsReport = new DataTable();
        public DataSet dsDepartmentDoctor = new DataSet();
        private string pid;

        #endregion

        public FrmReportAll_VNIO()
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
            //cboAoThat.SelectedIndex = 0;
            GetAll();
            LoadSoAoThat();
            FillNOINGOAITRU();
            //cboReportType.SelectedIndex = 0;
            FillTestTypeList();
            FillObjectList();
            FillDepartmentList();
            //Gán hàm xử lý sự kiện click chuột phải trên lưới
            grdObjectType.MouseUp += GridRightClick;
            grdTestTypeList.MouseUp += GridRightClick;
            //grdDataControl.MouseUp += GridRightClick;
        }

        private void FillNOINGOAITRU()
        {
            try
            {
                grdNoiNgoaiTru.AutoGenerateColumns = false;
                grdNoiNgoaiTru.DataSource = ReportBusiness.GetAllDepartmentDoctorVnio().Tables[2];
            }
            catch (Exception)
            {
                throw;
            }
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

        private void LoadSoAoThat()
        {
            var dt = new DataTable();
            dt.Columns.Add("DISPLAY");
            dt.Columns.Add("VALUE");


            DataRow dr = dt.NewRow();
            dr["DISPLAY"] = "SỐ THẬT";
            dr["VALUE"] = "1";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["DISPLAY"] = "SỐ ẢO";
            dr["VALUE"] = "0";
            dt.Rows.Add(dr);
            cboAoThat.DataSource = dt;
            cboAoThat.DisplayMember = "DISPLAY";
            cboAoThat.ValueMember = "VALUE";
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
            try
            {
                Utility.WaitNow(this);
                string strTestTypeId = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
                //string strTestTypeIdV2 = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
                int strTestTypeIdV3 = SelectedIdToStringForSqlV2(grdTestTypeList, "colX_TestType", "colID_TestType");
                //string strObjectId = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
                string strObjectIdV = GetSelectedObjectId(grdObjectType, "colX_Object", "colID_Object");

                //string strObjectIdV2 = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
                string reportType = "";
                //short strObjectIdV3 =
                //Convert.ToInt16(SelectedIdToStringForSqlV2(grdObjectType, "colX_Object", "colID_Object"));
                int noitru = SelectedIdToStringForSqlV2(grdNoiNgoaiTru, "colChon", "colnoitru");
                DateTime fromdate = dtpFromDate.Value;
                DateTime todate = dtpFromDate.Value;
                var dataTable = new DataTable();
                string soaosothat = cboAoThat.SelectedValue.ToString();
                GetDataReportVienmat(ref _dsReport, fromdate, todate, noitru, strObjectIdV, strTestTypeId, soaosothat);
                //GetDataReportVienmat(ref _dsReport, fromdate, todate, strObjectIdV, noitru, strTestTypeId, chkThucAo.Checked);
                InvaliData(_dsReport);
                grdReport.DataSource = _dsReport;
                //dataTable = ProcessDataCreenTest();
                //     PrintCreenTest(dataTable);
                ClickGetData = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Utility.DefaultNow(this);
            }
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
        private void GetDataReportVienmat(ref DataTable dtreportvm, DateTime fromdate, DateTime toDate, int noitru,
                                          string  strObjectId, string strTestTypeId, string reporttype)
        {
            dtreportvm.Clear();
            fromdate = dtpFromDate.Value;
            toDate = dtpTodate.Value;
            dtreportvm = ReportBusiness.ReportALlVnio(fromdate, toDate, noitru, strObjectId, strTestTypeId, reporttype);
        }

        /// <summary>
        /// thuc hien in bao cao theo kieu thu 3 la in bao cao so luong hang ngay
        /// </summary>
        /// <param name="m_dsReport"></param>
        public string Getsday(DateTime pv_date)
        {
            return string.Format("Ngày {0} tháng {1} năm {2}", pv_date.Day, pv_date.Month, pv_date.Year);
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
            //var DT = new ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable();
            
            string sTungayDenNgay = dtpFromDate.Value.Date != dtpTodate.Value.Date
                                        ? string.Format("{0} --- đến --- {1}", Getsday(dtpFromDate.Value),
                                                        Getsday(dtpTodate.Value))
                                        : Getsday(dtpFromDate.Value);

            if (!InvaliData(_dsReport)) return;
            var crpt = new crpt_DailyTotalTestReportDetailVNIO();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dsReport);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
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

            int strDoctor = 0;
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

        private string GetSelectedObjectId(DataGridViewX grd, string colXName, string colIDName)
        {
            return grd.Rows.Cast<DataGridViewRow>().Where(row => row.Cells[colXName].Value.ToString() == "1").Aggregate("", (current, row) => string.Format("{0}{1},", current, row.Cells[colIDName].Value));
        }

        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdDataControl.Enabled = cboReportType.SelectedIndex>= 3;
            //grdObjectType.Enabled = cboReportType.SelectedIndex < 5;
            //grdTestTypeList.Enabled = cboReportType.SelectedIndex < 5;
        }

        private void chkThucAo_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility .WaitNow(this);
                string strTestTypeId = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");

                //string strObjectId = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
                int strObjectIdV = SelectedIdToStringForSqlV2(grdObjectType, "colX_Object", "colID_Object");
                int noitru = SelectedIdToStringForSqlV2(grdNoiNgoaiTru, "colChon", "colnoitru");
                DateTime fromdate = dtpFromDate.Value;
                DateTime todate = dtpFromDate.Value;
                var dataTable = new DataTable();
                //if (!ClickGetData)
                //GetDataReportVienmat(ref _dsReport, fromdate, todate,noitru, strObjectIdV ,strTestTypeId, chkThucAo.Checked);

                PrintReportDetailTotalTest(_dsReport);
                ClickGetData = false;
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                Utility .DefaultNow(this);
            }
        }

        private void grdReport_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            grdReport.UpdateData();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Close();
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
    }
}