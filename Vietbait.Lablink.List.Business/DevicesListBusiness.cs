using System;
using System.Data;
using System.Transactions;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class DevicesListBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static DevicesListBusiness()
        {
            new DevicesListBusiness();
        }

        /// <summary>
        /// Hàm lấy về danh sách các thiết bị
        /// </summary>
        /// <returns>Đối tượng trả về là một datatable</returns>
        public static DataTable GetAllDevicesList()
        {
            try
            {
                StoredProcedure std = SPs.SpGetDeviceListVer2();
                DataTable query = std.GetDataSet().Tables[0];
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Hàm lấy về danh sách tên và id của thiết bị
        /// </summary>
        /// <returns>Đối tượng trả về là một datatable</returns>
        public static DataTable GetDeviceNameAndIdList()
        {
            try
            {
                DataTable query =
                    new Select(DDeviceList.DeviceIdColumn, DDeviceList.DeviceNameColumn).From(DDeviceList.Schema.Name).
                        ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTestType(string testtypeid)
        {
            DataTable query;
            if (testtypeid == "-1")
            {
                query =
                    new Select(TTestTypeList.TestTypeIdColumn, TTestTypeList.TestTypeNameColumn).From(
                        TTestTypeList.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            else
            {
                query =
                    new Select(TTestTypeList.TestTypeIdColumn, TTestTypeList.TestTypeNameColumn).From(
                        TTestTypeList.Schema.Name).Where(TTestTypeList.TestTypeIdColumn).IsEqualTo(testtypeid).
                        ExecuteDataSet().Tables[0];
                return query;
            }
        }

        /// <summary>
        /// Hàm lấy về danh sách tên và id của thiết bị theo TestTypeId
        /// </summary>
        /// <param name="testtypeid">Đối số truyền vào là một string</param>
        /// <returns>Đối tượng trả về là một datatable</returns>
        public static DataTable GetDeviceNameAndIdListByTestTypeId(string testtypeid)
        {
            try
            {
                DataTable query;
                if (testtypeid == "-1")
                {
                    query =
                        new Select(DDeviceList.DeviceIdColumn, DDeviceList.DeviceNameColumn,
                                   DDeviceList.TestTypeIdColumn).From(DDeviceList.Schema.Name).ExecuteDataSet().Tables[0
                            ];
                    return query;
                }
                else
                {
                    query =
                        new Select(DDeviceList.DeviceIdColumn, DDeviceList.DeviceNameColumn,
                                   DDeviceList.TestTypeIdColumn).From(DDeviceList.Schema.Name).
                            Where(DDeviceList.Columns.TestTypeId).IsEqualTo(testtypeid)
                            .ExecuteDataSet().Tables[0];
                    return query;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Thêm mới thiết bị
        /// </summary>
        /// <param name="pItem"></param>
        public static string InsertDevice(DDeviceList pItem)
        {
            try
            {
                Query queryRs = DDeviceList.CreateQuery();
                using (var sh = new SharedDbConnectionScope())
                {
                    using (var scope = new TransactionScope())
                    {
                        pItem.IsNew = true;
                        pItem.Save();
                        return queryRs.GetMax(DDeviceList.Columns.DeviceId).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateDevice(DDeviceList pItem)
        {
            try
            {
                //Kiểm tra xem nếu tồn tại thì Update
                if (DDeviceList.FetchByID(pItem.DeviceId) != null)
                {
                    pItem.IsNew = false;
                    pItem.Save();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteDevice(int pId)
        {
            try
            {
                if (DDeviceList.FetchByID(pId) != null)
                {
                    new Delete().From(DDeviceList.Schema)
                        .Where(DDeviceList.Columns.DeviceId).IsEqualTo(pId)
                        .Execute();
                    DDataControl.Delete(DDataControl.Columns.DeviceId, pId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}