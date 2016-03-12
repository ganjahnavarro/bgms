Imports System.IO

Public Class Controller

    Public Shared updateMode As String
    Public Shared currentUser As user
    Public Shared config As New Dictionary(Of String, String)

    Public Shared stockList As New AutoCompleteStringCollection
    Public Shared stockListDesc As New AutoCompleteStringCollection
    Public Shared agentList As New AutoCompleteStringCollection
    Public Shared categoryList As New AutoCompleteStringCollection
    Public Shared customerList As New AutoCompleteStringCollection
    Public Shared supplierList As New AutoCompleteStringCollection
    Public Shared unitList As New AutoCompleteStringCollection

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        'loadConfig()
        Try
            init()
            Using context As New bgmsEntities
                currentUser = context.users.FirstOrDefault
            End Using

            hotkeyListener.Enabled = True
            pollTimer.Enabled = True
        Catch ex As Exception
            MsgBox("Can't connect to database. Check network connection. Server (192.168.1.125): " + ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub loadConfig()
        'config.Clear()

        'Dim tmp() As String
        'Dim settings() As String = File.ReadAllLines(Util.getPathCreateIfNotExists(Constants.FILE_CONFIG))

        'For Each setting As String In settings
        '    tmp = setting.Split(New Char() {CChar("=")})
        '    config.Add(tmp(0), tmp(1))
        'Next
    End Sub

    Private Sub formShown(sender As Object, e As EventArgs) Handles Me.Shown
        showLogin()
    End Sub

    Private Sub notificationTimer_Tick(sender As Object, e As EventArgs) Handles notificationTimer.Tick
        Util.notifyTick()
    End Sub

    Private Sub pollTimer_Tick(sender As Object, e As EventArgs) Handles pollTimer.Tick
        initRecentActivities()
    End Sub

#Region "Init"
    Private panels As New List(Of Panel)

    Private Sub init()
        updateMode = Nothing
        initRecentActivities()
        initPanels()
        initStocks()
    End Sub

    Private Sub initPanels()
        mainPanel.Dock = DockStyle.Fill
        panels.Clear()
        panels.Add(filesPanel)
        panels.Add(transactionPanel)
        panels.Add(postPanel)
        panels.Add(reportsPanel)
        panels.Add(activitiesPanel)
        panels.Add(settingsPanel)
        panels.Add(usersPanel)
        panels.Add(logoutPanel)
        panels.Add(aboutPanel)
    End Sub

    Private Sub initRecentActivities()
        activityList.Items.Clear()
        Using context As New bgmsEntities
            activityList.Items.AddRange(context.activities _
                .OrderByDescending(Function(c) c.Id) _
                .Select(Function(c) c.Description).Take(14).ToArray())
        End Using
    End Sub

    Public Sub initStocks()
        stockList.Clear()
        Using context As New bgmsEntities
            stockList.AddRange(context.stocks.Where(Function(c) c.Active = True) _
                .Select(Function(c) c.Name.ToUpper).ToArray())
        End Using
    End Sub

    Public Sub initStocksDesc()
        stockListDesc.Clear()
        Using context As New bgmsEntities
            stockListDesc.AddRange(context.stocks.Where(Function(c) c.Active = True) _
                .Select(Function(c) c.Name.ToUpper + " : " + c.Description).ToArray())
        End Using
    End Sub

    Public Sub initAgents()
        agentList.Clear()
        Using context As New bgmsEntities
            agentList.AddRange(context.agents.Where(Function(c) c.Active = True) _
                .Select(Function(c) c.Name.ToUpper).ToArray())
        End Using
    End Sub

    Public Sub initCustomers()
        customerList.Clear()
        Using context As New bgmsEntities
            customerList.AddRange(context.customers.Where(Function(c) c.Active = True) _
                .Select(Function(c) c.Name.ToUpper).ToArray())
        End Using
    End Sub

    Public Sub initCategories()
        categoryList.Clear()
        Using context As New bgmsEntities
            categoryList.AddRange(context.categories.Where(Function(c) c.Active = True) _
                .Select(Function(c) c.Name.ToUpper).ToArray())
        End Using
    End Sub

    Public Sub initSuppliers()
        supplierList.Clear()
        Using context As New bgmsEntities
            supplierList.AddRange(context.suppliers.Where(Function(c) c.Active = True) _
                .Select(Function(c) c.Name.ToUpper).ToArray())
        End Using
    End Sub

    Public Sub initUnits()
        unitList.Clear()
        Using context As New bgmsEntities
            unitList.AddRange(context.units.Where(Function(c) c.Active = True) _
                .Select(Function(c) c.Name.ToUpper).ToArray())
        End Using
    End Sub
#End Region

#Region "Styles"


    Private Sub panelHoverIn(ByRef sender As Panel)
        For Each p In panels
            panelHoverOut(p)
        Next

        sender.BackColor = Color.LightGray
    End Sub

    Private Sub panelHoverOut(ByRef sender As Panel)
        sender.BackColor = Color.Gainsboro
    End Sub

    Private Sub settingsPanel_MouseEnter(sender As Object, e As EventArgs) Handles settingsPanel.MouseEnter, lblSettings.MouseEnter, imgSettings.MouseEnter
        panelHoverIn(settingsPanel)
    End Sub

    Private Sub settingsPanel_MouseLeave(sender As Object, e As EventArgs) Handles settingsPanel.MouseLeave
        panelHoverOut(settingsPanel)
    End Sub

    Private Sub usersPanel_MouseEnter(sender As Object, e As EventArgs) Handles usersPanel.MouseEnter, lblUsers.MouseEnter, imgUsers.MouseEnter
        panelHoverIn(usersPanel)
    End Sub

    Private Sub usersPanel_MouseLeave(sender As Object, e As EventArgs) Handles usersPanel.MouseLeave
        panelHoverOut(usersPanel)
    End Sub

    Private Sub aboutPanel_MouseEnter(sender As Object, e As EventArgs) Handles aboutPanel.MouseEnter, lblAbout.MouseEnter, imgAbout.MouseEnter
        panelHoverIn(aboutPanel)
    End Sub

    Private Sub aboutPanel_MouseLeave(sender As Object, e As EventArgs) Handles aboutPanel.MouseLeave
        panelHoverOut(aboutPanel)
    End Sub

    Private Sub logoutPanel_MouseEnter(sender As Object, e As EventArgs) Handles logoutPanel.MouseEnter, lblLogout.MouseEnter, imgLogout.MouseEnter
        panelHoverIn(logoutPanel)
    End Sub

    Private Sub logoutPanel_MouseLeave(sender As Object, e As EventArgs) Handles logoutPanel.MouseLeave
        panelHoverOut(logoutPanel)
    End Sub

    Private Sub filesHoverIn(sender As Object, e As EventArgs) Handles filesPanel.MouseEnter, lblFAgent.MouseEnter, lblFCtgr.MouseEnter, lblFCust.MouseEnter, lblFStock.MouseEnter, lblFSupp.MouseEnter, lblFUnit.MouseEnter, imgFiles.MouseEnter
        panelHoverIn(filesPanel)
    End Sub

    Private Sub filesHoverOut(sender As Object, e As EventArgs) Handles filesPanel.MouseLeave
        panelHoverOut(filesPanel)
    End Sub

    Private Sub transHoverIn(sender As Object, e As EventArgs) Handles transactionPanel.MouseEnter, lblTCC.MouseEnter, lblTPO.MouseEnter, lblTPR.MouseEnter, lblTSO.MouseEnter, lblTSP.MouseEnter, lblTSR.MouseEnter, imgTrans.MouseEnter
        panelHoverIn(transactionPanel)
    End Sub

    Private Sub transHoverOut(sender As Object, e As EventArgs) Handles transactionPanel.MouseLeave
        panelHoverOut(transactionPanel)
    End Sub

    Private Sub postHoverIn(sender As Object, e As EventArgs) Handles postPanel.MouseEnter, lblPCC.MouseEnter, lblPPO.MouseEnter, lblPPR.MouseEnter, lblPSO.MouseEnter, lblPSP.MouseEnter, lblPSR.MouseEnter, imgPost.MouseEnter
        panelHoverIn(postPanel)
    End Sub

    Private Sub postHoverOut(sender As Object, e As EventArgs) Handles postPanel.MouseLeave
        panelHoverOut(postPanel)
    End Sub

    Private Sub reportsHoverIn(sender As Object, e As EventArgs) Handles reportsPanel.MouseEnter, lblRPMN.MouseEnter, lblRS.MouseEnter, lblRSMN.MouseEnter, lblRSMV.MouseEnter, lblRSOA.MouseEnter, lblRSP.MouseEnter, lblRTL.MouseEnter, imgReport.MouseEnter
        panelHoverIn(reportsPanel)
    End Sub

    Private Sub reportsHoverOut(sender As Object, e As EventArgs) Handles reportsPanel.MouseLeave
        panelHoverOut(reportsPanel)
    End Sub

#End Region

#Region "Navigation"
    Private Sub showLogin()
        EtcLogin.ShowDialog()
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles lblTPO.Click
        currentForm = TransPO.Name
        TransPO.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles lblTPR.Click
        currentForm = TransPR.Name
        TransPR.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label24_Click(sender As Object, e As EventArgs) Handles lblFStock.Click
        currentForm = FStock.Name
        FStock.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label23_Click(sender As Object, e As EventArgs) Handles lblFSupp.Click
        currentForm = FSupplier.Name
        FSupplier.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label25_Click(sender As Object, e As EventArgs) Handles lblFCtgr.Click
        currentForm = FCategory.Name
        FCategory.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label21_Click(sender As Object, e As EventArgs) Handles lblFCust.Click
        currentForm = FCustomer.Name
        FCustomer.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label20_Click(sender As Object, e As EventArgs) Handles lblFAgent.Click
        currentForm = FAgent.Name
        FAgent.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label22_Click(sender As Object, e As EventArgs) Handles lblFUnit.Click
        currentForm = FUnit.Name
        FUnit.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblTSP.Click
        currentForm = TransSP.Name
        TransSP.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles lblTSO.Click
        currentForm = TransSO.Name
        TransSO.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles lblTSR.Click
        currentForm = TransSR.Name
        TransSR.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles lblTCC.Click
        currentForm = TransCC.Name
        TransCC.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles lblPPO.Click
        currentForm = PostPO.Name
        PostPO.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles lblPPR.Click
        currentForm = PostPR.Name
        PostPR.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles lblPSP.Click
        currentForm = PostSP.Name
        PostSP.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles lblPSO.Click
        currentForm = PostSO.Name
        PostSO.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles lblPSR.Click
        currentForm = PostSR.Name
        PostSR.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles lblPCC.Click
        currentForm = PostCC.Name
        PostCC.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles lblRTL.Click
        currentForm = RptTransactions.Name
        RptTransactions.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label15_Click(sender As Object, e As EventArgs) Handles lblRPMN.Click
        currentForm = RptPurchaseMn.Name
        RptPurchaseMn.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles lblRSMN.Click
        currentForm = RptStockMn.Name
        RptStockMn.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label16_Click(sender As Object, e As EventArgs) Handles lblRSMV.Click
        currentForm = RptStockMv.Name
        RptStockMv.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs) Handles lblRSP.Click
        currentForm = RptSupplierPy.Name
        RptSupplierPy.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs) Handles lblRSOA.Click
        currentForm = RptSOA.Name
        RptSOA.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub Label17_Click(sender As Object, e As EventArgs) Handles lblRS.Click
        currentForm = RptSalesMn.Name
        RptSalesMn.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub settingsClick(sender As Object, e As EventArgs) Handles imgSettings.Click, lblSettings.Click, settingsPanel.Click
        currentForm = EtcSettings.Name
        EtcSettings.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles imgUsers.Click, lblUsers.Click, usersPanel.Click
        currentForm = EtcUsers.Name
        EtcUsers.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles imgAbout.Click, lblAbout.Click, aboutPanel.Click
        currentForm = EtcAbout.Name
        EtcAbout.ShowDialog()
        Util.notifyDisplay(False)
    End Sub

    Private Sub minimizeButton_Click(sender As Object, e As EventArgs) Handles minimizeButton.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub closeButton_Click(sender As Object, e As EventArgs) Handles closeButton.Click
        Me.Close()
    End Sub

    Private Sub imgLogoutClick(sender As Object, e As EventArgs) Handles imgLogout.Click, lblLogout.Click, logoutPanel.Click
        showLogin()
    End Sub

