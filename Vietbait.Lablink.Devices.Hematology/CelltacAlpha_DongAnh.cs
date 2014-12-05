using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class CelltacAlpha_DongAnh : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                string pTestDate, pday;
                string pMonth = new string[] {}.ToString();
                //Kiểm tra nếu chưa kết thúc nhận dữ liệu
                if (!StringData.EndsWith(DeviceHelper.ETX.ToString(CultureInfo.InvariantCulture))) return;

                Log.Trace("Begin Process Data");
                Log.Trace(DeviceHelper.CRLF + StringData);

                string[] strResult = StringData.Split(DeviceHelper.CR);
                TestResult.TestDate = strResult[0].Substring(1, 9).Trim();
                pMonth = strResult[0].Substring(4, 3);
                //pTestDate = GetMonth(pMonth);
                pTestDate = DeviceHelper.GetMonth(pMonth);
                TestResult.TestDate = string.Format("{0}/{1}/20{2}", TestResult.TestDate.Substring(0, 2), pTestDate,
                                                    TestResult.TestDate.Substring(7, 2));
                TestResult.Barcode = strResult[1].Trim();
                if (TestResult.Barcode.Equals("")) TestResult.Barcode = "0";
                AddResult(new ResultItem("WBC", strResult[2].Replace("L", "").Replace("H", "").Trim(),"10^9/L"));
                AddResult(new ResultItem("RBC", strResult[10].Replace("L", "").Replace("H", "").Trim(), "10^12/L"));
                AddResult(new ResultItem("HGB", strResult[11].Replace("L", "").Replace("H", "").Trim(), "g/L"));
                AddResult(new ResultItem("HCT", strResult[12].Replace("L", "").Replace("H", "").Trim(), "%"));
                AddResult(new ResultItem("MCV", strResult[13].Replace("L", "").Replace("H", "").Trim(), "fL"));
                AddResult(new ResultItem("MCH", strResult[14].Replace("L", "").Replace("H", "").Trim(), "Pg"));
                AddResult(new ResultItem("MCHC", strResult[15].Replace("L", "").Replace("H", "").Trim(),"g/L"));
                AddResult(new ResultItem("PLT", strResult[17].Replace("L", "").Replace("H", "").Trim(), "10^3/uL"));
                AddResult(new ResultItem("%LY", strResult[3].Replace("L", "").Replace("H", "").Trim(), "%"));
                AddResult(new ResultItem("%MO", strResult[4].Replace("L", "").Replace("H", "").Trim(), "%"));
                AddResult(new ResultItem("%GR", strResult[5].Replace("L", "").Replace("H", "").Trim(), "%"));
                AddResult(new ResultItem("#LY", strResult[6].Replace("L", "").Replace("H", "").Trim(), "10^9/L"));
                AddResult(new ResultItem("#MO", strResult[7].Replace("L", "").Replace("H", "").Trim(), "10^9/L"));
                AddResult(new ResultItem("#GR", strResult[8].Replace("L", "").Replace("H", "").Trim(), "10^9/L"));
                AddResult(new ResultItem("#EO", strResult[9].Replace("L", "").Replace("H", "").Trim(), "10^9/L"));
                AddResult(new ResultItem("RDW", strResult[16].Replace("L", "").Replace("H", "").Trim(), "%CV"));
                AddResult(new ResultItem("PCT", strResult[18].Replace("L", "").Replace("H", "").Trim(), "%"));
                AddResult(new ResultItem("MPV", strResult[19].Replace("L", "").Replace("H", "").Trim(), "fL"));
                AddResult(new ResultItem("PDW", strResult[20].Replace("L", "").Replace("H", "").Trim(), "%"));
                ImportResults();
                Log.Debug("Import Result Success For barocde:{0}", TestResult.Barcode);
            }
            catch (Exception ex)
            {
                //File.AppendAllText(@"C:\NIHONKODEN-ERROR.txt", ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }
    }
}