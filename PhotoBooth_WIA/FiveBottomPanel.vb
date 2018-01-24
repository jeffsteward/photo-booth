Public Class FiveBottomPanel
    Private mainFileName As String
    Private previousFileName As String
    Private photoCount As Integer = 0
    Private firstPass As Boolean = True

    Public Sub ShowImage(ByVal newImage As Image, ByVal fileName As String)
        picBox.SizeMode = PictureBoxSizeMode.Zoom
        picBox.Image = newImage
        picBox.Refresh()

        'This will be useful when adding the cached version to the thumbnail
        'strip along the right side of the screen
        mainFileName = fileName
        photoCount += 1

        CycleThumbStrip()

        'Update the message
        lblCounterMessage.Text = String.Format(My.Settings.CounterMessage, photoCount)

        firstPass = False
        previousFileName = mainFileName
    End Sub

    Private Sub CycleThumbStrip()
        If firstPass = False Then
            Try
                thumb5.Image = thumb4.Image
                thumb4.Image = thumb3.Image
                thumb3.Image = thumb2.Image
                thumb2.Image = thumb1.Image

                thumb1.Image = Image.FromFile(My.Settings.ImageCache & "\" & previousFileName)

            Catch ex As Exception
                'TODO: log the errors
            End Try
        End If
    End Sub
End Class
