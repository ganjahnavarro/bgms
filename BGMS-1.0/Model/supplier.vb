'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class supplier
    Public Property Id As Integer
    Public Property Name As String
    Public Property Address As String
    Public Property Contact As String
    Public Property Fax As String
    Public Property Tin As String
    Public Property ModifyBy As String
    Public Property ModifyDate As Nullable(Of Date)
    Public Property Active As Boolean

    Public Overridable Property purchaseorders As ICollection(Of purchaseorder) = New HashSet(Of purchaseorder)
    Public Overridable Property purchasereturns As ICollection(Of purchasereturn) = New HashSet(Of purchasereturn)
    Public Overridable Property supplierpayments As ICollection(Of supplierpayment) = New HashSet(Of supplierpayment)

End Class
