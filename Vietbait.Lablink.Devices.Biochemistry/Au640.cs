﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Au640 : Rs232Base
    {
        public override void ProcessRawData()
        {
            try
            {
                //Kiểm tra dữ liệu
                if (!ValidData()) return;
                Log.Debug("Begin Process Data");
                Log.Debug("Raw Data \n: {0}", StringData);
                IEnumerable<string> arrPatients = SeparatorData(StringData);
                foreach (string patient in arrPatients)
                {
                    if (!patient.StartsWith("D ")) continue;
                    if (patient.Length < 44) continue;
                    // Lấy ngày tháng năm hiện tại
                    TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                    string barcode = patient.Substring(15, 22).Trim();

                    //lấy barcode bệnh nhân
                    TestResult.Barcode = barcode.Length < 1 ? "0" : barcode;

                    string tempPatient = patient;

                    string temp = tempPatient.Substring(44);
                    while (temp.Length >= 9)
                    {
                        //lấy tên loại XN
                        string testName = temp.Substring(0, 2).Trim();
                        //Lấy kq
                        string testValue = temp.Substring(2, 8).Trim();

                        //Xử lý các ký tự thừa trong kết quả

                        //Loại bỏ dấu "$,"r"
                        testValue = testValue.Replace("r", "").Replace("$", "");

                        //Loại bỏ các chữ cái in hoa và in thường trong chuỗi kết quả
                        for (byte i = 65; i <= 90; i++)
                            testValue =
                                testValue.Replace(Convert.ToChar(i).ToString(CultureInfo.InvariantCulture), "").Replace(
                                    Convert.ToChar(i + 32).ToString(CultureInfo.InvariantCulture), "");
                        //add kết quả
                        TestResult.Add(new ResultItem(testName, testValue));
                        temp = temp.Substring(10);
                    }
                    //Lưu vào db
                    Log.Debug("Begin Import Result");
                    Log.Debug(ImportResults() ? "Import Success" : "Import false");
                }
                ClearData();
            }
            catch (Exception ex)
            {
                ClearData();
                Log.Error("Error while process Data - Error:{0}", ex.ToString());
            }
        }

        /// <summary>
        /// Tách dữ liệu
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns></returns>
        private IEnumerable<string> SeparatorData(string stringData)
        {
            try
            {
                //loại bỏ các ký tự "DB", "DE"
                string[] arrStringData = stringData.Split(DeviceHelper.STX, DeviceHelper.ETX);
                arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData, false);
                return arrStringData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidData()
        {
            if (StringData.EndsWith(DeviceHelper.ETX.ToString(CultureInfo.InvariantCulture)))
            {
                SendByte((byte) DeviceHelper.ACK);
                return true;
            }
            return false;
        }
    }
}