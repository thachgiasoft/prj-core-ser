using System;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    [ExternalDataExchange]
    public interface InterfaceAU
    {
        //event EventHandler<ExternalDataEventArgs> OnIncomingData;
        event EventHandler<ExternalDataEventArgs> GetUnparseDataEvent;
        event EventHandler<ExternalDataEventArgs> GetDataEvent;
        //event EventHandler<ExternalDataEventArgs> GetWrongFrame;
        //event EventHandler<ExternalDataEventArgs> GetEOT;
        event EventHandler<ExternalDataEventArgs> GetQuery;
        event EventHandler<ExternalDataEventArgs> GetACK;
        event EventHandler<ExternalDataEventArgs> GetNAK;        
        //event EventHandler<ExternalDataEventArgs> CloseSession;
        //event EventHandler<ExternalDataEventArgs> Timeout;

        void SendACK();
        void SendNAK();
        //void SendENQ();
        //void SendEOT();
        
    }
}