using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class HaBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static HaBusiness()
        {
            new HaBusiness();
        }

        /// <summary>
        /// Lấy về danh sách các địa chỉ TCPIP
        /// 
        /// </summary>
        public static DataTable GetAllTCPIP()
        {
            try
            {
                DataTable query =
                    new Select().From(LTcpip.Schema.Name).OrderAsc(LTcpip.Columns.Id).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hàm thực hiện Insert, Delte, Update TCIP
        /// </summary>
        /// <param name="pitems"></param>
        public static string InsertTCPIP(LTcpip pitems)
        {
            int i = 0;
            Query _QueryRS = LTcpip.CreateQuery();
            try
            {
                if (!LTcpip.FetchByParameter(LTcpip.Columns.IPAddress, Comparison.Equals, pitems.IPAddress).Read())
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return _QueryRS.GetMax(LTcpip.Columns.Id).ToString();
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTcpip(int pId)
        {
            if (LTcpip.FetchByID(pId) != null)
            {
                LTcpip.Delete(pId);
            }
        }

        public static void UpdateTcpip(LTcpip pitems)
        {
            try
            {
                if (LTcpip.FetchByID(pitems.Id) != null)
                {
                    pitems.IsNew = false;
                    pitems.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool InValiTCP(LTcpip pitems)
        {
            LTcpipCollection collection =
                new LTcpipController().FetchByQuery(LTcpip.CreateQuery().AddWhere(LTcpip.Columns.Id,
                                                                                  Comparison.NotEquals, pitems.Id)
                                                        .AND(LTcpip.Columns.IPAddress, Comparison.Equals,
                                                             pitems.IPAddress));
            if (collection.Count <= 0) return true;
            else
            {
                return false;
            }
        }

        public static string UpdateTcpip1(LTcpip pitems)
        {
            int i = 0;
            string id = "-1";
            Query _QueryRS = LTcpip.CreateQuery();
            try
            {
                if (LTcpip.FetchByParameter(LTcpip.Columns.Id, Comparison.Equals, pitems.Id).Read())
                {
                    pitems.IsNew = false;
                    pitems.Save();
                    id = _QueryRS.GetMax(LTcpip.Columns.Id).ToString();
                }
            }
            catch (Exception ex)
            {
                id = "-1";
            }
            return id;
        }
    }
}