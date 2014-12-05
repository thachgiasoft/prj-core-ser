using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.List.Business
{
    public class CodeToCtrlEquip : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static CodeToCtrlEquip()
        {
            new CodeToCtrlEquip();
        }

        /// <summary>
        /// Lấy về danh sách thiết bị
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllDataControl()
        {
            try
            {
                DataTable query =
                    new Select().From(DDataControl.Schema.Name).OrderDesc(DDataControl.Columns.DeviceId).ExecuteDataSet()
                        .Tables[0];
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataControlForDevice(string deviceId)
        {
            try
            {
                if (deviceId == "-1")
                {
                    DataTable query = new Select().From(DDataControl.Schema.Name).ExecuteDataSet().Tables[0];
                    return query;
                }
                else
                {
                    DataTable query =
                        new Select().From(DDataControl.Schema.Name).Where(DDataControl.DeviceIdColumn).IsEqualTo(
                            deviceId).
                            ExecuteDataSet().Tables[0];
                    return query;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string InsertDataControl(DDataControl pitems)
        {
            int i = 0;
            Query _QueryRS = DDataControl.CreateQuery();
            try
            {
                //if ((!DDataControl.FetchByParameter(DDataControl.Columns.DataName, Comparison.Equals, pitems.DataName).
                //          Read()) &&
                //    (!DDataControl.FetchByParameter(DDataControl.Columns.AliasName, Comparison.Equals, pitems.AliasName)
                //          .Read()))
                //{
                pitems.IsNew = true;
                pitems.Save(i);
                return _QueryRS.GetMax(DDataControl.Columns.DataControlId).ToString();
                //}
                //else
                //{
                //    return "-1";
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void DeleteDataControl(int pId)
        {
            try
            {
                if (DDataControl.FetchByID(pId) != null)
                {
                    DDataControl.Delete(pId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateDataControl(DDataControl pitems)
        {
            try
            {
                if (DDataControl.FetchByID(pitems.DataControlId) != null)
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