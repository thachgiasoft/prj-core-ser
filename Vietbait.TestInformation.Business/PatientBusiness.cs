using System;
using System.Data;
using System.Transactions;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;

namespace Vietbait.TestInformation.Business
{
    public class PatientBusiness : CommonBusiness
    {
        static PatientBusiness()
        {
            new PatientBusiness();
        }

        public static DataTable GetlAllPatient()
        {
            try
            {
                DataTable query =
                    new Select().From(LPatientInfo.Schema.Name).OrderAsc(LPatientInfo.Columns.PatientName).
                        ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string InsertPatient(LPatientInfo pitems)
        {
            if (pitems.Pid == null) pitems.Pid = "";
            if (pitems.PatientName == null) pitems.PatientName = "";
            if (pitems.Address == null) pitems.Address = "";
            if (pitems.Age == null) pitems.Age = 0;
            if (pitems.YearBirth == null) pitems.YearBirth = 0;
            if (pitems.Sex == null) pitems.Sex = 0;
            pitems.Dateupdate = DateTime.Now;
            if (pitems.Diagnostic == null) pitems.Diagnostic = "";
            if (pitems.IdentifyNum == null) pitems.IdentifyNum = "";
            if (pitems.DepartmentID == null) pitems.DepartmentID = -1;
            if (pitems.Room == null) pitems.Room = "";
            if (pitems.Bed == null) pitems.Bed = "";
            if (pitems.ObjectType == null) pitems.ObjectType = -1;

            int i = 0;
            Query _QueryRS = LPatientInfo.CreateQuery();
            try
            {
                if (!LPatientInfo.FetchByParameter(LPatientInfo.Columns.Pid, Comparison.Equals, pitems.Pid).Read() ||
                    pitems.Pid == "")
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return _QueryRS.GetMax(LPatientInfo.Columns.PatientId).ToString();
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static string InsertPatienttoLpatientDelte(LPatientInfo pitems)
        {
            if (pitems.Pid == null) pitems.Pid = "";
            if (pitems.PatientName == null) pitems.PatientName = "";
            if (pitems.Address == null) pitems.Address = "";
            if (pitems.Age == null) pitems.Age = 0;
            if (pitems.YearBirth == null) pitems.YearBirth = 0;
            if (pitems.Sex == null) pitems.Sex = 0;
            pitems.Dateupdate = DateTime.Now;
            if (pitems.Diagnostic == null) pitems.Diagnostic = "";
            if (pitems.IdentifyNum == null) pitems.IdentifyNum = "";
            if (pitems.DepartmentID == null) pitems.DepartmentID = -1;
            if (pitems.Room == null) pitems.Room = "";
            if (pitems.Bed == null) pitems.Bed = "";
            if (pitems.ObjectType == null) pitems.ObjectType = -1;

            int i = 0;
            Query _QueryRS = LPatientInfo.CreateQuery();
            try
            {
                if (LPatientInfo.FetchByID(pitems.PatientId)!=null)
                {
                    pitems.IsNew = true;
                    pitems.Save(i);

                    return _QueryRS.GetMax(LPatientInfo.Columns.PatientId).ToString();
                }
                else
                {
                    return "-1";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }
        
        public static ActionResult UpdatePatientInfo(LPatientInfo pitems)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        if (LPatientInfo.FetchByID(pitems.PatientId) != null)
                        {
                            new Update(LPatientInfo.Schema)
                                .Set(LPatientInfo.Columns.YearBirth).EqualTo(pitems.YearBirth)
                                .Set(LPatientInfo.Columns.PatientName).EqualTo(pitems.PatientName)
                                .Set(LPatientInfo.Columns.ObjectType).EqualTo(pitems.ObjectType)
                                .Set(LPatientInfo.Columns.DepartmentID).EqualTo(pitems.DepartmentID)
                                .Set(LPatientInfo.Columns.Sex).EqualTo(pitems.Sex)
                                .Set(LPatientInfo.Columns.Address).EqualTo(pitems.Address)
                                .Set(LPatientInfo.Columns.Room).EqualTo(pitems.Room)
                                .Set(LPatientInfo.Columns.Bed).EqualTo(pitems.Bed)
                                .Set(LPatientInfo.Columns.InsuranceNum).EqualTo(pitems.InsuranceNum)
                                .Set(LPatientInfo.Columns.IdentifyNum).EqualTo(pitems.IdentifyNum)
                                .Where(LPatientInfo.Columns.PatientId).IsEqualTo(pitems.PatientId).Execute();
                        }
                        scope.Complete();
                        return ActionResult.Success;
                    }
                }
            }
            catch (Exception exception)
            {
                return ActionResult.Error;
            }
        }

        public static void DeletePatient(string patient_ID)
        {
            var query = new Query(LPatientInfo.Schema.Name);
            query.QueryType = QueryType.Delete;
            query.WHERE(LPatientInfo.Columns.PatientId, patient_ID);
            query.Execute();
        }

        public static void UpdatePatient(LPatientInfo pitems)
        {
            try
            {
                if (LPatientInfo.FetchByID(pitems.PatientId) != null)
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

        public static DataSet GetPatientListByDate(DateTime fromDate, DateTime toDate)
        {
            try
            {
                string strFormat = "dd/MM/yyyy";
                string strFromDate = fromDate.ToString(strFormat);
                string strToDate = toDate.ToString(strFormat);
                return SPs.SpGetPatientListByDate(strFromDate, strToDate).GetDataSet();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetBarCodeTestInfo(int vPatient_ID, int SystemPara, int Para_ID)
        {
            var dataTable = new DataTable();
            string sBarcode = "";
            TTestInfoCollection testInfoController =
                new TTestInfoController().FetchByQuery(TTestInfo.CreateQuery().AddWhere(TTestInfo.Columns.PatientId,
                                                                                        Comparison.Equals, vPatient_ID));
            if (testInfoController.Count > 0)
            {
                sBarcode = testInfoController[0].Barcode;
            }
            else
            {
                dataTable = SPs.SpGetNewTestSID(globalVariables.SysDate, SystemPara, Para_ID).GetDataSet().Tables[0];
              
                if (dataTable.Rows.Count > 0)
                {
                    sBarcode = dataTable.Rows[0][0].ToString();
                }
            }
            return sBarcode;
        }

        public static DataTable GetPatientTestType(string Patient_ID)
        {
            try
            {
                DataTable dt = SPs.SpGetTestTypeByPatient(Convert.ToInt32(Patient_ID)).GetDataSet().Tables[0];

                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int insertPatientDelete(int Patient_ID,string PID,string patientName,string insurancenum,string address,int age,int yearbrith,bool sex,string diagnostic,string IdentifyNum,short DepartmentID,string Room,string bed,DateTime dateupdate,short objecttype)
        {
           
                return
                       SPs.PatientInfoInsertDelete(Patient_ID, PID, patientName, address, age, yearbrith, sex, diagnostic,
                                                   insurancenum, insurancenum, DepartmentID, Room, bed, dateupdate, objecttype)
                           .Execute();
           
            
        }


        public static string BarCodeTestInfo(int vPatient_ID, int testTyeId, int Para_ID)
        {
            var dataTable = new DataTable();
            string sBarcode = "";
            TTestInfoCollection testInfoController =
                new TTestInfoController().FetchByQuery(TTestInfo.CreateQuery().AddWhere(TTestInfo.Columns.PatientId,
                                                                                        Comparison.Equals, vPatient_ID));
            if (testInfoController.Count > 0)
            {
                sBarcode = testInfoController[0].Barcode;
            }
            else
            {
                dataTable = SPs.SpGetNewTestSID(globalVariables.SysDate, testTyeId, Para_ID).GetDataSet().Tables[0];
                if (dataTable.Rows.Count > 0)
                {
                    sBarcode = dataTable.Rows[0][0].ToString();
                }
            }
            return sBarcode;
        }

        public static string InsertPatientInfo(LPatientInfo pitems)
        {
            int i = 0;
            Query _QueryRS = LPatientInfo.CreateQuery();
            try
            {
                if (!LUser.FetchByParameter(LPatientInfo.Columns.PatientId, Comparison.Equals, pitems.PatientId).Read())
                {
                    pitems.IsNew = true;
                    pitems.Save(i);
                    return _QueryRS.GetMax(LPatientInfo.Columns.PatientId).ToString();
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
    }
}