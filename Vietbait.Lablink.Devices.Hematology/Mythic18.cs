using System;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class Mythic18 : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                //Kiểm tra nếu chưa kết thúc nhận dữ liệu
                // if(!StringData.EndsWith(DeviceHelper.ETX.ToString())) return;
                string[] allResult = SeparatorData(StringData);

                foreach (string record in allResult)
                {
                    string[] strResult = DeviceHelper.DeleteAllBlankLine(record, DeviceHelper.CR.ToString(CultureInfo.InvariantCulture));
                    TestResult.TestDate = strResult[1].Split(';')[1].Trim();
                    try
                    {
                        TestResult.Barcode = Convert.ToInt32(strResult[8].Split(';')[1].Trim()).ToString(CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        TestResult.Barcode = strResult[8].Split(';')[1].Trim();
                    }

                    for (int id = 12; id < 30; id++)
                    {
                        AddResult(new ResultItem(strResult[id].Split(';')[0], strResult[id].Split(';')[1]));
                    }

                    ImportResults();
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;

                foreach (string line in arrStringData)
                {
                    if (line.StartsWith("MYTHIC"))
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

        #endregion
    }
}