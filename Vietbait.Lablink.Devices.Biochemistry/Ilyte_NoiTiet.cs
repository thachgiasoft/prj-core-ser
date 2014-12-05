using System;
using Vietbait.Lablink.Devices;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Ilyte_NoiTiet : Rs232Base
    {
        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                Log.Debug("Raw Data: {0}{1}", DeviceHelper.CRLF, StringData);
                if (!StringData.EndsWith(string.Format("{0}{0}", DeviceHelper.CRLF))) return;
                string[] strResutl = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF.ToCharArray());

                //TestResult.TestDate = strResutl[5].Substring(0, 9).Trim();
                string pTempTestDate = strResutl[strResutl.Length - 1].Substring(0, 9).Trim();
                string[] tmpDate = pTempTestDate.Split('-');
                string pTestDate = tmpDate[0];
                pTestDate = GetMonth(pTestDate);

                TestResult.TestDate = string.Format("{0}/{1}/20{2}", tmpDate[1], pTestDate, tmpDate[2]);

                //Tìm chuỗi ID# - Nếu không có => Barcode = ""
                var iDrowNumber = GetRowIndex(strResutl, "ID#");
                // Nếu kô tìm thấy => Barcode = "" nếu có lấy ID
                if (iDrowNumber == -1)
                {
                    TestResult.Barcode = "";
                    iDrowNumber = GetRowIndex(strResutl, "SAMPLE");
                    if (iDrowNumber != -1) TestResult.TestSequence = strResutl[iDrowNumber].Substring(6).Trim();
                }
                else
                {
                    TestResult.Barcode = strResutl[iDrowNumber].Substring(3).Trim();
                    
                }

                // Trường hợp có kết quả thì xử lý
                if(iDrowNumber!=-1)
                {
                    // Nếu dòng thứ 2 có chứa CL thì lấy kết quả 1 dòng
                    if(strResutl[iDrowNumber+1].IndexOf("Cl")>0)
                    {
                        string temResult = strResutl[iDrowNumber + 1];
                        AddResult(new ResultItem(temResult.Substring(0, 2).Trim(), temResult.Substring(2, 6).Trim()));
                        AddResult(new ResultItem(temResult.Substring(9, 2).Trim(), temResult.Substring(11, 6).Trim()));
                        AddResult(new ResultItem(temResult.Substring(17, 2).Trim(), temResult.Substring(19).Trim()));
                    }
                    else
                    {
                        string temResult = strResutl[iDrowNumber + 1];
                        AddResult(new ResultItem(temResult.Substring(0, 2).Trim(), temResult.Substring(2, 6).Trim()));
                        AddResult(new ResultItem(temResult.Substring(9, 2).Trim(), temResult.Substring(11).Trim()));
                        temResult = strResutl[iDrowNumber + 2];
                        AddResult(new ResultItem(temResult.Substring(0, 2).Trim(), temResult.Substring(2, 5).Trim()));
                        //AddResult(new ResultItem(temResult.Substring(8, 2).Trim(), temResult.Substring(2, 6).Trim()));
                    }
                    
                }

                Log.Debug("Begin Import Result");
                Log.Debug(ImportResults()?"Import Result success":"Error While Import Result");
                ClearData();
            }
            catch (Exception)
            {
                //throw ex;
                Log.Error("Co loi khi xu ly du lieu");
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

        private string GetMonth(string pMonth)
        {
            switch (pMonth)
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
    }
}