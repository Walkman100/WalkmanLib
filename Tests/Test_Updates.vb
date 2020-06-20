Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

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
    End Module
End Namespace