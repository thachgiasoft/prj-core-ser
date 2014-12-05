using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace WorkflowLibrary1
{
    public sealed partial class ASTMWorkflow2 : StateMachineWorkflowActivity
    {
        public ASTMWorkflow2()
        {
            InitializeComponent();
        }

        private void InitIdleActivity_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"Init Idle State!");
        }

        private void InitReceiveDataActivity_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"Init Get Data State!");
        }

        private void InitWorkflowActivity_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"Init Workflow here!");
        }
    }
}
