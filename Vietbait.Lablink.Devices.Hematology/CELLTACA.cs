using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class CELLTACA : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace(StringData);

                string pTestDate;
                string pMonth = new string[] {}.ToString();
                //Kiểm tra nếu chưa kết thúc nhận dữ liệu
                if (!StringData.EndsWith(DeviceHelper.ETX.ToString(CultureInfo.InvariantCulture))) return;
                string[] strResult = StringData.Split(DeviceHelper.CR);
                TestResult.TestDate = strResult[0].Substring(1, 9).Trim();
                pMonth = strResult[0].Substring(4, 3);
                pTestDate = GetMonth(pMonth);
                TestResult.TestDate = string.Format("{0}/{1}/{2}{3}", TestResult.TestDate.Substring(0, 2), pTestDate,
                                                    DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring(0, 2),
                                                    TestResult.TestDate.Substring(7, 2));
                TestResult.Barcode = strResult[1].Trim();
                if (TestResult.Barcode.Equals("")) TestResult.Barcode = "0";
                AddResult(new ResultItem("WBC", strResult[2].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("RBC", strResult[9].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("HGB", strResult[10].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("HCT", strResult[11].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("MCV", strResult[12].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("MCH", strResult[13].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("MCHC", strResult[14].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("PLT", strResult[16].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("%LY", strResult[3].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("%MO", strResult[4].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("%GR", strResult[5].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("#LY", strResult[6].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("#MO", strResult[7].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("#GR", strResult[8].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("RDW", strResult[15].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("PCT", strResult[17].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("MPV", strResult[18].Replace("L", "").Replace("H", "").Trim()));
                AddResult(new ResultItem("PDW", strResult[19].Replace("L", "").Replace("H", "").Trim()));
                Log.Debug("Import Result for barcode:" + TestResult.Barcode);
                ImportResults();
                Log.Debug("Import Result Success");
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0} /n {1}", "Lỗi Trong quá trình xử lý dữ liệu", ex));
            }
            finally
            {
                ClearData();
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