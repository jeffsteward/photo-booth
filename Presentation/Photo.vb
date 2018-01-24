Imports Microsoft.DirectX
Imports Microsoft.DirectX.Direct3D

Public Class Photo
    Private Const m_ScatterRadius = 75.0F
    Private Const m_ScatterUpperLimit = m_ScatterRadius
    Private Const m_ScatterLowerLimtit = -m_ScatterRadius

    Private m_Height As Single = 20.0F
    Private m_Width As Single = 20.0F
    Private m_XOffset As Single = 0.0F
    Private m_YOffset As Single = 0.0F
    Private m_ZOffset As Single = 0.0F

    Private m_InMotion As Boolean = False
    Private m_T As Single = 0.0F
    Private m_TimeElapsed As Single = 0.0F

    Private m_Device As Device
    Private m_Texture As Texture
    Private m_Photo As VertexBuffer
    Private m_Vertices As CustomVertex.PositionNormalTextured()
    Private m_Position As Vector3
    Private m_Home As Vector3

    Public Sub New(ByVal device As Device, ByVal position As Vector3)
        m_Device = device
        m_Home = position
        m_Position = position
        SetupPhotoFaces()
    End Sub

    Public Sub New(ByVal device As Device, ByVal position As Vector3, ByVal texture As Texture)
        Me.New(device, position)
        m_Texture = texture
    End Sub

    Public Sub Update(ByVal deltaTime As Single)

        If m_InMotion Then
            m_T += (1.0F / (2.0F / deltaTime))
            If m_T <= 1.0F Then
                m_Position.X = (m_Home.X - m_XOffset) * m_T + m_XOffset
                m_Position.Y = (m_Home.Y - m_YOffset) * m_T + m_YOffset
                m_Position.Z = (m_Home.Z - m_ZOffset) * m_T + m_ZOffset
            Else
                'Me.MoveHome()
                'm_Position.X = m_Home.X
                'm_Position.Y = m_Home.Y
                'm_Position.Z = m_Home.Z
                m_InMotion = False
                m_T = 0.0F
            End If

            ' Hold the current jitter position for a little while
            If m_TimeElapsed > 0.025F Then
                'm_Position.X += CType(Rnd() * 0.1, Single)
                'm_Position.Y += CType(Rnd() * 0.1, Single)

                m_TimeElapsed = 0
            Else
                m_TimeElapsed += deltaTime
            End If
        End If
    End Sub

    Public Sub Render()
        Me.Render(m_Texture)
    End Sub

    Public Sub Render(ByVal texture As Texture)
        m_Texture = texture

        m_Device.Transform.World = Matrix.Translation(m_Position)

        m_Device.RenderState.ZBufferWriteEnable = False
        m_Device.RenderState.ZBufferEnable = False

        RenderPhoto()

        m_Device.RenderState.ZBufferWriteEnable = True
        m_Device.RenderState.ZBufferEnable = True
    End Sub

    ' Move the partical to a random point in space
    Public Sub Scatter()
        m_XOffset = (m_ScatterUpperLimit - m_ScatterLowerLimtit + 1) * Rnd() + m_ScatterLowerLimtit
        m_XOffset += 100 * Math.Sign(m_XOffset)

        m_YOffset = (m_ScatterUpperLimit - m_ScatterLowerLimtit + 1) * Rnd() + m_ScatterLowerLimtit
        m_YOffset += 100 * Math.Sign(m_YOffset)

        m_ZOffset = m_Home.Z

        m_T = 0
        m_InMotion = True
    End Sub

    Public Sub Scatter(ByVal returnTo As Vector3)
        Me.Scatter()
        m_Home = returnTo
    End Sub

    ' Move the photo to its home location
    Public Sub MoveHome()
        m_XOffset = m_Home.X
        m_YOffset = m_Home.Y
        m_ZOffset = m_Home.Z
        m_InMotion = True
    End Sub

    ' Send the particle from its current home location to a new home location
    Public Sub MoveTo(ByVal position As Vector3)
        m_XOffset = m_Home.X
        m_YOffset = m_Home.Y
        m_ZOffset = m_Home.Z
        m_Home = position
        m_T = 0
        m_InMotion = True
    End Sub

    Private Sub RenderPhoto()
        m_Device.SetStreamSource(0, m_Photo, 0)
        m_Device.VertexFormat = CustomVertex.PositionNormalTextured.Format
        m_Device.SetTexture(0, m_Texture)
        m_Device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2)
    End Sub

    Private Sub SetupPhotoFaces()
        m_Vertices = New CustomVertex.PositionNormalTextured(4) {}

        m_Vertices(0).X = m_Home.X - (m_Width / 2)
        m_Vertices(0).Y = m_Home.Y + (m_Height / 2)
        m_Vertices(0).Z = m_Home.Z
        m_Vertices(0).Tu = 0.0F
        m_Vertices(0).Tv = 0.0F
        m_Vertices(0).Nx = 0.0F
        m_Vertices(0).Ny = 0.0F
        m_Vertices(0).Nz = -1.0F
        m_Vertices(1).X = m_Home.X + (m_Width / 2)
        m_Vertices(1).Y = m_Home.Y + (m_Height / 2)
        m_Vertices(1).Z = m_Home.Z
        m_Vertices(1).Tu = 1.0F
        m_Vertices(1).Tv = 0.0F
        m_Vertices(1).Nx = 0.0F
        m_Vertices(1).Ny = 0.0F
        m_Vertices(1).Nz = -1.0F
        m_Vertices(2).X = m_Home.X - (m_Width / 2)
        m_Vertices(2).Y = m_Home.Y - (m_Height / 2)
        m_Vertices(2).Z = m_Home.Z
        m_Vertices(2).Tu = 0.0F
        m_Vertices(2).Tv = 1.0F
        m_Vertices(2).Nx = 0.0F
        m_Vertices(2).Ny = 0.0F
        m_Vertices(2).Nz = -1.0F
        m_Vertices(3).X = m_Home.X + (m_Width / 2)
        m_Vertices(3).Y = m_Home.Y - (m_Height / 2)
        m_Vertices(3).Z = m_Home.Z
        m_Vertices(3).Tu = 1.0F
        m_Vertices(3).Tv = 1.0F
        m_Vertices(3).Nx = 0.0F
        m_Vertices(3).Ny = 0.0F
        m_Vertices(3).Nz = -1.0F

        m_Photo = New VertexBuffer(GetType(CustomVertex.PositionNormalTextured), _
                                4, m_Device, Usage.WriteOnly, CustomVertex.PositionNormalTextured.Format, Pool.Default)

        m_Photo.SetData(m_Vertices, 0, 0)
    End Sub

    Public ReadOnly Property Home() As Vector3
        Get
            Return m_Home
        End Get
    End Property

    Friend Property Texture() As Texture
        Get
            Return m_Texture
        End Get
        Set(ByVal value As Texture)
            m_Texture = value
        End Set
    End Property
End Class
