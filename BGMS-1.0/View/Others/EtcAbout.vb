Public Class EtcAbout

    Private Sub EtcAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Syncer.loadLastSyncDate()
        syncLabel.Text = "Last sync date: " & Constants.LAST_SYNC_DATE
    End Sub

    Private Sub syncButton_Click(sender As Object, e As EventArgs) Handles syncButton.Click
        Syncer.syncAll()
    End Sub

End Class