using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Vietbait.DemoProject
{
    public partial class FrmRs232ServerMonitor : Form
    {
        private readonly Dictionary<string, object> _colDevices;
        private readonly Dictionary<string, string> _colDevicesName;
        private string _baseDirectory;
        private Rs232ServerImpl _rs232Impl;
        


        public FrmRs232ServerMonitor()
        {
            InitializeComponent();
        }
        private void TempAddComPortForRs232Server()
        {
            string comDirectory = string.Format("{0}{1}COM", _baseDirectory, "");
            if (!Directory.Exists(comDirectory))
                Directory.CreateDirectory(comDirectory);
            //Đọc từng file text trong thư mục
            foreach (string file in Directory.GetFiles(comDirectory))
            {
                try
                {
                    string[] tempString = File.ReadAllLines(file);
                    string portName = tempString[0];
                    int baudRate = Convert.ToInt32(tempString[1]);
                    int dataBits = Convert.ToInt32(tempString[2]);
                    var stopBits = (StopBits)Enum.Parse(typeof(StopBits), tempString[3]);
                    var parity = (Parity)Enum.Parse(typeof(Parity), tempString[4]);
                    _rs232Impl.AddPort(portName, baudRate, dataBits, stopBits, parity, Handshake.None);
                    //_lablinkRs232.AddPort(portName, baudRate, 8, StopBits.One, Parity.None, Handshake.None);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private void FrmRs232ServerMonitor_Load(object sender, EventArgs e)
        {
            textBox1.Text = string.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd_H-m-s"));
            _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            Directory.SetCurrentDirectory(_baseDirectory);
            _rs232Impl = new Rs232ServerImpl(_colDevices, _colDevicesName, textBox1.Text);
            TempAddComPortForRs232Server();
            button1.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _rs232Impl.StartServer();
            }
            catch (Exception ex)
            {
                System .IO .File .WriteAllText("Error.txt",ex.ToString());
            }
        }
    }
}
