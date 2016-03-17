Public Class EtcSearch

    Private tick As Integer
    Private amountColumn As String

    Public Sub openUp(ByVal fromScreen As String)
        Controller.previousForm = fromScreen
        Controller.currentForm = Me.Name
        Me.ShowDialog()
    End Sub

    Private Sub EtcSearchFormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Controller.currentForm = Controller.previousForm
    End Sub

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        initialize()
        filterGrid()
    End Sub

    Private Sub EtcSearchShown(sender As Object, e As EventArgs) Handles Me.Shown
        tbDoc.Focus()
        Util.notifyDisplay(False)
    End Sub

    Private Sub initialize()
        tick = 0
        resetAll()
        tbFilter.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbFilter.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        initLabels()
        loadGrid()
    End Sub

    Private Sub resetAll()
        tbDoc.Clear()
        tbFilter.Clear()
        dateTo.Value = Date.Today.AddDays(7)
        dateFrom.Value = Date.Today.AddYears(-5)
    End Sub

    Private Sub initLabels()
        Select Case Controller.previousForm
            Case TransPO.Name, TransPR.Name
                Controller.initSuppliers()
                lblFilter.Text = "Supplier"
                tbFilter.AutoCompleteCustomSource = Controller.supplierList
                amountColumn = "Total Amount"
                Exit Select

            Case TransSO.Name, TransSR.Name
                Controller.initCustomers()
                lblFilter.Text = "Customer"
                tbFilter.AutoCompleteCustomSource = Controller.customerList
                amountColumn = "Total Amount"
                Exit Select

            Case TransCC.Name
                Controller.initCustomers()
                lblFilter.Text = "Customer"
                tbFilter.AutoCompleteCustomSource = Controller.customerList
                amountColumn = "Total Check"
                Exit Select

            Case TransSP.Name
                Controller.initSuppliers()
                lblFilter.Text = "Supplier"
                tbFilter.AutoCompleteCustomSource = Controller.supplierList
                amountColumn = "Total Check"
                Exit Select
        End Select
    End Sub

    Private Sub loadGrid()
        itemsGrid.Dock = DockStyle.Fill
        itemsGrid.Columns.Clear()

        itemsGrid.Columns.Add("DocNo", "Doc No.")
        itemsGrid.Columns.Add("Date", "Date")
        itemsGrid.Columns.Add(lblFilter.Text, lblFilter.Text)
        itemsGrid.Columns.Add("Amount", amountColumn)

        itemsGrid.Columns(lblFilter.Text).MinimumWidth = 200

        itemsGrid.Columns("Amount").DefaultCellStyle.Alignment = _
            DataGridViewContentAlignment.MiddleRight
        itemsGrid.Columns("Amount").DefaultCellStyle.Format = "N2"

        If Controller.previousForm.Equals(TransCC.Name) _
            OrElse Controller.previousForm.Equals(TransSP.Name) Then
            itemsGrid.Columns.Add("Paid", "Total Paid")
            itemsGrid.Columns("Paid").DefaultCellStyle.Alignment = _
            DataGridViewContentAlignment.MiddleRight
            itemsGrid.Columns("Paid").DefaultCellStyle.Format = "N2"
        End If
    End Sub

    Private Sub filterGrid()
        itemsGrid.Rows.Clear()

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Select Case Controller.previousForm
                Case TransPO.Name
                    Dim objx = context.purchaseorders.Include("Supplier") _
                        .Where(Function(c) c.DocumentNo.ToUpper.Contains(tbDoc.Text.ToUpper) _
                            And c.supplier.Name.ToUpper.Contains(tbFilter.Text.ToUpper) _
                            And c.supplier.Active = True _
                            And c.Date >= dateFrom.Value And c.Date <= dateTo.Value) _
                        .Take(300).ToList

                    For Each obj In objx
                        itemsGrid.Rows.Add(obj.DocumentNo,
                            Format(obj.Date, Constants.DATE_FORMAT),
                            obj.supplier.Name, obj.TotalAmount)
                    Next
                    Exit Select

                Case TransSO.Name
                    Dim objx = context.salesorders.Include("Customer") _
                        .Where(Function(c) c.DocumentNo.ToUpper.Contains(tbDoc.Text.ToUpper) _
                            And c.customer.Name.ToUpper.Contains(tbFilter.Text.ToUpper) _
                            And c.customer.Active = True _
                            And c.Date >= dateFrom.Value And c.Date <= dateTo.Value) _
                        .Take(300).ToList

                    For Each obj In objx
                        itemsGrid.Rows.Add(obj.DocumentNo,
                            Format(obj.Date, Constants.DATE_FORMAT),
                            obj.customer.Name, obj.TotalAmount)
                    Next
                    Exit Select

                Case TransPR.Name
                    Dim objx = context.purchasereturns.Include("Supplier") _
                        .Where(Function(c) c.DocumentNo.ToUpper.Contains(tbDoc.Text.ToUpper) _
                            And c.supplier.Name.ToUpper.Contains(tbFilter.Text.ToUpper) _
                            And c.supplier.Active = True _
                            And c.Date >= dateFrom.Value And c.Date <= dateTo.Value) _
                        .Take(300).ToList

                    For Each obj In objx
                        itemsGrid.Rows.Add(obj.DocumentNo,
                            Format(obj.Date, Constants.DATE_FORMAT),
                            obj.supplier.Name, obj.TotalAmount)
                    Next
                    Exit Select

                Case TransSR.Name
                    Dim objx = context.salesreturns.Include("Customer") _
                        .Where(Function(c) c.DocumentNo.ToUpper.Contains(tbDoc.Text.ToUpper) _
                            And c.customer.Name.ToUpper.Contains(tbFilter.Text.ToUpper) _
                            And c.customer.Active = True _
                            And c.Date >= dateFrom.Value And c.Date <= dateTo.Value) _
                        .Take(300).ToList

                    For Each obj In objx
                        itemsGrid.Rows.Add(obj.DocumentNo,
                            Format(obj.Date, Constants.DATE_FORMAT),
                            obj.customer.Name, obj.TotalAmount)
                    Next
                    Exit Select

                Case TransCC.Name
                    Dim objx = context.customercollections.Include("Customer") _
                        .Where(Function(c) c.DocumentNo.ToUpper.Contains(tbDoc.Text.ToUpper) _
                            And c.customer.Name.ToUpper.Contains(tbFilter.Text.ToUpper) _
                            And c.customer.Active = True _
                            And c.Date >= dateFrom.Value And c.Date <= dateTo.Value) _
                        .Take(300).ToList

                    For Each obj In objx
                        itemsGrid.Rows.Add(obj.DocumentNo,
                            Format(obj.Date, Constants.DATE_FORMAT),
                            obj.customer.Name, obj.TotalCheck, obj.TotalPaid)
                    Next
                    Exit Select

                Case TransSP.Name
                    Dim objx = context.supplierpayments.Include("Supplier") _
                        .Where(Function(c) c.DocumentNo.ToUpper.Contains(tbDoc.Text.ToUpper) _
                            And c.supplier.Name.ToUpper.Contains(tbFilter.Text.ToUpper) _
                            And c.supplier.Active = True _
                            And c.Date >= dateFrom.Value And c.Date <= dateTo.Value) _
                        .Take(300).ToList

                    For Each obj In objx
                        itemsGrid.Rows.Add(obj.DocumentNo,
                            Format(obj.Date, Constants.DATE_FORMAT),
                            obj.supplier.Name, obj.TotalCheck, obj.TotalPaid)
                    Next
                    Exit Select
            End Select
        End Using
    End Sub

    Private Sub selectObject(ByVal docNo As String)
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Select Case Controller.previousForm
                Case TransPO.Name
                    TransPO.currentObject = context.purchaseorders _
                        .Where(Function(c) c.DocumentNo.ToUpper.Equals(docNo.ToUpper)) _
                        .FirstOrDefault
                    TransPO.loadObject()
                    Exit Select

                Case TransSO.Name
                    TransSO.currentObject = context.salesorders _
                        .Where(Function(c) c.DocumentNo.ToUpper.Equals(docNo.ToUpper)) _
                        .FirstOrDefault
                    TransSO.loadObject()
                    Exit Select

                Case TransPR.Name
                    TransPR.currentObject = context.purchasereturns _
                        .Where(Function(c) c.DocumentNo.ToUpper.Equals(docNo.ToUpper)) _
                        .FirstOrDefault
                    TransPR.loadObject()
                    Exit Select

                Case TransSR.Name
                    TransSR.currentObject = context.salesreturns _
                        .Where(Function(c) c.DocumentNo.ToUpper.Equals(docNo.ToUpper)) _
                        .FirstOrDefault
                    TransSR.loadObject()
                    Exit Select

                Case TransCC.Name
                    TransCC.currentObject = context.customercollections _
                        .Where(Function(c) c.DocumentNo.ToUpper.Equals(docNo.ToUpper)) _
                        .FirstOrDefault
                    TransCC.loadObject()
                    Exit Select

                Case TransSP.Name
                    TransSP.currentObject = context.supplierpayments _
                        .Where(Function(c) c.DocumentNo.ToUpper.Equals(docNo.ToUpper)) _
                        .FirstOrDefault
                    TransSP.loadObject()
                    Exit Select
            End Select
        End Using
        Me.Close()
    End Sub

    Private Sub itemsGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles itemsGrid.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not IsNothing(itemsGrid.CurrentCell) Then
            Dim rowIndex = itemsGrid.CurrentCell.RowIndex

            If Not String.IsNullOrEmpty(itemsGrid("DocNo", rowIndex).Value) Then
                selectObject(itemsGrid("DocNo", rowIndex).Value)
            End If
        End If
    End Sub

    Private Sub tbDoc_KeyDown(sender As Object, e As KeyEventArgs) Handles tbDoc.KeyDown
        If e.KeyCode = Keys.Up Then
            itemsGrid.Focus()
            SendKeys.Send("{UP}")
        End If

        If e.KeyCode = Keys.Down Then
            itemsGrid.Focus()
            SendKeys.Send("{DOWN}")
        End If

        If e.KeyCode = Keys.Enter Then
            itemsGrid_KeyDown(sender, e)
        End If
    End Sub

    Private Sub tbFilter_TextChanged(sender As Object, e As EventArgs) Handles tbFilter.TextChanged
        If tbFilter.Focused Then
            preFilter()
        End If
    End Sub

    Private Sub tbDoc_TextChanged(sender As Object, e As EventArgs) Handles tbDoc.TextChanged
        If tbDoc.Focused Then
            preFilter()
        End If
    End Sub

    Private Sub dateFrom_ValueChanged(sender As Object, e As EventArgs) Handles dateFrom.ValueChanged
        If dateFrom.Focused Then
            preFilter()
        End If
    End Sub

    Private Sub dateTo_ValueChanged(sender As Object, e As EventArgs) Handles dateTo.ValueChanged
        If dateTo.Focused Then
            preFilter()
        End If
    End Sub

    Private Sub preFilter()
        delayTimer.Enabled = True
        tick = 0
    End Sub

    Private Sub delayTimer_Tick(sender As Object, e As EventArgs) Handles delayTimer.Tick
        If tick > 3 Then
            filterGrid()
            delayTimer.Enabled = False
        End If

        tick += 1
    End Sub

End Class
