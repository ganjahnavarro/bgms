Public Class PrintMonitorByAgent : Inherits PrintWithHeader

    Public sales As List(Of salesorder)

    Dim objCustomer, disc1, disc2, currentAgent As String
    Dim objDate As Date
    Dim objAmount, agentTotal, grandTotal As Double

    Dim col1, col2, col3, col4, col5 As Integer
    Dim maxLines, currentLines, w3 As Integer
    Dim r3 As Rectangle

    Public Overrides Sub init()
        BOUND_LEFT = BOUND_LEFT + 25
        BOUND_RIGHT = BOUND_RIGHT - 50
        WIDTH_MAX = BOUND_RIGHT - BOUND_LEFT

        report_name = "Sales Monitoring by Agent"

        col1 = BOUND_LEFT + PADDING_COL
        col2 = col1 + CInt(0.12 * WIDTH_MAX)
        col3 = col2 + CInt(0.12 * WIDTH_MAX)
        col4 = col3 + CInt(0.397 * WIDTH_MAX)
        col5 = col4 + CInt(0.241 * WIDTH_MAX)

        w3 = col4 - col3 - PADDING_COL
        r3 = New Rectangle(col3, Y, w3, 100)

        currentAgent = String.Empty
        agentTotal = 0.0
        grandTotal = 0.0
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString("Agent", COURIER_10B, Brushes.Black, col1, Y)
        e.Graphics.DrawString("Date", COURIER_10B, Brushes.Black, col2, Y)
        e.Graphics.DrawString("Customer", COURIER_10B, Brushes.Black, col3, Y)
        e.Graphics.DrawString("Document No.", COURIER_10B, Brushes.Black, col4, Y)
        e.Graphics.DrawString("Amount", COURIER_10B, Brushes.Black, col5, Y)
        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < sales.Count
            Y += PADDING_ROW
            maxLines = 0
            currentLines = 0
            drawItems(e)
            agentTotal += objAmount
            INDEX += 1

            If INDEX = sales.Count OrElse _
                Not sales(INDEX).agent.Name.ToUpper.Equals(currentAgent.ToUpper) Then
                drawTotalForAgent(e)
            End If

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                currentAgent = String.Empty
                Exit Sub
            End If

            Y += ROW_HEIGHT * maxLines
        End While

        drawGrandTotal(e)
        e.HasMorePages = False
    End Sub

    Private Sub drawItems(ByRef e As Printing.PrintPageEventArgs)
        If String.IsNullOrEmpty(currentAgent) Then
            currentAgent = sales(INDEX).agent.Name.ToUpper
            e.Graphics.DrawString(currentAgent, COURIER_10, Brushes.Black, col1, Y)
        End If

        objDate = sales(INDEX).Date
        e.Graphics.DrawString(Format(objDate, Constants.DATE_FORMAT), _
            ARIAL_10, Brushes.Black, col2, Y)

        r3.Y = Y
        objCustomer = sales(INDEX).customer.Name
        e.Graphics.DrawString(objCustomer, ARIAL_10, Brushes.Black, r3)
        e.Graphics.MeasureString(objCustomer, ARIAL_10, New Size(w3, 100), _
            New StringFormat(), COLS, currentLines)
        maxLines = Math.Max(maxLines, currentLines)

        disc1 = If(sales(INDEX).Discount1 > 0, _
                   " L" & sales(INDEX).Discount1 & "%", String.Empty)
        disc2 = If(sales(INDEX).Discount2 > 0, _
                   " L" & sales(INDEX).Discount2 & "%", String.Empty)

        e.Graphics.DrawString(sales(INDEX).DocumentNo & disc1 & disc2, ARIAL_10, Brushes.Black, col4, Y)

        objAmount = sales(INDEX).TotalAmount
        e.Graphics.DrawString(FormatNumber(objAmount, 2), _
            ARIAL_10, Brushes.Black, New Rectangle(BOUND_RIGHT - 200, Y, 200, ROW_HEIGHT + 5), ALIGN_RIGHT)
    End Sub

    Private Sub drawTotalForAgent(ByRef e As Printing.PrintPageEventArgs)
        Y += ROW_HEIGHT + (PADDING_ROW * 2)
        e.Graphics.DrawLine(Pens.Black, col4, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += PADDING_ROW
        e.Graphics.DrawString("TOTAL PER AGENT:", COURIER_10, Brushes.Black, col4 - 20, Y)
        e.Graphics.DrawString(FormatNumber(agentTotal, 2), ARIAL_10, _
            Brushes.Black, New Rectangle(BOUND_RIGHT - 200, Y, 200, ROW_HEIGHT + 5), ALIGN_RIGHT)

        Y += ROW_HEIGHT + (PADDING_ROW * 2)
        e.Graphics.DrawLine(Pens.Black, col1, Y, BOUND_RIGHT - PADDING_COL, Y)
        Y = Y - ROW_HEIGHT

        currentAgent = String.Empty
        grandTotal += agentTotal
        agentTotal = 0.0
    End Sub

    Private Sub drawGrandTotal(ByRef e As Printing.PrintPageEventArgs)
        Y += PADDING_ROW
        e.Graphics.DrawLine(Pens.Black, col1, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += PADDING_ROW
        e.Graphics.DrawString("GRAND TOTAL:", COURIER_10, Brushes.Black, col4 - 20, Y)
        e.Graphics.DrawString(FormatNumber(grandTotal, 2), ARIAL_10, _
            Brushes.Black, New Rectangle(BOUND_RIGHT - 200, Y, 200, ROW_HEIGHT + 5), ALIGN_RIGHT)
    End Sub

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)

    End Sub

End Class
