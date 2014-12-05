using System;
using SubSonic;
using VietBaIT.HISLink.DataAccessLayer;
using System.Data;
using System.Windows.Forms;


namespace VietBaIT.CommonLibrary
{
    [Serializable]
    public class BusinessHelper
    {
       // private static long PatientID=0;

        public static string GeneratePatientCode()
        {
            string workingYear = GetUTCDate().Year.ToString();
            string result = string.Format("{0}-{1}", workingYear, GenerateCode(PatientCodeIncrement() +1));
            return result;
        }
       
        public static string GenerateInt(SqlQuery q)
        {
           // SqlQuery q = new Select(Aggregate.Max(LDepartment.Columns.IntOrder)).From(LDepartment.Schema.TableName);
            int record = q.ExecuteScalar<int>();
            return (record + 1).ToString();
        }
        /// <summary>
        /// Thuc hiên lây Identity
        /// </summary>
        /// <returns></returns>
        public static string GenerateIdentity()
        {
            string workingYear = GetUTCDate().Year.ToString();
            string result = workingYear + "-" + GetIdentity();
            return result;
        }
       /// <summary>
       /// Hàm thự hiện sinh ra PatientID
       /// </summary>
       /// <returns></returns>
        public static string GeneratePatientID()
        {
            //long PatientID = new Select(TPatientInfo.Columns.PatientId).Top("1")
            //             .From(TPatientInfo.Schema.TableName)
            //             .OrderDesc(TPatientInfo.Columns.PatientId).ExecuteScalar<long>();
            long PatientID =Convert.ToInt64(SPs.GeneratePatientID().ExecuteScalar());

            return (PatientID+1).ToString();
        }
        public static string GetIdentity()
        {
            long Identity = Convert.ToInt64(SPs.GetIdentity().ExecuteScalar());
            return (Identity + 1).ToString();
        }
        /// <summary>
        /// Lấy só thứ tự theo điều kiện load từ tham số trên
        /// </summary>
        /// <returns></returns>
        public static string NoOfDay()
        {
          
                return (CurrentNumberOfDay() + 1).ToString();
           
        }
        /// <summary>
        /// hàm thực hiện lấy ngày hiện tại
        /// </summary>
        /// <returns></returns>
        public static DateTime GetUTCDate()
        {
            DateTime? outPutDT = null;
            var result = new DateTime();
            StoredProcedure  sp = SPs.GetSQLServerUTCDate(outPutDT);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
                                        {
                                            result = (DateTime) objOutput;
                                        });
            return result;
        }
        /// <summary>
        /// Hàm thuc hiên out value
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static string OutParamaterValue(StoredProcedure sp)
        {
            DateTime? outPutDT = null;
            var result = new decimal();          
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                result = (decimal)objOutput;
            });
            return result.ToString();
        }
        /// <summary>
        /// Lấy thời gian của hệ thống
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSysDateTime()
        {
            DataTable dataTable=new DataTable();
            DateTime dt =new DateTime();
            dt = DateTime.Now;
            dataTable = SPs.GetSYSDateTime().GetDataSet().Tables[0];
            if(dataTable.Rows.Count>0)
            {
                dt = Convert.ToDateTime(dataTable.Rows[0]["sysDateTime"]);
            }
            return dt;
        }
        /// <summary>
        /// Lấy thời gian hệ thống theo định dạng của VN
        /// </summary>
        /// <returns></returns>
        public static DateTime GetSysVNDateTime()
        {
            DateTime? outPutDT = null;
            var result = new DateTime();
            StoredProcedure sp = SPs.GetVNDateTime(outPutDT);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                result = (DateTime)objOutput;
            });
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objectTypeID"></param>
        /// <returns></returns>
        public static bool IsObjectNeedPayment(int objectTypeID)
        {
            bool result = false;
            var objectType =
                new Select().From<LObjectType>().Where(LObjectType.Columns.ObjectTypeId).IsEqualTo(objectTypeID).
                    ExecuteSingle<LObjectType>();
            if (objectType != null)
            {
                //result = (objectType.HospitalFee != 0) ? true : false;
            }
            return result;
        }
        
        #region Private Method
        public static int CurrentNumberOfDay()
        {
            int maxNumberOfDay = 0;
            int? output = null;
            StoredProcedure sp = SPs.GetNumberOfRecordsByDate(output);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                maxNumberOfDay = (int)objOutput;
            });
            
            return maxNumberOfDay;
        }
        public static int CurrentNumberOfYear()
        {
            int maxNumberOfDay = 0;
            int? output = null;
            StoredProcedure sp = SPs.GetNumberOfRecordsByYear(output);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                maxNumberOfDay = (int)objOutput;
            });

            return maxNumberOfDay;
        }
        private static int PatientCodeIncrement()
        {
            
            return Convert.ToInt32(NumberPatientCode());
        }
        
        public static string NumberPatientCode()
        { 
            string OutpatientcCode="";
            string LastCode = "";
          
           // var Patient = SPs.GetPatientCode(patientcCode);
            //DateTime? outPutDT = null;
            string result="";
            StoredProcedure sp = SPs.GetPatientCode(OutpatientcCode);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                result = (String)objOutput;
            });

            LastCode = result.Substring(result.IndexOf("-") + 1, result.Length - (result.IndexOf("-") + 1));
          //  VietBaIT.CommonLibrary.Utility.ShowMsg(LastCode);
            return LastCode;
                     

        }

        public static string ToTalPatientExam(string OutpatientcCode)
        {

            decimal? total = null;

            // var Patient = SPs.GetPatientCode(patientcCode);
            //DateTime? outPutDT = null;
            decimal result = 0;
            StoredProcedure sp = SPs.ProcTotalNgoaiTru(OutpatientcCode, total);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput)
            {
                result = (decimal)objOutput;
            });

            return result.ToString();

        }
        private static string GenerateCode(int increment)
        {
            string result = string.Empty;
            string suffix;
            suffix = string.Empty;
            char[] arr = increment.ToString().ToCharArray();
            for (int i = arr.Length; i < 5; i++)
            {
                suffix += "0";
            }
            result = string.Format("{0}{1}", suffix,increment);
            return result;
       }
        private static string Generate_Service_Code(int increment)
        {
            string result = string.Empty;
            string suffix;
            suffix = string.Empty;
            char[] arr = increment.ToString().ToCharArray();
            for (int i = arr.Length; i < 4; i++)
            {
                suffix += "0";
            }
            result = string.Format("{0}{1}", suffix, increment);
            return result;
        }
        public static string OutPut_ID(StoredProcedure sp)
        {
           return sp.OutputValues[0].ToString();
        }
        #endregion

        /// <summary>
        /// lấy thông tin của nhân viên
        /// </summary>
        /// <param name="v_Staff_Id"></param>
        /// <returns></returns>
        public static string GetStaffName(int v_Staff_Id)
        {
            LStaff objStaff = LStaff.FetchByID(v_Staff_Id);
            if(objStaff!=null)
            return objStaff.StaffName;
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="v_Staff_Id"></param>
        /// <returns></returns>
        public static string GetDepartmentName(int v_Department_Id)
        {
            LDepartment objDepartment = LDepartment.FetchByID(v_Department_Id);
            if (objDepartment != null)
                return objDepartment.DepartmentName;
            else
            {
                return "";
            }
        }
        public static string GetServiceName(int Service_Id)
        {
            LService objService = LService.FetchByID(Service_Id);
            if (objService != null) return objService.ServiceName;
            else
            {
                return "";
            }
        }
        public static string GetExtendExamDoctorAssign()
        {
            return "Staff_Type_Type_ID=1 or Staff_ID=-1";
        }
        /// <summary>
        /// ham thuc hien phan biet trai tuyen hay la dung tuyen
        /// </summary>
        /// <param name="vClinicCode"></param>
        /// <returns></returns>
        public static bool DiscorrectLinePatientExam(string vClinicCode)
        {
            DataTable dataTable=new DataTable();
            dataTable = SPs.DetmayGetClinicCode(vClinicCode).GetDataSet().Tables[0];
            if(dataTable.Rows.Count>0)
            {
                SqlQuery q = new SqlQuery().From(SysSystemParameter.Schema)
                    .Where(SysSystemParameter.Columns.SName).IsEqualTo("ACCOUNTCLINIC");
                if(q.GetRecordCount()>0)
                {
                    SysSystemParameter objParameter = q.ExecuteSingle<SysSystemParameter>();
                    if (objParameter.SValue.Equals(vClinicCode))return false;
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
                

            }
               
            else
            {
                return true;
            }
        }
    }
}
