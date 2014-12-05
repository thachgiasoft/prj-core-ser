using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class HM1060 : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace(StringData);

                string data = StringData.Remove(380);
                string[] strResult = DeviceHelper.DeleteAllBlankLine(data.Split(','));

                //lay tháng
                string pMonth = strResult[2].Substring(5, 2).Trim();
                //lấy ngày
                string pday = strResult[2].Substring(8, 2).Trim();
                //năm
                string pYear = strResult[2].Substring(0, 4).Trim();
                // lấy ngày tháng nắm
                TestResult.TestDate = string.Format("{0}/{1}/{2}", pday, pMonth, pYear);
                //lấy barcode
                TestResult.Barcode = strResult[4].Trim().Length < 1 ? "0" : strResult[4].Trim();

                int j = -1;
                for (j = 0; j < strResult .Length ; j++)
                {
                    if (strResult [j].StartsWith( "WBC")) break;
                }

                for (int i = j; i < 67; i = i + 3)
                {
                    //add result
                    var item = new ResultItem(strResult[i].Trim(), strResult[i + 1].Split(' ')[0].Trim(),
                                                  strResult[i + 2].Trim());
                    AddResult(item);
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

        #endregion
    }
}