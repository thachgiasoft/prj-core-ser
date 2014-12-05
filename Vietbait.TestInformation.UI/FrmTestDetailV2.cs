using System;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Report.UI.Reports;
using Vietbait.Lablink.Utilities;
using Vietbait.TestInformation.Business;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmTestDetailV2 : Office2007Form
    {
        #region WarningBox Status Class

        private DataTable dtPatientList;
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
        private DataTable dtTestTypeList;

        // BackgroundWorker để nạp dữ liệu trên một luồng khác 
        private BackgroundWorker _bgProcessing;
        private DataTable _dtTestStatus;
        private DataTable _patientTestTypeRegistered;
        private string _reportType = LablinkBusinessConfig.GetReportType();
        private DataTable _tblPatients;
        private DataTable _testAllResult;
        private string selectedPatientId;
        
        #endregion

        #region Contructor

        public FrmTestDetailV2()
        {
             InitializeComponent();

            //Add Handler for All checkbox
            //cboCoKetQua.CheckedChanged += CboTatcaCheckedChanged;
            //cboChuaCoKetQua.CheckedChanged += CboTatcaCheckedChanged;
            grdPatients.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            grdPatients.AutoGenerateColumns = false;
            grdResultByTestType.AutoGenerateColumns = false;
            
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
            comboBoxEx1.SelectedValueChanged -= cboStyle_SelectedValueChanged;
            comboBoxEx1.Items.Clear();
            foreach (eStyle style in Enum.GetValues(typeof (eStyle)))
            {
                comboBoxEx1.Items.Add(style);
            }
            comboBoxEx1.SelectedIndex = 0;
            comboBoxEx1.SelectedValueChanged += cboStyle_SelectedValueChanged;
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

               

                DataBinding.BindData(clbTestType, dtTestTypeList, TTestTypeList.Columns.TestTypeId,
                                     TTestTypeList.Columns.TestTypeName);
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

                    grdPatients.DataSource = _tblPatients;
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


                //grdAllTodayTestResult.DataSource = dt;
                //grdAllTodayTestResult.AutoGenerateColumns = true;
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        private void FillSuperTabItem()
        {
            foreach (DataRow dr in dtTestTypeList.Rows)
            {
                var tab = new SuperTabItem();
                tab.Tag = dr[TTestTypeList.Columns.TestTypeId].ToString();
                tab.Text = ShortNameTestType(dr[TTestTypeList.Columns.TestTypeName].ToString());
                tab.Visible = true;

                sttResult.Tabs.Add(tab);
            }
        }

        private void PrintAction(bool Quick)
        {
            try
            {
                if (grdPatients.CurrentRow != null)
                {
                    var frm = new FrmTestTypeSelection();
                    frm.dtTestTypeCLB = _patientTestTypeRegistered;
                    frm.ShowInTaskbar = false;
                    frm.ShowDialog();

                    if (!frm.vCancel)
                    {
                        int patientId = Convert.ToInt32(grdPatients.CurrentRow.Cells["Patient_ID"].Value);
                        DataTable result = TestInfoBusiness.GetIndividualTestResult(patientId, frm.strSelectedTestType);
                        if (result.Rows.Count > 0)
                        {
                            DataRow PatientInfo = TestInfoBusiness.GetPatientInfo(patientId);
                            var crpt = new crpt_IndividualTestResult();

                            crpt.SetDataSource(result);
                           // crpt.SetDataSource(_testAllResult);
                            crpt.SetParameterValue("ShowSubReport", 1);

                            crpt.SetParameterValue("ShowMainReport", 0);
                            crpt.SetParameterValue("ParentBranchName", LablinkBusinessConfig.GetParentBranchName());
                            crpt.SetParameterValue("BranchName", LablinkBusinessConfig.GetBranchName());
                            // string sss = LablinkBusinessConfig.GetPhone();
                            crpt.SetParameterValue("Address", LablinkBusinessConfig.GetAddress());
                            crpt.SetParameterValue("sPhone", LablinkBusinessConfig.GetPhone());

                            crpt.SetParameterValue("Patient_Name",
                                                   PatientInfo[LPatientInfo.Columns.PatientName].ToString());
                            crpt.SetParameterValue("Patient_Age", PatientInfo[LPatientInfo.Columns.Age].ToString());
                            crpt.SetParameterValue("Patient_Diagnose",
                                                   PatientInfo[LPatientInfo.Columns.Diagnostic].ToString());
                            crpt.SetParameterValue("Patient_Address",
                                                   PatientInfo[LPatientInfo.Columns.Address].ToString());
                            crpt.SetParameterValue("Patient_Department",
                                                   PatientInfo[LDepartment.Columns.SName].ToString());
                            crpt.SetParameterValue("Patient_Sex", PatientInfo["Sex_Name"].ToString());
                            crpt.SetParameterValue("Patient_Insurance",
                                                   PatientInfo[LPatientInfo.Columns.InsuranceNum].ToString());
                            crpt.SetParameterValue("Patient_Room", PatientInfo[LPatientInfo.Columns.Room].ToString());
                            crpt.SetParameterValue("Patient_Bed", PatientInfo[LPatientInfo.Columns.Bed].ToString());


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
                }
                else SetWarningStatus(new WarningBoxStatus("Không có bệnh nhân", true));
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        #endregion

        #region Form Events

        private void FrmTestDetail_Load(object sender, EventArgs e)
        {
            try
            {
                circularProgress1.IsRunning = true;

                try
                {
                    FillGrpDatePicker();

                    // Khởi tạo Sex Combobox
                    FillSexCombobox();

                    // Load test type list to checklistbox
                    FillTestTypeList();

                    //Load Status List
                    FillStatusListBox();

                    //Get All Style from Style Manager :D
                    GetAllStyle();

                    FillSuperTabItem();
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
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.P)
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
        }

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

        private void cboStyle_SelectedValueChanged(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = (eStyle) comboBoxEx1.SelectedItem;
        }

        private void expandablePanel1_ExpandedChanging(object sender, ExpandedChangeEventArgs e)
        {
            if (expandablePanel1.Expanded) expandablePanel1.TitleText = "TÌM KIẾM: " + cboDate.Text;
            else expandablePanel1.TitleText = "TÌM KIẾM";
        }
   
           private void btnAddPatient_Click(object sender, EventArgs e)
        {
            FrmPatientRegistration frmPatientRegistration = new FrmPatientRegistration();
            frmPatientRegistration.ShowDialog();
            //var obj = new FrmPatientInfo();
            //obj.grdList = grdPatients;
            //obj.gv_dtpatient = _tblPatients;
            //obj.ShowDialog();
        }

        private void btnPrintQuick_Click(object sender, EventArgs e)
        {
            PrintAction(true);
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            PrintAction(false);
        }

        private void btnParaEntry_Click(object sender, EventArgs e)
        {
            if (_testAllResult != null && sttResult.SelectedTabIndex != null)
            {
                var obj = new FrmParaEntry();
                if (sttResult.SelectedTabIndex == 0) obj.GvTestTypeId = "-1";
                else obj.GvTestTypeId = sttResult.SelectedTab.Name;
                obj.GvDtParaEntry = _testAllResult;
                obj.GvPatientId = selectedPatientId;
                obj.ShowDialog();
            }
        }

        #endregion

        #region Controls Events      
        /// <summary>
        /// hàm thực hiện tìm kiếm thông tin của xét nghiệm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            circularProgress1.IsRunning = true;
            btnSearch.Enabled = false;
            _bgProcessing = new BackgroundWorker();
            _bgProcessing.DoWork += GetPatientInfo;
            _bgProcessing.RunWorkerCompleted += DisplaySearchInfo;
            _bgProcessing.RunWorkerAsync();
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
                    selectedPatientId = grdPatients.CurrentRow.Cells[LPatientInfo.Columns.PatientId].Value.ToString();
                    _testAllResult = TestInfoBusiness.GetIndividualTestResult(Utility.Int32Dbnull(selectedPatientId,0), "-1");

                    _patientTestTypeRegistered = TestInfoBusiness.GetPatientTestTypeList(selectedPatientId);

                    //Lấy về mảng các loại xét nghiệm của bệnh nhân
                    var ids = (from row in _patientTestTypeRegistered.AsEnumerable()
                              select row[TTestTypeList.Columns.TestTypeId].ToString()).ToArray();


                    //Duyệt từng Tab để set visible
                    for (int i = 1; i < sttResult.Tabs.Count; i++)
                    {
                        sttResult.Tabs[i].Visible = ids.Contains(sttResult.Tabs[i].Tag);
                    }
                    sttResult_SelectedTabChanged(null, null);
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
        private void sttResult_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            try
            {
                grdResultByTestType.DataSource = null;
                if (_testAllResult.Rows.Count > 0)
                {
                    if (sttResult.SelectedTab == null)
                    {
                        sttResult.SelectedTabIndex = 0;
                    }
                    else
                    {
                        string rowfillter = "1=1";
                        string testTypeId = sttResult.SelectedTab.Tag.ToString();
                        if(Utility.Int32Dbnull(testTypeId) > 0)
                        {
                            rowfillter = TResultDetail.Columns.TestTypeId + "=" + testTypeId;
                        }
                      
                        _testAllResult.DefaultView.RowFilter = rowfillter;
                        _testAllResult.AcceptChanges();
                        grdResultByTestType.DataSource = _testAllResult;
                    }
                }
               
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void tsmiSelectAll_Click(object sender, EventArgs e)
        {
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

        private void tsmiUnselectAll_Click(object sender, EventArgs e)
        {
            try
            {
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

        private void grdAllTestResult_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // var grd = sender as DataGridViewX;
                //MessageBox.Show(grd.Rows .Count.ToString());
                //MessageBox.Show(e.ColumnIndex.ToString() + e.RowIndex.ToString());
                if (e.ColumnIndex == 1)
                {
                    DataRow dr = (grdResultByTestType.DataSource as DataTable).Rows[e.RowIndex];

                    //Nếu đã có Detail_ID thì Update
                    int testDetailId = Convert.ToInt32(dr[TResultDetail.Columns.TestDetailId]);
                    string barcode = dr[TResultDetail.Columns.Barcode].ToString().Trim();
                    string result = dr[TResultDetail.Columns.TestResult].ToString().Trim();
                    if (testDetailId > 0)
                    {
                        if (result.Trim() != "")
                        {
                            DeviceHelper.SpUpdateTestResult(testDetailId, barcode, result);
                        }
                      
                    }

                        //Nếu chưa có Detail_ID thì Insert
                    else
                    {
                        //Lấy về Test_ID
                        long testId = Convert.ToInt32(dr[TResultDetail.Columns.TestId]);
                        long patientId = Convert.ToInt32(dr[TResultDetail.Columns.PatientId]);
                        short testTypeId = Convert.ToInt16(dr[TResultDetail.Columns.TestTypeId]);
                        string testSequence = dr[TResultDetail.Columns.TestSequence].ToString();
                        string testDate = (Convert.ToDateTime(dr[TResultDetail.Columns.TestDate])).ToString("dd/MM/yyyy");
                        short dataSequence = Convert.ToInt16(dr[TResultDetail.Columns.DataSequence]);
                        string paraName = dr[TResultDetail.Columns.ParaName].ToString();
                        string measureUnit = dr[TResultDetail.Columns.MeasureUnit].ToString();
                        string normalLevel = dr[TResultDetail.Columns.NormalLevel].ToString();
                        string normalLevelW = dr[TResultDetail.Columns.NormalLevelW].ToString();
                        DeviceHelper.SpInsertResult(testId, patientId, testTypeId,
                                                    testSequence, testDate, dataSequence,
                                                    barcode, paraName, result, measureUnit,
                                                    normalLevel, normalLevelW);
                        testDetailId = TestInfoBusiness.GetTestDetailId(barcode, paraName);
                        dr[TResultDetail.Columns.TestDetailId] = testDetailId;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnAutoUpdate_Click(object sender, EventArgs e)
        {
            var obj = new FrmAutoUpdate();
            obj.ShowDialog();
         
        }
    }
}