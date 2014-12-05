using System;
using System.Collections.Generic;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Immunology
{
    public class Access2 : ASTMManager
    {
        private Access2HeaderRecord _clsHRecord;
        private Access2TestOrderRecord _clsORecord;
        private Access2PatientInformationRecord _clsPRecord;
        private Access2RequestInformationRecord _clsQRecord;
        private Access2ResultRecord _clsRRecord;
        private Access2TerminationRecord _clsTRecord;
        private string _sQBarcode;


        public Access2()
        {
            try
            {
                _clsHRecord = new Access2HeaderRecord();
                _clsPRecord = new Access2PatientInformationRecord();
                _clsORecord = new Access2TestOrderRecord();
                _clsQRecord = new Access2RequestInformationRecord();
                _clsRRecord = new Access2ResultRecord();
                _clsTRecord = new Access2TerminationRecord();
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

                if (inputBuffer != string.Empty)
                    arrRecords = inputBuffer.Split(new[] {_clsRRecord.Rules.EndOfRecordCharacter},
                                                   StringSplitOptions.RemoveEmptyEntries);
                int i = 0;
                bool newResult = false;
                while (i < arrRecords.Length)
                {
                    string[] arrFields = arrRecords[i].Split(_clsPRecord.Rules.FieldDelimiter);

                    if (arrFields[0].Equals(_clsRRecord.RecordType.Data))
                    {
                        _clsRRecord = new Access2ResultRecord(arrRecords[i]);
                        AddResult(_clsRRecord.GetResult());
                        newResult = true;
                    }
                    else if (arrFields[0].StartsWith(_clsPRecord.RecordType.Data))
                        _clsPRecord = new Access2PatientInformationRecord(arrRecords[i]);
                    else if (arrFields[0].StartsWith(_clsORecord.RecordType.Data))
                    {
                        _clsORecord = new Access2TestOrderRecord(arrRecords[i]);
                        string barcode = _clsORecord.SpecimenId.Data.Trim();
                        while (barcode.IndexOf(".")>=0)
                        {
                            barcode = barcode.Replace(".", "");
                        }
                        TestResult.Barcode = barcode;

                        string tempDate = _clsHRecord.DateAndTimeOfMessage.Data.Trim();


                        TestResult.TestDate = string.IsNullOrEmpty(tempDate)
                                                  ? DateTime.Now.ToString("dd/MM/yyyy")
                                                  : string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                                  tempDate.Substring(4, 2), tempDate.Substring(0, 4));
                    }
                    else if (arrFields[0].Equals(_clsQRecord.RecordType.Data))
                    {
                        string patientName = "";
                        _clsQRecord = new Access2RequestInformationRecord(arrRecords[i]);
                        _sQBarcode = _clsQRecord.GetOrderBarcode();
                        Log.Debug("Barcode is:{0}",_sQBarcode);

                        List<string> regList = new List<string>();
                        
                        //if (System.Diagnostics.Debugger.IsAttached)
                        //{
                        //    regList.AddRange(new[] { "AFP", "TSH" });
                        //}
                        //else
                        //{
                        regList = GetRegList(_sQBarcode.Replace(".", ""), ref patientName);
                        //}
                        
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
                        orderList = CreateOrderFrame(regList,patientName);
                        return true;
                    }
                    else if (arrFields[0].Equals(_clsHRecord.RecordType.Data))
                        _clsHRecord = new Access2HeaderRecord(arrRecords[i]);

                    i++;
                }

                if (newResult)
                {
                    Log.Debug("Begin Import {0} Result",TestResult.Items.Count);
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

        private List<string> CreateOrderFrame(List<string> orderList, string patientName)
        {
            var retList = new List<string>();

            //Tạo Header mới
            var newHeaderRecord = (Access2HeaderRecord) _clsHRecord.Clone();
            newHeaderRecord.SenderNameOrId.Data = _clsHRecord.ReceiverId.Data;
            newHeaderRecord.ReceiverId.Data = _clsHRecord.SenderNameOrId.Data;
            newHeaderRecord.DateAndTimeOfMessage.Data = DateTime.Now.ToString("yyyyMMddHHmmss");

            var sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "1", newHeaderRecord.Create(), DeviceHelper.ETX);
            var checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

            //string sTemp = newHeaderRecord.Create();
            _clsPRecord.SequenceNumber.Data = "1";

            //Todo: TestOnly
            //_clsPRecord.PatientId.Data = "0003";
            //_clsPRecord.PatientName.Data = "NGUYEN VAN A^^";
            //_clsPRecord.PatientSex.Data = "U";
            _clsPRecord.PracticeAssignedPatientId.Data = _sQBarcode;
            _clsPRecord.DateOfBirth.Data = "20000101";
            _clsPRecord.PatientName.Data = string.Format("{0}^^^^", patientName);
            _clsPRecord.PatientSex.Data = "U";

            sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "2", _clsPRecord.Create(), DeviceHelper.ETX);
            checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

            //Xử lý kết quả )
            if ((orderList != null) && (orderList.Count != 0))
            {
                //Add OrderRecord
                _clsORecord = new Access2TestOrderRecord();
                _clsORecord.SequenceNumber.Data = "1";
                _clsORecord.SpecimenId.Data = _sQBarcode;
                _clsORecord.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(orderList);
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsORecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                _clsTRecord = new Access2TerminationRecord { TerminationCode = { Data = "F" } };
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "4", _clsTRecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

            }
            else
            {
                _clsTRecord = new Access2TerminationRecord { TerminationCode = { Data = "F" } };
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, "3", _clsTRecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            }
            return retList;
        }
    }
}