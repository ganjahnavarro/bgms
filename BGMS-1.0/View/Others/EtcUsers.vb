Public Class EtcUsers
    Implements IControl

    Dim currentObject As user

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        displayList(Util.getUsernames)
        initialize()
        selectFirstAndLoad()
    End Sub

    Private Sub selectFirstAndLoad()
        If Not IsNothing(box.Items) And box.Items.Count > 0 Then
            box.ClearSelected()
            box.SelectedIndex = 0
            selectedItemChanged()
        End If
    End Sub

    Private Sub selectedItemChanged()
        findAndLoad(Util.removeBullet(box.SelectedItem))
        enableInputs(False)
        showUpdateButtons(False)
        Controller.updateMode = Nothing
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
                Try
                    saveChangesOnMe()
                    resetMe()
                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try
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
        Me.loadObject()
        Me.enableInputs(False)
        showUpdateButtons(False)
        Controller.updateMode = Nothing
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
            currentObject = context.users.Where(Function(c) _
                c.Id.Equals(currentObject.Id)).FirstOrDefault

            If currentObject.Username.ToUpper = Constants.DEFAULT_USER.ToUpper Then
                Util.notifyError("Can't delete super user.")
            Else
                currentObject.Active = False

                Dim action As String = Controller.currentUser.Username & " deleted a user (" & _
                    currentObject.Username & ")"
                context.activities.Add(New activity(action))

                context.SaveChanges()
            End If
        End Using
    End Sub

    Public Sub deleteRow() Implements IControl.deleteRow
    End Sub

    Public Sub enableInputs(enable As Boolean) Implements IControl.enableInputs
        tbName.Enabled = enable
        tbPassword.Enabled = enable
        tbConfirm.Enabled = enable
        tbFirstname.Enabled = enable
        tbLastname.Enabled = enable
        tbLast.Enabled = enable
        isAdmin.Enabled = enable

        tbConfirm.Visible = enable
        lblConfirm.Visible = enable
        showPassword.Visible = If(Controller.currentUser.Admin, Not enable, False)

        If enable Then
            tbName.Focus()
        End If
    End Sub

    Public Sub loadObject() Implements IControl.loadObject
        If Not IsNothing(currentObject) Then
            lblBy.Text = Util.fmtModifyBy(currentObject.ModifyBy)
            lblOn.Text = Util.fmtModifyDate(currentObject.ModifyDate)

            tbName.Text = currentObject.Username
            tbPassword.Text = currentObject.Password
            tbConfirm.Text = currentObject.Password
            tbFirstname.Text = currentObject.Firstname
            tbLastname.Text = currentObject.Lastname
            tbLast.Text = currentObject.Contact
            isAdmin.Checked = currentObject.Admin

            showPassword.Enabled = True
        End If
    End Sub

    Public Sub reset() Implements IControl.reset
        lblBy.Text = ""
        lblOn.Text = ""
        tbSearch.Text = ""

        tbName.Text = ""
        tbPassword.Text = ""
        tbConfirm.Text = ""
        tbFirstname.Text = ""
        tbLastname.Text = ""
        tbLast.Text = ""

        showPassword.Checked = False
        tbPassword.PasswordChar = "*"
        showPassword.Visible = True
        lblConfirm.Visible = False
        tbConfirm.Visible = False

        showPassword.Enabled = Not IsNothing(currentObject)
    End Sub

    Public Sub saveObject() Implements IControl.saveObject
        Try
            Using context As New bgmsEntities
                currentObject = New user
                setObjectValues()
                context.users.Add(currentObject)

                Dim action As String = Controller.currentUser.Username & " added a user (" & _
                    currentObject.Username & ")"
                context.activities.Add(New activity(action))

                context.SaveChanges()
                Util.notifyInfo("Added 1 user.")
            End Using
        Catch ex As Entity.Validation.DbEntityValidationException
            For Each eve As Entity.Validation.DbEntityValidationResult In ex.EntityValidationErrors
                MsgBox(String.Format("Entity of type {0} in state {1} has the following validation errors:", _
                                   eve.Entry.Entity.GetType().Name, eve.Entry.State))
                For Each ve As Entity.Validation.DbValidationError In eve.ValidationErrors
                    MsgBox(ve.PropertyName & " - " & ve.ErrorMessage)
                Next
            Next
        End Try
        
    End Sub

    Public Sub search() Implements IControl.search
        tbSearch.Focus()
    End Sub

    Public Sub setObjectValues()
        currentObject.Username = tbName.Text
        currentObject.Password = tbPassword.Text
        currentObject.Firstname = tbFirstname.Text
        currentObject.Lastname = tbLastname.Text
        currentObject.ModifyBy = Controller.currentUser.Username
        currentObject.ModifyDate = DateTime.Today
        currentObject.Contact = tbLast.Text
        currentObject.Admin = isAdmin.Checked
        currentObject.Active = True
    End Sub

    Public Function showAllButtons() As Integer Implements IControl.getButtonsShown
        Return Constants.SHOW_ACTION
    End Function

    Public Sub updateObject() Implements IControl.updateObject
        Using context As New bgmsEntities
            currentObject = context.users.Where(Function(c) _
                c.Id.Equals(currentObject.Id)).FirstOrDefault
            setObjectValues()

            Dim action As String = Controller.currentUser.Username & " updated a user (" & _
                currentObject.Username & ")"
            context.activities.Add(New activity(action))

            context.SaveChanges()
            Util.notifyInfo("Updated 1 User.")
        End Using
    End Sub

    Public Function getErrorMessage() As String Implements IControl.getErrorMessage
        If String.IsNullOrWhiteSpace(tbName.Text) Then
            Return "Username is required."
        End If

        If String.IsNullOrWhiteSpace(tbPassword.Text) Then
            Return "Password is required."
        End If

        If String.IsNullOrWhiteSpace(tbConfirm.Text) Then
            Return "Password confirmation is required."
        End If

        Using context As New bgmsEntities
            'c.Active = True AndAlso
            Dim duplicate = context.users _
                .Where(Function(c) c.Username.ToUpper.Equals(tbName.Text.ToUpper)) _
                .FirstOrDefault

            If Not IsNothing(duplicate) Then
                Return "Username already exists."
            End If
        End Using

        If Not tbPassword.Text.Equals(tbConfirm.Text) Then
            Return "Passwords didn't match."
        End If

        Return Nothing
    End Function

    Private Sub displayList(ByVal list As List(Of String))
        box.Items.Clear()
        For Each c In list
            box.Items.Add(c)
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
            currentObject = context.users.Where(Function(c) c.Username.Equals(name) And c.Active = True).FirstOrDefault
        End Using
    End Sub

    Private Sub tbSearch_KeyUp(sender As Object, e As KeyEventArgs) Handles tbSearch.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
        End If

        If tbSearch.Text = "" Then
            displayList(Util.getUsernames)
        Else
            Using context = New bgmsEntities
                displayList(context.users _
                    .Where(Function(c) _
                        c.Username.ToLower.Contains(tbSearch.Text.ToLower) And _
                        c.Active = True) _
                    .OrderBy(Function(c) c.Username) _
                    .Select(Function(c) c.Username) _
                    .ToList)
                selectFirstAndLoad()
            End Using
        End If
    End Sub

    Private Sub tbLast_KeyUp(sender As Object, e As KeyEventArgs) _
        Handles tbLast.KeyUp
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            saveObject()
        End If
    End Sub

    Private Sub showPassword_CheckedChanged(sender As Object, e As EventArgs) Handles showPassword.CheckedChanged
        tbPassword.PasswordChar = If(showPassword.Checked, Nothing, "*")
    End Sub

    Private Sub boxChanged(sender As Object, e As EventArgs) Handles box.SelectedIndexChanged
        selectedItemChanged()
    End Sub

    Public Sub resetListSelection() Implements IControl.resetListSelection
        tbSearch.Clear()
        currentObject = Nothing
        displayList(Util.getUsernames)
    End Sub

    Public Sub nextObject() Implements IControl.nextObject
    End Sub

    Public Sub previousObject() Implements IControl.previousObject
    End Sub

    Public Sub firstObject() Implements IControl.firstObject
    End Sub

    Public Sub lastObject() Implements IControl.lastObject
    End Sub

    Public Function getAllowEditDelete() As Boolean Implements IControl.getAllowEditDelete
        Return True
    End Function

    Public Sub printObject() Implements IControl.printObject
    End Sub

End Class