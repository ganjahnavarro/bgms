Public Class EtcLogin

    Private Shared users As List(Of user)

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            loadUsers()
        Catch ex As Exception
            btnCancel_Click(sender, e)
        End Try
        'loadPreviousUsername()
    End Sub

    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                If tbPassword.Focused Then
                    validateUsernameAndPassword()
                    Return Nothing
                End If
                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub loadUsers()
        Using context As New bgmsEntities
            users = context.users.Where(Function(c) c.Active = True).ToList()
        End Using
    End Sub

    'Private Sub loadPreviousUsername()
    '    tbUsername.Text = If(Controller.config.ContainsKey("PREVIOUS_USERNAME"), _
    '        Controller.config.Item("PREVIOUS_USERNAME"), String.Empty)
    '    tbPassword.Select()
    'End Sub

    Private Sub validateUsernameAndPassword() Handles btnLogin.Click
        If Not String.IsNullOrWhiteSpace(tbUsername.Text) _
            And Not String.IsNullOrWhiteSpace(tbPassword.Text) Then
            lblNoti.Visible = False
            For Each user In users
                If user.Username = tbUsername.Text _
                    And user.Password = tbPassword.Text Then

                    'saveUsername(user)
                    Controller.currentUser = user
                    Controller.mainPanel.Visible = True
                    tbPassword.Clear()
                    Me.Close()
                    Exit Sub
                End If
            Next
        End If
        lblNoti.Visible = True
    End Sub

    'Private Sub saveUsername(ByVal user As user)
    '    Controller.config.Item("PREVIOUS_USERNAME") = user.Username
    'End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Controller.Close()
        Me.Close()
    End Sub

    Private Sub EtcLogin_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        tbUsername.Focus()
    End Sub
End Class