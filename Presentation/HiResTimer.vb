Imports System.Runtime.InteropServices

Class HiResTimer

    Private Sub New()
    End Sub

    Shared Sub New()
        isTimerStopped = True
        ticksPerSecond = 0
        stopTime = 0
        lastElapsedTime = 0
        baseTime = 0
        isUsingQPF = QueryPerformanceFrequency(ticksPerSecond)
    End Sub

    Public Shared Sub Reset()
        If Not isUsingQPF Then
            Return
        End If
        Dim time As Long = 0
        If Not (stopTime = 0) Then
            time = stopTime
        Else
            QueryPerformanceCounter(time)
        End If
        baseTime = time
        lastElapsedTime = time
        stopTime = 0
        isTimerStopped = False
    End Sub

    Public Shared Sub Start()
        If Not isUsingQPF Then
            Return
        End If
        Dim time As Long = 0
        If Not (stopTime = 0) Then
            time = stopTime
        Else
            QueryPerformanceCounter(time)
        End If
        If isTimerStopped Then
            baseTime += (time - stopTime)
        End If
        stopTime = 0
        lastElapsedTime = time
        isTimerStopped = False
    End Sub

    Public Shared Sub StopTimer()
        If Not isUsingQPF Then
            Return
        End If
        If Not isTimerStopped Then
            Dim time As Long = 0
            If Not (stopTime = 0) Then
                time = stopTime
            Else
                QueryPerformanceCounter(time)
            End If
            stopTime = time
            lastElapsedTime = time
            isTimerStopped = True
        End If
    End Sub

    Public Shared Sub Advance()
        If Not isUsingQPF Then
            Return
        End If
        stopTime += ticksPerSecond / 10
    End Sub

    Public Shared Function GetAbsoluteTime() As Single
        If Not isUsingQPF Then
            Return -1
        End If
        Dim time As Long = 0
        If Not (stopTime = 0) Then
            time = stopTime
        Else
            QueryPerformanceCounter(time)
        End If
        Dim absolueTime As Double = time / CType(ticksPerSecond, Double)
        Return CType(absolueTime, Single)
    End Function

    Public Shared Function GetTime() As Double
        If Not isUsingQPF Then
            Return -1
        End If
        Dim time As Long = 0
        If Not (stopTime = 0) Then
            time = stopTime
        Else
            QueryPerformanceCounter(time)
        End If
        Dim appTime As Double = CType((time - baseTime), Double) / CType(ticksPerSecond, Double)
        Return appTime
    End Function

    Public Shared Function GetElapsedTime() As Double
        If Not isUsingQPF Then
            Return -1
        End If
        Dim time As Long = 0
        If Not (stopTime = 0) Then
            time = stopTime
        Else
            QueryPerformanceCounter(time)
        End If
        Dim elapsedTime As Double = CType((time - lastElapsedTime), Double) / CType(ticksPerSecond, Double)
        lastElapsedTime = time
        Return elapsedTime
    End Function

    Public Shared ReadOnly Property IsStopped() As Boolean
        Get
            Return isTimerStopped
        End Get
    End Property
    Private Shared isUsingQPF As Boolean
    Private Shared isTimerStopped As Boolean
    Private Shared ticksPerSecond As Long
    Private Shared stopTime As Long
    Private Shared lastElapsedTime As Long
    Private Shared baseTime As Long

    <System.Security.SuppressUnmanagedCodeSecurity()> _
    <DllImport("kernel32")> _
    Public Shared Function QueryPerformanceFrequency(ByRef PerformanceFrequency As Long) As Boolean
    End Function

    <System.Security.SuppressUnmanagedCodeSecurity()> _
    <DllImport("kernel32")> _
    Public Shared Function QueryPerformanceCounter(ByRef PerformanceCount As Long) As Boolean
    End Function
End Class
