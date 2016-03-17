Public Class RptSalesMn

#Region "init"
    Dim groupBy As String

    Private Sub RptSalesMn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initStocks()
        Controller.initCustomers()
        Controller.initAgents()

        tbStock.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbStock.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbStock.AutoCompleteCustomSource = Controller.stockList

        tbCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbCustomer.AutoCompleteCustomSource = Controller.customerList

        tbAgent.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbAgent.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbAgent.AutoCompleteCustomSource = Controller.agentList

        dateFrom.Value = Util.getInitialDate
        dateTo.Value = DateTime.Today

        groupBy = "Stock"
    End Sub

    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub tabAgentClick(sender As Object, e As EventArgs) Handles tabAgent.Click
        tabAgent.BackColor = Color.Chocolate
        tabAgent.ForeColor = Color.White

        tabCustomer.BackColor = Color.White
        tabCustomer.ForeColor = Color.Chocolate

        tabStock.BackColor = Color.White
        tabStock.ForeColor = Color.Chocolate

        lblStock.Visible = False
        lblFilter.Text = "AGENT"
        groupBy = "Agent"

        tbAgent.Visible = True
        tbCustomer.Visible = False
        tbStock.Visible = False
    End Sub

    Private Sub tabCustomerCLick(sender As Object, e As EventArgs) Handles tabCustomer.Click
        tabCustomer.BackColor = Color.Chocolate
        tabCustomer.ForeColor = Color.White

        tabAgent.BackColor = Color.White
        tabAgent.ForeColor = Color.Chocolate

        tabStock.BackColor = Color.White
        tabStock.ForeColor = Color.Chocolate

        lblStock.Visible = True
        lblFilter.Text = "CUSTOMER"
        groupBy = "Customer"

        tbAgent.Visible = False
        tbCustomer.Visible = True
        tbStock.Visible = True
    End Sub

    Private Sub tabStockClick(sender As Object, e As EventArgs) Handles tabStock.Click
        tabStock.BackColor = Color.Chocolate
        tabStock.ForeColor = Color.White

        tabCustomer.BackColor = Color.White
        tabCustomer.ForeColor = Color.Chocolate

        tabAgent.BackColor = Color.White
        tabAgent.ForeColor = Color.Chocolate

        lblStock.Visible = True
        lblFilter.Text = "CUSTOMER"
        groupBy = "Stock"

        tbAgent.Visible = False
        tbCustomer.Visible = True
        tbStock.Visible = True
    End Sub
#End Region

    Private Sub printBtnClick() Handles printBtn.Click
        If groupBy = "Agent" Then
            If Not String.IsNullOrWhiteSpace(tbAgent.Text) _
                AndAlso Not Controller.agentList.Contains(tbAgent.Text.ToUpper) Then
                Util.notifyError("Invalid agent name.")
                Exit Sub
            End If
            printAgentSales()
        Else
            If Not String.IsNullOrWhiteSpace(tbCustomer.Text) _
                AndAlso Not Controller.customerList.Contains(tbCustomer.Text.ToUpper) Then
                Util.notifyError("Invalid customer name.")
                Exit Sub
            End If

            If Not String.IsNullOrWhiteSpace(tbStock.Text) _
                AndAlso Not Controller.stockList.Contains(tbStock.Text.ToUpper) Then
                Util.notifyError("Invalid stock name.")
                Exit Sub
            End If

            printSales()
        End If
    End Sub

    Private Sub printSales()
        Dim printDoc As New PrintMonitor
        printDoc.filter = groupBy
        printDoc.report_name = "SALES MONITORING BY " & groupBy.ToUpper

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString

            Dim sel As String = If(groupBy.ToUpper.Equals("STOCK"),
                "select s.name, c.name as filter, concat('UNIT: ', u.name) as unit, ",
                "select c.name, concat(s.name, ' (', u.name, ')') as filter,")

            '"so.discount1, so.discount2 " & _
            'and so.totalAmount - (so.totalpaid + so.totalreturned)) <= 0" & _
            Dim qry As String = sel & " so.date, so.documentno as docno, i.quantity " &
                "as qtyordered, i.quantityreturned as qtyrtn, (i.quantity - i.quantityreturned) " &
                "as qtysales, i.price, ((i.quantity - i.quantityreturned) * i.price) as amount " &
                "from salesorderitems i, salesorders so, stocks s, customers c, units u " &
                "where i.salesorderid = so.id and i.stockid = s.id and so.customerid = c.id " &
                "and s.unitid = u.id and so.posteddate is not null " &
                " and so.date >= " & Util.inSql(dateFrom.Value) &
                " and so.date <= " & Util.inSql(dateTo.Value)

            If (Not String.IsNullOrWhiteSpace(tbCustomer.Text)) Then
                Dim customerName As String = tbCustomer.Text.ToUpper
                qry += " and c.active = true and ucase(c.name) = '" & customerName & "'"
                printDoc.report_detail += " Customer: " & customerName & "."
            Else
                printDoc.report_detail += " All Customer."
            End If

            If (Not String.IsNullOrWhiteSpace(tbStock.Text)) Then
                Dim stockName = tbStock.Text.ToUpper
                qry += " and s.active = true and ucase(s.name) = '" & stockName & "'"
                printDoc.report_detail += " Stock: " & stockName & "."
            Else
                printDoc.report_detail += " All Stock."
            End If

            qry += If(groupBy.ToUpper.Equals("STOCK"),
                      " order by s.name, so.date", " order by c.name, so.date")

            printDoc.items = context.Database.SqlQuery(Of _Monitor)(qry).ToList
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

    Private Sub printAgentSales()
        Dim printDoc As New PrintMonitorByAgent
        Dim sales As New List(Of salesorder)

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            If Not String.IsNullOrWhiteSpace(tbAgent.Text) Then
                printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString & ". Agent: " & tbAgent.Text.ToUpper
                sales = context.salesorders _
                    .Include("Customer").Include("Agent") _
                    .Where(Function(c) c.Date >= dateFrom.Value And c.Date <= dateTo.Value _
                        And c.agent.Name.ToUpper.Equals(tbAgent.Text.ToUpper)) _
                    .OrderBy(Function(c) c.agent.Name).ThenBy(Function(c) c.Date).ToList
            Else
                printDoc.report_detail = "From " & dateFrom.Value.ToShortDateString & " To " & dateTo.Value.ToShortDateString & ". All agents"
                sales = context.salesorders _
                    .Include("Customer").Include("Agent") _
                    .Where(Function(c) c.Date >= dateFrom.Value And c.Date <= dateTo.Value) _
                    .OrderBy(Function(c) c.agent.Name).ThenBy(Function(c) c.Date).ToList
            End If

            If sales.Count > 0 Then
                printDoc.sales = sales
                Util.previewDocument(printDoc)
            Else
                Util.notifyError("Nothing to Print.")
            End If
        End Using
    End Sub

End Class