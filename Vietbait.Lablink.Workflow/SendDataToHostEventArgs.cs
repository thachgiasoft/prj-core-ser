using System;
using System.Collections.Generic;

namespace Vietbait.Lablink.Workflow
{

    public class SendDataToHostEventArgs : EventArgs
    {

        List<string> _sResults;

        /// <summary >
        /// This property is used to pass in the Event
        /// the response to host
        /// </summary >
        public  string AURackNo { get; set; }
        public  string AUPosNo { get; set; }
        public  string AUSampleNo { get; set; }
        public  string AUBarcode { get; set; }
        
        /// <summary >
        /// This property is used to pass in the Event
        /// the response to host
        /// </summary >
        public List<string> AUStringResult
        {
            get
            {
                return _sResults;
            }
            set
            {
                _sResults = value; 
            }
        }
    }
}