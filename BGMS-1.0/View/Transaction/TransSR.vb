Public Class TransSR
    Implements IControl

    Public currentObject As salesreturn
    Dim totalAmount As Double
    Dim retainIds As New List(Of Integer)
    Dim selectedStock As stock
    Dim prevStockName As String

    Dim stockListDesc As New AutoCompleteStringCollection
    Dim priceList As New List(Of Double)
    Dim discountList1 As New List(Of Double)
    Dim discountList2 As New List(Of Double)
    Dim maxQuantity As Integer

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initCustomers()
        tbCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbCustomer.AutoCompleteCustomSource = Controller.customerList

        Controller.initAgents()
        tbAgent.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbAgent.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbAgent.AutoCompleteCustomSource = Controller.agentList

        loadGrid()
        initialize()

        prevStockName = String.Empty
    End Sub

#Region "Standard"
    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Public Sub validateItems()
        btnCheck.Visible = True
        btnCheck.Focus()
    End Sub

    Private Sub enterGrid_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGrid.CellBeginEdit
        btnCheck.Visible = False
    End Sub

    Private Sub initialize()
        Me.reset()
        Me.loadObject()
        Me.enableInputs(False)
        Me.showUpdateButtons(False)
        Controller.updateMode = Nothing
    End Sub

    Public Sub addClick() Handles btnAdd.Click
        If btnAdd.Visible Then
            Me.reset()
            Me.enableInputs(True)
            Controller.updateMode = Constants.UPDATE_MODE_CREATE
            showUpdateButtons(True)
            updateCountLabel()
        End If
    End Sub

    Public Sub editClick() Handles btnEdit.Click
        If btnEdit.Visible Then
            Me.enableInputs(True)
            Controller.updateMode = Constants.UPDATE_MODE_EDIT
            Me.loadObject()
            showUpdateButtons(True)
        End If
    End Sub

    Public Sub deleteClick() Handles btnDelete.Click
        If btnDelete.Visible Then
            If Util.askForConfirmation("Delete this item?", "Delete") Then
                Me.deleteObject()
                Me.resetListSelection()
                resetMe()
            End If
        End If
    End Sub

    Public Sub cancelClick() Handles btnCancel.Click
        If btnCancel.Visible Then
            Util.clearRows(enterGrid)
            resetMe()
        End If
    End Sub

    Public Sub checkClick() Handles btnCheck.Click
        If btnCheck.Visible Then
            If Not IsNothing(Me.getErrorMessage) Then
                Util.notifyError(Me.getErrorMessage)
            Else
                Try
                    saveChangesOnMe()
                    resetMe()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
            End If
        End If
    End Sub

    Private Sub nextClick() Handles btnNext.Click
        If btnNext.Visible Then
            Me.nextObject()
        End If
    End Sub

    Private Sub nextDblClick() Handles btnNext.DoubleClick
        If btnNext.Visible Then
            Me.lastObject()
        End If
    End Sub

    Private Sub previousClick() Handles btnPrev.Click
        If btnPrev.Visible Then
            Me.previousObject()
        End If
    End Sub

    Private Sub previousDblClick() Handles btnPrev.DoubleClick
        If btnPrev.Visible Then
            Me.firstObject()
        End If
    End Sub

    Public Sub searchClick() Handles btnSearch.Click
        If btnSearch.Visible Then
            Me.search()
        End If
    End Sub

    Private Sub saveChangesOnMe()
        If Controller.updateMode = Constants.UPDATE_MODE_CREATE Then
            Me.saveObject()
        Else
            Me.updateObject()
        End If
    End Sub

    Private Sub resetMe()
        Me.reset()
        Me.enableInputs(False)
        showUpdateButtons(False)
        Controller.updateMode = Nothing
        Me.loadObject()
    End Sub

    Public Sub showUpdateButtons(ByVal show As Boolean)
        btnCheck.Visible = show
        btnCancel.Visible = show

        btnAdd.Visible = Not show

        If getAllowEditDelete() Then
            btnEdit.Visible = Not show
            btnDelete.Visible = Not show
        Else
            btnEdit.Visible = False
            btnDelete.Visible = False
        End If

        btnPrev.Visible = Not show
        btnNext.Visible = Not show
        btnSearch.Visible = Not show
        btnPrint.Visible = Not show
    End Sub

