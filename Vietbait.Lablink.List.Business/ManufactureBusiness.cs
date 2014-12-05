using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class ManufactureBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static ManufactureBusiness()
        {
            new ManufactureBusiness();
        }

        //Lấy toàn bộ danh sách Manufacture
        public static DataTable GetAllManufacture()
        {
            try
            {
                DataTable query = new Select().From(LManufacture.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetManufacturerNameAndIdList()
        {
            try
            {
                DataTable query =
                    new Select(LManufacture.IdColumn, LManufacture.SNameColumn).From(LManufacture.Schema.Name).
                        ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Hàm Insert dữ liệu vào bảng Manufacture
        /// </summary>
        /// <param name="pitems"></param>
        /// <returns></returns>
        public static string InsertManufacture(LManufacture pitems)
        {
            int i = 0;
            Query queryRs = LManufacture.CreateQuery();
            try
            {
                if (!LManufacture.FetchByParameter(LManufacture.Columns.Id, Comparison.Equals, pitems.Id).Read())
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return queryRs.GetMax(LManufacture.Columns.Id).ToString();
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

        /// <summary>
        /// Hàm xóa dữ liệu bảng Manufacture
        /// </summary>
        /// <param name="pid"></param>
        public static void DeleteManuafacture(int pid)
        {
            if (LManufacture.FetchByID(pid) != null)
            {
                LManufacture.Delete(pid);
            }
        }

        /// <summary>
        /// Hàm update dữ liệu bảng Manufacture
        /// </summary>
        /// <param name="pitems"></param>
        public static void UpdateManuafacture(LManufacture pitems)
        {
            try
            {
                if (LManufacture.FetchByID(pitems.Id) != null)
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