using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class Urisys1100 : Rs232Devcie
    {
        private Boolean ValidData()
        {
            try
            {
                return StringData.Contains("....................");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override void ProcessRawData()
        {
            try
            {
                if (!ValidData()) return;
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(StringData);

                string[] result = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF.ToCharArray());
                TestResult.TestDate = result[3].Split()[0].Replace('.', '/');
                TestResult.Barcode = result[1].Split()[result[1].Split().Length - 1].Trim();
                Log.Trace(string.Format("Barcode: {0}", TestResult.Barcode));
                AddResult(new ResultItem(result[4].Substring(0, 5).Replace("*","").Trim(), GetResult(result[4])));
                AddResult(new ResultItem(result[5].Substring(0, 5).Replace("*","").Trim(), GetResult(result[5])));
                AddResult(new ResultItem(result[6].Substring(0, 5).Replace("*","").Trim(), GetResult(result[6])));
                AddResult(new ResultItem(result[7].Substring(0, 5).Replace("*","").Trim(), GetResult(result[7])));
                AddResult(new ResultItem(result[8].Substring(0, 5).Replace("*","").Trim(), GetResult(result[8])));
                AddResult(new ResultItem(result[9].Substring(0, 5).Replace("*","").Trim(), GetResult(result[9])));
                AddResult(new ResultItem(result[10].Substring(0, 5).Replace("*","").Trim(), GetResult(result[10])));
                AddResult(new ResultItem(result[11].Substring(0, 5).Replace("*","").Trim(), GetResult(result[11])));
                AddResult(new ResultItem(result[12].Substring(0, 5).Replace("*","").Trim(), GetResult(result[12])));
                AddResult(new ResultItem(result[13].Substring(0, 5).Replace("*","").Trim(), GetResult(result[13])));
                Log.Trace("Begin Import Result");
                Log.Trace(ImportResults() ? "Import Result Success" : "Error While Import result");
                
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
        
        private string GetResult(string rawData)
        {
            try
            {
                var result = string.Empty;
                var tempStr = rawData.Substring(rawData.IndexOf(" ")).Trim();
                var strings = tempStr.Split(" ".ToCharArray());
                if (strings.Length==1) result = strings[0];
                else
                {
                    for (int i = 0; i <= strings.Length-2; i++)
                    {
                        if (i == 0) result = strings[i] + " ";
                        else result = result + strings[i];
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return rawData.Substring(4).Trim();
            }
        }
    }
}