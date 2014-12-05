using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class Clinitekadv : Rs232Devcie
    {
        private Boolean ValidData()
        {
            try
            {
                if (!StringData.StartsWith(DeviceHelper.STX.ToString(CultureInfo.InvariantCulture)) &&
                    StringData.Length <= 8 && String.IsNullOrEmpty(StringData)) return false;
                if (StringData.Length <= 8) return false;
                return !String.IsNullOrEmpty(StringData);
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

                var arrPatients = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.STX.ToString(CultureInfo.InvariantCulture));
                foreach (var record in arrPatients)
                {
                    if (record=="?")
                    {
                        continue;
                    }
                    var result = record.Split(DeviceHelper.CRLF.ToCharArray());
                    result = DeviceHelper.DeleteAllBlankLine(result);
                    var pTestdate = result[0].Split('#')[1].Substring(result[0].Length - 9).Replace('-', '/');
                    TestResult.TestDate = pTestdate.Insert(pTestdate.Length - 2, "20");
                    var pBarcode = result[1].Substring(result[1].IndexOf('=') + 1).Length < 0
                                          ? "0"
                                          : result[1].Substring(result[1].IndexOf('=') + 1);
                    TestResult.Barcode = pBarcode;
                    Log.Trace(string.Format("Barcode: {0}", pBarcode));
                    AddResult(new ResultItem(result[4].Substring(0, 3).Trim(), GetResult(result[4])));
                    AddResult(new ResultItem(result[5].Substring(0, 3).Trim(), GetResult(result[5])));
                    AddResult(new ResultItem(result[6].Substring(0, 3).Trim(), GetResult(result[6])));
                    AddResult(new ResultItem(result[7].Substring(0, 3).Trim(), GetResult(result[7])));
                    AddResult(new ResultItem(result[8].Substring(0, 3).Trim(), GetResult(result[8])));
                    //xét trường hợp thông số BLD máy trả ra kết quả là MODERATE khi in ra kết quả khác là 80
                    var bld = result[12].Substring(4).Trim();
                    //try
                    //{
                    //    bld = bld=="MODERATE" ? "80" : result[12].Substring(4).Trim();
                    //}
                    //catch (Exception)
                    //{
                    //    bld = result[12].Substring(4).Trim();
                    //}
                    AddResult(new ResultItem(result[9].Substring(0, 3).Trim(), GetResult(result[9])));
                    AddResult(new ResultItem(result[10].Substring(0, 3).Trim(), GetResult(result[10])));
                    AddResult(new ResultItem(result[11].Substring(0, 3).Trim(), GetResult(result[11])));
                    AddResult(new ResultItem(result[12].Substring(0, 3).Trim(), GetResult(result[12])));
                    AddResult(new ResultItem(result[13].Substring(0, 3).Trim(), GetResult(result[13])));
                    Log.Trace("Begin Import Result");
                    Log.Trace(ImportResults()?"Import Result Success":"Error While Import result");
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0} /n {1}", "Lỗi Trong quá trình xử lý dữ liệu", ex));
                //File.AppendAllText(@"C:\CLINITEK-ERROR.txt", ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        private string[] SeparatorData(string stringData)
        {
            try
            {
                //Ngắt chuỗi theo ký tự CRLF
                string[] arrStringData = stringData.Split(DeviceHelper.CRLF.ToCharArray());
                arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData);
                return arrStringData;
            }
            catch (Exception ex)
            {
                throw ex;
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