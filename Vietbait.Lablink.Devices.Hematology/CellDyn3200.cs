using System;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class CellDyn3200 : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(string.Format("Raw Data:\n{0}", StringData));
                string[] multiResult = DeviceHelper.DeleteAllBlankLine(StringData,
                                                                       new char[] {DeviceHelper.STX, DeviceHelper.ETX});

                foreach (var _result in multiResult)
                {
                    string[] strResult = DeviceHelper.DeleteAllBlankLine(_result, ",");
                    //File.AppendAllText("C:/CELLDYN3200-KQ.txt", strResutl.ToString());
                    //TestResult.TestSequence = strResutl[0];
                    string sTemp = strResult[13];
                    TestResult.TestDate = string.Format(@"{0}/{1}/{2}", sTemp.Substring(4, 2), sTemp.Substring(1, 2),
                                                        sTemp.Substring(7, 4));

                    //Barcode
                    TestResult.Barcode = strResult[6].Substring(1, 12).Trim().Length < 2
                                             ? "0"
                                             : strResult[6].Substring(1, 12).Trim();

                    //Add Items
                    try
                    {
                        AddResult(new ResultItem("WBC", float.Parse(strResult[18]).ToString() ));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("WBC", strResult[18]));
                    }
                    try
                    {
                        AddResult(new ResultItem("NEU", float.Parse(strResult[19]).ToString()));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("NEU", strResult[19]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[20], out test)
                                      ? new ResultItem("LYM", test.ToString())
                                      : new ResultItem("LYM", strResult[20]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("LYM", strResult[20]));
                    }

                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[21], out test)
                                      ? new ResultItem("MONO", test.ToString())
                                      : new ResultItem("MONO", strResult[21]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("MONO", strResult[21]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[22], out test)
                                      ? new ResultItem("EOS", test.ToString())
                                      : new ResultItem("EOS", strResult[22]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("EOS", strResult[22]));
                    }

                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[23], out test)
                                      ? new ResultItem("BASO", test.ToString())
                                      : new ResultItem("BASO", strResult[23]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("BASO", strResult[23]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[24], out test)
                                      ? new ResultItem("RBC", test.ToString())
                                      : new ResultItem("RBC", strResult[24]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("RBC", strResult[24]));
                    }
                   
                    try
                    {

                        double tmp = Convert.ToDouble(strResult[25].Trim()) * 10;
                        AddResult(new ResultItem("HGB", string.Format("{0}", tmp)));
                    }
                    catch (Exception)
                    {
                        AddResult(new ResultItem("HGB", strResult[25]));
                    }
                    try
                    {

                        double tmp = Convert.ToDouble(strResult[26].Trim()) / 100;
                        AddResult(new ResultItem("HCT", string.Format("{0}", tmp)));
                    }
                    catch (Exception)
                    {
                        AddResult(new ResultItem("HCT", strResult[26]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[27], out test)
                                      ? new ResultItem("MCV", test.ToString())
                                      : new ResultItem("MCV", strResult[27]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("MCV", strResult[27]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[28], out test)
                                      ? new ResultItem("MCH", test.ToString())
                                      : new ResultItem("MCH", strResult[28]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("MCH", strResult[28]));
                    }
                    
                    try
                    {

                        double tmp = Convert.ToDouble(strResult[29].Trim()) * 10;
                        AddResult(new ResultItem("MCHC", string.Format("{0}", tmp)));
                    }
                    catch (Exception)
                    {
                        AddResult(new ResultItem("MCHC", strResult[29]));

                    }

                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[30], out test)
                                      ? new ResultItem("RDW", test.ToString())
                                      : new ResultItem("RDW", strResult[30]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("RDW", strResult[30]));
                    }

                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[31], out test)
                                      ? new ResultItem("PLT", test.ToString())
                                      : new ResultItem("PLT", strResult[31]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("PLT", strResult[31]));
                    }

                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[32], out test)
                                      ? new ResultItem("MPV", test.ToString())
                                      : new ResultItem("MPV", strResult[32]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("MPV", strResult[32]));
                    }

                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[35], out test)
                                      ? new ResultItem("%N", test.ToString())
                                      : new ResultItem("%N", strResult[35]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("%N", strResult[35]));
                    }

                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[36], out test)
                                      ? new ResultItem("%L", test.ToString())
                                      : new ResultItem("%L", strResult[36]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("%L", strResult[36]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[37], out test)
                                      ? new ResultItem("%M", test.ToString())
                                      : new ResultItem("%M", strResult[37]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("%M", strResult[37]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[38], out test)
                                      ? new ResultItem("%E", test.ToString())
                                      : new ResultItem("%E", strResult[38]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("%E", strResult[38]));
                    }
                    try
                    {
                        float test;
                        AddResult(float.TryParse(strResult[39], out test)
                                      ? new ResultItem("%B", test.ToString())
                                      : new ResultItem("%B", strResult[39]));
                    }
                    catch (Exception)
                    {

                        AddResult(new ResultItem("%B", strResult[39]));
                    }
                    
                    Log.Debug("Begin Import Result");
                    Log.Debug(ImportResults() ? "Finish Import Result" : "Finish Import Error");
                }
                
                
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"C:\CELLDYN3200-ERROR.txt", ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        #endregion
    }
}