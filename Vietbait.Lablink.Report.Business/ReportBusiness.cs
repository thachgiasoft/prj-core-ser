using System;
using System.Data;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.Lablink.Report.Business
{
    public class ReportBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo lớp
        /// Sửa lại conneciton của Subsonic
        /// </summary>
        static ReportBusiness()
        {
            new ReportBusiness();
        }

        public static void CreateManagementUnit()
        {
            SqlQuery q = new Select().From(TblManagementUnit.Schema);
            var objManagementUnit = q.ExecuteSingle<TblManagementUnit>();
            if (objManagementUnit != null)
            {
                globalVariables.ParentBranch_Name = objManagementUnit.SParentBranchName;
                globalVariables.Branch_Name = objManagementUnit.SName;
                globalVariables.Branch_Phone = objManagementUnit.SPhone;
                globalVariables.Branch_Address = objManagementUnit.SAddress;
            }
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
                    new Select("1 as Checked", TTestTypeList.Columns.TestTypeId, TTestTypeList.Columns.TestTypeName).
                        From(
                            TTestTypeList.Schema.Name).ExecuteDataSet().Tables[0];
                //query.Columns.Add("Checked", typeof(bool), "true");
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public static DataTable GetAllObjectList()
        {
            try
            {
                DataTable query =
                    new Select("1 as Checked, *").From(LObjectType.Schema.Name).ExecuteDataSet().Tables[0];
                //query.Columns.Add("Checked", typeof(Int32) );
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable GetAllDoctorVnio()
        {
            try
            {
                DataTable query =
                    new Select("DISTINCT 1 as Checked ,bacSyDieuTri,idBacSyDieuTri").From(TblHisLisPatientInfoVnio.Schema.Name).ExecuteDataSet().Tables[0];
                //query.Columns.Add("Checked", typeof(Int32) );
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable GetAllDoctor()
        {
            try
            {
                DataTable query =
                    new Select("1 as Checked, *").From(LUser.Schema.Name).ExecuteDataSet().Tables[0];
                //query.Columns.Add("Checked", typeof(Int32)); 
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetAllDepartment()
        {
            try
            {
                DataTable query =
                    new Select("1 as Checked, *").From(LDepartment.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable GetAllDepartmentVnio()
        {
            try
            {
                DataTable query =
                    new Select("distinct 1 as Checked, idkhoa, khoa").From(TblHisLisPatientInfoVnio.Schema.Name).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataSet GetAllDepartmentDoctorVnio()
        {
            try
            {
                return SPs.SpGetAllDoctoranDepartment().GetDataSet();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static DataTable GetAllDailyTestTypeClear(DateTime pTestDateFrom, DateTime pTestDateTo,
                                                         string pTestType_ID,
                                                         string pObjecttype_id)
        {
            var dataTable = new DataTable();
            dataTable =
                SPs.SpDailyTestReportDetailV3(pTestDateFrom, pTestDateTo, pTestType_ID, pObjecttype_id).GetDataSet()
                    .Tables[0];
            ;

            return dataTable;
        }

        public static DataTable GetAllDailyTestType(DateTime pTestDateFrom, DateTime pTestDateTo, string pTestType_ID,
                                                    string pObjecttype_id, string pDoctor)
        {
            var dataTable = new DataTable();
            dataTable =
                SPs.SpDailyTestReportNbV2(pTestDateFrom, pTestDateTo, pTestType_ID, pObjecttype_id, pDoctor).GetDataSet()
                    .Tables[0];
         

            return dataTable;
        }

        public static DataTable GetAllDailyTestTypeV2(string pTestDateFrom, string pTestDateTo, string pTestType_ID,
                                                      string pObjecttype_id, int pDoctor, string pid)
        {
            var dataTable = new DataTable();
            dataTable =
                SPs.SpDailyTestReportParamDetailKqVdV2(pTestDateFrom, pTestDateTo, pTestType_ID, pObjecttype_id, pDoctor,
                                                       pid).GetDataSet()
                    .Tables[0];
            ;

            return dataTable;
        }

        //public static DataTable GetAllDailyTestPatient(string pTestDateFrom, string pTestDateTo,string pTestTypeId,string pObjectid,int pDepartment)
        //{
        //    var datatable = new DataTable();
        //    datatable =
        //        SPs.SpDailyTestReportParamDetailKqVdV3(pTestDateFrom, pTestDateTo, pTestTypeId, pObjectid, pDepartment).GetDataSet().Tables[0];
        //    return datatable;
        //}
        /// <summary>
        /// ham thực hiện thông tin của phần lấy thông tin của báo cáo số lwownjgg theo hàng ngày
        /// </summary>
        /// <param name="pTestDateFrom">thông tin từ ngày tới ngày</param>
        /// <param name="pTestDateTo">ngày kết thúc</param>
        /// <param name="pTestType_ID">mảng xét nghiệm</param>
        /// <param name="pObjecttype_id">mảng đối tượng</param>
        /// <param name="pDoctor">mảng bác sỹ</param>
        /// <returns>thông tin trả về một datatable</returns>
        public static DataTable GetDataForCountingEachTest(DateTime pTestDateFrom, DateTime pTestDateTo,
                                                           string pTestType_ID, string pObjecttype_id, string pDoctor)
        {
            var dataTable = new DataTable();
            dataTable =
                SPs.SpDailyTestReportDetailV2(pTestDateFrom, pTestDateTo, pTestType_ID, pObjecttype_id, pDoctor).
                    GetDataSet().Tables[0];
            return dataTable;
        }

        public static DataTable GetDataCreenTest(DateTime FromDate, DateTime ToDate)
        {
            var dataTable = new DataTable();
            dataTable = SPs.SpCreenTestReport(FromDate, ToDate).GetDataSet().Tables[0];
            return dataTable;
        }
        //public static DataTable GetDataReportVm(string fromDate, string toDate, string testTypeId, string objectType)
        //{
        //    try
        //    {
        //        var ReportALl = new DataTable();
        //        ReportALl = SPs.SpReportAllVNIOV2(fromDate, toDate, testTypeId, objectType).GetDataSet().Tables[0];
        //        return ReportALl;
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}

        public static DataTable ReportALlVnio(DateTime pTestDateFrom, DateTime pTestDateTo,
                                                         int noitru, string  dichvu, string pTestType_ID, string reportType)
        {
           
            try
            {
                var dsReportVnio = SPs.VienmatBaocaoXnTonghop(pTestDateFrom, pTestDateTo, noitru, dichvu, pTestType_ID, reportType).GetDataSet().Tables[0];
                return dsReportVnio;
            }
            catch (Exception ex)
            {
                throw;

            }

        }

        //public static DataTable BaocaoLuuKq(DateTime pTestDateFrom, DateTime pTestDateTo, int pTestType_ID, short objectype, int departmentId)
        //{

        //    try
        //    {
        //        var dsReportVnio = new DataTable();
        //        dsReportVnio =
        //            SPs.SpDailyTestReportParamDetailKqVnio(pTestDateFrom, pTestDateTo, pTestType_ID, objectype, departmentId).GetDataSet().Tables[0];
        //        return dsReportVnio;
        //    }
        //    catch (Exception)
        //    {
        //        throw;

        //    }

        //}

        public static DataTable GetDataReportDailyDepartMentVnio(DateTime fromDate, DateTime toDate, string department,string testTypeId)
        {
            try
            {
                var reportALldepartment = new DataTable();
                reportALldepartment = SPs.SpReportDailyBNVNIO(fromDate, toDate, department, testTypeId).GetDataSet().Tables[0];
                return reportALldepartment;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}