using System;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class ACL100 : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                var tempDate = new string[] {};
                string pDay = null;
                string[] strResutl = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF);
                File.WriteAllLines(@"C:\ACL-KETQUA.txt", strResutl);
                TestResult.TestDate = strResutl[0];
                tempDate = strResutl[0].Split();
                //Lay ve ngay thang nam lam xn
                string pTestDate = tempDate[2].Replace(".", "");
                pTestDate = GetMonth(pTestDate);
                pDay = tempDate[1].Replace(".", "");
                if (pDay.Length == 1)
                {
                    pDay = "0" + pDay;
                }
                string tempTestDate = string.Format("{0}/{1}/20{2}", pDay, pTestDate, tempDate[3]);
                string[] arrPatients = SeparatorData(StringData);

                foreach (string patient in arrPatients)
                {
                    TestResult.TestDate = tempTestDate;
                    string[] allRecord = patient.Split(DeviceHelper.CR);
                    for (int i = 0; i <= 2; i++)
                    {
                        if (i == 0)
                        {
                            TestResult.TestSequence = allRecord[i].Split()[1];
                            TestResult.Barcode = allRecord[i].Split()[0];
                        }
                        else if (i == 1)
                        {
                            AddResult(new ResultItem("PT", allRecord[i].Substring(0, 5).Replace("*", "").Trim()));
                            AddResult(new ResultItem("PT(%)", allRecord[i].Substring(5, 4).Replace("*", "").Trim()));
                            AddResult(new ResultItem("PT-INR", allRecord[i].Substring(9, 5).Replace("*", "").Trim()));
                            AddResult(new ResultItem("FIB", allRecord[i].Substring(14, 4).Replace("*", "").Trim()));
                        }
                        else
                        {
                            AddResult(new ResultItem("APTT", allRecord[i].Substring(0, 4).Replace("*", "").Trim()));
                            AddResult(new ResultItem("APTT-INR", allRecord[i].Substring(4, 4).Replace("*", "").Trim()));
                        }
                    }
                    ImportResults();
                }
            }
            catch (Exception ex)
            {
                //  throw ex;
                File.AppendAllText("C:/ACL-ERROR.txt", ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        private string GetMonth(string pMonth)
        {
            switch (pMonth)
            {
                case "JAN":
                    pMonth = "01";
                    break;
                case "FEB":
                    pMonth = "02";
                    break;
                case "MAR":
                    pMonth = "03";
                    break;
                case "APR":
                    pMonth = "04";
                    break;
                case "MAY":
                    pMonth = "05";
                    break;
                case "JUN":
                    pMonth = "06";
                    break;
                case "JUL":
                    pMonth = "07";
                    break;
                case "AUG":
                    pMonth = "08";
                    break;
                case "SEP":
                    pMonth = "09";
                    break;
                case "OCT":
                    pMonth = "10";
                    break;
                case "NOV":
                    pMonth = "11";
                    break;
                case "DEC":
                    pMonth = "12";
                    break;
            }
            return pMonth;
        }

        private string[] SeparatorData(string stringData)
        {
            var strTemp = new string[] {};
            try
            {
                //Ngắt chuỗi theo ký tự CRLFs
                string[] arrStringData = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF);

                //Biến để lưu tất cả các chuỗi kết quả.
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;

                for (int i = 0; i < arrStringData.Length; i++)
                {
                    if (i > 2)
                    {
                        id++;
                        Array.Resize(ref strTemp, id);
                        strTemp[id - 1] = arrStringData[i];
                    }
                }

                int soBoKetQua = strTemp.Length/3;
                id = -1;
                for (int i = strTemp.Length - soBoKetQua; i < strTemp.Length; i++)
                {
                    id++;
                    Array.Resize(ref allResult, id + 1);
                    allResult[id] = strTemp[i] + DeviceHelper.CR + strTemp[id*2] + DeviceHelper.CR + strTemp[(id*2) + 1];
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