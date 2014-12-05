using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vietbait.Lablink.Devices.Immunology.CentaurDevice;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Immunology
{
    public class CentaurManager : ASTMManager
    {
        private readonly CentaurHeaderRecord _clsHRecord;
        private CentaurOrderRecord _clsORecord;
        private CentaurPatientRecord _clsPRecord;
        private CentaurQueryRecord _clsQRecord;
        private CentaurResultRecord _clsRRecord;
        private CentaurTerminationRecord _clsTRecord;
        private bool _newResult = false;
        public CentaurManager()
        {
            
            try
            {
                _clsHRecord = new CentaurHeaderRecord();
                _clsPRecord = new CentaurPatientRecord();
                _clsORecord = new CentaurOrderRecord();
                _clsQRecord = new CentaurQueryRecord();
                _clsRRecord = new CentaurResultRecord();
                _clsTRecord = new CentaurTerminationRecord();
             
                
            }
            catch (Exception ex)
            {

                Log.Error(string.Format("Error: {0}", ex));   
            }
        }
        public override bool ProcessData(string inputBuffer, ref List<string> orderList)
        
        {
            try
            {
                var arrRecords = new string[] { };
                var listRecords = new ArrayList();
                var listFields = new ArrayList();

                if (inputBuffer != string.Empty)
                {
                    arrRecords = inputBuffer.Split(new[] { _clsRRecord.RecordDelimiter },
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

                                if ((_clsRRecord.TestId.ToUpper() == @"HBSII") || (_clsRRecord.TestId.ToUpper() == @"AHCV")|| (_clsRRecord.TestId.ToUpper() == @"EHIV"))
                                {
                                    if (_clsRRecord.ResultAspects.Trim() == @"INDX")
                                    {
                                        AddResult(new ResultItem(_clsRRecord.TestId,
                                                             _clsRRecord.DataValue.PubFieldData));
                                        _newResult = true;
                                        TestResult.TestDate = _clsRRecord.TestDate;                                        
                                        continue;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                
                                {
                                    if (_clsRRecord.ResultAspects.Trim() == @"DOSE")
                                    {
                                        AddResult(new ResultItem(_clsRRecord.TestId,
                                                         _clsRRecord.DataValue.PubFieldData));
                                        _newResult = true;
                                        TestResult.TestDate = _clsRRecord.TestDate;
                                        
                                        continue;
                                    }
                                    
                                }

                            }
                            else if (fields[0].StartsWith(_clsQRecord.RecordType))
                            {
                                _clsQRecord = new CentaurQueryRecord(fields);
                                
                                List<string> regList = GetRegList(_clsQRecord.QueryBarcode);
                                orderList .Add(CreateOrderFrame(regList));
                                return true;
                                
                            }
                        }
                        Log.Debug("Begin Import Result");
                        Log.Debug(ImportResults() ? "Import Result Success" : "Error While Import Result");
                    }
                    
                    ++i;
                }

                
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error: {0}", ex));   
            }
            return false;
        }

        private string CreateOrderFrame(List<string> orderList)
        {

        
            _clsPRecord = new CentaurPatientRecord();
            _clsORecord = new CentaurOrderRecord();
            _clsTRecord = new CentaurTerminationRecord();
            string sTemp = String.Concat("1", _clsHRecord.sHeaderRecord);
            if ((orderList != null) && (orderList.Count > 0))
            {
                sTemp = String.Concat(sTemp, _clsPRecord.CreateData());
                sTemp = String.Concat(sTemp, _clsORecord.CreateData(_clsQRecord.QueryBarcode, orderList));
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
            Log.Debug("Order frame: <", orderFrame);
            return orderFrame;
        }

               
    }
}
