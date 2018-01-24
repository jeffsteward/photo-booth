Public Class Usage

    Private Sub Usage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Me.Close()
    End Sub

    Private Sub Usage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub

    Private Sub Usage_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        DrawFormBorder()
    End Sub

    Private Sub DrawFormBorder()
        ' Draw a thin border 10 pixels in on all sides
        Const BORDER_MARGIN = 10

        Dim myPen As New System.Drawing.Pen(System.Drawing.Color.White)
        Dim formGraphics As System.Drawing.Graphics

        formGraphics = Me.CreateGraphics()
        formGraphics.DrawRectangle(myPen, _
                            BORDER_MARGIN, BORDER_MARGIN, _
                            Me.Width - BORDER_MARGIN * 2, Me.Height - BORDER_MARGIN * 2)

        myPen.Dispose()
        formGraphics.Dispose()
    End Sub
End Class