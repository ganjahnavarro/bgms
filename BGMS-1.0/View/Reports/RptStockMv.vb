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
            Dim stockName As String = tbFilter.Text.ToUpper.Trim
            additionalCriteria = " and s.active = true and ucase(s.name) = '" & stockName & "'"
            detail = " Stock: " & stockName
        End If

        Dim printDoc As New PrintMovement

        printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString & detail

        Dim qry As String = "select s.name as stock, po.date as date, 'PO' as doc, po.documentno as docno, " &
            "f.name as filter, i.quantity as qty, i.price, i.Discount1, i.Discount2, i.Discount3 from purchaseorderitems i, " &
            "purchaseorders po, stocks s, suppliers f where i.purchaseorderid = po.id " &
            "and i.stockid = s.id and po.supplierid = f.id and po.posteddate is not null " &
            " and po.date >= " & Util.inSql(dateFrom.Value) &
            " and po.date <= " & Util.inSql(dateTo.Value) &
            additionalCriteria & _
 _
            " union all " & _
 _
            "select s.name as stock, so.date as date, 'SO' as doc, so.documentno as docno, " &
            "f.name as filter, -1 * i.quantity as qty, i.price, i.Discount1, i.Discount2, '-1' as Discount3 from salesorderitems i, " &
            "salesorders so, stocks s, customers f where i.salesorderid = so.id " &
            "and i.stockid = s.id and so.customerid = f.id and so.posteddate is not null " &
            " and so.date >= " & Util.inSql(dateFrom.Value) &
            " and so.date <= " & Util.inSql(dateTo.Value) &
            additionalCriteria & _
 _
            " union all " & _
 _
            "select s.name as stock, pr.date as date, 'PR' as doc, pr.documentno as docno, " &
            "f.name as filter, - 1 * i.quantity as qty, i.price, i.Discount1, i.Discount2, i.Discount3 from purchasereturnitems i, " &
            "purchasereturns pr, stocks s, suppliers f where i.purchasereturnid = pr.id " &
            "and i.stockid = s.id and pr.supplierid = f.id and pr.posteddate is not null " &
            " and pr.date >= " & Util.inSql(dateFrom.Value) &
            " and pr.date <= " & Util.inSql(dateTo.Value) &
            additionalCriteria & _
 _
            " union all " & _
 _
            "select s.name as stock, sr.date as date, 'SR' as doc, sr.documentno as docno, " &
            "f.name as filter, i.quantity as qty, i.price, i.Discount1, i.Discount2, '-1' as Discount3 from salesreturnitems i, " &
            "salesreturns sr, stocks s, customers f where i.salesreturnid = sr.id " &
            "and i.stockid = s.id and sr.customerid = f.id and sr.posteddate is not null " &
            " and sr.date >= " & Util.inSql(dateFrom.Value) &
            " and sr.date <= " & Util.inSql(dateTo.Value) &
            additionalCriteria
        '       & _
        '_
        '           " union all " & _
        '_
        '           "select s.name as stock, adj.modifydate as date, 'AD' as doc, concat('AD', adj.id) as docno, " &
        '           "'' as filter, adj.quantity as qty, '-1' as price, '-1' as Discount1, '-1' as Discount2, '-1' as Discount3" &
        '           " from adjustments adj, stocks s where adj.stockid = s.id " &
        '           " and adj.modifydate >= " & Util.inSql(dateFrom.Value) &
        '           " and adj.modifydate <= " & Util.inSql(dateTo.Value) &
        '           additionalCriteria

        qry += "order by stock, date "

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            context.Database.CommandTimeout = 90

            printDoc.items = context.Database _
                .SqlQuery(Of _Movement)(qry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing To Print.")
        End If
    End Sub

End Class