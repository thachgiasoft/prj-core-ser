using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using NLog;
using NLog.Config;
using NLog.Targets;
using Vietbait.Lablink.Devices;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Service
{
    public sealed partial class MainService : ServiceBase
    {
        #region Attributies

        private static readonly string Crlf = DeviceHelper.CRLF;
        private readonly Logger Log;
        private readonly Dictionary<string, object> _colDevices = new Dictionary<string, object>();
        private readonly Dictionary<string, string> _colDevicesName = new Dictionary<string, string>();
        private readonly LablinkFileWatcherServer _lablinkFileWatcher;
        private readonly LablinkRs232 _lablinkRs232;
        private readonly LablinkTcpIp _lablinkTcpIp;
        private string _tempString = "";
        private readonly string _deviceFolder = string.Format("Devices");
        private readonly bool _isRs232Enable;
        private readonly bool _isTcpIpServerEnable;
        private readonly bool _isFileWatcherServerEnable;

        #endregion

        #region Contructor

        /// <summary>
        /// Hàm khởi tạo Service
        /// Khởi tạo Log, thiết bị, thiết lập cổng COM, và load dynamic các dll
        /// </summary>
        public MainService()
        {
            //Khởi tạo Log
            LogConfig();
            Log = LogManager.GetLogger(DeviceHelper.MainServiceLogger);
            Log.Trace(
                "\n-------------------------------------------------------------------------------------------------------------------------------");
            Log.Trace("Main Service Startup now!");

            //Xác định các thành phần sẽ được nạp
            string rs232Status = LablinkServiceConfig.GetRS232Status();
            _isRs232Enable = rs232Status.Equals("1") || rs232Status.ToUpper().Trim().Equals("TRUE");
            Log.Trace("RS232 Server is {0}", _isRs232Enable ? "Enabled!" : "Disable");

            string tcpipStatus = LablinkServiceConfig.GetTCPIPStatus();
            _isTcpIpServerEnable = tcpipStatus.Equals("1") || tcpipStatus.ToUpper().Trim().Equals("TRUE");
            Log.Trace("TCP/IP Server is {0}", _isTcpIpServerEnable ? "Enabled!" : "Disable");

            string fileWatcherService = LablinkServiceConfig.GetFileStatus();
            _isFileWatcherServerEnable = fileWatcherService.Equals("1") || fileWatcherService.ToUpper().Trim().Equals("TRUE");
            Log.Trace("File Watcher Server is {0}", _isFileWatcherServerEnable ? "Enabled!" : "Disable");

            //Khai báo Label để khởi động đến khi thành công
            label1:
            try
            {
                InitializeComponent();

                if (_isRs232Enable)
                {
                    _lablinkRs232 = new LablinkRs232(_colDevices, _colDevicesName);
                    AddComPortForRs232Server();
                    LoadDevices("COM");
                }

                if (_isTcpIpServerEnable)
                {
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
                    _lablinkTcpIp = new LablinkTcpIp(_colDevices, _colDevicesName);
                    _lablinkTcpIp.Port = port;
                    _lablinkTcpIp.TimerInterval = timerinterval;
                    _lablinkTcpIp.DelayTime = delaytime;
                    LoadDevices("TCP/IP");
                }

                if (_isFileWatcherServerEnable)
                {
                    _lablinkFileWatcher = new LablinkFileWatcherServer(_colDevices, _colDevicesName);
                    LoadFileWatcherDevice(); 
                }

                StartDebugMainServcies();
            }
            catch (Exception ex)
            {
                _tempString = string.Format("Error While Starting Service{0}Error Message:{1}", Crlf, ex);
                Log.Error(string.Format("{0}{1}{2}", _tempString, DeviceHelper.CRLF, ex));
                Thread.Sleep(5000);
                goto label1;
            }
        }

        /// <summary>
        /// Nếu chạy ở chế độ Debug thì bắt đầu khởi chạy các service
        /// </summary>
        public void StartDebugMainServcies()
        {
            if (!Debugger.IsAttached) return;

            try
            {
                if (_isTcpIpServerEnable) _lablinkTcpIp.StartServer();
            }
            catch (Exception)
            {
                throw;
            }


            try
            {
                if (_isRs232Enable) _lablinkRs232.StartServer();
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                if (_isFileWatcherServerEnable) _lablinkFileWatcher.StartServer();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Service Controller Methods

        /// <summary>
        /// Method To Start Service
        /// </summary>
        /// <param name="args">Default = null</param>
        protected override void OnStart(string[] args)
        {
            try
            {
                if (_isTcpIpServerEnable)
                {
                    _lablinkTcpIp.StartServer();
                    Log.Debug("TCPIP Service Startup Success");
                }
                if (_isRs232Enable)
                {
                    _lablinkRs232.StartServer();
                    Log.Debug("Rs232 Service Startup Success");
                }
                if (_isFileWatcherServerEnable)
                {
                    _lablinkFileWatcher.StartServer();
                    Log.Debug("File Watcher Service Startup Success");
                }
            }
            catch (Exception ex)
            {
                Log.Error("Server Service could not start - Error: \n{0}", ex.ToString());
            }
            finally
            {
                Log.Debug("Finish Onstart Service");
            }
        }

        /// <summary>
        /// Stop Service
        /// </summary>
        protected override void OnStop()
        {
            try
            {
                if (_isTcpIpServerEnable) _lablinkTcpIp.StopServer();
                if (_isRs232Enable) _lablinkRs232.StopServer();
                Log.Debug("Service Stoped !");
            }
            catch (Exception ex)
            {
                Log.Error("Error while stop main service Error string: {0}", ex.ToString());
                throw ex;
            }
            finally
            {
                Dispose(true);
            }
        }

        #endregion

        #region InitRS232 Server

        private void AddComPortForRs232Server()
        {
            Log.Debug("Load All Ports from COM Directory");
            var comDirectory = string.Format("COM");
            if (!Directory.Exists(comDirectory))
                Directory.CreateDirectory(comDirectory);
            //Đọc từng file text trong thư mục
            foreach (string file in Directory.GetFiles(comDirectory))
            {
                try
                {
                    var tempString = File.ReadAllLines(file);

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
                        Console.WriteLine(@"Error while load DataBits: DataBits = 1");
                        dataBits = 1;
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
                        Console.WriteLine("Error while load DTR Enable Status: DTR = false");
                        dtrEnable = false;
                    }

                    int timerInterval;
                    try
                    {
                        timerInterval = Convert.ToInt32(tempString[8]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(@"Error while load DTR Enable Status: DTR = false");
                        timerInterval = 1000;
                    }

                    if (_lablinkRs232.AddPort(portName, baudRate, dataBits, stopBits, parity, handshake, rtsEnable,
                                              dtrEnable, timerInterval))
                    {

                        Log.Debug("Add Port Success:PortName:{0}\tBaudRate:{1}\tDataBits:{2}\tStopBits:{3}\tParity:{4}\tHandShake:{5}\tRTS Enable:{6}\tDTR Enable:{7}",
                                 portName,  baudRate,  dataBits,  stopBits,  parity, handshake,  rtsEnable,  dtrEnable);
                    }
                    else
                    {
                        Log.Debug("Add Port Error:PortName:{0}\tBaudRate:{1}\tDataBits:{2}\tStopBits:{3}\tParity:{4}\tHandShake:{5}\tRTS Enable:{6}\tDTR Enable:{7}",
                                 portName, baudRate, dataBits, stopBits, parity, handshake, rtsEnable, dtrEnable);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Error while adding port:\n{0}", ex.ToString());
                    continue;
                }
            }
        }

        private void LoadDevices(string connectorType)
        {
            var deviceDirectory = _deviceFolder;
            if (!Directory.Exists(deviceDirectory))
                Directory.CreateDirectory(deviceDirectory);
            Log.Debug("Load All Device from '{0}'", deviceDirectory);
            //Đọc từng file text trong thư mục
            foreach (var file in Directory.GetFiles(deviceDirectory))
            {
                try
                {
                    //Ghi Log
                    Log.Debug("Open File:{0}", file);

                    var tempString = File.ReadAllLines(file);
                    var deviceName = tempString[0];
                    var deviceConnector = tempString[1];
                    var devicePath = tempString[2];
                    var deviceNameSpace = tempString[3];

                    if (connectorType == "COM")
                    {
                        if (deviceConnector.StartsWith(connectorType))
                        {
                            if (!_lablinkRs232.CheckPortAvailable(deviceConnector))
                            {
                                Log.Debug("Port: " + deviceConnector + "is not available");
                                continue;
                            }
                            CreateDeviceInstance(deviceName, deviceConnector, devicePath, deviceNameSpace);
                        }
                        
                    }
                    else
                    {
                        if(!deviceConnector.StartsWith("COM")) CreateDeviceInstance(deviceName, deviceConnector, devicePath, deviceNameSpace);
                    }
                }
                catch (Exception ex)
                {
                    //Ghi Log
                    _tempString = string.Format("Open File:{0}Error - Check File Config", file);
                    Log.Error(string.Format("{0}{1}{2}", _tempString, DeviceHelper.CRLF, ex));
                    continue;
                }
            }
        }

        #endregion

        #region File Watcher Server init

        private void LoadFileWatcherDevice()
        {
            string deviceDirectory = string.Format("Machine");
            if (!Directory.Exists(deviceDirectory))
                Directory.CreateDirectory(deviceDirectory);
            foreach (string file in Directory.GetFiles(deviceDirectory))
            {
                try
                {
                    //Ghi Log
                    _tempString = string.Format("Open File:{0}", file);
                    Log.Debug(_tempString);

                    string[] tempString = File.ReadAllLines(file);
                    string deviceName = tempString[0];
                    string deviceConnector = tempString[1];
                    string devicePath = tempString[2];
                    string deviceNameSpace = tempString[3];

                    _lablinkFileWatcher.AddWatcher(deviceConnector);

                    CreateDeviceInstance(deviceName, deviceConnector, devicePath, deviceNameSpace);
                }
                catch (Exception ex)
                {
                    //Ghi Log
                    _tempString = string.Format("Open File:{0}Error - Check File Config", file);
                    Log.Error(string.Format("{0}{1}{2}", _tempString, DeviceHelper.CRLF, ex));
                    continue;
                }
            } // Kết thúc quá trình nạp tham số.
        }

        #endregion

        #region Private Method

        /// <summary>
        /// Khởi tạo thiết  bị
        /// </summary>
        /// <param name="deviceName">Tên thiết bị</param>
        /// <param name="deviceConnector">IP của thiết bị hay tên cổng Com trên máy chủ mà thiết bị kết nối đến</param>
        /// <param name="devicesPath">Đường dẫn đến file DLL chứa code điều khiển thiết bị</param>
        /// <param name="deviceNameSpace">NameSpace của thiết bị</param>
        private void CreateDeviceInstance(string deviceName, string deviceConnector, string devicesPath,
                                          string deviceNameSpace)
        {
            try
            {
                if (!File.Exists(devicesPath))
                {
                    Log.Debug("File: " + devicesPath + " Not Found");
                    return;
                }
                Assembly asm = Assembly.LoadFrom(devicesPath);
                object obj = asm.CreateInstance(deviceNameSpace);
                if (obj != null)
                {
                    var dv = (BaseDevice) obj;

                    // Initial Device param
                    bool deviceInitialSuccess = string.IsNullOrEmpty(deviceName.Trim())
                                                    ? dv.InitialDevice()
                                                    : dv.InitialDevice(deviceName);

                    Log.Debug(deviceInitialSuccess
                                  ? "Load device configuration success from data base"
                                  : "Load device configuration fail. Devices got default value");

                    _colDevicesName.Add(deviceConnector, deviceName);
                    _colDevices.Add(_colDevicesName[deviceConnector], dv);
                    _tempString =
                        string.Format(
                            "Create Instance Success:DeviceName:{0} - Device Connector:{1} - Device Namespace:{2} - DLL File:{3}",
                            deviceName, deviceConnector, deviceNameSpace, devicesPath);
                    Log.Debug(_tempString);
                }
                else
                {
                    _tempString =
                        string.Format(
                            "Null Instance:DeviceName:{0} - Device Connector:{1} - Device Namespace:{2} - DLL File:{3}",
                            deviceName, deviceConnector, deviceNameSpace, devicesPath);
                    Log.Error(_tempString);
                }
            }
            catch (Exception ex)
            {
                _tempString =
                    string.Format(
                        "Create Instance Error for:{0}DeviceName:{1}{2}Device Connector:{3}{4}Device Namespace:{5}{6}DLL File:{7}{8}Check Database Connection{9} Error Messase: {10}",
                        Crlf, deviceName, Crlf, deviceConnector, Crlf, deviceNameSpace, Crlf, devicesPath, Crlf, Crlf,
                        ex);
                Log.Error(string.Format("{0}{1}{2}", _tempString, DeviceHelper.CRLF, ex));
            }
        }

        /// <summary>
        /// Cấu hình Log của hệ thống
        /// </summary>
        private static void LogConfig()
        {
            try
            {
                var config = new LoggingConfiguration();
                var fileTarget = new FileTarget
                                     {
                                         FileName =
                                             "${basedir}/MyLog/${date:format=yyyy}/${date:format=MM}/${date:format=dd}/${logger}.log",
                                         Layout =
                                             "${date:format=dd/MM/yyy HH\\:mm\\:ss}|${threadid}|${level}|${logger}|${stacktrace}|${message}",
                                         ArchiveAboveSize = 5242880
                                     };
                config.AddTarget("file", fileTarget);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Trace, fileTarget));
                LogManager.Configuration = config;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}