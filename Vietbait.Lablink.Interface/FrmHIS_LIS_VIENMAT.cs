using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.VisualBasic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Interface.Barcode;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;
using VB6 = Microsoft.VisualBasic.Strings;

namespace Vietbait.Lablink.Interface
{
    public partial class FrmHIS_LIS_VIENMAT : Form

    {
        #region Attributes

        private const byte Col = 2;
        private const string Delete = "Xóa thành công";
        private const string Error = "Lỗi cập nhật";
        private const string SenddataHis = "Gửi kết quả sang hệ thống HIS thành công";
        private const string Success = "Cập nhật thành công";
        private readonly string _datetime = DateTime.Now.ToString("yyMMdd");
        private readonly List<string> _lstBarcode = new List<string>();
        private readonly Margins _margins = new Margins(25, 25, 5, 5);
        private string _add = "Thêm mới thành công";
        private string[] _barcodeConfig;
        private int _currTestTypeId;
        private Vnio _dataWrapper;
        private string _deviceName;
        private DataTable _diagnostic;
        private string _dinhTinh = "Âm tính;Dương tính";
        private DataSet _dsRegtest;
        private DataSet _dsResultDetail = new DataSet();
        private DataTable _dtAlldevice;
        private DataTable _dtChandoan;
        private DataTable _dtIdXn;
        private DataTable _dtPatientInfor;
        private DataTable _dtPatientOut = new DataTable();
        private DataTable _dtYeucauXn;
        private string _errorResult = "Lỗi chưa có kết quả! Bạn chọn có kết quả rồi gửi";
        private string _errorServer = " Lỗi không kết nối tới Server, Đề nghị bạn kiểm tra lại kết nối";
        private DataTable _hisParam;
        private string[] _idCanlamSangThucHien;
        private string _idhis;
        private string _idxn;
        private DataTable _loadYeuCauXn;
        private DataTable _mDtResultDetail = new DataTable();
        private DataTable _mDtTestInfo = new DataTable();
        private DataTable _mDttblYeucauxn = new DataTable();
        private DataTable _paramMapping;
        private DataTable _patienInfo, _reglist;
        private string _rowFilter = "1=1";
        private string _rowFilter1 = "1=1";
        private string _rowFilter2 = "1=1";
        private string _strEntryLink;
        private string _strEntryLink2;
        private string _strPassword;
        private string _strUserName;
        private string _tempBarcode;
        private string _test = @"07110915001.xml";
        private int _testTypeId;
        private int _trangthai;
        private int _vTestId = -1;
        private string _vTestTypeList = "";
        private string deviceID;
        private int testType_ID;
        public string idHisXn = "";

        public PrintDocument pd = new PrintDocument();
        public Margins margins = new Margins();
        private string[] BarcodeConfig;
        public Int32 PrintAreaHeight;
        public Int32 PrintAreaWidth;
        private string barcodeConfigFileName = "BarcodePrinterConfig.txt";
        public Int32 marginLeft;
        public Int32 marginTop;
        public Int32 marginleft2;
        private int ConfigCount = 8;

        #endregion

        #region Contructor

        public FrmHIS_LIS_VIENMAT()
        {
            InitializeComponent();
            InitializeEventControl();
        }

        private void InitializeEventControl()
        {
            grdList.AutoGenerateColumns = false;
            grdRegList.AutoGenerateColumns = false;
            cboHasTest.SelectedIndex = 0;
        }

        private void CreateConfigFile()
        {
            BarcodeConfig = new string[ConfigCount];
            BarcodeConfig[0] = "-1";
            BarcodeConfig[1] = "130";
            BarcodeConfig[2] = "75";
            BarcodeConfig[3] = "30";
            BarcodeConfig[4] = "10";
            BarcodeConfig[5] = "2";
            BarcodeConfig[6] = "0";
            BarcodeConfig[7] = "1";
            //File.WriteAllLines(barcodeConfigFileName, BarcodeConfig)
        }

