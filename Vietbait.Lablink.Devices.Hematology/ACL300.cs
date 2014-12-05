using System;
using System.Collections.Generic;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class ACL300 : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace(StringData);

                var strResult = StringData;
                while (!Char.IsLetterOrDigit(strResult[0])) strResult = strResult.Remove(0, 1);

                //Lay ve ngay thang nam lam xn
                var pMonth = strResult.Substring(5).Substring(4, 5).Replace(".", "").Trim();
                //lấy tháng
                pMonth = DeviceHelper.GetMonth(pMonth);
                //Lấy ngày
                var pDay = strResult.Substring(5).Substring(0, 3).Replace(".", "").Trim();
                //lấy Năm
                var pYear = strResult.Substring(5).Substring(9, 4).Trim();
                if (pDay.Length == 1)
                    pDay = "0" + pDay;

                var tempTestDate = string.Format("{0}/{1}/20{2}", pDay, pMonth, pYear);

                var arrPatients = SeparatorData(strResult);
                //duyệt từng bệnh nhân
                foreach (var patient in arrPatients)
                {
                    TestResult.TestDate = tempTestDate;
                    var allRecord = patient.Split(DeviceHelper.CR);
                    for (var i = 0; i <= 2; i++)
                    {
                        if (i == 0)
                        {
                            TestResult.TestSequence = allRecord[i].Split()[1];
                            TestResult.Barcode = allRecord[i].Split()[0];
                        }
                        else
                        {
                            string testName;
                            string testValue;
                            if (i == 1)
                            {
                                testName = "TT";
                                testValue = allRecord[i].Substring(0, 4).Trim();
                                if (testValue!="")
                                {
                                    AddResult(new ResultItem(testName, testValue));
                                    Log.Debug("Add new result Success: TestName={0} TestValue = {1}",testName,testValue);
                                }

                                testName = "TT(%)";
                                testValue = allRecord[i].Substring(4, 4).Trim();
                                if (testValue != "")
                                {
                                    AddResult(new ResultItem(testName, testValue));
                                    Log.Debug("Add new result Success: TestName={0} TestValue = {1}", testName, testValue);
                                }

                                testName = "PT-INR";
                                testValue = allRecord[i].Substring(9, 5).Trim();
                                if (testValue != "")
                                {
                                    AddResult(new ResultItem(testName, testValue));
                                    Log.Debug("Add new result Success: TestName={0} TestValue = {1}", testName, testValue);
                                }

                                testName = "FIB";
                                testValue = allRecord[i].Substring(14, 4).Trim();
                                if (testValue != "")
                                {
                                    AddResult(new ResultItem(testName, testValue));
                                    Log.Debug("Add new result Success: TestName={0} TestValue = {1}", testName, testValue);
                                }
                            }
                            else
                            {
                                try
                                {

                                    testName = "APTT";
                                    testValue = allRecord[i].Substring(0, 4).Trim();
                                    if (testValue != "")
                                    {
                                        AddResult(new ResultItem(testName, testValue));
                                        Log.Debug("Add new result Success: TestName={0} TestValue = {1}", testName, testValue);
                                    }

                                    testName = "APTT-INR";
                                    testValue = allRecord[i].Substring(4, 4).Trim();
                                    if (testValue != "")
                                    {
                                        AddResult(new ResultItem(testName, testValue));
                                        Log.Debug("Add new result Success: TestName={0} TestValue = {1}", testName, testValue);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Error("Error while add APTT or APTT-INR {0}",ex.ToString());
                                }
                            }
                        }
                    }
                    Log.Debug("Begin Import Result");
                    ImportResults();
                    Log.Debug("Import Result Success");
                }
                Log.Trace("Finish Process Data {0}", DeviceHelper.CRLF);
            }
            catch (Exception ex)
            {
                //  throw ex;
                Log.Error(string.Format("{0} /n {1}", "Lỗi Trong quá trình xử lý dữ liệu", ex));
            }
            finally
            {
                ClearData();
            }
        }

        private IEnumerable<string> SeparatorData(string stringData)
        {
            var strTemp = new string[] { };
            while (stringData.StartsWith(DeviceHelper.NULL.ToString(CultureInfo.InvariantCulture))) stringData = stringData.Remove(0, 1);
            //Ngắt chuỗi theo ký tự CRLFs
            var arrStringData = DeviceHelper.DeleteAllBlankLine(stringData, DeviceHelper.CRLF);

            //Biến để lưu tất cả các chuỗi kết quả.
            var allResult = new string[] { };

            //Biến lưu Index
            var id = 0;

            for (var i = 0; i < arrStringData.Length; i++)
                if (i > 2)
                {
                    id++;
                    Array.Resize(ref strTemp, id);
                    strTemp[id - 1] = arrStringData[i];
                }

            var sId = strTemp[strTemp.Length - 1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var soBoKetQua = Convert.ToByte(sId[sId.Length-1]) - 20;
            id = -1;
            for (var i = strTemp.Length - soBoKetQua; i < strTemp.Length; i++)
            {
                id++;
                Array.Resize(ref allResult, id + 1);
                allResult[id] = strTemp[i] + DeviceHelper.CR + strTemp[id * 2] + DeviceHelper.CR + strTemp[(id * 2) + 1];
            }
            return allResult;
        }
    }
}