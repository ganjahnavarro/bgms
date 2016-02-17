Public Class RptSupplierPy

    Private Sub RptSupplierPy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initSuppliers()
        tbFilter.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbFilter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbFilter.AutoCompleteCustomSource = Controller.supplierList

        dateFrom.Value = Util.getInitialDate
        dateTo.Value = DateTime.Today
        cbSummary.Checked = True
    End Sub

    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If cbSummary.Checked Then
            printSummary()
        Else
            printWithDetail()
        End If
    End Sub

    Private Sub printSummary()
        Dim printDoc As New PrintStatement
        printDoc.report_name = "PAYMENT TO SUPPLIER"
        printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString
        printDoc.fName = "Supplier"

        Dim qry As String = "select s.name, sum(po.totalamount - (po.totalreturned + po.totalpaid)) as balance " & _
            " from purchaseorders po, suppliers s where po.supplierid = s.id " & _
            " and (po.totalamount - (po.totalreturned + po.totalpaid)) > 0 " & _
            " and po.date >= " & Util.inSql(dateFrom.Value) & _
            " and po.date <= " & Util.inSql(dateTo.Value) & _
            " and po.posteddate is not null"

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            qry += " and s.active = true and ucase(s.name) = '" & tbFilter.Text.ToUpper & "'"
        End If

        qry += " group by s.name order by s.name, po.date"

        Using context As New bgmsEntities
            printDoc.items = context.Database _
                .SqlQuery(Of _Statement)(qry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

    Private Sub printWithDetail()
        Dim printDoc As New PrintStatementDetail
        printDoc.report_name = "PAYMENT TO SUPPLIER"
        printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString
        printDoc.filter = "Supplier"
        printDoc.docPrefix = "PO"

        Dim qry As String = "select s.name, (po.totalamount - (po.totalreturned + po.totalpaid)) as balance, " & _
            " po.documentno as docno, po.date" & _
            " from purchaseorders po, suppliers s where po.supplierid = s.id " & _
            " and (po.totalamount - (po.totalreturned + po.totalpaid)) > 0 " & _
            " and po.date >= " & Util.inSql(dateFrom.Value) & _
            " and po.date <= " & Util.inSql(dateTo.Value) & _
            " and po.posteddate is not null "

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            qry += " and s.active = true and ucase(s.name) = '" & tbFilter.Text.ToUpper & "'"
        End If

        qry += " order by s.name, po.date"

        Using context As New bgmsEntities
            printDoc.items = context.Database _
                .SqlQuery(Of _StatementDetail)(qry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

End Class