using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace Vietbait.Lablink.Workflow
{
    public sealed partial class WaitForACKWorkflow : SequentialWorkflowActivity
    {
        public WaitForACKWorkflow()
        {
            InitializeComponent();
        }

        private void ACKDelay_InitializeTimeoutDuration(object sender, EventArgs e)
        {
            DelayActivity delay = sender as DelayActivity;
            if (delay != null)
            {

                delay.TimeoutDuration = new TimeSpan(0, 10, 0);
            }
        }
    }

}
