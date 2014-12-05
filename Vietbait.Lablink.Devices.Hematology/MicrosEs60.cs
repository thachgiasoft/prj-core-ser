using System;
using System.Globalization;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class MicrosEs60 : Rs232AstmDevice
    //internal class MicrosEs60 : Rs232Devcie
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
                                TestResult.Barcode = temp[2].Trim() == "" ? "0000" : temp[2].Trim();
                            }
                            catch (Exception)
                            {

                                TestResult.Barcode = "0000";
                            }
                            //Lấy về ngày tháng làm xét nghiệm
                            try
                            {
                                TestResult.TestDate = string.Format("{0}/{1}/{3}{2}", temp[7].Substring(4, 2),
                                                                    temp[7].Substring(2, 2), temp[7].Substring(0, 2),
                                                                    DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring(0, 2));

                            }
                            catch (Exception ex)
                            {
                                TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                            }
                        }
                        //lấy kq ra
                        else if (record.StartsWith("R"))
                        {
                            try
                            {
                                //string strAmtinh = "ÂM TÍNH";
                                temp = record.Split('|');
                                var strTestName = temp[2].Split('^')[3];
                                var strTestValue = temp[3].Trim();
                                AddResult(new ResultItem(strTestName, strTestValue));
                            }
                            catch (Exception ex)
                            {
                                File.WriteAllText(@"C:\Evolis-Exception-Result.txt", ex.ToString());
                                continue;
                            }
                        }
                    }
                    ImportResults();
                    Log.Debug("Finish Import Result");
                }
                Log.Trace("Finish Process Data {0}", DeviceHelper.CRLF);
            }
            catch (Exception ex)
            {
                // throw ex;
                File.WriteAllText(@"C:\Evolis-Exception.txt", ex.ToString());
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
                stringData = stringData.Substring(stringData.IndexOf(DeviceHelper.CR + "P|", StringComparison.Ordinal) + 1);

                //Ngắt chuỗi theo ký tự CR
                string[] arrAstmData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;

                foreach (string line in arrAstmData)
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