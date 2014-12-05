using System.IO.Ports;

namespace Vietbait.Lablink.Utilities
{
    public class DataReceiveRs232
    {
        #region "Attributies"

        #endregion

        #region "Properties"

        public byte[] Data { get; set; }

        public SerialPort Port { get; set; }

        #endregion

        #region "Contructor"

        public DataReceiveRs232(SerialPort pPort, byte[] pData)
        {
            Port = pPort;
            Data = pData;
        }

        #endregion
    }
}