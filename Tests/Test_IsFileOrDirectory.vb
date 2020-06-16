Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.IO

Namespace Tests
    Module Tests_IsFileOrDirectory
        Function Test_IsFileOrDirectory1(rootTestFolder As String) As Boolean
            Using testDir As DisposableDirectory = New DisposableDirectory(Path.Combine(rootTestFolder, "isFileOrDirectory1"))
                Return TestNumber("IsFileOrDirectory1", WalkmanLib.IsFileOrDirectory(testDir.dirPath), PathEnum.Exists Or PathEnum.IsDirectory)
            End Using
        End Function

        Function Test_IsFileOrDirectory2(rootTestFolder As String) As Boolean
            Using testFile As DisposableFile = New DisposableFile(Path.Combine(rootTestFolder, "isFileOrDirectory2.txt"))
                Return TestNumber("IsFileOrDirectory2", WalkmanLib.IsFileOrDirectory(testFile.filePath), PathEnum.Exists Or PathEnum.IsFile)
            End Using
        End Function

        Function Test_IsFileOrDirectory3() As Boolean
            Return TestNumber("IsFileOrDirectory3", WalkmanLib.IsFileOrDirectory("C:\"), PathEnum.Exists Or PathEnum.IsDirectory Or PathEnum.IsDrive)
        End Function

        Function Test_IsFileOrDirectory4() As Boolean
            Return TestNumber("IsFileOrDirectory4", WalkmanLib.IsFileOrDirectory("C:\nonexistantpath"), PathEnum.NotFound)
        End Function

        Function Test_IsFileOrDirectory5() As Boolean
            Return TestNumber("IsFileOrDirectory5", WalkmanLib.IsFileOrDirectory("test:test\test"), PathEnum.NotFound)
        End Function

        Function Test_IsFileOrDirectory6() As Boolean
            Return TestNumber("IsFileOrDirectory6", WalkmanLib.IsFileOrDirectory("~!@#$%^&*(){}[]/?=+-_\|"), PathEnum.NotFound)
        End Function

        Function Test_IsFileOrDirectory7() As Boolean
            Return TestNumber("IsFileOrDirectory7", WalkmanLib.IsFileOrDirectory("M:\"), PathEnum.Exists Or PathEnum.IsDrive)
        End Function
    End Module
End Namespace
