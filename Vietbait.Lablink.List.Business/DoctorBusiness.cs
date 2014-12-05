using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class DoctorBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static DoctorBusiness()
        {
            new DoctorBusiness();
        }

        /// <summary>
        /// Lấy về danh sách thiết bị
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDoctor()
        {
            try
            {
                DataTable query = new Select().From(LUser.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string InsertDoctor(LUser pitems)
        {
            int i = 0;
            Query _QueryRS = LUser.CreateQuery();
            try
            {
                if (!LUser.FetchByParameter(LUser.Columns.UserId, Comparison.Equals, pitems.UserId).Read())
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return _QueryRS.GetMax(LUser.Columns.UserId).ToString();
                }
                else
                {
                    return "-1";
                    //throw new Exception(string.Format("Name:{0} Đã tồn tại", pitems.IPAddress));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteDoctor(int pId)
        {
            if (LUser.FetchByID(pId) != null)
            {
                LUser.Delete(pId);
            }
        }

        public static void UpdateDoctor(LUser pitems)
        {
            try
            {
                if (LUser.FetchByID(pitems.UserId) != null)
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
    }
}