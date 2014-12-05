using System;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    public class ClsAstm : InterfaceASTM

    {
        //public string BufferData;

        //#region Implementation of InterfaceASTM

        ////public event EventHandler<ExternalDataEventArgs> OnIncomingData;

        #region InterfaceASTM Members

        public event EventHandler<ExternalDataEventArgs> GetENQ;
        public event EventHandler<ExternalDataEventArgs> GetRightFrame;
        public event EventHandler<ExternalDataEventArgs> GetWrongFrame;
        public event EventHandler<ExternalDataEventArgs> GetEOT;
        public event EventHandler<ExternalDataEventArgs> GetQuery;
        public event EventHandler<ExternalDataEventArgs> GetACK;
        public event EventHandler<ExternalDataEventArgs> GetNAK;
        public event EventHandler<ExternalDataEventArgs> CloseSession;
        public event EventHandler<ExternalDataEventArgs> Timeout;

        /// <summary>
        /// Implement the external Method
        /// </summary>
        public void SendACK()
        {
           SendACKEvent.Invoke(this,new EventArgs());
        }

        /// <summary>
        /// Implement the external Method
        /// </summary>
        public void SendNAK()
        {
            var e = new EventArgs();

            EventHandler sendData = SendNAKEvent;
            if (sendData != null)
            {
                sendData(this, e);
            }
        }

        /// <summary>
        /// Implement the external Method
        /// </summary>
        public void SendENQ()
        {
            var e = new EventArgs();

            EventHandler sendData = SendENQEvent;
            if (sendData != null)
            {
                sendData(this, e);
            }
        }

        /// <summary>
        /// Implement the external Method
        /// </summary>
        public void SendEOT()
        {
            //var e = new EventArgs();

            //EventHandler sendData = SendEOTEvent;
            //if (sendData != null)
            //{
            //    sendData(this, e);
            //}
            SendEOTEvent.Invoke(this, new EventArgs());
        }

        

        #endregion

        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler SendACKEvent;

        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler SendNAKEvent;

        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler SendENQEvent;

        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler SendEOTEvent;
        

        public void CallGetENQ(ExternalDataEventArgs args)
        {
            GetENQ(null, args);
            Console.WriteLine("Get ENQ!");
            //SendByte((byte)DeviceHelper.ACK);
        }

        public void CallGetRightFrame(ExternalDataEventArgs args)
        {
            GetRightFrame(null, args);
            Console.WriteLine("Get Right Frame!");
        }

        public void CallGetWrongFrame(ExternalDataEventArgs args)
        {
            GetWrongFrame(null, args);
            Console.WriteLine("Get Wrong Frame!");
        }

        public void CallGetEOT(ExternalDataEventArgs args)
        {
            GetEOT(null, args);
            Console.WriteLine("Get EOT!");
        }

        public void CallGetQuery(ExternalDataEventArgs args)
        {
            GetQuery(null, args);
            Console.WriteLine("Get Query!");
        }

        public void CallGetACK(ExternalDataEventArgs args)
        {
            GetACK(null, args);
            Console.WriteLine("Get ACK!");
        }

        public void CallGetNAK(ExternalDataEventArgs args)
        {
            GetNAK(null, args);
            Console.WriteLine("Get NAK!");
        }

        public void CallCloseSession(ExternalDataEventArgs args)
        {
            CloseSession(null, args);
            Console.WriteLine("Close Session!");
        }
        public void CallTimeout(ExternalDataEventArgs args)
        {
            Timeout(null, args);
            Console.WriteLine("Close Session!");
        }
    }
}