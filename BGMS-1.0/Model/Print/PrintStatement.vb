Public Class PrintStatement : Inherits PrintWithHeader

    Public items As List(Of _Statement)
    Public fName As String

    Dim currentKey As String
    Dim grandTotal, curBalance As Double
    Dim cName, cBalance As Integer

    Public Overrides Sub init()
        Dim extraPadding As Integer = 100
        BOUND_RIGHT = BOUND_RIGHT - extraPadding
        BOUND_LEFT = BOUND_LEFT + extraPadding
        WIDTH_MAX = WIDTH_MAX - (extraPadding * 2)

        cName = BOUND_LEFT + PADDING_COL
        cBalance = cName + CInt(0.7 * WIDTH_MAX)
        grandTotal = 0.0
        curBalance = 0.0
    End Sub

    Public Overrides Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawString(fName, COURIER_10B, Brushes.Black, cName, Y)
        e.Graphics.DrawString("Balance", COURIER_10B, _
               Brushes.Black, createRectangle(cBalance, BOUND_RIGHT - PADDING_COL), _
               ALIGN_RIGHT)

        Y += ROW_HEIGHT + 3

        e.Graphics.DrawLine(Pens.Black, BOUND_LEFT, Y, BOUND_RIGHT, Y)
    End Sub

    Public Overrides Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < items.Count
            Y += PADDING_ROW

            curBalance = items(INDEX).Balance
            e.Graphics.DrawString(items(INDEX).Name, COURIER_10, Brushes.Black, cName, Y)
            e.Graphics.DrawString(FormatNumber(curBalance, 2), COURIER_10, Brushes.Black, _
                createRectangle(cName, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)

            grandTotal += curBalance
            INDEX += 1

            If Y > BOUND_BOTTOM Then
                e.HasMorePages = True
                Exit Sub
            End If

            Y += ROW_HEIGHT + PADDING_ROW
        End While

        drawGrandTotal(e)
        e.HasMorePages = False
    End Sub

    Private Sub drawGrandTotal(ByRef e As Printing.PrintPageEventArgs)
        Y += PADDING_ROW
        e.Graphics.DrawLine(Pens.Black, cName, Y, BOUND_RIGHT - PADDING_COL, Y)

        Y += (PADDING_ROW * 2)
        e.Graphics.DrawString("GRAND TOTAL: ", ARIAL_10, Brushes.Black, _
            (cName + (0.5 * WIDTH_MAX)), Y)
        e.Graphics.DrawString(FormatNumber(grandTotal, 2), ARIAL_10, Brushes.Black, _
                createRectangle(cName, BOUND_RIGHT - PADDING_COL), ALIGN_RIGHT)
    End Sub

    Public Overrides Sub printFooter(ByRef e As Printing.PrintPageEventArgs)
    End Sub

    Private Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

End Class
