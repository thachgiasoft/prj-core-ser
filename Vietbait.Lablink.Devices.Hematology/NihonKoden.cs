using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class NihonKoden : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                //Kiểm tra nếu chưa kết thúc nhận dữ liệu
                // if(!StringData.EndsWith(DeviceHelper.ETX.ToString())) return;
                if (StringData.IndexOf(DeviceHelper.ETX) < 0) return;
                string[] strResult = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CR.ToString());
                TestResult.Barcode = strResult[15];
                TestResult.TestDate = string.Format("{0}/{1}/{2}", strResult[11], strResult[10], strResult[9]);

                ResultItem item;

                item = new ResultItem("WBC", strResult[16]);
                AddResult(item);

                item = new ResultItem("RBC", strResult[17]);
                AddResult(item);

                item = new ResultItem("HGB", strResult[18]);
                AddResult(item);

                item = new ResultItem("HCT", strResult[19]);
                AddResult(item);

                item = new ResultItem("MCV", strResult[20]);
                AddResult(item);

                item = new ResultItem("MCH", strResult[21]);
                AddResult(item);

                item = new ResultItem("MCHC", strResult[22]);
                AddResult(item);

                item = new ResultItem("PLT", strResult[24]);
                AddResult(item);

                item = new ResultItem("LY", strResult[28]);
                AddResult(item);

                item = new ResultItem("MO", strResult[29]);
                AddResult(item);

                item = new ResultItem("GR", strResult[30]);
                AddResult(item);

                item = new ResultItem("RDW", strResult[23]);
                AddResult(item);

                item = new ResultItem("PCT", strResult[25]);
                AddResult(item);

                item = new ResultItem("MPV", strResult[26]);
                AddResult(item);

                item = new ResultItem("PDW", strResult[27]);
                AddResult(item);
                ImportResults();
                ClearData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}