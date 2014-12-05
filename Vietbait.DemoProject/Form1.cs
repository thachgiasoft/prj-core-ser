using System;
using System.Reflection;
using System.Windows.Forms;
using Vietbait.Server.TCPIPServer;

namespace Vietbait.DemoProject
{
    public partial class Form1 : Form
    {
        private const string StrEMyDocumentsVisualStudio2005ProjectsLablinkNewV =
            @"E:\MyDocuments\Visual Studio 2005\Projects\Lablink_New\Vietbait.Lablink\Vietbait.Lablink.Service\bin\Debug\Vietbait.Lablink.Devices.dll";

        private TcpipServerImpl _demoServer;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                ////Start Demo Server
                //_demoServer = new TcpipServerImpl {Port = 9999, TimerInterval = 100, DelayTime = 30};
                //_demoServer.StartServer();

                Assembly sampleAssembly = Assembly.LoadFrom(StrEMyDocumentsVisualStudio2005ProjectsLablinkNewV);
                // Obtain a reference to a method known to exist in assembly.
                MethodInfo method = sampleAssembly.GetTypes()[0].GetMethod("Method1");
                // Obtain a reference to the parameters collection of the MethodInfo instance.
                ParameterInfo[] Params = method.GetParameters();
                // Display information about method parameters.
                // Param = sParam1
                //   Type = System.String
                //   Position = 0
                //   Optional=False
                foreach (ParameterInfo Param in Params)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button1Click(object sender, EventArgs e)
        {
            _demoServer.StopServer();
        }
    }
}