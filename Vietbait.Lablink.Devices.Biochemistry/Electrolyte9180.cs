using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Electrolyte9180 : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                //Log
                Log.Debug("Begin Process Data");
                Log.Trace("RawData:\n{0}", StringData);
                //Lấy về danh sách bệnh nhân
                //Duyệt từng bệnh nhân
                try
                {
                    if (!ValidRawData(StringData)) return;

                    var allPatient = SeparatorData(StringData);

                    foreach (string patient in allPatient)
                    {
                        if(patient.IndexOf("Sample:")<0) continue;
                        var tempResult = DeviceHelper.DeleteAllBlankLine(patient,DeviceHelper.CRLF);
                        string testValue;
                        const string testUnit = "mmol/L";

                        var rowIndex = GetRowIndex(tempResult, "-   NA-K");
                        TestResult.TestDate = tempResult[rowIndex + 1].Trim().Split()[0];
                        TestResult.TestDate = String.Format("{0}/{1}/{2}{3}", TestResult.TestDate.Substring(0, 2),
                                                            DeviceHelper.GetMonth(TestResult.TestDate.Substring(2, 3)),
                                                            DateTime.Now.Year.ToString(CultureInfo.InvariantCulture).
                                                                Substring(0, 2), TestResult.TestDate.Substring(5, 2));

                        TestResult.Barcode = DateTime.Now.ToString("yyMMddHHmmss");
                        //TestResult.Barcode = "";

                        var testName = "Na=";
                        rowIndex = GetRowIndex(tempResult, testName);
                        if (rowIndex > -1)
                        {
                            testName = "Na";
                            testValue = tempResult[rowIndex].Substring(3, 5).Trim();
                            AddResult(new ResultItem(testName, testValue, testUnit));
                            Log.Debug("Add Result success TestName:{0} - TestValue:{1} - TestUnit:{2}", testName, testValue,
                                      testUnit);
                        }

                        testName = "K";
                        rowIndex = GetRowIndex(tempResult, testName);
                        if (rowIndex > -1)
                        {
                            testValue = tempResult[rowIndex].Substring(3, 5).Trim();
                            AddResult(new ResultItem(testName, testValue, testUnit));
                            Log.Debug("Add Result success TestName:{0} - TestValue:{1} - TestUnit:{2}", testName,
                                      testValue, testUnit);
                        }

                        testName = "Ca";
                        rowIndex = GetRowIndex(tempResult, testName);
                        if (rowIndex > -1)
                        {
                            testValue = tempResult[rowIndex].Substring(3, 5).Trim();
                            AddResult(new ResultItem(testName, testValue, testUnit));
                            Log.Debug("Add Result success TestName:{0} - TestValue:{1} - TestUnit:{2}", testName,
                                      testValue, testUnit);
                        }

                        testName = "Cl";
                        rowIndex = GetRowIndex(tempResult, testName);
                        if (rowIndex > -1)
                        {
                            testValue = tempResult[rowIndex].Substring(3, 5).Trim();
                            AddResult(new ResultItem(testName, testValue, testUnit));
                            Log.Debug("Add Result success TestName:{0} - TestValue:{1} - TestUnit:{2}", testName,
                                      testValue, testUnit);
                        }

                        //Import dữ liệu
                        Log.Debug("Begin Import Result");
                        Log.Debug(ImportResults()?"Import Result success":"Error while import result"); 
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Co loi khi xu ly du lieu cua benh nhan:{0}{1}", DeviceHelper.CRLF,ex));
                }
                Log.Trace("Finish Process Data");
                ClearData();
            }
            catch (Exception ex)
            {
                //throw ex;
                Log.Error(string.Format("Co Loi trong qua trinh xu ly du lieu {0}{1}", DeviceHelper.CRLF, ex));
            }
            finally
            {
            }
        }

        /// <summary>
        /// Kiểm tra dữ liệu xem có phải là kết quả không
        /// </summary>
        /// <param name="inputString">Chuỗi truyền vào</param>
        /// <returns>True:Là kết quả tiếp tục xử lý - False:Không phải là kết quả</returns>
        private bool ValidRawData(string inputString)
        {
            return inputString.IndexOf(DeviceHelper.ETX.ToString())>0;
        }

        /// <summary>
        /// Trả về index của dòng với chuỗ bắt đầu xác định
        /// </summary>
        /// <param name="arrString">Mảng cần tìm Index</param>
        /// <param name="beginString">Chuỗi bắt đầu</param>
        /// <returns></returns>
        private int GetRowIndex(string[] arrString, string beginString)
        {
            try
            {
                int result = -1;
                for (int i = 0; i < arrString.Length; i++)
                    if (arrString[i].StartsWith(beginString))
                    {
                        result = i;
                        break;
                    }
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        private string[] SeparatorData(string stringData)
        {
            try
            {

                return DeviceHelper.SeperatorRawData(stringData, DeviceHelper.STX, DeviceHelper.ETX);

                stringData = stringData.Replace(DeviceHelper.CRLF, DeviceHelper.CR.ToString(CultureInfo.InvariantCulture));
                
                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] { };

                //Biến lưu Index
                int id = 0;

                foreach (string line in arrStringData)
                {
                    if (line.IndexOf("AVL 9180", StringComparison.Ordinal)>-1)
                    {
                        id++;
                        Array.Resize(ref allResult, id);
                    }
                    
                    if(!string.IsNullOrEmpty(line.Trim()))
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

    }
}