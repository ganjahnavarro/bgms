Public Class SalesOrderSyncer

    Public Shared Sub sync()
        Dim updatedCount As Integer = 0

        Dim masterDictionary As New Dictionary(Of String, salesorder)
        Dim slaveDictionary As New Dictionary(Of String, salesorder)

        Dim unsyncedMaster As New Dictionary(Of String, salesorder)
        Dim unsyncedSlave As New Dictionary(Of String, salesorder)

        loadAllRecordsByDate(masterDictionary, Constants.CONNECTION_STRING_NAME_MASTER)
        loadAllRecordsByDate(slaveDictionary, Constants.CONNECTION_STRING_NAME_SLAVE)

        Dim keys As New List(Of String)

        For Each pair In masterDictionary
            keys.Add(pair.Value.DocumentNo)
        Next

        For Each pair In slaveDictionary
            keys.Add(pair.Value.DocumentNo)
        Next

        loadAllRecords(masterDictionary, Constants.CONNECTION_STRING_NAME_MASTER, keys)
        loadAllRecords(slaveDictionary, Constants.CONNECTION_STRING_NAME_SLAVE, keys)

        loadUnsyncedRecords(masterDictionary, slaveDictionary, unsyncedMaster)
        loadUnsyncedRecords(slaveDictionary, masterDictionary, unsyncedSlave)

        updatedCount += saveAndUpdateRecords(unsyncedMaster, unsyncedSlave, Constants.CONNECTION_STRING_NAME_SLAVE)
        updatedCount += saveAndUpdateRecords(unsyncedSlave, unsyncedMaster, Constants.CONNECTION_STRING_NAME_MASTER)

        Console.WriteLine("Updated sales orders: " & updatedCount)
    End Sub

    Private Shared Sub loadAllRecordsByDate(ByRef dictionary As Dictionary(Of String, salesorder), ByVal connection As String)
        Using context As New bgmsEntities(connection)
            dictionary = context.salesorders _
                .Where(Function(c) c.ModifyDate >= Constants.LAST_SYNC_DATE) _
                .ToDictionary(Function(c) c.DocumentNo & "@" & c.ModifyDate.ToString, Function(c) c)
        End Using
    End Sub

    Private Shared Sub loadAllRecords(ByRef dictionary As Dictionary(Of String, salesorder),
        ByVal connection As String, ByVal keys As List(Of String))

        Using context As New bgmsEntities(connection)
            dictionary = context.salesorders _
                .Include("Customer").Include("SalesOrderItems.Stock").Include("Agent") _
                .Where(Function(c) keys.Contains(c.DocumentNo)) _
                .ToDictionary(Function(c) c.DocumentNo & "@" & c.ModifyDate.ToString, Function(c) c)
        End Using
    End Sub

    Private Shared Sub loadUnsyncedRecords(ByVal outer As Dictionary(Of String, salesorder),
        ByVal inner As Dictionary(Of String, salesorder), ByRef unsynced As Dictionary(Of String, salesorder))
        For Each pair In outer
            If inner.ContainsKey(pair.Key) = False Then
                unsynced.Add(pair.Key, pair.Value)
            End If
        Next
    End Sub

    Private Shared Function saveAndUpdateRecords(ByRef outer As Dictionary(Of String, salesorder),
        ByRef inner As Dictionary(Of String, salesorder), ByVal connection As String) As Integer

        Dim updatedCount As Integer = 0

        Using context As New bgmsEntities(connection)
            For Each pair In outer
                Dim found As Boolean = False
                Dim newest As Boolean = False

                For Each innerPair In inner
                    'check if update or new
                    If pair.Value.DocumentNo = innerPair.Value.DocumentNo Then
                        found = True

                        'check if outer is newer than inner
                        If pair.Value.ModifyDate > innerPair.Value.ModifyDate Then
                            newest = True
                        End If
                    End If
                Next

                If found = False Then
                    'new record
                    Dim newObject As New salesorder
                    copyObjectValues(pair.Value, newObject, context)
                    context.salesorders.Add(newObject)
                    updatedCount += 1
                Else
                    If newest Then
                        'update record
                        Dim updateObject = context.salesorders.Where(Function(c) c.DocumentNo = pair.Value.DocumentNo).FirstOrDefault

                        If Not IsNothing(updateObject) Then
                            copyObjectValues(pair.Value, updateObject, context)
                            updatedCount += 1
                        End If
                    End If
                End If
            Next
            context.SaveChanges()
        End Using

        Return updatedCount
    End Function

    Private Shared Sub copyObjectValues(ByVal fromObject As salesorder, ByRef toObject As salesorder, ByRef context As bgmsEntities)
        toObject.ModifyBy = fromObject.ModifyBy
        toObject.ModifyDate = fromObject.ModifyDate
        toObject.Date = fromObject.Date
        toObject.Discount1 = fromObject.Discount1
        toObject.Discount2 = fromObject.Discount2
        toObject.PostedDate = fromObject.PostedDate
        toObject.Remarks = fromObject.Remarks
        toObject.TotalAmount = fromObject.TotalAmount
        toObject.TotalPaid = fromObject.TotalPaid
        toObject.TotalReturned = fromObject.TotalReturned
        toObject.DocumentNo = fromObject.DocumentNo

        context.salesorderitems.RemoveRange(toObject.salesorderitems)

        For Each item In fromObject.salesorderitems
            Dim newItem As New salesorderitem
            copyObjectItemValues(item, newItem, context)
            toObject.salesorderitems.Add(newItem)
        Next

        toObject.agentId = context.agents.Where(Function(c) c.Name = fromObject.agent.Name AndAlso c.Active).Select(Function(c) c.Id).FirstOrDefault
        toObject.customerId = context.customers.Where(Function(c) c.Name = fromObject.customer.Name AndAlso c.Active).Select(Function(c) c.Id).FirstOrDefault
    End Sub

    Private Shared Sub copyObjectItemValues(ByVal fromItem As salesorderitem, ByRef toItem As salesorderitem, ByRef context As bgmsEntities)
        toItem.Discount1 = fromItem.Discount1
        toItem.Discount2 = fromItem.Discount2
        toItem.Price = fromItem.Price
        toItem.Quantity = fromItem.Quantity
        toItem.QuantityReturned = fromItem.QuantityReturned

        toItem.stockId = context.stocks.Where(Function(c) c.Name = fromItem.stock.Name AndAlso c.Active).Select(Function(c) c.Id).FirstOrDefault
    End Sub

End Class
