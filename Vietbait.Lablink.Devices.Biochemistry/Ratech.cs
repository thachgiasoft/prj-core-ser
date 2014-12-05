using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Ratech: Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                Log.Trace(DeviceHelper.CRLF + StringData);
                string rawResult = StringData.Substring(StringData.IndexOf("SAMPLE RESULTS", StringComparison.Ordinal));
                if ((rawResult.IndexOf("Na")<0)||(rawResult.IndexOf("K")<0)||(rawResult.IndexOf("Cl")<0))
                {
                    Log.Trace("Data is not valided");
                    return;
                }
                var strResutl = DeviceHelper.DeleteAllBlankLine(rawResult, DeviceHelper.CRLF.ToCharArray());
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                //Lưu lại Data

                TestResult.TestDate = strResutl[strResutl.Length-1].Substring(0, 9).Trim();
                string pTestDate = strResutl[strResutl.Length-1].Substring(0, 3);
                pTestDate = DeviceHelper.GetMonth(pTestDate);
                TestResult.TestDate = string.Format("{0}/{1}/{3}{2}", TestResult.TestDate.Substring(4, 2), pTestDate,
                                                    TestResult.TestDate.Substring(7, 2),DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring(0,2));

                //Lấy về dòng chứa ID
                var tempRowIndex = GetRowIndex(strResutl,"ID");
                if (tempRowIndex == -1) TestResult.Barcode = "0000";
                else
                {
                    var tempId = strResutl[tempRowIndex].Split();
                    TestResult.Barcode = tempId.Length>=2 ? Convert.ToInt32(tempId[1]).ToString() : "0000";
                }
                //Tìm đến phần tử đầu tien chứa kết quả.
                tempRowIndex = GetRowIndex(strResutl, "Na");
                if(tempRowIndex != -1)
                {
                    var tempResult = DeviceHelper.DeleteAllBlankLine(strResutl[tempRowIndex].Split());
                    AddResult(new ResultItem(tempResult[0].Replace("\"","").Replace("^","").Trim(),tempResult[1].Trim(),tempResult[2].Trim()));
                    tempResult = DeviceHelper.DeleteAllBlankLine(strResutl[tempRowIndex+1].Split());
                    AddResult(new ResultItem(tempResult[0].Replace("\"", "").Replace("^", "").Trim(), tempResult[1].Trim(), tempResult[2].Trim()));
                    tempResult = DeviceHelper.DeleteAllBlankLine(strResutl[tempRowIndex+2].Split());
                    AddResult(new ResultItem(tempResult[0].Replace("\"", "").Replace("^", "").Trim(), tempResult[1].Trim(), tempResult[2].Trim()));
                }

                Log.Debug("Begin Importdata");
                Log.Debug(ImportResults()?"Finish Imported result":"Error While Import Result to DB");
                ClearData();

                //  }
            }
            catch (Exception ex)
            {
                //throw ex;
                Log.Error("Error while processing data Error: {0}",ex);
                ClearData();
            }
        }
        /// <summary>
        /// Trả về index của dòng với chuỗ bắt đầu xác định
        /// </summary>
        /// <param name="arrString">Mảng cần tìm Index</param>
        /// <param name="beginString">Chuỗi bắt đầu</param>
        /// <returns></returns>
        private int GetRowIndex(string[] arrString, string beginString)
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
    }
}