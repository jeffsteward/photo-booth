Imports System.IO.Ports

Public Class Main
    Private Enum PhotoBoothState
        Unknown = 0     'The state when the application is first started
        Ready           'The booth is ready to take another photo
        Working         'The booth is taking/processing a photo
        Stopped         'The booth is stopped (no folder watching, the COM port is closed)
    End Enum

    Private BoothState As PhotoBoothState = PhotoBoothState.Unknown
    Private photos As Collection
    Private input As SerialPort
    Private log As EZLogger
    Private logWindow As Debugger
    Private messagePanel As FaderPanel

    Private Delegate Sub TakePhotoDelegate()

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.D
                ToggleDebugWindow()
            Case Keys.S
                StartPhotoBooth()
            Case Keys.T
                TakePhoto()
            Case Keys.U
                ShowUsageMenu()
            Case Keys.O
                Options.ShowDialog()
                LoadSettings()
            Case Keys.R
                input.Write("R")
            Case Keys.G
                input.Write("G")
            Case Keys.Escape
                ExitPhotoBooth()
            Case Keys.PageDown
                ToggleWindowMode()
        End Select
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Initialize the log 
        log = New EZLogger(My.Application.Info.DirectoryPath & "\log.txt", True, EZLogger.Level.All, True)
        log.StartLog()

        'Initialize the in-memory list of photos
        photos = New Collection()

        'Initialize the file watcher
        log.Info("Initializing the file watcher")
        fsWatcher.EnableRaisingEvents = False
        fsWatcher.NotifyFilter = (IO.NotifyFilters.CreationTime Or IO.NotifyFilters.LastWrite)
        fsWatcher.Filter = "*.jpg"

        'Initialize the serial port
        log.Info("Initializing the serial port")
        input = New SerialPort
        AddHandler input.DataReceived, AddressOf DataReceived

        'Initialize the message panel
        log.Info("Initializing the message panel")
        messagePanel = New FaderPanel
        messagePanel.Dock = DockStyle.Fill
        messagePanel.BackColor = Color.Black
        messagePanel.ForeColor = Color.White
        messageDock.Visible = False
        messageDock.Controls.Add(messagePanel)

        'Initialize the informational messages
        lblCounterMessage.Text = String.Empty

        'Initialize the photo booth settings
        'TODO: move this out of here and only load the settings when the booth is started
        LoadSettings()

        'Show the usage screen
        Me.Show()   'Be sure the main form is visible before displaying the usage screen
        ShowUsageMenu()

        'TODO: If this is the first time launching the app, show the options screen
    End Sub

    Private Sub StartPhotoBooth()
        'Put the photo booth in a ready state
        log.Info("Starting the photo booth")

        'Initialize the photos and cache folder
        'Make sure the folder exists and clear it if there are files in it
        Dim fileCounter As Collections.ObjectModel.ReadOnlyCollection(Of String)

        'Inspect the cache folder
        fileCounter = My.Computer.FileSystem.GetFiles( _
                        My.Settings.ImageCache, FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
        If fileCounter.Count <> 0 Then
            For Each file As String In fileCounter
                My.Computer.FileSystem.DeleteFile(file)
            Next
        End If

        'Inspect the photos folder
        fileCounter = My.Computer.FileSystem.GetFiles( _
                        My.Settings.WatchFolder, FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
        If fileCounter.Count <> 0 Then
            If MessageBox.Show("There are files in the photos folder. Do you want to empty the folder now?", _
                                "Clear photo folders", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                For Each file As String In fileCounter
                    My.Computer.FileSystem.DeleteFile(file)
                Next
            End If
        End If

        'TODO: Verify Cam2COM is running
        'Process.GetProcessesByName("Cam2ComM")

        OpenSerialPort()
        fsWatcher.EnableRaisingEvents = True

        'Toggle the stop and go LEDs on the input device
        input.Write("r")
        input.Write("G")

        BoothState = PhotoBoothState.Ready
    End Sub

    Private Sub ExitPhotoBooth()
        'Shut down the photo booth
        log.Info("Shutting down the application")

        'Close the debugger window
        If logWindow IsNot Nothing Then
            logWindow.Close()
            logWindow = Nothing
        End If

        'Stop watching the photo folder
        log.Info("Shutting down the file watcher")
        fsWatcher.EnableRaisingEvents = False

        'Shut down the serial port for the trigger
        CloseSerialPort()

        'Stop logging events
        log.StopLog()

        Application.Exit()
    End Sub

    Private Sub LoadSettings()
        log.Info("Loading settings")
        messagePanel.DisplayDuration = My.Settings.MessageDuration
        messagePanel.Font = My.Settings.MessageFont
        fsWatcher.Path = My.Settings.WatchFolder
    End Sub

    Private Sub ToggleDebugWindow()
        If logWindow Is Nothing Then
            logWindow = New Debugger
            logWindow.BindLogToList(log)
        End If
        logWindow.Visible = Not logWindow.Visible
    End Sub

    Private Sub ToggleWindowMode()
        If Me.WindowState <> FormWindowState.Maximized Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
            Windows.Forms.Cursor.Hide()
        Else
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            Me.WindowState = FormWindowState.Normal
            Windows.Forms.Cursor.Show()
        End If
    End Sub

    Private Sub ShowUsageMenu()
        Using u As New Usage
            u.ShowDialog(Me)
        End Using
    End Sub

    Private Sub CloseSerialPort()
        log.Info("Closing the serial port")
        If input.IsOpen Then
            input.Close()
        End If
    End Sub

    Private Sub OpenSerialPort()
        'Set up serial communication with the Arduino
        log.Info("Opening the serial port")
        If input IsNot Nothing Then
            If input.IsOpen Then
                input.Close()
            End If
        End If

        Try
            input.PortName = My.Settings.COMPort
            input.BaudRate = 9600
            input.Parity = Parity.None
            input.DataBits = 8
            input.Open()
        Catch ex As Exception
            log.Errors(ex.Message)
        End Try
    End Sub

    Private Sub ShowNewPicture(ByVal FullPath As String)
        log.Info("Show new photo")
        messageDock.Visible = False
        messagePanel.Visible = False
        Try
            With picBox
                .SizeMode = PictureBoxSizeMode.Zoom
                .Load(FullPath)
                .Refresh()

                'Save a scaled down image in the photo cache
                AddPhotoToCache(.Image, FullPath)
            End With
        Catch ex As Exception
            log.Errors(ex.Message)
        End Try
    End Sub

    Private Sub CycleFilmStrip()
        log.Info("Cycling the filmstrip")

        If photos.Count > 1 Then
            try
                thumb5.Image = thumb4.Image
                thumb4.Image = thumb3.Image
                thumb3.Image = thumb2.Image
                thumb2.Image = thumb1.Image

                thumb1.Image = Image.FromFile(My.Settings.ImageCache & "\" & photos(photos.Count - 1).ToString())

                tlpFilmstrip.Visible = True
            Catch ex As Exception
                log.Errors(ex.Message)
            End Try
        End If
    End Sub

    Private Sub AddPhotoToCache(ByVal NewPhoto As Drawing.Image, ByVal FullPath As String)
        log.Info("Creating cached version")

        Try
            'Dim f As New IO.FileInfo(IO.Path.GetTempFileName())
            Dim f As New IO.FileInfo(FullPath)
            Dim cacheDestination As New Bitmap(CInt(NewPhoto.Size.Width * 0.35), CInt(NewPhoto.Size.Height * 0.35))
            Dim gPhoto As Graphics = Graphics.FromImage(cacheDestination)
            gPhoto.DrawImage(NewPhoto, _
                    New Rectangle(0, 0, CInt(NewPhoto.Size.Width * 0.35), CInt(NewPhoto.Height * 0.35)), _
                    New Rectangle(0, 0, NewPhoto.Width, NewPhoto.Height), _
                    GraphicsUnit.Pixel)
            cacheDestination.Save(My.Settings.ImageCache & "\" & f.Name.Replace(f.Extension, ".jpg"), _
                    Imaging.ImageFormat.Jpeg)
            gPhoto.Dispose()
            cacheDestination.Dispose()
        Catch ex As Exception
            log.Errors(ex.Message)
        End Try
    End Sub

    Private Sub TakePhoto()
        If BoothState = PhotoBoothState.Ready Then
            BoothState = PhotoBoothState.Working
            log.Info("Take photo")

            'Toggle the stop and go LEDs on the input device
            input.Write("g")
            input.Write("R")

            'Clear the display
            picBox.Image = Nothing
            lblCounterMessage.Text = String.Empty
            tlpFilmstrip.Visible = False

            'Give the application some time to fully clear the image from the screen before flashing the wait message
            My.Application.DoEvents()

            'Display a "Get Ready" message
            messageDock.Visible = True
            messagePanel.FlashMessage(My.Settings.WaitMessage)

            'Call the camera control application
            Shell(My.Settings.CameraControlEXE)
        End If
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub

    Private Sub fsWatcher_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fsWatcher.Changed, fsWatcher.Created
        If Not photos.Contains(e.Name) Then
            log.Info("New photo found in the watched folder")
            photos.Add(e.Name, e.Name)

            ShowNewPicture(My.Settings.WatchFolder & "\" & e.Name)

            CycleFilmStrip()

            'Update the counter message
            lblCounterMessage.Text = String.Format(My.Settings.CounterMessage, photos.Count)

            My.Application.DoEvents()

            'Toggle the stop and go LEDs on the input device
            input.Write("r")
            input.Write("G")

            'The booth is ready to take more photos
            BoothState = PhotoBoothState.Ready
        End If
    End Sub

    Private Sub DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        Dim newResponse As Integer = input.ReadChar
        Select Case newResponse
            Case 66
                log.Info("Button pushed")
                Me.Invoke(New TakePhotoDelegate(AddressOf TakePhoto), New Object() {})
            Case 80
                log.Info("Pedal pushed")
                Me.Invoke(New TakePhotoDelegate(AddressOf TakePhoto), New Object() {})
            Case Else
                log.Info("Board responsed with " & Chr(newResponse))
        End Select
    End Sub
End Class
