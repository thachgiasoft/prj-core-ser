using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;
using Vietbait.Lablink.TestResult;

namespace Vietbait.Lablink.Workflow
{
    [ExternalDataExchange]
	interface IOlympusAUComm
	{
        #region Communication Host -> WF
        /// <summary>
        /// External Event to send information to workflow (number)
        /// </summary>
        event EventHandler<AU480EventArgs> SendAU480DataToWF;
        event EventHandler<ExternalDataEventArgs> GetRequestStartEvent; 
        #endregion
        void SendACK();
        //void GetRequestStart();
        void SendAUDataToHost(List<string> result);
        void SendAURequestToHost(string RackNo,string PosNo,string SampleNo,string Barcode);
	}
}
