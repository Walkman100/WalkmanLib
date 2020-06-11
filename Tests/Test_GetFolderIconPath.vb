Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_GetFolderIconPath
        Function Test_GetFolderIconPath1(rootTestFolder As String) As Boolean
            Using testDir As DisposableDirectory = New DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath1"))
                File.WriteAllLines(Path.Combine(testDir.dirPath, "desktop.ini"), {
                    "[.ShellClassInfo]",
                    "IconResource=testIconPath,23"
                })

                Return TestString("GetFolderIconPath1", WalkmanLib.GetFolderIconPath(testDir.dirPath), testDir.dirPath & Path.DirectorySeparatorChar & "testIconPath,23")
            End Using
        End Function

        Function Test_GetFolderIconPath2(rootTestFolder As String) As Boolean
            Using testDir As DisposableDirectory = New DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath2"))
                File.WriteAllLines(Path.Combine(testDir.dirPath, "desktop.ini"), {
                    "[.ShellClassInfo]",
                    "IconFile=testIconPath",
                    "IconIndex=23"
                })

                Return TestString("GetFolderIconPath2", WalkmanLib.GetFolderIconPath(testDir.dirPath), testDir.dirPath & Path.DirectorySeparatorChar & "testIconPath,23")
            End Using
        End Function

        Function Test_GetFolderIconPath3(rootTestFolder As String) As Boolean
            Using testDir As DisposableDirectory = New DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath3"))
                File.WriteAllLines(Path.Combine(testDir.dirPath, "desktop.ini"), {
                    "[.ShellClassInfo]",
                    "IconResource=D:\test\testIconPath,23"
                })

                Return TestString("GetFolderIconPath3", WalkmanLib.GetFolderIconPath(testDir.dirPath), "D:\test\testIconPath,23")
            End Using
        End Function

        Function Test_GetFolderIconPath4(rootTestFolder As String) As Boolean
            Using testDir As DisposableDirectory = New DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath4"))
                File.WriteAllLines(Path.Combine(testDir.dirPath, "desktop.ini"), {
                    "[.ShellClassInfo]",
                    "IconResource=%SystemRoot%\system32\imageres.dll,-184"
                })

                Return TestString("GetFolderIconPath4", WalkmanLib.GetFolderIconPath(testDir.dirPath), Environment.GetEnvironmentVariable("SystemRoot") & "\system32\imageres.dll,-184")
            End Using
        End Function

        Function Test_GetFolderIconPath5(rootTestFolder As String) As Boolean
            Using testDir As DisposableDirectory = New DisposableDirectory(Path.Combine(rootTestFolder, "getFolderIconPath5"))
                File.WriteAllLines(Path.Combine(testDir.dirPath, "desktop.ini"), {
                    "[.ShellClassInfo]"
                })

                Return TestString("GetFolderIconPath5", WalkmanLib.GetFolderIconPath(testDir.dirPath), "no icon found")
            End Using
        End Function
    End Module
End Namespace
