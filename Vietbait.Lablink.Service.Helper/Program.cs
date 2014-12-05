using System.ServiceProcess;

namespace Vietbait.Lablink.Service.Helper
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
                                {
                                    new ServicesHelper()
                                };
            ServiceBase.Run(ServicesToRun);
        }
    }
}