Public Class RptSOA

    Private Sub RptSOA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initCustomers()
        tbFilter.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbFilter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbFilter.AutoCompleteCustomSource = Controller.customerList

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
        If Not String.IsNullOrWhiteSpace(tbFilter.Text) AndAlso _
            Not Controller.customerList.Contains(tbFilter.Text.ToUpper) Then
            Util.notifyError("Invalid cutomer name.")
            Exit Sub
        End If

        If cbSummary.Checked Then
            printSummary()
        Else
            printWithDetail()
        End If
    End Sub

    Private Sub printSummary()
        Dim printDoc As New PrintStatement
        printDoc.report_name = "STATEMENT OF ACCOUNT"
        printDoc.fName = "Customer"
        printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString

        Dim qry As String = "select c.name, sum(so.totalamount - (so.totalreturned + so.totalpaid)) as balance " & _
            " from salesorders so, customers c where so.customerid = c.id " & _
            " and (so.totalamount - (so.totalreturned + so.totalpaid)) > 0 " & _
            " and so.date >= " & Util.inSql(dateFrom.Value) & _
            " and so.date <= " & Util.inSql(dateTo.Value) & _
            " and so.posteddate is not null "

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            qry += " and c.active = true and ucase(c.name) = '" & tbFilter.Text.ToUpper & "'"
        End If

        qry += " group by c.name order by c.name, so.date"

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
        printDoc.report_name = "STATEMENT OF ACCOUNT"
        printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString
        printDoc.filter = "Customer"
        printDoc.docPrefix = "SO"

        Dim qry As String = "select c.name, (so.totalamount - (so.totalreturned + so.totalpaid)) as balance, " & _
            " so.documentno as docno, so.date" & _
            " from salesorders so, customers c where so.customerid = c.id " & _
            " and (so.totalamount - (so.totalreturned + so.totalpaid)) > 0 " & _
            " and so.date >= " & Util.inSql(dateFrom.Value) & _
            " and so.date <= " & Util.inSql(dateTo.Value) & _
            " and so.posteddate is not null "

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            qry += " and c.active = true and ucase(c.name) = '" & tbFilter.Text.ToUpper & "'"
        End If

        qry += " order by c.name, so.date"

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