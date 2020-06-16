Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_GetOpenWith
        Function Test_GetOpenWith1() As Boolean
            Dim testPath As String = Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "explorer.exe").ToLower()
            Return TestString("GetOpenWith1", WalkmanLib.GetOpenWith(testPath).ToLower(), testPath)
        End Function

        Function Test_GetOpenWith2(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith2.bat"))
                Dim pathRoot As String = Path.GetPathRoot(rootTestFolder).Remove(2)
                Dim fsUtilOutput As String = WalkmanLib.RunAndGetOutput("fsutil.exe", "8dot3name query " & pathRoot)

                If fsUtilOutput.EndsWith("is disabled on " & pathRoot) Then
                    Return TestString("GetOpenWith2", WalkmanLib.GetOpenWith(testFile.filePath), testFile.filePath)
                ElseIf fsUtilOutput.EndsWith("is enabled on " & pathRoot) Then
                    Return TestString("GetOpenWith2", WalkmanLib.GetOpenWith(testFile.filePath), Path.Combine(rootTestFolder, "TESTOP~1.BAT"))
                Else
                    Return TestString("GetOpenWith2", fsUtilOutput, "Valid 8dot3 info")
                End If
            End Using
        End Function

        Function Test_GetOpenWith3(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith3.cmd"))
                Dim pathRoot As String = Path.GetPathRoot(rootTestFolder).Remove(2)
                Dim fsUtilOutput As String = WalkmanLib.RunAndGetOutput("fsutil.exe", "8dot3name query " & pathRoot)

                If fsUtilOutput.EndsWith("is disabled on " & pathRoot) Then
                    Return TestString("GetOpenWith3", WalkmanLib.GetOpenWith(testFile.filePath), testFile.filePath)
                ElseIf fsUtilOutput.EndsWith("is enabled on " & pathRoot) Then
                    Return TestString("GetOpenWith3", WalkmanLib.GetOpenWith(testFile.filePath), Path.Combine(rootTestFolder, "TESTOP~1.CMD"))
                Else
                    Return TestString("GetOpenWith3", fsUtilOutput, "Valid 8dot3 info")
                End If
            End Using
        End Function

        Function Test_GetOpenWith4(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith4.randomextension"))
                Return TestString("GetOpenWith4", WalkmanLib.GetOpenWith(testFile.filePath),
                                  "Filetype not associated!")
            End Using
        End Function

        Function Test_GetOpenWith5(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith5.txt"))
                Return TestString("GetOpenWith5", WalkmanLib.GetOpenWith(testFile.filePath).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "notepad.exe").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith6(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith6.url"))
                Return TestString("GetOpenWith6", WalkmanLib.GetOpenWith(testFile.filePath).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "ieframe.dll").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith7(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith7.vbs"))
                Return TestString("GetOpenWith7", WalkmanLib.GetOpenWith(testFile.filePath).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "wscript.exe").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith8(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith8.cat"))
                Return TestString("GetOpenWith8", WalkmanLib.GetOpenWith(testFile.filePath).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "cryptext.dll").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith9(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith9.cer"))
                Return TestString("GetOpenWith9", WalkmanLib.GetOpenWith(testFile.filePath).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "cryptext.dll").ToLower())
            End Using
        End Function
    End Module
End Namespace
