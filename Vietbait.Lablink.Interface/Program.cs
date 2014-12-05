using System;
using System.Threading;
using System.Windows.Forms;
using Vietbait.Lablink.Interface.LAOKHOA;


namespace VietBaIT.HISLink.UI.ExternalExam
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

          
                //Application.EnableVisualStyles();
                //  log4net.Config.XmlConfigurator.Configure();
               // Application.SetCompatibleTextRenderingDefault(false);
                // Application.ThreadException += new ThreadExceptionHandler().ApplicationThreadException;
                // Application.Run(new frm_RegServices("2009-00007"));
               // Application.Run(new  frm_TiepDonBN());
                Application.Run(new frm_LAOKHOA_KETNOI());
            
           
        }

        /// <summary>
        /// Handles any thread exceptions
        /// </summary>
        public class ThreadExceptionHandler
        {
            public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
            {
              //  ILogService log = ServiceFacade.LogService;
              //  log.Fatal("Application Error", e.Exception);
                MessageBox.Show("Có lỗi hệ thống. Liên lạc với ban quản trị để thông báo.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
