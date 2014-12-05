using System;
using System.Windows.Forms;

namespace Vietbait.Lablink.Report.UI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FrmReportAll_VNIO());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}