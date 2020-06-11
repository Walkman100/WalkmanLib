Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

Namespace Tests
    Partial Module Tests
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
    End Module
End Namespace
