Public Class RptTransactions

    Dim tabs As New List(Of Button)
    Dim transactionName As String

    Dim ccOrSp As Boolean
    Dim rptQry As String
    Dim rptDetail As String

#Region "Etc"
    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initCustomers()
        Controller.initAgents()
        Controller.initSuppliers()

        tbFilter.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbFilter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        loadSuppliers()

        tabs.Add(tabSO)
        tabs.Add(tabPO)
        tabs.Add(tabSR)
        tabs.Add(tabPR)
        tabs.Add(tabCC)
        tabs.Add(tabSP)

        dateFrom.Value = Util.getInitialDate
        dateTo.Value = DateTime.Today

        loadSuppliers()
        transactionName = "Purchase Order"
        cbDetail.Checked = False
    End Sub

    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub highlight(ByRef selected As Button, ByVal label As String)
        For Each btn In tabs
            btn.BackColor = Color.White
            btn.ForeColor = Color.Chocolate
        Next

        selected.BackColor = Color.Chocolate
        selected.ForeColor = Color.White

        tbFilter.Text = String.Empty
        lblFilter.Text = label

        If label.Equals("CUSTOMER") Then
            loadCustomers()
        ElseIf label.Equals("SUPPLIER") Then
            loadSuppliers()
        ElseIf label.Equals("AGENT") Then
            loadAgents()
        End If
    End Sub

    Private Sub loadCustomers()
        tbFilter.AutoCompleteCustomSource = Controller.customerList
    End Sub

    Private Sub loadSuppliers()
        tbFilter.AutoCompleteCustomSource = Controller.supplierList
    End Sub

    Private Sub loadAgents()
        tbFilter.AutoCompleteCustomSource = Controller.agentList
    End Sub

    Private Sub tabPO_Click(sender As Object, e As EventArgs) Handles tabPO.Click
        highlight(tabPO, "SUPPLIER")
        transactionName = "Purchase Order"
        clickedPaymentOrCollection(False)
    End Sub

    Private Sub tabPR_Click(sender As Object, e As EventArgs) Handles tabPR.Click
        highlight(tabPR, "SUPPLIER")
        transactionName = "Purchase Return"
        clickedPaymentOrCollection(False)
    End Sub

    Private Sub tabSP_Click(sender As Object, e As EventArgs) Handles tabSP.Click
        highlight(tabSP, "SUPPLIER")
        transactionName = "Supplier Payment"
        clickedPaymentOrCollection(True)
    End Sub

    Private Sub tabSO_Click(sender As Object, e As EventArgs) Handles tabSO.Click
        highlight(tabSO, "CUSTOMER")
        transactionName = "Sales Order"
        clickedPaymentOrCollection(False)
    End Sub

    Private Sub tabSR_Click(sender As Object, e As EventArgs) Handles tabSR.Click
        highlight(tabSR, "AGENT")
        transactionName = "Sales Return"
        clickedPaymentOrCollection(False)
    End Sub

    Private Sub tabCC_Click(sender As Object, e As EventArgs) Handles tabCC.Click
        highlight(tabCC, "CUSTOMER")
        transactionName = "Customer Collection"
        clickedPaymentOrCollection(True)
    End Sub

    Private Sub clickedPaymentOrCollection(ByVal spOrCC As Boolean)
        If spOrCC Then
            cbDetail.Text = "CHECK"
        Else
            cbDetail.Text = "DETAIL"
        End If

        ccOrSp = spOrCC
        cbSpecial.Visible = spOrCC
        cbOrder.Visible = spOrCC
    End Sub

