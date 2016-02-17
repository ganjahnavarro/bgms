Imports System.Drawing
Imports System.Drawing.Printing

Public MustInherit Class PrintWithHeader : Inherits PrintDocument

    Protected Y, WIDTH_MAX, ROW_HEIGHT, PADDING_COL, PADDING_ROW, COLS As Integer
    Protected BOUND_TOP, BOUND_LEFT, BOUND_RIGHT, BOUND_BOTTOM As Integer
    Protected INDEX, ITEM_INDEX, PAGE_COUNT As Integer
    Protected DATE_STRING As String

    Protected DASHES As Single() = {17, 6}
    Protected DASHED_GRAY_PEN As New Pen(Brushes.Gray)

    Protected ARIAL_8 As New Font("Arial", 8)
    Protected ARIAL_10 As New Font("Arial", 10)
    Protected ARIAL_8B As New Font("Arial", 8, FontStyle.Bold)
    Protected COURIER_8 As New Font("Courier New", 8)
    Protected COURIER_10 As New Font("Courier New", 10)
    Protected COURIER_10B As New Font("Courier New", 10, FontStyle.Bold)
    Protected COURIER_12B As New Font("Courier New", 12, FontStyle.Bold)
    Protected COURIER_14BI As New Font("Courier New", 14, FontStyle.Bold Or FontStyle.Italic)
    Protected ALIGN_CENTER As New StringFormat
    Protected ALIGN_RIGHT As New StringFormat

    Public company_name As String = Constants.COMPANY_NAME
    Public report_name As String = String.Empty
    Public report_detail As String = String.Empty

    Protected Overrides Sub OnBeginPrint(e As PrintEventArgs)
        MyBase.OnBeginPrint(e)

        ALIGN_CENTER.Alignment = StringAlignment.Center
        ALIGN_RIGHT.Alignment = StringAlignment.Far
        DASHED_GRAY_PEN.DashPattern = DASHES

        INDEX = 0
        ITEM_INDEX = 0
        ROW_HEIGHT = 0
        PAGE_COUNT = 0
        PADDING_COL = 4
        PADDING_ROW = 4

        With Me.DefaultPageSettings.PrintableArea
            BOUND_TOP = .Top
            BOUND_BOTTOM = .Bottom
            BOUND_LEFT = .Left
            BOUND_RIGHT = .Right
            WIDTH_MAX = .Width
        End With

        DATE_STRING = Format(DateTime.Now, "MM/dd/yyyy hh:mm:ss tt")
        init()
    End Sub

    Protected Overrides Sub OnPrintPage(e As PrintPageEventArgs)
        MyBase.OnPrintPage(e)
        If ROW_HEIGHT = 0 Then
            ROW_HEIGHT = ARIAL_8.GetHeight(e.Graphics)
            BOUND_BOTTOM = BOUND_BOTTOM - 50
        End If

        PAGE_COUNT += 1
        printHeader(e)
        printTable(e)
        printFooter(e)

        If e.HasMorePages = False Then
            Y += (ROW_HEIGHT * 2)
            e.Graphics.DrawString("*** END OF REPORT ***", COURIER_10, _
                 Brushes.Black, New Rectangle(BOUND_LEFT, Y, WIDTH_MAX, 20), ALIGN_CENTER)
        End If
    End Sub

    Private Sub printHeader(ByRef e As Printing.PrintPageEventArgs)
        Y = BOUND_TOP + 22

        e.Graphics.DrawString(DATE_STRING, ARIAL_8, Brushes.DimGray, BOUND_LEFT, Y)
        e.Graphics.DrawString("page : " & PAGE_COUNT, COURIER_8, _
            Brushes.DimGray, New Rectangle(BOUND_RIGHT - 100, Y, 100, ROW_HEIGHT), ALIGN_RIGHT)
        Y += 8

        e.Graphics.DrawString(company_name, COURIER_12B, _
            Brushes.Black, New Rectangle(BOUND_LEFT, Y, WIDTH_MAX, 20), ALIGN_CENTER)
        Y += 18

        e.Graphics.DrawString(report_name, COURIER_14BI, _
            Brushes.Black, New Rectangle(BOUND_LEFT, Y, WIDTH_MAX, 20), ALIGN_CENTER)
        Y += 20

        e.Graphics.DrawString(report_detail, COURIER_10, _
            Brushes.Black, New Rectangle(BOUND_LEFT, Y, WIDTH_MAX, 20), ALIGN_CENTER)
        Y += 26
    End Sub

    Private Sub printTable(ByRef e As Printing.PrintPageEventArgs)
        printTableHeader(e)
        printTableBody(e)
    End Sub

    Public MustOverride Sub init()

    Public MustOverride Sub printFooter(ByRef e As Printing.PrintPageEventArgs)

    Public MustOverride Sub printTableBody(ByRef e As Printing.PrintPageEventArgs)

    Public MustOverride Sub printTableHeader(ByRef e As Printing.PrintPageEventArgs)

End Class