#End Region

#Region "Form Drag"
    Dim isDragging As Boolean
    Dim dragX As Integer
    Dim dragY As Integer

    Private Sub HeaderLabel_MouseDown(sender As Object, e As MouseEventArgs)
        TopPanel_MouseDown(sender, e)
    End Sub

    Private Sub HeaderLabel_MouseMove(sender As Object, e As MouseEventArgs)
        TopPanel_MouseMove(sender, e)
    End Sub

    Private Sub HeaderLabel_MouseUp(sender As Object, e As MouseEventArgs)
        TopPanel_MouseUp(sender, e)
    End Sub

    Private Sub TopPanel_MouseDown(sender As Object, e As MouseEventArgs) Handles topPanel.MouseDown
        isDragging = True
        dragX = Windows.Forms.Cursor.Position.X - Me.Left
        dragY = Windows.Forms.Cursor.Position.Y - Me.Top
    End Sub

    Private Sub TopPanel_MouseMove(sender As Object, e As MouseEventArgs) Handles topPanel.MouseMove
        If isDragging Then
            Me.Top = Windows.Forms.Cursor.Position.Y - dragY
            Me.Left = Windows.Forms.Cursor.Position.X - dragX
        End If
    End Sub

    Private Sub TopPanel_MouseUp(sender As Object, e As MouseEventArgs) Handles topPanel.MouseUp
        isDragging = False
    End Sub
