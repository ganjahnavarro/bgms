Imports System.IO

Public Class Util

#Region "Notification"
    Public Shared Sub notifyError(ByVal msg As String)
        notify(msg, Color.Crimson)
    End Sub

    Public Shared Sub notifyInfo(ByVal msg As String)
        notify(msg, Color.MediumSeaGreen)
    End Sub

    Public Shared Sub notifyWarning(ByVal msg As String)
        notify(msg, Color.Chocolate)
    End Sub

    Public Shared Sub notify(ByVal msg As String, ByVal color As Color)
        notifyDisplay(True, msg, color)
        Constants.NOTIFICATION_TICK_COUNT = 0
    End Sub

    Public Shared Sub notifyTick()
        Constants.NOTIFICATION_TICK_COUNT += 1
        If Constants.NOTIFICATION_TICK_COUNT > Constants.NOTIFICATION_MAX_TICK Then
            notifyDisplay(False)
        End If
    End Sub

    Public Shared Sub notifyDisplay(ByVal display As Boolean)
        notifyDisplay(display, "Notification", Color.Gray)
    End Sub

    Public Shared Sub notifyDisplay(ByVal display As Boolean, ByVal msg As String, ByVal color As Color)
        Controller.notificationTimer.Enabled = display

        Select Case Controller.currentForm
            Case "FAgent"
                FAgent.notificationPanel.Visible = display
                FAgent.notificationLabel.Text = msg
                FAgent.notificationPanel.BackColor = color
                Exit Select
            Case "FCategory"
                FCategory.notificationPanel.Visible = display
                FCategory.notificationLabel.Text = msg
                FCategory.notificationPanel.BackColor = color
                Exit Select
            Case "FCustomer"
                FCustomer.notificationPanel.Visible = display
                FCustomer.notificationLabel.Text = msg
                FCustomer.notificationPanel.BackColor = color
                Exit Select
            Case "FStock"
                FStock.notificationPanel.Visible = display
                FStock.notificationLabel.Text = msg
                FStock.notificationPanel.BackColor = color
                Exit Select
            Case "FSupplier"
                FSupplier.notificationPanel.Visible = display
                FSupplier.notificationLabel.Text = msg
                FSupplier.notificationPanel.BackColor = color
                Exit Select
            Case "FUnit"
                FUnit.notificationPanel.Visible = display
                FUnit.notificationLabel.Text = msg
                FUnit.notificationPanel.BackColor = color
                Exit Select
            Case "EtcUsers"
                EtcUsers.notificationPanel.Visible = display
                EtcUsers.notificationLabel.Text = msg
                EtcUsers.notificationPanel.BackColor = color
                Exit Select
            Case "EtcAddItem"
                EtcAddItem.notificationPanel.Visible = display
                EtcAddItem.notificationLabel.Text = msg
                EtcAddItem.notificationPanel.BackColor = color
                Exit Select
            Case "TransCC"
                TransCC.notificationPanel.Visible = display
                TransCC.notificationLabel.Text = msg
                TransCC.notificationPanel.BackColor = color
                Exit Select
            Case "TransPO"
                TransPO.notificationPanel.Visible = display
                TransPO.notificationLabel.Text = msg
                TransPO.notificationPanel.BackColor = color
                Exit Select
            Case "TransPR"
                TransPR.notificationPanel.Visible = display
                TransPR.notificationLabel.Text = msg
                TransPR.notificationPanel.BackColor = color
                Exit Select
            Case "TransSO"
                TransSO.notificationPanel.Visible = display
                TransSO.notificationLabel.Text = msg
                TransSO.notificationPanel.BackColor = color
                Exit Select
            Case "TransSP"
                TransSP.notificationPanel.Visible = display
                TransSP.notificationLabel.Text = msg
                TransSP.notificationPanel.BackColor = color
                Exit Select
            Case "TransSR"
                TransSR.notificationPanel.Visible = display
                TransSR.notificationLabel.Text = msg
                TransSR.notificationPanel.BackColor = color
                Exit Select
            Case "PostCC"
                PostCC.notificationPanel.Visible = display
                PostCC.notificationLabel.Text = msg
                PostCC.notificationPanel.BackColor = color
                Exit Select
            Case "PostPO"
                PostPO.notificationPanel.Visible = display
                PostPO.notificationLabel.Text = msg
                PostPO.notificationPanel.BackColor = color
                Exit Select
            Case "PostPR"
                PostPR.notificationPanel.Visible = display
                PostPR.notificationLabel.Text = msg
                PostPR.notificationPanel.BackColor = color
                Exit Select
            Case "PostSO"
                PostSO.notificationPanel.Visible = display
                PostSO.notificationLabel.Text = msg
                PostSO.notificationPanel.BackColor = color
                Exit Select
            Case "PostSP"
                PostSP.notificationPanel.Visible = display
                PostSP.notificationLabel.Text = msg
                PostSP.notificationPanel.BackColor = color
                Exit Select
            Case "PostSR"
                PostSR.notificationPanel.Visible = display
                PostSR.notificationLabel.Text = msg
                PostSR.notificationPanel.BackColor = color
                Exit Select
            Case "RptPurchaseMn"
                RptPurchaseMn.notificationPanel.Visible = display
                RptPurchaseMn.notificationLabel.Text = msg
                RptPurchaseMn.notificationPanel.BackColor = color
                Exit Select
            Case "RptSalesMn"
                RptSalesMn.notificationPanel.Visible = display
                RptSalesMn.notificationLabel.Text = msg
                RptSalesMn.notificationPanel.BackColor = color
                Exit Select
            Case "RptSOA"
                RptSOA.notificationPanel.Visible = display
                RptSOA.notificationLabel.Text = msg
                RptSOA.notificationPanel.BackColor = color
                Exit Select
            Case "RptStockMn"
                RptStockMn.notificationPanel.Visible = display
                RptStockMn.notificationLabel.Text = msg
                RptStockMn.notificationPanel.BackColor = color
                Exit Select
            Case "RptStockMv"
                RptStockMv.notificationPanel.Visible = display
                RptStockMv.notificationLabel.Text = msg
                RptStockMv.notificationPanel.BackColor = color
                Exit Select
            Case "RptSupplierPy"
                RptSupplierPy.notificationPanel.Visible = display
                RptSupplierPy.notificationLabel.Text = msg
                RptSupplierPy.notificationPanel.BackColor = color
                Exit Select
            Case "RptTransactions"
                RptTransactions.notificationPanel.Visible = display
                RptTransactions.notificationLabel.Text = msg
                RptTransactions.notificationPanel.BackColor = color
                Exit Select
            Case "EtcSettings"
                EtcSettings.notificationPanel.Visible = display
                EtcSettings.notificationLabel.Text = msg
                EtcSettings.notificationPanel.BackColor = color
                Exit Select
        End Select
    End Sub

