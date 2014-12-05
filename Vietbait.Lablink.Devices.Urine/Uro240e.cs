using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class Uro240E : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                Log.Trace("RawData:{0}{1}", DeviceHelper.CRLF, StringData);
                //Khai báo danh sách các kết quả cần lấy
                var arrResultName = new[] {"SG", "LEU", "NIT", "pH", "PRO", "GLU", "KET", "UBG", "BIL", "BLD"};
                string[] allPatients = DeviceHelper.SeperatorRawData(StringData, DeviceHelper.STX, DeviceHelper.ETX);
                Log.Trace("Result has {0} Patients", allPatients.Length);
                //Xử lý từng kết quả của bệnh nhân
                foreach (string patient in allPatients)
                {
                    //Loại bỏ các dấu * ở đầu dòng và ngắt dòng theo ký tự CRLF
                    string[] result =DeviceHelper.DeleteAllBlankLine(patient.Replace(DeviceHelper.CRLF + "*", DeviceHelper.CRLF).Replace(DeviceHelper.CRLF + ":", DeviceHelper.CRLF),DeviceHelper.CRLF);

                    //Lấy PatientID:
                    int rowIndex = GetRowIndex(result, "Pat.ID");
                    TestResult.Barcode = result[rowIndex].Split(':')[1].Trim();
                    string[] tempDate = result[rowIndex + 1].Split()[0].Split('.');
                    //Lấy ngày:
                    TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate[1], tempDate[0], tempDate[2]);
                    //Lọc Kết quả
                    foreach (string sResultName in arrResultName)
                    {
                        int indexResult = GetRowIndex(result, sResultName);
                        if (indexResult > -1)
                        {
                            string testValue = result[indexResult].Substring(5, 5).Trim();
                            if (result[indexResult].Length >= 22)
                                testValue = string.Format("{0}({1})", result[indexResult].Substring(17, 5).Trim(),testValue);
                            AddResult(new ResultItem(sResultName, testValue));
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
    }
}