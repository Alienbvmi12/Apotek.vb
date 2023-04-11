Public Class Apoteker
    Dim dt As DataTable
    Dim id As String
    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Apoteker_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_tb()
    End Sub

    Private Sub load_tb()
        dt = Read("Select * from resep", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Sub TextBox7_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox7.KeyUp
        dt = Read("Select * from resep where no_resep like '%" & TextBox7.Text & "%'", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SendOnly("update resep set 
                no_resep='" & TextBox1.Text & "',
                tgl_resep='" & DateTime.Parse(DateTimePicker1.Text).ToString("yyyy-MM-dd") & "',
                nama_pasien='" & TextBox3.Text & "',
                nama_dokter='" & TextBox4.Text & "',
                nama_obat_dibeli='" & TextBox5.Text & "',
                jumlah_obat_dibeli='" & TextBox6.Text & "' where id_resep='" & id & "'", True, "Edit resep berhasil!!", True)
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        id = ""
        load_tb()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SendOnly("Delete from resep where id_resep='" & id & "'", True, "Hapus Resep Berhasi", True)
        TextBox1.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        id = ""
        load_tb()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Try
            Dim tabel As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            id = tabel.Cells(0).Value
            TextBox1.Text = tabel.Cells(1).Value
            DateTimePicker1.Value = tabel.Cells(2).Value
            TextBox3.Text = tabel.Cells(4).Value
            TextBox4.Text = tabel.Cells(3).Value
            TextBox5.Text = tabel.Cells(5).Value
            TextBox6.Text = tabel.Cells(6).Value
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
End Class