using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class BS300 : TcpIpDevice
    {
        #region Constant

        private const string MessageHeaderStr = "MSH";
        private const string MessageOBR = "OBR";
        private const string MessageOBX = "OBX";
        private const char FieldSeparator = '|';
        private static readonly string SegmentSeparator = DeviceHelper.CRLF;

        #endregion

        #region Overrides of TcpIpDevice

        public override void ProcessRawData()
        {
            try
            {
                Log.Debug("Begin Process Data");
                Log.Debug("RawData: {0}{1}{0}", DeviceHelper.CRLF, StringData);
                var rawData = StringData;
                while (rawData.IndexOf(DeviceHelper.FS) >= 0)
                {
                    var resultItem = rawData.Substring(0, rawData.IndexOf(DeviceHelper.FS) + 1);
                    ProcessResult(resultItem);
                    rawData = rawData.Replace(resultItem, string.Empty);
                }
                ClearData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessResult(string resultItem)
        {
            try
            {
                string[] strResult = resultItem.Split(SegmentSeparator.ToCharArray());
                strResult = DeviceHelper.DeleteAllBlankLine(strResult);
                var ackTempString = string.Empty;
                foreach (var tempString in strResult)
                {
                    var tempArr = tempString.Split(FieldSeparator);
                    if (tempString.StartsWith(MessageHeaderStr))
                    {
                        ackTempString = tempString;
                        var tempTestDate = tempArr[6];
                        TestResult.TestDate = String.Format("{0}/{1}/{2}", tempTestDate.Substring(6, 2),
                                                            tempTestDate.Substring(4, 2),
                                                            tempTestDate.Substring(0, 4));
                    }
                    else if (tempString.StartsWith(MessageOBR))
                    {
                        TestResult.Barcode = tempArr[2];
                    }
                    else if (tempString.StartsWith(MessageOBX))
                    {
                        var item = new ResultItem(tempArr[4].Trim(), tempArr[13].Trim(), tempArr[6]);
                        AddResult(item);
                    }
                }

                Log.Debug("Begin Import Result");
                Log.Debug(ImportResults() ? "Import Result Success" : "Error while import result"); 

                //Send Confirm cho Devcies
                ackTempString = ackTempString.Replace("ORU^R01", "ACK^R01");
                var sendData = string.Format("{0}{1}{2}MSA|AA|1|Message accepted|||0|{3}{4}{5}", DeviceHelper.VT,
                                                ackTempString, DeviceHelper.CR, DeviceHelper.CR, DeviceHelper.FS,
                                                DeviceHelper.CR);

                SendStringData(sendData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}