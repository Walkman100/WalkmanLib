Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Resources

Namespace Tests
    Module Tests_Icons
        Private Sub ExtractResXIcon(resXpath As String, resourceName As String, outputPath As String)
            Using resXset As New ResXResourceSet(resXpath)
                Dim resXobject As Object = resXset.GetObject(resourceName)
                If Not TypeOf resXobject Is Icon Then
                    Throw New InvalidDataException("ResX Object was not of type Icon. Got type: " & resXobject.GetType().FullName)
                End If
                Dim resXicon As Icon = DirectCast(resXobject, Icon)

                Using fs As New FileStream(outputPath, FileMode.Create)
                    resXicon.Save(fs)
                End Using
            End Using
        End Sub

        Function Test_ExtractIcon1(rootTestFolder As String, projectRoot As String) As Boolean
            Dim resXlocation As String = Path.Combine(projectRoot, "CustomMsgBox.resx")
            If Not File.Exists(resXlocation) Then
                Return TestString("ExtractIcon1", "CustomMsgBox.resx doesn't exist", "CustomMsgBox.resx exists")
            End If

            Using extractedIcon As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon1.ico"), False, False)
                Try
                    ExtractResXIcon(resXlocation, "Win7_Information", extractedIcon.filePath)
                Catch ex As Exception
                    Return TestType("ExtractIcon1", ex.GetType, GetType(NoException))
                End Try

                Return TestNumber("ExtractIcon1", New FileInfo(extractedIcon.filePath).Length, 82979)
            End Using
        End Function

        Function Test_ExtractIcon2(rootTestFolder As String, projectRoot As String) As Boolean
            Dim resXlocation As String = Path.Combine(projectRoot, "CustomMsgBox.resx")
            If Not File.Exists(resXlocation) Then
                Return TestString("ExtractIcon2", "CustomMsgBox.resx doesn't exist", "CustomMsgBox.resx exists")
            End If

            Using resXextractedIcon As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon2.ico"), False, False)
                Try
                    ExtractResXIcon(resXlocation, "Win7_Information", resXextractedIcon.filePath)
                Catch ex As Exception
                    Return TestType("ExtractIcon2", ex.GetType, GetType(NoException))
                End Try

                Dim extractedIcon As Icon
                Try
                    extractedIcon = WalkmanLib.ExtractIconByIndex(resXextractedIcon.filePath, 0, 256)
                Catch ex As Exception
                    Return TestType("ExtractIcon2", ex.GetType, GetType(NoException))
                End Try

                Using extractedIconByIndex As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon2ByIndex.ico"), False, False)
                    Using fs As New FileStream(extractedIconByIndex.filePath, FileMode.Create)
                        extractedIcon.Save(fs)
                    End Using

                    Return TestNumber("ExtractIcon2", New FileInfo(extractedIconByIndex.filePath).Length, 41086)
                    '                                           extractbyindex extracts the 16-bit icon for some reason...
                End Using
            End Using
        End Function

        Function Test_ExtractIcon3(rootTestFolder As String) As Boolean
            Dim extractedIcon As Icon
            Try
                extractedIcon = WalkmanLib.ExtractIconByIndex(Path.Combine(Environment.SystemDirectory, "shell32.dll"), 32)
            Catch ex As Exception
                Return TestType("ExtractIcon3", ex.GetType, GetType(NoException))
            End Try

            Using targetIcon As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon3.ico"), False, False)
                Using fs As New FileStream(targetIcon.filePath, FileMode.Create)
                    extractedIcon.Save(fs)
                End Using

                Return TestNumber("ExtractIcon3", New FileInfo(targetIcon.filePath).Length, 41086)
            End Using
        End Function
    End Module
End Namespace
