using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Immunology
{
    public class VersaCell : Rs232Base //,IRS232Communication
    {
        private readonly Guid InstanceId;
        private readonly Queue<List<string>> PrvRequestArray;
        private readonly VersaCellHeaderRecord _clsHRecord;
        private readonly ExternalDataEventArgs objDataEventArgs;
        private readonly ExternalDataExchangeService objService;
        private readonly WorkflowInstance objWorkFlowInstance;
        private string BufferData;
        private VersaCellOrderRecord _clsORecord;
        private VersaCellPatientRecord _clsPRecord;
        private VersaCellQueryRecord _clsQRecord;
        private VersaCellResultRecord _clsRRecord;
        private VersaCellTerminationRecord _clsTRecord;
        private bool _newQuery;
        private bool _newResult;
        private string _prvTestOrder;
        private string _sQBarcode;
        private string _tempbuffer;

        private List<string> currentOrder = new List<string>();
        private byte currentOrderIdx;
        private ClsAstm objASTM;
        private WorkflowRuntime objWorkFlowRuntime = new WorkflowRuntime();
        private string prvLastStringRequest;

        public VersaCell()
        {
            try
            {
                _clsHRecord = new VersaCellHeaderRecord();
                _clsPRecord = new VersaCellPatientRecord();
                _clsORecord = new VersaCellOrderRecord();
                _clsQRecord = new VersaCellQueryRecord();
                _clsRRecord = new VersaCellResultRecord();
                _clsTRecord = new VersaCellTerminationRecord();
                PrvRequestArray = new Queue<List<string>>();
                objService = new ExternalDataExchangeService();

                InstanceId = Guid.NewGuid();
                objWorkFlowRuntime.AddService(objService);
                objASTM = new ClsAstm();
                objService.AddService(objASTM);
                objASTM.SendACKEvent += objASTM_SendACKEvent;
                objASTM.SendNAKEvent += objASTM_SendNAKEvent;
                objASTM.SendENQEvent += objASTM_SendENQEvent;
                objASTM.SendEOTEvent += objASTM_SendEOTEvent;
                objWorkFlowInstance = objWorkFlowRuntime.CreateWorkflow(typeof (ASTMWorkflow), null, InstanceId);
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

        ~VersaCell()
        {
            objWorkFlowInstance.Suspend("Closed");
            objASTM = null;
            objWorkFlowRuntime.Dispose();
            objWorkFlowRuntime = null;
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
            if ((PrvRequestArray != null) && (PrvRequestArray.Count != 0))
            {
                currentOrder.Clear();
                currentOrder = PrvRequestArray.Dequeue();
                currentOrderIdx = 0;
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

                foreach (byte onebyte in allbyte)
                {
                    if (onebyte.Equals((byte) DeviceHelper.ENQ))
                    {
                        Log.Debug(@"Centaur: <ENQ>");
                        objASTM.CallGetENQ(objDataEventArgs);
                        DumpStateMachine(objWorkFlowRuntime, InstanceId);
                        //SendByte((byte)DeviceHelper.ACK);
                        //RaiseEventSendENQToWF(null, onebyte);
                        BufferData = _tempbuffer = string.Empty;
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.LF))
                    {
                        Log.Debug("Centaur: {0}", _tempbuffer);
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
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.EOT))
                    {
                        Log.Debug(@"Centaur: <EOT>");
                        objASTM.CallGetEOT(objDataEventArgs);
                        ProcessData();
                        //Send((int)EventID.GetEOT,_newQuery);
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.ACK))
                    {
                        //Send((int) EventID.GetACK);
                        Log.Debug(@"Centaur: <ACK>");
                        objASTM.CallGetACK(objDataEventArgs);
                        if ((currentOrder != null) && (currentOrder.Count != 0) &&
                            (currentOrderIdx < currentOrder.Count))
                        {
                            prvLastStringRequest = currentOrder[currentOrderIdx];
                            currentOrderIdx++;
                            SendStringData(prvLastStringRequest);
                            Log.Debug("Host: {0}", prvLastStringRequest);
                        }
                        else
                        {
                            //SendByte((byte)DeviceHelper.EOT);
                            Log.Debug(@"Session closing ...");
                            objASTM.CallCloseSession(objDataEventArgs);
                            if (PrvRequestArray != null && PrvRequestArray.Count != 0)
                                objASTM.CallGetQuery(objDataEventArgs);
                        }
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.NAK))
                    {
                        Log.Debug(@"Centaur: <NAK>");
                        objASTM.CallGetNAK(objDataEventArgs);
                        SendStringData(prvLastStringRequest);
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
                var listRecords = new ArrayList();
                var listFields = new ArrayList();

                if (BufferData != string.Empty)
                    arrRecords = BufferData.Split(new[] {_clsRRecord.RecordDelimiter},
                                                  StringSplitOptions.RemoveEmptyEntries);

                int i = 0;
                while (i < arrRecords.Length)
                {
                    string[] arrFields = arrRecords[i].Split(_clsPRecord.FieldDelimiter);

                    if (arrFields[0].Equals(_clsPRecord.RecordType))
                    {
                        listFields.Clear();
                        listFields.Add(arrFields);
                        listRecords.Add(listFields);

                        do
                        {
                            if (++i > arrRecords.Length - 1) break;
                            arrFields = arrRecords[i].Split(_clsPRecord.FieldDelimiter);
                            if (arrFields[0].Equals(_clsORecord.RecordType) ||
                                arrFields[0].Equals(_clsRRecord.RecordType))
                                listFields.Add(arrFields);
                        } while (!arrFields[0].Equals(_clsPRecord.RecordType));

                        foreach (ArrayList l in listRecords)
                        {
                            foreach (string[] fields in l)
                            {
                                if (fields[0].StartsWith(_clsPRecord.RecordType))
                                {
                                    _clsPRecord = new VersaCellPatientRecord(fields);
                                }
                                else if (fields[0].StartsWith(_clsORecord.RecordType))
                                {
                                    _clsORecord = new VersaCellOrderRecord(fields);
                                    TestResult.Barcode = _clsORecord.Barcode;
                                }
                                else if (fields[0].StartsWith(_clsRRecord.RecordType))
                                {
                                    _clsRRecord = new VersaCellResultRecord(fields);

                                    AddResult(new ResultItem(_clsRRecord.TestId,
                                                             _clsRRecord.DataValue.PubFieldData));
                                    TestResult.TestDate = _clsRRecord.TestDate;
                                    _newResult = true;
                                }
                            }
                        }
                        if (_newResult)
                        {
                            ImportResults();
                            _newResult = false;
                        }
                    }
                    else if (arrFields[0].Equals(_clsQRecord.RecordType))
                    {
                        _clsQRecord = new VersaCellQueryRecord(arrFields);
                        _sQBarcode = _clsQRecord.QueryBarcode;
                        _newQuery = true;
                        List<string> regList = GetRegList(_clsQRecord.QueryBarcode);
                        PrvRequestArray.Enqueue(CreateOrderFrame(regList));
                        ++i;
                    }
                    else
                        ++i;
                }

                if (_newQuery)
                {
                    //Send((int)EventID.OpenNewOutputSession, _sQBarcode);
                    objASTM.CallGetQuery(objDataEventArgs);
                    if ((PrvRequestArray != null) && (PrvRequestArray.Count != 0))
                    {
                        currentOrder.Clear();
                        currentOrder = PrvRequestArray.Dequeue();
                        currentOrderIdx = 0;
                    }
                    //SendByte((byte)DeviceHelper.ENQ);
                    _newQuery = false;
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
            string sTemp = String.Concat(DeviceHelper.STX, "1", _clsHRecord.sHeaderRecord, DeviceHelper.ETX);
            string checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(String.Concat(sTemp, checksum, DeviceHelper.CRLF));

            if ((orderList != null) && (orderList.Count != 0))
            {
                _clsPRecord = new VersaCellPatientRecord();
                sTemp = String.Concat(DeviceHelper.STX, "2", _clsPRecord.CreateData(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(String.Concat(sTemp, checksum, DeviceHelper.CRLF));

                _clsORecord = new VersaCellOrderRecord();
                sTemp = String.Concat(DeviceHelper.STX, "3", _clsORecord.CreateData(_sQBarcode, orderList),
                                      DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(String.Concat(sTemp, checksum, DeviceHelper.CRLF));

                _clsTRecord = new VersaCellTerminationRecord(1, @"F");
                sTemp = String.Concat(DeviceHelper.STX, "4", _clsTRecord.TerminationRecord);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(String.Concat(sTemp, checksum, DeviceHelper.CRLF));
            }
            else
            {
                _clsTRecord = new VersaCellTerminationRecord(1, @"I");
                sTemp = String.Concat(DeviceHelper.STX, "2", _clsTRecord.TerminationRecord);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(String.Concat(sTemp, checksum, DeviceHelper.CRLF));
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