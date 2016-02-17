Public Class EtcHelp

    Private Sub EtcHelp_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        Controller.currentForm = Controller.previousForm
    End Sub

End Class