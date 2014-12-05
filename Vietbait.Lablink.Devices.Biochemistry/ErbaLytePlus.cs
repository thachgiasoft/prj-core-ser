using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class ErbaLytePlus : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            Log.Trace("Begin Process Data");

            //Lưu lại Data
            Log.Trace(StringData);

            try
            {
                if (!StringData.EndsWith(DeviceHelper.CRLF)) return;
                string[] allPatient = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF);

                foreach (string patient in allPatient)
                {
                    string[] result = DeviceHelper.DeleteAllBlankLine(patient, ' ');
                    TestResult.TestSequence = result[0];
                    TestResult.Barcode = Convert.ToInt32(result[1]).ToString(CultureInfo.InvariantCulture);
                    TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                    AddResult(new ResultItem("K", result[3]));
                    AddResult(new ResultItem("Na", result[4]));
                    AddResult(new ResultItem("Cl", result[5]));
                    double iCa = -999999;
                    try
                    {
                        iCa = Convert.ToDouble(result[6]);
                    }
                    catch (Exception)
                    {
                        iCa = -999999;
                    }

                    AddResult(new ResultItem("iCa", result[6]));
                    AddResult(new ResultItem("PH", result[7]));
                    if (iCa != -999999)
                    {
                        AddResult(new ResultItem("TCa", Math.Round(iCa * 1.953,2).ToString(CultureInfo.InvariantCulture)));
                    }

                    Log.Debug("Import Result for barcode:" + TestResult.Barcode);
                    ImportResults();
                    Log.Debug("Import Result Success");
                }
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