using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Report.UI.Reports.VD_1C;
using Vietbait.Lablink.Utilities;
using Vietbait.TestInformation.Business;

namespace Vietbait.TestInformation.UI
{
    public partial class FrmTestDetail : Office2007Form
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
        private readonly List<string> _arrTestTypeList = new List<string>();

        //Biến phục vụ cho việc load dữ liệu động lên lưới
        private readonly DataTable _grdPatientsSource = new DataTable();

        // BackgroundWorker để nạp dữ liệu trên một luồng khác 
        private BackgroundWorker _bgProcessing;
        private string _reportType;
        private DataTable _tblPatients;
        private DataTable _testTypeList;

        #endregion

        #region Contructor

        public FrmTestDetail()
        {
            InitializeComponent();

            //Add Handler for All checkbox
            cboCoKetQua.CheckedChanged += CboTatcaCheckedChanged;
            cboChuaCoKetQua.CheckedChanged += CboTatcaCheckedChanged;
            grdPatients.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;
        }

        #endregion

        #region Private Method

        #region SuperTabStrip Function And Events

        private SuperTabItem GetSelectedTabItem()
        {
            try
            {
                return (from control in superTabControl1.Tabs.OfType<SuperTabItem>()
                        where control.IsSelected
                        select control).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataGridViewX GetGridinSelectedTab(SuperTabItem tab)
        {
            try
            {
                return (from control in tab.AttachedControl.Controls.OfType<DataGridViewX>()
                        select control).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GridInTab_KeyUp(object sender, KeyEventArgs e)
        {
            var grd = sender as DataGridViewX;
            if ((e.KeyCode == Keys.Delete) && (grd.CurrentRow != null))
            {
                if (
                    MessageBox.Show("Bạn có muốn xóa kết quả này ?", "Thông Báo", MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    DataRow dr = (grd.DataSource as DataTable).Rows[grd.CurrentRow.Index];
                    string testDetailId = dr[TResultDetail.Columns.TestDetailId].ToString();
                    TestInfoBusiness.DeleteTestResult(testDetailId);
                    (grd.DataSource as DataTable).Rows.Remove(dr);
                }
            }
        }

        private void TabClick(object sender, EventArgs e)
        {
            //Lấy về GridviewX của Tab đang được chọn
            var tab = sender as SuperTabItem;

            if (tab != null)
            {
                // Lấy về TesttypeID từ TestID
                string testTypeId = TestInfoBusiness.GetTestTypeIdFromTestId(tab.Tag.ToString());


                //Xóa các Button đã tạo trước đó
                superTabControl1.ControlBox.SubItems.Clear();


                //Bổ sung Button Items

                //Nếu là tab tất cả thì không thêm nút
                if (testTypeId != "-1")
                {
                    //Thêm nút bấm :D
                    var btnThemKetQuaCuaXetNghiem = new ButtonItem();

                    btnThemKetQuaCuaXetNghiem.Text = @"Thêm kết quả";
                    btnThemKetQuaCuaXetNghiem.Tag = @"-1";
                    btnThemKetQuaCuaXetNghiem.ButtonStyle = eButtonStyle.ImageAndText;
                    btnThemKetQuaCuaXetNghiem.Click += Help_Click;
                    btnThemKetQuaCuaXetNghiem.SubItemsExpandWidth = 50;

                    //Lấy về các thiết bị chạy TesttypeId trên
                    DataTable tblDevices = TestInfoBusiness.GetDevicesFromTestTypeId(testTypeId);

                    foreach (DataRow dr in tblDevices.Rows)
                    {
                        var sbi = new ButtonItem();
                        sbi.Text = dr[DDeviceList.Columns.DeviceName].ToString();
                        //Tag của Button là DeviceID
                        sbi.Tag = dr[DDeviceList.Columns.DeviceId].ToString();
                        sbi.AutoCollapseOnClick = true;
                        sbi.Click += Help_Click;
                        btnThemKetQuaCuaXetNghiem.SubItems.Add(sbi);
                    }
                    superTabControl1.ControlBox.SubItems.Add(btnThemKetQuaCuaXetNghiem); //Kết thúc thêm nút bấm
                }

                if (grdPatients.CurrentRow != null)
                {
                    string patientId = grdPatients.CurrentRow.Cells["Patient_ID"].Value.ToString();
                    DataTable result = TestInfoBusiness.GetTestResultByTestType(patientId, testTypeId);
                    DataGridViewX grd = GetGridinSelectedTab(tab);
                    //Gán DataSource cho Grid
                    if (grd != null)
                    {
                        //grd.Focus();
                        grd.DataSource = result;
                        grd.Refresh();
                        //if (grd.Rows.Count > 0) grd.CurrentCell = grd.Rows[0].Cells[1];
                        if (testTypeId != "-1") grd.Columns["colLoaiXetNghiem"].Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Xóa tất cả các TAB
        /// </summary>
        private void ClearAllTabPage()
        {
            try
            {
                while (superTabControl1.Tabs.Count != 0)
                {
                    superTabControl1.CloseTab(superTabControl1.Tabs[0] as SuperTabItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles ControlBox sample item selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Click(object sender, EventArgs e)
        {
            try
            {
                //Lấy về Button được nhấn
                var bi = sender as ButtonItem;

                //Nếu Tag của Button != -1 thì xử lý
                if ((bi != null) && bi.Tag.ToString() != "-1")
                {
                    SuperTabItem tab = GetSelectedTabItem();
                    DataGridViewX gridViewX = GetGridinSelectedTab(tab);
                    var dt = gridViewX.DataSource as DataTable;

                    int rowCount = gridViewX.Rows.Count;

                    //Lấy về tên các kết quả đã có trên lưới
                    EnumerableRowCollection<string> oldResult = (from dr in dt.AsEnumerable()
                                                                 select dr[TResultDetail.Columns.ParaName]).Cast<string>
                        ();

                    string patientId, testTypeId, barcode, testId;
                    testId = tab.Tag.ToString();
                    patientId = grdPatients.CurrentRow.Cells["Patient_ID"].Value.ToString();
                    testTypeId = TestInfoBusiness.GetTestTypeIdFromTestId(testId);
                    barcode = TestInfoBusiness.GetBarcodeFromTestId(testId);

                    //DataTable modifiedTable = dt.GetChanges(DataRowState.Modified);
                    //DataTable newTable = dt.GetChanges(DataRowState.Added);

                    DataTable tblDataControlToAdd = TestInfoBusiness.GetDataControlsFromDeviceId(bi.Tag.ToString(),
                                                                                                 oldResult);
                    foreach (DataRow dataRow in tblDataControlToAdd.Rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr[TResultDetail.Columns.TestDetailId] = -1;
                        dr[TResultDetail.Columns.TestId] = Convert.ToInt32(testId);
                        dr[TResultDetail.Columns.PatientId] = Convert.ToInt32(patientId);
                        dr[TResultDetail.Columns.TestTypeId] = Convert.ToInt32(testTypeId);
                        dr[TResultDetail.Columns.TestDate] = DateTime.Now;
                        dr[TResultDetail.Columns.TestSequence] = -1;
                        dr[TResultDetail.Columns.DataSequence] = -1;
                        dr[TResultDetail.Columns.TestResult] = string.Empty;
                        dr[TResultDetail.Columns.NormalLevelW] = dataRow[DDataControl.Columns.NormalLevelW];
                        dr[TResultDetail.Columns.NormalLevel] = dataRow[DDataControl.Columns.NormalLevel];
                        dr[TResultDetail.Columns.MeasureUnit] = dataRow[DDataControl.Columns.MeasureUnit];
                        dr[TResultDetail.Columns.ParaName] = dataRow[DDataControl.Columns.DataName];
                        dr[TResultDetail.Columns.ParaStatus] = 1;
                        dr[TResultDetail.Columns.Note] = string.Empty;
                        dr[TResultDetail.Columns.PrintData] = 1;
                        dr[TResultDetail.Columns.Barcode] = barcode;
                        dr[TResultDetail.Columns.UpdateNum] = 1;
                        dt.Rows.Add(dr);
                    }
                    gridViewX.Focus();
                    gridViewX.CurrentCell = gridViewX.Rows[rowCount].Cells["colKetQua"];
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GridInTabCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var grd = sender as DataGridViewX;
                //MessageBox.Show(grd.Rows .Count.ToString());
                //MessageBox.Show(e.ColumnIndex.ToString() + e.RowIndex.ToString());
                if (e.ColumnIndex == 1)
                {
                    DataRow dr = (grd.DataSource as DataTable).Rows[e.RowIndex];

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
                        else
                        {
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

        private void AddNewUserTab(string tabName, string testId)
        {
            try
            {
                //Gán Tabpage lên Tabcontrol
                string colTenXetNghiem,
                       colKetQua,
                       colBarcode,
                       colLoaiXetNghiem,
                       tenXetNghiem,
                       ketQua,
                       barcode,
                       loaiXetNghiem;
                colTenXetNghiem = "colTenXetNghiem";
                colKetQua = "colKetQua";
                colBarcode = "colBarcode";
                colLoaiXetNghiem = "colLoaiXetNghiem";
                tenXetNghiem = "Tên Xét Nghiệm";
                ketQua = "Kết Quả";
                barcode = "Barcode";
                loaiXetNghiem = "Loại Xét Nghiệm";

                SuperTabItem tab = superTabControl1.CreateTab(tabName);
                tab.Tag = testId;

                //Add Columns to GridviewX
                var grd = new DataGridViewX();
                grd.Columns.Add(Lablink.Utilities.UI.CreateGridTextColumn(colTenXetNghiem, tenXetNghiem,
                                                                          TResultDetail.Columns.ParaName));
                grd.Columns.Add(Lablink.Utilities.UI.CreateGridTextColumn(colKetQua, ketQua,
                                                                          TResultDetail.Columns.TestResult));
                grd.Columns.Add(Lablink.Utilities.UI.CreateGridTextColumn(colBarcode, barcode,
                                                                          TResultDetail.Columns.Barcode));
                grd.Columns.Add(Lablink.Utilities.UI.CreateGridTextColumn(colLoaiXetNghiem, loaiXetNghiem,
                                                                          TTestTypeList.Columns.TestTypeName));

                //Set Grid Properties
                grd.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                grd.MultiSelect = false;
                grd.AllowUserToAddRows = false;
                grd.AutoGenerateColumns = false;
                grd.BackgroundColor = Color.White;
                grd.AllowUserToDeleteRows = false;
                grd.Dock = DockStyle.Fill;

                //Set Event handler for Grid
                grd.CellValueChanged += GridInTabCellValueChanged;
                grd.KeyUp += GridInTab_KeyUp;
                grd.RowPostPaint += Lablink.Utilities.UI.GridviewRowPostPaint;


                //Add Grid to Tab
                tab.AttachedControl.Controls.Clear();
                tab.AttachedControl.Controls.Add(grd);

                //Gán sự kiện xử lý Tab Mouseup
                tab.Click += TabClick;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

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
            cboStyle.SelectedValueChanged -= cboStyle_SelectedValueChanged;
            cboStyle.Items.Clear();
            foreach (eStyle style in Enum.GetValues(typeof (eStyle)))
            {
                cboStyle.Items.Add(style);
            }
            cboStyle.SelectedIndex = 0;
            cboStyle.SelectedValueChanged += cboStyle_SelectedValueChanged;
        }

        //private DataTable GetDataForPrint()
        //{
        //    DataTable tblForPrint = new DataTable();
        //    DataRow dr = null;
        //    if (grdPatients.CurrentRow != null)
        //    {
        //        foreach (DataRow row in _tblPatients.Rows)
        //        {
        //            if (row["Patient_ID"])
        //            {

        //            }
        //        }
        //        tblForPrint.ImportRow();
        //    }

        //    return tblForPrint;
        //}
        /// <summary>
        /// Load tuỳ chọn ngày vào Combobox
        /// </summary>
        private void InitComboboxAndDatePicker()
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

                grbDate.Enabled = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Load thông tin về các loại xét nghiệm vào combobox
        /// </summary>
        /// <param name="fromDate">Ngày bắt đầu</param>
        /// <param name="toDate">Ngày kết thúc</param>
        private void FillTestTypeList()
        {
            try
            {
                //Lấy về data table chứa dữ liệu
                //DateTime? pfromdate=null, ptodate=null;
                DataTable dataTable = TestInfoBusiness.LoadTestType(null, null);

                //Fill DataTable vào Checklistbox

                //Xóa trắng Checklistbox
                clbTestType.Items.Clear();

                //Xóa trắng mảng lưu trữ testtype ID
                _arrTestTypeList.Clear();
                _arrTestTypeList.Add("-3");

                //Kiểm tra nếu tìm thấy dữ liệu thì tiến hành load vào Checklistbox);
                if (dataTable.Rows.Count > 0)
                {
                    //Bổ sung phần tử đầu tiên (có vai trò điều khiển)
                    clbTestType.Items.Add("Tất cả", true);

                    //Load dữ liệu đọc được từ Datatable ở trên
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        clbTestType.Items.Add(
                            dr[TTestTypeList.Columns.TestTypeName].ToString().ToUpper().Replace("XÉT NGHIỆM", "").Trim(),
                            true);
                        _arrTestTypeList.Add(dr[TTestTypeList.Columns.TestTypeId].ToString());
                    }
                }

                //Thiết lập chiều rộng tự động cho lưới hiển thị
                clbTestType.ColumnWidth = MkStrWidth(clbTestType) + 20;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int MkStrWidth(CheckedListBox clb)
        {
            Font fnt = Font;
            int result = 0;
            foreach (string str in clb.Items)
            {
                int width = TextRenderer.MeasureText(str, fnt).Width;
                if (width > result)
                {
                    result = width;
                }
            }
            return result;
        }

        private void SetCheckListboxState()
        {
            if (clbTestType.Items.Count < 0) return;
            try
            {
                int index = clbTestType.SelectedIndex;
                //Xử lý khi click vào Item đầu tiên
                if (index == 0)
                {
                    if (clbTestType.Items.Count >= 2)
                    {
                        for (int i = 1; i < clbTestType.Items.Count; i++)
                        {
                            clbTestType.SetItemChecked(i, clbTestType.GetItemChecked(0));
                        }
                    }
                }
                else
                {
                    clbTestType.SetItemChecked(0, false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Fill dữ liệu vào combobox giới tính
        /// </summary>
        private void FillSexCombobox()
        {
            cboSex.Items.Clear();

            var dt = new DataTable();
            dt.Columns.Add("ValueItem");
            dt.Columns.Add("DisplayItem");

            DataRow dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "Tất Cả";
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

        //Hàm lấy dữ liệu và in
        private void GetDataForPrint(DataTable mDsReport)
        {
            var crpt = new VD_1C_crpt_DetailTestReport_ALL();
            var objForm = new frmPrintPreview("", crpt, true, true);
            try
            {
                crpt.SetDataSource(mDsReport);

                //crpt.DataDefinition.FormulaFields["Formula_1"].Text = Strings.Chr(34) +
                //                                                      "    Bác sĩ điều trị                                                                   "
                //                                                          .Replace("#$X$#",
                //                                                                   Strings.Chr(34) + "&Chr(13)&" +
                //                                                                   Strings.Chr(34)) + Strings.Chr(34);
                crpt.SetParameterValue("ShowSubReport", 1);
                crpt.SetParameterValue("ShowMainReport", 0);

                crpt.SetParameterValue("ParentBranchName", LablinkBusinessConfig.GetParentBranchName());
                crpt.SetParameterValue("BranchName", LablinkBusinessConfig.GetBranchName());
                string sss = LablinkBusinessConfig.GetPhone();
                crpt.SetParameterValue("Address", LablinkBusinessConfig.GetAddress());
                crpt.SetParameterValue("sPhone", LablinkBusinessConfig.GetPhone());


                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
                Utility.DefaultNow(this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Load Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmTestDetail_Load(object sender, EventArgs e)
        {
            try
            {
                _reportType = LablinkBusinessConfig.GetReportType();
                circularProgress1.IsRunning = true;

                try
                {
                    InitComboboxAndDatePicker();

                    cboDate.SelectedIndex = 0;

                    // Khởi tạo Sex Combobox
                    FillSexCombobox();

                    // Load test type list to checklistbox
                    FillTestTypeList();

                    grdPatients.AutoGenerateColumns = false;


                    //Get All Style from Style Manager :D
                    GetAllStyle();
                }
                catch (Exception ex)
                {
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
                File.WriteAllText("FrmTestDetail.cs.txt", ex.ToString());
            }
        }

        private void cboDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = cboDate.SelectedIndex;
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
                    grbDate.Enabled = true;
                    dtpFromDate.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void clbTestType_MouseUp(object sender, MouseEventArgs e)
        {
            SetCheckListboxState();
        }

        private void clbTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetCheckListboxState();
        }

        /// <summary>
        /// Xử lý khi click vào một ô check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboTatcaCheckedChanged(object sender, EventArgs e)
        {
            var ctrl = (CheckBoxX) sender;

            IEnumerable<CheckBoxX> controls = from control in grpStatus.Controls.OfType<CheckBoxX>()
                                              where control != ctrl
                                              select control;
            foreach (CheckBoxX checkBoxX in controls)
            {
                checkBoxX.CheckedChanged -= CboTatcaCheckedChanged;
                if (checkBoxX.Checked) checkBoxX.Checked = false;
                checkBoxX.CheckedChanged += CboTatcaCheckedChanged;
            }
        }

        //Click vào nút tìm kiếm :D
        private void BtnSearchClick(object sender, EventArgs e)
        {
            ClearAllTabPage();
            circularProgress1.IsRunning = true;
            btnSearch.Enabled = false;
            _bgProcessing = new BackgroundWorker();
            _bgProcessing.DoWork += FillToGrid;
            _bgProcessing.RunWorkerCompleted += FinishFillToGrid;
            _bgProcessing.RunWorkerAsync();
        }

        private void FinishFillToGrid(object sender, RunWorkerCompletedEventArgs e)
        {
            //Gán Handler của Grid
            grdPatients.SelectionChanged += GrdPatientsSelectionChanged;
            GrdPatientsSelectionChanged(new object(), new EventArgs());

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
                        new WarningBoxStatus(string.Format("Tìm thấy <b> {0} </b> Bệnh nhân", _tblPatients.Rows.Count),
                                             false));
                }

                grdPatients.DataSource = _tblPatients;
            }
            circularProgress1.IsRunning = false;
            btnSearch.Enabled = true;
        }

        private void FillToGrid(object sender, DoWorkEventArgs e)
        {
            //Bỏ Handler của Grid
            grdPatients.SelectionChanged -= GrdPatientsSelectionChanged;
            _grdPatientsSource.Clear();
            string testIdString;
            testIdString = string.Empty;
            try
            {
                //var dt = grdPatients.DataSource as DataTable;
                //if (dt != null) dt.Clear();
                // Lấy về danh sách các test iD được check
                testIdString = GetTestIdString();

                // Nếu loại xét nghiệm chưa được chọn thì hiển thị thông báo 
                if (testIdString.Trim() == string.Empty)
                    throw new Exception("Bạn phải chọn loại xét nghiệm");

                // Lấy về trạng thái kết quả được tìm kiếm 
                int hasTest = -1, age = 0;
                try
                {
                    age = Convert.ToInt32(txtAge.Text);
                }
                catch (Exception)
                {
                    age = 0;
                }


                // Nếu Check tất cả các loại xét nghiệm
                //DataTable result = null;
                if (cboTatca.Checked)
                {
                    hasTest = -1;
                }
                    // Chỉ check ô có kết quả
                else if (cboCoKetQua.Checked)
                {
                    hasTest = 1;
                }

                    // Chỉ check Chưa có kết quả
                else if (cboChuaCoKetQua.Checked)
                {
                    hasTest = 0;
                    //dgvPatientList.DataSource = result;
                }
                //todo: Get Sex
                //int sex = Convert.ToInt32(cboSex.SelectedValue);
                int sex = Convert.ToInt32(GetControlPropertyThreadSafe(cboSex, "SelectedValue"));
                _tblPatients = TestInfoBusiness.GetPatientList(dtpFromDate.Value, dtpTodate.Value, testIdString,
                                                               txtBarcode.Text, txtPID.Text, txtName.Text, age,
                                                               sex, hasTest);
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        /// <summary>
        /// Trả về danh sách các loại xét nghiệm dạng chuỗi cách nhau dấu ,
        /// </summary>
        /// <returns></returns>
        private string GetTestIdString()
        {
            try
            {
                string testIdString = string.Empty;
                testIdString =
                    (from object checkedItem in clbTestType.CheckedItems select clbTestType.Items.IndexOf(checkedItem)).
                        Aggregate(testIdString, (current, index) => current + (_arrTestTypeList[index] + ",")).Trim();
                if (testIdString.EndsWith(",")) testIdString = testIdString.Remove(testIdString.Length - 1);

                return testIdString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Thoát và giải phóng vùng nhớ cho form
        private void BtnThoatClick(object sender, EventArgs e)
        {
            Dispose(true);
        }

        //Xử lý sự kiện khi click vào lưới dữ liệu danh sách bệnh nhân
        private void GrdPatientsSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                //superTabControl1.SuspendLayout();


                //superTabControl1.TabStrip.BeginUpdate();

                //Clear all tab
                ClearAllTabPage();

                //Lấy về PatientID
                if (grdPatients.CurrentRow != null)
                {
                    decimal patientId = Convert.ToDecimal(grdPatients.CurrentRow.Cells["Patient_ID"].Value);
                    //lấy về danh sách các loại xét nghiệm của bệnh nhân đó
                    string testtypeids = GetTestIdString();
                    _testTypeList = TestInfoBusiness.GetTestTypeIdFromDateToDate(patientId, testtypeids,
                                                                                 dtpFromDate.Value,
                                                                                 dtpTodate.Value);

                    AddNewUserTab("Tất cả", "-1");

                    foreach (DataRow dr in _testTypeList.Rows)
                    {
                        string testName =
                            dr[TTestTypeList.Columns.TestTypeName].ToString().ToUpper().Replace("XÉT NGHIỆM", "").Trim();
                        string testId = dr[TTestInfo.Columns.TestId].ToString();
                        AddNewUserTab(testName, testId);
                    }

                    //Lấy về TAB đầu tiên được chọn
                    SuperTabItem controls = (from control in superTabControl1.Tabs.OfType<SuperTabItem>()
                                             select control).First();

                    //Giả lập sự kiện Click trên Tab đầu tiên

                    if (controls != null)
                    {
                        controls.Focus();
                        TabClick(controls, new EventArgs());
                    }
                }
                //superTabControl1.ResetTabStripColor();
                //superTabControl1.TabStrip.EndUpdate();
                //superTabControl1.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
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

        private void btnXoaThongTinBenhNhanVaTimKiem_Click(object sender, EventArgs e)
        {
            foreach (TextBox textBox in (from control in grbThongtinbenhnhan.Controls.OfType<TextBox>()
                                         where control.Text.Trim() != string.Empty
                                         select control))
            {
                textBox.Text = string.Empty;
            }
            cboSex.SelectedIndex = 0;
            btnSearch.PerformClick();
        }

        private void cboStyle_SelectedValueChanged(object sender, EventArgs e)
        {
            styleManager1.ManagerStyle = (eStyle) cboStyle.SelectedItem;
        }

        private void btnPrintByPatient_Click(object sender, EventArgs e)
        {
            try
            {
                if (grdPatients.CurrentRow != null)
                {
                    int patientId = Convert.ToInt32(grdPatients.CurrentRow.Cells["Patient_ID"].Value);
                    DateTime dat = Convert.ToDateTime(grdPatients.CurrentRow.Cells["clDate"].Value);
                    DataTable result = TestInfoBusiness.GetDataForPrint(patientId, -1, dat);
                    GetDataForPrint(result);
                }
            }
            catch (Exception ex)
            {
                SetWarningStatus(new WarningBoxStatus(ex.Message, true));
            }
        }

        #endregion

        private void btnTest_Click(object sender, EventArgs e)
        {
            //IEnumerable<DataGridViewX> controls = from control in tab.AttachedControl.Controls.OfType<DataGridViewX>()
            //                                      select control;

            //IEnumerable<SuperTabItem> controls = from control in superTabControl1.Tabs.OfType<SuperTabItem>()
            //                                     select control;


            //MessageBox.Show(controls.Count().ToString());
            //superTabControl1.SelectedTab = null;
            superTabControl1.SelectedTabIndex = -1;
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
        }
    }
}