using System;
using System.Workflow.Activities;

namespace Vietbait.Lablink.Workflow
{
    public sealed partial class ASTMWorkflow : StateMachineWorkflowActivity
    {
        public ASTMWorkflow()
        {
            InitializeComponent();
        }

        private void codeActivityInit_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"Init Idle State!");
        }

        private void codeReceiveDataInittialization_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"Init Get Data State!");
        }

        private void InitActivity_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"Init Workflow!");
            //            WorkflowTimeout.TimeoutDuration = TimeSpan.Parse(@"00:00:00");
        }
        //private void InitTimeout_ExecuteCode(object sender, EventArgs e)
        //{
        //    //WorkflowTimeout.TimeoutDuration = TimeSpan.Parse(@"00:30:00");

        //}
        //private void TerminateTimeout_ExecuteCode(object sender, EventArgs e)
        //{
        //    //WorkflowTimeout.TimeoutDuration = TimeSpan.Parse(@"00:00:00");
        //}
        private void codeInitializationSendRequest_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine(@"Init Send Request State!");
        }
    }
}