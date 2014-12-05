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
    public class CI4100Manager : ASTMManager 
    {
        private CI4100HeaderRecord _clsHRecord;
        private CI4100TestOrderRecord _clsORecord;
        private CI4100PatientInformationRecord _clsPRecord;
        private CI4100RequestInformationRecord _clsQRecord;
        private CI4100ResultRecord _clsRRecord;
        private CI4100TerminationRecord _clsTRecord;
        private string _sQBarcode;
        private string _patientName;


        public CI4100Manager()
        {
            try
            {
                _clsHRecord = new CI4100HeaderRecord();
                _clsPRecord = new CI4100PatientInformationRecord();
                _clsORecord = new CI4100TestOrderRecord();
                _clsQRecord = new CI4100RequestInformationRecord();
                _clsRRecord = new CI4100ResultRecord();
                _clsTRecord = new CI4100TerminationRecord();

                
            }
            catch (Exception ex)
            {
                Log.Error("Fatal Error: {0}", ex);
            }
        }

        ~CI4100Manager()
        {
            
        }

        
       

        public override bool ProcessData(string inputBuffer, ref List<string> orderList)
        {
            
            try
            {
                var arrRecords = new string[] {};
                var listRecords = new ArrayList();
                

                if (inputBuffer != string.Empty)
                    arrRecords = inputBuffer.Split(new[] {_clsRRecord.Rules.EndOfRecordCharacter},
                                                  StringSplitOptions.RemoveEmptyEntries);
                var i = 0;
                bool newResult = false;
                bool newQuery = false;
                while (i < arrRecords.Length)
                {
                    var arrFields = arrRecords[i].Split(_clsPRecord.Rules.FieldDelimiter);


                    if (arrFields[0].Equals(_clsHRecord.RecordType.Data))
                    {
                        var listFields = new ArrayList();
                        //listFields.Clear();
                        listFields.Add(arrFields);
                        listRecords.Add(listFields);

                        do
                        {
                            if (++i > arrRecords.Length - 1) break;
                            arrFields = arrRecords[i].Split(_clsHRecord.Rules.FieldDelimiter);
                            //if (arrFields[0].Equals(_clsORecord.RecordType.Data) ||
                            //    arrFields[0].Equals(_clsRRecord.RecordType.Data))
                                                                
                            listFields.Add(arrFields);
                        } while (!arrFields[0].Equals(_clsTRecord.RecordType.Data));
                    }

                    //else
                    ++i;

                }
                        foreach (ArrayList l in listRecords)
                        {
                            foreach (string[] fields in l)
                            {
                                if (fields[0].StartsWith(_clsPRecord.RecordType.Data))
                                {
                                    _clsPRecord = new CI4100PatientInformationRecord(fields);
                                }
                                else if (fields[0].StartsWith(_clsORecord.RecordType.Data))
                                {
                                    _clsORecord = new CI4100TestOrderRecord(fields);
                                    TestResult.Barcode = _clsORecord.SpecimenId.Data.Trim().Replace(".","");
                                }
                                else if (fields[0].StartsWith(_clsRRecord.RecordType.Data))
                                {
                                    _clsRRecord = new CI4100ResultRecord(fields);
                                    var tempDate = _clsRRecord.DateTimeTestStarted.Data;
                                    TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                                        tempDate.Substring(4, 2), tempDate.Substring(0, 4));

                                    AddResult(_clsRRecord.GetResult());
                                    newResult = true;
                                }
                                else if (fields[0].Equals(_clsQRecord.RecordType.Data))
                                {
                                    _clsQRecord = new CI4100RequestInformationRecord(fields);
                                    _sQBarcode = _clsQRecord.GetOrderBarcode();
                                    newQuery = true;

                                    var regList = GetRegList(_sQBarcode,ref _patientName);
                                    Log.Debug("Patient_Name: O" ,_patientName);
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
                                            Log.Debug("No order 1!");
                                        }
                                    }
                                    else
                                    {
                                        Log.Debug("No order 2!");
                                    }
                                    orderList = CreateOrderFrame(regList);
                                    //++i;
                                }
                                if (newResult)
                                {
                                    Log.Debug("Begin Import Result");
                                    Log.Debug(ImportResults() ? "Import Result Success" : "Error While Import Result");
                                    newResult = false;
                                    break;
                                }
                            }
                        }
                    
                if(newQuery)
                    return true;

                return false;
                
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
            var newHeaderRecord = new CI4100HeaderRecord();            
            
            var sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "1", newHeaderRecord.Create(),DeviceHelper.ETX);
            var checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(string.Format("{0}{1}{2}",sTemp,checksum,DeviceHelper.CRLF));

            //Xử lý kết quả 
            if ((orderList != null) && (orderList.Count != 0))
            {
                //Add Patient Record
                _clsPRecord.SequenceNumber.Data = "1";
                if (_patientName.Length > 20)
                {
                    _patientName = _patientName.Substring(_patientName.Length - 20);
                    Log.Debug("Patient_Name: {0},{1}",_patientName.Length, _patientName);
                }
               
                _clsPRecord.PatientName.Data = string.Format("{0}^^", _patientName.Trim());
                //_clsPRecord.PatientSex.Data = "U";
                //_clsPRecord.LaboratoryAssignedPatientId.Data = _sQBarcode;
                Log.Debug("Patient_Name_P: ", _clsPRecord.PatientName.Data);
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "2", _clsPRecord.Create(), DeviceHelper.ETX);

                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                if(orderList.Count>20)
                {
                    List<string> firstOrderList=orderList.GetRange(0,20);
                    List<string> restOrderList = orderList.GetRange(20, orderList.Count-20);
                    //Add OrderRecord
                    _clsORecord = new CI4100TestOrderRecord();
                    
                    _clsORecord.SpecimenId.Data = _sQBarcode;


                    

                    //_clsORecord.SpecimenCollectionDateAndTime.Data = DateTime.Now.ToString("yyyyMMddHHmmss");

                    _clsORecord.ActionCode.Data = "A";

                    _clsORecord.ReportTypes.Data = "Q";

                    _clsORecord.SequenceNumber.Data = "1";

                    _clsORecord.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(firstOrderList);

                    sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETX);
                    checksum = DeviceHelper.GetCheckSumValue(sTemp);
                    retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                    _clsORecord.SequenceNumber.Data = "2";

                    _clsORecord.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(restOrderList);

                    sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "4", _clsORecord.Create(), DeviceHelper.ETX);
                    checksum = DeviceHelper.GetCheckSumValue(sTemp);
                    retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                    _clsTRecord = new CI4100TerminationRecord();
                    sTemp = String.Concat(DeviceHelper.STX, "5", _clsTRecord.Create(), DeviceHelper.ETX);
                    checksum = DeviceHelper.GetCheckSumValue(sTemp);
                    retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
                }
                else
                {
                    //Add OrderRecord
                    _clsORecord = new CI4100TestOrderRecord();
                    _clsORecord.SequenceNumber.Data = "1";
                    _clsORecord.SpecimenId.Data = _sQBarcode;


                    _clsORecord.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(orderList);

                    //_clsORecord.SpecimenCollectionDateAndTime.Data = DateTime.Now.ToString("yyyyMMddHHmmss");

                    _clsORecord.ActionCode.Data = "A";

                    _clsORecord.ReportTypes.Data = "Q";

                    sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETX);
                    checksum = DeviceHelper.GetCheckSumValue(sTemp);
                    retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                    _clsTRecord = new CI4100TerminationRecord();
                    sTemp = String.Concat(DeviceHelper.STX, "4", _clsTRecord.Create(), DeviceHelper.ETX);
                    checksum = DeviceHelper.GetCheckSumValue(sTemp);
                    retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
                }
                
            }
            else
            {
                _clsQRecord = new CI4100RequestInformationRecord
                                  {
                                      StatusCode = new AstmField(12, @"X"),
                                      SequenceNumber = new AstmField(1, "1"),
                                      IDNumber = new AstmField(2, string.Format(@"^{0}", _sQBarcode)),
                                      UniversalTestId = new AstmField(4, "^^^ALL")
                                  };
                sTemp = String.Concat(DeviceHelper.STX, "2", _clsQRecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                _clsTRecord = new CI4100TerminationRecord ();
                sTemp = String.Concat(DeviceHelper.STX, "3", _clsTRecord.Create(),DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
                Log.Debug("No order! Else");
            }

            return retList;
        }

        
    }
}