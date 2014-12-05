using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class CelltaxF : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace("RawData:\n{0}",StringData);
                //Kiểm tra nếu là kí tự STX thì nhận dữ liệu
                if (!(StringData.StartsWith(DeviceHelper.STX.ToString()) && StringData.EndsWith(DeviceHelper.ETX.ToString())))
                {
                    Log.Trace("Dữ liệu không hợp lệ");
                    return;
                }

                var rawData = StringData.Split(DeviceHelper.CR);

                //Lấy ngày:
                TestResult.TestDate = string.Format("{0}/{1}/{2}", rawData[17], rawData[16], rawData[15]);
                
                //Lấy barcode:
                TestResult.Barcode = rawData[22].Trim();

                //Add Kết quả:
                AddResult(new ResultItem("WBC", rawData[23].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("NE%", rawData[24].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("LY%", rawData[25].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("MO%", rawData[26].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("EO%", rawData[27].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("BA%", rawData[28].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("NE", rawData[29].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("LY", rawData[30].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("MO", rawData[31].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("EO", rawData[32].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("BA", rawData[33].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("RBC", rawData[34].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("HGB", rawData[35].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("HCT", rawData[36].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("MCV", rawData[37].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("MCH", rawData[38].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("MCHC", rawData[39].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("RDW", rawData[40].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("PLT", rawData[41].Replace("*","").Replace("H","").Replace("L","").Trim()));
                AddResult(new ResultItem("MPV", rawData[43].Replace("*","").Replace("H","").Replace("L","").Trim()));

                Log.Trace("Begin Import Result");
                Log.Trace(ImportResults() ? "Import Result Success" : "Error while import result");

            }
            catch (Exception ex)
            {
                Log.Error(string.Format ("Error while process data {0}",ex));
            }
            finally
            {
                ClearData();
            }
        }

        #endregion
    }
}