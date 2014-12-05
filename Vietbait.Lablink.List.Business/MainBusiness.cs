using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class MainBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static MainBusiness()
        {
            new MainBusiness();
        }

        /// <summary>
        /// Lấy về danh sách thiết bị
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDevice()
        {
            try
            {
                DataTable query = new Select().From(DDeviceList.Schema.Name).ExecuteDataSet().Tables[0];

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetAllRs232Port()
        {
            try
            {
                return new Select().From(LRS232.Schema.Name).ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // private
        public static string InsertRs232(LRS232 pItem)
        {
            Query _QueryRS = LRS232.CreateQuery();
            int i = 0;
            try
            {
                //Nếu không tồn tại thì tiến hành insert
                if (!LRS232.FetchByParameter(LRS232.Columns.Name, Comparison.Equals, pItem.Name).Read())
                {
                    pItem.IsNew = true;
                    pItem.Save(i);
                    return _QueryRS.GetMax(LRS232.Columns.Id).ToString();
                    // return "-1";
                }
                else
                {
                    return "-1";

                    // throw new Exception(string.Format("Name:{0} Đã tồn tại", pItem.Name));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public static string UpdateRs232(LRS232 pitem)
        {
            int i = 0;
            string id = "-1";
            Query _QueryRS = LRS232.CreateQuery();
            try
            {
                //Kiểm tra xem nếu tồn tại thì Update
                // if (LRS232.FetchByID(pitem.Id) != null)
                if (LRS232.FetchByParameter(LRS232.Columns.Id, Comparison.Equals, pitem.Id).Read())
                {
                    pitem.IsNew = false;
                    pitem.Save();
                    id = _QueryRS.GetMax(LRS232.Columns.Id).ToString();
                }
            }
            catch (Exception ex)
            {
                id = "-1";
            }
            return id;
        }

        public static bool InValiRs232(LRS232 pitems)
        {
            LRS232Collection collection =
                new LRS232Controller().FetchByQuery(LRS232.CreateQuery().AddWhere(LRS232.Columns.Id,
                                                                                  Comparison.NotEquals, pitems.Id)
                                                        .AND(LRS232.Columns.Name, Comparison.Equals,
                                                             pitems.Name));
            if (collection.Count <= 0) return true;
            else
            {
                return false;
            }
        }

        public static void DeleteRs232(int pId)
        {
            if (LRS232.FetchByID(pId) != null)
            {
                LRS232.Delete(pId);
            }
        }
    }
}