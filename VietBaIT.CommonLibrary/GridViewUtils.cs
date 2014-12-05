using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Collections;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;

using System.ComponentModel;


using System.Threading;
namespace VietBaIT.CommonLibrary
{
    
    public class GridViewUtils
    {
        DataGridView _DataGridView=new DataGridView();
        DataGridView _Temp = new DataGridView();
        ContainerControl _Container;
        private int Alignment=0;
        private Int16 Codan=0;
        string CurrentDataGridViewName = "";
        Hashtable ArrGridColWidth = new Hashtable();
        Hashtable ArrCodan = new Hashtable();
        Hashtable ArrAlignment = new Hashtable();
        string OriginalWidthOfGridColumns = "";
        string SubSystem;
        string BranchID = "";
        string UID="";
        bool AllowSearchOnGrid = false;
        ArrayList _arrCol = new ArrayList();
        private bool ControlHasCreadted = false;
        private System.Data.SqlClient.SqlConnection gCon;

        private ContextMenuStrip _CtxMnu = new ContextMenuStrip();
        /// <summary>
        /// Khởi tạo lớp tiện ích cho phép tùy chọn cấu hình việc ẩn hiện của cột trong một GridView
        /// </summary>
        /// <param name="_Container">Điều khiển gốc, thường là Form hoặc UserControl. Ví dụ: this(C#),Me(VB)</param>
        /// <param name="BranchID">Mã chi nhánh làm việc. Ví dụ: PD1400,PD0200,...</param>
        /// <param name="UID">Tên người dùng. Ví dụ: HoanBQ,HungND,...</param>
        /// <param name="SubSystem">Mã phân hệ. Ví dụ: Hoadon.DLL,Dodem.dll,...</param>
        /// /// <param name="AllowSearchOnGrid">Cho phép nhấn F3 để kích hoạt việc tìm kiếm dữ liệu trên lưới thông qua sự kiện Keydown của DataGrid hiện tại</param>
        public GridViewUtils(ContainerControl _Container, string BranchID, string UID, string SubSystem, bool AllowSearchOnGrid)
        {
            
            this._Container = _Container;
            this.BranchID = BranchID;
            this.UID = UID;
            this.SubSystem = SubSystem;
            this.AllowSearchOnGrid = AllowSearchOnGrid;
            _CtxMnu.Items.Add("Ẩn hiện cột trên lưới dữ liệu", null, new EventHandler(_Onclick));
            StartUp();
        }
        /// <summary>
        /// Sự kiện chọn menu Options để cấu hình ẩn hiện cột của GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Onclick(object sender, EventArgs e)
        {
            ToolStripItem Source = (ToolStripItem)sender;
            ContextMenuStrip Ctx =(ContextMenuStrip) Source.Owner;
            DataGridView _grd = (DataGridView)Ctx.SourceControl;
            
            CurrentDataGridViewName = _grd.Name;
            GridOptions _grdOptions = new GridOptions(GetAllColumnsOfGridView(_grd),_arrCol);
            if (ArrCodan.Contains(CurrentDataGridViewName)) Codan = Convert.ToInt16(ArrCodan[CurrentDataGridViewName]);
            if (ArrAlignment.Contains(CurrentDataGridViewName)) Alignment =Convert.ToInt32( ArrAlignment[CurrentDataGridViewName]);
            if (ArrGridColWidth.Contains(CurrentDataGridViewName)) OriginalWidthOfGridColumns = ArrGridColWidth[CurrentDataGridViewName].ToString();
            if (_grd.GetContainerControl().GetType().FullName.Equals(new Form().GetType().FullName) || _grd.GetContainerControl().GetType().BaseType.BaseType.FullName.Equals(new Form().GetType().FullName))
            {
            Form f = _grd.GetContainerControl() as Form;
            f.Opacity = 0.2;
            }
            _grdOptions.Codan = Codan;
            _grdOptions.Alignment = Alignment;
            _grdOptions.ShowDialog();
            if (_grd.GetContainerControl().GetType().FullName.Equals(new Form().GetType().FullName) || _grd.GetContainerControl().GetType().BaseType.BaseType.FullName.Equals(new Form().GetType().FullName))
            {
                Form f = _grd.GetContainerControl() as Form;
                f.Opacity = 1;
            }
            if (!_grdOptions._Cancel)
            {
                Alignment = _grdOptions.Alignment;
                Codan = _grdOptions.Codan;
                if (!ArrCodan.Contains(CurrentDataGridViewName)) ArrCodan.Add(CurrentDataGridViewName, Codan);
                else
                    ArrCodan[CurrentDataGridViewName] = Codan;
                if (!ArrAlignment.Contains(CurrentDataGridViewName)) ArrAlignment.Add(CurrentDataGridViewName, Alignment);
                else
                    ArrAlignment[CurrentDataGridViewName] = Alignment;
                string ArrColName = FromArrayListToStringValue(_grdOptions.InvisibleCols);
                SaveOptions(_Container.Name, _grd.Name, ArrColName,_grdOptions.Alignment, _grdOptions.Codan,OriginalWidthOfGridColumns);
                StartUpGrid(_grd);
               
            }
        }
       
       
        private string FromArrayListToStringValue(ArrayList _parrCol)
        {
            string reval = "";
            for (int i = 0; i <= _parrCol.Count - 1; i++)
            {
                reval += _parrCol[i].ToString().Split(':')[1] + ":";
               
            }
            //if (reval != "") reval = reval.Substring(0, reval.Length - 1);
            return reval;
        }
        /// <summary>
        /// Lưu thông tin cấu hình Grid vào CSDL để hiển thị cho lần kế tiếp
        /// </summary>
        /// <param name="RootControl"></param>
        /// <param name="DataGridName"></param>
        /// <param name="ColName"></param>
        private void SaveOptions(string RootControl, string DataGridName, string ColName, int Alignment, Int16 Codan, string OriginalWidth)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataAdapter Da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            cmd.Connection = CommonLibrary.globalVariables.SqlConn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "InsertOptions";
            cmd.Parameters.AddWithValue("@BranchID",BranchID).Direction=ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@UID",UID).Direction=ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@SubSystem",SubSystem).Direction=ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Container", RootControl).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@DataGridView", DataGridName).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@ArrColName", ColName).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Alignment", Alignment).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@Codan", Codan).Direction = ParameterDirection.Input;
            cmd.Parameters.AddWithValue("@OriginalWidth", OriginalWidth).Direction = ParameterDirection.Input;
            
            try
            {
                cmd.ExecuteNonQuery();
                
            }
            catch
            {
                
            }
        }
        private void ORASaveOptions(string RootControl, string DataGridName, string ColName)
        {
            

            //OracleCommand cmd = new OracleCommand();
            //OracleDataAdapter Da = new OracleDataAdapter(cmd);
            //cmd.Connection = globalVariables.OraCon;
            //cmd.CommandType = CommandType.StoredProcedure;
            
            //cmd.CommandText = "GRIDVIEWUTILS.InsertOptions";
            //cmd.Parameters.AddWithValue("v_BranchID", BranchID).Direction = ParameterDirection.Input;
            //cmd.Parameters.AddWithValue("v_UserName", UID).Direction = ParameterDirection.Input;
            //cmd.Parameters.AddWithValue("v_SubSystem", SubSystem).Direction = ParameterDirection.Input;
            //cmd.Parameters.AddWithValue("v_ControlContainer", RootControl).Direction = ParameterDirection.Input;
            //cmd.Parameters.AddWithValue("v_DataGridView", DataGridName).Direction = ParameterDirection.Input;
            //cmd.Parameters.AddWithValue("ArrColName", ColName).Direction = ParameterDirection.Input;
            try
            {
             //bool Success=   DataService.ORASaveGridOptions(RootControl, DataGridName, ColName, BranchID, UID, SubSystem);
                //cmd.ExecuteNonQuery();

            }
            catch
            {

            }
        }
        /// <summary>
        /// Lấy về tất cả các cột của Grid vào trong mảng
        /// </summary>
        /// <param name="_grd"></param>
        /// <returns></returns>
        private ArrayList GetAllColumnsOfGridView(DataGridView _grd)
        {
            ArrayList _Arr = new ArrayList();
            foreach (DataGridViewColumn Col in _grd.Columns)
            {
                if (Col.ToolTipText!="H") _Arr.Add(Col.HeaderText + ":" + Col.Name + ":" + TranslateFromAlignmentToString(Col.DefaultCellStyle.Alignment) + ":" + TranslateFromAlignmentToInt16(Col.DefaultCellStyle.Alignment));
                if (Col.Visible == false)
                    _arrCol.Add(Col.HeaderText + ":" + Col.Name + ":" + TranslateFromAlignmentToString(Col.DefaultCellStyle.Alignment) + ":" + TranslateFromAlignmentToInt16(Col.DefaultCellStyle.Alignment));
            }
            return _Arr;
        }
        private string  TranslateFromAlignmentToInt16(DataGridViewContentAlignment Align)
        {
            switch (Align)
            {
                case DataGridViewContentAlignment.NotSet:
                    return "0";
                case DataGridViewContentAlignment.MiddleLeft:
                    return "1";
                case DataGridViewContentAlignment.MiddleCenter:
                    return "2";
                case DataGridViewContentAlignment.MiddleRight:
                    return "3";
                default:
                    return "4";
            }
        }
        private string TranslateFromAlignmentToString(DataGridViewContentAlignment Align)
        {
            switch (Align)
            {
                case DataGridViewContentAlignment.NotSet:
                    return "Mặc định";
                case DataGridViewContentAlignment.MiddleLeft:
                    return "Trái";
                case DataGridViewContentAlignment.MiddleCenter:
                    return "Giữa";
                case DataGridViewContentAlignment.MiddleRight:
                    return "Phải";
                default:
                    return "Unknown";
            }
        }
        /// <summary>
        /// Thiết lập contextMenu và khởi động hàm hiển thị cột của Grid. Hàm này được chạy ngầm tránh ảnh hưởng đến việc LoadForm
        /// </summary>
        public void StartUp()
        {
            do
            {
                IntPtr IR = _Container.Handle;
                _IAr = _Container.BeginInvoke(new StartUpCallBack(SetContextMenu));
                ControlHasCreadted = true;
            } while (!ControlHasCreadted);
        }
        /// <summary>
        /// 
        /// </summary>
        public void SetContextMenu()
        {
            _Container.EndInvoke(_IAr);
            foreach (Control ctrl in _Container.Controls)
            {
                if (ctrl.GetType().Equals(_DataGridView.GetType()))
                {

                    AddContextMenu((DataGridView)ctrl);
                    StartUpGrid((DataGridView)ctrl);
                    
                }
                else
                {
                    LoopControl(ctrl);
                }
            }
        }
        /// <summary>
        /// THực hiện vòng lặp để tìm tất cả các GridView trong một Control và thiết lập ẩn hiện cột cho chúng
        /// </summary>
        /// <param name="_Root"></param>
        private void LoopControl(Control _Root)
        {
            try
            {
                foreach (Control ctrl in _Root.Controls)
                {
                    if (ctrl.GetType().Equals(_DataGridView.GetType()))
                    {
                        AddContextMenu((DataGridView)ctrl);
                        StartUpGrid((DataGridView)ctrl);
                    }
                    else
                    {
                        LoopControl(ctrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private IAsyncResult _IAr;
        private delegate void StartUpCallBack();
        /// <summary>
        /// Thực hiện việc ẩn hiện cột cho một GridView
        /// </summary>
        /// <param name="grd"></param>
        private void StartUpGrid(DataGridView grd)
        {
            if (grd.Columns.Count <= 0) return;
            _Temp = grd;
            CurrentDataGridViewName = grd.Name;
            grd.KeyDown -= grd_KeyDown;
            if(AllowSearchOnGrid) grd.KeyDown += new KeyEventHandler(grd_KeyDown);
            
            GetInvisibleCol(_Container.Name, grd.Name, UID, BranchID);
            int MaxWidth = 0;
            int TotalWidthForLastColumn = 0;
            if (ArrGridColWidth.Contains(CurrentDataGridViewName)) OriginalWidthOfGridColumns = ArrGridColWidth[CurrentDataGridViewName].ToString();
            else
                OriginalWidthOfGridColumns = "";
            DataGridViewColumn LastColVisible = grd.Columns[0];
            //Get Original width, set Visible to all columns, get MaxWidth
            foreach (DataGridViewColumn Col in grd.Columns)
            {
                if (OriginalWidthOfGridColumns.Trim()==string.Empty || !ArrGridColWidth.Contains(CurrentDataGridViewName)) OriginalWidthOfGridColumns += Col.Width + ":";
               if(Col.ToolTipText!="H") Col.Visible = true;
            }
            GetMaxWidth(ref MaxWidth, OriginalWidthOfGridColumns);
            if (!ArrGridColWidth.Contains(grd.Name)) ArrGridColWidth.Add(grd.Name, OriginalWidthOfGridColumns);
            else
                ArrGridColWidth[grd.Name] = OriginalWidthOfGridColumns;

            //Set visible for certain columns, set Alignment for DataGrid, Set Width for columns
            foreach (DataGridViewColumn Col in grd.Columns)
            {
                //Thiết lập giá trị độ rộng các cột về trạng thái ban đầu
                Col.Width = Convert.ToInt32(OriginalWidthOfGridColumns.Split(':')[Col.Index]);
                if (_arrCol.Contains(Col.Name))
                {
                    Col.Visible = false;
                    TotalWidthForLastColumn += Col.Width;
                }
                else
                {
                    LastColVisible = Col;
                }
                Col.Width = Convert.ToInt32(OriginalWidthOfGridColumns.Split(':')[Col.Index]);
                OriginalWidthOfGridColumns = ArrGridColWidth[grd.Name].ToString();
                if (Codan == 0) Col.Width = Convert.ToInt32(OriginalWidthOfGridColumns.Split(':')[Col.Index]);
                if (Codan == 1) Col.Width = MaxWidth;
                Col.DefaultCellStyle.Alignment = TranslateFromIntToAlignment(Alignment);
            }
            //set width for LastColVisible
            if (Codan == 2)
            {
                grd.Columns[LastColVisible.Name].Width = TotalWidthForLastColumn + LastColVisible.Width;
            }
            //Set Alignment
            grd.RowsDefaultCellStyle.Alignment = TranslateFromIntToAlignment(Alignment);
        }
        private void GetMaxWidth(ref int MaxWidth,string ArrWidth)
        {
            MaxWidth = 0;
            string[] arrW = ArrWidth.Split(':');
            for (int i = 0; i <= arrW.Length - 1; i++)
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(arrW[i]))
                {
                    if (MaxWidth < Convert.ToInt32(arrW[i])) MaxWidth =Convert.ToInt32(arrW[i]);
                }
            }
        }
        private DataGridViewContentAlignment TranslateFromIntToAlignment(int Alignment)
        {
            switch (Alignment)
            {
                case 0:
                    return DataGridViewContentAlignment.NotSet;
                case 1:
                    return DataGridViewContentAlignment.MiddleLeft;
                case 2:
                    return DataGridViewContentAlignment.MiddleCenter;
                case 3:
                    return DataGridViewContentAlignment.MiddleRight;
                default:
                    return DataGridViewContentAlignment.MiddleLeft;
            }
        }
        void grd_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.F3)
            {
                SearchOnGridView _Form = new SearchOnGridView(SubSystem);
                _Form.mv_FormofGrid = _Container;
                _Form.mv_grd = _Temp;
                _Form.ShowDialog();
            }
        }
        /// <summary>
        /// Thêm ContextMenu Options để tùy chọn ẩn hiện cột cho Grid.
        /// </summary>
        /// <param name="grd"></param>
        private void AddContextMenu(DataGridView grd)
        {
            ContextMenuStrip _Ctx = grd.ContextMenuStrip;
            if (_Ctx == null)
                grd.ContextMenuStrip = _CtxMnu;
            else
            {
               
                    _Ctx.Items.Add(new ToolStripSeparator());
                    _Ctx.Items.Add("Ẩn hiện cột trên lưới dữ liệu", null, new EventHandler(_Onclick));
            }
        }
        /// <summary>
        /// Lấy danh sách các cột ẩn của một Grid đã được lưu trong CSDL
        /// </summary>
        /// <param name="RootControl"></param>
        /// <param name="DataGridName"></param>
        /// <param name="UserName"></param>
        /// <param name="BranchID"></param>
        private void GetInvisibleCol(string RootControl, string DataGridName, string UserName, string BranchID)
        {
            DataTable _InvisibleCols = null;
            _InvisibleCols = GetColsFromDB(RootControl, DataGridName);
            //Get Default Width
           SetArrayListFromDataTable( _InvisibleCols);
        }
        private DataTable GetColsFromDB(string RootControl, string DataGridName)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataAdapter Da = new System.Data.SqlClient.SqlDataAdapter(cmd);
            cmd.Connection = CommonLibrary.globalVariables.SqlConn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Sys_GridViewOptions WHERE BranchID=N'" + BranchID + "' AND UID=N'" + UID + "' AND SubSystem=N'" + SubSystem + "' AND Container=N'" + RootControl + "' AND DataGridView=N'" + DataGridName + "'";
            cmd.ExecuteNonQuery();
            DataTable dt=new DataTable();
            try
            {
                //dt = DataService.ORAGetColsGridFromDB(RootControl, DataGridName, BranchID, UID, SubSystem);
                Da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        private DataTable ORAGetColsFromDB(string RootControl, string DataGridName)
        {
            //OracleCommand cmd = new OracleCommand();
            //OracleDataAdapter Da = new OracleDataAdapter(cmd);
            //cmd.Connection = globalVariables.OraCon;
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * FROM Sys_GridViewOptions WHERE BranchID=N'" + BranchID + "' AND UserName=N'" + UID + "' AND SubSystem=N'" + SubSystem + "' AND ControlContainer=N'" + RootControl + "' AND DataGridView=N'" + DataGridName + "'";
            //cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            try
            {
                //dt = DataService.ORAGetColsGridFromDB(RootControl, DataGridName, BranchID, UID, SubSystem);
                //Da.Fill(dt);
                return dt;
            }
            catch
            {
                return null;
            }
        }
        private void SetArrayListFromDataTable(DataTable _dt)
        {
            if (_dt == null || _dt.Rows.Count <= 0)
            {
                if (!ArrCodan.Contains(CurrentDataGridViewName)) ArrCodan.Add(CurrentDataGridViewName, Codan);
                else ArrCodan[CurrentDataGridViewName] = Codan;
                if (!ArrAlignment.Contains(CurrentDataGridViewName)) ArrAlignment.Add(CurrentDataGridViewName, Alignment);
                else ArrAlignment[CurrentDataGridViewName] = Alignment;
                return;
            }
            OriginalWidthOfGridColumns = _dt.Rows[0]["OriginalWidth"] == null ? "" : _dt.Rows[0]["OriginalWidth"].ToString();
            Codan = _dt.Rows[0]["Codan"] == null ? Convert.ToInt16("0") : Convert.ToInt16(_dt.Rows[0]["codan"]);
            Alignment = _dt.Rows[0]["Alignment"] == null ? 0 : Convert.ToInt32(_dt.Rows[0]["Alignment"]);
            if (!ArrGridColWidth.Contains(CurrentDataGridViewName)) ArrGridColWidth.Add(CurrentDataGridViewName, OriginalWidthOfGridColumns);
            if (!ArrCodan.Contains(CurrentDataGridViewName)) ArrCodan.Add(CurrentDataGridViewName, Codan);
            if (!ArrAlignment.Contains(CurrentDataGridViewName)) ArrAlignment.Add(CurrentDataGridViewName, Alignment);
            _arrCol.Clear();
            string[] _ArrInvisibleCols= _dt.Rows[0]["ColName"].ToString().Split(':');
            
           for(int i=0;i<=_ArrInvisibleCols.GetLength(0)-1;i++)
            {
                if (!_arrCol.Contains(_ArrInvisibleCols[i]))
                    if (_ArrInvisibleCols[i].Trim()!="")
                    _arrCol.Add(_ArrInvisibleCols[i]);
            }
        }
    }
}
