using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;
//using Vietbait.Lablink.Devices.Immunology.CentaurDevice;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    public class ADVIA1650 : Rs232Base //,IRS232Communication
    {
        private ADVIA1650OrderRecord _clsORecord;
        private ADVIA1650QueryRecord _clsQRecord;
        private ADVIA1650ResultRecord _clsRRecord;
        private string _tempbuffer;
        private bool _newQuery;

        private string BufferData;
        private readonly List<string> PrvRequestArray;
        private string prvLastStringRequest;

        private readonly ExternalDataEventArgs objDataEventArgs;
        private readonly WorkflowInstance objWorkFlowInstance;
        private readonly ClsAstm objASTM;
        private readonly WorkflowRuntime objWorkFlowRuntime = new WorkflowRuntime();
        private readonly Guid InstanceId;

        public ADVIA1650()
        {
            try
            {
                _clsORecord = new ADVIA1650OrderRecord();
                _clsQRecord = new ADVIA1650QueryRecord();
                _clsRRecord = new ADVIA1650ResultRecord();
                PrvRequestArray = new List<string>();
                var objService = new ExternalDataExchangeService();

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
                Console.WriteLine("Work flow started");

                objDataEventArgs = new ExternalDataEventArgs(InstanceId);
                objDataEventArgs.WaitForIdle = true;
                //DumpStateMachine(objWorkFlowRuntime, InstanceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void objASTM_SendACKEvent(object sender, EventArgs e)
        {
            SendByte((byte) DeviceHelper.ACK);
            Log.Trace("ACK");
        }

        private void objASTM_SendNAKEvent(object sender, EventArgs e)
        {
            SendByte((byte) DeviceHelper.NAK);
            Log.Trace("NAK");
        }

        private void objASTM_SendENQEvent(object sender, EventArgs e)
        {
            SendByte((byte) DeviceHelper.ENQ);
            Log.Trace("ENQ");
        }

        private void objASTM_SendEOTEvent(object sender, EventArgs e)
        {
            SendByte((byte) DeviceHelper.EOT);
            Log.Trace("EOT");
        }

        #region Overrides of Rs232Base

        public override void ProcessRawData()
        {
            try
            {
                byte[] allbyte = ByteData;
                Log.Trace(allbyte);
                _tempbuffer = string.Concat(_tempbuffer, Encoding.ASCII.GetString(allbyte));

                for (int i = 0; i < allbyte.Length; ++i)
                {
                    byte onebyte = allbyte[i];
                    if (onebyte.Equals((byte) DeviceHelper.ENQ))
                    {
                        objASTM.CallGetENQ(objDataEventArgs);
                        //DumpStateMachine(objWorkFlowRuntime, InstanceId);
                        //SendByte((byte)DeviceHelper.ACK);
                        //RaiseEventSendENQToWF(null, onebyte);
                        BufferData = _tempbuffer = string.Empty;
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.LF))
                    {
                        string temp = DeviceHelper.GetStringAfterCheckSum(_tempbuffer);
                        Log.Trace(temp);
                        _tempbuffer = string.Empty;
                        if (temp != string.Empty)
                        {
                            //SendStringData(DeviceHelper.ACK.ToString());
                            objASTM.CallGetRightFrame(objDataEventArgs);
                            BufferData = BufferData.StartsWith(@"R") ? string.Format("{0}{1}{2}", BufferData, DeviceHelper.CR, temp) : string.Concat(BufferData, temp);
                        }

                        else
                        {
                            //SendByte((byte)DeviceHelper.NAK);
                            objASTM.CallGetWrongFrame(objDataEventArgs);
                        }
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.EOT))
                    {
                        objASTM.CallGetEOT(objDataEventArgs);
                        ProcessData();
                        //Send((int)EventID.GetEOT,_newQuery);
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.ACK))
                    {
                        //Send((int) EventID.GetACK);
                        objASTM.CallGetACK(objDataEventArgs);
                        if ((PrvRequestArray != null) && (PrvRequestArray.Count != 0))
                        {
                            prvLastStringRequest = PrvRequestArray[0];
                            PrvRequestArray.RemoveAt(0);
                            SendStringData(prvLastStringRequest);
                        }
                        else
                        {
                            //SendByte((byte)DeviceHelper.EOT);
                            objASTM.CallCloseSession(objDataEventArgs);
                        }
                    }
                    else if (onebyte.Equals((byte) DeviceHelper.NAK))
                    {
                        //Send((int)EventID.GetNAK);
                        objASTM.CallGetNAK(objDataEventArgs);
                        SendStringData(prvLastStringRequest);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error While Process Raw Data: {0}",ex.ToString()));
            }
            finally
            {
                ClearData();
            }
        }

        #endregion

        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        private void ProcessData()
        {
            try
            {
                var arrRecords = new string[] {};

                if (BufferData != string.Empty)
                {
                    arrRecords = BufferData.Split(new[] {_clsRRecord.RecordDelimiter},
                                                  StringSplitOptions.RemoveEmptyEntries);
                }

                int i = 0;
                while (i < arrRecords.Length)
                {
                    if (arrRecords[i].StartsWith("R"))
                    {
                        Log.Trace(arrRecords[i]);
                        _clsRRecord = new ADVIA1650ResultRecord(arrRecords[i]);
                        TestResult.TestDate = string.Concat(_clsRRecord.DateTestCompleted.Substring(6, 2), "/",
                                                            _clsRRecord.DateTestCompleted.Substring(4, 2), "/",
                                                            _clsRRecord.DateTestCompleted.Substring(0, 4));
                        TestResult.Barcode = _clsRRecord.Barcode;
                        foreach (DictionaryEntry entry in _clsRRecord.htResult)
                        {
                            AddResult(new ResultItem(entry.Key.ToString(), entry.Value.ToString()));
                        }
                        Log.Debug(ImportResults() ? "Import Result Success" : "Import Result Error");
                    }
                    if (arrRecords[i].StartsWith("q") || arrRecords[i].StartsWith("Q"))
                    {
                        Log.Trace(arrRecords[i]);
                        _clsQRecord = new ADVIA1650QueryRecord(arrRecords[i]);
                        _clsORecord = new ADVIA1650OrderRecord();
                        Log.Debug("Request for barcode: {0}", _clsQRecord.QueryBarcode);

                        for (int index = 0; index < _clsQRecord.QueryBarcode.Count; index++)
                        {
                            string barcode = _clsQRecord.QueryBarcode[index];
                            List<string> reglist = GetRegList(barcode);
                            Log.Debug("Reglist for barcode: {0}", string.Join(",", reglist.ToArray()));
                            _newQuery = true;
                            if ((reglist == null) || (reglist.Count == 0)) continue;
                            string data = _clsORecord.CreateData(index, barcode, reglist);
                            PrvRequestArray.Add(data);
                            Log.Trace(data);
                        }
                    }
                    ++i;
                }

                if (_newQuery)
                {
                    objASTM.CallGetQuery(objDataEventArgs);

                    _newQuery = false;
                }
            }
            catch (Exception ex)
            {
                Log.Error(String.Format("Error While Process Data: {0}", ex.ToString()));
            }
        }
    }
}
