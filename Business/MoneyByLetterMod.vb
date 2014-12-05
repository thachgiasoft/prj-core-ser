Module MoneyByLetterMod

    Public Function sMoneyToLetter(ByVal strNumber As String) As String
        Dim chu As String, lng As Integer, CD As String, dd As Integer
        Dim i As Integer
        Dim TP As String
        Dim bolCheck As Boolean
        Dim BolSoAm As Boolean = False
        If Left(strNumber, 1) = "-" Then  ' số âm
            BolSoAm = True
            strNumber = strNumber.Substring(1)
        End If
        bolCheck = False
        CD = ""
        TP = ""
        If strNumber = "" Then
            sMoneyToLetter = ""
            Exit Function
        End If
        'If Trim(Str$(strNumber)) <> "" Then
        If Trim(strNumber) <> "" Then
            For i = 0 To Len(strNumber)
                If Right(Left(strNumber, i), 1) = "." Then
                    bolCheck = True
                    CD = Left(strNumber, i - 1)
                    TP = Right(strNumber, Len(strNumber) - i)
                    TP = TP + "0"
                    TP = Left(TP, 2)
                    Exit For
                End If
            Next
            If bolCheck = False Then
                '                CD = Trim(Str$(strNumber))
                CD = Trim(strNumber)
            End If
            lng = Len(CD)
            Select Case lng
                Case 0 : chu = " "
                Case 1 : chu = DonVi(CD)
                Case 2 : chu = Chuc(CD)
                Case 3 : chu = Tram(CD)
                Case 4, 5, 6 : chu = Nghin(CD)
                Case 7, 8, 9 : chu = Trieu(CD)
                Case 10, 11, 12, 13, 14, 15, 16 : chu = Ti(CD)
            End Select

            dd = Len(Trim(chu))
            sMoneyToLetter = UCase$(Mid$(Trim(chu), 1, 1)) + Mid$(Trim(chu), 2, dd - 1) + " đồng"
            'MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
            sMoneyToLetter = Replace(sMoneyToLetter, " không trăm  nghìn", "")
            sMoneyToLetter = Replace(sMoneyToLetter, " không trăm  triệu", "")
            sMoneyToLetter = Replace(sMoneyToLetter, "  ", " ")
            If TP <> "" Then
                i = CInt(TP)
                lng = Len(TP)
                If lng > 0 Then
                    Select Case lng
                        Case 1 : chu = DonVi(TP)
                        Case 2 : chu = ChucHao(TP)
                    End Select
                    dd = Len(Trim(chu))
                    sMoneyToLetter = sMoneyToLetter + " " + LCase$(Mid$(Trim(chu), 1, 1)) + Mid$(Trim(chu), 2, dd - 1) + " xu"
                End If
            End If
        End If
        If BolSoAm Then sMoneyToLetter = "Âm " & sMoneyToLetter
    End Function
    Public Function MoneyByLetterShort(ByVal strNumber As String, ByVal muc As Integer) As String
        Dim chu As String, lng As Integer, CD As String, dd As Integer
        Dim i As Integer
        Dim TP As String
        Dim bolCheck As Boolean
        Dim BolSoAm As Boolean = False
        If Left(strNumber, 1) = "-" Then  ' số âm
            BolSoAm = True
            strNumber = strNumber.Substring(1)
        End If
        bolCheck = False
        CD = ""
        TP = ""
        If strNumber = "" Then
            MoneyByLetterShort = ""
            Exit Function
        End If
        'If Trim(Str$(strNumber)) <> "" Then
        If Trim(strNumber) <> "" Then
            For i = 0 To Len(strNumber)
                If Right(Left(strNumber, i), 1) = "." Then
                    bolCheck = True
                    CD = Left(strNumber, i - 1)
                    TP = Right(strNumber, Len(strNumber) - i)
                    TP = TP + "0"
                    TP = Left(TP, 2)
                    Exit For
                End If
            Next
            If bolCheck = False Then
                '                CD = Trim(Str$(strNumber))
                CD = Trim(strNumber)
            End If
            lng = Len(CD)
            Select Case lng
                Case 0 : chu = " "
                Case 1 : chu = DonVi(CD)
                Case 2 : chu = ChucShort(CD)
                Case 3 : chu = TramShort(CD, muc)
                Case 4, 5, 6 : chu = NghinShort(CD, muc)
                Case 7, 8, 9 : chu = TrieuShort(CD, muc)
                Case 10, 11, 12 : chu = TiShort(CD, muc)
            End Select

            dd = Len(Trim(chu))
            MoneyByLetterShort = UCase$(Mid$(Trim(chu), 1, 1)) + Mid$(Trim(chu), 2, dd - 1) + " đồng"
            'MoneyByLetter = Replace(MoneyByLetter, " không trăm  ngh×n", "")
            MoneyByLetterShort = Replace(MoneyByLetterShort, "  ", " ")
            MoneyByLetterShort = Replace(MoneyByLetterShort, " không trăm nghìn", "")
            MoneyByLetterShort = Replace(MoneyByLetterShort, " không trăm triệu", "")
            'MoneyByLetterShort = Replace(MoneyByLetterShort, " mươi", "")
            'If muc = 2 Then
            '    MoneyByLetterShort = Replace(MoneyByLetterShort, " trăm", "")
            'End If
            If TP <> "" Then
                i = CInt(TP)
                lng = Len(TP)
                If lng > 0 Then
                    Select Case lng
                        Case 1 : chu = DonVi(TP)
                        Case 2 : chu = ChucHaoShort(TP)
                    End Select
                    dd = Len(Trim(chu))
                    MoneyByLetterShort = MoneyByLetterShort + " " + LCase$(Mid$(Trim(chu), 1, 1)) + Mid$(Trim(chu), 2, dd - 1) + " xu"
                End If
            End If
        End If
        'If BolSoAm Then MoneyByLetter = "Âm " & MoneyByLetter
    End Function
    Private Function ChucHao(ByVal d As String) As String
        Dim tmp As String
        If Len(d) <> 2 Then
            tmp = "Error"
        Else : Select Case Mid$(d, 1, 1)
                Case "0"
                    tmp = " " + DonVi(Mid$(d, 2, 1))
                Case "1"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = "mười "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = "lăm "
                    Else : tmp = "mười " + DonVi(Mid$(d, 2, 1))
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi "
                    ElseIf Mid$(d, 2, 1) = "1" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi mốt "
                    ElseIf Mid$(d, 2, 1) = "4" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi tư "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi lăm "
                    Else : tmp = DonVi(Mid$(d, 1, 1)) + "mươi " + DonVi(Mid$(d, 2, 1))
                    End If
            End Select
        End If
        ChucHao = tmp
    End Function
    Private Function ChucHaoShort(ByVal d As String) As String
        Dim tmp As String
        If Len(d) <> 2 Then
            tmp = "Error"
        Else : Select Case Mid$(d, 1, 1)
                Case "0"
                    tmp = " " + DonVi(Mid$(d, 2, 1))
                Case "1"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = "mười "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = "lăm "
                    Else : tmp = "mười " + DonVi(Mid$(d, 2, 1))
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi "
                    ElseIf Mid$(d, 2, 1) = "1" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mốt "
                    ElseIf Mid$(d, 2, 1) = "4" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "tư "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "lăm "
                    Else : tmp = DonVi(Mid$(d, 1, 1)) + DonVi(Mid$(d, 2, 1))
                    End If
            End Select
        End If
        ChucHaoShort = tmp
    End Function
    Private Function DonVi(ByVal d As String) As String
        Dim tmp As Integer
        tmp = Len(d)
        If tmp = 0 Then
            DonVi = " "
        Else : Select Case Mid$(d, 1, 1)
                Case "0" : DonVi = "không "
                Case "1" : DonVi = "một "
                Case "2" : DonVi = "hai "
                Case "3" : DonVi = "ba "
                Case "4" : DonVi = "bốn "
                Case "5" : DonVi = "năm "
                Case "6" : DonVi = "sáu "
                Case "7" : DonVi = "bảy "
                Case "8" : DonVi = "tám "
                Case "9" : DonVi = "chín "
            End Select
        End If
    End Function

    Private Function Chuc(ByVal d As String) As String
        Dim tmp As String
        If Len(d) <> 2 Then
            tmp = "Error"
        Else : Select Case Mid$(d, 1, 1)
                Case "1"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = "mười "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = "mười lăm "
                    Else : tmp = "mười " + DonVi(Mid$(d, 2, 1))
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi "
                    ElseIf Mid$(d, 2, 1) = "1" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi mốt "
                    ElseIf Mid$(d, 2, 1) = "4" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi tư "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi lăm "
                    Else : tmp = DonVi(Mid$(d, 1, 1)) + "mươi " + DonVi(Mid$(d, 2, 1))
                    End If
            End Select
        End If
        Chuc = tmp
    End Function
    Private Function ChucShort(ByVal d As String) As String
        Dim tmp As String
        If Len(d) <> 2 Then
            tmp = "Error"
        Else : Select Case Mid$(d, 1, 1)
                Case "1"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = "mười "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = "mười lăm "
                    Else : tmp = "mười " + DonVi(Mid$(d, 2, 1))
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    If Mid$(d, 2, 1) = "0" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mươi "
                    ElseIf Mid$(d, 2, 1) = "1" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "mốt "
                    ElseIf Mid$(d, 2, 1) = "4" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "tư "
                    ElseIf Mid$(d, 2, 1) = "5" Then
                        tmp = DonVi(Mid$(d, 1, 1)) + "lăm "
                    Else : tmp = DonVi(Mid$(d, 1, 1)) + DonVi(Mid$(d, 2, 1))
                    End If
            End Select
        End If
        ChucShort = tmp
    End Function
    Private Function Tram(ByVal d As String) As String
        Dim tmp As String, d1 As String, d2 As String, d3 As String
        Dim Temp As String

        d1 = Mid$(d, 1, 1) : d2 = Mid$(d, 2, 1) : d3 = Mid$(d, 3, 1)
        If Len(d) <> 3 Then
            Temp = "Error"
        Else
            Select Case d2
                Case "0"
                    If d3 = "0" Then
                        tmp = DonVi(d1) + "trăm  "
                    ElseIf d3 = "1" Then
                        tmp = DonVi(d1) + "trăm linh một "
                    ElseIf d3 = "4" Then
                        tmp = DonVi(d1) + "trăm linh tư "
                    ElseIf d3 = "5" Then
                        tmp = DonVi(d1) + "trăm linh năm "
                    Else
                        tmp = DonVi(d1) + "trăm linh " + DonVi(d3)
                    End If
                Case "1"
                    If d3 = "0" Then
                        tmp = DonVi(d1) + "trăm mười "
                    Else
                        tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                    End If
                Case "2", "3", "4", "5", "6", "7", "8", "9"
                    ' tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                    tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
            End Select
            Tram = tmp
        End If
    End Function
    Private Function TramShort(ByVal d As String, ByVal muc As Integer) As String
        Dim tmp As String, d1 As String, d2 As String, d3 As String
        Dim Temp As String
        If muc = 2 Then
            d1 = Mid$(d, 1, 1) : d2 = Mid$(d, 2, 1) : d3 = Mid$(d, 3, 1)
            If Len(d) <> 3 Then
                Temp = "Error"
            Else
                Select Case d2
                    Case "0"
                        If d3 = "0" Then
                            tmp = DonVi(d1) + "trăm  "
                        ElseIf d3 = "1" Then
                            tmp = DonVi(d1) + "linh một "
                        ElseIf d3 = "4" Then
                            tmp = DonVi(d1) + "linh tư "
                        ElseIf d3 = "5" Then
                            tmp = DonVi(d1) + "linh năm "
                        Else
                            tmp = DonVi(d1) + "linh " + DonVi(d3)
                        End If
                    Case "1"
                        If d3 = "0" Then
                            tmp = DonVi(d1) + "trăm mười "
                        Else
                            tmp = DonVi(d1) + ChucShort(d2 + d3)
                        End If
                    Case "2", "3", "4", "5", "6", "7", "8", "9"
                        ' tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                        tmp = DonVi(d1) + ChucShort(d2 + d3)
                End Select
                TramShort = tmp
            End If
        Else
            d1 = Mid$(d, 1, 1) : d2 = Mid$(d, 2, 1) : d3 = Mid$(d, 3, 1)
            If Len(d) <> 3 Then
                Temp = "Error"
            Else
                Select Case d2
                    Case "0"
                        If d3 = "0" Then
                            tmp = DonVi(d1) + "trăm  "
                        ElseIf d3 = "1" Then
                            tmp = DonVi(d1) + "trăm linh một "
                        ElseIf d3 = "4" Then
                            tmp = DonVi(d1) + "trăm linh tư "
                        ElseIf d3 = "5" Then
                            tmp = DonVi(d1) + "trăm linh năm "
                        Else
                            tmp = DonVi(d1) + "trăm linh " + DonVi(d3)
                        End If
                    Case "1"
                        If d3 = "0" Then
                            tmp = DonVi(d1) + "trăm mười "
                        Else
                            tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                        End If
                    Case "2", "3", "4", "5", "6", "7", "8", "9"
                        ' tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                        tmp = DonVi(d1) + "trăm " + Chuc(d2 + d3)
                End Select
                TramShort = tmp
            End If
        End If

    End Function
    Private Function Nghin(ByVal d As String) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 3
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 3)
        If s2 = "000" Then
            tmp1 = "nghìn "
        Else
            tmp1 = "nghìn " + Tram(s2)
        End If
        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = Chuc(s1) + tmp1
            Case 3
                tmp = Tram(s1) + tmp1
        End Select
        Nghin = tmp
    End Function
    Private Function NghinShort(ByVal d As String, ByVal muc As Integer) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 3
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 3)
        If s2 = "000" Then
            tmp1 = "nghìn "
        Else
            tmp1 = "nghìn " + TramShort(s2, muc)
        End If
        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = ChucShort(s1) + tmp1
            Case 3
                tmp = TramShort(s1, muc) + tmp1
        End Select
        NghinShort = tmp
    End Function
    Private Function Trieu(ByVal d As String) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 6
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 6)
        If s2 = "000000" Then
            tmp1 = "triệu "
        Else
            tmp1 = "triệu " + Nghin(s2)
        End If
        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = Chuc(s1) + tmp1
            Case 3
                tmp = Tram(s1) + tmp1
        End Select
        Trieu = tmp
    End Function
    Private Function TrieuShort(ByVal d As String, ByVal muc As Integer) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 6
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 6)
        If s2 = "000000" Then
            tmp1 = "triệu "
        Else
            tmp1 = "triệu " + NghinShort(s2, muc)
        End If
        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = ChucShort(s1) + tmp1
            Case 3
                tmp = TramShort(s1, muc) + tmp1
        End Select
        TrieuShort = tmp
    End Function
    Private Function Ti(ByVal d As String) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 9
        s1 = Mid$(d, 1, ln)
        s2 = Mid$(d, ln + 1, 9)

        If s2 = "000000000" Then
            tmp1 = "tỷ "
        Else
            tmp1 = "tỷ " + Trieu(s2)
        End If

        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = Chuc(s1) + tmp1
            Case 3
                tmp = Tram(s1) + tmp1
            Case 4
                tmp = Nghin(s1) + tmp1
            Case 5
                tmp = "(Số tiền quá lớn. Bạn cần xem lại đơn hàng)"
        End Select
        Ti = tmp
    End Function
    Private Function TiShort(ByVal d As String, ByVal muc As Integer) As String
        Dim tmp As String, s1 As String, s2 As String, tmp1 As String
        Dim dai As Integer, ln As Integer
        dai = Len(d)
        ln = dai - 9
        s1 = Mid$(d, 1, ln) : s2 = Mid$(d, ln + 1, 9)

        If s2 = "000000000" Then
            tmp1 = "tỷ "
        Else
            tmp1 = "tỷ " + TrieuShort(s2, muc)
        End If

        Select Case Len(s1)
            Case 1
                tmp = DonVi(s1) + tmp1
            Case 2
                tmp = ChucShort(s1) + tmp1
            Case 3
                tmp = TramShort(s1, muc) + tmp1
            Case 4
                tmp = TrieuShort(s1, muc) + tmp1
        End Select
        TiShort = tmp
    End Function
End Module
