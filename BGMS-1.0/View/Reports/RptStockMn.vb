Public Class RptStockMn

    'all, positive, zero, negative
    Dim detail As String
    Dim tabs As New List(Of Button)

#Region "Etc"
    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initCategories()
        tbFilter.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbFilter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbFilter.AutoCompleteCustomSource = Controller.categoryList

        tabs.Add(tabAll)
        tabs.Add(tabZero)
        tabs.Add(tabPositive)
        tabs.Add(tabNegative)
        detail = "View all stock"

        cbSort.Checked = True
        shOnHand.Checked = True
        shCost.Checked = True
        shPrice.Checked = True
    End Sub

    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub tabNegative_Click(sender As Object, e As EventArgs) Handles tabNegative.Click
        highlight(tabNegative)
        detail = "View negative inventory"
    End Sub

    Private Sub tabZero_Click(sender As Object, e As EventArgs) Handles tabZero.Click
        highlight(tabZero)
        detail = "View zero inventory"
    End Sub

    Private Sub tabAll_Click(sender As Object, e As EventArgs) Handles tabAll.Click
        highlight(tabAll)
        detail = "View all stock"
    End Sub

    Private Sub tabPositive_Click(sender As Object, e As EventArgs) Handles tabPositive.Click
        highlight(tabPositive)
        detail = "View with inventory"
    End Sub

    Private Sub highlight(ByRef selected As Button)
        For Each btn In tabs
            btn.BackColor = Color.White
            btn.ForeColor = Color.Chocolate
        Next

        selected.BackColor = Color.Chocolate
        selected.ForeColor = Color.White
    End Sub
#End Region

    Private Sub printBtn_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If Not String.IsNullOrWhiteSpace(tbFilter.Text) AndAlso _
            Not Controller.categoryList.Contains(tbFilter.Text.ToUpper) Then
            Util.notifyError("Invalid Category.")
            Exit Sub
        End If

        Dim printDoc As New PrintMonitorStock

        Dim qry As String = "select c.name as category, s.name as stock, s.description, " & _
            "s.cost, s.price, s.qtyonhand as onhand from stocks s, categories c " & _
            "where s.categoryid = c.id"

        qry = qry + If(detail.Contains("with inventory"), " and s.qtyonhand > 0", String.Empty)
        qry = qry + If(detail.Contains("negative inventory"), " and s.qtyonhand < 0", String.Empty)
        qry = qry + If(detail.Contains("zero inventory"), " and s.qtyonhand = 0", String.Empty)

        If Not String.IsNullOrWhiteSpace(tbFilter.Text) Then
            Dim category = tbFilter.Text.ToUpper
            qry += " and ucase(c.name) = '" & category & "'"
            detail += ". Category: " & category
        End If

        qry = qry + If(Not String.IsNullOrWhiteSpace(tbFilter.Text), _
                       " and ucase(c.name) = '" & tbFilter.Text.ToUpper & "'", String.Empty)

        qry = qry + " order by" + If(cbSort.Checked, " c.name", " s.name")

        printDoc.shCost = shCost.Checked
        printDoc.shPrice = shPrice.Checked
        printDoc.shOnHand = shOnHand.Checked
        printDoc.report_detail = detail

        Using context As New bgmsEntities
            printDoc.items = context.Database _
                .SqlQuery(Of _StockMonitor)(qry) _
                .ToList()
        End Using

        If Not IsNothing(printDoc.items) AndAlso printDoc.items.Count > 0 Then
            Util.previewDocument(printDoc)
        Else
            Util.notifyError("Nothing to Print.")
        End If
    End Sub

End Class