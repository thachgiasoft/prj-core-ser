using System;
using System.Collections.Generic;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class Clinitek : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                Log.Trace(DeviceHelper.CRLF + StringData);

                var arrPatients = SeparatorData(StringData);
                Log.Trace("Result has {0} Patients", arrPatients.Length);

                //Duyệt từng bệnh nhân để xử lý dữ liệu
                foreach (var record in arrPatients)
                {
                    try
                    {
                        var strInput = record;
                        //Loại bỏ các ký tự không phải là chữ hoặc số
                        while (!Char.IsLetterOrDigit(strInput, 0)) strInput = strInput.Remove(0);
                        var strResult = strInput.Split(',');
                        
                        //Nếu dữ liệu nhỏ hơn 12 dòng là không hợp lệ
                        if (strResult.Length < 40) continue;
                        //Tìm đến index của GLU
                        var idGlu = -1;
                        for (var i = 0; i < strResult.Length; i++)
                            if (strResult[i].StartsWith("GLU")) idGlu = i;

                        //Nếu không tìm thấy phần tử GLU nào thì bỏ qua.
                        if (idGlu == -1) continue;

                        //Nếu tìm thấy GLU nhưng độ dài chuỗi không đủ => bỏ qua
                        if (strResult.Length < (idGlu + 30)) continue;

                        // Bắt đầu xử lý dữ liệu
                        TestResult.TestSequence = strResult[0];
                        TestResult.TestDate = strResult[1].Trim().Replace('-', '/');
                        TestResult.Barcode = strResult[4].Trim();
                        Log.Debug("Barcode:{0}, TestDate:{1}", TestResult.Barcode, TestResult.TestDate);

                        for (var i = 12; i < 40; i = i + 3)
                        {
                            if (strResult[i + 2].IndexOf('*') < 0)
                            {
                                var testName = strResult[i].Trim();
                                var testValue = strResult[i + 1].Split(' ')[0];
                                AddResult(new ResultItem(testName, testValue));
                                Log.Debug("Add Result Item success: TestName={0}, TestValue={1}", testName,testValue);
                            }
                            else
                            {
                                var testName = strResult[i].Trim();
                                var testValue = string.Format("{0}**",strResult[i + 1].Replace("Ca", "").Trim().Split(' ')[0]);
                                AddResult(new ResultItem(testName, testValue));
                                Log.Debug("Add Result Item success: TestName={0}, TestValue={1}", testName, testValue);
                            }
                        }
                        Log.Debug(string.Format("Import Result For barocde:{0}", TestResult.Barcode));
                        ImportResults();
                        Log.Debug(string.Format("Import Result Success"));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing data {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                throw;
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
                var arrStringData = stringData.Split(DeviceHelper.CR);
                arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData);
                return arrStringData;
            }
            catch (Exception ex)
            {
                Log.Error("Error while parsing data {0}Error:{1}", DeviceHelper.CRLF, ex.ToString());
                throw ex;
            }
        }

        #endregion
    }
}