using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class ObjectBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static ObjectBusiness()
        {
            new ObjectBusiness();
        }

        /// <summary>
        /// Lấy về danh sách thiết bị
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllObject()
        {
            try
            {
                DataTable query = new Select().From(LObjectType.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}