#End Region

    Public Sub deleteRow() Implements IControl.deleteRow
        If Not IsNothing(Controller.updateMode) AndAlso Not IsNothing(enterGrid.CurrentCell) _
            AndAlso enterGrid.CurrentCell.RowIndex >= 0 Then
            Try
                enterGrid.Rows.RemoveAt(enterGrid.CurrentCell.RowIndex)
                updateTotalAmount()
                updateCountLabel()
            Catch ex As Exception
                For Each cell As DataGridViewCell In enterGrid _
                        .Rows(enterGrid.CurrentCell.RowIndex).Cells()
                    cell.Value = String.Empty
                Next
            End Try
        End If
    End Sub

    Public Sub deleteObject() Implements IControl.deleteObject
        Using context As New bgmsEntities
            currentObject = context.salesreturns.Where(Function(c) _
                c.Id.Equals(currentObject.Id)).FirstOrDefault
            context.salesreturnitems.RemoveRange(currentObject.salesreturnitems)
            context.salesreturns.Remove(currentObject)

            Dim action As String = Controller.currentUser.Username & " deleted a sales return (" & _
                currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
        End Using

        currentObject = Nothing
    End Sub

    Public Function getAllowEditDelete() As Boolean Implements IControl.getAllowEditDelete
        Return If(IsNothing(currentObject) OrElse Not IsNothing(currentObject.PostedDate), False, True)
    End Function

    Public Sub enableInputs(enable As Boolean) Implements IControl.enableInputs
        tbRemarks.Enabled = enable
        docDate.Enabled = enable
        tbCustomer.Enabled = enable
        tbAgent.Enabled = enable

        enterGrid.ReadOnly = Not enable
        setReadOnlyColumns()

        If enable Then
            tbDocNo.Text = getUpdatedDocumentNo()
            tbCustomer.Focus()
            enterGrid.ClearSelection()

            enterGrid.Columns.Item("Disc1").Visible = True
            enterGrid.Columns.Item("Disc2").Visible = True
        End If
    End Sub

    Private Function getUpdatedDocumentNo() As String
        Using context As New bgmsEntities
            Dim counter = context.counters.Where(Function(c) _
               c.Owner.Equals(Constants.OWNER_NAME_SALES_RETURN)).FirstOrDefault

            If Not IsNothing(counter) Then
                Return Util.getFormattedDocumentNo(counter.Prefix, counter.Count)
            End If
        End Using

        Return Nothing
    End Function

    Public Sub loadObject() Implements IControl.loadObject
        Using context As New bgmsEntities
            If Not IsNothing(currentObject) Then
                currentObject = context.salesreturns _
                    .Include("SalesReturnItems").Include("Customer").Include("Agent") _
                    .Where(Function(f) f.Id.Equals(currentObject.Id)) _
                    .FirstOrDefault

                loadCurrentObject()
            Else
                currentObject = context.salesreturns _
                    .Include("SalesReturnItems").Include("Customer").Include("Agent") _
                    .OrderByDescending(Function(f) f.DocumentNo) _
                    .FirstOrDefault

                If Not IsNothing(currentObject) Then
                    loadCurrentObject()
                Else
                    reset()
                End If
            End If
        End Using
    End Sub

    Public Sub nextObject() Implements IControl.nextObject
        If IsNothing(currentObject) Then
            Exit Sub
        End If

        Using context As New bgmsEntities
            Dim nextObj As New salesreturn
            nextObj = context.salesreturns _
                .Where(Function(c) c.DocumentNo.CompareTo(currentObject.DocumentNo) > 0) _
                .OrderBy(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(nextObj) Then
                currentObject = nextObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub previousObject() Implements IControl.previousObject
        If IsNothing(currentObject) Then
            Exit Sub
        End If

        Using context As New bgmsEntities
            Dim prevObj As New salesreturn
            prevObj = context.salesreturns _
                .Where(Function(c) c.DocumentNo.CompareTo(currentObject.DocumentNo) < 0) _
                .OrderByDescending(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(prevObj) Then
                currentObject = prevObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub firstObject() Implements IControl.firstObject
        Using context As New bgmsEntities
            Dim firstObj As New salesreturn
            firstObj = context.salesreturns _
                .OrderBy(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(firstObj) Then
                currentObject = firstObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub lastObject() Implements IControl.lastObject
        Using context As New bgmsEntities
            Dim lastObj As New salesreturn
            lastObj = context.salesreturns _
                .OrderByDescending(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(lastObj) Then
                currentObject = lastObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub reset() Implements IControl.reset
        lblBy.Visible = False
        lblOn.Visible = False
        lblPostedDate.Visible = False
        lblPostedOn.Visible = False

        tbRemarks.Text = String.Empty
        tbTotalAmt.Text = String.Empty
        docDate.Value = DateTime.Today
        tbCustomer.Text = String.Empty
        tbAgent.Text = String.Empty

        Util.clearRows(enterGrid)
    End Sub

    Public Sub saveObject() Implements IControl.saveObject
        Using context As New bgmsEntities
            currentObject = New salesreturn
            setObjectValues(context)

            Dim counter = context.counters.Where(Function(c) _
            c.Owner.Equals(Constants.OWNER_NAME_SALES_RETURN)) _
                .SingleOrDefault()
            counter.Count += 1

            context.salesreturns.Add(currentObject)

            Dim action As String = Controller.currentUser.Username & " created a sales return (" & _
                currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
            Util.notifyInfo("Created 1 Sales Order.")
        End Using
    End Sub

    Public Sub search() Implements IControl.search
        EtcSearch.openUp(Me.Name)
    End Sub

    Public Sub setObjectValues(ByRef context As bgmsEntities)
        updateTotalAmount()

        If Controller.updateMode.Equals(Constants.UPDATE_MODE_CREATE) Then
            currentObject.DocumentNo = getUpdatedDocumentNo()
        End If

        currentObject.Date = docDate.Value

        currentObject.Remarks = tbRemarks.Text
        currentObject.TotalAmount = totalAmount

        currentObject.ModifyBy = Controller.currentUser.Username
        currentObject.ModifyDate = DateTime.Today

        If String.IsNullOrWhiteSpace(tbCustomer.Text) Then
            currentObject.customer = Nothing
        Else
            currentObject.customerId = context.customers _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbCustomer.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If

        If String.IsNullOrEmpty(tbAgent.Text) Then
            currentObject.agent = Nothing
        Else
            currentObject.agentId = context.agents _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbAgent.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If

        setObjectItemsValues(context)
    End Sub

    Public Function showAllButtons() As Integer Implements IControl.getButtonsShown
        Return Constants.SHOW_ALL
    End Function

    Public Sub updateObject() Implements IControl.updateObject
        Using context As New bgmsEntities
            currentObject = context.salesreturns _
                .Where(Function(c) c.Id.Equals(currentObject.Id)).FirstOrDefault()
            setObjectValues(context)

            Dim action As String = Controller.currentUser.Username & " updated a sales return (" & _
                currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
            Util.notifyInfo("Updated 1 Sales Order.")
        End Using
    End Sub

    Public Function getErrorMessage() As String Implements IControl.getErrorMessage
        If IsNothing(docDate.Value) Then
            Return "Date is required."
        End If

        If String.IsNullOrEmpty(tbCustomer.Text) Then
            Return "Customer name is required."
        End If

        If String.IsNullOrEmpty(tbAgent.Text) Then
            Return "Agent name is required."
        End If

        If Not Controller.customerList.Contains(tbCustomer.Text.ToUpper) Then
            Return "Invalid Customer name."
        End If

        If Not Controller.agentList.Contains(tbAgent.Text.ToUpper) Then
            Return "Invalid Agent name."
        End If

        If enterGrid.RowCount <= 1 Then
            Return Constants.ERROR_MSG_INPUT_ITEMS
        End If

        Return Nothing
    End Function

    Private Sub loadCurrentObject()
        lblBy.Text = Util.fmtModifyBy(currentObject.ModifyBy)
        lblOn.Text = Util.fmtModifyDate(currentObject.ModifyDate)
        lblBy.Visible = True
        lblOn.Visible = True

        tbDocNo.Text = currentObject.DocumentNo
        tbRemarks.Text = currentObject.Remarks
        docDate.Value = currentObject.Date
        tbTotalAmt.Text = FormatNumber(CDbl(currentObject.TotalAmount), 2)
        tbCustomer.Text = currentObject.customer.Name
        tbAgent.Text = currentObject.agent.Name

        lblPostedOn.Visible = If(IsNothing(currentObject.PostedDate), False, True)
        lblPostedDate.Visible = If(IsNothing(currentObject.PostedDate), False, True)
        lblPostedDate.Text = Format(currentObject.PostedDate, Constants.DATE_FORMAT)

        Dim modifiable = If(IsNothing(Controller.updateMode) And IsNothing(currentObject.PostedDate), True, False)
        btnEdit.Visible = modifiable
        btnDelete.Visible = modifiable

        loadObjectItems(currentObject.salesreturnitems.ToList())
    End Sub

    Private Sub loadObjectItems(ByVal orderItems As List(Of salesreturnitem))
        Util.clearRows(enterGrid)

        Dim hasDisc1, hasDisc2 As Boolean
        'orderItem.stock.Description, _
        For Each orderItem In orderItems
            enterGrid.Rows.Add(
                orderItem.Id,
                orderItem.stock.Name + " : " + orderItem.stock.Description,
                orderItem.Price,
                orderItem.Discount1,
                orderItem.Discount2,
                orderItem.Quantity,
                orderItem.stock.unit.Name,
                getRowAmount(orderItem.Quantity, orderItem.Price,
                    orderItem.Discount1, orderItem.Discount2))

            hasDisc1 = If(orderItem.Discount1 > 0, True, hasDisc1)
            hasDisc2 = If(orderItem.Discount2 > 0, True, hasDisc2)
        Next

        enterGrid.Columns.Item("Disc1").Visible = hasDisc1
        enterGrid.Columns.Item("Disc2").Visible = hasDisc2
        updateCountLabel()
    End Sub

    Private Sub updateCountLabel()
        lblCount.Text = enterGrid.Rows.Count - 1
    End Sub

    Private Sub enterGrid_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles enterGrid.UserAddedRow
        updateCountLabel()
    End Sub

    Private Sub setObjectItemsValues(ByRef context As bgmsEntities)
        retainIds.Clear()

        For rowIndex = 0 To enterGrid.RowCount - 2

            Dim itemId As Integer = If(String.IsNullOrEmpty(enterGrid("Id", rowIndex).Value), _
                 Nothing, enterGrid("Id", rowIndex).Value)

            If Not IsNothing(itemId) And itemId <> 0 Then
                For Each orderItem In currentObject.salesreturnitems
                    If itemId = orderItem.Id Then
                        setItemValues(orderItem, rowIndex, context)
                        retainIds.Add(itemId)
                        Exit For
                    End If
                Next
            Else
                Dim orderItem As New salesreturnitem
                setItemValues(orderItem, rowIndex, context)
                currentObject.salesreturnitems.Add(orderItem)
            End If
        Next

        For count = currentObject.salesreturnitems.Count - 1 To 0 Step -1
            If Not IsNothing(currentObject.salesreturnitems(count).Id) _
                And currentObject.salesreturnitems(count).Id <> 0 _
                And Not retainIds.Contains(currentObject.salesreturnitems(count).Id) Then

                context.salesreturnitems _
                    .Remove(currentObject.salesreturnitems(count))
            End If
        Next
    End Sub

    Private Sub setItemValues(ByRef orderItem As salesreturnitem, _
                              ByVal rowIndex As Integer, ByRef context As bgmsEntities)
        Dim stockName As String = enterGrid("Stock", rowIndex).Value
        Dim stockCode As String = stockName.Substring(0, stockName.LastIndexOf(":"))
        stockCode = stockCode.Trim

        orderItem.Price = enterGrid("Price", rowIndex).Value
        orderItem.Discount1 = enterGrid("Disc1", rowIndex).Value
        orderItem.Discount2 = enterGrid("Disc2", rowIndex).Value
        orderItem.Quantity = enterGrid("Qty", rowIndex).Value

        orderItem.stockId = context.stocks _
                   .Where(Function(c) c.Name.ToUpper.Equals(stockCode.ToUpper) And c.Active = True) _
                   .Select(Function(c) c.Id).FirstOrDefault
    End Sub

    Private Sub populateDiscountColumn(ByRef tb As TextBox, ByVal columnName As String)
        Dim disc As New Double
        Double.TryParse(tb.Text, disc)

        If Not IsNothing(disc) Then
            For rowIndex = 0 To enterGrid.RowCount - 2
                enterGrid(columnName, rowIndex).Value = disc
                varsChanged(rowIndex)
            Next
        End If

        tb.Text = If(IsNothing(disc), String.Empty, tb.Text)
        updateTotalAmount()
    End Sub

    Private Sub updateTotalAmount()
        totalAmount = 0

        For rowIndex = 0 To enterGrid.RowCount - 2
            totalAmount += If(IsNothing(getRowAmount(rowIndex)), 0, getRowAmount(rowIndex))
        Next
        tbTotalAmt.Text = FormatNumber(CDbl(totalAmount), 2)
    End Sub

    Private Sub enterGrid_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles enterGrid.CellValidated
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            Select Case e.ColumnIndex
                Case 1
                    stockChanged(e)
                    Exit Select
                Case 3
                    loadAvailableDiscounts1(e.RowIndex)
                    loadAvailableDiscounts2(e.RowIndex)
                    loadMaxTotalQuantity(e.RowIndex)
                    varsChanged(e)
                    Exit Select
                Case 4
                    loadAvailableDiscounts2(e.RowIndex)
                    loadMaxTotalQuantity(e.RowIndex)
                    varsChanged(e)
                    Exit Select
                Case 5
                    loadMaxTotalQuantity(e.RowIndex)
                    varsChanged(e)
                    Exit Select
                Case 6
                    varsChanged(e)
                    Exit Select
            End Select
        End If
    End Sub

    Private Sub stockChanged(ByVal e As DataGridViewCellEventArgs)
        If Not IsNothing(enterGrid("Stock", e.RowIndex).Value) Then
            Dim stockName As String = enterGrid("Stock", e.RowIndex).Value.ToString
            Dim stockCode As String = stockName.Substring(0, stockName.LastIndexOf(":"))
            stockCode = stockCode.Trim

            If Not IsNothing(stockCode) Then
                If stockCode.ToUpper.Equals(prevStockName.ToUpper) Then
                    Exit Sub
                End If

                prevStockName = stockCode
                Using context As New bgmsEntities
                    selectedStock = context.stocks _
                        .Where(Function(c) c.Name.Equals(stockCode) And c.Active = True).FirstOrDefault

                    If Not IsNothing(selectedStock) Then
                        'enterGrid("Desc", e.RowIndex).Value = selectedStock.Description
                        enterGrid("Unit", e.RowIndex).Value = selectedStock.unit.Name
                        reloadSelectedStockVariables(e.RowIndex, True)
                        varsChanged(e)
                    End If
                End Using
            End If
            updateTotalAmount()
        End If
    End Sub

    Private Sub varsChanged(ByVal e As DataGridViewCellEventArgs)
        varsChanged(e.RowIndex)
    End Sub

    Private Sub varsChanged(ByVal idx As Integer)
        enterGrid("Amount", idx).Value = getRowAmount(idx)
        updateTotalAmount()
    End Sub

    Private Function getRowAmount(ByVal rowIndex As Integer) As Double
        Dim qty, price, discount1, discount2 As Double

        Double.TryParse(enterGrid("Qty", rowIndex).Value, qty)
        Double.TryParse(enterGrid("Price", rowIndex).Value, price)
        Double.TryParse(enterGrid("Disc1", rowIndex).Value, discount1)
        Double.TryParse(enterGrid("Disc2", rowIndex).Value, discount2)

        Return getRowAmount(qty, price, discount1, discount2)
    End Function

    Private Function getRowAmount(ByVal qty As Integer, ByVal price As Double, _
            ByVal discount1 As Double, ByVal discount2 As Double) As Double
        If Not IsNothing(qty) AndAlso Not IsNothing(price) Then
            Dim amount = qty * price
            amount = If(IsNothing(discount1), amount, _
                        amount - (amount * (discount1 / 100)))

            amount = If(IsNothing(discount2), amount, _
                        amount - (amount * (discount2 / 100)))
            Return amount
        End If

        Return Nothing
    End Function

    Private Sub loadGrid()
        enterGrid.Dock = DockStyle.Fill
        Util.clearRows(enterGrid)
        enterGrid.Columns.Clear()

        enterGrid.Columns.Add("Id", "Id")
        enterGrid.Columns.Add("Stock", "Stock")
        'enterGrid.Columns.Add("Desc", "Description")
        enterGrid.Columns.Add("Price", "Price")
        enterGrid.Columns.Add("Disc1", "Disc1")
        enterGrid.Columns.Add("Disc2", "Disc2")
        enterGrid.Columns.Add("Qty", "Qty")
        enterGrid.Columns.Add("Unit", "Unit")
        enterGrid.Columns.Add("Amount", "Amount")

        enterGrid.Columns.Item("Id").Visible = False
        setReadOnlyColumns()

        enterGrid.Columns.Item("Price").DefaultCellStyle.Alignment = _
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Disc1").DefaultCellStyle.Alignment = _
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Disc2").DefaultCellStyle.Alignment = _
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Amount").DefaultCellStyle.Alignment = _
            DataGridViewContentAlignment.MiddleRight

        enterGrid.Columns.Item("Disc1").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Disc2").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Price").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Amount").DefaultCellStyle.Format = "N2"

        enterGrid.Columns.Item("Qty").Width = 40
        enterGrid.Columns.Item("Unit").Width = 40

        enterGrid.Columns.Item("Stock").MinimumWidth = 270
        enterGrid.Columns.Item("Amount").MinimumWidth = 100
        'enterGrid.Columns.Item("Desc").MinimumWidth = 140
    End Sub

    Public Sub resetListSelection() Implements IControl.resetListSelection
    End Sub

    Private Sub enterGrid_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles enterGrid.EditingControlShowing
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If enterGrid.Columns.Item(enterGrid.CurrentCell.ColumnIndex).HeaderText = "Stock" Then
                Dim tb As TextBox = e.Control
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource
                tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                tb.AutoCompleteCustomSource = stockListDesc
            Else
                Dim tb As TextBox = e.Control
                tb.AutoCompleteMode = AutoCompleteMode.None
            End If
        End If
    End Sub

    Private Sub reloadStocks(ByVal name As String)
        stockListDesc.Clear()
        Using context As New bgmsEntities
            Dim qry As String = "select c.* from salesorderitems c, salesorders p, customers s " & _
                "where p.posteddate is not null and c.salesorderid = p.id and p.customerid = s.id " & _
                "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            Dim items As List(Of salesorderitem) = context.salesorderitems.SqlQuery(qry).ToList

            For Each item In items
                If Not stockListDesc.Contains(item.stock.Name.ToUpper) Then
                    stockListDesc.Add(item.stock.Name.ToUpper + " : " + item.stock.Description)
                End If
            Next
        End Using
    End Sub

    Private Sub loadAvailablePrices(ByVal stockId As Integer, _
            ByVal name As String, ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Using context As New bgmsEntities
            Dim qry As String = "select c.* from salesorderitems c, salesorders p, customers s " & _
                "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.salesorderid = p.id and p.customerid = s.id " & _
                "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            priceList = context.salesorderitems.SqlQuery(qry) _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Price).ToList
        End Using

        If setFields AndAlso priceList.Count > 0 Then
            enterGrid("Price", rowIndex).Value = priceList(0)
        End If
    End Sub

    Private Sub loadAvailableDiscounts1(ByVal rowIndex As Integer)
        If Not IsNothing(selectedStock) And _
            Not String.IsNullOrWhiteSpace(enterGrid("Price", rowIndex).Value) Then

            Dim p As Double
            Double.TryParse(enterGrid("Price", rowIndex).Value, p)

            loadAvailableDiscounts1(selectedStock.Id, tbCustomer.Text, _
                p, True, rowIndex)
        End If
    End Sub

    Private Sub loadAvailableDiscounts1(ByVal stockId As Integer, _
            ByVal name As String, ByVal price As Double, _
            ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Using context As New bgmsEntities
            Dim qry As String = "select c.* from salesorderitems c, salesorders p, customers s " & _
                "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price & _
                "' and c.salesorderid = p.id and p.customerid = s.id " & _
                "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            discountList1 = context.salesorderitems.SqlQuery(qry) _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Discount1).ToList
        End Using

        If setFields Then
            enterGrid("Disc1", rowIndex).Value = If(discountList1.Count > 0, discountList1(0), Nothing)
        End If
    End Sub

    Private Sub loadAvailableDiscounts2(ByVal rowIndex As Integer)
        If Not IsNothing(selectedStock) AndAlso _
            Not String.IsNullOrWhiteSpace(enterGrid("Price", rowIndex).Value) AndAlso _
            Not String.IsNullOrWhiteSpace(enterGrid("Disc1", rowIndex).Value) Then

            Dim p, d1 As Double
            Double.TryParse(enterGrid("Price", rowIndex).Value, p)
            Double.TryParse(enterGrid("Disc1", rowIndex).Value, d1)

            loadAvailableDiscounts2(selectedStock.Id, tbCustomer.Text, _
                p, d1, True, rowIndex)
        End If
    End Sub

    Private Sub loadAvailableDiscounts2(ByVal stockId As Integer, _
           ByVal name As String, ByVal price As Double, ByVal disc1 As Double, _
           ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Using context As New bgmsEntities
            Dim qry As String = "select c.* from salesorderitems c, salesorders p, customers s " & _
               "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price & "' and c.discount1 = '" & disc1 & _
               "' and c.salesorderid = p.id and p.customerid = s.id " & _
               "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            discountList2 = context.salesorderitems.SqlQuery(qry) _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Discount2).ToList
        End Using

        If setFields Then
            enterGrid("Disc2", rowIndex).Value = If(discountList2.Count > 0, discountList2(0), Nothing)
        End If
    End Sub

    Private Sub loadMaxTotalQuantity(ByVal rowIndex As Integer)
        If Not IsNothing(selectedStock) AndAlso _
            Not String.IsNullOrWhiteSpace(enterGrid("Price", rowIndex).Value) AndAlso _
            Not String.IsNullOrWhiteSpace(enterGrid("Disc1", rowIndex).Value) AndAlso _
            Not String.IsNullOrWhiteSpace(enterGrid("Disc2", rowIndex).Value) Then

            Dim p, d1, d2 As Double
            Double.TryParse(enterGrid("Price", rowIndex).Value, p)
            Double.TryParse(enterGrid("Disc1", rowIndex).Value, d1)
            Double.TryParse(enterGrid("Disc2", rowIndex).Value, d2)

            loadMaxTotalQuantity(selectedStock.Id, tbCustomer.Text, _
                p, d1, d2, True, rowIndex)
        End If
    End Sub

    Private Sub loadMaxTotalQuantity(ByVal stockId As Integer, ByVal name As String, _
            ByVal price As Double, ByVal disc1 As Double, ByVal disc2 As Double, _
            ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Dim maxQtyOrdered, maxQtyReturned As Integer
        Using context As New bgmsEntities
            Dim qry As String = "select c.* from salesorderitems c, salesorders p, customers s " & _
               "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price & _
               "' and c.discount1 = '" & disc1 & "' and c.discount2 = '" & disc2 & _
               "' and c.salesorderid = p.id and p.customerid = s.id " & _
               "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            Dim orderedItems = context.salesorderitems.SqlQuery(qry).ToList

            Dim anotherQry As String = "select c.* from salesreturnitems c, salesreturns p, customers s " & _
               "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price & _
               "' and c.discount1 = '" & disc1 & "' and c.discount2 = '" & disc2 & _
               "' and c.salesreturnid = p.id and p.customerid = s.id " & _
               "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            Dim returnedItems = context.salesreturnitems.SqlQuery(anotherQry).ToList

            maxQtyOrdered = 0
            For Each item In orderedItems
                maxQtyOrdered += item.Quantity
            Next

            maxQtyReturned = 0
            For Each item In returnedItems
                maxQtyReturned += item.Quantity
            Next

            maxQuantity = If(IsNothing(maxQtyReturned), maxQtyOrdered, _
                maxQtyOrdered - maxQtyReturned)
        End Using

        If setFields AndAlso Not IsNothing(maxQuantity) Then
            enterGrid("Qty", rowIndex).Value = maxQuantity
        End If
    End Sub

    Private Sub reloadSelectedStockVariables(ByVal rowIndex As Integer)
        reloadSelectedStockVariables(rowIndex, False)
    End Sub

    Private Sub reloadSelectedStockVariables(ByVal rowIndex As Integer, ByVal setFields As Boolean)
        loadAvailablePrices(selectedStock.Id, tbCustomer.Text, setFields, rowIndex)
        loadAvailableDiscounts1(selectedStock.Id, tbCustomer.Text, _
            enterGrid("Price", rowIndex).Value, setFields, rowIndex)
        loadAvailableDiscounts2(selectedStock.Id, tbCustomer.Text, _
            enterGrid("Price", rowIndex).Value, enterGrid("Disc1", rowIndex).Value, _
            setFields, rowIndex)
        loadMaxTotalQuantity(selectedStock.Id, tbCustomer.Text, _
            enterGrid("Price", rowIndex).Value, _
            enterGrid("Disc1", rowIndex).Value, _
            enterGrid("Disc2", rowIndex).Value, _
            setFields, rowIndex)
    End Sub

    Private Sub enterGrid_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGrid.RowValidating
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If Not String.IsNullOrWhiteSpace(enterGrid("Stock", e.RowIndex).Value) Then
                If Not stockListDesc.Contains(enterGrid("Stock", e.RowIndex).Value.ToString.ToUpper) Then
                    Util.notifyError("Invalid Stock Name.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Stock", e.RowIndex)
                    Exit Sub
                End If

                Dim price, disc1, disc2 As Double
                Dim qty As Integer

                Double.TryParse(enterGrid("Price", e.RowIndex).Value, price)
                Double.TryParse(enterGrid("Disc1", e.RowIndex).Value, disc1)
                Double.TryParse(enterGrid("Disc2", e.RowIndex).Value, disc2)
                Integer.TryParse(enterGrid("Qty", e.RowIndex).Value, qty)

                If IsNothing(price) OrElse price < 0 OrElse Not priceList.Contains(price) Then
                    Util.notifyError("Invalid Price value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Price", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(disc1) OrElse disc1 < 0 OrElse Not discountList1.Contains(disc1) Then
                    Util.notifyError("Invalid Discount 1 value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Disc1", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(disc2) OrElse disc2 < 0 OrElse Not discountList2.Contains(disc2) Then
                    Util.notifyError("Invalid Discount 2 value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Disc2", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(qty) OrElse qty < 0 Then
                    Util.notifyError("Invalid Quantity value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Qty", e.RowIndex)
                    Exit Sub
                ElseIf qty > maxQuantity Then
                    Util.notifyError("Maximum quantity to return is " & maxQuantity & ".")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Qty", e.RowIndex)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub enterGrid_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles enterGrid.RowEnter
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If Not IsNothing(enterGrid("Stock", e.RowIndex).Value) Then
                Dim stockName As String = enterGrid("Stock", e.RowIndex).Value.ToString
                Dim stockCode As String = stockName.Substring(0, stockName.LastIndexOf(":"))
                stockCode = stockCode.Trim

                If Not IsNothing(stockCode) Then
                    prevStockName = stockCode
                    Using context As New bgmsEntities
                        selectedStock = context.stocks _
                            .Where(Function(c) c.Name.Equals(stockCode) _
                                And c.Active = True).FirstOrDefault

                        If Not IsNothing(selectedStock) Then
                            reloadSelectedStockVariables(e.RowIndex)
                        End If
                    End Using
                Else
                    prevStockName = String.Empty
                End If
            Else
                prevStockName = String.Empty
            End If
        End If
    End Sub

    Public Sub printObject() Implements IControl.printObject
        Dim printDoc As New PrintTransaction

        If btnPrint.Visible And Not IsNothing(currentObject) Then
            Using context As New bgmsEntities
                printDoc.name = currentObject.customer.Name
                printDoc.address = currentObject.customer.Address
                printDoc.docNo = currentObject.DocumentNo
                printDoc.docDate = currentObject.Date
                printDoc.agent = currentObject.agent.Name

                printDoc.items = context.Database _
                    .SqlQuery(Of _Transaction)( _
                        "select i.quantity, if(i.quantity > 1, u.pluralname, u.name) as unit, s.name as stock, " & _
                        "s.description, i.price, i.discount1, i.discount2, 0 " & _
                        "from salesreturnitems i, stocks s, units u " & _
                        "where i.stockid = s.id and s.unitid = u.id " & _
                        "and i.salesreturnid = '" & currentObject.Id & "'") _
                    .ToList()
            End Using

            Util.previewDocument(printDoc)
        End If
    End Sub

    Dim previousFilter As String

    Private Sub tbCustomer_GotFocus(sender As Object, e As EventArgs) Handles tbCustomer.GotFocus
        previousFilter = tbCustomer.Text
    End Sub

    Private Sub tbCustomerChanged(sender As Object, e As EventArgs) Handles tbCustomer.Validated
        If Not String.IsNullOrEmpty(Controller.updateMode) _
            AndAlso Not previousFilter.ToUpper.Equals(tbCustomer.Text) Then
            Util.clearRows(enterGrid)

            If String.IsNullOrEmpty(tbCustomer.Text) Then
                enterGrid.ReadOnly = True
                stockListDesc.Clear()
            Else
                reloadStocks(tbCustomer.Text)
                enterGrid.ReadOnly = False
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printObject()
    End Sub

    Private Sub tbCustomer_TextChanged(sender As Object, e As EventArgs) Handles tbCustomer.TextChanged
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            Using context As New bgmsEntities
                Dim cust As customer = context.customers.Where(Function(c) _
                    c.Name.ToUpper.Equals(tbCustomer.Text.ToUpper) And c.Active = True).FirstOrDefault

                If Not IsNothing(cust) Then
                    tbAgent.Text = If(cust.agent.Active, cust.agent.Name, String.Empty)
                End If
            End Using
        End If
    End Sub

    Private Sub enterGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles enterGrid.CellEnter
        If Not String.IsNullOrEmpty(Controller.updateMode) AndAlso enterGrid.Focused Then
            If enterGrid.CurrentCell.ReadOnly Then
                SendKeys.Send("{TAB}")
            End If
        End If
    End Sub

    Private Sub setReadOnlyColumns()
        'enterGrid.Columns.Item("Desc").ReadOnly = True
        enterGrid.Columns.Item("Unit").ReadOnly = True
        enterGrid.Columns.Item("Amount").ReadOnly = True
    End Sub

End Class