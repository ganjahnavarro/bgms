Public Class PrintMovement : Inherits PrintWithHeader

    Public items As List(Of _Movement)

    Private cStock, cDateMv, cDoc, cName, cQty, cPrice, cDisc, cTotalLabel,
        stockTotal As Integer
    Private rQty, rPrice As Rectangle
    Private currentStock As String
    Private prevPrice As Double

    Public Overrides Sub init()
        report_name = "STOCK MOVEMENT"

        cStock = BOUND_LEFT + PADDING_COL
        cDateMv = cStock + 40
        cDoc = cDateMv + CInt(0.14 * WIDTH_MAX)
        cName = cDoc + CInt(0.15 * WIDTH_MAX)
        cDisc = cName + CInt(0.325 * WIDTH_MAX)
        cQty = cDisc + CInt(0.125 * WIDTH_MAX)
        cPrice = cQty + CInt(0.07 * WIDTH_MAX)

        cTotalLabel = cName + CInt(0.25 * WIDTH_MAX)
        rQty = createRectangle(cQty, cPrice)
        rPrice = createRectangle(cPrice, BOUND_RIGHT - PADDING_COL)

        prevPrice = 0.0
        stockTotal = 0
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        rQty.Y = Y
        rPrice.Y = Y

        e.Graphics.DrawString("STOCK    DATE", COURIER_10B, Brushes.Black, cStock, Y)
        e.Graphics.DrawString("DOC NO", COURIER_10B, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString("SUPPLIER/CUSTOMER", COURIER_10B, Brushes.Black, cName, Y)
        e.Graphics.DrawString("QTY", COURIER_10B, Brushes.Black, rQty, ALIGN_RIGHT)
        e.Graphics.DrawString("PRICE", COURIER_10B, Brushes.Black, rPrice, ALIGN_RIGHT)
        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < items.Count
            Y += PADDING_ROW
            drawItems(e)

            INDEX += 1

            If INDEX = items.Count OrElse _
                Not items(INDEX).Stock.Equals(currentStock) Then
                drawTotalForStock(e)
            End If

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                currentStock = String.Empty
                Exit Sub
            End If

            Y += ROW_HEIGHT
        End While

        e.HasMorePages = False
    End Sub

    Private Sub drawItems(ByRef e As Printing.PrintPageEventArgs)
        Dim item As _Movement = items(INDEX)

        If String.IsNullOrEmpty(currentStock) Then
            currentStock = item.Stock
            e.Graphics.DrawString(currentStock, COURIER_10, Brushes.Black, cStock, Y)
            Y += ROW_HEIGHT + PADDING_COL
        End If

        e.Graphics.DrawString(Format(item.Date, Constants.DATE_FORMAT),
            COURIER_10, Brushes.Black, cDateMv, Y)
        e.Graphics.DrawString(item.Doc & ": " & item.DocNo, COURIER_10, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString(item.Filter, COURIER_10, Brushes.Black, cName, Y)
        e.Graphics.DrawString(item.getDiscountDisplay(True), COURIER_10, Brushes.Black, cDisc, Y)

        rQty.Y = Y
        rPrice.Y = Y
        e.Graphics.DrawString(item.Qty, COURIER_10, Brushes.Black, rQty, ALIGN_RIGHT)

        'Less than 0. (-1) For adjustments. Need same column numbers on union
        If item.Price < 0 Then
            e.Graphics.DrawString(FormatNumber(prevPrice, 2), COURIER_10B, Brushes.Black,
                rPrice, ALIGN_RIGHT)
        Else
            e.Graphics.DrawString(FormatNumber(item.Price, 2), COURIER_10B, Brushes.Black,
                rPrice, ALIGN_RIGHT)
            prevPrice = item.Price
        End If

        stockTotal += item.Qty
    End Sub

    Private Sub drawTotalForStock(ByRef e As Printing.PrintPageEventArgs)
        Y += ROW_HEIGHT + (PADDING_ROW * 2)
        e.Graphics.DrawLine(Pens.Black, cQty, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += PADDING_ROW
        rQty.Y = Y
        e.Graphics.DrawString("STOCK BALANCE:", COURIER_10B, Brushes.Black, cTotalLabel, Y)
        e.Graphics.DrawString(stockTotal, COURIER_10, Brushes.Black, rQty, ALIGN_RIGHT)

        Y += ROW_HEIGHT + (PADDING_ROW * 2)
        e.Graphics.DrawLine(Pens.Black, cStock, Y, BOUND_RIGHT - PADDING_COL, Y)
        Y = Y - ROW_HEIGHT

        currentStock = String.Empty
        stockTotal = 0
    End Sub

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)
    End Sub

    Private Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

End Class
