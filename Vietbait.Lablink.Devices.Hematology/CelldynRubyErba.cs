using System;
using System.Collections.Generic;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class CelldynRubyErba : Rs232Base
    {
        private List<int> resultIndex;
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try
            {
                resultIndex = new List<int>();
                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace("Raw Data: \n{0}",StringData);
                string[] tempStringArr;

                var strResult = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.CRLF);

                //Lấy ngày:
                var rowIndex = GetRowIndex(strResult, "DATE");
                if (rowIndex > -1)
                {
                    tempStringArr = strResult[rowIndex].Split(';');
                    TestResult.TestDate = tempStringArr.Length < 2
                                              ? DateTime.Now.ToString("dd/MM/yyyy")
                                              : (tempStringArr[1].Trim() == ""
                                                     ? DateTime.Now.ToString("dd/MM/yyyy")
                                                     : tempStringArr[1].Trim());
                }
                else
                {
                    TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                }

                //Lấy TestID:
                rowIndex = GetRowIndex(strResult, "ID");
                if (rowIndex>-1)
                {
                    tempStringArr = strResult[rowIndex].Split(';');
                    TestResult.Barcode = tempStringArr.Length < 2
                                              ? "0000"
                                              : (tempStringArr[1].Trim() == ""
                                                     ? "0000"
                                                     : tempStringArr[1].Trim()); 
                }
                else
                {
                    TestResult.Barcode = "0000";
                }

                //Lấy các kết quả.
                //Khai báo mảng kết quả.
                var arrResultName = new[]
                                             {
                                                 "WBC", "RBC", "HGB", "HCT", "MCV", "MCH", "MCHC", "RDW", "PLT", "MPV",
                                                 "PCT", "PDW", "LYM%", "MID%", "GRA%", "LYM", "MID", "GRA"
                                             };
                //Lấy kết quả và lưu vào mảng.
                foreach (var sResultName in arrResultName)
                {
                    var indexResult = GetRowIndex(strResult, sResultName);
                    if (indexResult>-1)
                    {
                        tempStringArr = strResult[indexResult].Split(';');
                        var testValue = tempStringArr.Length < 2 ? "": (tempStringArr[1].Trim() == ""? "": tempStringArr[1].Trim());
                        AddResult(new ResultItem(sResultName, testValue)); 
                    }
                }
                
                Log.Debug("Import Result for barcode:" + TestResult.Barcode);

                Log.Debug(ImportResults() ? "Import Result Success" : "Import Result Error");
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Exception while processing data:{0}", ex));
            }
            finally
            {
                ClearData();
            }
        }

        /// <summary>
        /// Trả về index của dòng với chuỗ bắt đầu xác định
        /// </summary>
        /// <param name="arrString">Mảng cần tìm Index</param>
        /// <param name="beginString">Chuỗi bắt đầu</param>
        /// <returns></returns>
        private int GetRowIndex(IList<string> arrString,string beginString)
        {
            try
            {
                var result = -1;
                for (var i = 0; i < arrString.Count; i++)
                    //if (arrString[i].StartsWith(beginString,StringComparison.Ordinal))
                    if (beginString==arrString[i].Substring(0,beginString.Length))
                    {
                        if(resultIndex.Contains(i))
                        {
                            continue;
                        }
                        else
                        {
                            resultIndex.Add(i);
                            result = i;
                            break;
                        }
                            
                        
                    }
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        #endregion
    }
}