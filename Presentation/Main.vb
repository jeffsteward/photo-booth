Module Main
    Public Sub Main()
        Dim theOptions As New Options
        If theOptions.ShowDialog() = DialogResult.Cancel Then
            Exit Sub
        End If

        Randomize()

        Dim frm As New RenderForm
        If frm.InitializeGraphics() Then
            frm.Show()

            Do While frm.Created
                frm.Render()
                Application.DoEvents()
            Loop
        End If
    End Sub
End Module
