using System;
using System.Collections.Generic;
using System.Data;
using System.Transactions;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.TestInformation.Business
{
    public class TestInfoBusiness : CommonBusiness
    {
        /// <summary>
        /// Hàm khởi tạo Subsonic
        /// </summary>
        static TestInfoBusiness()
        {
            new TestInfoBusiness();
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

        public static string GetParaName(string id)
        {
            try
            {
                object strpara =
                    new Select(TResultDetail.Columns.ParaName).From(TResultDetail.Schema.Name).Where(
                        TResultDetail.Columns.ParaName).IsEqualTo(
                            id).ExecuteScalar();
                return strpara.ToString();
            }
            catch (Exception e)
            {
                return "";
                throw e;
            }
        }

        /// <summary>
        /// Load tên các loại xét nghiệm nằm trong khoảng 2 ngày
        /// </summary>
        /// <param name="fromDate">Ngày bắt đầu</param>
        /// <param name="toDate">Ngày kết thúc</param>
        /// <returns>Data Table</returns>
        public static DataTable LoadTestType(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                StoredProcedure std = SPs.LoadTestType(fromDate, toDate);
                DataTable query = std.GetDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Trả về danh sách bệnh nhân
        /// </summary>
        /// <param name="fromDate">Ngày bắt đầu</param>
        /// <param name="toDate">Ngày kết thúc</param>
        /// <param name="testTypeIds">Danh sách các testtypeid</param>
        /// <param name="barcode">Barcode</param>
        /// <param name="pId">PID của bệnh nhân</param>
        /// <param name="patientName">Tên bệnh nhân</param>
        /// <param name="age">Tuổi</param>
        /// <param name="sex">Giới tính (3=Tìm tất cả)</param>
        /// <param name="testStatus"></param>
        /// <returns></returns>
        public static DataTable GetTestInfoGroupedBySID(DateTime fromDate, DateTime toDate, string testTypeIds,
                                                        string barcode,
                                                        string pId, string patientName, int age, int sex,
                                                        string testStatus)
        {
            try
            {
                string strFormat = "dd/MM/yyyy";
                string strFromDate = fromDate.ToString(strFormat);
                string strToDate = toDate.ToString(strFormat);

                if (barcode.Trim() == "") barcode = "NOTHING";
                if (pId.Trim() == "") pId = "NOTHING";
                if (patientName.Trim() == "") patientName = "NOTHING";

                return SPs.SpGetTestInfoGroupBySID(strFromDate, strToDate, testTypeIds, barcode,
                                                   pId, patientName, age, sex, testStatus).GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetPatientList(DateTime fromDate, DateTime toDate, string testTypeIds, string barcode,
                                               string pId, string patientName, int age, int sex, int hasTest)
        {
            try
            {
                string strFormat = "dd/MM/yyyy";
                string strFromDate = fromDate.ToString(strFormat);
                string strToDate = toDate.ToString(strFormat);

                if (barcode.Trim() == "") barcode = "NOTHING";
                if (pId.Trim() == "") pId = "NOTHING";
                if (patientName.Trim() == "") patientName = "NOTHING";

                return SPs.SpGetTestInforForPatientXNV2New(strFromDate, strToDate, testTypeIds, barcode,
                                                           pId, patientName, age, sex, hasTest).GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static DataTable GetTestTypeIdFromDateToDate(decimal? patientID, string testtypeid, DateTime? fromdate,
                                                            DateTime? todate)
        {
            try
            {
                return
                    SPs.SpGetTestTypeListFromDateToDateForPatientNew(patientID, testtypeid, fromdate, todate).GetDataSet
                        ().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTestResultByTestType(string patientId, string testTypeId)
        {
            try
            {
                if (testTypeId != "-1")
                {
                    return
                        //new Select(TResultDetail.Schema.Name + ".*," + TTestTypeList.Schema.Name + "." + TTestTypeList.Columns.TestTypeName).From(TResultDetail.Schema.Name).LeftOuterJoin(TTestTypeList.TestTypeIdColumn,TResultDetail.TestTypeIdColumn)
                        // todo: Chọn cột select
                        //new Select(TResultDetail.Columns.Barcode, TResultDetail.Columns.DataSequence,
                        //           TResultDetail.Columns.MeasureUnit, TResultDetail.Columns.NormalLevel,
                        //           TResultDetail.Columns.NormalLevelW, TResultDetail.Columns.Note,
                        //           TResultDetail.Columns.ParaName, TResultDetail.Columns.ParaStatus,
                        //           TResultDetail.Columns.PatientId, TResultDetail.Columns.PrintData,
                        //           TResultDetail.Columns.TestDate, TResultDetail.Columns.TestDetailId,
                        //           TResultDetail.Columns.TestId, TResultDetail.Columns.TestResult,
                        //           TResultDetail.Columns.TestSequence, TResultDetail.Columns.TestTypeId,
                        //           TResultDetail.Columns.UpdateNum, TTestTypeList.Columns.TestTypeName
                        //    ).From(TResultDetail.Schema.Name).LeftOuterJoin(TTestTypeList.TestTypeIdColumn,
                        //                                                    TResultDetail.TestTypeIdColumn)
                        //    .Where(TResultDetail.Columns.PatientId).IsEqualTo(
                        //        patientId).OrderAsc(TResultDetail.Schema.Name + "." + TResultDetail.Columns.TestTypeId).
                        //    OrderAsc(
                        //        TResultDetail.Columns.DataSequence).
                        //    ExecuteDataSet().Tables[0];
                        new Select(TResultDetail.Columns.TestDetailId, TResultDetail.Columns.ParaName,
                                   TResultDetail.Columns.TestResult, TResultDetail.Columns.NormalLevel,
                                   TResultDetail.Columns.NormalLevelW, TResultDetail.Columns.MeasureUnit,
                                   TResultDetail.Columns.Note, TResultDetail.Columns.TestTypeId,
                                   TTestTypeList.Columns.TestTypeName
                            ).From(TResultDetail.Schema.Name).LeftInnerJoin(TTestTypeList.TestTypeIdColumn,
                                                                            TResultDetail.TestTypeIdColumn).OrderAsc(
                                                                                TResultDetail.Schema.Name + "." +
                                                                                TResultDetail.Columns.TestTypeId).Where(
                                                                                    TResultDetail.Columns.PatientId).
                            IsEqualTo(
                                patientId).And(
                                    TResultDetail.Columns.TestTypeId).
                            IsEqualTo(testTypeId).
                            OrderAsc(
                                TResultDetail.Columns.DataSequence).
                            ExecuteDataSet().Tables[0];
                }
                else
                {
                    return
                        new Select(TResultDetail.Columns.TestDetailId, TResultDetail.Columns.ParaName,
                                   TResultDetail.Columns.TestResult, TResultDetail.Columns.NormalLevel,
                                   TResultDetail.Columns.NormalLevelW, TResultDetail.Columns.MeasureUnit,
                                   TResultDetail.Columns.Note, TResultDetail.Columns.TestTypeId,
                                   TTestTypeList.Columns.TestTypeName).From(TResultDetail.Schema.Name).LeftOuterJoin(
                                       TTestTypeList.TestTypeIdColumn, TResultDetail.TestTypeIdColumn).Where(
                                           TResultDetail.Columns.PatientId).IsEqualTo(patientId)
                            .OrderAsc(TResultDetail.Columns.DataSequence).
                            ExecuteDataSet().Tables[0];
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTestResultByTestId(string TestID,string testTypeId)
        {
            try
            {
                return
                    new Select(TResultDetail.Columns.TestDetailId, TResultDetail.Columns.ParaName,TResultDetail.Columns.TestTypeId,
                               TResultDetail.Columns.TestResult, TResultDetail.Columns.NormalLevel,
                               TResultDetail.Columns.NormalLevelW, TResultDetail.Columns.MeasureUnit,
                               TResultDetail.Columns.Note).From(TResultDetail.Schema.Name).Where(TResultDetail.Columns.TestId).IsEqualTo(TestID).And(TResultDetail.Columns.TestTypeId).IsEqualTo(testTypeId).
                        ExecuteDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTestResultByTestId(string TestID )
        {
            try
            {
                return
                    new Select(TResultDetail.Columns.TestDetailId, TResultDetail.Columns.ParaName, TResultDetail.Columns.TestTypeId,
                               TResultDetail.Columns.TestResult, TResultDetail.Columns.NormalLevel,
                               TResultDetail.Columns.NormalLevelW, TResultDetail.Columns.MeasureUnit,
                               TResultDetail.Columns.Note).From(TResultDetail.Schema.Name).Where(TResultDetail.Columns.TestId).IsEqualTo(TestID).
                        ExecuteDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTestResultByBarcode(string barcode)
        {
            try
            {
                return
                    new Select(TResultDetail.Columns.Barcode, TResultDetail.Columns.TestDetailId,TResultDetail.Columns.TestTypeId,
                               TResultDetail.Columns.ParaName,
                               TResultDetail.Columns.TestResult, TResultDetail.Columns.NormalLevel,
                               TResultDetail.Columns.NormalLevelW, TResultDetail.Columns.MeasureUnit,
                               TResultDetail.Columns.Note).From(
                                   TResultDetail.Schema.Name).Where(TResultDetail.Columns.Barcode).IsEqualTo(barcode).
                        ExecuteDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lấy về mã loại xét nghiệm của mã xét nghiệm
        /// </summary>
        /// <param name="testId"></param>
        /// <returns></returns>
        public static string GetTestTypeIdFromTestId(string testId)
        {
            try
            {
                if (testId == "-1")
                {
                    return "-1";
                }
                else
                {
                    return
                        new Select(TTestInfo.Columns.TestTypeId).From(TTestInfo.Schema.Name).Where(
                            TTestInfo.Columns.TestId)
                            .IsEqualTo(testId).ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Trả về các thiết bị có TestTypeID truyền vào
        /// </summary>
        /// <param name="testTypeId">TestTypeId</param>
        /// <returns>IDataReader: Các thiết bị</returns> 
        public static DataTable GetDevicesFromTestTypeId(string testTypeId)
        {
            try
            {
                return
                    new Select(DDeviceList.Columns.DeviceId, DDeviceList.Columns.DeviceName).From(
                        DDeviceList.Schema.Name).Where(
                            DDeviceList.Columns.TestTypeId).IsEqualTo(testTypeId).ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataControlsFromDeviceId(string deviceId, IEnumerable<string> oldResult)
        {
            try
            {
                return
                    new Select(DDataControl.Columns.NormalLevelW, DDataControl.Columns.NormalLevel,
                               DDataControl.Columns.MeasureUnit, DDataControl.Columns.DataName).
                        From(DDataControl.Schema.Name).Where(DDataControl.Columns.DeviceId).IsEqualTo(deviceId).
                        And(DDataControl.Columns.DataName).NotIn(oldResult).
                        ExecuteDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetBarcodeFromTestId(string testId)
        {
            try
            {
                string result =
                    new Select(TTestInfo.Columns.Barcode).From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.TestId)
                        .IsEqualTo(testId).ExecuteScalar().ToString();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetTestDetailId(string barcode, string paraName)
        {
            try
            {
                object result =
                    new Select(TResultDetail.Columns.TestDetailId).From(TResultDetail.Schema.Name).Where(
                        TResultDetail.Columns.Barcode)
                        .IsEqualTo(barcode.Trim()).And(TResultDetail.Columns.ParaName).IsEqualTo(paraName).ExecuteScalar
                        ();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTestResult(string testDetailId)
        {
            try
            {
                var query = new Query(TResultDetail.Schema.Name);
                query.QueryType = QueryType.Delete;
                query.WHERE(TResultDetail.Columns.TestDetailId, testDetailId);
                query.Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTestInfoV2(string Test_ID, string barcode)
        {
            try
            {
                var query = new Query(TTestInfo.Schema.Name);
                query.QueryType = QueryType.Delete;
                query.WHERE(TTestInfo.Columns.TestId, Test_ID).AND(TTestInfo.Columns.Barcode, barcode);
                query.Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteTesInfor(int test_Id)
        {
            if (TTestInfo.FetchByID(test_Id) != null)
            {
                TTestInfo.Delete(test_Id);
            }
        }

        public static void DeleteTestInfo(string Test_ID)
        {
            try
            {
                var query = new Query(TTestInfo.Schema.Name);
                query.QueryType = QueryType.Delete;
                query.WHERE(TTestInfo.Columns.TestId, Test_ID);
                query.Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataForPrint(int patientID, int TestTypeID, DateTime datetime)
        {
            try
            {
                return SPs.SpGetTestResultForPrint(patientID, TestTypeID, datetime).GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetIndividualTestResult(int patientID,string testTypeId,string testId)
        {
            try
            {
                return SPs.SpGetTestResultForPrintV2(patientID, testTypeId, testId).GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public static DataTable GetIndividualTestResultV2(int patientID, string testTypeId, string testId)
        //{
        //    try
        //    {
        //        return SPs.SpGetTestResultForPrintV3(patientID, testTypeId, testId).GetDataSet().Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //Lấy các TestTypeID
        public static DataTable GetPatientTestTypeList(string Patient_ID)
        {
            try
            {
                //DataTable query =
                //    new Select(TTestInfo.Columns.TestTypeId,TTestInfo.Columns.TestStatus).From(TTestInfo.Schema.Name).Where(
                //        TTestInfo.Columns.PatientId).IsEqualTo(Patient_ID).ExecuteDataSet().Tables[0];
                //return query;
                return SPs.SpGetPatientTestTypeList(Patient_ID).GetDataSet().Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetAllTodayTestResult()
        {
            try
            {
                return SPs.SpGetAllTodayTestResult().GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTestStatusList()
        {
            try
            {
                DataTable dt =
                    new Select().From(LTestStatus.Schema.Name).Where(LTestStatus.Columns.InUse).IsEqualTo(true).
                        ExecuteDataSet().Tables[0];

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetTestReg(int testId)
        {
            try
            {
                DataTable dt =
                    new Select("1 as CHON", TRegList.Columns.TestId, TRegList.Columns.AliasName,
                               TRegList.Columns.Barcode, TRegList.Columns.ParaName,
                               TRegList.Columns.Status, TRegList.Columns.DeviceId).From(TRegList.Schema.Name).Where(
                                   TRegList.Columns.TestId).
                        IsEqualTo(testId).ExecuteDataSet().Tables[0];
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string InsertTestInfo(TTestInfo pitems)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        if (pitems.TestDate == null) pitems.TestDate = DateTime.Now;
                        if (pitems.RequireDate == null) pitems.RequireDate = DateTime.Now;
                        if (pitems.TestStatus == null) pitems.TestStatus = 0;


                        int i = 0;
                        Query _QueryRS = TTestInfo.CreateQuery();
                        TTestInfoCollection testInfoCollection =
                            new TTestInfoController().FetchByQuery(
                                TTestInfo.CreateQuery().AddWhere(TTestInfo.Columns.PatientId, Comparison.Equals,
                                                                 pitems.PatientId));
                        if (testInfoCollection.Count > 0) pitems.ParaId = testInfoCollection[0].ParaId;
                        pitems.IsNew = true;
                        pitems.Save(i);
                        scope.Complete();
                        return _QueryRS.GetMax(TTestInfo.Columns.TestId).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertTestInfoV1(int testTypeid, int patientId,string barcode,DateTime testdate, int asigid,int dialogid)
        {
            try
            {
                return SPs.InsertTestinfor(testTypeid, patientId, barcode, testdate, asigid, dialogid).Execute();


            }
            catch (Exception)
            {
                return -1; 
                //throw;
            }
            
        }

        public static DataTable GetTestStatus()
        {
            try
            {
                return
                    new Select().From(LTestStatus.Schema.Name).Where(LTestStatus.Columns.InUse).IsEqualTo(true).
                        ExecuteDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetPatientInfo(int Patient_ID)
        {
            try
            {
                return SPs.SpGetPatientInfo(Patient_ID).GetDataSet().Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// hàm thwucj hiện lấy thông tin của test info
        /// </summary>
        /// <param name="v_PatientId"></param>
        /// <returns></returns>
        public static DataSet GetTestInfoByPatientInfoLaokhoa(int v_PatientId, string testTypeId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                DataSet ds = new DataSet();
                string strFormat = "dd/MM/yyyy";
                string strFromDate = fromDate.ToString(strFormat);
                string strToDate = toDate.ToString(strFormat);

                ds = SPs.SpGetTestInfoByPatientID(v_PatientId, testTypeId,strFromDate,strToDate).GetDataSet();
                return ds;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public static DataSet GetTestInfoByPatientInfo(int v_PatientId, string testTypeId,DateTime fromDate, DateTime toDate)
        {
            try
            {
                var ds = new DataSet();
                string strFormat = "dd/MM/yyyy";
                string strFromDate = fromDate.ToString(strFormat);
                string strToDate = toDate.ToString(strFormat);
                ds = SPs.SpGetTestInfoByPatientID(v_PatientId, testTypeId,strFromDate,strToDate).GetDataSet();
                return ds;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //public static DataSet GetTestInfoByPatientInfo(int v_PatientId, string testTypeId)
        //{
        //    try
        //    {
        //        var ds = new DataSet();
        //        string strFormat = "dd/MM/yyyy";
        //        //string strFromDate = fromDate.ToString(strFormat);
        //        //string strToDate = toDate.ToString(strFormat);
        //        ds = SPs.SpGetTestInfoByPatientIDV2(v_PatientId, testTypeId).GetDataSet();
        //        return ds;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public static DataSet GetTestInfoByPatientInfo( string testTypeId)
        //{
        //    DataSet ds = new DataSet();
        //    ds = SPs.SpGetTestInfoByPatientID(testTypeId).GetDataSet();
        //    return ds;
        //}
        public static int InsertResultDetail(TResultDetail resultDetail)
        {
            int record = -1;
            Query _Query = TResultDetail.CreateQuery();
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        resultDetail.IsNew = true;
                        resultDetail.Save();
                        record = Convert.ToInt32(_Query.GetMax(TResultDetail.Columns.TestDetailId));
                    }
                    scope.Complete();
                }
            }
            catch (Exception exception)
            {
                record = -1;
            }
            return record;
        }

        public ActionResult UpdateDataResultDetail(ref DataTable resultDetail, int v_TestInfo)
        {
            int TestResult_Id = -1;
            Query _Query = TResultDetail.CreateQuery();
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        new Update(TTestInfo.Schema)
                            .Set(TTestInfo.Columns.TestStatus).EqualTo(80)
                            .Where(TTestInfo.Columns.TestId).IsEqualTo(v_TestInfo).Execute();
                        foreach (DataRow dr in resultDetail.Rows)
                        {
                            //if (dr["CHON"].ToString() == "1")
                            //{
                            if (dr["IsNew"].ToString() == "1")
                            {
                                TResultDetail ObjResultDetail = CreateResultDetail(dr);
                                if (ObjResultDetail != null)
                                {
                                    ObjResultDetail.IsNew = true;
                                    ObjResultDetail.Save();
                                    TestResult_Id =
                                        Convert.ToInt32(_Query.GetMax(TResultDetail.Columns.TestDetailId));
                                    dr[TResultDetail.Columns.TestDetailId] = TestResult_Id;
                                }
                                //dr["IsNew"] = 0;
                            }
                            else
                            {
                                TResultDetail ObjResultDetail = CreateResultDetail(dr);
                                UpdateResultDetail(ObjResultDetail);
                                //}
                            }
                        }
                        resultDetail.AcceptChanges();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }

        public int UpdateTestStatusPrinter(int Test_Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        new Update(TTestInfo.Schema)
                            .Set(TTestInfo.Columns.Printstatus).EqualTo(1)
                            .Set(TTestInfo.Columns.TestStatus).EqualTo(90)
                            .Where(TTestInfo.Columns.TestId).IsEqualTo(Test_Id).Execute();
                    }
                    scope.ToString();
                    return 1;
                }
            }
            catch (Exception exception)
            {
                return -1;
            }
        }
       
        public ActionResult UpdateTestInfor(ref DataTable testInfoDetail, int testInfo)
        {
            Query query = TTestInfo.CreateQuery();
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TResultDetail.Schema)
                            .Where(TResultDetail.Columns.TestId).IsEqualTo(testInfo);

                        if(sqlQuery.GetRecordCount()>0)
                        {
                            new Update(TTestInfo.Schema)
                           .Set(TTestInfo.Columns.TestStatus).EqualTo(90)
                           .Where(TTestInfo.Columns.TestId).IsEqualTo(testInfo).Execute();
                        }
                       
                        foreach (DataRow dr in testInfoDetail.Rows)
                        {
                            if (dr["CHON"].ToString() == "1")
                            {
                                if (dr["IsNew"].ToString() == "1")
                                {
                                    TTestInfo objTestInfor = CreateTestInfoDetail(dr);
                                    if (objTestInfor != null)
                                    {
                                        objTestInfor.IsNew = true;
                                        objTestInfor.Save();
                                        int testId = Convert.ToInt32(query.GetMax(TTestInfo.Columns.TestId));
                                        dr[TTestInfo.Columns.TestId] = testId;
                                    }
                                }
                                else
                                {
                                    TTestInfo objTestInfor = CreateTestInfoDetail(dr);
                                    UpdateTestInfor(objTestInfor);
                                }
                            }
                        }
                        testInfoDetail.AcceptChanges();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }
        public int UpdatePrintStatus(int Test_ID)
        {
            int record = 0;
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (new SharedDbConnectionScope())
                    {
                        SqlQuery sqlQuery = new Select().From(TResultDetail.Schema)
                           .Where(TResultDetail.Columns.TestId).IsEqualTo(Test_ID);

                        if (sqlQuery.GetRecordCount() > 0){
                           
                           record= new Update(TTestInfo.Schema)
                           .Set(TTestInfo.Columns.TestStatus).EqualTo(90)
                           .Set(TTestInfo.Columns.Printstatus).EqualTo(true)
                           .Where(TTestInfo.Columns.TestId).IsEqualTo(Test_ID).Execute();
                        }
                        
                    }
                    scope.Complete();
                    //'return ActionResult.Success;
                }
            }catch(Exception exception)
            {
               // return ActionResult.Error;
                record = 0;
            }
            return record;

        }

        private TResultDetail CreateResultDetail(DataRow dataRow)
        {
            var resultDetail = new TResultDetail();
            if (dataRow["IsNew"].ToString() == "0")
                resultDetail.TestDetailId = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.TestDetailId], -1);
            resultDetail.TestId = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.TestId], -1);
            resultDetail.TestTypeId = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.TestTypeId], -1);
            resultDetail.Barcode = Utility.sDbnull(dataRow[TResultDetail.Columns.Barcode], "");
            ;
            resultDetail.PatientId = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.PatientId], -1);
            ;
            resultDetail.NormalLevel = Utility.sDbnull(dataRow[TResultDetail.Columns.NormalLevel], "");
            resultDetail.NormalLevelW = Utility.sDbnull(dataRow[TResultDetail.Columns.NormalLevelW], "");
            resultDetail.ParaName = Utility.sDbnull(dataRow[TResultDetail.Columns.ParaName], "");
            resultDetail.PrintData = Convert.ToBoolean(dataRow[TResultDetail.Columns.PrintData]);
            resultDetail.MeasureUnit = Utility.sDbnull(dataRow[TResultDetail.Columns.MeasureUnit]);
            resultDetail.ParaStatus = 0;
            resultDetail.TestResult = Utility.sDbnull(dataRow[TResultDetail.Columns.TestResult]);
            resultDetail.TestDate = Convert.ToDateTime(dataRow[TResultDetail.Columns.TestDate].ToString());
            resultDetail.DataSequence = Utility.Int32Dbnull(dataRow[TResultDetail.Columns.DataSequence]);
            resultDetail.UpdateNum = 1;
            return resultDetail;
        }

        private TTestInfo CreateTestInfoDetail(DataRow dataRow)
        {
            var testInforDetail = new TTestInfo();
            if (dataRow["IsNew"].ToString() == "0")
                testInforDetail.TestId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.TestId], -1);
            testInforDetail.TestTypeId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.TestTypeId], -1);
            testInforDetail.Barcode = Utility.sDbnull(dataRow[TTestInfo.Columns.Barcode], "");
            testInforDetail.TestSeq = Utility.sDbnull(dataRow[TTestInfo.Columns.TestSeq], "");
            testInforDetail.PatientId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.PatientId], -1);
            testInforDetail.TestDate = globalVariables.SysDate;
            //testInforDetail.RequireDate = globalVariables.SysDate;
            testInforDetail.AssignId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.AssignId], -1);
            testInforDetail.DiagnosticianId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.DiagnosticianId], -1);
            testInforDetail.ReceiverId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.ReceiverId], -1);
            testInforDetail.TestStatus = 90;
            testInforDetail.DiagResult = Utility.sDbnull(dataRow[TTestInfo.Columns.DiagResult], "");
            testInforDetail.DeviceId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.DeviceId], -1);
            testInforDetail.ParaId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.ParaId], -1);
            testInforDetail.Printstatus = true;
            testInforDetail.UpdateDate = globalVariables.SysDate;
            testInforDetail.UpdateUser = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.UpdateUser], -1);
            testInforDetail.HisAssignId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.HisAssignId], -1);
            return testInforDetail;
        }

        public ActionResult UpdateDeviceTestInfor(ref DataTable testInfoDetail, int v_TestInfo)
        {
            int Test_Id = -1;
            Query _Query = TTestInfo.CreateQuery();
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        new Update(TTestInfo.Schema)
                            .Set(TTestInfo.Columns.TestStatus).EqualTo(90)
                            .Where(TTestInfo.Columns.TestId).IsEqualTo(v_TestInfo).Execute();
                        foreach (DataRow dr in testInfoDetail.Rows)
                        {
                            if (dr["CHON"].ToString() == "1")
                            {
                                if (dr["IsNew"].ToString() == "1")
                                {
                                    TTestInfo ObjTestInfor = CreateTestInfoDetail(dr);
                                    if (ObjTestInfor != null)
                                    {
                                        ObjTestInfor.IsNew = true;
                                        ObjTestInfor.Save();
                                        Test_Id =
                                            Convert.ToInt32(_Query.GetMax(TTestInfo.Columns.TestId));
                                        dr[TTestInfo.Columns.TestId] = Test_Id;
                                    }
                                    //dr["IsNew"] = 0;
                                }
                                else
                                {
                                    TTestInfo ObjTestInforV = CreateDeviceInfor(dr);
                                    UpdateTestInfor(ObjTestInforV);
                                }
                            }
                        }
                        testInfoDetail.AcceptChanges();
                    }
                    scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }

        private TTestInfo CreateDeviceInfor(DataRow dataRow)
        {
            var testInforDetail = new TTestInfo();
            if (dataRow["IsNew"].ToString() == "0")
                testInforDetail.TestId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.TestId], -1);
            testInforDetail.TestTypeId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.TestTypeId], -1);
            testInforDetail.Barcode = Utility.sDbnull(dataRow[TTestInfo.Columns.Barcode], "");
            testInforDetail.TestSeq = Utility.sDbnull(dataRow[TTestInfo.Columns.TestSeq], "");
            testInforDetail.PatientId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.PatientId], -1);
            testInforDetail.DeviceId = Utility.Int32Dbnull(dataRow[TTestInfo.Columns.DeviceId], -1);
            return testInforDetail;
        }


        /// <summary>
        /// hàm thực hiện update UpdateResultDetail dữ liệu
        /// </summary>
        /// <param name="resultDetail"></param>
        /// <returns></returns>
        public void UpdateResultDetail(TResultDetail resultDetail)
        {
            int record = -1;
            Query _Query = TResultDetail.CreateQuery();


            if (TResultDetail.FetchByID(resultDetail.TestDetailId) != null)
            {
                new Update(TResultDetail.Schema)
                    .Set(TResultDetail.Columns.Barcode).EqualTo(resultDetail.Barcode)
                    .Set(TResultDetail.Columns.ParaName).EqualTo(resultDetail.ParaName)
                    .Set(TResultDetail.Columns.Note).EqualTo(resultDetail.Note)
                    .Set(TResultDetail.Columns.PrintData).EqualTo(resultDetail.PrintData)
                    .Set(TResultDetail.Columns.TestResult).EqualTo(resultDetail.TestResult)
                    .Set(TResultDetail.Columns.TestId).EqualTo(resultDetail.TestId)
                    .Set(TResultDetail.Columns.TestTypeId).EqualTo(resultDetail.TestTypeId)
                    .Set(TResultDetail.Columns.PatientId).EqualTo(resultDetail.PatientId)
                    .Set(TResultDetail.Columns.TestDate).EqualTo(resultDetail.TestDate)
                    .Set(TResultDetail.Columns.TestSequence).EqualTo(resultDetail.TestSequence)
                    .Set(TResultDetail.Columns.ParaStatus).EqualTo(resultDetail.ParaStatus)
                    .Set(TResultDetail.Columns.UpdateNum).EqualTo(resultDetail.UpdateNum)
                    .Set(TResultDetail.Columns.MeasureUnit).EqualTo(resultDetail.MeasureUnit)
                    .Set(TResultDetail.Columns.NormalLevelW).EqualTo(resultDetail.NormalLevelW)
                    .Set(TResultDetail.Columns.NormalLevel).EqualTo(resultDetail.NormalLevel)
                    .Where(TResultDetail.Columns.TestDetailId).IsEqualTo(resultDetail.TestDetailId).Execute();
            }
        }

        public void UpdateTestInfor(TTestInfo testInfor)
        {
            int record = -1;
            Query _Query = TTestInfo.CreateQuery();


            if (TTestInfo.FetchByID(testInfor.TestId) != null)
            {
                new Update(TTestInfo.Schema)
                    .Set(TTestInfo.Columns.Barcode).EqualTo(testInfor.Barcode)
                    .Set(TTestInfo.Columns.TestTypeId).EqualTo(testInfor.TestTypeId)
                    .Set(TTestInfo.Columns.TestSeq).EqualTo(testInfor.TestSeq)
                    .Set(TTestInfo.Columns.PatientId).EqualTo(testInfor.PatientId)
                    //.Set(TTestInfo.Columns.TestDate).EqualTo(testInfor.TestDate)
                    //.Set(TTestInfo.Columns.RequireDate).EqualTo(testInfor.RequireDate)
                    .Set(TTestInfo.Columns.TestStatus).EqualTo(testInfor.TestStatus)
                    .Set(TTestInfo.Columns.DeviceId).EqualTo(testInfor.DeviceId)
                    .Set(TTestInfo.Columns.ParaId).EqualTo(testInfor.ParaId)
                    .Set(TTestInfo.Columns.Printstatus).EqualTo(testInfor.Printstatus)
                    .Where(TTestInfo.Columns.TestId).IsEqualTo(testInfor.TestId).Execute();
            }
        }

        public void UpdateResultDetailDetail(TResultDetail objResultDetail)
        {
            SqlQuery q = new Select().From(TResultDetail.Schema)
                .Where(TResultDetail.Columns.TestId).IsEqualTo(objResultDetail.TestId).And(
                    TResultDetail.Columns.TestTypeId).IsEqualTo(objResultDetail.TestTypeId).And(
                        TResultDetail.Columns.PatientId).IsEqualTo(objResultDetail.PatientId);


            if (q.GetRecordCount() <= 0)
            {
                objResultDetail.IsNew = true;
                objResultDetail.Save();
            }
            else
            {
                new Update(TResultDetail.Schema)
                    .Set(TResultDetail.Columns.Barcode).EqualTo(objResultDetail.Barcode)
                    .Set(TResultDetail.Columns.ParaName).EqualTo(objResultDetail.ParaName)
                    .Set(TResultDetail.Columns.Note).EqualTo(objResultDetail.Note)
                    .Set(TResultDetail.Columns.PrintData).EqualTo(objResultDetail.PrintData)
                    .Set(TResultDetail.Columns.TestResult).EqualTo(objResultDetail.TestResult)
                    .Set(TResultDetail.Columns.TestId).EqualTo(objResultDetail.TestId)
                    .Set(TResultDetail.Columns.TestTypeId).EqualTo(objResultDetail.TestTypeId)
                    .Set(TResultDetail.Columns.PatientId).EqualTo(objResultDetail.PatientId)
                    .Set(TResultDetail.Columns.TestDate).EqualTo(objResultDetail.TestDate)
                    .Set(TResultDetail.Columns.TestSequence).EqualTo(objResultDetail.TestSequence)
                    .Set(TResultDetail.Columns.ParaStatus).EqualTo(objResultDetail.ParaStatus)
                    .Set(TResultDetail.Columns.UpdateNum).EqualTo(objResultDetail.UpdateNum)
                    .Set(TResultDetail.Columns.MeasureUnit).EqualTo(objResultDetail.MeasureUnit)
                    .Set(TResultDetail.Columns.NormalLevelW).EqualTo(objResultDetail.NormalLevelW)
                    .Set(TResultDetail.Columns.NormalLevel).EqualTo(objResultDetail.NormalLevel)
                    .Where(TResultDetail.Columns.TestDetailId).IsEqualTo(objResultDetail.TestDetailId).Execute();
            }
        }

        public static DataTable GetTRegListByTestID(int input)
        {
            var reciveTable = new DataTable();
            reciveTable =
                new Select(TRegList.Columns.ParaName, TRegList.Columns.Barcode, TRegList.Columns.DeviceId).From(
                    TRegList.Schema.Name).Where(TRegList.Columns.TestId).IsEqualTo(input).ExecuteDataSet().Tables[0];
            return reciveTable;
        }

        public static DataTable GetTestInfor(string testTypeId, DateTime date)
        {
            string strdate = date.ToString("dd/MM/yyyy");
            var TestTyeInfo = new DataTable();
            TestTyeInfo =
                new Select().From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.TestTypeId).IsEqualTo(testTypeId).And(
                    TTestInfo.Columns.TestDate).IsEqualTo(date).ExecuteDataSet().Tables[0];
            return TestTyeInfo;
        }

        public static DataTable GetTestInfo(string testTypeId)
        {
            DataTable dttestinfor =
                new Select(TTestInfo.Columns.PatientId, LPatientInfo.Columns.PatientId).From(TTestInfo.Schema).InnerJoin
                    (LPatientInfo.PatientIdColumn, TTestInfo.PatientIdColumn).ExecuteDataSet().Tables[0];
            return dttestinfor;
        }

        public static DataTable GetTestInfoToPatient(string patientId)
        {
            DataTable dtTestInfor =
                new Select().From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.PatientId).IsEqualTo(patientId).
                    ExecuteDataSet().Tables[0];
            return dtTestInfor;
        }

        public static DataTable Result(int v_Test_ID)
        {
            DataTable dtresult =
                new Select().From(TResultDetail.Schema.Name).Where(TResultDetail.Columns.PatientId).IsEqualTo(v_Test_ID)
                    .
                    ExecuteDataSet().Tables[0];
            return dtresult;
        }

        /// <summary>
        /// Hàm đăng ký xét nghiệm 2 chiều 
        /// </summary>
        /// <param name="TestId"></param>
        /// <param name="DeviceId"></param>
        /// <param name="Barcode"></param>
        /// <param name="AliasName"></param>
        /// <param name="ParaName"></param>
        /// <param name="Save"></param>
        /// <returns></returns>
        public static int InsertRegList(int TestId, int DeviceId, string Barcode, string AliasName, string ParaName,
                                        bool Save)
        {
            try
            {
                return SPs.SpCreateRegList(TestId, DeviceId, Barcode, AliasName, ParaName, Save).Execute();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static DataTable GetDataControlByDevice(int deviveID, int TestType_ID, string fromData, string toDate)
        {
            var reciveData = new DataTable();
            return reciveData = SPs.SpGet2WayDataControl(deviveID, TestType_ID, fromData, toDate).GetDataSet().Tables[0];
        }

        public static void DeleteRegList(int pid)
        {
            if (TRegList.FetchByID(pid) != null)
            {
                TRegList.Delete(pid);
            }
        }

        public static int InsertTestInforDelete(int testId,string barcode,int patientId,DateTime testDate,DateTime requiredate,int assignId,int dianogsId,int recviecID,short teststatus,int updateUser)
        {
            return
                SPs.TTestInfoInsertDelete(testId, barcode, patientId, testDate, requiredate, assignId, dianogsId,
                                          recviecID, teststatus, updateUser).Execute();
        }
        public static int InsertTestResultDelete(int testDetailId, int testId,int patientId, int testTypeId, string testdate,string testSeq, int datasequen,string barcode,string paraname,string testResult,string measureunit,string nomallevel,string nomallevelW)
        {
            return
                SPs.SpInsertTestResultDelete(testDetailId,testId, patientId, testTypeId, testdate, testSeq, datasequen, barcode,
                                             paraname, testResult, measureunit, nomallevel, nomallevelW).Execute();

        }
        public  static int DeleteTestInforTResulTblYeucauxn(string barcode,int testTypeId)
        {
            return SPs.DeleteTestinforTResultTblYeucauXN(barcode, testTypeId).Execute();
        }

        public static int GetPrintStatusForPatient(int patientId)
        {
            int result = 0;
            try
            {
                int total =
                    new Select(TTestInfo.Columns.TestId).From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.PatientId).
                        IsEqualTo(patientId).GetRecordCount();
                int printed =
                    new Select(TTestInfo.Columns.TestId).From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.PatientId).
                        IsEqualTo(patientId).And
                        (TTestInfo.Columns.Printstatus).IsEqualTo(1)
                        .GetRecordCount();
                if (total == printed) result = 2;
                else if (printed > 0) result = 1;
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
    }
}