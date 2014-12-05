using System;
using System.Text;

namespace Vietbait.Lablink.Utilities
{
    public class DataHeader
    {
        #region Properties

        public string ClientIP { get; set; }
        public string ServerIP { get; set; }
        public int ClientPort { get; set; }
        public int ServerPort { get; set; }

        #endregion

        #region Contructors

        public DataHeader(string pClientIP, string pClientPort, string pServerIP, string pServerPort)
        {
            try
            {
                ClientIP = pClientIP;
                ClientPort = Convert.ToInt32(pClientPort);
                ServerIP = pServerIP;
                ServerPort = Convert.ToInt32(pServerPort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataHeader(string pClientIP, int pClientPort, string pServerIP, int pServerPort)
        {
            ClientIP = pClientIP;
            ClientPort = pClientPort;
            ServerIP = pServerIP;
            ServerPort = pServerPort;
        }

        #endregion

        #region Override Method

        public override string ToString()
        {
            var mystring = new StringBuilder();
            mystring.AppendFormat("{0}:{1}-{2}:{3}", ClientIP, ClientPort, ServerIP, ServerPort);
            return mystring.ToString();
        }

        #endregion
    }
}