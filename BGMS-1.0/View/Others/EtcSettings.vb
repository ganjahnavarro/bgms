Public Class EtcSettings

    Private counters As New Dictionary(Of String, counter)

    Private Sub EtcSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadValues()
    End Sub

    Private Sub showUpdateButtons(ByVal show As Boolean)
        btnCancel.Visible = show
        btnCheck.Visible = show
        btnEdit.Visible = Not show
    End Sub

    Private Sub loadValues()
        Using context As New bgmsEntities
            counters = context.counters.ToDictionary(Function(c) c.Prefix, Function(c) c)
        End Using

        counterSODR.Text = If(counters.ContainsKey("DR"), counters.Item("DR").Count, "Error getting value.")
        counterSOPO.Text = If(counters.ContainsKey("PO"), counters.Item("PO").Count, "Error getting value.")
        counterSORF.Text = If(counters.ContainsKey("RF"), counters.Item("RF").Count, "Error getting value.")
        counterSOSI.Text = If(counters.ContainsKey("SI"), counters.Item("SI").Count, "Error getting value.")
        counterPR.Text = If(counters.ContainsKey("PR"), counters.Item("PR").Count, "Error getting value.")
        counterSR.Text = If(counters.ContainsKey("SR"), counters.Item("SR").Count, "Error getting value.")
        counterCC.Text = If(counters.ContainsKey("CL"), counters.Item("CL").Count, "Error getting value.")
        counterSP.Text = If(counters.ContainsKey("PY"), counters.Item("PY").Count, "Error getting value.")
    End Sub

    Private Sub enableInputs(ByVal enable As Boolean)
        counterSODR.Enabled = enable
        counterSOPO.Enabled = enable
        counterSORF.Enabled = enable
        counterSOSI.Enabled = enable
        counterPR.Enabled = enable
        counterSR.Enabled = enable
        counterCC.Enabled = enable
        counterSP.Enabled = enable
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        enableInputs(True)
        showUpdateButtons(True)
    End Sub

    Private Sub reset() Handles btnCancel.Click
        enableInputs(False)
        showUpdateButtons(False)
    End Sub

    Private Sub btnCheck_Click(sender As Object, e As EventArgs) Handles btnCheck.Click
        Dim errorMsg As String = getErrorMessage()

        If String.IsNullOrEmpty(errorMsg) Then
            saveChanges()
        Else
            Util.notifyError(errorMsg)
        End If
    End Sub

    Private Sub saveChanges()
        Using context As New bgmsEntities
            counters = context.counters.ToDictionary(Function(c) c.Prefix, Function(c) c)

            counters.Item("DR").Count = counterSODR.Text
            counters.Item("RF").Count = counterSORF.Text
            counters.Item("SI").Count = counterSOSI.Text
            counters.Item("PO").Count = counterSOPO.Text

            counters.Item("PR").Count = counterPR.Text
            counters.Item("SR").Count = counterSR.Text
            counters.Item("CL").Count = counterCC.Text
            counters.Item("PY").Count = counterSP.Text

            context.SaveChanges()
        End Using

        Util.notifyInfo("Settings updated.")
        reset()
    End Sub

    Private Function getErrorMessage() As String
        Dim newCount As Integer

        Integer.TryParse(counterSODR.Text, newCount)
        If newCount < counters.Item("DR").Count Then
            Return "Invalid Sales Order (DR) count."
        End If

        Integer.TryParse(counterSORF.Text, newCount)
        If newCount < counters.Item("RF").Count Then
            Return "Invalid Sales Order (RF) count."
        End If

        Integer.TryParse(counterSOSI.Text, newCount)
        If newCount < counters.Item("SI").Count Then
            Return "Invalid Sales Order (SI) count."
        End If

        Integer.TryParse(counterSOPO.Text, newCount)
        If newCount < counters.Item("PO").Count Then
            Return "Invalid Sales Order (PO) count."
        End If

        Integer.TryParse(counterPR.Text, newCount)
        If newCount < counters.Item("PR").Count Then
            Return "Invalid Purchase Return count."
        End If

        Integer.TryParse(counterSR.Text, newCount)
        If newCount < counters.Item("SR").Count Then
            Return "Invalid Sales Return count."
        End If

        Integer.TryParse(counterCC.Text, newCount)
        If newCount < counters.Item("CL").Count Then
            Return "Invalid Customer Collection count."
        End If

        Integer.TryParse(counterSP.Text, newCount)
        If newCount < counters.Item("PY").Count Then
            Return "Invalid Payment to Supplier count."
        End If

        Return Nothing
    End Function

    Private Sub EtcSettings_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Util.notifyDisplay(False)
    End Sub

End Class