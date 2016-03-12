Imports System.Drawing
Imports System.Drawing.Printing

Public Class PrintTransaction : Inherits PrintDocument

    Public items As List(Of _Transaction)
    Public transactionDict As Dictionary(Of String, List(Of _Transaction))
    Public name, address, agent, docNo, stringDate As String
    Public docDate As Date

    Private cQty, cUnit, cStock, cDesc, cPrice, cDiscs, cAmount As Integer
    Private Y, WIDTH_MAX, ROW_HEIGHT, INDEX, PAGE_COUNT, _
        BOUND_TOP, BOUND_LEFT, BOUND_RIGHT, BOUND_BOTTOM, RIGHT_PADDING As Integer

    Private rDesc As Rectangle

    Protected DASHES As Single() = {20, 5}
    Protected DASHED_PEN As New Pen(Brushes.Black)

    Private DATE_FORMAT As String = "dd MMMM yyyy"
    Private ARIAL_9, ARIAL_11, ARIAL_13B, COURIER_10B As Font

    Private ALIGN_CENTER As New StringFormat
    Private ALIGN_RIGHT As New StringFormat

    Private totalAmount, totalDiscounted As Double
    Private isDiscountPerItem As Boolean
    Private discountCount As Integer

    Protected Overrides Sub OnBeginPrint(e As PrintEventArgs)
        MyBase.OnBeginPrint(e)
        initVariables()
    End Sub

    Private Sub initVariables()
        totalAmount = 0
        totalDiscounted = 0
        transactionDict = New Dictionary(Of String, List(Of _Transaction))

        checkItemDiscounts()

        DASHED_PEN.DashPattern = DASHES
        RIGHT_PADDING = 20

        ARIAL_9 = New Font("Arial", 9.8)
        ARIAL_11 = New Font("Arial", 11)
        ARIAL_13B = New Font("Arial", 13, FontStyle.Bold)
        COURIER_10B = New Font("Courier New", 10, FontStyle.Bold)

        INDEX = 0
        ROW_HEIGHT = 24
        PAGE_COUNT = 0

        With Me.DefaultPageSettings.PrintableArea
            BOUND_TOP = .Top
            BOUND_BOTTOM = .Bottom
            BOUND_LEFT = .Left
            BOUND_RIGHT = .Right
            WIDTH_MAX = .Width
        End With

        BOUND_RIGHT -= 25

        ALIGN_CENTER.Alignment = StringAlignment.Center
        ALIGN_RIGHT.Alignment = StringAlignment.Far

        initColumnWidths()
        rDesc = New Rectangle(cDesc, 0, cPrice - cDesc, ARIAL_9.GetHeight)

        stringDate = If(IsNothing(docDate), Format(DateTime.Now, DATE_FORMAT), _
                     Format(docDate, DATE_FORMAT))
    End Sub

    Private Sub initColumnWidths()
        cQty = BOUND_LEFT + 5
        cUnit = cQty + CInt(0.071 * WIDTH_MAX)
        cStock = cUnit + CInt(0.085 * WIDTH_MAX)
        cDesc = cStock + CInt(0.175 * WIDTH_MAX)
        cPrice = cDesc + CInt(0.366 * WIDTH_MAX)
        cDiscs = cPrice + CInt(0.1 * WIDTH_MAX)
        cAmount = cDiscs + CInt(0.06 * WIDTH_MAX)
    End Sub

    Private Sub getGroupedDiscounts()
        For Each transac In items
            transac.sortDiscount()
            Dim key As String = transac.Discount1.ToString + "-" + transac.Discount2.ToString + "-" + transac.Discount3.ToString
            If transactionDict.ContainsKey(key) Then
                transactionDict.Item(key).Add(transac)
            Else
                Dim list As New List(Of _Transaction)
                list.Add(transac)
                transactionDict.Add(key, list)
            End If
        Next
    End Sub


    Private Sub checkItemDiscounts()
        isDiscountPerItem = False

        Dim disc1 As Double = items(0).Discount1
        Dim disc2 As Double = items(0).Discount2
        Dim disc3 As Double = items(0).Discount3

        Dim foundDisc1 As Integer = 0
        Dim foundDisc2 As Integer = 0
        Dim foundDisc3 As Integer = 0

        For Each item In items
            If item.Discount1 <> disc1 Then
                isDiscountPerItem = True
            ElseIf item.Discount2 <> disc2 Then
                isDiscountPerItem = True
            ElseIf item.Discount3 <> disc3 Then
                isDiscountPerItem = True
            End If

            If Not FormatNumber(item.Discount1, 2).Equals(FormatNumber(0, 2)) Then
                foundDisc1 = 1
            End If

            If Not FormatNumber(item.Discount2, 2).Equals(FormatNumber(0, 2)) Then
                foundDisc2 = 1
            End If

            If Not FormatNumber(item.Discount3, 2).Equals(FormatNumber(0, 2)) Then
                foundDisc3 = 1
            End If
        Next

        discountCount = foundDisc1 + foundDisc2 + foundDisc3

        totalAmount = 0
        totalDiscounted = 0
    End Sub

    Protected Overrides Sub OnPrintPage(e As PrintPageEventArgs)
        MyBase.OnPrintPage(e)

        Y = BOUND_TOP
        PAGE_COUNT += 1
        printHeader(e)
        printBody(e)

        drawTotal(e)
    End Sub

    Private Sub printHeader(ByRef e As Printing.PrintPageEventArgs)
        addY(35)
        e.Graphics.DrawString("No.:" & docNo, COURIER_10B, _
               Brushes.Black, createRectangle(cDesc, cPrice - 20), ALIGN_RIGHT)

        addY(25)
        e.Graphics.DrawString(stringDate, COURIER_10B, _
               Brushes.Black, createRectangle(cPrice, BOUND_RIGHT - RIGHT_PADDING + 20), ALIGN_CENTER)

        addY(48)
        e.Graphics.DrawString(name, ARIAL_13B, _
                Brushes.Black, createRectangle(cQty, cPrice - 20), ALIGN_CENTER)
        Y = Y - 8

        addY(50)
        e.Graphics.DrawString(address, COURIER_10B, _
                Brushes.Black, createRectangle(cQty, cPrice - 20), ALIGN_CENTER)

        addY(40)
        e.Graphics.DrawString(agent, COURIER_10B, _
               Brushes.Black, createRectangle(cPrice, BOUND_RIGHT - RIGHT_PADDING + 20), ALIGN_CENTER)
    End Sub

    Private Sub printBody(ByRef e As Printing.PrintPageEventArgs)
        isDiscountPerItem = True
        addY(67)
        getGroupedDiscounts()

        For Each pair In transactionDict
            Dim grouptotalAmount = 0
            Dim grouptotalDiscounted = 0
            Dim groupDiscount As String = " "
            For Each item In pair.Value
                e.Graphics.DrawString(item.Quantity, ARIAL_9,
                        Brushes.Black, createRectangle(cQty, cUnit), ALIGN_RIGHT)

                e.Graphics.DrawString(item.Unit, ARIAL_9, Brushes.Black, cUnit, Y)

                e.Graphics.DrawString(item.Stock, ARIAL_9, Brushes.Black, cStock, Y)

                e.Graphics.DrawString(item.Description, ARIAL_9, Brushes.Black,
                getRectangle(rDesc, Y))

                'If isDiscountPerItem Then
                '    e.Graphics.DrawString(item.getDiscountDisplay, ARIAL_9, Brushes.Black, cDiscs + 5, Y)
                'End If

                'e.Graphics.DrawString(FormatNumber(item.Price, 2), ARIAL_9,
                'Brushes.Black, createRectangle(cPrice, cDiscs), ALIGN_RIGHT)

                'If isDiscountPerItem Then
                '    e.Graphics.DrawString(FormatNumber(item.getDiscountedAmount), ARIAL_9,
                'Brushes.Black, createRectangle(cAmount, BOUND_RIGHT - RIGHT_PADDING), ALIGN_RIGHT)
                'Else
                '    e.Graphics.DrawString(FormatNumber(item.getAmount, 2), ARIAL_9,
                '    Brushes.Black, createRectangle(cAmount, BOUND_RIGHT - RIGHT_PADDING), ALIGN_RIGHT)
                'End If
                totalAmount += item.getAmount
                totalDiscounted += item.getDiscountedAmount
                grouptotalAmount += item.getAmount
                grouptotalDiscounted += item.getDiscountedAmount
                groupDiscount = item.getDiscountDisplay
                addY(ROW_HEIGHT)
            Next

            'TODO Format discount display

            e.Graphics.DrawString(FormatNumber(grouptotalDiscounted, 2), ARIAL_9,
            Brushes.Black, createRectangle(cAmount, BOUND_RIGHT - RIGHT_PADDING), ALIGN_RIGHT)

            e.Graphics.DrawString(groupDiscount, ARIAL_9, Brushes.Black, cDiscs + 5, Y)
            addY(ROW_HEIGHT)
        Next

        e.HasMorePages = False
    End Sub

    Private Sub drawTotal(ByRef e As Printing.PrintPageEventArgs)
        'Dashes
        e.Graphics.DrawLine(DASHED_PEN, cAmount - 20, Y, BOUND_RIGHT - RIGHT_PADDING, Y)
        addY(4)
        e.Graphics.DrawLine(DASHED_PEN, cAmount - 20, Y, BOUND_RIGHT - RIGHT_PADDING, Y)
        addY(4)

        Dim amt = If(isDiscountPerItem, totalDiscounted, totalAmount)
        e.Graphics.DrawString(FormatNumber(amt, 2), ARIAL_11, _
            Brushes.Black, createRectangle(cPrice, BOUND_RIGHT - RIGHT_PADDING), ALIGN_RIGHT)

        If Not isDiscountPerItem AndAlso _
            Not String.IsNullOrWhiteSpace(items(0).getDiscountDisplay) Then
            addY(ROW_HEIGHT)

            e.Graphics.DrawString(items(0).getDiscountDisplay(False), ARIAL_11,
                Brushes.Black, cPrice - 30, Y)
            e.Graphics.DrawString(FormatNumber(totalAmount - totalDiscounted, 2), ARIAL_9, _
                Brushes.Black, createRectangle(cPrice, BOUND_RIGHT - RIGHT_PADDING), ALIGN_RIGHT)

            addY(ROW_HEIGHT)
            e.Graphics.DrawLine(DASHED_PEN, cAmount - 20, Y, BOUND_RIGHT - RIGHT_PADDING, Y)
            addY(4)

            e.Graphics.DrawString(FormatNumber(totalDiscounted, 2), ARIAL_11,
            Brushes.Black, createRectangle(cPrice, BOUND_LEFT - RIGHT_PADDING), ALIGN_RIGHT)
        End If
    End Sub

    Private Function getRectangle(ByVal rectangle As Rectangle, ByVal yPosition As Integer) As Rectangle
        rectangle.Y = yPosition
        Return rectangle
    End Function

    Private Function createRectangle(ByVal xPosition As Integer, ByVal uptoPosition As Integer) As Rectangle
        Return New Rectangle(xPosition, Y, uptoPosition - xPosition, ROW_HEIGHT)
    End Function

    Private Sub addY(ByVal movement As Integer)
        Y += movement
    End Sub

End Class
