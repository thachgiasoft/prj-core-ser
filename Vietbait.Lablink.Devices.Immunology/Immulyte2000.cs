using System;
using System.Collections.Generic;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Immunology
{
    internal class Immulyte2000 : Rs232AstmDevice
    {
        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                Log.Debug("Begin Process Data");
                Log.Debug("RawData: {0}{1}{0}", DeviceHelper.CRLF, StringData);
                //Lấy về dữ liệu của các bệnh nhân
                var arrPatients = SeparatorData(StringData);

                foreach (var patient in arrPatients)
                {
                    foreach (var record in patient.Split(DeviceHelper.CRLF.ToCharArray()))
                    {
                        string[] temp;
                        if (record.StartsWith("O"))
                        {
                            temp = record.Split('|');
                            ////Barcode
                            TestResult.Barcode = temp[2].Trim();
                        }
                        else if (record.StartsWith("R"))
                        {
                            temp = record.Split('|');

                            //Lấy ngày tháng
                            var datetime = record.Split('|')[12];
                            TestResult.TestDate = string.Format("{0}/{1}/{2}", datetime.Substring(6, 2),
                                                                datetime.Substring(4, 2),
                                                                datetime.Substring(0, 4));

                            // Lấy tên loại XN
                            var strParaName = temp[2].Split('^')[3];
                            var strResult = temp[3].Trim();
                            var strUnit = temp[4].Trim();
                            AddResult(new ResultItem(strParaName, strResult,strUnit));
                        }
                    }
                    Log.Debug("Begin Import Result");
                    Log.Debug(ImportResults()?"Import Result Success":"Error while import result");
                    
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while process data: {0}", ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        private IEnumerable<string> SeparatorData(string stringData)
        {
            //Ngắt chuỗi theo ký tự CR
            string[] arrStringData = stringData.Split(DeviceHelper.CRLF.ToCharArray());
            arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData);

            var allResult = new string[] {};

            //Biến lưu Index
            var id = -1;

            foreach (var line in arrStringData)
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
    }
}