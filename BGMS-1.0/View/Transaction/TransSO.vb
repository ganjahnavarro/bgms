Public Class TransSO
    Implements IControl

    Public currentObject As salesorder
    Dim ctr As counter
    Dim totalAmount As Double
    Dim retainIds As New List(Of Integer)
    Dim selectedStock As stock
    Dim prevStockName As String

    Dim currentPrefix, currentUnprefixedDoc As String

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

    Private Sub initialize()
        Me.reset()
        Me.loadObject()
        Me.enableInputs(False)
        Me.showUpdateButtons(False)
        Controller.updateMode = Nothing
    End Sub

    Public Sub validateItems()
        btnCheck.Visible = True
        btnCheck.Focus()
    End Sub

    Private Sub enterGrid_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGrid.CellBeginEdit
        btnCheck.Visible = False
        btnAddItem.Visible = False
    End Sub

    Public Sub addClick() Handles btnAdd.Click
        If btnAdd.Visible Then
            Controller.updateMode = Constants.UPDATE_MODE_CREATE
            showUpdateButtons(True)
            Me.reset()
            Me.enableInputs(True)
            setLatestDR()
            updateCountLabel()
        End If
    End Sub

    Private Sub setLatestDR()
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim nextCounter = context.counters.Where(Function(c) c.Prefix.Equals("DR")).FirstOrDefault
            tbDoc.Text = "DR" & nextCounter.Count
        End Using
    End Sub

    Public Sub editClick() Handles btnEdit.Click
        If btnEdit.Visible Then
            Controller.updateMode = Constants.UPDATE_MODE_EDIT
            showUpdateButtons(True)
            Me.enableInputs(True)
            Me.loadObject()
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
            Dim msg = getErrorMessage()
            If Not IsNothing(msg) Then
                Util.notifyError(msg)
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

        stockDescription.Text = String.Empty
        stockDescription.Visible = show

        If getAllowEditDelete() Then
            btnEdit.Visible = Not show
            btnDelete.Visible = Not show
        Else
            btnEdit.Visible = False
            btnDelete.Visible = False
        End If

        btnAdd.Visible = Not show
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
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = context.salesorders.Where(Function(c) _
                c.Id.Equals(currentObject.Id)).FirstOrDefault
            context.salesorderitems.RemoveRange(currentObject.salesorderitems)
            context.salesorders.Remove(currentObject)

            Dim action As String = Controller.currentUser.Username & " deleted a sales order (" &
               currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
        End Using

        'trash
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim trashItemAction = "delete from salesorderitems where salesorderid in " &
                " (select id from salesorders where documentno = ''" & currentObject.DocumentNo & "''" &
                " and modifydate <= ''" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'')"

            Dim trashAction = "delete from salesorders where documentno = ''" & currentObject.DocumentNo & "''" &
                " and modifydate <= ''" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "''"

            context.Database.ExecuteSqlCommand("insert into trash(date, action) values(current_date," &
                " '" & trashItemAction & ";" & trashAction & "')")
        End Using

        currentObject = Nothing
    End Sub

    Public Function getAllowEditDelete() As Boolean Implements IControl.getAllowEditDelete
        Return If(IsNothing(currentObject) OrElse Not IsNothing(currentObject.PostedDate), False, True)
    End Function

    Public Sub enableInputs(enable As Boolean) Implements IControl.enableInputs
        tbDisc1.Enabled = enable
        tbDisc2.Enabled = enable
        tbRemarks.Enabled = enable
        docDate.Enabled = enable
        tbAgent.Enabled = enable
        tbDoc.Enabled = enable
        tbCustomer.Enabled = enable
        btnAddItem.Visible = enable

        enterGrid.ReadOnly = Not enable
        setReadOnlyColumns()

        If enable Then
            tbDoc.Focus()
            enterGrid.ClearSelection()
        End If

        Try
            enterGrid.Columns("Available").Visible = enable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub setReadOnlyColumns()
        enterGrid.Columns.Item("Desc").ReadOnly = True
        enterGrid.Columns.Item("Unit").ReadOnly = True
        enterGrid.Columns.Item("Cost").ReadOnly = True
        enterGrid.Columns.Item("OnHand").ReadOnly = True
        enterGrid.Columns.Item("Available").ReadOnly = True
        enterGrid.Columns.Item("Amount").ReadOnly = True
    End Sub

    Public Sub loadObject() Implements IControl.loadObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            If Not IsNothing(currentObject) Then
                currentObject = context.salesorders _
                    .Include("Customer").Include("SalesOrderItems").Include("Agent") _
                    .Where(Function(f) f.Id.Equals(currentObject.Id)) _
                    .FirstOrDefault
                loadCurrentObject()
            Else
                currentObject = context.salesorders _
                    .Include("Customer").Include("SalesOrderItems").Include("Agent") _
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

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim nextObj As New salesorder
            nextObj = context.salesorders _
                .Where(Function(c) c.DocumentNo.CompareTo(currentObject.DocumentNo) > 0) _
                .OrderBy(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(nextObj) Then
                currentObject = nextObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub previousObject() Implements IControl.previousObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim prevObj As New salesorder
            prevObj = context.salesorders _
                .Where(Function(c) c.DocumentNo.CompareTo(currentObject.DocumentNo) < 0) _
                .OrderByDescending(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(prevObj) Then
                currentObject = prevObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub firstObject() Implements IControl.firstObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim firstObj As New salesorder
            firstObj = context.salesorders _
                .OrderBy(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(firstObj) Then
                currentObject = firstObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub lastObject() Implements IControl.lastObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim lastObj As New salesorder
            lastObj = context.salesorders _
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
        tbDisc1.Text = String.Empty
        tbDisc2.Text = String.Empty
        docDate.Value = DateTime.Today
        tbCustomer.Text = String.Empty
        tbAgent.Text = String.Empty

        Util.clearRows(enterGrid)
    End Sub

    Public Sub saveObject() Implements IControl.saveObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = New salesorder
            setObjectValues(context)
            context.salesorders.Add(currentObject)

            updateCounter(context)
            Dim action As String = Controller.currentUser.Username & " created a sales order (" &
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

        currentObject.DocumentNo = tbDoc.Text
        currentObject.Date = docDate.Value

        Dim d1, d2 As Double
        Double.TryParse(tbDisc1.Text, d1)
        Double.TryParse(tbDisc2.Text, d2)

        currentObject.Discount1 = d1
        currentObject.Discount2 = d2

        currentObject.Remarks = tbRemarks.Text
        currentObject.TotalAmount = totalAmount

        currentObject.ModifyBy = Controller.currentUser.Username
        currentObject.ModifyDate = DateTime.Now

        If String.IsNullOrEmpty(tbAgent.Text) Then
            currentObject.agent = Nothing
        Else
            currentObject.agentId = context.agents _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbAgent.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If

        If String.IsNullOrEmpty(tbCustomer.Text) Then
            currentObject.customer = Nothing
        Else
            currentObject.customerId = context.customers _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbCustomer.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If

        setObjectItemsValues(context)
    End Sub

    Public Function showAllButtons() As Integer Implements IControl.getButtonsShown
        Return Constants.SHOW_ALL
    End Function

    Public Sub updateObject() Implements IControl.updateObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = context.salesorders _
                .Where(Function(c) c.Id.Equals(currentObject.Id)).FirstOrDefault()
            setObjectValues(context)

            updateCounter(context)
            Dim action As String = Controller.currentUser.Username & " updated a sales order (" &
               currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
            Util.notifyInfo("Updated 1 Sales Order.")
        End Using
    End Sub

    Public Function getErrorMessage() As String Implements IControl.getErrorMessage
        If String.IsNullOrWhiteSpace(tbDoc.Text) Then
            Return "Invalid Document No."
        End If

        If Not getDocPrefix.Equals("DR") _
            AndAlso Not getDocPrefix.Equals("SI") _
            AndAlso Not getDocPrefix.Equals("PO") _
            AndAlso Not getDocPrefix.Equals("RF") Then
            Return "Invalid Document No."
        End If

        If IsNothing(docDate.Value) Then
            Return "Date is required."
        End If

        If String.IsNullOrWhiteSpace(tbCustomer.Text) Then
            Return "Customer is required."
        End If

        If Not Controller.customerList.Contains(tbCustomer.Text.ToUpper) Then
            Return "Invalid Customer Name."
        End If

        If String.IsNullOrWhiteSpace(tbAgent.Text) Then
            Return "Agent is required."
        End If

        If Not Controller.agentList.Contains(tbAgent.Text.ToUpper) Then
            Return "Invalid Agent Name."
        End If

        If Controller.updateMode.Equals(Constants.UPDATE_MODE_CREATE) _
            OrElse Not currentObject.DocumentNo.ToUpper.Equals(tbDoc.Text.ToUpper) Then

            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                Dim duplicate = context.salesorders _
                    .Where(Function(c) c.DocumentNo.ToUpper.Equals(tbDoc.Text.ToUpper)).FirstOrDefault

                If Not IsNothing(duplicate) Then
                    Dim doc = getDocPrefix()
                    Dim ctr = context.counters.Where(Function(c) c.Prefix.Equals(doc)).FirstOrDefault

                    If Not IsNothing(ctr) Then
                        Dim maxExistingValQuery As String = "select cast(substring(documentno, 3) as unsigned) as nextval from salesorders" &
                            " where documentno like '" & doc & "%' order by cast(substring(documentno, 3) as unsigned) desc limit 1"
                        Dim maxExistingVal As Integer = context.Database.SqlQuery(Of Integer)(maxExistingValQuery).FirstOrDefault

                        If Not IsNothing(maxExistingVal) Then
                            tbDoc.Text = getDocPrefix() & (maxExistingVal + 1)
                            Return "Document No. already exists. Use this instead?"
                        End If
                    End If
                    Return "Document No. already exists."
                End If
            End Using
        End If

        If enterGrid.RowCount <= 1 Then
            Return Constants.ERROR_MSG_INPUT_ITEMS
        End If

        Return Nothing
    End Function

    Private Sub updateCounter(ByRef context As bgmsEntities)
        Dim doc = getDocPrefix()
        ctr = context.counters.Where(Function(c) _
            c.Prefix.ToUpper.Equals(doc)).FirstOrDefault

        If Not IsNothing(ctr) Then
            Dim maxExistingValQuery As String = "select cast(substring(documentno, 3) as unsigned) as nextval from salesorders" &
            " where documentno like '" & doc & "%' order by cast(substring(documentno, 3) as unsigned) desc limit 1"
            Dim maxExistingVal As Integer = context.Database.SqlQuery(Of Integer)(maxExistingValQuery).FirstOrDefault

            If Not IsNothing(maxExistingVal) Then
                Dim currentVal = CInt(currentObject.DocumentNo.Substring(2))
                Dim comparedVal = Math.Max(maxExistingVal, currentVal)
                ctr.Count = comparedVal + 1
            Else
                ctr.Count += 1
            End If
        End If
    End Sub

    Private Function getDocPrefix() As String
        If Not String.IsNullOrWhiteSpace(tbDoc.Text) _
            AndAlso tbDoc.TextLength > 2 Then
            Return tbDoc.Text.Substring(0, 2).ToUpper
        End If

        Return String.Empty
    End Function

    Private Sub loadCurrentObject()
        tbDoc.Text = currentObject.DocumentNo

        lblBy.Text = Util.fmtModifyBy(currentObject.ModifyBy)
        lblOn.Text = Util.fmtModifyDate(currentObject.ModifyDate)
        lblBy.Visible = True
        lblOn.Visible = True

        tbDisc1.Text = FormatNumber(CDbl(currentObject.Discount1), 2)
        If currentObject.Discount1 = 0 Then
            tbDisc1.Text = Nothing
        End If

        tbDisc2.Text = FormatNumber(CDbl(currentObject.Discount2), 2)
        If currentObject.Discount2 = 0 Then
            tbDisc2.Text = Nothing
        End If

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

        loadObjectItems(currentObject.salesorderitems.ToList())
        updateColumnSizes()
    End Sub

    Private Sub updateCountLabel()
        lblCount.Text = enterGrid.Rows.Count - 1
    End Sub

    Private Sub enterGrid_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles enterGrid.UserAddedRow
        updateCountLabel()
    End Sub

    Private Sub loadObjectItems(ByVal orderItems As List(Of salesorderitem))
        Util.clearRows(enterGrid)
        For Each orderItem In orderItems
            enterGrid.Rows.Add(
                orderItem.Id,
                orderItem.stock.Name,
                orderItem.stock.Description,
                orderItem.Quantity,
                orderItem.stock.unit.Name,
                orderItem.Price,
                orderItem.stock.Cost,
                orderItem.stock.QtyOnHand, "",
                orderItem.Discount1,
                orderItem.Discount2,
                getRowAmount(orderItem.Quantity, orderItem.Price,
                    orderItem.Discount1, orderItem.Discount2))
        Next
        updateCountLabel()
    End Sub

    Private Sub setObjectItemsValues(ByRef context As bgmsEntities)
        retainIds.Clear()

        For rowIndex = 0 To enterGrid.RowCount - 2
            Dim itemId As Integer = If(String.IsNullOrEmpty(enterGrid("Id", rowIndex).Value),
                Nothing, enterGrid("Id", rowIndex).Value)

            If Not IsNothing(itemId) And itemId <> 0 Then
                For Each orderItem In currentObject.salesorderitems
                    If itemId = orderItem.Id Then
                        setItemValues(orderItem, rowIndex, context)
                        retainIds.Add(itemId)
                        Exit For
                    End If
                Next
            Else
                Dim orderItem As New salesorderitem
                setItemValues(orderItem, rowIndex, context)
                currentObject.salesorderitems.Add(orderItem)
            End If
        Next

        For count = currentObject.salesorderitems.Count - 1 To 0 Step -1
            If Not IsNothing(currentObject.salesorderitems(count).Id) _
                And currentObject.salesorderitems(count).Id <> 0 _
                And Not retainIds.Contains(currentObject.salesorderitems(count).Id) Then

                context.salesorderitems _
                    .Remove(currentObject.salesorderitems(count))
            End If
        Next
    End Sub

    Private Sub setItemValues(ByRef orderItem As salesorderitem,
                              ByVal rowIndex As Integer, ByRef context As bgmsEntities)
        Dim stockName As String = enterGrid("Stock", rowIndex).Value

        orderItem.Price = enterGrid("Price", rowIndex).Value
        orderItem.Quantity = enterGrid("Qty", rowIndex).Value

        Dim d1, d2 As Double
        Double.TryParse(enterGrid("Disc1", rowIndex).Value, d1)
        Double.TryParse(enterGrid("Disc2", rowIndex).Value, d2)
        orderItem.Discount1 = d1
        orderItem.Discount2 = d2

        orderItem.stockId = context.stocks _
                   .Where(Function(c) c.Name.ToUpper.Equals(stockName.Trim.ToUpper) And c.Active = True) _
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
        updateColumnSizes()
        updateTotalAmount()
    End Sub

    Private Sub updateTotalAmount()
        totalAmount = 0

        For rowIndex = 0 To enterGrid.RowCount - 2
            totalAmount += If(IsNothing(getRowAmount(rowIndex)), 0, getRowAmount(rowIndex))
        Next
        tbTotalAmt.Text = FormatNumber(CDbl(totalAmount), 2)
    End Sub

    Private Sub discountAChanged(sender As Object, e As EventArgs) Handles tbDisc1.TextChanged
        populateDiscountColumn(tbDisc1, "Disc1")
    End Sub

    Private Sub discountBChanged(sender As Object, e As EventArgs) Handles tbDisc2.TextChanged
        populateDiscountColumn(tbDisc2, "Disc2")
    End Sub

    Private Sub enterGrid_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles enterGrid.CellValidated
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            Select Case e.ColumnIndex
                Case 1
                    stockChanged(e)
                    Exit Select
                Case 3, 5, 8, 9
                    varsChanged(e)
                    Exit Select
            End Select
        End If
    End Sub

    Private Sub stockChanged(ByVal e As DataGridViewCellEventArgs)
        If Not IsNothing(enterGrid("Stock", e.RowIndex).Value) Then
            Dim stockName As String = enterGrid("Stock", e.RowIndex).Value

            If Not IsNothing(stockName) Then
                stockName = stockName.ToUpper.Trim

                If stockName.Equals(prevStockName) Then
                    Exit Sub
                End If

                prevStockName = stockName
                Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                    selectedStock = context.stocks _
                        .Where(Function(c) c.Name.Equals(stockName) And c.Active = True).FirstOrDefault

                    If Not IsNothing(selectedStock) Then
                        enterGrid("Desc", e.RowIndex).Value = selectedStock.Description
                        enterGrid("Unit", e.RowIndex).Value = selectedStock.unit.Name
                        enterGrid("Cost", e.RowIndex).Value = selectedStock.Cost
                        enterGrid("OnHand", e.RowIndex).Value = selectedStock.QtyOnHand
                        enterGrid("Available", e.RowIndex).Value = Util.getStockAvailableQty(selectedStock.Id)

                        'If IsNothing(enterGrid("Price", e.RowIndex).Value) Then
                        enterGrid("Price", e.RowIndex).Value = selectedStock.Price
                        'End If

                        If IsNothing(enterGrid("Qty", e.RowIndex).Value) Then
                            enterGrid("Qty", e.RowIndex).Value = 1
                        End If

                        If IsNothing(enterGrid("Disc1", e.RowIndex).Value) Then
                            enterGrid("Disc1", e.RowIndex).Value = tbDisc1.Text
                        End If

                        If IsNothing(enterGrid("Disc2", e.RowIndex).Value) Then
                            enterGrid("Disc2", e.RowIndex).Value = tbDisc2.Text
                        End If

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

    Public Function getRowAmount(ByVal qty As Double, ByVal price As Double,
            ByVal discount1 As Double, ByVal discount2 As Double) As Double
        If Not IsNothing(qty) And Not IsNothing(price) Then
            Dim amount = qty * price
            amount = If(IsNothing(discount1), amount,
                        amount - (amount * (discount1 / 100)))

            amount = If(IsNothing(discount2), amount,
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
        enterGrid.Columns.Add("Desc", "Description")
        enterGrid.Columns.Add("Qty", "Qty")
        enterGrid.Columns.Add("Unit", "Unit")
        enterGrid.Columns.Add("Price", "Price")
        enterGrid.Columns.Add("Cost", "Cost")
        enterGrid.Columns.Add("OnHand", "OnHand")
        enterGrid.Columns.Add("Available", "Available")
        enterGrid.Columns.Add("Disc1", "Disc1")
        enterGrid.Columns.Add("Disc2", "Disc2")
        enterGrid.Columns.Add("Amount", "Amount")

        enterGrid.Columns.Item("Id").Visible = False
        enterGrid.Columns.Item("OnHand").Visible = False
        enterGrid.Columns.Item("Cost").Visible = If(Controller.currentUser.Admin, True, False)

        setReadOnlyColumns()

        enterGrid.Columns.Item("Cost").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Price").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Disc1").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Disc2").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Amount").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight

        enterGrid.Columns.Item("Disc1").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Disc2").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Cost").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Price").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Amount").DefaultCellStyle.Format = "N2"

        enterGrid.Columns.Item("Qty").Width = 40
        enterGrid.Columns.Item("Unit").Width = 40

        enterGrid.Columns.Item("Stock").MinimumWidth = 130
        enterGrid.Columns.Item("Amount").MinimumWidth = 100
        enterGrid.Columns.Item("Desc").MinimumWidth = 140

        updateColumnSizes()
    End Sub

    Private Sub updateColumnSizes()
        If Not String.IsNullOrEmpty(tbDisc1.Text) And Not String.IsNullOrEmpty(tbDisc2.Text) Then
            enterGrid.Columns.Item("Disc1").Visible = True
            enterGrid.Columns.Item("Disc2").Visible = True
        ElseIf Not String.IsNullOrEmpty(tbDisc1.Text) Then
            enterGrid.Columns.Item("Disc1").Visible = True
            enterGrid.Columns.Item("Disc2").Visible = False
        ElseIf Not String.IsNullOrEmpty(tbDisc2.Text) Then
            enterGrid.Columns.Item("Disc1").Visible = False
            enterGrid.Columns.Item("Disc2").Visible = True
        Else
            enterGrid.Columns.Item("Disc1").Visible = False
            enterGrid.Columns.Item("Disc2").Visible = False
        End If
    End Sub

    Public Sub resetListSelection() Implements IControl.resetListSelection
    End Sub

    Private Sub enterGrid_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles enterGrid.EditingControlShowing
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If enterGrid.Columns.Item(enterGrid.CurrentCell.ColumnIndex).HeaderText = "Stock" Then
                Dim tb As TextBox = e.Control
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource
                tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                tb.AutoCompleteCustomSource = Controller.stockList

                AddHandler tb.TextChanged, AddressOf stockInputChanged
            Else
                Dim tb As TextBox = e.Control
                tb.AutoCompleteMode = AutoCompleteMode.None
            End If
        End If
    End Sub

    Private Sub stockInputChanged(sender As Object, e As EventArgs)
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            Dim tb As TextBox = sender

            If Controller.stockDictionary.ContainsKey(tb.Text) Then
                Controller.stockDictionary.TryGetValue(tb.Text, stockDescription.Text)
            Else
                stockDescription.Text = String.Empty
            End If
        End If
    End Sub

    Private Sub enterGrid_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGrid.RowValidating
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If Not String.IsNullOrWhiteSpace(enterGrid("Stock", e.RowIndex).Value) Then
                Dim stockName = enterGrid("Stock", e.RowIndex).Value

                If Not IsNothing(stockName) AndAlso Not Controller.stockList.Contains(stockName.ToString.ToUpper) Then
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

                If IsNothing(price) OrElse price < 0 Then
                    Util.notifyError("Invalid Price value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Price", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(disc1) OrElse disc1 < 0 Then
                    Util.notifyError("Invalid Discount 1 value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Disc1", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(disc2) OrElse disc2 < 0 Then
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
                End If
            End If
        End If
    End Sub

    Public Sub printObject() Implements IControl.printObject
        Dim printDoc As New PrintTransaction

        If btnPrint.Visible And Not IsNothing(currentObject) Then
            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                printDoc.name = currentObject.customer.Name
                printDoc.agent = currentObject.agent.Name
                printDoc.address = currentObject.customer.Address
                printDoc.docNo = currentObject.DocumentNo
                printDoc.docDate = currentObject.Date

                printDoc.items = context.Database _
                    .SqlQuery(Of _Transaction)(
                        "select i.quantity, if(i.quantity > 1, u.pluralname, u.name) as unit, s.name as stock, " &
                        "s.description, i.price, i.discount1, i.discount2, 0 " &
                        "from salesorderitems i, stocks s, units u " &
                        "where i.stockid = s.id and s.unitid = u.id " &
                        "and i.salesorderid = '" & currentObject.Id & "'") _
                    .ToList()
            End Using

            Util.previewDocument(printDoc)
        End If
    End Sub

    Private Sub tbCustomer_TextChanged(sender As Object, e As EventArgs) Handles tbCustomer.TextChanged
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                Dim cust As customer = context.customers.Where(Function(c) _
                    c.Name.ToUpper.Equals(tbCustomer.Text.ToUpper) And c.Active = True).FirstOrDefault

                If Not IsNothing(cust) Then
                    tbAgent.Text = If(cust.agent.Active, cust.agent.Name, String.Empty)
                End If
            End Using
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printObject()
    End Sub

    Private Sub enterGrid_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles enterGrid.RowEnter
        Dim stockValue = enterGrid("Stock", e.RowIndex).Value
        prevStockName = If(Not IsNothing(stockValue), stockValue, String.Empty)
    End Sub

    Private Sub enterGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles enterGrid.CellEnter
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If enterGrid.Focused AndAlso enterGrid.CurrentCell.ReadOnly Then
                SendKeys.Send("{TAB}")
            End If

            btnAddItem.Visible = Not enterGrid.IsCurrentRowDirty
        End If
    End Sub

    Private Sub btnAddItem_Click(sender As Object, e As EventArgs) Handles btnAddItem.Click
        showAddItem()
    End Sub

    Public Sub showAddItem()
        If btnAddItem.Visible Then
            Double.TryParse(tbDisc1.Text, EtcAddItem.d1)
            Double.TryParse(tbDisc2.Text, EtcAddItem.d2)

            EtcAddItem.d1enabled = If(String.IsNullOrEmpty(tbDisc1.Text), False, True)
            EtcAddItem.d2enabled = If(String.IsNullOrEmpty(tbDisc2.Text), False, True)

            EtcAddItem.fromPO = False
            EtcAddItem.openUp(Me.Name)
        End If
    End Sub

    Public Sub addOrderItem(ByVal values As List(Of String))
        Dim qty As Integer
        Dim price, cost, d1, d2 As Double

        Integer.TryParse(values(3), qty)
        Double.TryParse(values(5), price)
        Double.TryParse(values(6), cost)
        Double.TryParse(values(9), d1)
        Double.TryParse(values(10), d2)

        enterGrid.Rows.Add(values(0), values(1), values(2), _
            qty, values(4), price, cost, values(7), values(8), _
            d1, d2, getRowAmount(qty, price, d1, d2))

        enterGrid.CurrentCell = enterGrid("Stock", enterGrid.RowCount - 2)
        updateCountLabel()
    End Sub

End Class