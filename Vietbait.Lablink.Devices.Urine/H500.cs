/* Change Log
 * 2013-04-09: Hiệu chỉnh kết quả bỏ đơn vị
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class H500 : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            Log.Trace("Begin Process Data");
            Log.Trace("Raw Data \n {0}",StringData);
            try
            {
                // Tách riêng dữ liệu của từng bệnh nhân
                IEnumerable<string> allPatients = SeparatorData(StringData);

                // Xử lý dữ liệu của từng bệnh nhân
                foreach (string patient in allPatients)
                {
                    try
                    {
                        string[] strResult = DeviceHelper.DeleteAllBlankLine(patient, DeviceHelper.CRLF,false);

                        // Lấy Ngày tháng
                        string sTemp = strResult[0];
                        TestResult.TestDate = sTemp.ToUpper().StartsWith("DATE")
                                                  ? sTemp.Substring(sTemp.IndexOf(':') + 1, 10)
                                                  : DateTime.Now.ToString("dd/MM/yyyy");

                        //Lấy barcode:
                        sTemp = strResult[2];
                        sTemp = sTemp.Substring(sTemp.IndexOf(':') + 1).Replace("-", "");
                        TestResult.Barcode = sTemp == "" ? "0" : sTemp;
                        Log.Debug("Begin Add result for barcode: {0}", TestResult.Barcode);

                        for (int i = 3; i < strResult.Length; i++)
                        {
                            strResult[i] = strResult[i].Substring(0, strResult[i].Length - 6);
                            string testName = strResult[i].Substring(0, 4).Replace("*", "").Trim();
                            var tempValue = DeviceHelper.DeleteAllBlankLine(strResult[i], " ");
                            string testValue = tempValue[tempValue.Length - 1];
                            if (testValue.ToUpper()=="POS")
                            {
                                testValue = "Positive";
                            }
                            else if (testValue.ToUpper()=="NEG")
                            {
                                testValue = "Negative";
                            }
                            AddResult(new ResultItem(testName,testValue));
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    Log.Debug(ImportResults()?"Import Result Success":"Error While Import Result");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ClearData();
            }
        }

        private IEnumerable<string> SeparatorData(string rawData)
        {
            try
            {
                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = DeviceHelper.DeleteAllBlankLine(rawData, DeviceHelper.CRLF,false);
                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;
                foreach (string line in arrStringData)
                {
                    bool b1 = line.StartsWith(DeviceHelper.STX.ToString(CultureInfo.InvariantCulture));
                    bool b2 = line.StartsWith(string.Format("{0}{1}", DeviceHelper.ETX, DeviceHelper.STX));
                    if (b1 || b2)
                    {
                        id++;
                        Array.Resize(ref allResult, id);
                    }
                    else if (!line.StartsWith(DeviceHelper.ETX.ToString(CultureInfo.InvariantCulture)))
                    {
                        allResult[id - 1] = allResult[id - 1] + line + DeviceHelper.CRLF;
                    }
                }
                return allResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}