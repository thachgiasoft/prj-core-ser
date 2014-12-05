using System;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    public class ClsAu : InterfaceAU
    {
        public event EventHandler<ExternalDataEventArgs> GetUnparseDataEvent;
        public event EventHandler<ExternalDataEventArgs> GetDataEvent;
        public event EventHandler<ExternalDataEventArgs> GetQuery;
        public event EventHandler<ExternalDataEventArgs> GetACK;
        public event EventHandler<ExternalDataEventArgs> GetNAK;

        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler SendACKEvent;

        /// <summary >
        /// Event to communicate the host the result.
        /// </summary >
        public event EventHandler SendNAKEvent;
        /// <summary>
        /// Implement the external Method
        /// </summary>
        public void SendACK()
        {
            var e = new EventArgs();

            EventHandler sendData = SendACKEvent;
            if (sendData != null)
            {
                sendData(this, e);
            }
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

        public void CallGetUnparseData(ExternalDataEventArgs args)
        {
            GetUnparseDataEvent(null, args);
            Console.WriteLine("Get Unparse Data!");
        }

        public void CallGetData(ExternalDataEventArgs args)
        {
            GetDataEvent(null, args);
            Console.WriteLine("Get Data!");
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
    }
}