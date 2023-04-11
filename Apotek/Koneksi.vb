Imports MySql.Data.MySqlClient

Module Koneksi
    Dim strkoneksi As String = "server=localhost;username=root;password=;database=apotek"
    Dim dataread As MySqlDataReader
    Dim command As MySqlCommand

    Public Function config()
        Return New MySqlConnection(strkoneksi)
    End Function

    Public Function SendOnly(ByVal query As String, showDialog As Boolean, Optional act As String = "Nice", Optional confirm As Boolean = False)
        Using koneksi As MySqlConnection = config()
            koneksi.Open()
            If confirm Then
                Dim result = MessageBox.Show("Yakin?", "Confirm", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    Try
                        command = New MySqlCommand(query, koneksi)
                        command.ExecuteNonQuery()
                        If showDialog Then
                            MsgBox("Operation Success: " & act)
                        End If
                    Catch ee As Exception
                        If showDialog Then
                            MsgBox("Operation Failed: " & act & "\n Exception: \n " & ee.Message.ToString())
                        End If
                    End Try
                End If
            Else

                Try
                    command = New MySqlCommand(query, koneksi)
                    command.ExecuteNonQuery()
                    If showDialog Then
                        MsgBox("Operation Success: " & act)
                    End If
                Catch ee As Exception
                    If showDialog Then
                        MsgBox("Operation Failed, Exception: \n " & ee.Message.ToString())
                    End If
                End Try
            End If
            koneksi.Close()
        End Using
    End Function

    Public Function Read(ByVal query As String, showDialog As Boolean, Optional act As String = "Nice")
        Using koneksi As MySqlConnection = config()
            koneksi.Open()
            Try
                command = New MySqlCommand(query, koneksi)
                dataread = command.ExecuteReader()
                If showDialog Then
                    MsgBox("Operation Success: " & act)
                End If
            Catch ex As Exception
                If showDialog Then
                    MsgBox("Operation Failed, Exception: \n " & ex.Message.ToString())
                End If
            End Try
            Dim datable As New DataTable
            datable.Load(dataread)
            dataread.Close()
            koneksi.Close()
            Return datable
        End Using
    End Function
End Module
