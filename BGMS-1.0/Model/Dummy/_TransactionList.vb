Public Class _TransactionList

    Public Property DocumentNo As String
    Public Property [Date] As Date
    Public Property Filter As String
    Public Property TotalAmount As Double
    Public Property Discount1 As Integer
    Public Property Discount2 As Integer
    Public Property Discount3 As Integer

    Public Function getDiscountDisplay() As String
        Dim display = String.Empty

        display = If(Discount1 > 0, " L" & Discount1 & "%", String.Empty)
        display &= If(Discount2 > 0, " L" & Discount2 & "%", String.Empty)
        display &= If(Discount3 > 0, " L" & Discount3 & "%", String.Empty)

        Return display
    End Function

End Class
