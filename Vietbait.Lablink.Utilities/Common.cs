using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using System.Web;

namespace Vietbait.Lablink.Utilities
{
    public static class Common
    {
        //private static DateTime startdate = new DateTime(2014, 11, 20);
        private static DateTime endDate = new DateTime(2015, 04, 30);
        //private static string path = @"hadk.txt";
        //public static string temp;
        /// <summary>
        /// Hàm trả về connection string mặc định
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultConnectionString()
        {
            try
            {

                // webserver.Service1 connsql=new Service1();
                // //connsql.Url = connsql.getUrl();
                //connsql.Url = "http://192.168.1.106:88/Service1.asmx";
                // connsql.Credentials = System.Net.CredentialCache.DefaultCredentials;
                // string conn = connsql.ConntoServer(@"C:\VietBaConfigs\LABConfig");
                // return conn;
                //var server = LablinkServiceConfig.GetServer();
                //var database = LablinkServiceConfig.GetDatabase();
                //var userId = LablinkServiceConfig.GetUserId();
                //var password = LablinkServiceConfig.GetPassword();
                ///anh
                //return string.Format(@"Data Source={0}; Initial Catalog={1}; User ID={2};Password={3}", server, database, userId, password);

              
                if (DateTime.Now <= endDate)
                {
                    
                     string  conn = HongNgocITUtilities.Lis.ConnecttoServerforService();
                     return conn;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Kiem tra connection
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static bool CheckConnection(string server, string database, string user, string pass)
        {
            var conn =
                new SqlConnection("Data Source=" + server + ";Initial Catalog=" + database + ";User Id=" + user +
                                  ";Password=" + pass + "");
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool CheckConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                conn.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Kiểm tra đầu vào là số hay không
        /// </summary>
        /// <param name="inputValue">Tham số đầu vào</param>
        /// <returns>True: là số, False: không là số </returns>
        public static bool IsItNumber(string inputValue)
        {
            var isnumber = new Regex("[^0-9]");
            return !isnumber.IsMatch(inputValue);
        }
       
    }
}