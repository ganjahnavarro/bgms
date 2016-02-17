Public Class PrintTransactionDetail : Inherits PrintWithHeader

    Public items As List(Of _TransactionDetail)
    Public filterName As String
    Public ccOrSp As Boolean

    Dim vfilter, transactionName As String
    Dim cDoc, cDocDate, cFilter, cDiscs, cTotalAmt, maxLines, currentLines As Integer
    Dim curAmount, grandTotal As Double

    Dim sFilter As Size
    Dim rFilter As Rectangle
    Dim sfFilter As StringFormat

    'With Details
    Dim cStock, cQty, cUnit, cPrice, cEquals, cAmount As Integer
    Dim currentObject As String

    Public Overrides Sub init()
        cDoc = BOUND_LEFT + PADDING_COL
        cDocDate = cDoc + CInt(0.14 * WIDTH_MAX)
        cFilter = cDocDate + CInt(0.13 * WIDTH_MAX)
        cDiscs = cFilter + CInt(0.37 * WIDTH_MAX)
        cTotalAmt = cDiscs + CInt(0.065 * WIDTH_MAX)

        cStock = cDocDate + 40
        cQty = cStock + 250
        cUnit = cQty + 40
        cPrice = cUnit + 40
        cEquals = cPrice + 100

        rFilter = New Rectangle(cFilter, 0, cDiscs - cFilter, 100)
        sFilter = New Size(cDiscs - cFilter - PADDING_COL, 100)
        sfFilter = New StringFormat

        grandTotal = 0
        curAmount = 0
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        'report_name = transactionName

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
            curAmount = 0

            drawTransaction(e)

            grandTotal += curAmount
            INDEX += 1

            If INDEX = items.Count OrElse _
                Not items(INDEX).DocumentNo.Equals(currentObject) Then
                Y += ROW_HEIGHT + 8
                e.Graphics.DrawLine(Pens.Black, cDoc, Y, BOUND_RIGHT - PADDING_COL, Y)
                Y -= ROW_HEIGHT
                currentObject = String.Empty
            End If

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                Exit Sub
            End If

            Y += ROW_HEIGHT + PADDING_ROW
        End While

        drawGrandTotal(e)
        e.HasMorePages = False
    End Sub

    Protected Overridable Sub drawTransaction(ByRef e As Printing.PrintPageEventArgs)
        If String.IsNullOrEmpty(currentObject) Then
            drawGroupHeader(e)
        End If

        If ccOrSp Then
            drawDetailForCollectionOrPayment(e)
        Else
            drawDetail(e)
        End If
    End Sub

    Private Sub drawDetail(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString(items(INDEX).Stock, COURIER_10, Brushes.Black, cStock, Y)
        e.Graphics.DrawString(items(INDEX).Quantity, COURIER_10, _
                 Brushes.Black, createRectangle(cQty, cUnit), ALIGN_RIGHT)
        e.Graphics.DrawString(items(INDEX).Unit, COURIER_10, Brushes.Black, cUnit, Y)
        e.Graphics.DrawString(FormatNumber(items(INDEX).getDiscountedPrice, 2), COURIER_10, _
                 Brushes.Black, createRectangle(cPrice, cEquals), ALIGN_RIGHT)
        e.Graphics.DrawString("=", COURIER_10, Brushes.Black, cEquals, Y)
        e.Graphics.DrawString(FormatNumber(items(INDEX).getDiscountedAmount, 2), COURIER_10, _
                 Brushes.Black, createRectangle(cFilter, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)
    End Sub

    Private Sub drawDetailForCollectionOrPayment(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString(items(INDEX).xDetail, COURIER_10, Brushes.Black, cStock, Y)
        e.Graphics.DrawString(Format(items(INDEX).xDate, Constants.DATE_FORMAT), _
            COURIER_10, Brushes.Black, cStock + 130, Y)
        e.Graphics.DrawString(items(INDEX).xBank, COURIER_10, Brushes.Black, cStock + 270, Y)
        e.Graphics.DrawString(FormatNumber(items(INDEX).xAmount, 2), COURIER_10, _
                Brushes.Black, createRectangle(cFilter, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)
    End Sub

    Private Sub drawGroupHeader(ByRef e As Printing.PrintPageEventArgs)
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
        e.Graphics.DrawString(FormatNumber(curAmount, 2), COURIER_10B, Brushes.Black, _
           createRectangle(cFilter, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)

        currentObject = items(INDEX).DocumentNo

        Y += (ROW_HEIGHT + PADDING_ROW) * maxLines
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
