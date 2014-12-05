using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Linq;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Vietbait.Lablink.Workflow
{
    public sealed partial class AUMachineWorkflow : StateMachineWorkflowActivity
    {
        public AUMachineWorkflow()
        {
            InitializeComponent();
        }

        private void codeInitWorkflowActivity_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"AU Machine Workflow Init!");
        }
    }
}
