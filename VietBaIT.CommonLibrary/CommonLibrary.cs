using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using Microsoft.VisualBasic;
using System.IO;
using System.Reflection;
using System.Collections;
using Microsoft.Win32;
using SubSonic;
using System.Data.SqlClient;
using System.Drawing;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Mabry.Windows.Forms.Barcode;
using System.Net;
using Janus.Windows.GridEX;

namespace VietBaIT.CommonLibrary
{
    /// <summary>
    /// Biến xác định xem Form được gọi từ nguồn nào
    /// </summary>
    public enum CallAction { FromMenu = 0, FromParentFormList = 1, FromAnotherForm = 2 };
    public enum actionHeathCare { FromHeath = 0, FromTreat = 1};
    /// <summary>
    /// Kiểu kết quả trả về khi thực hiện các lệnh DML với CSDL
    /// </summary>
    public enum ActionResult { ActAsInput = 1, DataHasUsedinAnotherTable = 2, Error = 3, Exceed = 4, Exception = 5, ExistedRecord = 6, Success = 7, NotEnoughDrugInStock = 8, UNKNOW = 9 };
    /// <summary>
    /// Kiểu dữ liệu thao tác
    /// </summary>
    public enum action {Insert = 0, Update = 1, Delete = 2, Select = 3, FirstOrFinished = 4, Normal = 5, Error = -1, doNothing = 6, ConfirmData = 7 };
    public enum actionExamPres {PresExam=1,PresNoExam=2 };
    public enum ParamsPaymentType { RegExam = 1, Pres = 4 };
    
    /// <summary>
    /// Drug status in stock
    /// </summary>
    public enum DRUG_STATUS { Available = 0, OutOfStock = 1, Expired = 2 }
    public enum ActionObject { Drug = 0, Surgery = 1, Service = 2, DetailService = 3, Material = 4, Department = 5 }
    /// <summary>=5
    /// Kiểu tiến trình đang làm việc. Được dùng khi việc Search dữ liệu để Report hoặc hiện thị hoặc làm việc nào đó mất nhiều time
    /// Form Progress sẽ hiển thị lên và thông báo tiến trình HIS đang thực hiện là gì để User biết đỡ nhàm chán
    /// </summary>
    public enum ProcessStatus { PreparingData = 0, SetValueforEntity = 1, RequestingData = 2, ProcessDataBeforeUsing = 3, PushDataIntoDataGridview = 4, PushDataIntoReport = 5, WritetoFile = 6 };
    public enum CodeGeneratorStatus { OpenConnection = 0, LoadTables = 1, LoadColumns = 2, LoadContraints = 3, WriteController = 4, WriteInfor = 5 };

    //----------------------------------------------------------------------------
    //-----------------------------NEW CLASS--------------------------------------
    //----------------------------------------------------------------------------
    ///<summary>
    ///<para>Lớp Utility chứa các hàm dùng chung dùng cho việc lập trình</para>
    ///</summary>
    public class Utility
    {

        #region "PHAN CODE CỦA ANH CƯỜNG"
        public static bool IsCheckConnected(string strservers, string DbName, string users, string password)
        {

            string strsql = "";
            // Common.cTripleDES MaHoa = new cTripleDES();
            bool flag = false;
            //string servers = MaHoa.Decrypt(strservers);
            //string DataName = MaHoa.Decrypt(DbName);
            //string User_ID = MaHoa.Decrypt(users);
            //string Password = MaHoa.Decrypt(password);               
            strsql += "packet size=4096;workstation id=" + strservers;
            strsql += ";data source=" + strservers;
            strsql += " ;persist security info=False;initial catalog=" + DbName;
            strsql += ";uid=" + users;
            strsql += ";pwd=" + password;
            SqlConnection cnn = new SqlConnection(strsql);
            ///Mở CSDL
            try
            {
                // Me.Cursor = Cursors.WaitCursor
                // this.Cursor = Cursors.WaitCursor;
                cnn.Open();
                //  MessageBox.Show("Kết nối thành công. Chúc mừng bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cnn.Close();
                cnn.Dispose();
                // cmdSave.Focus();
                //this.Cursor = Cursors.Default;
                flag = true;
            }

            catch
            {
                flag = false;
            }
            return flag;
        }
        /// <summary>
        /// Khởi tạo Subsonic
        /// </summary>
        /// <param name="globalVariables.SqlConnstr">Connection String kết nối tới CSDL</param>
        /// <param name="ProviderName">Tên Provider mặc định là ORM</param>
        public static void InitSubSonic(string SqlConnstr, string ProviderName)
        {
            DataService.Providers = new DataProviderCollection();
            var myProvider = new CustomSqlProvider(SqlConnstr);
            if (SubSonic.DataService.Providers[ProviderName] == null)
            {
                DataService.Providers.Add(myProvider);
                DataService.Provider = myProvider;
            }
            else
            {

                DataService.Provider.DefaultConnectionString = SqlConnstr;

            }
        }
        /// <summary>
        /// Thêm mới một Datarow có giá trị cột DisplayValueName=String.Empty và cột SelectedValueName=-1
        /// Được dùng chủ yếu trong mục đích bind dữ liệu từ một danh mục vào Combobox với phần tử hiển thị đầu tiên có giá trị=FirstValueToDisplay
        /// Bảng phải thỏa mãn có các cột sau
        /// Cột 1 tên:SelectedValueName
        /// Cột 2 tên:DisplayValueName
        /// Cột 3 tên:IntOrder
        /// Các cột còn lại phải allownull=true
        /// </summary>
        /// <param name="dt">Bảng chưa dữ liệu danh mục khi chưa thêm gì cả</param>
        /// <param name="SelectedValueName">Tên cột chứa Giá trị cần lấy(Thường là cột ID)</param>
        /// <param name="DisplayValueName">Tên cột chứa giá trị hiển thị(Thường là cột Name)</param>
        public static void AddColumnAlltoDataTable(ref DataTable dt, string SelectedValueName, string DisplayValueName, string FirstValueToDisplay)
        {
            if (dt == null) return;
            DataRow dr = dt.NewRow();
            dr[SelectedValueName] = -1;
            dr[DisplayValueName] = FirstValueToDisplay;
            //L_Stocks
            if (dt.Columns.Contains("Speciality")) dr["Speciality"] = 1;
            if (dt.Columns.Contains("SpecMoney")) dr["SpecMoney"] = 1;
            //LRooms
            if (dt.Columns.Contains("Room_Fee")) dr["Room_Fee"] = 1;
            if (dt.Columns.Contains("Room_Code")) dr["Room_Code"] = "";
            //List Objects
            if (dt.Columns.Contains("IntOrder")) dr["IntOrder"] = 0;
            if (dt.Columns.Contains("Stock_Type")) dr["Stock_Type"] = Convert.ToByte("0");
            //SysRoles
            if (dt.Columns.Contains("iParentRole")) dr["iParentRole"] = 1;
            if (dt.Columns.Contains("iParentRole")) dr["iParentRole"] = 1;
            if (dt.Columns.Contains("fp_sbranchID")) dr["fp_sbranchID"] = globalVariables.Branch_ID;
            //LStaffs
            if (dt.Columns.Contains("StaffType_ID")) dr["StaffType_ID"] = 1;
            if (dt.Columns.Contains("Department_ID")) dr["Department_ID"] = -1;
            if (dt.Columns.Contains("Rank_ID")) dr["Rank_ID"] = 1;
            if (dt.Columns.Contains("Status")) dr["Status"] = 1;
            //LDepartments
            if (dt.Columns.Contains("Speciality")) dr["Speciality"] = 1;
            if (dt.Columns.Contains("Parent_ID")) dr["Parent_ID"] = -1;
            if (dt.Columns.Contains("Stock_Code")) dr["Stock_Code"] = "";
            if (dt.Columns.Contains("Stock_Nature")) dr["Stock_Nature"] = 0;
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        public static void AddColumnAlltoUserDataTable(ref DataTable dt, string SelectedValueName, string FirstValueToDisplay)
        {
            if (dt == null) return;
            DataRow dr = dt.NewRow();
            dr[SelectedValueName] = FirstValueToDisplay;
            if (dt.Columns.Contains("FP_sBranchID")) dr["FP_sBranchID"] = "";
            dt.Rows.Add(dr);
            dt.AcceptChanges();
        }
        /// <summary>
        /// Lấy về số các phần tử được Check trong các Datatable có cột Chọn(CheckColumnName)
        /// </summary>
        /// <param name="dt">Data chứa dữ liệu</param>
        /// <param name="CheckColumnName">Tên cột GridCheckboxColumn</param>
        /// <returns></returns>
        public static int GetItemsChecked(DataTable dt, string CheckColumnName)
        {
            int reval = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[CheckColumnName] != DBNull.Value && dr[CheckColumnName] != null && Convert.ToInt16(dr[CheckColumnName]) == 1) reval += 1;
            }
            return reval;
        }
        /// <summary>
        /// HAM THỰC HIỆN HIÊN THỊ LABLEL
        /// </summary>
        /// <param name="lblMsg"></param>
        /// <param name="Message"></param>
        /// <param name="isErr"></param>
        public static void SetMsg(Label lblMsg, string Message, bool isErr)
        {
            if (isErr)
            {
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.ForeColor = Color.DarkBlue;
            }
            lblMsg.Text = Message;
        }

        public static void SetMsg(ToolStripLabel lblMsg, string Message, bool isErr)
        {
            if (isErr)
            {
                lblMsg.ForeColor = Color.Red;
            }
            else
            {
                lblMsg.ForeColor = Color.DarkBlue;
            }
            lblMsg.Text = Message;
        }

        public static byte ByteDbnull(object obj)
        {
            if (!(((obj != null) && (obj != DBNull.Value)) && IsNumeric(obj)))
            {
                return 0;
            }
            return Convert.ToByte(obj);
        }

        public static void TryToFilter(DataView dv, string RowFilter)
        {
            try
            {
                dv.RowFilter = RowFilter;
            }
            catch
            {
                dv.RowFilter = "1=1";
            }
        }
        /// <summary>
        /// Lấy về số các phần tử được Check trong các Datatable có cột Chọn(CheckColumnName)
        /// </summary>
        /// <param name="dt">Data chứa dữ liệu</param>
        /// <param name="CheckColumnName">Tên cột GridCheckboxColumn</param>
        /// <returns></returns>
        public static int GetItemsChecked(DataView dtv, string CheckColumnName)
        {
            int reval = 0;
            foreach (DataRowView drv in dtv)
            {
                if (drv[CheckColumnName] != DBNull.Value && drv[CheckColumnName] != null && Convert.ToInt16(drv[CheckColumnName]) == 1) reval += 1;
            }
            return reval;
        }
        /// <summary>
        /// Hàm dùng để binding giá trị nhập của một Textbox chỉ được phép là chữ số
        /// Hàm này được gọi trong sự kiện Keypress
        /// </summary>
        /// <param name="pstrChar">e.Keychar</param>
        /// <param name="oTextBox">TextBox cần binding</param>
        /// <returns></returns>
        public static bool NumbersOnly(Char pstrChar, TextBox oTextBox)
        {
            if (pstrChar.ToString() != Constants.vbBack)
            {
                return IsNumeric(pstrChar) || IsNumeric(pstrChar.ToString() + oTextBox.Text) ? false : true;
            }
            return false;
        }
       
