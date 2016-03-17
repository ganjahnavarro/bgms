Public Class RptPurchaseMn

    Dim groupBy As String

    Private Sub RptPurchaseMn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initStocks()
        Controller.initSuppliers()

        tbSupplier.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbSupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbSupplier.AutoCompleteCustomSource = Controller.supplierList

        tbStock.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbStock.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbStock.AutoCompleteCustomSource = Controller.stockList

        dateFrom.Value = Util.getInitialDate
        dateTo.Value = DateTime.Today.AddDays(7)
        groupBy = "Stock"
    End Sub

    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub tabSupplierClick(sender As Object, e As EventArgs) Handles tabSupplier.Click
        tabSupplier.BackColor = Color.Chocolate
        tabSupplier.ForeColor = Color.White

        tabStock.BackColor = Color.White
        tabStock.ForeColor = Color.Chocolate

        groupBy = "Supplier"
        tbSupplier.AutoCompleteCustomSource = Controller.supplierList
    End Sub

    Private Sub tabStockClick(sender As Object, e As EventArgs) Handles tabStock.Click
        tabStock.BackColor = Color.Chocolate
        tabStock.ForeColor = Color.White

        tabSupplier.BackColor = Color.White
        tabSupplier.ForeColor = Color.Chocolate

        groupBy = "Stock"
        tbSupplier.AutoCompleteCustomSource = Controller.stockList
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If Not String.IsNullOrWhiteSpace(tbSupplier.Text) Then
            If Not Controller.supplierList.Contains(tbSupplier.Text.ToUpper) Then
                Util.notifyError("Invalid supplier name.")
                Exit Sub
            End If
        End If

        If Not String.IsNullOrWhiteSpace(tbStock.Text) Then
            If Not Controller.stockList.Contains(tbStock.Text.ToUpper) Then
                Util.notifyError("Invalid stock name.")
                Exit Sub
            End If
        End If

        printPurchases()
    End Sub

    Private Sub printPurchases()
        Dim printDoc As New PrintMonitor
        printDoc.filter = groupBy
        printDoc.report_name = "PURCHASE MONITORING BY " & groupBy.ToUpper

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString

            Dim sel As String = If(groupBy.ToUpper.Equals("STOCK"),
                "select s.name, c.name as filter, concat('UNIT: ', u.name) as unit, ",
                "select c.name, concat(s.name, ' (', u.name, ')') as filter, ")

            '"po.discount1, po.discount2, po.discount3 " & _
            'and (po.totalAmount - (po.totalpaid + po.totalreturned)) <= 0" & _
            Dim qry As String = sel & " po.date, po.documentno as docno, i.quantity " &
                "as qtyordered, i.quantityreturned as qtyrtn, (i.quantity - i.quantityreturned) " &
                "as qtysales, i.price, ((i.quantity - i.quantityreturned) * i.price) as amount " &
                "from purchaseorderitems i, purchaseorders po, stocks s, suppliers c, units u " &
                "where i.purchaseorderid = po.id and i.stockid = s.id and po.supplierid = c.id " &
                "and s.unitid = u.id and po.posteddate is not null " &
                " and po.date >= " & Util.inSql(dateFrom.Value) &
                " and po.date <= " & Util.inSql(dateTo.Value)

            If (Not String.IsNullOrWhiteSpace(tbSupplier.Text)) Then
                qry += " and c.active = true and ucase(c.name) = '" & tbSupplier.Text.ToUpper & "'"
                printDoc.report_detail += " Supplier: " & tbSupplier.Text.ToUpper & "."
            Else
                printDoc.report_detail += " All Supplier."
            End If

            If (Not String.IsNullOrWhiteSpace(tbStock.Text)) Then
                qry += " and s.active = true and ucase(s.name) = '" & tbStock.Text.ToUpper & "'"
                printDoc.report_detail += " Stock: " & tbStock.Text.ToUpper & "."
            Else
                printDoc.report_detail += " All Stock."
            End If

            qry += If(groupBy.ToUpper.Equals("STOCK"),
                      " order by s.name, po.date", " order by c.name, po.date")

            printDoc.items = context.Database.SqlQuery(Of _Monitor)(qry).ToList
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

End Class