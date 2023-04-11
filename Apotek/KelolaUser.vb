Public Class KelolaUser
    Dim dt As DataTable
    Dim id As String
    Private Sub KelolaObatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KelolaObatToolStripMenuItem.Click
        KelolaObat.Show()
        Me.Hide()
    End Sub

    Private Sub LaporanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanToolStripMenuItem.Click
        KelolaLaporan.Show()
        Me.Hide()
    End Sub

    Private Sub AdminNavigationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminNavigationToolStripMenuItem.Click
        Admin.Show()
        Me.Hide()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

    Private Sub KelolaUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 500
        load_tb()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SendOnly("Insert into user values('', '" & ComboBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox4.Text & "', '" & TextBox3.Text & "', '" & TextBox5.Text & "', '" & TextBox6.Text & "' )", True, "User berhasil ditambahkan!!", True)
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        load_tb()
        id = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SendOnly("Delete from user where id_user='" & id & "'", True, "Hapus user Berhasil", True)
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        id = ""
        load_tb()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim tabel As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                ComboBox1.SelectedItem = tabel.Cells(1).Value
                id = tabel.Cells(0).Value
                TextBox2.Text = tabel.Cells(2).Value
                TextBox3.Text = tabel.Cells(4).Value
                TextBox4.Text = tabel.Cells(3).Value
                TextBox5.Text = tabel.Cells(5).Value
                TextBox6.Text = tabel.Cells(6).Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SendOnly("Update user set tipe_user='" & ComboBox1.Text & "',
                    nama_user='" & TextBox2.Text & "',
                    telpon='" & TextBox3.Text & "',
                    alamat='" & TextBox4.Text & "',
                    username='" & TextBox5.Text & "',
                    password='" & TextBox6.Text & "' where id_user='" & id & "'", True, "Edit data user berhasil!!", True)
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        id = ""
        load_tb()
    End Sub

    Private Sub TextBox7_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyUp
        dt = Read("Select * from user where id_user like '%" & TextBox7.Text & "%' or
                tipe_user like '%" & TextBox7.Text & "%' or
                nama_user like '%" & TextBox7.Text & "%' or
                alamat like '%" & TextBox7.Text & "%' or
                telpon like '%" & TextBox7.Text & "%' or
                username like '%" & TextBox7.Text & "%' or
                password like '%" & TextBox7.Text & "%'", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub load_tb()
        dt = Read("Select * from user", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If id = "" Or id = Nothing Then
            Button2.Enabled = False
            Button3.Enabled = False
        Else
            Button2.Enabled = True
            Button3.Enabled = True
        End If
    End Sub
End Class