using System;
using System.IO;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class AU480 : Rs232Base
    {
        ExternalDataEventArgs objDataEventArgs;
        WorkflowInstance objWorkFlowInstance;
        OlympusAUComm objAU480 = null;
        WorkflowRuntime objWorkFlowRuntime = new WorkflowRuntime();
        Guid InstanceId;
        private string _tempbuffer=string.Empty;

        public AU480()
        {
            try
            {
                var objService = new ExternalDataExchangeService();

                InstanceId = Guid.NewGuid();
                objWorkFlowRuntime.AddService(objService);
                objAU480 = new OlympusAUComm();
                objService.AddService(objAU480);
                objAU480.SendACKEvent += objAU480_SendACKEvent;
                objAU480.ReturnRequestEvent +=objAU480_ReturnRequestEvent;
                objAU480.ReturnDataEvent += objAU480_ReturnDataEvent;
                objWorkFlowInstance = objWorkFlowRuntime.CreateWorkflow(typeof(OlympusAU), null, InstanceId);
                objWorkFlowInstance.Start();
                Console.WriteLine(@"Work flow started");

                //objDataEventArgs = new ExternalDataEventArgs(InstanceId);

                //objDataEventArgs.WaitForIdle = true;
                //DumpStateMachine(objWorkFlowRuntime, InstanceId);
            }
            catch (Exception)
            {                
                throw;
            }            
        }

        void objAU480_SendACKEvent(object sender, EventArgs e)
        {
            SendByte((byte)DeviceHelper.ACK);
        }

        void objAU480_ReturnDataEvent(object sender, SendDataToHostEventArgs e)
        {
           
               try
               {
                   foreach (string sAUMessage in e.AUStringResult)
                   {

                       
                       Result _singleResult = new Result();
                       _singleResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                       string barcode = sAUMessage.Substring(13, 20).Trim();

                       //lấy barcode bệnh nhân
                       _singleResult.Barcode = barcode.Length < 1 ? "0" : barcode;

                       string tempPatient = sAUMessage, testCode, testValue;

                       string temp = sAUMessage.Substring(28);
                       while (temp.Length > 10)
                       {
                           //lấy tên loại XN
                           testCode = temp.Substring(0, 3).Trim();
                           //Lấy kq
                           testValue = temp.Substring(3, 6).Trim();

                           //add kết quả
                           _singleResult.Add(new ResultItem(testCode, testValue));
                           temp = temp.Remove(0, 11);

                       }
                       DeviceHelper.ImportResultToDb((short)GetTestTypeID(), _singleResult, GetParaNameTable(), GetDeviceID());

                   }
                   
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           
        }
        
        private Boolean ValidData()
        {
            try
            {
                if (String.IsNullOrEmpty(StringData)) return false;
                //  if (StringData.Length <= 4) return false;
                // if (!StringData.StartsWith(DeviceHelper.STX + "DB"))return false;
                if (!StringData.StartsWith(DeviceHelper.STX + "DB" + DeviceHelper.ETX)) return false;
                // if (!StringData.EndsWith(DeviceHelper.STX + DeviceHelper.STX + "DE" + DeviceHelper.ETX)) return false;
                //if (!StringData.EndsWith("DE" + DeviceHelper.ETX)) return false;
                // if (!StringData.EndsWith(DeviceHelper.STX+DeviceHelper.STX + "DE" + DeviceHelper.ETX)) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        void objAU480_ReturnRequestEvent(object sender, SendDataToHostEventArgs e)
        {

            string stringNeg = DeviceHelper.STX+ "SE"+DeviceHelper.ETX ;
            try
            {
                var regList  = GetRegList(e.AUBarcode);
                if((regList != null )||(regList.Count >0))
                {
                    var stringOrder = CreatOrderString(e.AUBarcode, regList.ToArray(), e.AURackNo, e.AUPosNo, e.AUSampleNo);
                    SendStringData(stringOrder);
                }
                else SendStringData(stringNeg);
                
                
            }
            catch (Exception)
            {
                SendStringData(stringNeg);
                throw;
            }
        }

        //public override void ProcessData()
        //{
        //    try
        //    {
        //        //Kiểm tra dữ liệu
        //        // if (!ValidData()) return;
        //        File.AppendAllText(@"C:\AU480-KQ.txt", StringData);

        //        string[] arrPatients = SeparatorData(StringData);

        //        foreach (string patient in arrPatients)
        //        {
        //            // Lấy ngày tháng năm hiện tại
        //            TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
        //            string barcode = patient.Substring(13, 30).Trim();

        //            //lấy barcode bệnh nhân
        //            TestResult.Barcode = barcode.Length < 1 ? "0" : barcode;

        //            string tempPatient = patient, testName, testValue;

        //            string temp = tempPatient.Substring(44);
        //            while (temp.Length >= 10)
        //            {
        //                //lấy tên loại XN
        //                testName = temp.Substring(0, 4).Trim();
        //                //Lấy kq
        //                testValue = temp.Substring(4, 7).Trim();

        //                //Xử lý các ký tự thừa trong kết quả

        //                //Loại bỏ dấu "$,"r"
        //                testValue = testValue.Replace("r", "").Replace("$", "");

        //                //Loại bỏ các chữ cái in hoa và in thường trong chuỗi kết quả
        //                for (byte i = 65; i <= 90; i++)
        //                {
        //                    testValue =
        //                        testValue.Replace(Convert.ToChar(i).ToString(), "").Replace(
        //                            Convert.ToChar(i + 32).ToString(), "");
        //                }
        //                //add kết quả
        //                TestResult.Add(new ResultItem(testName, testValue));
        //                temp = temp.Substring(10);
        //            }
        //            //Lưu vào db
        //            ImportResults();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        File.AppendAllText(@"C:\AU480-ERROR.txt", ex.ToString());
        //    }
        //    finally
        //    {
        //        ClearData();
        //    }
        //}

        public override void ProcessData()
        {
            throw new NotImplementedException();
        }

        public override void OnInCommingData(object obj)
        {
            try
            {
                // Chuyển kiểu dữ liệu về dạng DataReceiveRs232 do lớp Base Devices chỉ gửi Object
                var rawData = (DataReceiveRs232)obj;
                if (rawData != null)
                {
                    // Nếu chưa gán Port thì gán
                    SetNetStream(rawData.Port);
                    byte[] allbyte = rawData.Data;
                    _tempbuffer = string.Concat(_tempbuffer, Encoding.ASCII.GetString(allbyte));
                    if(_tempbuffer.EndsWith(DeviceHelper.STX + "DE" + DeviceHelper.ETX))
                    {
                        if(_tempbuffer.StartsWith( DeviceHelper.STX + "DB" + DeviceHelper.ETX))
                        {
                            _tempbuffer = _tempbuffer.Substring(4,_tempbuffer.Length - 8);
                            if (!string.IsNullOrEmpty(_tempbuffer)) objAU480.RaiseEventSendDataToWF(InstanceId, _tempbuffer, 1);
                            
                        }
                    }
                    else if (_tempbuffer.EndsWith(DeviceHelper.STX + "RE" + DeviceHelper.ETX))
                    {
                        if (_tempbuffer.StartsWith(DeviceHelper.STX + "RB" + DeviceHelper.ETX))
                        {
                            _tempbuffer = _tempbuffer.Substring(4, _tempbuffer.Length - 8);
                            if (!string.IsNullOrEmpty(_tempbuffer)) objAU480.RaiseEventSendDataToWF(InstanceId, _tempbuffer, 0);
                        }
                    }

                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private string CreatOrderString(string  sid, string[] testID, string  rackNo  , string posNo  , string sampleNo   )  
        {
            string[] sTemp=new string[8+testID.GetLength(0)];
            sTemp[0] = DeviceHelper.STX.ToString();                     
            sTemp[1] = "S ";
            sTemp[2] = rackNo;
            sTemp[3] = posNo;
            sTemp[4] = " ";
            sTemp[5] = sampleNo;
            sTemp[6] = sid.PadLeft(20);
            sTemp[7] = "    ";          //Dummy
            sTemp[8] = "E";             //Last block
            for (byte i=0; i< testID.GetLength(0);++i )
            {
                sTemp[9 + i] = string.Concat(testID[i].PadLeft(3),"0");
            }
            return string.Concat(string.Concat(sTemp), DeviceHelper.ETX.ToString());
        }
        //private string[] SeparatorData(string stringData)
        //{
        //    try
        //    {
        //        //loại bỏ các ký tự "DB", "DE"
        //        stringData = stringData.Replace(DeviceHelper.STX + "DB" + DeviceHelper.ETX, "");
        //        stringData = stringData.Replace(DeviceHelper.STX + "DE" + DeviceHelper.ETX, "");
        //        string[] arrStringData = stringData.Split(DeviceHelper.STX, DeviceHelper.ETX);
        //        arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData);

        //        return arrStringData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}