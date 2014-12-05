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

namespace WorkflowLibrary1
{
    partial class ASTMWorkflow2
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
            this.InitReceiveDataActivity = new System.Workflow.Activities.CodeActivity();
            this.setInitWorkflow = new System.Workflow.Activities.SetStateActivity();
            this.HandleGetEOT = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStayReceiveData2 = new System.Workflow.Activities.SetStateActivity();
            this.HandleGetWrongFrame = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setStayReceiveData = new System.Workflow.Activities.SetStateActivity();
            this.HandleGetRightFrame = new System.Workflow.Activities.HandleExternalEventActivity();
            this.InitIdleActivity = new System.Workflow.Activities.CodeActivity();
            this.setReceiveData = new System.Workflow.Activities.SetStateActivity();
            this.HandleGetENQ = new System.Workflow.Activities.HandleExternalEventActivity();
            this.setIdleState = new System.Workflow.Activities.SetStateActivity();
            this.InitWorkflowActivity = new System.Workflow.Activities.CodeActivity();
            this.InitializationReceiveData = new System.Workflow.Activities.StateInitializationActivity();
            this.GetEOT = new System.Workflow.Activities.EventDrivenActivity();
            this.GetWrongFrame = new System.Workflow.Activities.EventDrivenActivity();
            this.GetRightFrame = new System.Workflow.Activities.EventDrivenActivity();
            this.InitializationIdle = new System.Workflow.Activities.StateInitializationActivity();
            this.GetENQ = new System.Workflow.Activities.EventDrivenActivity();
            this.WorkflowInit = new System.Workflow.Activities.StateInitializationActivity();
            this.ReceiveData = new System.Workflow.Activities.StateActivity();
            this.Idle = new System.Workflow.Activities.StateActivity();
            this.ASTMWorkflow2InitialState = new System.Workflow.Activities.StateActivity();
            // 
            // InitReceiveDataActivity
            // 
            this.InitReceiveDataActivity.Name = "InitReceiveDataActivity";
            this.InitReceiveDataActivity.ExecuteCode += new System.EventHandler(this.InitReceiveDataActivity_ExecuteCode);
            // 
            // setInitWorkflow
            // 
            this.setInitWorkflow.Name = "setInitWorkflow";
            this.setInitWorkflow.TargetStateName = "ASTMWorkflow2InitialState";
            // 
            // HandleGetEOT
            // 
            this.HandleGetEOT.EventName = "GetEOT";
            this.HandleGetEOT.InterfaceType = typeof(WorkflowLibrary1.InterfaceASTM);
            this.HandleGetEOT.Name = "HandleGetEOT";
            // 
            // setStayReceiveData2
            // 
            this.setStayReceiveData2.Name = "setStayReceiveData2";
            this.setStayReceiveData2.TargetStateName = "ReceiveData";
            // 
            // HandleGetWrongFrame
            // 
            this.HandleGetWrongFrame.EventName = "GetWrongFrame";
            this.HandleGetWrongFrame.InterfaceType = typeof(WorkflowLibrary1.InterfaceASTM);
            this.HandleGetWrongFrame.Name = "HandleGetWrongFrame";
            // 
            // setStayReceiveData
            // 
            this.setStayReceiveData.Name = "setStayReceiveData";
            this.setStayReceiveData.TargetStateName = "ReceiveData";
            // 
            // HandleGetRightFrame
            // 
            this.HandleGetRightFrame.EventName = "GetRightFrame";
            this.HandleGetRightFrame.InterfaceType = typeof(WorkflowLibrary1.InterfaceASTM);
            this.HandleGetRightFrame.Name = "HandleGetRightFrame";
            // 
            // InitIdleActivity
            // 
            this.InitIdleActivity.Name = "InitIdleActivity";
            this.InitIdleActivity.ExecuteCode += new System.EventHandler(this.InitIdleActivity_ExecuteCode);
            // 
            // setReceiveData
            // 
            this.setReceiveData.Name = "setReceiveData";
            this.setReceiveData.TargetStateName = "ReceiveData";
            // 
            // HandleGetENQ
            // 
            this.HandleGetENQ.EventName = "GetENQ";
            this.HandleGetENQ.InterfaceType = typeof(WorkflowLibrary1.InterfaceASTM);
            this.HandleGetENQ.Name = "HandleGetENQ";
            // 
            // setIdleState
            // 
            this.setIdleState.Name = "setIdleState";
            this.setIdleState.TargetStateName = "Idle";
            // 
            // InitWorkflowActivity
            // 
            this.InitWorkflowActivity.Name = "InitWorkflowActivity";
            this.InitWorkflowActivity.ExecuteCode += new System.EventHandler(this.InitWorkflowActivity_ExecuteCode);
            // 
            // InitializationReceiveData
            // 
            this.InitializationReceiveData.Activities.Add(this.InitReceiveDataActivity);
            this.InitializationReceiveData.Name = "InitializationReceiveData";
            // 
            // GetEOT
            // 
            this.GetEOT.Activities.Add(this.HandleGetEOT);
            this.GetEOT.Activities.Add(this.setInitWorkflow);
            this.GetEOT.Name = "GetEOT";
            // 
            // GetWrongFrame
            // 
            this.GetWrongFrame.Activities.Add(this.HandleGetWrongFrame);
            this.GetWrongFrame.Activities.Add(this.setStayReceiveData2);
            this.GetWrongFrame.Name = "GetWrongFrame";
            // 
            // GetRightFrame
            // 
            this.GetRightFrame.Activities.Add(this.HandleGetRightFrame);
            this.GetRightFrame.Activities.Add(this.setStayReceiveData);
            this.GetRightFrame.Name = "GetRightFrame";
            // 
            // InitializationIdle
            // 
            this.InitializationIdle.Activities.Add(this.InitIdleActivity);
            this.InitializationIdle.Name = "InitializationIdle";
            // 
            // GetENQ
            // 
            this.GetENQ.Activities.Add(this.HandleGetENQ);
            this.GetENQ.Activities.Add(this.setReceiveData);
            this.GetENQ.Name = "GetENQ";
            // 
            // WorkflowInit
            // 
            this.WorkflowInit.Activities.Add(this.InitWorkflowActivity);
            this.WorkflowInit.Activities.Add(this.setIdleState);
            this.WorkflowInit.Name = "WorkflowInit";
            // 
            // ReceiveData
            // 
            this.ReceiveData.Activities.Add(this.GetRightFrame);
            this.ReceiveData.Activities.Add(this.GetWrongFrame);
            this.ReceiveData.Activities.Add(this.GetEOT);
            this.ReceiveData.Activities.Add(this.InitializationReceiveData);
            this.ReceiveData.Name = "ReceiveData";
            // 
            // Idle
            // 
            this.Idle.Activities.Add(this.GetENQ);
            this.Idle.Activities.Add(this.InitializationIdle);
            this.Idle.Name = "Idle";
            // 
            // ASTMWorkflow2InitialState
            // 
            this.ASTMWorkflow2InitialState.Activities.Add(this.WorkflowInit);
            this.ASTMWorkflow2InitialState.Name = "ASTMWorkflow2InitialState";
            // 
            // ASTMWorkflow2
            // 
            this.Activities.Add(this.ASTMWorkflow2InitialState);
            this.Activities.Add(this.Idle);
            this.Activities.Add(this.ReceiveData);
            this.CompletedStateName = null;
            this.DynamicUpdateCondition = null;
            this.InitialStateName = "ASTMWorkflow2InitialState";
            this.Name = "ASTMWorkflow2";
            this.CanModifyActivities = false;

        }

        #endregion

        private EventDrivenActivity GetENQ;
        private StateInitializationActivity WorkflowInit;
        private StateActivity Idle;
        private HandleExternalEventActivity HandleGetENQ;
        private EventDrivenActivity GetRightFrame;
        private StateActivity ReceiveData;
        private SetStateActivity setReceiveData;
        private SetStateActivity setInitWorkflow;
        private HandleExternalEventActivity HandleGetEOT;
        private SetStateActivity setStayReceiveData2;
        private HandleExternalEventActivity HandleGetWrongFrame;
        private SetStateActivity setStayReceiveData;
        private HandleExternalEventActivity HandleGetRightFrame;
        private EventDrivenActivity GetEOT;
        private EventDrivenActivity GetWrongFrame;
        private SetStateActivity setIdleState;
        private CodeActivity InitIdleActivity;
        private StateInitializationActivity InitializationIdle;
        private CodeActivity InitReceiveDataActivity;
        private StateInitializationActivity InitializationReceiveData;
        private CodeActivity InitWorkflowActivity;
        private StateActivity ASTMWorkflow2InitialState;




















    }
}
