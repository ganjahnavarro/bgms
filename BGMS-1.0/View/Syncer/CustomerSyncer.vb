﻿Public Class CustomerSyncer

    Public Shared Sub sync()
        Dim updatedCount As Integer = 0

        Dim masterDictionary As New Dictionary(Of String, customer)
        Dim slaveDictionary As New Dictionary(Of String, customer)

        Dim unsyncedMaster As New Dictionary(Of String, customer)
        Dim unsyncedSlave As New Dictionary(Of String, customer)

        loadAllRecords(masterDictionary, Constants.CONNECTION_STRING_NAME_MASTER)
        loadAllRecords(slaveDictionary, Constants.CONNECTION_STRING_NAME_SLAVE)

        loadUnsyncedRecords(masterDictionary, slaveDictionary, unsyncedMaster)
        loadUnsyncedRecords(slaveDictionary, masterDictionary, unsyncedSlave)

        updatedCount += saveAndUpdateRecords(unsyncedMaster, unsyncedSlave, Constants.CONNECTION_STRING_NAME_SLAVE)
        updatedCount += saveAndUpdateRecords(unsyncedSlave, unsyncedMaster, Constants.CONNECTION_STRING_NAME_MASTER)

        Console.WriteLine("Updated customers: " & updatedCount)
    End Sub

    Private Shared Sub loadAllRecords(ByRef dictionary As Dictionary(Of String, customer), ByVal connection As String)
        Using context As New bgmsEntities(connection)
            dictionary = context.customers _
                .Include("Agent") _
                .Where(Function(c) c.Active = True) _
                .ToDictionary(Function(c) c.Name & "@" & c.ModifyDate.ToString, Function(c) c)
        End Using
    End Sub

    Private Shared Sub loadUnsyncedRecords(ByVal outer As Dictionary(Of String, customer),
        ByVal inner As Dictionary(Of String, customer), ByRef unsynced As Dictionary(Of String, customer))
        For Each pair In outer
            If inner.ContainsKey(pair.Key) = False Then
                unsynced.Add(pair.Key, pair.Value)
            End If
        Next
    End Sub

    Private Shared Function saveAndUpdateRecords(ByRef outer As Dictionary(Of String, customer),
        ByRef inner As Dictionary(Of String, customer), ByVal connection As String) As Integer

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
                    Dim newObject As New customer
                    copyObjectValues(pair.Value, newObject, context)
                    context.customers.Add(newObject)
                    updatedCount += 1
                Else
                    If newest Then
                        'update record
                        Dim updateObject = context.customers.Where(Function(c) c.Name = pair.Value.Name).FirstOrDefault

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

    Private Shared Sub copyObjectValues(ByVal fromObject As customer, ByRef toObject As customer, ByRef context As bgmsEntities)
        toObject.Active = fromObject.Active
        toObject.Address = fromObject.Address
        toObject.Contact = fromObject.Contact
        toObject.ModifyBy = fromObject.ModifyBy
        toObject.ModifyDate = fromObject.ModifyDate
        toObject.Name = fromObject.Name
        toObject.Fax = fromObject.Fax
        toObject.Commission = fromObject.Commission
        toObject.Tin = fromObject.Tin

        toObject.AgentId = context.agents.Where(Function(c) c.Name = fromObject.agent.Name And c.Active).Select(Function(c) c.Id).FirstOrDefault
    End Sub

End Class
