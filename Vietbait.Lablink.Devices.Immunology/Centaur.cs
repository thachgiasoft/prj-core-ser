
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using NLog;
using Vietbait.Lablink.Devices.Immunology.CentaurDevice;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;
using System.Timers;

namespace Vietbait.Lablink.Devices.Immunology
{
    public class Centaur : Rs232Base//,IRS232Communication
    {
       
        private readonly CentaurHeaderRecord _clsHRecord;
        private CentaurOrderRecord _clsORecord;
        private CentaurPatientRecord _clsPRecord;
        private CentaurQueryRecord _clsQRecord;
        private CentaurResultRecord _clsRRecord;
        private CentaurTerminationRecord _clsTRecord;
        private string _prvTestOrder;
        private string _tempbuffer;
        private bool _newResult;
        private bool _newQuery;
        private string _sQBarcode;
        private Timer _timeoutManager;
        private byte _failSending;
        
        private string BufferData;
        private Queue<string> PrvRequestArray;
        
        private string prvLastStringRequest;
        private byte _lastState;

        ExternalDataEventArgs objDataEventArgs;
        WorkflowInstance objWorkFlowInstance;
        ClsAstm objASTM = null;
        WorkflowRuntime objWorkFlowRuntime = new WorkflowRuntime();
        Guid InstanceId;
        ExternalDataExchangeService objService;

        public Centaur()
        {
            
            try
            {
                _clsHRecord = new CentaurHeaderRecord();
                _clsPRecord = new CentaurPatientRecord();
                _clsORecord = new CentaurOrderRecord();
                _clsQRecord = new CentaurQueryRecord();
                _clsRRecord = new CentaurResultRecord();
                _clsTRecord = new CentaurTerminationRecord();
                PrvRequestArray = new Queue<string>();
                _failSending = 0;
                _timeoutManager = new Timer(30000);
                _timeoutManager.Elapsed += new ElapsedEventHandler(_timeoutManager_Elapsed);
                objService = new ExternalDataExchangeService();

                InstanceId = Guid.NewGuid();
                objWorkFlowRuntime.AddService(objService);
                objASTM = new ClsAstm();
                objService.AddService(objASTM);
                objASTM.SendACKEvent +=new EventHandler(objASTM_SendACKEvent);
                objASTM.SendNAKEvent +=new EventHandler(objASTM_SendNAKEvent);
                objASTM.SendENQEvent += new EventHandler(objASTM_SendENQEvent);
                objASTM.SendEOTEvent +=new EventHandler(objASTM_SendEOTEvent);
                //objASTM.ACKTimeoutEvent += new EventHandler(objASTM_ACKTimeoutEvent);
                objWorkFlowInstance = objWorkFlowRuntime.CreateWorkflow(typeof(ASTMWorkflow), null, InstanceId);
                objWorkFlowInstance.Start();
                Console.WriteLine(@"Work flow started");

                objDataEventArgs = new ExternalDataEventArgs(InstanceId);
                objDataEventArgs.WaitForIdle = true;
                DumpStateMachine(objWorkFlowRuntime, InstanceId);
            }
            catch (Exception ex)
            {

                Log.FatalException("Fatal Error: ", ex);   
            }
        }

        void _timeoutManager_Elapsed(object sender, ElapsedEventArgs e)
        {
            Log.Debug(@"Timeout expired ...");
            Log.Debug(@"Session closing ...");
            objASTM.CallTimeout(objDataEventArgs);
        }
        

