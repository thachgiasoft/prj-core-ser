using System;
using System.Collections.Generic;
using Vietbait.Lablink.TestResult;
using Vietbait.Lablink.Utilities;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    public class OlympusAUComm : IOlympusAUComm
    {
        #region Implementation of IOlympusAUComm

        
        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler<SendDataToHostEventArgs> ReturnDataEvent;

        public event EventHandler<ExternalDataEventArgs> GetRequestStartEvent;
        
        public void GetRequestStart(ExternalDataEventArgs args)
        {
            GetRequestStartEvent(null, args);
            //Console.WriteLine("Get ENQ!");
            //SendByte((byte)DeviceHelper.ACK);
        }
        public void SendAUDataToHost(List< string > result)
        {
            try
            {
                var e = new SendDataToHostEventArgs();
                e.AUStringResult  = result;

                EventHandler<SendDataToHostEventArgs> sendData = this.ReturnDataEvent;
                if (sendData != null)
                {

                    sendData(this, e);

                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }

        public event EventHandler<SendDataToHostEventArgs> ReturnRequestEvent;
        public void SendAURequestToHost(string RackNo, string PosNo, string SampleNo, string Barcode)
        {
            try
            {
                var e = new SendDataToHostEventArgs();
                e.AUBarcode = Barcode;
                e.AUPosNo = PosNo;
                e.AURackNo = RackNo;
                e.AUSampleNo = SampleNo;
                EventHandler<SendDataToHostEventArgs> sendData = this.ReturnRequestEvent;
                if (sendData != null)
                {
                    sendData(this, e);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

        #region Implementation of IOlympusAUComm

        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler SendACKEvent;

        public void SendACK()
        {
            var e = new EventArgs();

            EventHandler sendData = SendACKEvent;
            if (sendData != null)
            {
                sendData(this, e);
            }
        }


        public event EventHandler<AU480EventArgs> SendAU480DataToWF;

        public void RaiseEventSendDataToWF(Guid instanceID, string sReceive,byte bStatus)
        {
            if (SendAU480DataToWF != null)
            {
                AU480EventArgs e = new AU480EventArgs(instanceID);
                e.Receive = sReceive;
                e.ReceiveStatus = bStatus;
                SendAU480DataToWF(null, e);
            }
        }
        #endregion

        //#region IOlympusAUComm Members


        //event EventHandler<ExternalDataEventArgs> IOlympusAUComm.GetRequestStartEvent
        //{
        //    add { throw new NotImplementedException(); }
        //    remove { throw new NotImplementedException(); }
        //}

        //#endregion
    }
}