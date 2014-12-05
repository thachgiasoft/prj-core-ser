using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class ILYTENHTD : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                string[] strResutl = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF.ToCharArray());
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                //Lưu lại Data
                Log.Trace(DeviceHelper.CRLF + StringData);

                TestResult.TestDate = strResutl[strResutl.Length-1].Substring(0, 9).Trim();
                string pTestDate = strResutl[strResutl.Length-1].Substring(0, 3);
                pTestDate = GetMonth(pTestDate);
                TestResult.TestDate = string.Format("{0}/{1}/{3}{2}", TestResult.TestDate.Substring(4, 2), pTestDate,
                                                    TestResult.TestDate.Substring(7, 2),DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring(0,2));

                var barcode = strResutl[2].IndexOf("ID#", StringComparison.Ordinal) > 0 ? strResutl[2].Split()[1] : "0000";
                TestResult.Barcode = barcode;


                if (TestResult.Barcode.Equals("")) TestResult.Barcode = "0000";
                
                var temResult = DeviceHelper.DeleteAllBlankLine(strResutl[3], ' ');
                AddResult(new ResultItem(temResult[0], temResult[1].Trim()));
                AddResult(new ResultItem(temResult[2], temResult[3].Trim()));
                AddResult(new ResultItem(temResult[4], temResult[5].Trim()));

                Log.Debug("Barcode:{0} NA:{1} K:{2} CL:{3}", TestResult.Barcode,
                          temResult[1].Trim(), temResult[3].Trim(),
                          temResult[5].Trim());

                Log.Debug("Begin Importdata");
                ImportResults();
                Log.Debug("Finish Imported result");
                ClearData();

                //  }
            }
            catch (Exception ex)
            {
                //throw ex;
                Log.Error("Error while processing data Error: {0}",ex);
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