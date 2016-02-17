Public Class PrintTransactionList : Inherits PrintWithHeader

    Public items As List(Of _TransactionList)
    Public filterName As String

    Dim vfilter, transactionName As String
    Dim cDoc, cDocDate, cFilter, cDiscs, cTotalAmt, maxLines, currentLines As Integer
    Dim curAmount, grandTotal As Double

    Dim sFilter As Size
    Dim rFilter As Rectangle
    Dim sfFilter As StringFormat

    Public Overrides Sub init()
        cDoc = BOUND_LEFT + PADDING_COL
        cDocDate = cDoc + CInt(0.14 * WIDTH_MAX)
        cFilter = cDocDate + CInt(0.13 * WIDTH_MAX)
        cDiscs = cFilter + CInt(0.405 * WIDTH_MAX)
        cTotalAmt = cDiscs + CInt(0.04 * WIDTH_MAX)

        rFilter = New Rectangle(cFilter, 0, cDiscs - cFilter, 100)
        sFilter = New Size(cDiscs - cFilter - PADDING_COL, 100)
        sfFilter = New StringFormat

        grandTotal = 0
        curAmount = 0
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString("DOC NO.", COURIER_10B, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString("DATE", COURIER_10B, Brushes.Black, cDocDate, Y)
        e.Graphics.DrawString(filterName, COURIER_10B, Brushes.Black, cFilter, Y)
        e.Graphics.DrawString("AMOUNT", COURIER_10B, Brushes.Black, _
           createRectangle(cFilter, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)
        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < items.Count
            Y += PADDING_ROW
            maxLines = 0
            currentLines = 0
            drawTransaction(e)
            grandTotal += curAmount
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

    Protected Overridable Sub drawTransaction(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString(items(INDEX).DocumentNo, COURIER_10, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString(Format(items(INDEX).Date, Constants.DATE_FORMAT), _
            COURIER_10, Brushes.Black, cDocDate, Y)

        rFilter.Y = Y
        vfilter = items(INDEX).Filter
        e.Graphics.DrawString(vfilter, COURIER_10, Brushes.Black, rFilter)
        e.Graphics.MeasureString(vfilter, COURIER_10, sFilter, _
            sfFilter, COLS, currentLines)
        maxLines = Math.Max(maxLines, currentLines)

        e.Graphics.DrawString(items(INDEX).getDiscountDisplay, _
            COURIER_10, Brushes.Black, cDiscs, Y)

        curAmount = items(INDEX).TotalAmount
        e.Graphics.DrawString(FormatNumber(curAmount, 2), COURIER_10, Brushes.Black, _
           createRectangle(cFilter, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)
    End Sub

    Private Sub drawGrandTotal(ByRef e As Printing.PrintPageEventArgs)
        Y += PADDING_ROW
        e.Graphics.DrawLine(Pens.Black, cDoc, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += PADDING_ROW
        e.Graphics.DrawString("GRAND TOTAL: ", COURIER_10B, Brushes.Black, cDiscs, Y)

        e.Graphics.DrawString(FormatNumber(grandTotal, 2), COURIER_10, Brushes.Black, _
               createRectangle(cFilter, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)
    End Sub

    Protected Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)
    End Sub

End Class
