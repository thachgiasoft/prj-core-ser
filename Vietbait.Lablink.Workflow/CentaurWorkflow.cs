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
using Vietbait.Lablink.Devices;
using Vietbait.Lablink.Workflow;

namespace WorkflowLibrary1
{
    public sealed partial class CentaurWorkflow : SequentialWorkflowActivity
    {
        private string _receive = string.Empty;
        private byte _receivestatus = 0;
        public RS232EventArguments _returnedArguments = default(RS232EventArguments);
        public CentaurWorkflow()
        {
            InitializeComponent();
        }

        private void codeInitParameter_ExecuteCode(object sender, EventArgs e)
        {
            Console.WriteLine("Init parameter!");
        }

        private void SetResponseInVariables(object sender, ExternalDataEventArgs e)
        {
            _receive = _returnedArguments.Response;
            _receivestatus = _returnedArguments.ReceiveStatus;
        }

    }

}
