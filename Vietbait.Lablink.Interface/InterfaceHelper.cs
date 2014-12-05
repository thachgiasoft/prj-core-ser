using System;
using System.Data;
using System.IO;
using System.Transactions;
using System.Xml.Serialization;
using SubSonic;
using VietBaIT.CommonLibrary;
using Vietbait.Lablink.Model;
using Vietbait.Lablink.Utilities;
using VB6 = Microsoft.VisualBasic.Strings;

namespace Vietbait.Lablink.Interface
{
    public class InterfaceHelper : CommonBusiness
    {
        public static InterfaceProperties MyProperties;
        public static string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        public static InterfaceProperties GetInterfaceConfig()
        {
            try
            {
                var myProperties = new InterfaceProperties();
                var filePath = string.Format("{0}{1}.xml", AppPath, myProperties.GetType().Name);
                var myFileStream = new FileStream(filePath, FileMode.Open);
                var mySerializer = new XmlSerializer(myProperties.GetType());
                myProperties = (InterfaceProperties)mySerializer.Deserialize(myFileStream);
                myFileStream.Close();
                return myProperties;
            }
            catch (Exception ex)
            {
                return new InterfaceProperties();
            }
        }

        public static void SaveInterfaceConfig()
        {
            var myWriter = new StreamWriter(string.Format("{0}{1}.xml", AppPath, MyProperties.GetType().Name));
            try
            {
                var mySerializer = new XmlSerializer(MyProperties.GetType());
                mySerializer.Serialize(myWriter, MyProperties);
            }
            catch (Exception)
            {
            }
            finally
            {
                myWriter.Flush();
                myWriter.Close();
            }
        }

        #region Contructor

        static InterfaceHelper()
        {
            new InterfaceHelper();
            MyProperties = GetInterfaceConfig();
        }

        #endregion

        #region Hàm Load các thông tin lên datatable

