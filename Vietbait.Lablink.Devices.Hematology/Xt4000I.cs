using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Devices.Hematology
{
    internal class Xt4000I : Rs232AstmDevice
        //internal class XS1000I : Rs232Devcie
    {
        #region Feild

        /// <summary>
        /// Tên file lưu đường dẫn
        /// Đường dẫn này lưu vào dòng số 1
        /// </summary>
        private string _xt4000IconfigFileName = "_Xt-4000iConfig.txt";

        /// <summary>
        /// Thư mục lưu đường dẫn ảnh do máy Xt trả ra
        /// Thư mục này được lưu trên máy XT và cấu hình qua file 
        /// </summary>
        private string _sourceImagePath = string.Empty;

        /// <summary>
        /// Thư mục lưu ảnh nhận được 
        /// Thư mục này lấy trong SystemParam
        /// </summary>
        private string _destImagePath = string.Empty;


        #endregion

        /// <summary>
        /// Xử lý sau kkhi nhận được dữ liệu
        /// </summary>
        public override void ProcessData()
        {
            try
            {
                // Nạp thông tin cấu hình

                // Kiểm tra file cấu hình có tồn tại hay không?
                // Nếu không tồn tại thì tạo file mới
                if (!File.Exists(_xt4000IconfigFileName))
                {
                    File.WriteAllText(_xt4000IconfigFileName,string.Format("\r\nDòng 1: Đường dẫn của thư mục chứa ảnh trên máy XT-400i"));
                }
                else
                {
                    _sourceImagePath = File.ReadAllLines(_xt4000IconfigFileName)[0];
                    _destImagePath = DeviceHelper.GetImageFolder();
                    if (!_destImagePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                        _destImagePath = string.Format("{0}{1}", _destImagePath, Path.DirectorySeparatorChar);
                    if (!_sourceImagePath.EndsWith(Path.DirectorySeparatorChar.ToString()))
                        _sourceImagePath = string.Format("{0}{1}", _sourceImagePath, Path.DirectorySeparatorChar);
                }

                Log.Trace("Begin Process Data");
                //Lưu lại Data
                Log.Trace(StringData);

                //Lấy về dữ liệu của các bệnh nhân
                IEnumerable<string> arrPatients = SeparatorData(StringData);

                //Duyệt qua mảng xử lý dữ liệu của từng bệnh nhân
                foreach (string patient in arrPatients)
                {
                    IEnumerable<string> resultArray = null;
                    var records = patient.Split(DeviceHelper.CR);
                    foreach (string record in records)
                    {
                        string[] temp;
                        if (record.StartsWith("P"))
                        {
                            temp = record.Split('|');
                            if (temp.Length > 4)
                            {
                                TestResult.Barcode = temp[4].Trim();
                                Log.Debug("Barcode:{0}", TestResult.Barcode);
                            }
                        }
                        else if (record.StartsWith("O"))
                        {
                            temp = record.Split('|');
                            //Lấy barcode xét nghiệm
                            resultArray = from s in temp[4].Split("\\".ToCharArray())
                                          select s.Split('^')[4];
                            try
                            {
                                if (TestResult.Barcode.Trim() == "")
                                {
                                    string strTempbarcode = temp[3].Split('^')[2].Trim();
                                    TestResult.Barcode = strTempbarcode == "" ? "0000" : strTempbarcode;
                                    Log.Debug("Barcode:{0}", TestResult.Barcode);
                                }
                            }
                            catch (Exception)
                            {
                                TestResult.Barcode = "0000";
                                Log.Debug("Barcode:{0}", TestResult.Barcode);
                            }
                        }
                        else if (record.StartsWith("R"))
                        {
                            try
                            {
                                temp = record.Split('|');
                                //Lấy về ngày tháng làm xét nghiệm
                                if (String.IsNullOrEmpty(TestResult.TestDate))
                                {
                                    string tempDate = temp[12];
                                    TestResult.TestDate = string.Format("{0}/{1}/{2}", tempDate.Substring(6, 2),
                                                                        tempDate.Substring(4, 2),
                                                                        tempDate.Substring(0, 4));
                                }

                                string strTestName = temp[2].Split('^')[4].Trim();
                                bool testInclude = (from s in resultArray
                                                    where s == strTestName
                                                    select s).Any();
                                if (!testInclude) continue;


                                string strTestValue = temp[3].Trim();
                                string strTestUnit = temp[4].Trim();

                                if (strTestName == "HGB")
                                {
                                    try
                                    {
                                        strTestValue = Math.Round(Convert.ToDouble(strTestValue)*10, 3).ToString();
                                    }
                                    catch
                                    {
                                    }
                                }
                                else if (strTestName == "MCHC")
                                {
                                    try
                                    {
                                        strTestValue = Math.Round(Convert.ToDouble(strTestValue)*10, 3).ToString();
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                else if (strTestName == "HCT")
                                {
                                    try
                                    {
                                        strTestValue = Math.Round(Convert.ToDouble(strTestValue)/100, 3).ToString();
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                AddResult(new ResultItem(strTestName, strTestValue, strTestUnit));
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while processing data Error: {0}", ex);
                                continue;
                            }
                        }
                    }
                    // Lưu các ảnh thu được:

                    // Kiểm tra nếu các thư mục lưu ảnh tồn tại
                    if (Directory.Exists(_destImagePath) && Directory.Exists(_sourceImagePath))
                    {
                        IEnumerable<string> lstImg = from r in records
                                                     let header = r.Split('|')[0]
                                                     where
                                                         header.StartsWith("R") &&
                                                         r.Split('|')[3].ToUpper().EndsWith(".PNG")
                                                     select r;

                        foreach (string s in lstImg)
                        {
                            try
                            {
                                string[] temp = s.Split('|');
                                string strTestName = temp[2].Split('^')[4].Trim();
                                string strTestValue = temp[3].Trim();
                                string[] x = strTestValue.Split('&');
                                strTestValue = x[x.Length - 1];
                                // Copy Image
                                var sourceFileName = string.Format("{0}{2}{1}{3}", _sourceImagePath,Path.DirectorySeparatorChar,x[2], strTestValue);
                                var destFileName = string.Format("{0}{1}", _destImagePath, strTestValue);
                                if(File.Exists(sourceFileName)) File.Copy(sourceFileName, destFileName,true);
                                Log.Debug("Copy file Success: {0} to {1}",sourceFileName,destFileName);
                                AddResult(new ResultItem(strTestName, destFileName));
                            }
                            catch (Exception ex)
                            {
                                Log.Error("Error while copy image to folder - Error:{0}",ex.ToString());
                            }
                        }
                    }
                    else
                    {
                        Log.Error("Image Folder is not available - could not copy Image");
                    }

                    

                    Log.Trace("Begin Import Result");
                    Log.Trace(ImportResults() ? "Import Result Success" : "Error While Import result");

                   

                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while processing data Error: {0}", ex);
            }
            finally
            {
                ClearData();
            }
        }

        private IEnumerable<string> SeparatorData(string stringData)
        {
            try
            {
                //Bỏ tất cả các ký tự cho đến khi gặp ký tự "P" đầu tiên
                stringData =
                    stringData.Substring(stringData.IndexOf(DeviceHelper.CR + "P|", StringComparison.Ordinal) + 1);

                //Ngắt chuỗi theo ký tự CR
                string[] arrStringData = stringData.Split(DeviceHelper.CR);

                //Biến để lưu tất cả các chuỗi kết quả.
                //Mỗi phần tử của chuỗi là một kết quả của bệnh nhân gồm các record P,O,R,C.....
                var allResult = new string[] {};

                //Biến lưu Index
                int id = 0;

                foreach (string line in arrStringData)
                {
                    if (line.StartsWith("P"))
                    {
                        id++;
                        Array.Resize(ref allResult, id);
                    }
                    allResult[id - 1] = allResult[id - 1] + line + DeviceHelper.CR;
                }

                return allResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}