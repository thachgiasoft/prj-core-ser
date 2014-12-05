using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.Threading;
using NLog;
using NLog.Config;
using NLog.Targets;
using Vietbait.Lablink.Devices;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.DevicesDebug
{
    internal class Program
    {
        #region Attributies

        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly Dictionary<string, object> ColDevices = new Dictionary<string, object>();
        private static readonly Dictionary<string, string> ColDevicesName = new Dictionary<string, string>();


        private static LablinkRs232 _lablinkRs232;
        private static LablinkTcpIp _lablinkTcpIp;

        #endregion

        private static void TempAddComPortForRs232Server()
        {
            string comDirectory = string.Format("{0}{1}COM", BaseDirectory, "");
            if (!Directory.Exists(comDirectory))
                Directory.CreateDirectory(comDirectory);
            //Đọc từng file text trong thư mục
            foreach (string file in Directory.GetFiles(comDirectory))
            {
                try
                {
                    string[] tempString = File.ReadAllLines(file);

                    string portName;


                    try
                    {
                        portName = tempString[0];
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    int baudRate;
                    try
                    {
                        baudRate = Convert.ToInt32(tempString[1]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load Baudrate: BaudRate = 9600");
                        baudRate = 9600;
                    }

                    int dataBits;
                    try
                    {
                        dataBits = Convert.ToInt32(tempString[2]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load DataBits: DataBits = 8");
                        dataBits = 8;
                    }

                    StopBits stopBits;
                    try
                    {
                        stopBits = (StopBits) Enum.Parse(typeof (StopBits), tempString[3]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load StopBits: DataBits = One");
                        stopBits = StopBits.One;
                    }

                    Parity parity;
                    try
                    {
                        parity = (Parity) Enum.Parse(typeof (Parity), tempString[4]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load Parity: Parity = None");
                        parity = Parity.None;
                    }

                    Handshake handshake;

                    try
                    {
                        handshake = (Handshake) Enum.Parse(typeof (Handshake), tempString[5]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load Handshake: Handshake = None");
                        handshake = Handshake.None;
                    }

                    bool rtsEnable, dtrEnable;

                    try
                    {
                        rtsEnable = Convert.ToBoolean(tempString[6]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load RTS Enable Status: RTS = false");
                        rtsEnable = false;
                    }

                    try
                    {
                        dtrEnable = Convert.ToBoolean(tempString[7]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load DTR Enable Status: DTR = false");
                        dtrEnable = false;
                    }

                    _lablinkRs232.AddPort(portName, baudRate, dataBits, stopBits, parity, handshake, rtsEnable,
                                          dtrEnable,1000);
                    Console.WriteLine("Add Port: {0} {1} {2} {3} {4} {5} {6} {7}", portName, baudRate, dataBits,
                                      stopBits,
                                      parity, handshake, rtsEnable, dtrEnable);

                    //_lablinkRs232.AddPort(portName, baudRate, 8, StopBits.One, Parity.None, Handshake.None);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Load Device from DB or from Config
        /// </summary>
        private static void LoadDevice()
        {
            string deviceDirectory = string.Format("{0}{1}Devices", BaseDirectory, "");
            if (!Directory.Exists(deviceDirectory))
                Directory.CreateDirectory(deviceDirectory);
            //Đọc từng file text trong thư mục
            foreach (string file in Directory.GetFiles(deviceDirectory))
            {
                try
                {
                    string[] tempString = File.ReadAllLines(file);
                    string deviceName = tempString[0];
                    string deviceConnector = tempString[1];
                    string devicePath = tempString[2];
                    string deviceNameSpace = tempString[3];
                    CreateDeviceInstance(deviceName, deviceConnector, devicePath, deviceNameSpace);
                }
                catch (Exception)
                {
                    Console.WriteLine(@"Co loi khi doc thong tin tu file");
                    continue;
                }
            }

            //CreateDeviceInstance("BS400", "127.0.0.1", DevicePath, DeviceNameSpace);
            //CreateDeviceInstance("NihonKoden", "COM6", DevicePath, DeviceNameSpace);
        }

        /// <summary>
        /// Khởi tạo thiết  bị
        /// </summary>
        /// <param name="deviceName">Tên thiết bị</param>
        /// <param name="deviceConnector">IP của thiết bị hay tên cổng Com trên máy chủ mà thiết bị kết nối đến</param>
        /// <param name="devicesPath">Đường dẫn đến file DLL chứa code điều khiển thiết bị</param>
        /// <param name="deviceClassName">NameSpace của thiết bị</param>
        private static void CreateDeviceInstance(string deviceName, string deviceConnector, string devicesPath,
                                                 string deviceClassName)
        {
            try
            {
                //if (!deviceClassName.EndsWith("."))
                //{
                //    deviceClassName = string.Format("{0}.", deviceClassName);
                //}

                if (!File.Exists(devicesPath))
                {
                    Console.WriteLine(@"File {0} Khong ton tai", devicesPath);
                    return;
                }

                Assembly asm = Assembly.LoadFrom(devicesPath);
                Console.WriteLine(@"Load Devece thanh cong ""{0} """, devicesPath);
                object obj = asm.CreateInstance(deviceClassName);
                if (obj != null)
                {
                    ColDevicesName.Add(deviceConnector, deviceName);
                    ColDevices.Add(ColDevicesName[deviceConnector], obj);
                    var dv = (BaseDevice)obj;
                    dv.DeviceName = deviceName;
                    dv.Log = LogManager.GetLogger(deviceName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        

        private static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);
            fileTarget.FileName =
                "${basedir}/MyLog/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log";
            fileTarget.Layout = "${date:format=HH\\:MM\\:ss}|${level}|${logger}|${stacktrace}|${message}";
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
            LogManager.Configuration = config;
            try
            {
                //Loại bỏ ký tự :Test
                //string testName = "21AaiZz";
                //for (byte i = 65; i <=90; i++)
                //{
                //    testName = testName.Replace(Convert.ToChar(i).ToString(), "").Replace(Convert.ToChar(i+32).ToString(), "");
                //}

                // Set Start Service Directory to Current Exe Directory
                Directory.SetCurrentDirectory(BaseDirectory);

                //Get Parameter for Service
                int port, timerinterval, delaytime;
                try
                {
                    port = Convert.ToInt32(LablinkServiceConfig.GetPort());
                    timerinterval = Convert.ToInt32(LablinkServiceConfig.GetTimerInternal());
                    delaytime = Convert.ToInt32(LablinkServiceConfig.GetDelayTime());
                }
                catch (Exception)
                {
                    port = 0;
                    timerinterval = 0;
                    delaytime = 0;
                }

                //Server Declare
                _lablinkTcpIp = new LablinkTcpIp(ColDevices, ColDevicesName)
                                    {
                                        Port = port,
                                        TimerInterval = timerinterval,
                                        DelayTime = delaytime
                                    };

                Console.WriteLine(@"Port: {0} - Timer Interval: {1} - Delay Time: {2}", port, timerinterval, delaytime);

                _lablinkRs232 = new LablinkRs232(ColDevices, ColDevicesName);

                TempAddComPortForRs232Server();
                // Finish Server Init

                //Load Device from DB or from Config
                LoadDevice();


                //_lablinkTcpIp.StartServer();
                //Console.WriteLine(@"Start TCPIP Success");


                _lablinkRs232.StartServer();
                Console.WriteLine(@"Start RS232 Success");
                while (true)
                {
                    Thread.Sleep(5);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}