#End Region

#Region "Formatting"

    Public Shared Function inSql(ByVal d As Date) As String
        Return "str_to_date('" & d & "', '%m/%d/%Y')"
    End Function


    Public Shared Sub clearRows(ByRef grid As DataGridView)
        For rowIndex = grid.RowCount - 2 To 0 Step -1
            grid.Rows.RemoveAt(rowIndex)
        Next
    End Sub

    Public Shared Function getFormattedDocumentNo(ByVal prefix As String, _
        ByVal count As Integer) As String
        'Dim tmpCount As String = count.ToString
        'For i = 0 To (Constants.MAX_DOCUMENT_NO_LENGTH - tmpCount.Length) Step 1
        '    tmpCount = "0" & tmpCount
        'Next
        'Return prefix & tmpCount

        Return prefix & count
    End Function

    Public Shared Function addBullet(ByVal value As String) As String
        If Not IsNothing(value) Then
            Return Constants.BULLET & value
        End If

        Return Nothing
    End Function

    Public Shared Function removeBullet(ByVal value As String) As String
        If Not IsNothing(value) Then
            Return value.Replace(Constants.BULLET, "")
        End If

        Return Nothing
    End Function

    Public Shared Function fmtModifyBy(ByVal value As String) As String
        If Not IsNothing(value) Then
            Return Constants.PREFIX_MODIFY_BY & value
        End If

        Return String.Empty
    End Function

    Public Shared Function fmtModifyDate(ByVal value As String) As String
        If Not IsNothing(value) Then
            Return Constants.PREFIX_MODIFY_DATE & value
        End If

        Return String.Empty
    End Function

    Public Shared Function capitalize(ByVal val As String) As String
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        Dim array() As Char = val.ToCharArray
        array(0) = Char.ToUpper(array(0))

        Return New String(array)
    End Function
#End Region

#Region "Confirmation"

    Private Shared confirmationShown As Boolean = False

    Public Shared Function askForConfirmation(ByVal msg As String, ByVal header As String) As Boolean
        If confirmationShown = False Then
            confirmationShown = True

            Dim result As Integer = MessageBox.Show(msg, header, MessageBoxButtons.OKCancel)

            If result = DialogResult.OK Then
                confirmationShown = False
                Return True
            Else
                confirmationShown = False
                Return False
            End If
        End If

        Return Nothing
    End Function
#End Region

