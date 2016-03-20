Public Class TransCC
    Implements IControl

    Public currentObject As customercollection
    Dim checkTotalAmount As Double = 0
    Dim paidTotalAmount As Double = 0

    Dim orderList As New AutoCompleteStringCollection
    Dim retainIds As New List(Of Integer)
    Dim selectedOrder As salesorder
    Dim prevOrderName As String

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initCustomers()
        tbCustomer.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbCustomer.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbCustomer.AutoCompleteCustomSource = Controller.customerList

        loadGrids()
        initialize()
        prevOrderName = String.Empty
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
        Me.showUpdateButtons(False)
        Controller.updateMode = Nothing
        Me.enableInputs(False)
    End Sub

    Public Sub validateItems()
        btnCheck.Visible = True
        btnCheck.Focus()
    End Sub

    Private Sub enterGridChecks_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGridChecks.CellBeginEdit
        btnCheck.Visible = False
    End Sub

    Private Sub enterGridChecks_GotFocus(sender As Object, e As EventArgs) Handles enterGridChecks.GotFocus
        enterGridOrders.ClearSelection()
    End Sub

    Private Sub enterGridOrders_CellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGridOrders.CellBeginEdit
        btnCheck.Visible = False
        addItems.Visible = False
    End Sub

    Private Sub enterGridOrders_GotFocus(sender As Object, e As EventArgs) Handles enterGridOrders.GotFocus
        enterGridChecks.ClearSelection()
    End Sub

    Public Sub addClick() Handles btnAdd.Click
        If Not IsNothing(Me) And btnAdd.Visible Then
            Controller.updateMode = Constants.UPDATE_MODE_CREATE
            Me.reset()
            Me.enableInputs(True)
            showUpdateButtons(True)
        End If
    End Sub

    Public Sub editClick() Handles btnEdit.Click
        If Not IsNothing(Me) And btnEdit.Visible Then
            Controller.updateMode = Constants.UPDATE_MODE_EDIT
            Me.enableInputs(True)
            Me.loadObject()
            showUpdateButtons(True)
            reloadOrders(tbCustomer.Text)
            updateTotalAmount()
        End If
    End Sub

    Public Sub deleteClick() Handles btnDelete.Click
        If Not IsNothing(Me) And btnDelete.Visible Then
            If Util.askForConfirmation("Delete this item?", "Delete") Then
                Me.deleteObject()
                Me.resetListSelection()
                resetMe()
            End If
        End If
    End Sub

    Public Sub cancelClick() Handles btnCancel.Click
        If Not IsNothing(Me) And btnCancel.Visible Then
            Util.clearRows(enterGridOrders)
            Util.clearRows(enterGridChecks)
            resetMe()
        End If
    End Sub

    Public Sub checkClick() Handles btnCheck.Click
        If Not IsNothing(Me) And btnCheck.Visible Then
            Dim msg As String = getErrorMessage()
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
        If Not IsNothing(Me) And btnNext.Visible Then
            Me.nextObject()
        End If
    End Sub

    Private Sub nextDblClick() Handles btnNext.DoubleClick
        If Not IsNothing(Me) And btnNext.Visible Then
            Me.lastObject()
        End If
    End Sub

    Private Sub previousClick() Handles btnPrev.Click
        If Not IsNothing(Me) And btnPrev.Visible Then
            Me.previousObject()
        End If
    End Sub

    Private Sub previousDblClick() Handles btnPrev.DoubleClick
        If Not IsNothing(Me) And btnPrev.Visible Then
            Me.firstObject()
        End If
    End Sub

    Public Sub searchClick() Handles btnSearch.Click
        If Not IsNothing(Me) And btnSearch.Visible Then
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
        showUpdateButtons(False)
        Controller.updateMode = Nothing
        Me.enableInputs(False)
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
    End Sub

