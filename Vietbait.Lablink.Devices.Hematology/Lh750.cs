using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class LH750 : Rs232AstmDeviceLh
    //internal class LH750 : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace(StringData);
                string[] tempStringArr;

                string[] strResult = ReformatData(StringData).Split(DeviceHelper.CR);

                //Lấy ngày:
                int rowIndex = GetRowIndex(strResult, "DATE");
                if (rowIndex > -1)
                {
                    tempStringArr = strResult[rowIndex].Split();
                    TestResult.TestDate = tempStringArr.Length < 2
                                              ? DateTime.Now.ToString("dd/MM/yyyy")
                                              : (tempStringArr[1].Trim() == ""
                                                     ? DateTime.Now.ToString("dd/MM/yyyy")
                                                     : tempStringArr[1].Trim());
                }
                else
                {
                    TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                }

                //Lấy TestID:
                rowIndex = GetRowIndex(strResult, "ID1");
                if (rowIndex>-1)
                {
                    tempStringArr = strResult[rowIndex].Split();
                    TestResult.Barcode = tempStringArr.Length < 2
                                              ? "0000"
                                              : (tempStringArr[1].Trim() == ""
                                                     ? "0000"
                                                     : tempStringArr[1].Trim()); 
                }
                else
                {
                    TestResult.Barcode = "0000";
                }

                //Lấy các kết quả.
                //Khai báo mảng kết quả.
                var arrResultName = new[]
                                             {
                                                 "WBC", "RBC", "HGB", "HCT", "MCV", "MCH", "MCHC", "RDW", "PLT", "MPV",
                                                 "LY#", "MO#", "NE#", "EO#", "BA#", "NRBC#", "LY%", "MO%", "NE%", "EO%",
                                                 "BA%", "NRBC%"
                                             };
                //Lấy kết quả và lưu vào mảng.
                foreach (string sResultName in arrResultName)
                {
                    int indexResult = GetRowIndex(strResult, sResultName);
                    if (indexResult>-1)
                    {
                        tempStringArr = strResult[indexResult].Split();
                        string testValue = tempStringArr.Length < 2
                                              ? ""
                                              : (tempStringArr[1].Trim() == ""
                                                     ? ""
                                                     : tempStringArr[1].Trim());
                        AddResult(new ResultItem(sResultName, testValue)); 
                    }
                }
                
                Log.Debug("Import Result for barcode:" + TestResult.Barcode);
                ImportResults();
                Log.Debug("Import Result Success");
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception while processing data:{0}", ex));
            }
            finally
            {
                ClearData();
            }
        }

        /// <summary>
        /// Chuẩn hóa lại dữ liệu. Xóa bỏ các dòng trắng, xóa bỏ các dòng bắt đầu bằng ký tự DC1
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private string ReformatData(string inputString)
        {
            //Ngắt chuỗi theo ký tự CR
            string[] arrStringData = DeviceHelper.DeleteAllBlankLine(inputString, DeviceHelper.CRLF);
            string allResult = string.Empty;

            if (arrStringData == null) return "";
            //Biến lưu Index

            foreach (string line in arrStringData)
                if ((line.Trim() != "") && (!line.StartsWith(DeviceHelper.DC1.ToString(CultureInfo.InvariantCulture))) && (!line.StartsWith("-")))
                    allResult = allResult + line + DeviceHelper.CR;
            return allResult;
        }

        /// <summary>
        /// Trả về index của dòng với chuỗ bắt đầu xác định
        /// </summary>
        /// <param name="arrString">Mảng cần tìm Index</param>
        /// <param name="beginString">Chuỗi bắt đầu</param>
        /// <returns></returns>
        private int GetRowIndex(string[] arrString,string beginString)
        {
            try
            {
                var result = -1;
                for (var i = 0; i < arrString.Length; i++)
                    if (arrString[i].StartsWith(beginString))
                    {
                        result = i;
                        break;
                    }
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion
    }
}