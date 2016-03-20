Public Class SupplierSyncer

    Public Shared Sub sync()
        Dim updatedCount As Integer = 0

        Dim masterDictionary As New Dictionary(Of String, supplier)
        Dim slaveDictionary As New Dictionary(Of String, supplier)

        Dim unsyncedMaster As New Dictionary(Of String, supplier)
        Dim unsyncedSlave As New Dictionary(Of String, supplier)

        loadAllRecordsByDate(masterDictionary, Constants.CONNECTION_STRING_NAME_MASTER)
        loadAllRecordsByDate(slaveDictionary, Constants.CONNECTION_STRING_NAME_SLAVE)

        Dim keys As New List(Of String)

        For Each pair In masterDictionary
            keys.Add(pair.Value.Name)
        Next

        For Each pair In slaveDictionary
            keys.Add(pair.Value.Name)
        Next

        loadAllRecords(masterDictionary, Constants.CONNECTION_STRING_NAME_MASTER, keys)
        loadAllRecords(slaveDictionary, Constants.CONNECTION_STRING_NAME_SLAVE, keys)

        loadUnsyncedRecords(masterDictionary, slaveDictionary, unsyncedMaster)
        loadUnsyncedRecords(slaveDictionary, masterDictionary, unsyncedSlave)

        updatedCount += saveAndUpdateRecords(unsyncedMaster, unsyncedSlave, Constants.CONNECTION_STRING_NAME_SLAVE)
        updatedCount += saveAndUpdateRecords(unsyncedSlave, unsyncedMaster, Constants.CONNECTION_STRING_NAME_MASTER)

        Console.WriteLine("Updated suppliers: " & updatedCount)
    End Sub

    Private Shared Sub loadAllRecordsByDate(ByRef dictionary As Dictionary(Of String, supplier), ByVal connection As String)
        Using context As New bgmsEntities(connection)
            dictionary = context.suppliers _
                .Where(Function(c) c.Active = True AndAlso c.ModifyDate >= Constants.LAST_SYNC_DATE) _
                .ToDictionary(Function(c) c.Name & "@" & c.ModifyDate.ToString, Function(c) c)
        End Using
    End Sub

    Private Shared Sub loadAllRecords(ByRef dictionary As Dictionary(Of String, supplier),
        ByVal connection As String, ByVal keys As List(Of String))

        Using context As New bgmsEntities(connection)
            dictionary = context.suppliers _
                .Where(Function(c) c.Active = True AndAlso keys.Contains(c.Name)) _
                .ToDictionary(Function(c) c.Name & "@" & c.ModifyDate.ToString, Function(c) c)
        End Using
    End Sub

    Private Shared Sub loadUnsyncedRecords(ByVal outer As Dictionary(Of String, supplier),
        ByVal inner As Dictionary(Of String, supplier), ByRef unsynced As Dictionary(Of String, supplier))
        For Each pair In outer
            If inner.ContainsKey(pair.Key) = False Then
                unsynced.Add(pair.Key, pair.Value)
            End If
        Next
    End Sub

    Private Shared Function saveAndUpdateRecords(ByRef outer As Dictionary(Of String, supplier),
        ByRef inner As Dictionary(Of String, supplier), ByVal connection As String) As Integer

        Dim updatedCount As Integer = 0

        Using context As New bgmsEntities(connection)
            For Each pair In outer
                Dim found As Boolean = False
                Dim newest As Boolean = False

                For Each innerPair In inner
                    'check if update or new
                    If pair.Value.Name = innerPair.Value.Name Then
                        found = True

                        'check if outer is newer than inner
                        If pair.Value.ModifyDate > innerPair.Value.ModifyDate Then
                            newest = True
                        End If
                    End If
                Next

                If found = False Then
                    'new record
                    Dim newObject As New supplier
                    copyObjectValues(pair.Value, newObject, context)
                    context.suppliers.Add(newObject)
                    updatedCount += 1
                Else
                    If newest Then
                        'update record
                        Dim updateObject = context.suppliers.Where(Function(c) c.Name = pair.Value.Name).FirstOrDefault

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

    Private Shared Sub copyObjectValues(ByVal fromObject As supplier, ByRef toObject As supplier, ByRef context As bgmsEntities)
        toObject.Active = fromObject.Active
        toObject.ModifyBy = fromObject.ModifyBy
        toObject.ModifyDate = fromObject.ModifyDate
        toObject.Name = fromObject.Name
        toObject.Address = fromObject.Address
        toObject.Contact = fromObject.Contact
        toObject.Tin = fromObject.Tin
    End Sub

End Class