#End Region

    Public Sub deleteObject() Implements IControl.deleteObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = context.customercollections.Where(Function(c) _
                c.Id.Equals(currentObject.Id)).FirstOrDefault
            context.collectioncheckitems.RemoveRange(currentObject.collectioncheckitems)
            context.collectionorderitems.RemoveRange(currentObject.collectionorderitems)
            context.customercollections.Remove(currentObject)

            Dim action As String = Controller.currentUser.Username & " deleted a customer collection (" &
                   currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
        End Using
    End Sub

    Public Sub deleteRow() Implements IControl.deleteRow
        If Not IsNothing(Controller.updateMode) AndAlso enterGridChecks.Focused _
            AndAlso Not IsNothing(enterGridChecks.CurrentCell) _
            AndAlso enterGridChecks.CurrentCell.RowIndex >= 0 Then
            Try
                enterGridChecks.Rows.RemoveAt(enterGridChecks.CurrentCell.RowIndex)
                updateTotalAmount()
            Catch ex As Exception
                For Each cell As DataGridViewCell In enterGridChecks _
                        .Rows(enterGridChecks.CurrentCell.RowIndex).Cells()
                    cell.Value = String.Empty
                Next
            End Try
        End If

        If Not IsNothing(Controller.updateMode) AndAlso enterGridOrders.Focused _
            AndAlso Not IsNothing(enterGridOrders.CurrentCell) _
            AndAlso enterGridOrders.CurrentCell.RowIndex >= 0 Then
            Try
                enterGridOrders.Rows.RemoveAt(enterGridOrders.CurrentCell.RowIndex)
                updateTotalAmount()
            Catch ex As Exception
                For Each cell As DataGridViewCell In enterGridOrders _
                        .Rows(enterGridOrders.CurrentCell.RowIndex).Cells()
                    cell.Value = String.Empty
                Next
            End Try
        End If
    End Sub

    Private Sub updateTotalAmount()
        updateCheckTotalAmount()
        updateOrdersTotalAmount()
    End Sub

    Private Sub updateCheckTotalAmount()
        checkTotalAmount = 0
        For rowIndex = 0 To enterGridChecks.RowCount - 2
            Dim amount As Double
            Double.TryParse(enterGridChecks("Amount", rowIndex).Value, amount)
            checkTotalAmount += amount
        Next
        tbTotalCheck.Text = FormatNumber(checkTotalAmount, 2)
    End Sub

    Private Sub updateOrdersTotalAmount()
        paidTotalAmount = 0
        For rowIndex = 0 To enterGridOrders.RowCount - 2
            Dim amount As Double
            Double.TryParse(enterGridOrders("Paid", rowIndex).Value, amount)
            paidTotalAmount += amount
        Next
        tbTotalPaid.Text = FormatNumber(paidTotalAmount, 2)
    End Sub

    Public Sub enableInputs(enable As Boolean) Implements IControl.enableInputs
        tbRemarks.Enabled = enable
        docDate.Enabled = enable
        tbCustomer.Enabled = enable
        tbBank.Enabled = enable

        enterGridChecks.ReadOnly = Not enable
        enterGridOrders.ReadOnly = Not enable
        setReadOnlyColumns()

        If enable Then
            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                tbDocNo.Text = getUpdatedDocumentNo(context)
            End Using

            tbCustomer.Focus()
            enterGridChecks.ClearSelection()
            enterGridOrders.ClearSelection()
        End If

        addItems.Visible = If(enable AndAlso Not IsNothing(Controller.updateMode) _
            AndAlso Controller.updateMode.Equals(Constants.UPDATE_MODE_EDIT), True, False)
    End Sub

    Public Sub nextObject() Implements IControl.nextObject
        If IsNothing(currentObject) Then
            Exit Sub
        End If

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim nextObj As New customercollection
            nextObj = context.customercollections _
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
            Dim prevObj As New customercollection
            prevObj = context.customercollections _
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
            Dim firstObj As New customercollection
            firstObj = context.customercollections _
                .OrderBy(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(firstObj) Then
                currentObject = firstObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub lastObject() Implements IControl.lastObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim lastObj As New customercollection
            lastObj = context.customercollections _
                .OrderByDescending(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(lastObj) Then
                currentObject = lastObj
                loadObject()
            End If
        End Using
    End Sub

    Public Function getAllowEditDelete() As Boolean Implements IControl.getAllowEditDelete
        Return If(IsNothing(currentObject) OrElse Not IsNothing(currentObject.PostedDate), False, True)
    End Function

    Public Function getButtonsShown() As Integer Implements IControl.getButtonsShown
        Return Constants.SHOW_ALL
    End Function

    Public Function getErrorMessage() As String Implements IControl.getErrorMessage
        If String.IsNullOrWhiteSpace(tbCustomer.Text) Then
            Return "Customer name is required."
        End If

        If Not Controller.customerList.Contains(tbCustomer.Text.ToUpper) Then
            Return "Invalid Customer name."
        End If

        If IsNothing(docDate.Value) Then
            Return "Date is required."
        End If

        If enterGridChecks.RowCount <= 1 Then
            Return Constants.ERROR_MSG_INPUT_ITEMS
        End If

        If enterGridOrders.RowCount <= 1 Then
            Return Constants.ERROR_MSG_INPUT_ITEMS
        End If

        Return Nothing
    End Function

    Public Sub loadObject() Implements IControl.loadObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)

            If Not IsNothing(currentObject) Then
                currentObject = context.customercollections _
                    .Include("Customer").Include("CollectionOrderItems") _
                    .Include("CollectionCheckItems") _
                .Where(Function(f) f.Id.Equals(currentObject.Id)) _
                .FirstOrDefault()
                loadCurrentObject()
            Else
                currentObject = context.customercollections _
                    .Include("Customer").Include("CollectionOrderItems") _
                    .Include("CollectionCheckItems") _
                .OrderByDescending(Function(f) f.DocumentNo) _
                .FirstOrDefault()

                If Not IsNothing(currentObject) Then
                    loadCurrentObject()
                Else
                    reset()
                End If
            End If
        End Using
    End Sub

    Private Sub loadCurrentObject()
        lblBy.Text = Util.fmtModifyBy(currentObject.ModifyBy)
        lblOn.Text = Util.fmtModifyDate(currentObject.ModifyDate)
        lblBy.Visible = True
        lblOn.Visible = True

        tbBank.Text = currentObject.Bank
        tbRemarks.Text = currentObject.Remarks
        docDate.Value = currentObject.Date
        tbTotalCheck.Text = FormatNumber(CDbl(currentObject.TotalCheck), 2)
        tbTotalPaid.Text = FormatNumber(CDbl(currentObject.TotalPaid), 2)
        tbCustomer.Text = currentObject.customer.Name
        tbDocNo.Text = currentObject.DocumentNo

        lblPostedOn.Visible = If(IsNothing(currentObject.PostedDate), False, True)
        lblPostedDate.Visible = If(IsNothing(currentObject.PostedDate), False, True)
        lblPostedDate.Text = Format(currentObject.PostedDate, Constants.DATE_FORMAT)

        Dim modifiable = If(IsNothing(Controller.updateMode) And IsNothing(currentObject.PostedDate), True, False)
        btnEdit.Visible = modifiable
        btnDelete.Visible = modifiable

        loadObjectItems()
    End Sub

    Private Sub loadObjectItems()
        loadCheckItems()
        loadOrderItems()
    End Sub

    Private Sub loadCheckItems()
        Util.clearRows(enterGridChecks)

        For Each orderItem In currentObject.collectioncheckitems
            enterGridChecks.Rows.Add(
                orderItem.Id,
                orderItem.DocumentNo,
                Format(orderItem.Date, Constants.DATE_FORMAT),
                orderItem.Amount)
        Next
    End Sub

    Private Sub loadOrderItems()
        Util.clearRows(enterGridOrders)

        For Each orderItem In currentObject.collectionorderitems
            enterGridOrders.Rows.Add(
                orderItem.Id,
                orderItem.salesorder.DocumentNo,
                Format(orderItem.salesorder.Date, Constants.DATE_FORMAT),
                orderItem.BalanceGot,
                orderItem.Amount)
        Next
    End Sub

    Public Sub reset() Implements IControl.reset
        lblBy.Visible = False
        lblOn.Visible = False
        lblPostedDate.Visible = False
        lblPostedOn.Visible = False

        tbDocNo.Text = String.Empty
        tbRemarks.Text = String.Empty
        tbTotalCheck.Text = String.Empty
        tbTotalPaid.Text = String.Empty
        tbBank.Text = String.Empty
        docDate.Value = DateTime.Today
        tbCustomer.Text = String.Empty

        Util.clearRows(enterGridChecks)
        Util.clearRows(enterGridOrders)
    End Sub

    Public Sub resetListSelection() Implements IControl.resetListSelection
    End Sub

    Public Sub saveObject() Implements IControl.saveObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = New customercollection
            setObjectValues(context)

            Dim counter = context.counters.Where(Function(c) _
                c.Owner.Equals(Constants.OWNER_NAME_CUSTOMER_COLLECTION)) _
               .SingleOrDefault()

            counter.Count += 1

            Dim action As String = Controller.currentUser.Username & " created a customer collection (" &
                   currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.customercollections.Add(currentObject)
            context.SaveChanges()
            Util.notifyInfo("Created 1 Customer Collection.")
        End Using
    End Sub

    Public Sub search() Implements IControl.search
        EtcSearch.openUp(Me.Name)
    End Sub

    Public Sub setObjectValues(ByRef context As bgmsEntities)
        updateTotalAmount()

        If Controller.updateMode.Equals(Constants.UPDATE_MODE_CREATE) Then
            currentObject.DocumentNo = getUpdatedDocumentNo(context)
        End If

        currentObject.Date = docDate.Value
        currentObject.Remarks = tbRemarks.Text
        currentObject.TotalPaid = paidTotalAmount
        currentObject.TotalCheck = checkTotalAmount

        currentObject.Bank = If(String.IsNullOrEmpty(tbBank.Text),
            Constants.DEFAULT_BANK, tbBank.Text)

        currentObject.ModifyBy = Controller.currentUser.Username
        currentObject.ModifyDate = DateTime.Now

        If String.IsNullOrEmpty(tbCustomer.Text) Then
            currentObject.customer = Nothing
        Else
            currentObject.customerId = context.customers _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbCustomer.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If

        setObjectItemsValues(context)
    End Sub

    Private Function getUpdatedDocumentNo(ByRef context As bgmsEntities) As String
        Dim counter = context.counters.Where(Function(c) _
               c.Owner.Equals(Constants.OWNER_NAME_CUSTOMER_COLLECTION)).FirstOrDefault

        If Not IsNothing(counter) Then
            Return Util.getFormattedDocumentNo(counter.Prefix, counter.Count)
        End If

        Return Nothing
    End Function

    Private Sub setObjectItemsValues(ByRef context As bgmsEntities)
        setCheckItemsValues(context)
        setOrderItemsValues(context)
    End Sub

    Private Sub setCheckItemsValues(ByRef context As bgmsEntities)
        retainIds.Clear()

        For rowIndex = 0 To enterGridChecks.RowCount - 2

            Dim itemId As Integer = If(String.IsNullOrEmpty(enterGridChecks("Id", rowIndex).Value),
                 Nothing, enterGridChecks("Id", rowIndex).Value)

            If Not IsNothing(itemId) And itemId <> 0 Then
                For Each item In currentObject.collectioncheckitems
                    If itemId = item.Id Then
                        setCheckItem(item, rowIndex)
                        retainIds.Add(itemId)
                        Exit For
                    End If
                Next
            Else
                Dim orderItem As New collectioncheckitem
                setCheckItem(orderItem, rowIndex)
                currentObject.collectioncheckitems.Add(orderItem)
            End If
        Next

        For count = currentObject.collectioncheckitems.Count - 1 To 0 Step -1
            If Not IsNothing(currentObject.collectioncheckitems(count).Id) _
                And currentObject.collectioncheckitems(count).Id <> 0 _
                And Not retainIds.Contains(currentObject.collectioncheckitems(count).Id) Then

                context.collectioncheckitems _
                    .Remove(currentObject.collectioncheckitems(count))
            End If
        Next
    End Sub

    Private Sub setCheckItem(ByRef item As collectioncheckitem, ByVal rowIndex As Integer)
        item.DocumentNo = enterGridChecks("Check", rowIndex).Value
        item.Amount = enterGridChecks("Amount", rowIndex).Value
        Date.TryParse(enterGridChecks("Date", rowIndex).Value, item.Date)
    End Sub

    Private Sub enterGridChecks_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGridChecks.RowValidating
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If Not String.IsNullOrWhiteSpace(enterGridChecks("Check", e.RowIndex).Value) _
                OrElse Not String.IsNullOrWhiteSpace(enterGridChecks("Amount", e.RowIndex).Value) _
                OrElse Not String.IsNullOrWhiteSpace(enterGridChecks("Date", e.RowIndex).Value) Then

                If String.IsNullOrWhiteSpace(enterGridChecks("Check", e.RowIndex).Value) Then
                    Util.notifyError("Check is required.")
                    e.Cancel = True
                    enterGridChecks.CurrentCell = enterGridChecks("Check", e.RowIndex)
                    Exit Sub
                End If

                If String.IsNullOrWhiteSpace(enterGridChecks("Amount", e.RowIndex).Value) Then
                    Util.notifyError("Amount is required.")
                    e.Cancel = True
                    enterGridChecks.CurrentCell = enterGridChecks("Amount", e.RowIndex)
                    Exit Sub
                End If

                If String.IsNullOrWhiteSpace(enterGridChecks("Date", e.RowIndex).Value) Then
                    Util.notifyError("Date is required.")
                    e.Cancel = True
                    enterGridChecks.CurrentCell = enterGridChecks("Date", e.RowIndex)
                    Exit Sub
                End If

                Dim dt As DateTime
                DateTime.TryParse(enterGridChecks("Date", e.RowIndex).Value, dt)

                If dt.CompareTo(Util.getValidDate) < 0 Then
                    Util.notifyError("Invalid Date.")
                    e.Cancel = True
                    enterGridChecks.CurrentCell = enterGridChecks("Date", e.RowIndex)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Private Sub setOrderItemsValues(ByRef context As bgmsEntities)
        retainIds.Clear()

        For rowIndex = 0 To enterGridOrders.RowCount - 2

            Dim itemId As Integer = If(String.IsNullOrEmpty(enterGridOrders("Id", rowIndex).Value),
                 Nothing, enterGridOrders("Id", rowIndex).Value)

            If Not IsNothing(itemId) And itemId <> 0 Then
                For Each item In currentObject.collectionorderitems
                    If itemId = item.Id Then
                        setOrderItem(item, rowIndex, context)
                        retainIds.Add(itemId)
                        Exit For
                    End If
                Next
            Else
                Dim orderItem As New collectionorderitem
                setOrderItem(orderItem, rowIndex, context)
                currentObject.collectionorderitems.Add(orderItem)
            End If
        Next

        For count = currentObject.collectionorderitems.Count - 1 To 0 Step -1
            If Not IsNothing(currentObject.collectionorderitems(count).Id) _
                And currentObject.collectionorderitems(count).Id <> 0 _
                And Not retainIds.Contains(currentObject.collectionorderitems(count).Id) Then

                context.collectionorderitems _
                    .Remove(currentObject.collectionorderitems(count))
            End If
        Next
    End Sub

    Private Sub setOrderItem(ByRef item As collectionorderitem,
           ByVal rowIndex As Integer, ByRef context As bgmsEntities)
        Dim docNo As String = enterGridOrders("SO", rowIndex).Value
        item.salesorderId = context.salesorders _
            .Where(Function(c) c.DocumentNo.ToUpper.Equals(docNo.ToUpper)) _
            .Select(Function(c) c.Id).FirstOrDefault
        item.Amount = enterGridOrders("Paid", rowIndex).Value
        item.BalanceGot = enterGridOrders("Balance", rowIndex).Value
    End Sub

    Public Sub updateObject() Implements IControl.updateObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = context.customercollections _
                .Where(Function(c) c.Id.Equals(currentObject.Id)).FirstOrDefault()
            setObjectValues(context)

            Dim action As String = Controller.currentUser.Username & " updated a customer collection (" &
                   currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
            Util.notifyInfo("Updated 1 Customer Collection.")
        End Using
    End Sub

    Private Sub loadGrids()
        loadEnterGridChecks()
        loadEnterGridOrders()
    End Sub

    Private Sub loadEnterGridChecks()
        enterGridChecks.Dock = DockStyle.Fill
        Util.clearRows(enterGridChecks)
        enterGridChecks.Columns.Clear()

        enterGridChecks.Columns.Add("Id", "Id")
        enterGridChecks.Columns.Add("Check", "Check No.")
        enterGridChecks.Columns.Add("Date", "Date")
        enterGridChecks.Columns.Add("Amount", "Amount")

        enterGridChecks.Columns.Item("Id").Visible = False

        enterGridChecks.Columns.Item("Amount").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGridChecks.Columns.Item("Amount").DefaultCellStyle.Format = "N2"
    End Sub

    Private Sub loadEnterGridOrders()
        enterGridOrders.Dock = DockStyle.Fill
        Util.clearRows(enterGridOrders)
        enterGridOrders.Columns.Clear()

        enterGridOrders.Columns.Add("Id", "Id")
        enterGridOrders.Columns.Add("SO", "SO")
        enterGridOrders.Columns.Add("Date", "Date")
        enterGridOrders.Columns.Add("Balance", "Balance")
        enterGridOrders.Columns.Add("Paid", "Paid")

        enterGridOrders.Columns.Item("Id").Visible = False
        setReadOnlyColumns()

        enterGridOrders.Columns.Item("Balance").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGridOrders.Columns.Item("Paid").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight

        enterGridOrders.Columns.Item("Balance").DefaultCellStyle.Format = "N2"
        enterGridOrders.Columns.Item("Paid").DefaultCellStyle.Format = "N2"
    End Sub

    Private Sub reloadOrders(ByVal name As String)
        orderList.Clear()
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim qry As String = "select s.* from salesorders s, customers c " &
                "where s.customerId = c.Id and s.postedDate is not null and ucase(c.Name) = '" _
                & name.ToUpper & "' and c.active = true"

            orderList.AddRange(context.salesorders.SqlQuery(qry) _
                .Select(Function(c) c.DocumentNo.ToUpper) _
                .Distinct().ToArray())
        End Using
    End Sub

    Private Sub enterGridOrders_CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles enterGridOrders.CellValidated
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            Select Case e.ColumnIndex
                Case 1
                    orderChanged(e)
                    Exit Select
            End Select
        End If
    End Sub

    Private Sub orderChanged(ByVal e As DataGridViewCellEventArgs)
        orderChanged(e.RowIndex)
    End Sub

    Private Sub orderChanged(ByVal rowIndex As Integer)
        If Not IsNothing(enterGridOrders("SO", rowIndex).Value) Then
            Dim orderDocNo As String = enterGridOrders("SO", rowIndex).Value.ToString

            If Not String.IsNullOrEmpty(orderDocNo) Then
                If orderDocNo.ToUpper.Equals(prevOrderName.ToUpper) Then
                    Exit Sub
                End If

                prevOrderName = orderDocNo
                Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                    selectedOrder = context.salesorders _
                        .Where(Function(c) c.DocumentNo.ToUpper.Equals(orderDocNo.ToUpper)) _
                        .FirstOrDefault

                    If Not IsNothing(selectedOrder) Then
                        enterGridOrders("Paid", rowIndex).Value = 0
                        updateOrdersTotalAmount()

                        Dim remaining As Double = checkTotalAmount - paidTotalAmount

                        enterGridOrders("Date", rowIndex).Value = Format(selectedOrder.Date, Constants.DATE_FORMAT)
                        enterGridOrders("Balance", rowIndex).Value = selectedOrder.getBalance
                        enterGridOrders("Paid", rowIndex).Value =
                            If(remaining >= selectedOrder.getBalance, selectedOrder.getBalance, remaining)
                    End If
                End Using
            End If
            updateOrdersTotalAmount()
        End If
    End Sub

    Private Sub enterGrid_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles enterGridChecks.CellValueChanged
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If e.ColumnIndex = 3 Then
                updateCheckTotalAmount()
                updatePaidFromChecks()
            End If
        End If
    End Sub

    Private Sub enterGridOrders_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles enterGridOrders.EditingControlShowing
        If enterGridOrders.Columns.Item(enterGridOrders.CurrentCell.ColumnIndex).HeaderText = "SO" Then
            Dim tb As TextBox = e.Control
            tb.AutoCompleteSource = AutoCompleteSource.CustomSource
            tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            tb.AutoCompleteCustomSource = orderList
        Else
            Dim tb As TextBox = e.Control
            tb.AutoCompleteMode = AutoCompleteMode.None
        End If
    End Sub

    Private Sub enterGridOrders_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGridOrders.RowValidating
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If Not String.IsNullOrWhiteSpace(enterGridOrders("SO", e.RowIndex).Value) Then
                If Not orderList.Contains(enterGridOrders("SO", e.RowIndex).Value.ToString.ToUpper) Then
                    Util.notifyError("Invalid Sales Order.")
                    e.Cancel = True
                    enterGridOrders.CurrentCell = enterGridOrders("SO", e.RowIndex)
                    Exit Sub
                End If
            End If
        End If
    End Sub

    Public Sub printObject() Implements IControl.printObject
    End Sub

    Dim previousFilter As String

    Private Sub tbCustomer_GotFocus(sender As Object, e As EventArgs) Handles tbCustomer.GotFocus
        previousFilter = tbCustomer.Text
    End Sub

    Private Sub tbCustomer_Validated(sender As Object, e As EventArgs) Handles tbCustomer.Validated
        If Not String.IsNullOrEmpty(Controller.updateMode) _
            AndAlso Not previousFilter.ToUpper.Equals(tbCustomer.Text) Then
            Util.clearRows(enterGridOrders)
            If String.IsNullOrEmpty(tbCustomer.Text) Then
                enterGridOrders.ReadOnly = True
                orderList.Clear()
                addItems.Visible = False
            Else
                reloadOrders(tbCustomer.Text)
                enterGridOrders.ReadOnly = False
                addItems.Visible = True
            End If
        End If
    End Sub

    Public Sub showSelection() Handles addItems.Click
        If addItems.Visible Then
            Dim addedOrders As New List(Of String)

            For rowIndex = 0 To enterGridOrders.RowCount - 2
                Dim doc = enterGridOrders("SO", rowIndex).Value

                If Not IsNothing(doc) Then
                    addedOrders.Add(doc.ToString.ToUpper)
                End If
            Next

            SelectCC.addedOrders = addedOrders
            SelectCC.customerName = tbCustomer.Text
            SelectCC.initialPaid = paidTotalAmount
            SelectCC.totalCheck = checkTotalAmount
            SelectCC.openUp(Me.Name)
        End If
    End Sub

    Private Sub updatePaidFromChecks()
        Dim remaining As Double = checkTotalAmount

        For rowIndex = 0 To enterGridOrders.RowCount - 2
            Dim balance, paid As Double
            Double.TryParse(enterGridOrders("Balance", rowIndex).Value, balance)

            paid = If(remaining >= balance, balance, remaining)
            enterGridOrders("Paid", rowIndex).Value = FormatNumber(paid, 2)
            remaining = remaining - balance
        Next

        updateOrdersTotalAmount()
    End Sub

    Public Sub addOrders(ByVal orders As List(Of String))
        For Each order In orders
            enterGridOrders.Rows.Add(String.Empty, order)
            enterGridOrders.CurrentCell = enterGridOrders("SO", enterGridOrders.RowCount - 2)
            prevOrderName = String.Empty
            orderChanged(enterGridOrders.CurrentCell.RowIndex)
        Next
    End Sub

    Private Sub enterGridOrders_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles enterGridOrders.RowEnter
        Dim orderValue = enterGridOrders("SO", e.RowIndex).Value
        prevOrderName = If(Not IsNothing(orderValue), orderValue, String.Empty)
    End Sub

    Private Sub enterGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles enterGridOrders.CellEnter
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If enterGridOrders.Focused AndAlso enterGridOrders.CurrentCell.ReadOnly Then
                SendKeys.Send("{TAB}")
            End If

            addItems.Visible = Not enterGridOrders.IsCurrentRowDirty
        End If
    End Sub

    Private Sub setReadOnlyColumns()
        enterGridOrders.Columns.Item("Date").ReadOnly = True
        enterGridOrders.Columns.Item("Balance").ReadOnly = True
        enterGridOrders.Columns.Item("Paid").ReadOnly = True
    End Sub

End Class