        /// <summary>
        /// Sự kiện formload
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmHIS_LIS_VIENMAT_Load(object sender, EventArgs e)
        {
            try
            {
                //ToggleEndlessProgress.Execute();
                circularProgress1.IsRunning = true;
                TypeConverter tc = TypeDescriptor.GetConverter(Font);
                string paramBarcodeConfig = "BarcodeNetNam.txt";
                if (File.Exists(paramBarcodeConfig))
                {
                    _barcodeConfig = File.ReadAllLines(paramBarcodeConfig);
                }
                else
                {
                    _barcodeConfig = new string[5];

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
                FillSexCombobox();
                IdCanLamSangThucHien();
                barcode1.Data = "0000000000";
                txtSoPhieu.Text = _datetime;
                LoadTestTypeAlias();
                LoadHisParam();
                LoadAllDevices();
                LoadParaHisLis();
                grdRegList.RowPostPaint += UI.GridviewRowPostPaint;
                grdResultDetail.RowPostPaint += UI.GridviewRowPostPaint;
                grdRegList_out.RowPostPaint += UI.GridviewRowPostPaint;
                grdList.RowPostPaint += UI.GridviewRowPostPaint;
                grdlist_out.RowPostPaint += UI.GridviewRowPostPaint;
                cboDeviceID_SelectedIndexChanged(cboDeviceID, new EventArgs());
                //grdRegList_out_SelectionChanged(grdRegList_out,new EventArgs());
                // strEntryLink = "http://119.17.194.249:8080";

                GrdThongTinBn();
                GrdyeuCauXn();
                GrdThongTinBnOut();
                //grdKetqua();
                cboTestName.SelectedIndex = 0;
                LoadTestType();
                ThongTinDinhTinh();

                GetTestTypeId();
                LoadTestTypeforSeach();
                txtmabenhpham.Focus();
                //ChangeColor();

                const string netnamTxt = "NETNAM.txt";
                if (File.Exists(netnamTxt))
                {
                    string[] readAllLines = File.ReadAllLines(netnamTxt);
                    string[] arrayList = readAllLines;
                    _strEntryLink = arrayList[0].Trim();
                    _strUserName = arrayList[1].Trim();
                    _strEntryLink2 = arrayList[3].Trim();
                    _strPassword = arrayList[2].Trim();
                    _dataWrapper = new Vnio(_strEntryLink, _strUserName, _strPassword);
                }

                if (File.Exists(barcodeConfigFileName))
                {
                    BarcodeConfig = File.ReadAllLines(barcodeConfigFileName);
                }
                else
                {
                    CreateConfigFile();
                }
                if (BarcodeConfig.Length < ConfigCount)
                {
                    CreateConfigFile();
                }
                Int32 temp = Convert.ToInt32(BarcodeConfig[3]);
                marginleft2 = Convert.ToInt32(BarcodeConfig[4]);
                margins = new Margins(temp, temp, 20, 5);
            }
            catch (Exception ex)
            {
                //throw;
            }
            finally
            {
                circularProgress1.IsRunning = false;
            }
        }

        #endregion

        #region Form Events

        private void btnLayThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                UseWaitCursor = true;
                if (txtmabenhpham.Text == "")
                    if (_patienInfo != null) _patienInfo.Clear();
                if (_reglist != null) _reglist.Clear();

                var dataSet = new DataSet();

                DataSet dataSet1;

                DataRow[] arrDrMapping =
                    _dtIdXn.Select("TESTTYPE_ID=" + Utility.Int32Dbnull(cboTestType.SelectedValue, -1));
                if (arrDrMapping.Length > 0)
                {
                    idHisXn = Strings.Right("00" + StrDbNull(arrDrMapping[0]["id_his_xn"]), 2);

                    if (_dataWrapper != null)
                    {
                        if (Debugger.IsAttached)
                        {
                            SetTextWarning("Bạn đang chạy ở chế độ Debug");
                            var of = new OpenFileDialog();
                            of.ShowDialog();
                            dataSet1 = Getxmlpatient(of.FileName);
                        }
                        else
                        {
                            dataSet1 =
                            _dataWrapper.GetPatientInfomationById(idHisXn + txtSoPhieu.Text.Trim().ToUpper() +
                                                                  txtmabenhpham.Text);
                        }
                        

                        if (dataSet1 != null && dataSet1.Tables.Count > 0 &&
                            dataSet1.Tables["thongTinNguoiBenh"].Rows.Count > 0)
                        {
                            if (dataSet1.Tables["thongTinNguoiBenh"].Rows.Count > 0)
                            {

                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("chanDoan"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add(new DataColumn("chanDoan",
                                                                                                    typeof (string)));
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("bacSyDieuTri"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("bacSyDieuTri");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("khoa"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("khoa");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("diaChi"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("diaChi");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("dichVu"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("dichVu");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("maBenhNhan"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("maBenhNhan");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("buong"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("buong");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("idKhoa"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("idKhoa");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("idBacSyDieuTri"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("idBacSyDieuTri");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("giuong"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("giuong");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("diaChi"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("diaChi");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("noiTru"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("noiTru");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("phong"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("phong");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("tuoi"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("tuoi");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("tenBenhNhan"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("tenBenhNhan");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("idDoiTuongBenhNhan"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("idDoiTuongBenhNhan");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("DoiTuong"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("DoiTuong");

                                CopyTable(dataSet1, dataSet);
                            }
                            else
                            {
                                SetTextWarning("Không có thông tin bệnh nhân");
                                
                            }
                        }
                       
                        if (dataSet.Tables.Count > 0 && dataSet.Tables.Contains("yeuCauXetNghiem") &&
                            !dataSet.Tables["yeuCauXetNghiem"].Columns.Contains("id_his_xn") &&
                            !dataSet.Tables.Contains("chanDoan"))
                            dataSet.Tables.Add("chanDoan");
                        dataSet.Tables["yeuCauXetNghiem"].Columns.Add(new DataColumn("id_his_xn", typeof(int)));

                        if (dataSet.Tables.Count > 0 && dataSet.Tables.Contains("yeuCauXetNghiem"))
                        {
                            foreach (DataRow dr in dataSet.Tables["yeuCauXetNghiem"].Rows)
                            {
                                //xet nghiem dong mau
                                DataRow[] arrDrMapping2 = _dtIdXn.Select("TESTTYPE_ID=" + GetTestTypeIdDm());
                                if (dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[0] ||
                                    dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[1] ||
                                    dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[2] ||
                                    dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[3] ||
                                    dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[4])
                                {
                                    if (arrDrMapping2.Length > 0)
                                    {
                                        string idDm = Strings.Right("00" + StrDbNull(arrDrMapping2[0]["id_his_xn"]), 2);
                                        if (NullOrDbNullValue(dr["id_his_xn"]))
                                            dr["id_his_xn"] = idDm;
                                    }
                                }
                                //Xet nghiem mien dich
                                DataRow[] arrDrMapping3 = _dtIdXn.Select("TESTTYPE_ID=" + GetTestTypeIdMd());
                                if (dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[5] ||
                                    dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[6])
                                {
                                    if (arrDrMapping3.Length > 0)
                                    {
                                        string idMd = Strings.Right("00" + StrDbNull(arrDrMapping3[0]["id_his_xn"]), 2);
                                        if (NullOrDbNullValue(dr["id_his_xn"]))
                                            dr["id_his_xn"] = idMd;
                                    }
                                }
                                //Xet nghiem Chung all
                                else
                                {
                                    if (NullOrDbNullValue(dr["id_his_xn"]))
                                        dr["id_his_xn"] = idHisXn;
                                }
                            }
                        }
                    }
                    else
                    {
                        //dataSet1 = getxmlpatient(id + "06110915003.xml");
                        //dataSet1 = getxmlpatient(@"C:\Users\HienTD\Desktop\01120112018.xml");
                        if (Debugger.IsAttached)
                        {
                            SetTextWarning("Bạn đang chạy ở chế độ Debug");
                            var of = new OpenFileDialog();
                            of.ShowDialog();
                            dataSet1 = Getxmlpatient(of.FileName);
                            if (dataSet1 != null && dataSet1.Tables.Count > 0 &&
                                dataSet1.Tables["thongTinNguoiBenh"].Rows.Count > 0)
                            {
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("chanDoan"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add(new DataColumn("chanDoan",
                                                                                                    typeof(string)));
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("bacSyDieuTri"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("bacSyDieuTri");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("khoa"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("khoa");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("diaChi"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("diaChi");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("dichVu"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("dichVu");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("maBenhNhan"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("maBenhNhan");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("buong"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("buong");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("idKhoa"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("idKhoa");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("idBacSyDieuTri"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("idBacSyDieuTri");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("giuong"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("giuong");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("diaChi"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("diaChi");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("noiTru"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("noiTru");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("phong"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("phong");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("tuoi"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("tuoi");
                                if (!dataSet1.Tables["thongTinNguoiBenh"].Columns.Contains("tenBenhNhan"))
                                    dataSet1.Tables["thongTinNguoiBenh"].Columns.Add("tenBenhNhan");
                                CopyTable(dataSet1, dataSet);
                            }
                            if (dataSet.Tables.Count > 0 && dataSet.Tables.Contains("yeuCauXetNghiem") &&
                                !dataSet.Tables["yeuCauXetNghiem"].Columns.Contains("id_his_xn") &&
                                !dataSet.Tables.Contains("chanDoan"))
                                dataSet.Tables.Add("chanDoan");
                            dataSet.Tables["yeuCauXetNghiem"].Columns.Add(new DataColumn("id_his_xn", typeof(int)));

                            if (dataSet.Tables.Count > 0 && dataSet.Tables.Contains("yeuCauXetNghiem"))
                            {
                                foreach (DataRow dr in dataSet.Tables["yeuCauXetNghiem"].Rows)
                                {
                                    //xet nghiem dong mau
                                    DataRow[] arrDrMapping2 = _dtIdXn.Select("TESTTYPE_ID=" + GetTestTypeIdDm());
                                    if (dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[0] ||
                                        dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[1] ||
                                        dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[2] ||
                                        dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[3] ||
                                        dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[4])
                                    {
                                        if (arrDrMapping2.Length > 0)
                                        {
                                            string idDm = Strings.Right(
                                                "00" + StrDbNull(arrDrMapping2[0]["id_his_xn"]), 2);
                                            if (NullOrDbNullValue(dr["id_his_xn"]))
                                                dr["id_his_xn"] = idDm;
                                        }
                                    }
                                    //Xet nghiem mien dich
                                    DataRow[] arrDrMapping3 = _dtIdXn.Select("TESTTYPE_ID=" + GetTestTypeIdMd());
                                    if (dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[5] ||
                                        dr["IdCanLamSangThucHien"].ToString() == _idCanlamSangThucHien[6])
                                    {
                                        if (arrDrMapping3.Length > 0)
                                        {
                                            string idMd = Strings.Right(
                                                "00" + StrDbNull(arrDrMapping3[0]["id_his_xn"]), 2);
                                            if (NullOrDbNullValue(dr["id_his_xn"]))
                                                dr["id_his_xn"] = idMd;
                                        }
                                    }
                                    //Xet nghiem Chung all
                                    else
                                    {
                                        if (NullOrDbNullValue(dr["id_his_xn"]))
                                            dr["id_his_xn"] = idHisXn;
                                    }
                                }
                            }
                        }
                        else
                        {
                            SetTextWarning("Kết nối tới HIS chưa được khởi tạo");
                        }
                    }
                }

                if (dataSet.Tables.Count > 0)
                {
                    _patienInfo = dataSet.Tables["thongTinNguoiBenh"].Clone();

                    foreach (DataRow drPatientInfor in dataSet.Tables["thongTinNguoiBenh"].Rows)
                    {
                        drPatientInfor["DoiTuong"] =
                            InterfaceHelper.GetObjectNameFromObjectId(drPatientInfor["idDoiTuongBenhNhan"].ToString());
                        if (_patienInfo.Select("maBenhNhan='" + drPatientInfor["maBenhNhan"] + "'").Length <= 0)

                            _patienInfo.ImportRow(drPatientInfor);
                    }
                    try
                    {
                        if (dataSet.Tables["chanDoan"] != null && dataSet.Tables["chanDoan"].Rows.Count > 0)
                        {
                            DataTable dt = dataSet.Tables["chanDoan"];
                            foreach (DataRow drv in _patienInfo.Rows)
                            {
                                if (dt.Rows[0]["tenBenh"] != null && dt.Rows.Count > 0)
                                {
                                    drv["chanDoan"] = dt.Rows[0]["tenBenh"];
                                }
                                else
                                {
                                    drv["chanDoan"] = "";
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        SetTextWarning(ex.ToString());
                    }


                    _patienInfo.AcceptChanges();
                    _reglist = dataSet.Tables["yeuCauXetNghiem"];
                    if (_reglist.Columns.IndexOf("TenThongSo") == -1)
                    {
                        _reglist.Columns.Add("TenThongSo");
                    }
                    if (_reglist.Columns.IndexOf("IsTestName") == -1)
                    {
                        _reglist.Columns.Add("IsTestName");
                    }
                    if (_reglist.Columns.IndexOf("DeviceId") == -1)
                    {
                        _reglist.Columns.Add("DeviceId");
                    }
                    if (_reglist.Columns.IndexOf("AliasName") == -1)
                    {
                        _reglist.Columns.Add("AliasName");
                    }
                    if (_reglist.Columns.IndexOf("ParaName") == -1)
                    {
                        _reglist.Columns.Add("ParaName");
                    }
                    grdList.DataSource = _patienInfo;

                    grdRegList.DataSource = _reglist;
                    foreach (DataRow dr in _reglist.Rows)
                    {
                        DataRow drr = InterfaceHelper.GetParaNameFromParaId(dr["idCanLamSangThucHien"].ToString());

                        if (drr != null)
                        {
                            dr["TenThongSo"] = StrDbNull(drr[TblParamMapping.Columns.MedParaName].ToString());
                            string deviceId = StrDbNull(drr[TblParamMapping.Columns.DeviceId].ToString());
                            dr["DeviceId"] = deviceId;
                            string lisParaName = StrDbNull(drr[TblParamMapping.Columns.LisParaName].ToString());
                            dr["ParaName"] = lisParaName;
                            dr["AliasName"] = InterfaceHelper.GetAliasNameFromParaNameAndDeviceId(lisParaName, deviceId);
                        }
                        else
                        {
                            dr["TenThongSo"] = @"Chưa ánh xạ thông số với HIS";
                        }
                    }
                    if (grdList.Rows.Count > 0 && grdRegList.Rows.Count > 0)
                    {
                        cmdSavetoLIS.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
                File.WriteAllText(@"C:\GetDataError.txt", ex.ToString(), Encoding.UTF8);
                MessageBox.Show(@"Mã Bệnh phẩm nhập vào không đúng - đề nghị kiểm tra lại!", @"Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            finally
            {
                UseWaitCursor = false;
            }
            cmdSavetoLIS.Focus();
        }

        private void cmdSavetoLIS_Click(object sender, EventArgs e)
        {
            try
            {
                string patientCode = "";
                var tblBenhNHan = grdList.DataSource as DataTable;
                var tblYCXN = grdRegList.DataSource as DataTable;
                //InsertIntoPatientInfor();
                var arrTestTypeID = new ArrayList();
                foreach (DataRow drYCXN in tblYCXN.Rows)
                {
                    if (!arrTestTypeID.Contains(drYCXN["id_his_xn"]))
                        arrTestTypeID.Add(drYCXN["id_his_xn"]);
                    _idhis = Strings.Right("00" + StrDbNull(drYCXN["id_his_xn"]), 2);
                }
                if (tblBenhNHan != null)
                    foreach (DataRow drBN in tblBenhNHan.Rows)
                    {
                        bool dichvu = !string.IsNullOrEmpty(drBN["dichVu"].ToString()) && Convert.ToBoolean(drBN["dichVu"]);
                        patientCode = drBN[TblHisLisPatientInfoVnio.Columns.MaBenhNhan].ToString();
                        string maBn = string.IsNullOrEmpty(drBN["MaBenhNhan"].ToString())
                                          ? GetPid()
                                          : Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.MaBenhNhan].ToString(), "");
                        bool gioitinh = !string.IsNullOrEmpty(drBN["GioiTinh"].ToString()) &&
                                        Convert.ToBoolean(drBN[TblHisLisPatientInfoVnio.Columns.GioiTinh]);
                        bool noitru = !string.IsNullOrEmpty(drBN["noiTru"].ToString()) &&
                                      Convert.ToBoolean(drBN[TblHisLisPatientInfoVnio.Columns.NoiTru]);

                        int dtt =InterfaceHelper.InsertPatientInfo(maBn,Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.TenBenhNhan].ToString(), ""),
                                                                   Utility.Int32Dbnull(drBN[TblHisLisPatientInfoVnio.Columns.Tuoi], 0), gioitinh,
                                                                   Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.DiaChi].ToString(),""),
                                                                   Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.Giuong].ToString(),""),
                                                                   Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.Khoa].ToString(), ""),
                                                                   Utility.Int32Dbnull(drBN[TblHisLisPatientInfoVnio.Columns.IdKhoa], -1),
                                                                   Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.Buong].ToString(),""),
                                                                   Utility.Int32Dbnull(drBN[TblHisLisPatientInfoVnio.Columns.IdBacSyDieuTri], -1),
                                                                   Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.BacSyDieuTri].ToString(), ""), noitru,
                                                                   Utility.sDbnull(drBN[TblHisLisPatientInfoVnio.Columns.Phong].ToString(),""),
                                                                   DateTime.Now, "", "0", dichvu,
                                                                   Utility.sDbnull(drBN["chanDoan"], ""),
                                                                   Utility.Int32Dbnull(drBN[TblHisLisPatientInfoVnio.Columns.IdDoiTuongBenhNhan].ToString(),-1));

                        foreach (object obj in arrTestTypeID)
                        {
                            //InsertIntoTestInfor
                            DataRow[] drv = _dtIdXn.Select("id_his_xn=" + obj);
                            _testTypeId = (int) drv[0]["TestType_ID"];
                            int testId = 0;
                            _tempBarcode = "";
                            InterfaceHelper.InsertTestInfor(_testTypeId, maBn,
                                                            Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyy")),
                                                            ref _tempBarcode, ref testId);


                            DataRow[] arrYcxn = tblYCXN.Select("id_his_xn=" + obj);

                            foreach (DataRow dr in arrYcxn)
                            {
                                //tblYeucauXetnghiem_VNIO 
                                if (!_reglist.Columns.Contains("Barcode")) _reglist.Columns.Add("Barcode", typeof (string));
                                if (!_reglist.Columns.Contains("TestType_ID"))
                                    _reglist.Columns.Add("TestType_ID", typeof (int));
                                dr[TblYeucauXetnghiemVnio.Columns.Barcode] = _tempBarcode;
                                dr[TblYeucauXetnghiemVnio.Columns.TestTypeId] = _testTypeId;
                                //Utility.GetValueFromGridColumn(grdList, "maBenhNhan",
                                //                                     grdList.CurrentRow.Index),
                                int tblyeucauxn =
                                    InterfaceHelper.InserttblYeucauXN(
                                        Utility.Int32Dbnull(dr[TblYeucauXetnghiemVnio.Columns.IdCanLamSangThucHien], -1),
                                        Utility.Int16Dbnull(dr[TblYeucauXetnghiemVnio.Columns.ThucHienCho], -1),
                                        Utility.Int32Dbnull(dr[TblYeucauXetnghiemVnio.Columns.TrangThaiThucHien], -1),
                                        Utility.Int16Dbnull(dr["yeuCauXetNghiems_Id"], -1),
                                        Utility.Int32Dbnull(dr[TblYeucauXetnghiemVnio.Columns.Id], -1), maBn,
                                        Utility.sDbnull(dr[TblYeucauXetnghiemVnio.Columns.Barcode].ToString(), ""),
                                        Utility.Int16Dbnull(dr[TblYeucauXetnghiemVnio.Columns.TestTypeId], -1),
                                        idHisXn + txtSoPhieu.Text + txtmabenhpham.Text,DateTime.Now,
                                        InterfaceHelper.GetIsTestName(dr[TblYeucauXetnghiemVnio.Columns.IdCanLamSangThucHien].ToString()), false);

                                DataTable dtthietbi = InterfaceHelper.GetAllDevices();
                                int deviceId = dr["DeviceId"].ToString().Trim() == "" ? -1 : Convert.ToInt32(dr["DeviceId"]);
                                string filterDeviceName = "Device_Name='AU480' and Device_ID=" + deviceId;
                                DataRow[] arrDevice = dtthietbi.Select(filterDeviceName);

                                // Kiểm tra tên thiết bị 2 chiều nếu = AU480 thì insert vào TReglist
                                foreach (DataRow dataRow in arrDevice)
                                {
                                    _deviceName = dataRow[DDeviceList.Columns.DeviceName].ToString();
                                    if (_deviceName == "AU480")
                                    {
                                        int insertRegList = InterfaceHelper.InsertRegList(testId, dr["AliasName"].ToString(),
                                                                                          _tempBarcode,
                                                                                          dr["ParaName"].
                                                                                              ToString(), true, deviceId);
                                    }
                                }
                                SetTextWarning(Success);
                            }
                        }
                    }
                if (!_lstBarcode.Contains(_tempBarcode))
                    _lstBarcode.Add(_tempBarcode);

                barcode1.Data = _tempBarcode;
                barcode2.Data = _tempBarcode;
                txtmabenhpham.Focus();
            }

            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void cboDeviceID_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataControlFromDeviceId(Utility.Int32Dbnull(cboDeviceID.SelectedValue, -1));
        }

        private void cmdClose2_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void cmdUpdateParaMapping_Click(object sender, EventArgs e)
        {
            int para = InterfaceHelper.InsertParaMapping(CreateParaMapping());
            LoadParaHisLis();
        }

        private void grdParamMapping_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteParaMapping();
                //LoadParaHisLis();
            }
        }

        private void warningtimer_Tick(object sender, EventArgs e)
        {
            warningtimer.Enabled = false;
            warningStatus.Text = string.Empty;
        }

        private void txtSoPhieu_TextChanged(object sender, EventArgs e)
        {
            cmdSavetoLIS.Enabled = false;
        }

        private void FrmHIS_LIS_VIENMAT_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
            }
        }

        private void cmdClose1_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void CmdGetDataOut_Click(object sender, EventArgs e)
        {
            //txtSoPhieu_out.Text = "06110629004";
            UseWaitCursor = true;
           
            try
            {
                _patienInfo.Clear();
                _reglist.Clear();
            }
            catch (Exception)
            {
            }
            try
            {
              
                if (txtSoPhieu_out.Text == "" && txtName.Text == "" && cbxTimChinhXacSoPhieu.Checked &&
                    cboGioitinh.SelectedIndex == 0)
                {
                    Utility.ShowMsg("Bạn phải nhập mã bệnh nhân hoặc tên đầy đủ bệnh nhân!", "Thông báo");
                }
                else
                {
                    int sex = Convert.ToInt32(GetControlPropertyThreadSafe(cboGioitinh, "SelectedValue"));
                    //circularProgress1.IsRunning = true;
                    _dsResultDetail = InterfaceHelper.GetResultDetailV(txtSoPhieu_out.Text, txtName.Text, sex,
                                                                       dtpFromdateTime.Value.Date,
                                                                       dtpTodatetime.Value.Date,
                                                                       cbxTimChinhXacSoPhieu.Checked,
                                                                       Utility.Int32Dbnull(cboTestType.SelectedValue, -1),
                                                                       cboHasTest.SelectedIndex - 1);
                  
                  
                    _dtPatientOut = _dsResultDetail.Tables[0];
                    //  m_dttblYeucauxn = dsResultDetail.Tables[1]; 
                    _mDtTestInfo = _dsResultDetail.Tables[1];
                    _mDtResultDetail = _dsResultDetail.Tables[2];
                    //string deviceId2 = null;
                    //foreach (DataRow drv in m_dtResultDetail.Rows)//
                    
                    _mDtResultDetail = _mDtResultDetail.Select("Printstatus=1").CopyToDataTable();
                    Utility.ResetProgressBar(processbar, _dtPatientOut.Rows.Count, true);
                   
                    //DataTable dtResult2 = m_dtResultDetail.Select("Device_ID=" + deviceId2 + "or Device_ID<>null").CopyToDataTable();
                    Utility.SetDataSourceForDataGridView(grdlist_out, _dtPatientOut, false, true, "", "");
                    Utility.SetDataSourceForDataGridView(grdRegList_out, _mDtTestInfo, false, true, "", "");
                    Utility.SetDataSourceForDataGridView(grdResultDetail, _mDtResultDetail, false, true, "", "");
                 
                    try
                    {
                        //foreach (DataRow dr in _dtPatientOut.Rows)
                        foreach (DataGridViewRow dvr in grdlist_out.Rows)
                        {

                            //DataRow[] pid = _dtPatientOut.Select("CHOn= 1 and maBenhNhan='" + dvr["maBenhNhan"] + "'");
                            //string mbn = Utility.sDbnull(dvr["maBenhNhan"].ToString());
                            processbar.Value += 1;
                            string mbn = dvr.Cells["colmaBenhNhan"].Value.ToString();
                            GotoNewRow(grdRegList, "maBenhNhan", mbn);
                            //DataTable dulieuTest2 = _mDtTestInfo.Select("maBenhNhan='" + mbn + "'").CopyToDataTable();
                            int count = -1;
                            count = _mDtResultDetail.Select("maBenhNhan='" + mbn + "'" + "and TestType_ID=" +
                                                     Utility.Int32Dbnull(cboTestType.SelectedValue, -1)).Count();
                            //DataTable dtTest = _mDtResultDetail.Select("maBenhNhan='" + mbn + "'" + "and TestType_ID=" + Utility.Int32Dbnull(cboTestType.SelectedValue, -1)).CopyToDataTable();


                            if (count > 0)
                            {
                                dvr.DefaultCellStyle.BackColor = Color.Empty;
                            }

                            else if (count <= 0)
                            {
                                dvr.DefaultCellStyle.BackColor = Color.LightGreen;
                            }
                            //count = -1;


                        }
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                   // Utility.ResetProgressBar(processbar, _dtPatientOut.Rows.Count, false);
                  
                    Utility.ResetProgressBar(processbar, _dtPatientOut.Rows.Count, false);
                  //  grdlist_out_SelectionChanged(grdlist_out, new EventArgs());
                    // grdRegList_out_SelectionChanged(grdRegList_out,new EventArgs());
                    
                }
                ChangeColorVnio();
               // ChangeColor();
            }

            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
            finally
            {
                UseWaitCursor = false;
            }
            if (grdlist_out.RowCount <= 0)
            {
                Utility.ShowMsg("Không có bệnh nhân nào!", "Thông báo");
            }
        }

        void ChangeColorVnio()
        {
            try
            {
                //foreach (DataRow dr in _dtPatientOut.Rows)
                foreach (DataGridViewRow dvr in grdlist_out.Rows)
                {
                       string mbn = dvr.Cells["colmaBenhNhan"].Value.ToString();
                    GotoNewRow(grdRegList, "maBenhNhan", mbn);
                    //DataTable dulieuTest2 = _mDtTestInfo.Select("maBenhNhan='" + mbn + "'").CopyToDataTable();
                    DataRow[] count;
                    //_dsRegtest = InterfaceHelper.GetRegTest(mbn, Utility.Int32Dbnull(cboTestTypes.SelectedValue, -1),
                    //                                       dtpFromdateTime.Value.Date);
                    //_dtYeucauXn = _dsRegtest.Tables[0];

                    DataRow[] dr = _mDtTestInfo.Select("maBenhNhan='" + mbn + "'" + "and SendStatus=1");
                   
                    if (dr.GetLength(0) > 0)
                    {
                        dvr.DefaultCellStyle.BackColor = Color.Yellow;
                    }


                    //else if(dr.GetLength(0) <= 0)
                    //{
                    //    dvr.DefaultCellStyle.BackColor = Color.Empty;
                    //}
                  


                }
            }
            catch (Exception)
            {

                //throw;
            }
        }

        private void cmdSaveToMed_Click(object sender, EventArgs e)
        {
            // string content = SendData();
            //XElement newElement = GetResponse("", content);
            SendData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateParaMaping();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            warningBox1.Text = string.Empty;
            circularProgress1.Value += 2;

        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            //_patienInfo.Clear();
            //try
            //{
            //    rowfilterpatien = "MaBenhNhan='" +
            //                         Utility.sDbnull(
            //                             Utility.GetValueFromGridColumn(grdList, "colmabn", grdList.CurrentRow.Index), -1) + "'";


            //    _patienInfo.DefaultView.RowFilter = rowfilterpatien;
            //    //_patienInfo.AcceptChanges();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void timersendHis_Tick(object sender, EventArgs e)
        {
            timersendHis.Enabled = false;
            warningsendHis.Text = string.Empty;
        }

        private void grdlist_out_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdlist_out.CurrentRow != null)
                {
                    _rowFilter1 = "maBenhnhan='" + Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdlist_out, TblHisLisPatientInfoVnio.Columns.MaBenhNhan,
                                                       grdlist_out.CurrentRow.Index), -1) + "'";
                }
                _mDtResultDetail.DefaultView.RowFilter = _rowFilter1;
                _mDtResultDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
            finally
            {
                ChangeColorVnio();
                //ChangeColor();
            }
        }

        private void cboTestType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadTestType(Utility.Int32Dbnull(cboTestType.SelectedValue, -1));
            //LoadTestType(_testTypeId);
        }

        private void grdRegList_out_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grdRegList_out.CurrentRow != null)
                {
                    _currTestTypeId = Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdRegList_out, "colTest_ID",
                                                       grdRegList_out.CurrentRow.Index), -1);
                    _rowFilter = "Test_ID=" + _currTestTypeId;
                }
                _mDtResultDetail.DefaultView.RowFilter = _rowFilter;
                _mDtResultDetail.AcceptChanges();
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private void txtmabenhpham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLayThongTin.Focus();
            }
        }

        private void btnInBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Debugger.IsAttached) _tempBarcode = "130527011235";
                if ((grdRegList.CurrentRow != null) || (!string.IsNullOrEmpty(InterfaceHelper.MyProperties.BarcodeData.Trim())))
                {
                    if (_tempBarcode != null)
                    {
                        barcode1.Data = _tempBarcode;
                        barcode2.Data = _tempBarcode;
                    }
                    else
                    {
                        barcode1.Data = "0000000000";
                    }

                    // In barcode
                    // Xử lý trường hợp in barcode 4 số
                    if(InterfaceHelper.MyProperties.UseShortBarcode)
                    {
                        if (!InterfaceHelper.MyProperties.UseOldPrintMethod)
                        {
                            int count = _lstBarcode.Count();
                            var dt = new DataTable("Barcode");
                            dt.Columns.Add("Barcode1", typeof (byte[]));
                            dt.Columns.Add("Barcode2", typeof (byte[]));
                            var dr = dt.NewRow();
                            if (string.IsNullOrEmpty(InterfaceHelper.MyProperties.BarcodeData.Trim()))
                            {
                                if (count%2 == 0)
                                {
                                    dr["Barcode1"] =
                                        BarcodeInfo.GetByteDataOfBarcode(
                                            _lstBarcode[0].Substring(_lstBarcode[0].Length - 4));
                                    dr["Barcode2"] =
                                        BarcodeInfo.GetByteDataOfBarcode(
                                            _lstBarcode[1].Substring(_lstBarcode[0].Length - 4));
                                }
                                else
                                {
                                    dr["Barcode1"] =
                                        BarcodeInfo.GetByteDataOfBarcode(
                                            _lstBarcode[0].Substring(_lstBarcode[0].Length - 4));
                                    dr["Barcode2"] =
                                        BarcodeInfo.GetByteDataOfBarcode(
                                            _lstBarcode[0].Substring(_lstBarcode[0].Length - 4));
                                }
                            }
                            else
                            {
                                dr["Barcode1"] =
                                    BarcodeInfo.GetByteDataOfBarcode(InterfaceHelper.MyProperties.BarcodeData);
                                dr["Barcode2"] =
                                    BarcodeInfo.GetByteDataOfBarcode(InterfaceHelper.MyProperties.BarcodeData);
                            }
                            dt.Rows.Add(dr);

                            ReportDocument crpt = new Barcode_VNIO();
                            crpt.PrintOptions.PaperOrientation = PaperOrientation.Portrait;
                            crpt.SetDataSource(dt);

                            PrinterSettings printerSettings = new PrinterSettings();
                            PrintDialog printDialog = new PrintDialog();
                            printDialog.PrinterSettings = printerSettings;
                            printDialog.AllowPrintToFile = false;
                            printDialog.AllowSomePages = true;
                            printDialog.UseEXDialog = true;

                            DialogResult result = printDialog.ShowDialog();

                            if (result == DialogResult.Cancel)
                            {
                                return;
                            }

                            crpt.PrintOptions.PrinterName = printerSettings.PrinterName;
                            crpt.PrintToPrinter(printerSettings.Copies, false, 0, 0);
                            _lstBarcode.Clear();
                        }
                            // Xử lý in theo barcode kiểu cũ.
                        else
                        {
                            int count = _lstBarcode.Count();
                            
                            if (string.IsNullOrEmpty(InterfaceHelper.MyProperties.BarcodeData.Trim()))
                            {
                                oldBarcode1.Data= _lstBarcode[0].Substring(_lstBarcode[0].Length - 4);
                                oldBarcode2.Data = count % 2 == 0 ? _lstBarcode[1].Substring(_lstBarcode[0].Length - 4) : oldBarcode1.Data;
                            }
                            else
                            {
                                oldBarcode1.Data = InterfaceHelper.MyProperties.BarcodeData;
                                oldBarcode2.Data = oldBarcode1.Data;
                            }

                            if (printDialog1.ShowDialog() == DialogResult.OK)
                            {
                                //pd.PrinterSettings.PrinterName = "doPDF v7";
                                pd.PrinterSettings.PrinterName = printDialog1.PrinterSettings.PrinterName;
                                //pd.PrinterSettings = printDialog1.PrinterSettings;
                                //pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Barcode",
                                //                                 Convert.ToInt32(
                                //                                     pd.DefaultPageSettings.PrintableArea.Width),
                                //                                 Convert.ToInt32(
                                //                                     pd.DefaultPageSettings.PrintableArea.Height));
                                //pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Barcode", 284, 57);
                                pd.DefaultPageSettings.Margins = margins;

                                pd.PrintController = new StandardPrintController();

                                PrintAreaHeight = Convert.ToInt32(BarcodeConfig[2]);
                                PrintAreaWidth = Convert.ToInt32(BarcodeConfig[1]);

                                // initializing local variables to hold margin values that            will(serve)
                                // as the X and Y coordinates for the upper left corner of            the(Printing)
                                // area rectangle.
                                marginLeft = pd.DefaultPageSettings.Margins.Left;
                                marginTop = pd.DefaultPageSettings.Margins.Top;
                                // X and Y coordinate
                                if (pd.DefaultPageSettings.Landscape)
                                {
                                    Int32 intTemp = default(Int32);
                                    intTemp = PrintAreaHeight;
                                    PrintAreaHeight = PrintAreaWidth;
                                    PrintAreaWidth = intTemp;
                                    // if the user selects landscape mode, swap the printing  Area(Height And Width)
                                }

                                pd.PrintPage += PrintPageHandler;
                                pd.Print();
                                pd.PrintPage -= PrintPageHandler;
                                _lstBarcode.Clear();
                            }
                        }
                    }
                    else if (printDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printDocument1.DefaultPageSettings.Margins = _margins;
                        printDocument1.Print();
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            //Me.Barcode1.Font = New System.Drawing.Font("Times New Roman", (8.25! / (zoom Mod 5)), System.Drawing.FontStyle.Bold)
            try
            {
                marginLeft = margins.Left;
                int i = 0;

                for (i = 1; i <= 4; i++)
                {
                    if ((i == 1) || (i == 2))
                    {
                        e.Graphics.DrawImage(oldBarcode1.Image(500, 500), marginLeft, marginTop, PrintAreaWidth,
                                         PrintAreaHeight);
                    }
                    else if ((i == 3) || (i == 4))
                    {
                        e.Graphics.DrawImage(oldBarcode2.Image(500, 500), marginLeft, marginTop, PrintAreaWidth,
                                         PrintAreaHeight);
                    }
                    //e.Graphics.DrawImage(oldBarcode1.Image(500, 500), marginLeft, marginTop, PrintAreaWidth,
                    //                     PrintAreaHeight);
                    
                    marginLeft = marginLeft + pd.DefaultPageSettings.PaperSize.Width / 4 + marginleft2;
                }
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //Me.Barcode1.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Bold)
        }


        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Int32 printAreaHeight;
                Int32 printAreaWidth = default(Int32);
                Int32 marginLeft = default(Int32);
                Int32 marginTop = default(Int32);
                Int32 marginMiddle = default(Int32);
                PageSettings _with1 = printDocument1.DefaultPageSettings;

                printAreaHeight = Convert.ToInt32(_barcodeConfig[1]);
                printAreaWidth = Convert.ToInt32(_barcodeConfig[0]);
                marginLeft = Convert.ToInt32(_barcodeConfig[2]);
                marginTop = Convert.ToInt32(_barcodeConfig[3]);
                //marginMiddle = Convert.ToInt32(_barcodeConfig[4]);

                if (printDocument1.DefaultPageSettings.Landscape)
                {
                    Int32 intTemp = default(Int32);
                    intTemp = printAreaHeight;
                    printAreaHeight = printAreaWidth;
                    printAreaWidth = intTemp;
                }

                Image stockImage1 = default(Image);
                Image stockImage2 = default(Image);
                //Image stockImage3 = default(Image);
                int _count = 0;
                _count = _lstBarcode.Count();
                if (_count%2 == 0)
                {
                    barcode2.Data = _lstBarcode[_count - 1];
                    stockImage1 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                    barcode2.Data = _lstBarcode[_count - 2];
                    stockImage2 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                }
                else
                {
                    // barcode2.Text = lstBarcode[_count - 1].Substring(6, 4);
                    //barcode2.Data = lstBarcode[_count - 1];
                    barcode2.Data = _tempBarcode;
                    stockImage1 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                    stockImage2 = barcode2.Image(printAreaWidth*10, printAreaHeight*10);
                    //stockImage3 = barcode1.Image(printAreaWidth * 10, printAreaHeight * 10);
                }
                //giải phóng bộ nhớ khỏi listbarcode
                _lstBarcode.Clear();

                #region Barcode Nhỏ

                //marginLeft += printDocument1.DefaultPageSettings.PaperSize.Width/Col;
                //// stockImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                //e.Graphics.DrawImage(stockImage1, 40, marginTop, printAreaWidth, printAreaHeight);
                //// marginLeft = marginLeft + printAreaWidth+300 ;//+ marginMiddle;
                //marginLeft = marginLeft + printAreaWidth; //+ 300;//+ marginMiddle;
                //// e.Graphics.DrawImage(stockImage2, marginLeft-600, marginTop, printAreaWidth, printAreaHeight);
                //e.Graphics.DrawImage(stockImage2, marginLeft - 140, marginTop, printAreaWidth, printAreaHeight);

                #endregion

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
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private void chkDaochonPatient_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView dr in _dtPatientOut.DefaultView)
                {
                    dr["CHON"] = dr["CHON"].ToString() == "0" ? 1 : 0;
                }

                _dtPatientOut.AcceptChanges();
                //BoimauGrid();
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
            finally
            {
               // ChangeColorVnio();
               ChangeColor();
            }
        }

        private void chkAllpatient_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRowView dr in _dtPatientOut.DefaultView)
                {
                    dr["CHON"] = chkAllpatient.Checked ? 1 : 0;
                }

                
                _dtPatientOut.AcceptChanges();
                //BoimauGrid();
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
            finally
            {
                //ChangeColorVnio();
                ChangeColor();
            }
        }

        #endregion

        #region Private Method

        private static bool NullOrDbNullValue(object obj)
        {
            return obj == "" || obj.Equals(DBNull.Value);
        }

        internal void BoimauGrid()
        {
            foreach (DataGridViewRow drv in grdlist_out.Rows)
            {
                if (drv.Cells["colsSendStatus"].Value.ToString() == "1")
                {
                    drv.DefaultCellStyle.BackColor = Color.Yellow;
                }

                else
                {
                    drv.DefaultCellStyle.BackColor = Color.Empty;
                }
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

        private static string GetPid()
        {
            DateTime now = DateAndTime.Now;
            return now.Year + VB6.Right("0" + now.Month, 2) +
                   VB6.Right("0" + now.Day, 2) + VB6.Right("0" + now.Hour, 2) +
                   VB6.Right("0" + now.Minute, 2) + VB6.Right("0" + now.Second, 2);
        }

        private void CopyTable(DataSet dsSource, DataSet dsDes)
        {
            try
            {
                foreach (DataTable dt in dsSource.Tables)
                {
                    if (!dsDes.Tables.Contains(dt.TableName))
                    {
                        dsDes.Tables.Add(dt.Copy());
                    }
                    else
                    {
                        foreach (DataRow dr in dsSource.Tables[dt.TableName].Rows)
                        {
                            dsDes.Tables[dt.TableName].ImportRow(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private void ChangeColor()
        {
            try
            {
                foreach (DataGridViewRow dvr in grdlist_out.Rows)
                {
                    int lastStatus = 0;

                    string mbn = dvr.Cells["colmaBenhNhan"].Value.ToString();
                    _dsRegtest = InterfaceHelper.GetRegTest(mbn, Utility.Int32Dbnull(cboTestTypes.SelectedValue, -1),
                                                            dtpFromdateTime.Value.Date);
                    _dtYeucauXn = _dsRegtest.Tables[0];

                    int tatCa = _dtYeucauXn.Rows.Count;
                    int daIn = _dtYeucauXn.Select("SendStatus=1").Length;
                    int chuaIn = _dtYeucauXn.Select("SendStatus=0").Length;
                   // int chuakq = _dtYeucauXn.Select("SendStatus=0 and IsTestName=0").Length;
                    if (daIn == tatCa) lastStatus = 1;
                    if (chuaIn == tatCa) lastStatus = 0;

                    if (daIn > 0 && daIn == tatCa) lastStatus = 1;
                    //if (chuakq == tatCa) lastStatus = 2;
                    if (lastStatus == 1)
                    {
                        dvr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (lastStatus == 0)
                    {
                        dvr.DefaultCellStyle.BackColor = Color.Empty;
                    }
                    //if (lastStatus == 2)
                    //{
                    //    dvr.DefaultCellStyle.BackColor = Color.LightGreen;
                    //}
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private void FillSexCombobox()
        {
            cboGioitinh.DataSource = null;
            cboGioitinh.Items.Clear();

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

            cboGioitinh.DataSource = dt;
            cboGioitinh.DisplayMember = "DisplayItem";
            cboGioitinh.ValueMember = "ValueItem";

            cboGioitinh.SelectedIndex = 0;
        }

        private void LoadTestTypeAlias()
        {
            try
            {
                _dtIdXn = InterfaceHelper.GetIdHis();
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        /// <summary>
        /// Gửi kết quả đến HIS NETNAM
        /// </summary>
        private void SendData()
        {
            int testTypeId = -1;
            try
            {
                if (grdRegList_out != null)
                {
                    testTypeId = Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdRegList_out, "colTestType_ID",
                                                       grdRegList_out.CurrentRow.Index), -1);
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
            try

            {
                foreach (DataRow dataRow in _dtPatientOut.Rows)
                {
                    if (dataRow["CHON"].ToString() == "1")
                    {
                        DataRow[] pid = _dtPatientOut.Select("CHOn= 1 and maBenhNhan='" + dataRow["maBenhNhan"] + "'");
                        string mbn = "";
                        if (pid.Length > 0)
                        {
                            mbn = Utility.sDbnull(pid[0]["maBenhNhan"].ToString());
                        }
                        GotoNewRow(grdRegList, "maBenhNhan", mbn);
                        DataTable dulieuTest = _mDtTestInfo.Select("maBenhNhan='" + mbn + "'").CopyToDataTable();

                        string testId = "";

                        foreach (DataRow drtestinfor in dulieuTest.Rows)
                        {
                            deviceID = drtestinfor["Device_ID"].ToString();
                            testId += drtestinfor["Test_ID"] + ",";
                        }
                        try
                        {
                            if (testId.Trim() != "")
                            {
                                testId = testId.Substring(0, testId.Length - 1);
                                if (_mDtResultDetail.Select("test_id =" + testId).Count() > 0)
                                {
                                    DataTable duLieuCanGui =
                                        _mDtResultDetail.Select(" test_id in " + "(" + testId + ")").
                                            CopyToDataTable();

                                    //DataTable dtResult = DuLieuCanGui.Select("Device_ID is not null").CopyToDataTable();

                                    var arrId = new ArrayList();
                                    foreach (DataRow dr in duLieuCanGui.Rows)
                                    {
                                        if (!arrId.Contains(Convert.ToInt32(dr["id"])))
                                            arrId.Add(Convert.ToInt32(dr["id"]));
                                    }

                                    foreach (DataRow dr in duLieuCanGui.Select("mabenhnhan='" + mbn + "'"))
                                    {
                                        int updatestatus =
                                            InterfaceHelper.UpdateStatusHis(
                                                Utility.Int32Dbnull(dr[TblYeucauXetnghiemVnio.Columns.Id]), mbn,
                                                Convert.ToDateTime(dr[TblYeucauXetnghiemVnio.Columns.TestDate]), true);
                                    }


                                    string tagName = "";
                                    Int16 displayParamLevel = 1;
                                    DataRow[] arrTestType = _dtIdXn.Select("TESTTYPE_ID=" + testTypeId);
                                    if (arrTestType.Length > 0)
                                    {
                                        tagName = Utility.sDbnull(arrTestType[0]["TAG_NAME"].ToString(), "");
                                        displayParamLevel =
                                            Utility.Int16Dbnull(arrTestType[0]["Display_ParamLevel"].ToString(), 1);
                                    }

                                    foreach (int _id in arrId)
                                    {
                                        #region Đông máu cơ bản

                                        var xmlDoc2 = new XmlDocument();
                                        XmlElement rootNode22 = xmlDoc2.CreateElement("thongTinKetQua");
                                        XmlElement rootNode23 = xmlDoc2.CreateElement("ketQuaXetNghiem");
                                        rootNode22.AppendChild(rootNode23);
                                        xmlDoc2.AppendChild(rootNode22);

                                        #endregion

                                        #region Tagchung

                                        var xmlDoc = new XmlDocument();
                                        DataTable duLieutheoId = duLieuCanGui.Select(" id=" + _id).CopyToDataTable();
                                        string strname = "ketQuaXetNghiem";
                                        XmlElement rootNode = xmlDoc.CreateElement("thongTinKetQua");
                                        XmlElement rootNode1;
                                        XmlElement rootNode2;
                                        xmlDoc.AppendChild(rootNode);

                                        #endregion

                                        bool hasData = false;

                                        if (displayParamLevel == 1)
                                        {
                                            rootNode1 = xmlDoc.CreateElement(tagName);
                                            rootNode.AppendChild(rootNode1);
                                        }
                                        else
                                            rootNode1 = rootNode;

                                        var arrMauChay = new Hashtable();
                                        string[] name = {
                                                            "ptinr", "apttthoigian", "appttratio", "fibrinogen",
                                                            "ptphantram",
                                                            "pthoitgian"
                                                        };
                                        foreach (DataRowView drv in duLieutheoId.DefaultView)
                                        {
                                            //bỏ dấu tên xét nghiệm
                                            string d;
                                            if (displayParamLevel == 1)
                                                strname = Bodau(GetMedParamCode(drv["Para_Name"].ToString()));
                                            if (strname != null && strname.Trim() != "")
                                            {
                                                if (name.Contains(strname.ToLower()))
                                                {
                                                    arrMauChay.Add(strname,
                                                                   StrDbNull(drv["Result"]) + " " +
                                                                   StrDbNull(drv["Measure_Unit"]));
                                                }
                                                else
                                                {
                                                    hasData = true;
                                                    XmlNode nameNode = xmlDoc.CreateElement(strname);
                                                    nameNode.InnerText =
                                                        (AmtinhDuongTinh(StrDbNull(drv["Result"])) + " " +
                                                         StrDbNull(drv["Measure_Unit"])).
                                                            Trim();
                                                    rootNode1.AppendChild(nameNode);
                                                }
                                            }
                                        }
                                        //Đông máu cơ bản
                                        if (arrMauChay.Count > 0)
                                        {
                                            string strValue = arrMauChay.Keys.Cast<string>().Aggregate("",
                                                                                                       (current, skey)
                                                                                                       =>
                                                                                                       current +
                                                                                                       (skey + "=" +
                                                                                                        "\"" +
                                                                                                        arrMauChay[skey] +
                                                                                                        "\"" + ";"));
                                            if (strValue.Trim() != "")
                                            {
                                                rootNode23.InnerText = strValue.Substring(0, strValue.Length - 1);
                                            }
                                        }
                                        if (arrMauChay.Count > 0)
                                        {
                                            XElement newElementMauChay = GetResponse(_id, xmlDoc2.InnerXml);
                                            SetTextWarning(SenddataHis);
                                            File.AppendAllText(@"C:/SendDataToHis2.txt", xmlDoc2.InnerXml);
                                        }
                                        if (hasData)
                                        {
                                            XElement newElement = GetResponse(_id, xmlDoc.InnerXml);
                                            SetTextWarning(SenddataHis);
                                            File.AppendAllText(@"C:/SendDataToHis.txt", xmlDoc.InnerXml);
                                        }
                                    }
                                }
                                else
                                {
                                    SetTextWarning("Bệnh nhân chưa có kết quả");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            SetTextWarning(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
            finally
            {
                ChangeColor();
            }
        }

        public static void GotoNewRow(DataGridView grdList, string columnName, string value)
        {
            for (int i = 0; i <= grdList.RowCount - 1; i++)
            {
                if (Utility.sDbnull(grdList[columnName, i].Value).Trim() == value.Trim())
                {
                    foreach (DataGridViewColumn grdCol in grdList.Columns)
                    {
                        if (grdCol.Visible)
                        {
                            grdList.CurrentCell = grdList[grdCol.Name, i];
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Lấy mã xét nghiệm LIS để ánh xạ tới HIS khi gửi
        /// </summary>
        /// <param name="lisParamcode"></param>
        /// <returns></returns>
        private string GetMedParamCode(string lisParamcode)
        {
            try
            {
                DataRow[] arrDr =
                    _paramMapping.Select("LIS_PARANAME='" + lisParamcode +
                                         "' AND (MED_PARAMCODE<>' ' OR MED_PARAMCODE<>'' OR MED_PARAMCODE<>null)");
                if (arrDr.Length > 0)
                    return arrDr[0]["MED_PARAMCODE"].ToString();
                return lisParamcode;
            }
            catch (Exception)
            {
                return lisParamcode;
            }
        }

        //Load thông tin XN từ loại thiết bị XN 
        private void LoadDataControlFromDeviceId(int deviceId)
        {
            DataTable dttable = InterfaceHelper.LoadParaname(deviceId);
            cboLisParam.DataSource = dttable;
            cboLisParam.DisplayMember = DDataControl.Columns.DataName;
            cboLisParam.ValueMember = DDataControl.Columns.DataControlId;
        }

        /// <summary>
        /// Update chức năng Paramapping
        /// </summary>
        public void UpdateParaMaping()
        {
            try
            {
                TblParamMapping pitems;
                string medPara_id = "", medParaname = "", lisParaname = "", DevicesId = "", MedParacode = "";
                bool isTestName = false;
                var dataTable = grdParamMapping.DataSource as DataTable;
                for (int i = 0; i < _paramMapping.Rows.Count; i++)
                {
                    DataRow dr = _paramMapping.Rows[i];
                    object c = dr[TblParamMapping.Columns.MedParaName];
                    //Kiểm tra trạng thái của dòng:
                    //if ((dr.RowState == DataRowState.Modified) || (dr.RowState == DataRowState.Added))
                    //{
                    //    Name = dr[TblParamMapping.Columns.MedParaName].ToString();

                    //    if ((Name == ""))
                    //    {
                    //        //SetTextForWarning(MsgEror);
                    //        continue;
                    //    }
                    //}
                    if (dr.RowState == DataRowState.Modified)
                    {
                        pitems = new TblParamMapping(dr[TblParamMapping.Columns.Id]);
                        pitems.MedParamID = dr[TblParamMapping.Columns.MedParamID].ToString();
                        pitems.MedParaName = dr[TblParamMapping.Columns.MedParaName].ToString();
                        ;
                        pitems.MedParamCode = dr[TblParamMapping.Columns.MedParamCode].ToString();
                        ;
                        pitems.LisParaName = dr[TblParamMapping.Columns.LisParaName].ToString();
                        ;
                        pitems.IsTestName = Convert.ToBoolean(dr[TblParamMapping.Columns.IsTestName].ToString());
                        pitems.DeviceId = Utility.Int32Dbnull(dr[TblParamMapping.Columns.DeviceId].ToString());
                        InterfaceHelper.UpdateParaMaping(pitems);
                        SetTextWarning(Success);
                    }
                }
                _paramMapping.AcceptChanges();
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        //Load Các thông tin XN mapping HIS-LIS
        private void LoadParaHisLis()
        {
            _paramMapping = InterfaceHelper.LoadParaMappingHisLis();
            grdParamMapping.DataSource = _paramMapping;
        }

        /// <summary>
        /// Xóa mã Paramapping in db
        /// </summary>
        private void DeleteParaMapping()
        {
            try
            {
                DataGridViewRow currentRow = grdParamMapping.CurrentRow;

                if (currentRow != null)
                {
                    string pid = UI.GetCellValue(currentRow.Cells["id_"]);
                    if (pid != "")
                    {
                        // int pid = Convert.ToInt32(.Value);
                        if (
                            MessageBox.Show("Bạn có muốn xóa ?", "Thông báo", MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question) ==
                            DialogResult.Yes)
                        {
                            InterfaceHelper.DeleteParaName(Convert.ToInt32(pid));
                            DataRow[] arrDr = _paramMapping.Select("ID=" + pid);
                            if (arrDr.Length > 0)
                            {
                                _paramMapping.Rows.Remove(arrDr[0]);
                                SetTextWarning(Delete);
                            }
                            _paramMapping.AcceptChanges();
                        }
                    }
                    else
                    {
                        SetTextWarning(Error);
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
            finally
            {
                (grdParamMapping.DataSource as DataTable).AcceptChanges();
            }
        }

        /// <summary>
        /// Hàm đọc file XML từ HIS
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static DataTable GetHisParamFromXml(string fileName)
        {
            try
            {
                var theReader = new StringReader(File.ReadAllText(fileName));
                var dataSet = new DataSet();
                dataSet.ReadXml(theReader);
                return dataSet.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Không tìm thấy file {0}", fileName));
                return null;
            }
        }

        /// <summary>
        /// Load all Devices
        /// </summary>
        private void LoadAllDevices()
        {
            _dtAlldevice = InterfaceHelper.GetAllDevices();
            cboDeviceID.DataSource = _dtAlldevice;
            cboDeviceID.DisplayMember = DDeviceList.Columns.DeviceName;
            cboDeviceID.ValueMember = DDeviceList.Columns.DeviceId;
        }

        private void LoadTestType()
        {
            DataBinding.BindData(cboTestType, _dtIdXn, "TestType_Id", "Localalias");
            cboTestType.SelectedIndex = Utility.GetSelectedIndex(cboTestType, GetTestTypeId().ToString());
        }

        private void LoadTestTypeforSeach()
        {
            DataBinding.BindData(cboTestTypes, _dtIdXn, "TestType_Id", "Localalias");
            cboTestTypes.SelectedIndex = Utility.GetSelectedIndex(cboTestTypes, GetTestTypeId().ToString());
        }

        /// <summary>
        /// Load paramHis từ file XML
        /// </summary>
        private void LoadHisParam()
        {
            try
            {
                _hisParam = GetHisParamFromXml(@"hispara_netnam.xml");
                cboMedParam.DataSource = _hisParam;
                cboMedParam.DisplayMember = "ten_loai_can_lam_sang";
                cboMedParam.ValueMember = "id";
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private TblParamMapping CreateParaMapping()
        {
            var objParaMapping = new TblParamMapping
                                     {
                                         DeviceId = Utility.Int32Dbnull(cboDeviceID.SelectedValue, -1),
                                         MedParamID = Utility.sDbnull(cboMedParam.SelectedValue, ""),
                                         MedParaName = Utility.sDbnull(cboMedParam.Text, ""),
                                         LisParaName = Utility.sDbnull(cboLisParam.Text, ""),
                                         IsTestName = Convert.ToBoolean(cboTestName.Text)
                                     };
            return objParaMapping;
        }

        /// <summary>
        /// Hàm lấy về TestTypeId từ localAlias
        /// </summary>
        /// <returns>Nếu có lỗi trả về -1</returns>
        private static int GetTestTypeId()
        {
            int result = -1;
            const string filename = "Config.xml";
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
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        private static int GetTestTypeIdDm()
        {
            int result = -1;
            const string filename = "ConfigDM.xml";
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
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        private static int GetTestTypeIdMd()
        {
            int result = -1;
            const string filename = "ConfigMD.xml";
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
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        /// <summary>
        /// Kiểm tra định tính load từ file text
        /// </summary>
        private void ThongTinDinhTinh()
        {
            try
            {
                if (File.Exists("DINH_TINH.txt"))
                {
                    var reader = new StreamReader("DINH_TINH.txt");
                    object obj = reader.ReadLine();
                    if (obj != null)
                    {
                        _dinhTinh = obj.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        private string AmtinhDuongTinh(string obj)
        {
            try
            {
                //Mảng này chứa 2 phần tử mặc định Âm tính nhập trước sau đó là Dương tính phía sau
                //DINH_TINH="Âm tính;Dương Tính";
                string[] arrDT =
                    _dinhTinh.Split(';');
                if (Bodau(arrDT[0]).ToLower().Contains(Bodau(obj).ToLower())) return "0";
                if (Bodau(arrDT[1]).ToLower().Contains(Bodau(obj).ToLower())) return "1";
                return obj;
            }
            catch (Exception)
            {
                return obj;
            }
        }

        private void IdCanLamSangThucHien()
        {
            try
            {
                const string idCanlamsangText = "IdCanLamSangThucHien.txt";
                if (File.Exists(idCanlamsangText))
                {
                    _idCanlamSangThucHien = File.ReadAllLines(idCanlamsangText);
                }
                else
                {
                    _idCanlamSangThucHien = new string[7];
                    _idCanlamSangThucHien[0] = "58";
                    _idCanlamSangThucHien[1] = "48";
                    _idCanlamSangThucHien[2] = "47";
                    _idCanlamSangThucHien[3] = "46";
                    _idCanlamSangThucHien[4] = "49";
                    _idCanlamSangThucHien[5] = "49";
                    _idCanlamSangThucHien[6] = "49";
                }
            }
            catch (Exception ex)
            {
                SetTextWarning(ex.ToString());
            }
        }

        /// <summary>
        /// Load thông tin  BN lên lưới
        /// </summary>
        private void GrdThongTinBn()
        {
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.MaBenhNhan,
                                                        "Mã BN",
                                                        TblHisLisPatientInfoVnio.Columns.MaBenhNhan, null, true));

            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.TenBenhNhan,
                                                        "Tên BN",
                                                        TblHisLisPatientInfoVnio.Columns.TenBenhNhan, null, true));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Tuoi,
                                                        "Tuổi",
                                                        TblHisLisPatientInfoVnio.Columns.Tuoi));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.GioiTinh,
                                                        "Giới Tính",
                                                        TblHisLisPatientInfoVnio.Columns.GioiTinh));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.DiaChi,
                                                        "Địa Chỉ",
                                                        TblHisLisPatientInfoVnio.Columns.DiaChi));
            grdList.Columns.Add(UI.CreateGridTextColumn("chanDoan",
                                                        "Chẩn đoán",
                                                        "chanDoan", null, true));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Phong,
                                                        "Phòng",
                                                        TblHisLisPatientInfoVnio.Columns.Phong));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Khoa,
                                                        "Khoa",
                                                        TblHisLisPatientInfoVnio.Columns.Khoa));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Buong,
                                                        "Buồng",
                                                        TblHisLisPatientInfoVnio.Columns.Buong));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Giuong,
                                                        "Giường",
                                                        TblHisLisPatientInfoVnio.Columns.Giuong));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.IdKhoa,
                                                        "Id Khoa",
                                                        TblHisLisPatientInfoVnio.Columns.IdKhoa));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.BacSyDieuTri,
                                                        "Bác Sỹ DT",
                                                        TblHisLisPatientInfoVnio.Columns.BacSyDieuTri));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.IdBacSyDieuTri,
                                                        "Mã Bác Sỹ",
                                                        TblHisLisPatientInfoVnio.Columns.IdBacSyDieuTri, null, false));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.NoiTru,
                                                        "Nội Trú",
                                                        TblHisLisPatientInfoVnio.Columns.NoiTru));
            grdList.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.IdDoiTuongBenhNhan,
                                                        "Mã Đối tượng",TblHisLisPatientInfoVnio.Columns.IdDoiTuongBenhNhan,1,true,false));
            grdList.Columns.Add(UI.CreateGridTextColumn(LObjectType.Columns.SName,
                                                        "Đối tượng", "DoiTuong"
                                                        ));
        }

        private void GrdThongTinBnOut()
        {
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Sophieu,
                                                            "Số phiếu",
                                                            TblHisLisPatientInfoVnio.Columns.Sophieu, null, false, false));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.MaBenhNhan,
                                                            "Mã BN",
                                                            TblHisLisPatientInfoVnio.Columns.MaBenhNhan, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.TenBenhNhan,
                                                            "Tên BN",
                                                            TblHisLisPatientInfoVnio.Columns.TenBenhNhan, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Tuoi,
                                                            "Tuổi",
                                                            TblHisLisPatientInfoVnio.Columns.Tuoi, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn("Sex_name",
                                                            "Giới Tính",
                                                            "Sex_name", null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn("StatusSend",
                                                            "Trang thai",
                                                            "StatusSend", null, true, false));


            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.DiaChi,
                                                            "Địa Chỉ",
                                                            TblHisLisPatientInfoVnio.Columns.DiaChi, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Phong,
                                                            "Phòng",
                                                            TblHisLisPatientInfoVnio.Columns.Phong, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Khoa,
                                                            "Khoa",
                                                            TblHisLisPatientInfoVnio.Columns.Khoa, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Buong,
                                                            "Buồng",
                                                            TblHisLisPatientInfoVnio.Columns.Buong, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Giuong,
                                                            "Giường",
                                                            TblHisLisPatientInfoVnio.Columns.Giuong, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.IdKhoa,
                                                            "Id Khoa",
                                                            TblHisLisPatientInfoVnio.Columns.IdKhoa, null, true, false));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.BacSyDieuTri,
                                                            "Bác Sỹ DT",
                                                            TblHisLisPatientInfoVnio.Columns.BacSyDieuTri, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.IdBacSyDieuTri,
                                                            "Id Bác Sỹ",
                                                            TblHisLisPatientInfoVnio.Columns.IdBacSyDieuTri,
                                                            null, true, false));

            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.Id, "ID",
                                                            TblHisLisPatientInfoVnio.Columns.Id, null, true, false));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.NoiTru,
                                                            "Nội Trú",
                                                            TblHisLisPatientInfoVnio.Columns.NoiTru, null, true));
            grdlist_out.Columns.Add(UI.CreateGridTextColumn(TblHisLisPatientInfoVnio.Columns.TestDate,
                                                            "Ngày làm XN",
                                                            TblHisLisPatientInfoVnio.Columns.TestDate, null, true));
        }

        /// <summary>
        /// Load thông tin Yeu cau XN lên lưới
        /// </summary>
        private void GrdyeuCauXn()
        {
            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.IdCanLamSangThucHien,
                                                           "ID Cận LS",
                                                           TblYeucauXetnghiemVnio.Columns.IdCanLamSangThucHien, null,
                                                           true));
            grdRegList.Columns.Add(UI.CreateGridTextColumn("TenThongSo",
                                                           "Tên thông số",
                                                           "TenThongSo", null, false));
            grdRegList.Columns.Add(UI.CreateGridTextColumn("DeviceID",
                                                           "Device ID ",
                                                           "DeviceID", null, false, false));
            grdRegList.Columns.Add(UI.CreateGridTextColumn("AliasName",
                                                           "Alias Name ",
                                                           "AliasName", null, false, false));
            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.TestTypeId,
                                                           "ID Loại XN",
                                                           TblYeucauXetnghiemVnio.Columns.TestTypeId, null, true));
            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.Barcode,
                                                           "Barcode",
                                                           TblYeucauXetnghiemVnio.Columns.Barcode, null, true));
            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.Id,
                                                           "ID",
                                                           TblYeucauXetnghiemVnio.Columns.Id, null, false));
            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.TrangThaiThucHien,
                                                           "Trạng thái",
                                                           TblYeucauXetnghiemVnio.Columns.TrangThaiThucHien, null,
                                                           false));
            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.ThucHienCho,
                                                           "Thực hiện cho",
                                                           TblYeucauXetnghiemVnio.Columns.ThucHienCho, null, false));
            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.MaBenhNhan,
                                                           "Ma BN",
                                                           TblYeucauXetnghiemVnio.Columns.MaBenhNhan, null, false, false));

            grdRegList.Columns.Add(UI.CreateGridTextColumn(TblYeucauXetnghiemVnio.Columns.IsTestName,
                                                           "IsTestName",
                                                           TblYeucauXetnghiemVnio.Columns.IsTestName, null, false, false));
        }

        /// <summary>
        /// Hàm truyền text lên warning
        /// </summary>
        /// <param name="text"></param>
        private void SetTextWarning(string text)
        {
            warningStatus.Text = text;
            warningBox1.Text = text;
            warningsendHis.Text = text;
            warningtimer.Enabled = true;
            timer1.Enabled = true;
            timersendHis.Enabled = true;
            warningsendHis.Enabled = true;
            warningBox1.Enabled = true;
            warningStatus.Enabled = true;
        }

        /// <summary>
        /// Hàm gửi trả dữ liệu cho HIS
        /// </summary>
        /// <param name="idResult">ID của kết quả</param>
        /// <param name="content">q</param>
        /// <returns></returns>
        private XElement GetResponse(int idResult, string content)
        {
            if (idResult == null) throw new ArgumentNullException("idResult");
            if (content == null) throw new ArgumentNullException("content");
            // return null;
            //if (Debugger.IsAttached)
            //{
            //    idResult = id;// "829";
            //    content = CreateXML();// "<thongTinKetQua><ketQuaXetNghiem>5.6mgpl</ketQuaXetNghiem></thongTinKetQua>";
            //}

            try
            {
                var mycache = new CredentialCache
                                  {
                                      {
                                          new Uri(_strEntryLink2 + idResult), "Basic",
                                          new NetworkCredential(_strUserName, _strPassword)
                                          }
                                  };
                byte[] bytes = Encoding.UTF8.GetBytes(content);
                //  var request = WebRequest.Create(strEntryLink2 + "/service/CLS/saveKetQua/" + idResult) as HttpWebRequest;
                var request = WebRequest.Create(_strEntryLink2 + idResult) as HttpWebRequest;
                if (request != null)
                {
                    request.Credentials = mycache;
                    request.Headers.Add("Authorization",
                                        string.Format("Basic {0}",
                                                      Convert.ToBase64String(
                                                          new ASCIIEncoding().GetBytes(string.Format("{0}:{1}",
                                                                                                     _strUserName,
                                                                                                     _strPassword)))));
                    request.Method = "POST";
                    request.ContentType = "application/xml";
                    request.ContentLength = bytes.Length;
                    request.KeepAlive = true;

                    using (Stream putStream = request.GetRequestStream())
                    {
                        putStream.Write(bytes, 0, bytes.Length);
                    }
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        if (request.HaveResponse && response != null)
                        {
                            var reader = new StreamReader(response.GetResponseStream());
                            //SetTextWarning(senddataHis);
                            return XElement.Parse(reader.ReadToEnd());
                        }
                        throw new Exception("Error fetching data.");
                    }
                }
                else
                {
                    throw new Exception("Request is Null");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi không kết nối tới Server, Đề nghị bạn kiểm tra lại kết nối", "Thông báo",
                //                MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetTextWarning(ex.ToString());
                return null;
            }
        }

        private static string StrDbNull(object obj)
        {
            try
            {
                return obj == null || obj == DBNull.Value ? "" : obj.ToString().Trim();
            }
            catch (Exception)
            {
                return "";
            }
        }

        private static DataSet Getxmlpatient(string filename)
        {
            if (!File.Exists(filename)) return null;
            var thereader = new StringReader(File.ReadAllText(filename));
            var dataset = new DataSet();
            dataset.ReadXml(thereader);
            return dataset;
        }

        /// <summary>
        /// Hàm bỏ dấu
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string Bodau(string s)
        {
            int i = 0;
            string result = null;

            if (!string.IsNullOrEmpty(Strings.Trim(s)))
            {
                for (i = 0; i <= s.Length - 1; i++)
                {
                    //ch = Strings.Mid(s, i, 1);
                    switch (s[i])
                    {
                        case 'â':
                        case 'ă':
                        case 'ấ':
                        case 'ầ':
                        case 'ậ':
                        case 'ẫ':
                        case 'ẩ':
                        case 'ắ':
                        case 'ằ':
                        case 'ẵ':
                        case 'ẳ':
                        case 'ặ':
                        case 'á':
                        case 'à':
                        case 'ả':
                        case 'ã':
                        case 'ạ':
                            result += 'a';
                            break;
                        case 'Â':
                        case 'Ă':
                        case 'Ấ':
                        case 'Ầ':
                        case 'Ậ':
                        case 'Ẫ':
                        case 'Ẩ':
                        case 'Ắ':
                        case 'Ằ':
                        case 'Ẵ':
                        case 'Ẳ':
                        case 'Ặ':
                        case 'Á':
                        case 'À':
                        case 'Ả':
                        case 'Ã':
                        case 'Ạ':
                            result += 'A';
                            break;
                        case 'ó':
                        case 'ò':
                        case 'ỏ':
                        case 'õ':
                        case 'ọ':
                        case 'ô':
                        case 'ố':
                        case 'ồ':
                        case 'ổ':
                        case 'ỗ':
                        case 'ộ':
                        case 'ơ':
                        case 'ớ':
                        case 'ờ':
                        case 'ợ':
                        case 'ở':
                        case 'ỡ':
                            result += 'o';
                            break;
                        case 'Ó':
                        case 'Ò':
                        case 'Ỏ':
                        case 'Õ':
                        case 'Ọ':
                        case 'Ô':
                        case 'Ố':
                        case 'Ồ':
                        case 'Ổ':
                        case 'Ỗ':
                        case 'Ộ':
                        case 'Ơ':
                        case 'Ớ':
                        case 'Ờ':
                        case 'Ợ':
                        case 'Ở':
                        case 'Ỡ':
                            result += 'O';
                            break;
                        case 'ư':
                        case 'ứ':
                        case 'ừ':
                        case 'ự':
                        case 'ử':
                        case 'ữ':
                        case 'ù':
                        case 'ú':
                        case 'ủ':
                        case 'ũ':
                        case 'ụ':
                            result += 'u';
                            break;
                        case 'Ư':
                        case 'Ứ':
                        case 'Ừ':
                        case 'Ự':
                        case 'Ử':
                        case 'Ữ':
                        case 'Ù':
                        case 'Ú':
                        case 'Ủ':
                        case 'Ũ':
                        case 'Ụ':
                            result += 'U';
                            break;
                        case 'ê':
                        case 'ế':
                        case 'ề':
                        case 'ệ':
                        case 'ể':
                        case 'ễ':
                        case 'è':
                        case 'é':
                        case 'ẻ':
                        case 'ẽ':
                        case 'ẹ':
                            result += 'e';
                            break;
                        case 'Ê':
                        case 'Ế':
                        case 'Ề':
                        case 'Ệ':
                        case 'Ể':
                        case 'Ễ':
                        case 'È':
                        case 'É':
                        case 'Ẻ':
                        case 'Ẽ':
                        case 'Ẹ':
                            result += 'E';
                            break;
                        case 'í':
                        case 'ì':
                        case 'ị':
                        case 'ỉ':
                        case 'ĩ':
                            result += 'i';
                            break;
                        case 'Í':
                        case 'Ì':
                        case 'Ỉ':
                        case 'Ĩ':
                        case 'Ị':
                            result += 'I';
                            break;
                        case 'ý':
                        case 'ỳ':
                        case 'ỵ':
                        case 'ỷ':
                        case 'ỹ':
                            result += 'y';
                            break;
                        case 'Ý':
                        case 'Ỳ':
                        case 'Ỵ':
                        case 'Ỷ':
                        case 'Ỹ':
                            result += 'Y';
                            break;
                        case 'đ':
                            result += 'd';
                            break;
                        case 'Đ':
                            result += 'D';
                            break;
                        case '%':
                            result += "";
                            break;
                        case '-':
                            result += "";
                            break;

                        default:
                            if (s[i] != ' ') result += s[i];
                            break;
                    }
                }
            }
            return result;
        }

        private delegate object GetControlPropertyThreadSafeDelegate(Control control, string propertyName);

        #endregion


        private void btnBarcodeConfig_Click(object sender, EventArgs e)
        {
            var f = new FrmConfig();
            f.ShowDialog();
        }
    }
}