<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FiveBottomPanel
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tlpFilmstrip = New System.Windows.Forms.TableLayoutPanel
        Me.thumb1 = New System.Windows.Forms.PictureBox
        Me.thumb2 = New System.Windows.Forms.PictureBox
        Me.thumb3 = New System.Windows.Forms.PictureBox
        Me.thumb4 = New System.Windows.Forms.PictureBox
        Me.thumb5 = New System.Windows.Forms.PictureBox
        Me.picBox = New System.Windows.Forms.PictureBox
        Me.lblCounterMessage = New System.Windows.Forms.Label
        Me.tlpFilmstrip.SuspendLayout()
        CType(Me.thumb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.thumb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.thumb3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.thumb4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.thumb5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBox, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'tlpFilmstrip
        '
        Me.tlpFilmstrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlpFilmstrip.ColumnCount = 5
        Me.tlpFilmstrip.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tlpFilmstrip.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tlpFilmstrip.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tlpFilmstrip.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tlpFilmstrip.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.tlpFilmstrip.Controls.Add(Me.thumb1, 0, 0)
        Me.tlpFilmstrip.Controls.Add(Me.thumb2, 1, 0)
        Me.tlpFilmstrip.Controls.Add(Me.thumb3, 2, 0)
        Me.tlpFilmstrip.Controls.Add(Me.thumb4, 3, 0)
        Me.tlpFilmstrip.Controls.Add(Me.thumb5, 4, 0)
        Me.tlpFilmstrip.Location = New System.Drawing.Point(0, 262)
        Me.tlpFilmstrip.Name = "tlpFilmstrip"
        Me.tlpFilmstrip.RowCount = 1
        Me.tlpFilmstrip.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpFilmstrip.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160.0!))
        Me.tlpFilmstrip.Size = New System.Drawing.Size(621, 160)
        Me.tlpFilmstrip.TabIndex = 6
        '
        'thumb1
        '
        Me.thumb1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.thumb1.Location = New System.Drawing.Point(3, 3)
        Me.thumb1.Name = "thumb1"
        Me.thumb1.Size = New System.Drawing.Size(118, 154)
        Me.thumb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.thumb1.TabIndex = 5
        Me.thumb1.TabStop = False
        '
        'thumb2
        '
        Me.thumb2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.thumb2.Location = New System.Drawing.Point(127, 3)
        Me.thumb2.Name = "thumb2"
        Me.thumb2.Size = New System.Drawing.Size(118, 154)
        Me.thumb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.thumb2.TabIndex = 6
        Me.thumb2.TabStop = False
        '
        'thumb3
        '
        Me.thumb3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.thumb3.Location = New System.Drawing.Point(251, 3)
        Me.thumb3.Name = "thumb3"
        Me.thumb3.Size = New System.Drawing.Size(118, 154)
        Me.thumb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.thumb3.TabIndex = 7
        Me.thumb3.TabStop = False
        '
        'thumb4
        '
        Me.thumb4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.thumb4.Location = New System.Drawing.Point(375, 3)
        Me.thumb4.Name = "thumb4"
        Me.thumb4.Size = New System.Drawing.Size(118, 154)
        Me.thumb4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.thumb4.TabIndex = 8
        Me.thumb4.TabStop = False
        '
        'thumb5
        '
        Me.thumb5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.thumb5.Location = New System.Drawing.Point(499, 3)
        Me.thumb5.Name = "thumb5"
        Me.thumb5.Size = New System.Drawing.Size(119, 154)
        Me.thumb5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.thumb5.TabIndex = 9
        Me.thumb5.TabStop = False
        '
        'picBox
        '
        Me.picBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picBox.Location = New System.Drawing.Point(0, 0)
        Me.picBox.Name = "picBox"
        Me.picBox.Size = New System.Drawing.Size(621, 256)
        Me.picBox.TabIndex = 8
        Me.picBox.TabStop = False
        '
        'lblCounterMessage
        '
        Me.lblCounterMessage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCounterMessage.AutoSize = True
        Me.lblCounterMessage.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCounterMessage.ForeColor = System.Drawing.Color.White
        Me.lblCounterMessage.Location = New System.Drawing.Point(3, 425)
        Me.lblCounterMessage.Name = "lblCounterMessage"
        Me.lblCounterMessage.Size = New System.Drawing.Size(0, 16)
        Me.lblCounterMessage.TabIndex = 9
        '
        'FiveBottomPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblCounterMessage)
        Me.Controls.Add(Me.picBox)
        Me.Controls.Add(Me.tlpFilmstrip)
        Me.Name = "FiveBottomPanel"
        Me.Size = New System.Drawing.Size(621, 441)
        Me.tlpFilmstrip.ResumeLayout(False)
        CType(Me.thumb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.thumb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.thumb3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.thumb4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.thumb5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBox, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tlpFilmstrip As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents thumb1 As System.Windows.Forms.PictureBox
    Friend WithEvents thumb2 As System.Windows.Forms.PictureBox
    Friend WithEvents thumb3 As System.Windows.Forms.PictureBox
    Friend WithEvents thumb4 As System.Windows.Forms.PictureBox
    Friend WithEvents thumb5 As System.Windows.Forms.PictureBox
    Friend WithEvents picBox As System.Windows.Forms.PictureBox
    Friend WithEvents lblCounterMessage As System.Windows.Forms.Label

End Class
