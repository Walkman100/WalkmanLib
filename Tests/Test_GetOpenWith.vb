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

        Function Is8Dot3Enabled(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "test file.txt"))
                Return File.Exists(Path.Combine(rootTestFolder, "TESTFI~1.TXT"))
            End Using
        End Function

        Function Test_GetOpenWith2(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith2.bat"))
                If Is8Dot3Enabled(rootTestFolder) Then
                    Return TestString("GetOpenWith2", WalkmanLib.GetOpenWith(testFile), Path.Combine(rootTestFolder, "TESTOP~1.BAT"))
                Else
                    Return TestString("GetOpenWith2", WalkmanLib.GetOpenWith(testFile), testFile)
                End If
            End Using
        End Function

        Function Test_GetOpenWith3(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith3.cmd"))
                If Is8Dot3Enabled(rootTestFolder) Then
                    Return TestString("GetOpenWith3", WalkmanLib.GetOpenWith(testFile), Path.Combine(rootTestFolder, "TESTOP~1.CMD"))
                Else
                    Return TestString("GetOpenWith3", WalkmanLib.GetOpenWith(testFile), testFile)
                End If
            End Using
        End Function

        Function Test_GetOpenWith4(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith4.randomextension"))
                Return TestString("GetOpenWith4", WalkmanLib.GetOpenWith(testFile),
                                  "Filetype not associated!")
            End Using
        End Function

        Function Test_GetOpenWith5(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith5.txt"))
                Return TestString("GetOpenWith5", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "notepad.exe").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith6(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith6.url"))
                Return TestString("GetOpenWith6", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "ieframe.dll").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith7(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith7.vbs"))
                Return TestString("GetOpenWith7", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "wscript.exe").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith8(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith8.cat"))
                Return TestString("GetOpenWith8", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "cryptext.dll").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith9(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "testOpenWith9.cer"))
                Return TestString("GetOpenWith9", WalkmanLib.GetOpenWith(testFile).ToLower(),
                                  Path.Combine(Environment.SystemDirectory, "cryptext.dll").ToLower())
            End Using
        End Function

        Function Test_GetOpenWith10(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "testOpenWith10.txt")
            Return TestString("GetOpenWith10", WalkmanLib.GetOpenWith(testPath), "File not found!")
        End Function
    End Module
End Namespace
