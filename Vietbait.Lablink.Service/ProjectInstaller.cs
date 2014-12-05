using System.ComponentModel;
using System.Configuration.Install;

namespace Vietbait.Lablink.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}