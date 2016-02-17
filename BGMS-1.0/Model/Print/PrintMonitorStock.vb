Public Class PrintMonitorStock : Inherits PrintWithHeader

    Public items As List(Of _StockMonitor)
    Public shCost, shPrice, shOnHand As Boolean

    Dim cCategory, cStock, cDesc, cCost, cPrice, cOnHand As Integer
    Dim maxLines, currentLines As Integer
    Dim objDesc, currentCategory As String

    Dim rDesc, rCategory, rCost, rPrice, rOnHand As Rectangle

    Public Overrides Sub init()
        report_name = "STOCK MONITORING"

        INDEX = 0
        ROW_HEIGHT = ARIAL_8.GetHeight + 5
        PAGE_COUNT = 0

        Dim cWidth As Integer = CInt(0.12 * WIDTH_MAX)

        cCategory = BOUND_LEFT + PADDING_COL
        cStock = cCategory + CInt(0.15 * WIDTH_MAX)
        cDesc = cStock + CInt(0.15 * WIDTH_MAX)

        cCost = cStock + CInt(0.365 * WIDTH_MAX) + If(shCost, cWidth, 0)
        cCost = cCost + If(shCost, 0, cWidth) + If(shPrice, 0, cWidth) + If(shOnHand, 0, cWidth)

        cPrice = If(shPrice, cCost + cWidth, cCost)
        cOnHand = If(shOnHand, cPrice + cWidth, cPrice)

        rDesc = New Rectangle(cDesc, 0, cCost - cDesc, 100)
        rCategory = New Rectangle(cCategory, 0, cStock - cCategory, 100)

        Dim tmp As Integer = cPrice
        If cCost = cPrice AndAlso cPrice = cOnHand Then
            tmp = BOUND_RIGHT - PADDING_COL
        ElseIf cCost = cPrice Then
            tmp = cOnHand
        End If

        rCost = createRectangle(cCost, tmp)
        rPrice = createRectangle(cPrice, If(cOnHand = cPrice, BOUND_RIGHT - PADDING_COL, cOnHand))
        rOnHand = createRectangle(cOnHand, BOUND_RIGHT - PADDING_COL)

        currentCategory = String.Empty

        BOUND_BOTTOM -= 60
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString("Category", ARIAL_8B, Brushes.Black, cCategory, Y)
        e.Graphics.DrawString("Stock", ARIAL_8B, Brushes.Black, cStock, Y)
        e.Graphics.DrawString("Description", ARIAL_8B, Brushes.Black, cDesc, Y)

        If shCost Then
            e.Graphics.DrawString("Unit Price", ARIAL_8B, Brushes.Black, cCost, Y)
        End If

        If shPrice Then
            e.Graphics.DrawString("Sell Price", ARIAL_8B, Brushes.Black, cPrice, Y)
        End If

        If shOnHand Then
            e.Graphics.DrawString("On Hand", ARIAL_8B, Brushes.Black, cOnHand, Y)
        End If

        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < items.Count
            Y += PADDING_ROW
            maxLines = 0
            currentLines = 0

            If String.IsNullOrEmpty(currentCategory) Then
                currentCategory = items(INDEX).Category.ToUpper

                rCategory.Y = Y
                e.Graphics.DrawString(currentCategory, ARIAL_8, Brushes.Black, rCategory)
                e.Graphics.MeasureString(currentCategory, ARIAL_8, New Size(cStock - cCategory - PADDING_COL, 100), _
                    New StringFormat(), COLS, currentLines)
                maxLines = Math.Max(maxLines, currentLines)
            End If

            e.Graphics.DrawString(items(INDEX).Stock, ARIAL_8, Brushes.Black, cStock, Y)

            rDesc.Y = Y
            objDesc = items(INDEX).Description
            e.Graphics.DrawString(objDesc, ARIAL_8, Brushes.Black, rDesc)
            e.Graphics.MeasureString(objDesc, ARIAL_8, New Size(cCost - cDesc - PADDING_COL, 100), _
                New StringFormat(), COLS, currentLines)
            maxLines = Math.Max(maxLines, currentLines)

            rCost.Y = Y
            If shCost Then
                e.Graphics.DrawString(FormatNumber(items(INDEX).Cost, 2), ARIAL_8, _
               Brushes.Black, rCost, ALIGN_RIGHT)
            End If

            rPrice.Y = Y
            If shPrice Then
                e.Graphics.DrawString(FormatNumber(items(INDEX).Price, 2), ARIAL_8, _
               Brushes.Black, rPrice, ALIGN_RIGHT)
            End If

            rOnHand.Y = Y
            If shOnHand Then
                e.Graphics.DrawString(items(INDEX).OnHand, ARIAL_8, _
               Brushes.Black, rOnHand, ALIGN_RIGHT)
            End If

            INDEX += 1

            If INDEX = items.Count OrElse _
                Not items(INDEX).Category.ToUpper.Equals(currentCategory.ToUpper) Then
                currentCategory = String.Empty
            End If

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                Exit Sub
            End If

            Y += ROW_HEIGHT * maxLines - ((maxLines - 1) * 5)
        End While

        e.HasMorePages = False
    End Sub

    Private Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)

    End Sub

End Class
