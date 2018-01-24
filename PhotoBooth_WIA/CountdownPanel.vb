Imports System.Drawing.Imaging

Public Class CountdownPanel
    Inherits Panel

    Private A As Integer = 255
    Private msg As Bitmap
    Private msgBackGround As Bitmap
    Private _countDown As Integer
    Private _duration As Integer = 3
    Private WithEvents _tmr As Timer
    Private WithEvents _fadeTmr As Timer

    Public Event Complete(ByVal sender As Object, ByVal e As EventArgs)

    Public Sub New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)

        _tmr = New Timer
        _tmr.Interval = 1000

        'Fade out the message in the first half second of the main timer interval
        _fadeTmr = New Timer
        _fadeTmr.Interval = 10 'The value is derived from this formula (_tmr.Interval/2)/(255/5)
    End Sub

    Public Property Duration() As Integer
        Get
            Return _duration
        End Get
        Set(ByVal value As Integer)
            _duration = value
        End Set
    End Property

    Public Sub Start()
        _countDown = Duration
        _tmr.Start()
        Me.Visible = True

        MakeMessage(_countDown)
        ShowMessage(A)
        '_fadeTmr.Start()
    End Sub

    Private Sub tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _tmr.Tick
        _countDown -= 1
        If _countDown > 0 Then
            MakeMessage(_countDown)
            ShowMessage(A)
            '_fadeTmr.Start()
        Else
            _tmr.Stop()
            _countDown = Duration
            Me.Visible = False

            RaiseEvent Complete(Me, Nothing)
        End If
    End Sub

    Private Sub _fadeTmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _fadeTmr.Tick
        A -= 5
        If A < 0 Then
            A = 255
            _fadeTmr.Stop()
        Else
            ShowMessage(A)
        End If
    End Sub

    Private Sub ShowMessage(ByVal alpha As Byte)
        Dim cm As ColorMatrix = New ColorMatrix(New Single()() _
                    {New Single() {1, 0, 0, 0, 0}, _
                    New Single() {0, 1, 0, 0, 0}, _
                    New Single() {0, 0, 1, 0, 0}, _
                    New Single() {0, 0, 0, (alpha / 255), 0}, _
                    New Single() {0, 0, 0, 0, 1}})

        Dim IA As New ImageAttributes
        IA.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap)

        Dim tmp As New Bitmap(msgBackGround)
        Dim G As Graphics = Graphics.FromImage(tmp)
        G.DrawImage(msg, Me.ClientRectangle, 0, 0, Me.Width, Me.Height, GraphicsUnit.Pixel, IA)
        G.Dispose()
        IA.Dispose()

        Me.BackgroundImage = tmp
    End Sub

    Private Sub MakeMessage(ByVal message As String)
        If Me.IsHandleCreated Then

            Me.Visible = True

            msgBackGround = New Bitmap(Me.ClientRectangle.Width, Me.ClientRectangle.Height)
            Dim g As Graphics = Graphics.FromImage(msgBackGround)
            g.Clear(Me.BackColor)
            g.Dispose()

            msg = New Bitmap(msgBackGround.Width, msgBackGround.Height)
            g = Graphics.FromImage(msg)
            g.Clear(Color.Transparent)
            Dim sb As New SolidBrush(Me.ForeColor)

            'Center the message on screen
            Dim xPos As Single
            xPos = (Me.Parent.Width - g.MeasureString(message, Me.Font).Width) / 2

            Dim yPos As Single
            yPos = (Me.Parent.Height - g.MeasureString(message, Me.Font).Height) / 2

            g.DrawString(message, Me.Font, sb, xPos, yPos)
            sb.Dispose()
            g.Dispose()

            'Reset the alpha value to full opacity
            A = 255
        End If
    End Sub
End Class
