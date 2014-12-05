using System;
using System.Data;
using VietBaIT.HISLink.DataAccessLayer;

namespace VietBa.HISLink.Services
{
    public interface IExamService
    {
       
        DataSet getListDoctor();
        DataSet getListDiseaseType();
        DataSet getListDisease();
        bool updateDiag_Info(TDiagInfo objTDiagInfo);
        bool deleteDiag_Info(Object intDiag);
        bool delExamAll(object ExamId, object[] DiagId);
        bool delExamAll(object ExamId);
        bool saveExamAll(TExamInfo objExamInfo, TExamFull objTExamFull, TExamDetail objTExamDetail, TExamOther objTExamOther, TDiagInfo[] objTDiagInfo,TAssignInfoCollection AssColl,TAssignDetailCollection AssDetailColl);
        bool saveExamAllPatientInfo(TExamInfo objExamInfo, TExamFull objTExamFull, TExamDetail objTExamDetail, TExamOther objTExamOther, TDiagInfo[] objTDiagInfo, DataTable AssColl, DataTable AssDetailColl, TPatientDept patientDept, DataTable exam, ref long Exam_ID);
        bool updateExamAllPatientInfo(TExamInfo objExamInfo, TExamFull objTExamFull, TExamDetail objTExamDetail, TExamOther objTExamOther, TDiagInfo[] ArrTDiagInfo, bool blnExamFull, bool blnExamDetail, bool blnExamOrther, bool blnDiagInfo, DataTable AssColl, DataTable AssDetailColl,TPatientDept objPatientDept,DataTable objRegExam);

        DataSet GetListPatientInDepts(DateTime FromDate, DateTime ToDate, int departId, int ObjectType_ID, int Sex,
                                      string Barcode, string PatientName);
    }
}
