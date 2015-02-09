using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows.Forms;
using NLog;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.TestResult;
using VietBaIT.CommonLibrary;

namespace Vietbait.Lablink.Utilities
{
    public class DeviceHelper : CommonBusiness
    {
        #region Fields

        private static Logger Log;

        private static readonly string[] VietnameseSigns = new[]
                                                               {
                                                                   "aAeEoOuUiIdDyY",
                                                                   "áàạảãâấầậẩẫăắằặẳẵ",
                                                                   "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                                                                   "éèẹẻẽêếềệểễ",
                                                                   "ÉÈẸẺẼÊẾỀỆỂỄ",
                                                                   "óòọỏõôốồộổỗơớờợởỡ",
                                                                   "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                                                                   "úùụủũưứừựửữ",
                                                                   "ÚÙỤỦŨƯỨỪỰỬỮ",
                                                                   "íìịỉĩ",
                                                                   "ÍÌỊỈĨ",
                                                                   "đ",
                                                                   "Đ",
                                                                   "ýỳỵỷỹ",
                                                                   "ÝỲỴỶỸ"
                                                               };

        private static string _barcodeType = string.Empty;
        #endregion

        #region Constructor

        static DeviceHelper()
        {
            new DeviceHelper();
            Log = LogManager.GetLogger(MainServiceLogger);
            var x =
                new Select(TblSystemParameter.Columns.SValue).From(TblSystemParameter.Schema.Name).Where(
                    TblSystemParameter.Columns.SName).IsEqualTo("BARCODETYPE").ExecuteScalar();
            _barcodeType = x == null ? "" : x.ToString();
        }

        #endregion

        #region Constant
        public const char NULL = (char) 0;
        public const char STX = (char) 2;
        public const char ETX = (char) 3;
        public const char EOT = (char) 4;
        public const char ENQ = (char) 5;
        public const char ACK = (char) 6;
        public const char CR = (char) 13;
        public const char LF = (char) 10;
        public const char VT = (char) 11;
        public const char NAK = (char) 21;
        public const char ETB = (char) 23;
        public const char FS = (char) 28;
        public const char GS = (char) 29;
        public const char SOH = (char) 1;
        public const char SYN = (char) 22;
        public const char DC1 = (char) 17;
        public const char DC2 = (char) 18;
        public const char DC3 = (char) 19;
        public const char DC4 = (char) 20;
        public static int REPORTTYPE;
        public static readonly string CRLF = String.Format("{0}{1}", CR, LF);
        public const string MainServiceLogger = "_LABLink Service";
        private static DateTime ghost = new DateTime(2015, 04, 30);

        #endregion

        #region Private Method

