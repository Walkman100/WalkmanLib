Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO
Imports System.Threading

Namespace Tests
    Module Tests_Attributes
        Private Function TestGetAttributes(path As String) As FileAttributes
            ' have to clear Compressed attribute, as tests assume it isn't set, and decompressing files involves P/Invoke
            Dim fAttr As FileAttributes = File.GetAttributes(path)
            If fAttr = FileAttributes.Compressed Then
                Return FileAttributes.Normal
            Else
                Return fAttr And Not FileAttributes.Compressed
            End If
        End Function

        Function Test_Attributes1(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest1.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                If Not TestNumber("Attributes1.1", TestGetAttributes(testFile.filePath), FileAttributes.Normal) Then returnVal = False

                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Hidden)
                If Not TestNumber("Attributes1.2", TestGetAttributes(testFile.filePath), FileAttributes.Hidden) Then returnVal = False

                WalkmanLib.SetAttribute(testFile.filePath, TestGetAttributes(testFile.filePath) Or FileAttributes.System)
                If Not TestNumber("Attributes1.3", TestGetAttributes(testFile.filePath), FileAttributes.Hidden Or FileAttributes.System) Then returnVal = False

                Return returnVal
            End Using
        End Function

        Function Test_Attributes2(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest2.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Archive)

                WalkmanLib.AddAttribute(testFile.filePath, FileAttributes.Normal)
                If Not TestNumber("Attributes2.1", TestGetAttributes(testFile.filePath), FileAttributes.Archive) Then returnVal = False

                WalkmanLib.AddAttribute(testFile.filePath, FileAttributes.Hidden)
                If Not TestNumber("Attributes2.2", TestGetAttributes(testFile.filePath), FileAttributes.Archive Or FileAttributes.Hidden) Then returnVal = False

                WalkmanLib.AddAttribute(testFile.filePath, FileAttributes.System)
                If Not TestNumber("Attributes2.3", TestGetAttributes(testFile.filePath), FileAttributes.Archive Or FileAttributes.Hidden Or FileAttributes.System) Then returnVal = False

                Return returnVal
            End Using
        End Function

        Function Test_Attributes3(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True

            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest3.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal Or FileAttributes.ReadOnly Or FileAttributes.Hidden Or FileAttributes.System)

                WalkmanLib.RemoveAttribute(testFile.filePath, FileAttributes.Normal)
                If Not TestNumber("Attributes3.1", TestGetAttributes(testFile.filePath), FileAttributes.ReadOnly Or FileAttributes.Hidden Or FileAttributes.System) Then returnVal = False

                WalkmanLib.RemoveAttribute(testFile.filePath, FileAttributes.Hidden)
                If Not TestNumber("Attributes3.2", TestGetAttributes(testFile.filePath), FileAttributes.ReadOnly Or FileAttributes.System) Then returnVal = False

                WalkmanLib.RemoveAttribute(testFile.filePath, FileAttributes.ReadOnly Or FileAttributes.System)
                If Not TestNumber("Attributes3.3", TestGetAttributes(testFile.filePath), FileAttributes.Normal) Then returnVal = False

                Return returnVal
            End Using
        End Function

        Function Test_Attributes4(rootTestFolder As String) As Boolean
            Dim returnVal As Boolean = True

            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "setAttributeTest4.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.System Or FileAttributes.Hidden)

                WalkmanLib.ChangeAttribute(testFile.filePath, FileAttributes.Hidden, False)
                If Not TestNumber("Attributes4.1", TestGetAttributes(testFile.filePath), FileAttributes.System) Then returnVal = False

                WalkmanLib.ChangeAttribute(testFile.filePath, FileAttributes.Hidden, True)
                If Not TestNumber("Attributes4.2", TestGetAttributes(testFile.filePath), FileAttributes.System Or FileAttributes.Hidden) Then returnVal = False

                WalkmanLib.ChangeAttribute(testFile.filePath, FileAttributes.Archive, True)
                If Not TestNumber("Attributes4.3", TestGetAttributes(testFile.filePath), FileAttributes.System Or FileAttributes.Hidden Or FileAttributes.Archive) Then returnVal = False

                Return returnVal
            End Using
        End Function

        Function Test_Attributes5() As Boolean
            Dim returnVal As Boolean = True

            Dim testPath As String = "C:\Windows"

            If Not TestBoolean("Attributes5.1", WalkmanLib.SetAttribute(testPath, FileAttributes.Hidden, AddressOf Test_Attributes5_delegate), False) Then returnVal = False
            If Not TestBoolean("Attributes5.2", WalkmanLib.AddAttribute(testPath, FileAttributes.Hidden, AddressOf Test_Attributes5_delegate), False) Then returnVal = False
            If Not TestBoolean("Attributes5.3", WalkmanLib.RemoveAttribute(testPath, FileAttributes.Hidden, AddressOf Test_Attributes5_delegate), False) Then returnVal = False
            If Not TestBoolean("Attributes5.4", WalkmanLib.ChangeAttribute(testPath, FileAttributes.Hidden, True, AddressOf Test_Attributes5_delegate), False) Then returnVal = False

            Return returnVal
        End Function
        Sub Test_Attributes5_delegate(ex As Exception)
        End Sub

        Function Test_Attributes6() As Boolean
            Dim testPath As String = "C:\Windows"

            delegateHasBeenCalled = False
            delegateEx = Nothing

            WalkmanLib.SetAttribute(testPath, FileAttributes.Hidden, AddressOf Test_Attributes6_delegate)

            Do Until delegateHasBeenCalled = True
                Thread.Sleep(10)
            Loop

            Return TestType("Attributes6", delegateEx.GetType(), GetType(UnauthorizedAccessException))
        End Function

        Dim delegateHasBeenCalled As Boolean
        Dim delegateEx As Exception
        Sub Test_Attributes6_delegate(ex As Exception)
            delegateEx = ex
            delegateHasBeenCalled = True
        End Sub
    End Module
End Namespace
