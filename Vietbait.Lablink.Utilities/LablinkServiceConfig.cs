namespace Vietbait.Lablink.Utilities
{
    public static class LablinkServiceConfig
    {
        #region Attributes

        private const string StrLabLinkService = "LABLinkService";
        private const string FileName = "App.Config";
        private const string Port = "Port";
        private const string Database = "Database";
        private const string UserId = "UserId";
        private const string Password = "Password";
        private const string Server = "Server";
        private const string Timerinterval = "Timerinterval";
        private const string Delaytime = "Delaytime";
        private const string IpAdress = "IpAdress";
        private const string PortNumber = "PortNumber";
        private const string DateFormat = "DateFormat";
        private const string LogState = "LogState";
        private const string RS232Status = "RS232";
        private const string TCPIPStatus = "TCPIP";
        private const string FileStatus = "File";
        private const string UseTestTypeIdToGenerateBarcode = "UseTestTypeIdToGenerateBarcode";
        private const string UseMultiInsertResultToDB = "UseMultiInsertResultToDB";
        private const string AutoGeneratePatientAndTestOrder = "AutoGeneratePatientAndTestOrder";
        private const string AutoUpdateOrderStatusAfterSend = "AutoUpdateOrderStatusAfterSend";
        private static Config.Config ServiceConfig;

        #endregion

        #region Contructor

        static LablinkServiceConfig()
        {
            ServiceConfig = new Config.Config(FileName, StrLabLinkService);
        }

        #endregion

        #region Get Properties of Lablink Service

        public static void RefreshConfig()
        {
            ServiceConfig = new Config.Config(FileName, StrLabLinkService);
        }

        /// <summary>
        /// Lấy giá trị từ form
        /// </summary>
        /// <returns>String</returns>
        public static string GetTestTypeBarcode()
        {
            return ServiceConfig.Get(UseTestTypeIdToGenerateBarcode).ToString();
        }

        /// <summary>
        /// Gets the state of the log.
        /// </summary>
        /// <returns>String</returns>
        public static string GetLogState()
        {
            return ServiceConfig.Get(LogState).ToString();
        }

        /// <summary>
        /// Date format
        /// </summary>
        /// <returns>String</returns>
        public static string GetDateFormat()
        {
            return ServiceConfig.Get(DateFormat).ToString();
        }

        /// <summary>
        /// Tên Port
        /// </summary>
        /// <returns>Kiểu String</returns>
        public static string GetPort()
        {
            return ServiceConfig.Get(Port).ToString();
        }

        /// <summary>
        /// Tên Database
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetDatabase()
        {
            return ServiceConfig.Get(Database).ToString();
        }

        /// <summary>
        /// Mã Password
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetPassword()
        {
            return ServiceConfig.Get(Password).ToString();
        }

        /// <summary>
        /// Tên User
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetUserId()
        {
            return ServiceConfig.Get(UserId).ToString();
        }

        /// <summary>
        /// Tên Server
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetServer()
        {
            return ServiceConfig.Get(Server).ToString();
        }

        /// <summary>
        ///Timer Internal
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetTimerInternal()
        {
            return ServiceConfig.Get(Timerinterval).ToString();
        }

        /// <summary>
        /// Delay time
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetDelayTime()
        {
            return ServiceConfig.Get(Delaytime).ToString();
        }

        /// <summary>
        /// IP Adress   
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetIpAdress()
        {
            return ServiceConfig.Get(IpAdress).ToString();
        }

        /// <summary>
        /// Port Number
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetPortNumber()
        {
            return ServiceConfig.Get(PortNumber).ToString();
        }

        /// <summary>
        /// RS232 status
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetRS232Status()
        {
            return ServiceConfig.Get(RS232Status).ToString();
        }

        /// <summary>
        /// TCP/IP status 
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetTCPIPStatus()
        {
            return ServiceConfig.Get(TCPIPStatus).ToString();
        }

        /// <summary>
        /// File status
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetFileStatus()
        {
            return ServiceConfig.Get(FileStatus).ToString();
        }

        /// <summary>
        /// File status
        /// </summary>
        /// <returns>Kiểu string</returns>
        public static string GetUseMultiInsertResultToDB()
        {
            return ServiceConfig.Get(UseMultiInsertResultToDB).ToString();
        }

        public static string GetAutoGeneratePatientAndTestOrder()
        {
            return ServiceConfig.Get(AutoGeneratePatientAndTestOrder).ToString();
        }

        public static string GetAutoUpdateOrderStatusAfterSend()
        {
            return ServiceConfig.Get(AutoUpdateOrderStatusAfterSend).ToString();
        }

        #endregion

        #region Set Properties of Lablink Service

        /// <summary>
        /// Thiết lập TestTypeID để sinh Barcode hoặc không
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetTestTypeBarcode(string value)
        {
            return ServiceConfig.Set(UseTestTypeIdToGenerateBarcode, value);
        }

        /// <summary>
        /// Set the state of the log.
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>String</returns>
        public static bool SetLogState(string value)
        {
            return ServiceConfig.Set(LogState, value);
        }

        /// <summary>
        /// Set Date Format
        /// </summary>
        /// <param name="value">String</param>
        /// <returns>String</returns>
        public static bool SetDateFormat(string value)
        {
            return ServiceConfig.Set(DateFormat, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetPort(string value)
        {
            return ServiceConfig.Set(Port, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetDatabase(string value)
        {
            return ServiceConfig.Set(Database, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetServer(string value)
        {
            return ServiceConfig.Set(Server, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetPassword(string value)
        {
            return ServiceConfig.Set(Password, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetUserId(string value)
        {
            return ServiceConfig.Set(UserId, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetTimerInternal(string value)
        {
            return ServiceConfig.Set(Timerinterval, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetDelayTime(string value)
        {
            return ServiceConfig.Set(Delaytime, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetIpAdress(string value)
        {
            return ServiceConfig.Set(IpAdress, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetPortNumber(string value)
        {
            return ServiceConfig.Set(PortNumber, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetRS232Status(string value)
        {
            return ServiceConfig.Set(RS232Status, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetTCPIPStatus(string value)
        {
            return ServiceConfig.Set(TCPIPStatus, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetFileStatus(string value)
        {
            return ServiceConfig.Set(FileStatus, value);
        }

        /// <param name="value">Kiểu string</param>
        /// <returns>Kiểu boolean</returns>
        public static bool SetUseMultiInsertResultToDB(string value)
        {
            return ServiceConfig.Set(UseMultiInsertResultToDB, value);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetAutoUpdateOrderStatusAfterSend(string value)
        {
            return ServiceConfig.Set(AutoUpdateOrderStatusAfterSend, value);
        }


        #endregion

        #region Private Method

        public static void SaveConfig()
        {
            ServiceConfig.SaveConfig();
        }

        #endregion
    }
}