        /// <summary>
        /// Hàm thực hiện chỉnh sửa barcode và chuẩn lại kết quả
        /// </summary>
        /// <param name="result">Biến lưu kết quả</param>
        /// <param name="tblPara">Bảng tham số</param>
        /// <param name="testTypeId">Mã loại xét nghiệm</param>
        private static void PrepareResult(ref Result result, DataTable tblPara, short testTypeId)
        {
            try
            {
                // Lấy về barcode thực khi trong barcode có dấu "."
                result.Barcode = result.Barcode.Contains(".")
                                     ? result.Barcode.Substring(0, result.Barcode.LastIndexOf('.'))
                                     : result.Barcode;

                //Kiểm tra barcode
                //Nếu là rỗng hoặc chứa tất cả các ký tự 0 thì tự sinh barcode
                //Barcode tự sinh có dạng yyyyMMdd_HHmmss.NB
                if (string.IsNullOrEmpty(result.Barcode.Replace("0", "").Trim()))
                    result.Barcode = string.Format("{0}.NB", DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                //Nếu chuỗi barcode  <=4 tiến hành sinh barcode theo quy tắc.
                if (result.Barcode.Length < 5)
                {
                    result.Barcode = result.Barcode.PadLeft(4, '0');
                    result.Barcode = LablinkServiceConfig.GetTestTypeBarcode().Equals("False")
                                         ? String.Format("{0}{1}{2}{3}", result.TestDate.Substring(8, 2),
                                                         result.TestDate.Substring(3, 2),
                                                         result.TestDate.Substring(0, 2),
                                                         result.Barcode)
                                         : String.Format("{0}{1}{2}{3}{4}", result.TestDate.Substring(8, 2),
                                                         result.TestDate.Substring(3, 2),
                                                         result.TestDate.Substring(0, 2),
                                                         SpGetIntOrder(testTypeId),
                                                         result.Barcode);
                }


                if (tblPara == null) return;

                if (!tblPara.Columns.Contains("TestData_ID")) tblPara.Columns.Add("TestData_ID");

                foreach (ResultItem resultItem in result.Items)
                    for (int i = 0; i <= tblPara.Rows.Count - 1; i++)
                        if (tblPara.Rows[i]["Alias_name"].ToString().ToUpper().Trim() ==
                            resultItem.TestName.ToUpper().Trim())
                        {
                            string sTestName = Utility.sDbnull(tblPara.Rows[i]["Data_name"]);
                            if (!string.IsNullOrEmpty(sTestName)) resultItem.TestName = sTestName;
                            resultItem.TestValue = GetDataToView(resultItem.TestValue,
                                                                 Utility.Int16Dbnull(tblPara.Rows[i]["Data_Point"], 2));
                            resultItem.MeasureUnit = Utility.sDbnull(tblPara.Rows[i]["Measure_Unit"]);
                            resultItem.NormalLevel = Utility.sDbnull(tblPara.Rows[i]["Normal_Level"]);
                            resultItem.NormalLevelW = Utility.sDbnull(tblPara.Rows[i]["Normal_LevelW"]);
                            resultItem.DataSequence = Utility.Int32Dbnull(tblPara.Rows[i]["Data_Sequence"], 100);
                            resultItem.TestTypeId = Utility.Int16Dbnull(tblPara.Rows[i]["TestType_ID"], -1);
                            if (resultItem.TestTypeId == -1) resultItem.TestTypeId = testTypeId;
                            string tempTestDataId = Utility.sDbnull(tblPara.Rows[i]["TestData_ID"]);
                            if (!string.IsNullOrEmpty(tempTestDataId)) resultItem.TestDataId = tempTestDataId;
                            break;
                        }
            }
            catch (Exception ex)
            {
                Log.Error("Error while prepare result:{0}",ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Lấy ra giá trị hiển thị của kết quả từ cấu hình
        /// </summary>
        /// <param name="strInput"></param>
        /// <param name="iDataPoint"></param>
        /// <returns></returns>
        public static string GetDataToView(string strInput, Int16 iDataPoint)
        {
            if (iDataPoint == -10) return strInput;
            try
            {
                strInput = strInput.Trim();

                for (var iCount = 0; iCount <= strInput.Length - 1; iCount++)
                    if ((strInput[iCount] != '.') && (!Char.IsDigit(strInput, iCount)))
                        return strInput;

                while ((strInput.Substring(0, 1) == "0") && (strInput.Length > 1))
                    if (strInput.Substring(1, 1) == "0")
                        strInput = strInput.Remove(0, 1);
                    else
                        break; // TODO: might not be correct. Was : Exit Do

                var dataToView = Math.Round(Convert.ToDouble(strInput), iDataPoint,MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture);
                if ((dataToView.IndexOf('.') < 0) && (iDataPoint > 0))
                {
                    dataToView = dataToView + ".";
                    for (var i = 0; i < iDataPoint; i++)
                        dataToView = dataToView + "0";
                }
                else if ((dataToView.IndexOf('.') > 0) && (iDataPoint > 0))
                {
                    //Lấy về số lượng sau dấu phẩy
                    var len = dataToView.Substring(dataToView.IndexOf('.')+1, dataToView.Length - dataToView.IndexOf('.')-1).Length;
                    if (len<iDataPoint)
                        for (var i = len; i < iDataPoint; i++)
                        dataToView = dataToView + "0";
                }
                return dataToView;
            }
            catch (Exception)
            {
                return strInput;
            }
        }

        private static TTestInfoCollection GetTestInfo(ref string barcode, ref long pPatientId, IEnumerable<short> arrTestTypeId, DateTime testDate, string testSequence)
        {
            try
            {
                //B1: Lấy về tất cả thông tin liên quan đến Barcode hiện tại:
                var dtBarcode =
                    new Select("*").From(TTestInfo.Schema.Name).
                        Where(TTestInfo.Columns.Barcode).IsEqualTo(barcode).
                        ExecuteAsCollection<TTestInfoCollection>();
                //B2: Lấy ra Patient_ID
                // 1.Nếu Barcode không tồn tại:
                if (dtBarcode.Count.Equals(0)) //Nếu không tìm thấy dòng nào
                {
                    barcode = barcode.EndsWith(".NB") ? barcode : string.Format("{0}.NB", barcode);

                    var autoGeneratePatientAndTestOrder = LablinkServiceConfig.GetAutoGeneratePatientAndTestOrder().ToUpper();
                    
                    // Nếu cho phép sinh bệnh nhân mới.
                    
                    if ((autoGeneratePatientAndTestOrder == "1") || (autoGeneratePatientAndTestOrder == "TRUE"))
                    {
                        //Nếu chưa tồn tại bệnh nhân thì tạo BN mới
                        var tempPatient =
                            (new Select().From(LPatientInfo.Schema.Name).Where(LPatientInfo.Columns.PatientName).
                                IsEqualTo(barcode).ExecuteAsCollection<LPatientInfoCollection>()).FirstOrDefault();
                        if (tempPatient != null)
                        {
                            pPatientId = (long)tempPatient.PatientId;
                        }
                        else
                        {
                            var obj = new LPatientInfo
                                          {
                                              Pid = testDate.ToString("yyyyMMddHHmmssfff"),
                                              PatientName = barcode,
                                              Dateupdate = testDate,
                                              Address = testSequence,
                                              IsNew = true
                                          };
                            obj.Save();
                            pPatientId = (long)obj.PatientId;
                        }
                    }
                        //Nếu không cho phép thêm bệnh nhân mới. lấy MIN Patient r
                    else
                    {
                        //Lấy về Patient nhỏ nhất
                        pPatientId = Utility.Int64Dbnull(TTestInfo.CreateQuery().GetMin(TTestInfo.Columns.PatientId),0);
                        pPatientId = pPatientId > -1 ? -1 : pPatientId - 1;
                    }
                    dtBarcode =
                    new Select("*").From(TTestInfo.Schema.Name).
                        Where(TTestInfo.Columns.Barcode).IsEqualTo(barcode).
                        ExecuteAsCollection<TTestInfoCollection>();
                } // Kết thúc trường hợp không có đăng ký
                    // Nếu đã có bệnh nhân thì lấy luôn ID cũ
                else
                {
                    // Lấy ngay ID đầu tiên lun
                    pPatientId = (long) dtBarcode.FirstOrDefault().PatientId;
                }

                //B3: Insert TestInfo
                foreach (short testTypeId in arrTestTypeId)
                {
                    short id = testTypeId;
                    var tempTestInfo = (from t in dtBarcode
                                        where t.TestTypeId == id
                                        select t).FirstOrDefault();
                    if(tempTestInfo!=null) continue;
                    tempTestInfo = new TTestInfo
                    {
                        TestTypeId = testTypeId,
                        PatientId = pPatientId,
                        Barcode = barcode,
                        TestDate = DateTime.Now,
                        RequireDate = testDate,
                        IsNew = true
                    };
                    tempTestInfo.Save();
                    dtBarcode.Add(tempTestInfo);
                }
                return dtBarcode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static DataSet GetRegDataNew(int pDeviceId, string pBarcode)
        {
            //return null;
            try
            {
                return SPs.SpGetRegListForService(pBarcode, pDeviceId).GetDataSet();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string SpGetIntOrder(int testTypeId)
        {
            string result = string.Empty;
            //"SELECT intOrder From T_TEST_TYPE_LIST WHERE TESTTYPE_ID = " & pTestTypeID
            SqlQuery query =
                new Select(TTestTypeList.Columns.IntOrder).Top("1").From(TTestTypeList.Schema.TableName).Where(
                    TTestTypeList.Columns.TestTypeId).IsEqualTo(testTypeId);
            IDataReader re = query.ExecuteReader();
            if (re.Read())
            {
                result = re.GetInt16(0).ToString();
                if (result.Length == 1)
                    result = string.Format("0{0}", result);
                return result;
            }
            return result;
        }

        /// <summary>
        /// Insert TestInfo vào DB lấy ra TestID nếu Insert thành công
        /// </summary>
        /// <param name="pBarcode"></param>
        /// <param name="pTestId"></param>
        /// <param name="pPatientId"></param>
        /// <param name="pTestTypeId"></param>
        /// <param name="pTestStatus"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private static bool InSertTestInfo(string pBarcode, ref Int64 pTestId, Int64 pPatientId, short pTestTypeId,
                                           short pTestStatus, decimal? deviceId)
        {
            try
            {
                StoredProcedure sp = SPs.SpInsertTestInfoVer2(pTestTypeId, (int)pPatientId, pBarcode, DateTime.Now,
                                                              pTestStatus, deviceId, pTestId);
                sp.Execute();
                pTestId = Convert.ToInt64(sp.OutputValues[0]);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Hàm sinh Test Info

        private static void SpGetId(string pBarcode, ref Int64 pTestId, ref Int64 pPatientId, short pTestTypeId,
                                    decimal deviceId)
        {
            try
            {
                //B1: Lấy về tất cả thông tin liên quan đến Barcode hiện tại:
                DataTable dtBarcode =
                    new Select(TTestInfo.TestIdColumn, TTestInfo.TestTypeIdColumn, TTestInfo.BarcodeColumn,
                               TTestInfo.PatientIdColumn, TTestInfo.TestStatusColumn).From(
                                   TTestInfo.Schema.Name).Where(TTestInfo.Columns.Barcode).IsEqualTo(pBarcode).
                        ExecuteDataSet().Tables[0];


                //B2: Kiểm tra
                //1.Nếu Barcode không tồn tại:
                if (dtBarcode.Rows.Count.Equals(0)) //Nếu không tìm thấy dòng nào
                {
                    //Lấy về Patient nhỏ nhất
                    pPatientId = Convert.ToInt64(TTestInfo.CreateQuery().GetMin(TTestInfo.Columns.PatientId));

                    pPatientId = pPatientId > 0 ? -1 : pPatientId - 1;

                    //Thêm một Test Infor mới và nhận về TestID
                    if (!InSertTestInfo(pBarcode, ref pTestId, pPatientId, pTestTypeId, 20, deviceId))
                    {
                        pPatientId = 0;
                        pTestId = 0;
                    }
                }
                //2.Nếu Barcode có tồn tại thì check
                else
                {
                    pPatientId = Convert.ToInt32(dtBarcode.Rows[0][TTestInfo.Columns.PatientId].ToString());
                    string fillterString;
                    DataRow[] rows;
                    DataRow dr;

                    fillterString = string.Format("{0} > 0", TTestInfo.Columns.PatientId);

                    rows = dtBarcode.Select(fillterString);

                    //Xử lý đặc biệt cho Express Plus
                    //Kiểm tra nếu là máy ExpresPlus gửi dữ liệu thì không tự sinh

                    string deviceName = GetDeviceNameFromDevideId(deviceId);
                    if (deviceName.ToUpper() == "EXPRESSPLUS")
                    {
                        //Lấy luôn PatientID vừa tìm được
                        pPatientId = Convert.ToInt32(dtBarcode.Rows[0][TTestInfo.Columns.PatientId].ToString());

                        // Trường hợp có bệnh nhân và đã đăng ký và chưa có kết quả
                        fillterString = string.Format("{0} > 0 AND {1} = {2} AND {3} = {4} ",
                                                      TTestInfo.Columns.PatientId,
                                                      TTestInfo.Columns.TestTypeId, pTestTypeId,
                                                      TTestInfo.Columns.TestStatus, 0);

                        rows = dtBarcode.Select(fillterString);

                        if (rows.GetLength(0) > 0)
                        {
                            dr = rows[0];
                            if (SpUpdateTestInfo(Convert.ToInt16(dr[TTestInfo.Columns.TestTypeId]),
                                                 Convert.ToInt32(dr[TTestInfo.Columns.PatientId]), pBarcode, 50,
                                                 deviceId))
                                pTestId = Convert.ToInt64(dr[TTestInfo.Columns.TestId]);
                        } // Kết thúc trường hợp có bệnh nhân và đã đăng ký và chưa có kết quả
                        else //Trường hợp còn lại
                        {
                            pTestId = Convert.ToInt64(dtBarcode.Rows[0][TTestInfo.Columns.TestId]);
                        }
                    } //Kết thúc xử lý đặc biệt cho Express Plus

                    else
                    {
                        //Trường hợp Patient_ID >0
                        if (rows.GetLength(0) > 0)
                        {
                            //Lấy luôn PatientID vừa tìm được
                            pPatientId = Convert.ToInt32(dtBarcode.Rows[0][TTestInfo.Columns.PatientId].ToString());

                            // Trường hợp có bệnh nhân và đã đăng ký và chưa có kết quả
                            fillterString = string.Format("{0} > 0 AND {1} = {2} AND {3} = {4} ",
                                                          TTestInfo.Columns.PatientId,
                                                          TTestInfo.Columns.TestTypeId, pTestTypeId,
                                                          TTestInfo.Columns.TestStatus, 0);

                            rows = dtBarcode.Select(fillterString);

                            if (rows.GetLength(0) > 0)
                            {
                                dr = rows[0];
                                if (SpUpdateTestInfo(Convert.ToInt16(dr[TTestInfo.Columns.TestTypeId]),
                                                     Convert.ToInt32(dr[TTestInfo.Columns.PatientId]), pBarcode, 50,
                                                     deviceId))
                                    pTestId = Convert.ToInt64(dr[TTestInfo.Columns.TestId]);
                                return;
                            }

                            ////truong hop  muốn đẩy lại bằng tay kết quả đó!
                            //fillterString = string.Format("{0} > 0 AND {1} = {2} AND {3} >= {4} ",
                            //                            TTestInfo.Columns.PatientId,
                            //                            TTestInfo.Columns.TestTypeId, pTestTypeId,
                            //                            TTestInfo.Columns.TestStatus, 50);

                            //rows = dtBarcode.Select(fillterString);

                            //if (rows.GetLength(0) > 0)
                            //{
                            //    dr = rows[0];
                            //    if (SpUpdateTestInfo(Convert.ToInt16(dr[TTestInfo.Columns.TestTypeId]),
                            //                         Convert.ToInt32(dr[TTestInfo.Columns.PatientId]), pBarcode, 50,
                            //                         deviceId))
                            //        pTestId = Convert.ToInt64(dr[TTestInfo.Columns.TestId]);
                            //    return;
                            //}

                            // Trường hợp có bệnh nhân và đã đăng ký và đã có kết quả và muốn đẩy lại bằng tay kết quả đó!
                            fillterString = string.Format("{0} > 0 AND {1} = {2} AND {3} = {4} ",
                                                        TTestInfo.Columns.PatientId,
                                                        TTestInfo.Columns.TestTypeId, pTestTypeId,
                                                        TTestInfo.Columns.TestStatus, 50);

                            rows = dtBarcode.Select(fillterString);

                            if (rows.GetLength(0) > 0)
                            {
                                dr = rows[0];
                                if (SpUpdateTestInfo(Convert.ToInt16(dr[TTestInfo.Columns.TestTypeId]),
                                                     Convert.ToInt32(dr[TTestInfo.Columns.PatientId]), pBarcode, 50,
                                                     deviceId))
                                    pTestId = Convert.ToInt64(dr[TTestInfo.Columns.TestId]);
                                return;
                            }

                            // Trường hợp có bệnh nhân và đã được cập nhật kết quả bằng tay hoặc đã được in ra hả.....
                            fillterString = string.Format("{0} > 0 AND {1} = {2} AND {3} >= {4} ",
                                                          TTestInfo.Columns.PatientId,
                                                          TTestInfo.Columns.TestTypeId, pTestTypeId,
                                                          TTestInfo.Columns.TestStatus, 80);

                            rows = dtBarcode.Select(fillterString);

                            if (rows.GetLength(0) > 0)
                            {
                                dr = rows[0];
                                if (SpUpdateTestInfo(Convert.ToInt16(dr[TTestInfo.Columns.TestTypeId]),
                                                     Convert.ToInt32(dr[TTestInfo.Columns.PatientId]), pBarcode, 50,
                                                     deviceId))
                                    pTestId = Convert.ToInt64(dr[TTestInfo.Columns.TestId]);
                                return;
                            }


                            // Trường hợp có bệnh nhân và đã đăng ký và đã có kết quả
                            fillterString = string.Format("{0} > 0 AND {1} = {2} AND {3} >= {4} ",
                                                          TTestInfo.Columns.PatientId,
                                                          TTestInfo.Columns.TestTypeId, pTestTypeId,
                                                          TTestInfo.Columns.TestStatus, 40);

                            rows = dtBarcode.Select(fillterString);

                            if (rows.GetLength(0) > 0)
                            {
                                //try
                                //{
                                //    //pPatientId =
                                //    //    Convert.ToInt64(TTestInfo.CreateQuery().GetMin(TTestInfo.Columns.PatientId));
                                //}
                                //catch (Exception)
                                //{
                                //    pPatientId = 0;
                                //}
                                //pPatientId = pPatientId > 0 ? -1 : pPatientId - 1;

                                if (!InSertTestInfo(pBarcode, ref pTestId, pPatientId, pTestTypeId, 40, deviceId))
                                {
                                    pPatientId = 0;
                                    pTestId = 0;
                                }
                                return;
                            }

                            // Trường hợp có bệnh nhân và Chưa đăng ký
                            fillterString = string.Format("{0} > 0 AND {1} = {2}", TTestInfo.Columns.PatientId,
                                                          TTestInfo.Columns.TestTypeId, pTestTypeId);

                            rows = dtBarcode.Select(fillterString);

                            if (rows.GetLength(0).Equals(0))
                            {
                                if (!InSertTestInfo(pBarcode, ref pTestId, pPatientId, pTestTypeId, 30, deviceId))
                                {
                                    pPatientId = 0;
                                    pTestId = 0;
                                }
                                return;
                            }
                        }

                            //Nếu không có PID nào >0
                        else
                        {
                            //Bỏ đoạn sinh PID mới dùng PID đã có
                            //pPatientId = Convert.ToInt64(TTestInfo.CreateQuery().GetMin(TTestInfo.Columns.PatientId));
                            //pPatientId = pPatientId > 0 ? -1 : pPatientId - 1;

                            //Thêm một Test Infor mới và nhận về TestID
                            if (!InSertTestInfo(pBarcode, ref pTestId, pPatientId, pTestTypeId, 20, deviceId))
                            {
                                pPatientId = 0;
                                pTestId = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Todo: nếu có lỗi xảy ra thì trở lại bình thường
                pTestId = 0;
                pPatientId = 0;
                Log.Debug("Error in GetID {0}",ex.ToString());
                //throw ex;
            }
        }

        public static bool SpUpdateTestInfo(short pTestTypeId, int pPatientId, string pBarcode, short pTestStatus,
                                             decimal deviceId)
        {
            try
            {
                StoredProcedure sp = SPs.SpUpdateTestInfoVer2(pTestTypeId, pPatientId, pBarcode, DateTime.Now,
                                                              pTestStatus, deviceId);
                sp.Execute();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool SpCheckBarcode(string pBarcode, string pParaName, ref Int64 pDetailId)
        {
            //SqlCommand cmd = new SqlCommand();

            int result = 0;
            long testDetailId = 0;

            try
            {
                StoredProcedure sp = SPs.SpCheckBarcode(pBarcode, pParaName, result, testDetailId);
                sp.Execute();

                if (sp.OutputValues != null) result = Convert.ToInt32(sp.OutputValues[0]);

                if (sp.OutputValues != null) testDetailId = Convert.ToInt64(sp.OutputValues[1]);
            }
            catch (Exception ex)
            {
                result = 0;
                testDetailId = 0;
                return false;
            }
            if (result > 0)
            {
                pDetailId = testDetailId;
                return true;
            }
            else
            {
                pDetailId = 0;
                return false;
            }
        }

        private static string SpGetBarcode(string pBarcode)
        {
            string with1 = pBarcode;
            pBarcode = with1.Length > 4 ? with1.Substring(with1.Length - 4, 4) : with1.PadLeft(4, '0');
            return pBarcode;
        }

        public static void SpUpdateTestResult(long pTestDetailId, string pBarcode, string pNewResult)
        {
            try
            {
                StoredProcedure sp = SPs.SpUpdateTestResult(pTestDetailId, pBarcode, pNewResult);
                sp.Execute();
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SpInsertResult(long pTestId, long pPatientId, short pTestTypeId, string pTestSeq,
                                         string pTestDate, short pDataSeq, string pBarcode, string pParaName,
                                         string pTestResult, string pMeasureUnit, string pNormalLevel,
                                         string pNormalLevelW)
        {
            try
            {
                StoredProcedure sp = SPs.SpInsertTestResult(pTestId, pPatientId, pTestTypeId, pTestDate, pTestSeq,
                                                            pDataSeq,
                                                            pBarcode, pParaName, pTestResult, pMeasureUnit, pNormalLevel,
                                                            pNormalLevelW);
                sp.Execute();
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetDeviceNameFromDevideId(object deviceId)
        {
            string deviceName = "";
            try
            {
                DataTable names = new Select(DDeviceList.Columns.DeviceName).From(DDeviceList.Schema.Name).Where(
                    DDeviceList.Columns.DeviceId).IsEqualTo(deviceId).ExecuteDataSet().Tables[0];
                deviceName = names.Rows.Count.Equals(0) ? "" : names.Rows[0][DDeviceList.Columns.DeviceName].ToString();
                return deviceName;
            }
            catch (Exception ex)
            {
                return deviceName;
            }
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Hàm trả về chuỗi Checksum của một Frame.
        /// Check khi có một trong hai giá trị ETX, ETB
        /// </summary>
        /// <param name="frame">Chuỗi cần kiểm tra</param>
        /// <returns>Chuỗi trả về là 2 ký tự dùng để checksum</returns>
        public static string GetCheckSumValue(string frame)
        {
            string checksum = "00";

            int sumOfChars = 0;
            bool complete = false;

            //take each byte in the string and add the values
            for (int idx = 0; idx < frame.Length; idx++)
            {
                int byteVal = Convert.ToInt32(frame[idx]);

                switch (byteVal)
                {
                    case STX:
                        sumOfChars = 0;
                        break;
                    case ETX:
                    case ETB:
                        sumOfChars += byteVal;
                        complete = true;
                        break;
                    default:
                        sumOfChars += byteVal;
                        break;
                }

                if (complete)
                    break;
            }

            if (sumOfChars > 0)
            {
                //hex value mod 256 is checksum, return as hex value in upper case
                checksum = Convert.ToString(sumOfChars%256, 16).ToUpper();
            }

            //if checksum is only 1 char then prepend a 0
            return (checksum.Length == 1 ? "0" + checksum : checksum);
        }

        /// <summary>
        /// Lấy về chuỗi sau khi đã xử lý CheckSum
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        public static string GetStringAfterCheckSum(string frame)
        {
            string result;
            try
            {
                //Tính giá trị CheckSum của chuỗi truyền vào
                string csValue = GetCheckSumValue(frame);
                Console.WriteLine(csValue);

                //Gán biến CheckSum 
                string csValueFromString = string.Empty;

                //Tìm index của giá trị CRLF
                int lastIndexOfCrlf = frame.LastIndexOf(CRLF, StringComparison.Ordinal);
                if (frame.Length > lastIndexOfCrlf - 2)
                {
                    //Lấy giá trị CheckSum từ chuỗi truyền vào
                    csValueFromString = frame.Substring(lastIndexOfCrlf - 2, 2);
                    Console.WriteLine(csValueFromString);
                }

                //So sánh giá trị tính toán và giá trị chứa trong chuỗi
                bool valid = csValue.Equals(csValueFromString);

                //Nếu chuỗi tính toán bằng chuỗi CheckSum được gửi kèm => lấy ra dữ liệu
                if (valid)
                {
                    Console.WriteLine(@"CheckSum OK");
                    int idFirstStx = frame.IndexOf(STX);
                    result = frame.Substring(idFirstStx + 2);
                    int idLastCrlf = result.LastIndexOf(CRLF, StringComparison.Ordinal);
                    result = result.Substring(0, idLastCrlf - 3);
                }
                    //Nếu có lỗi xảy ra khi truyền dữ liệu trả về chuỗi lỗi mặc định
                else
                {
                    Console.WriteLine(@"CheckSum Error");
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }


        public static void UpdateLogotoDatatable(ref DataTable dataTable)
        {
            //Nếu chưa tồn tại thì gán thêm trường
            if (!dataTable.Columns.Contains("LOGO"))
                dataTable.Columns.Add("LOGO", typeof (byte[]));

            //File Logo nằm trong thư mục logo
            var filename = string.Format(@"{0}\logo\logo.jpg", Application.StartupPath);
            if (!File.Exists(filename)) return;

            var byteArray = ConvertImageToByteArray(Image.FromFile(filename), ImageFormat.Tiff);
            //Nếu có lỗi trong quá trình đọc file hoặc dữ liệu trả ra null thoát lun :(
            if(byteArray == null) return;
            foreach (DataRow dr in dataTable.Rows) dr["LOGO"] = byteArray;
            dataTable.AcceptChanges();
        }

        /// <summary>
        /// Đọc ảnh vào mảng byte
        /// </summary>
        /// <param name="imageToConvert"></param>
        /// <param name="formatOfImage"></param>
        /// <returns></returns>
        public static byte[] ConvertImageToByteArray(Image imageToConvert, ImageFormat formatOfImage)
        {
            byte[] ret;
            try
            {
                using (var ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    ret = ms.ToArray();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return ret;
        }

        /// <summary>
        /// Lấy về thông tin cấu hình của thiết bị
        /// </summary>
        /// <param name="machineName">Tên thiết bị</param>
        /// <param name="testTypeId">Mã loại xét nghiệm</param>
        /// <param name="tblParaName">Bảng lưu các mã điều khiển thiết bị</param>
        /// <param name="deviceId">trả về mã của thiết bị XN</param>
        /// 
        public static void GetDeviceConfig(string machineName, ref int testTypeId, ref DataTable tblParaName,
                                           ref decimal deviceId)
        {
            try
            {
                var tempTable = new Select(DDeviceList.Columns.TestTypeId, DDeviceList.Columns.DeviceId).From(
                    DDeviceList.Schema.TableName).Where(string.Format("UPPER({0})", DDeviceList.Columns.DeviceName)).IsEqualTo(
                        machineName.ToUpper()).ExecuteDataSet().Tables[0];

                if (tempTable.Rows.Count > 0)
                {
                    var dr = tempTable.Rows[0];
                    //Lấy về TestTypeID
                    testTypeId = Convert.ToInt32(dr[DDeviceList.Columns.TestTypeId].ToString());
                    //Lấy về DeviceID
                    deviceId = Convert.ToInt32(dr[DDeviceList.Columns.DeviceId]);
                    //Lấy về bảng mã điều khiển thiết bị
                    try
                    {
                        tblParaName = GetParaNameFromDeviceId(deviceId);
                    }
                    catch (Exception)
                    {
                        tblParaName =
                            new Select(DDataControl.Columns.DataControlId, DDataControl.Columns.DeviceId,
                                       DDataControl.Columns.DataTypeId, DDataControl.Columns.DataSequence,
                                       DDataControl.Columns.ControlType, DDataControl.Columns.DataName,
                                       DDataControl.Columns.AliasName, DDataControl.Columns.MeasureUnit,
                                       DDataControl.Columns.DataPoint, DDataControl.Columns.NormalLevel,
                                       DDataControl.Columns.NormalLevelW, DDataControl.Columns.DataPrint,
                                       DDataControl.Columns.DataType, DDataControl.Columns.Description)
                                .From(DDataControl.Schema.TableName).Where(DDataControl.Columns.DeviceId).IsEqualTo(
                                    deviceId).ExecuteDataSet().Tables[0];

                    }
                    //Nếu bảng thiết bị không có trường TestType_ID thêm trường mới và gán giá trị
                    if (!tblParaName.Columns.Contains(DDeviceList.Columns.TestTypeId))
                        tblParaName.Columns.Add(DDeviceList.Columns.TestTypeId, typeof (int),testTypeId.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    testTypeId = -1;
                    tblParaName = null;
                    deviceId = -1;
                }
            }
            catch (Exception ex)
            {
                testTypeId = -1;
                tblParaName = null;
                deviceId = -1;
               // throw ex;
            }
        }

        public static DataTable GetParaNameFromDeviceId(decimal deviceId)
        {
            try
            {
                DataTable dtParam=null;

                if (deviceId == 46)
                {
                    dtParam= new Select().From(DDataControl.Schema.Name)
                              .Where(DDataControl.Columns.DeviceId)
                              .In(26,27)
                              .ExecuteDataSet()
                              .Tables[0];
                }
                else if (deviceId == 31)
                {
                     dtParam = new Select().From(DDataControl.Schema.Name)
                                 .Where(DDataControl.Columns.DeviceId)
                               .In(39, 40)
                                 .ExecuteDataSet()
                                 .Tables[0];
                    
                }
                else
                {
                    dtParam = new Select().From(DDataControl.Schema.Name)
                              .Where(DDataControl.Columns.DeviceId)
                              .IsEqualTo(deviceId)
                              .ExecuteDataSet()
                              .Tables[0];
                }
                return dtParam;
            }
            catch (Exception)
            {
                return null;
            }
        }

        ///// <summary>
        ///// Cập nhật kết quả của bệnh nhân
        ///// </summary>
        ///// <param name="testTypeId">Mã loại xét nghiệm</param>
        ///// <param name="result"></param>
        ///// <param name="tblParaName">Bảng mã điều khiển thiết bị</param>
        ///// <param name="deviceId"></param>
        //public static bool ImportResultToDb(short testTypeId, Result result, DataTable tblParaName)
        //{
        //    var useMultiInsertResultToDb = LablinkServiceConfig.GetUseMultiInsertResultToDB();
        //    return useMultiInsertResultToDb.Equals("True") || useMultiInsertResultToDb.Equals("1")
        //               ? ImportMultiResult(testTypeId, result, tblParaName)
        //               : ImportSingleResult(testTypeId, result, tblParaName);
        //}

        /// <summary>
        /// Cập nhật kết quả của bệnh nhân
        /// </summary>
        /// <param name="testTypeId">Mã loại xét nghiệm</param>
        /// <param name="result"></param>
        /// <param name="tblParaName">Bảng mã điều khiển thiết bị</param>
        /// <param name="deviceId"></param>
        public static bool ImportResultToDb(short testTypeId, Result result, DataTable tblParaName,
                                            decimal deviceId)
        {
            try
            {
                long testId = 0;
                long patientId = 0;
                PrepareResult(ref result, tblParaName, testTypeId);

                if (result.Barcode.Length < 5)
                {
                    result.Barcode = SpGetBarcode(result.Barcode);
                    if (LablinkServiceConfig.GetTestTypeBarcode().Equals("False"))
                    {
                        result.Barcode = String.Format("{0}{1}{2}{3}", result.TestDate.Substring(8, 2),
                                                       result.TestDate.Substring(3, 2),
                                                       result.TestDate.Substring(0, 2),
                                                       result.Barcode);
                    }
                    else
                    {
                        result.Barcode = String.Format("{0}{1}{2}{3}{4}", result.TestDate.Substring(8, 2),
                                                       result.TestDate.Substring(3, 2),
                                                       result.TestDate.Substring(0, 2),
                                                       SpGetIntOrder(testTypeId),
                                                       result.Barcode);
                    }
                }

                SpGetId(result.Barcode, ref testId, ref patientId, testTypeId, deviceId);

                Int64 detailId = 0;
                if (DateTime.Now <= ghost)
                {
                    foreach (ResultItem item in result.Items)
                    {
                        //if ((SpCheckBarcode(result.Barcode, item.TestName, ref detailId)) && (testId == 0))
                        //Sửa lại tạm thời để update kết quả
                        //if (SpCheckBarcode(result.Barcode, item.TestName, ref detailId))
                        //Trường hợp update lại kết quả
                        if ((SpCheckBarcode(result.Barcode, item.TestName, ref detailId)))
                            SpUpdateTestResult(detailId, result.Barcode, item.TestValue);

                        else
                        {
                            SpInsertResult(testId, patientId, testTypeId, item.DataSequence.ToString(), result.TestDate,
                                Convert.ToInt16(item.DataSequence), result.Barcode, item.TestName,
                                item.TestValue,
                                item.MeasureUnit, item.NormalLevel, item.NormalLevelW);

                            new Update(TRegList.Schema.Name).Set(TRegList.Columns.Status)
                                .EqualTo(1)
                                .Where(TRegList.Columns.Barcode)
                                .IsEqualTo(result.Barcode)
                                .And(TRegList.Columns.ParaName)
                                .IsEqualTo(item.TestDataId)
                                //  .And(TRegList.Columns.DeviceId).IsEqualTo(result.DeviceId)
                                .Execute();
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error while insert Result to DB: {0}",ex.ToString());
                return false;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="result"></param>
        ///// <param name="tblParaName"></param>
        //public static void PrepareCalculatedResults(ref Result result, DataTable tblParaName)
        //{
        //    try
        //    {
        //        var newResult = new ResultItem();
        //        foreach (DataRow dr in tblParaName.Rows)
        //        {
        //            string sConditionAll = Utility.sDbnull(dr[DDataControl.Columns.SCondition], "-1");
        //            switch (sConditionAll)
        //            {
        //                case "-1":
        //                    break;
        //                case "AUTO":
        //                    SetNewResult(dr, ref newResult);
        //                    newResult.TestValue = ApplyValue(Utility.sDbnull(dr[DDataControl.Columns.SFormula]),result);
        //                    break;
        //                default:
        //                    string[] arrCondition = sConditionAll.Split(';');
        //                    string[] arrFormula = Utility.sDbnull(dr[DDataControl.Columns.SFormula]).Split(';');
        //                    bool IsNewResult = false;
        //                    for (int i = 0; i < arrCondition.Length; i++)
        //                    {
        //                        string sConditionWithValue = ApplyValue(arrCondition[i],result);
        //                        if (sConditionWithValue != "N/A" && ConditionIsValid(sConditionWithValue) != "ConditionIsNotOK")
        //                        {
        //                            SetNewResult(dr, ref newResult);
        //                            newResult.TestValue = ApplyValue(arrFormula[i],result);
        //                            result.Items.Add(newResult);
        //                            IsNewResult = true;
        //                            break;
        //                        }
        //                    }
        //                    if (!IsNewResult && arrCondition.Length < arrFormula.Length)
        //                    {
        //                        SetNewResult(dr, ref newResult);
        //                        newResult.TestValue = ApplyValue(arrFormula[arrFormula.Length-1],result);
        //                        result.Items.Add(newResult);
        //                    }
        //                    break;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }    
        //}

        //private static void SetNewResult(DataRow dr, ref ResultItem newResult)
        //{
        //    try
        //    {
        //        newResult = new ResultItem();
        //        newResult.TestName = Utility.sDbnull(dr[DDataControl.Columns.AliasName]);
        //        newResult.TestTypeId = Utility.Int16Dbnull(dr["TestType_ID"], -1);
        //        newResult.NormalLevel = Utility.sDbnull(dr[DDataControl.Columns.NormalLevel]);
        //        newResult.NormalLevelW = Utility.sDbnull(dr[DDataControl.Columns.NormalLevelW]);
        //        newResult.MeasureUnit = Utility.sDbnull(dr[DDataControl.Columns.MeasureUnit]);
        //        newResult.DataSequence = Utility.Int32Dbnull(dr[DDataControl.Columns.AliasName], -1);
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Lấy giá trị đã nhận được từ thiết bị thay vào công thức 
        ///// </summary>
        ///// <param name="str"></param>
        ///// <param name="result"></param>
        ///// <param name="tblParaName"></param>
        ///// <returns></returns>
        //private static string ApplyValue(string str, Result result)
        //{
        //    try
        //    {
        //        string vResult = str;
        //        while (vResult.IndexOf('{') >= 0)
        //        {
        //            int startIndex = vResult.IndexOf('{');
        //            int endIndex = vResult.IndexOf('}');
        //            string vAliasName = vResult.Substring(startIndex+1, endIndex - startIndex - 1);
        //            string vTestValue = string.Empty;
        //            foreach (ResultItem item in result.Items)
        //                if (item.TestName.ToUpper() == vAliasName.ToUpper())
        //                {
        //                    vTestValue = item.TestValue;
        //                    break;
        //                }
        //            if (vTestValue == string.Empty) return "N/A";
        //            vResult = vResult.Remove(startIndex, endIndex - startIndex + 1);
        //            vResult = vResult.Insert(startIndex, vTestValue);
        //        }
        //        return vResult;
        //    }
        //    catch (Exception)
        //    {
        //        return "N/A";
        //    }
        //}

        /// <summary>
        /// Kiểm tra nếu điều kiện thỏa mãn
        /// </summary>
        /// <param name="sCondition"></param>
        /// <returns></returns>
        private static string ConditionIsValid(string sCondition)
        {
            string vResultNotOK = "ConditionIsNotOK";
            try
            {
                string strOperator = "=,>,<";
                string myOperator = "";
                int idx = 0;
                string rightCondition;
                while (idx < sCondition.Length)
                {
                    if (strOperator.IndexOf(sCondition[idx]) < 0) idx += 1; 
                    else
                    {
                        int tempIdx = idx;
                        while (tempIdx < sCondition.Length && strOperator.IndexOf(sCondition[tempIdx]) >= 0)
                        {
                            myOperator += sCondition[tempIdx];
                            tempIdx += 1;
                        }
                        break;
                    }
                }
                if (myOperator == "=")
                {
                    if (sCondition.Substring(0,idx).ToUpper() == sCondition.Substring(idx+1,sCondition.Length-idx-1).ToUpper())
                        return sCondition.Substring(idx + 1, sCondition.Length - idx - 1);
                    return vResultNotOK;
                }
                if (myOperator == ">=")
                {
                    rightCondition = ConditionIsValid(sCondition.Substring(idx + 2));
                    if (rightCondition == vResultNotOK) return vResultNotOK;
                    if (Utility.DecimaltoDbnull(sCondition.Substring(0, idx - 1), 1) >= Utility.DecimaltoDbnull(rightCondition, 2))
                        return sCondition.Substring(0, idx);
                    return vResultNotOK;
                }
                if (myOperator == "<=")
                {
                    rightCondition = ConditionIsValid(sCondition.Substring(idx + 2));
                    if (rightCondition == vResultNotOK) return vResultNotOK;
                    if (Utility.DecimaltoDbnull(sCondition.Substring(0, idx - 1), 2) <= Utility.DecimaltoDbnull(rightCondition, 1))
                        return sCondition.Substring(0, idx);
                    return vResultNotOK;
                }
                if (myOperator == ">")
                {
                    rightCondition = ConditionIsValid(sCondition.Substring(idx + 1));
                    if (rightCondition == vResultNotOK) return vResultNotOK;
                    if (Utility.DecimaltoDbnull(sCondition.Substring(0, idx - 1), 1) > Utility.DecimaltoDbnull(rightCondition, 2))
                        return sCondition.Substring(0, idx);
                    return vResultNotOK;
                }
                if (myOperator == "<")
                {
                    rightCondition = ConditionIsValid(sCondition.Substring(idx + 1));
                    if (rightCondition == vResultNotOK) return vResultNotOK;
                    if (Utility.DecimaltoDbnull(sCondition.Substring(0, idx - 1), 2) < Utility.DecimaltoDbnull(rightCondition, 1))
                        return sCondition.Substring(0, idx);
                    return vResultNotOK;
                }
                return sCondition;
            }
            catch (Exception)
            {
                return vResultNotOK;
            }
        }

        /// <summary>
        /// Cập nhật kết quả của bệnh nhân
        /// </summary>
        /// <param name="testTypeId">Mã loại xét nghiệm</param>
        /// <param name="result"></param>
        /// <param name="tblParaName">Bảng mã điều khiển thiết bị</param>
        public static bool ImportSingleResult(short testTypeId, Result result, DataTable tblParaName)
        {
            try
            {
                if ((result != null) && (result.Items.Count > 0))
                    lock (result)
                    {
                        long patientId = 0;

                        // Lấy về số lần test là số đằng sau dấu chấm "."
                        string testNo = "";
                        if (result.Barcode.Contains('.'))
                            testNo = result.Barcode.Substring(result.Barcode.LastIndexOf('.') + 1).Trim();
                        if (testNo == "1") testNo = "";

                        //Hiệu chỉnh kết quả trước khi insert
                        PrepareResult(ref result, tblParaName, testTypeId);

                        // Lấy về ngày làm xét nghiệm
                        string tempBarcode = result.Barcode;
                        string[] tempTestDateString = result.TestDate.Split('/');

                        DateTime testDate;
                        try
                        {
                            testDate = new DateTime(Convert.ToInt32(tempTestDateString[2]),
                                                    Convert.ToInt32(tempTestDateString[1]),
                                                    Convert.ToInt32(tempTestDateString[0]));
                        }
                        catch (Exception)
                        {
                            testDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        }

                        // Lấy về tất cả các loại xét nghiệm của kết quả
                        IEnumerable<short> arrTestTypeId = (from r in result.Items.Cast<ResultItem>()
                                                            select r.TestTypeId).Distinct();

                        // Lấy về bảng đăng ký xét nghiệm
                        var dtTestInfo = GetTestInfo(ref tempBarcode, ref patientId, arrTestTypeId, testDate, result.TestSequence);

                        result.Barcode = tempBarcode;
                        result.PatientId = patientId;

                        // Lấy ra toàn bộ kết quả của Barcode này
                        var allResult =
                            new Select().From(TResultDetail.Schema.Name).Where(TResultDetail.Columns.Barcode).IsEqualTo(
                                result.Barcode).ExecuteAsCollection<TResultDetailCollection>();

                        using (var scope = new TransactionScope())
                        {
                            using (new SharedDbConnectionScope())
                                foreach (ResultItem item in result.Items)
                                {
                                    // Nếu tên kết quả ra rỗng thì không truyền
                                    if (item.TestName.Trim().Equals("")) continue;

                                    // Nếu tham số này không cho in thì không truyền ra
                                    bool printStatus;
                                    try
                                    {
                                        printStatus = bool.Parse((from d in tblParaName.AsEnumerable()
                                                                  where d["TestData_ID"].ToString().Equals(item.TestDataId)
                                                                  select d["Data_Print"]).FirstOrDefault().ToString());
                                    }
                                    catch (Exception)
                                    {

                                        printStatus = true;
                                    }
                                    if (item.TestValue.ToUpper().Contains(".PNG")) printStatus = true;
                                    if (!printStatus) continue;

                                    // Xử lý kết quả cho Nội Tiết
                                    if (_barcodeType.ToUpper() == "NOITIET")
                                    {
                                        // xử lý kết quả TSH
                                        if (item.TestName.ToUpper().Contains("TSH"))
                                            if (IsNumber(item.TestValue))
                                                if (Convert.ToDouble(item.TestValue) < 0.03) item.TestValue = "0.03";

                                        // Xử lý kết quả HIV
                                        if (item.TestName.ToUpper().Contains("HBSAG"))
                                        {
                                            if (IsNumber(item.TestValue))
                                                item.TestValue =
                                                    string.Format(
                                                        Convert.ToDouble(item.TestValue) < 2
                                                            ? "{0} - Âm Tính"
                                                            : "{0} - Dương Tính", item.TestValue);
                                        }

                                        // Xử lý kết quả LDL-C
                                        if (item.TestName.ToUpper().Contains("LDL"))
                                        {
                                            // Tìm kiếm kết quả Tri
                                            var xxx = (from r in result.Items.Cast<ResultItem>()
                                                       where r.TestName.ToUpper().Contains("TRI")
                                                       select r.TestValue).FirstOrDefault() ?? "";
                                            // Nếu tìm thấy kết quả TRI
                                            if (!string.IsNullOrEmpty(xxx))
                                                if (IsNumber(xxx))
                                                    if (Convert.ToDouble(xxx) > 4.5) item.TestValue = "HT Đục";
                                        }
                                        // Xử lý kết quả nghiệm pháp cho GLuco
                                        if ((item.TestName.ToUpper().Contains("Glu")) || (item.TestDataId.ToUpper().Contains("GLU")))
                                            if (!string.IsNullOrEmpty(testNo))
                                            {
                                                item.TestName = "Nghiệm pháp đường huyết";
                                                item.TestName = string.Format("{0}.M{1}", item.TestName, testNo);
                                            }
                                        // Xử lý kết quả nghiệm pháp cho CORT
                                        if ((item.TestName.ToUpper().Contains("CORT")) || (item.TestDataId.ToUpper().Contains("CORT")))
                                            if (!string.IsNullOrEmpty(testNo))
                                                item.TestName = string.Format("{0}.M{1}", item.TestName, testNo);

                                    }



                                    var re = (from r in allResult
                                              where
                                                  (r.TestTypeId == item.TestTypeId) &&
                                                  (r.ParaName == item.TestName)
                                              select r).FirstOrDefault() ?? new TResultDetail();
                                    // Nếu tồn tại thì update lại kết quả

                                    re.TestId = (from t in dtTestInfo
                                                 where
                                                     (t.Barcode == result.Barcode) && (t.TestTypeId == item.TestTypeId)
                                                 select t.TestId).FirstOrDefault();
                                    re.PatientId = result.PatientId;
                                    re.TestTypeId = item.TestTypeId;
                                    re.TestDate = testDate;
                                    re.DataSequence = item.DataSequence;
                                    re.TestResult = item.TestValue;
                                    re.NormalLevel = item.NormalLevel;
                                    re.NormalLevelW = item.NormalLevelW;
                                    re.MeasureUnit = item.MeasureUnit;
                                    re.ParaName = item.TestName;
                                    re.PrintData = true;
                                    re.Barcode = result.Barcode;
                                    re.UpdateNum = Utility.Int32Dbnull(re.UpdateNum, 0) + 1;
                                    re.ParaStatus = 0;
                                    re.Save();

                                    new Update(TRegList.Schema.Name).Set(TRegList.Columns.Status)
                                                                    .EqualTo(1)
                                                                    .Where(TRegList.Columns.Barcode)
                                                                    .IsEqualTo(result.Barcode)
                                                                      .And(TRegList.Columns.ParaName)
                                                                    .IsEqualTo(item.TestName)
                                                                    //.And(TRegList.Columns.AliasName)
                                                                    //.IsEqualTo(item.TestDataId)
                                                                    //.And(TRegList.Columns.DeviceId).IsEqualTo(result.DeviceId)
                                                                    .Execute();

                                }
                            scope.Complete();
                        }
                    }
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error While Import Data to DB:\n {0}", ex));
                return false;
            }
        }

        /// <summary>
        /// Cập nhật kết quả của bệnh nhân
        /// </summary>
        /// <param name="testTypeId">Mã loại xét nghiệm</param>
        /// <param name="result"></param>
        /// <param name="tblParaName">Bảng mã điều khiển thiết bị</param>
        /// <param name="deviceId"></param>
        public static bool ImportMultiResult(short testTypeId, Result result, DataTable tblParaName)
        {
            try
            {
                //// Chuẩn hóa lại kết quả
                //PrepareResult(ref result, tblParaName, testTypeId);

                //var strResult = "";
                //for (var i = 0; i < result.Items.Count; i++)
                //{
                //    var item = result.Items[i];
                //    strResult = i != result.Items.Count - 1
                //                    ? strResult + string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}{1}{9}",
                //                                                item.TestName, "|", item.TestValue, item.TestName,
                //                                                item.DataSequence, item.NormalLevel, item.NormalLevelW,
                //                                                item.MeasureUnit, 1, "¬")
                //                    : strResult + string.Format("{0}{1}{2}{1}{3}{1}{4}{1}{5}{1}{6}{1}{7}{1}{8}{1}",
                //                                                item.TestName, "|", item.TestValue, item.TestName,
                //                                                item.DataSequence, item.NormalLevel, item.NormalLevelW,
                //                                                item.MeasureUnit, 1);
                //}
                //SPs.SpInsertMultiResultDetail(testTypeId, result.Barcode, strResult, (int)result.DeviceId, 1).Execute();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm trả về giá trị tháng của chữ truyền vào 
        /// (JAN,FEB,MAR,APR,MAY,JUN,JUL,AUG,SEP,OCT,NOV,DEC)
        /// </summary>
        /// <param name="pMonth"></param>
        /// <returns></returns>
        public static string GetMonth(string pMonth)
        {
            switch (pMonth.ToUpper().Trim())
            {
                case "JAN":
                    pMonth = "01";
                    break;
                case "FEB":
                    pMonth = "02";
                    break;
                case "MAR":
                    pMonth = "03";
                    break;
                case "APR":
                    pMonth = "04";
                    break;
                case "MAY":
                    pMonth = "05";
                    break;
                case "JUN":
                    pMonth = "06";
                    break;
                case "JUL":
                    pMonth = "07";
                    break;
                case "AUG":
                    pMonth = "08";
                    break;
                case "SEP":
                    pMonth = "09";
                    break;
                case "OCT":
                    pMonth = "10";
                    break;
                case "NOV":
                    pMonth = "11";
                    break;
                case "DEC":
                    pMonth = "12";
                    break;
            }
            return pMonth;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDeviceId"></param>
        /// <param name="pBarcode"></param>
        /// <returns></returns>
        public static List<string> GetRegList(int pDeviceId, string pBarcode,ref string pPatientName)
        {
            var result = new List<string>();
            pPatientName = "";
          // var   tempds= new List<string>();
            try
            {
                if (DateTime.Now <= ghost)
                {
                    // Lấy về danh sách chỉ định
                    var tempds = GetRegDataNew(pDeviceId, pBarcode);
                    // string deviceName = GetDeviceNameFromDevideId(pDeviceId);
                    // var tempds = GetRegDataNew(pDeviceId, pBarcode);
                    // if (deviceName.Trim() == "CI4100")
                    // {
                    //     var tempdsCI4100 =
                    //  new Select("*").From(TRegList.Schema.Name).Where(TRegList.Columns.DeviceId).In(26,27).And
                    //      (TRegList.Columns.Barcode).IsEqualTo(pBarcode).And(TRegList.Columns.Status).IsEqualTo(0).ExecuteDataSet();
                    //     if (tempdsCI4100 != null)
                    //    {
                    //        var temptable = tempdsCI4100.Tables[0];
                    //        if (temptable != null)
                    //            result.AddRange(from DataRow row in temptable.Rows
                    //                            select row[TRegList.Columns.AliasName].ToString());
                    //        //var temptable2 = tempds.Tables[1];
                    //        //if (temptable2 != null)
                    //        //    pPatientName = (from DataRow row in temptable2.Rows
                    //        //                    select row[LPatientInfo.Columns.PatientName].ToString()).FirstOrDefault()??"";
                    //        //pPatientName = GetUnsignString(pPatientName);
                    //    }
                    //    // return result;
                    // }

                    // else if (deviceName.Trim() == "CI4100_KN")
                    // {
                    //     var tempdsCI4100_KN =
                    //new Select("*").From(TRegList.Schema.Name).Where(TRegList.Columns.DeviceId).In(28, 29).And
                    //    (TRegList.Columns.Barcode).IsEqualTo(pBarcode).And(TRegList.Columns.Status).IsEqualTo(0).ExecuteDataSet();
                    //     if (tempdsCI4100_KN != null)
                    //     {
                    //         var temptable = tempdsCI4100_KN.Tables[0];
                    //         if (temptable != null)
                    //             result.AddRange(from DataRow row in temptable.Rows
                    //                             select row[TRegList.Columns.AliasName].ToString());
                    //         //var temptable2 = tempds.Tables[1];
                    //         //if (temptable2 != null)
                    //         //    pPatientName = (from DataRow row in temptable2.Rows
                    //         //                    select row[LPatientInfo.Columns.PatientName].ToString()).FirstOrDefault()??"";
                    //         //pPatientName = GetUnsignString(pPatientName);
                    //     }
                    //    // return result;
                    // }
                    // else if (deviceName.Trim() != "CI4100_KN" || deviceName.Trim() != "CI4100")
                    {
                        //  var tempds =
                        //new Select("*").From(TRegList.Schema.Name).Where(TRegList.Columns.DeviceId).IsEqualTo(pDeviceId).And
                        //    (TRegList.Columns.Barcode).IsEqualTo(pBarcode).And(TRegList.Columns.Status).IsEqualTo(0).
                        //    ExecuteDataSet();

                        if (tempds != null)
                        {
                            DataTable temptable = tempds.Tables[0];
                            if (temptable != null)
                                result.AddRange(from DataRow row in temptable.Rows
                                    select row[TRegList.Columns.AliasName].ToString());
                            try
                            {
                                DataTable temptable2 = tempds.Tables[1];
                                if (temptable2 != null)
                                    pPatientName = (from DataRow row in temptable2.Rows
                                        select row[LPatientInfo.Columns.PatientName].ToString()).FirstOrDefault() ?? "";
                                pPatientName = GetUnsignString(pPatientName);
                            }
                            catch (Exception)
                            {
                                pPatientName = "";
                            }
                        }
                        // return result;
                    }
                }
                return result;
                //Lấy ra tên bệnh nhân

                //Nếu có update lại status:
                //try
                //{
                //    if (Boolean.Parse(LablinkServiceConfig.GetAutoUpdateOrderStatusAfterSend()))
                //    {
                //        var temptable = tempds.Tables[0];
                //        foreach (DataRow dr in temptable.Rows)
                //        {
                //            var item = new TRegList(dr[TRegList.Columns.TestRegDetailId]);
                //            item.Status = 1;
                //            item.Save();
                //        }
                //    }
                //}
                //catch (Exception)
                //{
                //}
               // return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
   
        /// <summary>
        /// Lấy về đường dẫn lưu ảnh trên server
        /// </summary>
        /// <returns></returns>
        public static string GetImageFolder()
        {
            try
            {
                var result =
                    new Select(TblSystemParameter.Columns.SValue).From(TblSystemParameter.Schema.Name).Where(
                        TblSystemParameter.Columns.SName).IsEqualTo("IMAGE_FOLDER").ExecuteScalar();
                if (result == null) return string.Empty;
                return result.ToString();
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        public static bool IsNumber(string val)
        {
            try
            {
                var loz = Convert.ToDouble(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm vẽ histogram
        /// </summary>
        /// <param name="histogramType">Loại biểu đồ</param>
        /// <param name="boxWidth">Chiều rộng của ảnh</param>
        /// <param name="boxHeight">Chiều cao của ảnh</param>
        /// <param name="input">Chuỗi ký tự nhập vào</param>
        /// <param name="xpoint">Tọa độ các điểm trên trục X</param>
        /// <returns></returns>
        public static Bitmap CreateHistogram(string histogramType, int boxWidth, int boxHeight, string input)
        {
            try
            {
                var bmp = new Bitmap(boxWidth, boxHeight);
                var picFont = new Font("Arial", 8,FontStyle.Bold);
                var textFont = new Font("Arial", 7);
                Graphics g = Graphics.FromImage(bmp);
                const int axisGapBox = 20;
                // Kích thước của toàn bộ hình
                int picWidth = boxWidth - 2*axisGapBox;
                int picHeight = boxHeight - 2*axisGapBox;
                var rect = new Rectangle(0, 0, boxWidth - 1, boxHeight - 1);
                Pen blackPen = Pens.Black;
                Pen redPen = Pens.Red;

                // draw black background
                g.Clear(Color.White);
                g.DrawRectangle(blackPen, rect);

                int index = histogramType == "PLT" ? 14 : 0;
                Point[] points = (from Match m in Regex.Matches(input, "..")
                                  select new Point(index++, Convert.ToInt32(m.Value, 16))).ToArray();

                float drawingWidth = histogramType == "PLT" ? 150 : 350;

                float drawingHeight = points.Max(p => p.Y) - points.Min(p => p.Y);

                // Calculate the scale aspect we need to apply to points.
                float scaleX = picWidth/drawingWidth;
                float scaleY = picHeight/drawingHeight;

                var matrix = new Matrix();
                matrix.Scale(scaleX, scaleY);
                matrix.TransformPoints(points);

                // Vẽ các điểm 50,100,200,300
                Point[] xpoint = new Point[] {};
                if(histogramType=="WBC")
                    xpoint = new[] { new Point(50, 0), new Point(100, 0), new Point(200, 0), new Point(300, 0) };
                else if (histogramType == "RBC")
                    xpoint = new[] { new Point(80, 0), new Point(110, 0), new Point(200, 0), new Point(300, 0) };
                else if (histogramType == "PLT")
                    xpoint = new[] { new Point(2, 0), new Point(10, 0), new Point(20, 0), new Point(30, 0) };

                string[] keyArray = (from p in xpoint
                                     select p.X.ToString(CultureInfo.InvariantCulture)).ToArray();
                matrix.TransformPoints(xpoint);
                var xdic = new Dictionary<string, Point>();
                for (int i = 0; i < xpoint.Length; i++)
                    xdic.Add(keyArray[i], xpoint[i]);

                // Vẽ chữ tiêu đề
                string PicText = string.Format("{0} Histogram", histogramType);
                float TextPosition = (float) Math.Truncate(boxWidth - g.MeasureString(PicText, picFont).Width)/2 + 1;
                Brush blackBrush = Brushes.Black;
                g.DrawString(PicText, picFont, blackBrush, TextPosition, 1);
                g.DrawString("fL", picFont, blackBrush, boxWidth-15, boxHeight - 15);

                GraphicsContainer containerState = g.BeginContainer();
                // Đảo ngược trục Y
                g.ScaleTransform(1.0F, -1.0F);

                // Dịch chuyển gốc tọa độ
                g.TranslateTransform(axisGapBox, -(float) boxHeight + axisGapBox);

                //Vẽ các trục tọa độ
                g.DrawLine(blackPen, 0, 0, 0, picHeight);
                g.DrawLine(blackPen, 0, 0, picWidth, 0);

                // Vẽ đường gióng

                //foreach (var point in xdic)
                //{
                //    g.DrawLine(blackPen, point.Value.X, -3, point.Value.X, 3);
                //    g.ScaleTransform(1.0F, -1.0F);
                //    g.DrawString(point.Key, textFont, blackBrush, point.Value.X-10, 5);
                //    g.ScaleTransform(1.0F, -1.0F);
                //}
                //if(histogramType=="RBC")
                //{
                //    g.DrawLine(blackPen, xdic["80"].X, 0, xdic["80"].X, picHeight);   
                //    g.DrawLine(blackPen, xdic["110"].X, 0, xdic["110"].X, picHeight);   
                //}

                // Vẽ hình
                //g.TranslateTransform(20, 0);
                g.DrawCurve(redPen, points);

                g.EndContainer(containerState);
                return bmp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region String Utilities

        /// <summary>
        /// Hàm ngắt chuỗi thành các mảng nhỏ với ký tự bắt đầu và ký tự kết thúc biết trước
        /// </summary>
        /// <param name="rawStringData">Chuỗi truyền vào</param>
        /// <param name="beginChar">Ký tự bắt đầu</param>
        /// <param name="endChar">Ký tự kết thúc</param>
        /// <returns>Mảng dữ liệu trả ra</returns>
        public static string[] SeperatorRawData(string rawStringData, char beginChar, char endChar)
        {
            var allResult = new string[] {};
            try
            {
                bool isNewString = false;
                int id = -1;
                foreach (char chr in rawStringData)
                {
                    //Nếu gặp ký tự beginChar thì khởi tạo chuỗi mới
                    if (chr == beginChar)
                    {
                        id++;
                        Array.Resize(ref allResult, id + 1);
                        isNewString = true;
                    }

                    //Nếu gặp ký tự endChar thì kết thúc nhận chuỗi
                    if (chr == endChar)
                    {
                        allResult[id] += chr;
                        isNewString = false;
                    }

                    //Nếu là ký tự bình thường thì ghép vào chuỗi đang xử lý trong điều kiện isNewString=true
                    if (isNewString) allResult[id] += chr;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return allResult;
        }

        public static string[] DeleteAllBlankLine(IEnumerable<string> lines)
        {
            return DeleteAllBlankLine(lines, true);
        }

        /// <summary>
        /// Xóa các dòng trắng của dữ liệu
        /// </summary>
        /// <param name="lines">Mảng các chuỗi truyền vào</param>
        /// <param name="trim"> </param>
        /// <returns></returns>
        public static string[] DeleteAllBlankLine(IEnumerable<string> lines,bool trim)
        {
            string[] sReturn = null;
            int i = -1;
            foreach (string s in lines)
            {
                if (!String.IsNullOrEmpty(s.Trim()))
                {
                    i = i + 1;
                    Array.Resize(ref sReturn, i + 1);
                    sReturn[i] = trim ? s.Trim() : s;
                }
            }
            return sReturn;
        }

        /// <summary>
        /// Xóa các dòng trắng của dữ liệu
        /// </summary>
        /// <param name="pString">Chuỗi dữ liệu truyền vào</param>
        /// <param name="pSeparateChar">Ký tự ngăn cách dòng</param>
        /// <returns> string[] Dữ liệu sau khi xóa dòng trắng</returns>
        public static string[] DeleteAllBlankLine(string pString, char[] pSeparateChar)
        {
            string[] pTempString = DeleteAllBlankLine(pString.Split(pSeparateChar));
            return pTempString;
        }

        public static string[] DeleteAllBlankLine(string pString, char pSeparateChar)
        {
            string[] pTempString = DeleteAllBlankLine(pString.Split(pSeparateChar));
            return pTempString;
        }

        public static string[] DeleteAllBlankLine(string pString, string pSeparateChar)
        {
            string[] pTempString = DeleteAllBlankLine(pString.Split(pSeparateChar.ToCharArray()));
            return pTempString;
        }

        public static string[] DeleteAllBlankLine(string pString, string pSeparateChar,bool pTrim)
        {
            string[] pTempString = DeleteAllBlankLine(pString.Split(pSeparateChar.ToCharArray()),pTrim);
            return pTempString;
        }

        /// <summary>
        /// Hàm bỏ dấu tiếng Việt
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetUnsignString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }

        #endregion

        #endregion
    }
}