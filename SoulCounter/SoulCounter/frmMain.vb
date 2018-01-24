Public Class frmMain

    Dim souls As Collection

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Application.Exit()
            Case Keys.PageDown
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
                Me.WindowState = FormWindowState.Maximized
            Case Keys.PageUp
                Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
                Me.WindowState = FormWindowState.Normal
        End Select
    End Sub


    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        souls = New Collection()
        Label1.Text = "Waiting to consume a few souls."
        flpPics.WrapContents = True

        FileSystemWatcher1.EnableRaisingEvents = True
        FileSystemWatcher1.Path = "g:\"

        TestLoad()
    End Sub

    Private Sub TestLoad()
        Dim file As String
        For Each file In IO.Directory.GetFiles("G:\test")
            ShowNewPicture(file)
        Next
    End Sub

    Private Sub ShowNewPicture(ByVal FullPath As String)
        Try
            Dim picBox As PictureBox
            picBox = New PictureBox

            With picBox
                .SizeMode = PictureBoxSizeMode.Zoom
                .Load(FullPath)
                .Height = 100
                .Width = 100
            End With
            flpPics.Controls.Add(picBox)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FileSystemWatcher1_Created(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles FileSystemWatcher1.Created
        If Not souls.Contains(e.Name) Then
            'souls.Add(e.Name, e.Name)

            'Label1.Text = souls.Count.ToString & " soul" & IIf(souls.Count = 1, "", "s") & " taken."

            'IO.File.Copy(e.FullPath, "\\robbie\drop\halloween_2\" & e.Name, True)
            'ShowNewPicture("\\robbie\drop\halloween_2\" & e.Name)

            'My.Application.DoEvents()
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        Label1.BackColor = Color.Transparent
    End Sub
End Class
