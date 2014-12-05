using System;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Immunology
{
    internal class CI4100 : Rs232AstmDevice
    {
        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                string strAmtinh = " | ÂM TÍNH", strDuongtinh = " | DƯƠNG TÍNH";
                //Lấy về dữ liệu của các bệnh nhân
                File.AppendAllText("C:/CI4100-KQ.txt", StringData);
                string[] arrPatients = SeparatorData(StringData);
                string[] temp;

                foreach (string patient in arrPatients)
                {
                    foreach (string record in patient.Split(DeviceHelper.CRLF.ToCharArray()))
                    {
                        if (record.StartsWith("P"))
                        {
                            temp = record.Split('|');
                            ////Barcode
                            TestResult.Barcode = temp[5].Trim();
                        }
                        else if (record.StartsWith("R"))
                        {
                            temp = record.Split('|');

                            //Lấy ngày tháng
                            string datetime = record.Split('|')[12];
                            TestResult.TestDate = string.Format("{0}/{1}/{2}", datetime.Substring(6, 2),
                                                                datetime.Substring(4, 2),
                                                                datetime.Substring(0, 4));

                            // Lấy tên loại XN
                            string strParaName = temp[2].Split('^')[4];
                            if (temp[2].EndsWith("F"))
                            {
                                //set định tính cho 3 thông số XN for 1C VDuc

                                if ((strParaName == "Anti-HCV") || (strParaName == "HBsAg Qual") ||
                                    (strParaName == "HIV Ag/Ab"))
                                    temp[3] = Convert.ToDouble(temp[3]) > 3
                                                  ? temp[3] + strDuongtinh
                                                  : temp[3] + strAmtinh;
                                //Insert KQ
                                AddResult(new ResultItem(strParaName, temp[3].Trim()));
                            }
                        }
                    }
                    ImportResults();
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText("C:/CI4100-ERROR.txt", ex.ToString());
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
                throw ex;
            }
        }
    }
}