Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.ComponentModel
Imports System.IO

Namespace Tests
    Module Tests_RunAndGetOutput
        Function Test_RunAndGetOutput1() As Boolean
            Dim stdOut As String = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo hi")

            Return TestString("RunAndGetOutput1", stdOut, "hi")
        End Function

        Function Test_RunAndGetOutput2() As Boolean
            Dim stdOut As String = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo %CD%")

            Return TestString("RunAndGetOutput2", stdOut, Environment.CurrentDirectory)
        End Function

        Function Test_RunAndGetOutput3() As Boolean
            Dim stdOut As String = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo %CD%", workingDirectory:=Environment.GetEnvironmentVariable("WinDir"))

            Return TestString("RunAndGetOutput3", stdOut, Environment.GetEnvironmentVariable("WinDir"))
        End Function

        Function Test_RunAndGetOutput4() As Boolean
            Dim exitCode As Integer
            WalkmanLib.RunAndGetOutput("cmd.exe", "/c exit 5", exitCode:=exitCode)

            Return TestNumber("RunAndGetOutput4", exitCode, 5)
        End Function

        Function Test_RunAndGetOutput5() As Boolean
            Dim stdErr As String = Nothing
            WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo hi >&2", mergeStdErr:=False, stdErrReturn:=stdErr)

            Return TestString("RunAndGetOutput5", stdErr, "hi")
        End Function

        Function Test_RunAndGetOutput6() As Boolean
            Dim mergedStdOut As String = WalkmanLib.RunAndGetOutput("cmd.exe", "/c echo hi >&2 & echo hi")

            Return TestString("RunAndGetOutput6", mergedStdOut,
                "hi" & Microsoft.VisualBasic.vbNewLine &
                "StdErr: hi" & Microsoft.VisualBasic.vbNewLine &
                "ExitCode: 0")
        End Function

        Function Test_RunAndGetOutputThrows1() As Boolean
            Try
                WalkmanLib.RunAndGetOutput("nonExistantFile")
                Return TestType("RunAndGetOutputThrows1", GetType(NoException), GetType(Win32Exception))
            Catch ex As Win32Exception ' expected. none of the other Test* should run.
                Return TestNumber("RunAndGetOutputThrows1", ex.NativeErrorCode, 2) '0x2: ERROR_FILE_NOT_FOUND: The system cannot find the file specified.
            Catch ex As Exception
                Return TestType("RunAndGetOutputThrows1", ex.GetType, GetType(Win32Exception))
            End Try
        End Function

        Function Test_RunAndGetOutputThrows2() As Boolean
            Try
                WalkmanLib.RunAndGetOutput(Environment.GetEnvironmentVariable("WinDir"))
                Return TestType("RunAndGetOutputThrows2", GetType(NoException), GetType(Win32Exception))
            Catch ex As Win32Exception ' expected. none of the other Test* should run.
                Return TestNumber("RunAndGetOutputThrows2", ex.NativeErrorCode, 5) '0x5: ERROR_ACCESS_DENIED: Access is denied.
            Catch ex As Exception
                Return TestType("RunAndGetOutputThrows2", ex.GetType, GetType(Win32Exception))
            End Try
        End Function

        Function Test_RunAndGetOutputThrows3() As Boolean
            Try
                WalkmanLib.RunAndGetOutput(Path.Combine(Environment.SystemDirectory, "shell32.dll"))
                Return TestType("RunAndGetOutputThrows3", GetType(NoException), GetType(Win32Exception))
            Catch ex As Win32Exception ' expected. none of the other Test* should run.
                Return TestNumber("RunAndGetOutputThrows3", ex.NativeErrorCode, 193) '0xC1: ERROR_BAD_EXE_FORMAT: %1 is not a valid Win32 application.
            Catch ex As Exception
                Return TestType("RunAndGetOutputThrows3", ex.GetType, GetType(Win32Exception))
            End Try
        End Function
    End Module
End Namespace
