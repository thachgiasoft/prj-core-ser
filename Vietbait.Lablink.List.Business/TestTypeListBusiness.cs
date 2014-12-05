using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class TestTypeListBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static TestTypeListBusiness()
        {
            new TestTypeListBusiness();
        }

        /// <summary>
        /// Hàm trả về danh sách T_Test_Type_List
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllTestTypeList()
        {
            try
            {
                DataTable query =
                    new Select().From(TTestTypeList.Schema.Name).OrderAsc(TTestTypeList.Columns.IntOrder).ExecuteDataSet
                        ().Tables[0];
                return query;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static DataTable GetTestTypeNameAndIdList()
        {
            try
            {
                DataTable query =
                    new Select(TTestTypeList.TestTypeIdColumn, TTestTypeList.TestTypeNameColumn,TTestTypeList.IntOrderColumn).From(
                        TTestTypeList.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Hàm insert Loại xét nghiệm
        /// </summary>
        /// <param name="pitems"></param>
        /// <returns></returns>
        public static string InsertTestTypeList(TTestTypeList pitems)
        {
            int i = 0;
            Query queryRs = TTestTypeList.CreateQuery();
            try
            {
                if (
                    !TTestTypeList.FetchByParameter(TTestTypeList.Columns.TestTypeId, Comparison.Equals,
                                                    pitems.TestTypeId).Read())
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return queryRs.GetMax(TTestTypeList.Columns.TestTypeId).ToString();
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
        /// Hàm Delete 1 dòng trên grid
        /// </summary>
        /// <param name="pId"></param>
        public static void DelteTestTypeList(int pId)
        {
            try
            {
                if (TTestTypeList.FetchByID(pId) != null)
                {
                    TTestTypeList.Delete(pId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hàm Update dữ liệu trên Grid
        /// </summary>
        /// <param name="pitems"></param>
        public static void UpdateTestTypeList(TTestTypeList pitems)
        {
            try
            {
                if (TTestTypeList.FetchByID(pitems.TestTypeId) != null)
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