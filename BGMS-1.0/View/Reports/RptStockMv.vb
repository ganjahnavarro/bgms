Public Class RptStockMv

    Private Sub RptStockMv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initStocks()
        tbFilter.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbFilter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbFilter.AutoCompleteCustomSource = Controller.stockList

        dateFrom.Value = Util.getInitialDate
        dateTo.Value = DateTime.Today
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
            Not Controller.stockList.Contains(tbFilter.Text.ToUpper) Then
            Util.notifyError("Invalid stock name.")
            Exit Sub
        End If

        Dim additionalCriteria As String = String.Empty
        Dim detail As String = " All Stock."

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            Dim stockName As String = tbFilter.Text.ToUpper
            additionalCriteria = " and s.active = true and ucase(s.name) = '" & stockName & "'"
            detail = " Stock: " & stockName
        End If

        Dim printDoc As New PrintMovement

        printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString & detail

        Dim qry As String = "select s.name as stock, po.date as date, 'PO' as doc, po.documentno as docno, " & _
            "f.name as filter, i.quantity as qty, i.price from purchaseorderitems i, " & _
            "purchaseorders po, stocks s, suppliers f where i.purchaseorderid = po.id " & _
            "and i.stockid = s.id and po.supplierid = f.id and po.posteddate is not null " & _
            " and po.date >= " & Util.inSql(dateFrom.Value) & _
            " and po.date <= " & Util.inSql(dateTo.Value) & _
            additionalCriteria & _
 _
            " union all " & _
 _
            "select s.name as stock, so.date as date, 'SO' as doc, so.documentno as docno, " & _
            "f.name as filter, -1 * i.quantity as qty, i.price from salesorderitems i, " & _
            "salesorders so, stocks s, customers f where i.salesorderid = so.id " & _
            "and i.stockid = s.id and so.customerid = f.id and so.posteddate is not null " & _
            " and so.date >= " & Util.inSql(dateFrom.Value) & _
            " and so.date <= " & Util.inSql(dateTo.Value) & _
            additionalCriteria & _
 _
            " union all " & _
 _
            "select s.name as stock, pr.date as date, 'PR' as doc, pr.documentno as docno, " & _
            "f.name as filter, - 1 * i.quantity as qty, i.price from purchasereturnitems i, " & _
            "purchasereturns pr, stocks s, suppliers f where i.purchasereturnid = pr.id " & _
            "and i.stockid = s.id and pr.supplierid = f.id and pr.posteddate is not null " & _
            " and pr.date >= " & Util.inSql(dateFrom.Value) & _
            " and pr.date <= " & Util.inSql(dateTo.Value) & _
            additionalCriteria & _
 _
            " union all " & _
 _
            "select s.name as stock, sr.date as date, 'SR' as doc, sr.documentno as docno, " & _
            "f.name as filter, i.quantity as qty, i.price from salesreturnitems i, " & _
            "salesreturns sr, stocks s, customers f where i.salesreturnid = sr.id " & _
            "and i.stockid = s.id and sr.customerid = f.id and sr.posteddate is not null " & _
            " and sr.date >= " & Util.inSql(dateFrom.Value) & _
            " and sr.date <= " & Util.inSql(dateTo.Value) & _
            additionalCriteria & _
 _
            " union all " & _
 _
            "select s.name as stock, adj.modifydate as date, 'AD' as doc, concat('AD', adj.id) as docno, " & _
            "'' as filter, adj.quantity as qty, '-1' as price from adjustments adj, stocks s where adj.stockid = s.id " & _
            " and adj.modifydate >= " & Util.inSql(dateFrom.Value) & _
            " and adj.modifydate <= " & Util.inSql(dateTo.Value) & _
            additionalCriteria

        qry += "order by stock, date "

        Using context As New bgmsEntities
            printDoc.items = context.Database _
                .SqlQuery(Of _Movement)(qry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

End Class