        public static DataTable GetallPatient(DateTime testdate)
        {
            try
            {
                DataTable query =
                    new Select().From(TblHisLisPatientInfoVnio.Schema.Name).Where(
                        TblHisLisPatientInfoVnio.Columns.TestDate).IsEqualTo(testdate).ExecuteDataSet().Tables[0];
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetallPatient()
        {
            DataTable query = new Select().From(TblHisLisPatientInfoVnio.Schema.Name).ExecuteDataSet().Tables[0];
            return query;
        }

        //Load thông tin các tên xn
        public static DataTable LoadParaMappingHisLis()
        {
            DataTable query =
                new Select().From(TblParamMapping.Schema.Name).OrderDesc(TblParamMapping.Columns.Id).ExecuteDataSet().
                    Tables[0];
            return query;
        }

        //Load thông tin yeu cau xn lên lưới
        public static DataTable LoadYeuCauXn(string barcode)
        {
            DataTable query =
                new Select(TblYeucauXetnghiemVnio.Columns.TestTypeId, TblYeucauXetnghiemVnio.Columns.Barcode,
                           TblParamMapping.Columns.MedParaName
                    ).From(TblYeucauXetnghiemVnio.Schema.Name).InnerJoin(
                        TblParamMapping.Schema.Name, TblParamMapping.Columns.MedParamID,
                        TblYeucauXetnghiemVnio.Schema.Name, TblYeucauXetnghiemVnio.Columns.IdCanLamSangThucHien).Where(
                            TblYeucauXetnghiemVnio.Columns.Barcode).IsEqualTo(barcode).ExecuteDataSet().Tables[0];
            return query;
        }

        //private Query _Query = TblParamMapping.CreateQuery();

        //Load thông tin lên combobox
        public static DataTable LoadParaname(int deviceId)
        {
            try
            {
                DataTable query =
                    new Select(DDataControl.Columns.DataName, DDataControl.Columns.DataControlId).From(
                        DDataControl.Schema).Where(DDataControl.Columns.DeviceId).IsEqualTo(deviceId).
                        ExecuteDataSet().Tables[0];

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int GetIDHISXetnghiem(int testtypeId)
        {
            int ID_HIS = -1;
            ID_HIS = Utility.Int32Dbnull(new Select(TblAliasMapping.Columns.IdHisXn).From(
                TblAliasMapping.Schema).Where(TblAliasMapping.Columns.TestTypeId).IsEqualTo(testtypeId).
                                             ExecuteScalar(), -1);
            return ID_HIS;
        }

        public static DataTable LoadTestType(int testtypeId)
        {
            try
            {
                DataTable query =
                    new Select().From(
                        TblAliasMapping.Schema).Where(TblAliasMapping.Columns.TestTypeId).IsEqualTo(testtypeId).
                        ExecuteDataSet().Tables[0];

                return query;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Lấy all thiết bị
        public static DataTable GetAllDevices()
        {
            try
            {
                DataTable dtDevices = new Select().From(DDeviceList.Schema.Name).ExecuteDataSet().Tables[0];
                return dtDevices;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetTestType()
        {
            try
            {
                DataTable dtTestType = new Select().From(TTestTypeList.Schema.Name).ExecuteDataSet().Tables[0];
                return dtTestType;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region public Method

        private static DataTable loadMabenhnhan()
        {
            DataTable dtmabenhnhan =
                new Select(TblHisLisPatientInfoVnio.Columns.MaBenhNhan).From(TblHisLisPatientInfoVnio.Schema.Name).
                    ExecuteDataSet().Tables[0];
            return dtmabenhnhan;
        }

        public static void UpdateTrangthaiKetqua()
        {
        }

        //Insert tblThongTinBN vào db
        public static int InsertPatientInfo(string maBn, string tenBn, int tuoi, bool gioitinh, string diachi,
                                            string giuong, string khoa, int idKhoa, string buong, int idBacSydieuTri,
                                            string bacSyDieuTri, bool noiTru, string phong, DateTime testdate,
                                            string sophieu, string datra, bool dichVu, string chandoan, int idDoiTuongBenhNhan)
        {
            try
            {
                SPs.MedLisPatientupdate(maBn, tenBn, diachi, khoa, phong, globalVariables.SysDate.Year - tuoi, tuoi,gioitinh,testdate, idDoiTuongBenhNhan, chandoan).Execute();
                return
                    
                    SPs.SpInsertHisLisVNIO(maBn, tenBn, tuoi, gioitinh, diachi, giuong, khoa, idKhoa, buong,
                                           idBacSydieuTri, bacSyDieuTri, noiTru, phong, testdate, sophieu, datra, dichVu,idDoiTuongBenhNhan)
                        .Execute();
            }
            catch (Exception)
            {
                return -1;
            }
        }


        public static int UpdateInsertpatient(string maBn, string tenBn, int tuoi, bool gioitinh, string diachi,
                                              string giuong, string khoa, int idKhoa, string buong, int idBacSydieuTri,
                                              string bacSyDieuTri, bool noiTru, string phong, DateTime testdate,
                                              string sophieu, string datra, bool dichvu, int idDoiTuongBenhNhan)
        {
            try
            {
                return
                    SPs.SpInsertHisLisVNIO(maBn, tenBn, tuoi, gioitinh, diachi, giuong, khoa, idKhoa, buong,
                                           idBacSydieuTri, bacSyDieuTri, noiTru, phong, testdate, sophieu, datra, dichvu, idDoiTuongBenhNhan)
                        .Execute();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int InserttblYeucauXN(int idcls, short thuchiencho, int trangthaitt, short yeucauxnid, int id,
                                            string mabn, string barcode, int testtypeid, string sophieu,
                                            DateTime testdate, bool istestname, bool? sendhis)
        {
            try
            {
                //DateTime strdate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                return
                    SPs.SpInsertYeuCauXNVnio(idcls, thuchiencho, trangthaitt, yeucauxnid, id, mabn, barcode, testtypeid,
                                             sophieu, testdate, istestname, sendhis).Execute();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int UpdateStatusHis(int id, string mabn, DateTime testdate, bool? sendhis)
        {
            try
            {
                //DateTime strdate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy"));
                return new Update(TblYeucauXetnghiemVnio.Schema)
                    .Set(TblYeucauXetnghiemVnio.Columns.TestDate).EqualTo(testdate)
                    .Set(TblYeucauXetnghiemVnio.Columns.SendStatus).EqualTo(sendhis)
                    .Where(TblYeucauXetnghiemVnio.Columns.MaBenhNhan).IsEqualTo(mabn)
                    .And(TblYeucauXetnghiemVnio.Columns.Id).IsEqualTo(id).Execute();
                // SPs.SpUpdateStatusToHisVnio( id,mabn,  testdate, sendhis).Execute();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DateTime Getdate(DateTime testdate)
        {
            SPs.GetAllPatientVnio(testdate).Execute();
            return testdate;
        }

        public static DataSet GetRegTest(string mabenhnhan, int testTypeId, DateTime date)
        {
            return SPs.SpGetRegVNIO(mabenhnhan, testTypeId, date).GetDataSet();
        }

        //public static DataSet GetResultDetail(string sophieu)
        //{

        //        return SPs.GetResultDetailVnio(sophieu).GetDataSet();

        //}

        public static DataSet GetResultDetailV(string sophieu, string name, int gioitinh, DateTime fromtestdate,
                                               DateTime toTestdate, bool check, int testTypeId, int HasTest)
        {
            return
                SPs.GetResultDetailVnioV2(sophieu, name, gioitinh, fromtestdate, toTestdate, check, testTypeId, HasTest)
                    .GetDataSet();
        }

        //public static DataSet GetPatientAndTestInfo(string sophieu, string name, int gioitinh, DateTime fromtestdate,
        //                                      DateTime toTestdate, bool check, int testTypeId, int HasTest)
        //{
        //    return
        //        SPs.GetPatientAndTestInfoVnio(sophieu, name, gioitinh, fromtestdate, toTestdate, check, testTypeId, HasTest)
        //            .GetDataSet();
        //}

        //public static DataTable GetResultByMaBenhNhan(string sophieu, DateTime fromtestdate,
        //                                      DateTime toTestdate, bool check, int testTypeId, int HasTest, string maBenhNhan)
        //{
        //    try
        //    {
        //        return
        //                SPs.GetResultDetailVnioV3(sophieu, fromtestdate, toTestdate, check, testTypeId, HasTest, maBenhNhan)
        //                    .GetDataSet().Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        //public static DataSet GetResultDetailV(string sophieu, DateTime testdate, bool check)
        //{

        //    return SPs.GetResultDetailVnioV2(sophieu, testdate, check, -100).GetDataSet();

        //}
        //Insert Yêu cầu XN

        public static ActionResult InsertYeuCauXn(TblYeucauXetnghiemVnio[] arrYeucauxetnghiem)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        foreach (TblYeucauXetnghiemVnio objYeucauXetnghiem in arrYeucauxetnghiem)
                        {
                            TblYeucauXetnghiemVnioCollection objYeucauXetnghiemCollection =
                                new TblYeucauXetnghiemVnioController().FetchByQuery(
                                    TblYeucauXetnghiemVnio.CreateQuery().AddWhere(
                                        TblYeucauXetnghiemVnio.Columns.IdCanLamSangThucHien, Comparison.Equals,
                                        objYeucauXetnghiem.IdCanLamSangThucHien));
                            if (objYeucauXetnghiemCollection.Count <= 0)
                            {
                                objYeucauXetnghiem.IsNew = true;
                                objYeucauXetnghiem.Save();
                            }
                            else
                            {
                                new Update(TblYeucauXetnghiemVnio.Schema)
                                    .Set(TblYeucauXetnghiemVnio.Columns.Id).EqualTo(objYeucauXetnghiem.Id)
                                    .Set(TblYeucauXetnghiemVnio.Columns.YeuCauXetNghiemId).EqualTo(
                                        objYeucauXetnghiem.YeuCauXetNghiemId)
                                    .Set(TblYeucauXetnghiemVnio.Columns.ThucHienCho).EqualTo(
                                        objYeucauXetnghiem.ThucHienCho)
                                    .Set(TblYeucauXetnghiemVnio.Columns.TrangThaiThucHien).EqualTo(
                                        objYeucauXetnghiem.TrangThaiThucHien)
                                    .Set(TblYeucauXetnghiemVnio.Columns.MaBenhNhan).EqualTo(
                                        objYeucauXetnghiem.MaBenhNhan)
                                    .Set(TblYeucauXetnghiemVnio.Columns.Barcode).EqualTo(objYeucauXetnghiem.Barcode)
                                    .Set(TblYeucauXetnghiemVnio.Columns.TestTypeId).EqualTo(
                                        objYeucauXetnghiem.TestTypeId)
                                    .Set(TblYeucauXetnghiemVnio.Columns.Sophieu).EqualTo(
                                        objYeucauXetnghiem.Sophieu)
                                    .Set(TblYeucauXetnghiemVnio.Columns.TestDate).EqualTo(
                                        objYeucauXetnghiem.TestDate)
                                    .Set(TblYeucauXetnghiemVnio.Columns.IsTestName).EqualTo(
                                        objYeucauXetnghiem.IsTestName)
                                    .Where(TblYeucauXetnghiemVnio.Columns.IdCanLamSangThucHien).IsEqualTo(
                                        objYeucauXetnghiem.IdCanLamSangThucHien)
                                    .Execute();
                            }
                        }
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

        //Insert Paramapping vào db

        public static int InsertParaMapping(TblParamMapping objParaMapping)
        {
            int record = -1;
            Query _QueryMapping = TblParamMapping.CreateQuery();
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        //TblParamMappingCollection objCollection =new TblParamMappingController().FetchByQuery(
                        //        TblParamMapping.CreateQuery().AddWhere(TblParamMapping.Columns.MedParamID,
                        //                                               Comparison.Equals, objParaMapping.MedParamID).AND
                        //            (TblParamMapping.Columns.LisParaName, Comparison.Equals, objParaMapping.LisParaName)
                        //            .AND(TblParamMapping.Columns.DeviceId, Comparison.Equals, objParaMapping.DeviceId));

                        SqlQuery q = new Select().From(TblParamMapping.Schema)
                            .Where(TblParamMapping.Columns.MedParamID).IsEqualTo(objParaMapping.MedParamID)
                            .And(TblParamMapping.Columns.LisParaName).IsEqualTo(objParaMapping.LisParaName)
                            .And(TblParamMapping.Columns.DeviceId).IsEqualTo(objParaMapping.DeviceId);
                        if (q.GetRecordCount() <= 0)
                        {
                            objParaMapping.IsNew = true;
                            objParaMapping.Save();
                            record = Utility.Int32Dbnull(_QueryMapping.GetMax(TblParamMapping.Columns.Id), -1);
                        }
                        //else
                        //{
                        //    new Update(TblParamMapping.Schema)
                        //        .Set(TblParamMapping.Columns.LisParaName).EqualTo(objParaMapping.LisParaName)
                        //        .Set(TblParamMapping.Columns.DeviceId).EqualTo(objParaMapping.DeviceId)
                        //        .Set(TblParamMapping.Columns.MedParaName).EqualTo(objParaMapping.MedParaName)
                        //        .Set(TblParamMapping.Columns.IsTestName).EqualTo(objParaMapping.IsTestName)
                        //        .Where(TblParamMapping.Columns.MedParamID).IsEqualTo(objParaMapping.MedParamID).Execute();
                        //}
                    }

                    scope.Complete();
                    //record = 1;
                }
            }
            catch (Exception ex)
            {
                record = -1;
            }
            return record;
        }

        //Lấy ID XN từ LocalAlias
        public static int GetTestTypeIdFromLocalAlias(string localAlias)
        {
            object strResult =
                new Select(TblAliasMapping.Columns.TestTypeId, TblAliasMapping.Columns.LocalAlias).From(
                    TblAliasMapping.Schema.Name).Where(
                        TblAliasMapping.Columns.LocalAlias).IsEqualTo(
                            localAlias).ExecuteScalar();
            try
            {
                if (strResult != null)
                {
                    int result = Convert.ToInt32(strResult);
                    return result;
                }
                return -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int GetIdHisXN(string localalias)
        {
            try
            {
                object strIdXn =
                    new Select(TblAliasMapping.Columns.IdHisXn, TblAliasMapping.Columns.LocalAlias).From(
                        TblAliasMapping.Schema).Where(
                            TblAliasMapping.Columns.LocalAlias).IsEqualTo(localalias).ExecuteScalar();
                if (strIdXn != null)
                {
                    int id = Convert.ToInt32(strIdXn);
                    return Convert.ToInt32(id);
                }
                return -1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetIdHis()
        {
            DataTable dtidhis = new Select().From(TblAliasMapping.Schema).ExecuteDataSet().Tables[0];
            return dtidhis;
        }

        //Lay ma id xn
        public static DataRow GetParaNameFromParaId(string id)
        {
            try
            {
                DataRow row =
                    new Select(TblParamMapping.Columns.MedParaName, TblParamMapping.Columns.DeviceId,
                               TblParamMapping.Columns.LisParaName).From(
                                   TblParamMapping.Schema.Name).Where(
                                       TblParamMapping.Columns.MedParamID).IsEqualTo(id).ExecuteDataSet().Tables[0].Rows
                        [0];
                return row;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string GetAliasNameFromParaNameAndDeviceId(string paraName, string deviceId)
        {
            try
            {
                object strAlias = new Select(DDataControl.Columns.AliasName).From(DDataControl.Schema.Name).Where(
                    DDataControl.Columns.DeviceId).IsEqualTo(deviceId).And(DDataControl.Columns.DataName).IsEqualTo(
                        paraName).ExecuteScalar();
                return Convert.ToString(strAlias);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        //Lấy Barcode từ tblYeucauXN
        public static int GetBarcodeFromYeucauXn(string id)
        {
            try
            {
                object strBarcode =
                    new Select(TblYeucauXetnghiemVnio.Columns.Barcode).From(TblYeucauXetnghiemVnio.Schema.Name).Where(
                        TblYeucauXetnghiemVnio.Columns.Barcode).IsEqualTo(id).ExecuteScalar();
                return (int) strBarcode;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable GetPatientByRegisterID(string sophieu, DateTime testDate, bool ok)
        {
            try
            {
                if (ok)
                {
                    DataTable strSophieu =
                        new Select().From(TblHisLisPatientInfoVnio.Schema.Name).Where(
                            TblHisLisPatientInfoVnio.Columns.Sophieu).IsEqualTo(sophieu).ExecuteDataSet().Tables[0];
                    return strSophieu;
                }
                else
                {
                    DataTable strSophieu =
                        new Select().From(TblHisLisPatientInfoVnio.Schema.Name).Where(
                            TblHisLisPatientInfoVnio.Columns.Sophieu).Like(string.Format("{0}{1}{0}", "%", sophieu)).And
                            (
                                TblHisLisPatientInfoVnio.Columns.TestDate).IsEqualTo(testDate)
                            .ExecuteDataSet().Tables[0];
                    return strSophieu;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Xóa loại xn
        public static void DeleteParaName(int pId)
        {
            try
            {
                if (TblParamMapping.FetchByID(pId) != null)
                {
                    TblParamMapping.Delete(pId);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Insert vào Test_Info old run
        public static void InsertTestInfor(int testTypeId, string patientCode, DateTime testDate, ref string barcode,
                                           ref int testId)
        {
            //Lấy về mã BN làm XN
            object strpatientId =
                new Select(LPatientInfo.Columns.PatientId).From(LPatientInfo.Schema.Name).Where(LPatientInfo.Columns.Pid)
                    .IsEqualTo(patientCode).ExecuteScalar();
            if (String.IsNullOrEmpty(strpatientId.ToString().Trim()))
            {
                strpatientId = -1;
            }
            int patientId = Convert.ToInt32(strpatientId);

            //Lấy về TestTypeOrder
            object strTestTypeOrder =
                new Select(TTestTypeList.Columns.IntOrder).From(TTestTypeList.Schema.Name).Where(
                    TTestTypeList.Columns.TestTypeId).IsEqualTo(testTypeId).ExecuteScalar();
            if (String.IsNullOrEmpty(strTestTypeOrder.ToString().Trim()))
            {
                strTestTypeOrder = 0;
            }
            int intTestTypeOrder = Convert.ToInt32(strTestTypeOrder);

            //Lấy về Barcode của XN được đăng ký:
            //object strBarcode =
            //    new Select(TTestInfo.Columns.Barcode).From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.PatientId).
            //        IsEqualTo(patientId).ExecuteScalar();

            DateTime now = DateTime.Now;
            var mintick = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 000);
            var maxtick = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59);


            object strBarcode =
                new Select(TTestInfo.Columns.Barcode).From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.PatientId).
                    IsEqualTo(patientId).And(TTestInfo.Columns.TestDate).IsBetweenAnd(mintick, maxtick)
                    .ExecuteScalar();

            try
            {
                if (String.IsNullOrEmpty(strBarcode.ToString().Trim()))
                {
                    if (LablinkServiceConfig.GetTestTypeBarcode().Equals("False"))
                    {
                        barcode = testDate.ToString("yyMMdd") + VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
                    }
                    else
                    {
                        barcode = testDate.ToString("yyMMdd") + VB6.Right("00" + intTestTypeOrder, 2) +
                                  VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
                    }
                }
                else
                {
                    if (LablinkServiceConfig.GetTestTypeBarcode().Equals("False"))
                    {
                        barcode = testDate.ToString("yyMMdd") + VB6.Right("0000" + strBarcode, 4);
                    }
                    else
                    {
                        barcode = testDate.ToString("yyMMdd") + VB6.Right("00" + intTestTypeOrder, 2) +
                                  VB6.Right("0000" + strBarcode, 4);
                    }
                }
            }
            catch (Exception ex)
            {
                if (LablinkServiceConfig.GetTestTypeBarcode().Equals("False"))
                {
                    barcode = testDate.ToString("yyMMdd") + VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
                }
                else
                {
                    barcode = testDate.ToString("yyMMdd") + VB6.Right("00" + intTestTypeOrder, 2) +
                              VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
                }
            }

            //Insert vao DB
            //code old
            SpInsertTestInfor(testTypeId, patientId, barcode, testDate, ref testId);
            // SpInsertTestInfor(testTypeId, patientId, barcode, testDate);
        }

        //public static void InsertTestInfor(int testTypeId, string patientCode, DateTime testDate, ref string barcode)
        //{
        //    //Lấy về mã BN làm XN
        //    object strpatientId =
        //        new Select(LPatientInfo.Columns.PatientId).From(LPatientInfo.Schema.Name).Where(LPatientInfo.Columns.Pid)
        //            .IsEqualTo(patientCode).ExecuteScalar();
        //    if (String.IsNullOrEmpty(strpatientId.ToString().Trim()))
        //    {
        //        strpatientId = -1;
        //    }
        //    int patientId = Convert.ToInt32(strpatientId);

        //    //Lấy về TestTypeOrder
        //    object strTestTypeOrder =
        //        new Select(TTestTypeList.Columns.IntOrder).From(TTestTypeList.Schema.Name).Where(
        //            TTestTypeList.Columns.TestTypeId).IsEqualTo(testTypeId).ExecuteScalar();
        //    if (String.IsNullOrEmpty(strTestTypeOrder.ToString().Trim()))
        //    {
        //        strTestTypeOrder = 0;
        //    }
        //    int intTestTypeOrder = Convert.ToInt32(strTestTypeOrder);

        //    //Lấy về Barcode của XN được đăng ký:
        //    object strBarcode =
        //        new Select(TTestInfo.Columns.Barcode).From(TTestInfo.Schema.Name).Where(TTestInfo.Columns.PatientId).
        //            IsEqualTo(patientId).ExecuteScalar();

        //    try
        //    {
        //        if (String.IsNullOrEmpty(strBarcode.ToString().Trim()))
        //        {
        //            if (LablinkServiceConfig.GetTestTypeBarcode().Equals("False"))
        //            {
        //                barcode = testDate.ToString("yyMMdd") + VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
        //            }
        //            else
        //            {
        //                barcode = testDate.ToString("yyMMdd") + VB6.Right("00" + intTestTypeOrder, 2) +
        //                          VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
        //            }
        //        }
        //        else
        //        {
        //            if (LablinkServiceConfig.GetTestTypeBarcode().Equals("False"))
        //            {
        //                barcode = testDate.ToString("yyMMdd") + VB6.Right("0000" + strBarcode, 4);
        //            }
        //            else
        //            {
        //                barcode = testDate.ToString("yyMMdd") + VB6.Right("00" + intTestTypeOrder, 2) +
        //                          VB6.Right("0000" + strBarcode, 4);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (LablinkServiceConfig.GetTestTypeBarcode().Equals("False"))
        //        {
        //            barcode = testDate.ToString("yyMMdd") + VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
        //        }
        //        else
        //        {
        //            barcode = testDate.ToString("yyMMdd") + VB6.Right("00" + intTestTypeOrder, 2) +
        //                      VB6.Right("0000" + (GetMaxBarcode(testDate) + 1), 4);
        //        }
        //    }

        //    //Insert vao DB

        //    SpInsertTestInfor(testTypeId, patientId, barcode, testDate);
        //}


        //Lấy ngày tháng năm làm barcode [Used]
        public static int GetMaxBarcode(DateTime testDate)
        {
            try
            {
                StoredProcedure sp = SPs.SpGetMaxBarcode(testDate.ToString("yyMMdd"));
                object id = sp.ExecuteScalar();
                if (string.IsNullOrEmpty(id.ToString()))
                {
                    return 0;
                }
                if (Convert.ToInt32(id) == -1)
                {
                    return 0;
                }
                return Convert.ToInt32(id);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        //Update tbl PatienInfo
        //public static void UpdatePatientInfo(string patientId, string patientName, string diachi, bool? pSex, int pAge,
        //                                     int pYearOfBirth, DateTime testdate)
        //{
        //    try
        //    {
        //        StoredProcedure sps = SPs.MedLisPatientupdate(patientId, patientName, diachi, pAge, pYearOfBirth, pSex,
        //                                                      testdate);
        //        sps.Execute();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        /// <summary>
        /// /Hàm stored InsertTestInfo
        /// </summary>
        /// <param name="testTypeId"></param>
        /// <param name="patientId"></param>
        /// <param name="barCode"></param>
        /// <param name="testDate"></param>
        private static void SpInsertTestInfor(int testTypeId, int patientId, string barCode, DateTime testDate,
                                              ref int testId)
        {
            try
            {
                StoredProcedure sps = SPs.MedlisInsertTestinfor(testTypeId, patientId, barCode,
                                                                testDate.ToString("dd/MM/yyyy"), testId);
                sps.Execute();
                testId = Convert.ToInt32(sps.OutputValues[0]);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        //private static void SpInsertTestInfor(int testTypeId, int patientId, string barCode, DateTime testDate)
        //{
        //    try
        //    {

        //        StoredProcedure sps = SPs.MedlisInsertTestinfor(testTypeId, patientId, barCode,
        //                                                        testDate.ToString("dd/MM/yyyy"));
        //        sps.Execute();

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public static DataTable GetResult(string sophieu)
        {
            DataTable strResult =
                new Select().From(TblYeucauXetnghiemVnio.Schema.Name).Where(TblYeucauXetnghiemVnio.Columns.Sophieu).
                    IsEqualTo(sophieu).ExecuteDataSet().Tables[0];
            return strResult;
        }

        // lấy mã trị số his True, false
        public static bool GetIsTestName(string medparaId)
        {
            try
            {
                object strIsTestname =
                    new Select(TblParamMapping.Columns.IsTestName).From(TblParamMapping.Schema.Name).Where(
                        TblParamMapping.Columns.MedParamID).IsEqualTo(medparaId).ExecuteScalar();
                if (string.IsNullOrEmpty(strIsTestname.ToString()))
                {
                    return false;
                }
                else
                {
                    return (bool) strIsTestname;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable GetResultByTestIdBarCodeAndIsTestName(string idCanLamSangThucHien, string barcode,
                                                                      bool isTestName)
        {
            try
            {
                if (!isTestName)
                {
                    object lispara =
                        new Select(TblParamMapping.Columns.LisParaName).From(TblParamMapping.Schema.Name).Where(
                            TblParamMapping.Columns.MedParaName).IsEqualTo(idCanLamSangThucHien).ExecuteScalar();
                    if (lispara == null)
                    {
                        return null;
                    }
                    DataTable resul =
                        new Select().From(TResultDetail.Schema.Name).Where(TResultDetail.Columns.ParaName).IsEqualTo
                            (lispara).And(TResultDetail.Columns.Barcode).IsEqualTo(barcode).ExecuteDataSet().Tables[
                                0];
                    return resul;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateParaMaping(TblParamMapping pitems)
        {
            try
            {
                if (TblParamMapping.FetchByID(pitems.Id) != null)
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

        public static int InsertRegList(int testId, string aliasName, string barcode, string paraName, bool status,
                                        int deviceId)
        {
            try
            {
                return SPs.SpCreateRegList(testId, deviceId, barcode, aliasName, paraName, true).Execute();
            }
            catch (Exception)
            {
                return 0;
            }
        }


        /// <summary>
        /// Lấy về tên loại đối tượng từ ID
        /// </summary>
        /// <param name="id">Mã đối tượng</param>
        /// <returns>Tên loại đối tượng</returns>
        public static string GetObjectNameFromObjectId(string id)
        {
            var result = "";
            try
            {
                DataTable strResult =
                new Select(LObjectType.Columns .SName).From(LObjectType.Schema.Name).Where(LObjectType .Columns.Id).
                    IsEqualTo(id).ExecuteDataSet().Tables[0];
                if (strResult != null)
                {
                    if (strResult.Rows.Count>0)
                    {
                        result = strResult.Rows[0][0].ToString();
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }


        #endregion
    }
}