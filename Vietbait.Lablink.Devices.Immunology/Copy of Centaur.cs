#define USING_CENTAUR_MACHINE

#if !USING_CENTAUR_MACHINE
#define USING_CENTAUR_MACHINE
#else
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Vietbait.Lablink.Devices.Immunology.CentaurDevice;
using Vietbait.Lablink.Devices.StateMachineToolkit;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Immunology
{
    internal class Centaur : AstmGeneral
    {
        private CentaurHeaderRecord _clsHRecord;
        private CentaurOrderRecord _clsORecord;
        private CentaurPatientRecord _clsPRecord;
        private CentaurQueryRecord _clsQRecord;
        private CentaurResultRecord _clsRRecord;
        private CentaurTerminationRecord _clsTRecord;
        private string _prvTestOrder;
        private string _tempbuffer;
        private bool _newResult = false;
        private bool _newQuery = false;
        private string _sQBarcode;


        public Centaur()
        {
            _clsHRecord = new CentaurHeaderRecord();
            _clsPRecord = new CentaurPatientRecord();
            _clsORecord = new CentaurOrderRecord();
            _clsQRecord = new CentaurQueryRecord();
            _clsRRecord = new CentaurResultRecord();
            _clsTRecord = new CentaurTerminationRecord();
            TransitionCompleted += HandleTransitionCompleted;
            
        }

        #region Overrides of AstmGeneral

        /// <summary>
        /// Hàm được gọi khi có sự kiện dữ liệu tồn tại trên cổng COM của thiết bị
        /// </summary>
        /// <param name="obj"></param>
        protected override void OnInCommingData(object obj)
        {
            byte[] allbyte = ReadAll();
            _tempbuffer = string.Concat(_tempbuffer, Encoding.ASCII.GetString(allbyte));
            for (int i = 0; i < allbyte.Length; ++i)
            {
                byte onebyte = allbyte[i];
                if (onebyte.Equals((byte) DeviceHelper.ENQ))
                {
                    Send((int) EventID.GetENQ);
                    _tempbuffer = string.Empty;
                }
                else if (onebyte.Equals((byte) DeviceHelper.LF))
                {
                    string temp = GetStringAfterCheckSum(_tempbuffer);
                    _tempbuffer = string.Empty;
                    if (temp != string.Empty) Send((int) EventID.GetRightFrame, temp);
                    else Send((int) EventID.GetWrongFrame);
                }
                else if (onebyte.Equals((byte) DeviceHelper.EOT))
                {
                    ProcessData();
                    Send((int)EventID.GetEOT,_newQuery);
                    
                }
                else if (onebyte.Equals((byte) DeviceHelper.ACK))
                {
                    Send((int) EventID.GetACK);
                }
                else if (onebyte.Equals((byte)DeviceHelper.NAK))
                {
                    Send((int)EventID.GetNAK);
                }
            }
        }

        #endregion

        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                var arrRecords = new string[]{};
                ArrayList listRecords = new ArrayList();
                ArrayList listFields = new ArrayList();

                if (BufferData != string.Empty)
                {
                    arrRecords = BufferData.Split(new[] {_clsRRecord.RecordDelimiter},
                                                  StringSplitOptions.RemoveEmptyEntries);
                }
                
                int i = 0;
                while (i<arrRecords.Length)
                {
                    
                    string[] arrFields = arrRecords[i].Split(_clsPRecord.FieldDelimiter);

                    if(arrFields[0].Equals("H"))
                    {                        
                        while(!arrFields[0].Equals("L"))
                        {
                            if (++i > arrRecords.Length-1) break;
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

                                AddResult(new TestResult.ResultItem(_clsRRecord.TestId,
                                                                    _clsRRecord.DataValue.PubFieldData));
                                TestResult.TestDate = _clsRRecord.TestDate;
                                _newResult = true;
                            }
                            else if (fields[0].StartsWith(_clsQRecord.RecordType))
                            {
                                _clsQRecord = new CentaurQueryRecord(fields);
                                
                                _sQBarcode = _clsQRecord.QueryBarcode;
                                _newQuery = true;
                                
                            }
                        }
                        
                        //if(_newResult )
                        //{
                        //    ImportResults();
                        //    _newResult = false;
                        //}
                        if (_newQuery)
                        {
                            Send((int)EventID.OpenNewOutputSession, _sQBarcode);
                            _newQuery = false;
                        }
                        
                    }
                    ++i;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private ArrayList CreateOrderFrame()
        {
            string sTemp = string.Empty;

            sTemp = String.Concat("1", _clsHRecord.sHeaderRecord);            
            sTemp = String.Concat(sTemp, _clsPRecord.CreateData());

            sTemp = String.Concat(sTemp, _clsTRecord.TerminationRecord);

            return null;
        }

        /// <summary>
        /// Hàm trả về chuỗi Checksum của một Frame.
        /// Check khi có một trong hai giá trị ETX, ETB
        /// </summary>
        /// <param name="frame">Chuỗi cần kiểm tra</param>
        /// <returns>Chuỗi trả về là 2 ký tự dùng để checksum</returns>
        private string GetCheckSumValue(string frame)
        {
            string checksum = "00";

            int sumOfChars = 0;
            bool complete = false;

            //take each byte in the string and add the values
            for (int idx = 0; idx < frame.Length; idx++)
            {
                int byteVal = Convert.ToInt32(frame[idx]);

                switch (byteVal)
                {
                    case DeviceHelper.STX:
                        sumOfChars = 0;
                        break;
                    case DeviceHelper.ETX:
                    case DeviceHelper.ETB:
                        sumOfChars += byteVal;
                        complete = true;
                        break;
                    default:
                        sumOfChars += byteVal;
                        break;
                }

                if (complete)
                    break;
            }

            if (sumOfChars > 0)
            {
                //hex value mod 256 is checksum, return as hex value in upper case
                checksum = Convert.ToString(sumOfChars%256, 16).ToUpper();
            }

            //if checksum is only 1 char then prepend a 0
            return (checksum.Length == 1 ? "0" + checksum : checksum);
        }

        /// <summary>
        /// Lấy về chuỗi sau khi đã xử lý CheckSum
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private string GetStringAfterCheckSum(string frame)
        {
            string result;
            try
            {
                //Tính giá trị CheckSum của chuỗi truyền vào
                string csValue = GetCheckSumValue(frame);
                Console.WriteLine(csValue);

                //Gán biến CheckSum 
                string csValueFromString = string.Empty;

                //Tìm index của giá trị CRLF
                int lastIndexOfCrlf = frame.LastIndexOf(DeviceHelper.CRLF);
                if (frame.Length > lastIndexOfCrlf - 2)
                {
                    //Lấy giá trị CheckSum từ chuỗi truyền vào
                    csValueFromString = frame.Substring(lastIndexOfCrlf - 2, 2);
                    Console.WriteLine(csValueFromString);
                }

                //So sánh giá trị tính toán và giá trị chứa trong chuỗi
                bool valid = csValue.Equals(csValueFromString);

                //Nếu chuỗi tính toán bằng chuỗi CheckSum được gửi kèm => lấy ra dữ liệu
                if (valid)
                {
                    Console.WriteLine(@"CheckSum OK");
                    int idFirstStx = frame.IndexOf(DeviceHelper.STX);
                    result = frame.Substring(idFirstStx + 2);
                    int idLastCrlf = result.LastIndexOf(DeviceHelper.CRLF);
                    result = result.Substring(0, idLastCrlf - 3);
                }
                    //Nếu có lỗi xảy ra khi truyền dữ liệu trả về chuỗi lỗi mặc định
                else
                {
                    Console.WriteLine(@"CheckSum Error");
                    result = string.Empty;
                }
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }

        private void HandleTransitionCompleted(object sender, TransitionCompletedEventArgs e)
        {
            Console.WriteLine("Transition Completed:");
            Console.WriteLine("\tState ID: {0}", ((StateID) (e.StateID)));
            Console.WriteLine("\tEvent ID: {0}", ((EventID) (e.EventID)));

            if (e.Error != null)
            {
                Console.WriteLine("\tException: {0}", e.Error.Message);
            }
            else
            {
                Console.WriteLine("\tException: No exception was thrown.");
                switch (e.StateID)
                {
                    case (int) StateID.Idle:
                        Console.WriteLine(@"Idle State.");                        
                        break;
                        
                    case (int)StateID.CheckRequest:
                        Console.WriteLine(@"Check Request State.");
                        if (PrvRequestArray.Count != 0)
                        {
                            Send((int)EventID.RemainRequest, PrvRequestArray[0]);
                        }
                        else
                        {
                            Send((int)EventID.EndOfRequest);
                        }
                        break;
                }
            }

            if (e.ActionResult != null)
            {
                Console.WriteLine("\tAction Result: {0}", e.ActionResult);
            }
            else
            {
                Console.WriteLine("\tAction Result: No action result.");
            }
        }


        protected override void ImportResult(object[] args)
        {
            if (_newResult)
            {
                ImportResults();
                _newResult = false;
            }
        }
    }
}

#endif