using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Hematology
{
    public class AclTop500Manager : ASTMManager
    {
        private AclTopHeaderRecord _clsHRecord;
        private AclTopTestOrderRecord _clsORecord;
        private AclTopPatientInformationRecord _clsPRecord;
        private AclTopRequestInformationRecord _clsQRecord;
        private AclTopResultRecord _clsRRecord;
        private AclTopTerminationRecord _clsTRecord;
        private List<string> _sQBarcode;


        public AclTop500Manager()
        {
            try
            {
                _clsHRecord = new AclTopHeaderRecord();
                _clsPRecord = new AclTopPatientInformationRecord();
                _clsORecord = new AclTopTestOrderRecord();
                _clsQRecord = new AclTopRequestInformationRecord();
                _clsRRecord = new AclTopResultRecord();
                _clsTRecord = new AclTopTerminationRecord();
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
                        _clsRRecord = new AclTopResultRecord(arrRecords[i]);
                        AddResult(_clsRRecord.GetResult());
                        newResult = true;
                    }
                    else if (arrFields[0].StartsWith(_clsPRecord.RecordType.Data))
                        _clsPRecord = new AclTopPatientInformationRecord(arrRecords[i]);
                    else if (arrFields[0].StartsWith(_clsORecord.RecordType.Data))
                    {
                        _clsORecord = new AclTopTestOrderRecord(arrRecords[i]);
                        string barcode = _clsORecord.SpecimenId.Data.Trim();
                        while (barcode.IndexOf(".")>=0)
                        {
                            barcode = barcode.Replace(".", "");
                        }
                        TestResult.Barcode = barcode;

                        string tempDate = _clsORecord.RequestedOrderedDateandTime.Data.Trim();


                        TestResult.TestDate = string.IsNullOrEmpty(tempDate)
                                                  ? DateTime.Now.ToString("dd/MM/yyyy")
                                                  : string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                                  tempDate.Substring(4, 2), tempDate.Substring(0, 4));
                    }
                    else if (arrFields[0].Equals(_clsQRecord.RecordType.Data))
                    {
                        string patientName = "";
                        _clsQRecord = new AclTopRequestInformationRecord(arrRecords[i]);
                        _sQBarcode = _clsQRecord.GetOrderBarcode();
                        orderList = CreateOrderFrame(_sQBarcode, patientName);
                        return true;
                    }
                    else if (arrFields[0].Equals(_clsHRecord.RecordType.Data))
                        _clsHRecord = new AclTopHeaderRecord(arrRecords[i]);

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

        private List<string> CreateOrderFrame(List<string> barcodeList, string patientName)
        {
            var retList = new List<string>();

            //Tạo Header mới
            var newHeaderRecord = (AclTopHeaderRecord)_clsHRecord.Clone();
            newHeaderRecord.SenderNameOrId.Data = _clsHRecord.ReceiverId.Data;
            newHeaderRecord.ReceiverId.Data = _clsHRecord.SenderNameOrId.Data;
            newHeaderRecord.DateAndTimeOfMessage.Data = DateTime.Now.ToString("yyyyMMddHHmmss");

            //result = newHeaderRecord.Create();

            int count = 0;

            var sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, ++count, newHeaderRecord.Create(), DeviceHelper.ETB);
            var checksum = DeviceHelper.GetCheckSumValue(sTemp);
            retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            
            //Xử lý kết quả )
            if ((barcodeList != null) && (barcodeList.Count != 0))
            {
                int patientCount = 0;
                // Duyệt danh sách barcode
                for (int i = 0; i < barcodeList.Count; i++)
                {
                    // Tạo chuỗi Order cho bệnh nhân
                    var queryBarcode = barcodeList[i];
                    Log.Debug("Receive Barcode is:{0}", queryBarcode);

                    if (queryBarcode.Length <= 4)
                        queryBarcode = string.Format("{0}{1}", DateTime.Now.ToString("yyMMdd"), queryBarcode);

                    List<string> regList = new List<string>();
                    var tempRegList = GetRegList(queryBarcode.Replace(".", ""), ref patientName);
                    if(tempRegList.Count>0)
                    {
                        // Tạo bệnh nhân mới và add vào danh sách
                        var newP = new AclTopPatientInformationRecord();
                        newP.SequenceNumber.Data = (++patientCount).ToString();
                        if (count == 7) count = -1;
                        sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, ++count, newP.Create(), DeviceHelper.ETB);
                        checksum = DeviceHelper.GetCheckSumValue(sTemp);
                        retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));

                        regList = (from s in tempRegList
                               select s.Split('-')[0]).Distinct().ToList();
                        var newO = new AclTopTestOrderRecord();
                        newO.SpecimenId.Data = barcodeList[i];
                        newO.UniversalTestId.Data = _clsORecord.CreateUniversalTestid(regList);
                        newO.RequestedOrderedDateandTime.Data = DateTime.Now.ToString("yyyyMMddHHmmss");
                        if (count == 7) count = -1;
                        sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, ++count, newO.Create(), DeviceHelper.ETB);
                        checksum = DeviceHelper.GetCheckSumValue(sTemp);
                        retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
                    }
                    else
                    {
                        Log.Debug("No order for Barcode:{0}",queryBarcode);
                    }
                }

                _clsTRecord = new AclTopTerminationRecord { TerminationCode = { Data = "F" } };
                //result = string.Format("{0}{1}", result, _clsTRecord.Create());
                if (count == 7) count = -1;
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, ++count, _clsTRecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            }
            else
            {
                _clsTRecord = new AclTopTerminationRecord { TerminationCode = { Data = "I" } };
                //result = string.Format("{0}{1}", result, _clsTRecord.Create());
                if (count == 7) count = -1;
                sTemp = string.Format("{0}{1}{2}{3}", DeviceHelper.STX, ++count, _clsTRecord.Create(), DeviceHelper.ETX);
                checksum = DeviceHelper.GetCheckSumValue(sTemp);
                retList.Add(string.Format("{0}{1}{2}", sTemp, checksum, DeviceHelper.CRLF));
            }
            return retList;
        }
    }
}