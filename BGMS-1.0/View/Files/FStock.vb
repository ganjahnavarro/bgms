Public Class FStock
    Implements IControl

    Dim prevQtyOnHand As Integer
    Dim currentObject As stock

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        displayList(Util.getInitialStockNames)
        reloadAutocompletes()
        initialize()
        selectFirstAndLoad()
    End Sub

    Private Sub reloadAutocompletes()
        Controller.initCategories()
        tbCategory.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbCategory.AutoCompleteCustomSource = Controller.categoryList
        tbCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend

        Controller.initUnits()
        tbUnit.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbUnit.AutoCompleteCustomSource = Controller.unitList
        tbUnit.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    Private Sub tbSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles tbSearch.KeyDown
        If e.KeyCode = Keys.Down Then
            box.Focus()
            SendKeys.Send("{DOWN}")
        End If
    End Sub

#Region "Standard"
    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                If tbLast.Focused Then
                    checkClick()
                Else
                    Return MyBase.ProcessDialogKey(Keys.Tab)
                End If
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub initialize()
        Me.reset()
        Me.enableInputs(False)
        Me.showUpdateButtons(False)
        Controller.updateMode = Nothing
    End Sub

    Public Sub addClick() Handles btnAdd.Click
        If Not IsNothing(Me) And btnAdd.Visible Then
            Me.reset()
            Me.enableInputs(True)
            Controller.updateMode = Constants.UPDATE_MODE_CREATE
            showUpdateButtons(True)
        End If
    End Sub

    Public Sub editClick() Handles btnEdit.Click
        If Not IsNothing(Me) And btnEdit.Visible Then
            Me.enableInputs(True)
            Controller.updateMode = Constants.UPDATE_MODE_EDIT
            Me.loadObject()
            showUpdateButtons(True)
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
            resetMe()
        End If
    End Sub

    Public Sub checkClick() Handles btnCheck.Click
        If Not IsNothing(Me) And btnCheck.Visible Then
            If Not IsNothing(Me.getErrorMessage) Then
                Util.notifyError(Me.getErrorMessage)
            Else
                'Try
                saveChangesOnMe()
                resetMe()
                'Catch ex As Exception
                'MsgBox(ex.Message)
                'End Try
            End If
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
        btnEdit.Visible = Not show
        btnDelete.Visible = Not show
    End Sub

