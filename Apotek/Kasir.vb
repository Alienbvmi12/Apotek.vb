Imports System.Drawing.Printing

Public Class Kasir
    Dim dt As DataTable
    Dim PreviewPrint As New PrintPreviewDialog
    Dim totQty As Integer = 0
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Result = MessageBox.Show("Yakin?", "Confirm", MessageBoxButtons.YesNo)
        If Result = DialogResult.Yes Then
            Dim newRow As New DataGridViewRow
            newRow.CreateCells(DataGridView1)
            newRow.Cells(0).Value = TextBox1.Text
            newRow.Cells(1).Value = TextBox2.Text
            newRow.Cells(2).Value = DateTimePicker1.Value
            newRow.Cells(3).Value = TextBox3.Text
            newRow.Cells(4).Value = TextBox4.Text
            newRow.Cells(5).Value = TextBox5.Text
            newRow.Cells(6).Value = TextBox6.Text
            newRow.Cells(7).Value = MaskedTextBox1.Text
            DataGridView1.Rows.Add(newRow)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Result = MessageBox.Show("Yakin?", "Confirm", MessageBoxButtons.YesNo)
        If Result = DialogResult.Yes Then
            DataGridView1.Rows.Clear()
            totQty = 0
        End If
    End Sub

    Private Sub Kasir_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label9.Text = Login.UsernameTextBox.Text
        Timer1.Interval = 1000
        Timer1.Enabled = True
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox1.SelectedIndex = 0
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim values As Integer = 0
        If DataGridView1.Rows.Count > 0 Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells(6).Value = "" Or row.Cells(6).Value = Nothing Then
                Else
                    values = values + Integer.Parse(row.Cells(6).Value) * Integer.Parse(row.Cells(7).Value)
                End If
            Next
        End If
        Total.Text = values.ToString()
        Label12.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")
        If TextBox1.Text = "" Then
            Button2.Enabled = False
        Else
            Button2.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Integer.Parse(Total.Text) > Integer.Parse(MaskedTextBox2.Text) Then
            MsgBox("Uang anda tidak cukup!!")
        Else
            Label13.Text = (Integer.Parse(MaskedTextBox2.Text) - Integer.Parse(Total.Text)).ToString
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Function generateNoTrans(dates As String, no As String)
        If no.Length = 6 Then
            Return dates & no
        ElseIf no.Length = 5 Then
            Return dates & "0" & no
        ElseIf no.Length = 4 Then
            Return dates & "00" & no
        ElseIf no.Length = 3 Then
            Return dates & "000" & no
        ElseIf no.Length = 2 Then
            Return dates & "0000" & no
        ElseIf no.Length = 1 Then
            Return dates & "00000" & no
        End If
    End Function

    Private Sub TextBox5_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox5.KeyUp
        Try
            dt = Read("Select harga From obat where nama_obat='" & TextBox5.Text & "'", False)
            TextBox6.Text = dt.Rows(0).Item(0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TextBox1.SelectedIndexChanged
        If TextBox1.Text = "No Resep" Then
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
        Else
            TextBox2.Enabled = True
            TextBox3.Enabled = True
            TextBox4.Enabled = True
        End If
    End Sub

    Private Sub LogoutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LogoutToolStripMenuItem.Click
        Login.Show()
        Me.Hide()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PreviewPrint.Document = PrintDocument1
        PreviewPrint.ShowDialog()
        SaveToDatabase()
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        'Font Setting variables

        Dim f10 As New Font("Times New Roman", 10, FontStyle.Regular)
        Dim f10b As New Font("Times New Roman", 10, FontStyle.Bold)
        Dim f14 As New Font("Times New Roman", 10, FontStyle.Bold)

        'Margin Setting variables

        Dim leftMargin As Integer = PrintDocument1.DefaultPageSettings.Margins.Left
        Dim rightMargin As Integer = PrintDocument1.DefaultPageSettings.PaperSize.Width - 30
        Dim centerMargin As Integer = PrintDocument1.DefaultPageSettings.PaperSize.Width / 2

        'Alignment Setting variables

        Dim kanan As New StringFormat
        Dim tengah As New StringFormat
        kanan.Alignment = StringAlignment.Far
        tengah.Alignment = StringAlignment.Center

        ' Dapatkan data yang diperlukan dari db

        Dim countNoTrans = Read("Select no_transaksi from transaksi group by no_transaksi", False)
        countNoTrans = generateNoTrans(
                            DateTime.Now.ToString("yyyyMMdd"),
                            (Integer.Parse(countNoTrans.Rows.Count) + 1).ToString())
        dt = Read("Select nama_user from user where id_user='" & Login.idUser & "'", False)
        Dim NamaKasir = dt(0).Item("nama_user").ToString()

        Dim line As String = "--------------------------------------------------"
        '                      String       Font  Warna brush         X       Y  Align
        e.Graphics.DrawString("Apotek XYZ", f14, Brushes.Black, centerMargin, 5, tengah)
        e.Graphics.DrawString("Jl. Kebahagiaan no SSR++, Kota S", f10, Brushes.Black, centerMargin, 21, tengah)
        e.Graphics.DrawString("Hp: 081224018624", f10, Brushes.Black, centerMargin, 32, tengah)

        e.Graphics.DrawString("No Transaksi", f10, Brushes.Black, 0, 52)
        e.Graphics.DrawString(":", f10, Brushes.Black, 70, 52)
        e.Graphics.DrawString(countNoTrans, f10, Brushes.Black, 80, 52)

        e.Graphics.DrawString("Nama Kasir", f10, Brushes.Black, 0, 65)
        e.Graphics.DrawString(":", f10, Brushes.Black, 70, 65)
        e.Graphics.DrawString(NamaKasir, f10, Brushes.Black, 80, 65)

        e.Graphics.DrawString(DateTime.Now.ToString(), f10, Brushes.Black, 0, 78)

        e.Graphics.DrawString(line, f10, Brushes.Black, 0, 91)

        Dim gap As Integer = 105
        For row As Integer = 0 To DataGridView1.Rows.Count - 1
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(7).Value, f10, Brushes.Black, 0, gap)
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(5).Value, f10, Brushes.Black, centerMargin, gap, tengah)
            e.Graphics.DrawString(DataGridView1.Rows(row).Cells(6).Value, f10, Brushes.Black, rightMargin, gap, kanan)
            gap += 15
            totQty += Integer.Parse(DataGridView1.Rows(row).Cells(7).Value)
        Next

        e.Graphics.DrawString(line, f10, Brushes.Black, 0, gap + 15)
        e.Graphics.DrawString("Total Qty   : " & totQty.ToString() & "", f10, Brushes.Black, 0, gap + 30)
        e.Graphics.DrawString("Total   : Rp. " & Total.Text & "", f10, Brushes.Black, leftMargin, gap + 30)
        e.Graphics.DrawString("Bayar   : Rp. " & MaskedTextBox2.Text & "", f10, Brushes.Black, leftMargin, gap + 45)
        e.Graphics.DrawString("Kembali : Rp. " & Label13.Text & "", f10, Brushes.Black, leftMargin, gap + 60)



    End Sub

    Private Sub PrintDocument1_BeginPrint(sender As Object, e As PrintEventArgs) Handles PrintDocument1.BeginPrint
        PrintDocument1.DefaultPageSettings.PaperSize = New PaperSize("custom", 250, 500)
    End Sub

    Private Sub SaveToDatabase()
        Dim result = MessageBox.Show("Simpan ke database?", "Confirm", MessageBoxButtons.YesNo)
        Dim jumrow = DataGridView1.Rows.Count - 1
        If result = DialogResult.Yes Then
            Dim countNoTrans = Read("Select no_transaksi from transaksi group by no_transaksi", False)
            countNoTrans = generateNoTrans(
                            DateTime.Now.ToString("yyyyMMdd"),
                            (Integer.Parse(countNoTrans.Rows.Count) + 1).ToString())
            For cnt As Integer = 0 To jumrow
                Dim row As DataGridViewRow = DataGridView1.Rows(cnt)
                Dim _id_obat As DataTable = Read("select id_obat from obat where 
                        nama_obat='" & row.Cells(5).Value & "'", False)
                Dim id_obat As String
                If _id_obat.Rows.Count > 0 Then
                    id_obat = _id_obat(0).Item("id_obat")
                Else
                    id_obat = "404"
                End If
                Dim _id_resep As DataTable = Read("select id_resep from resep where 
                        no_resep='" & row.Cells(1).Value & "'", False)
                Dim id_resep As String
                If _id_resep.Rows.Count > 0 Then
                    id_resep = _id_resep(0).Item("id_resep").ToString()
                Else
                    id_resep = "52"
                End If

                If TextBox1.Text = "Resep" Then
                    SendOnly("Insert into transaksi values('',
                            '" & countNoTrans & "',
                            '" & DateTime.Now.ToString("yyyy-MM-dd") & "',
                            " & row.Cells(6).Value & ",
                            " & Login.idUser & ",
                            " & id_obat & ",
                            " & id_resep & " )", False)
                Else
                    SendOnly("Insert into transaksi values('',
                            '" & countNoTrans & "',
                            '" & DateTime.Now.ToString("yyyy-MM-dd") & "',
                            " & Integer.Parse(row.Cells(6).Value) * Integer.Parse(row.Cells(7).Value) & ",
                            " & Login.idUser & ",
                            " & id_obat & ",
                            52 )", False)
                End If
            Next
        End If
    End Sub
End Class