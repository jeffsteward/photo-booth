Public Class Presentation
    Private mainFileName As String
    Private previousFileName As String
    Private photoCount As Integer = 0
    Private currentPhotoIndex As Integer = 0
    Private firstPass As Boolean = True
    Private photos As New List(Of String)

    Public Sub ShowImage(ByVal newImage As Image, ByVal fileName As String)
        picBox.SizeMode = PictureBoxSizeMode.Zoom
        picBox.Image = newImage
        picBox.Refresh()

        photos.Add(My.Settings.WatchFolder & "\" & fileName)

        'This will be useful when adding the cached version to the thumbnail
        'strip along the right side of the screen
        mainFileName = fileName
        photoCount += 1

        CycleThumbStrip()

        'Update the message
        Label1.Text = String.Format(My.Settings.CounterMessage, photoCount)

        firstPass = False
        previousFileName = mainFileName
    End Sub

    Private Sub CycleThumbStrip()
        If firstPass = False Then
            thumb4.Image = thumb3.Image
            thumb3.Image = thumb2.Image
            thumb2.Image = thumb1.Image

            thumb1.Image = Image.FromFile(My.Settings.ImageCache & "\" & previousFileName)
        End If
    End Sub

    Private Sub ShowPhoto()
        Dim img As Drawing.Image
        img = Image.FromFile(photos.Item(currentPhotoIndex))

        picBox.Image = img
        picBox.Refresh()
    End Sub

    Private Sub Presentation_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Right
                currentPhotoIndex += 1
                If currentPhotoIndex >= photos.Count Then currentPhotoIndex = 0
                ShowPhoto()
            Case Keys.Left
                currentPhotoIndex -= 1
                If currentPhotoIndex < 0 Then currentPhotoIndex = photos.Count - 1
                ShowPhoto()
        End Select
    End Sub
End Class