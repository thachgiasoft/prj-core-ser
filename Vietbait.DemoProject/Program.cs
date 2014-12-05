using System;
using System.Windows.Forms;

namespace Vietbait.DemoProject
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestDataForm());
            ////string s = System.IO.File.ReadAllText(@"C:\1.txt");
            ////int idFirstSTX = s.IndexOf(Lablink.Utilities.DeviceHelper.STX);
            ////s = s.Substring(idFirstSTX + 2);
            ////Console.WriteLine(s);
            ////int idLastCRLF = s.LastIndexOf(Lablink.Utilities.DeviceHelper.CRLF);
            ////s = s.Substring(0, idLastCRLF-3);
            ////Console.WriteLine(s);

            //////Test append text
            //System .IO.File .AppendAllText(_logFileName,"Hello1");
            //System.IO.File.AppendAllText(_logFileName, "Hello2");
            //System.IO.File.AppendAllText(_logFileName, "Hello100");
            
        }
    }
}