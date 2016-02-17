Public Class _TransactionDetail : Inherits _TransactionList

    Public Property Stock As String
    Public Property Quantity As Integer
    Public Property Unit As String
    Public Property ItemDiscount1 As Double
    Public Property ItemDiscount2 As Double
    Public Property ItemDiscount3 As Double
    Public Property Price As Double

    Public Property xDetail As String
    Public Property xDate As Date
    Public Property xBank As String
    Public Property xAmount As Double

    Public Function getDiscountedPrice() As Double
        Dim p As Double = Price

        If Not IsNothing(ItemDiscount1) And Not ItemDiscount1 = 0 Then
            p = p - ((ItemDiscount1 / 100) * p)
        End If

        If Not IsNothing(ItemDiscount2) And Not ItemDiscount2 = 0 Then
            p = p - ((ItemDiscount2 / 100) * p)
        End If

        If Not IsNothing(ItemDiscount3) And Not ItemDiscount3 = 0 Then
            p = p - ((ItemDiscount3 / 100) * p)
        End If

        Return p
    End Function

    Public Function getDiscountedAmount() As Double
        Dim amount As Double = Price * Quantity

        If Not IsNothing(ItemDiscount1) And Not ItemDiscount1 = 0 Then
            amount = amount - ((ItemDiscount1 / 100) * amount)
        End If

        If Not IsNothing(ItemDiscount2) And Not ItemDiscount2 = 0 Then
            amount = amount - ((ItemDiscount2 / 100) * amount)
        End If

        If Not IsNothing(ItemDiscount3) And Not ItemDiscount3 = 0 Then
            amount = amount - ((ItemDiscount3 / 100) * amount)
        End If

        Return amount
    End Function

End Class
