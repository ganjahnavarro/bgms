Public Class _Transaction

    Public Property Quantity As Integer
    Public Property Unit As String
    Public Property Stock As String
    Public Property Description As String
    Public Property Price As Double
    Public Property Discount1 As Double
    Public Property Discount2 As Double
    Public Property Discount3 As Double

    Public Function getAmount() As Double
        Return Price * Quantity
    End Function

    Public Function getDiscountedAmount() As Double
        Dim amount As Double = Price * Quantity

        If Not IsNothing(Discount1) And Not Discount1 = 0 Then
            amount = amount - ((Discount1 / 100) * amount)
        End If

        If Not IsNothing(Discount2) And Not Discount2 = 0 Then
            amount = amount - ((Discount2 / 100) * amount)
        End If

        If Not IsNothing(Discount3) And Not Discount3 = 0 Then
            amount = amount - ((Discount3 / 100) * amount)
        End If

        Return amount
    End Function

    Public Function getDiscountDisplay() As String
        Return getDiscountDisplay(True)
    End Function

    Public Function getDiscountDisplay(ByVal withPrefix As Boolean) As String
        Dim display = String.Empty
        Dim prefix = If(withPrefix, "L", String.Empty)

        display = If(Discount1 > 0, prefix & Discount1 & "%", String.Empty)
        display &= If(Discount2 > 0, " " & prefix & Discount2 & "%", String.Empty)
        display &= If(Discount3 > 0, " " & prefix & Discount3 & "%", String.Empty)

        Return display
    End Function

End Class
