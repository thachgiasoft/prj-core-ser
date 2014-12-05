using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class CelltaxE : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(StringData);
                //Kiểm tra nếu là kí tự STX thì nhận dữ liệu
                if (StringData.StartsWith(DeviceHelper.STX.ToString(CultureInfo.InvariantCulture)) && StringData.Length >= 0 &&
                    String.IsNullOrEmpty(StringData))
                {
                    Log.Trace("Dữ liệu không hợp lệ");
                    return;
                }

                var arrPatients = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.STX.ToString(CultureInfo.InvariantCulture));

                foreach (var record in arrPatients)
                {
                    var result = record.Split(DeviceHelper.CR);
                    result = DeviceHelper.DeleteAllBlankLine(result);
                    //lay tháng
                    var pMonth = arrPatients[0].Substring(3, 3);
                    pMonth = GetMonth(pMonth);
                    //lấy ngày
                    var pday = result[0].Substring(0, 2);
                    //năm
                    var pYear = result[0].Substring(7, 2);
                   
                    // lấy ngày tháng
                    TestResult.TestDate = string.Format("{0}/{1}/{2}", pday, pMonth,
                                                        string.Format("{0}{1}", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring( 0,2), pYear));
                    
                    //lấy barcode
                   TestResult.Barcode = result[1].Trim().Split()[0].Length < 1 ? "0" : result[1].Trim().Split()[0];

                   Log.Trace(string.Format("Barcode: {0}", TestResult.Barcode));
                    //add result
                    AddResult(new ResultItem("WBC", result[3].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("RBC", result[14].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    var hgb = result[15].Replace("L", "").Replace("H", "").Trim().Replace("*", "");
                    try
                    {
                        hgb = (Convert.ToDouble(hgb)*10).ToString(CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                    }
                    AddResult(new ResultItem("HGB", hgb));
                    AddResult(new ResultItem("HCT", result[16].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("MCV", result[17].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("MCH", result[18].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    string mchc = result[19].Replace("L", "").Replace("H", "").Trim().Replace("*", "");
                    try
                    {
                        mchc = (Convert.ToDouble(mchc)*10).ToString(CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                    }
                    AddResult(new ResultItem("MCHC", mchc));
                    AddResult(new ResultItem("PLT", result[21].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("%NE", result[6].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("%LY", result[4].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("%MO", result[5].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("%EO", result[7].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("%BA", result[8].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("#NE", result[11].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("#LY", result[9].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("#MO", result[10].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("#EO", result[12].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("#BA", result[13].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("RDW", result[20].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    AddResult(new ResultItem("MPV", result[23].Replace("L", "").Replace("H", "").Trim().Replace("*", "")));
                    ImportResults();
                    Log.Trace("Finish Import Result");
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format ("{0} /n {1}", "Lỗi Trong quá trình xử lý dữ liệu",ex));
            }
            finally
            {
                ClearData();
            }
        }

        #endregion

        #region GetMonth

        private static string GetMonth(string pMonth)
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

        #endregion
    }
}