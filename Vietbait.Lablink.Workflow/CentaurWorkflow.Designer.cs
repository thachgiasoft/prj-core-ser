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
    partial class CentaurWorkflow
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
            System.Workflow.ComponentModel.WorkflowParameterBinding workflowparameterbinding1 = new System.Workflow.ComponentModel.WorkflowParameterBinding();
            System.Workflow.Activities.Rules.RuleConditionReference ruleconditionreference1 = new System.Workflow.Activities.Rules.RuleConditionReference();
            this.HandleGetENQEvent = new System.Workflow.Activities.HandleExternalEventActivity();
            this.faultHandlersActivity1 = new System.Workflow.ComponentModel.FaultHandlersActivity();
            this.whileActivity1 = new System.Workflow.Activities.WhileActivity();
            // 
            // HandleGetENQEvent
            // 
            this.HandleGetENQEvent.EventName = "SendENQToWF";
            this.HandleGetENQEvent.InterfaceType = typeof(Vietbait.Lablink.Devices.IRS232Communication);
            this.HandleGetENQEvent.Name = "HandleGetENQEvent";
            workflowparameterbinding1.ParameterName = "e";
            this.HandleGetENQEvent.ParameterBindings.Add(workflowparameterbinding1);
            // 
            // faultHandlersActivity1
            // 
            this.faultHandlersActivity1.Name = "faultHandlersActivity1";
            // 
            // whileActivity1
            // 
            this.whileActivity1.Activities.Add(this.HandleGetENQEvent);
            ruleconditionreference1.ConditionName = "Condition1";
            this.whileActivity1.Condition = ruleconditionreference1;
            this.whileActivity1.Name = "whileActivity1";
            // 
            // CentaurWorkflow
            // 
            this.Activities.Add(this.whileActivity1);
            this.Activities.Add(this.faultHandlersActivity1);
            this.Name = "CentaurWorkflow";
            this.CanModifyActivities = false;

        }

        #endregion

        private HandleExternalEventActivity HandleGetENQEvent;
        private FaultHandlersActivity faultHandlersActivity1;
        private WhileActivity whileActivity1;







































    }
}