#End Region

    Public Sub deleteObject() Implements IControl.deleteObject
        Using context As New bgmsEntities
            currentObject = context.stocks.Where(Function(c) _
                c.Id.Equals(currentObject.Id)).FirstOrDefault
            currentObject.Active = False

            Dim action As String = Controller.currentUser.Username & " deleted a stock (" & _
                currentObject.Name & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
        End Using
    End Sub

    Public Sub deleteRow() Implements IControl.deleteRow
    End Sub

    Public Function getAllowEditDelete() As Boolean Implements IControl.getAllowEditDelete
        Return True
    End Function

    Public Sub enableInputs(enable As Boolean) Implements IControl.enableInputs
        tbName.Enabled = enable
        tbCost.Enabled = enable
        tbOnHand.Enabled = enable
        tbLast.Enabled = enable
        tbDescription.Enabled = enable
        tbUnit.Enabled = enable
        tbCategory.Enabled = enable

        If enable Then
            tbName.Focus()
        End If
    End Sub

    Public Sub loadObject() Implements IControl.loadObject
        If Not IsNothing(currentObject) Then
            Using context As New bgmsEntities
                currentObject = context.stocks _
                    .Include("Unit").Include("Category") _
                    .Where(Function(c) c.Id = currentObject.Id) _
                    .FirstOrDefault()
            End Using
            loadCurrentObject()
        Else
            Using context As New bgmsEntities
                currentObject = context.stocks _
                    .Include("Unit").Include("Category") _
                    .Where(Function(c) c.Active = True) _
                    .OrderBy(Function(c) c.Name) _
                    .FirstOrDefault()
            End Using

            If Not IsNothing(currentObject) Then
                loadCurrentObject()
            Else
                reset()
            End If
        End If
    End Sub

    Private Sub loadCurrentObject()
        tbAvailable.Text = Util.getStockAvailableQty(currentObject.Id)

        lblBy.Text = Util.fmtModifyBy(currentObject.ModifyBy)
        lblOn.Text = Util.fmtModifyDate(currentObject.ModifyDate)
        tbName.Text = currentObject.Name
        tbDescription.Text = currentObject.Description

        tbCost.Text = If(IsNothing(currentObject.Cost), String.Empty, _
            FormatNumber(CDbl(currentObject.Cost), 2))

        tbLast.Text = If(IsNothing(currentObject.Price), String.Empty, _
            FormatNumber(CDbl(currentObject.Price), 2))

        tbOnHand.Text = If(IsNothing(currentObject.QtyOnHand), String.Empty, currentObject.QtyOnHand)
        tbCategory.Text = If(IsNothing(currentObject.category), String.Empty, currentObject.category.Name)
        tbUnit.Text = If(IsNothing(currentObject.unit), String.Empty, currentObject.unit.Name)

        prevQtyOnHand = currentObject.QtyOnHand
    End Sub

    Public Sub nextObject() Implements IControl.nextObject
    End Sub

    Public Sub previousObject() Implements IControl.previousObject
    End Sub

    Public Sub reset() Implements IControl.reset
        lblBy.Text = String.Empty
        lblOn.Text = String.Empty
        tbSearch.Text = String.Empty

        tbName.Text = String.Empty
        tbOnHand.Text = String.Empty
        tbDescription.Text = String.Empty
        tbCost.Text = String.Empty
        tbLast.Text = String.Empty
    End Sub

    Public Sub saveObject() Implements IControl.saveObject
        Using context As New bgmsEntities
            currentObject = New stock
            setObjectValues(context)
            context.stocks.Add(currentObject)

            Dim action As String = Controller.currentUser.Username & " added a stock (" & _
                currentObject.Name & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
        End Using

        Util.notifyInfo("Added 1 Stock")
        Controller.stockList.Add(currentObject.Name.ToUpper)
    End Sub

    Public Sub search() Implements IControl.search
        tbSearch.Focus()
    End Sub

    Public Sub setObjectValues(ByRef context As bgmsEntities)
        currentObject.Name = tbName.Text
        currentObject.Description = tbDescription.Text
        currentObject.Cost = If(String.IsNullOrWhiteSpace(tbCost.Text), Nothing, tbCost.Text)
        currentObject.Price = If(String.IsNullOrWhiteSpace(tbLast.Text), Nothing, tbLast.Text)
        currentObject.QtyOnHand = If(String.IsNullOrWhiteSpace(tbOnHand.Text), Nothing, tbOnHand.Text)
        currentObject.ModifyDate = DateTime.Today
        currentObject.ModifyBy = Controller.currentUser.Username
        currentObject.Active = True

        If String.IsNullOrWhiteSpace(tbCategory.Text) Then
            currentObject.CategoryId = Nothing
        Else
            currentObject.CategoryId = context.categories _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbCategory.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If

        If String.IsNullOrWhiteSpace(tbUnit.Text) Then
            currentObject.UnitId = Nothing
        Else
            currentObject.UnitId = context.units _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbUnit.Text.ToUpper) And c.Active = True) _
                    .Select(Function(c) c.Id).FirstOrDefault
        End If
    End Sub

    Public Function showAllButtons() As Integer Implements IControl.getButtonsShown
        Return Constants.SHOW_ACTION
    End Function

    Public Sub updateObject() Implements IControl.updateObject
        Using context As New bgmsEntities
            currentObject = context.stocks.Where(Function(c) _
                c.Id.Equals(currentObject.Id)).FirstOrDefault
            setObjectValues(context)

            Dim action As String = Controller.currentUser.Username & " updated a stock (" & _
                currentObject.Name & ")"
            context.activities.Add(New activity(action))

            If (currentObject.QtyOnHand - prevQtyOnHand <> 0) Then
                Dim adj As New adjustment
                adj.ModifyDate = DateTime.Today
                adj.ModifyBy = Controller.currentUser.Username
                adj.Quantity = currentObject.QtyOnHand - prevQtyOnHand
                adj.stockId = currentObject.Id
                context.adjustments.Add(adj)
            End If

            context.SaveChanges()
            Util.notifyInfo("Updated 1 Stock")
        End Using
    End Sub

    Public Function getErrorMessage() As String Implements IControl.getErrorMessage
        If String.IsNullOrWhiteSpace(tbName.Text) Then
            Return "Stock name is required."
        End If

        If String.IsNullOrWhiteSpace(tbLast.Text) Then
            Return "Price is required."
        End If

        If Not String.IsNullOrWhiteSpace(tbCategory.Text) _
            AndAlso Not Controller.categoryList.Contains(tbCategory.Text.ToUpper) Then
            Return "Invalid Category name."
        End If

        If Not String.IsNullOrWhiteSpace(tbUnit.Text) _
            AndAlso Not Controller.unitList.Contains(tbUnit.Text.ToUpper) Then
            Return "Invalid Unit name."
        End If

        If Controller.updateMode.Equals(Constants.UPDATE_MODE_CREATE) _
            OrElse Not currentObject.Name.ToUpper.Equals(tbName.Text.ToUpper) Then
            Using context As New bgmsEntities
                'c.Active = True AndAlso 
                Dim duplicate = context.stocks _
                    .Where(Function(c) c.Name.ToUpper.Equals(tbName.Text.ToUpper)) _
                    .FirstOrDefault

                If Not IsNothing(duplicate) Then
                    Return "Stock name already exists."
                End If
            End Using
        End If

        Dim price, cost As Double
        Dim qty As Integer

        Double.TryParse(tbLast.Text, price)
        Double.TryParse(tbCost.Text, cost)
        Integer.TryParse(tbOnHand.Text, qty)

        If IsNothing(price) OrElse price < 0 Then
            Return "Invalid Price value."
        End If

        If IsNothing(cost) OrElse cost < 0 Then
            Return "Invalid Cost value."
        End If

        If IsNothing(qty) Then
            Return "Invalid Quantity on Hand value."
        End If

        Return Nothing
    End Function

    Private Sub displayList(ByVal list As List(Of String))
        box.Items.Clear()
        For Each cName In list
            box.Items.Add(Util.addBullet(cName))
        Next
    End Sub

    Private Sub findAndLoad(ByVal name As String)
        If Not IsNothing(name) Then
            findObjectByName(name)
            loadObject()
        End If
    End Sub

    Private Sub findObjectByName(ByVal name As String)
        Using context = New bgmsEntities
            currentObject = (From stock In context.stocks.Include("Unit").Include("Category") _
                Select stock).Where(Function(c) c.Name.Equals(name) And c.Active = True).FirstOrDefault()
        End Using
    End Sub

    Private Sub boxChanged(sender As Object, e As EventArgs) Handles box.SelectedIndexChanged
        If Not IsNothing(box.SelectedItem) Then
            selectedItemChanged()
        End If
    End Sub

    Private Sub selectedItemChanged()
        findAndLoad(Util.removeBullet(box.SelectedItem))
        enableInputs(False)
        showUpdateButtons(False)
        Controller.updateMode = Nothing
    End Sub

    Public Sub resetListSelection() Implements IControl.resetListSelection
        tbSearch.Clear()
        currentObject = Nothing
        displayList(Util.getInitialStockNames)
    End Sub

    Private Sub tbSearch_TextChanged(sender As Object, e As EventArgs) Handles tbSearch.TextChanged
        If tbSearch.Text = String.Empty Then
            displayList(Util.getInitialStockNames)
        Else
            Using context = New bgmsEntities
                displayList(context.stocks _
                    .Where(Function(c) _
                        (c.Name.ToUpper.StartsWith(tbSearch.Text.ToUpper) _
                         Or c.Description.Contains(tbSearch.Text.ToUpper)) _
                        And c.Active = True) _
                    .OrderBy(Function(c) c.Name) _
                    .Select(Function(c) c.Name) _
                    .Take(500) _
                    .ToList)
            End Using
            selectFirstAndLoad()
        End If
    End Sub

    Private Sub selectFirstAndLoad()
        If Not IsNothing(box.Items) And box.Items.Count > 0 Then
            box.ClearSelected()
            box.SelectedIndex = 0
            selectedItemChanged()
        End If
    End Sub

    Public Sub firstObject() Implements IControl.firstObject
    End Sub

    Public Sub lastObject() Implements IControl.lastObject
    End Sub

    Public Sub printObject() Implements IControl.printObject
    End Sub

End Class