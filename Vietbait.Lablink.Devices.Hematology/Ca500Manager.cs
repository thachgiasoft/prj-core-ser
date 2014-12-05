using System;
using System.Collections;
using System.Collections.Generic;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Hematology
{
    public class Ca500Manager : ASTMManager
    {
        private Ca500HeaderRecord _clsHRecord;
        private Ca500TestOrderRecord _clsORecord;
        private Ca500PatientRecord _clsPRecord;
        private Ca500RequestInformationRecord _clsQRecord;
        private Ca500ResultRecord _clsRRecord;
        private Ca500TerminationRecord _clsTRecord;
        private string _patientName;
        private string _sQBarcode;


        public Ca500Manager()
        {
            try
            {
                _clsHRecord = new Ca500HeaderRecord();
                _clsPRecord = new Ca500PatientRecord();
                _clsORecord = new Ca500TestOrderRecord();
                _clsQRecord = new Ca500RequestInformationRecord();
                _clsRRecord = new Ca500ResultRecord();
                _clsTRecord = new Ca500TerminationRecord();
            }
            catch (Exception ex)
            {
                Log.Error("Fatal Error: {0}", ex);
            }
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
                int i = 0;
                bool newResult = false;
                bool newQuery = false;
                while (i < arrRecords.Length)
                {
                    string[] arrFields = arrRecords[i].Split(_clsPRecord.Rules.FieldDelimiter);


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
                        if (fields[0].StartsWith(_clsHRecord.RecordType.Data))
                        {
                            _clsHRecord = new Ca500HeaderRecord(fields);
                        }
                        else if (fields[0].StartsWith(_clsPRecord.RecordType.Data))
                        {
                            _clsPRecord = new Ca500PatientRecord(fields);
                        }
                        else if (fields[0].StartsWith(_clsORecord.RecordType.Data))
                        {
                            _clsORecord = new Ca500TestOrderRecord(fields);
                            TestResult.Barcode = _clsORecord.GetBarcode();
                        }
                        else if (fields[0].StartsWith(_clsRRecord.RecordType.Data))
                        {
                            _clsRRecord = new Ca500ResultRecord(fields);
                            var tempDate = _clsRRecord.DateTimeTestCompleted.Data;
                            TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                                tempDate.Substring(4, 2), tempDate.Substring(0, 4));

                            AddResult(_clsRRecord.GetResult());
                            newResult = true;
                        }
                        else if (fields[0].Equals(_clsQRecord.RecordType.Data))
                        {
                            _clsQRecord = new Ca500RequestInformationRecord(fields);
                            _sQBarcode = _clsQRecord.GetOrderBarcode();
                            if (_sQBarcode.Length <= 4)
                                _sQBarcode = string.Format("{0}{1}", DateTime.Now.ToString("yyMMdd"),
                                                           _sQBarcode.PadLeft(4, '0'));
                            newQuery = true;

                            List<string> regList = GetRegList(_sQBarcode, ref _patientName);
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
                            orderList = CreateOrderFrame(regList);
                            //++i;
                        }
                        
                    }
                }
                if (newResult)
                {
                    Log.Debug("Begin Import Result");
                    Log.Debug(ImportResults() ? "Import Result Success" : "Error While Import Result");
                }
                if (newQuery)
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
            var newHeaderRecord = new Ca500OrderHeaderRecord(_clsHRecord.Create());
            newHeaderRecord.SenderNameOrId.Data = "LIS^^^^";
            newHeaderRecord.ReceiverId.Data = _clsHRecord.SenderNameOrId.Data.Split('^')[0].Trim();

            string sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "1", newHeaderRecord.Create(),
                                         DeviceHelper.ETX);
            var checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

            //Xử lý kết quả 
            if ((orderList != null) && (orderList.Count != 0))
            {
                //Add Patient Record
                _clsPRecord = new Ca500PatientRecord();
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "2", _clsPRecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                //Add OrderRecord
                _clsORecord = new Ca500TestOrderRecord();

                _clsORecord.SequenceNo.Data = "1";
                _clsORecord.SpecimenId.Data = _clsQRecord.StartingRangeIdNo.Data;
                _clsORecord.InstrumentSpecimenId.Data = "";
                _clsORecord.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(orderList);
                _clsORecord.Priority.Data = "R";
                _clsORecord.OrderDateAndTime.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                _clsORecord.ActionCode.Data = "N";
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
                _clsTRecord = new Ca500TerminationRecord();
                sTemp = String.Concat(DeviceHelper.STX, "4", _clsTRecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            }
            //else
            //{
            //    _clsQRecord = new CI4100RequestInformationRecord
            //                      {
            //                          StatusCode = new AstmField(12, @"X"),
            //                          SequenceNumber = new AstmField(1, "1"),
            //                          IDNumber = new AstmField(2, string.Format(@"^{0}", _sQBarcode)),
            //                          UniversalTestId = new AstmField(4, "^^^ALL")
            //                      };
            //    sTemp = String.Concat(DeviceHelper.STX, "2", _clsQRecord.Create(), DeviceHelper.ETX);
            //    checksum = DeviceHelper.GetCheckSumValue(sTemp);
            //    retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

            //    _clsTRecord = new CI4100TerminationRecord();
            //    sTemp = String.Concat(DeviceHelper.STX, "3", _clsTRecord.Create(), DeviceHelper.ETX);
            //    checksum = DeviceHelper.GetCheckSumValue(sTemp);
            //    retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            //}

            return retList;
        }
    }
}