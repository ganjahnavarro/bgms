Public Class PrintMonitor : Inherits PrintWithHeader

    Public items As List(Of _Monitor)
    Public byStock As Boolean
    Public filter As String

    Dim totalQtyOrdered, totalQtyRtn, totalQtySales, totalAmount, _
        grandQtyOrdered, grandQtyRtn, grandQtySales, grandAmount As Double
    Dim currentObj As String

    Dim cGroupBy, cDateDoc, cFilter, cDoc, cQtyOrder, cQtyRtn, _
        cQtySales, cPrice, cAmount, cTotalLabel As Integer

    Dim rQtyOrder, rQtyRtn, rQtySales, rPrice, rAmount, rFilter As Rectangle

    Public Overrides Sub init()
        cGroupBy = BOUND_LEFT + PADDING_COL
        cDateDoc = cGroupBy + 20
        cFilter = cDateDoc + CInt(0.13 * WIDTH_MAX)
        cDoc = cFilter + CInt(0.25 * WIDTH_MAX)
        cQtyOrder = cDoc + CInt(0.12 * WIDTH_MAX)
        cQtyRtn = cQtyOrder + CInt(0.073 * WIDTH_MAX)
        cQtySales = cQtyRtn + CInt(0.073 * WIDTH_MAX)
        cPrice = cQtySales + CInt(0.073 * WIDTH_MAX)
        cAmount = cPrice + CInt(0.12 * WIDTH_MAX)

        cTotalLabel = cDoc - 100

        resetGroupTotals()
        grandQtyOrdered = 0.0
        grandQtyRtn = 0.0
        grandQtySales = 0.0
        grandAmount = 0.0

        rQtyOrder = createRectangle(cQtyOrder, cQtyRtn)
        rQtyRtn = createRectangle(cQtyRtn, cQtySales)
        rQtySales = createRectangle(cQtySales, cPrice)
        rPrice = createRectangle(cPrice, cAmount)
        rAmount = createRectangle(cAmount, BOUND_RIGHT - PADDING_COL)

        rFilter = New Rectangle(cFilter, 0, cDoc - cFilter, COURIER_10.GetHeight)
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        setRectanglesY()

        e.Graphics.DrawString(If(byStock, "STOCK", filter.ToUpper) & " / DATE", _
            COURIER_10B, Brushes.Black, cGroupBy, Y)
        e.Graphics.DrawString(If(byStock, filter.ToUpper, "STOCK"), COURIER_10B, _
            Brushes.Black, cFilter + 50, Y)
        e.Graphics.DrawString("DOC NO", COURIER_10B, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString("ORDER", COURIER_10B, Brushes.Black, rQtyOrder, ALIGN_RIGHT)
        e.Graphics.DrawString("RETURN", COURIER_10B, Brushes.Black, rQtyRtn, ALIGN_RIGHT)
        e.Graphics.DrawString("SALES", COURIER_10B, Brushes.Black, rQtySales, ALIGN_RIGHT)
        e.Graphics.DrawString("PRICE", COURIER_10B, Brushes.Black, rPrice, ALIGN_RIGHT)
        e.Graphics.DrawString("AMOUNT", COURIER_10B, Brushes.Black, rAmount, ALIGN_RIGHT)
        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < items.Count
            Y += PADDING_ROW
            drawItems(e)
            updateTotals()
            INDEX += 1

            If INDEX = items.Count OrElse _
                Not items(INDEX).Name.Equals(currentObj) Then
                drawTotalForGroup(e)
            End If

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                currentObj = String.Empty
                Exit Sub
            End If

            Y += ROW_HEIGHT
        End While

        drawGrandTotal(e)
        e.HasMorePages = False
    End Sub

    Private Sub drawItems(ByRef e As Printing.PrintPageEventArgs)
        If String.IsNullOrEmpty(currentObj) Then
            Y += 5
            currentObj = items(INDEX).Name
            e.Graphics.DrawString(items(INDEX).Name, COURIER_10B, Brushes.Black, cGroupBy, Y)
            e.Graphics.DrawString(items(INDEX).Unit, COURIER_10B, _
                Brushes.Black, cTotalLabel, Y)
            Y += ROW_HEIGHT + PADDING_COL
        End If

        setRectanglesY()

        e.Graphics.DrawString(Format(items(INDEX).Date, Constants.DATE_FORMAT), _
            COURIER_10, Brushes.Black, cDateDoc, Y)
        e.Graphics.DrawString(items(INDEX).Filter, COURIER_10, Brushes.Black, rFilter)
        e.Graphics.DrawString(items(INDEX).DocNo, COURIER_10, Brushes.Black, cDoc, Y)
        'e.Graphics.DrawString(items(INDEX).DocNo & items(INDEX).getDiscountDisplay, _
        '    COURIER_10, Brushes.Black, cDoc, Y)

        e.Graphics.DrawString(items(INDEX).QtyOrdered, COURIER_10, Brushes.Black, _
            rQtyOrder, ALIGN_RIGHT)
        e.Graphics.DrawString(items(INDEX).QtyReturned, COURIER_10, Brushes.Black, _
            rQtyRtn, ALIGN_RIGHT)
        e.Graphics.DrawString(items(INDEX).QtySales, COURIER_10, Brushes.Black, _
            rQtySales, ALIGN_RIGHT)
        e.Graphics.DrawString(FormatNumber(items(INDEX).getDiscountedPrice, 2), COURIER_10, Brushes.Black, _
            rPrice, ALIGN_RIGHT)
        e.Graphics.DrawString(FormatNumber(items(INDEX).Amount, 2), COURIER_10, Brushes.Black, _
            rAmount, ALIGN_RIGHT)

        totalQtyOrdered += items(INDEX).QtyOrdered
        totalQtyRtn += items(INDEX).QtyReturned
        totalQtySales += items(INDEX).QtySales
        totalAmount += items(INDEX).Amount
    End Sub

    Private Sub drawTotalForGroup(ByRef e As Printing.PrintPageEventArgs)
        Y += ROW_HEIGHT + (PADDING_ROW * 2)
        e.Graphics.DrawLine(Pens.Black, cQtyOrder, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += PADDING_ROW
        e.Graphics.DrawString("TOTAL PER " & filter.ToUpper & ":", COURIER_10B, _
            Brushes.Black, cTotalLabel, Y)

        setRectanglesY()
        e.Graphics.DrawString(totalQtyOrdered, COURIER_10, Brushes.Black, rQtyOrder, ALIGN_RIGHT)
        e.Graphics.DrawString(totalQtyRtn, COURIER_10, Brushes.Black, rQtyRtn, ALIGN_RIGHT)
        e.Graphics.DrawString(totalQtySales, COURIER_10, Brushes.Black, rQtySales, ALIGN_RIGHT)
        e.Graphics.DrawString(FormatNumber(totalAmount, 2), COURIER_10, _
            Brushes.Black, rAmount, ALIGN_RIGHT)

        Y += ROW_HEIGHT + (PADDING_ROW * 2)
        e.Graphics.DrawLine(Pens.Black, cGroupBy, Y, BOUND_RIGHT - PADDING_COL, Y)
        Y = Y - ROW_HEIGHT

        currentObj = String.Empty
        resetGroupTotals()
    End Sub

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)
    End Sub

    Private Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

    Private Sub drawGrandTotal(ByRef e As Printing.PrintPageEventArgs)
        Y += PADDING_ROW
        e.Graphics.DrawLine(Pens.Black, cGroupBy, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += PADDING_ROW
        setRectanglesY()
        e.Graphics.DrawString("GRAND TOTAL:", COURIER_10B, Brushes.Black, cTotalLabel, Y)
        e.Graphics.DrawString(grandQtyOrdered, COURIER_10B, Brushes.Black, createRectangle(cQtyOrder - 60, cQtyRtn - 30), ALIGN_RIGHT)
        e.Graphics.DrawString(grandQtyRtn, COURIER_10B, Brushes.Black, createRectangle(cQtyRtn - 40, cQtySales - 15), ALIGN_RIGHT)
        e.Graphics.DrawString(grandQtySales, COURIER_10B, Brushes.Black, createRectangle(cQtySales - 20, cPrice), ALIGN_RIGHT)
        e.Graphics.DrawString(FormatNumber(grandAmount, 2), COURIER_10B, _
            Brushes.Black, createRectangle(cPrice, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)

        'rQtyOrder = createRectangle(cQtyOrder, cQtyRtn)
        'rQtyRtn = createRectangle(cQtyRtn, cQtySales)
        'rQtySales = createRectangle(cQtySales, cPrice)
        'rPrice = createRectangle(cPrice, cAmount)
        'rAmount = createRectangle(cAmount, BOUND_RIGHT - PADDING_COL)
    End Sub

    Private Sub setRectanglesY()
        rQtyOrder.Y = Y
        rQtyRtn.Y = Y
        rQtySales.Y = Y
        rPrice.Y = Y
        rAmount.Y = Y
        rFilter.Y = Y
    End Sub

    Private Sub resetGroupTotals()
        totalQtyOrdered = 0.0
        totalQtyRtn = 0.0
        totalQtySales = 0.0
        totalAmount = 0.0
    End Sub

    Private Sub updateTotals()
        grandQtyOrdered += totalQtyOrdered
        grandQtyRtn += totalQtyRtn
        grandQtySales += totalQtySales
        grandAmount += totalAmount
    End Sub

End Class
