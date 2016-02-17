Public Class PrintSpecial : Inherits PrintWithHeader

    Public items As List(Of _Special)
    Public filterName As String

    Private cDocDate, cDoc, cBank, cFilter, cAmount, maxLines, currentLines As Integer
    Private grandTotal As Double
    Private rFilter, rAmount As Rectangle
    Dim sFilter As Size
    Dim sfFilter As StringFormat

    Public Overrides Sub init()
        cDocDate = BOUND_LEFT + PADDING_COL
        cDoc = cDocDate + 110
        cBank = cDoc + 120
        cFilter = cBank + 50
        cAmount = cFilter + 350

        rAmount = createRectangle(cAmount, BOUND_RIGHT - PADDING_COL)

        rFilter = New Rectangle(cFilter, 0, cAmount - cFilter, 100)
        sFilter = New Size(cAmount - cFilter - PADDING_COL, 100)
        sfFilter = New StringFormat

        grandTotal = 0
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        rFilter.Y = Y
        rAmount.Y = Y

        e.Graphics.DrawString("Check Date", COURIER_10B, Brushes.Black, cDocDate, Y)
        e.Graphics.DrawString("Check No.", COURIER_10B, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString("Bank", COURIER_10B, Brushes.Black, cBank, Y)
        e.Graphics.DrawString(filterName, COURIER_10B, Brushes.Black, rFilter)
        e.Graphics.DrawString("Amount", COURIER_10B, Brushes.Black, rAmount, ALIGN_RIGHT)
        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < items.Count
            Y += PADDING_ROW

            rAmount.Y = Y

            e.Graphics.DrawString(Format(items(INDEX).Date, Constants.DATE_FORMAT), _
                COURIER_10, Brushes.Black, cDocDate, Y)
            e.Graphics.DrawString(items(INDEX).DocNo, COURIER_10, Brushes.Black, cDoc, Y)
            e.Graphics.DrawString(items(INDEX).Bank, COURIER_10, Brushes.Black, cBank, Y)

            rFilter.Y = Y
            e.Graphics.DrawString(items(INDEX).Filter, COURIER_10, Brushes.Black, rFilter)
            e.Graphics.MeasureString(items(INDEX).Filter, COURIER_10, sFilter, _
                sfFilter, COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            e.Graphics.DrawString(FormatNumber(items(INDEX).Amount, 2), COURIER_10, Brushes.Black, rAmount, ALIGN_RIGHT)

            grandTotal += items(INDEX).Amount

            INDEX += 1

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                Exit Sub
            End If

            Y += (ROW_HEIGHT + PADDING_ROW) * maxLines
        End While

        drawGrandTotal(e)
        e.HasMorePages = False
    End Sub

    Private Sub drawGrandTotal(ByRef e As Printing.PrintPageEventArgs)
        Y += PADDING_ROW
        e.Graphics.DrawLine(Pens.Black, cDocDate, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += PADDING_ROW
        e.Graphics.DrawString("GRAND TOTAL: ", COURIER_10B, Brushes.Black, cFilter + 20, Y)

        e.Graphics.DrawString(FormatNumber(grandTotal, 2), COURIER_10, Brushes.Black, _
               createRectangle(cFilter, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)
    End Sub

    Protected Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)
    End Sub

End Class
