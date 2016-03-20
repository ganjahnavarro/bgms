Public Class CustomerCollectionSyncer

    Public Shared Sub sync()
        Dim updatedCount As Integer = 0

        Dim masterDictionary As New Dictionary(Of String, customercollection)
        Dim slaveDictionary As New Dictionary(Of String, customercollection)

        Dim unsyncedMaster As New Dictionary(Of String, customercollection)
        Dim unsyncedSlave As New Dictionary(Of String, customercollection)

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

        Console.WriteLine("Updated customer collections: " & updatedCount)
    End Sub

    Private Shared Sub loadAllRecordsByDate(ByRef dictionary As Dictionary(Of String, customercollection), ByVal connection As String)
        Using context As New bgmsEntities(connection)
            dictionary = context.customercollections _
                .Where(Function(c) c.ModifyDate >= Constants.LAST_SYNC_DATE) _
                .ToDictionary(Function(c) c.DocumentNo & "@" & c.ModifyDate.ToString, Function(c) c)
        End Using
    End Sub

    Private Shared Sub loadAllRecords(ByRef dictionary As Dictionary(Of String, customercollection),
        ByVal connection As String, ByVal keys As List(Of String))

        Using context As New bgmsEntities(connection)
            dictionary = context.customercollections _
                .Include("Customer").Include("CollectionOrderItems.SalesOrder") _
                .Include("CollectionCheckItems") _
                .Where(Function(c) keys.Contains(c.DocumentNo)) _
                .ToDictionary(Function(c) c.DocumentNo & "@" & c.ModifyDate.ToString, Function(c) c)
        End Using
    End Sub

    Private Shared Sub loadUnsyncedRecords(ByVal outer As Dictionary(Of String, customercollection),
        ByVal inner As Dictionary(Of String, customercollection), ByRef unsynced As Dictionary(Of String, customercollection))
        For Each pair In outer
            If inner.ContainsKey(pair.Key) = False Then
                unsynced.Add(pair.Key, pair.Value)
            End If
        Next
    End Sub

    Private Shared Function saveAndUpdateRecords(ByRef outer As Dictionary(Of String, customercollection),
        ByRef inner As Dictionary(Of String, customercollection), ByVal connection As String) As Integer

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
                    Dim newObject As New customercollection
                    copyObjectValues(pair.Value, newObject, context)
                    context.customercollections.Add(newObject)
                    updatedCount += 1
                Else
                    If newest Then
                        'update record
                        Dim updateObject = context.customercollections.Where(Function(c) c.DocumentNo = pair.Value.DocumentNo).FirstOrDefault

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

    Private Shared Sub copyObjectValues(ByVal fromObject As customercollection, ByRef toObject As customercollection, ByRef context As bgmsEntities)
        toObject.ModifyBy = fromObject.ModifyBy
        toObject.ModifyDate = fromObject.ModifyDate
        toObject.Date = fromObject.Date
        toObject.PostedDate = fromObject.PostedDate
        toObject.Remarks = fromObject.Remarks
        toObject.TotalPaid = fromObject.TotalPaid
        toObject.DocumentNo = fromObject.DocumentNo
        toObject.Bank = fromObject.Bank
        toObject.TotalCheck = fromObject.TotalCheck

        context.collectioncheckitems.RemoveRange(toObject.collectioncheckitems)
        context.collectionorderitems.RemoveRange(toObject.collectionorderitems)

        For Each item In fromObject.collectioncheckitems
            Dim newItem As New collectioncheckitem
            copyCheckItemValues(item, newItem, context)
            toObject.collectioncheckitems.Add(newItem)
        Next

        For Each item In fromObject.collectionorderitems
            Dim newItem As New collectionorderitem
            copyOrderItemValues(item, newItem, context)
            toObject.collectionorderitems.Add(newItem)
        Next

        toObject.customerId = context.customers.Where(Function(c) c.Name = fromObject.customer.Name AndAlso c.Active).Select(Function(c) c.Id).FirstOrDefault
    End Sub

    Private Shared Sub copyCheckItemValues(ByVal fromItem As collectioncheckitem, ByRef toItem As collectioncheckitem, ByRef context As bgmsEntities)
        toItem.Amount = fromItem.Amount
        toItem.Date = fromItem.Date
        toItem.DocumentNo = fromItem.Date
    End Sub

    Private Shared Sub copyOrderItemValues(ByVal fromItem As collectionorderitem, ByRef toItem As collectionorderitem, ByRef context As bgmsEntities)
        toItem.Amount = fromItem.Amount
        toItem.BalanceGot = fromItem.BalanceGot
        toItem.salesorderId = context.salesorders.Where(Function(c) c.DocumentNo = fromItem.salesorder.DocumentNo).Select(Function(c) c.Id).FirstOrDefault
    End Sub

End Class
