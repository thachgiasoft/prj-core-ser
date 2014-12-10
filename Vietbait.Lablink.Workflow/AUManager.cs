using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using Vietbait.Lablink.Devices;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Workflow
{
    public abstract class AUManager : Rs232Base //,IRS232Communication
    {
        #region DelegateRegion

        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public abstract bool ProcessData(string inputBuffer, ref List<string> orderList);

        #endregion

        private readonly Guid InstanceId;
        private readonly Queue<List<string>> _prvRequestArray;
        //private readonly Timer _timeoutManager;
        private readonly ExternalDataEventArgs _objDataEventArgs;
        private readonly ExternalDataExchangeService _objService;
        private readonly WorkflowInstance _objWorkFlowInstance;

        private string _bufferData;
        private List<string> _currentOrder;
        private byte _currentOrderIdx;
        private byte _failSending;

        private byte _lastState;
        private string _prvLastStringRequest;
        private string _tempbuffer;

        private ClsAu _objAu;
        private WorkflowRuntime _objWorkFlowRuntime = new WorkflowRuntime();

        public AUManager()
        {
            try
            {
                _prvRequestArray = new Queue<List<string>>();
                _currentOrder = new List<string>();
                _failSending = 0;
                //_timeoutManager = new Timer(60000);
                //_timeoutManager.Elapsed += _timeoutManager_Elapsed;
                _objService = new ExternalDataExchangeService();

                InstanceId = Guid.NewGuid();
                _objWorkFlowRuntime.AddService(_objService);
                _objAu = new ClsAu();
                _objService.AddService(_objAu);
                _objAu.SendACKEvent += new EventHandler(ObjAuSendACKEvent);
                _objAu.SendNAKEvent += new EventHandler(ObjAuSendNAKEvent);
                
                //objASTM.ACKTimeoutEvent += new EventHandler(objASTM_ACKTimeoutEvent);
                _objWorkFlowInstance = _objWorkFlowRuntime.CreateWorkflow(typeof (AUMachineWorkflow), null, InstanceId);
                _objWorkFlowInstance.Start();
                Console.WriteLine(@"Work flow started");

                _objDataEventArgs = new ExternalDataEventArgs(InstanceId);
                _objDataEventArgs.WaitForIdle = true;
                //DumpStateMachine(_objWorkFlowRuntime, InstanceId);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error: {0}", ex));
            }
        }

        //private void _timeoutManager_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    Log.Debug(@"Timeout expired ...");
        //    Log.Debug(@"Session closing ...");
        //    //_objAu.CallTimeout(_objDataEventArgs);
        //    //_timeoutManager.Stop();
        //}


      ~ AUManager()
        {
            _objWorkFlowInstance.Suspend("Closed");
            _objAu = null;
            _objWorkFlowRuntime.Dispose();
            _objWorkFlowRuntime = null;
            //_timeoutManager.Dispose();
        }

        private void ObjAuSendACKEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <ACK>");
            SendByte((byte) DeviceHelper.ACK);
        }

        private void ObjAuSendNAKEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <NAK>");
            SendByte((byte) DeviceHelper.NAK);
        }

        //private void objASTM_SendENQEvent(object sender, EventArgs e)
        //{
        //    Log.Debug(@"Host: <ENQ>");
        //    SendByte((byte) DeviceHelper.ENQ);
        //    _lastState = 0;
        //    _failSending = 0;
        //}

        //private void objASTM_SendEOTEvent(object sender, EventArgs e)
        //{
        //    Log.Debug(@"Host: <EOT>");
        //    SendByte((byte) DeviceHelper.EOT);
        //}

        //private static void DumpStateMachine(WorkflowRuntime runtime, Guid instanceID)
        //{
        //    var instance =
        //        new StateMachineWorkflowInstance(runtime, instanceID);

        //    Console.WriteLine("Workflow ID: {0}", instanceID);
        //    Console.WriteLine("Current State: {0}",
        //                      instance.CurrentStateName);
        //    Console.WriteLine("Possible Transitions: {0}",
        //                      instance.PossibleStateTransitions.Count);

        //    foreach (string name in instance.PossibleStateTransitions)
        //    {
        //        Console.WriteLine("\t{0}", name);
        //    }
        //}

        #region Overrides of Rs232Base

        public override void ProcessRawData()
        {
            try
            {
                _tempbuffer = string.Concat(_tempbuffer, StringData);

                if ((_tempbuffer.Equals(string.Format(@"{0}RB{1}", DeviceHelper.STX, DeviceHelper.ETX))) || (_tempbuffer.Equals(string.Format(@"{0}DB{1}", DeviceHelper.STX, DeviceHelper.ETX))) || (_tempbuffer.EndsWith(string.Format(@"RB")))|| (_tempbuffer.EndsWith(string.Format(@"DB"))))
                {
                    Log.Debug(@"AU Machine: {0}", _tempbuffer);
                    //DumpStateMachine(_objWorkFlowRuntime, InstanceId);
                    //_objAu.CallGetENQ(_objDataEventArgs);
                    Log.Debug(@"Host: <ACK>");
                    SendByte((byte)DeviceHelper.ACK);
                    _bufferData = _tempbuffer = string.Empty;
                    //_timeoutManager.Stop();
                    //_timeoutManager.Start();
                }
                else if (((_tempbuffer.StartsWith(string.Format(@"{0}R ", DeviceHelper.STX)))
                    || (_tempbuffer.StartsWith(string.Format(@"{0}RH", DeviceHelper.STX)))
                    || (_tempbuffer.StartsWith(string.Format(@"{0}Rh", DeviceHelper.STX)))
                    || (_tempbuffer.ToUpper().StartsWith(string.Format(@"{0}D ", DeviceHelper.STX)))
                    || (_tempbuffer.ToUpper().StartsWith(string.Format(@"{0}d ", DeviceHelper.STX)))
                    || (_tempbuffer.ToUpper().StartsWith(string.Format(@"{0}DH", DeviceHelper.STX)))
                    || (_tempbuffer.ToUpper().StartsWith(string.Format(@"{0}Dh", DeviceHelper.STX))))
                    && (_tempbuffer.EndsWith(DeviceHelper.ETX.ToString())))
                {
                    Log.Debug("AU Machine: {0}", _tempbuffer);
                    //_timeoutManager.Stop();

                    string temp = _tempbuffer;
                    _tempbuffer = string.Empty;
                    if (temp != string.Empty)
                    {
                        //xu ly thêm trường hợp lỗi
                        Log.Debug("AU Machine: {0}", temp);
                        Log.Debug(@"Host: <ACK>");
                        SendByte((byte)DeviceHelper.ACK);
                        //SendStringData(DeviceHelper.ACK.ToString());
                        _objAu.CallGetUnparseData(_objDataEventArgs);
                        _bufferData = string.Concat(_bufferData, temp);
                        //_timeoutManager.Stop();
                        var orderString = new List<string>();
                        if (ProcessData(_bufferData, ref orderString))
                        {
                            _objAu.CallGetQuery(_objDataEventArgs);
                            _prvLastStringRequest = orderString[0];

                            Log.Debug("Send String Data Ok: {0}", _prvLastStringRequest);
                            SendStringData(_prvLastStringRequest);
                            //_timeoutManager.Start();
                        }
                        else if (_prvLastStringRequest==" ")
                        {
                            _prvLastStringRequest = string.Concat(DeviceHelper.STX,"SE",DeviceHelper.ETX);
                            Log.Debug("Send String Data Not Ok: {0}", _prvLastStringRequest);
                        }
                        else
                        {
                            _objAu.CallGetData(_objDataEventArgs);
                        }
                        _bufferData = string.Empty;

                    }
                    //_timeoutManager.Start();
                }
                else if ((_tempbuffer.Equals(string.Format(@"{0}RE{1}", DeviceHelper.STX, DeviceHelper.ETX)))
                    || (_tempbuffer.Equals(string.Format(@"{0}DE{1}", DeviceHelper.STX, DeviceHelper.ETX))) || (_tempbuffer.EndsWith(string.Format(@"RE"))) || (_tempbuffer.EndsWith(string.Format(@"DE"))) || (_tempbuffer.Equals(string.Format(@"DE"))) || (_tempbuffer.Equals(string.Format(@"RE"))))
                {
                    Log.Debug(@"AU Machine: {0}", _tempbuffer);
                    _bufferData = _tempbuffer = string.Empty;
                    Log.Debug(@"Host: <ACK>");
                    SendByte((byte)DeviceHelper.ACK);
                    //_objAu.CallGetEOT(_objDataEventArgs);


                    //Send((int)EventID.GetEOT,_newQuery);
                }
                else if (_tempbuffer.Equals(DeviceHelper.ACK.ToString())||_tempbuffer.EndsWith(DeviceHelper.ACK.ToString()))
                {
                    //Send((int) EventID.GetACK);
                    Log.Debug(@"ASTM Machine: <ACK>");
                    //DumpStateMachine(_objWorkFlowRuntime, InstanceId);
                    //_timeoutManager.Stop();
                    _objAu.CallGetACK(_objDataEventArgs);
                    _tempbuffer = string.Empty;
                }
                else if (_tempbuffer.Equals(DeviceHelper.NAK.ToString()) || _tempbuffer.EndsWith(DeviceHelper.NAK.ToString()))
                {
                    //_timeoutManager.Stop();
                    _tempbuffer = string.Empty;
                    Log.Debug(@"ASTM Machine: <NAK>");
                    if (++_failSending < 5)
                    {
                        if (_lastState > 0)
                        {
                            _objAu.CallGetNAK(_objDataEventArgs);
                            SendStringData(_prvLastStringRequest);
                        }
                        //if NAK after ENQ sent then session must be closed
                        else
                        {
                            Log.Debug(@"Get NAK after ENQ sent. Session closing ...");
                            //_objAu.CallCloseSession(_objDataEventArgs);
                        }

                        //_timeoutManager.Start();
                    }
                    else
                    {
                        Log.Error("Too much NAK! Session closed!");
                        //_objAu.CallCloseSession(_objDataEventArgs);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.FatalException("Lỗi cbn rồi!!!",ex);
                throw;
            }
            finally
            {
                ClearData();                
            }
            
        }
    //    public override void OnInCommingData(object obj)
    //{
    //    try
    //    {
    //        // Chuyển kiểu dữ liệu về dạng DataReceiveRs232 do lớp Base Devices chỉ gửi Object
    //        var rawData = (DataReceiveRs232) obj;
    //        if (rawData != null)
    //        {
    //            // Nếu chưa gán Port thì gán
    //            SetNetStream(rawData.Port);

    //            byte[] allbyte = rawData.Data;
    //            _tempbuffer = string.Concat(_tempbuffer, Encoding.ASCII.GetString(allbyte));

    //            if ((_tempbuffer.Equals(string.Format(@"{0}RB{1}", DeviceHelper.STX, DeviceHelper.ETX)))||(_tempbuffer.Equals(string.Format(@"{0}DB{1}", DeviceHelper.STX, DeviceHelper.ETX))))
    //            {
    //                Log.Debug(@"AU Machine: {0}", _tempbuffer);
    //                //DumpStateMachine(_objWorkFlowRuntime, InstanceId);
    //                //_objAu.CallGetENQ(_objDataEventArgs);
    //                Log.Debug(@"Host: <ACK>");
    //                SendByte((byte)DeviceHelper.ACK);
    //                _bufferData = _tempbuffer = string.Empty;
    //                //_timeoutManager.Stop();
    //                //_timeoutManager.Start();
    //            }
    //            else if (((_tempbuffer.StartsWith(string.Format(@"{0}R ", DeviceHelper.STX))) 
    //                || (_tempbuffer.StartsWith(string.Format(@"{0}RH", DeviceHelper.STX )))
    //                || (_tempbuffer.StartsWith(string.Format(@"{0}Rh", DeviceHelper.STX)))
    //                ||(_tempbuffer.ToUpper().StartsWith(string.Format(@"{0}D ", DeviceHelper.STX)))
    //                || (_tempbuffer.ToUpper().StartsWith(string.Format(@"{0}DH", DeviceHelper.STX)))
    //                || (_tempbuffer.ToUpper().StartsWith(string.Format(@"{0}Dh", DeviceHelper.STX))))
    //                && (_tempbuffer.EndsWith(DeviceHelper.ETX.ToString())))
    //            {
    //                Log.Debug("AU Machine: {0}", _tempbuffer);
    //                //_timeoutManager.Stop();

    //                string temp = _tempbuffer;
    //                _tempbuffer = string.Empty;
    //                if (temp != string.Empty)
    //                {
    //                    //SendStringData(DeviceHelper.ACK.ToString());
    //                    _objAu.CallGetUnparseData(_objDataEventArgs);
    //                    _bufferData = string.Concat(_bufferData, temp);
    //                    //_timeoutManager.Stop();
    //                    var orderString = new List<string>();
    //                    if (ProcessData(_bufferData, ref orderString))
    //                    {
    //                        _objAu.CallGetQuery(_objDataEventArgs);
    //                        _prvLastStringRequest = orderString[0];
                            
    //                        Log.Debug("Send String Data: {0}", _prvLastStringRequest);
    //                        SendStringData(_prvLastStringRequest);
    //                        //_timeoutManager.Start();
    //                    }
    //                    else
    //                    {
    //                        _objAu.CallGetData(_objDataEventArgs);
    //                    }
    //                    _bufferData = string.Empty;

    //                }
    //                //_timeoutManager.Start();
    //            }
    //            else if ((_tempbuffer.Equals(string.Format(@"{0}RE{1}", DeviceHelper.STX, DeviceHelper.ETX))) 
    //                || (_tempbuffer.Equals(string.Format(@"{0}DE{1}", DeviceHelper.STX, DeviceHelper.ETX))))
    //            {
    //                Log.Debug(@"AU Machine: {0}",_tempbuffer);
    //                _bufferData = _tempbuffer = string.Empty;
    //                Log.Debug(@"Host: <ACK>");
    //                SendByte((byte)DeviceHelper.ACK);
    //                //_objAu.CallGetEOT(_objDataEventArgs);
                    

    //                //Send((int)EventID.GetEOT,_newQuery);
    //            }
    //            else if (_tempbuffer.Equals(DeviceHelper.ACK.ToString()))
    //            {
    //                //Send((int) EventID.GetACK);
    //                Log.Debug(@"ASTM Machine: <ACK>");
    //                //DumpStateMachine(_objWorkFlowRuntime, InstanceId);
    //                //_timeoutManager.Stop();
    //                _objAu.CallGetACK(_objDataEventArgs);
    //                _tempbuffer = string.Empty;
    //            }
    //            else if (_tempbuffer.Equals(DeviceHelper.NAK.ToString()))
    //            {
    //                //_timeoutManager.Stop();
    //                _tempbuffer = string.Empty;
    //                Log.Debug(@"ASTM Machine: <NAK>");
    //                if (++_failSending < 5)
    //                {
    //                    if (_lastState > 0)
    //                    {
    //                        _objAu.CallGetNAK(_objDataEventArgs);
    //                        SendStringData(_prvLastStringRequest);
    //                    }
    //                    //if NAK after ENQ sent then session must be closed
    //                    else
    //                    {
    //                        Log.Debug(@"Get NAK after ENQ sent. Session closing ...");
    //                        //_objAu.CallCloseSession(_objDataEventArgs);
    //                    }

    //                    //_timeoutManager.Start();
    //                }
    //                else
    //                {
    //                    Log.Error("Too much NAK! Session closed!");
    //                    //_objAu.CallCloseSession(_objDataEventArgs);
    //                }
    //            }
    //            //for (int i = 0; i < allbyte.Length; ++i)
    //            //{
    //            //    byte onebyte = allbyte[i];
    //            //    if (onebyte.Equals((byte) DeviceHelper.ENQ))
    //            //    {
    //            //        Log.Debug(@"ASTM Machine: <ENQ>");
    //            //        DumpStateMachine(objWorkFlowRuntime, InstanceId);
    //            //        objASTM.CallGetENQ(objDataEventArgs);
    //            //        BufferData = _tempbuffer = string.Empty;
    //            //        _timeoutManager.Stop();
    //            //        _timeoutManager.Start();
    //            //    }
    //            //    else if (onebyte.Equals((byte) DeviceHelper.LF))
    //            //    {
    //            //        Log.Debug("ASTM Machine: {0}", _tempbuffer);
    //            //        _timeoutManager.Stop();

    //            //        string temp = DeviceHelper.GetStringAfterCheckSum(_tempbuffer);
    //            //        _tempbuffer = string.Empty;
    //            //        if (temp != string.Empty)
    //            //        {
    //            //            //SendStringData(DeviceHelper.ACK.ToString());
    //            //            objASTM.CallGetRightFrame(objDataEventArgs);
    //            //            BufferData = string.Concat(BufferData, temp);
    //            //        }

    //            //        else
    //            //        {
    //            //            //SendByte((byte)DeviceHelper.NAK);
    //            //            objASTM.CallGetWrongFrame(objDataEventArgs);
    //            //        }
    //            //        _timeoutManager.Start();
    //            //    }
    //            //    else if (onebyte.Equals((byte) DeviceHelper.EOT))
    //            //    {
    //            //        Log.Debug(@"ASTM Machine: <EOT>");
    //            //        objASTM.CallGetEOT(objDataEventArgs);
    //            //        _timeoutManager.Stop();
    //            //        var orderString = new List<string>();
    //            //        if (ProcessData(BufferData, ref orderString))
    //            //        {
    //            //            if (_prvRequestArray != null)
    //            //            {
    //            //                _prvRequestArray.Enqueue(orderString);
    //            //            }
    //            //        }
    //            //        if (_prvRequestArray != null && _prvRequestArray.Count != 0)
    //            //        {
    //            //            Log.Debug(@"Number of order in queue: {0}", _prvRequestArray.Count);
    //            //            objASTM.CallGetQuery(objDataEventArgs);

    //            //            _timeoutManager.Start();
    //            //        }

    //            //        //Send((int)EventID.GetEOT,_newQuery);
    //            //    }
    //            //    else if (onebyte.Equals((byte) DeviceHelper.ACK))
    //            //    {
    //            //        //Send((int) EventID.GetACK);
    //            //        Log.Debug(@"ASTM Machine: <ACK>");
    //            //        DumpStateMachine(objWorkFlowRuntime, InstanceId);
    //            //        _timeoutManager.Stop();
    //            //        objASTM.CallGetACK(objDataEventArgs);
    //            //        if (_lastState == 0)
    //            //        {
    //            //            _currentOrder = _prvRequestArray.Dequeue();
    //            //            _currentOrderIdx = 0;
    //            //        }
    //            //        _lastState++;
    //            //        if (((_currentOrder != null) && (_currentOrder.Count != 0)) &&
    //            //            (_currentOrderIdx < _currentOrder.Count))
    //            //        {
    //            //            _prvLastStringRequest = _currentOrder[_currentOrderIdx];
    //            //            _currentOrderIdx++;
    //            //            Log.Debug("Send String Data: {0}", _prvLastStringRequest);
    //            //            SendStringData(_prvLastStringRequest);
    //            //            _timeoutManager.Start();
    //            //        }
    //            //        else
    //            //        {
    //            //            //SendByte((byte)DeviceHelper.EOT);
    //            //            _timeoutManager.Stop();
    //            //            Log.Debug(@"Session closing ...");
    //            //            objASTM.CallCloseSession(objDataEventArgs);

    //            //            if (_prvRequestArray != null && _prvRequestArray.Count != 0)
    //            //            {
    //            //                objASTM.CallGetQuery(objDataEventArgs);

    //            //                _timeoutManager.Start();
    //            //            }
    //            //        }
    //            //    }
    //            //    else if (onebyte.Equals((byte) DeviceHelper.NAK))
    //            //    {
    //            //        _timeoutManager.Stop();
    //            //        Log.Debug(@"ASTM Machine: <NAK>");
    //            //        if (++_failSending < 5)
    //            //        {
    //            //            if (_lastState > 0)
    //            //            {
    //            //                objASTM.CallGetNAK(objDataEventArgs);
    //            //                SendStringData(_prvLastStringRequest);
    //            //            }
    //            //                //if NAK after ENQ sent then session must be closed
    //            //            else
    //            //            {
    //            //                Log.Debug(@"Get NAK after ENQ sent. Session closing ...");
    //            //                objASTM.CallCloseSession(objDataEventArgs);
    //            //            }

    //            //            _timeoutManager.Start();
    //            //        }
    //            //        else
    //            //        {
    //            //            Log.Error("Too much NAK! Session closed!");
    //            //            objASTM.CallCloseSession(objDataEventArgs);
    //            //        }
    //            //    }
    //            //}
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Error(string.Format("Error: {0}", ex));
    //    }
        
    //}

        #endregion
    }
}