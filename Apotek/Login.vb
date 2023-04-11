Public Class Login

    Dim dt As DataTable
    Dim datetime As String = Date.Now.ToString("yyyy-MM-dd HH:mm:ss")
    Public idUser As String

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        dt = Read("Select * from user where username='" & UsernameTextBox.Text & "' and password='" & PasswordTextBox.Text & "'", False)
        If dt.Rows.Count = 1 Then
            If dt(0).Item("tipe_user") = "Admin" Then
                idUser = dt(0).Item("id_user")
                Admin.Show()
                Me.Hide()
                MsgBox("Welcome Admin")
            ElseIf dt(0).Item("tipe_user") = "Apoteker" Then
                idUser = dt(0).Item("id_user")
                Apoteker.Show()
                Me.Hide()
                MsgBox("Welcome Apoteker")
            ElseIf dt(0).Item("tipe_user") = "Kasir" Then
                idUser = dt(0).Item("id_user")
                Kasir.Show()
                Me.Hide()
                MsgBox("Welcome Kasir")
            ElseIf dt(0).Item("tipe_user") = "Dokter" Then
                MsgBox("Bukan untuk dokter!!")
            End If
            SendOnly("Insert into log (aktifitas, id_user) values('Login', '" & dt(0).Item("id_user").ToString() & "')", True)
        Else
            MsgBox("Username atau Password salah!!")
        End If

    End Sub

    Private Sub UsernameLabel_Click(sender As Object, e As EventArgs) Handles UsernameLabel.Click

    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 500
        Timer1.Enabled = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        UsernameTextBox.Text = ""
        PasswordTextBox.Text = ""
        SendOnly("Insert into log (aktifitas) values('Gagal login')", True)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If UsernameTextBox.Text = "" Or PasswordTextBox.Text = "" Then
            OK.Enabled = False
        Else
            OK.Enabled = True
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

    End Sub
End Class
