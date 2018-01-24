<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.lblCaption = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Test = New System.Windows.Forms.Button
        Me.TestMessage = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Button3 = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtMessageFont = New System.Windows.Forms.TextBox
        Me.FontDialog1 = New System.Windows.Forms.FontDialog
        Me.Button4 = New System.Windows.Forms.Button
        Me.SelectCamera = New System.Windows.Forms.Button
        Me.txtCamera = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtCounterMessage = New System.Windows.Forms.TextBox
        Me.txtErrorMessage = New System.Windows.Forms.TextBox
        Me.txtImageCachePath = New System.Windows.Forms.TextBox
        Me.txtWaitMessage = New System.Windows.Forms.TextBox
        Me.COMPorts = New System.Windows.Forms.ComboBox
        Me.MessageDuration = New System.Windows.Forms.NumericUpDown
        Me.txtWatchPath = New System.Windows.Forms.TextBox
        Me.txtCamControlPath = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.MessageDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 420)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'lblCaption
        '
        Me.lblCaption.AutoSize = True
        Me.lblCaption.Location = New System.Drawing.Point(9, 53)
        Me.lblCaption.Name = "lblCaption"
        Me.lblCaption.Size = New System.Drawing.Size(175, 13)
        Me.lblCaption.TabIndex = 2
        Me.lblCaption.Text = "Camera control application location:"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(394, 69)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(26, 20)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(394, 112)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(26, 20)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Image location:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 311)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(191, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Show message while taking picture for:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(65, 329)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(47, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "seconds"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 354)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "COM port for camera trigger:"
        '
        'Test
        '
        Me.Test.Location = New System.Drawing.Point(87, 370)
        Me.Test.Name = "Test"
        Me.Test.Size = New System.Drawing.Size(48, 21)
        Me.Test.TabIndex = 13
        Me.Test.Text = "Test"
        Me.Test.UseVisualStyleBackColor = True
        '
        'TestMessage
        '
        Me.TestMessage.AutoSize = True
        Me.TestMessage.Location = New System.Drawing.Point(141, 374)
        Me.TestMessage.Name = "TestMessage"
        Me.TestMessage.Size = New System.Drawing.Size(77, 13)
        Me.TestMessage.TabIndex = 14
        Me.TestMessage.Text = "Opening port..."
        Me.TestMessage.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 182)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Wait message:"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(394, 155)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(26, 20)
        Me.Button3.TabIndex = 19
        Me.Button3.Text = "..."
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 139)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(112, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Image cache location:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(241, 182)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Font:"
        '
        'txtMessageFont
        '
        Me.txtMessageFont.Enabled = False
        Me.txtMessageFont.Location = New System.Drawing.Point(244, 198)
        Me.txtMessageFont.Name = "txtMessageFont"
        Me.txtMessageFont.Size = New System.Drawing.Size(144, 20)
        Me.txtMessageFont.TabIndex = 21
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(394, 198)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(26, 20)
        Me.Button4.TabIndex = 22
        Me.Button4.Text = "..."
        Me.Button4.UseVisualStyleBackColor = True
        '
        'SelectCamera
        '
        Me.SelectCamera.Location = New System.Drawing.Point(334, 25)
        Me.SelectCamera.Name = "SelectCamera"
        Me.SelectCamera.Size = New System.Drawing.Size(89, 22)
        Me.SelectCamera.TabIndex = 24
        Me.SelectCamera.Text = "Select camera"
        Me.SelectCamera.UseVisualStyleBackColor = True
        '
        'txtCamera
        '
        Me.txtCamera.Location = New System.Drawing.Point(12, 26)
        Me.txtCamera.Name = "txtCamera"
        Me.txtCamera.Size = New System.Drawing.Size(316, 20)
        Me.txtCamera.TabIndex = 25
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 10)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(46, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "Camera:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(9, 268)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(77, 13)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Error message:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 225)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(92, 13)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Counter message:"
        '
        'txtCounterMessage
        '
        Me.txtCounterMessage.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.PhotoBooth.My.MySettings.Default, "CounterMessage", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtCounterMessage.Location = New System.Drawing.Point(12, 241)
        Me.txtCounterMessage.Name = "txtCounterMessage"
        Me.txtCounterMessage.Size = New System.Drawing.Size(376, 20)
        Me.txtCounterMessage.TabIndex = 30
        Me.txtCounterMessage.Text = Global.PhotoBooth.My.MySettings.Default.CounterMessage
        '
        'txtErrorMessage
        '
        Me.txtErrorMessage.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.PhotoBooth.My.MySettings.Default, "ErrorMessage", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtErrorMessage.Location = New System.Drawing.Point(12, 284)
        Me.txtErrorMessage.Name = "txtErrorMessage"
        Me.txtErrorMessage.Size = New System.Drawing.Size(376, 20)
        Me.txtErrorMessage.TabIndex = 27
        Me.txtErrorMessage.Text = Global.PhotoBooth.My.MySettings.Default.ErrorMessage
        '
        'txtImageCachePath
        '
        Me.txtImageCachePath.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.PhotoBooth.My.MySettings.Default, "ImageCache", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtImageCachePath.Location = New System.Drawing.Point(12, 155)
        Me.txtImageCachePath.Name = "txtImageCachePath"
        Me.txtImageCachePath.Size = New System.Drawing.Size(376, 20)
        Me.txtImageCachePath.TabIndex = 17
        Me.txtImageCachePath.Text = Global.PhotoBooth.My.MySettings.Default.ImageCache
        '
        'txtWaitMessage
        '
        Me.txtWaitMessage.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.PhotoBooth.My.MySettings.Default, "WaitMessage", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtWaitMessage.Location = New System.Drawing.Point(12, 198)
        Me.txtWaitMessage.Name = "txtWaitMessage"
        Me.txtWaitMessage.Size = New System.Drawing.Size(226, 20)
        Me.txtWaitMessage.TabIndex = 15
        Me.txtWaitMessage.Text = Global.PhotoBooth.My.MySettings.Default.WaitMessage
        '
        'COMPorts
        '
        Me.COMPorts.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.PhotoBooth.My.MySettings.Default, "COMPort", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.COMPorts.FormattingEnabled = True
        Me.COMPorts.Location = New System.Drawing.Point(12, 370)
        Me.COMPorts.Name = "COMPorts"
        Me.COMPorts.Size = New System.Drawing.Size(69, 21)
        Me.COMPorts.TabIndex = 12
        Me.COMPorts.Text = Global.PhotoBooth.My.MySettings.Default.COMPort
        '
        'MessageDuration
        '
        Me.MessageDuration.DataBindings.Add(New System.Windows.Forms.Binding("Value", Global.PhotoBooth.My.MySettings.Default, "MessageDuration", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.MessageDuration.DecimalPlaces = 1
        Me.MessageDuration.Increment = New Decimal(New Integer() {5, 0, 0, 65536})
        Me.MessageDuration.Location = New System.Drawing.Point(12, 327)
        Me.MessageDuration.Name = "MessageDuration"
        Me.MessageDuration.Size = New System.Drawing.Size(47, 20)
        Me.MessageDuration.TabIndex = 9
        Me.MessageDuration.Value = Global.PhotoBooth.My.MySettings.Default.MessageDuration
        '
        'txtWatchPath
        '
        Me.txtWatchPath.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.PhotoBooth.My.MySettings.Default, "WatchFolder", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtWatchPath.Location = New System.Drawing.Point(12, 112)
        Me.txtWatchPath.Name = "txtWatchPath"
        Me.txtWatchPath.Size = New System.Drawing.Size(376, 20)
        Me.txtWatchPath.TabIndex = 4
        Me.txtWatchPath.Text = Global.PhotoBooth.My.MySettings.Default.WatchFolder
        '
        'txtCamControlPath
        '
        Me.txtCamControlPath.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.PhotoBooth.My.MySettings.Default, "CameraControlEXE", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtCamControlPath.Location = New System.Drawing.Point(12, 69)
        Me.txtCamControlPath.Name = "txtCamControlPath"
        Me.txtCamControlPath.Size = New System.Drawing.Size(376, 20)
        Me.txtCamControlPath.TabIndex = 1
        Me.txtCamControlPath.Text = Global.PhotoBooth.My.MySettings.Default.CameraControlEXE
        '
        'Options
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 461)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtCounterMessage)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtErrorMessage)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtCamera)
        Me.Controls.Add(Me.SelectCamera)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.txtMessageFont)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtImageCachePath)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtWaitMessage)
        Me.Controls.Add(Me.TestMessage)
        Me.Controls.Add(Me.Test)
        Me.Controls.Add(Me.COMPorts)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MessageDuration)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtWatchPath)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblCaption)
        Me.Controls.Add(Me.txtCamControlPath)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Options"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.MessageDuration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtCamControlPath As System.Windows.Forms.TextBox
    Friend WithEvents lblCaption As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtWatchPath As System.Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents MessageDuration As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents COMPorts As System.Windows.Forms.ComboBox
    Friend WithEvents Test As System.Windows.Forms.Button
    Friend WithEvents TestMessage As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtWaitMessage As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtImageCachePath As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtMessageFont As System.Windows.Forms.TextBox
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents SelectCamera As System.Windows.Forms.Button
    Friend WithEvents txtCamera As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtErrorMessage As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCounterMessage As System.Windows.Forms.TextBox

End Class
