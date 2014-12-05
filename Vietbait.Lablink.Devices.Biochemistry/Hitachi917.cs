using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Hitachi917 : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                //Nếu ký tự kết thúc bằng CR thì tiến hành xử lý
                if (!StringData.EndsWith(DeviceHelper.CR.ToString(CultureInfo.InvariantCulture))) return;

                Log.Trace("Begin Process Data");

                Log.Trace(DeviceHelper.CRLF + StringData);
                SendStringData(DeviceHelper.ACK.ToString(CultureInfo.InvariantCulture));
                string[] allPatients = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF);
                
                Log.Trace("Result has {0} Patients",allPatients.Length);
               
                //Duyệt theo từng dòng 
                foreach (string patient in allPatients)
                {
                    string tempPatient = patient, testName, testValue;
                    int totalTest;
                    //Nếu đúng với các ký tự bắt đầu thì xử lý:
                    if (Validdata(patient))
                    {
                        TestResult.Barcode = patient.Substring(21, 13).Trim();
                        TestResult.TestDate = patient.Substring(39, 6).Trim();
                        TestResult.TestDate = string.Format("{0}/{1}/{2}{3}", TestResult.TestDate.Substring(2, 2),
                                                            TestResult.TestDate.Substring(0, 2),
                                                            DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).Substring(0, 2),
                                                            TestResult.TestDate.Substring(4, 2));
                        //Nếu số lượng kết quả =0 chuyển xử lý kết quả sau
                        totalTest = Convert.ToInt32(tempPatient.Substring(56, 2));
                        if (totalTest == 0) continue;
                        tempPatient = tempPatient.Substring(58);
                        int testCount = 1;
                        while ((tempPatient.Length > 9)&&(testCount<=totalTest))
                        {
                            testCount++;
                            var result = tempPatient.Substring(0, 10);
                            testName = result.Substring(0, 3).Trim();
                            testValue = result.Substring(3, 7).Trim();

                            //Xử lý các ký tự thừa trong kết quả

                            //Loại bỏ dấu "$"
                            testValue = testValue.Replace("$", "");
                            testValue = testValue.Replace("!", "");
                            testValue = testValue.Replace("%", "");

                            //Loại bỏ các chữ cái in hoa và in thường trong chuỗi kết quả
                            for (byte i = 65; i <= 90; i++)
                                testValue = testValue.Replace(Convert.ToChar(i).ToString(CultureInfo.InvariantCulture),"").Replace(Convert.ToChar(i + 32).ToString(CultureInfo.InvariantCulture), "");

                            //Thêm kết quả mới
                            //Nếu tên xét nghiệm khác rỗng thì insert

                            if (!string.IsNullOrEmpty(testName.Trim()))
                            {
                                TestResult.Add(new ResultItem(testName, testValue));
                                Log.Debug("Add new Result: TestName = {0}, TestValue = {1}", testName, testValue); 
                            }

                            //Cắt bỏ các dữ liệu vừa được xử lý
                            tempPatient = tempPatient.Substring(10);
                        }

                        Log.Debug("Begin Import Result For barocde:{0}", TestResult.Barcode);
                        ImportResults();
                        Log.Debug("Import Result Success");
                    }
                }
                ClearData();
            }
            catch (Exception ex)
            {
                ClearData();
                Log.Error("Error while processing data, Error:{0}",ex.ToString());
            }
        }

        private bool Validdata(string record)
        {
            try
            {
                string flag = record.Substring(4, 1);
                if ((flag == "1") || (flag == "2") || (flag == "3") || (flag == "4") || (flag == "5") || (flag == ":")) return true;
                
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("Error when check string data Exception:" + ex.ToString());
                return false;
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