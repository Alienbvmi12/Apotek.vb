Imports System.Threading

Public Class KelolaLaporan
    Dim dt As DataTable
    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

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

    End Sub

    Private Sub AdminNavigationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdminNavigationToolStripMenuItem.Click
        Admin.Show()
        Me.Hide()
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DateTimePicker2.MinDate = DateTimePicker1.Value
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        dt = Read("Select tgl_transaksi as tanggal, sum(total_bayar) as income from transaksi where tgl_transaksi  
                between '" & DateTime.Parse(DateTimePicker1.Value).ToString("yyyy-MM-dd") & "'
                and '" & DateTime.Parse(DateTimePicker2.Value).ToString("yyyy-MM-dd") & "' group by tgl_transaksi", False)
        DataGridView1.DataSource = dt
    End Sub

    Private Sub KelolaLaporan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Chart1.Series.Clear()
        Chart1.Series.Add("Omset")
        For Each row As DataRow In dt.Rows
            Chart1.Series("Omset").Points.AddXY(row(0), row(1))
        Next
        Chart1.Show()
    End Sub
End Class