#End Region

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If Not String.IsNullOrWhiteSpace(tbFilter.Text) AndAlso _
            Not tbFilter.AutoCompleteCustomSource.Contains(tbFilter.Text.ToUpper) Then
            Util.notifyError("Invalid " & Util.capitalize(lblFilter.Text))
            Exit Sub
        End If

        If Not transactionName.ToUpper.Contains("PAYMENT") _
            AndAlso Not transactionName.ToUpper.Contains("COLLECTION") Then
            If cbDetail.Checked Then
                printTransactionWithDetails()
            Else
                printTransactions()
            End If
        Else
            If Not cbDetail.Checked AndAlso Not cbOrder.Checked AndAlso Not cbSpecial.Checked Then
                printTransactions()
            ElseIf cbSpecial.Checked Then
                printSpecial()
            Else
                printTransactionWithDetails()
            End If
        End If

    End Sub

    Private Sub printSpecial()
        rptDetail = "From " & dateFrom.Value.ToShortDateString _
            & " To " & dateTo.Value.ToShortDateString

        Dim printDoc As New PrintSpecial

        Select Case transactionName.ToUpper
            Case "CUSTOMER COLLECTION"
                rptQry = "select i.date, i.documentno as docno, o.bank, f.name as filter, i.amount " & _
                    "from collectioncheckitems i, customercollections o, customers f " & _
                    "where i.customercollectionid = o.id and o.customerid = f.id and o.posteddate is not null "
                Exit Select

            Case "SUPPLIER PAYMENT"
                rptQry = "select i.date, i.documentno as docno, o.bank, f.name as filter, i.amount " & _
                    "from paymentcheckitems i, supplierpayments o, suppliers f " & _
                    "where i.supplierpaymentid = o.id and o.supplierid = f.id and o.posteddate is not null "
                Exit Select
        End Select

        appendFiltersOnQuery()
        printDoc.report_name = transactionName
        printDoc.report_detail = rptDetail
        printDoc.filterName = lblFilter.Text.ToUpper

        rptQry.Replace(" order by o.documentno", " order by i.date")

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            printDoc.items = context.Database _
                .SqlQuery(Of _Special)(rptQry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

    Private Sub printTransactions()
        Dim printDoc As New PrintTransactionList
        rptDetail = "From " & dateFrom.Value.ToShortDateString _
            & " To " & dateTo.Value.ToShortDateString

        loadQueryString()

        printDoc.report_name = transactionName
        printDoc.report_detail = rptDetail
        printDoc.filterName = If(lblFilter.Text.ToUpper.Equals("CUSTOMER") OrElse lblFilter.Text.ToUpper.Equals("AGENT"), "CUSTOMER - AGENT", lblFilter.Text)

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            printDoc.items = context.Database _
                .SqlQuery(Of _TransactionList)(rptQry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

    Private Sub printTransactionWithDetails()
        Dim printDoc As New PrintTransactionDetail
        rptDetail = "From " & dateFrom.Value.ToShortDateString _
            & " To " & dateTo.Value.ToShortDateString

        loadWithDetailsQueryString()

        printDoc.report_name = transactionName
        printDoc.report_detail = rptDetail
        printDoc.filterName = If(lblFilter.Text.ToUpper.Equals("CUSTOMER") OrElse lblFilter.Text.ToUpper.Equals("AGENT"), "CUSTOMER - AGENT", lblFilter.Text)
        printDoc.ccOrSp = ccOrSp

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            printDoc.items = context.Database _
                .SqlQuery(Of _TransactionDetail)(rptQry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

    Private Sub loadQueryString()
        Select Case transactionName.ToUpper
            Case "PURCHASE ORDER"
                rptQry = "select o.documentno, o.date, f.name as filter, o.totalamount, " & _
                    "o.discount1, o.discount2, o.discount3 from purchaseorders o, suppliers f " & _
                    "where o.supplierid = f.id "
                Exit Select

            Case "SALES ORDER"
                rptQry = "select o.documentno, o.date, concat(f.name, ' (', ag.name, ')') as filter, o.totalamount, " & _
                    "o.discount1, o.discount2 from salesorders o, agents ag, customers f " & _
                    "where f.agentid = ag.id and o.customerid = f.id "
                Exit Select

            Case "PURCHASE RETURN"
                rptQry = "select o.documentno, o.date, f.name as filter, o.totalamount " & _
                    "from purchasereturns o, suppliers f " & _
                    "where o.supplierid = f.id "
                Exit Select

            Case "SALES RETURN"
                rptQry = "select o.documentno, o.date, concat(c.name, ' (', f.name, ')') as filter, o.totalamount " &
                    "from salesreturns o, agents f, customers c " &
                    "where c.agentid = f.id and o.customerid = c.id "
                Exit Select

            Case "CUSTOMER COLLECTION"
                rptQry = "select o.documentno, o.date, f.name as filter, o.totalcheck as totalamount " & _
                    "from customercollections o, customers f " & _
                    "where o.customerid = f.id "
                Exit Select

            Case "SUPPLIER PAYMENT"
                rptQry = "select o.documentno, o.date, f.name as filter, o.totalcheck as totalamount " & _
                    "from supplierpayments o, suppliers f " & _
                    "where o.supplierid = f.id "
                Exit Select
        End Select

        rptQry += " and o.posteddate is not null and o.date >= " & Util.inSql(dateFrom.Value) & _
               " and o.date <= " & Util.inSql(dateTo.Value)

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            Dim filter = tbFilter.Text.ToUpper
            rptQry += " and f.active = true and ucase(f.name) = '" & filter & "'"
            rptDetail += " " & Util.capitalize(lblFilter.Text) & ": " & tbFilter.Text.ToUpper
        Else
            rptDetail += " All " & Util.capitalize(lblFilter.Text) & "."
        End If

        If Not String.IsNullOrWhiteSpace(tbFilterDoc.Text) Then
            Dim doc = tbFilterDoc.Text.ToUpper
            rptQry += " and ucase(o.documentno) = '" & doc & "'"
            rptDetail += " Document No: " & tbFilter.Text.ToUpper
        End If

        rptQry += " order by o.documentno"
    End Sub

    Private Sub loadWithDetailsQueryString()
        Select Case transactionName.ToUpper
            Case "PURCHASE ORDER"
                rptQry = "select o.documentno, o.date, f.name as filter, o.totalamount, " & _
                    "o.discount1, o.discount2, o.discount3, s.name as stock, i.quantity, " & _
                    "if(i.quantity > 1, u.pluralname, u.name) as unit, i.discount1 as itemdiscount1, " & _
                    "i.discount2 as itemdiscount2, i.discount3 as itemdiscount3, " & _
                    "i.price from purchaseorders o, suppliers f, stocks s, units u, " & _
                    "purchaseorderitems i where i.purchaseorderid = o.id and o.supplierid = f.id " & _
                    "and i.stockid = s.id and s.unitid = u.id and o.posteddate is not null "
                appendFiltersOnQuery()
                Exit Select

            Case "SALES ORDER"
                rptQry = "select o.documentno, o.date, concat(f.name, ' (', ag.name, ')') as filter, o.totalamount, " & _
                    "o.discount1, o.discount2, s.name as stock, i.quantity, " & _
                    "if(i.quantity > 1, u.pluralname, u.name) as unit, i.discount1 as itemdiscount1, " & _
                    "i.discount2 as itemdiscount2, i.price from salesorders o, agents ag, customers f, " & _
                    "salesorderitems i, stocks s, units u where i.salesorderid = o.id and " & _
                    "f.agentid = ag.id and o.customerid = f.id and i.stockid = s.id and s.unitid = u.id and o.posteddate is not null"
                appendFiltersOnQuery()
                Exit Select

            Case "PURCHASE RETURN"
                rptQry = "select o.documentno, o.date, f.name as filter, o.totalamount, " & _
                    "s.name as stock, i.quantity, if(i.quantity > 1, u.pluralname, u.name) as unit, " & _
                    "i.price from purchasereturns o, suppliers f, stocks s, units u, purchasereturnitems i " & _
                    "where i.purchasereturnid = o.id and o.supplierid = f.id and i.stockid = s.id " & _
                    "and s.unitid = u.id and o.posteddate is not null"
                appendFiltersOnQuery()
                Exit Select

            Case "SALES RETURN"
                rptQry = "select o.documentno, o.date, concat(c.name, ' (', f.name, ')') as filter, o.totalamount, " &
                    "s.name as stock, i.quantity, if(i.quantity > 1, u.pluralname, u.name) as unit, " &
                    "i.price from salesreturns o, agents f, customers c, stocks s, units u, salesreturnitems i " &
                    "where i.salesreturnid = o.id and c.agentid = f.id and o.customerid = c.id and i.stockid = s.id " &
                    "and s.unitid = u.id and o.posteddate is not null"
                appendFiltersOnQuery()
                Exit Select

            Case "CUSTOMER COLLECTION"
                Dim qryCheck = "select o.documentno as documentno, o.date, f.name as filter, o.totalcheck as totalamount, " & _
                    "i.documentno as xdetail, i.date as xdate, o.bank as xbank, i.amount as xamount " & _
                    "from customercollections o, customers f, collectioncheckitems i " & _
                    "where i.customercollectionid = o.id and o.customerid = f.id "

                Dim qryOrder = "select o.documentno as documentno, o.date, f.name as filter, o.totalcheck as totalamount, " & _
                    "oo.documentno as xdetail, oo.date as xdate, '' as xbank, i.amount as xamount " & _
                    "from customercollections o, customers f, collectionorderitems i, salesorders oo " & _
                    "where i.customercollectionid = o.id and o.customerid = f.id and i.salesorderid = oo.id "

                If cbDetail.Checked And cbOrder.Checked Then
                    qryCheck &= getAdditionalFilterWithoutOrderBy()
                    qryOrder &= getAdditionalFilterWithoutOrderBy()

                    rptQry = qryCheck & " union all " & qryOrder
                    rptQry += " order by documentno"
                ElseIf cbDetail.Checked Then
                    appendFiltersOnQuery(qryCheck)
                    rptQry = qryCheck
                Else
                    appendFiltersOnQuery(qryOrder)
                    rptQry = qryOrder
                End If

                Exit Select

            Case "SUPPLIER PAYMENT"
                Dim qryCheck = "select o.documentno as documentno, o.date, f.name as filter, o.totalcheck as totalamount, " & _
                    "i.documentno as xdetail, i.date as xdate, o.bank as xbank, i.amount as xamount " & _
                    "from supplierpayments o, suppliers f, paymentcheckitems i " & _
                    "where i.supplierpaymentid = o.id and o.supplierid = f.id "

                Dim qryOrder = "select o.documentno as documentno, o.date, f.name as filter, o.totalcheck as totalamount, " & _
                    "oo.documentno as xdetail, oo.date as xdate, '' as xbank, i.amount as xamount " & _
                    "from supplierpayments o, suppliers f, paymentorderitems i, purchaseorders oo " & _
                    "where i.supplierpaymentid = o.id and o.supplierid = f.id and i.purchaseorderid = oo.id "

                If cbDetail.Checked And cbOrder.Checked Then
                    qryCheck &= getAdditionalFilterWithoutOrderBy()
                    qryOrder &= getAdditionalFilterWithoutOrderBy()

                    rptQry = qryCheck & " union all " & qryOrder
                    rptQry += " order by documentno"
                ElseIf cbDetail.Checked Then
                    appendFiltersOnQuery(qryCheck)
                    rptQry = qryCheck
                Else
                    appendFiltersOnQuery(qryOrder)
                    rptQry = qryOrder
                End If

                Exit Select
        End Select

        
    End Sub

    Private Sub appendFiltersOnQuery()
        appendFiltersOnQuery(rptQry)
    End Sub

    Private Function getAdditionalFilterWithoutOrderBy() As String
        Dim q As String = " and o.date >= " & Util.inSql(dateFrom.Value) & _
               " and o.date <= " & Util.inSql(dateTo.Value)

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            Dim filter = tbFilter.Text.ToUpper
            q += " and f.active = true and ucase(f.name) = '" & filter & "'"

            If Not rptDetail.ToUpper.Contains(lblFilter.Text.ToUpper) Then
                rptDetail += " " & Util.capitalize(lblFilter.Text) & ": " & tbFilter.Text.ToUpper
            End If
        Else
            rptDetail += " All " & Util.capitalize(lblFilter.Text) & "."
        End If

        If Not String.IsNullOrWhiteSpace(tbFilterDoc.Text) Then
            Dim doc = tbFilterDoc.Text.ToUpper
            q += " and ucase(o.documentno) = '" & doc & "'"

            If Not rptDetail.ToUpper.Contains(tbFilter.Text.ToUpper) Then
                rptDetail += " Document No: " & tbFilter.Text.ToUpper
            End If
        End If

        Return q
    End Function

    Private Sub appendFiltersOnQuery(ByRef rQry As String)
        rQry += " and o.date >= " & Util.inSql(dateFrom.Value) & _
               " and o.date <= " & Util.inSql(dateTo.Value)

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            Dim filter = tbFilter.Text.ToUpper
            rQry += " and f.active = true and ucase(f.name) = '" & filter & "'"

            If Not rptDetail.ToUpper.Contains(lblFilter.Text.ToUpper) Then
                rptDetail += " " & Util.capitalize(lblFilter.Text) & ": " & tbFilter.Text.ToUpper
            End If
        Else
            rptDetail += " All " & Util.capitalize(lblFilter.Text) & "."
        End If

        If Not String.IsNullOrWhiteSpace(tbFilterDoc.Text) Then
            Dim doc = tbFilterDoc.Text.ToUpper
            rQry += " and ucase(o.documentno) = '" & doc & "'"

            If Not rptDetail.ToUpper.Contains(tbFilter.Text.ToUpper) Then
                rptDetail += " Document No: " & tbFilter.Text.ToUpper
            End If
        End If

        rQry += " order by o.documentno"
    End Sub

    Private Sub cbSpecial_CheckedChanged(sender As Object, e As EventArgs) Handles cbSpecial.CheckedChanged
        If cbSpecial.Checked Then
            cbOrder.Checked = False
            cbDetail.Checked = False
        End If
    End Sub

    Private Sub cbOrder_CheckedChanged(sender As Object, e As EventArgs) Handles cbOrder.CheckedChanged
        If cbOrder.Checked Then
            cbSpecial.Checked = False
        End If
    End Sub

    Private Sub cbDetail_CheckedChanged(sender As Object, e As EventArgs) Handles cbDetail.CheckedChanged
        If cbDetail.Checked Then
            cbSpecial.Checked = False
        End If
    End Sub

End Class