        /// <summary>
        /// Trả về DataRow trong bảng dt thỏa mãn điều kiện lọc filterExpression
        /// </summary>
        /// <param name="dt">Bảng chứa dữ liệu cần tìm</param>
        /// <param name="filterExpression">Điều kiện lọc</param>
        /// <returns></returns>
        public static DataRow FetchOnebyCondition(DataTable dt, string filterExpression)
        {
            try
            {
                DataRow[] dr = dt.Select(filterExpression);
                if (dr.GetLength(0) > 0) return dr[0];
                else return null;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Lấy về các nhân viên dựa vào loại nhân viên
        /// </summary>
        /// <returns></returns>
        private static string GetStaffRowFilter(string StaffType)
        {
            string Reval = "";
            string[] Args = StaffType.Split(',');
            for (int i = 0; i <= Args.Length - 1; i++)
            {
                if (!Utility.IsNumeric(Args[i])) return "1=1";
                Reval = Reval + " StaffType_ID=" + Args[i] + " OR ";
            }
            Reval = Reval.Substring(0, Reval.LastIndexOf(" OR "));
            return Reval;
        }
        /// <summary>
        /// Lấy về các nhân viên là bảo vệ dựa vào biến hệ thống có tên Guardian
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyGuardians()
        {
            return GetStaffRowFilter(globalVariables.Guardian);
        }
        /// <summary>
        /// Lấy về các nhân viên là Thư ký dựa vào biến hệ thống có tên Clerk
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyClerks()
        {
            return GetStaffRowFilter(globalVariables.Clerk);
        }
        /// <summary>
        /// Lấy về các nhân viên là Trợ lý dựa vào biến hệ thống có tên Assistant
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyAssistants()
        {
            return GetStaffRowFilter(globalVariables.Assistant);
        }
        /// <summary>
        /// Lấy về các nhân viên là nhân viên tiếp tân dựa vào biến hệ thống có tên Receptionist
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyReceptionists()
        {
            return GetStaffRowFilter(globalVariables.Receptionist);
        }
        /// <summary>
        /// Lấy về các nhân viên là bác sĩ chẩn đoán dựa vào biến hệ thống có tên DiagnosticDoctor
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyDiagnosticDoctors()
        {
            return GetStaffRowFilter(globalVariables.DiagnosticDoctor);
        }
        /// <summary>
        /// Lấy về các nhân viên là bác sĩ chỉ định dựa vào biến hệ thống có tên AssignDoctor
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyAssignDoctors()
        {
            return GetStaffRowFilter(globalVariables.AssignDoctor);
        }
        /// <summary>
        /// Lấy về các nhân viên là kế toán dựa vào biến hệ thống có tên Accountant
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyAccountants()
        {
            return GetStaffRowFilter(globalVariables.Accountant);
        }
        /// <summary>
        /// Lấy về các nhân viên là Dược sĩ dựa vào biến hệ thống có tên Druggist
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyDruggists()
        {
            return GetStaffRowFilter(globalVariables.Druggist);
        }

        /// <summary>
        /// Lấy về các nhân viên là bác sĩ dựa vào biến hệ thống có tên Doctors
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyDoctors()
        {
            return GetStaffRowFilter(globalVariables.Doctors);
        }
        /// <summary>
        /// Lấy về các nhân viên là nhân viên kho dựa vào biến hệ thống có tên StoreKeepers
        /// </summary>
        /// <returns></returns>
        public static string GetOnlyStoreKeepers()
        {
            return GetStaffRowFilter(globalVariables.StoreKeepers);
        }

        /// <summary>
        /// Trả về mảng các DataRow trong bảng dt thỏa mãn điều kiện lọc filterExpression
        /// </summary>
        /// <param name="dt">Bảng chứa dữ liệu cần tìm</param>
        /// <param name="filterExpression">Điều kiện lọc</param>
        /// <returns></returns>
        public static DataRow[] FetchAllsbyCondition(DataTable dt, string filterExpression)
        {
            try
            {
                DataRow[] dr = dt.Select(filterExpression);
                if (dr.GetLength(0) > 0) return dr;
                else return null;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Ánh xạ từ Subsonic Object vào DataRow
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dr"></param>
        public static void FromObjectToDatarow(object obj, ref DataRow dr)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (dr == null || obj == null) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in dr.Table.Columns)
                {
                    foreach (System.Reflection.PropertyInfo pro in obj.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public))
                    {
                        if (pro.Name.ToUpper() == Col.ColumnName.ToUpper() || pro.Name.Replace("_", "").ToUpper() == Col.ColumnName.Replace("_", "").ToUpper())
                        {
                            try
                            {
                                dr[Col.ColumnName] = pro.GetValue(obj, null) == null ? DBNull.Value : pro.GetValue(obj, null);
                            }
                            catch
                            {
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }

        ///<summary>
        ///<para>Tự động Mapping các giá trị từ Entity sang ObjectInfor</para>
        ///</summary>   
        /// <param name="ObjectInfor"><para>Đối tượng cần Map giá trị</para></param>     
        /// <param name="Entity"><para>Entity chứa giá trị của Object</para></param>     
        ///<returns>Gắn các giá trị từ Entity vào ObjectInfor</returns>
        /// <summary>
        public static void MapValueFromEntityIntoObjectInfor(object ObjectInfo, DataTable Entity)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (Entity == null || ObjectInfo == null || Entity.Rows.Count <= 0) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in Entity.Columns)
                {
                    foreach (System.Reflection.FieldInfo field in ObjectInfo.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public))
                    {
                        if (field.Name.ToUpper() == Col.ColumnName.ToUpper())
                        {
                            if (Entity.Rows[0][Col.ColumnName] == DBNull.Value || Entity.Rows[0][Col.ColumnName] == null)
                                field.SetValue(ObjectInfo, null);
                            else
                                field.SetValue(ObjectInfo, Entity.Rows[0][Col.ColumnName]);
                            //thoát khỏi vòng lặp sau khi tìm thấy
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }
        /// <summary>
        /// Ánh xạ giá trị từ DataRowView thành giá trị của ObjectInfo
        /// </summary>
        /// <param name="ObjectInfo">Object Info</param>
        /// <param name="Entity">Entity chứa dữ liệu</param>
        public static void MapValueFromEntityIntoObjectInfor(object ObjectInfo, DataRowView DRV)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (DRV == null || ObjectInfo == null) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in DRV.DataView.Table.Columns)
                {

                    foreach (System.Reflection.FieldInfo field in ObjectInfo.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public))
                    {
                        if (field.Name.ToUpper() == Col.ColumnName.ToUpper())
                        {
                            if (DRV[Col.ColumnName] == DBNull.Value || DRV[Col.ColumnName] == null)
                                field.SetValue(ObjectInfo, null);
                            else
                                field.SetValue(ObjectInfo, DRV[Col.ColumnName]);
                            //thoát khỏi vòng lặp sau khi tìm thấy
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }
        /// <summary>
        /// Ánh xạ giá trị từ Entity thành giá trị của ObjectInfo
        /// </summary>
        /// <param name="ObjectInfo">Object Info</param>
        /// <param name="Entity">Entity chứa dữ liệu</param>
        public static void MapValueFromEntityIntoObjectInfor(object ObjectInfo, DataRow Entity)
        {
            try
            {
                //Nếu dữ liệu không hợp lệ thì không làm gì cả
                if (Entity == null || ObjectInfo == null) return;
                //Nếu dữ liệu hợp lệ thì tiến hành set giá trị cho ObjectInfor từ giá trị các cột chứa trong Entity
                foreach (DataColumn Col in Entity.Table.Columns)
                {
                    foreach (System.Reflection.FieldInfo field in ObjectInfo.GetType().GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public))
                    {
                        if (field.Name.ToUpper() == Col.ColumnName.ToUpper())
                        {
                            if (Entity[Col.ColumnName] == DBNull.Value || Entity[Col.ColumnName] == null)
                                field.SetValue(ObjectInfo, null);
                            else
                                field.SetValue(ObjectInfo, Entity[Col.ColumnName]);
                            //thoát khỏi vòng lặp sau khi tìm thấy
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return;
            }
        }
        ///<summary>
        ///<para>Lấy về IPAddress của máy tính thực hiện gọi hàm này. Khi lập trình thì IP này được xác định ngay từ các máy Client(Browse)</para>
        ///</summary>   
        ///<returns>Địa chỉ máy tính. Ví dụ: 10.1.6.60</returns>
        public static string GetIPAddress()
        {
            try
            {
                return System.Net.Dns.Resolve(System.Net.Dns.GetHostName()).AddressList[0].ToString();
            }
            catch (Exception ex)
            {
                return "127.0.0.1";
            }
        }
        /// <summary>
        /// Lấy về tên máy tính người dùng đang chạy hệ thống
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            return System.Environment.MachineName;
        }
        /// <summary>
        /// Lấy về tên tài khoản đăng nhập vào windows
        /// </summary>
        /// <returns></returns>
        public static string GetAccountName()
        {
            return System.Environment.UserName;
        }
        ///<summary>
        ///<para>Lưu vết người dùng</para>
        ///</summary>   
        /// <param name="trace"><para>Đối tượng vết <see cref="HISTrace"/></para></param>     
        ///<returns>
        /// <para>ResultType.SaveTraceSuccess nếu SaveTrace thành công</para>
        /// <para>ResultType.CannotSaveTrace or ResultType.Exception nếu SaveTrace không thành công</para>
        ///</returns>
        public static ActionResult SaveTrace(TraceInfor trace, System.Data.SqlClient.SqlTransaction Trans)
        {
            if (trace == null)
            {
                //Utility.ShowMsg("Bạn phải khởi tạo đối tượng trace=new TraceInfor() trước khi sử dụng Method này");
                //Nếu TraceInfor=null có nghĩa là ko cần theo dõi vết đối với nghiệp vụ đang xét
                return ActionResult.Success;
            }
            return new TraceController(trace).Save(Trans);
        }
        ///<summary>
        ///<para>Lấy về đối tượng vết từ gói tin gửi từ Client lên Application Server: Header</para>
        ///</summary>   
        /// <param name="Header"><para>Bảng chứa thông tin vết. Thường là Header.Tables["Header"] <see cref="DataTable"/></para></param>     
        ///<returns>HISTrace nếu tồn tại Header. Ngược lại trả về null.</returns>
        public static TraceInfor GetTraceObjectFromHeader(DataTable Header)
        {
            TraceInfor trace = new TraceInfor();
            try
            {
                if (Header.Equals(null))
                {
                    //Utility.ShowMsg("Header bạn đưa vào chưa được khởi tạo nên chưa có thông tin về Trace. Đề nghị bạn xem lại...");
                    return null;
                }
                //Read from Header Into Trace
                trace.pID = Int32Dbnull(Header.Rows[0]["ID"], 0);
                trace.pBranchID = CorrectStringValue(sDbnull(Header.Rows[0]["BranchID"]));
                trace.pUserName = CorrectStringValue(sDbnull(Header.Rows[0]["UserName"]));
                trace.pCreatedDate = CorrectStringValue(sDbnull(Header.Rows[0]["CreatedDate"]));
                trace.pIPAddress = CorrectStringValue(sDbnull(Header.Rows[0]["IPAddress"]));
                trace.ComputerName = CorrectStringValue(sDbnull(Header.Rows[0]["ComputerName"]));
                trace.AccountName = CorrectStringValue(sDbnull(Header.Rows[0]["AccountName"]));
                trace.DLLName = CorrectStringValue(sDbnull(Header.Rows[0]["DLLName"]));
                trace.pSubSystemName = CorrectStringValue(sDbnull(Header.Rows[0]["SubSystemName"]));
                trace.pFunctionID = Int16Dbnull(Header.Rows[0]["FunctionID"]);
                trace.FunctionName = CorrectStringValue(sDbnull(Header.Rows[0]["FunctionName"]));
                trace.pTableName = CorrectStringValue(sDbnull(Header.Rows[0]["TableName"]));
                trace.pDesc = CorrectStringValue(sDbnull(Header.Rows[0]["Desc"]));
                trace.pLO = Int16Dbnull(Header.Rows[0]["LOT"], 0);
                return trace;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy đối tượng vết từ Header:\n" + ex.Message);
                return null;
            }
        }
        ///<summary>
        ///<para>Lấy về đối tượng vết từ gói tin gửi từ Client lên Application Server: Header</para>
        ///</summary>   
        /// <param name="Header"><para>Dataset chứa Header chứa thông tin vết. Thường là Header <see cref="DataTable"/></para></param>     
        ///<returns>HISTrace nếu tồn tại Header. Ngược lại trả về null.</returns>
        public static TraceInfor GetTraceObjectFromHeader(DataSet Header)
        {
            TraceInfor trace = new TraceInfor();
            try
            {
                if (Header.Equals(null) || Header.Tables.Count <= 0 || Header.Tables["Header"].Rows.Count <= 0)
                {
                    //Utility.ShowMsg("Header bạn đưa vào chưa được khởi tạo nên chưa có thông tin về Trace. Đề nghị bạn xem lại...");
                    return null;
                }
                //Read from Header Into Trace
                trace.pID = Int32Dbnull(Header.Tables["Header"].Rows[0]["ID"], 0);
                trace.pBranchID = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["BranchID"]));
                trace.pUserName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["UserName"]));
                trace.pCreatedDate = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["CreatedDate"]));
                trace.pIPAddress = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["IPAddress"]));
                trace.ComputerName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["ComputerName"]));
                trace.AccountName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["AccountName"]));
                trace.DLLName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["DLLName"]));
                trace.pSubSystemName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["SubSystemName"]));
                trace.pFunctionID = Int16Dbnull(Header.Tables["Header"].Rows[0]["FunctionID"]);
                trace.FunctionName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["FunctionName"]));
                trace.pTableName = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["TableName"]));
                trace.pDesc = CorrectStringValue(sDbnull(Header.Tables["Header"].Rows[0]["Desc"]));
                trace.pLO = Int16Dbnull(Header.Tables["Header"].Rows[0]["LOT"], 0);
                return trace;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy đối tượng vết từ Header:\n" + ex.Message);
                return null;
            }
        }
        /// <summary>
        /// Các trạng thái được dùng khi tìm kiếm hoặc thao tác với CSDL mất nhiều time
        /// </summary>
        public enum LoadingStatus
        {
            PreparingSearchCondition = 0,
            RequestingData = 1,
            PustDataIntoDataGrid = 2,
            ProcessingDataBeforeUsing = 3,
            PustDataIntoCrystalReport = 4,
            WritetoFile = 5
        }
        /// <summary>
        /// Chuyển giá trị(thường là của một đối tượng Datetime) có giá trị mặc định là MinValue thành null
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static object DateTimeToNUll(DateTime dt)
        {
            if (dt.ToShortDateString() == DateTime.MinValue.ToShortDateString()) return null;
            else return dt;
        }
        /// <summary>
        /// Kiểm tra đối tượng có phải là kiểu chữ số hay không
        /// </summary>
        /// <param name="Value">đối tượng cần kiểm tra</param>
        /// <returns></returns>
        public static bool IsNumeric(object Value)
        {
            return Microsoft.VisualBasic.Information.IsNumeric(Value);
        }
        /// <summary>
        /// Thủ tục thường dùng cho sự kiện Keypress của TextBox để ngăn không cho nhập ký tự
        /// </summary>
        /// <param name="newChar">Thường là e.KeyChar</param>
        /// <param name="CurrentString">Thường là TextObject.Text. Trong đó TextObject là tên của TextObject bạn muốn ràng buộc</param>
        /// <returns></returns>
        public static bool NumbersOnly(char newChar, string CurrentString)
        {
            if (newChar.ToString() != Microsoft.VisualBasic.Constants.vbBack)
            {
                return (IsNumeric(newChar.ToString()) || IsNumeric(newChar.ToString() + CurrentString)) ? false : true;//check if numeric is returned
            }
            return false;// for backspace
        }
        ///<summary>
        ///<para>Thiết lập DataSource cho DataGridView</para>
        ///</summary>   
        /// <param name="DataGridView"><para>GridView hiển thị thông tin <see cref="DataGridView"/></para></param>     
        /// <param name="DataSource"><para>Source của GridView <see cref="DataTable"/></para></param> 
        /// <param name="AutogeneratedColumns"><para>Có tự động sinh cột hay không. Mặc định nên đặt giá trị này=false <see cref="bool"/></para></param>     
        /// <param name="DefaultView"><para>Source là Dataview của DataTable hay không. Nên đặt=true <see cref="bool"/></para></param>     
        /// <param name="RowFilterExpression"><para> Điều kiện lọc dữ liệu từ DefaultView.Nếu ko muốn thì đặt giá trị=Empty<see cref="bool"/></para></param>     
        /// <param name="DefaultViewSort"><para>Thứ tự sắp xếp trên DefaultView.Nếu ko muốn thì đặt giá trị=Empty<see cref="bool"/></para></param>     
        ///<returns></returns>
        public static void SetDataSourceForDataGridView(DataGridView varDataGridView, DataTable DataSource, bool AutogeneratedColumns, bool UseDefaultViewAsDataSource, string RowFilterExpression, string DefaultViewSort)
        {
            try
            {
                if (DataSource == null || DataSource.Columns.Count <= 0) return;
                varDataGridView.DataSource = null;
                varDataGridView.AutoGenerateColumns = false;
                if (UseDefaultViewAsDataSource)
                {
                    if (DefaultViewSort.Trim() != "" && DefaultViewSort.Trim() != string.Empty) DataSource.DefaultView.Sort = DefaultViewSort;
                    if (RowFilterExpression.Trim() != "" && RowFilterExpression.Trim() != string.Empty) DataSource.DefaultView.RowFilter = RowFilterExpression;
                    varDataGridView.DataSource = DataSource.DefaultView;
                }
                else
                {
                    varDataGridView.DataSource = DataSource;
                }
            }
            catch (Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }
       
        /// <summary>
        /// Tự động Select dòng đầu tiên của DataGridView
        /// </summary>
        /// <param name="grd">DataGridView</param>
        public static void SetCurrentCellOfDataGridView(DataGridView grd)
        {
            if (grd.RowCount <= 0) return;
            string FirstVisibleColName = GetFirstColumnVisibleOfDataGridView(grd);
            if (FirstVisibleColName != "")
            {
                grd.CurrentCell = grd[FirstVisibleColName, 0];
            }
        }
        /// <summary>
        /// Trả về tên cột đầu tiên mà có tính chất hiển thị của cột là Visible
        /// </summary>
        /// <param name="grd">DataGridView</param>
        /// <returns></returns>
        public static string GetFirstColumnVisibleOfDataGridView(DataGridView grd)
        {
            foreach (DataGridViewColumn col in grd.Columns)
            {
                if (col.Visible) return col.Name;
            }
            return "";
        }
        /// <summary>
        /// Chưa dùng
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="EqualValue"></param>
        /// <param name="WantedValue"></param>
        public static void SetValue(ref object obj, string EqualValue, string WantedValue)
        {
            if (obj == null || obj == DBNull.Value)
            {
                obj = EqualValue;
            }
            if (obj.ToString() == EqualValue)
            {
                obj = WantedValue;
            }
        }
        /// <summary>
        /// Chưa dùng
        /// </summary>
        /// <param name="Compareobj"></param>
        /// <param name="EqualValue"></param>
        /// <param name="ObjectValueChanged"></param>
        /// <param name="ChangedValue"></param>
        public static void SetValue(object Compareobj, string EqualValue, ref object ObjectValueChanged, string ChangedValue)
        {
            if (Compareobj == null)
            {
                Compareobj = EqualValue;
            }
            if (Compareobj.ToString() == EqualValue)
            {
                ObjectValueChanged = ChangedValue;
            }
        }
        /// <summary>
        /// Copy giá trị từ bảng nguồn chứa dữ liệu SourceTable sang bảng đích DesTable. Điều kiện thực hiện thành công là 2 bảng này phải có cấu trúc giống nhau
        /// </summary>
        /// <param name="SourceTable">Bảng nguồn chứa dữ liệu</param>
        /// <param name="DesTable">Bảng đích nơi dữ liệu được chép sang</param>
        /// <param name="ClearAllRowsBeforeCopy">Có xóa dữ liệu bảng đích trước khi copy các bản ghi mới hay không?</param>
        public static void CopyData(DataTable SourceTable, DataTable DesTable, bool ClearAllRowsBeforeCopy)
        {
            try
            {
                if (SourceTable == null || DesTable == null)
                {
                    Utility.ShowMsg("Bạn chưa khởi tạo bảng nguồn và bảng đích! Một trong 2 bảng này đang có giá trị null.");
                    return;
                }
                if (ClearAllRowsBeforeCopy) DesTable.Rows.Clear();
                foreach (DataRow dr in SourceTable.Rows)
                {
                    DesTable.ImportRow(dr);
                }
                DesTable.AcceptChanges();
            }
            catch { }
        }
        /// <summary>
        /// Copy giá trị từ Datarow nguồn chứa dữ liệu SourceRow sang Datarow đích DesTable. 
        /// </summary>
        /// <param name="SourceRow">Datarow nguồn chứa dữ liệu</param>
        /// <param name="DesRow">Datarow đích nơi dữ liệu được chép sang</param>
        public static void CopyData(DataRow SourceRow, ref DataRow DesRow)
        {
            try
            {
                if (SourceRow == null || DesRow == null)
                {
                    Utility.ShowMsg("Bạn chưa khởi tạo Datarow nguồn và Datarow đích! Một trong 2 Datarow này đang có giá trị null.");
                }
                foreach (DataColumn col in SourceRow.Table.Columns)
                {
                    if (DesRow.Table.Columns.Contains(col.ColumnName) && DesRow.Table.Columns[col.ColumnName].GetType().Equals(SourceRow.Table.Columns[col.ColumnName].GetType()))
                        DesRow[col.ColumnName] = SourceRow[col.ColumnName];
                }
                DesRow.AcceptChanges();
            }
            catch
            {
            }
        }
        /// <summary>
        /// Copy giá trị từ Datarow nguồn chứa dữ liệu SourceRow sang Datarow đích DesTable. 
        /// </summary>
        /// <param name="SourceRow">Datarow nguồn chứa dữ liệu</param>
        /// <param name="DesRow">Datarow đích nơi dữ liệu được chép sang</param>
        public static DataRow CopyData(DataRow SourceRow, DataTable DesTable)
        {
            DataRow DesRow = DesTable.NewRow();
            try
            {
                if (SourceRow == null || DesRow == null)
                {
                    Utility.ShowMsg("Bạn chưa khởi tạo Datarow nguồn và Datarow đích! Một trong 2 Datarow này đang có giá trị null.");
                    return null;
                }
                foreach (DataColumn col in SourceRow.Table.Columns)
                {
                    DesRow[col.ColumnName] = SourceRow[col.ColumnName];
                }
                return DesRow;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <param name="fieldName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(DataTable SourceTable, string[] fieldName, object[] Value)
        {
            try
            {
                foreach (DataRow dr in SourceTable.Rows)
                {
                    bool found = true;
                    for (int i = 0; i <= fieldName.Length - 1; i++)
                    {
                        found = found && dr[fieldName[i]].ToString() == Value[i].ToString();
                    }
                    if (found)
                        return dr;
                }
                return null;

            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SourceTable"></param>
        /// <param name="fieldName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static DataRow GetDataRow(DataTable SourceTable, string fieldName, object Value)
        {
            try
            {
                foreach (DataRow dr in SourceTable.Rows)
                {
                    if (dr[fieldName].ToString() == Value.ToString())
                        return dr;
                }
                return null;

            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Trả về một đối tượng Exception với Message truyền vào
        /// </summary>
        /// <param name="ExMsg"></param>
        /// <returns></returns>
        public static Exception NewException(string ExMsg)
        {
            return new Exception(ExMsg);
        }
        /// <summary>
        /// Lấy giá trị thực sự từ một giá trị cho trước. Thường dùng khi lấy giá trị từ TextBox
        /// Nếu cho phép trả về null (RevalCanbeNull==true) thì kiểm tra nếu chuỗi rỗng sẽ trả về giá trị null
        /// Nếu không cho phép trả về null thì sẽ
        /// CorrectStringValue: Nhằm thay thế dấu ' bằng dấu ''
        /// NotEmptyStrValue: Thay chuỗi rỗng bằng chuỗi cách " ". Tránh lỗi nếu CSDL là OracleDB
        /// </summary>
        /// <param name="OriginVal">Giá trị ban đầu</param>
        /// <param name="RevalCanbeNull">Có thể trả về null nếu giá trị là chuỗi rỗng hay không?</param>
        /// <returns></returns>
        public static string GetValue(string OriginVal, bool RevalCanbeNull)
        {
            if (RevalCanbeNull && String.IsNullOrEmpty(OriginVal.Trim()))
                return null;
            else
                return NotEmptyStrValue(CorrectStringValue(OriginVal), " ");
        }
        /// <summary>
        /// Chuyển đổi giá trị từ DBNull.Value thành null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromDBNullToNull(object value)
        {
            if (value == DBNull.Value) return null;
            else return value;
        }
        /// <summary>
        /// Chuyển đổi giá trị từ null thành DBNull.Value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object FromNullToDBNull(object value)
        {
            if (value == null) return DBNull.Value;
            else return value;
        }
        /// <summary>
        /// Chuyển giá trị chuỗi rỗng thành giá trị null
        /// </summary>
        /// <param name="OriginVal"></param>
        /// <returns></returns>
        public static string ChangeEmptyStringToNull(string OriginVal)
        {
            if (String.IsNullOrEmpty(OriginVal.Trim()))
                return null;
            else
                return OriginVal;
        }
        /// <summary>
        /// Hàm kiểm tra và chuyển chuỗi từ Empty sang giá trị ReplaceVal. Hàm này dùng để tránh trường hợp truyền chuỗi rỗng lên Server vì Oracle ko cho phép
        /// </summary>
        /// <param name="OriginVal">Chuỗi ban đầu</param>
        /// <param name="ReplaceVal">Chuỗi thay thế</param>
        /// <returns></returns>
        public static string NotEmptyStrValue(string OriginVal, string ReplaceVal)
        {
            if (String.IsNullOrEmpty(OriginVal.Trim()))
                return ReplaceVal;
            else
                return OriginVal;
        }

        ///<summary>
        ///<para>Thay dấu ' thành dấu '' để tránh lỗi khi thực hiện DML trên DataBase</para>
        ///</summary>   
        /// <param name="obj"><para>chuỗi có chứa dấu ' <see cref="string"/></para></param>     
        ///<returns>Chuỗi đã được thay thế dấu ' bằng ''</returns>
        public static string CorrectStringValue(string obj)
        {
            return obj.Replace("'", "''");

        }
        /// <summary>
        /// Hiển thị hộp thoại OKCancel để hỏi người dùng khi cần câu trả lời là có hay không?
        /// </summary>
        /// <param name="msg">Câu hỏi</param>
        /// <param name="title">Tiêu đề</param>
        /// <returns></returns>
        public static bool AcceptQuestion(string msg, string title, bool YesNoOrOKCancel)
        {
            if (System.Windows.Forms.MessageBox.Show(msg, title, YesNoOrOKCancel == true ? System.Windows.Forms.MessageBoxButtons.YesNo : System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question) == (YesNoOrOKCancel == true ? System.Windows.Forms.DialogResult.Yes : System.Windows.Forms.DialogResult.OK))
                return true;
            else
                return false;
        }
        ///<summary>
        ///<para>Hiển thị hộp thoại thông báo</para>
        ///</summary>   
        /// <param name="Message"><para>Nội dung thông báo</para></param>     
        ///<returns>Hiển thị MessageBox có title="Thông báo"</returns>
        public static void ShowMsg(string Message)
        {
            System.Windows.Forms.MessageBox.Show(Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        ///<summary>
        ///<para>Hiển thị hộp thoại thông báo</para>
        ///</summary>   
        /// <param name="Message"><para>Nội dung thông báo</para></param>     
        /// <param name="Title"><para>Title của MessageBox</para></param>     
        ///<returns>Hiển thị MessageBox có title= <see cref="Title"/></returns>
        public static void ShowMsg(string Message, string Title)
        {
            System.Windows.Forms.MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// hàm thực hiện message alert thông báo ra
        /// </summary>
        /// <param name="Message">nội dung message</param>
        /// <param name="Title">nội dung tiêu đề</param>
        /// <param name="Icon">Icon thực hiện</param>
        public static void ShowMsg(string Message, string Title,MessageBoxIcon Icon)
        {
            System.Windows.Forms.MessageBox.Show(Message, Title, MessageBoxButtons.OK,Icon);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="NotAllowedEmptyString"></param>
        /// <returns></returns>
        public static string sDbnull(object obj, bool NotAllowedEmptyString)
        {
            if (obj == null || obj == DBNull.Value)
            {
                if (NotAllowedEmptyString)
                    return " ";
                else
                    return "";
            }
            else
            {
                return obj.ToString();
            }
        }
        /// <summary>
        /// hàm thực hiện trạng thái thanh toán
        /// </summary>
        /// <param name="PaymentStatus"></param>
        /// <returns></returns>
        public static  string GetPaymentStatus(int PaymentStatus)
        {
            string paymentStatus = "Chưa thanh toán";
            switch (PaymentStatus)
            {
                case 0:
                    paymentStatus = "Chưa thanh toán";
                    break;
                case 1:
                    paymentStatus = "Đã thanh toán";
                    break;
            }
            return paymentStatus;
        }

        ///<summary>
        ///<para>Xử lý một Object nếu là null thì trả về giá trị truyền vào</para>
        ///</summary>   
        /// <param name="obj"><para>Object truyền vào <see cref="object"/></para></param>     
        /// <param name="DefaultVal"><para>Giá trị mặc định sẽ trả về nếu obj=null <see cref="object"/></para></param>     
        ///<returns>Đối tượng chuỗi trả về từ Object. Nếu obj=null thì trả về DefaultVal</returns>
        public static string sDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return DefaultVal.ToString();
            }
            else
            {
                return obj.ToString();
            }
        }
        /// <summary>
        /// Hàm chuyển đổi một giá trị chuỗi nếu rỗng thành một giá trị mặc định
        /// </summary>
        /// <param name="obj">Giá trị chuỗi</param>
        /// <param name="DefaultVal">Giá trị mặc định trả về nếu obj=rỗng</param>
        /// <returns></returns>
        public static string EmptytoDefaultValue(string obj, string DefaultVal)
        {
            if (string.IsNullOrEmpty(obj.Trim()))
            {
                return DefaultVal;
            }
            else
            {
                return GetValue(obj, false);
            }
        }
        ///<summary>
        ///<para>Xử lý một Object nếu là null thì trả về giá trị truyền vào</para>
        ///</summary>   
        /// <param name="obj"><para>Object truyền vào <see cref="object"/></para></param>     
        /// <param name="DefaultVal"><para>Giá trị mặc định sẽ trả về nếu obj=null <see cref="object"/></para></param>     
        ///<returns>Đối tượng chuỗi trả về từ Object. Nếu obj=null thì trả về DefaultVal</returns>
        public static string sDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return string.Empty;
            }
            else
            {
                return obj.ToString();
            }
        }
        public static double fDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return 0D;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        public static void FocusAndSelectAll(TextBox txt)
        {
            txt.Focus();
            txt.SelectAll();
        }
        public static string GetYYYYMMDDHHMMSS(DateTime dtp)
        {
            return dtp.Year.ToString() + dtp.Month.ToString().PadLeft(2, '0') + dtp.Day.ToString().PadLeft(2, '0') + dtp.Hour.ToString().PadLeft(2, '0') + dtp.Minute.ToString().PadLeft(2, '0') + dtp.Second.ToString().PadLeft(2, '0');
        }
        ///<summary>
        ///<para>Tạo Header có gắn DML type và HISTrace</para>
        ///</summary>   
        /// <param name="Header"><para>Header chứa gói tin</para></param>     
        /// <param name="action"><para>Loại nghiệp vụ cần thao tác <see cref="action"/></para></param>     
        /// <param name="trace"><para>Đối tượng lưu vết <see cref="HISTrace"/></para></param>     
        ///<returns></returns>
        public static void CreateHeader(ref DataTable Header, action action, TraceInfor trace)
        {
            try
            {
                if ((Header == null))
                {
                    Utility.ShowMsg("Header chưa được khởi tạo. Đề nghị LTV khai báo Header=new Header() trước khi sử dụng thủ tục này!");
                    return;
                }
                if (trace == null)
                {
                    //Utility.ShowMsg("Bạn chưa khởi tạo đối tượng trace. Đề nghị bạn khởi tạo trace trước khi sử dụng hàm này!");
                    return;
                }

                //Read from Header Into Trace
                DataRow dr = Header.NewRow();
                dr["action"] = action;
                dr["ID"] = trace.pID;
                dr["BranchID"] = CorrectStringValue(sDbnull(trace.pBranchID));
                dr["UserName"] = CorrectStringValue(sDbnull(trace.pUserName));
                dr["CreatedDate"] = CorrectStringValue(sDbnull(trace.pCreatedDate));
                dr["IPAddress"] = CorrectStringValue(sDbnull(trace.pIPAddress));
                dr["ComputerName"] = CorrectStringValue(sDbnull(trace.ComputerName));
                dr["AccountName"] = CorrectStringValue(sDbnull(trace.AccountName));
                dr["DLLName"] = CorrectStringValue(sDbnull(trace.DLLName));
                dr["SubSystemName"] = CorrectStringValue(sDbnull(trace.pSubSystemName));
                dr["FunctionID"] = CorrectStringValue(sDbnull(trace.pFunctionID));
                dr["FunctionName"] = CorrectStringValue(sDbnull(trace.FunctionName));
                dr["TableName"] = CorrectStringValue(sDbnull(trace.pTableName));
                dr["Desc"] = CorrectStringValue(sDbnull(trace.pDesc));
                dr["LOT"] = Int32Dbnull(trace.pLO, 0);
                Header.Rows.Add(dr);
                Header.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo Header từ Trace:\n" + ex.Message);
                return;
            }
        }
        ///<summary>
        ///<para>Tạo Header có gắn DML type và HISTrace</para>
        ///</summary>   
        /// <param name="Header"><para>Header chứa gói tin</para></param>     
        /// <param name="action"><para>Loại nghiệp vụ cần thao tác <see cref="action"/></para></param>     
        ///<returns></returns>
        public static void CreateHeader(ref DataTable Header, action action, string BranchID, string UserName, string CreatedDate, string IPAddress, string SubSystemName, string FunctionID)
        {
            if ((Header == null))
            {
                Utility.ShowMsg("Header chưa được khởi tạo. Đề nghị LTV khai báo Header=new Header() trước khi sử dụng thủ tục này!");
                return;
            }
            Header.Rows.Clear();
            DataRow newRow = Header.NewRow();
            newRow["action"] = action;
            newRow["BranchID"] = BranchID;
            newRow["UserName"] = UserName;
            newRow["CreatedDate"] = CreatedDate;
            newRow["IPAddress"] = IPAddress;
            newRow["SubSystemName"] = SubSystemName;
            newRow["FunctionID"] = FunctionID;
            Header.Rows.Add(newRow);
            Header.AcceptChanges();
        }
        ///<summary>
        ///<para>Tạo Header có gắn DML type và HISTrace</para>
        ///</summary>   
        /// <param name="Header"><para>Header chứa gói tin</para></param>     
        /// <param name="action"><para>Loại nghiệp vụ cần thao tác <see cref="action"/></para></param>     
        ///<returns></returns>
        public static void CreateHeader(ref DataTable Header, action action, string BranchID, string UserName, string CreatedDate, string IPAddress, string SubSystemName, string FunctionID, string TableName, string Desc, Int16 LO)
        {
            if ((Header == null))
            {
                Utility.ShowMsg("Header chưa được khởi tạo. Đề nghị LTV khai báo Header=new Header() trước khi sử dụng thủ tục này!");
                return;
            }
            Header.Rows.Clear();
            DataRow newRow = Header.NewRow();
            newRow["action"] = action;
            newRow["BranchID"] = BranchID;
            newRow["UserName"] = UserName;
            newRow["CreatedDate"] = CreatedDate;
            newRow["IPAddress"] = IPAddress;
            newRow["SubSystemName"] = SubSystemName;
            newRow["FunctionID"] = FunctionID;
            newRow["TableName"] = TableName;
            newRow["Desc"] = Desc;
            newRow["LO"] = LO;
            Header.Rows.Add(newRow);
            Header.AcceptChanges();
        }
        ///<summary>
        ///<para>Gắn Header vào Entity để truyền lên BusinessLogic xử lý</para>
        ///</summary>   
        /// <param name="Entity"><para>Entity chứa thông tin nghiệp vụ</para></param>     
        /// <param name="Header"><para>Header chứa gói tin lưu vết và loại nghiệp vụ cần thao tác</para></param>     
        ///<returns></returns>
        public static void MergeHeader(ref DataSet Entity, DataTable Header)
        {
            try
            {
                if (Entity.Equals(null))
                {
                    Utility.ShowMsg("Bạn phải khởi tạo Entity trước khi thực hiện hàm này");
                    return;
                }
                if (Header.Equals(null))
                {
                    Utility.ShowMsg("Bạn phải khởi tạo Header trước khi thực hiện hàm này");
                    return;
                }
                if (Entity.Tables.Contains(Header.TableName))
                {
                    Entity.Tables.Remove(Header.TableName);
                    Entity.AcceptChanges();
                    Entity.Merge(Header);
                    Entity.AcceptChanges();
                }
                else
                {
                    Entity.Merge(Header);
                    Entity.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                ShowMsg("Lỗi khi gắn Header vào Entity:\n" + ex.Message);
            }
        }
        /// <summary>
        ///  Reset Entity về trạng thái ban đầu chưa chứa dữ liệu
        /// </summary>
        /// <param name="Entity">Entity</param>
        /// <param name="EntityName">Tên của Entity. Thường đặt trùng với tên bảng CSDL thao tác</param>
        public static void ResetEntity(ref DataTable Entity)
        {
            if (Entity != null)
            {
                Entity.Rows.Clear();
            }
        }
        /// <summary>
        ///  Reset Entity về trạng thái ban đầu ko chứa bảng nào
        /// </summary>
        /// <param name="Entity">Entity</param>
        /// <param name="EntityName">Tên của Entity. Thường đặt trùng với tên bảng CSDL thao tác</param>
        public static void ResetEntity(ref DataSet Entity)
        {
            if (Entity != null)
            {
                Entity.Tables.Clear();
            }
        }
        /// <summary>
        /// Hàm định dạng tiền trong HIS
        /// </summary>
        /// <returns></returns>
        public static string FormatCurrecy()
        {
            return "{0:0,0}";
        }
        /// <summary>
        /// Hàm thực hiện khi định dạng o textbox
        /// </summary>
        /// <param name="txtBox"></param>
        public static void FormatCurrencyHIS(TextBox txtBox)
        {
            if ((txtBox.Text != null) && (txtBox.Text.Trim() != ""))
                txtBox.Text = String.Format(FormatCurrecy(), Convert.ToDouble(txtBox.Text));
            txtBox.Select(txtBox.Text.Length, 0);
        }
        /// <summary>
        /// Hàm thực hiện khi định dạng o textbox của janus
        /// </summary>
        /// <param name="txtBox"></param>
        public static void FormatCurrencyHIS(Janus.Windows.GridEX.EditControls.EditBox txtBox)
        {
            if ((txtBox.Text != null) && (txtBox.Text.Trim() != ""))
                txtBox.Text = String.Format(FormatCurrecy(), Convert.ToDouble(txtBox.Text));
            txtBox.Select(txtBox.Text.Length, 0);
        }
        /// <summary>
        /// Dinh dang tien label cho his
        /// </summary>
        /// <param name="txtBox"></param>
        public static void FormatCurrencyHIS(Label lbl)
        {
            if ((lbl.Text != null) && (lbl.Text.Trim() != ""))
                lbl.Text = String.Format(FormatCurrecy(), Convert.ToDouble(lbl.Text));


        }
        /// <summary>
        /// HÀM THỰC HIỆN ĐỊNH DẠNG TIỀN CỦA ToolStripStatusLabel
        /// </summary>
        /// <param name="lbl">THAM SỐ TRUYỀN VÀO</param>
        public static void FormatCurrencyHIS(System.Windows.Forms.ToolStripStatusLabel lbl)
        {
            if ((lbl.Text != null) && (lbl.Text.Trim() != ""))
                lbl.Text = String.Format(FormatCurrecy(), Convert.ToDouble(lbl.Text));


        }
        public static void FormatCurrencyHIS(System.Windows.Forms.ToolStripLabel lbl)
        {
            if ((lbl.Text != null) && (lbl.Text.Trim() != ""))
                lbl.Text = String.Format(FormatCurrecy(), Convert.ToDouble(lbl.Text));


        }
        ///<summary>
        ///<para>Lấy về loại nghiệp vụ từ Header</para>
        ///</summary>   
        /// <param name="DT"><para>Bảng Header trong gói tin truyền lên BusinessLogic xử lý <see cref="Header"/></para></param>     
        ///<returns>action <see cref="action"/></returns>
        public static action Getaction(DataTable DT)
        {
            try
            {
                if (DT.Rows.Count > 0)
                {
                    if ((DT.Rows[0]["action"].ToString() == "-1") || (DT.Rows[0]["action"] == null))
                    {
                        return action.Error;
                    }
                    else
                    {
                        switch (DT.Rows[0]["action"].ToString())
                        {
                            case "0":
                                return action.Insert;
                            case "1":
                                return action.Update;
                            case "2":
                                return action.Delete;
                            case "3":
                                return action.Select;
                            default:
                                return action.Error;
                        }
                    }
                }
                else
                {
                    return action.Error;
                }
            }
            catch (Exception ex)
            {
                return action.Error;
            }
        }
        //Chỉnh lại tên tham số trên SQL phải có chữ @
        public static string CorrectParameterNameOfSQLServer(string ParameterName)
        {
            if (ParameterName.Substring(0, 1) != "@")
            {
                return ParameterName = "@" + ParameterName;
            }
            else
            {
                return ParameterName;
            }
        }
        //Chỉnh lại tên tham số loại bỏ chữ @
        public static string CorrectParameterNameOfOracleServer(string ParameterName)
        {
            if (ParameterName.Substring(0, 1) == "@")
            {
                return ParameterName.Substring(1);
            }
            else
            {
                return ParameterName;
            }
        }
        ///<summary>
        ///<para>Khởi tạo MSDTC để có thể sử dụng Transaction Scope bảo đảm toàn vẹn dữ liệu trong một nghiệp vụ</para>
        ///</summary>       
        public static void StartMSDTC()
        {
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
            info.FileName = "sc.exe";
            info.Arguments = "start msdtc";
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            try
            {
                ExecuteProcess(info);
            }
            catch (TimeoutException ex)
            {
                throw new Exception("Could not start MSDTC");
            }

        }
        /// <summary>
        /// Thực thi tiến trình khởi tạo MSDTC
        /// </summary>
        /// <param name="startInfo"></param>
        private static void ExecuteProcess(System.Diagnostics.ProcessStartInfo startInfo)
        {
            using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            {
                process.StartInfo = startInfo;
                process.Start();
                const int waitMilliSec = 60000;
                bool complete = process.WaitForExit(waitMilliSec);
                if (!complete)
                {
                    Utility.ShowMsg("Quá trình khởi tạo MSDTC không thành công. Các giao dịch của bạn không được theo dõi bởi Transaction và không được đảm bảo tính toàn vẹn dữ liệu");
                }
            }
        }
        /// <summary>
        /// Disable TextBox và đưa màu của TextBox=BackGroundColor
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        /// <param name="BackGroundColor">Màu của TextBox t lúc Disable</param>
        public static void DisabledTextBox(TextBox t, System.Drawing.Color BackGroundColor)
        {
            t.Enabled = false;
            t.BackColor = BackGroundColor;
        }
        /// <summary>
        /// Enabled TextBox và đưa màu của TextBox=BackGroundColor
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        /// <param name="BackGroundColor">Màu của TextBox t lúc Enabled</param>
        public static void EnabledTextBox(TextBox t, System.Drawing.Color BackGroundColor)
        {
            t.Enabled = true;
            t.BackColor = BackGroundColor;
        }
        /// <summary>
        /// Disabled TextBox và đưa màu của TextBox=System.Drawing.Color.WhiteSmoke
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void DisabledTextBox(TextBox t)
        {
            t.Enabled = false;
            t.BackColor = System.Drawing.Color.WhiteSmoke;
        }
        /// <summary>
        /// hàm thực hiện disable MaskedEditBox của janus
        /// </summary>
        /// <param name="t"></param>
        public static void DisabledTextBox(Janus.Windows.GridEX.EditControls.MaskedEditBox t)
        {
            t.Enabled = false;
            t.BackColor = System.Drawing.Color.WhiteSmoke;
        }
        /// <summary>
        /// Enabled TextBox và đưa màu của TextBox=System.Drawing.Color.White
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void EnabledTextBox(TextBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }
        /// <summary>
        /// hàm thực hiện enable MaskedEditBox
        /// </summary>
        /// <param name="t">đối tượng truyền vào</param>
        public static void EnabledTextBox(Janus.Windows.GridEX.EditControls.MaskedEditBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }
        /// <summary>
        /// Disabled TextBox và đưa màu của TextBox=System.Drawing.Color.WhiteSmoke
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void DisabledComboBox(ComboBox t)
        {
            t.Enabled = false;
            t.BackColor = System.Drawing.Color.WhiteSmoke;
        }
        /// <summary>
        /// Enabled TextBox và đưa màu của TextBox=System.Drawing.Color.White
        /// </summary>
        /// <param name="t">Đối tượng TextBox</param>
        public static void EnabledComboBox(ComboBox t)
        {
            t.Enabled = true;
            t.BackColor = System.Drawing.Color.White;
        }

        public static string GetDiscountType(int discountType_ID)
        {
                 string disCountType_Name = "Khấu trừ %";

                if (discountType_ID == 1) disCountType_Name = "Khấu trừ số lượng";
                if (discountType_ID == 2) disCountType_Name = "Khấu trừ tiền";
           

            return disCountType_Name;
        }
        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int16. Xử lý nếu trường hợp là null thì trả về giá trị 0
        /// </summary>
        /// <param name="obj">convert đối tượng 16</param>
        /// <returns></returns>
        public static Int16 Int16Dbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }
        /// <summary>
        /// Hàm thực hiện set cho đối tượng kiểu Int64 
        /// </summary>
        /// <param name="obj">Đối tượng convert thành 64  </param>
        /// <returns></returns>
        public static Int64 Int64Dbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// hàm thực hiện convert đối tượng thành 64
        /// </summary>
        /// <param name="obj">Lấy đối tượng thành 64 </param>
        /// <param name="DefaultVal">Để giá trị mặc đi khi là null</param>
        /// <returns></returns>
        public static Int64 Int64Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt64(DefaultVal);
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int16. Xử lý nếu trường hợp là null thì trả về giá trị DefaultVal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="DefaultVal"></param>
        /// <returns></returns>
        public static Int16 Int16Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt16(DefaultVal);
            }
            else
            {
                return Convert.ToInt16(obj);
            }
        }
        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int32. Xử lý nếu trường hợp là null thì trả về giá trị DefaultVal
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="DefaultVal"></param>
        /// <returns></returns>
        public static Int32 Int32Dbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt32(DefaultVal);
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// Chuyển đổi một đối tượng sang kiểu Int32. Xử lý nếu trường hợp là null thì trả về giá trị 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int32 Int32Dbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToInt32("0");
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// Hàm thực hiện lấy đối convert đối tượng thành kiểu decimal
        /// </summary>
        /// <param name="obj">Đối tượng cần convert</param>
        /// <returns></returns>
        public static decimal DecimaltoDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToDecimal("0");
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN CONVERT DOUBLE NẾU ĐỐI TƯỢNG LÀ NULL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double DoubletoDbnull(object obj)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToDouble("0");
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN LẤY CONVERT ĐỐI TƯỢNG NẾU LÀ NULL THÌ 
        /// </summary>
        /// <param name="obj">GIÁ TRỊ TRUYỀN VÀO</param>
        /// <param name="DefaultVal">GIÁ TRỊ MẶC ĐỊNH</param>
        /// <returns></returns>
        public static double DoubletoDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToDouble(DefaultVal);
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        /// <summary>
        /// Hàm thực hiện convert đối tượng thành Decimal
        /// </summary>
        /// <param name="obj">Đối tượng cần convert</param>
        /// <param name="DefaultVal">Giá trị mặc định nếu obj là null</param>
        /// <returns></returns>
        public static decimal DecimaltoDbnull(object obj, object DefaultVal)
        {
            if (obj == null || obj == DBNull.Value || !IsNumeric(obj))
            {
                return Convert.ToDecimal(DefaultVal);
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_time"></param>
        /// <returns></returns>
        public static int GetMinute(DateTime _time)
        {
            return _time.Hour * 60 + _time.Minute;
        }
        /// <summary>
        /// Lấy về chuỗi mô tả ngày tháng hiện tại. ví dụ: Ngày 01 tháng 01 năm 2010
        /// </summary>
        /// <returns></returns>
        public static string sGetCurrentDay()
        {
            return "Ngày " + Strings.Right("0" + System.DateTime.Now.Day, 2) + " tháng " + Strings.Right("0" + System.DateTime.Now.Month, 2) + " năm " + System.DateTime.Now.Year;
        }
        /// <summary>
        /// Thiết lập giá trị cho một cột trên DataGridView
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <param name="RowIndex"></param>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static void SetValueForGridColumn(DataGridView grdList, string ColumnName, int RowIndex, object objValue)
        {
            grdList[ColumnName, RowIndex].Value = objValue;
        }
        /// <summary>
        /// Lấy về giá trị của một cột trên DataGridView
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static string GetValueFromGridColumn(DataGridView grdList, string ColumnName, int RowIndex)
        {
            return Utility.sDbnull(grdList[ColumnName, RowIndex].Value);
        }
        /// <summary>
        /// Lấy về giá trị của một cột trên DataGridView
        /// </summary>
        /// <param name="grdList"></param>
        /// <param name="ColumnName"></param>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        public static object GetObjectValueFromGridColumn(DataGridView grdList, string ColumnName, int RowIndex)
        {
            return grdList[ColumnName, RowIndex].Value;
        }
        /// <summary>
        /// Trả về giá trị SelectedIndex của một ComboBox nếu SelectedValue=giá trị truyền vào
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static int GetSelectedIndex(ComboBox cbo, string Value)
        {
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.SelectedValue.ToString() == Value) return i;
            }
            return -1;
        }
        public static int GetSelectedIndex(Janus.Windows.EditControls.UIComboBox cbo, string Value)
        {
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.SelectedValue.ToString() == Value) return i;
            }
            return -1;
        }
        public static int GetSelectedIndex(ComboBox cbo, string Value, bool SourceAsDataView)
        {
            if (!SourceAsDataView) return GetSelectedIndex(cbo, Value);

            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                cbo.SelectedIndex = i;
                if (cbo.SelectedValue.ToString() == Value) return i;
            }
            return -1;
        }
        /// <summary>
        /// Trả về giá trị SelectedIndex của một ComboBox nếu Text =giá trị truyền vào.
        /// Trường hợp này Item.Text có dạng: ID+Seperator+Name. Ví dụ: 1-Việt Nam
        /// </summary>
        /// <param name="cbo">ComboBox</param>
        /// <param name="Value">Giá trị cần so sánh</param>
        /// <param name="Seperator">Dấu ngăn cách phần ID và Name</param>
        /// <returns></returns>
        public static int GetSelectedIndex(ComboBox cbo, string Value, char Seperator, int idx)
        {
            if (Value == "-1") return -1;
            if (cbo.Items.Count <= 0) return -1;
            for (int i = 0; i <= cbo.Items.Count - 1; i++)
            {
                if (cbo.Items[i].ToString().Split(Seperator)[idx].ToUpper() == Value.ToUpper()) return i;
            }
            return -1;
        }

        public static void CreateMessage(Label obj, string Text, bool Visible)
        {
            obj.Text = Text;
            obj.Visible = Visible;
        }
        /// <summary>
        /// Tìm và nhảy đến cột có tên ColumnName trên DataGridView nếu tồn tại cột có giá trị Value 
        /// </summary>
        /// <param name="grdList">Đối tượng lưới dữ liệu DataGridView</param>
        /// <param name="ColumnName">Tên cột chứa giá trị</param>
        /// <param name="Value">Giá trị cần tìm</param>
        public static void GotoNewRow(DataGridView grdList, string ColumnName, string Value)
        {
            for (int i = 0; i <= grdList.RowCount - 1; i++)
            {
                if (Utility.sDbnull(grdList[ColumnName, i].Value).Trim() == Value.Trim())
                {
                    foreach (DataGridViewColumn grdCol in grdList.Columns)
                    {
                        if (grdCol.Visible)
                        {
                            grdList.CurrentCell = grdList[grdCol.Name, i];
                            break;
                        }
                    }

                    break; // TODO: might not be correct. Was : Exit For
                }
            }
        }
        /// <summary>
        /// So sánh giá trị của 2 ngày
        /// </summary>
        /// <param name="pv_sDate1"></param>
        /// <param name="pv_sDate2"></param>
        /// <returns>True nếu pv_sDate2>pv_sDate1. Ngược lại bằng false</returns>
        public static bool CompareDate(string pv_sDate1, string pv_sDate2)
        {
            if (System.DateTime.Parse(pv_sDate2, new System.Globalization.CultureInfo("vi-VN")) >= System.DateTime.Parse(pv_sDate1, new System.Globalization.CultureInfo("vi-VN")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CompareToDate(string pv_sDate1, string pv_sDate2)
        {
            return (DateTime.Compare(Convert.ToDateTime(pv_sDate1),Convert.ToDateTime( pv_sDate2)) > 0 ? true : false);
        }
        public static bool CompareDateUsingDateTimePicker(string pv_sDate1, string pv_sDate2)
        {
            DateTimePicker d1 = new DateTimePicker();
            DateTimePicker d2 = new DateTimePicker();

            try
            {
                d1.Text = pv_sDate1;
                d2.Text = pv_sDate2;
                if (d1.Value > d2.Value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Trả về chuỗi date có dạng 01/01/2009 AM(hoặc PM)
        /// </summary>
        /// <param name="_Date"></param>
        /// <returns></returns>
        public static string ToShortDateString(System.DateTime _Date)
        {
            string AMPM = "";
            if (_Date.ToString().Contains("AM"))
            {
                AMPM = "AM";
            }
            else
            {
                AMPM = "PM";
            }
            return Strings.Right("0" + _Date.Day.ToString(), 2) + "/" + Strings.Right("0" + _Date.Month.ToString(), 2) + "/" + _Date.Year.ToString() + " " + _Date.ToLongTimeString();
            //_Date.ToShortDateString & " " & _Date.ToLongTimeString '
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pv_sDate"></param>
        /// <returns></returns>
        public static System.DateTime ConvertDate(string pv_sDate)
        {
            return DateTime.ParseExact(pv_sDate, "dd/MM/yyyy", new System.Globalization.CultureInfo("vi-VN", false));
        }
        /// <summary>
        /// Đưa con trỏ của Form về trạng thái chờ đợi
        /// </summary>
        /// <param name="_me"></param>
        public static void WaitNow(System.Windows.Forms.Form _me)
        {
            _me.Cursor = Cursors.WaitCursor;
        }
        /// <summary>
        /// Đưa con trỏ của Form về trạng thái mặc định để có thể làm việc bình thường
        /// </summary>
        /// <param name="_me"></param>
        public static void DefaultNow(System.Windows.Forms.Form _me)
        {
            _me.Cursor = Cursors.Default;
        }
        /// <summary>
        /// Lấy về chuỗi năm tháng ngày có dạng YYYYMMDD. Ví dụ ngày 02/01/2010 sẽ trả về 20100102
        /// </summary>
        /// <param name="tDate"></param>
        /// <returns></returns>
        public static string GetYYYYMMDD(System.DateTime tDate)
        {
            return tDate.Year.ToString() + Strings.Right("0" + tDate.Month.ToString(), 2) + Strings.Right("0" + tDate.Day.ToString(), 2);
        }
        /// <summary>
        /// Lấy về chuỗi năm tháng ngày có dạng YYMMDD. Ví dụ ngày 02/01/2010 sẽ trả về 100102
        /// </summary>
        /// <param name="tDate"></param>
        /// <returns></returns>
        public static string GetYYMMDD(System.DateTime tDate)
        {
            return Strings.Right(tDate.Year.ToString(), 2) + Strings.Right("0" + tDate.Month.ToString(), 2) + Strings.Right("0" + tDate.Day.ToString(), 2);
        }
        /// <summary>
        /// Lấy về chuỗi giờ phút có dạng HHMM: ví dụ 9h30 ' thì sẽ trả về 0930
        /// </summary>
        /// <param name="tDate">Giờ hiện tại</param>
        /// <returns></returns>
        public static string GetHHMM(System.DateTime tDate)
        {
            return Strings.Right("0" + tDate.Hour.ToString(), 2) + ":" + Strings.Right("0" + tDate.Minute.ToString(), 2);
        }
        public static byte[] bytGetImage(string pv_sImgPath)
        {
            try
            {
                FileStream fs = null;
                if (File.Exists(pv_sImgPath))
                {
                    fs = new FileStream(pv_sImgPath, FileMode.Open);
                }
                else
                {
                    return null;
                }
                BinaryReader rd = new BinaryReader(fs);
                byte[] arrData = rd.ReadBytes((int)fs.Length);
                fs.Flush();
                fs.Close();
                return arrData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Tìm đối tượng DataGridView có tên pv_sName trong Control objMain
        /// </summary>
        /// <param name="objMain">Controls cần tìm DataGridView có tên pv_sName</param>
        /// <param name="pv_sName">Tên DataGridView cần tìm</param>
        private static void objGetCurrentDataGridView(Control objMain, string pv_sName)
        {
            foreach (Control ctr in objMain.Controls)
            {
                if (ctr.Name == pv_sName)
                {
                    globalVariables.CurrDtGridView = (DataGridView)ctr;
                }
                else
                {
                    objGetCurrentDataGridView(ctr, pv_sName);
                }
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN LOAD PROCESSBAR
        /// </summary>
        /// <param name="prgb">ĐỐI TƯỢNG TRUYỀN VÀO</param>
        /// <param name="Max">SỐ LƯỢNG MAX BẢN GHI</param>
        /// <param name="visi">THAM SỐ ẨN HIỆN</param>
        public static void ResetProgressBar(ProgressBar prgb, int Max, bool visi)
        {
            prgb.Step = 1;
            prgb.Visible = visi;
            prgb.Maximum = Max;
            prgb.Minimum = 0;
            prgb.Value = 0;
        }
        /// <summary>
        /// HÀM THỰC HIỆN LOAD THÔNG TIN CỦA PROCESSBAR CỦA PHẦN TOOLSTRIPpROCESSBAR
        /// </summary>
        /// <param name="prgb">ĐỐI TƯỢNG TRUYỀN VÀO</param>
        /// <param name="Max">SỐ LƯỢNG MAX BẢN GHI</param>
        /// <param name="visi">THAM SỐ ẨN HIỆN</param>
        public static void ResetToolTipProgressBar(System.Windows.Forms.ToolStripProgressBar prgb, int Max, bool visi)
        {
            prgb.Step = 1;
            prgb.Visible = visi;
            prgb.Maximum = Max;
            prgb.Minimum = 0;
            prgb.Value = 0;
        }
        /// <summary>
        /// Chuyển đổi Sex từ M sang Nam và còn lại sang Nữ
        /// </summary>
        /// <param name="Sex">có giá trị M hoặc F hoặc bất kỳ giá trị nào khác</param>
        /// <returns></returns>
        public static string TranslateSex(string Sex)
        {
            if (Sex.ToUpper() == "M")
            {
                return "Nam";
            }
            else
            {
                return "Nữ";
            }
        }
        /// <summary>
        /// HÀM THỰC HIỆN LOAD THÔNG TIN GIỚI TÍNH
        /// </summary>
        /// <param name="PatientSex"></param>
        /// <returns></returns>
        public static string TranslateSex(int PatientSex)
        {
            if (PatientSex.ToString().ToUpper() == "0")
            {
                return "Nam";
            }
            else
            {
                return "Nữ";
            }
        }
        /// <summary>
        /// Lấy về giá trị ID từ một Text của ComboBox. Nếu chọn Text là "Tất cả" trả về giá trị -1. Nếu không trả về giá trị cbo.Text.Split('-')[0]
        /// Chú ý: Chỉ nên dùng với các ComboBox có chứa các Items có Text là "Tất cả" hoặc có dạng Chuỗi 1+"-"+Chuỗi 2
        /// </summary>
        /// <param name="cbo"></param>
        /// <returns></returns>
        public static int intGetCoboVal(ComboBox cbo)
        {
            if (cbo.Items.Count <= 0) return -1;
            if (cbo.Text.Trim().ToUpper() == "Tất cả".ToUpper())
            {
                return -1;
            }
            else
            {
                return Utility.Int32Dbnull(cbo.Text.Split('-')[0]);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldValue"></param>
        public static void SetField(Type t, string FieldName, object FieldValue)
        {
            try
            {
                t.InvokeMember(FieldName.Trim(), BindingFlags.Default | BindingFlags.SetField, null, t, new object[] { FieldValue });
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// Biến giá trị từ một mảng thành một chuỗi cách nhau bởi dấu ,
        /// </summary>
        /// <param name="ArrDelegateUser"></param>
        /// <returns></returns>
        public static string FromArrayListToString(ArrayList ArrDelegateUser)
        {
            string DelegateUser = "";
            for (int i = 0; i <= ArrDelegateUser.Count - 1; i++)
            {
                DelegateUser += ArrDelegateUser[i].ToString() + ",";
            }
            if (DelegateUser != "") DelegateUser = DelegateUser.Substring(0, DelegateUser.Length - 1);
            return DelegateUser;

        }
        /// <summary>
        /// ShowMessage dựa vào MessageID từ file XML
        /// </summary>
        /// <param name="MsgID">Mã Message</param>
        /// <returns></returns>
        public static void ShowMsgBox(string MsgCode)
        {
            if (!System.IO.File.Exists(Application.StartupPath + @"\HISLinkMessage.XML"))
            {
                ShowMsg("Chưa tồn tại file HISLinkMessage.XML để hiển thị các thông báo trong hệ thống. Đề nghị liên hệ với Admin để có file này!");
                return;
            }
            DataSet dsMsg = new DataSet();
            dsMsg.ReadXml(Application.StartupPath + @"\HISLinkMessage.XML");
            if (dsMsg == null || dsMsg.Tables.Count <= 0 || dsMsg.Tables[0].Rows.Count <= 0)
            {
                ShowMsg("Nội dung Message chưa có gì. Đề nghị liên hệ với Admin để có file HISLinkMessage.XML");
                return;
            }
            DataRow[] dr = dsMsg.Tables[0].Select("Msg_Code='" + MsgCode + "'");
            if (dr.GetLength(0) <= 0)
            {
                ShowMsg("Không tồn tại Message có mã: " + MsgCode.ToString() + "\n.Đề nghị bạn xem lại");
                return;
            }
            string VnMsg_Content = sDbnull(dr[0]["VnMsg_Content"], "Không có nội dung Message. Liên hệ với Admin");
            string EnMsg_Content = sDbnull(dr[0]["EnMsg_Content"], "There is no Message contents. Plz contact with your Admin");
            string MsgType = sDbnull(dr[0]["Msg_Type"], "Infor");
            string VnMsg_Title = sDbnull(dr[0]["VnMsg_Title"], "");
            string EnMsg_Title = sDbnull(dr[0]["EnMsg_Title"], "");
            switch (globalVariables.DisplayLanguage)
            {
                case "VN":
                    System.Windows.Forms.MessageBox.Show(VnMsg_Content, GetTitle(MsgType, VnMsg_Title), GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    break;
                case "EN":
                    System.Windows.Forms.MessageBox.Show(EnMsg_Content, GetTitle(MsgType, EnMsg_Title), GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    break;
                default:
                    ShowMsg("Không xác định được loại ngôn ngữ hiển thị trên giao diện của bạn. Đề nghị liên hệ với VietBaIT");
                    break;
            }
        }
        /// <summary>
        /// Hiển thị Message dưới dạng hỏi là YesNo hoặc OKCancel dựa vào MessageID từ file XML
        /// </summary>
        /// <param name="MsgID">Mã Message</param>
        /// <returns>True nếu chọn Yes hoặc OK. False nếu chọn No hoặc Cancel</returns>
        public static bool AcceptQuestion(string MsgCode)
        {
            if (!System.IO.File.Exists(Application.StartupPath + @"\HISLinkMessage.XML"))
            {
                ShowMsg("Chưa tồn tại file HISLinkMessage.XML để hiển thị các thông báo trong hệ thống. Đề nghị liên hệ với Admin để có file này!");
                return false;
            }
            DataSet dsMsg = new DataSet();
            dsMsg.ReadXml(Application.StartupPath + @"\HISLinkMessage.XML");
            if (dsMsg == null || dsMsg.Tables.Count <= 0 || dsMsg.Tables[0].Rows.Count <= 0)
            {
                ShowMsg("Nội dung Message chưa có gì. Đề nghị liên hệ với Admin để có file Message.XML");
                return false;
            }
            DataRow[] dr = dsMsg.Tables[0].Select("Msg_Code='" + MsgCode + "'");
            if (dr.GetLength(0) <= 0)
            {
                ShowMsg("Không tồn tại Message có mã: " + MsgCode.ToString() + "\n.Đề nghị bạn xem lại");
                return false;
            }
            string VnMsg_Content = sDbnull(dr[0]["VnMsg_Content"], "Không có nội dung Message. Liên hệ với Admin");
            string EnMsg_Content = sDbnull(dr[0]["EnMsg_Content"], "There is no Message contents. Plz contact with Admin");
            string MsgType = sDbnull(dr[0]["Msg_Type"], "Infor");
            string VnMsg_Title = sDbnull(dr[0]["VnMsg_Title"], "");
            string EnMsg_Title = sDbnull(dr[0]["EnMsg_Title"], "");
            DialogResult result;
            switch (globalVariables.DisplayLanguage)
            {

                case "VN":
                    result = System.Windows.Forms.MessageBox.Show(VnMsg_Content, GetTitle(MsgType, VnMsg_Title), GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    return (result == DialogResult.OK || result == DialogResult.Yes) ? true : false;
                    break;
                case "EN":
                    result = System.Windows.Forms.MessageBox.Show(EnMsg_Content, GetTitle(MsgType, EnMsg_Title), GetMsgButton(MsgType), GetMsgIcon(MsgType));
                    return (result == DialogResult.OK || result == DialogResult.Yes) ? true : false;
                    break;
                default:
                    ShowMsg("Không xác định được loại ngôn ngữ hiển thị trên giao diện của bạn. Đề nghị liên hệ với VietBaIT");
                    return false;
            }
        }
        private static string GetTitle(string MsgType, string Title)
        {
            if (Title.Trim() != "") return Title;
            switch (globalVariables.DisplayLanguage)
            {
                case "VN":
                    switch (MsgType.ToUpper())
                    {
                        case "INFOR":
                            return "Thông báo";
                        case "WARN":
                            return "Cảnh báo";
                        case "YESN":
                            return "Xác nhận";
                        case "OKC":
                            return "Xác nhận";
                        default:
                            return "Thông báo";
                    }
                    break;
                case "EN":
                    switch (MsgType.ToUpper())
                    {
                        case "INFOR":
                            return "Information";
                        case "WARN":
                            return "Warning";
                        case "YESN":
                            return "Confirm";
                        case "OKC":
                            return "Confirm";
                        default:
                            return "Information";
                    }
                default:
                    return "";
            }
        }
        private static MessageBoxIcon GetMsgIcon(string MsgType)
        {
            switch (MsgType.ToUpper())
            {
                case "ERR":
                    return MessageBoxIcon.Error;
                case "INFOR":
                    return MessageBoxIcon.Information;
                case "WARN":
                    return MessageBoxIcon.Warning;
                case "YESN":
                    return MessageBoxIcon.Question;
                case "OKC":
                    return MessageBoxIcon.Question;
                default:
                    return MessageBoxIcon.Information;
            }
        }
        private static MessageBoxButtons GetMsgButton(string MsgType)
        {
            switch (MsgType.ToUpper())
            {
                case "ERR":
                    return MessageBoxButtons.OK;
                case "INFOR":
                    return MessageBoxButtons.OK;
                case "WARN":
                    return MessageBoxButtons.OK;
                case "YESN":
                    return MessageBoxButtons.YesNo;
                case "OKC":
                    return MessageBoxButtons.OKCancel;
                default:
                    return MessageBoxButtons.OK;
            }
        }
        private static DialogResult GetMsgDialogResult(string MsgType)
        {
            switch (MsgType.ToUpper())
            {
                case "YESN":
                    return DialogResult.Yes;
                case "OKC":
                    return DialogResult.OK;
                default:
                    return DialogResult.OK;
            }
        }
        /// <summary>
        /// Hàm trả về điều kiện sử dụng dữ liệu theo DelegateUser có dạng AND (CreatedBy='CUONGDV' OR CreatedBy='HUNGND')
        /// </summary>
        /// <param name="sDelegateUser">Chuỗi các MainUser cách nhau bởi dấu ,: CuongDV,HungND</param>
        /// <returns></returns>
        public static string sGetConditionFromDelegate(string sDelegateUser)
        {
            string Reval = "";
            string[] arrDelegateUser = sDelegateUser.Split(',');
            for (int i = 0; i <= arrDelegateUser.Length - 1; i++)
            {
                Reval += " upper(CreatedBy)=N'" + arrDelegateUser[i].ToString().ToUpper() + "' OR ";
            }
            if (Reval != "")
            {
                //Chuỗi đang có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND' OR 
                Reval = Reval.Substring(0, Reval.LastIndexOf("OR "));
                //Chuỗi Đã có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND'
                Reval = " AND ( " + Reval + " ) ";
            }
            return Reval;
        }
        /// <summary>
        /// Hàm trả về điều kiện sử dụng dữ liệu theo DelegateUser có dạng AND (CreatedBy='CUONGDV' OR CreatedBy='HUNGND')
        /// </summary>
        /// <param name="ArrDelegateUser">Mảng chứa các DelegateUser</param>
        /// <returns></returns>
        public static string sGetConditionFromDelegate(ArrayList ArrDelegateUser)
        {
            string Reval = "";

            for (int i = 0; i <= ArrDelegateUser.Count - 1; i++)
            {
                Reval += " upper(CreatedBy)=N'" + ArrDelegateUser[i].ToString().ToUpper() + "' OR ";
            }
            if (Reval != "")
            {
                //Chuỗi đang có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND' OR 
                Reval = Reval.Substring(0, Reval.LastIndexOf("OR "));
                //Chuỗi Đã có dạng CreatedBy='CUONGDV' OR CreatedBy='HUNGND'
                Reval = " AND ( " + Reval + " ) ";
            }
            return Reval;
        }
        public static ArrayList FromStringToArrayList(string DelegateUser)
        {
            ArrayList arrDelegateUser = new ArrayList();
            string[] _arrDelegateUser = DelegateUser.Split(',');
            for (int i = 0; i <= _arrDelegateUser.Length - 1; i++)
            {
                arrDelegateUser.Add(_arrDelegateUser[i].ToString());
            }
            return arrDelegateUser;

        }
        public static string sGetDelegateUser(DataTable dtDelegate)
        {
            string DelegateUser = "";
            foreach (DataRow dr in dtDelegate.Rows)
            {
                DelegateUser += dr["MainUser"].ToString() + ",";
            }
            if (DelegateUser != "") DelegateUser = DelegateUser.Substring(0, DelegateUser.Length - 1);
            return DelegateUser;
        }
        public static string sGetName(string pv_sTableName, string FieldToTake, string pv_sFieldName, int pv_ID)
        {
            try
            {
                SqlDataAdapter fv_da = new SqlDataAdapter("SELECT " + FieldToTake + " FROM " + pv_sTableName + " WHERE " + pv_sFieldName + "=" + pv_ID, globalVariables.SqlConn);
                DataTable fv_dt = new DataTable();
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return Utility.sDbnull(fv_dt.Rows[0][FieldToTake]);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// biến thông tin 
        /// </summary>
        /// <param name="HosStatus"></param>
        /// <returns></returns>
        public static string GetHosStatus(byte  HosStatus)
        {
            string strHos_Status = "Ngoại trú";
            if (Convert.ToInt32(HosStatus) == 2) strHos_Status = "Xuất viện";
            if (Convert.ToInt32(HosStatus) == 0) strHos_Status = "Ngoại trú";
            if (Convert.ToInt32(HosStatus) == 1) strHos_Status = "Nội trú";
            return strHos_Status;
        }
        /// <summary>
        /// Lấy về giá trị SysDate từ DatabaseServer. Chủ yếu được dùng cho mục đích đồng bộ hóa thời gian giữa Client và Server
        /// </summary>
        /// <returns></returns>
        public static System.DateTime getSysDate()
        {
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter("SELECT GETDATE()", globalVariables.SqlConn);
                DataTable DT = new DataTable();
                DA.Fill(DT);
                return Convert.ToDateTime(DT.Rows[0][0]);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy thời gian của hệ thống");
                return DateTime.Now;
            }
        }
        /// <summary>
        /// hàm thực hiện đặt hienr thị cho phần ToolStripStatusLabel
        /// </summary>
        /// <param name="obj">Đối tượng truyền vào</param>
        /// <param name="Text">dữ kiệu hiển thị</param>
        /// <param name="Visible">ẩn hay hiện thị của control</param>
        public static void CreateMessage(System.Windows.Forms.ToolStripStatusLabel obj, string Text, bool Visible)
        {
            obj.Text = Text;
            obj.Visible = Visible;
        }
        public static string sGetFieldValue(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            try
            {
                SqlDataAdapter fv_da = new SqlDataAdapter("SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition, globalVariables.SqlConn);
                DataTable fv_dt = new DataTable();
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return Utility.sDbnull(fv_dt.Rows[0][0]);
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static DataRow drGetDatarow(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            try
            {
                SqlDataAdapter fv_da = new SqlDataAdapter("SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition, globalVariables.SqlConn);
                DataTable fv_dt = new DataTable();
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return fv_dt.Rows[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void FillComboBox(DataSet pv_ds, string sDisplaymember, string sValueMember, ComboBox pv_oCboBox)
        {
            try
            {
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = pv_ds.Tables[0].DefaultView;
                    pv_oCboBox.DisplayMember = sDisplaymember;
                    pv_oCboBox.ValueMember = sValueMember;
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với DisplayMember = "sNAME" và ValueMember = "ID"
        /// Chú ý: Chỉ thành công nếu bảng trong Dataset chứa 2 trường có tên sNAME và ID
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBox(DataSet pv_ds, ComboBox pv_oCboBox)
        {
            try
            {
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = pv_ds.Tables[0].DefaultView;
                    pv_oCboBox.DisplayMember = "SNAME";
                    pv_oCboBox.ValueMember = "ID";
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với Item.Text=giá trị cột ID+ "-"+ giá trị cột sName
        /// Chú ý: Chỉ thành công nếu bảng trong Dataset chứa 2 trường có tên sNAME và ID
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBoxNoUsingValueMember(DataSet pv_ds, ComboBox pv_oCboBox)
        {
            try
            {
                pv_oCboBox.Items.Clear();

                foreach (DataRow dr in pv_ds.Tables[0].Rows)
                {
                    pv_oCboBox.Items.Add(dr["ID"] + "-" + dr["sName"]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với Item.Text=giá trị cột ID+ "-"+ giá trị cột sName
        /// Ngoài ra còn có Item đầu tiên có text là FirstItem. Thường dùng cho ComboBox có chứa giá trị "Tất cả"
        /// Chú ý: Chỉ thành công nếu bảng trong Dataset chứa 2 trường có tên sNAME và ID
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        /// <param name="FirstItem"></param>
        public static void FillComboBoxNoUsingValueMember(DataSet pv_ds, ComboBox pv_oCboBox, string FirstItem)
        {
            try
            {
                pv_oCboBox.Items.Clear();

                pv_oCboBox.Items.Add(FirstItem);
                foreach (DataRow dr in pv_ds.Tables[0].Rows)
                {
                    pv_oCboBox.Items.Add(dr["ID"] + "-" + dr["sName"]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu DataSet với DisplayMember = sDisplaymember và ValueMember = sValueMember. Có sử dụng sắp sếp dữ liệu trước khi Fill vào ComboBox
        /// </summary>
        /// <param name="pv_ds"></param>
        /// <param name="pv_oCboBox"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="OrderExpression">Mệnh đề Order. Ví dụ: ID Desc(Sắp sếp tăng dần theo cột ID)</param>
        public static void FillComboBoxHavingOrderData(DataSet pv_ds, ComboBox pv_oCboBox, string sDisplaymember, string sValueMember, string OrderExpression)
        {
            try
            {
                pv_oCboBox.Items.Clear();
                pv_oCboBox.DataSource = null;
                pv_ds.Tables[0].DefaultView.Sort = OrderExpression;
                {
                    pv_oCboBox.DataSource = pv_ds.Tables[0].DefaultView;
                    pv_oCboBox.DisplayMember = sDisplaymember;
                    pv_oCboBox.ValueMember = sValueMember;
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Tạo các Items trong ComboBox từ nguồn dữ liệu (thông qua truy vấn bảng có tên pv_sTableName) với DisplayMember = sDisplaymember và ValueMember = sValueMember. 
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBox(string pv_sTableName, string sDisplaymember, string sValueMember, ComboBox pv_oCboBox)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName, globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = sv_Dt.DefaultView;
                    pv_oCboBox.DisplayMember = sDisplaymember;
                    pv_oCboBox.ValueMember = sValueMember;
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// /// Tạo các Items trong ComboBox từ nguồn dữ liệu (thông qua truy vấn bảng có tên pv_sTableName) với DisplayMember = sDisplaymember và ValueMember = sValueMember. 
        /// ComboBox có chứa phần tử đầu tiên mang giá trị FirstItem
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="pv_oCboBox"></param>
        /// <param name="FirstItem"></param>
        public static void FillComboBoxNoUsingValueMember(string pv_sTableName, string sDisplaymember, string sValueMember, ComboBox pv_oCboBox, string FirstItem)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName, globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                pv_oCboBox.Items.Add(FirstItem);
                foreach (DataRow dr in sv_Dt.Rows)
                {
                    pv_oCboBox.Items.Add(dr[sValueMember] + "-" + dr[sDisplaymember]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="sDisplaymember"></param>
        /// <param name="sValueMember"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBoxNoUsingValueMember(string pv_sTableName, string sDisplaymember, string sValueMember, ComboBox pv_oCboBox)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName, globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                foreach (DataRow dr in sv_Dt.Rows)
                {
                    pv_oCboBox.Items.Add(dr[sValueMember] + "-" + dr[sDisplaymember]);
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Fill giá trị của một bảng vào Combobox
        /// </summary>
        /// <param name="pv_sTableName"></param>
        /// <param name="pv_oCboBox"></param>
        public static void FillComboBox(string pv_sTableName, ComboBox pv_oCboBox)
        {
            SqlDataAdapter sv_Da = new SqlDataAdapter("SELECT * FROM " + pv_sTableName + " ORDER BY intSTT ASC", globalVariables.SqlConn);
            DataTable sv_Dt = new DataTable();
            try
            {
                sv_Da.Fill(sv_Dt);
                pv_oCboBox.Items.Clear();
                pv_oCboBox.DataSource = null;
                {
                    pv_oCboBox.DataSource = sv_Dt.DefaultView;
                    pv_oCboBox.DisplayMember = "SNAME";
                    pv_oCboBox.ValueMember = "ID";
                }
                if (pv_oCboBox.Items.Count > 0)
                {
                    pv_oCboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// Lấy về giá trị kế tiếp sau giá trị lớn nhất của trường pv_sFieldName trong bảng pv_sTableName. 
        /// Thường được dùng trong các bảng có cột ID làm khóa ,kiểu dữ liệu là Numeric và không có tính chất tự tăng
        /// </summary>
        /// <param name="pv_sFieldName"></param>
        /// <param name="pv_sTableName"></param>
        /// <returns>Nếu không có bản ghi nào thì trả về giá trị 1.</returns>
        public static int getNextMaxID(string pv_sFieldName, string pv_sTableName)
        {
            DataTable fv_dt = new DataTable();
            SqlDataAdapter fv_da = new SqlDataAdapter("SELECT MAX(" + pv_sFieldName + ") FROM " + pv_sTableName, globalVariables.SqlConn);
            try
            {
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return (Information.IsDBNull(fv_dt.Rows[0][0]) ? 1 : Utility.Int32Dbnull(fv_dt.Rows[0][0])) + 1;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 1;
            }
        }
        public static int getNextMaxID(string pv_sFieldName, string pv_sTableName, Int16 Val)
        {
            DataTable fv_dt = new DataTable();
            SqlDataAdapter fv_da = new SqlDataAdapter("SELECT MAX(" + pv_sFieldName + ") FROM " + pv_sTableName + " where Parent_ID=" + Val, globalVariables.SqlConn);
            try
            {
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return (Information.IsDBNull(fv_dt.Rows[0][0]) ? 1 : Utility.Int32Dbnull(fv_dt.Rows[0][0])) + 1;
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 1;
            }
        }
        /// <summary>
        /// Lấy về giá trị lớn nhất của một cột có tên pv_sFieldName trong bảng có tên pv_sTableName
        /// Thường được dùng trong các bảng có cột ID làm khóa ,kiểu dữ liệu là Numeric và không có tính chất tự tăng
        /// </summary>
        /// <param name="pv_sFieldName"></param>
        /// <param name="pv_sTableName"></param>
        /// <returns></returns>
        public static int getCurrentMaxID(string pv_sFieldName, string pv_sTableName)
        {
            DataTable fv_dt = new DataTable();
            SqlDataAdapter fv_da = new SqlDataAdapter("SELECT MAX(" + pv_sFieldName + ") FROM " + pv_sTableName, globalVariables.SqlConn);
            try
            {
                fv_da.Fill(fv_dt);
                if (fv_dt.Rows.Count > 0)
                {
                    return (Information.IsDBNull(fv_dt.Rows[0][0]) ? 1 : Utility.Int32Dbnull(fv_dt.Rows[0][0]));
                }
                else
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 1;
            }
        }
        /// <summary>
        /// Lấy về tỉ giá của một loại tiền so với đồng Vnd. Chưa dùng
        /// </summary>
        /// <param name="pv_MoneyID"></param>
        /// <param name="pv_sMoneyName"></param>
        /// <returns></returns>
        public static double dblgetExchangeRate(int pv_MoneyID, string pv_sMoneyName)
        {
            SqlDataAdapter fv_DA = null;
            if (pv_MoneyID != -1)
            {
                fv_DA = new SqlDataAdapter("SELECT fRate FROM TBL_MONEYUNIT WHERE ID=" + pv_MoneyID, globalVariables.SqlConn);
            }
            else
            {
                fv_DA = new SqlDataAdapter("SELECT fRate FROM TBL_MONEYUNIT WHERE sName=N'" + pv_sMoneyName + "'", globalVariables.SqlConn);
            }

            DataSet fv_DS = new DataSet();
            try
            {
                fv_DA.Fill(fv_DS, "TBL_MONEYUNIT");
                if (fv_DS.Tables[0].Rows.Count > 0)
                {
                    return (double)fv_DS.Tables[0].Rows[0][0];
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return 0;
            }
        }
        /// <summary>
        /// Kiểm tra sự tồn tại của một bản ghi trong bảng 
        /// </summary>
        /// <param name="pv_sTableName">Tên bảng</param>
        /// <param name="pv_sFieldName">Tên trường </param>
        /// <param name="pv_objValue">Giá trị của trường</param>
        /// <param name="pv_bIsDigit">Giá trị kiểu chữ số hay chữ cái</param>
        /// <returns></returns>
        public static bool bIsExisted(string pv_sTableName, string pv_sFieldName, object pv_objValue, bool pv_bIsDigit)
        {
            try
            {
                SqlDataAdapter fv_DA = new SqlDataAdapter("SELECT * FROM " + pv_sTableName + " WHERE " + pv_sFieldName + sGetCondition(pv_objValue, pv_bIsDigit), globalVariables.SqlConn);
                DataSet fv_DS = new DataSet();
                fv_DA.Fill(fv_DS, pv_sTableName);
                if (fv_DS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Kiểm tra sự tồn tại của bản ghi trong bảng CSDL
        /// </summary>
        /// <param name="pv_sTableName">Tên bảng</param>
        /// <param name="sCondition">Điều kiện tìm kiếm</param>
        /// <returns></returns>
        public static bool bIsExisted(string pv_sTableName, string sCondition)
        {
            try
            {
                SqlDataAdapter fv_DA = new SqlDataAdapter("SELECT 1 FROM " + pv_sTableName + " WHERE " + sCondition, globalVariables.SqlConn);
                DataSet fv_DS = new DataSet();
                fv_DA.Fill(fv_DS, pv_sTableName);
                if (fv_DS.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return false;
            }
        }
        public static string sGetCondition(object pv_objValue, bool pv_bIsDigit)
        {
            if (pv_bIsDigit)
            {
                return " =" + Utility.Int32Dbnull(pv_objValue);
            }
            else
            {
                return " =N'" + Utility.sDbnull(pv_objValue) + "'";
            }
        }
        /// <summary>
        /// hàm thực hiện Import thông tin của một mảng datarow vào trong một datatable
        /// </summary>
        /// <param name="dataTable">datatable</param>
        /// <param name="arrDr">mảng datarow</param>
        /// <returns></returns>
        public static DataTable ImportDataRowToDatatable(DataTable dt,DataRow [] arrDr)
        {
            DataTable dataTable=new DataTable();
            dataTable = dt.Clone();
            foreach (DataRow dr in arrDr)
            {
                dataTable.ImportRow(dr);
            }
            dataTable.AcceptChanges();
            return dataTable;
        }
        /// <summary>
        /// Ham thuc hien lay thong tin bien doi gia tri cua mot cot trong datatable vao chuoi string
        /// </summary>
        /// <param name="dt">datatable nguon truyen vao</param>
        /// <param name="colFilter">Gia tri can loc ,vd :"CHON=1"</param>
        /// <param name="colID">Gia tri can lay de luu vao string</param>
        /// <returns></returns>
        public static string GetCheckedID(DataTable dt,string colFilter,string colID)
        {
            string str = "-1";
            foreach (DataRow dr in dt.Select(colFilter))
            {
                str += "," + dr[colID].ToString();
            }
            return str;
        }
        /// <summary>
        /// hàm thực hiện thêm giá trị vào một cọt nào đó
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="FiledName"></param>
        /// <param name="FiledValue"></param>
        public static void InsertValueToDatatable(ref DataTable dataTable,string FiledName,string FiledValue)
        {
            if (!dataTable.Columns.Contains("CHON")) dataTable.Columns.Add("CHON", typeof (int));
            foreach (DataRow dr in dataTable.Rows)
            {
                dr[FiledName] = FiledValue;
            }
            dataTable.AcceptChanges();
        }
      
        /// <summary>
        /// Tìm kiếm ký tự đầu để làm số phiếu khám bệnh kế tiếp
        /// </summary>
        /// <param name="yymmdd"></param>
        /// <returns></returns>
        public static string findFirstCharacter(string yymmdd)
        {
            SqlDataAdapter da = new SqlDataAdapter("select max(Left(RegNo+'0',1)) from TBL_REGSERVICE where RIGHT('000'+REGNO,3)<='999' AND RIGHT(LEFT(REGNO+'0000000',7),6)='" + yymmdd + "'", globalVariables.SqlConn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Information.IsDBNull(dt.Rows[0][0]))
                    {
                        return "A";
                    }
                    if (Strings.InStr("ABCDEFGHIJKNMLOPQSRTVWZX", Utility.sDbnull(dt.Rows[0][0]), CompareMethod.Text) > 0)
                    {
                        return CharacterFound(Utility.sDbnull(dt.Rows[0][0]), Utility.sDbnull(dt.Rows[0][0]) + yymmdd);
                    }
                    else
                    {
                        return "A";
                    }
                }
                else
                {
                    return "A";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return "A";
            }
        }

        private static string CharacterFound(string C, string CYYMMDD)
        {
            SqlDataAdapter da = new SqlDataAdapter("select 1 from TBL_REGSERVICE where RIGHT('000'+REGNO,3)='999' AND LEFT(REGNO+'0000000',7)='" + CYYMMDD + "'", globalVariables.SqlConn);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string[] arrCharacter = "A,B,C,D,E,F,G,H,I,J,K,N,M,L,O,P,Q,S,R,T,V,W,X,Y,Z".Split(',');
                    int Index = 0;
                    for (int i = 0; i <= arrCharacter.GetLength(0) - 1; i++)
                    {
                        if (arrCharacter[i] == C)
                        {
                            Index = i;
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (Index < arrCharacter.GetLength(0) - 1)
                    {
                        return arrCharacter[Index + 1];
                    }
                    else
                    {
                        Utility.ShowMsg("Hệ thống sinh mã đã đạt tới giới hạn trong ngày Z_YYMMDD_999" + Constants.vbCrLf + "Bạn cần phải xem lại hệ thống trước khi tiếp tục sử dụng chương trình.");
                        return "-A";
                    }
                }
                else
                {
                    return C;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return C;
            }
        }
        /// <summary>
        /// Lấy giá trị của một trường pv_sFieldName trong  một bảng pv_sTableName thỏa mãn điều kiện tìm kiếm pv_sCondition
        /// </summary>
        /// <param name="pv_sTableName">Tên bảng</param>
        /// <param name="pv_sFieldName">Tên trường cần lấy giá trị</param>
        /// <param name="pv_sCondition">Tên điều kiện tìm kiếm</param>
        /// <returns></returns>
        public static object GetFieldValue(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            string fv_sSql = "";
            fv_sSql = "SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition;
            SqlDataAdapter DA = new SqlDataAdapter(fv_sSql, globalVariables.SqlConn);
            DataTable DT = new DataTable();
            try
            {
                DA.Fill(DT);
                if (DT.Rows.Count > 0)
                {
                    return DT.Rows[0][0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }

        }
        /// <summary>
        /// Tạo nguồn dữ liệu cho AutocompleteTextBox
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="FieldName">Tên column trong bảng chứa dữ liệu</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataTable datasource, string FieldName)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();

                foreach (DataRow dr in datasource.Rows)
                {
                    string Values = "";

                    Values += dr[FieldName].ToString();

                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }
        /// <summary>
        /// Tạo nguồn dữ liệu cho AutocompleteTextBox
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="FieldName">Tên column trong bảng chứa dữ liệu</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataView datasource, string FieldName)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();

                foreach (DataRowView drv in datasource)
                {
                    string Values = "";

                    Values += drv[FieldName].ToString();

                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }
        /// <summary>
        /// Tạo danh sách dữ liệu dùng trong các AutoCompleteTextBox. Chú ý: Số phần tử của arrField=Seperator.
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="arrField">Danh sách các field trong datasource muốn kết hợp tạo thành giá trị trong AutocompleteTextBox</param>
        /// <param name="Seperator">Phân cách giữa các field</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataTable datasource, ArrayList arrField, ArrayList Seperator)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();
                if (arrField.Count != Seperator.Count) return AutoComp;
                foreach (DataRow dr in datasource.Rows)
                {
                    string Values = "";
                    for (int i = 0; i <= arrField.Count - 1; i++)
                    {
                        Values += dr[arrField[i].ToString()].ToString() + Seperator[i].ToString();
                    }
                    Values = Values.Substring(0, Values.LastIndexOf(Seperator[Seperator.Count - 1].ToString()));
                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }
        /// <summary>
        /// Tạo danh sách dữ liệu dùng trong các AutoCompleteTextBox. Chú ý: Số phần tử của arrField=Seperator.
        /// </summary>
        /// <param name="datasource">Nguồn chứa dữ liệu</param>
        /// <param name="arrField">Danh sách các field trong datasource muốn kết hợp tạo thành giá trị trong AutocompleteTextBox</param>
        /// <param name="Seperator">Phân cách giữa các field</param>
        /// <returns></returns>
        public static AutoCompleteStringCollection GetAutoCompleteStringCollection(DataView datasource, ArrayList arrField, ArrayList Seperator)
        {
            try
            {
                AutoCompleteStringCollection AutoComp = new AutoCompleteStringCollection();
                if (arrField.Count != Seperator.Count) return AutoComp;
                foreach (DataRowView drv in datasource)
                {
                    string Values = "";
                    for (int i = 0; i <= arrField.Count - 1; i++)
                    {
                        Values += drv[arrField[i].ToString()].ToString() + Seperator[i].ToString();
                    }
                    Values = Values.Substring(0, Values.LastIndexOf(Seperator[Seperator.Count - 1].ToString()));
                    AutoComp.Add(Values);
                }
                return AutoComp;
            }
            catch
            {
                return new AutoCompleteStringCollection();
            }
        }
        public static DataRow[] ArrGetFieldValue(string pv_sTableName, string pv_sFieldName, string pv_sCondition)
        {
            string fv_sSql = "";
            fv_sSql = "SELECT " + pv_sFieldName + " FROM " + pv_sTableName + " WHERE " + pv_sCondition;
            SqlDataAdapter DA = new SqlDataAdapter(fv_sSql, globalVariables.SqlConn);
            DataTable DT = new DataTable();
            try
            {
                DA.Fill(DT);
                if (DT.Rows.Count > 0)
                {
                    return DT.Select("1=1");
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                return null;
            }

        }
        public static void LoadParamsValues(Assembly pv_objAss)
        {
            DataRow CurRow = default(DataRow);
            Type t = null;
            try
            {
                foreach (Type t_loopVariable in pv_objAss.GetTypes())
                {
                    t = t_loopVariable;
                    FieldInfo[] fi = t.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);
                    FieldInfo f = default(FieldInfo);
                    for (int i = 0; i <= fi.Length - 1; i++)
                    {
                        f = fi[i];
                        switch (f.Name)
                        {
                            case "Branch_Name":
                                SetField(t, "Branch_Name", globalVariables.Branch_Name);
                                break;
                            case "gv_StaffID":
                                SetField(t, "gv_StaffID", globalVariables.gv_StaffID);
                                break;
                            case "Branch_Address":
                                SetField(t, "Branch_Address", globalVariables.Branch_Address);
                                break;
                            case "IsAdmin":
                                SetField(t, "IsAdmin", globalVariables.IsAdminLogin);
                                break;
                            case "gv_sStaffName":
                                SetField(t, "gv_sStaffName", globalVariables.gv_sStaffName);
                                break;
                            case "gv_intServiceType_ID":
                                SetField(t, "gv_intServiceType_ID", globalVariables.gv_intServiceType_ID);
                                break;
                            case "Branch_Phone":
                                SetField(t, "Branch_Phone", globalVariables.Branch_Phone);
                                break;
                            case "Branch_Email":
                                SetField(t, "Branch_Email", globalVariables.Branch_Email);
                                break;
                            case "Branch_Website":
                                SetField(t, "Branch_Website", globalVariables.Branch_Website);
                                break;
                            case "globalVariables.SqlConn":
                                SetField(t, "globalVariables.SqlConn", globalVariables.SqlConn);
                                break;
                            case "Branch_ID":
                                SetField(t, "Branch_ID", globalVariables.Branch_ID);
                                break;
                            case "gv_intCurrMonth":
                                SetField(t, "gv_intCurrMonth", globalVariables.gv_intCurrMonth);
                                break;
                            case "gv_intCurrYear":
                                SetField(t, "gv_intCurrYear", globalVariables.gv_intCurrYear);
                                break;
                            case "UserName":
                                SetField(t, "UserName", globalVariables.UserName);
                                break;
                            case "ParentBranch_Name":
                                SetField(t, "ParentBranch_Name", globalVariables.ParentBranch_Name);
                                break;
                            case "DisplayLanguage":
                                SetField(t, "DisplayLanguage", globalVariables.DisplayLanguage);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString(), MsgBoxStyle.Exclamation, "DVC");
            }
        }
        #endregion
        #region "PHAN THU VIEN DUNG CHUNG CUA TÚ"
        public static void InsertValueIdentity(ref DataTable dt, string colFieldName)
        {
            if (!dt.Columns.Contains(colFieldName)) dt.Columns.Add(colFieldName, typeof(int));
            int idx = 1;
            foreach (DataRow dr in dt.Rows)
            {
                dr[colFieldName] = idx;
                idx++;
            }
            dt.AcceptChanges();
        }
        /// <summary>
        /// ham thuc hien load thong tin logo vao du lieu nguon
        /// </summary>
        /// <param name="dataTable"></param>
        public static void UpdateLogotoDatatable(ref DataTable dataTable)
        {

            if (!dataTable.Columns.Contains("LOGO"))dataTable.Columns.Add("LOGO", typeof(byte[]));
            byte[] byteArray =
                Utility.ConvertImageToByteArray(Image.FromFile(Application.StartupPath + @"\logo\logo.jpg"),
                                                ImageFormat.Jpeg);
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["LOGO"] = byteArray;
            }
            dataTable.AcceptChanges();
        }
        public static string ConvertDataTableToXML(DataTable dtData)
        {
            DataSet dsData = new DataSet();
            StringBuilder sbSQL;
            StringWriter swSQL;
            string XMLformat;
            try
            {
                sbSQL = new StringBuilder();
                swSQL = new StringWriter(sbSQL);
                dsData.Merge(dtData, true, MissingSchemaAction.AddWithKey);
                dsData.Tables[0].TableName = "SampleDataTable";
                foreach (DataColumn col in dsData.Tables[0].Columns)
                {
                    col.ColumnMapping = MappingType.Attribute;
                }
                dsData.WriteXml(swSQL, XmlWriteMode.WriteSchema);
                XMLformat = sbSQL.ToString();
                return XMLformat;
            }
            catch (Exception sysException)
            {
                throw sysException;
            }
        }

        /// <summary>
        /// Hàm tính tổng của một datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="DataField"></param>
        /// <returns></returns>
        public static string TotaltoDataTable(DataTable dt, string DataField)
        {
            decimal Total = 0;
            //decimal Total = 0;
            if (dt.Rows.Count > 0)
            {

                Total = Convert.ToDecimal(dt.Compute("Sum(" + DataField + ")", "1=1"));

            }
            return Total.ToString();
        }
        public static string ConvertDate(DateTime dt)
        {
            return dt.ToShortDateString();
        }
        /// <summary>
        /// Hàm convert Image vào mảng chỉ cần truyền vào là Image
        /// </summary>
        /// <param name="imageIn"></param>
        /// <returns></returns>
        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        /// <summary>
        /// Hàm convert từ Byte sang Image
        /// </summary>
        /// <param name="byteArrayIn"></param>
        /// <returns></returns>
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
        /// <summary>
        /// Hàm tự sinh Barcode trong Crystral report chỉ cần đầu vào là tên mã barcode
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static byte[] GenerateBarCode(Barcode barcode)
        {
            MemoryStream ms = new MemoryStream();

            barcode.Image().Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] data = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(data, 0, (int)ms.Length);
            return data;


        }
        /// <summary>
        /// Hàm thực hiện chỉ cho phép nhập só vào ô textbox
        /// </summary>
        /// <param name="e"></param>
        public static void OnlyDigit(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
            else
                if (Char.IsControl(e.KeyChar))
                {
                    int ma = (int)e.KeyChar;
                    if ((ma == 26) || (ma == 24) || (ma == 3) || (ma == 22) || (ma == 1))
                        e.Handled = true;

                }
        }
        public static void OnlyDigit(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }
        public static void OnlyDigitNative(KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0') || (e.KeyChar > '9')) e.Handled = true;
        }
        public static string FormatDateTime(DateTime dt)
        {
            string str = "Ngày ";
            str += dt.Day.ToString();
            str += " Tháng ";
            str += dt.Month.ToString();
            str += " Năm ";
            str += dt.Year;
            return str;
        }
        /// <summary>
        /// Định dạng ngày tháng của việt nam trong Crystral report 
        /// </summary>
        /// <param name="Todate"></param>
        /// <param name="FromDate"></param>
        /// <returns></returns>
        public static string FromToDateTime(string FromDate, string Todate)
        {
            string str = "Từ ngày ";
            str += FromDate;
            str += " đến ngày ";
            str += Todate;
            return str;
        }
        public static string GetYear(DateTime dt)
        {
            return dt.Year.ToString();
        }
        public static DataTable ReadXMLtoDatatable(string xmlPath, string DataTableName)
        {

            DataSet dsStore = new DataSet();
            dsStore.ReadXml(xmlPath);
            return dsStore.Tables[DataTableName];


        }
        public static byte[] ToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }
        public static decimal ConvertObjectToDecimal(object obj)
        {
            decimal Total = 0;
            try
            {
                Total = Convert.ToDecimal(obj);
            }
            catch (Exception ex) { }
            return Total;
        }
        public static bool CompareToDate(DateTime dt1, DateTime dt2)
        {
            return (DateTime.Compare(dt1, dt2) > 0 ? true : false);
        }
        public static bool IsLocalIpAddress(string host)
        {
            try
            { // get host IP addresses
                IPAddress[] hostIPs = Dns.GetHostAddresses(host);
                // get local IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // test if any host IP equals to any local IP or to localhost
                foreach (IPAddress hostIP in hostIPs)
                {
                    // is localhost
                    if (IPAddress.IsLoopback(hostIP)) return true;
                    // is local address
                    foreach (IPAddress localIP in localIPs)
                    {
                        if (hostIP.Equals(localIP)) return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        /// <summary>
        /// Hàm chuyển đổi từ ảnh sang mảng
        /// </summary>
        /// <param name="imageToConvert"></param>
        /// <param name="formatOfImage"></param>
        /// <returns></returns>
        public static byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (var ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }
        public static List<int> FindIndex(object source, List<int> values)
        {
            var result = new List<int>();
            var table = source as DataTable;
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    foreach (var value in values)
                    {
                        if (table.Rows[i][0].ToString().Equals(value.ToString()))
                            result.Add(i);
                    }
                }
            }
            return result;
        }
        public static List<int> FindIndex(object source, DataTable values)
        {
            var result = new List<int>();
            var table = source as DataTable;
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    foreach (DataRow value in values.Rows)
                    {
                        if (table.Rows[i][0].ToString().Equals(value.ToString()))
                            result.Add(i);
                    }
                }
            }
            return result;
        }
        public static void XMLtoDataTable(string Address, string DataBaseName, string User_ID, string Password, string MutiServiceType, string ConnectLab, string strBranchID)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            //create element root
            XmlElement rootNode = xmlDoc.CreateElement("NewDataSet");
            xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);
            xmlDoc.AppendChild(rootNode);
            // Create a new <Category> element and add it to the root node
            XmlElement parentNode = xmlDoc.CreateElement("Config");
            xmlDoc.DocumentElement.PrependChild(parentNode);
            XmlElement SERVERADDRESS = xmlDoc.CreateElement("SERVERADDRESS");
            XmlElement DATABASE_ID = xmlDoc.CreateElement("DATABASE_ID");
            XmlElement USERNAME = xmlDoc.CreateElement("USERNAME");
            XmlElement PASSWORD = xmlDoc.CreateElement("PASSWORD");
            XmlElement MUTISERVICETYPE = xmlDoc.CreateElement("MUTISERVICETYPE");
            XmlElement CONNECTLAB = xmlDoc.CreateElement("CONNECTLAB");
            XmlElement BranchID = xmlDoc.CreateElement("BranchID");
            //XmlElement CONNNECTED = xmlDoc.CreateElement("CONNNECTED");
            parentNode.AppendChild(SERVERADDRESS);
            parentNode.AppendChild(DATABASE_ID);
            parentNode.AppendChild(USERNAME);
            parentNode.AppendChild(PASSWORD);
            parentNode.AppendChild(MUTISERVICETYPE);
            parentNode.AppendChild(CONNECTLAB);
            parentNode.AppendChild(BranchID);
            //parentNode.AppendChild(CONNNECTED);

            // save the value of the fields into the nodes
            XmlText xmlServer = xmlDoc.CreateTextNode(Address);
            XmlText xmlDataBase = xmlDoc.CreateTextNode(DataBaseName);
            XmlText xmlUser = xmlDoc.CreateTextNode(User_ID);
            XmlText xmlPassword = xmlDoc.CreateTextNode(Password);
            XmlText xmlMutiServiceType = xmlDoc.CreateTextNode(MutiServiceType);
            XmlText xmlConectLab = xmlDoc.CreateTextNode(ConnectLab);
            XmlText xmlBranchID = xmlDoc.CreateTextNode(strBranchID);
            //XmlText xmlCONNNECTED = xmlDoc.CreateTextNode(strBranchID);


            ////
            SERVERADDRESS.AppendChild(xmlServer);
            DATABASE_ID.AppendChild(xmlDataBase);
            USERNAME.AppendChild(xmlUser);
            PASSWORD.AppendChild(xmlPassword);
            MUTISERVICETYPE.AppendChild(xmlMutiServiceType);
            CONNECTLAB.AppendChild(xmlConectLab);
            BranchID.AppendChild(xmlBranchID);
            xmlDoc.Save("LABLinkConfig.XML");
        }
        public static bool IsValidEmailAddress(string email)
        {
            try
            {
                MailAddress ma = new MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }
      
        public static string Chuanhoa(string str)
        {
            string st = str.Trim().ToLower();
            string st1 = null;
            string FieldName = "";
            if (!string.IsNullOrEmpty(str))
            {

                while (st.Trim().Length != 0)
                {
                    st += " ";
                    int i = st.IndexOf(" ");
                    string d = st.Substring(0, i);
                    d = char.ToUpper(d[0]) + d.Substring(1);
                    st = st.Substring(i + 1).Trim();
                    st1 += d.Trim() + " ";

                }
                FieldName = st1.TrimEnd();
            }
            return FieldName;
        }
        public static string UpperCharacter(string str)
        {
            string st = str.Trim().ToUpper();


            return st;
        }
        public static bool valiEmail(string str)
        {

            Regex rgxemail = new Regex(@"^[\w\-\.\+]+\@[a-zA-Z0-9\.\-]+\.[a-zA-z0-9]{2,4}$");
            if (!rgxemail.IsMatch(str))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        /// <summary>
        /// Returns a null if the entered string was null, otherwise it trims the entered string.
        /// </summary>
        public static string Trim(string value)
        {
            return value == null ? null : value.Trim();
        }
        /// <summary>
        /// Trims the string then returns if the trimmed string is null or empty.
        /// </summary>
        public static bool IsTrimmedNullOrEmpty(string value)
        {
            return String.IsNullOrEmpty(Trim(value));
        }
        /// <summary>
        /// Kiểm tra chuỗi nhập vào có empty không.
        /// </summary>
        /// <param name="s">Input String to check</param>
        /// <returns >boolean</returns>
        public static bool IsEmpty(string s)
        {
            bool b = false;
            try
            {
                b = string.IsNullOrEmpty(s);
                return b;
            }
            catch (Exception)
            {
                return b;
            }
        }
        /// <summary>
        /// ham thuc hien xoa thong tin cua datatable cua mot truong, voi dieu kien truyen vao
        /// </summary>
        /// <param name="dt_Datatable">datatable truyen vao</param>
        /// <param name="FieldName">truong so sanh</param>
        /// <param name="Condition">gia tri truyen vao</param>
        public static void DeleteFetchDataRow( ref DataTable dt_Datatable,string FieldName,string Condition)
        {
            foreach (DataRow dr in dt_Datatable.Rows)
            {
                if(dr[FieldName].ToString().Equals(Condition))
                {
                    dr.Delete();
                    break;
                }
            }
            dt_Datatable.AcceptChanges();
        }
        public static bool IsDigit(object obj)
        {
            bool b = false;
            try
            {
                b = Convert.ToDecimal(obj) != null;
                //chay thu lai cai SVN xem OK khong ????
                return b;
            }
            catch (Exception)
            {
                return b;
            }
        }
        public static bool ValiNumber(string str)
        {
            Regex rgxstring = new Regex(@"^\d*[0-9]?$");
            if (!rgxstring.IsMatch(str))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// hàm cho phép thực hiện công việc 
        /// </summary>
        /// <param name="q">câu lệnh sql của subsonic</param>
        /// <returns></returns>
        public static bool AllowDelete(SqlQuery q)
        {
            int recordcount = q.GetRecordCount();
            if (recordcount > 0) return false;
            else return true;

        }
        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText, string psDataPropertyName, bool pbVisible, bool pbReadOnly)
        {
            return createTextColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly);
        }
        /// <summary>
        /// Tạo ra Text Column co Data Grid View
        /// </summary>
        /// <param name="psName">Tên cột</param>
        /// <param name="psHeaderText">Tiêu đề cột</param>
        /// <param name="psDataPropertyName">Thuộc tính Dữ liệu</param>
        /// <param name="piColumnWidth">Độ rộng của cột</param>
        /// <param name="pbVisible">True:Hiện, False:Ẩn</param>
        /// <param name="pbReadOnly">True:ReadOnly</param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText,
                                                           string psDataPropertyName, int piColumnWidth, bool pbVisible,
                                                           bool pbReadOnly)
        {
            DataGridViewTextBoxColumn pmColumn;
            using (pmColumn = new DataGridViewTextBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,
                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0) { pmColumn.Width = piColumnWidth; }

            return pmColumn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="psName"></param>
        /// <param name="psHeaderText"></param>
        /// <param name="psDataPropertyName"></param>
        /// <param name="pbVisible"></param>
        /// <param name="pbReadOnly"></param>
        /// <param name="hidden"></param>
        /// <returns></returns>
        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText, string psDataPropertyName, bool pbVisible, bool pbReadOnly, string hidden)
        {
            return createTextColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly, hidden);
        }

        public static DataGridViewTextBoxColumn createTextColumn(string psName, string psHeaderText,
                                                           string psDataPropertyName, int piColumnWidth, bool pbVisible,
                                                           bool pbReadOnly, string hidden)
        {
            DataGridViewTextBoxColumn pmColumn;
            using (pmColumn = new DataGridViewTextBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,
                ToolTipText = hidden,
                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0) { pmColumn.Width = piColumnWidth; }

            return pmColumn;
        }

        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText, string psDataPropertyName, bool pbVisible, bool pbReadOnly)
        {
            return createCheckBoxColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly);
        }

        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText,
                                                         string psDataPropertyName, int piColumnWidth, bool pbVisible,
                                                           bool pbReadOnly)
        {
            DataGridViewCheckBoxColumn pmColumn;
            using (pmColumn = new DataGridViewCheckBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,

                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0) { pmColumn.Width = piColumnWidth; }

            return pmColumn;

        }
        public static string ReplaceStr(string mystring)
        {

            string temp = mystring.Replace(Environment.NewLine, string.Empty);
            return temp;
        }
        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText, string psDataPropertyName, bool pbVisible, bool pbReadOnly, string hidden)
        {
            return createCheckBoxColumn(psName, psHeaderText, psDataPropertyName, 0, pbVisible, pbReadOnly, hidden);
        }
        public static string GenerateInt(SqlQuery q)
        {
            // SqlQuery q = new Select(Aggregate.Max(LDepartment.Columns.IntOrder)).From(LDepartment.Schema.TableName);
            int record = q.ExecuteScalar<int>();
            return (record + 1).ToString();
        }
        public static DataGridViewCheckBoxColumn createCheckBoxColumn(string psName, string psHeaderText,
                                                         string psDataPropertyName, int piColumnWidth, bool pbVisible,
                                                           bool pbReadOnly, string hidden)
        {
            DataGridViewCheckBoxColumn pmColumn;
            using (pmColumn = new DataGridViewCheckBoxColumn
            {
                Name = psName,
                HeaderText = psHeaderText,
                ToolTipText = hidden,
                DataPropertyName = psDataPropertyName,
                Visible = pbVisible,
                ReadOnly = pbReadOnly
            })
                if (piColumnWidth != 0) { pmColumn.Width = piColumnWidth; }

            return pmColumn;

        }

        /// <summary>
        /// Thu hien lay du lieu tu 1 file XML 
        /// </summary>
        /// <param name="FileName">Ten file xml</param>
        /// <param name="FieldName">Truong file cua xml</param>
        /// <param name="ChildName">TRuong can lay cua file xml</param>
        /// <returns></returns>
        public static string ReadXMLtoString(string FileName, string FieldName, string ChildName)
        {
            string strxml = null;

            try
            {
                // XmlTextReader reader = new XmlTextReader();
                if (File.Exists(FileName))
                {



                    XmlDocument doc = new XmlDocument();
                    doc.Load(FileName);
                    XmlNodeList bookList = doc.GetElementsByTagName(FieldName);
                    foreach (XmlNode node in bookList)
                    {
                        XmlElement bookElement = (XmlElement)node;
                        strxml = bookElement.GetElementsByTagName(ChildName)[0].InnerText;
                        strxml = strxml;

                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return strxml;
        }

        public static int Config()
        {


            try
            {
                return Convert.ToInt32(Utility.ReadXMLtoString("LABLinkConfig.XML", "Config", "MUTISERVICETYPE"));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// Ham thuc hien lấy dữ liệu tình trạng tham số có cho phép kết nối 
        /// lablink hay không? Nếu là 1 là connect và 0 là disconnect
        /// </summary>
        /// <returns></returns>
        public static int SysParamenterStatus()
        {

            try
            {
                return Convert.ToInt32(Utility.ReadXMLtoString("LABLinkConfig.XML", "Config", "CONNECTLAB"));
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        /// <summary>
        /// Hàm thực hiện so sách giá trị truyền vào với giá trị Value của Combobox, và trả lại Index của Combobox
        /// </summary>
        /// <param name="cbo">Giá trị Combobox</param>
        /// <param name="Value">Giá trị Của combobox</param>
        /// <param name="ValueToCompare">Giá trị Của giá trị so sách</param>
        public static void CompareToCombox(ComboBox FillCombox, string Value, int ValueToCompare)
        {
            try
            {
                if (FillCombox.Items.Count > 0)
                {
                    //MessageBox.Show("Ok");
                    for (int i = 0; i < FillCombox.Items.Count; i++)
                    {

                        DataRowView dr = (DataRowView)FillCombox.Items[i];
                        if (Convert.ToInt16(dr[Value].ToString()) == ValueToCompare)
                        {
                            FillCombox.SelectedIndex = i;
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                FillCombox.SelectedIndex = -1;

            }

        }
        public static decimal TotalToDatatable(DataTable dataTable, string FieldName, string Filter)
        {
            decimal total = 0;
            try
            {
                total = Utility.DecimaltoDbnull(dataTable.Compute("SUM(" + FieldName + ")", Filter), 0);

            }
            catch (Exception ex)
            {

            }
            return total;

        }
        /// <summary>
        /// hamg lay thong tin vao combox
        /// </summary>
        /// <param name="FillCombox"></param>
        /// <param name="Value"></param>
        /// <param name="ValueToCompare"></param>
        public static void CompareToCombox(ComboBox FillCombox, string Value, string ValueToCompare)
        {
            try
            {
                if (FillCombox.Items.Count > 0)
                {
                    //MessageBox.Show("Ok");
                    for (int i = 0; i < FillCombox.Items.Count; i++)
                    {

                        DataRowView dr = (DataRowView)FillCombox.Items[i];
                        if (dr[Value].ToString().Equals(ValueToCompare))
                        {
                            FillCombox.SelectedIndex = i;
                            break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                FillCombox.SelectedIndex = -1;

            }

        }
        public static bool ValiFloat(string str)
        {
            //Regex rgxstring = new Regex(@"^\d*[0-9](|,\d*[0-9])?$");
            Regex rgxString = new Regex(@"^\d*[0-9](|,\d*[0-9]|.\d*[0-9])?$");
            if (!rgxString.IsMatch(str))
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        /// <summary>
        /// hàm thực hiện đưa vào thực hiện báo hiển thị lỗi dùng control errorProvider
        /// </summary>
        /// <param name="errorProvider">Đối tượng errorProvider</param>
        /// <param name="objValidate">Đối tượng hiển thị lỗi</param>
        /// <param name="messageError">Message muốn hiển thị</param>
        public  static void SetMessageError(System.Windows.Forms.ErrorProvider errorProvider,System.Windows.Forms.Control  objValidate,string messageError)
        {
            
            errorProvider.SetError(objValidate, messageError);
        }
        public static void ResetMessageError(System.Windows.Forms.ErrorProvider errorProvider)
        {
            errorProvider.Clear();
        }
        /// <summary>
        /// hàm thực hiện relace string,break line
        /// </summary>
        /// <param name="yourString"></param>
        /// <returns></returns>
        public static string ReplaceString(string yourString)
        {
            return yourString.Replace("\r\n", "\n").Replace("\r", "\n");
        }
        #endregion

        public static byte GetPaymentType(string VoteName)
        {
            byte v_PaymentTypeId =0;
            switch (VoteName)
            {
                case "REG":
                    v_PaymentTypeId = 1;
                    break;
                case "ASSIGN":
                    v_PaymentTypeId = 2;
                    break;
                case "DRUG":
                    v_PaymentTypeId = 3;
                    break;
            }
            return v_PaymentTypeId;
        }
    }
    
    public class TraceInfor
    {
        public int ID = -1;
        public string BranchID = "Unknown";
        public string UserName = "Unknown";
        public string CreatedDate = System.DateTime.Now.ToShortDateString();
        public string IPAddress = "127.0.0.1";
        public string SubSystemName = "Unknown";
        public int FunctionID = -1;
        public string TableName = "Unknown";
        public string Desc = "Unknown";
        public Int16 LOT = 0;
        public string FunctionName = "";
        public string ComputerName = "";
        public string DLLName = "";
        public string AccountName = "";
        public TraceInfor()
        {
        }
        public TraceInfor(string BranchID, string UserName, string Date, string IPAddress, string DLLName, string SubSystemName, int FunctionID, string FunctionName, string ComputerName, string AccountName)
        {
            this.BranchID = BranchID;
            this.UserName = UserName;
            this.CreatedDate = Date;
            this.IPAddress = IPAddress;
            this.SubSystemName = SubSystemName;
            this.FunctionID = FunctionID;
            this.DLLName = DLLName;
            this.FunctionID = FunctionID;
            this.FunctionName = FunctionName;
            this.ComputerName = ComputerName;
            this.AccountName = AccountName;
        }
        ///<summary>
        ///<para>Phương thức khởi tạo có tham số</para>
        ///</summary>   
        /// <param name="BranchID"><para>Mã đơn vị quản lý</para></param>     
        /// <param name="UserName"><para>User đang thao tác với chức năng. Là User đăng nhập</para></param>     
        /// <param name="Date"><para>Ngày thao tác: DateTime.Now.ToShortDateString()<see cref="DateTime.Now"/></para></param>     
        /// <param name="IPAddress"><para>Địa chỉ của máy tính chạy Client</para></param>     
        /// <param name="SubSystemName"><para>Tên phân hệ(Tên DLL trong winform hoặc tên bằng chữ trên webform).System.Reflection.Assembly.GetExecutingAssembly().FullName;</para></param>     
        /// <param name="FunctionID"><para>Tên hàm(Tên form trong winform hoặc tên bằng chữ của chức năng trong webform)</para></param>     
        /// <param name="Table_Name"><para>Tên bảng bị tác động thay đổi dữ liệu</para></param>     
        /// <param name="Desc"><para>Mô tả thao tác chức năng. Ví dụ:"Thêm mới nước có mã: " + objInfor.pMA_NUOC</para></param>     
        /// <param name="LO"><para></para>Xác định việc cập nhật thông tin vào CSDL theo một lô hay không. </param>     
        public TraceInfor(string BranchID, string UserName, string Date, string IPAddress, string DLLName, string SubSystemName, int FunctionID, string FunctionName, string ComputerName, string AccountName, string Table_Name, string Desc, Int16 LOT)
        {
            this.BranchID = BranchID;
            this.UserName = UserName;
            this.CreatedDate = Date;
            this.IPAddress = IPAddress;
            this.SubSystemName = SubSystemName;
            this.DLLName = DLLName;
            this.FunctionID = FunctionID;
            this.FunctionName = FunctionName;
            this.ComputerName = ComputerName;
            this.AccountName = AccountName;
            this.TableName = Table_Name;
            this.Desc = Desc;
            this.LOT = LOT;
        }
        ///<summary>
        ///<para>set hoặc get giá trị ID của Trace</para>
        ///</summary>  
        public int pID
        {
            set { ID = value; }
            get { return ID; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho mã đơn vị quản lý</para>
        ///</summary>  
        public string pBranchID
        {
            set { BranchID = value; }
            get { return BranchID; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho Người thao tác(Username)</para>
        ///</summary>  
        public string pUserName
        {
            set { UserName = value; }
            get { return UserName; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho ngày thao tác</para>
        ///</summary>  
        public string pCreatedDate
        {
            set { CreatedDate = value; }
            get { return CreatedDate; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho địa chỉ máy tính chạy Client</para>
        ///</summary>  
        public string pIPAddress
        {
            set { IPAddress = value; }
            get { return IPAddress; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho tên phân hệ</para>
        ///</summary>  
        public string pSubSystemName
        {
            set { SubSystemName = value; }
            get { return SubSystemName; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho tên chức năng</para>
        ///</summary>  
        public int pFunctionID
        {
            set { FunctionID = value; }
            get { return FunctionID; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho tên bảng bị tác động thay đổi dữ liệu bởi nghiệp vụ.</para>
        ///</summary>  
        public string pTableName
        {
            set { TableName = value; }
            get { return TableName; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho mô tả thao tác</para>
        ///</summary>  
        public string pDesc
        {
            set { Desc = value; }
            get { return Desc; }
        }
        ///<summary>
        ///<para>set hoặc get giá trị cho việc cập nhật theo Lô</para>
        ///</summary>  
        public Int16 pLO
        {
            set { LOT = value; }
            get { return LOT; }
        }
    }
    //----------------------------------------------------------------------------
    //-----------------------------NEW CLASS--------------------------------------
    //----------------------------------------------------------------------------
    ///<summary>
    ///<para>Lớp Trace dùng để lưu vết thao tác của người dùng</para>
    ///</summary> 
    public class TraceController
    {
        public TraceInfor Infor;
        ///<summary>
        ///<para>Phương thức khởi tạo không có tham số. Các thuộc tính lấy giá trị default</para>
        ///</summary>   
        public TraceController()
        {
        }
        public TraceController(TraceInfor Infor)
        {

            this.Infor = Infor;
        }


        ///<summary>
        ///<para>Lưu lại giá trị vết vào CSDL</para>
        ///</summary>   
        ///<returns>true nếu thành công và false nếu thất bại.</returns>
        public ActionResult Save(System.Data.SqlClient.SqlTransaction Trans)
        {

            //string SQLStatement = "INSERT INTO Sys_TRACE(BranchID,UserName,CreatedDate,IPAddress,ComputerName,AccountName,DLLName,SubSystemName,FunctionID,FunctionName,TableName,[Desc],LOT)";
            //SQLStatement += " VALUES('" + Infor.BranchID + "','" + Infor.UserName + "',CONVERT(datetime,'" + Infor.CreatedDate + "',103),'";
            //SQLStatement += Infor.IPAddress + "',N'" + Infor.ComputerName + "',N'" + Infor.AccountName + "',N'" + Infor.DLLName + "',N'" + Infor.SubSystemName + "'," + Infor.FunctionID + ",N'" + Infor.FunctionName + "',N'" + Infor.TableName + "',N'" + Infor.Desc + "'," + Infor.LOT + ")";

            //try
            //{
            //    return DataAccessLayer.DAL.DataAccess.ExecuteNonQuery(Trans, CommandType.Text, SQLStatement) > 0 ? ActionResult.Success : ActionResult.Error;

            //}
            //catch (Exception ex)
            //{

            //    Utility.ShowMsg("Lỗi khi lưu vết:\n" + ex.Message);
            return ActionResult.Exception;
            //}
        }
        ///<summary>
        ///<para>Đưa giá trị của vết vào Header trước khi gửi lên Business xử lý</para>
        ///</summary>   
        /// <param name="Header"><para>Bảng lưu vết</para></param>     
        /// <param name="trace"><para>Đối tượng vết <see cref="HISTrace"/></para></param>     
        public void MergeTraceIntoHeader(ref DataTable Header, TraceInfor trace)
        {
            try
            {
                DataRow dr;
                bool bExist = false;
                if (Header.Equals(null))
                {
                    Utility.ShowMsg("Không thể gắn Trace vào Header vì đối tượng Header bạn truyền vào chưa được khởi tạo. Đề nghị bạn xem lại...");
                    return;
                }
                if (Header.Rows.Count > 0)
                {
                    dr = Header.Rows[0];
                    bExist = true;
                }
                else
                {
                    bExist = false;
                    dr = Header.NewRow();
                }
                //Read from Header Into Trace
                if (trace == null) return;
                dr["ID"] = Utility.Int16Dbnull(trace.pID, 0);

                dr["BranchID"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pBranchID));
                dr["UserName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pUserName));
                dr["CreatedDate"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pCreatedDate, DateTime.Now.ToShortDateString()));
                dr["IPAddress"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pIPAddress));
                dr["ComputerName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.ComputerName));
                dr["AccountName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.AccountName));
                dr["DLLName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.DLLName));
                dr["SubSystemName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pSubSystemName));
                dr["FunctionID"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pFunctionID));
                dr["FunctionName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.FunctionName));
                dr["TableName"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pTableName));
                dr["Desc"] = Utility.CorrectStringValue(Utility.sDbnull(trace.pDesc));
                dr["LOT"] = Utility.Int16Dbnull(trace.pLO, 0);
                if (!bExist)
                {
                    Header.Rows.Add(dr);
                }
                Header.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi tạo gán thông tin Trace vào Header:\n" + ex.Message);
                return;
            }
        }
        ///<summary>
        ///<para>Tạo thông tin vết từ Header</para>
        ///</summary>   
        /// <param name="Header"><para>Bảng lưu vết trong Header</para></param>     
        public void GetTraceInfoFromHeader(DataTable Header)
        {

            try
            {
                if (Header.Equals(null))
                {
                    return;
                }

                //Read from Header Into Trace
                Infor.ID = Utility.Int32Dbnull(Header.Rows[0]["ID"], 0);
                Infor.BranchID = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["BranchID"]));
                Infor.UserName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["UserName"]));
                Infor.CreatedDate = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["CreatedDate"]));
                Infor.IPAddress = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["IPAddress"]));
                Infor.ComputerName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["ComputerName"]));
                Infor.AccountName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["AccountName"]));
                Infor.DLLName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["DLLName"]));
                Infor.SubSystemName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["SubSystemName"]));
                Infor.FunctionID = Utility.Int16Dbnull(Header.Rows[0]["FunctionID"]);
                Infor.FunctionName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["FunctionName"]));
                Infor.TableName = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["TableName"]));
                Infor.Desc = Utility.CorrectStringValue(Utility.sDbnull(Header.Rows[0]["Desc"]));
                Infor.LOT = Utility.Int16Dbnull(Header.Rows[0]["LOT"], 0);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lấy thông tin đối tượng lưu vết từ Header\n" + ex.Message);
                return;
            }
        }


    }

    class CustomSqlProvider : SqlDataProvider
    {
        public CustomSqlProvider(string connectionString)
        {
            this.DefaultConnectionString = connectionString;
        }

        public override string Name
        {
            get
            {
                return "ORM";
            }
        }
    }
    public class MoneyByLetter
    {

        public string sMoneyToLetter(string strNumber)
        {
            if (!Utility.IsNumeric(strNumber)) return "Số tiền phải là chữ số. Đề nghị bạn kiểm tra lại";
            strNumber = Convert.ToInt64(Convert.ToDouble(strNumber)).ToString();
            string functionReturnValue = null;
            string chu = "";
            int lng;
            string CD;
            int dd;
            int i;
            string TP;
            bool bolCheck;
            bool BolSoAm = false;
            if (Strings.Left(strNumber, 1) == "-")
            {
                // số âm
                BolSoAm = true;
                strNumber = strNumber.Substring(1);
            }
            bolCheck = false;
            CD = "";
            TP = "";
            if (strNumber == "")
            {
                functionReturnValue = "";
                return ""; // TODO: might not be correct. Was : Exit Function
            }
            //If Trim(Str$(strNumber)) <> "" Then
            if (Strings.Trim(strNumber) != "")
            {
                for (i = 0; i <= Strings.Len(strNumber); i++)
                {
                    if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                    {
                        bolCheck = true;
                        CD = Strings.Left(strNumber, i - 1);
                        TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                        TP = TP + "0";
                        TP = Strings.Left(TP, 2);
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (bolCheck == false)
                {
                    //                CD = Trim(Str$(strNumber))
                    CD = Strings.Trim(strNumber);
                }
                lng = Strings.Len(CD);
                switch (lng)
                {
                    case 0:
                        chu = " ";
                        break;
                    case 1:
                        chu = DonVi(CD);
                        break;
                    case 2:
                        chu = Chuc(CD);
                        break;
                    case 3:
                        chu = Tram(CD);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        chu = Nghin(CD);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        chu = Trieu(CD);
                        break;
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                        chu = Ti(CD);
                        break;
                }

                dd = Strings.Len(Strings.Trim(chu));
                functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                functionReturnValue = functionReturnValue.Replace(" không trăm  nghìn", "");
                functionReturnValue = functionReturnValue.Replace(" không trăm  triệu", "");
                functionReturnValue = functionReturnValue.Replace("  ", " ");
                if (TP != "")
                {
                    i = Convert.ToInt32(TP);
                    lng = Strings.Len(TP);
                    if (lng > 0)
                    {
                        switch (lng)
                        {
                            case 1:
                                chu = DonVi(TP);
                                break;
                            case 2:
                                chu = ChucHao(TP);
                                break;
                        }
                        dd = Strings.Len(Strings.Trim(chu));
                        functionReturnValue = functionReturnValue + " " + Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                    }
                }
            }
            if (BolSoAm) functionReturnValue = "Âm " + functionReturnValue;
            return functionReturnValue.Replace("không xu", "");
        }
        public string MoneyByLetterShort(string strNumber, int muc)
        {
            string functionReturnValue = null;
            string chu = "";
            int lng;
            string CD;
            int dd;
            int i;
            string TP;
            bool bolCheck;
            bool BolSoAm = false;
            if (Strings.Left(strNumber, 1) == "-")
            {
                // số âm
                BolSoAm = true;
                strNumber = strNumber.Substring(1);
            }
            bolCheck = false;
            CD = "";
            TP = "";
            if (strNumber == "")
            {
                functionReturnValue = "";
                return ""; // TODO: might not be correct. Was : Exit Function
            }
            //If Trim(Str$(strNumber)) <> "" Then
            if (Strings.Trim(strNumber) != "")
            {
                for (i = 0; i <= Strings.Len(strNumber); i++)
                {
                    if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                    {
                        bolCheck = true;
                        CD = Strings.Left(strNumber, i - 1);
                        TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                        TP = TP + "0";
                        TP = Strings.Left(TP, 2);
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
                if (bolCheck == false)
                {
                    //                CD = Trim(Str$(strNumber))
                    CD = Strings.Trim(strNumber);
                }
                lng = Strings.Len(CD);
                switch (lng)
                {
                    case 0:
                        chu = " ";
                        break;
                    case 1:
                        chu = DonVi(CD);
                        break;
                    case 2:
                        chu = ChucShort(CD);
                        break;
                    case 3:
                        chu = TramShort(CD, muc);
                        break;
                    case 4:
                    case 5:
                    case 6:
                        chu = NghinShort(CD, muc);
                        break;
                    case 7:
                    case 8:
                    case 9:
                        chu = TrieuShort(CD, muc);
                        break;
                    case 10:
                    case 11:
                    case 12:
                        chu = TiShort(CD, muc);
                        break;
                }

                dd = Strings.Len(Strings.Trim(chu));
                functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                functionReturnValue = functionReturnValue.Replace("  ", " ");
                functionReturnValue = functionReturnValue.Replace(" không trăm nghìn", "");
                functionReturnValue = functionReturnValue.Replace(" không trăm triệu", "");
                //MoneyByLetterShort = Replace(MoneyByLetterShort, " mươi", "")
                //If muc = 2 Then
                //    MoneyByLetterShort = Replace(MoneyByLetterShort, " trăm", "")
                //End If
                if (TP != "")
                {
                    i = Convert.ToInt32(TP);
                    lng = Strings.Len(TP);
                    if (lng > 0)
                    {
                        switch (lng)
                        {
                            case 1:
                                chu = DonVi(TP);
                                break;
                            case 2:
                                chu = ChucHaoShort(TP);
                                break;
                        }
                        dd = Strings.Len(Strings.Trim(chu));
                        functionReturnValue = functionReturnValue + " " + Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                    }
                }
            }
            return functionReturnValue;
            //If BolSoAm Then MoneyByLetter = "Âm " & MoneyByLetter
        }
        private string ChucHao(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "0":
                        tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                        break;
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }
        private string ChucHaoShort(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "0":
                        tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                        break;
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }
        private string DonVi(string d)
        {
            string functionReturnValue = null;
            int tmp;
            tmp = Strings.Len(d);
            if (tmp == 0)
            {
                functionReturnValue = " ";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "0":
                        functionReturnValue = "không ";
                        break;
                    case "1":
                        functionReturnValue = "một ";
                        break;
                    case "2":
                        functionReturnValue = "hai ";
                        break;
                    case "3":
                        functionReturnValue = "ba ";
                        break;
                    case "4":
                        functionReturnValue = "bốn ";
                        break;
                    case "5":
                        functionReturnValue = "năm ";
                        break;
                    case "6":
                        functionReturnValue = "sáu ";
                        break;
                    case "7":
                        functionReturnValue = "bảy ";
                        break;
                    case "8":
                        functionReturnValue = "tám ";
                        break;
                    case "9":
                        functionReturnValue = "chín ";
                        break;
                }
            }
            return functionReturnValue;
        }

        private string Chuc(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "mười lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }
        private string ChucShort(string d)
        {
            string tmp = "";
            if (Strings.Len(d) != 2)
            {
                tmp = "Error";
            }
            else
            {
                switch (Strings.Mid(d, 1, 1))
                {
                    case "1":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = "mười ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = "mười lăm ";
                        }
                        else
                        {
                            tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        if (Strings.Mid(d, 2, 1) == "0")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "1")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "4")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                        }
                        else if (Strings.Mid(d, 2, 1) == "5")
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                        }
                        else
                        {
                            tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                        }

                        break;
                }
            }
            return tmp;
        }
        private string Tram(string d)
        {
            string functionReturnValue = null;
            string tmp = "";
            string d1;
            string d2;
            string d3;
            string Temp = "";

            d1 = Strings.Mid(d, 1, 1);
            d2 = Strings.Mid(d, 2, 1);
            d3 = Strings.Mid(d, 3, 1);
            if (Strings.Len(d) != 3)
            {
                Temp = "Error";
            }
            else
            {
                switch (d2)
                {
                    case "0":
                        if (d3 == "0")
                        {
                            tmp = DonVi(d1) + "trăm  ";
                        }
                        else if (d3 == "1")
                        {
                            tmp = DonVi(d1) + "trăm linh một ";
                        }
                        else if (d3 == "4")
                        {
                            tmp = DonVi(d1) + "trăm linh tư ";
                        }
                        else if (d3 == "5")
                        {
                            tmp = DonVi(d1) + "trăm linh năm ";
                        }
                        else
                        {
                            tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                        }

                        break;
                    case "1":
                        if (d3 == "0")
                        {
                            tmp = DonVi(d1) + "trăm mười ";
                        }
                        else
                        {
                            tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                        }

                        break;
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                        // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                        tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                        break;
                }
                functionReturnValue = tmp;
            }
            return functionReturnValue;
        }
        private string TramShort(string d, int muc)
        {
            string functionReturnValue = null;
            string tmp = "";
            string d1;
            string d2;
            string d3;
            string Temp = "";
            if (muc == 2)
            {
                d1 = Strings.Mid(d, 1, 1);
                d2 = Strings.Mid(d, 2, 1);
                d3 = Strings.Mid(d, 3, 1);
                if (Strings.Len(d) != 3)
                {
                    Temp = "Error";
                }
                else
                {
                    switch (d2)
                    {
                        case "0":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm  ";
                            }
                            else if (d3 == "1")
                            {
                                tmp = DonVi(d1) + "linh một ";
                            }
                            else if (d3 == "4")
                            {
                                tmp = DonVi(d1) + "linh tư ";
                            }
                            else if (d3 == "5")
                            {
                                tmp = DonVi(d1) + "linh năm ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "linh " + DonVi(d3);
                            }

                            break;
                        case "1":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm mười ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + ChucShort(d2 + d3);
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                            tmp = DonVi(d1) + ChucShort(d2 + d3);
                            break;
                    }
                    functionReturnValue = tmp;
                }
            }
            else
            {
                d1 = Strings.Mid(d, 1, 1);
                d2 = Strings.Mid(d, 2, 1);
                d3 = Strings.Mid(d, 3, 1);
                if (Strings.Len(d) != 3)
                {
                    Temp = "Error";
                }
                else
                {
                    switch (d2)
                    {
                        case "0":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm  ";
                            }
                            else if (d3 == "1")
                            {
                                tmp = DonVi(d1) + "trăm linh một ";
                            }
                            else if (d3 == "4")
                            {
                                tmp = DonVi(d1) + "trăm linh tư ";
                            }
                            else if (d3 == "5")
                            {
                                tmp = DonVi(d1) + "trăm linh năm ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                            }

                            break;
                        case "1":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm mười ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                            tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            break;
                    }
                    functionReturnValue = tmp;
                }
            }
            return functionReturnValue;

        }
        private string Nghin(string d)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 3;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 3);
            if (s2 == "000")
            {
                tmp1 = "nghìn ";
            }
            else
            {
                tmp1 = "nghìn " + Tram(s2);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = Chuc(s1) + tmp1;
                    break;
                case 3:
                    tmp = Tram(s1) + tmp1;
                    break;
            }
            return tmp;
        }
        private string NghinShort(string d, int muc)
        {
            string tmp = "";
            string s1;
            string s2;
            string tmp1;
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 3;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 3);
            if (s2 == "000")
            {
                tmp1 = "nghìn ";
            }
            else
            {
                tmp1 = "nghìn " + TramShort(s2, muc);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = ChucShort(s1) + tmp1;
                    break;
                case 3:
                    tmp = TramShort(s1, muc) + tmp1;
                    break;
            }
            return tmp;
        }
        private string Trieu(string d)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 6;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 6);
            if (s2 == "000000")
            {
                tmp1 = "triệu ";
            }
            else
            {
                tmp1 = "triệu " + Nghin(s2);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = Chuc(s1) + tmp1;
                    break;
                case 3:
                    tmp = Tram(s1) + tmp1;
                    break;
            }
            return tmp;
        }
        private string TrieuShort(string d, int muc)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 6;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 6);
            if (s2 == "000000")
            {
                tmp1 = "triệu ";
            }
            else
            {
                tmp1 = "triệu " + NghinShort(s2, muc);
            }
            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = ChucShort(s1) + tmp1;
                    break;
                case 3:
                    tmp = TramShort(s1, muc) + tmp1;
                    break;
            }
            return tmp;
        }
        private string Ti(string d)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 9;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 9);

            if (s2 == "000000000")
            {
                tmp1 = "tỷ ";
            }
            else
            {
                tmp1 = "tỷ " + Trieu(s2);
            }

            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = Chuc(s1) + tmp1;
                    break;
                case 3:
                    tmp = Tram(s1) + tmp1;
                    break;
                case 4:
                    tmp = Nghin(s1) + tmp1;
                    break;
                case 5:
                    tmp = "(Số tiền quá lớn. Bạn cần xem lại đơn hàng)";
                    break;
            }
            return tmp;
        }
        private string TiShort(string d, int muc)
        {
            string tmp = "";
            string s1 = "";
            string s2 = "";
            string tmp1 = "";
            int dai;
            int ln;
            dai = Strings.Len(d);
            ln = dai - 9;
            s1 = Strings.Mid(d, 1, ln);
            s2 = Strings.Mid(d, ln + 1, 9);

            if (s2 == "000000000")
            {
                tmp1 = "tỷ ";
            }
            else
            {
                tmp1 = "tỷ " + TrieuShort(s2, muc);
            }

            switch (Strings.Len(s1))
            {
                case 1:
                    tmp = DonVi(s1) + tmp1;
                    break;
                case 2:
                    tmp = ChucShort(s1) + tmp1;
                    break;
                case 3:
                    tmp = TramShort(s1, muc) + tmp1;
                    break;
                case 4:
                    tmp = TrieuShort(s1, muc) + tmp1;
                    break;
            }
            return tmp;
        }
    }
    public class cls_SignInfor
    {

        #region "Khai bao bien"
        public string mv_TEN_BIEUBC = "";
        public string mv_TEN_DOITUONG = "";
        public int mv_CHIEU_RONG = 0;
        public int mv_CHIEU_DAI = 0;
        public int mv_TOADO_DOC = 0;
        public int mv_TOADO_NGANG = 0;
        public string mv_FONT_CHU = "Arial";
        public int mv_CO_CHU = 12;
        public string mv_KIEU_CHU = "Regular";
        public string mv_NOI_DUNG = "";
        public string mv_MA_DVIQLY = "";
        public System.DateTime mv_NGAY_CAPNHAT = new System.DateTime();
        private bool exists = false;
        #endregion
        #region "Properties"
        public bool _TonTai
        {
            get { return exists; }
        }
        #endregion
        #region "Cac ham khoi tao"
        public cls_SignInfor()
        {
            exists = false;
        }
        //ham nay se thuc hien lay thong tin tren server ve
        public cls_SignInfor(string sv_sBieuMau, string sv_sDonVi)
        {
            exists = true;
            SqlCommand cmdNguoiKy = new SqlCommand();
            DataTable dtbNguoiKy = new DataTable();
            try
            {
                {
                    cmdNguoiKy.Connection = globalVariables.SqlConn;
                    cmdNguoiKy.CommandType = CommandType.Text;
                    cmdNguoiKy.CommandText = "Select * from Sys_trinhKy Where ReportName = N'" + sv_sBieuMau + "'";
                }
                SqlDataAdapter adtNguoiKy = new SqlDataAdapter(cmdNguoiKy);
                adtNguoiKy.Fill(dtbNguoiKy);
            }
            catch (Exception ex)
            {
                exists = false;
            }
            if (exists == false) return;


            try
            {
                if (dtbNguoiKy.Rows.Count > 0)
                {
                    //gan cac thong tin co duoc vao bien
                    mv_TEN_BIEUBC = (string)dtbNguoiKy.Rows[0]["ReportName"];
                    try
                    {
                        mv_TEN_DOITUONG = (string)dtbNguoiKy.Rows[0]["ObjectName"];
                    }
                    catch (Exception ex)
                    {
                        mv_TEN_DOITUONG = " ";
                    }

                    mv_FONT_CHU = (string)dtbNguoiKy.Rows[0]["Font_Name"];
                    mv_CO_CHU = (int)dtbNguoiKy.Rows[0]["Font_Size"];
                    mv_KIEU_CHU = (string)dtbNguoiKy.Rows[0]["Font_Stype"];
                    try
                    {
                        mv_NOI_DUNG = (string)dtbNguoiKy.Rows[0]["ObjectContent"];
                    }
                    catch (Exception ex)
                    {
                        mv_NOI_DUNG = " ";
                    }
                }
                else
                {
                    exists = false;
                }
            }
            catch (Exception ex)
            {
                exists = false;
            }

        }

        //ham nay se thuc hien lay thong tin tu file rpt
        public cls_SignInfor(CrystalDecisions.CrystalReports.Engine.FieldObject rpt, string sv_sDonVi, string baoCao, string pv_sContent)
        {
            exists = false;
            try
            {
                //gan cac thong tin co duoc vao bien
                mv_TEN_BIEUBC = baoCao;
                //rpt.ToString
                mv_TEN_DOITUONG = "txtNguoiKy1";
                mv_FONT_CHU = rpt.Font.Name;
                mv_CO_CHU = (int)rpt.Font.Size;
                switch (rpt.Font.Style)
                {
                    case FontStyle.Bold:
                        mv_KIEU_CHU = "Chữ đậm";
                        break;
                    case FontStyle.Italic:
                        mv_KIEU_CHU = "Chữ nghiêng";
                        break;
                    case FontStyle.Regular:
                        mv_KIEU_CHU = "Bình thường";
                        break;
                    default:
                        mv_KIEU_CHU = "Bình thường";
                        break;
                }
                mv_NOI_DUNG = pv_sContent.Replace("\"", "");
                mv_MA_DVIQLY = sv_sDonVi;
                mv_NGAY_CAPNHAT = new System.DateTime();
            }
            catch (Exception ex)
            {

            }
        }

        #endregion
        #region "Cac thu tuc"
        //thu tuc de gan toan bo thong tin cua doi tuong vao RPT
        public void setValueToRPT(ref CrystalDecisions.CrystalReports.Engine.FieldObject rpt)
        {
            //rpt.ToString = sv_TEN_BIEUBC       'La duy nhat cho 1 file trong CT
            //rpt.Height = mv_CHIEU_RONG      'Do rong cua o text
            //rpt.Width = mv_CHIEU_DAI        'chieu dai cua o text
            //rpt.Top = mv_TOADO_DOC          'toa do docn(y) cua o text
            //rpt.Left = mv_TOADO_NGANG       'toa do ngang cua o text(tinh trong section do)
            //rpt.Text = mv_NOI_DUNG     'Noi dung hien thi
            //don vi
            //ngay thay doi thong tin
            Font ft = null;
            try
            {
                switch (mv_KIEU_CHU.Trim().ToUpper())
                {
                    case "CHỮ ĐẬM":
                        ft = new Font(mv_FONT_CHU, mv_CO_CHU, FontStyle.Bold);
                        break;
                    case "CHỮ NGHIÊNG":
                        ft = new Font(mv_FONT_CHU, mv_CO_CHU, FontStyle.Italic);
                        break;
                    case "BÌNH THƯỜNG":
                        ft = new Font(mv_FONT_CHU, mv_CO_CHU, FontStyle.Regular);
                        break;
                    default:
                        ft = new Font(mv_FONT_CHU, mv_CO_CHU, FontStyle.Regular);
                        break;
                }
            }
            catch (Exception ex)
            {
                //font mac dinh
                ft = new Font("Arial", 13, FontStyle.Regular);
            }
            rpt.ApplyFont(ft);
            //Font: cho phep thay doi
        }
        //thu tuc de ghi cau hinh cua file RPT len Database
        public void updateRPTtoDB(CrystalDecisions.CrystalReports.Engine.TextObject rpt, string madonvi, string filenane)
        {
            SqlCommand cmdNguoiKy = new SqlCommand();
            string strSQL = null;
            if (exists == true)
            {
                strSQL = "Update Sys_TRINHKY set";
                strSQL += " OBJECTName = N'" + mv_TEN_DOITUONG + "'";
                strSQL += " ,Font_Name = N'" + mv_FONT_CHU + "'";
                strSQL += " ,Font_Size = " + mv_CO_CHU;
                strSQL += " ,Font_Stype = N'" + mv_KIEU_CHU + "'";
                strSQL += " ,ObjectContent = N'" + mv_NOI_DUNG + "'";
                strSQL += " Where ReportNAME = N'" + mv_TEN_BIEUBC + "'";
            }
            else
            {
                strSQL = "Insert Into Sys_TRINHKY(ReportNAME, ObjectName,  Font_Name, Font_Size, Font_Stype, ObjectContent)";
                strSQL += " Values(";
                strSQL += " N'" + mv_TEN_BIEUBC + "'";
                strSQL += " ,N'" + mv_TEN_DOITUONG + "'";
                strSQL += " ,N'" + mv_FONT_CHU + "'";
                strSQL += " ," + mv_CO_CHU;
                strSQL += " ,N'" + mv_KIEU_CHU + "'";
                strSQL += " ,N'" + mv_NOI_DUNG + "')";

            }
            try
            {
                {
                    cmdNguoiKy.Connection = globalVariables.SqlConn;
                    cmdNguoiKy.CommandType = CommandType.Text;
                    cmdNguoiKy.CommandText = strSQL;
                    cmdNguoiKy.ExecuteNonQuery();
                }
                exists = true;
            }
            catch (Exception ex)
            {
                exists = false;
            }
        }
        //thu tuc de ghi cau hinh cua file RPT len Database
        public void updateRPTtoDB()
        {
            SqlCommand cmdNguoiKy = new SqlCommand();
            string strSQL = null;
            if (exists == true)
            {
                strSQL = "Update Sys_TRINHKY set";
                strSQL += " ObjectName = N'" + mv_TEN_DOITUONG + "'";
                strSQL += " ,Font_Name = N'" + mv_FONT_CHU + "'";
                strSQL += " ,Font_Size = " + mv_CO_CHU;
                strSQL += " ,Font_Stype = N'" + mv_KIEU_CHU + "'";
                strSQL += " ,ObjectContent = N'" + mv_NOI_DUNG + "'";
                strSQL += " Where ReportName = N'" + mv_TEN_BIEUBC + "'";
            }
            else
            {
                strSQL = "Insert Into Sys_TRINHKY(ReportName, ObjectName,  Font_Name, Font_Size, Font_Stype, ObjectContent)";
                strSQL += " Values(";
                strSQL += " N'" + mv_TEN_BIEUBC + "'";
                strSQL += " ,N'" + mv_TEN_DOITUONG + "'";
                strSQL += " ,N'" + mv_FONT_CHU + "'";
                strSQL += " ," + mv_CO_CHU;
                strSQL += " ,N'" + mv_KIEU_CHU + "'";
                strSQL += " ,N'" + mv_NOI_DUNG + "')";

            }
            try
            {
                {
                    cmdNguoiKy.Connection = globalVariables.SqlConn;
                    cmdNguoiKy.CommandType = CommandType.Text;
                    cmdNguoiKy.CommandText = strSQL;
                    cmdNguoiKy.ExecuteNonQuery();
                }
                exists = true;
            }
            catch (Exception ex)
            {
                //exists = False
            }
        }
        #endregion

    }
    public class objHisLink
    {
        private static short ServiceType_ID = -1;
        private static short Service_ID = -1;
        private static short ObjectType_ID = -1;
        private static bool b_Sended = false;
        private static long l_AssignInfo = 0;
        /// <summary>
        /// Ham thuc hien laybein cua ServiceType_ID
        /// </summary>
        public static short GetServiceType_ID
        {
            get { return ServiceType_ID; }
            set
            {

                ServiceType_ID = value;
            }
        }
        /// <summary>
        /// Ham thuc hien lai ID cua xet dich vu
        /// </summary>
        public static long GetAssignInfo_ID
        {
            get { return l_AssignInfo; }
            set
            {

                l_AssignInfo = value;
            }
        }
        public static bool GetStatusSended
        {
            get { return b_Sended; }
            set
            {

                b_Sended = value;
            }
        }
        public static short GetService_ID
        {
            get { return Service_ID; }
            set
            {

                Service_ID = value;
            }
        }
        public static short GetObjectType_ID
        {
            get { return ObjectType_ID; }
            set
            {

                ObjectType_ID = value;
            }
        }

        public static class DataBinding
        {

            /// <summary>
            /// Help to bind the data
            /// </summary>
            /// <param name="control"></param>
            /// <param name="data"></param>
            /// <param name="dataValueField"></param>
            /// <param name="dataTextField"></param>
            public static void BindData(Control control, object data, string dataValueField, string dataTextField)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {
                    var listControl = control as ListControl;

                    if (listControl != null)
                    {
                        listControl.DataSource = data;
                        listControl.ValueMember = dataValueField;
                        listControl.DisplayMember = dataTextField;

                    }

                }
                if (typeof(IDataBoundable).IsInstanceOfType(control))
                {
                    var dataBoundControl = control as IDataBoundable;
                    if (dataBoundControl != null)
                    {
                        dataBoundControl.DataSource = data;
                        dataBoundControl.DataBind();
                    }
                }
            }

            public static void BindDataCombox(Control control, object data, string dataValueField, string dataTextField)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {

                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                    dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = "--- Chọn ---";
                    dr[dataValueField] = "-1";
                    dt.Rows.InsertAt(dr, 0);
                    if (combox != null)
                    {

                        combox.DataSource = dt;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;


                    }

                }
                if (typeof(IDataBoundable).IsInstanceOfType(control))
                {
                    var dataBoundControl = control as IDataBoundable;
                    if (dataBoundControl != null)
                    {
                        dataBoundControl.DataSource = data;
                        dataBoundControl.DataBind();
                    }
                }
            }

            /// <summary>
            /// Hàm thực hiện convert từ datetime thành string
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string ConvertDate(DateTime str)
            {
                return str.ToString("dd/MM/yyyy");
            }
            public static bool IsCheckConnected(string strservers, string DbName, string users, string password)
            {

                string strsql = "";
                // Common.cTripleDES MaHoa = new cTripleDES();
                bool flag = false;
                //string servers = MaHoa.Decrypt(strservers);
                //string DataName = MaHoa.Decrypt(DbName);
                //string User_ID = MaHoa.Decrypt(users);
                //string Password = MaHoa.Decrypt(password);               
                strsql += "packet size=4096;workstation id=" + strservers;
                strsql += ";data source=" + strservers;
                strsql += " ;persist security info=False;initial catalog=" + DbName;
                strsql += ";uid=" + users;
                strsql += ";pwd=" + password;
                SqlConnection cnn = new SqlConnection(strsql);
                ///Mở CSDL
                try
                {
                    // Me.Cursor = Cursors.WaitCursor
                    // this.Cursor = Cursors.WaitCursor;
                    cnn.Open();
                    //  MessageBox.Show("Kết nối thành công. Chúc mừng bạn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cnn.Close();
                    cnn.Dispose();
                    // cmdSave.Focus();
                    //this.Cursor = Cursors.Default;
                    flag = true;
                }

                catch
                {
                    flag = false;
                }
                return flag;
            }
            public static void BindDataCombox(Control control, object data, string dataValueField, string dataTextField, string SelectItem)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {

                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                    dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = SelectItem;
                    dr[dataValueField] = "-1";
                    dt.Rows.InsertAt(dr, 0);
                    if (combox != null)
                    {

                        combox.DataSource = dt;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;


                    }

                }
                if (typeof(IDataBoundable).IsInstanceOfType(control))
                {
                    var dataBoundControl = control as IDataBoundable;
                    if (dataBoundControl != null)
                    {
                        dataBoundControl.DataSource = data;
                        dataBoundControl.DataBind();
                    }
                }
            }
            public static void BindDataComboxDefaultView(Control control, object data, bool TrueDefaultView, string dataValueField, string dataTextField, string SelectItem)
            {
                if (TrueDefaultView)
                {
                    if (typeof(ListControl).IsInstanceOfType(control))
                    {

                        ComboBox combox = control as ComboBox;
                        DataTable dt = new DataTable();
                        dt = (DataTable)data;
                        DataRow dr = dt.NewRow();
                        dr[dataTextField] = SelectItem;
                        dr[dataValueField] = "-1";
                        dt.Rows.InsertAt(dr, 0);

                        if (combox != null)
                        {

                            combox.DataSource = dt.DefaultView;
                            combox.ValueMember = dataValueField;
                            combox.DisplayMember = dataTextField;


                        }

                    }
                }
                else
                {
                    if (typeof(ListControl).IsInstanceOfType(control))
                    {

                        ComboBox combox = control as ComboBox;
                        DataTable dt = new DataTable();
                        dt = (DataTable)data;
                        DataRow dr = dt.NewRow();
                        dr[dataTextField] = "--- Chọn ---";
                        dr[dataValueField] = "0";
                        dt.Rows.InsertAt(dr, 0);
                        if (combox != null)
                        {

                            combox.DataSource = dt;
                            combox.ValueMember = dataValueField;
                            combox.DisplayMember = dataTextField;


                        }

                    }
                }

            }



            /// <summary>
            /// Bind when no need data value field and data field
            /// </summary>
            /// <param name="control"></param>
            /// <param name="data"></param>
            public static void BindData(Control control, object data)
            {
                BindData(control, data, null, null);
            }
            public static int GetValueFromCheckListBoxControl(object rowView, string displayValue)
            {
                var result = 0;
                if ((DataRowView)rowView != null)
                {
                    result = Convert.ToInt16(((DataRowView)rowView).Row[displayValue].ToString());
                }
                return result;
            }
           
        }
        public class MoneyByLetter
        {

            public string sMoneyToLetter(string strNumber)
            {
                if (!Utility.IsNumeric(strNumber)) return "Số tiền phải là chữ số. Đề nghị bạn kiểm tra lại";
                strNumber = Convert.ToInt64(Convert.ToDouble(strNumber)).ToString();
                string functionReturnValue = null;
                string chu = "";
                int lng;
                string CD;
                int dd;
                int i;
                string TP;
                bool bolCheck;
                bool BolSoAm = false;
                if (Strings.Left(strNumber, 1) == "-")
                {
                    // số âm
                    BolSoAm = true;
                    strNumber = strNumber.Substring(1);
                }
                bolCheck = false;
                CD = "";
                TP = "";
                if (strNumber == "")
                {
                    functionReturnValue = "";
                    return ""; // TODO: might not be correct. Was : Exit Function
                }
                //If Trim(Str$(strNumber)) <> "" Then
                if (Strings.Trim(strNumber) != "")
                {
                    for (i = 0; i <= Strings.Len(strNumber); i++)
                    {
                        if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                        {
                            bolCheck = true;
                            CD = Strings.Left(strNumber, i - 1);
                            TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                            TP = TP + "0";
                            TP = Strings.Left(TP, 2);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (bolCheck == false)
                    {
                        //                CD = Trim(Str$(strNumber))
                        CD = Strings.Trim(strNumber);
                    }
                    lng = Strings.Len(CD);
                    switch (lng)
                    {
                        case 0:
                            chu = " ";
                            break;
                        case 1:
                            chu = DonVi(CD);
                            break;
                        case 2:
                            chu = Chuc(CD);
                            break;
                        case 3:
                            chu = Tram(CD);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            chu = Nghin(CD);
                            break;
                        case 7:
                        case 8:
                        case 9:
                            chu = Trieu(CD);
                            break;
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                            chu = Ti(CD);
                            break;
                    }

                    dd = Strings.Len(Strings.Trim(chu));
                    functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                    //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                    functionReturnValue = functionReturnValue.Replace(" không trăm  nghìn", "");
                    functionReturnValue = functionReturnValue.Replace(" không trăm  triệu", "");
                    functionReturnValue = functionReturnValue.Replace("  ", " ");
                    if (TP != "")
                    {
                        i = Convert.ToInt32(TP);
                        lng = Strings.Len(TP);
                        if (lng > 0)
                        {
                            switch (lng)
                            {
                                case 1:
                                    chu = DonVi(TP);
                                    break;
                                case 2:
                                    chu = ChucHao(TP);
                                    break;
                            }
                            dd = Strings.Len(Strings.Trim(chu));
                            functionReturnValue = functionReturnValue + " " + Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                        }
                    }
                }
                if (BolSoAm) functionReturnValue = "Âm " + functionReturnValue;
                return functionReturnValue.Replace("không xu", "");
            }
            public string MoneyByLetterShort(string strNumber, int muc)
            {
                string functionReturnValue = null;
                string chu = "";
                int lng;
                string CD;
                int dd;
                int i;
                string TP;
                bool bolCheck;
                bool BolSoAm = false;
                if (Strings.Left(strNumber, 1) == "-")
                {
                    // số âm
                    BolSoAm = true;
                    strNumber = strNumber.Substring(1);
                }
                bolCheck = false;
                CD = "";
                TP = "";
                if (strNumber == "")
                {
                    functionReturnValue = "";
                    return ""; // TODO: might not be correct. Was : Exit Function
                }
                //If Trim(Str$(strNumber)) <> "" Then
                if (Strings.Trim(strNumber) != "")
                {
                    for (i = 0; i <= Strings.Len(strNumber); i++)
                    {
                        if (Strings.Right(Strings.Left(strNumber, i), 1) == ".")
                        {
                            bolCheck = true;
                            CD = Strings.Left(strNumber, i - 1);
                            TP = Strings.Right(strNumber, Strings.Len(strNumber) - i);
                            TP = TP + "0";
                            TP = Strings.Left(TP, 2);
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                    if (bolCheck == false)
                    {
                        //                CD = Trim(Str$(strNumber))
                        CD = Strings.Trim(strNumber);
                    }
                    lng = Strings.Len(CD);
                    switch (lng)
                    {
                        case 0:
                            chu = " ";
                            break;
                        case 1:
                            chu = DonVi(CD);
                            break;
                        case 2:
                            chu = ChucShort(CD);
                            break;
                        case 3:
                            chu = TramShort(CD, muc);
                            break;
                        case 4:
                        case 5:
                        case 6:
                            chu = NghinShort(CD, muc);
                            break;
                        case 7:
                        case 8:
                        case 9:
                            chu = TrieuShort(CD, muc);
                            break;
                        case 10:
                        case 11:
                        case 12:
                            chu = TiShort(CD, muc);
                            break;
                    }

                    dd = Strings.Len(Strings.Trim(chu));
                    functionReturnValue = Strings.UCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " đồng";
                    //MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
                    functionReturnValue = functionReturnValue.Replace("  ", " ");
                    functionReturnValue = functionReturnValue.Replace(" không trăm nghìn", "");
                    functionReturnValue = functionReturnValue.Replace(" không trăm triệu", "");
                    //MoneyByLetterShort = Replace(MoneyByLetterShort, " mươi", "")
                    //If muc = 2 Then
                    //    MoneyByLetterShort = Replace(MoneyByLetterShort, " trăm", "")
                    //End If
                    if (TP != "")
                    {
                        i = Convert.ToInt32(TP);
                        lng = Strings.Len(TP);
                        if (lng > 0)
                        {
                            switch (lng)
                            {
                                case 1:
                                    chu = DonVi(TP);
                                    break;
                                case 2:
                                    chu = ChucHaoShort(TP);
                                    break;
                            }
                            dd = Strings.Len(Strings.Trim(chu));
                            functionReturnValue = functionReturnValue + " " + Strings.LCase(Strings.Mid(Strings.Trim(chu), 1, 1)) + Strings.Mid(Strings.Trim(chu), 2, dd - 1) + " xu";
                        }
                    }
                }
                return functionReturnValue;
                //If BolSoAm Then MoneyByLetter = "Âm " & MoneyByLetter
            }
            private string ChucHao(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "0":
                            tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                            break;
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }
            private string ChucHaoShort(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "0":
                            tmp = " " + DonVi(Strings.Mid(d, 2, 1));
                            break;
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }
            private string DonVi(string d)
            {
                string functionReturnValue = null;
                int tmp;
                tmp = Strings.Len(d);
                if (tmp == 0)
                {
                    functionReturnValue = " ";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "0":
                            functionReturnValue = "không ";
                            break;
                        case "1":
                            functionReturnValue = "một ";
                            break;
                        case "2":
                            functionReturnValue = "hai ";
                            break;
                        case "3":
                            functionReturnValue = "ba ";
                            break;
                        case "4":
                            functionReturnValue = "bốn ";
                            break;
                        case "5":
                            functionReturnValue = "năm ";
                            break;
                        case "6":
                            functionReturnValue = "sáu ";
                            break;
                        case "7":
                            functionReturnValue = "bảy ";
                            break;
                        case "8":
                            functionReturnValue = "tám ";
                            break;
                        case "9":
                            functionReturnValue = "chín ";
                            break;
                    }
                }
                return functionReturnValue;
            }

            private string Chuc(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "mười lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }
            private string ChucShort(string d)
            {
                string tmp = "";
                if (Strings.Len(d) != 2)
                {
                    tmp = "Error";
                }
                else
                {
                    switch (Strings.Mid(d, 1, 1))
                    {
                        case "1":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = "mười ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = "mười lăm ";
                            }
                            else
                            {
                                tmp = "mười " + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            if (Strings.Mid(d, 2, 1) == "0")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mươi ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "1")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "mốt ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "4")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "tư ";
                            }
                            else if (Strings.Mid(d, 2, 1) == "5")
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + "lăm ";
                            }
                            else
                            {
                                tmp = DonVi(Strings.Mid(d, 1, 1)) + DonVi(Strings.Mid(d, 2, 1));
                            }

                            break;
                    }
                }
                return tmp;
            }
            private string Tram(string d)
            {
                string functionReturnValue = null;
                string tmp = "";
                string d1;
                string d2;
                string d3;
                string Temp = "";

                d1 = Strings.Mid(d, 1, 1);
                d2 = Strings.Mid(d, 2, 1);
                d3 = Strings.Mid(d, 3, 1);
                if (Strings.Len(d) != 3)
                {
                    Temp = "Error";
                }
                else
                {
                    switch (d2)
                    {
                        case "0":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm  ";
                            }
                            else if (d3 == "1")
                            {
                                tmp = DonVi(d1) + "trăm linh một ";
                            }
                            else if (d3 == "4")
                            {
                                tmp = DonVi(d1) + "trăm linh tư ";
                            }
                            else if (d3 == "5")
                            {
                                tmp = DonVi(d1) + "trăm linh năm ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                            }

                            break;
                        case "1":
                            if (d3 == "0")
                            {
                                tmp = DonVi(d1) + "trăm mười ";
                            }
                            else
                            {
                                tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            }

                            break;
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                            // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                            tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                            break;
                    }
                    functionReturnValue = tmp;
                }
                return functionReturnValue;
            }
            private string TramShort(string d, int muc)
            {
                string functionReturnValue = null;
                string tmp = "";
                string d1;
                string d2;
                string d3;
                string Temp = "";
                if (muc == 2)
                {
                    d1 = Strings.Mid(d, 1, 1);
                    d2 = Strings.Mid(d, 2, 1);
                    d3 = Strings.Mid(d, 3, 1);
                    if (Strings.Len(d) != 3)
                    {
                        Temp = "Error";
                    }
                    else
                    {
                        switch (d2)
                        {
                            case "0":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm  ";
                                }
                                else if (d3 == "1")
                                {
                                    tmp = DonVi(d1) + "linh một ";
                                }
                                else if (d3 == "4")
                                {
                                    tmp = DonVi(d1) + "linh tư ";
                                }
                                else if (d3 == "5")
                                {
                                    tmp = DonVi(d1) + "linh năm ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + "linh " + DonVi(d3);
                                }

                                break;
                            case "1":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm mười ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + ChucShort(d2 + d3);
                                }

                                break;
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                                // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                                tmp = DonVi(d1) + ChucShort(d2 + d3);
                                break;
                        }
                        functionReturnValue = tmp;
                    }
                }
                else
                {
                    d1 = Strings.Mid(d, 1, 1);
                    d2 = Strings.Mid(d, 2, 1);
                    d3 = Strings.Mid(d, 3, 1);
                    if (Strings.Len(d) != 3)
                    {
                        Temp = "Error";
                    }
                    else
                    {
                        switch (d2)
                        {
                            case "0":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm  ";
                                }
                                else if (d3 == "1")
                                {
                                    tmp = DonVi(d1) + "trăm linh một ";
                                }
                                else if (d3 == "4")
                                {
                                    tmp = DonVi(d1) + "trăm linh tư ";
                                }
                                else if (d3 == "5")
                                {
                                    tmp = DonVi(d1) + "trăm linh năm ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + "trăm linh " + DonVi(d3);
                                }

                                break;
                            case "1":
                                if (d3 == "0")
                                {
                                    tmp = DonVi(d1) + "trăm mười ";
                                }
                                else
                                {
                                    tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                                }

                                break;
                            case "2":
                            case "3":
                            case "4":
                            case "5":
                            case "6":
                            case "7":
                            case "8":
                            case "9":
                                // tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                                tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3);
                                break;
                        }
                        functionReturnValue = tmp;
                    }
                }
                return functionReturnValue;

            }
            private string Nghin(string d)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 3;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 3);
                if (s2 == "000")
                {
                    tmp1 = "nghìn ";
                }
                else
                {
                    tmp1 = "nghìn " + Tram(s2);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = Chuc(s1) + tmp1;
                        break;
                    case 3:
                        tmp = Tram(s1) + tmp1;
                        break;
                }
                return tmp;
            }
            private string NghinShort(string d, int muc)
            {
                string tmp = "";
                string s1;
                string s2;
                string tmp1;
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 3;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 3);
                if (s2 == "000")
                {
                    tmp1 = "nghìn ";
                }
                else
                {
                    tmp1 = "nghìn " + TramShort(s2, muc);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = ChucShort(s1) + tmp1;
                        break;
                    case 3:
                        tmp = TramShort(s1, muc) + tmp1;
                        break;
                }
                return tmp;
            }
            private string Trieu(string d)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 6;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 6);
                if (s2 == "000000")
                {
                    tmp1 = "triệu ";
                }
                else
                {
                    tmp1 = "triệu " + Nghin(s2);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = Chuc(s1) + tmp1;
                        break;
                    case 3:
                        tmp = Tram(s1) + tmp1;
                        break;
                }
                return tmp;
            }
            private string TrieuShort(string d, int muc)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 6;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 6);
                if (s2 == "000000")
                {
                    tmp1 = "triệu ";
                }
                else
                {
                    tmp1 = "triệu " + NghinShort(s2, muc);
                }
                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = ChucShort(s1) + tmp1;
                        break;
                    case 3:
                        tmp = TramShort(s1, muc) + tmp1;
                        break;
                }
                return tmp;
            }
            private string Ti(string d)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 9;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 9);

                if (s2 == "000000000")
                {
                    tmp1 = "tỷ ";
                }
                else
                {
                    tmp1 = "tỷ " + Trieu(s2);
                }

                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = Chuc(s1) + tmp1;
                        break;
                    case 3:
                        tmp = Tram(s1) + tmp1;
                        break;
                    case 4:
                        tmp = Nghin(s1) + tmp1;
                        break;
                    case 5:
                        tmp = "(Số tiền quá lớn. Bạn cần xem lại đơn hàng)";
                        break;
                }
                return tmp;
            }
            private string TiShort(string d, int muc)
            {
                string tmp = "";
                string s1 = "";
                string s2 = "";
                string tmp1 = "";
                int dai;
                int ln;
                dai = Strings.Len(d);
                ln = dai - 9;
                s1 = Strings.Mid(d, 1, ln);
                s2 = Strings.Mid(d, ln + 1, 9);

                if (s2 == "000000000")
                {
                    tmp1 = "tỷ ";
                }
                else
                {
                    tmp1 = "tỷ " + TrieuShort(s2, muc);
                }

                switch (Strings.Len(s1))
                {
                    case 1:
                        tmp = DonVi(s1) + tmp1;
                        break;
                    case 2:
                        tmp = ChucShort(s1) + tmp1;
                        break;
                    case 3:
                        tmp = TramShort(s1, muc) + tmp1;
                        break;
                    case 4:
                        tmp = TrieuShort(s1, muc) + tmp1;
                        break;
                }
                return tmp;
            }


        }
        public interface IDataBoundable
        {
            object DataSource { get; set; }
            void DataBind();
        }

    }

    public interface IDataBoundable
    {
        object DataSource { get; set; }
        void DataBind();
    }

    public static class DataBinding
    {

        /// <summary>
        /// Help to bind the data
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        /// <param name="dataValueField"></param>
        /// <param name="dataTextField"></param>
        public static void BindData(Control control, object data, string dataValueField, string dataTextField)
        {
            if (typeof(ListControl).IsInstanceOfType(control))
            {
                var listControl = control as ListControl;

                if (listControl != null)
                {
                    listControl.DataSource = data;
                    listControl.ValueMember = dataValueField;
                    listControl.DisplayMember = dataTextField;

                }

            }
            if (typeof(IDataBoundable).IsInstanceOfType(control))
            {
                var dataBoundControl = control as IDataBoundable;
                if (dataBoundControl != null)
                {
                    dataBoundControl.DataSource = data;
                    dataBoundControl.DataBind();
                }
            }
        }
        public static void BindData(Janus.Windows.EditControls.UIComboBox objCombobox, object data, string dataValueField, string dataTextField)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)data;
          

            objCombobox.DataSource = dt;
            objCombobox.ValueMember = dataValueField;
            objCombobox.DisplayMember = dataTextField;
            objCombobox.SelectedIndex = 0;
        }
        public static void BindDataCombox(Control control, object data, string dataValueField, string dataTextField)
        {
            if (typeof(ListControl).IsInstanceOfType(control))
            {

                ComboBox combox = control as ComboBox;
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                DataRow dr = dt.NewRow();
                dr[dataTextField] = "--- Chọn ---";
                dr[dataValueField] = "-1";
                dt.Rows.InsertAt(dr, 0);
                if (combox != null)
                {

                    combox.DataSource = dt;
                    combox.ValueMember = dataValueField;
                    combox.DisplayMember = dataTextField;


                }

            }
            if (typeof(IDataBoundable).IsInstanceOfType(control))
            {
                var dataBoundControl = control as IDataBoundable;
                if (dataBoundControl != null)
                {
                    dataBoundControl.DataSource = data;
                    dataBoundControl.DataBind();
                }
            }
        }
        public static void BindDataCombox(Control control, object data, string dataValueField, string dataTextField, string SelectItem)
        {
            if (typeof(ListControl).IsInstanceOfType(control))
            {

                ComboBox combox = control as ComboBox;
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                DataRow dr = dt.NewRow();
                dr[dataTextField] = SelectItem;
                dr[dataValueField] = -1;
                dt.Rows.InsertAt(dr, 0);
                if (combox != null)
                {

                    combox.DataSource = dt;
                    combox.ValueMember = dataValueField;
                    combox.DisplayMember = dataTextField;


                }

            }
            if (typeof(IDataBoundable).IsInstanceOfType(control))
            {
                var dataBoundControl = control as IDataBoundable;
                if (dataBoundControl != null)
                {
                    dataBoundControl.DataSource = data;
                    dataBoundControl.DataBind();
                }
            }
        }
        public static void BindDataCombox(Janus.Windows.EditControls.UIComboBox objCombobox, object data, string dataValueField, string dataTextField)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)data;
            DataRow dr = dt.NewRow();
            dr[dataTextField] = "---Chọn";
            dr[dataValueField] = -1;
            dt.Rows.InsertAt(dr, 0);

            objCombobox.DataSource = dt;
            objCombobox.ValueMember = dataValueField;
            objCombobox.DisplayMember = dataTextField;
            objCombobox.SelectedIndex = 0;
        }
        public static void BindDataCombox(Janus.Windows.EditControls.UIComboBox objCombobox, object data, string dataValueField, string dataTextField, string SelectItem)
        {
            

                
                DataTable dt = new DataTable();
                dt = (DataTable)data;
                DataRow dr = dt.NewRow();
                dr[dataTextField] = SelectItem;
                dr[dataValueField] = -1;
                dt.Rows.InsertAt(dr, 0);

                objCombobox.DataSource = data;
                objCombobox.ValueMember = dataValueField;
                objCombobox.DisplayMember = dataTextField;
                objCombobox.SelectedIndex = 0;
        }
        public static void BindDataComboxDefaultView(Control control, DataTable data, bool TrueDefaultView, string dataValueField, string dataTextField, string SelectItem)
        {
            if (TrueDefaultView)
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {

                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                   // dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = SelectItem;
                    dr[dataValueField] = "-1";
                    dt.Rows.InsertAt(dr, 0);

                    if (combox != null)
                    {

                        combox.DataSource = data.DefaultView;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;


                    }

                }
            }
            else
            {
                if (typeof(ListControl).IsInstanceOfType(control))
                {

                    ComboBox combox = control as ComboBox;
                    DataTable dt = new DataTable();
                    dt = (DataTable)data;
                    DataRow dr = dt.NewRow();
                    dr[dataTextField] = "--- Chọn ---";
                    dr[dataValueField] = "0";
                    dt.Rows.InsertAt(dr, 0);
                    if (combox != null)
                    {

                        combox.DataSource = dt.DefaultView;
                        combox.ValueMember = dataValueField;
                        combox.DisplayMember = dataTextField;


                    }

                }
            }

        }



        /// <summary>
        /// Bind when no need data value field and data field
        /// </summary>
        /// <param name="control"></param>
        /// <param name="data"></param>
        public static void BindData(Control control, object data)
        {
            BindData(control, data, null, null);
        }
        public static int GetValueFromCheckListBoxControl(object rowView, string displayValue)
        {
            var result = 0;
            if ((DataRowView)rowView != null)
            {
                result = Convert.ToInt16(((DataRowView)rowView).Row[displayValue].ToString());
            }
            return result;
        }
        public static int GetValueFromCheckListBoxControl(object rowView, string displayValue, int index)
        {
            var result = 0;
            if ((DataRowView)rowView != null)
            {
                result = Convert.ToInt16(((DataRowView)rowView).Row[displayValue].ToString());
            }
            return result;
        }
       
    }
    /// <summary>
    /// An useful class to read/write/delete/count registry keys
    /// </summary>
    public class ModifyRegistry
    {
        private bool showError = false;
        /// <summary>
        /// A property to show or hide error messages 
        /// (default = false)
        /// </summary>
        public bool ShowError
        {
            get { return showError; }
            set { showError = value; }
        }

        private string subKey = "SOFTWARE\\" + Application.ProductName.ToUpper();
        /// <summary>
        /// A property to set the SubKey value
        /// (default = "SOFTWARE\\" + Application.ProductName.ToUpper())
        /// </summary>
        public string SubKey
        {
            get { return subKey; }
            set { subKey = value; }
        }

        private RegistryKey baseRegistryKey = Registry.LocalMachine;
        /// <summary>
        /// A property to set the BaseRegistryKey value.
        /// (default = Registry.LocalMachine)
        /// </summary>
        public RegistryKey BaseRegistryKey
        {
            get { return baseRegistryKey; }
            set { baseRegistryKey = value; }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To read a registry key.
        /// input: KeyName (string)
        /// output: value (string) 
        /// </summary>
        public string Read(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = baseRegistryKey;
            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    // If the RegistryKey exists I get its value
                    // or null is returned.
                    return (string)sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
                    ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To write into a registry key.
        /// input: KeyName (string) , Value (object)
        /// output: true or false 
        /// </summary>
        public bool Write(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a registry key.
        /// input: KeyName (string)
        /// output: true or false 
        /// </summary>
        public bool DeleteKey(string KeyName)
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // If the RegistrySubKey doesn't exists -> (true)
                if (sk1 == null)
                    return true;
                else
                    sk1.DeleteValue(KeyName);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// To delete a sub key and any child.
        /// input: void
        /// output: true or false 
        /// </summary>
        public bool DeleteSubKeyTree()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists, I delete it
                if (sk1 != null)
                    rk.DeleteSubKeyTree(subKey);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Deleting SubKey " + subKey);
                return false;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// Retrive the count of subkeys at the current key.
        /// input: void
        /// output: number of subkeys
        /// </summary>
        public int SubKeyCount()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.SubKeyCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Retriving subkeys of " + subKey);
                return 0;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        /// <summary>
        /// Retrive the count of values in the key.
        /// input: void
        /// output: number of keys
        /// </summary>
        public int ValueCount()
        {
            try
            {
                // Setting
                RegistryKey rk = baseRegistryKey;
                RegistryKey sk1 = rk.OpenSubKey(subKey);
                // If the RegistryKey exists...
                if (sk1 != null)
                    return sk1.ValueCount;
                else
                    return 0;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                ShowErrorMessage(e, "Retriving keys of " + subKey);
                return 0;
            }
        }

        /* **************************************************************************
         * **************************************************************************/

        private void ShowErrorMessage(Exception e, string Title)
        {
            if (showError == true)
                MessageBox.Show(e.Message,
                                Title
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Error);
        }
    }
    /// <summary>
    /// hàm thực hiện lọc thông tin của phần bác sỹ
    /// </summary>
    public class FilterAssignStatff
    {
        /// <summary>
        /// lọc thông tin của bác sỹ
        /// </summary>
        /// <returns></returns>
        public static string  FilterAssignDoctor()
        {
            return "Staff_ID=-1 or Staff_Type_Type_ID='DOCTOR'";
        }
        /// <summary>
        /// lọc thông tin của y tá
        /// </summary>
        /// <returns></returns>
        public static string FilterAssignNurse()
        {
            return "Staff_ID =-1 or Staff_Type_Type_ID='NURSE'";
        }
         
    }
    /// <summary>
    /// Summary description for ExcelToXML.
    /// </summary>
   

}
