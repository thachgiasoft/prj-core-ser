using System;
using System.Collections;
using System.Globalization;
using Vietbait.Lablink.TestResult;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class Kx21 : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                var myArr = new ArrayList { "WBC", "RBC", "HGB", "HCT", "MCV", "MCH", "MCHC", "PLT", "LYM%", "MXD%", "NEUT%", "LYM#", "MXD#", "NEUT#", "RDW", "UNUSE", "PDW", "MPV", "P-LCR" };

                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(StringData);
                var idStx = StringData.IndexOf(Utilities.DeviceHelper.STX);

                if ((StringData.Length < 457) || (idStx < 0)) return;

                var allPatients = SeparatorData(StringData.Substring(idStx));
                Log.Debug("Result has {0} patient(s)" ,allPatients.Length);

                foreach (var patient in allPatients)
                {
                    var tempdate = patient.Substring(1, 8).Split('/');
                    TestResult.TestDate = string.Format("{0}/{1}/{2}{3}", tempdate[2].Trim().PadLeft(2, '0'),
                                                        tempdate[1].Trim().PadLeft(2, '0'),
                                                        DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring(0, 2), 
                                                        tempdate[0].Trim().PadLeft(2, '0'));
                    TestResult.Barcode = patient.Substring(14,13).Trim();

                    var tempResult = patient.Substring(82, 120);
                    foreach (string testName in myArr)
                    {
                        string testValue = tempResult.Substring(0, 5);
                        tempResult = tempResult.Substring(5);
                        if (testName != "UNUSE") AddResult(new ResultItem(testName,testValue));
                    }

                    Log.Debug("Begin Import Result");
                    ImportResults();
                    Log.Debug("Import Result Success");
                }
                Log.Trace("Finish Process Data");
                ClearData();
            }
            catch (Exception ex)
            {
                Log.Error("Error while process data Error:" + ex.ToString());
            }
            finally
            {
                //ClearData();
            }
        }

        #endregion

        /// <summary>
        /// Ngắt chuỗi kết quả thành các kết quả riêng biệt
        /// </summary>
        /// <param name="stringData">chuỗi dữ liệu truyền vào</param>
        /// <returns>Mảng các chuỗi trả về</returns>
        private string[] SeparatorData(string stringData)
        {
            try
            {
                //Ngắt chuỗi theo ký tự CR
                var rawdata = stringData;
                
                var allResult = new string[] { };

                //Biến lưu Index
                var id = 0;

                const int resultLeng = 457;
                while (rawdata.Length>=resultLeng)
                {
                    id++;
                    Array.Resize(ref allResult, id);
                    allResult[id - 1] = rawdata.Substring(0, resultLeng);
                    rawdata = rawdata.Substring(resultLeng);
                }
                return allResult;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}