#Region "Lookup"

    Public Shared Function getInitialDate() As Date
        Return New Date(2015, 1, 1)
    End Function

    Public Shared Function getValidDate() As Date
        Return New Date(1990, 1, 1)
    End Function

    Public Shared Function getStockAvailableQty(ByVal stockId As Integer) As Integer
        Using context As New bgmsEntities
            Dim onHand As Integer = context.stocks _
                .Where(Function(c) c.Id = stockId) _
                .Select(Function(c) c.QtyOnHand).FirstOrDefault

            Dim unpostedQty As Integer = context.purchaseorderitems _
                .Where(Function(c) c.stockId = stockId _
                    And c.purchaseorder.PostedDate.Equals(Nothing)) _
                .Select(Function(c) c.Quantity).DefaultIfEmpty(0).Sum()

            Return If(IsNothing(onHand), 0 - unpostedQty, onHand - unpostedQty)
        End Using
    End Function

    Public Shared Function getSupplierList() As List(Of supplier)
        Using context = New bgmsEntities
            Return context.suppliers _
                .Where(Function(c) c.Active = True) _
                .OrderBy(Function(c) c.Name) _
                .ToList
        End Using
    End Function

    Public Shared Function getSuppliersWithOrders() As List(Of supplier)
        Using context As New bgmsEntities
            Return context.purchaseorders _
                .OrderBy(Function(c) c.supplier.Name) _
                .Select(Function(c) c.supplier) _
                .Distinct().ToList()
        End Using
    End Function

    Public Shared Function getAgentList() As List(Of agent)
        Using context = New bgmsEntities
            Return context.agents.Where(Function(c) c.Active = True).ToList
        End Using
    End Function

    Public Shared Function getCategoryList() As List(Of category)
        Using context = New bgmsEntities
            Return context.categories _
                .Where(Function(c) c.Active = True) _
                .OrderBy(Function(c) c.Name) _
                .ToList()
        End Using
    End Function

    Public Shared Function getUnitList() As List(Of unit)
        Using context = New bgmsEntities
            Return context.units _
                .Where(Function(c) c.Active = True) _
                .OrderBy(Function(c) c.Name) _
                .ToList()
        End Using
    End Function

    Public Shared Function getCustomerList() As List(Of customer)
        Using context = New bgmsEntities
            Return context.customers _
                .Where(Function(c) c.Active = True) _
                .OrderBy(Function(c) c.Name) _
                .ToList()
        End Using
    End Function

    Public Shared Function getCustomersWithOrders() As List(Of customer)
        Using context As New bgmsEntities
            Return context.salesorders _
                .OrderBy(Function(c) c.customer.Name) _
                .Select(Function(c) c.customer) _
                .Distinct().ToList()
        End Using
    End Function

    Public Shared Function getInitialStockNames() As List(Of String)
        Using context = New bgmsEntities
            Return context.stocks _
                       .Where(Function(c) c.Active = True) _
                       .OrderBy(Function(c) c.Name) _
                       .Select(Function(c) c.Name) _
                       .Take(500).ToList()
        End Using
    End Function

    Public Shared Function getCategoryNames() As List(Of String)
        Using context = New bgmsEntities
            Return context.categories _
                       .Where(Function(c) c.Active = True) _
                       .OrderBy(Function(c) c.Name) _
                       .Select(Function(c) c.Name) _
                       .ToList()
        End Using
    End Function

    Public Shared Function getAgentNames() As List(Of String)
        Using context = New bgmsEntities
            Return context.agents _
                       .Where(Function(c) c.Active = True) _
                       .OrderBy(Function(c) c.Name) _
                       .Select(Function(c) c.Name) _
                       .ToList()
        End Using
    End Function

    Public Shared Function getCustomerNames() As List(Of String)
        Using context = New bgmsEntities
            Return context.customers _
                       .Where(Function(c) c.Active = True) _
                       .OrderBy(Function(c) c.Name) _
                       .Select(Function(c) c.Name) _
                       .ToList()
        End Using
    End Function

    Public Shared Function getSupplierNames() As List(Of String)
        Using context = New bgmsEntities
            Return context.suppliers _
                       .Where(Function(c) c.Active = True) _
                       .OrderBy(Function(c) c.Name) _
                       .Select(Function(c) c.Name) _
                       .ToList()
        End Using
    End Function

    Public Shared Function getUnitNames() As List(Of String)
        Using context = New bgmsEntities
            Return context.units _
                       .Where(Function(c) c.Active = True) _
                       .OrderBy(Function(c) c.Name) _
                       .Select(Function(c) c.Name) _
                       .ToList()
        End Using
    End Function

    Public Shared Function getUsernames() As List(Of String)
        Using context = New bgmsEntities
            Return context.users _
                       .Where(Function(c) c.Active = True) _
                       .OrderBy(Function(c) c.Username) _
                       .Select(Function(c) c.Username) _
                       .ToList()
        End Using
    End Function
#End Region

#Region "IO"
    Public Shared Sub logMessage(ByVal msg As String)
        My.Computer.FileSystem.WriteAllText( _
            getPathCreateIfNotExists(Constants.FILE_LOG), DateTime.Now & vbTab & msg & vbNewLine, True)
    End Sub

    Public Shared Function getApplicationPath() As String
        Dim fullPath As String = System.Reflection.Assembly.GetExecutingAssembly().Location
        Return My.Computer.FileSystem.GetParentPath(fullPath)
    End Function

    Public Shared Function getPathCreateIfNotExists(ByVal filename As String) As String
        Dim filePath As String = getApplicationPath() & "\" & filename

        If Not File.Exists(filePath) Then
            File.Create(filePath)
        End If

        Return filePath
    End Function
#End Region

#Region "Print"

    Public Shared Sub previewDocument(ByRef printDoc As Printing.PrintDocument)
        Dim preview As New CoolPrintPreviewDialog
        DirectCast(preview, Form).WindowState = FormWindowState.Maximized

        preview._preview.Zoom = 1
        preview.Document = printDoc
        preview.ShowDialog()
    End Sub

#End Region

End Class
