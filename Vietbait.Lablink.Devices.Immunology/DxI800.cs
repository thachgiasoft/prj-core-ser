using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Immunology
{
    //internal class Cobase411 : Rs232Devcie
    internal class DxI800 : Rs232AstmDevice
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
                string[] arrPatients = SeparatorData(StringData);
                Log.Trace("Result has {0} Patients", arrPatients.Length);
                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (string patient in arrPatients)
                {
                    foreach (string record in patient.Split(DeviceHelper.CR))
                    {
                        string[] temp;
                        if (record.StartsWith("O"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                TestResult.Barcode = temp[2].Trim();
                                //var tempTestDate = temp[7];
                                //TestResult.TestDate = string.Format("{0}/{1}/{2}", tempTestDate.Substring(6, 2),
                                //                                    tempTestDate.Substring(4, 2),
                                //                                    tempTestDate.Substring(0, 4));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else if (record.StartsWith("R"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                if (string.IsNullOrEmpty(TestResult.TestDate))
                                {
                                    var tempTestDate = temp[12];
                                    TestResult.TestDate = string.Format("{0}/{1}/{2}", tempTestDate.Substring(6, 2),
                                                                        tempTestDate.Substring(4, 2),
                                                                        tempTestDate.Substring(0, 4)); 
                                }
                                string testName = "";
                                string testValue = "";
                                string testUnit = "";
                                testName = temp[2].Split('^')[3].Split('/')[0].Trim();
                                testValue = temp[3].Trim().Replace("<","").Replace(">","");
                                testValue = testValue.Split('^').Length > 1
                                                ? testValue.Split('^')[testValue.Split('^').Length - 1]
                                                : testValue.Split('^')[0];
                                testUnit = temp[4];
                                AddResult(new ResultItem(testName,testValue,testUnit));
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    ImportResults();
                    Log.Debug("Import Result Success For barocde:{0}", TestResult.Barcode);
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