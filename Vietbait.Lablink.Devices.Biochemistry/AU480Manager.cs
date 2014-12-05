using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;
using Vietbait.Lablink.Workflow;

namespace Vietbait.Lablink.Devices.Biochemistry
{
    class AU480Manager : AUManager
    {
        private bool _newQuery = false;
        private string _rackNo;
        private string _posNo;
        private string _sampleNo;
        private bool _rerun;
        string _barcode;



        /*
         * Rack/Cup position use: Yes
         * Rack Number Digit: 4
         * Sex use: No
         * Use of age in years/months: No
         * Patient information  No
           
            Sample ID digits : 20 digits            
            Diluent type use Yes
            Reagent information use: No
            R2-1/R2-2 reagent information use: No
            Data digits 6 digits
            Data zero suppression: No
            Data flag outputs: 2 types
            Online test No. digits :2 digits            
            Sample type use: No
         */
        public override bool ProcessData(string inputBuffer, ref List<string> orderList)
        {
            try
            {
                //Kiểm tra dữ liệu
                // if (!ValidData()) return;
                Log.Debug("Input data: {0}",inputBuffer);
                string[] arrPatients = inputBuffer.Split(new char[]{DeviceHelper.STX, DeviceHelper.ETX},StringSplitOptions.RemoveEmptyEntries) ;
                
                foreach (string patient in arrPatients)
                {
                    if (patient.ToUpper().StartsWith("D"))
                    {
                        // Lấy ngày tháng năm hiện tại
                        TestResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                        string barcode = patient.Substring(13, 20).Trim();

                        //lấy barcode bệnh nhân
                        TestResult.Barcode = barcode.Length < 1 ? "0" : barcode;

                        string tempPatient = patient, testName, testValue;

                        string temp = tempPatient.Substring(38);
                        while (temp.Length > 9)
                        {
                            //lấy tên loại XN
                            testName = temp.Substring(0, 2).Trim();
                            //Lấy kq
                            testValue = temp.Substring(2, 6).Trim();

                            try
                            {
                                float test;
                                AddResult(float.TryParse(testValue, out test)
                                              ? new ResultItem(testName, test.ToString())
                                              : new ResultItem(testName, testValue));
                            }
                            catch (Exception)
                            {
                                AddResult(new ResultItem(testName, testValue));
                            }
                            //Xử lý các ký tự thừa trong kết quả

                            ////Loại bỏ dấu "$,"r"
                            //testValue = testValue.Replace("r", "").Replace("$", "");

                            ////Loại bỏ các chữ cái in hoa và in thường trong chuỗi kết quả
                            //for (byte i = 65; i <= 90; i++)
                            //{
                            //    testValue =
                            //        testValue.Replace(Convert.ToChar(i).ToString(), "").Replace(
                            //            Convert.ToChar(i + 32).ToString(), "");
                            //}
                            //add kết quả
                            //TestResult.Add(new ResultItem(testName, testValue));
                            temp = temp.Substring(10);
                        }
                        //Lưu vào db
                        Log.Debug("Begin Import Result");
                        Log.Debug(ImportResults() ? "Import Success" : "Import false");
                    }
                
                    else if (patient.ToUpper().StartsWith("R"))
                    {
                        if (patient.ToUpper().StartsWith("RH"))
                        {
                            _rerun = true;
                        }
                        else
                        {
                            _rerun = false;
                        }
                        _rackNo = patient.Substring(2, 4);
                        _posNo = patient.Substring(6, 2);
                        _sampleNo = patient.Substring(9, 4);
                        _barcode = patient.Substring(13, 20).Trim();
                        var regList = GetRegList(_barcode);
                        if (regList != null)
                        {
                            if (regList.Count > 0)
                            {
                                Log.Debug(string.Format("So order: {0}", regList.Count.ToString()));
                                foreach (string s in regList)
                                {
                                    Log.Debug(string.Format("{0}\r\n", s));
                                }
                            }
                            else
                            {
                                Log.Debug("No order!");
                                orderList.Add(string.Concat(DeviceHelper.STX,"SE",DeviceHelper.ETX));
                                return true;
                            }
                        }
                        else
                        {
                            Log.Debug("No order!");
                            orderList.Add(string.Concat(DeviceHelper.STX, "SE", DeviceHelper.ETX));
                            return true;
                        }
                        orderList = CreateOrderFrame(regList);
                        
                        return true;
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                Log.ErrorException("AU480 Exception: ",ex);
            }
            return false;
        }
        private List<string> CreateOrderFrame(List<string> orderList)
        {
            var retList=new List<string>();
            string request;
            request = _rerun ? "SH" : "S ";
            request = string.Format(@"{0}{1}{2} {3}{4}    E", request, _rackNo, _posNo, _sampleNo, _barcode.PadLeft(20));
            foreach (string s in orderList)
            {
                request = string.Format(@"{0}{1}", request, s);
            }
            Log.Debug(@"Request string: {0}",request);
            retList.Add(string.Concat(DeviceHelper.STX,request,DeviceHelper.ETX));
            return retList;
        }

        
    }
}
