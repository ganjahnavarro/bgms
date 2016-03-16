Public Class StockSelection

    Dim selectedRowIndex As Integer = 1
    Dim cell As DataGridViewCell

    Public Sub OpenStockSelection(ByRef cell As DataGridViewCell)
        'Me.Show()
        Me.cell = cell
    End Sub

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        loadGrid()
    End Sub

    Private Sub StockSelection_Deactivate(sender As Object, e As EventArgs) Handles Me.Deactivate
        Console.WriteLine("Deactivating..")
        'Me.Close()
    End Sub

    Private Sub loadGrid()
        stockGrid.Dock = DockStyle.Fill
        Util.clearRows(stockGrid)
        stockGrid.Columns.Clear()

        stockGrid.Columns.Add("Stock", "Stock")
        stockGrid.Columns.Add("Desc", "Description")

        stockGrid.Columns("Stock").FillWeight = 3
        stockGrid.Columns("Desc").FillWeight = 5
    End Sub

    Private Sub loadItems()
        If Not IsNothing(stockGrid.Rows) Then
            stockGrid.Rows.Clear()
        End If

        For Each pair In Controller.stockDictionary
            If pair.Key.StartsWith(stockInput.Text) Then
                stockGrid.Rows.Add(pair.Key, pair.Value)

                If stockGrid.Rows.Count > 30 Then
                    Exit Sub
                End If
            End If
        Next
    End Sub

    Private Sub stockInput_TextChanged(sender As Object, e As EventArgs) Handles stockInput.TextChanged
        loadItems()
        setSelectedRow()
    End Sub

    Private Sub stockInput_KeyDown(sender As Object, e As KeyEventArgs) Handles stockInput.KeyDown
        If e.KeyCode.Equals(Keys.Down) Then
            If selectedRowIndex + 1 < stockGrid.RowCount Then
                selectedRowIndex = selectedRowIndex + 1
                setSelectedRow()
            End If
        ElseIf e.KeyCode.Equals(Keys.Up) Then
            If selectedRowIndex - 1 >= 0 Then
                selectedRowIndex = selectedRowIndex - 1
                setSelectedRow()
            End If
        ElseIf e.KeyCode.Equals(Keys.Enter) Then
            Console.WriteLine("Value: " & stockGrid.CurrentCell.Value)
            cell.Value = stockGrid.CurrentCell.Value
            Me.Close()
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub setSelectedRow()
        If stockGrid.RowCount > 0 Then
            stockGrid.ClearSelection()
            stockGrid.CurrentCell = stockGrid.Rows(selectedRowIndex).Cells(0)
            stockGrid.Rows(selectedRowIndex).Selected = True

            'stockInput.Text = stockGrid.CurrentCell.Value
        End If
    End Sub

End Class