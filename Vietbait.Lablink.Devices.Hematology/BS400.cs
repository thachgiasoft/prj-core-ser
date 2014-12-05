namespace Vietbait.Lablink.Devices.Hematology
{
    public class BS400 : TcpIpDevice
    {
        public override void ProcessRawData()
        {
            //SendStringData("OK");
            SendBytesData(ByteData);
            if (StringData.EndsWith("$"))
            {
                ClearData();
            }
        }
    }
}