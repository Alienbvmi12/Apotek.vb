Public Class Admin
    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As DataTable = Read("Select id_log, user.username as username, waktu, aktifitas
                        from log, user where log.id_user = user.id_user and
                        date(log.waktu) = '" & DateTime.Parse(DateTimePicker1.Value).ToString("yyyy-MM-dd") & "'", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub KelolaUserToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KelolaUserToolStripMenuItem.Click
        KelolaUser.Show()
        Me.Hide()
    End Sub

    Private Sub KelolaObatToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KelolaObatToolStripMenuItem.Click
        KelolaObat.Show()
        Me.Hide()
    End Sub

    Private Sub LaporanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanToolStripMenuItem.Click
        KelolaLaporan.Show()
        Me.Hide()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class