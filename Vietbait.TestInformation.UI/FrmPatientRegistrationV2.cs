using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.List.Business;
using Vietbait.Lablink.Model;
using Vietbait.TestInformation.Business;
using Vietbait.TestInformation.UI.System_Para;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmPatientRegistrationV2 : Office2007Form 
    {
        #region Attributes

    
        private readonly List<string> _lstBarcode = new List<string>();
        private readonly DataTable dtTestStatus = TestInfoBusiness.GetTestStatus();
        private readonly Margins margins = new Margins(25, 25, 5, 5);
        private string[] BarcodeArray;
        private byte Col = 2;
        private string _barcode;
        private string[] _barcodeConfig = new string[5];
        private string _rowFilter = "1=1";
        private string _rowFilter1 = "1=1";
        private string b_UI;
        public bool b_actionStatus;
        private string barcode = "";
        private DataRow drPatientInfo;
        private DataSet ds = new DataSet();
        private DataTable dtDevice;
        private DataTable dtPatientList;
        public DataTable dtRegTest;
        private DataTable dtRegisteredTestType;
        private DataTable dtTestTypeList;
        public action em_Action = action.doNothing;
        private string pid;
        private string testTypeId;
        public string testId;
        public int v_Patient_Id = -1;
      
        #endregion

        #region Private Methods

        public FrmPatientRegistrationV2()
        {
            InitializeComponent();
            InitializeEventControl();
            grdPatientList.AutoGenerateColumns = false;
            grdTestType.AutoGenerateColumns = false;
            grdPatientList.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            grdTestType.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            grdTRegList.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
            
        }

        //private void GenerateNewSid()
        //{
        //    string newSid = TestInfoBusiness.GetNewBarcode();
        //    txtSid.Text = newSid;
        //}

        private void InitializeEventControl()
        {
            KeyPreview = true;
            Load += FrmPatientRegistration_Load;
            KeyDown += FrmPatientRegistration_KeyDown;
            grdPatientList.SelectionChanged += grdPatientList_SelectionChanged;
            grdPatientList.DoubleClick += grdPatientList_DoubleClick;
            grdTestType.SelectionChanged += grdTestType_SelectionChanged_1;
            cmdUpdate.Click += cmdUpdate_Click;
            cmdExit.Click += btnExit_Click;
            btnSaveContinue.Click += btnSaveContinue_Click;
            cmdSearch.Click += cmdSearch_Click;
            txtFilter.TextChanged += txtFilter_TextChanged;
            grdTestType.SelectionChanged += grdTestType_SelectionChanged;
            grdTestType.CellContentClick += grdTestType_CellContentClick;
            barcode1.Click += barcode1_Click;
            cboDate.SelectedIndexChanged += cboDate_SelectedIndexChanged;
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

        private void UpdatePatientInfo()
        {
            if (grdPatientList.CurrentRow != null)
            {
                var frmPatientInfo = new FrmPatientInfo("LIST");
                frmPatientInfo.grdList = grdPatientList;
                frmPatientInfo.gv_dtpatient = dtPatientList;
                frmPatientInfo.drPatientInfo = Utility.FetchOnebyCondition(dtPatientList,
                                                                           "Patient_ID=" +
                                                                           Utility.Int32Dbnull(
                                                                               Utility.GetValueFromGridColumn(
                                                                                   grdPatientList, "colPatient_ID",
                                                                                   grdPatientList.CurrentRow.Index), -1));
                frmPatientInfo.em_Action = action.Update;
                frmPatientInfo.ShowDialog();
                //  grdPatientList_SelectionChanged(grdPatientList, new EventArgs());
            }
        }

        private bool Invalidata()
        {
            string BarCode = globalVariables.SysDate.Year + globalVariables.SysDate.Month.ToString() +
                             globalVariables.SysDate.Date + txtBarcode.Text;
            Utility.ResetMessageError(errorProvider1);
            if (!string.IsNullOrEmpty(txtBarcode.Text))
            {
                if (Utility.Int32Dbnull(txtBarcode.Text, 0) < SystemParaBarcode.ParaBacode)
                {
                    Utility.SetMessageError(errorProvider1, txtBarcode,
                                            "Số thứ tự của barcode không nhỏ hơn " +
                                            SystemParaBarcode.ParaBacode);
                    txtBarcode.Focus();
                    return false;
                }
                TTestInfoCollection testInfoCollection =
                    new TTestInfoController().FetchByQuery(TTestInfo.CreateQuery().AddWhere(TTestInfo.Columns.Barcode,
                                                                                            Comparison.Equals,
                                                                                            txtBarcode.Text.Trim()));
                if (testInfoCollection.Count > 0)
                {
                    Utility.SetMessageError(errorProvider1, txtBarcode,
                                            "Tồn tại  " + SystemParaBarcode.ParaBacode);
                    txtBarcode.Focus();
                    return false;
                }
            }
            return true;
        }

        private void LoadTestTypeButton()
        {
            dtTestTypeList = TestTypeListBusiness.GetTestTypeNameAndIdList();
            foreach (DataRow dr in dtTestTypeList.Rows)
            {
                var btx = new ButtonItem();
                btx.Text = dr[TTestTypeList.Columns.TestTypeName].ToString();
                btx.Name = dr[TTestTypeList.Columns.TestTypeId].ToString();
                btx.Click += AddTestType;
                stsTestTypeList.Tabs.Add(btx);
            }
        }

        private void AddTestType(object sender, EventArgs e)
        {
            if (!Invalidata()) return;
            if (grdPatientList.CurrentRow != null)
            {
                int rowIndex = grdPatientList.CurrentRow.Index;

                var btni = (ButtonItem) sender;
                var pItem = new TTestInfo();
                pItem.TestTypeId = Convert.ToDecimal(btni.Name);
                int test = 0;
                if (string.IsNullOrEmpty(txtBarcode.Text))
                    pItem.Barcode =
                        PatientBusiness.GetBarCodeTestInfo(
                            Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                           grdPatientList.CurrentRow.Index)),
                            SystemParaBarcode.ParaBacode, 0);
                else
                {
                    pItem.Barcode =
                        PatientBusiness.GetBarCodeTestInfo(
                            Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                           grdPatientList.CurrentRow.Index)),
                            Utility.Int32Dbnull(txtBarcode.Text, 0), 1);
                }
                barcode = PatientBusiness.GetBarCodeTestInfo(
                    Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                   grdPatientList.CurrentRow.Index)),
                    SystemParaBarcode.ParaBacode, 0);

                if (!_lstBarcode.Contains(barcode))
                    _lstBarcode.Add(barcode);
                barcode1.Data = barcode;
                barcode2.Data = barcode;
                //barcode2.Data = barcode;
                //pItem.Barcode = txtSid == null ? string.Empty : txtSid.Text;
                pItem.PatientId = Convert.ToInt32(dtPatientList.Rows[rowIndex][LPatientInfo.Columns.PatientId]);
                //pItem.AssignId = Convert.ToInt32(cboAssignDoctor.SelectedValue);
                //pItem.DiagnosticianId = Convert.ToInt32(cboDiagnoseDoctor.SelectedValue);
                pItem.AssignId = Convert.ToInt32(-1);
                pItem.DiagnosticianId = Convert.ToInt32(-1);
                pItem.ParaId = string.IsNullOrEmpty(txtBarcode.Text) ? 0 : 1;
                string testID = TestInfoBusiness.InsertTestInfo(pItem);

                DataRow dr = dtRegisteredTestType.NewRow();
                dr[TTestInfo.Columns.TestId] = testID;
                dr[TTestInfo.Columns.Barcode] = pItem.Barcode;
                dr[TTestInfo.Columns.TestTypeId] = btni.Name;
                dr[TTestTypeList.Columns.TestTypeName] = btni.Text;
                dr[TTestInfo.Columns.TestStatus] = 0;
                dr[TTestInfo.Columns.PatientId] = pItem.PatientId;
                dr[TTestInfo.Columns.AssignId] = pItem.AssignId;

                dr[TTestInfo.Columns.DiagnosticianId] = pItem.DiagnosticianId;
                dr[LTestStatus.Columns.TestStatusName] =
                    dtTestStatus.Rows[0][LTestStatus.Columns.TestStatusName].ToString();
                //dr[TTestTypeList.Columns.TestTypeName] = btni.Text;
                dtRegisteredTestType.Rows.Add(dr);
                dtRegisteredTestType.AcceptChanges();
                grdTestType.Focus();
                //grdTestType.DataSource = dtRegisteredTestType;
            }
        }

        private void SearchPatient()
        {
            ds = PatientBusiness.GetPatientListByDate(dtpFromDate.Value,dtpTodate.Value);
            dtPatientList = ds.Tables[0];
            dtRegisteredTestType = ds.Tables[1];
            txtFilter.Clear();

            Utility.SetDataSourceForDataGridView(grdPatientList, dtPatientList, false, true, "", "");
            Utility.SetDataSourceForDataGridView(grdTestType, dtRegisteredTestType, false, true, _rowFilter, "");
            grdPatientList_SelectionChanged(grdPatientList, new EventArgs());
            txtFilter.Visible = grdPatientList.Rows.Count > 0;
            if (grdPatientList.Rows.Count <= 0)
            {
                barcode1.Data = "0000000000";
            }

            Utility.CreateMessage(lblMessage, "Không có bản ghi nào tìm thấy", dtPatientList.Rows.Count <= 0);
            if (grdPatientList.Rows.Count > 0) grdPatientList.Focus();
        }

        private void ModifyCommand()
        {
            cmdUpdate.Enabled = grdPatientList.Rows.Count > 0;
            grpListService.Enabled = grdPatientList.Rows.Count > 0;
        }

        private void DeletePatientTestType()
        {
            DataGridViewRow currentrow = grdTestType.CurrentRow;
            //if (grdTestType.CurrentRow != null)
            if (currentrow != null)
            {
                int rowIndex = grdTestType.CurrentRow.Index;
                //  int row = Convert.ToInt32(grdTestType.CurrentRow.Cells["colBarcode"].ToString());

                testId = Lablink.Utilities.UI.GetCellValue(currentrow.Cells["colTest_ID"]);
                pid = Lablink.Utilities.UI.GetCellValue(currentrow.Cells["colBarcode"]);
                testTypeId = Lablink.Utilities.UI.GetCellValue(currentrow.Cells["TestType_ID"]);
                DataTable dt1 =
                    TestInfoBusiness.GetTestResultByTestId(testId, testTypeId);
                //DataTable dt =
                //    TestInfoBusiness.GetTestResultByTestId(
                //        dtRegisteredTestType.Rows[rowIndex][TTestInfo.Columns.TestId].ToString());
                if (dt1.Rows.Count > 0)
                {
                    Utility.ShowMsg("Xét nghiệm có kết quả. Không thể xóa được !", "Thông báo");
                }
                //if (dtRegTest.Rows.Count > 0)
                //{
                //    Utility.ShowMsg("Loại xét nghiệm đã có đăng ký test?", "Thông báo");
                //}
                else if(dt1.Rows.Count <=0)
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xóa loại xét nghiệm này ko?", "Thông báo", true))
                    {
                        TestInfoBusiness.DeleteTesInfor(Convert.ToInt32(testId));
                        grdTestType.Rows.Remove(currentrow);
                    }

                    //TestInfoBusiness.DeleteTestInfo(
                    //    dtRegisteredTestType.Rows[rowIndex][TTestInfo.Columns.TestId].ToString());
                    // dtRegisteredTestType.Rows.RemoveAt(rowIndex);
                }
                dtRegisteredTestType.AcceptChanges();
                //Khi BN ko còn đăng ký XN nào thì đưa SID về Empty
                if (dtRegisteredTestType.Rows.Count == 0)
                {
                    dtPatientList.Rows[grdPatientList.CurrentRow.Index]["SID"] = string.Empty;
                    dtPatientList.AcceptChanges();
                }
            }
        }

        private void DeletePatient()
        {
            DataGridViewRow currentRow = grdPatientList.CurrentRow;
            DataGridViewRow currentTestInfor = grdTestType.CurrentRow;
            if (currentRow != null)
            {
                int rowIndex = grdPatientList.CurrentRow.Index;
                string patientId = Lablink.Utilities.UI.GetCellValue(currentRow.Cells["colPatient_ID"]);
                DataTable dtTestInfor = TestInfoBusiness.GetTestInfoToPatient(patientId);
                if (dtTestInfor.Rows.Count > 0)
                    MessageBox.Show("Bệnh nhận đã đăng ký xét nghiệm. Không thể xóa được !", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    PatientBusiness.DeletePatient(patientId);
                    //PatientBusiness.DeletePatient(
                    //    dtPatientList.Rows[rowIndex][LPatientInfo.Columns.PatientId].ToString());
                    dtPatientList.Rows.RemoveAt(rowIndex);
                    dtPatientList.AcceptChanges();
                }
                if (grdPatientList.Rows.Count <= 0)
                {
                    barcode1.Data = "0000000000";
                }
            }
        }

        private DataTable GetRegListByTestID(int input)
        {
            dtDevice = new DataTable();
            return dtDevice = TestInfoBusiness.GetTRegListByTestID(input);
        }

        private void DeletRegTest()
        {
            DataGridViewRow currentRow = grdTRegList.CurrentRow;
            if (currentRow != null)
            {
                //int Test_id;
                //    //Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdTRegList, TRegList.Columns.TestDetailId,
                //    //                                                   grdTRegList.CurrentRow.Index));
                ////string pid = Vietbait.Lablink.Utilities.UI.GetCellValue(currentRow.Cells[TRegList.Columns.TestId]);
                //if (Test_id != null)
                //{
                //    if (Utility.AcceptQuestion("Bạn có muốn xóa xét nghiệm này ko?", "Thông báo", true))
                //    {
                //        TestInfoBusiness.DeleteRegList(Convert.ToInt32(Test_id));
                //        grdTRegList.Rows.Remove(currentRow);
                //    }
                //}
            }
            dtRegTest.AcceptChanges();
        }

        //private void TryToSetStyle(string style)
        //{
        //    //var myStyle = (eStyle) style;
        //    var myStyle = (eStyle)Enum .Parse( typeof(eStyle ),style);
        //    var newStyleManager = new StyleManager(components) {ManagerStyle = myStyle};
        //}

        #endregion

        #region Form Events

        private void FrmPatientRegistration_Load(object sender, EventArgs e)
        {
            b_UI = "U";
            //grdPatientList.MouseUp += GridRightClick;
            //dtpFromDate.Text = DateTime.Now.ToString();
            FillGrpDatePicker();
            //LoadDataComboBox(cboAssignDoctor, DoctorBusiness.GetAllDoctor(), LUser.Columns.UserName,
            //                 LUser.Columns.UserId);
            //LoadDataComboBox(cboDiagnoseDoctor, DoctorBusiness.GetAllDoctor(), LUser.Columns.UserName,
            //                 LUser.Columns.UserId);

            TypeConverter tc = TypeDescriptor.GetConverter(Font);
            string paramBarcodeConfig = "BarcodeConfig.txt";
            if (File.Exists("BarcodeConfig.txt"))
            {
                _barcodeConfig = File.ReadAllLines(paramBarcodeConfig);
            }
            else
            {
                // ERROR: Not supported in C#: ReDimStatement

                _barcodeConfig[0] = "130";
                //Area Width
                _barcodeConfig[1] = "75";
                //Area Height
                _barcodeConfig[2] = "25";
                //Position Left
                _barcodeConfig[3] = "5";
                //Position top
                _barcodeConfig[4] = "45";
                //center blank
                //File.AppendAllText(paramBarcodeConfig, _barcodeConfig(0) + Constants.vbCrLf + _barcodeConfig(1) + Constants.vbCrLf + _barcodeConfig(2) + Constants.vbCrLf + _barcodeConfig(3) + Constants.vbCrLf + _barcodeConfig(4));
            }

            LoadTestTypeButton();
            SearchPatient();

            // ModifyCommand();
            if (em_Action == action.ConfirmData)
                btnSaveContinue.PerformClick();
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

        private void FrmPatientRegistration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Delete)
            {
                if (grdPatientList.Focused) DeletePatient();
                else if (grdTestType.Focused) DeletePatientTestType();
            }
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Alt && e.KeyCode == Keys.L) txtFilter.Focus();
            if (e.Control && e.KeyCode == Keys.N) btnSaveContinue.PerformClick();
            if (e.Control && e.KeyCode == Keys.U) cmdUpdate.PerformClick();
            //if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N) btnSaveContinue.PerformClick();
            //else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.U) btnUpdate.PerformClick();
        }

        private void btnSaveContinue_Click(object sender, EventArgs e)
        {
            var frmPatientInfo = new FrmPatientInfo("LIST");
            frmPatientInfo.grdList = grdPatientList;
            frmPatientInfo.gv_dtpatient = dtPatientList;
            frmPatientInfo.em_Action = action.Insert;
            frmPatientInfo.ShowDialog();
            if (em_Action == action.ConfirmData)
            {
                v_Patient_Id = frmPatientInfo.v_PatientID;
            }
            grdPatientList_SelectionChanged(grdPatientList, new EventArgs());
            txtBarcode.Clear();
            stsTestTypeList.Focus();
            // ModifyCommand();
        }

        private void grdPatientList_SelectionChanged(object sender, EventArgs e)
        {
            _rowFilter = "1=2";
            txtBarcode.Clear();
            if (grdPatientList.CurrentRow != null)
            {
                stsTestTypeList.Enabled = true;
                _rowFilter = "Patient_ID=" +
                             Utility.Int32Dbnull(
                                 Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                grdPatientList.CurrentRow.Index), -1);
                if (grdTestType.CurrentRow != null)
                {
                    if (!string.IsNullOrEmpty(Utility.sDbnull(grdTestType.CurrentRow.Cells["colBarcode"].Value, "")))
                    {
                        barcode = PatientBusiness.GetBarCodeTestInfo(
                            Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                           grdPatientList.CurrentRow.Index)),
                            SystemParaBarcode.ParaBacode, 0);
                        barcode1.Data = barcode;
                    }
                    else
                    {
                        barcode1.Data = "0000000000";
                    }
                }
            }
            dtRegisteredTestType.DefaultView.RowFilter = _rowFilter;
            dtRegisteredTestType.AcceptChanges();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            b_actionStatus = true;
            Close();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchPatient();
            //    ModifyCommand();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _rowFilter = "1=1";
            if (!string.IsNullOrEmpty(txtFilter.Text))
            {
                _rowFilter = "Patient_Name like '%" + txtFilter.Text.Trim() + "%'";
            }
            dtPatientList.DefaultView.RowFilter = _rowFilter;
            dtPatientList.AcceptChanges();
            ModifyCommand();
        }

        private void grdPatientList_DoubleClick(object sender, EventArgs e)
        {
            UpdatePatientInfo();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            UpdatePatientInfo();
        }

       private void grdTestType_SelectionChanged(object sender, EventArgs e)
        {
            txtBarcode.Enabled = grdTestType.RowCount <= 0;
        }

        private void grdTestType_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            grdTestType_SelectionChanged(grdTestType, new EventArgs());
            if (grdTestType.CurrentRow != null)
            {
                //cboAssignDoctor.SelectedIndex = Utility.GetSelectedIndex(cboAssignDoctor,
                //                                                         Utility.sDbnull(
                //                                                             Utility.GetValueFromGridColumn(
                //                                                                 grdTestType, "colAssign_ID",
                //                                                                 grdTestType.CurrentRow.Index), "-1"));
                //cboDiagnoseDoctor.SelectedIndex = Utility.GetSelectedIndex(cboAssignDoctor,
                //                                                           Utility.sDbnull(
                //                                                               Utility.GetValueFromGridColumn(
                //                                                                   grdTestType, "colDiagnostician_ID",
                //                                                                   grdTestType.CurrentRow.Index), "-1"));
            }
        }

        private void stsTestTypeList_ItemClick(object sender, EventArgs e)
        {
        }

        private void txtBarcode_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            if (!Invalidata()) return;
        }

     

        private void grdTestType_CurrentCellChanged(object sender, EventArgs e)
        {
            if (grdTestType.CurrentRow != null)
            {
                //grdTRegList.DataSource = null;
                //int testId = Utility.Int32Dbnull(grdTestType.CurrentRow.Cells["colTest_ID"].Value, -1);
                //DataTable reciveData = GetRegListByTestID(testId);
                //grdTRegList.DataSource = reciveData;
            }
        }

        private void btnRegtest_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    int Test_ID = 0;
                    if (grdTestType.CurrentRow != null)
                    {
                        Test_ID =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdTestType, "colTest_ID", grdTestType.CurrentRow.Index),
                                -1);
                    }
                    dtRegTest = TestInfoBusiness.GetTestReg(Test_ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (grdTestType.CurrentRow != null)
                {
                    var frm = new frm_CreateRegList();

                    frm.v_TestType_Id =
                        Utility.Int32Dbnull(
                            Utility.GetValueFromGridColumn(grdTestType, "TestType_ID", grdTestType.CurrentRow.Index), -1);
                    frm.g_dtResultDetail = dtRegTest;
                    frm.em_Action = action.doNothing;
                    //  frm.v_Barcode = Utility.GetValueFromGridColumn(grdTestType, "colBarcode", grdTestType.CurrentRow.Index);
                    frm.v_Test_ID =
                        Utility.Int32Dbnull(
                            Utility.GetValueFromGridColumn(grdTestType, "colTest_ID", grdTestType.CurrentRow.Index), -1);
                    frm.v_Patient_ID =
                        Utility.Int32Dbnull(
                            Utility.GetValueFromGridColumn(grdTestType, "colsPatient_ID", grdTestType.CurrentRow.Index),
                            -1);
                    frm.ShowDialog();
                    if (frm.b_Status)
                    {
                        dtRegTest = frm.g_dtResultDetail;
                        em_Action = frm.em_Action;

                        dtRegTest.AcceptChanges();
                    }
                    foreach (DataRow dr in dtRegTest.Rows)
                    {
                        //foreach (DataRow drv  in dtRegTest.Rows)
                        //{
                        //    drv["CHON"] = "1";
                        ////}
                        //dtRegTest.AcceptChanges();
                        //if (dr["CHON"] == "1")
                        //{
                        int InsertReg =
                            TestInfoBusiness.InsertRegList(Utility.Int32Dbnull(dr[TRegList.Columns.TestId], 0),
                                                           Utility.Int32Dbnull(dr[TRegList.Columns.DeviceId], 0),
                                                           dr[TRegList.Columns.Barcode].ToString(),
                                                           dr[TRegList.Columns.AliasName].ToString(),
                                                           dr[TRegList.Columns.ParaName].ToString(), true);
                        //}
                    }
                    Utility.SetDataSourceForDataGridView(grdTRegList, dtRegTest, false, true, "", "");
                }
                //ModifyCommand();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdTestType_SelectionChanged_1(object sender, EventArgs e)
        {
            if (grdTestType.CurrentRow != null)
            {
                grdTRegList.DataSource = null;
                int testId = Utility.Int32Dbnull(grdTestType.CurrentRow.Cells["colTest_ID"].Value, -1);
                dtRegTest = GetRegListByTestID(testId);
                Utility.SetDataSourceForDataGridView(grdTRegList, dtRegTest, false, true, "", "");
            }
        }

        private void grdTRegList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeletRegTest();
            }
        }


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Int32 printAreaHeight = default(Int32);
                Int32 printAreaWidth = default(Int32);
                Int32 marginLeft = default(Int32);
                Int32 marginTop = default(Int32);
                PageSettings _with1 = printDocument1.DefaultPageSettings;

                printAreaHeight = Convert.ToInt32(_barcodeConfig[1]);
                printAreaWidth = Convert.ToInt32(_barcodeConfig[0]);
                marginLeft = Convert.ToInt32(_barcodeConfig[2]);
                marginTop = Convert.ToInt32(_barcodeConfig[3]);

                if (printDocument1.DefaultPageSettings.Landscape)
                {
                    Int32 intTemp = default(Int32);
                    intTemp = printAreaHeight;
                    printAreaHeight = printAreaWidth;
                    printAreaWidth = intTemp;
                }
                Image stockImage1 = default(Image);
                Image stockImage2 = default(Image);
                int _count = 0;
                _count = _lstBarcode.Count();
                if (grdPatientList.CurrentRow != null)
                {
                    if (_count%2 == 0)
                    {
                        if (_lstBarcode.Count() > 0)
                        {
                            barcode2.Data = _lstBarcode[_count - 1];
                            stockImage1 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                            barcode2.Data = _lstBarcode[_count - 2];
                            stockImage2 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                        }
                        else
                        {
                            barcode2.Data = barcode;
                            stockImage1 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                            barcode2.Data = barcode;
                            stockImage2 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                        }
                    }
                    else
                    {
                        // barcode2.Text = lstBarcode[_count - 1].Substring(6, 4);
                        barcode2.Data = _lstBarcode[_count - 1];
                        stockImage1 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                        stockImage2 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                        //stockImage3 = barcode1.Image(printAreaWidth * 10, printAreaHeight * 10);
                    }

                    #region Barcode TO

                    marginLeft += printDocument1.DefaultPageSettings.PaperSize.Width/Col;
                    // stockImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    e.Graphics.DrawImage(stockImage1, 30, marginTop, printAreaWidth, printAreaHeight);
                    // marginLeft = marginLeft + printAreaWidth+300 ;//+ marginMiddle;
                    marginLeft = marginLeft + printAreaWidth; //+ 300;//+ marginMiddle;
                    // e.Graphics.DrawImage(stockImage2, marginLeft-600, marginTop, printAreaWidth, printAreaHeight);
                    e.Graphics.DrawImage(stockImage2, marginLeft - 170, marginTop, printAreaWidth, printAreaHeight);

                    #endregion
                }
                //if (grdPatientList.CurrentRow != null)
                //{
                //    string in_barcode = PatientBusiness.GetBarCodeTestInfo(
                //        Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                //                                                       grdPatientList.CurrentRow.Index)),
                //        SystemParaBarcode.ParaBacode, 0);
                //    barcode1.Data = in_barcode;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //marginLeft += printDocument1.DefaultPageSettings.PaperSize.Width / Col;
            ////Đoạn barcode bên phải
            // Image stockImage = barcode1.Image(printAreaWidth * 10, printAreaHeight * 10);
            //// stockImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            //e.Graphics.DrawImage(stockImage, marginLeft, marginTop, printAreaWidth, printAreaHeight);
            //marginLeft = marginLeft + printAreaWidth + 25;
            //e.Graphics.DrawImage(stockImage, marginLeft - 500, marginTop, printAreaWidth, printAreaHeight);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdPatientList.CurrentRow != null)
                {
                    string in_barcode = PatientBusiness.GetBarCodeTestInfo(
                        Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                       grdPatientList.CurrentRow.Index)),
                        SystemParaBarcode.ParaBacode, 0);
                    if (in_barcode != null)
                    {
                        barcode1.Data = in_barcode;
                        barcode1.Image().Save(Application.StartupPath + "\\Temp.jpg", ImageFormat.Jpeg);
                    }
                    else
                    {
                        barcode1.Data = "0000000000";
                    }
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printDocument1.DefaultPageSettings.Margins = margins;
                        printDocument1.Print();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GridRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (sender != null) (sender as DataGridViewX).Focus();
            }
        }

        private void tsmInBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdPatientList.CurrentRow != null)
                {
                    string in_barcode = PatientBusiness.GetBarCodeTestInfo(
                        Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                       grdPatientList.CurrentRow.Index)),
                        SystemParaBarcode.ParaBacode, 0);
                    if (in_barcode != null)
                    {
                        barcode1.Data = in_barcode;
                        barcode1.Image().Save(Application.StartupPath + "\\Temp.jpg", ImageFormat.Jpeg);
                    }
                    else
                    {
                        barcode1.Data = "0000000000";
                    }
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printDocument1.DefaultPageSettings.Margins = margins;
                        printDocument1.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                txtFilter.Text = ex.ToString();
            }
        }

        private void grdPatientList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    if (grdPatientList.CurrentRow != null)
                    {
                        string in_barcode = PatientBusiness.GetBarCodeTestInfo(
                            Convert.ToInt32(Utility.GetValueFromGridColumn(grdPatientList, "colPatient_ID",
                                                                           grdPatientList.CurrentRow.Index)),
                            SystemParaBarcode.ParaBacode, 0);
                        if (in_barcode != null)
                        {
                            barcode1.Data = in_barcode;
                            barcode1.Image().Save(Application.StartupPath + "\\Temp.jpg", ImageFormat.Jpeg);
                        }
                        else
                        {
                            barcode1.Data = "0000000000";
                        }
                        if (printDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printDocument1.DefaultPageSettings.Margins = margins;
                            printDocument1.Print();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        }
        private void UpdateRegTest()
        {
            try
            {
                try
                {
                    int Test_ID = 0;
                    if (grdTestType.CurrentRow != null)
                    {
                        Test_ID =
                            Utility.Int32Dbnull(
                                Utility.GetValueFromGridColumn(grdTestType, "colTest_ID", grdTestType.CurrentRow.Index),
                                -1);
                    }
                    dtRegTest = TestInfoBusiness.GetTestReg(Test_ID);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (grdTestType.CurrentRow != null)
                {
                    var frm = new frm_CreateRegList();

                    frm.v_TestType_Id =
                        Utility.Int32Dbnull(
                            Utility.GetValueFromGridColumn(grdTestType, "TestType_ID", grdTestType.CurrentRow.Index), -1);
                    frm.g_dtResultDetail = dtRegTest;
                    frm.em_Action = action.doNothing;
                    //  frm.v_Barcode = Utility.GetValueFromGridColumn(grdTestType, "colBarcode", grdTestType.CurrentRow.Index);
                    frm.v_Test_ID =
                        Utility.Int32Dbnull(
                            Utility.GetValueFromGridColumn(grdTestType, "colTest_ID", grdTestType.CurrentRow.Index), -1);
                    frm.v_Patient_ID =
                        Utility.Int32Dbnull(
                            Utility.GetValueFromGridColumn(grdTestType, "colsPatient_ID", grdTestType.CurrentRow.Index),
                            -1);
                    frm.ShowDialog();
                    if (frm.b_Status)
                    {
                        dtRegTest = frm.g_dtResultDetail;
                        em_Action = frm.em_Action;

                        dtRegTest.AcceptChanges();
                    }
                    foreach (DataRow dr in dtRegTest.Rows)
                    {
                        //foreach (DataRow drv  in dtRegTest.Rows)
                        //{
                        //    drv["CHON"] = "1";
                        ////}
                        //dtRegTest.AcceptChanges();
                        //if (dr["CHON"] == "1")
                        //{
                        int InsertReg =
                            TestInfoBusiness.InsertRegList(Utility.Int32Dbnull(dr[TRegList.Columns.TestId], 0),
                                                           Utility.Int32Dbnull(dr[TRegList.Columns.DeviceId], 0),
                                                           dr[TRegList.Columns.Barcode].ToString(),
                                                           dr[TRegList.Columns.AliasName].ToString(),
                                                           dr[TRegList.Columns.ParaName].ToString(), true);
                        //}
                    }
                    Utility.SetDataSourceForDataGridView(grdTRegList, dtRegTest, false, true, "", "");
                }
                //ModifyCommand();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void grdTestType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateRegTest();
            }
        }

    

      

        private void barcode1_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdPatientList.CurrentRow != null)
                {
                    if (barcode != null)
                    {
                        //barcode1.Data = barcode;
                        barcode2.Data = barcode;
                        //barcode2.Data = tempBarcode;

                        //   barcode1.Image().Save(Application.StartupPath + "\\Temp.jpg", ImageFormat.Jpeg);
                    }

                    else
                    {
                        barcode1.Data = "0000000000";
                    }
                    if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        //printDocument1.DefaultPageSettings.Margins = margins;
                        printDocument1.DefaultPageSettings.Margins = margins;
                        printDocument1.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                //SetTextWarning(ex.ToString());
            }
        }

        #endregion

        private void grdTestType_DoubleClick(object sender, EventArgs e)
        {
            UpdateRegTest();
        }

    
     
    }
}