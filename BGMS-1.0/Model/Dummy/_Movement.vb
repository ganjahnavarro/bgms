Public Class _Movement

    Public Property Stock As String
    Public Property [Date] As Date
    Public Property Doc As String
    Public Property DocNo As String
    Public Property Filter As String
    Public Property Qty As Integer
    Public Property Price As Double
    Public Property Discount1 As Double
    Public Property Discount2 As Double
    Public Property Discount3 As Double

    Public Function getDiscountDisplay(ByVal withPrefix As Boolean) As String
        Dim display = String.Empty
        Dim prefix = If(withPrefix, "L", String.Empty)

        display = If(Discount1 > 0, prefix & Discount1 & "%", String.Empty)
        display &= If(Discount2 > 0, " " & prefix & Discount2 & "%", String.Empty)
        display &= If(Discount3 > 0, " " & prefix & Discount3 & "%", String.Empty)

        Return display
    End Function

End Class
