Imports MySql.Data.MySqlClient

Public Class Syncer

    Private Shared masterOn, slaveOn As Boolean
    Public Shared message As String

    Public Shared Sub loadLastSyncDate()
        Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME)
            Dim lastSyncDateString As String = context.Database.SqlQuery(Of String) _
                ("select value from attributes where name = 'LAST_SYNC_DATE'").FirstOrDefault

            If Not String.IsNullOrEmpty(lastSyncDateString) Then
                Date.TryParse(lastSyncDateString, Constants.LAST_SYNC_DATE)
            Else
                Constants.LAST_SYNC_DATE = DateTime.MinValue
            End If
        End Using
    End Sub

    Public Shared Function syncAll(ByVal manual As Boolean) As Boolean
        validateConnection()

        If masterOn AndAlso slaveOn Then
            loadLastSyncDate()
            GarbageCollector.collect()
            syncTables()

            Using context As New bgmsEntities(Constants.CONNECTION_STRING_NAME_MASTER)
                context.Database.ExecuteSqlCommand _
                    ("update attributes set value = '" & DateTime.Now.ToString("MM/dd/yyyy HH:mm") & "' where name = 'LAST_SYNC_DATE'")

                Constants.LAST_SYNC_DATE = DateTime.Now
            End Using

            Return True
        Else
            If manual Then
                MsgBox("Sync failed. Not connected to primary and/or secondary server. Please check connection and try again.")
            End If

            Console.WriteLine("Error syncing databases: " & message)
            Return False
        End If
    End Function

    Private Shared Sub validateConnection()
        message = String.Empty
        masterOn = False
        slaveOn = False

        Try
            Dim masterConnection As New MySqlConnection()
            masterConnection.ConnectionString = "server=192.168.1.125;user id=root;database=bgms"
            masterConnection.Open()
            masterOn = True
            masterConnection.Close()
        Catch ex As Exception
            message += "Primary server is not available. "
        End Try

        Try
            Dim slaveConnection As New MySqlConnection()
            slaveConnection.ConnectionString = "server=192.168.1.150;user id=root;database=bgms"
            slaveConnection.Open()
            slaveOn = True
            slaveConnection.Close()
        Catch ex As Exception
            message += "Secondary server is not available. "
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
        SupplierPaymentSyncer.sync()

        Console.WriteLine("Syncing completed..")
    End Sub

End Class
