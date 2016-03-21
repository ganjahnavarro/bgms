Public Class GarbageCollector

    Public Shared Sub collect()
        Console.WriteLine("Emptying trash..")
        collect(Constants.CONNECTION_STRING_NAME_MASTER, Constants.CONNECTION_STRING_NAME_SLAVE)
        collect(Constants.CONNECTION_STRING_NAME_SLAVE, Constants.CONNECTION_STRING_NAME_MASTER)
        Console.WriteLine("Trash emptied..")
    End Sub

    Private Shared Sub collect(ByVal fromConnection As String, ByVal toConnection As String)
        Dim actions As List(Of String)

        Using context As New bgmsEntities(fromConnection)
            actions = context.Database.SqlQuery(Of String) _
                ("select o.action from trash o").ToList
        End Using

        If Not IsNothing(actions) AndAlso actions.Count > 0 Then
            Using context As New bgmsEntities(toConnection)
                For Each action In actions
                    If action.Contains(";") Then
                        Dim subActions As String() = action.Split(";")

                        For Each subAction In subActions
                            Console.WriteLine("Executing query: " & subAction)
                            context.Database.ExecuteSqlCommand(subAction)
                        Next
                    Else
                        Console.WriteLine("Executing query: " & action)
                        context.Database.ExecuteSqlCommand(action)
                    End If
                Next
            End Using

            Using context As New bgmsEntities(fromConnection)
                context.Database.ExecuteSqlCommand("delete from trash")
            End Using
        End If
    End Sub

End Class
