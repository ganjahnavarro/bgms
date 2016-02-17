Imports System.Configuration

Public Class Test

    Dim mySource As New AutoCompleteStringCollection

    Private Sub PictureBox1_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox1.MouseEnter
        Panel5.Visible = True
    End Sub

    Private Sub PictureBox1_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox1.MouseLeave
        Panel5.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim cs = ConfigurationManager.ConnectionStrings("bgmsEntities").ConnectionString

        'Using context As New bgmsEntities(cs.Replace("bgms", "bgmstest"))
        ' MsgBox(context.users.Select(Function(c) c.Firstname).FirstOrDefault)
        'End Using
    End Sub

    Private Sub Test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Using context As New bgmsEntities
        '    Dim so = context.salesorders.Where(Function(c) c.Id = 25442).FirstOrDefault
        '    Dim d1 As Double = FormatNumber(CDbl(so.TotalAmount), 2)
        '    Dim d2 As Double = FormatNumber(CDbl(so.TotalAmount), 2)
        '    MsgBox(d1)
        '    MsgBox(d2)
        '    MsgBox(d1.Equals(d2))

        '    MsgBox(so.TotalAmount = so.TotalReturned)
        'End Using
    End Sub

    Dim prevCharCount As Integer = 0

    Private Sub tb_KeyDown(sender As Object, e As KeyEventArgs) Handles tb.KeyDown
        If Not e.KeyCode = Keys.Enter And Not e.KeyCode = Keys.Back Then
            If tb.TextLength >= 10 Then
                e.Handled = True
            End If
        End If
    End Sub


    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles tb.TextChanged
        If tb.TextLength = 2 Or tb.TextLength = 5 Then
            If prevCharCount < tb.TextLength Then
                tb.AppendText("/")
            Else
                SendKeys.Send("{BACKSPACE}")
            End If
        End If

        prevCharCount = tb.TextLength
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As DateTime
        DateTime.TryParse(tb.Text, dt)

        MsgBox(dt.ToLongDateString)
    End Sub
End Class
