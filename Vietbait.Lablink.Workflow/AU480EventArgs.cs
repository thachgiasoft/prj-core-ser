using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    [Serializable]
    public class AU480EventArgs : ExternalDataEventArgs
	{
        private string _receive = string.Empty;
        private byte _receivestatus = 0;

        /// <summary >
        /// This property is used to pass in the Event
        /// the response to host
        /// </summary >
        public string Receive
        {
            get { return _receive; }
            set { _receive = value; }
        }
        public byte ReceiveStatus
        {
            get { return _receivestatus; }
            set { _receivestatus = value; }
        }

        
        public AU480EventArgs(Guid instanceId) : base(instanceId)
        {
        }
	}
}
