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
    public class Cobase6000BiManager : ASTMManager 
    {
        
        private CobasE6000HeaderRecord _clsHRecord;
        private CobasE6000TestOrderRecord _clsORecord;
        private CobasE6000PatientInformationRecord _clsPRecord;
        private CobasE6000RequestInformationRecord _clsQRecord;
        private CobasE6000ResultRecord _clsRRecord;
        private CobasE6000TerminationRecord _clsTRecord;
        private string _sQBarcode;


        public Cobase6000BiManager()
        {
            try
            {
                if (DateTime.Now > new DateTime(2013, 06, 06)) throw new Exception("XXX");
                _clsHRecord = new CobasE6000HeaderRecord();
                _clsPRecord = new CobasE6000PatientInformationRecord();
                _clsORecord = new CobasE6000TestOrderRecord();
                _clsQRecord = new CobasE6000RequestInformationRecord();
                _clsRRecord = new CobasE6000ResultRecord();
                _clsTRecord = new CobasE6000TerminationRecord();

                
            }
            catch (Exception ex)
            {
                Log.Error("Fatal Error: {0}", ex);
            }
        }
        
        ~Cobase6000BiManager()
        {
            
        }

        

        public override bool ProcessData(string inputBuffer, ref List<string> orderList)
        {
            try
            {
                var arrRecords = new string[] {};

                if (inputBuffer != string.Empty)
                    arrRecords = inputBuffer.Split(new[] {_clsRRecord.Rules.EndOfRecordCharacter},
                                                  StringSplitOptions.RemoveEmptyEntries);
                var i = 0;
                bool _newResult = false;
                while (i < arrRecords.Length)
                {
                    var arrFields = arrRecords[i].Split(_clsPRecord.Rules.FieldDelimiter);

                    if (arrFields[0].Equals(_clsRRecord.RecordType.Data))
                    {
                        _clsRRecord = new CobasE6000ResultRecord(arrRecords[i]);
                        AddResult(_clsRRecord.GetResult());
                        _newResult = true;
                    }
                    if (arrFields[0].StartsWith(_clsPRecord.RecordType.Data))
                        _clsPRecord = new CobasE6000PatientInformationRecord(arrRecords[i]);
                    else if (arrFields[0].StartsWith(_clsORecord.RecordType.Data))
                    {
                        _clsORecord = new CobasE6000TestOrderRecord(arrRecords[i]);
                        TestResult.Barcode = _clsORecord.SpecimenId.Data.Trim();
                        var tempDate = _clsORecord.DateTimeResultReportedOrLastModified.Data;
                        TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                            tempDate.Substring(4, 2), tempDate.Substring(0, 4));
                    }
                    else if (arrFields[0].Equals(_clsQRecord.RecordType.Data))
                    {
                        _clsQRecord = new CobasE6000RequestInformationRecord(arrRecords[i]);
                        _sQBarcode = _clsQRecord.GetOrderBarcode();

                        var regList = GetRegList(_sQBarcode);
                        if (regList != null)
                        {
                            if (regList.Count > 0)
                            {
                                Log.Debug(string.Format("So order: {0}", regList.Count.ToString()));
                                foreach (string s in regList)
                                {
                                    Log.Debug(string.Format("{0}\r\n", s));
                                }
                            }
                            else
                            {
                                Log.Debug("No order!");
                            }
                        }
                        else
                        {
                            Log.Debug("No order!");
                        }
                        orderList=CreateOrderFrame(regList);
                        return true;
                    }
                    else if (arrFields[0].Equals(_clsHRecord.RecordType.Data))
                        _clsHRecord = new CobasE6000HeaderRecord(arrRecords[i]);

                    i++;
                }

                

                if (_newResult)
                {
                    Log.Debug("Begin Import Result");
                    Log.Debug(ImportResults() ? "Import Result Success" : "Error While Import Result");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Fatal Error:{0}", ex.ToString());
            }
            return false;
        }

        private List<string> CreateOrderFrame(List<string> orderList)
        {
            var retList = new List<string>();

            //Tạo Header mới
            var newHeaderRecord = (CobasE6000HeaderRecord)_clsHRecord.Clone();
            newHeaderRecord.CommentOrSpecialInstructions.Data = "TSDWN^REPLY";
            newHeaderRecord.SenderNameOrId.Data = "";// _clsHRecord.ReveiverId.Data;
            newHeaderRecord.ReveiverId.Data = "";// _clsHRecord.SenderNameOrId.Data;
            
            //var sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "1", newHeaderRecord.Create(),DeviceHelper.ETB);
            //var checksum = DeviceHelper.GetCheckSumValue(sTemp);
            //retList.Add(string.Format("{0}{1}{2}",sTemp,checksum,DeviceHelper.CRLF));
                        
            ////Add Patient Record
            //_clsPRecord.SequenceNumber.Data = "1";
            //_clsPRecord.PatientSex.Data = "U";
            ////_clsPRecord.LaboratoryAssignedPatientId.Data = _sQBarcode;

            //sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "2", _clsPRecord.Create(), DeviceHelper.ETB);

            //checksum = DeviceHelper.GetCheckSumValue(sTemp);
            //retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

            var sTemp = newHeaderRecord.Create();
            _clsPRecord.SequenceNumber.Data = "1";
            sTemp = string.Concat(sTemp, _clsPRecord.Create());
            //_clsPRecord.PatientSex.Data = "U";
            //_clsPRecord.LaboratoryAssignedPatientId.Data = _sQBarcode;
            //Xử lý kết quả )
            if ((orderList != null) && (orderList.Count != 0))
            {
                

                //Add OrderRecord
                _clsORecord = new CobasE6000TestOrderRecord();
                _clsORecord.SequenceNumber.Data = "1";
                _clsORecord.SpecimenId.Data = _sQBarcode.PadRight(22);
                var tempIsId = _clsQRecord.StartingRangeIdNumber.Data.Split(_clsQRecord.Rules.ComponentDelimiter);
                _clsORecord.InstrumentSpecimenId.Data = string.Join(_clsORecord.Rules.ComponentDelimiter.ToString(),
                                                                    new[]
                                                                        {
                                                                            //tempIsId[3], tempIsId[4], tempIsId[5],tempIsId[6]
                                                                            string.Empty,string.Empty,string.Empty,string.Empty,
                                                                            
                                                                             tempIsId[7]
                                                                            ,string.Empty
                                                                            //, tempIsId[8]
                                                                        });
                _clsORecord.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(orderList);
                _clsORecord.Priority.Data = "R";
                //_clsORecord.SpecimenCollectionDateAndTime.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                //_clsORecord.DateTimeResultReportedOrLastModified.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                _clsORecord.ActionCode.Data = "A";
                _clsORecord.SpecimenDescriptor.Data = "1";
                _clsORecord.ReportTypes.Data = "O";

                //sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETB);
                sTemp = string.Concat(sTemp, _clsORecord.Create());
                
                //checksum = DeviceHelper.GetCheckSumValue(sTemp);
                //retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                _clsTRecord = new CobasE6000TerminationRecord();
                //sTemp = String.Concat(DeviceHelper.STX, "4", _clsTRecord.Create(),DeviceHelper.ETX);
                //checksum = DeviceHelper.GetCheckSumValue(sTemp);
                //retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                sTemp = string.Concat(sTemp, _clsTRecord.Create());
                
            }
            else
            {
                _clsORecord = new CobasE6000TestOrderRecord();
                _clsORecord.SequenceNumber.Data = "1";
                _clsORecord.SpecimenId.Data = _sQBarcode.PadRight(22);
                var tempIsId = _clsQRecord.StartingRangeIdNumber.Data.Split(_clsQRecord.Rules.ComponentDelimiter);
                _clsORecord.InstrumentSpecimenId.Data = string.Join(_clsORecord.Rules.ComponentDelimiter.ToString(),
                                                                    new[]
                                                                        {
                                                                            //tempIsId[3], tempIsId[4], tempIsId[5],tempIsId[6]
                                                                            string.Empty,string.Empty,string.Empty,string.Empty,
                                                                            
                                                                             tempIsId[7]
                                                                            ,string.Empty
                                                                            //, tempIsId[8]
                                                                        });
                _clsORecord.UniversalTestId.Data = string.Empty;
                _clsORecord.Priority.Data = "R";
                _clsORecord.ActionCode.Data = "A";
                _clsORecord.SpecimenDescriptor.Data = "1";
                _clsORecord.ReportTypes.Data = "O";
                sTemp = string.Concat(sTemp, _clsORecord.Create());
                //sTemp = String.Concat(DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETB);
                //checksum = DeviceHelper.GetCheckSumValue(sTemp);
                //retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
                _clsTRecord = new CobasE6000TerminationRecord {TerminationCode = {Data = "I"}};
                sTemp = string.Concat(sTemp, _clsTRecord.Create());
                //sTemp = String.Concat(DeviceHelper.STX, "4", _clsTRecord.Create(),DeviceHelper.ETX);
                //checksum = DeviceHelper.GetCheckSumValue(sTemp);
                //retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            }
            if (sTemp.Length > 240)
            {

                var sTempFirst = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "1", sTemp.Substring(0, 240), DeviceHelper.ETB);
                var checksum = DeviceHelper.GetCheckSumValue(sTempFirst);
                retList.Add(string.Format("{0}{1}{2}", sTempFirst, checksum, DeviceHelper.CRLF));
                var sTempSecond = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "2", sTemp.Substring(240), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTempSecond);
                retList.Add(string.Format("{0}{1}{2}", sTempSecond, checksum, DeviceHelper.CRLF));
            }
            else
            {
                var sTempFirst = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "1", sTemp, DeviceHelper.ETX);
                var checksum = DeviceHelper.GetCheckSumValue(sTempFirst);
                retList.Add(string.Format("{0}{1}{2}", sTempFirst, checksum, DeviceHelper.CRLF));
            }

            return retList;
        }

        
    }
}