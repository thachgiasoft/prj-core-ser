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
    public partial class FrmGeneralReport : Office2007Form
    {
        #region "KHAI BAO THUOC TINH"

        private DataTable _Objecttype = new DataTable();
        private DataTable _Testtype = new DataTable();
        private DataTable _dsReport = new DataTable();
        private string pid;

        #endregion

        public FrmGeneralReport()
        {
            InitializeComponent();
            ReportBusiness.CreateManagementUnit();
        }

        private void FrmGeneralReport_Load(object sender, EventArgs e)
        {
            cboDate.SelectedIndex = 0;
            cboReportType.SelectedIndex = 0;

            FillTestTypeList();
            FillObjectList();
            FillDepartmentList();
            FillDoctorList();

            //Gán hàm xử lý sự kiện click chuột phải trên lưới
            grdObjectType.MouseUp += GridRightClick;
            grdTestTypeList.MouseUp += GridRightClick;
            grdDoctor.MouseUp += GridRightClick;
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
        private void btnOK_Click(object sender, EventArgs e)
        {
            string strTestTypeId = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
            string strTestTypeIdV2 = SelectedIdToStringForSql(grdTestTypeList, "colX_TestType", "colID_TestType");
            int strTestTypeIdV3 = SelectedIdToStringForSqlV2(grdTestTypeList, "colX_TestType", "colID_TestType");
            string strObjectId = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
            string strObjectIdV2 = SelectedIdToStringForSql(grdObjectType, "colX_Object", "colID_Object");
            string strDoctorId = SelectedIdToStringForSql(grdDoctor, "colX_Doctor", "colID_Doctor");
            int strDoctorIdV2 = SelectedIdToStringForSqlV2(grdDoctor, "colX_Doctor", "colID_Doctor");
            short strObjectIdV3 =
                Convert.ToInt16(SelectedIdToStringForSqlV2(grdObjectType, "colX_Object", "colID_Object"));

            var dataTable = new DataTable();
            switch (cboReportType.SelectedIndex)
            {
                    //case 4:
                    //    GetDataForPrintingDailyV2(ref _dsReport, strTestTypeIdV2, strObjectIdV2);
                    //   PrintDailyDetailV2(_dsReport);
                    //    break;
                case 2:
                    GetDataForPrintingDaily(ref _dsReport, strTestTypeId, strObjectId, strDoctorId);
                    PrintDaily(_dsReport);
                    break;
                case 0:
                    GetDataForCountingEachTest(ref _dsReport, strTestTypeId, strObjectId, strDoctorId);
                    PrintForCountingEachTest(_dsReport);
                    break;
                case 3:
                    dataTable = ProcessDataCreenTest();
                    PrintCreenTest(dataTable);
                    break;
                case 4:
                    GetDataForPrintingDailyDetailV2(ref _dsReport, strTestTypeId, strObjectId, strDoctorIdV2, pid);

                    //GetDataForPrintingDailyDetail(ref _dsReport, strTestTypeId, strObjectId, strDoctorIdV2);
                    PrintDailyParaDetail(_dsReport);
                    break;
            }
        }

        private DataTable ProcessDataCreenTest()
        {
            DataTable dt_CreenTest = ReportBusiness.GetDataCreenTest(dtpFromDate.Value, dtpTodate.Value);


            //Lấy về bảng các Test_ID trong L_Creen để sàng lọc
            DataTable creenInvertHeaderNames = new Select().From("L_Creen").ExecuteDataSet().Tables[0];

            //dt_CreenInvert là đảo của bảng dt_CreenTest
            var dt_CreenInvert = new DataTable();
            //Đặt tên cho các cột của dt_CreenInvert theo Datacontrol_ID của các test được sàng lọc
            dt_CreenInvert.Columns.Add("barcode", typeof (string));
            foreach (DataRow dr in creenInvertHeaderNames.Rows)
            {
                dt_CreenInvert.Columns.Add(dr["DataControl_ID"].ToString(), typeof (string));
            }

            dt_CreenTest.Rows.InsertAt(dt_CreenTest.NewRow(), 0);
            DataRow drI = dt_CreenInvert.NewRow();

            for (int i = 1; i < dt_CreenTest.Rows.Count; i++)
            {
                if (dt_CreenTest.Rows[i]["barcode"].ToString() != "")
                {
                    if (dt_CreenTest.Rows[i]["barcode"].ToString() != dt_CreenTest.Rows[i - 1]["barcode"].ToString())
                    {
                        //Khi barcode của dòng hiện tại khác dòng trước thì add row dri vào dt_CreenInver và
                        //đưa các cột  của row drI về giá trị ""Không có test"
                        //DataRow drI = dt_CreenInvert.NewRow();

                        drI = dt_CreenInvert.NewRow();
                        drI["barcode"] = dt_CreenTest.Rows[i]["barcode"].ToString();
                        for (int j = 1; j <= creenInvertHeaderNames.Rows.Count; j++)
                        {
                            drI[j] = "Không có test";
                        }
                        //Gán kết quả vào row drI với column name = (DataControl_ID trong bảng dt_CreenTest)
                        drI[dt_CreenTest.Rows[i]["DataControl_ID"].ToString()] =
                            dt_CreenTest.Rows[i]["Test_Result"].ToString();

                        dt_CreenInvert.Rows.Add(drI);
                    }
                    else
                    {
                        //DataRow drI = dt_CreenInvert.Rows[dt_CreenInvert.Rows.Count - 1];
                        drI[dt_CreenTest.Rows[i]["DataControl_ID"].ToString()] =
                            dt_CreenTest.Rows[i]["Test_Result"].ToString();
                    }
                }
            }

            //Đổi tên cột của bảng để map với Report
            for (int i = 1; i <= creenInvertHeaderNames.Rows.Count; i++)
            {
                dt_CreenInvert.Columns[i].ColumnName = "Para_" + i;
            }
            return dt_CreenInvert;
        }

        private void GetDataForCountingEachTest(ref DataTable _dsReport, string strTestTypeId, string strObjectId,
                                                string strDoctorId)
        {
            _dsReport.Clear();
            _dsReport = ReportBusiness.GetDataForCountingEachTest(dtpFromDate.Value, dtpTodate.Value, strTestTypeId,
                                                                  strObjectId, strDoctorId);
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
        private void GetDataForPrintingDaily(ref DataTable _dsReport, string strTestTypeId, string strObjectId,
                                             string strDoctorId)
        {
            _dsReport.Clear();
            _dsReport = ReportBusiness.GetAllDailyTestType(dtpFromDate.Value, dtpTodate.Value, strTestTypeId,
                                                           strObjectId, strDoctorId);
        }

        //private void GetDataForPrintingDailyDetail(ref DataTable _dsReport, string strTestTypeId, string strObjectId, int strDepartMentId)
        //{
        //    _dsReport.Clear();
        //    _dsReport = ReportBusiness.GetAllDailyTestPatient(dtpFromDate.Text, dtpTodate.Text, strTestTypeId,strObjectId, strDepartMentId);
        //}

        private void GetDataForPrintingDailyDetailV2(ref DataTable _dsReport, string strTestTypeId, string strObjectId,
                                                     int strDepartMentId, string pid)
        {
            _dsReport.Clear();
            _dsReport = ReportBusiness.GetAllDailyTestTypeV2(dtpFromDate.Text, dtpTodate.Text, strTestTypeId,
                                                             strObjectId, strDepartMentId, "NOTHING");
        }

        private void GetDataForPrintingDailyV2(ref DataTable _dsReport, string strTestTypeId, string strObjectId)
        {
            _dsReport.Clear();
            _dsReport = ReportBusiness.GetAllDailyTestTypeClear(dtpFromDate.Value, dtpTodate.Value, strTestTypeId,
                                                                strObjectId);
        }


        /// <summary>
        /// thuc hien in bao cao theo kieu thu 3 la in bao cao so luong hang ngay
        /// </summary>
        /// <param name="m_dsReport"></param>
        private void PrintDaily(DataTable m_dsReport)
        {
            if (!InvaliData(_dsReport)) return;
            var crpt = new crpt_ThongkesoluongXN();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dsReport);
                crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                                                                      "    Nhân viên                                                                   "
                                                                          .Replace("#$X$#",
                                                                                   Strings.Chr(34) + "&Chr(13)&" +
                                                                                   Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("ParentBranchName",
                                       LablinkBusinessConfig.GetParentBranchName());
                crpt.SetParameterValue("BranchName", LablinkBusinessConfig.GetBranchName());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                Utility.DefaultNow(this);
            }
        }

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

        private void PrintDailyDetailV2(DataTable m_dsReport)
        {
            var DT = new ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable();
            string sTungayDenNgay = Getsday(Convert.ToDateTime(dtpFromDate.Text)) + " --- đến --- " +
                                    Getsday(Convert.ToDateTime(dtpTodate.Text));

            if (!InvaliData(_dsReport)) return;
            var crpt = new crpt_DailyTestReportDetail();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dsReport);
                crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                                                                      "    Nhân viên                                                                   "
                                                                          .Replace("#$X$#",
                                                                                   Strings.Chr(34) + "&Chr(13)&" +
                                                                                   Strings.Chr(34)) + Strings.Chr(34);
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


        private void PrintDailyParaDetail(DataTable m_dsReport)
        {
            //ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable DT = new ds_dailyDetailTestReport.dtDailyDetailTestReportDataTable();
            string sTungayDenNgay = Getsday(Convert.ToDateTime(dtpFromDate.Text)) + " --- đến --- " +
                                    Getsday(Convert.ToDateTime(dtpTodate.Text));

            if (!InvaliData(_dsReport)) return;
            var crpt = new crpt_DailyParamTestReport1();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dsReport);
                crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                                                                      "    Nhân viên                                                                   "
                                                                          .Replace("#$X$#",
                                                                                   Strings.Chr(34) + "&Chr(13)&" +
                                                                                   Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                crpt.SetParameterValue("ShowGroup", "1");
                // crpt.SetParameterValue("sTungayDenNgay", sTungayDenNgay);
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

        private void PrintCreenTest(DataTable m_dsReport)
        {
            if (!InvaliData(m_dsReport)) return;
            var crpt = new crpt_CreenTest();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(m_dsReport);
                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) + "    Nhân viên                                                                   ".Replace("#$X$#", Strings.Chr(34) + "&Chr(13)&" + Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("ParentBranchName",
                                       LablinkBusinessConfig.GetParentBranchName());
                crpt.SetParameterValue("Para_1", "1");
                crpt.SetParameterValue("Para_2", "2");
                crpt.SetParameterValue("Para_3", "3");
                crpt.SetParameterValue("Para_4", "4");
                crpt.SetParameterValue("Para_5", "5");
                crpt.SetParameterValue("BranchName", LablinkBusinessConfig.GetBranchName());
                crpt.SetParameterValue("sTuNgayDenNgay", Utility.FromToDateTime(dtpFromDate.Text, dtpTodate.Text));
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
        /// hàm thực hiện in báo cáo số lượng hàng ngày
        /// Kiểu báo cáo số 2
        /// </summary>
        /// <param name="m_dsReport"></param>
        private void PrintForCountingEachTest(DataTable m_dsReport)
        {
            if (!InvaliData(_dsReport)) return;
            var crpt = new crpt_DailyTestReportDetail();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                //foreach (DataRow dr in m_dsReport.Rows)
                //{
                //    dr["PrintDetail"] = 1;

                //}
                //m_dsReport.AcceptChanges();
                crpt.SetDataSource(m_dsReport);
                crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                                                                      "    Nhân viên                                                                   "
                                                                          .Replace("#$X$#",
                                                                                   Strings.Chr(34) + "&Chr(13)&" +
                                                                                   Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("ParentBranchName",
                                       LablinkBusinessConfig.GetParentBranchName());
                crpt.SetParameterValue("BranchName", LablinkBusinessConfig.GetBranchName());
                crpt.SetParameterValue("TotalTest", GetTotalTest(m_dsReport));
                crpt.SetParameterValue("sTuNgayDenNgay", Utility.FromToDateTime(dtpFromDate.Text, dtpTodate.Text));
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

        private void cboReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //grdDataControl.Enabled = cboReportType.SelectedIndex>= 3;
            grdObjectType.Enabled = cboReportType.SelectedIndex < 5;
            grdTestTypeList.Enabled = cboReportType.SelectedIndex < 5;
            grdDoctor.Enabled = cboReportType.SelectedIndex < 5;
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

        private void FillDoctorList()
        {
            try
            {
                grdDoctor.AutoGenerateColumns = false;
                grdDoctor.DataSource = ReportBusiness.GetAllDoctor();
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

        private void grdTestTypeList_Click(object sender, EventArgs e)
        {
            grdTestTypeList.CurrentRow.Cells["colX_TestType"].Value =
                grdTestTypeList.CurrentRow.Cells["colX_TestType"].Value.ToString() == "1" ? 0 : 1;
        }

        private void grdTestTypeList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                grdTestTypeList.CurrentRow.Cells["colX_TestType"].Value =
                    grdTestTypeList.CurrentRow.Cells["colX_TestType"].Value.ToString() == "1" ? 0 : 1;
            }
        }

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

        private void grdDoctor_Click(object sender, EventArgs e)
        {
            grdDoctor.CurrentRow.Cells["colX_Doctor"].Value =
                grdDoctor.CurrentRow.Cells["colX_Doctor"].Value.ToString() == "1" ? 0 : 1;
        }

        private void grdDoctor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                grdDoctor.CurrentRow.Cells["colX_Doctor"].Value =
                    grdDoctor.CurrentRow.Cells["colX_Doctor"].Value.ToString() == "1" ? 0 : 1;
            }
        }

        #endregion

        #region ContextStripMenu_Selection

        private void tsmSelectAll_Click(object sender, EventArgs e)
        {
            if (grdTestTypeList != null && grdTestTypeList.Focused)
            {
                foreach (DataGridViewRow dr in grdTestTypeList.Rows)
                {
                    dr.Cells[0].Value = 1;
                }
            }
            if (grdObjectType != null && grdObjectType.Focused)
            {
                foreach (DataGridViewRow dr in grdObjectType.Rows)
                {
                    dr.Cells[0].Value = 1;
                }
            }
            if (grdDoctor != null && grdDoctor.Focused)
            {
                foreach (DataGridViewRow dr in grdDoctor.Rows)
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
            if (grdDoctor != null && grdDoctor.Focused)
            {
                foreach (DataGridViewRow dr in grdDoctor.Rows)
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
            if (grdDoctor != null && grdDoctor.Focused)
            {
                foreach (DataGridViewRow dr in grdDoctor.Rows)
                {
                    dr.Cells[0].Value = dr.Cells[0].Value.ToString() == "0" ? 1 : 0;
                }
            }
        }

        #endregion
    }
}