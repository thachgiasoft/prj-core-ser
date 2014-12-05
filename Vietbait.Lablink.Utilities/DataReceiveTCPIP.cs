namespace Vietbait.Lablink.Utilities
{
    public class DataReceive
    {
        public DataReceive(string pSourceIP, int pSourcePort, string pDestinationIP, int pDestinationPort, byte[] pData)
        {
            Header = new DataHeader(pSourceIP, pSourcePort, pDestinationIP, pDestinationPort);
            Data = pData;
        }

        public DataHeader Header { get; set; }
        public byte[] Data { get; set; }
    }
}