#End Region

#Region "Global Hotkey"
    Public Shared currentForm As String
    Public Shared previousForm As String

    Private Declare Function GetKeyPress Lib "user32" _
        Alias "GetAsyncKeyState" (ByVal key As Integer) As Integer

    Public addPressed As Boolean = False
    Public closePressed As Boolean = False
    Public deletePressed As Boolean = False
    Public nextPressed As Boolean = False
    Public prevPressed As Boolean = False
    Public firstPressed As Boolean = False
    Public lastPressed As Boolean = False
    Public searchPressed As Boolean = False
    Public savePressed As Boolean = False
    Public printPressed As Boolean = False
    Public validatePressed As Boolean = False

    Private Sub hotkeyTick() Handles hotkeyListener.Tick
        If (String.IsNullOrEmpty(currentForm) Or currentForm <> EtcHelp.Name) And GetKeyPress(Keys.F1) Then
            previousForm = currentForm
            currentForm = EtcHelp.Name
            EtcHelp.ShowDialog()
        End If

        If Not String.IsNullOrEmpty(currentForm) Then
            If closePressed = False AndAlso GetKeyPress(Keys.F11) Then
                closePressed = True
                If IsNothing(updateMode) Or currentForm.Equals(EtcHelp.Name) _
                    Or currentForm.Equals(EtcAddItem.Name) Or currentForm.Equals(SelectSP.Name) _
                    Or currentForm.Equals(SelectCC.Name) Then
                    handleClose()
                Else
                    handleCancel()
                End If
            End If

            If GetKeyPress(Keys.F11) = False Then
                closePressed = False
            End If

            'If IsNothing(Controller.updateMode) AndAlso GetKeyPress(Keys.Menu) _
            '    And GetKeyPress(Keys.A) Then
            '    handleAdd()
            'End If

            If addPressed = False AndAlso GetKeyPress(Keys.Menu) _
                AndAlso GetKeyPress(Keys.A) Then
                addPressed = True
                If IsNothing(updateMode) Then
                    handleAdd()
                Else
                    handleAddItem()
                End If
            End If

            If GetKeyPress(Keys.A) = False Then
                addPressed = False
            End If

            If IsNothing(Controller.updateMode) AndAlso GetKeyPress(Keys.Menu) _
                AndAlso GetKeyPress(Keys.E) Then
                handleEdit()
            End If

            If deletePressed = False AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.D) Then
                deletePressed = True

                If IsNothing(Controller.updateMode) Then
                    handleDelete()
                Else
                    handleDeleteRow()
                End If
            End If

            If GetKeyPress(Keys.D) = False Then
                deletePressed = False
            End If

            If searchPressed = False AndAlso IsNothing(Controller.updateMode) _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.F) Then
                searchPressed = True
                handleSearch()
            End If

            If GetKeyPress(Keys.F) = False Then
                searchPressed = False
            End If

            If printPressed = False AndAlso IsNothing(Controller.updateMode) _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.P) Then
                printPressed = True
                handlePrint()
            End If

            If GetKeyPress(Keys.P) = False Then
                printPressed = False
            End If

            If IsNothing(Controller.updateMode) AndAlso lastPressed = False _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.T) Then
                lastPressed = True
                handleLast()
            End If

            If GetKeyPress(Keys.T) = False Then
                lastPressed = False
            End If

            If IsNothing(Controller.updateMode) AndAlso prevPressed = False _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.R) Then
                prevPressed = True
                handlePrev()
            End If

            If GetKeyPress(Keys.R) = False Then
                prevPressed = False
            End If

            If IsNothing(Controller.updateMode) AndAlso nextPressed = False _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.N) Then
                nextPressed = True
                handleNext()
            End If

            If GetKeyPress(Keys.N) = False Then
                nextPressed = False
            End If

            If IsNothing(Controller.updateMode) AndAlso firstPressed = False _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.B) Then
                firstPressed = True
                handleFirst()
            End If

            If GetKeyPress(Keys.B) = False Then
                firstPressed = False
            End If

            If Not IsNothing(Controller.updateMode) AndAlso savePressed = False _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.S) Then
                savePressed = True
                handleCheck()
            End If

            If GetKeyPress(Keys.S) = False Then
                savePressed = False
            End If

            If Not IsNothing(Controller.updateMode) AndAlso validatePressed = False _
                AndAlso GetKeyPress(Keys.Menu) AndAlso GetKeyPress(Keys.X) Then
                validatePressed = True
                handleValidateItems()
            End If

            If GetKeyPress(Keys.X) = False Then
                validatePressed = False
            End If
        End If
    End Sub

    Private Sub handleAdd()
        Select Case currentForm
            Case "FAgent"
                FAgent.addClick()
                Exit Select
            Case "FCategory"
                FCategory.addClick()
                Exit Select
            Case "FCustomer"
                FCustomer.addClick()
                Exit Select
            Case "FStock"
                FStock.addClick()
                Exit Select
            Case "FSupplier"
                FSupplier.addClick()
                Exit Select
            Case "FUnit"
                FUnit.addClick()
                Exit Select
            Case "EtcUsers"
                EtcUsers.addClick()
                Exit Select
            Case "TransCC"
                TransCC.addClick()
                Exit Select
            Case "TransPO"
                TransPO.addClick()
                Exit Select
            Case "TransPR"
                TransPR.addClick()
                Exit Select
            Case "TransSO"
                TransSO.addClick()
                Exit Select
            Case "TransSP"
                TransSP.addClick()
                Exit Select
            Case "TransSR"
                TransSR.addClick()
                Exit Select
        End Select
    End Sub

    Private Sub handleAddItem()
        Select Case currentForm
            Case "TransCC"
                TransCC.showSelection()
                Exit Select
            Case "TransPO"
                TransPO.showAddItem()
                Exit Select
            Case "TransSO"
                TransSO.showAddItem()
                Exit Select
            Case "TransSP"
                TransSP.showSelection()
                Exit Select
            Case "EtcAddItem"
                EtcAddItem.saveAndAddAnother()
                Exit Select
        End Select
    End Sub

    Private Sub handleDeleteRow()
        Select Case currentForm
            Case "TransCC"
                TransCC.deleteRow()
                Exit Select
            Case "TransPO"
                TransPO.deleteRow()
                Exit Select
            Case "TransPR"
                TransPR.deleteRow()
                Exit Select
            Case "TransSO"
                TransSO.deleteRow()
                Exit Select
            Case "TransSP"
                TransSP.deleteRow()
                Exit Select
            Case "TransSR"
                TransSR.deleteRow()
                Exit Select
        End Select
    End Sub

    Private Sub handleValidateItems()
        Select Case currentForm
            Case "TransCC"
                TransCC.validateItems()
                Exit Select
            Case "TransPO"
                TransPO.validateItems()
                Exit Select
            Case "TransPR"
                TransPR.validateItems()
                Exit Select
            Case "TransSO"
                TransSO.validateItems()
                Exit Select
            Case "TransSP"
                TransSP.validateItems()
                Exit Select
            Case "TransSR"
                TransSR.validateItems()
                Exit Select
        End Select
    End Sub

    Private Sub handleEdit()
        Select Case currentForm
            Case "FAgent"
                FAgent.editClick()
                Exit Select
            Case "FCategory"
                FCategory.editClick()
                Exit Select
            Case "FCustomer"
                FCustomer.editClick()
                Exit Select
            Case "FStock"
                FStock.editClick()
                Exit Select
            Case "FSupplier"
                FSupplier.editClick()
                Exit Select
            Case "FUnit"
                FUnit.editClick()
                Exit Select
            Case "EtcUsers"
                EtcUsers.editClick()
                Exit Select
            Case "TransCC"
                TransCC.editClick()
                Exit Select
            Case "TransPO"
                TransPO.editClick()
                Exit Select
            Case "TransPR"
                TransPR.editClick()
                Exit Select
            Case "TransSO"
                TransSO.editClick()
                Exit Select
            Case "TransSP"
                TransSP.editClick()
                Exit Select
            Case "TransSR"
                TransSR.editClick()
                Exit Select
        End Select
    End Sub

    Private Sub handleDelete()
        Select Case currentForm
            Case "FAgent"
                FAgent.deleteClick()
                Exit Select
            Case "FCategory"
                FCategory.deleteClick()
                Exit Select
            Case "FCustomer"
                FCustomer.deleteClick()
                Exit Select
            Case "FStock"
                FStock.deleteClick()
                Exit Select
            Case "FSupplier"
                FSupplier.deleteClick()
                Exit Select
            Case "FUnit"
                FUnit.deleteClick()
                Exit Select
            Case "EtcUsers"
                EtcUsers.deleteClick()
                Exit Select
            Case "TransCC"
                TransCC.deleteClick()
                Exit Select
            Case "TransPO"
                TransPO.deleteClick()
                Exit Select
            Case "TransPR"
                TransPR.deleteClick()
                Exit Select
            Case "TransSO"
                TransSO.deleteClick()
                Exit Select
            Case "TransSP"
                TransSP.deleteClick()
                Exit Select
            Case "TransSR"
                TransSR.deleteClick()
                Exit Select
        End Select
    End Sub

    Private Sub handleSearch()
        Select Case currentForm
            Case "FAgent"
                FAgent.search()
                Exit Select
            Case "FCategory"
                FCategory.search()
                Exit Select
            Case "FCustomer"
                FCustomer.search()
                Exit Select
            Case "FStock"
                FStock.search()
                Exit Select
            Case "FSupplier"
                FSupplier.search()
                Exit Select
            Case "FUnit"
                FUnit.search()
                Exit Select
            Case "EtcUsers"
                EtcUsers.search()
                Exit Select
            Case "TransCC"
                TransCC.search()
                Exit Select
            Case "TransPO"
                TransPO.search()
                Exit Select
            Case "TransPR"
                TransPR.search()
                Exit Select
            Case "TransSO"
                TransSO.search()
                Exit Select
            Case "TransSP"
                TransSP.search()
                Exit Select
            Case "TransSR"
                TransSR.search()
                Exit Select
        End Select
    End Sub

    Private Sub handlePrint()
        Select Case currentForm
            Case "FAgent"
                FAgent.printObject()
                Exit Select
            Case "FCategory"
                FCategory.printObject()
                Exit Select
            Case "FCustomer"
                FCustomer.printObject()
                Exit Select
            Case "FStock"
                FStock.printObject()
                Exit Select
            Case "FSupplier"
                FSupplier.printObject()
                Exit Select
            Case "FUnit"
                FUnit.printObject()
                Exit Select
            Case "EtcUsers"
                EtcUsers.printObject()
                Exit Select
            Case "TransCC"
                TransCC.printObject()
                Exit Select
            Case "TransPO"
                TransPO.printObject()
                Exit Select
            Case "TransPR"
                TransPR.printObject()
                Exit Select
            Case "TransSO"
                TransSO.printObject()
                Exit Select
            Case "TransSP"
                TransSP.printObject()
                Exit Select
            Case "TransSR"
                TransSR.printObject()
                Exit Select
        End Select
    End Sub

    Private Sub handleCheck()
        Select Case currentForm
            Case "FAgent"
                FAgent.checkClick()
                Exit Select
            Case "FCategory"
                FCategory.checkClick()
                Exit Select
            Case "FCustomer"
                FCustomer.checkClick()
                Exit Select
            Case "FStock"
                FStock.checkClick()
                Exit Select
            Case "FSupplier"
                FSupplier.checkClick()
                Exit Select
            Case "FUnit"
                FUnit.checkClick()
                Exit Select
            Case "EtcUsers"
                EtcUsers.checkClick()
                Exit Select
            Case "EtcAddItem"
                EtcAddItem.saveItem(True)
                Exit Select
            Case "TransCC"
                TransCC.checkClick()
                Exit Select
            Case "TransPO"
                TransPO.checkClick()
                Exit Select
            Case "TransPR"
                TransPR.checkClick()
                Exit Select
            Case "TransSO"
                TransSO.checkClick()
                Exit Select
            Case "TransSP"
                TransSP.checkClick()
                Exit Select
            Case "TransSR"
                TransSR.checkClick()
                Exit Select
        End Select
    End Sub

    Private Sub handleCancel()
        Select Case currentForm
            Case "FAgent"
                FAgent.cancelClick()
                Exit Select
            Case "FCategory"
                FCategory.cancelClick()
                Exit Select
            Case "FCustomer"
                FCustomer.cancelClick()
                Exit Select
            Case "FStock"
                FStock.cancelClick()
                Exit Select
            Case "FSupplier"
                FSupplier.cancelClick()
                Exit Select
            Case "FUnit"
                FUnit.cancelClick()
                Exit Select
            Case "EtcUsers"
                EtcUsers.cancelClick()
                Exit Select
            Case "TransCC"
                TransCC.cancelClick()
                Exit Select
            Case "TransPO"
                TransPO.cancelClick()
                Exit Select
            Case "TransPR"
                TransPR.cancelClick()
                Exit Select
            Case "TransSO"
                TransSO.cancelClick()
                Exit Select
            Case "TransSP"
                TransSP.cancelClick()
                Exit Select
            Case "TransSR"
                TransSR.cancelClick()
                Exit Select
        End Select
    End Sub

    Private Sub handleClose()
        Select Case currentForm
            Case "EtcHelp"
                EtcHelp.Close()
                Exit Select
            Case "EtcSearch"
                EtcSearch.Close()
                Exit Select
            Case "FAgent"
                FAgent.Close()
                Exit Select
            Case "FCategory"
                FCategory.Close()
                Exit Select
            Case "FCustomer"
                FCustomer.Close()
                Exit Select
            Case "FStock"
                FStock.Close()
                Exit Select
            Case "FSupplier"
                FSupplier.Close()
                Exit Select
            Case "FUnit"
                FUnit.Close()
                Exit Select
            Case "EtcUsers"
                EtcUsers.Close()
                Exit Select
            Case "TransCC"
                TransCC.Close()
                Exit Select
            Case "TransPO"
                TransPO.Close()
                Exit Select
            Case "TransPR"
                TransPR.Close()
                Exit Select
            Case "TransSO"
                TransSO.Close()
                Exit Select
            Case "TransSP"
                TransSP.Close()
                Exit Select
            Case "TransSR"
                TransSR.Close()
                Exit Select
            Case "PostCC"
                PostCC.Close()
                Exit Select
            Case "PostPO"
                PostPO.Close()
                Exit Select
            Case "PostPR"
                PostPR.Close()
                Exit Select
            Case "PostSO"
                PostSO.Close()
                Exit Select
            Case "PostSP"
                PostSP.Close()
                Exit Select
            Case "PostSR"
                PostSR.Close()
                Exit Select
            Case "RptPurchaseMn"
                RptPurchaseMn.Close()
                Exit Select
            Case "RptSalesMn"
                RptSalesMn.Close()
                Exit Select
            Case "RptSOA"
                RptSOA.Close()
                Exit Select
            Case "RptStockMn"
                RptStockMn.Close()
                Exit Select
            Case "RptStockMv"
                RptStockMv.Close()
                Exit Select
            Case "RptSupplierPy"
                RptSupplierPy.Close()
                Exit Select
            Case "RptTransactions"
                RptTransactions.Close()
                Exit Select
            Case "EtcSettings"
                EtcSettings.Close()
                Exit Select
            Case "EtcAddItem"
                EtcAddItem.Close()
                Exit Select
            Case "EtcAbout"
                EtcAbout.Close()
                Exit Select
            Case "SelectCC"
                SelectCC.Close()
                Exit Select
            Case "SelectSP"
                SelectSP.Close()
                Exit Select
        End Select

        currentForm = Nothing
    End Sub

    Private Sub handleLast()
        Select Case currentForm
            Case "TransCC"
                TransCC.lastObject()
                Exit Select
            Case "TransPO"
                TransPO.lastObject()
                Exit Select
            Case "TransPR"
                TransPR.lastObject()
                Exit Select
            Case "TransSO"
                TransSO.lastObject()
                Exit Select
            Case "TransSP"
                TransSP.lastObject()
                Exit Select
            Case "TransSR"
                TransSR.lastObject()
                Exit Select
        End Select
    End Sub

    Private Sub handlePrev()
        Select Case currentForm
            Case "TransCC"
                TransCC.previousObject()
                Exit Select
            Case "TransPO"
                TransPO.previousObject()
                Exit Select
            Case "TransPR"
                TransPR.previousObject()
                Exit Select
            Case "TransSO"
                TransSO.previousObject()
                Exit Select
            Case "TransSP"
                TransSP.previousObject()
                Exit Select
            Case "TransSR"
                TransSR.previousObject()
                Exit Select
        End Select
    End Sub

    Private Sub handleNext()
        Select Case currentForm
            Case "TransCC"
                TransCC.nextObject()
                Exit Select
            Case "TransPO"
                TransPO.nextObject()
                Exit Select
            Case "TransPR"
                TransPR.nextObject()
                Exit Select
            Case "TransSO"
                TransSO.nextObject()
                Exit Select
            Case "TransSP"
                TransSP.nextObject()
                Exit Select
            Case "TransSR"
                TransSR.nextObject()
                Exit Select
        End Select
    End Sub

    Private Sub handleFirst()
        Select Case currentForm
            Case "TransCC"
                TransCC.firstObject()
                Exit Select
            Case "TransPO"
                TransPO.firstObject()
                Exit Select
            Case "TransPR"
                TransPR.firstObject()
                Exit Select
            Case "TransSO"
                TransSO.firstObject()
                Exit Select
            Case "TransSP"
                TransSP.firstObject()
                Exit Select
            Case "TransSR"
                TransSR.firstObject()
                Exit Select
        End Select
    End Sub

#End Region

End Class
