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

Partial Public Class activity
    Public Property Id As Integer
    Public Property Description As String
    Public Property [Date] As Date

    Public Sub New(ByVal description As String)
        Me.Description = description
        Me.Date = DateTime.Today
    End Sub

End Class
