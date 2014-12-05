Imports System.Data.SqlClient
Imports Mabry.Windows.Forms.Barcode
Imports System.IO
Imports System.Windows.Forms
Imports VB6 = Microsoft.VisualBasic.Strings

Public Module globalModule
    'Khai báo các biến toàn cục sử dụng trong CT
    Public gv_oSqlCnn As SqlConnection 'Biến kết nối tới CSDL-->Bạn chỉ việc dùng biến này mà không cần khởi tạo chúng
    Public gv_sBranchID As String = "THAIHA" 'Mã chi nhánh
    Public gv_sBranchName As String = "Việt Ba" ' Tên chi nhánh
    Public gv_sParentBranchName As String = "Việt Ba"
    Public gv_sAddress As String = "Hà Nội"
    Public gv_sPhone As String = "0904 648006"
    ' Public gv_objecttype As String = "Khám Dịch vụ"
    Public gv_sUID As String = "" ' Chứa tên đăng nhập QTHT
    'Tháng Năm hệ thống
    Public gv_intCurrMonth As Integer'Tháng làm việc(Chưa dùng)
    Public gv_intCurrYear As Integer'Năm làm việc(Chưa dùng)
    Public gv_sLanguageDisplay As String = "VN" ' Biến xác định ngôn ngữ hiển thị.
    Public KTBARCODE As String = lablinkhelper.Utilities.GetKTBarcode
    'Bạn phải Load lại từ File Config nếu muốn dùng. Tiếng Việt có mã=VN. Tiếng Anh có mã=EN
    Public gv_sAnnouce As String = "Thông báo"
    Public CurrDtGridView As DataGridView
    Public gv_TestTypeDetailReport As Integer = 0
    Public gv_arrKeySearch As New ArrayList
    Public gv_bCrptHasCached As Boolean = False

    Public Function sGetFromDateToDate(ByVal fromDate As String, ByVal toDate As String) As String
        'Dim strFromDate = "Ngày " & VB6.Right("0" & fromDate.Day, 2) & " tháng " & VB6.Right("0" & fromDate.Month, 2) & " năm " & fromDate.Year
        Return "Từ ngày " & fromDate & " đến " & toDate
    End Function

    Public Function CorrectValue(ByVal pv_dblValue As Double) As Double
        If pv_dblValue <= 0.00001 Then
            Return 0
        Else
            Return pv_dblValue
        End If
    End Function

    Public Function sGetCurrentDay() As String
        Return "Ngày " & VB6.Right("0" & Now.Day, 2) & " tháng " & VB6.Right("0" & Now.Month, 2) & " năm " & Now.Year
    End Function

    Public Function sGetName(ByVal pv_sTableName As String, ByVal pv_sFieldName As String, ByVal pv_ID As Integer) As String
        Try
            Dim fv_da As New SqlDataAdapter("SELECT sNAME FROM " & pv_sTableName & " WHERE " & pv_sFieldName & "=" & pv_ID, gv_oSqlCnn)
            Dim fv_dt As New DataTable
            fv_da.Fill(fv_dt)
            If fv_dt.Rows.Count > 0 Then
                Return fv_dt.Rows(0)(0)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function sGetFieldValue(ByVal pv_sTableName As String, ByVal pv_sFieldName As String, ByVal pv_sCondition As String) As String
        Try
            Dim fv_da As New SqlDataAdapter("SELECT " & pv_sFieldName & " FROM " & pv_sTableName & " WHERE " & pv_sCondition, gv_oSqlCnn)
            Dim fv_dt As New DataTable
            fv_da.Fill(fv_dt)
            If fv_dt.Rows.Count > 0 Then
                Return fv_dt.Rows(0)(0)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function drGetDatarow(ByVal pv_sTableName As String, ByVal pv_sFieldName As String, ByVal pv_sCondition As String) As DataRow
        Try
            Dim fv_da As New SqlDataAdapter("SELECT " & pv_sFieldName & " FROM " & pv_sTableName & " WHERE " & pv_sCondition, gv_oSqlCnn)
            Dim fv_dt As New DataTable
            fv_da.Fill(fv_dt)
            If fv_dt.Rows.Count > 0 Then
                Return fv_dt.Rows(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CorrectValue(ByVal pv_sValue As String) As String
        Return pv_sValue.Trim.Replace("'", "''")
    End Function

    Public Sub FillComboBox(ByVal pv_ds As DataSet, ByVal sDisplaymember As String, ByVal sValueMember As String, ByVal pv_oCboBox As ComboBox)
        Try
            pv_oCboBox.DataSource = Nothing
            With pv_oCboBox
                .DataSource = pv_ds.Tables(0).DefaultView
                .DisplayMember = sDisplaymember
                .ValueMember = sValueMember
            End With
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillComboBox(ByVal pv_ds As DataSet, ByVal pv_oCboBox As ComboBox)
        Try
            pv_oCboBox.DataSource = Nothing
            With pv_oCboBox
                .DataSource = pv_ds.Tables(0).DefaultView
                .DisplayMember = "SNAME"
                .ValueMember = "ID"
            End With
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillComboBoxNoUsingValueMember1(ByVal pv_ds As DataSet, ByVal pv_oCboBox As ComboBox)
        Try
            pv_oCboBox.Items.Clear()

            For Each dr As DataRow In pv_ds.Tables(0).Rows
                pv_oCboBox.Items.Add(dr("ID") & "-" & dr("sName"))
            Next
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillComboBoxNoUsingValueMember(ByVal pv_ds As DataSet, ByVal pv_oCboBox As ComboBox)
        Try
            pv_oCboBox.Items.Clear()

            pv_oCboBox.Items.Add("Tất cả")
            For Each dr As DataRow In pv_ds.Tables(0).Rows
                pv_oCboBox.Items.Add(dr("ID") & "-" & dr("sName"))
            Next
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillComboBox(ByVal pv_sTableName As String, ByVal sDisplaymember As String, ByVal sValueMember As String, ByVal pv_oCboBox As ComboBox)
        Dim sv_Da As New SqlDataAdapter("SELECT * FROM " & pv_sTableName, gv_oSqlCnn)
        Dim sv_Dt As New DataTable
        Try
            sv_Da.Fill(sv_Dt)
            pv_oCboBox.Items.Clear()
            pv_oCboBox.DataSource = Nothing
            With pv_oCboBox
                .DataSource = sv_Dt.DefaultView
                .DisplayMember = sDisplaymember
                .ValueMember = sValueMember
            End With
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillComboBoxNoUsingValueMember(ByVal pv_sTableName As String, ByVal sDisplaymember As String, ByVal sValueMember As String, ByVal pv_oCboBox As ComboBox)
        Dim sv_Da As New SqlDataAdapter("SELECT * FROM " & pv_sTableName, gv_oSqlCnn)
        Dim sv_Dt As New DataTable
        Try
            sv_Da.Fill(sv_Dt)
            pv_oCboBox.Items.Clear()
            pv_oCboBox.Items.Add("Tất cả")
            For Each dr As DataRow In sv_Dt.Rows
                pv_oCboBox.Items.Add(dr(sValueMember) & "-" & dr(sDisplaymember))
            Next
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillComboBoxNoUsingValueMember1(ByVal pv_sTableName As String, ByVal sDisplaymember As String, ByVal sValueMember As String, ByVal pv_oCboBox As ComboBox)
        Dim sv_Da As New SqlDataAdapter("SELECT * FROM " & pv_sTableName, gv_oSqlCnn)
        Dim sv_Dt As New DataTable
        Try
            sv_Da.Fill(sv_Dt)
            pv_oCboBox.Items.Clear()
            For Each dr As DataRow In sv_Dt.Rows
                pv_oCboBox.Items.Add(dr(sValueMember) & "-" & dr(sDisplaymember))
            Next
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub FillComboBox(ByVal pv_sTableName As String, ByVal pv_oCboBox As ComboBox)
        Dim sv_Da As New SqlDataAdapter("SELECT * FROM " & pv_sTableName & " ORDER BY intSTT ASC", gv_oSqlCnn)
        Dim sv_Dt As New DataTable
        Try
            sv_Da.Fill(sv_Dt)
            pv_oCboBox.Items.Clear()
            pv_oCboBox.DataSource = Nothing
            With pv_oCboBox
                .DataSource = sv_Dt.DefaultView
                .DisplayMember = "SNAME"
                .ValueMember = "ID"
            End With
            If pv_oCboBox.Items.Count > 0 Then
                pv_oCboBox.SelectedIndex = 0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Function getMaxID(ByVal pv_sFieldName As String, ByVal pv_sTableName As String) As Integer
        Dim fv_dt As New DataTable
        Dim fv_da As New SqlDataAdapter("SELECT MAX(" & pv_sFieldName & ") FROM " & pv_sTableName, gv_oSqlCnn)
        Try
            fv_da.Fill(fv_dt)
            If fv_dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(fv_dt.Rows(0)(0)), 0, fv_dt.Rows(0)(0)) + 1
            Else
                Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function

    Public Function getExactlyMaxID(ByVal pv_sFieldName As String, ByVal pv_sTableName As String) As Integer
        Dim fv_dt As New DataTable
        Dim fv_da As New SqlDataAdapter("SELECT MAX(" & pv_sFieldName & ") FROM " & pv_sTableName, gv_oSqlCnn)
        Try
            fv_da.Fill(fv_dt)
            If fv_dt.Rows.Count > 0 Then
                Return IIf(IsDBNull(fv_dt.Rows(0)(0)), 0, fv_dt.Rows(0)(0))
            Else
                Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return -1
        End Try
    End Function
    Public Function bytGetImage(ByVal pv_sImgPath As String) As Byte()
        Try
            Dim fs As FileStream
            If File.Exists(pv_sImgPath) Then
                fs = New FileStream(pv_sImgPath, FileMode.Open)
            Else
                Return Nothing
            End If
            Dim rd As BinaryReader = New BinaryReader(fs)
            Dim arrData() As Byte = rd.ReadBytes(CInt(fs.Length))
            fs.Flush()
            fs.Close()
            Return arrData
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function AcceptQuestion(ByVal pv_sMsg As String) As Boolean
        If MessageBox.Show(pv_sMsg, gv_sAnnouce, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub ShowMsg(ByVal pv_sMsg As String, Optional ByVal sTitle As String = "Thông báo")
        MessageBox.Show(pv_sMsg, sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Sub ShowErrMsg(ByVal pv_sError As String)
        MessageBox.Show("Lỗi: " & pv_sError, gv_sAnnouce, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Function dblgetExchangeRate(ByVal pv_MoneyID As Integer, ByVal pv_sMoneyName As String) As Double
        Dim fv_DA As SqlDataAdapter
        If pv_MoneyID <> -1 Then
            fv_DA = New SqlDataAdapter("SELECT fRate FROM TBL_MONEYUNIT WHERE ID=" & pv_MoneyID, gv_oSqlCnn)
        Else
            fv_DA = New SqlDataAdapter("SELECT fRate FROM TBL_MONEYUNIT WHERE sName=N'" & pv_sMoneyName & "'", gv_oSqlCnn)
        End If

        Dim fv_DS As New DataSet
        Try
            fv_DA.Fill(fv_DS, "TBL_MONEYUNIT")
            If fv_DS.Tables(0).Rows.Count > 0 Then
                Return CDbl(fv_DS.Tables(0).Rows(0)(0))
            Else
                Return 0
            End If
        Catch ex As Exception
            ShowErrMsg(ex.Message)
            Return 0
        End Try
    End Function

    Public Function sDBnull(ByVal pv_obj As Object, Optional ByVal Reval As String = "") As String
        If IsDBNull(pv_obj) Or IsNothing(pv_obj) Then
            Return Reval
        Else
            Return pv_obj.ToString
        End If
    End Function

    Public Function IsDBnullOrNothing(ByVal pv_obj As Object) As Boolean
        If IsDBNull(pv_obj) Or IsNothing(pv_obj) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function intDBnull(ByVal pv_obj As Object, Optional ByVal reval As Integer = -1) As Integer
        If IsDBNull(pv_obj) Or IsNothing(pv_obj) Then
            Return reval
        Else
            Return CInt(pv_obj)
        End If
    End Function

    Public Function fDBnull(ByVal pv_obj As Object, Optional ByVal Reval As Double = -1) As Double
        If IsDBNull(pv_obj) OrElse IsNothing(pv_obj) OrElse sDBnull(pv_obj) = "" Then
            Return Reval
        Else
            Return CDbl(pv_obj)
        End If
    End Function

    Public Function bIsExisted(ByVal pv_sTableName As String, ByVal pv_sFieldName As String, ByVal pv_objValue As Object, Optional ByVal pv_bIsDigit As Boolean = True) As Boolean
        Try
            Dim fv_DA As New SqlDataAdapter("SELECT * FROM " & pv_sTableName & " WHERE " & pv_sFieldName & sGetCondition(pv_objValue, pv_bIsDigit), gv_oSqlCnn)
            Dim fv_DS As New DataSet
            fv_DA.Fill(fv_DS, pv_sTableName)
            If fv_DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrMsg(ex.Message)
            Return False
        End Try
    End Function

    Public Function bIsExisted(ByVal pv_sTableName As String, ByVal sCondition As String) As Boolean
        Try
            Dim fv_DA As New SqlDataAdapter("SELECT 1 FROM " & pv_sTableName & " WHERE " & sCondition, gv_oSqlCnn)
            Dim fv_DS As New DataSet
            fv_DA.Fill(fv_DS, pv_sTableName)
            If fv_DS.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            ShowErrMsg(ex.Message)
            Return False
        End Try
    End Function

    Public Function sGetCondition(ByVal pv_objValue As Object, ByVal pv_bIsDigit As Boolean) As String
        If pv_bIsDigit Then
            Return " =" & CInt(pv_objValue)
        Else
            Return " =N'" & CStr(pv_objValue) & "'"
        End If
    End Function

    Public Function CompareDate(ByVal pv_sDate1 As String, ByVal pv_sDate2 As String) As Boolean
        If Date.Parse(pv_sDate2, New Globalization.CultureInfo("vi-VN")) >= Date.Parse(pv_sDate1, New Globalization.CultureInfo("vi-VN")) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ConvertDate(ByVal pv_sDate As String) As Date
        Return DateTime.ParseExact(pv_sDate, "dd/MM/yyyy", New Globalization.CultureInfo("vi-VN", False))

    End Function

    Public Sub WaitNow(ByVal _me As Windows.Forms.Form)
        _me.Cursor = Cursors.WaitCursor
    End Sub

    Public Sub DefaultNow(ByVal _me As Windows.Forms.Form)
        _me.Cursor = Cursors.Default
    End Sub

    Public Function GetPID(Optional ByVal sOrder As String = "") As String
        Return Now.Year.ToString & VB6.Right("0" & Now.Month.ToString, 2) & VB6.Right("0" & Now.Day.ToString, 2) & VB6.Right("0" & Now.Hour.ToString, 2) & VB6.Right("0" & Now.Minute.ToString, 2) & VB6.Right("0" & Now.Second.ToString, 2) & sOrder
    End Function

    Public Function GetYYYYMMDD(ByVal tDate As Date) As String
        Return tDate.Year.ToString & VB6.Right("0" & tDate.Month.ToString, 2) & VB6.Right("0" & tDate.Day.ToString, 2)
    End Function

    Public Function GetYYMMDD(ByVal tDate As Date) As String
        Return VB6.Right(tDate.Year.ToString, 2) & VB6.Right("0" & tDate.Month.ToString, 2) & VB6.Right("0" & tDate.Day.ToString, 2)
    End Function
    
    Private Sub LoopControl(ByVal mainForm As Control, ByVal dt1 As DataTable, ByVal objCtr As Control, ByVal pv_sLang As String)
        For Each ctr As Control In objCtr.Controls
            If TypeOf (ctr) Is Label Or TypeOf (ctr) Is Button Or TypeOf (ctr) Is GroupBox Or TypeOf (ctr) Is CheckBox Or TypeOf (ctr) Is RadioButton Or TypeOf (ctr) Is Panel Or TypeOf (ctr) Is ComboBox Then
                Dim dr As DataRow()
                dr = dt1.Select("sFormName='" & mainForm.Name & "' AND sControlName='" & ctr.Name & "'")
                If dr.GetLength(0) > 0 Then
                    If pv_sLang.ToUpper = "VN" Then
                        If TypeOf (ctr) Is ComboBox Then
                            Dim cbo As ComboBox
                            Dim splt As String() = Split(sDBnull(dr(0)("sVn")), ",")
                            cbo = CType(ctr, ComboBox)
                            cbo.Items.Clear()
                            For i As Integer = 0 To splt.GetLength(0) - 1
                                cbo.Items.Add(splt(i))
                            Next
                        Else
                            ctr.Text = sDBnull(dr(0)("sVn"))
                        End If
                    Else
                        If TypeOf (ctr) Is ComboBox Then
                            Dim cbo As ComboBox
                            Dim splt As String() = Split(sDBnull(dr(0)("sEn")), ",")
                            cbo = CType(ctr, ComboBox)
                            cbo.Items.Clear()
                            For i As Integer = 0 To splt.GetLength(0) - 1
                                cbo.Items.Add(splt(i))
                            Next
                        Else
                            ctr.Text = sDBnull(dr(0)("sEn"))
                        End If
                    End If
                Else
                End If
                LoopControl(mainForm, dt1, ctr, pv_sLang)
            ElseIf TypeOf (ctr) Is DataGridView Then
                objGetCurrentDataGridView(CType(mainForm, Form), ctr.Name) ' CType(ctr, DataGridView)
                For Each col As DataGridViewColumn In CurrDtGridView.Columns
                    Dim dr As DataRow()
                    dr = dt1.Select("sFormName='" & mainForm.Name & "' AND sControlName='" & col.Name & "'")
                    If dr.GetLength(0) > 0 Then
                        If pv_sLang.ToUpper = "VN" Then
                            col.HeaderText = sDBnull(dr(0)("sVn"))
                        Else
                            col.HeaderText = sDBnull(dr(0)("sEn"))
                        End If
                    Else
                    End If
                Next
                LoopControl(mainForm, dt1, ctr, pv_sLang)
            End If
        Next
    End Sub

    Public Function GenerateBarCode(ByVal barcode As Barcode) As Byte()
        Dim ms As New MemoryStream()

        Dim bacodeWidth As Integer = 170
        Dim barcodeHeight As Integer = 85
        ' barcode.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp)

        barcode.Image(bacodeWidth, barcodeHeight).Save(ms, System.Drawing.Imaging.ImageFormat.Bmp)
        Dim data As Byte() = New Byte(ms.Length - 1) {}
        ms.Position = 0
        ms.Read(data, 0, CInt(ms.Length))
        Return data
    End Function

    Private Sub objGetCurrentDataGridView(ByVal objMain As Control, ByVal pv_sName As String)
        For Each ctr As Control In objMain.Controls
            If ctr.Name = pv_sName Then
                CurrDtGridView = CType(ctr, DataGridView)
            Else
                objGetCurrentDataGridView(ctr, pv_sName)
            End If
        Next
    End Sub

    Public Function findFirstCharacter(ByVal yymmdd As String) As String
        Dim da As New SqlDataAdapter("select max(Left(RegNo+'0',1)) from TBL_REGSERVICE where RIGHT('000'+REGNO,3)<='999' AND RIGHT(LEFT(REGNO+'0000000',7),6)='" & yymmdd & "'", gv_oSqlCnn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)(0)) Then
                    Return "A"
                End If
                If InStr("ABCDEFGHIJKNMLOPQSRTVWZX", sDBnull(dt.Rows(0)(0)), CompareMethod.Text) > 0 Then
                    Return CharacterFound(sDBnull(dt.Rows(0)(0)), sDBnull(dt.Rows(0)(0)) & yymmdd)
                Else
                    Return "A"
                End If
            Else
                Return "A"
            End If
        Catch ex As Exception
            ShowErrMsg(ex.Message)
            Return "A"
        End Try
    End Function

    Private Function CharacterFound(ByVal C As String, ByVal CYYMMDD As String) As String
        Dim da As New SqlDataAdapter("select 1 from TBL_REGSERVICE where RIGHT('000'+REGNO,3)='999' AND LEFT(REGNO+'0000000',7)='" & CYYMMDD & "'", gv_oSqlCnn)
        Dim dt As New DataTable
        Try
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                Dim arrCharacter() As String = "A,B,C,D,E,F,G,H,I,J,K,N,M,L,O,P,Q,S,R,T,V,W,X,Y,Z".Split(",")
                Dim Index As Integer = 0
                For i As Integer = 0 To arrCharacter.GetLength(0) - 1
                    If arrCharacter(i) = C Then
                        Index = i
                        Exit For
                    End If
                Next
                If Index < arrCharacter.GetLength(0) - 1 Then
                    Return arrCharacter(Index + 1)
                Else
                    ShowMsg("Hệ thống sinh mã đã đạt tới giới hạn trong ngày Z_YYMMDD_999" & vbCrLf & "Bạn cần phải xem lại hệ thống trước khi tiếp tục sử dụng chương trình.")
                    Return "-A"
                End If
            Else
                Return C
            End If
        Catch ex As Exception
            ShowErrMsg(ex.Message)
            Return C
        End Try
    End Function

    Public Function GetFieldValue(ByVal pv_sTableName As String, ByVal pv_sFieldName As String, ByVal pv_sCondition As String) As Object
        Dim fv_sSql As String = ""
        fv_sSql = "SELECT " & pv_sFieldName & " FROM " & pv_sTableName & " WHERE " & pv_sCondition
        Dim DA As New SqlDataAdapter(fv_sSql, gv_oSqlCnn)
        Dim DT As New DataTable
        Try
            DA.Fill(DT)
            If DT.Rows.Count > 0 Then
                Return DT.Rows(0)(0)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ShowErrMsg(ex.Message)
            Return Nothing
        End Try

    End Function

    Public Function ArrGetFieldValue(ByVal pv_sTableName As String, ByVal pv_sFieldName As String, ByVal pv_sCondition As String) As DataRow()
        Dim fv_sSql As String = ""
        fv_sSql = "SELECT " & pv_sFieldName & " FROM " & pv_sTableName & " WHERE " & pv_sCondition
        Dim DA As New SqlDataAdapter(fv_sSql, gv_oSqlCnn)
        Dim DT As New DataTable
        Try
            DA.Fill(DT)
            If DT.Rows.Count > 0 Then
                Return DT.Select("1=1")
            Else
                Return Nothing
            End If
        Catch ex As Exception
            ShowErrMsg(ex.Message)
            Return Nothing
        End Try

    End Function

    Public Sub ResetProgressBar(ByVal prgb As ProgressBar, ByVal Max As Integer, ByVal visi As Boolean)
        prgb.Step = 1
        prgb.Visible = visi
        prgb.Maximum = Max
        prgb.Minimum = 0
        prgb.Value = 0
    End Sub

    Public Function GetReportType() As Integer
        Return lablinkhelper.Utilities.GetReportType
    End Function
    Public Function GetDigibarcode() As Integer
        Return lablinkhelper.Utilities.BarcodeDigit
    End Function
    Public Function JCLV() As Boolean
        Try
            If IO.File.Exists(Application.StartupPath & "\JCLV.txt") Then Return True
            Return False
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function TranslateSex(ByVal Sex As String) As String
        If Sex.ToUpper = "M" Then
            Return "Nam"
        Else
            Return "Nữ"
        End If
    End Function

    Public Function GetReportDocument(ByVal intType As Integer) As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim ReportType As Integer
        ReportType = GetReportType()
        Select Case ReportType

            Case 0 'Thai Ha
                Select Case intType
                    Case 0
                        Dim crpt As New HN_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New HN_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New HN_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 1 'Viet Duc
                Select Case intType
                    Case 0
                        Dim crpt As New VD_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New VD_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New VD_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 2 'Ky Dong
                Select Case intType
                    Case 0
                        Dim crpt As New HP_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New HP_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New HP_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 3 'A5
                Select Case intType
                    Case 0
                        Dim crpt As New A5_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New A5_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New A5_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 4 'JCLV
                Select Case intType
                    Case 0
                        Dim crpt As New JCLV_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New JCLV_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New JCLV_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 7 'NIITD
                Select Case intType
                    Case 0
                        Dim crpt As New NIITD_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New NIITD_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New NIITD_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 8 'Xay Dung
                Select Case intType
                    Case 0
                        Dim crpt As New XayDung_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New XayDung_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New XayDung_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 10 'VienTimHN
                Select Case intType
                    Case 0
                        Dim crpt As New VienTimHN_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New VienTimHN_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New VienTimHN_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 11 'HongNgoc
                Select Case intType
                    Case 0
                        Dim crpt As New HongNgoc_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New HongNgoc_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New HongNgoc_crpt_DailyParamTestReport2
                        Return crpt
                End Select
            Case 12 ' Việt Đức 1C
                Select Case intType
                    Case 0
                        Dim crpt As New VD_1C_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New VD_1C_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New VD_1C_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case 13 ' Vabiotech
                Select Case intType
                    Case 0
                        Dim crpt As New Vabiotech_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New Vabiotech_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New Vabiotech_crpt_DailyParamTestReport2
                        Return crpt
                End Select

            Case Else ' Thai Ha
                Select Case intType
                    Case 0
                        Dim crpt As New HN_crpt_DailyParamTestReport
                        Return crpt
                    Case 1
                        Dim crpt As New HN_crpt_DailyParamTestReport1
                        Return crpt
                    Case 2
                        Dim crpt As New HN_crpt_DailyParamTestReport2
                        Return crpt
                End Select
        End Select
    End Function

    Public Function GetReportDocument(Optional ByVal All As Boolean = False) As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim ReportType As Integer
        ReportType = GetReportType()
        Select Case All
            Case True
                Select Case ReportType
                    Case 0 ' Thai Ha
                        Dim crpt As New HN_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 1 'Viet Duc
                        Dim crpt As New VD_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 2 ' Ky Dong
                        Dim crpt As New HP_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 3 ' A5
                        Dim crpt As New A5_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 4 'JCLV
                        Dim crpt As New JCLV_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 7 'NIITD
                        Dim crpt As New NIITD_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 8 'XayDung
                        Dim crpt As New XayDung_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 10 'VienTimHn
                        Dim crpt As New VienTimHN_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 11 'HongNgoc
                        Dim crpt As New HongNgoc_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 12 'Việt Đức 1C
                        Dim crpt As New new_VD_1C_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 13 'Vabiotech
                        Dim crpt As New Vabiotech_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 14 'Lão Khoa
                        Dim crpt As New LAOKHOA_crpt_DetailTestReport_ALL1
                        Return crpt
                    Case 15 'KHOA HH NHTD
                        Dim crpt As New NHTDHH_crpt_DetailTestReport_ALL
                        Return crpt
                    Case 16 'Dùng chung cho các account khác
                        Dim crpt As New crpt_GeneralDetailTestReport_ALL
                        Return crpt
                    Case Else
                        Dim crpt As New VD_crpt_DetailTestReport_ALL
                        Return crpt
                End Select
            Case False
                Select Case ReportType
                    Case 0 ' Thai Ha
                        Dim crpt As New HN_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 1 'Viet Duc
                        Dim crpt As New VD_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 2 ' Ky Dong
                        Dim crpt As New HP_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 3 ' A5
                        Dim crpt As New A5_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 4 'JCLV
                        Dim crpt As New JCLV_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 7 'NIITD
                        Dim crpt As New NIITD_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 8 'XayDung
                        Select Case gv_TestTypeDetailReport
                            Case 1 'Huyết học
                                Dim crpt As New XayDung_crpt_DetailTestReport_TESTTYPE_HuyetHoc
                                Return crpt
                            Case 5 'Sinh hóa
                                Dim crpt As New XayDung_crpt_DetailTestReport_TESTTYPE_SinhHoa
                                Return crpt
                            Case Else
                                Dim crpt As New XayDung_crpt_DetailTestReport_TESTTYPE
                                Return crpt
                        End Select
                    Case 9 'US1000
                        Dim crpt As New US1000_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 10 'VienTimHN
                        Dim crpt As New VienTimHN_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 11 'HongNgoc
                        Dim crpt As New HongNgoc_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 12 'Việt Đức 1C
                        Dim crpt As New new_VD_1C_crpt_DetailTestReport_TESTTYPE
                        Return crpt

                    Case 13 'Vabiotech
                        Dim crpt As New Vabiotech_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case 14 'Lão Khoa
                        Dim crpt As New LAOKHOA_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                    Case Else
                        Dim crpt As New VD_crpt_DetailTestReport_TESTTYPE
                        Return crpt
                End Select
        End Select

    End Function

    Public Function GetReportDocumentPrintAll() As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim ReportType As Integer
        ReportType = GetReportType()
        Select Case ReportType
            Case 8 'XayDung
                Select Case gv_TestTypeDetailReport
                    Case 1 'Huyết học
                        Dim crpt As New XayDung_crpt_DetailTestReport_ALLFromDateToDateAllPatient_HuyetHoc
                        Return crpt
                    Case 5 'Sinh hóa
                        Dim crpt As New XayDung_crpt_DetailTestReport_ALLFromDateToDateAllPatient_SinhHoa
                        Return crpt
                    Case Else
                        Dim crpt As New XayDung_crpt_DetailTestReport_ALLFromDateToDateAllPatient
                        Return crpt
                End Select
            Case Else
                Dim crpt As New XayDung_crpt_DetailTestReport_ALLFromDateToDateAllPatient
                Return crpt
        End Select
    End Function

    Public Function intGetCoboVal(ByVal cbo As ComboBox) As Integer
        If cbo.Items.Count <= 0 Then Return -1
        If cbo.Text.Trim.ToUpper = "Tất cả".ToUpper Then
            Return (-1)
        Else
            Return intDBnull(cbo.Text.Split("-")(0))
        End If
    End Function

    Public Function ParaName() As String
        Try
            If Not IO.File.Exists(Application.StartupPath & "\ParaName.txt") Then Return ""
            Dim strRd As New IO.StreamReader(Application.StartupPath & "\ParaName.txt")
            Dim s As String = strRd.ReadToEnd
            Return s
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Function SetLocalDateFormat(ByVal strdate As String) As Boolean
        Try

            Dim rkey As Microsoft.Win32.RegistryKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Control Panel\International", True)
            rkey.SetValue("sShortDate", strdate)
            rkey.SetValue("sLongDate", strdate)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Module
