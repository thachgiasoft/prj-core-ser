using System;
using System.IO;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Urine
{
    internal class OPT151 : Rs232Devcie
    {

        public override void ProcessRawData()
        {
            try
            {
                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(StringData);
                string[] arrPatients = SeparatorData(StringData);
                string testdate = string.Format("{0}/{1}/{2}",arrPatients[0].Substring(8, 2), arrPatients[0].Substring(5, 2) , arrPatients[0].Substring(0, 4));

                TestResult.TestDate = testdate;
                string pBarcode = arrPatients[2];
                TestResult.Barcode = pBarcode;
                Log.Trace(string.Format("Barcode: {0}", pBarcode));
                string LEU=arrPatients[3].Substring(4).Trim();
                if (LEU == "neg.")
                {
                    LEU = "NEGATIVE";
                }
                else
                {
                    LEU = arrPatients[3].Substring(4).Trim(); ;

                }
                string NIT = arrPatients[4].Substring(4).Trim();
                if (NIT == "neg.")
                {
                    NIT = "NEGATIVE";
                }
                else
                {
                    NIT = arrPatients[4].Substring(4).Trim(); ;

                }
                string URO = arrPatients[5].Substring(4).Trim().Split()[0];
                if (URO == "neg.")
                {
                    URO = "NEGATIVE";
                }
                else
                {
                    URO = arrPatients[5].Substring(4).Trim().Split()[0]; ;

                }
                string PRO = arrPatients[6].Substring(4).Trim().Split()[0];
                if (PRO == "neg.")
                {
                    PRO = "NEGATIVE";
                }
                else
                {
                    PRO = arrPatients[6].Substring(4).Trim().Split()[0]; ;

                }

                string PH = arrPatients[7].Substring(4).Trim();
                if (PH == "neg.")
                {
                    PH = "NEGATIVE";
                }
                else
                {
                    PH = arrPatients[7].Substring(4).Trim();

                }
                string BLD = arrPatients[8].Substring(4).Trim();
                if (BLD == "neg.")
                {
                    BLD = "NEGATIVE";
                }
                else
                {
                    BLD = arrPatients[8].Substring(4).Trim();
                }

                string SG =  arrPatients[9].Substring(4).Trim();
                if (SG == "neg.")
                {
                    SG = "NEGATIVE";
                }
                else
                {
                    SG =  arrPatients[9].Substring(4).Trim();
                }
                string KET = arrPatients[10].Substring(4).Trim();
                if (KET == "neg.")
                {
                    KET = "NEGATIVE";
                }
                else
                {
                    KET = arrPatients[10].Substring(4).Trim();
                }
                string BIL = arrPatients[11].Substring(4).Trim();
                if (BIL == "neg.")
                {
                    BIL = "NEGATIVE";
                }
                else
                {
                    BIL = arrPatients[11].Substring(4).Trim();
                }
                string GLU = arrPatients[12].Substring(4).Trim();
                if (GLU == "neg.")
                {
                    GLU = "NEGATIVE";
                }
                else
                {
                    GLU =  arrPatients[12].Substring(4).Trim();
                }
                AddResult(new ResultItem(arrPatients[3].Substring(0, 3).Trim(), LEU));
                AddResult(new ResultItem(arrPatients[4].Substring(0, 3).Trim(), NIT));
                AddResult(new ResultItem(arrPatients[5].Substring(0, 3).Trim(), URO));
                AddResult(new ResultItem(arrPatients[6].Substring(0, 3).Trim(), PRO));
                AddResult(new ResultItem(arrPatients[7].Substring(0, 3).Trim(),PH));
                AddResult(new ResultItem(arrPatients[8].Substring(0, 3).Trim(), BLD));
                AddResult(new ResultItem(arrPatients[9].Substring(0, 3).Trim(), SG));
                AddResult(new ResultItem(arrPatients[10].Substring(0, 3).Trim(), KET));
                AddResult(new ResultItem(arrPatients[11].Substring(0, 3).Trim(), BIL));
                AddResult(new ResultItem(arrPatients[12].Substring(0, 3).Trim(), GLU));
            
                ImportResults();
                Log.Trace("Finish Import Result");



            }
            catch (Exception)
            {
                ClearData();
                throw;
            }
            finally
            {
                ClearData();
            }
        }

        private string[] SeparatorData(string stringData)
        {
            try
            {
                string[] arrStringData = stringData.Split(DeviceHelper.CRLF.ToCharArray());
                arrStringData = DeviceHelper.DeleteAllBlankLine(arrStringData);
                return arrStringData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}