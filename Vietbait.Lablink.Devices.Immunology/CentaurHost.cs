using System;
using System.Workflow.Activities;
using System.Workflow.Runtime;
using WorkflowLibrary1;

namespace Vietbait.Lablink.Devices.Immunology
{
    public class CentaurHost
    {
        //Guid InstanceId;
        //private readonly Vietbait.Lablink.Devices.Immunology.Centaur objCentaur;
        //private Rs232Communication objRS232Comm;
        //private readonly ExternalDataEventArgs objDataEventArgs;
        //WorkflowInstance objWorkFlowInstance;

        //public CentaurHost ()
        //{
        //    var objWorkFlowRuntime = new WorkflowRuntime();

        //    objCentaur = new Centaur();
        //    var objService = new ExternalDataExchangeService();

        //    InstanceId = Guid.NewGuid();
        //    objWorkFlowRuntime.AddService(objService);
        //    objService.AddService(objCentaur);
        //    objWorkFlowInstance = objWorkFlowRuntime.CreateWorkflow(typeof(ASTMWorkflow), null, InstanceId);
        //    objWorkFlowInstance.Start();
        //    Console.WriteLine(@"Work flow started");

        //    objDataEventArgs = new ExternalDataEventArgs(InstanceId);
        //    objDataEventArgs.WaitForIdle = true;
        //}
        public CentaurHost ()
        {
            WorkflowInstance objWorkFlowInstance;
            Rs232Communication cservice = null;
            var objWorkFlowRuntime = new WorkflowRuntime();


            #region Add support to the Communication Service
            //Declare a ExternalDataExchangeService class
            ExternalDataExchangeService dataservice = new ExternalDataExchangeService();
            //Add to workflow runtime
            objWorkFlowRuntime.AddService(dataservice);
            //Declare our CommunicationService instance
            cservice = new Rs232Communication();
            //Add to the ExternalDataService
            dataservice.AddService(cservice);
            //Add a handler to Service Event (retrieve Summe from Workflow)
            //cservice.ReturnSummeEvent += new EventHandler<summeeventargs>(cservice_ReturnSummeEvent);
            objWorkFlowInstance = objWorkFlowRuntime.CreateWorkflow(typeof(CentaurWorkflow));
            objWorkFlowInstance.Start();
            Console.WriteLine(@"Work flow started");
            #endregion end of support to Communication Service.....
        }
    }
}
