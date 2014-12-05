using System;
using System.Collections.Generic;
using System.Globalization;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class Au400_Bi : Rs232Base
    {
        public override void ProcessRawData()
        {
            //Kiểm tra xem kết thúc nhận dữ liệu chưa?
            //Nếu kết thúc nhận dữ liệu kiểm tra tiếp định dạng dữ liệu
        }

        /// <summary>
        /// Tách dữ liệu
        /// </summary>
        /// <param name="stringData"></param>
        /// <returns></returns>
        private IEnumerable<string> SeparatorData(string stringData)
        {
            try
            {
                //loại bỏ các ký tự "DB", "DE"
                string[] arrStringData = stringData.Split(DeviceHelper.STX, DeviceHelper.ETX);
                arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData, false);
                return arrStringData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ValidData()
        {
            if (StringData.EndsWith(DeviceHelper.ETX.ToString(CultureInfo.InvariantCulture)))
            {
                SendByte((byte) DeviceHelper.ACK);
                return true;
            }
            return false;
        }
    }
}