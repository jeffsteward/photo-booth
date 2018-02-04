Imports System.IO.Ports
Imports WIA

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
    Private previewWindow As Presentation
    Private WithEvents messagePanel As CountdownPanel
    Private WithEvents dm As DeviceManager
    Private camera As Device
    Private photoCount As Integer = 0

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
            Case Keys.P
                TogglePresentationWindow()
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
        'log.Info("Initializing the file watcher")
        'fsWatcher.EnableRaisingEvents = False
        'fsWatcher.NotifyFilter = (IO.NotifyFilters.CreationTime Or IO.NotifyFilters.LastWrite)
        'fsWatcher.Filter = "*.jpg"

        'Initialize the serial port
        log.Info("Initializing the serial port")
        input = New SerialPort
        AddHandler input.DataReceived, AddressOf DataReceived

        'Initialize the message panel
        log.Info("Initializing the message panel")
        messagePanel = New CountdownPanel
        messagePanel.Dock = DockStyle.Fill
        messagePanel.BackColor = Color.Black
        messagePanel.ForeColor = Color.White
        messagePanel.Duration = 3
        messageDock.Visible = False
        messageDock.Controls.Add(messagePanel)

        'Initialize the photo booth settings
        'TODO: move this out of here and only load the settings when the booth is started
        LoadSettings()

        'Show the usage screen
        Me.Show()   'Be sure the main form is visible before displaying the usage screen
        ShowUsageMenu()

        'TODO: If this is the first time launching the app, show the options screen
    End Sub

    Private Sub StartPhotoBooth()
        'TODO: Put the photo booth in a ready state
        log.Info("Starting the photo booth")

        'Initialize the photos and cache folder
        'Make sure the folder exists and clear it if there are files in it
        Dim fileCounter As Collections.ObjectModel.ReadOnlyCollection(Of String)

        'Inspect the cache folder
        fileCounter = My.Computer.FileSystem.GetFiles(
                        My.Settings.ImageCache, FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
        If fileCounter.Count <> 0 Then
            For Each file As String In fileCounter
                My.Computer.FileSystem.DeleteFile(file)
            Next
        End If

        'Inspect the photos folder
        fileCounter = My.Computer.FileSystem.GetFiles(
                        My.Settings.WatchFolder, FileIO.SearchOption.SearchTopLevelOnly, "*.jpg")
        If fileCounter.Count <> 0 Then
            If MessageBox.Show("There are files in the photos folder. Do you want to empty the folder now?",
                                "Clear photo folders", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                For Each file As String In fileCounter
                    My.Computer.FileSystem.DeleteFile(file)
                Next
            End If
        End If

        'Initialize the preview window
        previewWindow = New Presentation
        previewWindow.Show()

        'Find the selected camera
        dm = New DeviceManager
        For Each dvi As DeviceInfo In dm.DeviceInfos
            If dvi.DeviceID = My.Settings.CameraDeviceID Then
                camera = dvi.Connect
                Exit For
            End If
        Next
        dm.RegisterEvent(EventID.wiaEventItemCreated, camera.DeviceID)

        OpenSerialPort()

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

        'Shut down the serial port for the trigger
        CloseSerialPort()

        'Stop logging events
        log.StopLog()

        Application.Exit()
    End Sub

    Private Sub LoadSettings()
        log.Info("Load settings")
        'messagePanel.DisplayDuration = My.Settings.MessageDuration
        messagePanel.Font = My.Settings.MessageFont
        'fsWatcher.Path = My.Settings.WatchFolder
    End Sub

    Private Sub ToggleDebugWindow()
        If logWindow Is Nothing Then
            logWindow = New Debugger
            logWindow.BindLogToList(log)
        End If
        logWindow.Visible = Not logWindow.Visible
    End Sub

    Private Sub TogglePresentationWindow()
        If previewWindow Is Nothing Then
            previewWindow = New Presentation
        End If
        previewWindow.Visible = Not previewWindow.Visible
    End Sub

    Private Sub ToggleWindowMode()
        If Me.WindowState <> FormWindowState.Maximized Then
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized

            previewWindow.FormBorderStyle = Windows.Forms.FormBorderStyle.None
            previewWindow.WindowState = FormWindowState.Maximized

            'Windows.Forms.Cursor.Hide()
        Else
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            Me.WindowState = FormWindowState.Normal

            previewWindow.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedSingle
            previewWindow.WindowState = FormWindowState.Normal

            'Windows.Forms.Cursor.Show()
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
            'log the message
            log.Errors("OpenSerialPort: " & ex.Message)
        End Try
    End Sub

    Private Sub ShowNewPicture(ByVal FullPath As String)
        log.Info("Show new photo")
        messageDock.Visible = False
        messagePanel.Visible = False
        Try
            Dim img As Drawing.Image
            img = Image.FromFile(FullPath)

            'Save a scaled down image in the photo cache
            AddPhotoToCache(img)

            'Show the new image on the presentation panel
            FiveBottomPanel1.ShowImage(img, photos(photos.Count).ToString)
            FiveBottomPanel1.Visible = True

            previewWindow.ShowImage(img, photos(photos.Count).ToString)

        Catch ex As Exception
            'log the message
            log.Errors("ShowNewPicture: " & ex.Message)
        End Try
    End Sub

    Private Sub AddPhotoToCache(ByVal NewPhoto As Drawing.Image)
        log.Info("Creating cached version")

        Try
            Const SCALE_FACTOR As Single = 0.125F
            Dim cacheDestination As New Bitmap(CInt(NewPhoto.Size.Width * SCALE_FACTOR), CInt(NewPhoto.Size.Height * SCALE_FACTOR))
            Dim gPhoto As Graphics = Graphics.FromImage(cacheDestination)
            gPhoto.DrawImage(NewPhoto, _
                    New Rectangle(0, 0, CInt(NewPhoto.Size.Width * SCALE_FACTOR), CInt(NewPhoto.Height * SCALE_FACTOR)), _
                    New Rectangle(0, 0, NewPhoto.Width, NewPhoto.Height), _
                    GraphicsUnit.Pixel)
            cacheDestination.Save(My.Settings.ImageCache & "\" & photoCount & ".jpg", _
                    Imaging.ImageFormat.Jpeg)
            gPhoto.Dispose()
            cacheDestination.Dispose()
        Catch ex As Exception
            log.Errors("AddPhotoToCache: " & ex.Message)
        End Try
    End Sub

    Private Sub TakePhoto()
        If BoothState = PhotoBoothState.Ready Then
            BoothState = PhotoBoothState.Working
            log.Info("Take photo")

            'Toggle the stop and go LEDs on the input device
            input.Write("g")
            input.Write("R")

            'Clear the current image
            FiveBottomPanel1.Visible = False
            Application.DoEvents()

            'Display a "Get Ready" message
            messageDock.Visible = True
            messagePanel.Start()

            'Call the camera control application
            'Shell(My.Settings.CameraControlEXE)
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

            My.Application.DoEvents()

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

    Private Sub dm_OnEvent(ByVal EventID As String, ByVal DeviceID As String, ByVal ItemID As String) Handles dm.OnEvent
        Select Case EventID
            Case WIA.EventID.wiaEventItemCreated
                'Retrieve the new photo from the camera
                Dim itm As WIA.Item = camera.GetItem(ItemID)
                Dim img As WIA.ImageFile = itm.Transfer(WIA.FormatID.wiaFormatJPEG)
                img.SaveFile(My.Settings.WatchFolder & "\" & photoCount & ".jpg")

                'Keep track of the photos taken
                photos.Add(photoCount & ".jpg", photoCount & ".jpg")

                'Display the new photo on screen
                ShowNewPicture(My.Settings.WatchFolder & "\" & photoCount & ".jpg")

                'Remove the new photo from the camera
                Try
                    camera.Items.Remove(1)
                    camera.ExecuteCommand(WIA.CommandID.wiaCommandSynchronize)
                Catch ex As Exception
                    log.Errors("dm_OnEvent: " & ex.Message)
                End Try

                'Toggle the stop and go LEDs on the input device
                input.Write("r")
                input.Write("G")

                'Update the photo count and carry on
                photoCount += 1
                BoothState = PhotoBoothState.Ready

            Case Else
                'Do nothing
        End Select
    End Sub

    Private Sub messagePanel_Complete(ByVal sender As Object, ByVal e As System.EventArgs) Handles messagePanel.Complete
        Try
            camera.ExecuteCommand(CommandID.wiaCommandTakePicture)
        Catch ex As Exception
            log.Errors("Panel Complete: " & ex.Message)
            'Restart the photo booth because the camera miss fired
            'Usually happens when it is unable to find something to focus on
            log.Info("Camera miss-fired")
            PrintMessage(My.Settings.ErrorMessage)

            'Toggle the stop and go LEDs on the input device
            input.Write("r")
            input.Write("G")

            BoothState = PhotoBoothState.Ready
        End Try
    End Sub

    Private Sub PrintMessage(ByVal Message As String)
        FiveBottomPanel1.Visible = False
        messageDock.Visible = False

        Dim g As Graphics = Me.CreateGraphics
        Dim sb As New SolidBrush(Color.White)
        Dim mf As New Font("Arial", 20, FontStyle.Bold, GraphicsUnit.Point)
        Dim xPos As Single = (Me.Width / 2) - (g.MeasureString(Message, mf).Width / 2)
        Dim yPos As Single = (Me.Height / 2) - (g.MeasureString(Message, mf).Height / 2)
        g.DrawString(Message, mf, sb, xPos, yPos)
        sb.Dispose()
        g.Dispose()
    End Sub
End Class
