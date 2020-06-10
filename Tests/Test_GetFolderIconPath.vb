Imports System
Imports System.IO

Namespace Tests
    Module Tests
        Function Test_GetFolderIconPath1(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "getFolderIconPath1")

            Directory.CreateDirectory(testPath)
            File.WriteAllLines(Path.Combine(testPath, "desktop.ini"), {
                "[.ShellClassInfo]",
                "IconResource=testIconPath,23"
            })

            Return TestString("GetFolderIconPath1", WalkmanLib.GetFolderIconPath(testPath), testPath & Path.DirectorySeparatorChar & "testIconPath,23")
        End Function

        Function Test_GetFolderIconPath2(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "getFolderIconPath2")

            Directory.CreateDirectory(testPath)
            File.WriteAllLines(Path.Combine(testPath, "desktop.ini"), {
                "[.ShellClassInfo]",
                "IconFile=testIconPath",
                "IconIndex=23"
            })

            Return TestString("GetFolderIconPath2", WalkmanLib.GetFolderIconPath(testPath), testPath & Path.DirectorySeparatorChar & "testIconPath,23")
        End Function

        Function Test_GetFolderIconPath3(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "getFolderIconPath3")

            Directory.CreateDirectory(testPath)
            File.WriteAllLines(Path.Combine(testPath, "desktop.ini"), {
                "[.ShellClassInfo]",
                "IconResource=D:\test\testIconPath,23"
            })

            Return TestString("GetFolderIconPath3", WalkmanLib.GetFolderIconPath(testPath), "D:\test\testIconPath,23")
        End Function

        Function Test_GetFolderIconPath4(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "getFolderIconPath4")

            Directory.CreateDirectory(testPath)
            File.WriteAllLines(Path.Combine(testPath, "desktop.ini"), {
                "[.ShellClassInfo]",
                "IconResource=%SystemRoot%\system32\imageres.dll,-184"
            })

            Return TestString("GetFolderIconPath4", WalkmanLib.GetFolderIconPath(testPath), Environment.GetEnvironmentVariable("SystemRoot") & "\system32\imageres.dll,-184")
        End Function

        Function Test_GetFolderIconPath5(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "getFolderIconPath5")

            Directory.CreateDirectory(testPath)
            File.WriteAllLines(Path.Combine(testPath, "desktop.ini"), {
                "[.ShellClassInfo]"
            })

            Return TestString("GetFolderIconPath5", WalkmanLib.GetFolderIconPath(testPath), "no icon found")
        End Function
    End Module
End Namespace
