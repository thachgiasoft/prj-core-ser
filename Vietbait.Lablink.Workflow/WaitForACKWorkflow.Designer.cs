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
    partial class WaitForACKWorkflow
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
            this.ACKTimeout = new System.Workflow.Activities.CallExternalMethodActivity();
            this.ACKDelay = new System.Workflow.Activities.DelayActivity();
            this.WaitForACKEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.eventDrivenActivity2 = new System.Workflow.Activities.EventDrivenActivity();
            this.eventDrivenActivity1 = new System.Workflow.Activities.EventDrivenActivity();
            this.terminateActivity1 = new System.Workflow.ComponentModel.TerminateActivity();
            this.listenActivity1 = new System.Workflow.Activities.ListenActivity();
            // 
            // ACKTimeout
            // 
            this.ACKTimeout.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.ACKTimeout.MethodName = "ACKTimeout";
            this.ACKTimeout.Name = "ACKTimeout";
            // 
            // ACKDelay
            // 
            this.ACKDelay.Name = "ACKDelay";
            this.ACKDelay.TimeoutDuration = System.TimeSpan.Parse("00:00:10");
            this.ACKDelay.InitializeTimeoutDuration += new System.EventHandler(this.ACKDelay_InitializeTimeoutDuration);
            // 
            // WaitForACKEvent
            // 
            this.WaitForACKEvent.EventName = "GetACK";
            this.WaitForACKEvent.InterfaceType = typeof(Vietbait.Lablink.Workflow.InterfaceASTM);
            this.WaitForACKEvent.Name = "WaitForACKEvent";
            // 
            // eventDrivenActivity2
            // 
            this.eventDrivenActivity2.Activities.Add(this.ACKDelay);
            this.eventDrivenActivity2.Activities.Add(this.ACKTimeout);
            this.eventDrivenActivity2.Name = "eventDrivenActivity2";
            // 
            // eventDrivenActivity1
            // 
            this.eventDrivenActivity1.Activities.Add(this.WaitForACKEvent);
            this.eventDrivenActivity1.Name = "eventDrivenActivity1";
            // 
            // terminateActivity1
            // 
            this.terminateActivity1.Name = "terminateActivity1";
            // 
            // listenActivity1
            // 
            this.listenActivity1.Activities.Add(this.eventDrivenActivity1);
            this.listenActivity1.Activities.Add(this.eventDrivenActivity2);
            this.listenActivity1.Name = "listenActivity1";
            // 
            // WaitForACKWorkflow
            // 
            this.Activities.Add(this.listenActivity1);
            this.Activities.Add(this.terminateActivity1);
            this.Name = "WaitForACKWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private DelayActivity ACKDelay;

        private HandleExternalEventActivity WaitForACKEvent;

        private EventDrivenActivity eventDrivenActivity2;

        private EventDrivenActivity eventDrivenActivity1;

        private CallExternalMethodActivity ACKTimeout;

        private TerminateActivity terminateActivity1;

        private ListenActivity listenActivity1;













    }
}
