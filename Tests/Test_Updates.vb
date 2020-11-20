Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Net
Imports System.Threading

Namespace Tests
    Module Tests_Updates
        Private Const projectName As String = "PortActions"

        Function Test_Updates1() As Boolean
            Return TestString("Updates1", WalkmanLib.GetLatestVersionInfo(projectName).TagName, "v1.0")
        End Function

        Function Test_Updates2() As Boolean
            Return TestString("Updates2", WalkmanLib.GetLatestVersionInfo(projectName).Title, "Version 1.0 Initial Release")
        End Function

        Function Test_Updates3() As Boolean
            Dim expectedBody As String = "Everything is functional except for the <kbd>More Info</kbd> button, which does nothing." & Microsoft.VisualBasic.vbLf
            Return TestString("Updates3", WalkmanLib.GetLatestVersionInfo(projectName).Body, expectedBody)
        End Function

        Function Test_Updates4() As Boolean
            Dim expectedLink As String = "https://github.com/Walkman100/PortActions/releases/download/v1.0/PortActions-Installer.exe"
            Return TestString("Updates4", WalkmanLib.GetLatestDownloadLink(projectName), expectedLink)
        End Function

        Function Test_Updates5() As Boolean
            Return TestString("Updates5", WalkmanLib.GetLatestVersion(projectName).ToString, "1.0")
        End Function

        Function Test_Updates6() As Boolean
            Return TestBoolean("Updates6", WalkmanLib.CheckIfUpdateAvailable(projectName, New Version(1, 0)), False)
        End Function

        Function Test_Updates7() As Boolean
            Return TestBoolean("Updates7", WalkmanLib.CheckIfUpdateAvailable(projectName, New Version(0, 9, 9)), True)
        End Function

        Function Test_Updates8() As Boolean
            delegateCallComplete = False
            delegateReturn = Nothing

            WalkmanLib.CheckIfUpdateAvailableInBackground(projectName, New Version(0, 9, 9), New ComponentModel.RunWorkerCompletedEventHandler(AddressOf UpdateCheckReturn))

            Dim count As Integer = 0
            Do Until delegateCallComplete
                Thread.Sleep(100)

                count += 1
                If count > 100 Then
                    Exit Do
                End If
            Loop

            Return TestString("Updates8", delegateReturn, "Update available: True")
        End Function

        Dim delegateCallComplete As Boolean
        Dim delegateReturn As String
        Sub UpdateCheckReturn(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)
            If e.Error Is Nothing Then
                delegateReturn = "Update available: " & DirectCast(e.Result, Boolean)
            Else
                delegateReturn = "Error checking for updates: " & e.Error.Message
            End If
            delegateCallComplete = True
        End Sub

        Function Test_UpdateThrows1() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetLatestVersionInfo("NonExistantProject")
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("UpdateThrows1", ex.GetType, GetType(WebException))
        End Function

        Function Test_UpdateThrows2() As Boolean
            Try
                WalkmanLib.GetLatestVersionInfo("NonExistantProject")
                Return TestType("UpdateThrows2", GetType(NoException), GetType(WebException))
            Catch ex As WebException
                If TypeOf ex.Response Is HttpWebResponse Then
                    Dim response As HttpWebResponse = DirectCast(ex.Response, HttpWebResponse)
                    ' expected result. all other Return Test* are unexpected.
                    Return TestNumber("UpdateThrows2", response.StatusCode, HttpStatusCode.NotFound)
                Else
                    Return TestType("UpdateThrows2", ex.Response.GetType, GetType(HttpWebResponse))
                End If
            Catch ex As Exception
                Return TestType("UpdateThrows2", ex.GetType, GetType(WebException))
            End Try
        End Function

        Function Test_UpdateThrows3() As Boolean
            delegateCallComplete = False
            delegateReturn = Nothing

            WalkmanLib.CheckIfUpdateAvailableInBackground("NonExistantProject", New Version(0, 9, 9), New ComponentModel.RunWorkerCompletedEventHandler(AddressOf UpdateCheckReturn))

            Dim count As Integer = 0
            Do Until delegateCallComplete
                Thread.Sleep(100)

                count += 1
                If count > 100 Then
                    Exit Do
                End If
            Loop

            Return TestString("UpdateThrows3", delegateReturn, "Error checking for updates: The remote server returned an error: (404) Not Found.")
        End Function
    End Module
End Namespace
