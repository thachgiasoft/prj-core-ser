using System;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class CellDyn1700 : Rs232Devcie
    {
        #region Overrides of Rs232Devcie

        public override void ProcessRawData()
        {
            try

            {
                Log.Trace("Begin Process Data");

                //Lưu lại Data
                Log.Trace(StringData);
            
                string pday, pmonth, pyear;
                File.AppendAllText(@"C:\CELLDYN1700-KETQUA.txt", StringData);
                string[] strResutl = DeviceHelper.DeleteAllBlankLine(StringData, ",");
                pmonth = strResutl[6].Substring(1, 2);
                pday = strResutl[6].Substring(4, 2);
                pyear = strResutl[6].Substring(7, 2);

                //string ptestdate = strResutl[6].Substring(1, 8);
                //TestResult.TestDate = ptestdate.Insert(ptestdate.Length - 2, "20");

                TestResult.TestDate = string.Format("{0}/{1}/20{2}", pday, pmonth, pyear);
                //nếu nhập patient ID
                string pbarcode = strResutl[8].Substring(5, 5).Trim();
                //nếu nhập Patient Name

                TestResult.Barcode = pbarcode.Length < 1 ? "0" : pbarcode;

                Log.Trace(string.Format("Barcode: {0}", pbarcode));
                string WBC = strResutl[16].Trim();
                try
                {
                    if (WBC == "-----")
                    {
                        WBC = "0000";
                    }
                    else
                    {

                        WBC = strResutl[16].Trim();
                    }
                }
                catch (Exception)
                {

                    WBC = strResutl[16].Trim();
                }

                AddResult(new ResultItem("WBC",
                                        float.Parse(WBC.Insert(WBC.Length - 1, ".")).ToString(),
                                        "G/L"));
                string LYM = strResutl[18].Trim();
                try
                {
                    if (LYM == "-----")
                    {
                        LYM = "0000";
                    }
                    else
                    {

                        LYM = strResutl[18].Trim();
                    }
                }
                catch (Exception)
                {

                    LYM = strResutl[18].Trim();
                }
                AddResult(new ResultItem("LYM",
                                         float.Parse(LYM.Insert(LYM.Length - 1, ".")).ToString()));
                string MID = strResutl[19].Trim();
                try
                {
                    if (MID == "-----")
                    {
                        MID = "0000";
                    }
                    else
                    {

                        MID = strResutl[19].Trim();
                    }
                }
                catch (Exception)
                {

                    MID = strResutl[19].Trim();
                }
                AddResult(new ResultItem("MID",
                                         float.Parse(MID.Insert(MID.Length - 1, ".")).ToString()));
                string GRAN = strResutl[20].Trim();
                try
                {
                    if (GRAN == "-----")
                    {
                        GRAN = "0000";
                    }
                    else
                    {

                        GRAN = strResutl[20].Trim();
                    }
                }
                catch (Exception)
                {

                    GRAN = strResutl[20].Trim();
                }
                AddResult(new ResultItem("GRAN",
                                         float.Parse(GRAN.Insert(GRAN.Length - 1, ".")).ToString()));
                string RBC = strResutl[22].Trim();
                try
                {
                    if (RBC == "-----")
                    {
                        RBC = "0000";
                    }
                    else
                    {

                        RBC = strResutl[22].Trim();
                    }
                }
                catch (Exception)
                {

                    RBC = strResutl[22].Trim();
                }
                AddResult(new ResultItem("RBC",
                                         float.Parse(RBC.Insert(RBC.Length - 2, ".")).ToString(),
                                         "T/L"));
                string HGB = strResutl[23].Trim();
                try
                {
                    if (HGB == "-----")
                    {
                        HGB = "0000";
                    }
                    else
                    {

                        HGB = strResutl[23].Trim();
                    }
                }
                catch (Exception)
                {

                    HGB = strResutl[23].Trim();
                }
                AddResult(new ResultItem("HGB", float.Parse(HGB).ToString(), "g/L"));
                string HCT = strResutl[24].Trim();
                try
                {
                    if (HCT == "-----")
                    {
                        HCT = "0000";
                    }
                    else
                    {

                        HCT = strResutl[24].Trim();
                    }
                }
                catch (Exception)
                {

                    HCT = strResutl[24].Trim();
                }
                AddResult(new ResultItem("HCT", float.Parse(HCT.Insert(HCT.Length - 3, ".")).ToString()));
                string MCV = strResutl[25].Trim();
                try
                {
                    if (MCV == "-----")
                    {
                        MCV = "0000";
                    }
                    else
                    {

                        MCV = strResutl[25].Trim();
                    }
                }
                catch (Exception)
                {

                    MCV = strResutl[25].Trim();
                }
                AddResult(new ResultItem("MCV",
                                         float.Parse(MCV.Insert(MCV.Length - 1, ".")).ToString(),
                                         "fL"));
                string MCH = strResutl[26].Trim();
                try
                {
                    if (MCH == "-----")
                    {
                        MCH = "0000";
                    }
                    else
                    {

                        MCH = strResutl[26].Trim();
                    }
                }
                catch (Exception)
                {

                    MCH = strResutl[26].Trim();
                }
                AddResult(new ResultItem("MCH",
                                         float.Parse(MCH.Insert(MCH.Length - 1, ".")).ToString(),
                                         "fg"));
                string MCHC = strResutl[27].Trim();
                try
                {
                    if (MCHC == "-----")
                    {
                        MCHC = "0000";
                    }
                    else
                    {

                        MCHC = strResutl[27].Trim();
                    }
                }
                catch (Exception)
                {

                    MCHC = strResutl[27].Trim();
                }
                AddResult(new ResultItem("MCHC", float.Parse(MCHC.Insert(MCHC.Length, "")).ToString(), "g/L"));
                string RDW = strResutl[28].Trim();
                try
                {
                    if (RDW == "-----")
                    {
                        RDW = "0000";
                    }
                    else
                    {

                        RDW = strResutl[28].Trim();
                    }
                }
                catch (Exception)
                {

                    RDW = strResutl[28].Trim();
                }
                AddResult(new ResultItem("RDW",
                                         float.Parse(RDW.Insert(RDW.Length - 1, ".")).ToString(),
                                         "%"));
                string PLT = strResutl[29].Trim();
                try
                {
                    if (PLT == "-----")
                    {
                        PLT = "0000";
                    }
                    else
                    {

                        PLT = strResutl[29].Trim();
                    }
                }
                catch (Exception)
                {

                    PLT = strResutl[29].Trim();
                }
                AddResult(new ResultItem("PLT", float.Parse(PLT.Insert(PLT.Length, ".")).ToString(), "G/L"));
                string MPV = strResutl[30].Trim();
                try
                {
                    if (MPV == "-----")
                    {
                        MPV = "0000";
                    }
                    else
                    {

                        MPV = strResutl[30].Trim();
                           }
                }
                catch (Exception)
                {

                    MPV = strResutl[30].Trim();
                }

                AddResult(new ResultItem("MPV",
                                         float.Parse(MPV.Insert(MPV.Length - 1, ".")).ToString()));

                string PCT = strResutl[31].Trim();
                try
                {
                    if (PCT == "-----")
                    {
                        PCT = "0000";
                    }
                    else
                    {

                        PCT = strResutl[31].Trim();
                    }
                }
                catch (Exception)
                {

                    PCT = strResutl[31].Trim();
                }

                AddResult(new ResultItem("PCT",
                                         float.Parse(PCT.Insert(PCT.Length - 1, ".")).ToString()));
                string PDW = strResutl[32].Trim();
                try
                {
                    if (PDW == "-----")
                    {
                        PDW = "0000";
                    }
                    else
                    {

                        PDW = strResutl[32].Trim();
                    }
                }
                catch (Exception)
                {

                    PDW = strResutl[32].Trim();
                }
                AddResult(new ResultItem("PDW",
                                        float.Parse(PDW.Insert(PDW.Length - 1, ".")).ToString()));

                string L = strResutl[34].Trim();
                try
                {
                    if (L == "-----")
                    {
                        L = "0000";
                    }
                    else
                    {

                        L = strResutl[34].Trim();
                    }
                }
                catch (Exception)
                {

                    L = strResutl[34].Trim();
                }
                               AddResult(new ResultItem("%L",
                                         float.Parse(L.Insert(L.Length - 1, ".")).ToString(),
                                         "%"));
                string M = strResutl[35].Trim();
                try
                {
                    if (M == "-----")
                    {
                        M = "0000";
                    }
                    else
                    {

                        M = strResutl[35].Trim();
                    }
                }
                catch (Exception)
                {

                    M = strResutl[35].Trim();
                }
                AddResult(new ResultItem("%M",
                                         float.Parse(M.Insert(M.Length - 1, ".")).ToString(),
                                         "%"));
                string G = strResutl[36].Trim();
                try
                {
                    if (G == "-----")
                    {
                        G = "0000";
                    }
                    else
                    {

                        G = strResutl[36].Trim();
                    }
                }
                catch (Exception)
                {

                    G = strResutl[36].Trim();
                }
                AddResult(new ResultItem("%G",
                                         float.Parse(G.Insert(G.Length - 1, ".")).ToString(),
                                         "%"));
                ImportResults();
                Log.Trace("Finish Import Result");
              }
            catch (Exception ex)
            {
                Log.Error(string.Format("{0} /n {1}", "Lỗi Trong quá trình xử lý dữ liệu", ex));
                //File.AppendAllText(@"C:\CELLDYN1700-ERROR.txt", ex.ToString());
            }
            finally
            {
                ClearData();
            }
        }

        #endregion
    }
}