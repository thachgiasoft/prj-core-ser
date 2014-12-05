using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    partial class ASTMWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.setStateSendRequestByNAK = new System.Workflow.Activities.SetStateActivity();
            this.HandleGetNAKEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStateActivity1 = new System.Workflow.Activities.SetStateActivity();
            this.SendEOT = new System.Workflow.Activities.CallExternalMethodActivity();
            this.HandleCloseSessionEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.SetSendRequest1 = new System.Workflow.Activities.SetStateActivity();
            this.HandleGetACKEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.codeInitializationSendRequest = new System.Workflow.Activities.CodeActivity();
            this.setStateIdle = new System.Workflow.Activities.SetStateActivity();
            this.InitActivity = new System.Workflow.Activities.CodeActivity();
            this.setBackReceiveData2 = new System.Workflow.Activities.SetStateActivity();
            this.SendACK2 = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleGetRightFrame = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setBackReceiveData = new System.Workflow.Activities.SetStateActivity();
            this.SendNAK = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleGetWrongFrame = new System.Workflow.Activities.HandleExternalEventActivity();
            this.codeReceiveDataInittialization = new System.Workflow.Activities.CodeActivity();
            this.setStateSendRequest = new System.Workflow.Activities.SetStateActivity();
            this.SendENQ = new System.Workflow.Activities.CallExternalMethodActivity();
            this.HandleGetQueryEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.codeActivityInit = new System.Workflow.Activities.CodeActivity();
            this.setStateActivity3 = new System.Workflow.Activities.SetStateActivity();
            this.handleGetEOT = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setReceiveData = new System.Workflow.Activities.SetStateActivity();
            this.SendACK = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleGetENQ = new System.Workflow.Activities.HandleExternalEventActivity();
            this.GetNAKEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.CloseSessionEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.GetACKEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.stateInitializationSendRequest = new System.Workflow.Activities.StateInitializationActivity();
            this.stateInitializationWorkflow = new System.Workflow.Activities.StateInitializationActivity();
            this.GetRightFrameEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.GetWrongFrameEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.stateInitializationReceiveData = new System.Workflow.Activities.StateInitializationActivity();
            this.GetQueryEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.stateInitializationIdle = new System.Workflow.Activities.StateInitializationActivity();
            this.GetEOTEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.GetENQEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.SendRequest = new System.Workflow.Activities.StateActivity();
            this.InitWorkflow = new System.Workflow.Activities.StateActivity();
            this.ReceiveData = new System.Workflow.Activities.StateActivity();
            this.Idle = new System.Workflow.Activities.StateActivity();
            // 
            // setStateSendRequestByNAK
            // 
            this.setStateSendRequestByNAK.Name = "setStateSendRequestByNAK";
            this.setStateSendRequestByNAK.TargetStateName = "SendRequest";
            // 
            // HandleGetNAKEvent
            // 
            this.HandleGetNAKEvent.EventName = "GetNAK";
            this.HandleGetNAKEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.HandleGetNAKEvent.Name = "HandleGetNAKEvent";
            // 
            // setStateActivity1
            // 
            this.setStateActivity1.Name = "setStateActivity1";
            this.setStateActivity1.TargetStateName = "InitWorkflow";
            // 
            // SendEOT
            // 
            this.SendEOT.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.SendEOT.MethodName = "SendEOT";
            this.SendEOT.Name = "SendEOT";
            // 
            // HandleCloseSessionEvent
            // 
            this.HandleCloseSessionEvent.EventName = "CloseSession";
            this.HandleCloseSessionEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.HandleCloseSessionEvent.Name = "HandleCloseSessionEvent";
            // 
            // SetSendRequest1
            // 
            this.SetSendRequest1.Name = "SetSendRequest1";
            this.SetSendRequest1.TargetStateName = "SendRequest";
            // 
            // HandleGetACKEvent
            // 
            this.HandleGetACKEvent.EventName = "GetACK";
            this.HandleGetACKEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.HandleGetACKEvent.Name = "HandleGetACKEvent";
            // 
            // codeInitializationSendRequest
            // 
            this.codeInitializationSendRequest.Name = "codeInitializationSendRequest";
            this.codeInitializationSendRequest.ExecuteCode += new System.EventHandler(this.codeInitializationSendRequest_ExecuteCode);
            // 
            // setStateIdle
            // 
            this.setStateIdle.Name = "setStateIdle";
            this.setStateIdle.TargetStateName = "Idle";
            // 
            // InitActivity
            // 
            this.InitActivity.Name = "InitActivity";
            this.InitActivity.ExecuteCode += new System.EventHandler(this.InitActivity_ExecuteCode);
            // 
            // setBackReceiveData2
            // 
            this.setBackReceiveData2.Name = "setBackReceiveData2";
            this.setBackReceiveData2.TargetStateName = "ReceiveData";
            // 
            // SendACK2
            // 
            this.SendACK2.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.SendACK2.MethodName = "SendACK";
            this.SendACK2.Name = "SendACK2";
            // 
            // handleGetRightFrame
            // 
            this.handleGetRightFrame.EventName = "GetRightFrame";
            this.handleGetRightFrame.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.handleGetRightFrame.Name = "handleGetRightFrame";
            // 
            // setBackReceiveData
            // 
            this.setBackReceiveData.Name = "setBackReceiveData";
            this.setBackReceiveData.TargetStateName = "ReceiveData";
            // 
            // SendNAK
            // 
            this.SendNAK.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.SendNAK.MethodName = "SendNAK";
            this.SendNAK.Name = "SendNAK";
            // 
            // handleGetWrongFrame
            // 
            this.handleGetWrongFrame.EventName = "GetWrongFrame";
            this.handleGetWrongFrame.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.handleGetWrongFrame.Name = "handleGetWrongFrame";
            // 
            // codeReceiveDataInittialization
            // 
            this.codeReceiveDataInittialization.Name = "codeReceiveDataInittialization";
            this.codeReceiveDataInittialization.ExecuteCode += new System.EventHandler(this.codeReceiveDataInittialization_ExecuteCode);
            // 
            // setStateSendRequest
            // 
            this.setStateSendRequest.Name = "setStateSendRequest";
            this.setStateSendRequest.TargetStateName = "SendRequest";
            // 
            // SendENQ
            // 
            this.SendENQ.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.SendENQ.MethodName = "SendENQ";
            this.SendENQ.Name = "SendENQ";
            // 
            // HandleGetQueryEvent
            // 
            this.HandleGetQueryEvent.EventName = "GetQuery";
            this.HandleGetQueryEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.HandleGetQueryEvent.Name = "HandleGetQueryEvent";
            // 
            // codeActivityInit
            // 
            this.codeActivityInit.Name = "codeActivityInit";
            this.codeActivityInit.ExecuteCode += new System.EventHandler(this.codeActivityInit_ExecuteCode);
            // 
            // setStateActivity3
            // 
            this.setStateActivity3.Name = "setStateActivity3";
            this.setStateActivity3.TargetStateName = "InitWorkflow";
            // 
            // handleGetEOT
            // 
            this.handleGetEOT.EventName = "GetEOT";
            this.handleGetEOT.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.handleGetEOT.Name = "handleGetEOT";
            // 
            // setReceiveData
            // 
            this.setReceiveData.Name = "setReceiveData";
            this.setReceiveData.TargetStateName = "ReceiveData";
            // 
            // SendACK
            // 
            this.SendACK.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.SendACK.MethodName = "SendACK";
            this.SendACK.Name = "SendACK";
            // 
            // handleGetENQ
            // 
            this.handleGetENQ.EventName = "GetENQ";
            this.handleGetENQ.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.handleGetENQ.Name = "handleGetENQ";
            // 
            // GetNAKEvent
            // 
            this.GetNAKEvent.Activities.Add(this.HandleGetNAKEvent);
            this.GetNAKEvent.Activities.Add(this.setStateSendRequestByNAK);
            this.GetNAKEvent.Name = "GetNAKEvent";
            // 
            // CloseSessionEvent
            // 
            this.CloseSessionEvent.Activities.Add(this.HandleCloseSessionEvent);
            this.CloseSessionEvent.Activities.Add(this.SendEOT);
            this.CloseSessionEvent.Activities.Add(this.setStateActivity1);
            this.CloseSessionEvent.Name = "CloseSessionEvent";
            // 
            // GetACKEvent
            // 
            this.GetACKEvent.Activities.Add(this.HandleGetACKEvent);
            this.GetACKEvent.Activities.Add(this.SetSendRequest1);
            this.GetACKEvent.Name = "GetACKEvent";
            // 
            // stateInitializationSendRequest
            // 
            this.stateInitializationSendRequest.Activities.Add(this.codeInitializationSendRequest);
            this.stateInitializationSendRequest.Name = "stateInitializationSendRequest";
            // 
            // stateInitializationWorkflow
            // 
            this.stateInitializationWorkflow.Activities.Add(this.InitActivity);
            this.stateInitializationWorkflow.Activities.Add(this.setStateIdle);
            this.stateInitializationWorkflow.Name = "stateInitializationWorkflow";
            // 
            // GetRightFrameEvent
            // 
            this.GetRightFrameEvent.Activities.Add(this.handleGetRightFrame);
            this.GetRightFrameEvent.Activities.Add(this.SendACK2);
            this.GetRightFrameEvent.Activities.Add(this.setBackReceiveData2);
            this.GetRightFrameEvent.Name = "GetRightFrameEvent";
            // 
            // GetWrongFrameEvent
            // 
            this.GetWrongFrameEvent.Activities.Add(this.handleGetWrongFrame);
            this.GetWrongFrameEvent.Activities.Add(this.SendNAK);
            this.GetWrongFrameEvent.Activities.Add(this.setBackReceiveData);
            this.GetWrongFrameEvent.Name = "GetWrongFrameEvent";
            // 
            // stateInitializationReceiveData
            // 
            this.stateInitializationReceiveData.Activities.Add(this.codeReceiveDataInittialization);
            this.stateInitializationReceiveData.Name = "stateInitializationReceiveData";
            // 
            // GetQueryEvent
            // 
            this.GetQueryEvent.Activities.Add(this.HandleGetQueryEvent);
            this.GetQueryEvent.Activities.Add(this.SendENQ);
            this.GetQueryEvent.Activities.Add(this.setStateSendRequest);
            this.GetQueryEvent.Name = "GetQueryEvent";
            // 
            // stateInitializationIdle
            // 
            this.stateInitializationIdle.Activities.Add(this.codeActivityInit);
            this.stateInitializationIdle.Name = "stateInitializationIdle";
            // 
            // GetEOTEvent
            // 
            this.GetEOTEvent.Activities.Add(this.handleGetEOT);
            this.GetEOTEvent.Activities.Add(this.setStateActivity3);
            this.GetEOTEvent.Name = "GetEOTEvent";
            // 
            // GetENQEvent
            // 
            this.GetENQEvent.Activities.Add(this.handleGetENQ);
            this.GetENQEvent.Activities.Add(this.SendACK);
            this.GetENQEvent.Activities.Add(this.setReceiveData);
            this.GetENQEvent.Name = "GetENQEvent";
            // 
            // SendRequest
            // 
            this.SendRequest.Activities.Add(this.stateInitializationSendRequest);
            this.SendRequest.Activities.Add(this.GetACKEvent);
            this.SendRequest.Activities.Add(this.CloseSessionEvent);
            this.SendRequest.Activities.Add(this.GetNAKEvent);
            this.SendRequest.Name = "SendRequest";
            // 
            // InitWorkflow
            // 
            this.InitWorkflow.Activities.Add(this.stateInitializationWorkflow);
            this.InitWorkflow.Name = "InitWorkflow";
            // 
            // ReceiveData
            // 
            this.ReceiveData.Activities.Add(this.stateInitializationReceiveData);
            this.ReceiveData.Activities.Add(this.GetWrongFrameEvent);
            this.ReceiveData.Activities.Add(this.GetRightFrameEvent);
            this.ReceiveData.Name = "ReceiveData";
            // 
            // Idle
            // 
            this.Idle.Activities.Add(this.stateInitializationIdle);
            this.Idle.Activities.Add(this.GetQueryEvent);
            this.Idle.Name = "Idle";
            // 
            // ASTMWorkflow
            // 
            this.Activities.Add(this.Idle);
            this.Activities.Add(this.ReceiveData);
            this.Activities.Add(this.InitWorkflow);
            this.Activities.Add(this.SendRequest);
            this.Activities.Add(this.GetENQEvent);
            this.Activities.Add(this.GetEOTEvent);
            this.CompletedStateName = null;
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "InitWorkflow";
            this.Name = "ASTMWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private CodeActivity codeActivityInit;

        private SetStateActivity setReceiveData;

        private HandleExternalEventActivity handleGetENQ;

        private EventDrivenActivity GetENQEvent;

        private StateInitializationActivity stateInitializationReceiveData;

        private StateInitializationActivity stateInitializationIdle;

        private StateActivity ReceiveData;

        private SetStateActivity setBackReceiveData2;

        private SetStateActivity setBackReceiveData;

        private EventDrivenActivity GetRightFrameEvent;

        private EventDrivenActivity GetWrongFrameEvent;

        private HandleExternalEventActivity handleGetRightFrame;

        private HandleExternalEventActivity handleGetWrongFrame;

        private CodeActivity codeReceiveDataInittialization;

        private CodeActivity InitActivity;

        private StateInitializationActivity stateInitializationWorkflow;

        private StateActivity InitWorkflow;

        private SetStateActivity setStateIdle;

        private HandleExternalEventActivity handleGetEOT;

        private EventDrivenActivity GetEOTEvent;

        private StateActivity SendRequest;

        private StateInitializationActivity stateInitializationSendRequest;

        private CodeActivity codeInitializationSendRequest;

        private SetStateActivity SetSendRequest1;

        private HandleExternalEventActivity HandleGetACKEvent;

        private EventDrivenActivity GetACKEvent;

        private HandleExternalEventActivity HandleCloseSessionEvent;

        private EventDrivenActivity CloseSessionEvent;

        private SetStateActivity setStateSendRequestByNAK;

        private HandleExternalEventActivity HandleGetNAKEvent;

        private EventDrivenActivity GetNAKEvent;

        private CallExternalMethodActivity SendACK;

        private CallExternalMethodActivity SendACK2;

        private CallExternalMethodActivity SendNAK;

        private CallExternalMethodActivity SendEOT;

        private SetStateActivity setStateSendRequest;

        private CallExternalMethodActivity SendENQ;

        private HandleExternalEventActivity HandleGetQueryEvent;

        private EventDrivenActivity GetQueryEvent;

        private SetStateActivity setStateActivity1;

        private SetStateActivity setStateActivity3;

        private StateActivity Idle;
















































































































































































































































    }
}
