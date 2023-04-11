Public Class KelolaObat
    Dim dt As DataTable
    Dim id As String
    Private Sub KelolaUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KelolaUserToolStripMenuItem.Click
        KelolaUser.Show()
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

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub load_tb()
        dt = Read("Select * from obat", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SendOnly("Delete from obat where id_obat='" & id & "'", True, "Obat berhasil dihapus!!", True)
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox4.Text = "0"
        TextBox5.Text = "0"
        load_tb()
        id = ""
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SendOnly("Update obat set 
                    kode_obat='" & TextBox1.Text & "',
                    nama_obat='" & TextBox2.Text & "',
                    expired_date='" & DateTime.Parse(DateTimePicker1.Text).ToString("yyyy-MM-dd") & "',
                    jumlah='" & TextBox4.Text & "',
                    harga='" & TextBox5.Text & "'
                    where id_obat='" & id & "'", True, "Obat berhasil diedit!!", True)
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox4.Text = "0"
        TextBox5.Text = "0"
        load_tb()
        id = ""
    End Sub

    Private Sub TextBox7_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyUp
        dt = Read("Select * from obat where
                    id_obat like '%" & TextBox7.Text & "%' or 
                    kode_obat like '%" & TextBox7.Text & "%' or 
                    nama_obat like '%" & TextBox7.Text & "%' or 
                    expired_date like '%" & TextBox7.Text & "%' or 
                    jumlah like '%" & TextBox7.Text & "%' or 
                    harga like '%" & TextBox7.Text & "%'", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub DataGridView1_RowHeightInfoPushed(sender As Object, e As DataGridViewRowHeightInfoPushedEventArgs) Handles DataGridView1.RowHeightInfoPushed

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            If e.RowIndex >= 0 Then
                Dim tabel = DataGridView1.Rows(e.RowIndex)
                id = tabel.Cells(0).Value
                TextBox1.Text = tabel.Cells(1).Value
                TextBox2.Text = tabel.Cells(2).Value
                DateTimePicker1.Value = tabel.Cells(3).Value
                TextBox4.Text = tabel.Cells(4).Value
                TextBox5.Text = tabel.Cells(5).Value
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SendOnly("Insert into obat values('', 
                    '" & TextBox1.Text & "',
                    '" & TextBox2.Text & "',
                    '" & DateTime.Parse(DateTimePicker1.Text).ToString("yyyy-MM-dd") & "',
                    " & TextBox4.Text & ",
                    " & TextBox5.Text & "
                    )", True, "Obat berhasil ditambahkan!!", True)
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox4.Text = "0"
        TextBox5.Text = "0"
        load_tb()
        id = ""
    End Sub

    Private Sub KelolaObat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_tb()
    End Sub
End Class