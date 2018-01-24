<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.fsWatcher = New System.IO.FileSystemWatcher
        Me.messageDock = New System.Windows.Forms.Panel
        Me.FiveBottomPanel1 = New PhotoBooth.FiveBottomPanel
        CType(Me.fsWatcher, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fsWatcher
        '
        Me.fsWatcher.EnableRaisingEvents = True
        Me.fsWatcher.SynchronizingObject = Me
        '
        'messageDock
        '
        Me.messageDock.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.messageDock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.messageDock.Location = New System.Drawing.Point(12, 160)
        Me.messageDock.Name = "messageDock"
        Me.messageDock.Size = New System.Drawing.Size(626, 163)
        Me.messageDock.TabIndex = 4
        '
        'FiveBottomPanel1
        '
        Me.FiveBottomPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.FiveBottomPanel1.Location = New System.Drawing.Point(12, 12)
        Me.FiveBottomPanel1.Name = "FiveBottomPanel1"
        Me.FiveBottomPanel1.Size = New System.Drawing.Size(629, 458)
        Me.FiveBottomPanel1.TabIndex = 5
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.ClientSize = New System.Drawing.Size(650, 482)
        Me.Controls.Add(Me.messageDock)
        Me.Controls.Add(Me.FiveBottomPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Photo Booth"
        CType(Me.fsWatcher, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents fsWatcher As System.IO.FileSystemWatcher
    Friend WithEvents messageDock As System.Windows.Forms.Panel
    Friend WithEvents FiveBottomPanel1 As PhotoBooth.FiveBottomPanel

End Class
