using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                var x = @"^0074";
                var y = (from s in x.Split('@')
                    select s.Split('^')[1]).ToList();
                Debug.WriteLine(y.Count);
            }
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            //Thread.Sleep(10000);
            try
            {
                while (!Common.CheckConnection(Common.GetDefaultConnectionString()))
                {
                    Thread.Sleep(5000);
                    LablinkServiceConfig.RefreshConfig();
                }
                //Get Directory of exe file
                var servicesToRun = new ServiceBase[] {new MainService()};
                ServiceBase.Run(servicesToRun);
            }
            catch (Exception ex)
            {
            }
        }
    }
}