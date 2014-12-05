using System;
using System.Collections.Generic;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    [ExternalDataExchange]
    public interface IRS232Communication 
    {
        #region Communication WF - > Host
        /// <summary>
        /// This method must be call by the CallExternalMethod Activity 
        /// to transfer the information to the console host application 
        /// <param name="response">Data to send to console</param >
        void SendDataToHost(List<string> response);
        #endregion

        #region Communication Host -> WF
        /// <summary>
        /// External Event to send information to workflow (number)
        /// </summary>
        event EventHandler<RS232EventArguments> SendDataToWF;

        #endregion
    }
}
