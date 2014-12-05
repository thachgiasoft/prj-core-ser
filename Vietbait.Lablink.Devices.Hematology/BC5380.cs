using System;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class BC5380 : TcpIpDevice
    {
        #region Overrides of TcpIpDevice

        public override void ProcessRawData()
        {
            try
            {
                Log.Debug("Begin Process Data");
                Log.Debug("RawData: {0}{1}{0}", DeviceHelper.CRLF, StringData);
                var strResult = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CR);
                foreach (var result in strResult)
                {
                    //Bắt đầu bằng OBR: => xử lý ngày tháng năm 
                    if (result.StartsWith("OBR"))
                    {
                        var tempString = result.Split('|');
                        if (tempString.Length > 7)
                        {
                            var tempTestDate = tempString[7];
                            TestResult.TestDate = string.Format("{0}/{1}/{2}", tempTestDate.Substring(6, 2),tempTestDate.Substring(4, 2), tempTestDate.Substring(0, 4));
                            TestResult.Barcode = tempString[3].Trim();
                        }
                    }
                    else if (result.StartsWith("OBX"))
                    {
                        var tempString = result.Split('|');
                        var resultIndex = Convert.ToInt32(tempString[1]);
                        if ((resultIndex >= 5) && (resultIndex != 16) && (resultIndex != 17) && (resultIndex != 18) &&
                            (resultIndex != 19) && (resultIndex <= 31))
                        {
                            var testName = tempString[3].Split('^')[1];
                            var testValue = tempString[5].Trim();
                            var testUnit = tempString[6].Trim();
                            var testNormalLevel = tempString[7].Trim();
                            AddResult(new ResultItem(testName, testValue, testUnit, testNormalLevel, testNormalLevel));
                            Log.Debug("Add Result Success: Testname = {0}, TestValue={1}, TestUnit={2}, TestNormalLevel={3}",testName, testValue, testUnit, testNormalLevel);
                        }
                    }
                }

                Log.Debug("Begin Import Result");
                ImportResults();
                Log.Debug("Import Result Success");
                
            }
            catch (Exception ex)
            {
                Log.Error("Error While process Data {0} {1}",DeviceHelper.CRLF,ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        #endregion
    }
}