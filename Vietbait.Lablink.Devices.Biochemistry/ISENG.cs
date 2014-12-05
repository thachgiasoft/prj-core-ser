using System;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class ISENG : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                //Log
                Log.Debug("Begin Process Data");
                Log.Trace("RawData:{0}",StringData);
                //Lấy về danh sách bệnh nhân
                string[] strResutl = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF.ToCharArray());
                Log.Trace("Data has {0} patient(s)", strResutl.Length);

                //Duyệt từng bệnh nhân
                foreach (string patient in strResutl)
                {
                    try
                    {
                        string[] tempTestDate = patient.Substring(0, 10).Split('/');
                        TestResult.TestDate = String.Format("{0}/{1}/{2}", tempTestDate[2], tempTestDate[1],
                                                            tempTestDate[0]);
                        //Xác định ID
                        int tempindex = patient.IndexOf("ID=", System.StringComparison.Ordinal);

                        //Nếu không tồn tại ID chuyển sang bản ghi tiếp theo
                        if (tempindex>0)
                        {
                            string testName = "";
                            string testValue = "";
                            TestResult.Barcode = patient.Substring(tempindex + 3, 3);

                            //Xác định chỉ số K
                            tempindex = patient.IndexOf("K=", StringComparison.Ordinal);
                            if (tempindex>0)
                            {
                                testName = "K";
                                testValue = patient.Substring(tempindex + 2, 5).Trim();
                                AddResult(new ResultItem(testName,testValue));
                            }
                            Log.Debug("Add Result success TestName:{0} - TestValue:{1}", testName, testValue);

                            //Xác định chỉ số Na
                            tempindex = patient.IndexOf("Na=", StringComparison.Ordinal);
                            if (tempindex > 0)
                            {
                                testName = "Na";
                                testValue = patient.Substring(tempindex + 3, 5).Trim();
                                AddResult(new ResultItem(testName, testValue));
                            }
                            Log.Debug("Add Result success TestName:{0} - TestValue:{1}", testName, testValue);

                            //Xác định chỉ số Cl
                            tempindex = patient.IndexOf("Cl=", StringComparison.Ordinal);
                            if (tempindex > 0)
                            {
                                testName = "Cl";
                                testValue = patient.Substring(tempindex + 3, 5).Trim();
                                AddResult(new ResultItem(testName, testValue));
                            }
                            Log.Debug("Add Result success TestName:{0} - TestValue:{1}", testName, testValue);


                            //Import dữ liệu
                            Log.Debug("Begin Import Result");
                            ImportResults();
                            Log.Debug("Import Result success");
                        }
                    }
                    catch (Exception)
                    {
                        Log.Error(string.Format("Co loi khi xu ly du lieu cua benh nhan:{0}Data:{1}", DeviceHelper.CRLF, patient));
                    }
                }
                Log.Trace("Finish Process Data");
                
            }
            catch (Exception ex)
            {
                //throw ex;
                Log.Error(string.Format("Co Loi trong qua trinh xu ly du lieu {0}{1}", DeviceHelper.CRLF, ex.ToString()));
            }
            finally
            {
                ClearData();
            }
        }
    }
}