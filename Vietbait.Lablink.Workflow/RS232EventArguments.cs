using System;
using System.Collections.Generic;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    /// <summary>
    /// See the attribute "Serializable" This argument must be
    /// passed fromHost to Workflow 
    /// through a underline Intercomunication Process. 
    /// You must mark as serializable!!
    /// </summary>
    [Serializable]
    public class RS232EventArguments : ExternalDataEventArgs
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
        #region Create a Constructor with the InstanceId to pass to base class

        public RS232EventArguments(Guid instanceID)
            : base(instanceID)
        {
        }

        #endregion
    }
}