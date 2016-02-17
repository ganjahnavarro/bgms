Imports System.Drawing
Imports System.Drawing.Printing

Public Class PrintStatementDetail : Inherits Printing.PrintDocument

    Public items As List(Of _StatementDetail)
    Public report_name, report_detail, filter, docPrefix As String

    Private currentObj As String
    Private total As Double

    Private cDoc, cDateDoc, cBalanceEnd As Integer
    Private cDoc2, cDateDoc2, cBalanceEnd2 As Integer

    Private Y, WIDTH_MAX, WIDTH_HALF, ROW_HEIGHT, INDEX, PAGE_COUNT, _
        BOUND_TOP, BOUND_LEFT, BOUND_RIGHT, BOUND_BOTTOM As Integer

    Private ARIAL_10, ARIAL_12B, ARIAL_13B, COURIER_10, COURIER_10B, COURIER_12 As Font
    Private ALIGN_CENTER As New StringFormat
    Private ALIGN_RIGHT As New StringFormat

    Protected Overrides Sub OnBeginPrint(e As PrintEventArgs)
        MyBase.OnBeginPrint(e)
        initVariables()
    End Sub

    Private Sub initVariables()
        total = 0
        DefaultPageSettings.Landscape = True

        ARIAL_10 = New Font("Arial", 10)
        ARIAL_12B = New Font("Arial", 12, FontStyle.Bold)
        ARIAL_13B = New Font("Arial", 13, FontStyle.Bold)

        COURIER_10 = New Font("Courier New", 10)
        COURIER_10B = New Font("Courier New", 10, FontStyle.Bold)
        COURIER_12 = New Font("Courier New", 12)

        INDEX = 0
        ROW_HEIGHT = 24
        PAGE_COUNT = 0

        With Me.DefaultPageSettings.PrintableArea
            BOUND_TOP = .Left
            BOUND_BOTTOM = .Right
            BOUND_LEFT = .Top
            BOUND_RIGHT = .Bottom
            WIDTH_MAX = .Height
        End With

        WIDTH_HALF = WIDTH_MAX / 2

        ALIGN_CENTER.Alignment = StringAlignment.Center
        ALIGN_RIGHT.Alignment = StringAlignment.Far

        initColumnWidths()
        'fRectangle = New Rectangle(cDesc, 0, cPrice - cDesc, ARIAL_9.GetHeight)
    End Sub

    Private Sub initColumnWidths()
        cDoc = BOUND_LEFT + 5
        cDateDoc = cDoc + 120
        cBalanceEnd = cDateDoc + 250

        cDoc2 = cDoc + WIDTH_HALF
        cDateDoc2 = cDateDoc + WIDTH_HALF
        cBalanceEnd2 = cBalanceEnd + WIDTH_HALF
    End Sub

    Protected Overrides Sub OnPrintPage(e As PrintPageEventArgs)
        MyBase.OnPrintPage(e)

        Y = BOUND_TOP
        PAGE_COUNT += 1
        printDoc(e)
    End Sub

    Private Sub printDoc(ByRef e As Printing.PrintPageEventArgs)
        While INDEX < items.Count
            If String.IsNullOrEmpty(currentObj) Then
                currentObj = items(INDEX).Name
                drawHeader(e)
            End If

            'Draw Items
            e.Graphics.DrawString(items(INDEX).DocNo, COURIER_12, Brushes.Black, cDoc, Y)
            e.Graphics.DrawString(items(INDEX).DocNo, COURIER_12, Brushes.Black, cDoc2, Y)

            e.Graphics.DrawString(Format(items(INDEX).Date, Constants.DATE_FORMAT), _
                COURIER_12, Brushes.Black, cDateDoc, Y)
            e.Graphics.DrawString(Format(items(INDEX).Date, Constants.DATE_FORMAT), _
                COURIER_12, Brushes.Black, cDateDoc2, Y)

            e.Graphics.DrawString(FormatNumber(items(INDEX).Balance, 2), COURIER_12, Brushes.Black, _
                createRectangle(cDoc, cBalanceEnd), ALIGN_RIGHT)
            e.Graphics.DrawString(FormatNumber(items(INDEX).Balance, 2), COURIER_12, Brushes.Black, _
                createRectangle(cDoc2, cBalanceEnd2), ALIGN_RIGHT)

            total += items(INDEX).Balance

            INDEX += 1

            If INDEX = items.Count OrElse _
                Not items(INDEX).Name.Equals(currentObj) Then
                drawTotalAndReceivedBy(e)

                If INDEX < items.Count Then
                    e.HasMorePages = True
                    Exit Sub
                End If
            End If

            If Y > BOUND_BOTTOM - 50 Then
                e.HasMorePages = True
                Exit Sub
            End If

            Y += ROW_HEIGHT
        End While

        e.HasMorePages = False
    End Sub

    Private Sub drawHeader(ByRef e As Printing.PrintPageEventArgs)
        e.Graphics.DrawLine(Pens.Black, cDoc, Y, cBalanceEnd, Y)
        e.Graphics.DrawLine(Pens.Black, cDoc2, Y, cBalanceEnd2, Y)
        Y += 5

        e.Graphics.DrawString(Constants.COMPANY_NAME, ARIAL_13B, _
               Brushes.Black, createRectangle(cDoc, cBalanceEnd), ALIGN_CENTER)
        e.Graphics.DrawString(Constants.COMPANY_NAME, ARIAL_13B, _
               Brushes.Black, createRectangle(cDoc2, cBalanceEnd2), ALIGN_CENTER)

        Y += ROW_HEIGHT
        e.Graphics.DrawString(report_name, ARIAL_10, _
               Brushes.Black, createRectangle(cDoc, cBalanceEnd), ALIGN_CENTER)
        e.Graphics.DrawString(report_name, ARIAL_10, _
               Brushes.Black, createRectangle(cDoc2, cBalanceEnd2), ALIGN_CENTER)

        Y += ROW_HEIGHT
        e.Graphics.DrawString(report_detail, COURIER_10, _
               Brushes.Black, createRectangle(cDoc, cBalanceEnd), ALIGN_CENTER)
        e.Graphics.DrawString(report_detail, COURIER_10, _
               Brushes.Black, createRectangle(cDoc2, cBalanceEnd2), ALIGN_CENTER)

        Y += ROW_HEIGHT
        e.Graphics.DrawString(filter & ": " & items(INDEX).Name, ARIAL_12B, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString(filter & ": " & items(INDEX).Name, ARIAL_12B, Brushes.Black, cDoc2, Y)

        Y += ROW_HEIGHT + 5
        e.Graphics.DrawString(docPrefix & " No.", COURIER_10B, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString(docPrefix & " No.", COURIER_10B, Brushes.Black, cDoc2, Y)

        e.Graphics.DrawString(docPrefix & " Date", COURIER_10B, Brushes.Black, cDateDoc, Y)
        e.Graphics.DrawString(docPrefix & " Date", COURIER_10B, Brushes.Black, cDateDoc2, Y)

        e.Graphics.DrawString("Balance", COURIER_10B, Brushes.Black, _
            createRectangle(cDoc, cBalanceEnd), ALIGN_RIGHT)
        e.Graphics.DrawString("Balance", COURIER_10B, Brushes.Black, _
            createRectangle(cDoc2, cBalanceEnd2), ALIGN_RIGHT)
        Y += ROW_HEIGHT

        e.Graphics.DrawLine(Pens.Black, cDoc, Y, cBalanceEnd, Y)
        e.Graphics.DrawLine(Pens.Black, cDoc2, Y, cBalanceEnd2, Y)
        Y += 5
    End Sub

    Private Sub drawTotalAndReceivedBy(ByRef e As Printing.PrintPageEventArgs)
        Y += ROW_HEIGHT
        e.Graphics.DrawLine(Pens.Black, cDoc, Y, cBalanceEnd, Y)
        e.Graphics.DrawLine(Pens.Black, cDoc2, Y, cBalanceEnd2, Y)
        Y += 5

        e.Graphics.DrawString("Total: ", COURIER_10B, Brushes.Black, cDateDoc + 40, Y)
        e.Graphics.DrawString("Total: ", COURIER_10B, Brushes.Black, cDateDoc2 + 40, Y)

        e.Graphics.DrawString(FormatNumber(total, 2), COURIER_12, Brushes.Black, _
                createRectangle(cDoc, cBalanceEnd), ALIGN_RIGHT)
        e.Graphics.DrawString(FormatNumber(total, 2), COURIER_12, Brushes.Black, _
                createRectangle(cDoc2, cBalanceEnd2), ALIGN_RIGHT)

        Y += (ROW_HEIGHT * 2)
        e.Graphics.DrawString(Constants.RECEIVED_BY, COURIER_10B, Brushes.Black, cDoc, Y)
        e.Graphics.DrawString(Constants.RECEIVED_BY, COURIER_10B, Brushes.Black, cDoc2, Y)

        total = 0.0
        currentObj = String.Empty
    End Sub

    Private Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

End Class
