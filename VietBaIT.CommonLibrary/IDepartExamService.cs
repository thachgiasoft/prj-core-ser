using System;
using System.Data;
using VietBaIT.HISLink.DataAccessLayer;
using VietBaIT.CommonLibrary;

namespace VietBaIT.HISLink.Business.ExternalExam
{
    interface IDepartExamService
    {
        DataTable GetAllPatientBySearchPatient(long varPatient_ID, string strPatientCode, string strPatientName,
                                               byte Sex, DateTime FromDate, DateTime ToDate, short varObject_Type_ID,
                                               byte Hos_Patient, string strInsurance_no, int Special_Id);

        ActionResult InsertPatientExam(TPatientInfo objPatientInfo, TPatientExam objPatientExam,
                                       TPatientDept patientDept);

        ActionResult UpdatePatientExam(TPatientInfo objPatientInfo, TPatientExam objPatientExam,
                                       TPatientDept objPatientDept, short Department_ID);
       
        ActionResult AddNewPatientExam(TPatientInfo objPatientInfo, TPatientExam objPatientExam);
       void UpdatePatientInfo(TPatientInfo objPatientInfo);
        ActionResult DeleteRegExam(int Reg_Id);

        long InsertRegExam(long Patient_ID, string strPatientCode, decimal Money, string UserName, short RoomDept_ID,
                           short Doctor_ID);

        ActionResult AddAssignInfo(TAssignInfo objAssignInfo, TAssignDetail[] objAssignDetails);
        ActionResult DeleteAssInfo(int Assgin_Id);
        ActionResult InsertStaffList(LStaff objStaffList);
        ActionResult DeleteStaffList(int Staff_ID);
        ActionResult UpdateStaffList(LStaff objStaffList);
        DataTable GetDataDepartment();
        DataTable GetMeasureUnit();

        ActionResult InsertSurgery(LSurgery objSurgery);
    }
}
