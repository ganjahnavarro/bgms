Public Class PostSR

    Private Sub postConstruct(sender As Object, e As EventArgs) Handles MyBase.Load
        itemsGrid.BackgroundColor = Color.WhiteSmoke
        loadGrid()
        loadUnposteds()
    End Sub

    Private Sub loadGrid()
        itemsGrid.Dock = DockStyle.Fill
        itemsGrid.Columns.Clear()

        itemsGrid.Columns.Add("Id", "Id")
        itemsGrid.Columns.Add("srNumber", "SR No.")
        itemsGrid.Columns.Add("Customer", "Customer")
        itemsGrid.Columns.Add("Date", "Date")
        itemsGrid.Columns.Add("Amount", "Amount")

        itemsGrid.Columns.Item("Id").Visible = False
        itemsGrid.Columns.Item("Customer").MinimumWidth = 220

        itemsGrid.Columns.Item("Amount").DefaultCellStyle.Alignment = _
            DataGridViewContentAlignment.MiddleRight
        itemsGrid.Columns.Item("Amount").DefaultCellStyle.Format = "N2"
    End Sub

    Private Sub loadUnposteds()
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim unposteds = context.salesreturns _
                .Where(Function(c) c.PostedDate.Equals(Nothing)).ToList()

            itemsGrid.Rows.Clear()

            For Each obj In unposteds
                itemsGrid.Rows.Add(
                    obj.Id,
                    obj.DocumentNo,
                    obj.customer.Name,
                    Format(obj.Date, Constants.DATE_FORMAT),
                    obj.TotalAmount)
            Next

            lblNoRecords.Visible = If(unposteds.Count = 0, True, False)
            checkSelection()
        End Using
    End Sub

    Private Sub btn3_Click(sender As Object, e As EventArgs) Handles btn3.Click
        If Util.askForConfirmation("Post selected Sales Returns?", "Post") Then
            postSelectedOrders()
        End If
    End Sub

    Private Sub postSelectedOrders()
        Dim ids As New List(Of Integer)
        For Each row As DataGridViewRow In itemsGrid.SelectedRows
            ids.Add(itemsGrid("Id", row.Index).Value)
        Next

        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim returns = context.salesreturns _
                      .Where(Function(c) ids.Contains(c.Id)).ToList()

            For Each rtn In returns
                rtn.PostedDate = docDate.Value

                'Posting
                Dim soUsed As New List(Of salesorder)

                For Each rtnItem In rtn.salesreturnitems
                    Dim totalReturn = rtnItem.Quantity

                    Dim qry As String = "select i.* from salesorderitems i, customers c, stocks s, salesorders so " &
                        "where i.salesorderid = so.id and i.stockid = s.id and so.customerid = c.id " &
                        "and so.posteddate is not null and ucase(c.name) = '" & rtn.customer.Name.ToUpper & "' " &
                        "and s.id = '" & rtnItem.stockId & "' and i.discount1 = '" & rtnItem.Discount1 & "' " &
                        "and i.discount2 = '" & rtnItem.Discount2 & "' and c.active = true and i.price = '" & rtnItem.Price & "'"

                    Dim soItems = context.salesorderitems.SqlQuery(qry) _
                        .OrderByDescending(Function(c) c.Id).ToList

                    '.Where(Function(c) c.salesorder.customer.Name.ToUpper.Equals(rtnItem.salesreturn.customer.Name.ToUpper) _
                    '        And c.salesorder.customer.Active = True _
                    '        And c.stockId.Equals(rtnItem.stockId) _
                    '        And c.Price.Equals(rtnItem.Price) _
                    '        And c.Discount1.Equals(rtnItem.Discount1) _
                    '        And c.Discount2.Equals(rtnItem.Discount2) _
                    '        And Not c.salesorder.PostedDate.Equals(Nothing))

                    soUsed.Clear()

                    'Update total qty returned of so items
                    For Each soItem In soItems
                        If Not soUsed.Contains(soItem.salesorder) Then
                            soUsed.Add(soItem.salesorder)
                        End If

                        If soItem.getRemainingQuantity < totalReturn Then
                            soItem.QuantityReturned += soItem.getRemainingQuantity
                        Else
                            soItem.QuantityReturned += totalReturn
                        End If

                        totalReturn = totalReturn - soItem.QuantityReturned

                        If totalReturn = 0 Then
                            Exit For
                        End If
                    Next

                    'Update total returned of salesorders
                    For Each so In soUsed
                        so.TotalReturned = so.getTotalReturnedFromItems
                    Next

                    rtnItem.stock.QtyOnHand += rtnItem.Quantity
                Next

                Dim action As String = Controller.currentUser.Username & " posted a sales return (" &
                   rtn.DocumentNo & ")"
                context.activities.Add(New activity(action))
            Next
            context.SaveChangesAsync()
        End Using

        Util.notifyInfo(ids.Count & " Sales Return/s was posted.")
        loadUnposteds()
    End Sub

    Private Sub btnMark_Click(sender As Object, e As EventArgs) Handles btnMark.Click
        itemsGrid.SelectAll()
        checkSelection()
    End Sub

    Private Sub btnUnmark_Click(sender As Object, e As EventArgs) Handles btnUnmark.Click
        itemsGrid.ClearSelection()
        checkSelection()
    End Sub

    Private Sub checkSelection()
        btn3.Visible = If(itemsGrid.SelectedRows.Count = 0, False, True)
    End Sub

    Private Sub itemsGrid_SelectionChanged(sender As Object, e As EventArgs) Handles itemsGrid.SelectionChanged
        checkSelection()
    End Sub

End Class