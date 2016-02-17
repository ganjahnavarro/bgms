Public Class _Monitor

    Public Property Name As String
    Public Property Filter As String
    Public Property [Date] As Date
    Public Property DocNo As String
    Public Property Discount1 As Double
    Public Property Discount2 As Double
    Public Property Discount3 As Double
    Public Property Unit As String
    Public Property QtyOrdered As Integer
    Public Property QtyReturned As Integer
    Public Property QtySales As Integer
    Public Property Price As Double
    Public Property Amount As Double

    Public Function getDiscountedPrice() As Double
        Dim p As Double = Price

        If Not IsNothing(Discount1) And Not Discount1 = 0 Then
            p = p - ((Discount1 / 100) * p)
        End If

        If Not IsNothing(Discount2) And Not Discount2 = 0 Then
            p = p - ((Discount2 / 100) * p)
        End If

        If Not IsNothing(Discount3) And Not Discount3 = 0 Then
            p = p - ((Discount3 / 100) * p)
        End If

        Return p
    End Function

    Public Function getDiscountDisplay() As String
        Dim display = String.Empty

        display = If(Discount1 > 0, " L" & FormatNumber(Discount1, 2) & "%", String.Empty)
        display &= If(Discount2 > 0, " L" & FormatNumber(Discount2, 2) & "%", String.Empty)
        display &= If(Discount3 > 0, " L" & FormatNumber(Discount3, 2) & "%", String.Empty)

        Return display
    End Function

End Class
