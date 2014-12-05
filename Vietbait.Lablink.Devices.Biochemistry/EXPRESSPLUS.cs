using System;
using System.Collections;
using System.Text;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    internal class EXPRESSPLUS : Rs232Devcie
    {
        public override void ProcessRawData()
        {
            try
            {
                Hashtable allPatient = SeparatorData(StringData);
                foreach (string key in allPatient.Keys)
                {
                    TestResult.Barcode = CleanString(key).Trim();
                    TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                    string[] strAllResult = DeviceHelper.DeleteAllBlankLine(allPatient[key].ToString(),
                                                                            DeviceHelper.CRLF.ToCharArray());
                    foreach (string result in strAllResult)
                    {
                        string temresult = result.Split('=')[1];
                        AddResult(new ResultItem(temresult.Split(DeviceHelper.GS)[0].Trim(),
                                                 temresult.Split(DeviceHelper.GS)[1].Trim(),
                                                 temresult.Split(DeviceHelper.GS)[2].Trim()));
                    }
                    ImportResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ClearData();
            }
        }

        private Hashtable SeparatorData(string stringData)
        {
            try
            {
                string[] allLine = DeviceHelper.DeleteAllBlankLine(StringData, DeviceHelper.STX);
                var ht = new Hashtable();
                foreach (string line in allLine)
                {
                    string pid = GetPIDFromLine(line);
                    try
                    {
                        ht[pid] += line + DeviceHelper.CRLF;
                    }
                    catch (Exception)
                    {
                        ht.Add(pid, line);
                    }
                }
                return ht;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetPIDFromLine(string stringData)
        {
            try
            {
                int indexofPid = stringData.IndexOf("iPNAME");
                int indexofPName = stringData.IndexOf("AGE");
                return stringData.Substring(indexofPid + 6, indexofPName - indexofPid - 6);
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        private string CleanString(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var sb = new StringBuilder(s.Length);
                foreach (char c in s)
                {
                    sb.Append(Char.IsControl(c) ? ' ' : c);
                }
                s = sb.ToString();
            }
            return s;
        }
    }
}