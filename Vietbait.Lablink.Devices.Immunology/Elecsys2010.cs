using System;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Immunology
{
    internal class Elecsys2010 : Rs232AstmDevice
    {
        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                Log.Trace("RawData:{0}{1}", DeviceHelper.CRLF, StringData);
                string[] arrPatients = SeparatorData(StringData);
                string[] temp;

                foreach (string patient in arrPatients)
                {
                    foreach (string record in patient.Split(DeviceHelper.CR))
                    {
                        if (record.StartsWith("O"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                TestResult.Barcode = temp[2].Trim();
                                TestResult.TestDate = string.Format("{0}/{1}/{2}", temp[6].Substring(6, 2),
                                                                    temp[6].Substring(4, 2),
                                                                    temp[6].Substring(0, 4));
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while process Barcode and TestDate: {0}", ex.ToString());
                            }
                        }
                        else if (record.StartsWith("R"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                string testName = temp[2].Split('^')[3].Trim();
                                string testValue = temp[3].Trim();
                                if (testValue.IndexOf('^') >= 0) testValue = testValue.Split('^')[1];
                                string mearsureUnit = temp[4].Trim();
                                string normalLevel = string.Format("{0} - {1}", temp[5].Split('^')[0].Trim(), temp[5].Split('^')[1].Trim());
                                AddResult(new ResultItem(testName, testValue, mearsureUnit, normalLevel, normalLevel));
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while add new result: {0}", ex.ToString());
                            }
                        }
                    }

                    Log.Debug("Begin Import Result For barocde:{0}", TestResult.Barcode);
                    ImportResults();
                    Log.Debug("Import Result Success");
                }
                Log.Trace("Finish Process Data");
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing data: {0}", ex.ToString());
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
                ////Bỏ tất cả các ký tự cho đến khi gặp ký tự "P" đầu tiên
                //stringData = stringData.Substring(stringData.IndexOf('P'));

                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = stringData.Split(DeviceHelper.CRLF.ToCharArray());

                arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = -1;

                foreach (string line in arrStringData)
                {
                    if (line.StartsWith("P"))
                    {
                        id++;
                        Array.Resize(ref allResult, id + 1);
                    }
                    if (id > -1) allResult[id] = allResult[id] + line + DeviceHelper.CR;
                }

                return allResult;
            }
            catch (Exception ex)
            {
                Log.Error("Error in SeparatorData function: {0}", ex.ToString());
                throw ex;
            }
        }
    }
}