Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
' add a reference to System.Web.Extensions

Partial Public Class WalkmanLib
    Structure VersionInfo
        ''' <summary>Name of the tag.</summary>
        Public TagName As String

        ''' <summary>Title of the release.</summary>
        Public Title As String

        ''' <summary>Body of the release. Consists of raw release text, optionally parse it as markdown before displaying it.</summary>
        Public Body As String
    End Structure

    ''' <summary>Gets information about the latest release in a GitHub project.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns>A <see cref="VersionInfo"/> object populated with information about the latest release.</returns>
    Shared Function GetLatestVersionInfo(projectName As String, Optional projectOwner As String = "Walkman100") As VersionInfo
        ' https://stackoverflow.com/a/2904963/2999220
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        ' https://stackoverflow.com/a/16655779/2999220
        Dim address As String = String.Format("https://api.github.com/repos/{0}/{1}/releases/latest", projectOwner, projectName)
        Dim client As New WebClient()

        ' https://stackoverflow.com/a/22134980/2999220
        client.Headers.Add("User-Agent", "anything")

        Dim reader As New StreamReader(client.OpenRead(address))
        Dim jsonObject As String = reader.ReadToEnd

        ' https://stackoverflow.com/a/38944715/2999220
        Dim jss As New JavaScriptSerializer()
        Dim jsonObjectDict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(jsonObject)

        Return New VersionInfo With {
            .TagName = DirectCast(jsonObjectDict("tag_name"), String),
            .Title = DirectCast(jsonObjectDict("name"), String),
            .Body = DirectCast(jsonObjectDict("body"), String)
        }
    End Function

    ''' <summary>Gets a download link for the latest installer released in a GitHub project.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <param name="fileName">Name of the file. Defaults to "<paramref name="projectName"/>-Installer.exe". Use {0} to replace with the version string.</param>
    ''' <returns>Download URI in a <see cref="String"/>.</returns>
    Shared Function GetLatestDownloadLink(projectName As String, Optional projectOwner As String = "Walkman100", Optional fileName As String = Nothing) As String
        Dim versionString As String
        versionString = GetLatestVersionInfo(projectName, projectOwner).TagName

        If fileName Is Nothing Then
            fileName = projectName & "-Installer.exe"
        Else
            fileName = String.Format(fileName, versionString)
        End If

        Return String.Format("https://github.com/{0}/{1}/releases/download/{2}/{3}", projectOwner, projectName, versionString, fileName)
    End Function

    ''' <summary>Gets the latest version released in a GitHub project. Note if the tag name is not in version format will throw an Exception.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns>The latest release version parsed as a <see cref="Version"/> object.</returns>
    Shared Function GetLatestVersion(projectName As String, Optional projectOwner As String = "Walkman100") As Version
        Dim versionString As String
        versionString = GetLatestVersionInfo(projectName, projectOwner).TagName

        If versionString.StartsWith("v") Then
            versionString = versionString.Substring(1)
        End If

        Return Version.Parse(versionString)
    End Function

    ''' <summary>Checks if an update release is available in a GitHub project.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="currentVersion"><see cref="Version"/> to check against.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns><see langword="True"/> if latest version is newer than <paramref name="currentVersion"/>, else <see langword="False"/>.</returns>
    Shared Function CheckIfUpdateAvailable(projectName As String, currentVersion As Version, Optional projectOwner As String = "Walkman100") As Boolean
        Dim latestVersion As Version = GetLatestVersion(projectName, projectOwner)

        Return latestVersion > currentVersion
    End Function

    ''' <summary>Checks if an update release is available in a GitHub project in a BackgroundWorker.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="currentVersion"><see cref="Version"/> to check against.</param>
    ''' <param name="checkComplete">Use "<c>New ComponentModel.RunWorkerCompletedEventHandler(AddressOf [returnSub])</c>" and put your return sub in place of <c>returnSub</c>.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns>
    ''' Create a Sub with the signature "<c>sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs</c>", and within check <c>e.Error</c> for an exception before
    ''' using "<c>DirectCast(e.Result, Boolean)</c>": <see langword="True"/> if latest version is newer than <paramref name="currentVersion"/>, else <see langword="False"/>.
    ''' </returns>
    Shared Sub CheckIfUpdateAvailableInBackground(projectName As String, currentVersion As Version, checkComplete As ComponentModel.RunWorkerCompletedEventHandler, Optional projectOwner As String = "Walkman100")
        Dim bwUpdateCheck As New ComponentModel.BackgroundWorker()

        AddHandler bwUpdateCheck.DoWork, AddressOf BackgroundUpdateCheck
        AddHandler bwUpdateCheck.RunWorkerCompleted, checkComplete

        bwUpdateCheck.RunWorkerAsync(New Object() {projectName, projectOwner, currentVersion})
    End Sub

    Private Shared Sub BackgroundUpdateCheck(sender As Object, e As ComponentModel.DoWorkEventArgs)
        Dim projectName As String = DirectCast(DirectCast(e.Argument, Object())(0), String)
        Dim projectOwner As String = DirectCast(DirectCast(e.Argument, Object())(1), String)
        Dim currentVersion As Version = DirectCast(DirectCast(e.Argument, Object())(2), Version)

        Dim latestVersion As New Version() ' warning squashing
        Dim retries As Integer = 0
        Do Until 0 <> 0
            Try
                latestVersion = GetLatestVersion(projectName, projectOwner)
                Exit Do
            Catch ex As WebException
                retries += 1
                If retries > 2 Then
                    Throw
                End If
            End Try
        Loop

        e.Result = latestVersion > currentVersion
    End Sub

    ' Example code to use the background update check:
    'Sub Main()
    '    WalkmanLib.CheckIfUpdateAvailableInBackground(projectName, My.Application.Info.Version, New ComponentModel.RunWorkerCompletedEventHandler(AddressOf UpdateCheckReturn))
    'End Sub
    'Sub UpdateCheckReturn(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)
    '    If Microsoft.VisualBasic.IsNothing(e.Error) Then
    '        Console.WriteLine("Update available: " & DirectCast(e.Result, Boolean))
    '    Else
    '        Console.WriteLine("Error checking for updates: " & e.Error.Message)
    '    End If
    'End Sub
End Class
