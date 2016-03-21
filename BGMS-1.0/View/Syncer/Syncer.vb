Public Class Syncer

    Private Shared masterOn, slaveOn As Boolean
    Public Shared message As String

    Public Shared Sub loadLastSyncDate()
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME_MASTER)
            Dim lastSyncDateString As String = context.Database.SqlQuery(Of String) _
                ("select value from attributes where name = 'LAST_SYNC_DATE'").FirstOrDefault

            If Not String.IsNullOrEmpty(lastSyncDateString) Then
                Date.TryParse(lastSyncDateString, Constants.LAST_SYNC_DATE)
            Else
                Constants.LAST_SYNC_DATE = DateTime.MinValue
            End If
        End Using
    End Sub

    Public Shared Sub syncAll()
        validateConnection()
        loadLastSyncDate()

        If masterOn AndAlso slaveOn Then
            GarbageCollector.collect()
            syncTables()

            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME_MASTER)
                context.Database.ExecuteSqlCommand _
                    ("update attributes set value = '" & DateTime.Now.ToString("MM/dd/yyyy HH:mm") & "' where name = 'LAST_SYNC_DATE'")

                Constants.LAST_SYNC_DATE = DateTime.Now
            End Using
        Else
            Console.WriteLine("Error syncing databases: " & message)
        End If
    End Sub

    Private Shared Sub validateConnection()
        message = String.Empty
        masterOn = False
        slaveOn = False

        Try
            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME_MASTER)
                context.users.FirstOrDefault
                masterOn = True
            End Using
        Catch ex As Exception
            message += "Primary server is not available. "
        End Try

        Try
            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME_SLAVE)
                context.users.FirstOrDefault
                slaveOn = True
            End Using
        Catch ex As Exception
            message += "Primary server is not available. "
        End Try
    End Sub

    Private Shared Sub syncTables()
        Console.WriteLine("Syncing started..")

        AgentSyncer.sync()
        UnitSyncer.sync()
        CategorySyncer.sync()
        SupplierSyncer.sync()
        CustomerSyncer.sync()
        StockSyncer.sync()

        PurchaseOrderSyncer.sync()
        SalesOrderSyncer.sync()
        PurchaseReturnSyncer.sync()
        SalesReturnSyncer.sync()

        CustomerCollectionSyncer.sync()

        Console.WriteLine("Syncing completed..")
    End Sub

End Class
