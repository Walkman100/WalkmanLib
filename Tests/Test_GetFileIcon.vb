Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Drawing
Imports System.IO

Namespace Tests
    Public Module Tests_GetFileIcon
        Function Test_GetFileIcon1(rootTestFolder As String) As Boolean
            Dim ico As Icon = WalkmanLib.GetFileIcon(Environment.GetEnvironmentVariable("WinDir"))

            Using savedImage As New DisposableFile(Path.Combine(rootTestFolder, "getFileIcon1.png"), False, False)
                ico.ToBitmap().Save(savedImage.filePath)

                Return TestBoolean("GetFileIcon1", File.Exists(savedImage.filePath), True)
            End Using
        End Function

        Function Test_GetFileIcon2(rootTestFolder As String) As Boolean
            Dim ico As Icon = WalkmanLib.GetFileIcon(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "explorer.exe"))

            Using savedImage As New DisposableFile(Path.Combine(rootTestFolder, "getFileIcon2.png"), False, False)
                ico.ToBitmap().Save(savedImage.filePath)

                Return TestBoolean("GetFileIcon2", File.Exists(savedImage.filePath), True)
            End Using
        End Function

        Function Test_GetFileIcon3() As Boolean
            Dim ex As Exception = New NoException
            Try
                Dim ico As Icon = WalkmanLib.GetFileIcon("nonexistantfile.txt")
            Catch ex2 As Exception
                ex = ex2
            End Try

            Return TestType("GetFileIcon3", ex.GetType(), GetType(FileNotFoundException))
        End Function

        Function Test_GetFileIcon4() As Boolean
            Dim ex As Exception = New NoException
            Try
                Dim ico As Icon = WalkmanLib.GetFileIcon("nonexistantfile.txt", False)
            Catch ex2 As Exception
                ex = ex2
            End Try

            Return TestType("GetFileIcon4", ex.GetType(), GetType(NoException))
        End Function
    End Module
End Namespace
