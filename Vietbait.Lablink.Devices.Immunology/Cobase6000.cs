using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Immunology
{
    internal class Cobase6000 : Rs232AstmDevice
    {
        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        //public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                Log.Trace(DeviceHelper.CRLF + StringData);
                //Lấy về dữ liệu của các bệnh nhân
                var arrPatients = SeparatorData(StringData);
                Log.Trace("Result has {0} Patients", arrPatients.Length);
                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (var patient in arrPatients)
                {
                    foreach (var record in patient.Split(DeviceHelper.CR))
                    {
                        string[] temp;
                        if (record.StartsWith("O"))
                            try
                            {
                                temp = record.Split('|');
                                TestResult.Barcode = temp[2].Trim();
                                var tempTestDate = temp[22];
                                TestResult.TestDate = string.Format("{0}/{1}/{2}", tempTestDate.Substring(6, 2),
                                                                    tempTestDate.Substring(4, 2),
                                                                    tempTestDate.Substring(0, 4));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        else if (record.StartsWith("R"))
                            try
                            {
                                temp = record.Split('|');
                                var testName = temp[2].Split('^')[3].Split('/')[0].Trim();
                                var testValue = temp[3].Trim().Replace("<", "").Replace(">", "");
                                var tempResult = testValue.Split('^');
                                if (tempResult.Length > 1)
                                {
                                    testValue = tempResult[tempResult.Length - 1];
                                    switch (tempResult[0])
                                    {
                                        case "1":
                                            testValue = string.Format("{0} | reac", testValue);
                                            break;
                                        case "-1":
                                            testValue = string.Format("{0} | n-re", testValue);
                                            break;
                                    }
                                }
                                else
                                    testValue = tempResult[0];

                                //testUnit = temp[4];
                                AddResult(new ResultItem(testName, testValue));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                    }
                    Log.Debug("Begin Import Result for barcode: {0}",TestResult.Barcode);
                    Log.Debug(ImportResults() ? "Import Result Success" : "Error while import result");
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

        private string[] SeparatorData(string stringData)
        {
            try
            {
                //Bỏ tất cả các ký tự cho đến khi gặp ký tự "P" đầu tiên
                stringData = stringData.Substring(stringData.IndexOf(DeviceHelper.CR + "P", StringComparison.Ordinal)+1);

                //Ngắt chuỗi theo ký tự CR
                var arrStringData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                var id = 0;

                foreach (var line in arrStringData)
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