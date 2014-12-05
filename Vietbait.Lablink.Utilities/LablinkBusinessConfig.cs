using System;

namespace Vietbait.Lablink.Utilities
{
    public static class LablinkBusinessConfig
    {
        #region Attributes

        private const string StrLabLinkBusiness = "LABLinkBusiness";
        private const string FileName = "App.Config";
        private const string ReportType = "ReportType";
        private const string ParentBranch = "ParentBranch";
        private const string Branch = "Branch";
        private const string Address = "Address";
        private const string Phone = "Phone";
        private const string EStyle = "eStyle";
        private static readonly Config.Config ServiceBusiness;

        #endregion

        #region Contructor

        static LablinkBusinessConfig()
        {
            try
            {
                ServiceBusiness = new Config.Config(FileName, StrLabLinkBusiness);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get Properties

        public static string GetReportType()
        {
            return ServiceBusiness.Get(ReportType).ToString();
        }

        public static string GetParentBranchName()
        {
            return ServiceBusiness.Get(ParentBranch).ToString();
        }

        public static string GetBranchName()
        {
            return ServiceBusiness.Get(Branch).ToString();
        }

        public static string GetAddress()
        {
            return ServiceBusiness.Get(Address).ToString();
        }

        public static string GetPhone()
        {
            return ServiceBusiness.Get(Phone).ToString();
        }
        public static string GetStyle()
        {
            return ServiceBusiness.Get(EStyle).ToString();
        }
        #endregion

        #region Set Properties

        public static bool SetReportType(string value)
        {
            return ServiceBusiness.Set(ReportType, value);
        }

        public static bool SetParentBanch(string value)
        {
            return ServiceBusiness.Set(ParentBranch, value);
        }

        public static bool SetBanch(string value)
        {
            return ServiceBusiness.Set(Branch, value);
        }

        public static bool SetAddress(string value)
        {
            return ServiceBusiness.Set(Address, value);
        }

        public static bool SetPhone(string value)
        {
            return ServiceBusiness.Set(Phone, value);
        }
        public static bool SetStyle(string value)
        {
            return ServiceBusiness.Set(EStyle, value);
        }
        #endregion

        public static void SaveConfig()
        {
            ServiceBusiness.SaveConfig();
        }
    }
}