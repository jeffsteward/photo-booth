Imports System
Imports System.IO

Public Class EZLogger

    ''/// <summary>
    ''/// An object that provides basic logging capabilities.
    ''/// Copyright (c) 2006 Ravi Bhavnani, ravib@ravib.com
    ''///
    ''/// This software may be freely used in any product or
    ''/// work, provided this copyright notice is maintained.
    ''/// To help ensure a single point of release, please
    ''/// email and bug reports, flames and suggestions to
    ''/// ravib@ravib.com.
    ''/// </summary>

#Region "Events"
    Event Updated()
#End Region

#Region "Attributes"

    ''/// <summary>
    ''/// Log levels.
    ''/// </summary>
    Public Enum Level
        ''/// <summary>Log debug messages.</summary>
        Debug = 1

        ''/// <summary>Log informational messages.</summary>
        Info = 2

        ''/// <summary>Log success messages.</summary>
        Success = 4

        ''/// <summary>Log warning messages.</summary>
        Warning = 8

        ''/// <summary>Log error messages.</summary>
        Errors = 16

        ''/// <summary>Log fatal errors.</summary>
        Fatal = 32

        ''/// <summary>Log all messages.</summary>
        All = 65535 ' 0xFFFF
    End Enum

    ''/// <summary>
    ''/// The logger's state.
    ''/// </summary>
    Public Enum State
        ''/// <summary>The logger is stopped.</summary>
        Stopped = 0
        ''/// <summary>The logger has been started.</summary>
        Running
        ''/// <summary>The logger is paused.</summary>
        Paused
    End Enum

#End Region

#Region "Construction/destruction"

    ''/// <summary>
    ''/// Constructs a EZLogger.
    ''/// </summary>
    ''/// <param name="logFilename">Log file to receive output.</param>
    ''/// <param name="bAppend">Flag: append to existing file (if any).</param>
    ''/// <param name="bCacheInMemory">Flag: cache log in memory.</param>
    ''/// <param name="logLevels">Mask indicating log levels of interest.</param>
    Public Sub New(ByVal logFilename As String, ByVal bAppend As Boolean, ByVal logLevels As UInteger, ByVal bCacheInMemory As Boolean)
        _logFilename = logFilename
        _bAppend = bAppend
        _bCacheInMemory = bCacheInMemory
        _levels = logLevels

        _logMem = New ArrayList
    End Sub

    ''/// <summary>
    ''/// Private default constructor.
    ''/// </summary>
    Private Sub New()
        '
    End Sub
#End Region

#Region "Properties"

    ''/// <summary>
    ''/// Gets and sets the log level.
    ''/// </summary>
    Public Property Levels() As UInteger
        Get
            Return _levels
        End Get
        Set(ByVal value As UInteger)
            _levels = value
        End Set
    End Property

    ''/// <summary>
    ''/// Retrieves the logger's state.
    ''/// </summary>
    Public ReadOnly Property LoggerState() As State
        Get
            Return _state
        End Get
    End Property

    ''/// <summary>
    ''/// Retrieves the log.
    ''/// </summary>
    Public ReadOnly Property Log() As ArrayList
        Get
            Return _logMem
        End Get
    End Property
#End Region

