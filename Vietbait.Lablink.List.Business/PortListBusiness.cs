using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;

namespace Vietbait.Lablink.List.Business
{
    public class PortListBusiness
    {
        //Khởi tạo contructor

        public static DataTable GetAllRS232()
        {
            try
            {
                DataTable query =
                    new Select().From(LRS232.Schema.Name).OrderAsc(LRS232.Columns.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetRs232NameAndIdList()
        {
            try
            {
                DataTable query =
                    new Select(LRS232.IdColumn, LRS232.NameColumn).From(LRS232.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTcpipNameAndIdList()
        {
            try
            {
                DataTable query =
                    new Select(LTcpip.IdColumn, LTcpip.IPAddressColumn).From(LTcpip.Schema.Name).ExecuteDataSet().Tables
                        [0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetAllTcpip()
        {
            try
            {
                DataTable query =
                    new Select().From(LTcpip.Schema.Name).OrderAsc(LTcpip.Columns.IPAddress).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}