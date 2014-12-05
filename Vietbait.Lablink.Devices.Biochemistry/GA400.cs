using System;
using System.Globalization;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class GA400 : Rs232AstmDevice
    {
        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                Log.Trace(DeviceHelper.CRLF + StringData);

                //Lấy về dữ liệu của các bệnh nhân
                var arrPatients = SeparatorData(StringData);

                Log.Trace("Result has {0} Patients", arrPatients.Length);
                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (string patient in arrPatients)
                {
                    foreach (string record in patient.Split(DeviceHelper.CR))
                    {
                        string[] temp;
                        if (record.StartsWith("O"))
                            try
                            {
                                temp = record.Split('|');
                                TestResult.Barcode = temp[2].Split('^')[0].Trim();
                                TestResult.TestDate = string.Format("{0}/{1}/{2}", temp[6].Substring(6, 2),
                                                                    temp[6].Substring(4, 2),
                                                                    temp[6].Substring(0, 4));
                                Log.Debug("Barcode:{0}, TestDate:{1}",TestResult.Barcode,TestResult.TestDate);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while parse data: {0}{1}Eror String:{2}", record, DeviceHelper.CRLF,
                                          ex.ToString());
                            }
                        else if (record.StartsWith("R"))
                            try
                            {
                                temp = record.Split('|');
                                string testName = temp[2].Replace("^", "").Trim();
                                string testValue = temp[3].Trim();
                                string testUnit = temp[4].Trim();
                                AddResult(new ResultItem(testName, testValue, testUnit));
                                Log.Debug("Add Result Item success: TestName={0}, TestValue={1}, TestUnit={2}", testName,
                                          testValue, testUnit);
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while parse data: {0}{1}Eror String:{2}", record, DeviceHelper.CRLF,
                                          ex.ToString());
                            }
                    }

                    //Xử lý các thông số tính toán
                    double iHdlc = -1, iTrig = -1, iChol = -1, iAlb = -1, iProT = -1, iBilt = -1, iBild = -1, iCa = -1;
                    foreach (ResultItem item in TestResult.Items)
                    {
                        //BIL-Toàn phần
                        if (item.TestName.ToUpper().Equals("TBIL")) iBilt = TryToConvertToDouble(item.TestValue);

                        //BIL-Trực tiếp
                        if (item.TestName.ToUpper().Equals("DBIL")) iBild = TryToConvertToDouble(item.TestValue);

                        //Protein-T                                    
                        if (item.TestName.ToUpper().Equals("TP")) iProT = TryToConvertToDouble(item.TestValue);

                        //ALB                                   
                        if (item.TestName.ToUpper().Equals("ALB")) iAlb = TryToConvertToDouble(item.TestValue);

                        //CHOL
                        if (item.TestName.ToUpper().Equals("CHOL")) iChol = TryToConvertToDouble(item.TestValue);

                        //Trig
                        if (item.TestName.ToUpper().Equals("TG")) iTrig = TryToConvertToDouble(item.TestValue);

                        //HDLC
                        if (item.TestName.ToUpper().Equals("HDL-C")) iHdlc = TryToConvertToDouble(item.TestValue);

                        //Ca
                        if (item.TestName.ToUpper().Equals("CA")) iCa = TryToConvertToDouble(item.TestValue);
                    }

                    //Tính toán

                    //Bil-Gián tiếp:
                    if ((iBild > 0) && (iBilt > 0))
                        try
                        {
                            string testName = "BIL-GT";
                            string testValue = Math.Round((iBilt - iBild), 2).ToString(CultureInfo.InvariantCulture);
                            TestResult.Add(new ResultItem(testName,testValue));
                            Log.Debug("Add Calculated Result Item success: TestName={0}, TestValue={1}", testName,
                                          testValue);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Error while calculate BIL - GT {0}Error:{1}",DeviceHelper.CRLF,ex.ToString());
                        }

                    //Globumin,Tỷ số A/G
                    if ((iProT > 0) && (iAlb > 0))
                    {
                        //Globumin
                        try
                        {
                            string testName = "GLOB";
                            string testValue = Math.Round((iProT - iAlb), 2).ToString(CultureInfo.InvariantCulture);
                            TestResult.Add(new ResultItem(testName, testValue));
                            Log.Debug("Add Calculated Result Item success: TestName={0}, TestValue={1}", testName,testValue);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Error while calculate GLOB {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                        }

                        //Tỷ số A/G
                        try
                        {
                            string testName = "A/G";
                            string testValue = Math.Round((iAlb/(iProT - iAlb)), 2).ToString(CultureInfo.InvariantCulture);
                            TestResult.Add(new ResultItem(testName, testValue));
                            Log.Debug("Add Calculated Result Item success: TestName={0}, TestValue={1}", testName, testValue);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Error while calculate A/G {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                        }
                    }

                    //LDLC:
                    if ((iChol > 0) && (iHdlc > 0) && (iTrig > 0))
                    {
                        try
                        {
                            string testName = "LDL-C";
                            string testValue = Math.Round((iChol - (iTrig/2.2 + iHdlc)), 2).ToString(CultureInfo.InvariantCulture);
                            TestResult.Add(new ResultItem(testName, testValue));
                            Log.Debug("Add Calculated Result Item success: TestName={0}, TestValue={1}", testName, testValue);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Error while calculate LDL-C {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                        }
                    }

                    //Ca-ion:
                    if ((iCa > 0) && (iProT > 0))
                    {
                        try
                        {
                            var testName = "Ca-ion";
                            var testValue =
                                Math.Round((((65*iCa + 11.4)/(iProT + 66)) - 0.16), 2).ToString(CultureInfo.InvariantCulture);
                            TestResult.Add(new ResultItem(testName, testValue));
                            Log.Debug("Add Calculated Result Item success: TestName={0}, TestValue={1}", testName,testValue);
                        }
                        catch (Exception ex)
                        {
                            Log.Error("Error while calculate Ca-ion {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                        }
                    }

                    Log.Debug(string.Format("Import Result For barocde:{0}", TestResult.Barcode));
                    ImportResults();
                    Log.Debug(string.Format("Import Result Success"));
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing data {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                throw;
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
                stringData = stringData.Substring(stringData.IndexOf('P'));

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
                Log.Error("Error while parsing data {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                throw ex;
            }
        }

        private double TryToConvertToDouble(string value)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}