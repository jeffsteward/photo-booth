Imports System.Drawing.Imaging

Public Class FaderPanel
    Inherits Panel

    Private Enum MsgState
        Delaying
        Fading
    End Enum

    Private A As Integer
    Private msg As Bitmap
    Private msgBackGround As Bitmap
    Private currentState As MsgState
    Private WithEvents tmr As New Timer
    Private fadeDelayInSeconds As Single        'Calculate to be a certain percentage of the display duration

    Public DisplayDuration As Single = 2.5F

    Public Sub New()
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
    End Sub

    Public Sub FlashMessage(ByVal message As String)
        If Me.IsHandleCreated Then
            tmr.Stop()

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

            g.DrawString(message, Me.Font, sb, xPos, 0)
            sb.Dispose()
            g.Dispose()

            'Reset the alpha value to full opacity
            A = 255
            ShowMessage(A)

            'Calculate the delay before starting the fade (25% of the display duration)
            fadeDelayInSeconds = DisplayDuration * 0.25

            currentState = MsgState.Delaying
            tmr.Interval = fadeDelayInSeconds * 1000    'Convert to milliseconds
            tmr.Start()
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

    Private Sub tmr_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tmr.Tick
        Select Case currentState
            Case MsgState.Delaying
                tmr.Stop()

                'Calculate the number of steps to fade out in the remaining display time
                '51 = full opacity - the step value (255/5)
                tmr.Interval = ((DisplayDuration - fadeDelayInSeconds) * 1000) / 51

                'Change the panel state to fade out
                currentState = MsgState.Fading
                tmr.Start()

            Case MsgState.Fading
                A = A - 5
                If A < 0 Then
                    tmr.Stop()
                Else
                    ShowMessage(A)
                End If
        End Select
    End Sub
End Class