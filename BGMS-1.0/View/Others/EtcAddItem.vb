Public Class EtcAddItem

    Private tick As Integer
    Private stockDesc As String
    Private stockCost As Double

    Public fromPO, d1enabled, d2enabled, d3enabled As Boolean
    Public d1, d2, d3 As Double

    Public Sub openUp(ByVal fromScreen As String)
        Controller.previousForm = fromScreen
        Controller.currentForm = Me.Name
        Me.ShowDialog()
    End Sub

    Protected Overloads Overrides Function ProcessDialogKey(ByVal keyData As Keys) As Boolean
        Select Case keyData
            Case Keys.Enter
                If tbDisc3.Enabled And tbDisc3.Visible Then
                    If tbDisc3.Focused Then
                        saveAndAddAnother()
                        Exit Select
                    End If
                ElseIf tbDisc2.Enabled Then
                    If tbDisc2.Focused Then
                        saveAndAddAnother()
                        Exit Select
                    End If
                ElseIf tbDisc1.Enabled Then
                    If tbDisc1.Focused Then
                        saveAndAddAnother()
                        Exit Select
                    End If
                ElseIf tbPrice.Focused Then
                    saveAndAddAnother()
                    Exit Select
                End If

                Return MyBase.ProcessDialogKey(Keys.Tab)
        End Select
        Return MyBase.ProcessDialogKey(keyData)
    End Function

    Private Sub etcFormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Controller.currentForm = Controller.previousForm
    End Sub

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        initialize()
        filterGrid()
    End Sub

    Private Sub EtcSearchShown(sender As Object, e As EventArgs) Handles Me.Shown
        tbStock.Focus()
        Util.notifyDisplay(False)
    End Sub

    Private Sub initialize()
        Controller.initCategories()
        tbCategory.AutoCompleteSource = AutoCompleteSource.CustomSource
        tbCategory.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        tbCategory.AutoCompleteCustomSource = Controller.categoryList

        lblDisc3.Visible = If(fromPO, True, False)
        tbDisc3.Visible = If(fromPO, True, False)
        lblAvailable.Visible = If(Not fromPO, True, False)
        tbAvailable.Visible = If(Not fromPO, True, False)

        tick = 0
        resetAll()
        loadGrid()
    End Sub

    Private Sub resetAll()
        tbStock.Clear()
        tbDescription.Clear()
        tbCategory.Clear()

        resetInputFields()
    End Sub

    Private Sub resetInputFields()
        lblStock.Text = String.Empty
        tbQty.Clear()
        tbPrice.Clear()
        tbDisc1.Clear()
        tbDisc2.Clear()
        tbDisc3.Clear()

        tbOnHand.Clear()
        tbAvailable.Clear()
        tbAmount.Clear()

        enableInputs(False)
    End Sub

    Private Sub enableInputs(ByVal enable As Boolean)
        tbQty.Enabled = enable
        tbPrice.Enabled = enable

        tbDisc1.Enabled = If(d1enabled, enable, False)
        tbDisc2.Enabled = If(d2enabled, enable, False)
        tbDisc3.Enabled = If(d3enabled, enable, False)
    End Sub

    Private Sub loadGrid()
        itemsGrid.Dock = DockStyle.Fill
        itemsGrid.Columns.Clear()

        itemsGrid.Columns.Add("Id", "Id")
        itemsGrid.Columns.Add("Stock", "Stock")
        itemsGrid.Columns.Add("Description", "Description")
        itemsGrid.Columns.Add("Price", "Price")
        itemsGrid.Columns.Add("OnHand", "OnHand")
        itemsGrid.Columns.Add("Cost", "Cost")

        itemsGrid.Columns("Id").Visible = False
        itemsGrid.Columns("Price").Visible = False
        itemsGrid.Columns("OnHand").Visible = False
        itemsGrid.Columns("Cost").Visible = False

        itemsGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        itemsGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
    End Sub

    Private Sub filterGrid()
        itemsGrid.Rows.Clear()

        Using context As New bgmsEntities
            Dim stocks = getFilteredStock()

            For Each stock In stocks
                itemsGrid.Rows.Add(stock.Id, stock.Name, _
                    stock.Description, stock.Price, stock.QtyOnHand, stock.Cost)
            Next
        End Using

        setValues(0)
    End Sub

    Private Function getFilteredStock() As List(Of stock)
        Dim byName = Not String.IsNullOrWhiteSpace(tbStock.Text)
        Dim byDesc = Not String.IsNullOrWhiteSpace(tbDescription.Text)
        Dim byCategory = Not String.IsNullOrWhiteSpace(tbCategory.Text)

        Dim qry As String = "select * from stocks s where s.active = true"

        If byName Then
            qry += " and ucase(s.name) like '%" & tbStock.Text & "%'"
        End If

        If byDesc Then
            qry += " and ucase(s.description) like '%" & tbDescription.Text & "%'"
        End If

        If byCategory Then
            qry += " and ucase(c.name) like '%" & tbCategory.Text & "%'"
            qry = qry.Replace("from stocks s where", _
                        "from stocks s, categories c where s.categoryid = c.id and")
        End If

        qry += " order by s.name limit 300"

        Using context As New bgmsEntities
            Return context.stocks.SqlQuery(qry).ToList
        End Using
    End Function

    Private Sub tbDescription_TextChanged(sender As Object, e As EventArgs) Handles tbDescription.TextChanged
        If tbDescription.Focused Then
            preFilter()
        End If
    End Sub

    Private Sub tbStock_TextChanged(sender As Object, e As EventArgs) Handles tbStock.TextChanged
        If tbStock.Focused Then
            preFilter()
        End If
    End Sub

    Private Sub tbCategory_TextChanged(sender As Object, e As EventArgs) Handles tbCategory.TextChanged
        If tbCategory.Focused Then
            preFilter()
        End If
    End Sub

    Private Function getErrorMessage() As String
        If String.IsNullOrEmpty(lblStock.Text) Then
            Return "Stock is required."
        End If

        If String.IsNullOrEmpty(tbQty.Text) Then
            Return "Quantity is required."
        End If

        If String.IsNullOrEmpty(tbPrice.Text) Then
            Return "Price is required."
        End If

        Return Nothing
    End Function

    Public Sub saveItem(ByVal close As Boolean)
        Dim msg As String = getErrorMessage()

        If Not String.IsNullOrEmpty(msg) Then
            Util.notifyError(msg)
        Else
            Dim unit As String

            Using context As New bgmsEntities
                unit = context.stocks.Where(Function(c) _
                    c.Name.ToUpper.Equals(lblStock.Text.ToUpper)) _
                    .Select(Function(c) c.unit.Name).FirstOrDefault
            End Using

            Dim values As New List(Of String)
            values.Add(String.Empty)
            values.Add(lblStock.Text)
            values.Add(stockDesc)
            values.Add(tbQty.Text)
            values.Add(unit)
            values.Add(tbPrice.Text)
            values.Add(stockCost)
            values.Add(tbOnHand.Text)
            values.Add(tbAvailable.Text)
            values.Add(tbDisc1.Text)
            values.Add(tbDisc2.Text)
            values.Add(tbDisc3.Text)

            If fromPO Then
                TransPO.addOrderItem(values)
            Else
                TransSO.addOrderItem(values)
            End If

            If close Then
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnCheckAdd_Click(sender As Object, e As EventArgs) Handles btnCheckAdd.Click
        saveAndAddAnother()
    End Sub

    Public Sub saveAndAddAnother()
        saveItem(False)
        resetAll()
        tbStock.Focus()
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        saveItem(True)
    End Sub

    Private Sub itemsGrid_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles itemsGrid.CellClick
        setValues(e.RowIndex)
    End Sub

    Private Sub setValues(ByVal index As Integer)
        If index >= 0 Then
            Dim stockName = itemsGrid("Stock", index).Value

            If Not String.IsNullOrWhiteSpace(stockName) Then
                enableInputs(True)

                lblStock.Text = stockName

                stockDesc = itemsGrid("Description", index).Value
                Double.TryParse(itemsGrid("Cost", index).Value, stockCost)

                tbQty.Text = 1

                If fromPO Then
                    tbPrice.Text = FormatNumber(If(stockCost > 0, stockCost, itemsGrid("Price", index).Value), 2)
                Else
                    tbPrice.Text = FormatNumber(itemsGrid("Price", index).Value, 2)
                End If

                tbPrice.Text = FormatNumber(If(fromPO, itemsGrid("Price", index).Value, _
                    itemsGrid("Price", index).Value), 2)

                tbOnHand.Text = itemsGrid("OnHand", index).Value
                tbDisc1.Text = FormatNumber(d1, 2)
                tbDisc2.Text = FormatNumber(d2, 2)
                tbDisc3.Text = FormatNumber(d3, 2)
                tbAvailable.Text = Util.getStockAvailableQty(itemsGrid("Id", index).Value)
            Else
                resetInputFields()
            End If
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

    Private Sub itemsGrid_KeyDown(sender As Object, e As KeyEventArgs) Handles itemsGrid.KeyDown
        If e.KeyCode = Keys.Enter AndAlso Not IsNothing(itemsGrid.CurrentCell) Then
            Dim rowIndex = itemsGrid.CurrentCell.RowIndex

            If Not String.IsNullOrEmpty(itemsGrid("Stock", rowIndex).Value) Then
                setValues(rowIndex)
                tbQty.Focus()
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub tbStock_KeyDown(sender As Object, e As KeyEventArgs) Handles tbStock.KeyDown
        checkForUpOrDown(e)
    End Sub

    Private Sub tbCategory_KeyDown(sender As Object, e As KeyEventArgs) Handles tbCategory.KeyDown
        'checkForUpOrDown(e)
    End Sub

    Private Sub tbDescription_KeyDown(sender As Object, e As KeyEventArgs) Handles tbDescription.KeyDown
        checkForUpOrDown(e)
    End Sub

    Private Sub checkForUpOrDown(ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Up Then
            itemsGrid.Focus()
            SendKeys.Send("{UP}")
        End If

        If e.KeyCode = Keys.Down Then
            itemsGrid.Focus()
            SendKeys.Send("{DOWN}")
        End If
    End Sub

    Private Sub updateAmount()
        Dim qty, price, d1, d2, d3 As Double
        Double.TryParse(tbQty.Text, qty)
        Double.TryParse(tbPrice.Text, price)
        Double.TryParse(tbDisc1.Text, d1)
        Double.TryParse(tbDisc2.Text, d2)
        Double.TryParse(tbDisc3.Text, d3)

        If fromPO Then
            tbAmount.Text = FormatNumber(TransPO.getRowAmount(qty, price, d1, d2, d3), 2)
        Else
            tbAmount.Text = FormatNumber(TransSO.getRowAmount(qty, price, d1, d2), 2)
        End If
    End Sub

    Private Sub tbQty_TextChanged(sender As Object, e As EventArgs) Handles tbQty.TextChanged
        updateAmount()
    End Sub

    Private Sub tbPrice_TextChanged(sender As Object, e As EventArgs) Handles tbPrice.TextChanged
        updateAmount()
    End Sub

    Private Sub tbDisc1_TextChanged(sender As Object, e As EventArgs) Handles tbDisc1.TextChanged
        updateAmount()
    End Sub

    Private Sub tbDisc2_TextChanged(sender As Object, e As EventArgs) Handles tbDisc2.TextChanged
        updateAmount()
    End Sub

    Private Sub tbDisc3_TextChanged(sender As Object, e As EventArgs) Handles tbDisc3.TextChanged
        updateAmount()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class