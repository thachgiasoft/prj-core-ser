Imports System.Reflection
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO.Ports
Imports System.IO
Imports System.Globalization
Imports Vietbait.Lablink.Utilities

'server
Public Module globalModule

    'Biến kết nối đến CSDL SQL
    Public gv_CurrAssembly As Assembly
    Public gv_bAdminLogin As Boolean = False
    Public gv_oSqlCnn As SqlConnection
    Public gv_LabLinkCN As SqlConnection
    Public gv_sAnnouncement As String = "Thông báo"
    Public gv_dsParam As New DataSet
    ' Biến lưu trữ danh sách các tham số của đơn vị trong CSDL
    Public gv_dsParamTemp As New DataSet

    'DataSet lưu giữ tất cả các Role
    Public gv_dsAllRole As DataSet
    Public gv_sSubSystemName As String = "Phân hệ tích hợp"
    Public gv_oTmr As Timer
    'Biến xác định xem người dùng nhấn nút Close hay là Login
    Public gv_bReturnHome As Boolean = True
    Public gv_bIncreateOrDecrete As Boolean = True
    Public gv_sBranchID As String = "VIETBA"
    'Mã chi nhánh
    Public gv_sBranchName As String = "VIETBA Company"
    ' Tên chi nhánh
    Public gv_sParentBranchName As String = "VIETBA Company"
    Public gv_sPhone As String = "000 000000"
    Public gv_sAddress As String = "Thai Ha"
    Public gv_sComName As String = Environment.MachineName
    ' Chứa tên hoặc ID của máy chủ CSDL
    Public gv_sDBName As String = "Assembly"
    ' Tên CSDL
    Public gv_sUID As String = ""
    ' Chứa tên đăng nhập QTHT
    Public gv_sPWD As String = ""
    ' Chứa mật khẩu công khai của QTHT
    Public gv_bLoginSuccess As Boolean = False
    Public gv_sSymmetricAlgorithmName As String = "Rijndael"
    Private gv_mtx As Mutex
    Public gv_oMainForm As New FrmMain
    Public gv_ConnectSuccess As Boolean = False
    'Biến xác định xem có kết nối được vào CSDL để làm việc không?
    Public gv_sLanguageDisplay As String = "VN"
    'Hệ thống hỗ trợ 2 loại ngôn ngữ hiển thị là Tiếng Việt(Vn) và Tiếng Ang(En)
    Public gv_sSubSystemDisplay As String = "MENU"
    'Hệ thống hỗ trợ 2 loại ngôn ngữ hiển thị là Tiếng Việt(Vn) và Tiếng Ang(En)
    Public gv_intMenuItemCount As Integer = 0
    'Biến đếm số menuItem của chương trình để phục vụ cho quá trình thay đổi giao diện ngôn ngữ hiển thị
    Public AllowUp, AllowDown As Boolean
    'Biến xác định có cho phép xuất hiện nút mũi tên lên xuống để chuyển phân hệ làm việc khi số lượng phân hệ quá nhiều
    Public gv_intSubSystemCount As Integer = 0
    'Biến chứa số lượng phân hệ có được
    Public mv_bCanClose As Boolean = True
    'Biến xác định xem có được Close form khi nhấn vào Nút X(đóng) trên form main? Trong trường hợp logout thì không đóng được. Ngược lại đóng được
    Public gv_sLocalAlias As String
    Public gv_objArrDLL As New ArrayList
    'Tháng Năm hệ thống
    Public gv_intCurrMonth As Integer = 1
    Public gv_intCurrYear As Integer = 2007
    Public gv_intImgIndex As Integer = 13
    Public gv_intPicOrder As Integer = 0
    'Nhận các giá trị 0: Pic above Text;1: Pic Before Text
    Public SubSystemPanel As New IFModulePanel
    Public gv_intWidth As Integer
    Public gv_DSVersionFiles As New DataSet
    Public gv_sConnString As String
    Public gv_bAnnouceWhenHavingLastestVersion As Boolean = True
    Public gv_bCallCheckUpdate As Boolean = False
    Public gv_bHasCheckedVer As Boolean = False
    Public gv_bIsChecking As Boolean = False
    Public gv_oDSRFU As New DataSet
    ' Dataset chứa danh sách quyền của mỗi người dùng
    Public seryPorts() As SerialPort
    Public gv_Result(1, 0) As String


    Public Function KhoiTaoKetNoi() As Boolean
        Dim sv_oEncrypt As New Encrypt.Encrypt ("Rijndael")
        Dim fv_sUID, fv_sPWD As String
        Try
            ' If bGetConfigInfor (fv_sUID, fv_sPWD) Then
            'If fv_sUID Is Nothing Or fv_sPWD Is Nothing Then
            '    Return False
            'End If
            gv_sComName = LablinkServiceConfig.GetServer
            gv_sDBName = LablinkServiceConfig.GetDatabase
            fv_sUID = LablinkServiceConfig.GetUserId
            fv_sPWD = LablinkServiceConfig.GetPassword
            gv_sBranchID = LablinkBusinessConfig.GetBranchName

            Dim sv_sConnectionString As String = "workstation id=" & gv_sComName & ";packet size=4096;data source=" & _
                                                 gv_sComName & ";persist security info=False;initial catalog=" & _
                                                 gv_sDBName & ";uid=" & fv_sUID & ";pwd=" & fv_sPWD

            gv_sConnString = sv_sConnectionString

            ' Them bien cho LABLINK

            If IsNothing(gv_oSqlCnn) Then
                gv_oSqlCnn = New SqlConnection(sv_sConnectionString)
                gv_LabLinkCN = New SqlConnection(sv_sConnectionString)
                gv_oSqlCnn.Open()
                gv_LabLinkCN.Open()
                GetBranchInfor(gv_sBranchID)
            ElseIf gv_oSqlCnn.State = ConnectionState.Closed Then
                gv_oSqlCnn.Open()
            End If
            Return True
            'Else
            Return False
            'End If
        Catch ex As Exception
            MessageBox.Show("Không kết nối được vào CSDL. Liên hệ với quản trị hệ thống ", gv_sAnnouncement, _
                             MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return False
        End Try
    End Function

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Đọc file cấu hình để lấy về mã đơn vị quản lý, tên CSDL, UserName, Password
    'Đầu vào          :
    'Đầu ra            :Thành công=True. Không thành công=False
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function bGetConfigInfor (ByRef pv_sUID As String, ByRef pv_sPWD As String) As Boolean
        Dim fv_DS As New DataSet
        Dim fv_sUID As String
        Dim fv_sPWD As String
        Try
            If File.Exists (Application.StartupPath & "\Config.XML") Then
                ' Tiến hành đọc File cấu hình vào DataSet
                fv_DS.ReadXml (Application.StartupPath & "\Config.XML")
                If fv_DS.Tables (0).Rows.Count > 0 Then
                    ' Đọc dữ liệu vào các biến toàn cục
                    'Địa chỉ máy chủ CSDL
                    gv_sComName = fv_DS.Tables (0).Rows (0) ("SERVERADDRESS")
                    'Mã chi nhánh
                    gv_sBranchID = fv_DS.Tables (0).Rows (0) ("BranchID")
                    'UID côngkhai
                    fv_sUID = fv_DS.Tables (0).Rows (0) ("USERNAME")
                    'Mật khẩu công khai
                    fv_sPWD = fv_DS.Tables (0).Rows (0) ("PASSWORD")
                    'Tên Cơ sở dữ liệu
                    gv_sDBName = fv_DS.Tables (0).Rows (0) ("DATABASE_ID")
                    'Ngôn ngữ hiển thị
                    gv_sLanguageDisplay = fv_DS.Tables (0).Rows (0) ("LANGUAGEDISPLAY")
                    gv_sSubSystemDisplay = fv_DS.Tables (0).Rows (0) ("INTERFACEDISPLAY")

                    'GetAlias của Local
                    gv_sLocalAlias = fv_DS.Tables (0).Rows (0) ("LOCALALIAS")


                    'Tiến hành kết nối bằng tài khoản công khai vừa đọc trong file Config để lấy về tài khoản đăng nhập CSDL
                    Dim fv_oSQLCon As SqlConnection
                    Dim _
                        fv_sSqlConstr = "workstation id=" & gv_sComName & ";packet size=4096;data source=" & gv_sComName & _
                                        ";persist security info=False;initial catalog=" & gv_sDBName & ";uid=" & fv_sUID & _
                                        ";pwd=" & fv_sPWD
                    fv_oSQLCon = New SqlConnection (fv_sSqlConstr)
                    'Mở CSDL
                    Try
                        fv_oSQLCon.Open()
                        Console.WriteLine ("Fv_osqlcon.open is OK")

                        pv_sUID = fv_sUID
                        pv_sPWD = fv_sPWD


                        'Lấy tài khoản bí mật để đăng nhập CSDL
                        GetSecretAccount (fv_oSQLCon, pv_sUID, pv_sPWD)
                    Catch SQLex As Exception
                        MessageBox.Show ( _
                                         "Không đăng nhập được vào CSDL " & gv_sDBName & _
                                         " bằng tài khoản công khai(UID=" & fv_sUID & ";PWD=" & fv_sPWD & _
                                         "). Hãy cấu hình lại File Config.XML sau đó đăng nhập lại.", gv_sAnnouncement, _
                                         MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                    End Try
                Else
                    MessageBox.Show ("Không có dữ liệu trong File cấu hình! Bạn hãy xem lại", gv_sAnnouncement, _
                                     MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False
                End If
            Else
                MessageBox.Show ("Không tồn tại File cấu hình có tên: Config.XML!", gv_sAnnouncement, _
                                 MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function

    Public Sub GetBranchInfo()
        Dim dt As New DataTable
        Dim da As New SqlDataAdapter("SELECT * FROM tbl_ManagementUnit WHERE PK_sBranchID=N'" & gv_sBranchID & "'", gv_oSqlCnn)
        Try
            da.Fill (dt)
            If dt.Rows.Count > 0 Then
                gv_sParentBranchName = sDBnull (dt.Rows (0) ("sParentBranchName"))
                gv_sPhone = sDBnull (dt.Rows (0) ("sPhone"))
                gv_sAddress = sDBnull (dt.Rows (0) ("sAddress"))
            End If
        Catch ex As Exception
            MsgBox ("Không lấy được thông tin đơn vị quản lý " & vbCrLf & ex.Message)
        End Try
    End Sub

    Public Function sDBnull (ByVal pv_obj As Object, Optional ByVal Reval As String = "") As String
        If IsDBNull (pv_obj) Or IsNothing (pv_obj) Then
            Return Reval
        Else
            Return pv_obj.ToString
        End If
    End Function

    Private Sub GetSecretAccount (ByVal pv_Conn As SqlConnection, ByRef pv_sUID As String, ByRef pv_sPWD As String)
        'Dim sv_Ds As New DataSet
        'Dim sv_DA As SqlDataAdapter
        'Try
        '    sv_DA = New SqlDataAdapter ("SELECT * FROM TBL_SECURITY", pv_Conn)
        '    sv_DA.Fill (sv_Ds, "TBL_SECURITY")
        '    If sv_Ds.Tables (0).Rows.Count > 0 Then
        '        pv_sUID = sv_Ds.Tables (0).Rows (0) ("sUID")
        '        pv_sPWD = sv_Ds.Tables (0).Rows (0) ("sPWD")
        '        Console.WriteLine ("GetSecret Account OK")

        '    Else
        '        MessageBox.Show("Không tồn tại tài khoản đăng nhập trong bảng TBL_SECURITY! Đề nghị với DBAdmin tạo tài khoản đăng nhập trong bảng đó.", _
        '                         gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show ( _
        '                     "Bạn cần gán lại quyền truy cập vào bảng TBL_SECURITY cho tài khoản công khai! Đề nghị với DBAdmin thực hiện công việc này bằng tiện ích CreateUser.exe", _
        '                     gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
        'End Try
    End Sub

    Private Sub GetBranchInfor (ByVal pv_sBranchID As String)
        Dim sv_Ds As New DataSet
        Dim sv_DA As SqlDataAdapter
        Try
            sv_DA = _
                New SqlDataAdapter ("SELECT * FROM TBL_ManagementUnit WHERE PK_sBranchID=N'" & pv_sBranchID & "'", _
                                    gv_oSqlCnn)
            sv_DA.Fill (sv_Ds, "TBL_ManagementUnit")
            If sv_Ds.Tables (0).Rows.Count > 0 Then
                gv_sBranchName = sv_Ds.Tables (0).Rows (0) ("sName")
            Else
                Return
            End If
        Catch ex As Exception

        End Try
    End Sub

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Hàm Main được gọi mỗi khi khởi tạo ứng dụng. Áp dụng cơ chế của lớp Mutex nhằm chỉ cho
    '                 phép chạy một khởi tạo của ứng dụng tại một thời điểm trên một máy
    'Đầu vào          :
    'Đầu ra            :
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Sub Main()
        Try
            Dim bCreated As Boolean
            gv_mtx = New Mutex (False, "APP_DVC", bCreated)
            If bCreated = True Then
                gv_intWidth = IIf (gv_intPicOrder = 0, 57, 45)
                gv_oMainForm.ShowDialog()
            Else
                MessageBox.Show ("Một khởi tạo của chương trình đang chạy!", "Thông báo", MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)
                End
            End If
        Catch ex As Exception
        End Try
    End Sub

    '---------------------------------------------------------------------------------------------------
    'Mục đích  :Tạo hiệu ứng mờ dần lúc Form hiện lên hoặc đóng đi
    'Đầu vào   : -Form cần tạo hiệu ứng(pv_oForm)
    '                 -Độ mờ dần(pv_fStepLevel)
    '                 -Đối tượng điều khiển sự mờ dần của Form(pv_oTmr as Timer)
    '                 -Biến xác định là tăng độ mờ hay giảm độ mờ của Form.Mặc định là tăng Opacity(pv_bIncreteOrDecrete)
    '                 -Biến xác định khoảng thời gian của mỗi lần tăng(giảm) độ mờ(pv_iTimerInterval)
    'Đầu ra: Form được trình diễn
    'Người Tạo :CuongDV
    'Return      :None
    'Ngày Tạo  :22/02/2005
    '---------------------------------------------------------------------------------------------------
    Public Sub gs_SlideMe (ByVal pv_oForm As Form, ByVal pv_fStepLevel As Double, ByVal pv_oTmr As Timers.Timer, _
                           Optional ByVal pv_bIncreteOrDecrete As Boolean = True, _
                           Optional ByVal pv_iTimerInterval As Integer = 10)
        Try
            'Thiết lập khoảng thời gian chờ của đối tượng điều khiển Timer
            pv_oTmr.Interval = pv_iTimerInterval
            Select Case pv_bIncreteOrDecrete
                Case True 'Tăng độ mờ
                    If pv_oForm.Opacity < 1 Then
                        pv_oForm.Opacity += pv_fStepLevel
                    Else
                        'Nếu Opacity=1(Tức là Form đã hiện hoàn toàn) thì ngừng không tăng nữa
                        pv_oTmr.Enabled = False
                    End If
                Case False 'Giảm độ mờ
                    If pv_oForm.Opacity > 0 Then
                        pv_oForm.Opacity -= pv_fStepLevel
                    Else
                        'Nếu Opacity<=0(Tức là Form đã mờ hoàn toàn) thì ngừng không giảm nữa và thực hiện đóng Form
                        pv_oTmr.Enabled = False
                        pv_oForm.Close()
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Sub SetField (ByVal t As Type, ByVal FieldName As String, ByVal FieldValue As Object)
        Try
            t.InvokeMember (FieldName.Trim, BindingFlags.Default Or BindingFlags.SetField, Nothing, t, _
                            New Object() {FieldValue})
        Catch ex As Exception
        End Try
    End Sub

    Public Function ConvertDataType (ByVal Value As String, ByVal DataType As String) As Object
        Try
            Select Case DataType.ToUpper
                Case "DATETIME"
                    Return Date.ParseExact (Value, "dd/MM/yyyy", New CultureInfo ("vi-VN"))
                    Exit Function
                Case "BIGINT", "INT", "SMALLINT"
                    Return Integer.Parse (Value)
                    Exit Function
                Case "DECIMAL", "FLOAT", "NUMERIC", "REAL", "MONEY"
                    Return Double.Parse (Value)
                    Exit Function
                Case "CHAR", "NVARCHAR"
                    Return Value.Trim
                Case "BOOLEAN"
                    Return CBool (Value)
                Case "CURSOR"
            End Select

        Catch ex As Exception
            Return Value
        End Try
    End Function

    '------------------------------------------------------------------------------------------------------------
    'Mục đích        : Lấy về danh sách tất cả các tham số từ CSDL
    'Đầu vào          :
    'Đầu ra            :Danh sách các Tham số
    'Người tạo       :CuongDV
    'Ngày tạo         :09/03/2005
    'Nhật kí sửa đổi:
    '------------------------------------------------------------------------------------------------------------
    Public Function dsGetAllParams() As DataSet
        Dim ds As New DataSet
        Dim da As SqlDataAdapter
        Dim sv_sSql As String
        Try
            sv_sSql = "SELECT * from tbl_SystemParameters WHERE  FP_sBranchID=N'" & gv_sBranchID & "' Order By sName"
            da = New SqlDataAdapter (sv_sSql, gv_oSqlCnn)
            da.Fill (ds, "tbl_Params")
            Return ds
        Catch ex As Exception
        End Try
    End Function

    Public Sub LoadParamsValues (ByVal pv_objAss As [Assembly])
        Dim CurRow As DataRow
        Dim t As Type
        Try
            gv_dsParam = dsGetAllParams()
            For Each t In pv_objAss.GetTypes
                Dim _
                    fi() As FieldInfo = _
                        t.GetFields (BindingFlags.Static Or BindingFlags.Public Or BindingFlags.IgnoreCase)
                Dim f As FieldInfo
                For Each f In fi
                    Select Case f.Name.ToUpper
                        Case "gv_sBranchName".ToUpper
                            SetField (t, "gv_sBranchName", gv_sBranchName)
                        Case "gv_oSqlCnn".ToUpper
                            SetField (t, "gv_oSqlCnn", gv_oSqlCnn)
                        Case "gv_sBranchID".ToUpper
                            SetField (t, "gv_sBranchID", gv_sBranchID)
                        Case "gv_intCurrMonth".ToUpper
                            SetField (t, "gv_intCurrMonth", gv_intCurrMonth)
                        Case "gv_intCurrYear".ToUpper
                            SetField (t, "gv_intCurrYear", gv_intCurrYear)
                        Case "gv_sUID".ToUpper
                            SetField (t, "gv_sUID", gv_sUID)
                        Case "SeryPorts".ToUpper
                            SetField (t, "SeryPorts", seryPorts)
                        Case "gv_sParentBranchName".ToUpper
                            SetField (t, "gv_sParentBranchName", gv_sParentBranchName)
                        Case "gv_sAddress".ToUpper
                            SetField (t, "gv_sAddress", gv_sAddress)
                        Case "gv_sPhone".ToUpper
                            SetField (t, "gv_sPhone", gv_sPhone)
                        Case "gv_sLanguageDisplay".ToUpper
                            SetField (t, "gv_sLanguageDisplay", gv_sLanguageDisplay)
                        Case Else
                    End Select
                    If gv_dsParam.Tables (0).Rows.Count > 0 Then
                        For Each CurRow In gv_dsParam.Tables (0).Rows
                            If _
                                f.Name.ToUpper = CurRow.Item ("sName").ToString.ToUpper And _
                                CInt (CurRow.Item ("iStatus")) = 1 Then
                                SetField (t, f.Name, _
                                          ConvertDataType (CurRow.Item ("sValue"), _
                                                           CurRow.Item ("sDataType")))
                            End If
                        Next
                    End If
                Next
            Next
        Catch ex As Exception
            MsgBox (ex.ToString, MsgBoxStyle.Exclamation, "DVC")
        End Try
    End Sub

    Public Sub LoadParamsValuesAgain (ByVal pv_objAss As [Assembly])
        Dim CurRow As DataRow
        Dim t As Type
        Try
            gv_dsParam = dsGetAllParams()
            For Each t In pv_objAss.GetTypes
                Dim _
                    fi() As FieldInfo = _
                        t.GetFields (BindingFlags.Static Or BindingFlags.Public Or BindingFlags.IgnoreCase)
                Dim f As FieldInfo
                For Each f In fi
                    If gv_dsParam.Tables (0).Rows.Count > 0 Then
                        For Each CurRow In gv_dsParam.Tables (0).Rows
                            If _
                                f.Name.ToUpper = CurRow.Item ("sName").ToString.ToUpper And _
                                CInt (CurRow.Item ("iStatus")) = 1 Then
                                SetField (t, f.Name, _
                                          ConvertDataType (CurRow.Item ("sValue"), _
                                                           CurRow.Item ("sDataType")))
                            End If
                        Next
                    End If
                Next
            Next
            MessageBox.Show ("Thiết lập tham số hệ thống thành công!", gv_sAnnouncement, MessageBoxButtons.OK, _
                             MessageBoxIcon.Information)
        Catch ex As Exception
            MsgBox (ex.ToString, MsgBoxStyle.Exclamation, "DVC")
        End Try
    End Sub

    Public Sub ProcessInterfacesAndMethods (ByVal pv_objAss As [Assembly]) '[Assembly])
        Dim t As Type
        Dim NewObject As Object
        Try
            For Each t In pv_objAss.GetTypes
                If t.IsInterface Then
                    Dim t1 As Type
                    For Each t1 In pv_objAss.GetTypes
                        Dim i As Object = t1.GetInterface (t.Name)
                        If Not i Is Nothing Then
                            NewObject = pv_objAss.CreateInstance (t1.FullName)
                            Exit For
                        End If
                    Next
                    Dim _
                        mi() As MethodInfo = _
                            t.GetMethods ( _
                                          BindingFlags.Static Or BindingFlags.Public Or BindingFlags.NonPublic Or _
                                          BindingFlags.Instance)
                    '  methods
                    Dim m As MethodInfo
                    For Each m In mi
                        t1.InvokeMember (m.Name, _
                                         BindingFlags.Default Or BindingFlags.InvokeMethod Or BindingFlags.Instance Or _
                                         BindingFlags.NonPublic Or BindingFlags.Public, Nothing, NewObject, Nothing)
                    Next
                End If
            Next
            NewObject = Nothing
        Catch ex As Exception
        End Try
    End Sub

    'Mục đích       :Thực hiện khi chọn một chức năng trên Menu
    'Đầu vào         :
    'Đầu ra           :
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Public Sub _ClickTbr (ByVal sender As Object, ByVal e As ToolBarButtonClickEventArgs)
        Try
            If _
                e.Button.Tag = "-1" Or e.Button.Tag = "-2" Or e.Button.Tag = "-3" Or e.Button.Tag = "-4" Or _
                e.Button.Tag = "-5" Then
            Else
                If CType (e.Button, ToolBarBtn).DropDownMenu Is Nothing Then
                    ms_InvokeForm_tbr (CType (e.Button, ToolBarBtn).mv_sAssemblyName, _
                                       CType (e.Button, ToolBarBtn).mv_sDLLName, _
                                       CType (e.Button, ToolBarBtn).mv_sFormName, _
                                       CType (e.Button, ToolBarBtn).mv_sParameterList)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '----------------------------------------------------------------------------------------------------
    'Mục đích       :Thực hiện chức năng tương ứng với hàm được gọi trong Assembly
    'Đầu vào         :Tên Assembly,tên DLL,tên Form
    'Đầu ra           :Menu của phân hệ được xây dựng 
    'Người tạo      :CuongDV
    'Ngày tạo       :
    'Last Modified:
    '----------------------------------------------------------------------------------------------------
    Public Sub ms_InvokeForm_tbr (ByVal pv_sAssemblyName As String, _
                                  ByVal pv_sDLLName As String, _
                                  ByVal pv_sFormName As String, ByVal pv_sParameterList As String)
        Try
            Dim sv_oAss As [Assembly]
            Dim bExistForm As Boolean = False
            Dim sv_oAllForm As Type()
            'Contains All Forms from Assembly
            If pv_sDLLName.Trim.Equals (String.Empty) Then
                MessageBox.Show ("Chưa gán Role cho nút này hoặc Role ứng với nút này chưa được gán chức năng", _
                                 "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
            If Not File.Exists (Application.StartupPath & "\" & pv_sDLLName & ".DLL") Then
                MessageBox.Show ("Không tồn tại phân hệ " & pv_sDLLName & ".DLL", gv_sAnnouncement, MessageBoxButtons.OK, _
                                 MessageBoxIcon.Information)
                Return
            End If
            'Load Assembly Information from AssemblyName
            sv_oAss = [Assembly].LoadFrom (Application.StartupPath & "\" & pv_sDLLName & ".DLL")
            'Get Forms from AssemblyInfor
            sv_oAllForm = sv_oAss.GetTypes
            gv_CurrAssembly = sv_oAss
            If Not gv_objArrDLL.Contains (pv_sDLLName) Then
                gv_objArrDLL.Add (pv_sDLLName)
            End If
            LoadParamsValues (sv_oAss)
            '----------------------------------------------------------------------------------
            Dim sv_iflags As BindingFlags = BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.Static Or _
                                            BindingFlags.Instance Or BindingFlags.DeclaredOnly
            Dim sv_oType As Type
            For Each sv_oType In sv_oAllForm
                Try
                    If sv_oType.Name.Trim.ToUpper.Trim.Equals (pv_sFormName.Trim.ToUpper) Then
                        Dim sv_oCalledForm As Form
                        If pv_sParameterList.Trim.Equals (String.Empty) Then
                            sv_oCalledForm = CType (Activator.CreateInstance (sv_oType), Form)
                        Else
                            sv_oCalledForm = _
                                CType (Activator.CreateInstance (sv_oType, New Object() {pv_sParameterList}), Form)
                        End If
                        bExistForm = True
                        sv_oCalledForm.ShowDialog()
                        Exit For
                    End If
                Catch ex As Exception
                End Try
            Next
            If Not bExistForm Then
                MessageBox.Show ("Không tồn tại chức năng " & pv_sFormName & " trong phân hệ " & pv_sDLLName & ".DLL", _
                                 gv_sAnnouncement, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If
        Catch ex As Exception
            MessageBox.Show ("Lỗi khởi động chức năng!" & vbCrLf & ex.Message, "Thông báo", MessageBoxButtons.OK, _
                             MessageBoxIcon.Information)
        End Try
    End Sub

    Private Function GetAllVersions() As DataSet
        Dim DA As New SqlDataAdapter
        Dim sv_ds As New DataSet
        Dim sv_sSql As String
        Dim g As SqlConnection
        g = New SqlConnection (gv_sConnString)
        g.Open()
        Try
            sv_sSql = _
                "SELECT 'T' AS CHON,'' AS sStatus, PK_intID,sFileName,objData,sVersion,dblCapacity,intRar,sRarFileName FROM TBL_VERSION V WHERE PK_intID=(SELECT MAX(PK_intID) FROM TBL_VERSION WHERE sFileName=V.sFileName)"
            DA = New SqlDataAdapter (sv_sSql, g)
            DA.Fill (sv_ds, "tbl_version")
            g.Close()
            g = Nothing
            Return sv_ds
        Catch ex As Exception
            g.Close()
            g = Nothing
            Return sv_ds
        End Try
    End Function

    Private Function GetAllImgAndIcons() As DataSet
        Dim DA As New SqlDataAdapter
        Dim sv_ds As New DataSet
        Dim sv_sSql As String
        Dim g As SqlConnection
        g = New SqlConnection (gv_sConnString)
        g.Open()
        Try
            sv_sSql = "SELECT sFileName,Data FROM TBL_IMGANDICON"
            DA = New SqlDataAdapter (sv_sSql, g)
            DA.Fill (sv_ds, "TBL_IMGANDICON")
            g.Close()
            g = Nothing
            Return sv_ds
        Catch ex As Exception
            g.Close()
            g = Nothing
            Return sv_ds
        End Try
    End Function

    Public Function GetLastestVersion() As DataSet
        Dim sv_DSLastestV As New DataSet
        Dim sv_DSVersion As New DataSet
        sv_DSVersion = GetAllVersions()
        Try
            sv_DSLastestV = sv_DSVersion.Clone
            If sv_DSVersion.Tables (0).Rows.Count > 0 Then
                For Each dr As DataRow In sv_DSVersion.Tables (0).Rows
                    If Not File.Exists (Application.StartupPath & "\" & dr ("sFileName")) Then
                        InsertNewRow (dr, sv_DSVersion.Tables (0), sv_DSLastestV)
                    Else 'Nếu tồn tại thì kiểm tra xem Version có khác nhau không?
                        Dim _
                            _FileVersionInfo As FileVersionInfo = _
                                FileVersionInfo.GetVersionInfo (Application.StartupPath & "\" & dr ("sFileName"))
                        Dim sVersion As String = _FileVersionInfo.ProductVersion
                        If Not sVersion.Equals (dr ("sVersion")) Then
                            InsertNewRow (dr, sv_DSVersion.Tables (0), sv_DSLastestV)
                        End If
                    End If
                Next
                sv_DSLastestV.Tables (0).AcceptChanges()
            End If
            Return sv_DSLastestV
        Catch ex As Exception

        End Try
    End Function

    Private Sub InsertNewRow (ByVal dr As DataRow, ByVal pv_SourceTable As DataTable, ByRef pv_DS As DataSet)
        Try
            Dim DRLastestV As DataRow = pv_DS.Tables (0).NewRow
            For Each col As DataColumn In pv_SourceTable.Columns
                DRLastestV (col.ColumnName) = dr (col.ColumnName)
            Next
            pv_DS.Tables (0).Rows.Add (DRLastestV)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub CheckingVersion()
        Dim sv_oform As New frm_WaitingForCheckingVer
        sv_oform.Show()
    End Sub

    Public Sub CheckLastestVersion()
        Try
            gv_bHasCheckedVer = False
            gv_oMainForm.Cursor = Cursors.AppStarting
            Dim thre As Thread
            thre = New Thread (AddressOf DownloadImgAndIcons)
            thre.Start()
            If Not gv_DSVersionFiles Is Nothing Then
                If gv_DSVersionFiles.Tables.Count > 0 Then
                    gv_DSVersionFiles.Tables.Clear()
                End If
            End If
            If gv_DSVersionFiles.Tables.Count = 0 Then
                gv_DSVersionFiles = GetLastestVersion()
            Else
                Dim DS As New DataSet
                DS = GetLastestVersion()
                For Each dr As DataRow In DS.Tables (0).Rows
                    InsertNewRow (dr, DS.Tables (0), gv_DSVersionFiles)
                Next
            End If
            If gv_DSVersionFiles.Tables.Count > 0 Then
                If gv_DSVersionFiles.Tables (0).Rows.Count > 0 Then
                    gv_bHasCheckedVer = True
                    gv_oMainForm.Cursor = Cursors.Default
                    Dim sv_oForm As New frm_DownLoadVersion
                    sv_oForm.ShowDialog (gv_oMainForm)
                Else
                    If gv_bCallCheckUpdate Then
                        MessageBox.Show ("Các phiên bản hiện thời trên máy bạn là mới nhất!", gv_sAnnouncement, _
                                         MessageBoxButtons.OK, MessageBoxIcon.Information)
                        gv_bCallCheckUpdate = False
                    End If
                    gv_oMainForm.Cursor = Cursors.Default
                    Return
                End If
            End If
            gv_oMainForm.Cursor = Cursors.Default
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DownloadImgAndIcons()
        Dim Ds As New DataSet
        Ds = GetAllImgAndIcons()
        If Directory.Exists ("C:\ImagesAndIcons") Then
        Else
            Directory.CreateDirectory ("C:\ImagesAndIcons")
        End If
        Try
            If Ds.Tables.Count > 0 Then
                If Ds.Tables (0).Rows.Count > 0 Then
                    For Each dr As DataRow In Ds.Tables (0).Rows
                        Try
                            If Not File.Exists ("C:\ImagesAndIcons\" & dr ("sFileName")) Then
                                Dim objData() As Byte = CType (dr ("Data"), Byte())
                                Dim ms As New MemoryStream (objData)
                                Dim _
                                    fs As _
                                        New FileStream ("C:\ImagesAndIcons\" & dr ("sFileName"), FileMode.Create, _
                                                        FileAccess.Write)
                                ms.WriteTo (fs)
                                ms.Flush()
                                fs.Close()
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function sGetTextDisplay (ByVal pv_intRolePerformed As Integer) As String
        Try
            Dim sSql As String
            Dim DA As SqlDataAdapter
            Dim DS As New DataSet
            sSql = "SELECT sRoleName,sEngRoleName From TBL_ROLES WHERE iRole=" & pv_intRolePerformed & _
                   " AND FP_sBranchID=N'" & gv_sBranchID & "'"
            Try
                DA = New SqlDataAdapter (sSql, gv_oSqlCnn)
                DA.Fill (DS, "TBL_ROLES")
                If DS.Tables (0).Rows.Count > 0 Then
                    If gv_sLanguageDisplay = "VN" Then
                        Return DS.Tables (0).Rows (0) ("sRoleName")
                    Else
                        If DS.Tables (0).Rows (0) ("sEngName").ToString.Trim <> "" Then
                            Return DS.Tables (0).Rows (0) ("sEngRoleName")
                        Else
                            Return DS.Tables (0).Rows (0) ("sRoleName")
                        End If
                    End If
                End If
            Catch ex As Exception
                Return ""
            End Try
        Catch ex As Exception

        End Try
    End Function
End Module
