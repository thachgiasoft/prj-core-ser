using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class DepartmentBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static DepartmentBusiness()
        {
            new DepartmentBusiness();
        }

        /// <summary>
        /// Lấy về danh sách thiết bị
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDepartment()
        {
            try
            {
                DataTable query = new Select().From(LDepartment.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string InsertDepartment(LDepartment pitems)
        {
            int i = 0;
            Query _QueryRS = LDepartment.CreateQuery();
            try
            {
                if (!LDepartment.FetchByParameter(LDepartment.Columns.Id, Comparison.Equals, pitems.Id).Read())
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return _QueryRS.GetMax(LDepartment.Columns.Id).ToString();
                    //var dt =
                    //    new Select(LDepartment.Columns.Id).From(LDepartment.Schema.Name).Where(LDepartment.Columns.IPAddress).IsEqualTo
                    //        (pitems.IPAddress).ExecuteDataSet().Tables[0];
                    //return dt.Rows [0][0].ToString();
                    //.ExecuteDataSet().Tables[0].Rows[0][0].ToString();
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

        public static void DeleteDepartment(int pId)
        {
            if (LDepartment.FetchByID(pId) != null)
            {
                LDepartment.Delete(pId);
            }
        }

        public static void UpdateDepartment(LDepartment pitems)
        {
            try
            {
                if (LDepartment.FetchByID(pitems.Id) != null)
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