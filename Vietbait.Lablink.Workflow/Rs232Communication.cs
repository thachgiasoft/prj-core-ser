using System;
using System.Collections.Generic;
using Vietbait.Lablink.TestResult;

namespace Vietbait.Lablink.Workflow
{
    public class Rs232Communication :  IRS232Communication
    {    
        #region Implementation of IRS232Communication
        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler<SendDataToHostEventArgs> SendDataToHostEvent;
        public void SendDataToHost(List<string> response)
        {
            SendDataToHostEventArgs e = new SendDataToHostEventArgs();
            e.Response = response;
            EventHandler<SendDataToHostEventArgs> sendData = this.SendDataToHostEvent;
            if (sendData != null)
            {
                sendData(this, e);
            }
        }

        public event EventHandler<RS232EventArguments> SendDataToWF;

        public void RaiseEventSendDataToWF(Guid instanceID,string sReceive)
        {
            if (SendDataToWF != null)
            {
                RS232EventArguments e = new RS232EventArguments(instanceID);
                e.Receive = sReceive;
                SendDataToWF(null, e);                
            }
        }
        #endregion

        
    }
}