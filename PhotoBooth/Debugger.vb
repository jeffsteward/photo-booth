Public Class Debugger
    Private WithEvents log As EZLogger
    Private bm As BindingManagerBase
    Private cm As CurrencyManager

    Private Sub log_Updated() Handles log.Updated
        If (cm IsNot Nothing) Then
            cm.Refresh()
            DebugMessages.SelectedIndex = DebugMessages.Items.Count - 1
        End If
    End Sub

    Public Sub BindLogToList(ByRef l As EZLogger)
        log = l

        'Bind the logger to the list box
        DebugMessages.DataSource = log.Log

        'Enable two-way binding
        bm = DebugMessages.BindingContext(log.Log)
        cm = bm
    End Sub
End Class