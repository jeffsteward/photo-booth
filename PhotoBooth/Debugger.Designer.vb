<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Debugger
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DebugMessages = New System.Windows.Forms.ListBox
        Me.SuspendLayout()
        '
        'DebugMessages
        '
        Me.DebugMessages.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DebugMessages.FormattingEnabled = True
        Me.DebugMessages.IntegralHeight = False
        Me.DebugMessages.Location = New System.Drawing.Point(0, 0)
        Me.DebugMessages.Name = "DebugMessages"
        Me.DebugMessages.Size = New System.Drawing.Size(441, 190)
        Me.DebugMessages.TabIndex = 0
        '
        'Debugger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(441, 190)
        Me.Controls.Add(Me.DebugMessages)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Debugger"
        Me.Text = "Debug"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DebugMessages As System.Windows.Forms.ListBox
End Class
