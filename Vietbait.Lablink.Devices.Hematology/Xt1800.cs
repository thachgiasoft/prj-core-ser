using System;
using System.Collections.Generic;
using System.Linq;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class Xt1800 : Rs232AstmDevice
        //internal class XS1000I : Rs232Devcie
    {
        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(StringData);

                //Lấy về dữ liệu của các bệnh nhân
                IEnumerable<string> arrPatients = SeparatorData(StringData);

                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (string patient in arrPatients)
                {
                    IEnumerable<string> resultArray = null;
                    foreach (string record in patient.Split(DeviceHelper.CR))
                    {
                        string[] temp;
                        if (record.StartsWith("O"))
                        {
                            temp = record.Split('|');
                            //Lấy barcode xét nghiệm
                            resultArray = from s in temp[4].Split("\\".ToCharArray())
                                          select s.Split('^')[4];
                            try
                            {
                                string strTempbarcode = temp[3].Split('^')[2].Trim();
                                TestResult.Barcode = strTempbarcode == "" ? "0000" : strTempbarcode;
                            }
                            catch (Exception)
                            {
                                TestResult.Barcode = "0000";
                            }
                        }
                        else if (record.StartsWith("R"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                //Lấy về ngày tháng làm xét nghiệm
                                if (String.IsNullOrEmpty(TestResult.TestDate))
                                {
                                    string tempDate = temp[12];
                                    TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                                        tempDate.Substring(4, 2),
                                                                        tempDate.Substring(0, 4));
                                }

                                var strTestName = temp[2].Split('^')[4].Trim();
                                var testInclude = (from s in resultArray
                                                    where s == strTestName
                                                    select s).Any();
                                if (!testInclude) continue;


                                string strTestValue = temp[3].Trim();
                                string strTestUnit = temp[4].Trim();

                                if (strTestName == "HGB")
                                {
                                    try
                                    {
                                        strTestValue = Math.Round(Convert.ToDouble(strTestValue)*10, 3).ToString();
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (strTestName == "MCHC")
                                {
                                    try
                                    {
                                        strTestValue = Math.Round(Convert.ToDouble(strTestValue)*10, 3).ToString();
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                AddResult(new ResultItem(strTestName, strTestValue, strTestUnit));
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while processing data Error: {0}", ex);
                                continue;
                            }
                        }
                    }
                    Log.Trace("Begin Import Result");
                    Log.Trace(ImportResults() ? "Import Result Success" : "Error While Import result");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing data Error: {0}", ex);
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