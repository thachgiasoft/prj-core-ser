using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Interface;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Report.UI.Reports;
using Vietbait.Lablink.Utilities;
using Vietbait.TestInformation.Business;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmTestDetailV3 : Form
    {
        #region WarningBox Status Class

        private class WarningBoxStatus
        {
            public WarningBoxStatus(string displayText, bool blinkStatus)
            {
                DisplayText = displayText;
                BlinkStatus = blinkStatus;
            }

            public string DisplayText { get; set; }
            public bool BlinkStatus { get; set; }
        }

        #endregion

        #region Attributes

        //Biến hệ thống để xác nhận đang load dữ liệu

        //Biến phục vụ cho việc load dữ liệu động lên lưới
        private readonly DataTable _grdPatientsSource = new DataTable();

        // BackgroundWorker để nạp dữ liệu trên một luồng khác 
        private BackgroundWorker _bgProcessing;
        private DataTable _dtTestStatus;
        private DataTable _patientTestTypeRegistered;
        private string _reportType = LablinkBusinessConfig.GetReportType();
        private string _rowFilter = "1=1";
        private DataTable _tblPatients;
        private DataTable _testAllResult;
        private int _testTypeId = GetTestTypeId();
        private ActionResult actionResult = ActionResult.Error;
        private DataSet ds = new DataSet();
        private DataTable dtTestInfo;
        private DataTable dtTestTypeList;
        private action em_Action = action.doNothing;
        private DataTable m_dtResultDetail = new DataTable();
        private DataTable m_dtTestInfo = new DataTable();
        private string rowfilter = "1=1";
        private string selectedPatientId;
        private int v_PatientId = -1;
        //  private string testIdString;

        private int v_Test_ID = -1;

        #endregion

        #region Contructor

        public FrmTestDetailV3()
        {
            InitializeComponent();
            InitializeEventControl();
            TestInfoBusiness.CreateManagementUnit();
            //Add Handler for All checkbox
            //cboCoKetQua.CheckedChanged += CboTatcaCheckedChanged;
            //cboChuaCoKetQua.CheckedChanged += CboTatcaCheckedChanged;
            grdPatients.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            grdResultDetail.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            grdTestInfo.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            
            grdPatients.AutoGenerateColumns = false;
            grdTestInfo.AutoGenerateColumns = false;
            grdResultDetail.AutoGenerateColumns = false;
            // grdResultByTestType.AutoGenerateColumns = false;
        }

        #endregion

        #region Private Method

        #region Set Text For WarningBox

        /// <summary>
        /// Set text cho Warningbox và kiểu Blink
        /// </summary>
        /// <param name="displayText"></param>
        /// <param name="blinkStatus"></param>
        private void SetWarningStatus(WarningBoxStatus status)
        {
            SetControlPropertyThreadSafe(warningBox1, "Text", status.DisplayText);
            if (!status.BlinkStatus) warningBox1.ColorScheme = eWarningBoxColorScheme.Default;
            WarningBoxTimer.Enabled = status.BlinkStatus;
        }

        private static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe),
                               new[] {control, propertyName, propertyValue});
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control,
                                               new[] {propertyValue});
            }
        }

        private static object GetControlPropertyThreadSafe(Control control, string propertyName)
        {
            //object result = null;
            if (control.InvokeRequired)
            {
                return control.Invoke(new GetControlPropertyThreadSafeDelegate(GetControlPropertyThreadSafe),
                                      new object[] {control, propertyName});
            }
            else
            {
                return control.GetType().InvokeMember(propertyName, BindingFlags.GetProperty, null, control, null);
            }
            //return result;
        }

        #region Nested type: GetControlPropertyThreadSafeDelegate

        private delegate object GetControlPropertyThreadSafeDelegate(Control control, string propertyName);

        #endregion

        #region Nested type: SetControlPropertyThreadSafeDelegate

        private delegate void SetControlPropertyThreadSafeDelegate(
            Control control, string propertyName, object propertyValue);

        #endregion

        #endregion

        private void GetAllStyle()
        {
            //cboFormStyle.SelectedValueChanged -= cboStyle_SelectedValueChanged;
            //cboFormStyle.Items.Clear();
            //foreach (eStyle style in Enum.GetValues(typeof (eStyle)))
            //{
            //    cboFormStyle.Items.Add(style);
            //}
            //cboFormStyle.SelectedIndex = 0;
            //cboFormStyle.SelectedValueChanged += cboStyle_SelectedValueChanged;
        }

        private int MkStrWidth(DataTable dt, Font fnt, string colName)
        {
            int result = 0;
            foreach (DataRow dr in dt.Rows)
            {
                int width = TextRenderer.MeasureText(dr[colName].ToString(), fnt).Width;
                if (width > result)
                {
                    result = width;
                }
            }
            return result;
        }

        private string ShortNameTestType(string testTypeName)
        {
            if (testTypeName == null) return "";
            else
                return
                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                        testTypeName.Replace("XÉT NGHIỆM", "").Trim().ToLower());
        }

        private string GetIdString(DataTable dt, CheckedListBox clb, string colID)
        {
            try
            {
                string testIdString = string.Empty;
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    if (clb.GetItemChecked(i))
                        testIdString += dt.Rows[i][colID] + ",";
                }
                if (testIdString.EndsWith(",")) testIdString = testIdString.Remove(testIdString.Length - 1);
                if (testIdString == string.Empty) return "-3";
                else return testIdString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FillGrpDatePicker()
        {
            try
            {
                // Thêm các tùy chọn
                cboDate.Items.Add("Hôm nay");
                cboDate.Items.Add("Hôm qua");
                cboDate.Items.Add("Tùy Chọn");

                //Set giá trị mặc định cho datetimepicker
                dtpFromDate.Value = DateTime.Now;
                dtpTodate.Value = DateTime.Now;

                cboDate.SelectedIndex = 0;
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void FillSexCombobox()
        {
            cboSex.DataSource = null;
            cboSex.Items.Clear();

            var dt = new DataTable();
            dt.Columns.Add("ValueItem");
            dt.Columns.Add("DisplayItem");

            DataRow dr = dt.NewRow();
            dr[0] = -1;
            dr[1] = "";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "Nam";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Nữ";
            dt.Rows.Add(dr);

            cboSex.DataSource = dt;
            cboSex.DisplayMember = "DisplayItem";
            cboSex.ValueMember = "ValueItem";

            cboSex.SelectedIndex = 0;
        }

        private void FillTestTypeList()
        {
            try
            {
                //Xóa trắng Checklistbox

                clbTestType.DataSource = null;
                clbTestType.Items.Clear();

                dtTestTypeList = TestTypeListBusiness.GetAllTestTypeList();

                //rowfilter = "TESTTYPE_ID=" + GetTestTypeId();
                // DataTable dt = dtTestTypeList.Select("TESTTYPE_ID");
                //dtTestTypeList.DefaultView.RowFilter = rowfilter;
                // xử lý riêng phần testtype cho việt mắt chỉ hiển thị 1 testId lấy từ localAlias in file config.xml
                DataBinding.BindData(clbTestType, dtTestTypeList, TTestTypeList.Columns.TestTypeId,
                                     TTestTypeList.Columns.TestTypeName);


                //DataBinding.BindData(clbTestType, dtTestTypeList, TTestTypeList.Columns.TestTypeId,
                //                   TTestTypeList.Columns.TestTypeName);


                SetAllCheckedBoxTrue(clbTestType);

                ////Thiết lập chiều rộng tự động cho lưới hiển thị
                //clbTestType.ColumnWidth =
                //    MkStrWidth(dtTestTypeList, clbTestType.Font, TTestTypeList.Columns.TestTypeName) + 20;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int GetTestTypeId()
        {
            int result = -100;
            string filename = "Config.xml";
            string localAlias = "";
            try
            {
                //Kiểm tra sự tồn tại của file config.xml
                if (File.Exists(filename))
                {
                    XDocument xmlDoc = XDocument.Load(filename);
                    IEnumerable<string> q = from c in xmlDoc.Descendants("Config")
                                            select (string) c.Element("LOCALALIAS");
                    foreach (string name in q)
                    {
                        localAlias = name;
                    }


                    result = InterfaceHelper.GetTestTypeIdFromLocalAlias(localAlias);
                }
                else
                {
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        private void SetAllCheckedBoxTrue(CheckedListBox clb)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, true);
            }
        }

        private void FillStatusListBox()
        {
            _dtTestStatus = TestInfoBusiness.GetTestStatusList();
            DataBinding.BindData(clbStatus, _dtTestStatus, "TestStatus_ID",
                                 "TestStatus_Name");
            SetAllCheckedBoxTrue(clbStatus);
        }

        private void DisplaySearchInfo(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (_tblPatients != null)
                {
                    if (_tblPatients.Rows.Count == 0)
                    {
                        SetWarningStatus(new WarningBoxStatus(@"Không có kết bệnh nhân nào thỏa mãn điều kiện tìm kiếm",
                                                              true));
                    }
                    else
                    {
                        SetWarningStatus(
                            new WarningBoxStatus(
                                string.Format("Tìm thấy <b> {0} </b> Bệnh nhân", _tblPatients.Rows.Count),
                                false));
                    }

                    //grdPatients.DataSource = _tblPatients;
                    Utility.SetDataSourceForDataGridView(grdPatients, _tblPatients, false, true, "", "Patient_ID desc");
                    Utility.GotoNewRow(grdPatients, "colPatient_ID", v_PatientId.ToString());
                }
                circularProgress1.IsRunning = false;
                btnSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplaySearchInfo1()
        {
            try
            {
                if (_tblPatients != null)
                {
                    if (_tblPatients.Rows.Count == 0)
                    {
                        SetWarningStatus(new WarningBoxStatus(@"Không có kết bệnh nhân nào thỏa mãn điều kiện tìm kiếm",
                                                              true));
                    }
                    else
                    {
                        SetWarningStatus(
                            new WarningBoxStatus(
                                string.Format("Tìm thấy <b> {0} </b> Bệnh nhân", _tblPatients.Rows.Count),
                                false));
                    }

                    //grdPatients.DataSource = _tblPatients;
                    Utility.SetDataSourceForDataGridView(grdPatients, _tblPatients, false, true, "", "Patient_ID desc");
                    //Utility.GotoNewRow(grdPatients, "colPatient_ID", v_PatientId.ToString());
                }
                circularProgress1.IsRunning = false;
                btnSearch.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetPatientInfo(object sender, DoWorkEventArgs e)
        {
            _grdPatientsSource.Clear();
            string testIdString;
            testIdString = string.Empty;
            try
            {
                // dtTestTypeList.DefaultView.RowFilter = rowfilter;
                // Lấy về danh sách các test iD được check
                testIdString = GetIdString(dtTestTypeList, clbTestType, TTestTypeList.Columns.TestTypeId);

                // Nếu loại xét nghiệm chưa được chọn thì hiển thị thông báo 
                if (testIdString.Trim() == string.Empty)
                    throw new Exception("Bạn phải chọn loại xét nghiệm");

                // Lấy về trạng thái kết quả được tìm kiếm 
                int age = 0;
                try
                {
                    age = Convert.ToInt32(txtAge.Text);
                }
                catch (Exception)
                {
                    age = 0;
                }

                int hasTest = -1;
                //if (clbStatus.GetItemChecked(0) && !clbStatus.GetItemChecked(1)) hasTest = 0;
                //else if (!clbStatus.GetItemChecked(0) && clbStatus.GetItemChecked(1)) hasTest = 1;

                string strStatus = GetIdString(_dtTestStatus, clbStatus, LTestStatus.Columns.TestStatusId);

                int sex = Convert.ToInt32(GetControlPropertyThreadSafe(cboSex, "SelectedValue"));
                _tblPatients = TestInfoBusiness.GetTestInfoGroupedBySID(dtpFromDate.Value, dtpTodate.Value, testIdString,
                                                                        txtBarcode.Text, txtPID.Text, txtName.Text, age,
                                                                        sex, strStatus);
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        private void GetPatientInfo1()
        {
            _grdPatientsSource.Clear();
            string testIdString;
            testIdString = string.Empty;
            try
            {
                // dtTestTypeList.DefaultView.RowFilter = rowfilter;
                // Lấy về danh sách các test iD được check
                testIdString = GetIdString(dtTestTypeList, clbTestType, TTestTypeList.Columns.TestTypeId);

                // Nếu loại xét nghiệm chưa được chọn thì hiển thị thông báo 
                if (testIdString.Trim() == string.Empty)
                    throw new Exception("Bạn phải chọn loại xét nghiệm");

                // Lấy về trạng thái kết quả được tìm kiếm 
                int age = 0;
                try
                {
                    age = Convert.ToInt32(txtAge.Text);
                }
                catch (Exception)
                {
                    age = 0;
                }

                int hasTest = -1;
                //if (clbStatus.GetItemChecked(0) && !clbStatus.GetItemChecked(1)) hasTest = 0;
                //else if (!clbStatus.GetItemChecked(0) && clbStatus.GetItemChecked(1)) hasTest = 1;

                string strStatus = GetIdString(_dtTestStatus, clbStatus, LTestStatus.Columns.TestStatusId);

                int sex = Convert.ToInt32(GetControlPropertyThreadSafe(cboSex, "SelectedValue"));
                _tblPatients = TestInfoBusiness.GetTestInfoGroupedBySID(dtpFromDate.Value, dtpTodate.Value, testIdString,
                                                                        txtBarcode.Text, txtPID.Text, txtName.Text, age,
                                                                        sex, strStatus);
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        private void BindTestInfo(int v_PatientId, string testTypeId)
        {
            ds = TestInfoBusiness.GetTestInfoByPatientInfo(v_PatientId, testTypeId);
            m_dtTestInfo = ds.Tables[0];
            m_dtResultDetail = ds.Tables[1];
            grdTestInfo.AutoGenerateColumns = false;
            grdResultDetail.AutoGenerateColumns = false;
            grdTestInfo.DataSource = m_dtTestInfo;
            grdResultDetail.DataSource = m_dtResultDetail;
        }

        private void PrintAction(bool Quick)
        {
            //try
            //{
            if (grdPatients.CurrentRow != null)
            {
                //var frm = new FrmTestTypeSelection();
                //frm.dtTestTypeCLB = m_dtTestInfo;
                //frm.ShowInTaskbar = false;
                //frm.ShowDialog();
                string strSelectedTestType = Utility.GetCheckedID(m_dtTestInfo, "CHON=1",
                                                                  TTestTypeList.Columns.TestTypeId);
                //if (!frm.vCancel)
                //{
                string barcode1 = "";
                int patientId = Utility.Int32Dbnull(grdPatients.CurrentRow.Cells["colPatient_ID"].Value, -1);
                  string TestId = "-1";
                foreach (DataRow row in m_dtTestInfo.Select("CHon = 1 ANd Patient_ID = " + patientId))
                {
                    TestId += "," + row["Test_ID"];
                    barcode1 += "," + row["Barcode"];
                }
                barcode1 = barcode1.Remove(0, 1);
                TestId = TestId.Remove(0, 1);
                DataTable result = TestInfoBusiness.GetIndividualTestResult(patientId, TestId, strSelectedTestType, barcode1);

                int TestTypeID = Utility.Int32Dbnull(grdTestInfo.CurrentRow.Cells["colTestType_ID"].Value, -1);
                foreach (DataRowView drv in m_dtTestInfo.DefaultView)
                {
                    if (drv["CHON"].ToString() == "1")
                    {
                        v_Test_ID = Utility.Int32Dbnull(drv["Test_ID"], -1);
                        //if (new TestInfoBusiness().UpdateTestStatusPrinter(v_Test_ID)>0)
                        //{
                        //    drv["Printstatus"] = 1;
                        //}
                        if (new TestInfoBusiness().UpdateTestInfor(ref m_dtTestInfo, v_Test_ID) > 0)

                            em_Action = action.doNothing;
                    }
                }
                m_dtTestInfo.AcceptChanges();
                foreach (DataGridViewRow dvr in grdTestInfo.Rows)
                {
                    int printstatus = Utility.Int32Dbnull(dvr.Cells["printStatus"].Value);
                    int testStatus = Utility.Int32Dbnull(dvr.Cells["TestStatus"].Value);

                    if (printstatus == 1)
                    {
                        dvr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }


                if (!result.Columns.Contains("BarcodeImg")) result.Columns.Add("BarcodeImg", typeof (byte[]));

                byte[] arrImage = Utility.GenerateBarCode(barcode);
                foreach (DataRow dr in result.Rows)
                {
                    dr["BarcodeImg"] = arrImage;
                }
                result.AcceptChanges();
                if (result.Rows.Count > 0)
                {
                    //int report = intGetReportType();
                    //if(report==1)
                    //{

                    //}

                    var crpt = new crpt_IndividualTestResult();

                    crpt.SetDataSource(result);
                    //crpt.SetDataSource(_testAllResult);
                    crpt.SetParameterValue("ShowSubReport", 1);
                    crpt.SetParameterValue("ShowMainReport", 0);
                    crpt.SetParameterValue("ParentBranchName", globalVariables.ParentBranch_Name);
                    crpt.SetParameterValue("BranchName", globalVariables.Branch_Name);
                    // string sss = LablinkBusinessConfig.GetPhone();
                    crpt.SetParameterValue("Address", globalVariables.Branch_Address);
                    crpt.SetParameterValue("sPhone", globalVariables.Branch_Phone);

                    if (!Quick)
                    {
                        var objForm = new frmPrintPreview("", crpt, true, true);
                        objForm.ShowDialog();
                    }
                    else
                    {
                        //objForm.crptViewer.ReportSource = crpt;
                        crpt.PrintToPrinter(1, false, 0, 0);
                    }

                    Utility.DefaultNow(this);
                }
                else SetWarningStatus(new WarningBoxStatus("Bệnh nhân không có kết quả", true));
            }
            //}
            //else SetWarningStatus(new WarningBoxStatus("Không có bệnh nhân", true));
            //}
            //catch (Exception ex)
            //{
            //    SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            //}
        }

        private void ChangeColor()
        {

            try
            {
                foreach (DataGridViewRow dvr in grdPatients.Rows)
                {
                    //Lấy tất cả Test Result của BN đang được chọn
                    //selectedPatientId = grdPatients.CurrentRow.Cells[LPatientInfo.Columns.PatientId].Value.ToString();
                    // _testAllResult = TestInfoBusiness.GetIndividualTestResult(Utility.Int32Dbnull(selectedPatientId,0), "-1");
                    int _v_PatientId = Utility.Int32Dbnull(dvr.Cells["colPatient_ID"].Value, -1);
                    //   _patientTestTypeRegistered = TestInfoBusiness.GetPatientTestTypeList(selectedPatientId);
                    string _testIdString = GetIdString(dtTestTypeList, clbTestType,
                                                       TTestTypeList.Columns.TestTypeId);
                    //barcode.Visible = !
                    //                  string.IsNullOrEmpty(
                    //                      Utility.sDbnull(dvr.Cells["colBarcode"].Value, ""));
                    //barcode.Data = Utility.sDbnull(dvr.Cells["colBarcode"].Value, "");
                    BindTestInfo(_v_PatientId, _testIdString);
                    int LastStatus = 0;
                    int TatCa = m_dtTestInfo.Rows.Count;
                    int DaIn = m_dtTestInfo.Select("Printstatus=1").Length;
                    int ChuaIn = m_dtTestInfo.Select("Printstatus=0").Length;
                    if (DaIn == TatCa) LastStatus = 2;
                    if (ChuaIn == TatCa) LastStatus = 0;
                    if (DaIn > 0 && DaIn < TatCa) LastStatus = 1;
                    if (LastStatus == 1)
                    {
                        dvr.DefaultCellStyle.BackColor = Color.Red;
                    }
                    if (LastStatus == 2)
                    {
                        dvr.DefaultCellStyle.BackColor = Color.LightCyan;
                    }
                }
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        private TResultDetail CreateResultDetail()
        {
            var resultDetail = new TResultDetail();
            if (grdResultDetail.CurrentRow != null)
            {
                resultDetail.ParaName = Utility.sDbnull(Utility.GetValueFromGridColumn(grdResultDetail, "colPara_Name",
                                                                                       grdResultDetail.CurrentRow.Index),
                                                        "");

                resultDetail.TestResult = Utility.GetValueFromGridColumn(grdResultDetail, "colTest_Result",
                                                                         grdResultDetail.CurrentRow.Index);
                resultDetail.NormalLevelW = Utility.GetValueFromGridColumn(grdResultDetail, "colNormal_levelW",
                                                                           grdResultDetail.CurrentRow.Index);
                resultDetail.NormalLevel = Utility.GetValueFromGridColumn(grdResultDetail, "colNormal_Level",
                                                                          grdResultDetail.CurrentRow.Index);
                resultDetail.MeasureUnit = Utility.GetValueFromGridColumn(grdResultDetail, "colMeasure_Unit",
                                                                          grdResultDetail.CurrentRow.Index);
                resultDetail.TestDetailId =
                    Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdResultDetail, "colTestDetail_ID",
                                                                       grdResultDetail.CurrentRow.Index), -1);
            }

            return resultDetail;
        }

        /// <summary>
        /// hàm xóa kết quả
        /// </summary>
        private void DeleteResultDetail()
        {
            if (grdResultDetail.CurrentRow != null)
            {
                if (Utility.AcceptQuestion("Bạn có muốn xóa bản ghi đang chọn này không", "Thông báo", true))
                {
                    int TestDetail_ID =
                        Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdResultDetail, "colTestDetail_ID",
                                                                           grdResultDetail.CurrentRow.Index));
                    if (
                        Utility.sDbnull(Utility.GetValueFromGridColumn(grdResultDetail, "colIsNew",
                                                                       grdResultDetail.CurrentRow.Index)) == "1")
                    {
                        DataRow[] arrDr =
                            m_dtResultDetail.Select("TestDetail_ID=" + TestDetail_ID);
                        if (arrDr.GetLength(0) > 0)
                        {
                            arrDr[0].Delete();
                        }
                    }
                    else
                    {
                        TResultDetail.Delete(TestDetail_ID);
                        DataRow[] arrDr =
                            m_dtResultDetail.Select("TestDetail_ID=" + TestDetail_ID);
                        if (arrDr.GetLength(0) > 0)
                        {
                            arrDr[0].Delete();
                        }
                    }
                }
            }
        }

        private void Reset()
        {
            foreach (DataRow dr in m_dtResultDetail.Rows)
            {
                dr["IsNew"] = "1";
            }
            m_dtResultDetail.AcceptChanges();
        }

        private void SetStatusMessage()
        {
            switch (actionResult)
            {
                case ActionResult.Success:
                    ProcessData();
                    Utility.ShowMsg("Bạn thực hiện cập nhập dữ liệu thành công");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập");
                    break;
            }
        }

        private void ProcessData()
        {
            DataRow[] arrDr = m_dtTestInfo.Select("Test_ID=" + v_Test_ID);
            if (arrDr.GetLength(0) > 0)
            {
                arrDr[0][TTestInfo.Columns.TestStatus] = 80;
                arrDr[0]["TestStatus_Name"] = "Cập nhập bằng tay";
            }
            m_dtTestInfo.AcceptChanges();
        }

        /// <summary>
        /// hàm thưc hiện khởi tạo đối tượng
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private TResultDetail CreateResultDetail(DataRowView dataRow)
        {
            var resultDetail = new TResultDetail();
            resultDetail.TestId = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.TestId], -1);
            resultDetail.TestTypeId = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.TestTypeId], -1);
            resultDetail.Barcode = Utility.sDbnull(dataRow[TResultDetail.Columns.Barcode], "");
            ;
            resultDetail.PatientId = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.PatientId], -1);
            ;
            resultDetail.NormalLevel = Utility.sDbnull(dataRow[TResultDetail.Columns.NormalLevel], "");
            resultDetail.NormalLevelW = Utility.sDbnull(dataRow[TResultDetail.Columns.NormalLevelW], "");
            resultDetail.ParaName = Utility.sDbnull(dataRow[TResultDetail.Columns.ParaName], "");
            resultDetail.PrintData = Convert.ToBoolean(dataRow[TResultDetail.Columns.PrintData]);
            resultDetail.MeasureUnit = Utility.sDbnull(dataRow[TResultDetail.Columns.MeasureUnit]);
            resultDetail.ParaStatus = 1;
            resultDetail.TestDate = globalVariables.SysDate;
            resultDetail.DataSequence = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.DataSequence]);
            return resultDetail;
        }

        public int intGetReportType()
        {
            try
            {
                if (!File.Exists(Application.StartupPath + "\\ReportType.txt"))
                    return 1;
                var strRd = new StreamReader(Application.StartupPath + "\\ReportType.txt");
                string s = strRd.ReadToEnd();
                if (string.IsNullOrEmpty(s.Trim()))
                    return 1;
                switch (s.Trim())
                {
                    case "1":
                        //Vien mat
                        return 1;
                    default:
                        return 1;
                }
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// hàm thực hiện load thông tin của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTestDetail_Load(object sender, EventArgs e)
        {
            try
            {
                circularProgress1.IsRunning = true;
                checkBox1.Checked = false;
                //cboATDT.SelectedIndex = 0;
                try
                {

                  
                    //tsmiInvertSelection.Click += tsmiUnselectAll_Click;
                    //tsmiUnselectAll.Click += tsmiUnselectAll_Click;
                    //tsmiSelectAll.Click += tsmiSelectAll_Click;
                   FillGrpDatePicker();

                    // Khởi tạo Sex Combobox
                    FillSexCombobox();

                    // Load test type list to checklistbox
                    FillTestTypeList();

                    //Load Status List
                    FillStatusListBox();

                    //Get All Style from Style Manager :D
                    GetAllStyle();
                    ModifyCommand();
                    //  FillSuperTabItem();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    SetWarningStatus(new WarningBoxStatus(ex.Message, true));
                }
                finally
                {
                    circularProgress1.IsRunning = false;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// not thông tin của dùng phím tắt của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTestDetailV2_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
                case Keys.F3:
                    {
                        btnSearch.PerformClick();
                        break;
                    }
            }
            if (e.KeyCode == Keys.F4)
            {
                btnPrintPreview.PerformClick();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Q)
            {
                btnPrintQuick.PerformClick();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                btnAddPatient.PerformClick();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.U)
            {
                btnAutoUpdate.PerformClick();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
            {
                cmdAccept.PerformClick();
            }
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.C)
            {
                btnParaEntry.PerformClick();
            }
        }

        /// <summary>
        /// hàm thực hiện thay đổi của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cboDate.SelectedIndex;
                dtpFromDate.Enabled = (index == 2);
                dtpTodate.Enabled = (index == 2);
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
                else dtpFromDate.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WarningBoxTimer_Tick(object sender, EventArgs e)
        {
            warningBox1.ColorScheme = warningBox1.ColorScheme == eWarningBoxColorScheme.Default
                                          ? eWarningBoxColorScheme.Red
                                          : eWarningBoxColorScheme.Default;
        }

        private void warningBox1_OptionsClick(object sender, EventArgs e)
        {
            WarningBoxTimer.Stop();
            warningBox1.ColorScheme = eWarningBoxColorScheme.Default;
        }

        private void expandablePanel1_ExpandedChanging(object sender, ExpandedChangeEventArgs e)
        {
            if (expandablePanel1.Expanded) expandablePanel1.TitleText = "TÌM KIẾM: " + cboDate.Text;
            else expandablePanel1.TitleText = "TÌM KIẾM";
        }

        /// <summary>
        /// hàm thực hiện thêm bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            //var obj = new FrmPatientInfo("RESULT");
            //obj.em_Action = action.Insert;
            //obj.ShowDialog();
            var frm = new FrmPatientRegistration();
            frm.em_Action = action.ConfirmData;
            frm.ShowDialog();
            if (frm.b_actionStatus)
            {
                btnSearch.PerformClick();
                ModifyCommand();
            }
        }

        /// <summary>
        /// hàm thực hiện in nhanh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintQuick_Click(object sender, EventArgs e)
        {
            PrintAction(true);
        }

        /// <summary>
        /// hàm thực hiện in phiếu xem trước
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dtTestInfo.Select("CHON=1").GetLength(0) <= 0)
                {
                    Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện in phiếu", "Thông báo");
                    grdTestInfo.Focus();
                    return;
                }
                PrintAction(false);
            }
            catch (Exception)
            {
            }
            finally
            {
                ChangeColor();
            }
        }

        /// <summary>
        /// hàm thực hiện m_dtResultDetail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnParaEntry_Click(object sender, EventArgs e)
        {
            if (grdTestInfo.CurrentRow != null)
            {
                var frm = new frm_CreateNewResultDetail();


                frm.v_Device_Id =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdTestInfo, "colDevice_ID", grdTestInfo.CurrentRow.Index), -1);
                frm.v_TestType_Id =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdTestInfo, "colTestType_ID", grdTestInfo.CurrentRow.Index), -1);
                frm.g_dtResultDetail = m_dtResultDetail;
                frm.em_Action = action.doNothing;
                frm.v_Barcode = Utility.GetValueFromGridColumn(grdTestInfo, "colsBarcode", grdTestInfo.CurrentRow.Index);
                frm.v_Test_ID =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdTestInfo, "colTest_ID", grdTestInfo.CurrentRow.Index), -1);
                frm.v_Patient_ID =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdTestInfo, "colsPatient_ID", grdTestInfo.CurrentRow.Index), -1);


                frm.ShowDialog();
                if (frm.b_Status)
                {
                    m_dtResultDetail = frm.g_dtResultDetail;
                    em_Action = frm.em_Action;
                    m_dtResultDetail.AcceptChanges();
                }
            }
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện nhấn nút autouopdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoUpdate_Click(object sender, EventArgs e)
        {
            var obj = new FrmAutoUpdate();
            obj.ShowDialog();
        }

        private void grdTestInfo_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                _rowFilter = "1=2";
                if (grdTestInfo.CurrentRow != null)
                {
                    v_Test_ID = Utility.Int32Dbnull(grdTestInfo.CurrentRow.Cells["colTest_ID"].Value, -1);
                    _rowFilter = "Test_ID=" + v_Test_ID;
                }
                m_dtResultDetail.DefaultView.RowFilter = _rowFilter;
                m_dtResultDetail.AcceptChanges();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                
              
            }
        }

        private void grdTestInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //   grdTestInfo_SelectionChanged(grdTestInfo, new EventArgs());
        }

        /// <summary>
        /// hàm thực hiện gọi sự kiện khi nhấn vào nội dung của grdview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //grdPatients_SelectionChanged(grdPatients, new EventArgs());
        }

        private void chkchontat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView drv in m_dtResultDetail.DefaultView)
                {
                    drv["CHON"] = chkchontat.Checked ? 1 : 0;
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void chkdaochon_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView drv in m_dtResultDetail.DefaultView)
                {
                    drv["CHON"] = drv["CHON"].ToString() == "0" ? 1 : 0;
                }
            }
            catch (Exception exception)
            {
            }
        }

        private void chkResulted_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string _rowFilterResult = "1=1";
                if (chkResulted.Checked) _rowFilterResult = "Test_Status>=50";

                m_dtTestInfo.DefaultView.RowFilter = _rowFilterResult;
                m_dtTestInfo.AcceptChanges();
            }
            catch (Exception exception)
            {
            }
        }

        /// <summary>
        /// hàm thực hiện chuyển kết quả xét nghiệm cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeResult_Click(object sender, EventArgs e)
        {
            var frm = new frm_QuickSearch();
            frm.m_dtPatientInfo = _tblPatients;
            frm.p_dtTestInfo = m_dtTestInfo;
            frm.p_dtResultDetail = m_dtResultDetail;

            if (grdTestInfo.CurrentRow != null)
            {
                frm.p_PatientID = Utility.Int32Dbnull(grdPatients.CurrentRow.Cells["colPatient_ID"].Value, -1);
                frm.v_TestInfo = Utility.Int32Dbnull(grdTestInfo.CurrentRow.Cells["colTest_ID"].Value, -1);
            }
            frm.ShowDialog();
        }

        private void grdResultDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (grdResultDetail.CurrentRow != null && grdResultDetail.Rows.Count > 0)
            {
                if (Utility.GetValueFromGridColumn(grdResultDetail, "colIsNew", grdResultDetail.CurrentRow.Index) == "0")
                {
                    int record = new Update(TResultDetail.Schema)
                        .Set(TResultDetail.Columns.TestResult).EqualTo(CreateResultDetail().TestResult)
                        .Set(TResultDetail.Columns.NormalLevelW).EqualTo(CreateResultDetail().NormalLevelW)
                        .Set(TResultDetail.Columns.NormalLevel).EqualTo(CreateResultDetail().NormalLevel)
                        .Set(TResultDetail.Columns.MeasureUnit).EqualTo(CreateResultDetail().MeasureUnit)
                        .Set(TResultDetail.Columns.ParaName).EqualTo(CreateResultDetail().ParaName)
                        .Where(TResultDetail.Columns.TestDetailId).IsEqualTo(CreateResultDetail().TestDetailId).Execute();
                }
            }
        }

        private void grdResultDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DeleteResultDetail();
                ModifyCommand();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in m_dtResultDetail.Rows)
                {
                    if (dr["CHON"].ToString() == "1")
                        dr["Test_Result"] = cboATDT.Text;
                }
                m_dtResultDetail.AcceptChanges();
            }
            catch (Exception)
            {
            }
        }

        private void chkchontat_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView drv in m_dtTestInfo.DefaultView)
                {
                    drv["CHON"] = chkchontat.Checked ? 1 : 0;
                }
                m_dtTestInfo.AcceptChanges();
            }
            catch (Exception exception)
            {
            }
        }

        private void chkDaochon_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView drv in m_dtTestInfo.DefaultView)
                {
                    drv["CHON"] = drv["CHON"].ToString() == "0" ? 1 : 0;
                }
                m_dtTestInfo.AcceptChanges();
            }
            catch (Exception exception)
            {
            }
        }

        private void chkAllPatient_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow dr in _tblPatients.Rows)
                {
                    dr["CHON"] = chkAllPatient.Checked ? 1 : 0;
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void chkDaochonPatient_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow drv in _tblPatients.Rows)
                {
                    drv["CHON"] = drv["CHON"].ToString() == "0" ? 1 : 0;
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }


        private void grdResultDetail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteResultDetail();
            ModifyCommand();
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            em_Action = action.doNothing;
            DataTable dttemp = m_dtResultDetail.Copy();
            int PatientId = -1;
            if (checkBox1.Checked)
            {
                //  string strdate = DateTime.Now.ToString("dd/MM/yyyy");
                string testIdString = GetIdString(dtTestTypeList, clbTestType, TTestTypeList.Columns.TestTypeId);
                //dtTestInfo = TestInfoBusiness.GetTestInfor(testIdString, Convert.ToDateTime(strdate));
                // dtTestInfo = TestInfoBusiness.GetTestInfo(testIdString);)
                //Biến để xác định là bệnh nhân đầu tiên
                bool firstPatient = true;
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn update toàn bộ kết quả cho nhiều bệnh nhân ko?",
                                           "Thông báo", true))

                    #region "code cu"

                    foreach (DataRow drv in _tblPatients.Rows)
                    {
                        int vPatientId2 = Utility.Int32Dbnull(drv["Patient_ID"], -1);
                        if (drv["CHON"].ToString() == "1")
                        {
                            DataTable mDtTestInfo2, m_dtResultDetail2;
                            DataSet ds2 = TestInfoBusiness.GetTestInfoByPatientInfo(vPatientId2, testIdString,);
                            mDtTestInfo2 = ds2.Tables[0];
                            DataTable temresultDetail = ds2.Tables[1];
                            //m_dtResultDetail2 = ds2.Tables[1];
                            // m_dtResultDetail2 = TestInfoBusiness.Result(vPatientId2);

                            if (temresultDetail.Rows.Count <= 0)
                            {
                                foreach (DataRow drvv in m_dtResultDetail.Rows)
                                {
                                    drvv[TTestInfo.Columns.TestId] = mDtTestInfo2.Rows[0][TTestInfo.Columns.TestId];
                                    drvv[TTestInfo.Columns.PatientId] =
                                        mDtTestInfo2.Rows[0][TTestInfo.Columns.PatientId];
                                    drvv[TTestInfo.Columns.Barcode] = mDtTestInfo2.Rows[0][TTestInfo.Columns.Barcode];
                                    drvv[TResultDetail.Columns.TestDetailId] =
                                        Utility.Int32Dbnull(m_dtResultDetail.Compute("Max(TestDetail_ID)", "1=1")) + 1;
                                }
                            }
                            else
                            {
                                m_dtResultDetail.Rows.Clear();
                                foreach (DataRow drvv in temresultDetail.Rows)
                                {
                                    string testtypeId = "TestType_ID=" + testIdString;
                                    DataRow[] arrdr = dttemp.Select("CHON=1 AND Para_name='" + drvv["para_name"] + "'");
                                    //DataRow[] arrdr = dttemp.Select(testtypeId);
                                    if (arrdr.Length > 0)
                                        drvv["Test_result"] = arrdr[0]["Test_result"];
                                    m_dtResultDetail.ImportRow(drvv);

                                }
                                m_dtResultDetail.AcceptChanges();
                            }

                            foreach (DataRow dr in mDtTestInfo2.Rows)
                            {
                                v_Test_ID = Utility.Int32Dbnull(dr["Test_ID"], -1);
                                if (v_Test_ID > -1)

                                    actionResult = new TestInfoBusiness().UpdateDataResultDetail(ref m_dtResultDetail,
                                                                                                 v_Test_ID);
                            }
                        }
                    }


                em_Action = action.doNothing;

                SetStatusMessage();

                #endregion
            }
            else
            {

                //if (new TestInfoBusiness().UpdateDeviceTestInfor(ref m_dtTestInfo, v_Test_ID) > 0)
               
                    //em_Action = action.doNothing;
                    actionResult = new TestInfoBusiness().UpdateDataResultDetail(ref m_dtResultDetail,
                                                                                 v_Test_ID);
                    em_Action = action.doNothing;

                    SetStatusMessage();
                
            }
        }

        /// <summary>
        /// hàm thực hiện tìm kiếm thông tin của xét nghiệm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                grdPatients.SelectionChanged -= grdPatients_SelectionChanged;
                circularProgress1.IsRunning = true;
                btnSearch.Enabled = false;
                //_bgProcessing = new BackgroundWorker();
                GetPatientInfo1();
                DisplaySearchInfo1();
                //_bgProcessing.DoWork += GetPatientInfo;
                //_bgProcessing.RunWorkerCompleted += DisplaySearchInfo;
                //_bgProcessing.RunWorkerAsync();
                //grdPatients_SelectionChanged(grdPatients, new EventArgs());
                ModifyCommand();
                grdPatients.Focus();
                
            }
            catch
            {
            }
            finally
            {
                ChangeColor();
                grdPatients.SelectionChanged += grdPatients_SelectionChanged;
            }
        }

    
        /// <summary>
        /// hàm thực hiện thay đổi khi di chuyển trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdPatients_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdPatients.CurrentRow != null)
                {
                    //Lấy tất cả Test Result của BN đang được chọn
                    //selectedPatientId = grdPatients.CurrentRow.Cells[LPatientInfo.Columns.PatientId].Value.ToString();
                    // _testAllResult = TestInfoBusiness.GetIndividualTestResult(Utility.Int32Dbnull(selectedPatientId,0), "-1");
                    v_PatientId = Utility.Int32Dbnull(grdPatients.CurrentRow.Cells["colPatient_ID"].Value, -1);
                    //   _patientTestTypeRegistered = TestInfoBusiness.GetPatientTestTypeList(selectedPatientId);
                    string testIdString = GetIdString(dtTestTypeList, clbTestType, TTestTypeList.Columns.TestTypeId);
                    barcode.Visible = !
                                      string.IsNullOrEmpty(
                                          Utility.sDbnull(grdPatients.CurrentRow.Cells["colBarcode"].Value, ""));
                    barcode.Data = Utility.sDbnull(grdPatients.CurrentRow.Cells["colBarcode"].Value, "");
                    BindTestInfo(v_PatientId, testIdString);


                    foreach (DataGridViewRow dvr in grdTestInfo.Rows)
                    {
                        int printstatus = Utility.Int32Dbnull(dvr.Cells["Printstatus"].Value);
                        int testStatus = Utility.Int32Dbnull(dvr.Cells["TestStatus"].Value);

                        if (printstatus == 1 && testStatus == 90)
                        {
                            dvr.DefaultCellStyle.BackColor = Color.DarkCyan;
                        }
                    }
                    ModifyCommand();
                    grdTestInfo_SelectionChanged(grdTestInfo, new EventArgs());
                }
                else
                {
                    m_dtTestInfo.Clear();
                    m_dtResultDetail.Clear();
                    ModifyCommand();
                    grdTestInfo_SelectionChanged(grdTestInfo, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        /// <summary>
        /// hàm thực hiện thông tin thay đổi 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (clbTestType.Items.Count > 0)
                {

                    for (int i = 0; i < clbTestType.Items.Count; i++)
                    {
                        clbTestType.SetItemChecked(i, true);
                    }
                }
                if (clbStatus.Items.Count > 0)
                {               
                    for (int i = 0; i < clbStatus.Items.Count; i++)
                    {
                        clbStatus.SetItemChecked(i, true);
                    }
                    
                }
 

                if (clbTestType.Focused && clbTestType.Items.Count > 0)
                {
                    for (int i = 0; i < clbTestType.Items.Count; i++)
                    {
                        clbTestType.SetItemChecked(i, true);
                    }
                }
                else if (clbStatus.Focused && clbStatus.Items.Count > 0)
                {
                    for (int i = 0; i < clbStatus.Items.Count; i++)
                    {
                        clbStatus.SetItemChecked(i, true);
                    }
                }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message);
            }
        }

        private void tsmiUnselectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (clbStatus.Items.Count > 0)
                {
                    for (int i = 0; i < clbStatus.Items.Count; i++)
                    {
                        clbStatus.SetItemChecked(i, false);
                    }

                }
               if (clbTestType.Items.Count > 0)
                {
                    for (int i = 0; i < clbTestType.Items.Count; i++)
                    {
                        clbTestType.SetItemChecked(i, false);
                    }
                   }
                if (clbTestType.Focused && clbTestType.Items.Count > 0)
                {
                    for (int i = 0; i < clbTestType.Items.Count; i++)
                    {
                        clbTestType.SetItemChecked(i, false);
                    }
                }
                else if (clbStatus.Focused && clbStatus.Items.Count > 0)
                {
                    for (int i = 0; i < clbStatus.Items.Count; i++)
                    {
                        clbStatus.SetItemChecked(i, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmiInvertSelection_Click(object sender, EventArgs e)
        {
            try
            {
                if (clbTestType.Items.Count > 0)
                {
                    for (int i = 0; i < clbTestType.Items.Count; i++)
                    {
                        clbTestType.SetItemChecked(i, (clbTestType.GetItemChecked(i) ? false : true));
                    }
                }
                if (clbStatus.Items.Count > 0)
                {
                    for (int i = 0; i < clbStatus.Items.Count; i++)
                    {
                        clbStatus.SetItemChecked(i, (clbStatus.GetItemChecked(i) ? false : true));
                    }
                }
             if (clbTestType.Focused && clbTestType.Items.Count > 0)
                {
                    for (int i = 0; i < clbTestType.Items.Count; i++)
                    {
                        clbTestType.SetItemChecked(i, (clbTestType.GetItemChecked(i) ? false : true));
                    }
                }
                else if (clbStatus.Focused && clbStatus.Items.Count > 0)
                {
                    for (int i = 0; i < clbStatus.Items.Count; i++)
                    {
                        clbStatus.SetItemChecked(i, (clbStatus.GetItemChecked(i) ? false : true));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region "HÀM THỰC HIỆN EVENT DÙNG CHUNG"

        private void InitializeEventControl()
        {
            //chkchontat.CheckedChanged += chkchontat_CheckedChanged;
            //chkDaochon.CheckedChanged += chkdaochon_CheckedChanged;
            chkResulted.CheckedChanged += chkResulted_CheckedChanged;
            chkchontat.CheckedChanged += chkchontat_CheckedChanged_1;
            chkDaochon.CheckedChanged += chkDaochon_CheckedChanged_1;
         
        }

        /// <summary>
        /// hàm thực hiện xêm trạng thái của các nút
        /// </summary>
        private void ModifyCommand()
        {
            btnPrintQuick.Enabled = grdTestInfo.RowCount > 0;
            btnPrintPreview.Enabled = grdTestInfo.RowCount > 0;
            chkAllPatient.Enabled = grdPatients.RowCount > 0;
            chkDaochonPatient.Enabled = grdPatients.RowCount > 0;
            chkchontat.Enabled = grdTestInfo.RowCount > 0;
            //    checkBox1.Enabled = grdResultDetail.RowCount > 0;
            chkDaochon.Enabled = grdTestInfo.RowCount > 0;
            chkResulted.Enabled = grdTestInfo.RowCount > 0;
            grdTestInfo.Enabled = grdPatients.RowCount > 0;
            grdResultDetail.Enabled = grdPatients.RowCount > 0;
            cmdAccept.Enabled = grdResultDetail.Rows.Count > 0;

            if (grdTestInfo.CurrentRow != null)
                cmdChangeResult.Enabled = grdPatients.RowCount > 0 &&
                                          Utility.Int32Dbnull(grdTestInfo.CurrentRow.Cells["colTestStatus_ID"].Value, 0) <
                                          60;
        }

        #endregion
    }
}