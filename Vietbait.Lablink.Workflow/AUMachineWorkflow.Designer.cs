using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Vietbait.Lablink.Workflow
{
    partial class AUMachineWorkflow
    {
        #region Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCode]
        [System.CodeDom.Compiler.GeneratedCode("", "")]
        private void InitializeComponent()
        {
            this.CanModifyActivities = true;
            this.setStateKeepSending = new System.Workflow.Activities.SetStateActivity();
            this.handleGetNAKEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStateIdle = new System.Workflow.Activities.SetStateActivity();
            this.handleGetACKEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStateSendingRequest = new System.Workflow.Activities.SetStateActivity();
            this.handleGetRequestEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStateIdleFromGetData = new System.Workflow.Activities.SetStateActivity();
            this.handleGetDataEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStateParsing = new System.Workflow.Activities.SetStateActivity();
            this.SendACK = new System.Workflow.Activities.CallExternalMethodActivity();
            this.handleGetUnparseDataEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStateActivity1 = new System.Workflow.Activities.SetStateActivity();
            this.codeInitWorkflowActivity = new System.Workflow.Activities.CodeActivity();
            this.GetNAKEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.GetACKEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.GetRequestEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.GetDataEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.stateInitializationParse = new System.Workflow.Activities.StateInitializationActivity();
            this.GetUnparseDataEvent = new System.Workflow.Activities.EventDrivenActivity();
            this.stateInitializationIdle = new System.Workflow.Activities.StateInitializationActivity();
            this.stateInitializationWorkflow = new System.Workflow.Activities.StateInitializationActivity();
            this.SendingRequest = new System.Workflow.Activities.StateActivity();
            this.ParseRawData = new System.Workflow.Activities.StateActivity();
            this.Idle = new System.Workflow.Activities.StateActivity();
            this.AUMachineWorkflowInitialState = new System.Workflow.Activities.StateActivity();
            // 
            // setStateKeepSending
            // 
            this.setStateKeepSending.Name = "setStateKeepSending";
            this.setStateKeepSending.TargetStateName = "SendingRequest";
            // 
            // handleGetNAKEvent
            // 
            this.handleGetNAKEvent.EventName = "GetNAK";
            this.handleGetNAKEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceAU);
            this.handleGetNAKEvent.Name = "handleGetNAKEvent";
            // 
            // setStateIdle
            // 
            this.setStateIdle.Name = "setStateIdle";
            this.setStateIdle.TargetStateName = "Idle";
            // 
            // handleGetACKEvent
            // 
            this.handleGetACKEvent.EventName = "GetACK";
            this.handleGetACKEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceAU);
            this.handleGetACKEvent.Name = "handleGetACKEvent";
            // 
            // setStateSendingRequest
            // 
            this.setStateSendingRequest.Name = "setStateSendingRequest";
            this.setStateSendingRequest.TargetStateName = "SendingRequest";
            // 
            // handleGetRequestEvent
            // 
            this.handleGetRequestEvent.EventName = "GetQuery";
            this.handleGetRequestEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceAU);
            this.handleGetRequestEvent.Name = "handleGetRequestEvent";
            // 
            // setStateIdleFromGetData
            // 
            this.setStateIdleFromGetData.Name = "setStateIdleFromGetData";
            this.setStateIdleFromGetData.TargetStateName = "Idle";
            // 
            // handleGetDataEvent
            // 
            this.handleGetDataEvent.EventName = "GetDataEvent";
            this.handleGetDataEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceAU);
            this.handleGetDataEvent.Name = "handleGetDataEvent";
            // 
            // setStateParsing
            // 
            this.setStateParsing.Name = "setStateParsing";
            this.setStateParsing.TargetStateName = "ParseRawData";
            // 
            // SendACK
            // 
            this.SendACK.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceAU);
            this.SendACK.MethodName = "SendACK";
            this.SendACK.Name = "SendACK";
            // 
            // handleGetUnparseDataEvent
            // 
            this.handleGetUnparseDataEvent.EventName = "GetUnparseDataEvent";
            this.handleGetUnparseDataEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceAU);
            this.handleGetUnparseDataEvent.Name = "handleGetUnparseDataEvent";
            // 
            // setStateActivity1
            // 
            this.setStateActivity1.Name = "setStateActivity1";
            this.setStateActivity1.TargetStateName = "Idle";
            // 
            // codeInitWorkflowActivity
            // 
            this.codeInitWorkflowActivity.Name = "codeInitWorkflowActivity";
            this.codeInitWorkflowActivity.ExecuteCode += new System.EventHandler(this.codeInitWorkflowActivity_ExecuteCode);
            // 
            // GetNAKEvent
            // 
            this.GetNAKEvent.Activities.Add(this.handleGetNAKEvent);
            this.GetNAKEvent.Activities.Add(this.setStateKeepSending);
            this.GetNAKEvent.Name = "GetNAKEvent";
            // 
            // GetACKEvent
            // 
            this.GetACKEvent.Activities.Add(this.handleGetACKEvent);
            this.GetACKEvent.Activities.Add(this.setStateIdle);
            this.GetACKEvent.Name = "GetACKEvent";
            // 
            // GetRequestEvent
            // 
            this.GetRequestEvent.Activities.Add(this.handleGetRequestEvent);
            this.GetRequestEvent.Activities.Add(this.setStateSendingRequest);
            this.GetRequestEvent.Name = "GetRequestEvent";
            // 
            // GetDataEvent
            // 
            this.GetDataEvent.Activities.Add(this.handleGetDataEvent);
            this.GetDataEvent.Activities.Add(this.setStateIdleFromGetData);
            this.GetDataEvent.Name = "GetDataEvent";
            // 
            // stateInitializationParse
            // 
            this.stateInitializationParse.Name = "stateInitializationParse";
            // 
            // GetUnparseDataEvent
            // 
            this.GetUnparseDataEvent.Activities.Add(this.handleGetUnparseDataEvent);
            this.GetUnparseDataEvent.Activities.Add(this.SendACK);
            this.GetUnparseDataEvent.Activities.Add(this.setStateParsing);
            this.GetUnparseDataEvent.Name = "GetUnparseDataEvent";
            // 
            // stateInitializationIdle
            // 
            this.stateInitializationIdle.Name = "stateInitializationIdle";
            // 
            // stateInitializationWorkflow
            // 
            this.stateInitializationWorkflow.Activities.Add(this.codeInitWorkflowActivity);
            this.stateInitializationWorkflow.Activities.Add(this.setStateActivity1);
            this.stateInitializationWorkflow.Name = "stateInitializationWorkflow";
            // 
            // SendingRequest
            // 
            this.SendingRequest.Activities.Add(this.GetACKEvent);
            this.SendingRequest.Activities.Add(this.GetNAKEvent);
            this.SendingRequest.Name = "SendingRequest";
            // 
            // ParseRawData
            // 
            this.ParseRawData.Activities.Add(this.stateInitializationParse);
            this.ParseRawData.Activities.Add(this.GetDataEvent);
            this.ParseRawData.Activities.Add(this.GetRequestEvent);
            this.ParseRawData.Name = "ParseRawData";
            // 
            // Idle
            // 
            this.Idle.Activities.Add(this.stateInitializationIdle);
            this.Idle.Activities.Add(this.GetUnparseDataEvent);
            this.Idle.Name = "Idle";
            // 
            // AUMachineWorkflowInitialState
            // 
            this.AUMachineWorkflowInitialState.Activities.Add(this.stateInitializationWorkflow);
            this.AUMachineWorkflowInitialState.Name = "AUMachineWorkflowInitialState";
            // 
            // AUMachineWorkflow
            // 
            this.Activities.Add(this.AUMachineWorkflowInitialState);
            this.Activities.Add(this.Idle);
            this.Activities.Add(this.ParseRawData);
            this.Activities.Add(this.SendingRequest);
            this.CompletedStateName = null;
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "AUMachineWorkflowInitialState";
            this.Name = "AUMachineWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private StateInitializationActivity stateInitializationWorkflow;

        private CodeActivity codeInitWorkflowActivity;

        private SetStateActivity setStateActivity1;

        private StateActivity Idle;

        private StateInitializationActivity stateInitializationIdle;

        private EventDrivenActivity GetUnparseDataEvent;

        private HandleExternalEventActivity handleGetUnparseDataEvent;

        private SetStateActivity setStateIdle;

        private CallExternalMethodActivity SendACK;

        private EventDrivenActivity GetACKEvent;

        private EventDrivenActivity GetRequestEvent;

        private EventDrivenActivity GetDataEvent;

        private StateInitializationActivity stateInitializationParse;

        private StateActivity SendingRequest;

        private StateActivity ParseRawData;

        private HandleExternalEventActivity handleGetACKEvent;

        private SetStateActivity setStateParsing;

        private HandleExternalEventActivity handleGetRequestEvent;

        private SetStateActivity setStateSendingRequest;

        private SetStateActivity setStateIdleFromGetData;

        private HandleExternalEventActivity handleGetDataEvent;

        private SetStateActivity setStateKeepSending;

        private HandleExternalEventActivity handleGetNAKEvent;

        private EventDrivenActivity GetNAKEvent;

        private StateActivity AUMachineWorkflowInitialState;












































    }
}
