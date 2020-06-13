Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_Shortcuts
        Function Test_Shortcuts1() As Boolean
            Dim shortcutPath As String = Path.Combine(Environment.GetEnvironmentVariable("AppData"),
                                                      "Microsoft", "Windows", "Start Menu", "Programs",
                                                      "System Tools", "Command Prompt.lnk")
            If Not File.Exists(shortcutPath) Then
                Return TestString("Shortcuts1", "System shortcut doesn't exist", "System shortcut exists")
            End If

            Return TestString("Shortcuts1", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "%HOMEDRIVE%%HOMEPATH%")
        End Function

        Function Test_Shortcuts2() As Boolean
            Dim shortcutPath As String = Path.Combine(Environment.GetEnvironmentVariable("AppData"),
                                                      "Microsoft", "Windows", "Start Menu", "Programs",
                                                      "System Tools", "Command Prompt.lnk")
            If Not File.Exists(shortcutPath) Then
                Return TestString("Shortcuts2", "System shortcut doesn't exist", "System shortcut exists")
            End If

            Return TestBoolean("Shortcuts2", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), False)
        End Function

        Function Test_Shortcuts3() As Boolean
            Dim shortcutPath As String = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"),
                                                      "Microsoft", "Windows", "Start Menu", "Programs",
                                                      "Accessories", "System Tools", "Character Map.lnk")
            If Not File.Exists(shortcutPath) Then
                Return TestString("Shortcuts3", "System shortcut doesn't exist", "System shortcut exists")
            End If

            Return TestString("Shortcuts3", WalkmanLib.GetShortcutInfo(shortcutPath).Description, "Selects special characters and copies them to your document.")
        End Function

        Function Test_Shortcuts4(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts4.lnk")
            Try
                Return TestString("Shortcuts4", WalkmanLib.CreateShortcut(shortcutPath), shortcutPath)
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts5(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts5")
            Try
                Return TestString("Shortcuts5", WalkmanLib.CreateShortcut(shortcutPath), shortcutPath & ".lnk")
            Finally
                If File.Exists(shortcutPath) Then File.Delete(shortcutPath) ' this shouldn't exist - don't show warning
                DeleteFileIfExists(shortcutPath & ".lnk")
            End Try
        End Function

        Function Test_Shortcuts6(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts6.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, targetPath:="C:\Windows\notepad.exe")
            Try
                Return TestString("Shortcuts6", WalkmanLib.GetShortcutInfo(shortcutPath).TargetPath, "C:\Windows\notepad.exe")
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts7(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts7.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, arguments:="testArgument")
            Try
                Return TestString("Shortcuts7", WalkmanLib.GetShortcutInfo(shortcutPath).Arguments, "testArgument")
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts8(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts8.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, workingDirectory:="C:\Windows")
            Try
                Return TestString("Shortcuts8", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "C:\Windows")
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts9(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts9.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, iconPath:="C:\Windows\regedit.exe,0")
            Try
                Return TestString("Shortcuts9", WalkmanLib.GetShortcutInfo(shortcutPath).IconLocation, "C:\Windows\regedit.exe,0")
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts10(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts10.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, comment:="testComment")
            Try
                Return TestString("Shortcuts10", WalkmanLib.GetShortcutInfo(shortcutPath).Description, "testComment")
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts11(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts11.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, shortcutKey:="CTRL+ALT+F")
            Try
                Return TestString("Shortcuts11", WalkmanLib.GetShortcutInfo(shortcutPath).Hotkey, "Alt+Ctrl+F")
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts12(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts12.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, windowStyle:=Windows.Forms.FormWindowState.Maximized)
            Try
                Return TestNumber("Shortcuts12", WalkmanLib.GetShortcutInfo(shortcutPath).WindowStyle, 3) ' 3 = Maximised. explained in the interface commentDoc
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts13(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts13.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath)
            Try
                Return TestBoolean("Shortcuts13", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), False)
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts14(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts14.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath)
            WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, True)
            Try
                Return TestBoolean("Shortcuts14", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), True)
            Finally
                DeleteFileIfExists(shortcutPath)
            End Try
        End Function
    End Module
End Namespace
