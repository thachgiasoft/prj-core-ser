using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Immunology
{
    public class Cobase411_Bi : Rs232Base 
    {
        private readonly Guid _instanceId;
        private readonly Queue<List<string>> _prvRequestArray;
        private readonly ExternalDataEventArgs _objDataEventArgs;
        private readonly ExternalDataExchangeService _objService;
        private readonly WorkflowInstance _objWorkFlowInstance;
        private string _bufferData;
        private CobasE411HeaderRecord _clsHRecord;
        private CobasE411TestOrderRecord _clsORecord;
        private CobasE411PatientInformationRecord _clsPRecord;
        private CobasE411RequestInformationRecord _clsQRecord;
        private CobasE411ResultRecord _clsRRecord;
        private CobasE411TerminationRecord _clsTRecord;
        private bool _newQuery;
        private bool _newResult;
        private string _sQBarcode;
        private string _tempbuffer;

        private List<string> _currentOrder = new List<string>();
        private byte _currentOrderIdx;
        private ClsAstm _objAstm;
        private WorkflowRuntime _objWorkFlowRuntime = new WorkflowRuntime();
        private string _prvLastStringRequest;

        public Cobase411_Bi()
        {
            try
            {
                _clsHRecord = new CobasE411HeaderRecord();
                _clsPRecord = new CobasE411PatientInformationRecord();
                _clsORecord = new CobasE411TestOrderRecord();
                _clsQRecord = new CobasE411RequestInformationRecord();
                _clsRRecord = new CobasE411ResultRecord();
                _clsTRecord = new CobasE411TerminationRecord();

                _prvRequestArray = new Queue<List<string>>();
                _objService = new ExternalDataExchangeService();

                _instanceId = Guid.NewGuid();
                _objWorkFlowRuntime.AddService(_objService);
                _objAstm = new ClsAstm();
                _objService.AddService(_objAstm);
                _objAstm.SendACKEvent += objASTM_SendACKEvent;
                _objAstm.SendNAKEvent += objASTM_SendNAKEvent;
                _objAstm.SendENQEvent += objASTM_SendENQEvent;
                _objAstm.SendEOTEvent += objASTM_SendEOTEvent;
                _objWorkFlowInstance = _objWorkFlowRuntime.CreateWorkflow(typeof (ASTMWorkflow), null, _instanceId);
                _objWorkFlowInstance.Start();
                Console.WriteLine(@"Work flow started");

                _objDataEventArgs = new ExternalDataEventArgs(_instanceId) {WaitForIdle = true};
                DumpStateMachine(_objWorkFlowRuntime, _instanceId);
            }
            catch (Exception ex)
            {
                Log.Error("Fatal Error: {0}", ex);
            }
        }

        ~Cobase411_Bi()
        {
            _objWorkFlowInstance.Suspend("Closed");
            _objAstm = null;
            _objWorkFlowRuntime.Dispose();
            _objWorkFlowRuntime = null;
        }

        private void objASTM_SendACKEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <ACK>");
            SendByte((byte) DeviceHelper.ACK);
        }

        private void objASTM_SendNAKEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <NAK>");
            SendByte((byte) DeviceHelper.NAK);
        }

        private void objASTM_SendENQEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <ENQ>");
            SendByte((byte) DeviceHelper.ENQ);
        }

        private void objASTM_SendEOTEvent(object sender, EventArgs e)
        {
            Log.Debug(@"Host: <EOT>");
            SendByte((byte) DeviceHelper.EOT);
            if ((_prvRequestArray != null) && (_prvRequestArray.Count != 0))
            {
                _currentOrder.Clear();
                _currentOrder = _prvRequestArray.Dequeue();
                _currentOrderIdx = 0;
            }
        }

        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessRawData()
        {
            try
            {
                var allbyte = ByteData;
                _tempbuffer = string.Concat(_tempbuffer, Encoding.ASCII.GetString(allbyte));

                foreach (var onebyte in allbyte)
                {
                    if (onebyte.Equals((byte) DeviceHelper.ENQ))
                    {
                        Log.Debug(@"Centaur: <ENQ>");
                        _objAstm.CallGetENQ(_objDataEventArgs);
                        DumpStateMachine(_objWorkFlowRuntime, _instanceId);
                        //SendByte((byte)DeviceHelper.ACK);
                        //RaiseEventSendENQToWF(null, onebyte);
                        _bufferData = _tempbuffer = string.Empty;
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.LF))
                    {
                        Log.Debug("Centaur: {0}", _tempbuffer);
                        var temp = DeviceHelper.GetStringAfterCheckSum(_tempbuffer);
                        _tempbuffer = string.Empty;
                        if (temp != string.Empty)
                        {
                            //SendStringData(DeviceHelper.ACK.ToString());
                            _objAstm.CallGetRightFrame(_objDataEventArgs);
                            _bufferData = string.Concat(_bufferData, temp);
                        }

                        else
                        {
                            //SendByte((byte)DeviceHelper.NAK);
                            _objAstm.CallGetWrongFrame(_objDataEventArgs);
                        }
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.EOT))
                    {
                        Log.Debug(@"Centaur: <EOT>");
                        _objAstm.CallGetEOT(_objDataEventArgs);
                        ProcessData();
                        //Send((int)EventID.GetEOT,_newQuery);
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.ACK))
                    {
                        //Send((int) EventID.GetACK);
                        Log.Debug(@"Centaur: <ACK>");
                        _objAstm.CallGetACK(_objDataEventArgs);
                        if ((_currentOrder != null) && (_currentOrder.Count != 0) &&
                            (_currentOrderIdx < _currentOrder.Count))
                        {
                            _prvLastStringRequest = _currentOrder[_currentOrderIdx];
                            _currentOrderIdx++;
                            SendStringData(_prvLastStringRequest);
                            Log.Debug("Host: {0}", _prvLastStringRequest);
                        }
                        else
                        {
                            //SendByte((byte)DeviceHelper.EOT);
                            Log.Debug(@"Session closing ...");
                            _objAstm.CallCloseSession(_objDataEventArgs);
                            if (_prvRequestArray != null && _prvRequestArray.Count != 0)
                                _objAstm.CallGetQuery(_objDataEventArgs);
                        }
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.NAK))
                    {
                        Log.Debug(@"Centaur: <NAK>");
                        _objAstm.CallGetNAK(_objDataEventArgs);
                        SendStringData(_prvLastStringRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.FatalException("Fatal Error: ", ex);
            }
            finally
            {
                ClearData();
            }
        }

        private void ProcessData()
        {
            try
            {
                var arrRecords = new string[] {};

                if (_bufferData != string.Empty)
                    arrRecords = _bufferData.Split(new[] {_clsRRecord.Rules.EndOfRecordCharacter},
                                                  StringSplitOptions.RemoveEmptyEntries);
                var i = 0;
                while (i < arrRecords.Length)
                {
                    var arrFields = arrRecords[i].Split(_clsPRecord.Rules.FieldDelimiter);

                    if (arrFields[0].Equals(_clsRRecord.RecordType.Data))
                    {
                        _clsRRecord = new CobasE411ResultRecord(arrRecords[i]);
                        AddResult(_clsRRecord.GetResult());
                        _newResult = true;
                    }
                    else if (arrFields[0].StartsWith(_clsPRecord.RecordType.Data))
                        _clsPRecord = new CobasE411PatientInformationRecord(arrRecords[i]);
                    else if (arrFields[0].StartsWith(_clsORecord.RecordType.Data))
                    {
                        _clsORecord = new CobasE411TestOrderRecord(arrRecords[i]);
                        TestResult.Barcode = _clsORecord.SpecimenId.Data.Trim();
                        var tempDate = _clsORecord.SpecimenCollectionDateAndTime.Data;
                        TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                            tempDate.Substring(4, 2), tempDate.Substring(0, 4));
                    }
                    else if (arrFields[0].Equals(_clsQRecord.RecordType.Data))
                    {
                        _clsQRecord = new CobasE411RequestInformationRecord(arrRecords[i]);
                        _sQBarcode = _clsQRecord.GetOrderBarcode();
                        _newQuery = true;
                        var regList = GetRegList(_sQBarcode);
                        _prvRequestArray.Enqueue(CreateOrderFrame(regList));
                    }
                    else if (arrFields[0].Equals(_clsHRecord.RecordType.Data))
                        _clsHRecord = new CobasE411HeaderRecord(arrRecords[i]);

                    i++;
                }

                if (_newQuery)
                {
                    //Send((int)EventID.OpenNewOutputSession, _sQBarcode);
                    _objAstm.CallGetQuery(_objDataEventArgs);
                    if ((_prvRequestArray != null) && (_prvRequestArray.Count != 0))
                    {
                        _currentOrder.Clear();
                        _currentOrder = _prvRequestArray.Dequeue();
                        _currentOrderIdx = 0;
                    }
                    //SendByte((byte)DeviceHelper.ENQ);
                    _newQuery = false;
                }

                if (_newResult)
                {
                    Log.Debug("Begin Import Result");
                    Log.Debug(ImportResults() ? "Import Result Success" : "Error While Import Result");
                    _newResult = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Fatal Error:{0}", ex.ToString());
            }
        }

        private List<string> CreateOrderFrame(List<string> orderList)
        {
            var retList = new List<string>();

            //Tạo Header mới
            var newHeaderRecord = (CobasE411HeaderRecord)_clsHRecord.Clone();
            newHeaderRecord.CommentOrSpecialInstructions.Data = "TSDWN^REPLY";
            newHeaderRecord.SenderNameOrId = _clsHRecord.ReveiverId;
            newHeaderRecord.ReveiverId = _clsHRecord.SenderNameOrId;
            
            var sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "1", newHeaderRecord.Create(),DeviceHelper.ETB);
            var checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(string.Format("{0}{1}{2}",sTemp,checksum,DeviceHelper.CRLF));

            //Tạo P mới
            var newPatientRecord = new CobasE411PatientInformationRecord();
            newPatientRecord.SequenceNumber.Data = "1";
            sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "2", newPatientRecord.Create(), DeviceHelper.ETB);
            checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

            //Xử lý kết quả 
            if ((orderList != null) && (orderList.Count != 0))
            {
                //Add OrderRecord
                _clsORecord = new CobasE411TestOrderRecord();
                _clsORecord.SequenceNumber.Data = "1";
                _clsORecord.SpecimenId.Data = _sQBarcode;
                var tempIsId = _clsQRecord.StartingRangeIdNumber.Data.Split(_clsQRecord.Rules.ComponentDelimiter);
                _clsORecord.InstrumentSpecimenId.Data = string.Join(_clsORecord.Rules.ComponentDelimiter.ToString(),
                                                                    new[]
                                                                        {
                                                                            tempIsId[3], tempIsId[4], tempIsId[5],
                                                                            tempIsId[6], tempIsId[7], tempIsId[8]
                                                                        });
                _clsORecord.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(orderList);
                _clsORecord.Priority.Data = "R";
                _clsORecord.SpecimenCollectionDateAndTime.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                _clsORecord.DateTimeResultReportedOrLastModified.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                _clsORecord.ActionCode.Data = "A";
                _clsORecord.SpecimenDescriptor.Data = "1";
                _clsORecord.ReportTypes.Data = "O";
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETB);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                _clsTRecord = new CobasE411TerminationRecord();
                sTemp = String.Concat(DeviceHelper.STX, "4", _clsTRecord.Create(),DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            }
            else
            {
                //Add OrderRecord
                _clsORecord = new CobasE411TestOrderRecord();
                _clsORecord.SequenceNumber.Data = "1";
                _clsORecord.SpecimenId.Data = _sQBarcode;
                _clsORecord.Priority.Data = "R";
                //_clsORecord.SpecimenCollectionDateAndTime.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                //_clsORecord.DateTimeResultReportedOrLastModified.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                _clsORecord.ActionCode.Data = "A";
                _clsORecord.SpecimenDescriptor.Data = "1";
                _clsORecord.ReportTypes.Data = "O";
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETB);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                _clsTRecord = new CobasE411TerminationRecord {TerminationCode = {Data = "I"}};
                sTemp = String.Concat(DeviceHelper.STX, "4", _clsTRecord.Create(),DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            }

            return retList;
        }

        private static void DumpStateMachine(WorkflowRuntime runtime, Guid instanceID)
        {
            var instance =
                new StateMachineWorkflowInstance(runtime, instanceID);

            Console.WriteLine("Workflow ID: {0}", instanceID);
            Console.WriteLine("Current State: {0}",
                              instance.CurrentStateName);
            Console.WriteLine("Possible Transitions: {0}",
                              instance.PossibleStateTransitions.Count);
            foreach (string name in instance.PossibleStateTransitions)
            {
                Console.WriteLine("\t{0}", name);
            }
        }
    }
}