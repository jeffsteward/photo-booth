Imports System.Windows.Forms
Imports System.IO.Ports

Public Class Options

    Private dataAvailable As Boolean = False

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click, Button3.Click
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            If sender.Equals(Button2) Then
                txtWatchPath.Text = FolderBrowserDialog1.SelectedPath
            ElseIf sender.Equals(Button3) Then
                txtImageCachePath.Text = FolderBrowserDialog1.SelectedPath
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            txtCamControlPath.Text = OpenFileDialog1.FileName
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If FontDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Settings.MessageFont = FontDialog1.Font
            txtMessageFont.Text = String.Format("{0}, {1}pt", _
                    My.Settings.MessageFont.FontFamily.Name, _
                    My.Settings.MessageFont.SizeInPoints)
        End If
    End Sub

    Private Sub Options_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Fill the COM combobox with a list of the available ports
        COMPorts.Items.Clear()
        For Each s As String In System.IO.Ports.SerialPort.GetPortNames
            COMPorts.Items.Add(s)
        Next
        Test.Enabled = (COMPorts.Text <> String.Empty)

        'Display the message font info
        FontDialog1.Font = My.Settings.MessageFont
        txtMessageFont.Text = String.Format("{0}, {1}pt", _
                My.Settings.MessageFont.FontFamily.Name, _
                My.Settings.MessageFont.SizeInPoints)
    End Sub

    Private Sub Test_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Test.Click
        'Test the selected COM 
        Const TIME_OUT = 30000
        Dim testPort As SerialPort
        Dim testResults As Boolean = False

        Test.Enabled = False

        'Start showing status messages
        TestMessage.Text = String.Empty
        TestMessage.Visible = True

        Try
            'Create the port
            testPort = New SerialPort(COMPorts.Text, 9600, Parity.None, 8)

            'Bind to a function to handle receive the test data
            AddHandler testPort.DataReceived, AddressOf ReceivePortData

            'Open the port
            If testPort.IsOpen Then
                testPort.Close()
            End If
            TestMessage.Text = "Attempting to open the port..."
            TestMessage.Update()
            testPort.Open()
            TestMessage.Text = "Successfully opened the port"
            TestMessage.Update()

            'Send a predetermined message to the device
            TestMessage.Text = "Sending message to the port..."
            TestMessage.Update()
            testPort.Write("T")     'TODO: Determine what the "handshake" is going to be
            TestMessage.Text = "Successfully sent a message to the port"
            TestMessage.Update()

            'Wait up to 30 seconds for a known response
            TestMessage.Text = "Waiting for response message..."
            TestMessage.Update()
            'Wait for a response
            Dim sw As New Stopwatch()
            sw.Start()
            Do While sw.ElapsedMilliseconds < TIME_OUT
                If dataAvailable Then
                    If testPort.ReadChar = Asc("T") Then
                        testResults = True
                        Exit Do
                    Else
                        'We did not receive the expected response
                        dataAvailable = False
                    End If
                End If
            Loop
            sw.Stop()
            If testResults Then
                TestMessage.Text = "Successfully received the expected response"
            Else
                TestMessage.Text = "Did not receive the expected response"
            End If
            TestMessage.Update()

            'Remove the binding to the receive function
            RemoveHandler testPort.DataReceived, AddressOf ReceivePortData

            'Close the port
            TestMessage.Text = "Attempting to close the port..."
            TestMessage.Update()
            testPort.Close()
            TestMessage.Text = "Successfully closed the port"
            TestMessage.Update()


        Catch ex As Exception
            'TODO: Report the COM port testing exceptions
        Finally
            Test.Enabled = True
            'TODO: Use a flag to display the correct message
            TestMessage.Text = "Successfully located the camera trigger on port " & COMPorts.Text
        End Try
    End Sub

    Private Sub ReceivePortData(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs)
        dataAvailable = True
    End Sub

    Private Sub SelectCamera_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectCamera.Click
        Try
            Dim c As New WIA.CommonDialog
            Dim camera As WIA.Device = c.ShowSelectDevice(WIA.WiaDeviceType.CameraDeviceType, True)
            txtCamera.Text = camera.Properties.Item("Name").Value
            My.Settings.CameraDeviceID = camera.DeviceID

        Catch ex As Exception
            MessageBox.Show("Could not connect to any cameras." & vbCrLf & ex.Message)
        End Try
    End Sub
End Class
