Option Explicit On
Option Strict On
Option Compare Binary
Option Infer On

Imports System
Imports System.IO
Imports System.Net
Imports System.Collections.Generic
Imports System.Web.Script.Serialization
' add a reference to System.Web.Extensions

Public Partial Class WalkmanLib
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
    ''' <returns>A WalkmanLib.VersionInfo object populated with information about the latest release.</returns>
    Shared Function GetLatestVersionInfo(projectName As String, Optional projectOwner As String = "Walkman100") As VersionInfo
        ' https://stackoverflow.com/a/2904963/2999220
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
        
        ' https://stackoverflow.com/a/16655779/2999220
        Dim address As String = "https://api.github.com/repos/" & projectOwner & "/" & projectName & "/releases/latest"
        Dim client As WebClient = New WebClient()
        
        ' https://stackoverflow.com/a/22134980/2999220
        client.Headers.Add("User-Agent", "anything")
        
        Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
        Dim jsonObject As String = reader.ReadToEnd
        
        ' https://stackoverflow.com/a/38944715/2999220
        Dim jss As New JavaScriptSerializer()
        Dim jsonObjectDict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(jsonObject)
        
        Dim returnVersionInfo As VersionInfo
        returnVersionInfo.TagName = DirectCast(jsonObjectDict("tag_name"), String)
        returnVersionInfo.Title = DirectCast(jsonObjectDict("name"), String)
        returnVersionInfo.Body = DirectCast(jsonObjectDict("body"), String)
        Return returnVersionInfo
    End Function
    
    ''' <summary>Gets a download link for the latest installer released in a GitHub project.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns>Download URI in a String.</returns>
    Shared Function GetLatestDownloadLink(projectName As String, Optional projectOwner As String = "Walkman100") As String
        Dim versionString As String
        versionString = GetLatestVersionInfo(projectName, projectOwner).TagName
        
        Return "https://github.com/" & projectOwner & "/" & projectName & "/releases/download/" & versionString & "/" & projectName & "-Installer.exe"
    End Function
    
    ''' <summary>Gets the latest version released in a GitHub project. Note if the tag name is not in version format will throw an Exception.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns>The latest release version parsed as a System.Version object.</returns>
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
    ''' <param name="currentVersion">System.Version to check against.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns>True if latest version is newer than currentVersion, else False.</returns>
    Shared Function CheckIfUpdateAvailable(projectName As String, currentVersion As Version, Optional projectOwner As String = "Walkman100") As Boolean
        Dim latestVersion As Version
        latestVersion = GetLatestVersion(projectName, projectOwner)
        
        If latestVersion > currentVersion Then
            Return True
        Else
            Return False
        End If
    End Function
    
    ''' <summary>Checks if an update release is available in a GitHub project in a BackgroundWorker.</summary>
    ''' <param name="projectName">Name of the project repository.</param>
    ''' <param name="currentVersion">System.Version to check against.</param>
    ''' <param name="checkComplete">Use "New ComponentModel.RunWorkerCompletedEventHandler(AddressOf [returnSub])" and put your return sub in place of returnSub.</param>
    ''' <param name="projectOwner">Owner of the project repository. Default: Walkman100</param>
    ''' <returns>Create a Sub with the signature "sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs", and within check e.Error for an exception before using "DirectCast(e.Result, Boolean)": True if latest version is newer than currentVersion, else False.</returns>
    Shared Sub CheckIfUpdateAvailableInBackground(projectName As String, currentVersion As Version, checkComplete As ComponentModel.RunWorkerCompletedEventHandler, Optional projectOwner As String = "Walkman100")
        Dim bwUpdateCheck = New ComponentModel.BackgroundWorker()
        
        AddHandler bwUpdateCheck.DoWork, AddressOf BackgroundUpdateCheck
        AddHandler bwUpdateCheck.RunWorkerCompleted, checkComplete
        
        Dim bwArgs(3) As Object
        bwArgs(0) = projectName
        bwArgs(1) = projectOwner
        bwArgs(2) = currentVersion
        
        bwUpdateCheck.RunWorkerAsync(bwArgs)
    End Sub
    
    Private Shared Sub BackgroundUpdateCheck(sender As Object, e As ComponentModel.DoWorkEventArgs)
        Dim bwArgs(3) As Object
        bwArgs = DirectCast(e.Argument, Object())
        
        Dim latestVersion As Version = New Version() ' warning squashing
        Dim retries = 0
        Do Until 0 <> 0
            Try
                latestVersion = GetLatestVersion(bwArgs(0).ToString, bwArgs(1).ToString)
                Exit Do
            Catch ex As System.Net.WebException
                retries += 1
                If retries > 2 Then
                    Throw
                End If
            End Try
        Loop
        
        If latestVersion > DirectCast(bwArgs(2), Version) Then
            e.Result = True
        Else
            e.Result = False
        End If
    End Sub
    
    ' Example code to use the background update check:
'    Sub Main()
'        WalkmanLib.CheckIfUpdateAvailableInBackground(projectName, My.Application.Info.Version, New ComponentModel.RunWorkerCompletedEventHandler(AddressOf UpdateCheckReturn))
'    End Sub
'    Sub UpdateCheckReturn(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)
'        If Microsoft.VisualBasic.IsNothing(e.Error) Then
'            Console.WriteLine("Update available: " & DirectCast(e.Result, Boolean))
'        Else
'            Console.WriteLine("Error checking for updates: " & e.Error.Message)
'        End If
'    End Sub
End Class