        ~Centaur()
        {
            
            objWorkFlowInstance.Suspend("Closed");
            objASTM = null;
            objWorkFlowRuntime.Dispose();
            objWorkFlowRuntime = null;
            _timeoutManager.Dispose();
        }
        void objASTM_SendACKEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <ACK>");
            SendByte((byte)DeviceHelper.ACK);
        }
        void objASTM_SendNAKEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <NAK>");
            SendByte((byte)DeviceHelper.NAK);
        }
        void objASTM_SendENQEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <ENQ>");
            SendByte((byte)DeviceHelper.ENQ);
        }
        void objASTM_SendEOTEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <EOT>");
            SendByte((byte)DeviceHelper.EOT);
        }
        
        #region Overrides of Rs232Base

        public override void ProcessRawData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Hàm được gọi khi có sự kiện dữ liệu tồn tại trên cổng COM của thiết bị
        /// </summary>
        /// <param name="obj"></param>
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
                    
                    for (int i = 0; i < allbyte.Length; ++i)
                    {
                        byte onebyte = allbyte[i];
                        if (onebyte.Equals((byte)DeviceHelper.ENQ))
                        {
                            Log.Debug(@"Centaur: <ENQ>");
                            DumpStateMachine(objWorkFlowRuntime, InstanceId);
                            objASTM.CallGetENQ(objDataEventArgs);                            
                            
                            //SendByte((byte)DeviceHelper.ACK);
                            //RaiseEventSendENQToWF(null, onebyte);
                            BufferData = _tempbuffer = string.Empty;
                            _timeoutManager.Start();
                        }
                        else if (onebyte.Equals((byte)DeviceHelper.LF))
                        {
                            Log.Debug("Centaur: {0}",_tempbuffer);
                            _timeoutManager.Stop();
                            
                            string temp = DeviceHelper.GetStringAfterCheckSum(_tempbuffer);
                            _tempbuffer = string.Empty;
                            if (temp != string.Empty)
                            {
                                //SendStringData(DeviceHelper.ACK.ToString());
                                objASTM.CallGetRightFrame(objDataEventArgs);
                                BufferData = string.Concat(BufferData, temp);
                            }
                            else
                            {
                                //SendByte((byte)DeviceHelper.NAK);
                                objASTM.CallGetWrongFrame(objDataEventArgs);
                            }
                            _timeoutManager.Start();

                        }
                        else if (onebyte.Equals((byte)DeviceHelper.EOT))
                        {
                            Log.Debug(@"Centaur: <EOT>");
                            objASTM.CallGetEOT(objDataEventArgs);
                            _timeoutManager.Stop();
                            ProcessData();
                            //Send((int)EventID.GetEOT,_newQuery);
                        }
                        else if (onebyte.Equals((byte)DeviceHelper.ACK))
                        {
                            //Send((int) EventID.GetACK);
                            Log.Debug(@"Centaur: <ACK>");
                            DumpStateMachine(objWorkFlowRuntime, InstanceId);
                            _timeoutManager.Stop();
                            objASTM.CallGetACK(objDataEventArgs);
                            if (((PrvRequestArray != null) && (PrvRequestArray.Count != 0))&&(_lastState==0))
                            {
                                _lastState++;
                                prvLastStringRequest = PrvRequestArray.Dequeue();    
                                Log.Debug("Send String Data: {0}",prvLastStringRequest);
                                SendStringData(prvLastStringRequest);
                                _timeoutManager.Start();
                            }
                            else
                            {
                                //SendByte((byte)DeviceHelper.EOT);
                                _timeoutManager.Stop();
                                Log.Debug(@"Session closing ...");
                                objASTM.CallCloseSession(objDataEventArgs);
                                if(PrvRequestArray != null && PrvRequestArray .Count!=0)
                                {
                                    objASTM.CallGetQuery(objDataEventArgs);
                                    _lastState = 0;
                                    _timeoutManager.Start();
                                }
                            }
                        }
                        else if (onebyte.Equals((byte)DeviceHelper.NAK))
                        {
                            
                            _timeoutManager.Stop();
                            Log.Debug(@"Centaur: <NAK>");
                            if (++_failSending < 5)
                            {
                                if (_lastState>0)
                                {
                                    objASTM.CallGetNAK(objDataEventArgs);
                                    SendStringData(prvLastStringRequest);
                                }
                                else
                                {
                                    Log.Debug(@"Get NAK. Session closing ...");
                                    objASTM.CallCloseSession(objDataEventArgs);
                                }
                                
                                _timeoutManager.Start();    
                            }
                            else
                            {
                                Log.Error("Too much NAK! Session closed!");
                                objASTM.CallCloseSession(objDataEventArgs);

                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.FatalException("Fatal Error: ",ex );                
            }
        }

        #endregion

        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public void ProcessData()
        {
            try
            {
                var arrRecords = new string[] { };
                var listRecords = new ArrayList();
                var listFields = new ArrayList();

                if (BufferData != string.Empty)
                {
                    arrRecords = BufferData.Split(new[] { _clsRRecord.RecordDelimiter },
                                                  StringSplitOptions.RemoveEmptyEntries);
                }

                int i = 0;
                while (i < arrRecords.Length)
                {
                    string[] arrFields = arrRecords[i].Split(_clsPRecord.FieldDelimiter);

                    if (arrFields[0].Equals("H"))
                    {
                        while (!arrFields[0].Equals("L"))
                        {
                            if (++i > arrRecords.Length - 1) break;
                            arrFields = arrRecords[i].Split(_clsPRecord.FieldDelimiter);
                            listFields.Add(arrFields);
                        }
                        listRecords.Add(listFields);
                    }

                    foreach (ArrayList l in listRecords)
                    {
                        foreach (string[] fields in l)
                        {
                            if (fields[0].StartsWith(_clsPRecord.RecordType))
                            {
                                _clsPRecord = new CentaurPatientRecord(fields);
                            }
                            else if (fields[0].StartsWith(_clsORecord.RecordType))
                            {
                                _clsORecord = new CentaurOrderRecord(fields);
                                TestResult.Barcode = _clsORecord.Barcode;
                            }
                            else if (fields[0].StartsWith(_clsRRecord.RecordType))
                            {
                                _clsRRecord = new CentaurResultRecord(fields);

                                if(!string.IsNullOrEmpty (_clsRRecord.DataValue.PubFieldData))
                                {
                                    AddResult(new ResultItem(_clsRRecord.TestId,
                                                         _clsRRecord.DataValue.PubFieldData));
                                    TestResult.TestDate = _clsRRecord.TestDate;
                                    _newResult = true;
                                    break;                                    
                                }
                                
                            }
                            else if (fields[0].StartsWith(_clsQRecord.RecordType))
                            {
                                _clsQRecord = new CentaurQueryRecord(fields);
                                _sQBarcode = _clsQRecord.QueryBarcode;
                                _newQuery = true;
                                List< string > regList  = GetRegList(_clsQRecord.QueryBarcode);
                                PrvRequestArray. Enqueue(CreateOrderFrame(regList));
                            }
                        }
                    }
                    ++i;
                }
                if (_newResult)
                {
                    ImportResults();
                    _newResult = false;
                }
                if (_newQuery)
                {
                    //Send((int)EventID.OpenNewOutputSession, _sQBarcode);

                    objASTM.CallGetQuery(objDataEventArgs);
                    _timeoutManager.Start();
                    _timeoutManager.Stop();
                    _lastState = 0;
                    //SendByte((byte)DeviceHelper.ENQ);
                    _newQuery = false;
                }
            }
            catch (Exception ex)
            {
                Log.FatalException("Fatal Error: ", ex);   
            }
        }

        private string CreateOrderFrame(List< string > orderList)
        {
            
            _clsPRecord = new CentaurPatientRecord();
            _clsORecord = new CentaurOrderRecord();
            _clsTRecord = new CentaurTerminationRecord();
            string sTemp = String.Concat("1", _clsHRecord.sHeaderRecord);
            if((orderList!=null)&&(orderList.Count>0))
            {
                sTemp = String.Concat(sTemp, _clsPRecord.CreateData());
                sTemp = String.Concat(sTemp, _clsORecord.CreateData(_sQBarcode, orderList));
                sTemp = String.Concat(sTemp, _clsTRecord.TerminationRecord);
            }
            else
            {
                _clsTRecord.TerminationCode = "I";
                sTemp = String.Concat(sTemp, _clsTRecord.CreateData());
            }
            
            
            sTemp = String.Concat(DeviceHelper.STX, sTemp, DeviceHelper.ETX);
            string checksum = DeviceHelper.GetCheckSumValue(sTemp);
            string orderFrame = String.Concat(sTemp, checksum, DeviceHelper.CRLF);
            Log.Debug("Order frame: <",orderFrame);
            return orderFrame  ;
        }

        //protected void ImportResult(object[] args)
        //{
        //    if (_newResult)
        //    {
        //        ImportResults();
        //        _newResult = false;
        //    }
        //}

        //#region Implementation of IRS232Communication
        ///// <summary >
        ///// Event to communicate the host the result.
        ///// </summary >
        //public event EventHandler<SendDataToHostEventArgs> SendDataToHostEvent;
        //public void SendDataToHost(string response)
        //{
        //    SendDataToHostEventArgs e = new SendDataToHostEventArgs();
        //    e.Response = response;
        //    EventHandler<SendDataToHostEventArgs> sendData = this.SendDataToHostEvent;
        //    if (sendData != null)
        //    {
        //        sendData(this, e);
        //    }
        //}

        //public event EventHandler<RS232EventArguments> SendENQToWF;

        //public void RaiseEventSendENQToWF(Guid instanceID, byte bStatus)
        //{
        //    if (SendENQToWF != null)
        //    {
        //        RS232EventArguments e = new RS232EventArguments(instanceID);
        //        e.ReceiveStatus = bStatus;
        //        SendENQToWF(null, e);
        //    }
        //}
        //#endregion
       

        private static void DumpStateMachine(WorkflowRuntime runtime, Guid instanceID)
        {
            StateMachineWorkflowInstance instance =
                   new StateMachineWorkflowInstance(runtime, instanceID);

            Console.WriteLine("Workflow ID: {0}", instanceID);
            Console.WriteLine("Current State: {0}",
                      instance.CurrentStateName);
            //Console.WriteLine("Possible Transitions: {0}",
            //          instance.StateHistory[0]);
            Console.WriteLine("Possible Transitions: {0}",
                      instance.PossibleStateTransitions.Count);
            
            foreach (string name in instance.PossibleStateTransitions)
            {
                Console.WriteLine("\t{0}", name);
            }
        }
    }
}