#Region "Operations"

    ''/// <summary>
    ''/// Starts logging.
    ''/// </summary>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function StartLog() As Boolean
        SyncLock Me
            '// Fail if logging has already been started
            If LoggerState <> State.Stopped Then
                Return False
            End If

            '// Fail if the log file isn't specified
            If String.IsNullOrEmpty(_logFilename) Then
                Return False
            End If

            '// Delete log file if it exists
            If Not _bAppend Then
                Try
                    File.Delete(_logFilename)
                Catch ex As Exception
                    Return False
                End Try
            End If

            '// Open file for writing - return on error
            If File.Exists(_logFilename) = False Then
                Try
                    _logFile = File.CreateText(_logFilename)
                Catch ex As Exception
                    _logFile = Nothing
                    Return False
                End Try
            Else
                Try
                    _logFile = File.AppendText(_logFilename)
                Catch ex As Exception
                    _logFile = Nothing
                    Return False
                End Try
            End If

            _logFile.AutoFlush = True

            '// Return successfully
            _state = EZLogger.State.Running
            Return True
        End SyncLock
    End Function

    ''/// <summary>
    ''/// Temporarily suspends logging.
    ''/// </summary>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function PauseLog() As Boolean
        SyncLock Me
            '// Fail if logging hasn't been started
            If LoggerState <> State.Running Then
                Return False
            End If

            '// Pause the logger
            _state = EZLogger.State.Paused
            Return True
        End SyncLock
    End Function


    ''/// <summary>
    ''/// Resumes logging.
    ''/// </summary>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function ResumeLog() As Boolean
        SyncLock Me
            '// Fail if logging hasn't been paused
            If LoggerState <> State.Paused Then
                Return False
            End If

            '// Resume logging
            _state = EZLogger.State.Running
            Return True
        End SyncLock
    End Function

    ''/// <summary>
    ''/// Stops logging.
    ''/// </summary>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function StopLog() As Boolean
        SyncLock Me
            '// Fail if logging hasn't been started
            If LoggerState <> State.Running Then
                Return False
            End If

            '// Stop logging
            Try
                _logFile.Close()
                _logFile = Nothing

            Catch ex As Exception
                Return False
            End Try

            _state = EZLogger.State.Stopped
            Return True
        End SyncLock
    End Function

    ''/// <summary>
    ''/// Logs a debug message.
    ''/// </summary>
    ''/// <param name="msg">The message.</param>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function Debug(ByVal msg As String) As Boolean
        _debugMsgs += 1
        Return WriteLogMsg(Level.Debug, msg)
    End Function

    ''/// <summary>
    ''/// Logs an informational message.
    ''/// </summary>
    ''/// <param name="msg">The message.</param>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function Info(ByVal msg As String) As Boolean
        _infoMsgs += 1
        Return WriteLogMsg(Level.Info, msg)
    End Function

    ''/// <summary>
    ''/// Logs a success message.
    ''/// </summary>
    ''/// <param name="msg">The message.</param>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function Success(ByVal msg As String) As Boolean
        _successMsgs += 1
        Return WriteLogMsg(Level.Success, msg)
    End Function

    ''/// <summary>
    ''/// Logs a warning message.
    ''/// </summary>
    ''/// <param name="msg">The message.</param>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function Warning(ByVal msg As String) As Boolean
        _warningMsgs += 1
        Return WriteLogMsg(Level.Warning, msg)
    End Function

    ''/// <summary>
    ''/// Logs an error message.
    ''/// </summary>
    ''/// <param name="msg">The message.</param>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function Errors(ByVal msg As String) As Boolean
        _errorMsgs += 1
        Return WriteLogMsg(Level.Errors, msg)
    End Function

    ''/// <summary>
    ''/// Logs a fatal error message.
    ''/// </summary>
    ''/// <param name="msg">The message.</param>
    ''/// <returns>true if successful, false otherwise.</returns>
    Public Function Fatal(ByVal msg As String) As Boolean
        _fatalMsgs += 1
        Return WriteLogMsg(Level.Fatal, msg)
    End Function


    ''/// <summary>
    ''/// Retrieves the count of messages logged at one or more levels.
    ''/// </summary>
    ''/// <param name="levelMask">Mask indicating levels of interest.</param>
    ''/// <returns></returns>
    Public Function GetMessageCount(ByVal levelMask As UInteger) As UInteger
        Dim uMessages As UInteger = 0
        If ((levelMask & CUInt(Level.Debug)) <> 0) Then
            uMessages += _debugMsgs
        End If
        If ((levelMask & CUInt(Level.Info)) <> 0) Then
            uMessages += _infoMsgs
        End If
        If ((levelMask & CUInt(Level.Success)) <> 0) Then
            uMessages += _successMsgs
        End If
        If ((levelMask & CUInt(Level.Warning)) <> 0) Then
            uMessages += _warningMsgs
        End If
        If ((levelMask & CUInt(Level.Errors)) <> 0) Then
            uMessages += _errorMsgs
        End If
        If ((levelMask & CUInt(Level.Fatal)) <> 0) Then
            uMessages += _fatalMsgs
        End If
        Return uMessages
    End Function

#End Region

#Region "Helper methods"

    ''/// <summary>
    ''/// Writes a log message.
    ''/// </summary>
    ''/// <param name="level"></param>
    ''/// <param name="msg"></param>
    ''/// <returns></returns>
    Private Function WriteLogMsg(ByVal level As Level, ByVal msg As String) As Boolean
        SyncLock Me
            '// Fail if logger hasn't been started
            If (LoggerState = State.Stopped) Then
                Return False
            End If

            '// Ignore message logging is paused or it doesn't pass the filter
            If ((LoggerState = State.Paused) Or ((_levels And CUInt(level)) <> CUInt(level))) Then
                Return True
            End If

            '// Write log message
            Dim tmNow As DateTime = DateTime.Now
            Dim logMsg As String = String.Format("{0} {1}  {2}: {3}", _
                                         tmNow.ToShortDateString(), tmNow.ToLongTimeString(), _
                                         level.ToString().Substring(0, 1), msg)
            _logFile.WriteLine(logMsg)

            If _bCacheInMemory Then
                _logMem.Add(logMsg)
            End If

            RaiseEvent Updated()

            Return True
        End SyncLock
    End Function

#End Region

#Region "Fields"

    ''/// <summary>Name of the log file.</summary>
    Private _logFilename As String

    ''/// <summary>Flag: append to existing file (if any).</summary>
    Private _bAppend As Boolean = True

    ''/// <summary>Flag: append to an array list in memory.</summary>
    Private _bCacheInMemory As Boolean = True

    ''/// <summary>The log file.</summary>
    Private _logFile As StreamWriter = Nothing

    ''/// <summary>The log in memory.</summary>
    Private _logMem As ArrayList = Nothing

    ''/// <summary>Levels to be logged.</summary>
    Private _levels As UInteger = (Level.Warning Or Level.Errors Or Level.Fatal)

    ''/// <summary>The logger's state.</summary>
    Private _state As State = State.Stopped

    ''/// <summary>Number of debug messages that have been logged.</summary>
    Private _debugMsgs As UInteger = 0

    ''/// <summary>Number of informational messages that have been logged.</summary>
    Private _infoMsgs As UInteger = 0

    ''/// <summary>Number of success messages that have been logged.</summary>
    Private _successMsgs As UInteger = 0

    ''/// <summary>Number of warning messages that have been logged.</summary>
    Private _warningMsgs As UInteger = 0

    ''/// <summary>Number of error messages that have been logged.</summary>
    Private _errorMsgs As UInteger = 0

    ''/// <summary>Number of fatal messages that have been logged.</summary>
    Private _fatalMsgs As UInteger = 0

#End Region

End Class

