Public Class PrintCustomers : Inherits PrintWithHeader

    Dim objName, objAddress, objTel, objFax, objTin, objAgent As String
    Dim col1, col2, col3, col4, col5, col6 As Integer
    Dim maxLines, currentLines, w1, w2, w3, w4, w5, w6 As Integer
    Dim r1, r2, r3, r4, r5, r6 As Rectangle

    Public customers As List(Of customer)

    Public Overrides Sub init()
        report_name = "LIST OF CUSTOMER"

        col1 = BOUND_LEFT + PADDING_COL
        col2 = col1 + CInt(0.215 * WIDTH_MAX)
        col3 = col2 + CInt(0.277 * WIDTH_MAX)
        col4 = col3 + CInt(0.158 * WIDTH_MAX)
        col5 = col4 + CInt(0.123 * WIDTH_MAX)
        col6 = col5 + CInt(0.123 * WIDTH_MAX)

        w1 = col2 - col1 - PADDING_COL
        w2 = col3 - col2 - PADDING_COL
        w3 = col4 - col3 - PADDING_COL
        w4 = col5 - col4 - PADDING_COL
        w5 = col6 - col5 - PADDING_COL
        w6 = BOUND_RIGHT - col6 - PADDING_COL

        r1 = New Rectangle(col1, Y, w1, 100)
        r2 = New Rectangle(col2, Y, w2, 100)
        r3 = New Rectangle(col3, Y, w3, 100)
        r4 = New Rectangle(col4, Y, w4, 100)
        r5 = New Rectangle(col5, Y, w5, 100)
        r6 = New Rectangle(col6, Y, w5, 100)
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString("Customer", ARIAL_8B, Brushes.Black, col1, Y)
        e.Graphics.DrawString("Address", ARIAL_8B, Brushes.Black, col2, Y)
        e.Graphics.DrawString("Tel. No.", ARIAL_8B, Brushes.Black, col3, Y)
        e.Graphics.DrawString("Fax No.", ARIAL_8B, Brushes.Black, col4, Y)
        e.Graphics.DrawString("Tin No.", ARIAL_8B, Brushes.Black, col5, Y)
        e.Graphics.DrawString("Agent", ARIAL_8B, Brushes.Black, col6, Y)
        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < customers.Count
            Y += PADDING_ROW
            maxLines = 0
            currentLines = 0

            r1.Y = Y
            objName = customers(INDEX).Name
            e.Graphics.DrawString(objName, ARIAL_8, Brushes.Black, r1)
            e.Graphics.MeasureString(objName, ARIAL_8, New Size(w1, 100), _
                New StringFormat(), COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            r2.Y = Y
            objAddress = customers(INDEX).Address
            e.Graphics.DrawString(objAddress, ARIAL_8, Brushes.Black, r2)
            e.Graphics.MeasureString(objAddress, ARIAL_8, New Size(w2, 100), _
                New StringFormat(), COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            r3.Y = Y
            objTel = customers(INDEX).Contact
            e.Graphics.DrawString(objTel, ARIAL_8, Brushes.Black, r3)
            e.Graphics.MeasureString(objTel, ARIAL_8, New Size(w3, 100), _
                New StringFormat(), COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            r4.Y = Y
            objFax = customers(INDEX).Fax
            e.Graphics.DrawString(objFax, ARIAL_8, Brushes.Black, r4)
            e.Graphics.MeasureString(objFax, ARIAL_8, New Size(w4, 100), _
                New StringFormat(), COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            r5.Y = Y
            objTin = customers(INDEX).Tin
            e.Graphics.DrawString(objTin, ARIAL_8, Brushes.Black, r5)
            e.Graphics.MeasureString(objTin, ARIAL_8, New Size(w5, 100), _
                New StringFormat(), COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            r6.Y = Y
            objAgent = customers(INDEX).agent.Name
            e.Graphics.DrawString(objAgent, ARIAL_8, Brushes.Black, r6)
            e.Graphics.MeasureString(objAgent, ARIAL_8, New Size(w6, 100), _
                New StringFormat(), COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            INDEX += 1

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                Exit Sub
            End If

            Y += ROW_HEIGHT * maxLines
            Y += PADDING_ROW
            e.Graphics.DrawLine(DASHED_GRAY_PEN, col1, Y, BOUND_RIGHT - PADDING_COL, Y)
        End While

        e.HasMorePages = False
    End Sub

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)

    End Sub

End Class
