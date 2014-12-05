using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Text;
using Vietbait.Lablink.Utilities;
using Vietbait.Server.Rs232Server;

namespace Vietbait.DemoProject
{
    internal class Rs232ServerImpl : Rs232Server
    {
        private readonly Dictionary<string, object> _colDevices;
        private readonly Dictionary<string, string> _colDevicesName;
        private readonly string _fileLog;
        private string _errorString = "-vietbait-hientd-error";
        private int _state;


        public Rs232ServerImpl(Dictionary<string, object> colDevices, Dictionary<string, string> colDevicesName,
                               string logFileName)
        {
            _state = 0;
            _colDevices = colDevices;
            _colDevicesName = colDevicesName;
            _fileLog = logFileName;
            File.WriteAllText(logFileName, "");
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
            Console.WriteLine(@"CheckSum Value from String:{0}", frame.Length);
            try
            {
                //Tính giá trị CheckSum của chuỗi truyền vào
                string csValue = GetCheckSumValue(frame);
                Console.WriteLine(@"CheckSum Value: {0}", csValue);

                //Gán biến CheckSum 
                string csValueFromString = string.Empty;

                //Tìm index của giá trị CRLF
                int lastIndexOfCrlf = frame.LastIndexOf(DeviceHelper.CRLF);
                if (frame.Length > lastIndexOfCrlf - 2)
                {
                    //Lấy giá trị CheckSum từ chuỗi truyền vào
                    csValueFromString = frame.Substring(lastIndexOfCrlf - 2, 2);
                }
                Console.WriteLine(@"CheckSum Value from String:{0}", csValueFromString);

                //So sánh giá trị tính toán và giá trị chứa trong chuỗi
                bool valid = csValue.Equals(csValueFromString);

                //Nếu chuỗi tính toán bằng chuỗi CheckSum được gửi kèm => lấy ra dữ liệu
                if (valid)
                {
                    int idFirstStx = frame.IndexOf(DeviceHelper.STX);
                    result = frame.Substring(idFirstStx + 2);
                    int idLastCrlf = result.LastIndexOf(DeviceHelper.CRLF);
                    result = result.Substring(0, idLastCrlf - 3);
                }
                    //Nếu có lỗi xảy ra khi truyền dữ liệu trả về chuỗi lỗi mặc định
                else result = _errorString;
            }
            catch (Exception)
            {
                result = _errorString;
            }
            return result;
        }

        public override void OnServerStartSuccess(string portName)
        {
            //throw new NotImplementedException();
        }

        public override void OnServerStartFalse(string portName)
        {
            //throw new NotImplementedException();
        }

        public override void OnIncommingData(DataReceiveRs232 obj)
        {
            //Console.WriteLine(obj.Port.BytesToRead.ToString());
            try
            {
                //Nếu trạng thái =0 và có ký tự ENQ kiểm tra ký tự ENQ
                byte[] bytesdata = obj.Data;
                string stringdata = Encoding.ASCII.GetString(bytesdata);
                //System.Threading.Thread.Sleep(5);
                //if (_state.Equals(0) && stringdata.EndsWith(DeviceHelper.ENQ.ToString()))
                if (stringdata.EndsWith(DeviceHelper.ENQ.ToString()))
                {
                    //Gửi lại tín hiệu ACK tới thiết bị
                    obj.Port.Write(DeviceHelper.ACK.ToString());

                    //Gán trạng thái = 1
                    _state = 1;
                    Console.WriteLine("Accept ENQ");
                } //Kết thúc kiểm tra ENQ

                    //Nếu trạng thái truyền dữ liệu đã được thiết lập
                else if (_state.Equals(1))
                {
                    //Nếu chuỗi ký tự kết thúc bằng CRLF
                    if (stringdata.EndsWith(DeviceHelper.CRLF))
                    {
                        //Kiểm tra CheckSum và lấy ra chuỗi dữ liệu
                        string dataAfterCheck = GetStringAfterCheckSum(stringdata);

                        //Nếu chuỗi trả bị lỗi gửi thông báo gửi lại
                        //Ngược lại bổ sung thêm dữ liệu đã xử lý vào bộ đệm và gửi tín hiệu thông báo thành công
                        if (dataAfterCheck.Equals(_errorString))
                        {
                            obj.Port.Write(DeviceHelper.NAK.ToString());
                        }
                        else
                        {
                            File.AppendAllText(_fileLog, dataAfterCheck);
                            obj.Port.Write(DeviceHelper.ACK.ToString());
                        }
                    }
                        //Nếu nhận chuỗi EOT thì phát sinh sự kiện kết thúc nhận dữ liệu
                    else if (stringdata.Equals(DeviceHelper.EOT.ToString()))
                    {
                        obj.Port.Write(DeviceHelper.ACK.ToString());
                        _state = 0;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public override void OnIncommingData(DataReceiveRs232 obj)
        //{

        //    try
        //    {
        //        byte[] bytesdata = obj.Data;
        //        string stringdata = Encoding.ASCII.GetString(bytesdata);
        //        if (stringdata.Equals(DeviceHelper.EOT.ToString()))
        //        {
        //            obj.Port.Write(DeviceHelper.ACK.ToString());

        //            Console.WriteLine("OK");
        //        }
        //        else
        //        {
        //            int idFirstStx = stringdata.IndexOf(DeviceHelper.STX);
        //            string dataAfterCheck;

        //            dataAfterCheck = stringdata.Substring(idFirstStx + 2);
        //            int idLastCrlf = dataAfterCheck.LastIndexOf(DeviceHelper.CRLF);
        //            dataAfterCheck = dataAfterCheck.Substring(0, idLastCrlf - 3);
        //            File.AppendAllText(_fileLog, dataAfterCheck);
        //            obj.Port.Write(DeviceHelper.ACK.ToString());
        //        }

        //    }
        //    catch (Exception)
        //    {

        //    }
        //}


        public override void OnEndReceiveData(SerialPort port)
        {
            //throw new NotImplementedException();
        }
    }
}