using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class Advia60_DM : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                //Kiểm tra ký tự STX khi kết thúc nhận dữ liệu.
                if (StringData.IndexOf(DeviceHelper.ETX) < 0) return;
                string[] strResutl = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CR.ToString());

                TestResult.TestSequence = strResutl[0];
                TestResult.TestDate = strResutl[3].Substring(2, 8).Trim();
                TestResult.TestDate = string.Format("{0}/{1}/20{2}", TestResult.TestDate.Substring(0, 2),
                                                    TestResult.TestDate.Substring(3, 2),
                                                    TestResult.TestDate.Substring(6, 2));

                //Barcode

                TestResult.Barcode = Convert.ToInt32(strResutl[4].Substring(2)).ToString();
                //  TestResult.Barcode = strResutl[6].Trim().Length < 2 ? "0" : strResutl[6].Substring(2);

                //Add Items

                AddResult(new ResultItem("WBC", strResutl[9].Substring(2, 5).Trim()));

                AddResult(new ResultItem("RBC", strResutl[10].Substring(2, 5).Trim()));

                AddResult(new ResultItem("HGB", strResutl[11].Substring(2, 5).Trim()));

                AddResult(new ResultItem("HTC", strResutl[12].Substring(2, 5).Trim()));

                AddResult(new ResultItem("MCV", strResutl[13].Substring(2, 5).Trim()));

                AddResult(new ResultItem("MCH", strResutl[14].Substring(2, 5).Trim()));

                AddResult(new ResultItem("MCHC", strResutl[15].Substring(2, 5).Trim()));

                AddResult(new ResultItem("RDW", strResutl[16].Substring(2, 5).Trim()));

                AddResult(new ResultItem("PLT", strResutl[17].Substring(2, 5).Trim()));

                AddResult(new ResultItem("MPV", strResutl[18].Substring(2, 5).Trim()));

                AddResult(new ResultItem("PCT", strResutl[19].Substring(2, 5).Trim()));

                AddResult(new ResultItem("PDW", strResutl[20].Substring(2, 5).Trim()));

                AddResult(new ResultItem("%LYM", strResutl[21].Substring(2, 5).Trim()));

                AddResult(new ResultItem("%MON", strResutl[22].Substring(2, 5).Trim()));

                AddResult(new ResultItem("%GRA", strResutl[23].Substring(2, 5).Trim()));

                AddResult(new ResultItem("#LYM", strResutl[24].Substring(2, 5).Trim()));

                AddResult(new ResultItem("#MON", strResutl[25].Substring(2, 5).Trim()));

                AddResult(new ResultItem("#GRA", strResutl[23].Substring(2, 5).Trim()));

                ImportResults();

                ClearData();
            }
            catch (Exception)
            {
                ClearData();
                throw;
            }
        }

        #endregion
    }
}