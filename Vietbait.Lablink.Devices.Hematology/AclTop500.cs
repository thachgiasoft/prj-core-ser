using System;
using System.Collections.Generic;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class AclTop500 : Rs232AstmDevice
    {
        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                if(!StringData.Contains(DeviceHelper.CR.ToString()))return;
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(String.Format("{0}{1}",DeviceHelper.CRLF,StringData));

                //Lấy về dữ liệu của các bệnh nhân
                var arrPatients = SeparatorData(StringData);

                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (var patient in arrPatients)
                {
                    foreach (var record in patient.Split(DeviceHelper.CR))
                    {
                        string[] temp;
                        if (record.StartsWith("O"))
                        {
                            temp = record.Split('|');
                            //Lấy barcode xét nghiệm
                            try
                            {
                                var strTempbarcode = temp[2].Trim();
                                TestResult.Barcode = strTempbarcode == "" ? "0000" : strTempbarcode;
                            }
                            catch (Exception)
                            {
                                TestResult.Barcode = "0000";
                            }
                        }
                            //lấy kq ra
                        else if (record.StartsWith("R"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                //Lấy về ngày tháng làm xét nghiệm
                                if (String.IsNullOrEmpty(TestResult.TestDate))
                                {
                                    var tempDate = temp[12];
                                    TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                                        tempDate.Substring(4, 2),
                                                                        tempDate.Substring(0, 4));
                                }
                                var strTestName = temp[2].Split('^')[3].Trim();
                                if (strTestName.Trim() == "")
                                {
                                    strTestName = "APPT - Ratio";
                                }
                                var strTestValue = temp[3].Trim();
                                var strTestUnit = temp[4].Trim();
                                if (!string.IsNullOrEmpty(strTestUnit))
                                strTestName =  string.Format("{0}-{1}", strTestName, strTestUnit.ToUpper());
                                AddResult(new ResultItem(strTestName, strTestValue, strTestUnit));
                            }
                            catch (Exception ex)
                            {
                                File.WriteAllText(@"C:\CA500.txt", ex.ToString());
                                continue;
                            }
                        }
                    }
                    Log.Debug("Begin Import Result");
                    Log.Debug(ImportResults()?"Finish Import Result":"Import Result Error");
                }
                Log.Trace("Finish Process Data {0}", DeviceHelper.CRLF);
            }
            catch (Exception ex)
            {
                // throw ex;
                Log.Error("Error while process data Error:" + ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        private IEnumerable<string> SeparatorData(string stringData)
        {
            try
            {
                //Bỏ tất cả các ký tự cho đến khi gặp ký tự "P" đầu tiên
                stringData =
                    stringData.Substring(stringData.IndexOf(DeviceHelper.CR + "P|", StringComparison.Ordinal) + 1);

                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;

                foreach (string line in arrStringData)
                {
                    if (line.StartsWith("P"))
                    {
                        id++;
                        Array.Resize(ref allResult, id);
                    }
                    allResult[id - 1] = allResult[id - 1] + line + DeviceHelper.CR;
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