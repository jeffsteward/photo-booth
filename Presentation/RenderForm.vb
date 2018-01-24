Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Public Class RenderForm
    Inherits Form

    Private i As Integer = 1
    Private m_Watcher As IO.FileSystemWatcher

    Private m_Log As EZLogger

    Private m_DeltaTime As Double
    Private m_Device As Device
    Private m_PhotoSystem As PhotoSystem
    Private m_PhotoList As Collection

    Public Function InitializeGraphics() As Boolean
        Dim params As New PresentParameters
        params.Windowed = True
        params.SwapEffect = SwapEffect.Discard
        params.EnableAutoDepthStencil = True
        params.AutoDepthStencilFormat = DepthFormat.D16
        params.PresentationInterval = PresentInterval.Immediate

        ' Uncomment to enable Full Screen
        'params.BackBufferWidth = 1024
        'params.BackBufferHeight = 768
        'params.BackBufferFormat = Format.R5G6B5
        'params.Windowed = False

        Try
            m_Device = New Device(0, DeviceType.Hardware, Me, CreateFlags.HardwareVertexProcessing, params)
            Debug.WriteLine("Hardware, HardwardVertexProcessing")
        Catch
        End Try

        If m_Device Is Nothing Then
            Try
                m_Device = New Device(0, DeviceType.Hardware, Me, CreateFlags.SoftwareVertexProcessing, params)
                Debug.WriteLine("Hardware, SoftwareVertexProcessing")
            Catch
            End Try
        End If

        If m_Device Is Nothing Then
            Try
                m_Device = New Device(0, DeviceType.Reference, Me, CreateFlags.SoftwareVertexProcessing, params)
                Debug.WriteLine("Reference, SoftwareVertexProcessing")
            Catch ex As Exception
                MessageBox.Show("Error initializing Direct3D" & vbCrLf & vbCrLf & ex.Message, _
                    "Direct3D Error", MessageBoxButtons.OK)
                Return False
            End Try
        End If

        m_Device.RenderState.Lighting = False
        m_Device.RenderState.CullMode = Cull.CounterClockwise
        m_Device.RenderState.FillMode = FillMode.Solid

        'AddHandler m_Device.DeviceLost, AddressOf OnDeviceLost
        'AddHandler m_Device.DeviceReset, AddressOf OnDeviceReset
        'AddHandler m_Device.Disposing, AddressOf OnDeviceDisposing

        'OnDeviceReset(Me, Nothing)

        BuildPhotos()

        ' Setup the file system watcher
        fsWatcher.EnableRaisingEvents = False
        fsWatcher.Path = My.Settings.WatchFolder
        fsWatcher.NotifyFilter = (IO.NotifyFilters.CreationTime Or IO.NotifyFilters.LastWrite)
        fsWatcher.Filter = "*.jpg"
        fsWatcher.EnableRaisingEvents = True

        m_PhotoList = New Collection

        HiResTimer.Start()

        Return True
    End Function

    Private Sub OnDeviceReset(ByVal sender As Object, ByVal e As EventArgs)
        m_Device.Transform.Projection = Matrix.PerspectiveFovLH(Math.PI / 4, 1, 1, 500)
    End Sub

    Private Sub OnDeviceLost(ByVal sender As Object, ByVal e As EventArgs)
        '
    End Sub

    Private Sub OnDeviceDisposing(ByVal sender As Object, ByVal e As EventArgs)
        '
    End Sub

    Private Sub OnCancelResizing(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        e.Cancel = True
    End Sub

    Public Sub Render()
        m_DeltaTime = HiResTimer.GetElapsedTime

        m_PhotoSystem.Update(m_DeltaTime)

        m_Device.Clear(ClearFlags.Target, Color.Black, 1, 0)

        m_Device.BeginScene()

        ' Draw stuff here...
        SetupMatrices()

        m_PhotoSystem.Render()

        m_Device.EndScene()
        m_Device.Present()
    End Sub

    Private Sub SetupMatrices()
        ' World matrix
        'Const TICKS_PER_REV As Integer = 2000
        'Dim angle As Double = Environment.TickCount * (2 * Math.PI) / TICKS_PER_REV
        'm_Device.Transform.World = Matrix.RotationY(CSng(angle))
        m_Device.Transform.World = Matrix.Identity()

        ' View matrix
        m_Device.Transform.View = Matrix.LookAtLH( _
             New Vector3(0, 0, 0), _
            New Vector3(0, 0, 160), _
            New Vector3(0, 1, 0))

        ' Projection matrix
        m_Device.Transform.Projection = Matrix.PerspectiveFovLH(Math.PI / 4, 1, 1, 2000)
    End Sub

    Public Sub BuildPhotos()
        m_PhotoSystem = New PhotoSystem(m_Device, New Vector3(0, 0, 150))
    End Sub

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Size = New Size(1024, 768)

        ' Start logging events
        m_Log = New EZLogger(My.Application.Info.DirectoryPath & "\log.txt", True, EZLogger.Level.All, False)
        m_Log.StartLog()
    End Sub

    Private Sub RenderForm_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Application.Exit()
        End Select
    End Sub

    Private Sub fsWatcher_Changed(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs) Handles fsWatcher.Changed, fsWatcher.Created
        If Not m_PhotoList.Contains(e.Name) Then
            m_PhotoList.Add(e.Name, e.Name)

            Try
                m_PhotoSystem.AddPhoto(TextureLoader.FromFile(m_Device, My.Settings.WatchFolder & "\" & e.Name, _
                                                    800, 531, -1, 0, Format.Unknown, Pool.Managed, _
                                                    Filter.Mirror Or Filter.Triangle, _
                                                    Filter.Mirror Or Filter.Triangle, 0))
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try
        End If
    End Sub
End Class
