using System;
using System.Windows.Forms;

namespace Vietbait.Lablink.List.UI
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
                //Application.Run(new frmDeviceList());
                //Application.Run(new FrmGeneralListV2());
            }
            catch (Exception ex)
            {
                throw;
            }
            //Application.Run(new frmDeviceList());
        }
    }
}