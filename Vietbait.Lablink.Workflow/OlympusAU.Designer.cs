using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
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
    partial class OlympusAU
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
            System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding1 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding2 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding3 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding4 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding5 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.Activities.CodeCondition codecondition1 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.Activities.CodeCondition codecondition2 = new System.Workflow.Activities.CodeCondition();
            System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding6 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            this.throwActivity1 = new System.Workflow.ComponentModel.ThrowActivity();
            this.codeActivity1 = new System.Workflow.Activities.CodeActivity();
            this.SendAUResultDataToHost = new System.Workflow.Activities.CallExternalMethodActivity();
            this.DataParse = new System.Workflow.Activities.CodeActivity();
            this.SendAURequestToHost = new System.Workflow.Activities.CallExternalMethodActivity();
            this.RequestParse = new System.Workflow.Activities.CodeActivity();
            this.GetRequestStart = new System.Workflow.Activities.HandleExternalEventActivity();
            this.codeActivity2 = new System.Workflow.Activities.CodeActivity();
            this.faultHandlerActivity1 = new System.Workflow.ComponentModel.FaultHandlerActivity();
            this.IfResultData = new System.Workflow.Activities.IfElseBranchActivity();
            this.IfQueryRequest = new System.Workflow.Activities.IfElseBranchActivity();
            this.cancellationHandlerActivity1 = new System.Workflow.ComponentModel.CancellationHandlerActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.ifElseActivity1 = new System.Workflow.Activities.IfElseActivity();
            this.SendACK = new System.Workflow.Activities.CallExternalMethodActivity();
            this.GetDataEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            activitybind1.Name = "faultHandlerActivity1";
            activitybind1.Path = "Fault";
            // 
            // throwActivity1
            // 
            this.throwActivity1.Name = "throwActivity1";
            this.throwActivity1.SetBinding(System.Workflow.ComponentModel.ThrowActivity.FaultProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
            // 
            // codeActivity1
            // 
            this.codeActivity1.Name = "codeActivity1";
            this.codeActivity1.ExecuteCode += new System.EventHandler(this.codeActivity1_ExecuteCode);
            // 
            // SendAUResultDataToHost
            // 
            this.SendAUResultDataToHost.InterfaceType = typeof(Vietbait.Lablink.Workflow.IOlympusAUComm);
            this.SendAUResultDataToHost.MethodName = "SendAUDataToHost";
            this.SendAUResultDataToHost.Name = "SendAUResultDataToHost";
            activitybind2.Name = "OlympusAU";
            activitybind2.Path = "sResults";
            workflowparameterbinding1.ParameterName = "result";
            workflowparameterbinding1.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
            this.SendAUResultDataToHost.ParameterBindings.Add(workflowparameterbinding1);
            // 
            // DataParse
            // 
            this.DataParse.Name = "DataParse";
            this.DataParse.ExecuteCode += new System.EventHandler(this.ParseData);
            // 
            // SendAURequestToHost
            // 
            this.SendAURequestToHost.InterfaceType = typeof(Vietbait.Lablink.Workflow.IOlympusAUComm);
            this.SendAURequestToHost.MethodName = "SendAURequestToHost";
            this.SendAURequestToHost.Name = "SendAURequestToHost";
            activitybind3.Name = "OlympusAU";
            activitybind3.Path = "_returnBarcode";
            workflowparameterbinding2.ParameterName = "Barcode";
            workflowparameterbinding2.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
            activitybind4.Name = "OlympusAU";
            activitybind4.Path = "_posNo";
            workflowparameterbinding3.ParameterName = "PosNo";
            workflowparameterbinding3.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
            activitybind5.Name = "OlympusAU";
            activitybind5.Path = "_rackNo";
            workflowparameterbinding4.ParameterName = "RackNo";
            workflowparameterbinding4.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
            activitybind6.Name = "OlympusAU";
            activitybind6.Path = "_sampleNo";
            workflowparameterbinding5.ParameterName = "SampleNo";
            workflowparameterbinding5.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
            this.SendAURequestToHost.ParameterBindings.Add(workflowparameterbinding2);
            this.SendAURequestToHost.ParameterBindings.Add(workflowparameterbinding3);
            this.SendAURequestToHost.ParameterBindings.Add(workflowparameterbinding4);
            this.SendAURequestToHost.ParameterBindings.Add(workflowparameterbinding5);
            // 
            // RequestParse
            // 
            this.RequestParse.Name = "RequestParse";
            this.RequestParse.ExecuteCode += new System.EventHandler(this.ParseRequest);
            // 
            // GetRequestStart
            // 
            this.GetRequestStart.EventName = "GetRequestStartEvent";
            this.GetRequestStart.InterfaceType = typeof(Vietbait.Lablink.Workflow.IOlympusAUComm);
            this.GetRequestStart.Name = "GetRequestStart";
            // 
            // codeActivity2
            // 
            this.codeActivity2.Name = "codeActivity2";
            this.codeActivity2.ExecuteCode += new System.EventHandler(this.codeActivity2_ExecuteCode);
            // 
            // faultHandlerActivity1
            // 
            this.faultHandlerActivity1.Activities.Add(this.codeActivity1);
            this.faultHandlerActivity1.Activities.Add(this.throwActivity1);
            this.faultHandlerActivity1.FaultType = typeof(System.Exception);
            this.faultHandlerActivity1.Name = "faultHandlerActivity1";
            // 
            // IfResultData
            // 
            this.IfResultData.Activities.Add(this.DataParse);
            this.IfResultData.Activities.Add(this.SendAUResultDataToHost);
            codecondition1.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.TestIfResult);
            this.IfResultData.Condition = codecondition1;
            this.IfResultData.Name = "IfResultData";
            // 
            // IfQueryRequest
            // 
            this.IfQueryRequest.Activities.Add(this.GetRequestStart);
            this.IfQueryRequest.Activities.Add(this.RequestParse);
            this.IfQueryRequest.Activities.Add(this.SendAURequestToHost);
            codecondition2.Condition += new System.EventHandler<System.Workflow.Activities.ConditionalEventArgs>(this.TestIfQuery);
            this.IfQueryRequest.Condition = codecondition2;
            this.IfQueryRequest.Name = "IfQueryRequest";
            // 
            // cancellationHandlerActivity1
            // 
            this.cancellationHandlerActivity1.Activities.Add(this.codeActivity2);
            this.cancellationHandlerActivity1.Name = "cancellationHandlerActivity1";
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Activities.Add(this.faultHandlerActivity1);
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // ifElseActivity1
            // 
            this.ifElseActivity1.Activities.Add(this.IfQueryRequest);
            this.ifElseActivity1.Activities.Add(this.IfResultData);
            this.ifElseActivity1.Name = "ifElseActivity1";
            // 
            // SendACK
            // 
            this.SendACK.InterfaceType = typeof(Vietbait.Lablink.Workflow.IOlympusAUComm);
            this.SendACK.MethodName = "SendACK";
            this.SendACK.Name = "SendACK";
            // 
            // GetDataEvent
            // 
            this.GetDataEvent.EventName = "SendAU480DataToWF";
            this.GetDataEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.IOlympusAUComm);
            this.GetDataEvent.Name = "GetDataEvent";
            activitybind7.Name = "OlympusAU";
            activitybind7.Path = "_returnedArguments";
            workflowparameterbinding6.ParameterName = "e";
            workflowparameterbinding6.SetBinding(System.Workflow.ComponentModel.WorkflowParameterBinding.ValueProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
            this.GetDataEvent.ParameterBindings.Add(workflowparameterbinding6);
            this.GetDataEvent.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.SetResponseInVariables);
            // 
            // OlympusAU
            // 
            this.Activities.Add(this.GetDataEvent);
            this.Activities.Add(this.SendACK);
            this.Activities.Add(this.ifElseActivity1);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Activities.Add(this.cancellationHandlerActivity1);
            this.Name = "OlympusAU";
            this.CanModifyActivities = false;

        }

        #endregion

        private HandleExternalEventActivity GetRequestStart;
        private ThrowActivity throwActivity1;
        private CodeActivity codeActivity2;
        private CancellationHandlerActivity cancellationHandlerActivity1;
        private CodeActivity codeActivity1;
        private FaultHandlerActivity faultHandlerActivity1;
        private FaultHandlersActivity faultHandlersActivity1;
        private CodeActivity RequestParse;
        private CallExternalMethodActivity SendAURequestToHost;
        private CallExternalMethodActivity SendAUResultDataToHost;
        private CodeActivity DataParse;
        private CallExternalMethodActivity SendACK;
        private IfElseBranchActivity IfResultData;
        private IfElseBranchActivity IfQueryRequest;
        private IfElseActivity ifElseActivity1;
        private HandleExternalEventActivity GetDataEvent;





















































































    }
}
