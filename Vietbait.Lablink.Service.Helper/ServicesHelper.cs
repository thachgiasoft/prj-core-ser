using System.ServiceProcess;
using System.Timers;

namespace Vietbait.Lablink.Service.Helper
{
    public partial class ServicesHelper : ServiceBase
    {
        private readonly ServiceController _myService = new ServiceController(SERVICENAME);
        private Timer _timer;

        public ServicesHelper()
        {
            InitializeComponent();
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                try
                {
                    _timer.Stop();
                    if (_myService.Status != ServiceControllerStatus.Running)
                    {
                        _myService.Start();
                        _myService.WaitForStatus(ServiceControllerStatus.Running);
                    }
                }
                catch
                {
                }
                finally
                {
                    _timer.Start();
                }
            }
            catch
            {
            }
        }

        protected override void OnStart(string[] args)
        {
            _timer = new Timer(10000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        protected override void OnStop()
        {
        }
    }
}