Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Drawing
Imports System.IO
Imports System.Resources
Imports System.Security.Cryptography
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

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

        Private Function Sha1File(filePath As String) As String
            Using fs As New FileStream(filePath, FileMode.Open),
                    sha1 As SHA1 = SHA1.Create
                Dim hash As Byte() = sha1.ComputeHash(fs)
                Return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant()
            End Using
        End Function

        Function Test_ExtractIcon1(rootTestFolder As String, projectRoot As String) As Boolean
            Dim resXlocation As String = Path.Combine(projectRoot, "CustomMsgBox.resx")
            If Not File.Exists(resXlocation) Then
                Return TestString("ExtractIcon1", "CustomMsgBox.resx doesn't exist", "CustomMsgBox.resx exists")
            End If

            Using extractedIcon As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon1.ico"), False, False)
                Try
                    ExtractResXIcon(resXlocation, "Win7_Information", extractedIcon)
                Catch ex As Exception
                    Return TestType("ExtractIcon1", ex.GetType, GetType(NoException))
                End Try

                Return TestString("ExtractIcon1", Sha1File(extractedIcon), "54055a0a6d465f465a444af7e784f93373376775")
            End Using
        End Function

        Function Test_ExtractIcon2(rootTestFolder As String, projectRoot As String) As Boolean
            Dim resXlocation As String = Path.Combine(projectRoot, "CustomMsgBox.resx")
            If Not File.Exists(resXlocation) Then
                Return TestString("ExtractIcon2", "CustomMsgBox.resx doesn't exist", "CustomMsgBox.resx exists")
            End If

            Using resXextractedIcon As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon2.ico"), False, False)
                Try
                    ExtractResXIcon(resXlocation, "Win7_Information", resXextractedIcon)
                Catch ex As Exception
                    Return TestType("ExtractIcon2", ex.GetType, GetType(NoException))
                End Try

                Dim extractedIcon As Icon
                Try
                    extractedIcon = WalkmanLib.ExtractIconByIndex(resXextractedIcon, 0, 256)
                Catch ex As Exception
                    Return TestType("ExtractIcon2", ex.GetType, GetType(NoException))
                End Try

                Using extractedIconByIndex As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon2ByIndex.png"), False, False)
                    extractedIcon.ToBitmap.Save(extractedIconByIndex)

                    Return TestString("ExtractIcon2", Sha1File(extractedIconByIndex), "5c6f21ec3f34bd07dec4e38dc9d83cdfa3ce5915")
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

            Using targetIcon As New DisposableFile(Path.Combine(rootTestFolder, "extractIcon3.png"), False, False)
                extractedIcon.ToBitmap.Save(targetIcon)

                Return TestString("ExtractIcon3", Sha1File(targetIcon), "b1419f59aea45aa1b1aee38d3d77cdaae9ff3916")
            End Using
        End Function

        Function Test_PickIconDialog1() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ESC}")
                     End Sub)

            Dim result As Boolean = WalkmanLib.PickIconDialogShow(Path.Combine(Environment.SystemDirectory, "shell32.dll"), 32)

            Return TestBoolean("PickIconDialog1", result, False)
        End Function

        Function Test_PickIconDialog2() As Boolean
            Dim filePath As String = Path.Combine(Environment.SystemDirectory, "shell32.dll")
            Dim iconIndex As Integer = 32

            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ENTER}")
                     End Sub)
            Dim result As Boolean = WalkmanLib.PickIconDialogShow(filePath, iconIndex)

            If Not result Then
                Return TestBoolean("PickIconDialog2", result, True)
            End If

            Return TestString("PickIconDialog2", filePath, Path.Combine(Environment.SystemDirectory, "shell32.dll"))
        End Function

        Function Test_PickIconDialog3() As Boolean
            Dim filePath As String = Path.Combine(Environment.SystemDirectory, "shell32.dll")
            Dim iconIndex As Integer = 32

            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{TAB 2}{DOWN}{ENTER}")
                     End Sub)
            Dim result As Boolean = WalkmanLib.PickIconDialogShow(filePath, iconIndex)

            If Not result Then
                Return TestBoolean("PickIconDialog3", result, True)
            End If

            Return TestNumber("PickIconDialog3", iconIndex, 33)
        End Function
    End Module
End Namespace
