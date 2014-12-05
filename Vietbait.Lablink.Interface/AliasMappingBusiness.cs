using System;
using System.Data;
using SubSonic;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Interface
{
    public class AliasMappingBusiness : CommonBusiness
    {
        static AliasMappingBusiness()
        {
            new AliasMappingBusiness();
        }

        #region Public Method

        // Lấy all alias in tblAlias
        public static DataTable GetAllAlias()
        {
            try
            {
                DataTable query =
                    new Select(TblAliasMapping.Columns.Id, TblAliasMapping.Columns.LocalAlias,
                               TblAliasMapping.Columns.IdHisXn,
                               TblAliasMapping.Columns.TestTypeId, TTestTypeList.Columns.TestTypeName).From(
                                   TTestTypeList.Schema.Name).InnerJoin(TblAliasMapping.Schema.Name,
                                                                        TblAliasMapping.Columns.TestTypeId,
                                                                        TTestTypeList.Schema.Name,
                                                                        TTestTypeList.Columns.TestTypeId).
                        ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Insert vào tblAliasMapping in db
        /// </summary>
        /// <param name="pitems"></param>
        /// <returns></returns>
        public static string InsertAliasMapping(TblAliasMapping pitems)
        {
            int i = 0;
            Query _QueryRS = TblAliasMapping.CreateQuery();
            try
            {
                if (
                    !TblAliasMapping.FetchByParameter(TblAliasMapping.Columns.LocalAlias, Comparison.Equals,
                                                      pitems.LocalAlias).Read())
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return _QueryRS.GetMax(TblAliasMapping.Columns.Id).ToString();
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
        /// Xóa tbleAliasMapping
        /// </summary>
        /// <param name="pId"></param>
        public static void DeleteAliasMapping(int pId)
        {
            if (TblAliasMapping.FetchByID(pId) != null)
            {
                TblAliasMapping.Delete(pId);
            }
        }

        /// <summary>
        /// Update tblAliasMaping           
        /// </summary>
        /// <param name="pitems"></param>
        public static void UpdateAliasMapping(TblAliasMapping pitems)
        {
            try
            {
                if (TblAliasMapping.FetchByID(pitems.Id) != null)
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

        #endregion
    }
}