using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Workflow
{
    public sealed partial class OlympusAU : SequentialWorkflowActivity
    {
        public OlympusAU()
        {
            InitializeComponent();
        }
        /// <summary>
        /// The HandleExternalEvent use this property to store the data from the Host
        /// </summary>

        public AU480EventArgs _returnedArguments = default(AU480EventArgs);

        public string _receive;
        public byte _newStatus;//0:query;1:result;

        //public Result _singleResult;
        public List<string> sResults;

        public string _returnBarcode = string.Empty;
        public string _rackNo = string.Empty;
        public string _posNo = string.Empty;
        public string _sampleNo = string.Empty;

        private NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// This method is call after the handlerExternalEvent activities finish.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void SetResponseInVariables(object sender, ExternalDataEventArgs e)
        {
            _receive = _returnedArguments.Receive;
            _newStatus = _returnedArguments.ReceiveStatus;
        }
        private void TestIfQuery(object sender, ConditionalEventArgs e)
        {
            e.Result = (_newStatus == 0);
        }
        private void TestIfResult(object sender, ConditionalEventArgs e)
        {
            e.Result = (_newStatus == 1);
        }
        private void ParseData(object sender, EventArgs e)
        {
            try
            {

                char[] sep = { DeviceHelper.STX, DeviceHelper.ETX };
                sResults = new List<string>(_receive.Split(sep, StringSplitOptions.RemoveEmptyEntries));
                _log.Trace(sResults);

                //foreach (string sAUMessage in arrStringData)
                //{

                //    _log.Trace(sAUMessage);
                //    _singleResult = new Result();
                //    _singleResult.TestDate = DateTime.Now.ToString("dd/MM/yyyy");
                //    string barcode = sAUMessage.Substring(13, 10).Trim();

                //    //lấy barcode bệnh nhân
                //    _singleResult.Barcode = barcode.Length < 1 ? "0" : barcode;

                //    string tempPatient = sAUMessage, testCode, testValue;

                //    string temp = sAUMessage.Substring(28);
                //    while (temp.Length > 10)
                //    {
                //        //lấy tên loại XN
                //        testCode = temp.Substring(0, 3).Trim();
                //        //Lấy kq
                //        testValue = temp.Substring(3, 6).Trim();

                //        //add kết quả
                //        _singleResult.Add(new ResultItem(testCode, testValue));
                //        temp = temp.Remove(0, 11);

                //    }
                //    DeviceHelper.ImportResultToDb((short)GetTestTypeID(), e.AUTestResult, GetParaNameTable(), GetDeviceID());

                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ParseRequest(object sender, EventArgs e)
        {
            try
            {

                char[] sep = { DeviceHelper.STX, DeviceHelper.ETX };
                List<string> arrStringData = new List<string>(_receive.Split(sep, StringSplitOptions.RemoveEmptyEntries));
                foreach (string sAUMessage in arrStringData)
                {

                    if (sAUMessage.Length > 33)
                    {
                        _rackNo = sAUMessage.Substring(2, 4);
                        _posNo = sAUMessage.Substring(6, 2);
                        _sampleNo = sAUMessage.Substring(9, 4);
                        _returnBarcode = sAUMessage.Substring(13, 20).Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void codeActivity1_ExecuteCode(object sender, EventArgs e)
        {

            Console.WriteLine("Error!");
        }

        private void codeActivity2_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Cancel!");
        }
    }

}
