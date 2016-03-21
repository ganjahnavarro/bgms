Public Class TransPR
    Implements IControl

    Public currentObject As purchasereturn
    Dim retainIds As New List(Of Integer)
    Dim selectedStock As stock
    Dim totalAmount As Double
    Dim prevStockName As String

    Dim stockList As New AutoCompleteStringCollection
    Dim priceList As New List(Of Double)
    Dim discount1List As New List(Of Double)
    Dim discount2List As New List(Of Double)
    Dim discount3List As New List(Of Double)
    Dim maxQuantity As Integer

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        Controller.initSuppliers()
        tbSupplier.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbSupplier.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbSupplier.AutoCompleteCustomSource = Controller.supplierList

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
            tbDocNo.Text = getUpdatedDocumentNo()
            updateCountLabel()
        End If
    End Sub

    Public Sub editClick() Handles btnEdit.Click
        If btnEdit.Visible Then
            Me.enableInputs(True)
            Controller.updateMode = Constants.UPDATE_MODE_EDIT
            Me.loadObject()
            showUpdateButtons(True)

            reloadStocks(tbSupplier.Text)
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

        stockDescription.Text = String.Empty
        stockDescription.Visible = show

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

    Public Sub search() Implements IControl.search
        EtcSearch.openUp(Me.Name)
    End Sub

    Public Sub loadObject() Implements IControl.loadObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            If Not IsNothing(currentObject) Then
                currentObject = context.purchasereturns _
                    .Include("PurchaseReturnItems").Include("Supplier") _
                    .Where(Function(f) f.Id.Equals(currentObject.Id)) _
                    .FirstOrDefault
                loadCurrentObject()
            Else
                currentObject = context.purchasereturns _
                    .Include("PurchaseReturnItems").Include("Supplier") _
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

    Private Sub loadCurrentObject()
        lblBy.Text = Util.fmtModifyBy(currentObject.ModifyBy)
        lblOn.Text = Util.fmtModifyDate(currentObject.ModifyDate)
        lblBy.Visible = True
        lblOn.Visible = True

        tbDocNo.Text = currentObject.DocumentNo
        tbRemarks.Text = currentObject.Remarks
        docDate.Value = currentObject.Date
        tbTotalAmt.Text = FormatNumber(CDbl(currentObject.TotalAmount), 2)
        tbSupplier.Text = currentObject.supplier.Name

        lblPostedOn.Visible = If(IsNothing(currentObject.PostedDate), False, True)
        lblPostedDate.Visible = If(IsNothing(currentObject.PostedDate), False, True)
        lblPostedDate.Text = Format(currentObject.PostedDate, Constants.DATE_FORMAT)

        Dim modifiable = If(IsNothing(Controller.updateMode) And IsNothing(currentObject.PostedDate), True, False)
        btnEdit.Visible = modifiable
        btnDelete.Visible = modifiable

        loadObjectItems(currentObject.purchasereturnitems.ToList())
    End Sub

    Public Function getAllowEditDelete() As Boolean Implements IControl.getAllowEditDelete
        Return If(IsNothing(currentObject) OrElse Not IsNothing(currentObject.PostedDate), False, True)
    End Function

    Private Sub loadObjectItems(ByVal orderItems As List(Of purchasereturnitem))
        Util.clearRows(enterGrid)

        Dim hasDisc1, hasDisc2, hasDisc3 As Boolean

        For Each orderItem In orderItems
            enterGrid.Rows.Add(
                orderItem.Id,
                orderItem.stock.Name,
                orderItem.stock.Description,
                orderItem.Price,
                orderItem.Discount1,
                orderItem.Discount2,
                orderItem.Discount3,
                orderItem.Quantity,
                orderItem.stock.unit.Name,
                getRowAmount(orderItem.Quantity, orderItem.Price,
                    orderItem.Discount1, orderItem.Discount2, orderItem.Discount3))

            hasDisc1 = If(orderItem.Discount1 > 0, True, hasDisc1)
            hasDisc2 = If(orderItem.Discount2 > 0, True, hasDisc2)
            hasDisc3 = If(orderItem.Discount3 > 0, True, hasDisc3)
        Next

        enterGrid.Columns.Item("Disc1").Visible = hasDisc1
        enterGrid.Columns.Item("Disc2").Visible = hasDisc2
        enterGrid.Columns.Item("Disc3").Visible = hasDisc3
        updateCountLabel()
    End Sub

    Private Sub updateCountLabel()
        lblCount.Text = enterGrid.Rows.Count - 1
    End Sub

    Private Sub enterGrid_UserAddedRow(sender As Object, e As DataGridViewRowEventArgs) Handles enterGrid.UserAddedRow
        updateCountLabel()
    End Sub

    Public Sub deleteObject() Implements IControl.deleteObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = context.purchasereturns.Where(Function(c) _
                    c.Id.Equals(currentObject.Id)).FirstOrDefault
            context.purchasereturnitems.RemoveRange(currentObject.purchasereturnitems)
            context.purchasereturns.Remove(currentObject)

            Dim action As String = Controller.currentUser.Username & " deleted a purchase return (" &
                currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
        End Using

        'trash
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim trashItemAction = "delete from purchasereturnitems where purchasereturnid in " &
                " (select id from purchasereturns where documentno = ''" & currentObject.DocumentNo & "''" &
                " and modifydate <= ''" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "'')"

            Dim trashAction = "delete from purchasereturns where documentno = ''" & currentObject.DocumentNo & "''" &
                " and modifydate <= ''" & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & "''"

            context.Database.ExecuteSqlCommand("insert into trash(date, action) values(current_date," &
                " '" & trashItemAction & ";" & trashAction & "')")
        End Using

        currentObject = Nothing
    End Sub

    Public Sub enableInputs(ByVal enable As Boolean) Implements IControl.enableInputs
        tbRemarks.Enabled = enable
        docDate.Enabled = enable
        tbSupplier.Enabled = enable

        enterGrid.ReadOnly = Not enable
        setReadOnlyColumns()

        If enable = True Then
            tbDocNo.Text = getUpdatedDocumentNo()
            tbSupplier.Focus()
            enterGrid.ClearSelection()

            enterGrid.Columns.Item("Disc1").Visible = True
            enterGrid.Columns.Item("Disc2").Visible = True
            enterGrid.Columns.Item("Disc3").Visible = True
        End If
    End Sub

    Private Function getUpdatedDocumentNo() As String
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim counter = context.counters.Where(Function(c) _
               c.Owner.Equals(Constants.OWNER_NAME_PURCHASE_RETURN)).FirstOrDefault

            If Not IsNothing(counter) Then
                Return Util.getFormattedDocumentNo(counter.Prefix, counter.Count)
            End If
        End Using
        Return Nothing
    End Function

    Public Sub reset() Implements IControl.reset
        lblBy.Visible = False
        lblOn.Visible = False
        lblPostedDate.Visible = False
        lblPostedOn.Visible = False

        tbDocNo.Text = String.Empty
        tbRemarks.Text = String.Empty
        tbTotalAmt.Text = String.Empty
        docDate.Value = DateTime.Today
        tbSupplier.Text = String.Empty

        Util.clearRows(enterGrid)
    End Sub

    Public Sub saveObject() Implements IControl.saveObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = New purchasereturn
            setObjectValues(context)

            Dim counter = context.counters.Where(Function(c) _
            c.Owner.Equals(Constants.OWNER_NAME_PURCHASE_RETURN)) _
                .SingleOrDefault()
            counter.Count += 1

            context.purchasereturns.Add(currentObject)

            Dim action As String = Controller.currentUser.Username & " created a purchase return (" &
                currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
            Util.notifyInfo("Created 1 Purchase Return.")
        End Using
    End Sub

    Public Sub updateObject() Implements IControl.updateObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            currentObject = context.purchasereturns _
                    .Where(Function(c) c.Id.Equals(currentObject.Id)).FirstOrDefault()
            setObjectValues(context)

            Dim action As String = Controller.currentUser.Username & " updated a purchase return (" &
                currentObject.DocumentNo & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
            Util.notifyInfo("Updated 1 Purchase Return.")
        End Using
    End Sub


    Public Sub setObjectValues(ByRef context As bgmsEntities)
        updateTotalAmount()

        If Controller.updateMode.Equals(Constants.UPDATE_MODE_CREATE) Then
            currentObject.DocumentNo = getUpdatedDocumentNo()
        End If

        currentObject.Date = docDate.Value
        currentObject.ModifyBy = Controller.currentUser.Username
        currentObject.ModifyDate = DateTime.Now
        currentObject.Remarks = tbRemarks.Text
        currentObject.TotalAmount = totalAmount

        If String.IsNullOrEmpty(tbSupplier.Text) Then
            currentObject.supplier = Nothing
        Else
            currentObject.supplierId = context.suppliers _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbSupplier.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If

        sebObjectItemsValues(context)
    End Sub

    Private Sub sebObjectItemsValues(ByRef context As bgmsEntities)
        retainIds.Clear()

        For rowIndex = 0 To enterGrid.RowCount - 2

            Dim itemId As Integer = If(String.IsNullOrEmpty(enterGrid("Id", rowIndex).Value),
                Nothing, enterGrid("Id", rowIndex).Value)

            If Not IsNothing(itemId) AndAlso itemId <> 0 Then
                For Each orderItem In currentObject.purchasereturnitems
                    If itemId = orderItem.Id Then
                        setItemValues(orderItem, rowIndex, context)
                        retainIds.Add(itemId)
                        Exit For
                    End If
                Next
            Else
                Dim orderItem As New purchasereturnitem
                setItemValues(orderItem, rowIndex, context)
                currentObject.purchasereturnitems.Add(orderItem)
            End If
        Next

        For count = currentObject.purchasereturnitems.Count - 1 To 0 Step -1
            If Not IsNothing(currentObject.purchasereturnitems(count).Id) _
                AndAlso currentObject.purchasereturnitems(count).Id <> 0 _
                AndAlso Not retainIds.Contains(currentObject.purchasereturnitems(count).Id) Then

                context.purchasereturnitems _
                    .Remove(currentObject.purchasereturnitems(count))
            End If
        Next
    End Sub

    Private Sub setItemValues(ByRef orderItem As purchasereturnitem,
                              ByVal rowIndex As Integer, ByRef context As bgmsEntities)
        Dim stockName As String = enterGrid("Stock", rowIndex).Value

        orderItem.Price = enterGrid("Price", rowIndex).Value
        orderItem.Quantity = enterGrid("Qty", rowIndex).Value
        orderItem.Discount1 = enterGrid("Disc1", rowIndex).Value
        orderItem.Discount2 = enterGrid("Disc2", rowIndex).Value
        orderItem.Discount3 = enterGrid("Disc3", rowIndex).Value

        orderItem.stockId = context.stocks _
                   .Where(Function(c) c.Name.ToUpper.Equals(stockName.Trim.ToUpper) AndAlso c.Active = True) _
                   .Select(Function(c) c.Id).FirstOrDefault
    End Sub

    Public Function getErrorMessage() As String Implements IControl.getErrorMessage
        If String.IsNullOrWhiteSpace(tbSupplier.Text) Then
            Return "Supplier name is required."
        End If

        If Not Controller.supplierList.Contains(tbSupplier.Text.ToUpper) Then
            Return "Invalid Supplier name."
        End If

        If enterGrid.RowCount <= 1 Then
            Return Constants.ERROR_MSG_INPUT_ITEMS
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
        enterGrid.Columns.Add("Price", "Price")
        enterGrid.Columns.Add("Disc1", "D1")
        enterGrid.Columns.Add("Disc2", "D2")
        enterGrid.Columns.Add("Disc3", "D3")
        enterGrid.Columns.Add("Qty", "Qty")
        enterGrid.Columns.Add("Unit", "Unit")
        enterGrid.Columns.Add("Amount", "Amount")

        enterGrid.Columns.Item("Qty").Width = 40
        enterGrid.Columns.Item("Unit").Width = 40

        enterGrid.Columns.Item("Stock").MinimumWidth = 130
        enterGrid.Columns.Item("Amount").MinimumWidth = 100
        enterGrid.Columns.Item("Desc").MinimumWidth = 140

        enterGrid.Columns.Item("Id").Visible = False
        setReadOnlyColumns()

        enterGrid.Columns.Item("Price").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Disc1").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Disc2").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Disc3").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight
        enterGrid.Columns.Item("Amount").DefaultCellStyle.Alignment =
            DataGridViewContentAlignment.MiddleRight

        enterGrid.Columns.Item("Price").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Disc1").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Disc2").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Disc3").DefaultCellStyle.Format = "N2"
        enterGrid.Columns.Item("Amount").DefaultCellStyle.Format = "N2"
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
                    loadAvailableDiscounts3(e.RowIndex)
                    loadMaxTotalQuantity(e.RowIndex)
                    varsChanged(e)
                    Exit Select
                Case 4
                    loadAvailableDiscounts2(e.RowIndex)
                    loadAvailableDiscounts3(e.RowIndex)
                    loadMaxTotalQuantity(e.RowIndex)
                    varsChanged(e)
                    Exit Select
                Case 5
                    loadAvailableDiscounts3(e.RowIndex)
                    loadMaxTotalQuantity(e.RowIndex)
                    varsChanged(e)
                    Exit Select
                Case 6
                    loadMaxTotalQuantity(e.RowIndex)
                    varsChanged(e)
                    Exit Select
                Case 7
                    varsChanged(e)
                    Exit Select
            End Select
        End If
    End Sub

    Private Sub stockChanged(ByVal e As DataGridViewCellEventArgs)
        If Not IsNothing(enterGrid("Stock", e.RowIndex).Value) Then
            Dim stockName As String = enterGrid("Stock", e.RowIndex).Value

            If Not IsNothing(stockName) Then
                stockName = stockName.Trim.ToUpper

                If stockName.Equals(prevStockName.Trim.ToUpper) Then
                    Exit Sub
                End If

                prevStockName = stockName
                Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                    selectedStock = context.stocks _
                        .Where(Function(c) c.Name.Equals(stockName) AndAlso c.Active = True).FirstOrDefault

                    If Not IsNothing(selectedStock) Then
                        enterGrid("Desc", e.RowIndex).Value = selectedStock.Description
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
        enterGrid("Amount", e.RowIndex).Value = getRowAmount(e.RowIndex)
        updateTotalAmount()
    End Sub


    Private Function getRowAmount(ByVal rowIndex As Integer) As Double
        Dim qty, price, disc1, disc2, disc3 As Double

        Double.TryParse(enterGrid("Qty", rowIndex).Value, qty)
        Double.TryParse(enterGrid("Price", rowIndex).Value, price)
        Double.TryParse(enterGrid("Disc1", rowIndex).Value, disc1)
        Double.TryParse(enterGrid("Disc2", rowIndex).Value, disc2)
        Double.TryParse(enterGrid("Disc3", rowIndex).Value, disc3)

        Return getRowAmount(qty, price, disc1, disc2, disc3)
    End Function

    Private Function getRowAmount(ByVal qty As Integer, ByVal price As Double,
        ByVal disc1 As Double, ByVal disc2 As Double, ByVal disc3 As Double) As Double
        If Not IsNothing(qty) AndAlso Not IsNothing(price) Then
            Dim amount = qty * price
            amount = If(IsNothing(disc1), amount,
                        amount - (amount * (disc1 / 100)))
            amount = If(IsNothing(disc2), amount,
                        amount - (amount * (disc2 / 100)))
            amount = If(IsNothing(disc3), amount,
                        amount - (amount * (disc3 / 100)))
            Return amount
        End If

        Return Nothing
    End Function

    Private Sub updateTotalAmount()
        totalAmount = 0

        For rowIndex = 0 To enterGrid.RowCount - 2
            totalAmount += If(IsNothing(getRowAmount(rowIndex)), 0, getRowAmount(rowIndex))
        Next
        tbTotalAmt.Text = FormatNumber(CDbl(totalAmount), 2)
    End Sub

    Public Sub nextObject() Implements IControl.nextObject
        If IsNothing(currentObject) Then
            Exit Sub
        End If

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim nextObj As New purchasereturn
            nextObj = context.purchasereturns _
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

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim prevObj As New purchasereturn
            prevObj = context.purchasereturns _
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
            Dim firstObj As New purchasereturn
            firstObj = context.purchasereturns _
                .OrderBy(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(firstObj) Then
                currentObject = firstObj
                loadObject()
            End If
        End Using
    End Sub

    Public Sub lastObject() Implements IControl.lastObject
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim lastObj As New purchasereturn
            lastObj = context.purchasereturns _
                .OrderByDescending(Function(c) c.DocumentNo).FirstOrDefault

            If Not IsNothing(lastObj) Then
                currentObject = lastObj
                loadObject()
            End If
        End Using
    End Sub

    Public Function showAllButtons() As Integer Implements IControl.getButtonsShown
        Return Constants.SHOW_ALL
    End Function

    Public Sub resetListSelection() Implements IControl.resetListSelection
    End Sub

    Private Sub enterGrid_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles enterGrid.EditingControlShowing
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If enterGrid.Columns.Item(enterGrid.CurrentCell.ColumnIndex).HeaderText = "Stock" Then
                Dim tb As TextBox = e.Control
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource
                tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend
                tb.AutoCompleteCustomSource = stockList

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

    Private Sub reloadStocks(ByVal name As String)
        stockList.Clear()
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim qry As String = "select c.* from purchaseorderitems c, purchaseorders p, suppliers s " &
                "where p.posteddate is not null and c.purchaseorderid = p.id and p.supplierid = s.id " &
                "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            Dim items As List(Of purchaseorderitem) = context.purchaseorderitems.SqlQuery(qry).ToList

            For Each item In items
                If Not stockList.Contains(item.stock.Name.ToUpper) Then
                    Console.WriteLine("Item: " & item.stock.Name)
                    stockList.Add(item.stock.Name.Trim.ToUpper)
                End If
            Next
        End Using
    End Sub

    Private Sub loadAvailablePrices(ByVal stockId As Integer,
            ByVal name As String, ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim qry As String = "select c.* from purchaseorderitems c, purchaseorders p, suppliers s " &
                "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.purchaseorderid = p.id and p.supplierid = s.id " &
                "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            priceList = context.purchaseorderitems.SqlQuery(qry) _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Price).ToList
        End Using

        If setFields AndAlso priceList.Count > 0 Then
            enterGrid("Price", rowIndex).Value = priceList(0)
        End If
    End Sub

    Private Sub loadAvailableDiscounts1(ByVal rowIndex As Integer)
        If Not IsNothing(selectedStock) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Price", rowIndex).Value) Then

            Dim p As Double
            Double.TryParse(enterGrid("Price", rowIndex).Value, p)

            loadAvailableDiscounts1(selectedStock.Id, tbSupplier.Text,
                p, True, rowIndex)
        End If
    End Sub

    Private Sub loadAvailableDiscounts1(ByVal stockId As Integer, ByVal name As String,
            ByVal price As Double, ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim qry As String = "select c.* from purchaseorderitems c, purchaseorders p, suppliers s " &
                "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price &
                "' and c.purchaseorderid = p.id and p.supplierid = s.id " &
                "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            discount1List = context.purchaseorderitems.SqlQuery(qry) _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Discount1).ToList
        End Using

        If setFields Then
            enterGrid("Disc1", rowIndex).Value = If(discount1List.Count > 0, discount1List(0), Nothing)
        End If
    End Sub

    Private Sub loadAvailableDiscounts2(ByVal rowIndex As Integer)
        If Not IsNothing(selectedStock) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Price", rowIndex).Value) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Disc1", rowIndex).Value) Then

            Dim p, d1 As Double
            Double.TryParse(enterGrid("Price", rowIndex).Value, p)
            Double.TryParse(enterGrid("Disc1", rowIndex).Value, d1)

            loadAvailableDiscounts2(selectedStock.Id, tbSupplier.Text,
                p, d1, True, rowIndex)
        End If
    End Sub

    Private Sub loadAvailableDiscounts2(ByVal stockId As Integer, ByVal name As String,
            ByVal price As Double, ByVal disc1 As Double, ByVal setFields As Boolean,
            ByVal rowIndex As Integer)
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim qry As String = "select c.* from purchaseorderitems c, purchaseorders p, suppliers s " &
               "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price & "' and c.discount1 = '" & disc1 &
               "' and c.purchaseorderid = p.id and p.supplierid = s.id " &
               "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            discount2List = context.purchaseorderitems.SqlQuery(qry) _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Discount2).ToList
        End Using

        If setFields Then
            enterGrid("Disc2", rowIndex).Value = If(discount2List.Count > 0, discount2List(0), Nothing)
        End If
    End Sub

    Private Sub loadAvailableDiscounts3(ByVal rowIndex As Integer)
        If Not IsNothing(selectedStock) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Price", rowIndex).Value) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Disc1", rowIndex).Value) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Disc2", rowIndex).Value) Then

            Dim p, d1, d2 As Double
            Double.TryParse(enterGrid("Price", rowIndex).Value, p)
            Double.TryParse(enterGrid("Disc1", rowIndex).Value, d1)
            Double.TryParse(enterGrid("Disc2", rowIndex).Value, d2)

            loadAvailableDiscounts3(selectedStock.Id, tbSupplier.Text,
                p, d1, d2, True, rowIndex)
        End If
    End Sub

    Private Sub loadAvailableDiscounts3(ByVal stockId As Integer, ByVal name As String,
            ByVal price As Double, ByVal disc1 As Double, ByVal disc2 As Double,
            ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim qry As String = "select c.* from purchaseorderitems c, purchaseorders p, suppliers s " &
               "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price &
               "' and c.discount1 = '" & disc1 & "' and c.discount2 = '" & disc2 &
               "' and c.purchaseorderid = p.id and p.supplierid = s.id " &
               "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            discount3List = context.purchaseorderitems.SqlQuery(qry) _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Discount3).ToList
        End Using

        If setFields Then
            enterGrid("Disc3", rowIndex).Value = If(discount3List.Count > 0, discount3List(0), Nothing)
        End If
    End Sub

    Private Sub loadMaxTotalQuantity(ByVal rowIndex As Integer)
        If Not IsNothing(selectedStock) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Price", rowIndex).Value) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Disc1", rowIndex).Value) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Disc2", rowIndex).Value) AndAlso
            Not String.IsNullOrWhiteSpace(enterGrid("Disc3", rowIndex).Value) Then

            Dim p, d1, d2, d3 As Double
            Double.TryParse(enterGrid("Price", rowIndex).Value, p)
            Double.TryParse(enterGrid("Disc1", rowIndex).Value, d1)
            Double.TryParse(enterGrid("Disc2", rowIndex).Value, d2)
            Double.TryParse(enterGrid("Disc3", rowIndex).Value, d3)

            loadMaxTotalQuantity(selectedStock.Id, tbSupplier.Text,
               p, d1, d2, d3, True, rowIndex)
        End If
    End Sub

    Private Sub loadMaxTotalQuantity(ByVal stockId As Integer, ByVal name As String,
            ByVal price As Double, ByVal disc1 As Double, ByVal disc2 As Double, ByVal disc3 As Double,
            ByVal setFields As Boolean, ByVal rowIndex As Integer)
        Dim maxQtyOrdered, maxQtyReturned As Integer

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim qry As String = "select c.* from purchaseorderitems c, purchaseorders p, suppliers s " &
               "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price &
               "' and c.discount1 = '" & disc1 & "' and c.discount2 = '" & disc2 & "' and c.discount3 = '" & disc3 &
               "' and c.purchaseorderid = p.id and p.supplierid = s.id " &
               "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            Dim orderedItems = context.purchaseorderitems.SqlQuery(qry).ToList

            Dim anotherQry As String = "select c.* from purchasereturnitems c, purchasereturns p, suppliers s " &
               "where p.posteddate is not null and c.stockId = '" & stockId & "' and c.price = '" & price &
               "' and c.discount1 = '" & disc1 & "' and c.discount2 = '" & disc2 & "' and c.discount3 = '" & disc3 &
               "' and c.purchasereturnid = p.id and p.supplierid = s.id " &
               "and ucase(s.name) = '" & name.ToUpper & "' and s.active = true"

            Dim returnedItems = context.purchasereturnitems.SqlQuery(anotherQry).ToList()

            maxQtyOrdered = 0
            For Each item In orderedItems
                maxQtyOrdered += item.Quantity
            Next

            maxQtyReturned = 0
            For Each item In returnedItems
                maxQtyReturned += item.Quantity
            Next

            maxQuantity = If(IsNothing(maxQtyReturned), maxQtyOrdered,
                maxQtyOrdered - maxQtyReturned)
        End Using

        If setFields Then
            enterGrid("Qty", rowIndex).Value = If(maxQuantity > 0, maxQuantity, 0)
        End If
    End Sub

    Private Sub enterGrid_RowValidating(sender As Object, e As DataGridViewCellCancelEventArgs) Handles enterGrid.RowValidating
        If Not String.IsNullOrEmpty(Controller.updateMode) Then
            If Not String.IsNullOrWhiteSpace(enterGrid("Stock", e.RowIndex).Value) Then
                If Not stockList.Contains(enterGrid("Stock", e.RowIndex).Value.ToString.Trim.ToUpper) Then
                    Util.notifyError("Invalid Stock Name.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Stock", e.RowIndex)
                    Exit Sub
                End If

                Dim price, disc1, disc2, disc3 As Double
                Dim qty As Integer

                Double.TryParse(enterGrid("Price", e.RowIndex).Value, price)
                Double.TryParse(enterGrid("Disc1", e.RowIndex).Value, disc1)
                Double.TryParse(enterGrid("Disc2", e.RowIndex).Value, disc2)
                Double.TryParse(enterGrid("Disc3", e.RowIndex).Value, disc3)
                Integer.TryParse(enterGrid("Qty", e.RowIndex).Value, qty)

                If IsNothing(price) OrElse price < 0 OrElse Not priceList.Contains(price) Then
                    Util.notifyError("Invalid Price value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Price", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(disc1) OrElse disc1 < 0 OrElse Not discount1List.Contains(disc1) Then
                    Util.notifyError("Invalid Discount 1 value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Disc1", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(disc2) OrElse disc2 < 0 OrElse Not discount2List.Contains(disc2) Then
                    Util.notifyError("Invalid Discount 2 value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Disc2", e.RowIndex)
                    Exit Sub
                End If

                If IsNothing(disc3) OrElse disc3 < 0 OrElse Not discount3List.Contains(disc3) Then
                    Util.notifyError("Invalid Discount 3 value.")
                    e.Cancel = True
                    enterGrid.CurrentCell = enterGrid("Disc3", e.RowIndex)
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
                Dim stockName As String = enterGrid("Stock", e.RowIndex).Value

                If Not IsNothing(stockName) Then
                    stockName = stockName.Trim.ToUpper

                    prevStockName = stockName
                    Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                        selectedStock = context.stocks _
                            .Where(Function(c) c.Name.Equals(stockName) _
                                AndAlso c.Active = True).FirstOrDefault

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

    Private Sub reloadSelectedStockVariables(ByVal rowIndex As Integer)
        reloadSelectedStockVariables(rowIndex, False)
    End Sub

    Private Sub reloadSelectedStockVariables(ByVal rowIndex As Integer, ByVal setFields As Boolean)
        loadAvailablePrices(selectedStock.Id, tbSupplier.Text, setFields, rowIndex)
        loadAvailableDiscounts1(selectedStock.Id, tbSupplier.Text,
            enterGrid("Price", rowIndex).Value, setFields, rowIndex)
        loadAvailableDiscounts2(selectedStock.Id, tbSupplier.Text,
            enterGrid("Price", rowIndex).Value, enterGrid("Disc1", rowIndex).Value, setFields, rowIndex)
        loadAvailableDiscounts3(selectedStock.Id, tbSupplier.Text,
            enterGrid("Price", rowIndex).Value, enterGrid("Disc1", rowIndex).Value,
             enterGrid("Disc2", rowIndex).Value, setFields, rowIndex)
        loadMaxTotalQuantity(selectedStock.Id, tbSupplier.Text,
            enterGrid("Price", rowIndex).Value,
            enterGrid("Disc1", rowIndex).Value, enterGrid("Disc2", rowIndex).Value,
            enterGrid("Disc3", rowIndex).Value, setFields, rowIndex)
    End Sub

    Public Sub printObject() Implements IControl.printObject
        Dim printDoc As New PrintTransaction

        If btnPrint.Visible And Not IsNothing(currentObject) Then
            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
                printDoc.name = currentObject.supplier.Name
                printDoc.address = currentObject.supplier.Address
                printDoc.docNo = currentObject.DocumentNo
                printDoc.docDate = currentObject.Date
                printDoc.agent = String.Empty

                printDoc.items = context.Database _
                    .SqlQuery(Of _Transaction)(
                        "select i.quantity, if(i.quantity > 1, u.pluralname, u.name) as unit, s.name as stock, " &
                        "s.description, i.price, i.discount1, i.discount2, i.discount3 " &
                        "from purchasereturnitems i, stocks s, units u " &
                        "where i.stockid = s.id and s.unitid = u.id " &
                        "and i.purchasereturnid = '" & currentObject.Id & "'") _
                    .ToList()
            End Using

            Util.previewDocument(printDoc)
        End If
    End Sub

    Dim previousFilter As String

    Private Sub tbSupplier_GotFocus(sender As Object, e As EventArgs) Handles tbSupplier.GotFocus
        previousFilter = tbSupplier.Text
    End Sub

    Private Sub tbSupplierValidated(sender As Object, e As EventArgs) Handles tbSupplier.Validated
        If Not String.IsNullOrEmpty(Controller.updateMode) _
            AndAlso Not previousFilter.ToUpper.Equals(tbSupplier.Text) Then
            Util.clearRows(enterGrid)

            If String.IsNullOrEmpty(tbSupplier.Text) Then
                enterGrid.ReadOnly = True
                stockList.Clear()
            Else
                reloadStocks(tbSupplier.Text)
                enterGrid.ReadOnly = False
                setReadOnlyColumns()
            End If
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printObject()
    End Sub

    Private Sub enterGrid_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles enterGrid.CellEnter
        If Not String.IsNullOrEmpty(Controller.updateMode) _
            AndAlso enterGrid.Focused AndAlso enterGrid.CurrentCell.ReadOnly Then
            SendKeys.Send("{TAB}")
        End If
    End Sub

    Private Sub setReadOnlyColumns()
        enterGrid.Columns.Item("Desc").ReadOnly = True
        enterGrid.Columns.Item("Unit").ReadOnly = True
        enterGrid.Columns.Item("Amount").ReadOnly = True
    End Sub


End Class