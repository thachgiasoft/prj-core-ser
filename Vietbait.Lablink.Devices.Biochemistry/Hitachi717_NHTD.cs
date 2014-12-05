using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Hitachi717_NHTD : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                //Nếu ký tự kết thúc khác EOT thi gửi ACK để confirm dữ liệu đã được nhận
                if (!(StringData.EndsWith(DeviceHelper.ETX.ToString(CultureInfo.InvariantCulture)) ||StringData.EndsWith(DeviceHelper.EOT.ToString(CultureInfo.InvariantCulture))))return;

                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace(DeviceHelper.CRLF +  StringData);

                //Kết thúc khi nhận được EOT và tiến hành xử lý DL
                SendStringData(DeviceHelper.ACK.ToString(CultureInfo.InvariantCulture));
                var allPatients = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.STX);
                Log.Trace("Result has {0} Patients", allPatients.Length);
                //Duyệt theo từng dòng 
                foreach (var patient in allPatients)
                {
                    var tempPatient = patient;
                    //Nếu đúng với các ký tự bắt đầu thì xử lý:
                    if (Validdata(patient))
                    {
                        TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                        TestResult.Barcode = tempPatient.Substring(12, 11).Trim();
                        tempPatient = tempPatient.Substring(24);
                        while (tempPatient.Length > 9)
                        {
                            var result = tempPatient.Substring(0, 9);
                            var testName = result.Substring(0, 2).Trim();
                            var testValue = result.Substring(2, 7).Trim();

                            //Xử lý các ký tự thừa trong kết quả

                            //Loại bỏ dấu "$"
                            testValue = testValue.Replace("$", "");

                            //Loại bỏ các chữ cái in hoa và in thường trong chuỗi kết quả
                            for (byte i = 65; i <= 90; i++)
                                testValue = testValue.Replace(Convert.ToChar(i).ToString(CultureInfo.InvariantCulture),"")
                                    .Replace(Convert.ToChar(i + 32).ToString(CultureInfo.InvariantCulture), "");

                            //Thêm kết quả mới
                            TestResult.Add(new ResultItem(testName, testValue));
                            Log .Debug("Add new Result: TestName = {0}, TestValue = {1}",testName,testValue);

                            //Cắt bỏ các dữ liệu vừa được xử lý
                            tempPatient = tempPatient.Substring(9);

                            //Xử lý các thông số tính toán
                            double iHdlc = -1, iTrig = -1, iChol = -1, iAlb = -1, iProT = -1, iBilt = -1, iBild = -1,iCa = -1;

                            foreach (ResultItem item in TestResult.Items)
                            {
                                //BIL-Toàn phần
                                if (item.TestName.Equals("10"))
                                {
                                    iBilt = TryToConvertToDouble(item.TestValue);
                                    if (iBilt > 50)
                                    {
                                        iBilt = Math.Round(iBilt, 0);
                                        item.TestValue = iBilt.ToString(CultureInfo.InvariantCulture);
                                    }
                                }

                                //BIL-Trực tiếp
                                if (item.TestName.Equals("11"))
                                {
                                    iBild = TryToConvertToDouble(item.TestValue);
                                    if (iBild > 50)
                                    {
                                        iBild = Math.Round(iBild, 0);
                                        item.TestValue = iBild.ToString(CultureInfo.InvariantCulture);
                                    }
                                }

                                //Protein-T                                    
                                if (item.TestName.Equals("4")) iProT = TryToConvertToDouble(item.TestValue);

                                //ALB                                   
                                if (item.TestName.Equals("5")) iAlb = TryToConvertToDouble(item.TestValue);

                                //CHOL
                                if (item.TestName.Equals("2")) iChol = TryToConvertToDouble(item.TestValue);

                                //Trig
                                if (item.TestName.Equals("3")) iTrig = TryToConvertToDouble(item.TestValue);

                                //HDLC
                                if (item.TestName.Equals("16")) iHdlc = TryToConvertToDouble(item.TestValue);
                                
                                //Ca
                                if (item.TestName.Equals("15")) iCa = TryToConvertToDouble(item.TestValue);
                            }

                            //Tính toán

                            //Bil-Gián tiếp:
                            if ((iBild > 0) && (iBilt > 0))
                            {
                                testName = "47";
                                testValue = (iBilt - iBild).ToString(CultureInfo.InvariantCulture);
                                TestResult.Add(new ResultItem(testName, testValue));
                                Log.Debug("Add new Calculated Result: TestName = {0}, TestValue = {1}", testName, testValue);
                            }

                            //Globumin,Tỷ số A/G
                            if ((iProT > 0) && (iAlb > 0))
                            {
                                //Globumin
                                testName = "51";
                                testValue = (iProT - iAlb).ToString(CultureInfo.InvariantCulture);
                                TestResult.Add(new ResultItem(testName, testValue));
                                Log.Debug("Add new Calculated Result: TestName = {0}, TestValue = {1}", testName, testValue);

                                //Tỷ số A/G
                                testName = "49";
                                testValue = (iAlb/(iProT - iAlb)).ToString(CultureInfo.InvariantCulture);
                                TestResult.Add(new ResultItem(testName, testValue));
                                Log.Debug("Add new Calculated Result: TestName = {0}, TestValue = {1}", testName, testValue);
                            }

                            //LDLC:
                            if ((iChol > 0) && (iHdlc > 0) && (iTrig > 0))
                            {
                                testName = "50";
                                testValue = iTrig <=4.5 ? (iChol - (iTrig/2.2 + iHdlc)).ToString(CultureInfo.InvariantCulture):"HTĐ";
                                TestResult.Add(new ResultItem(testName, testValue));
                                Log.Debug("Add new Calculated Result: TestName = {0}, TestValue = {1}", testName, testValue);
                            }

                            //CA-Ion
                            if (iCa>0)
                            {
                                testName = "caion";
                                testValue = (iCa*0.53).ToString(CultureInfo.InvariantCulture);
                                TestResult.Add(new ResultItem(testName, testValue));
                                Log.Debug("Add new Calculated Result: TestName = {0}, TestValue = {1}", testName, testValue);
                            }
                        }
                    }
                    Log.Debug("Begin Importdata");
                    ImportResults();
                    Log.Debug("Finish Imported result");
                    ClearData();
                }

                //System.IO.File.WriteAllText(@"C:\Hitachi717_Input.txt",StringData);
            }
            catch (Exception ex)
            {
                ClearData();
                throw ex;
            }
        }

        private bool Validdata(string record)
        {
            if (record.StartsWith("03N")) return true;
            if (record.StartsWith("02N")) return true;
            if (record.StartsWith("13N")) return true;
            if (record.StartsWith("12N")) return true;
            if (record.StartsWith("53N")) return true;
            if (record.StartsWith("52N")) return true;
            if (record.StartsWith("03E")) return true;
            if (record.StartsWith("02E")) return true;
            if (record.StartsWith("13E")) return true;
            if (record.StartsWith("12E")) return true;
            if (record.StartsWith("53E")) return true;
            if (record.StartsWith("52E")) return true;
            return false;
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