Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_Shortcuts
        Function Test_Shortcuts1() As Boolean
            Dim shortcutPath As String = Path.Combine(Environment.GetEnvironmentVariable("AppData"), "Microsoft",
                                                      "Windows", "Start Menu", "Programs", "System Tools",
                                                      "Command Prompt.lnk")
            If Not File.Exists(shortcutPath) Then
                Return TestString("Shortcuts1", "System shortcut doesn't exist", "System shortcut exists")
            End If

            Return TestString("Shortcuts1", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "%HOMEDRIVE%%HOMEPATH%")
        End Function

        Function Test_Shortcuts2() As Boolean
            Dim shortcutPath As String = Path.Combine(Environment.GetEnvironmentVariable("AppData"), "Microsoft",
                                                      "Windows", "Start Menu", "Programs", "System Tools",
                                                      "Command Prompt.lnk")
            If Not File.Exists(shortcutPath) Then
                Return TestString("Shortcuts2", "System shortcut doesn't exist", "System shortcut exists")
            End If

            Return TestBoolean("Shortcuts2", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), False)
        End Function

        Function Test_Shortcuts3() As Boolean
            Dim shortcutPath As String = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Microsoft",
                                                      "Windows", "Start Menu", "Programs", "Accessories", "System Tools",
                                                      "Character Map.lnk")
            If Not File.Exists(shortcutPath) Then
                Return TestString("Shortcuts3", "System shortcut doesn't exist", "System shortcut exists")
            End If

            Return TestString("Shortcuts3", WalkmanLib.GetShortcutInfo(shortcutPath).Description,
                              "Selects special characters and copies them to your document.")
        End Function

        Function Test_Shortcuts4(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "shortcuts4.lnk"), False, False)
                Return TestString("Shortcuts4", WalkmanLib.CreateShortcut(testFile.filePath), testFile.filePath)
            End Using
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
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestString("Shortcuts6", WalkmanLib.GetShortcutInfo(shortcutPath).TargetPath, "C:\Windows\notepad.exe")
            End Using
        End Function

        Function Test_Shortcuts7(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts7.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, arguments:="testArgument")
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestString("Shortcuts7", WalkmanLib.GetShortcutInfo(shortcutPath).Arguments, "testArgument")
            End Using
        End Function

        Function Test_Shortcuts8(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts8.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, workingDirectory:="C:\Windows")
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestString("Shortcuts8", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "C:\Windows")
            End Using
        End Function

        Function Test_Shortcuts9(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts9.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, iconPath:="C:\Windows\regedit.exe,0")
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestString("Shortcuts9", WalkmanLib.GetShortcutInfo(shortcutPath).IconLocation, "C:\Windows\regedit.exe,0")
            End Using
        End Function

        Function Test_Shortcuts10(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts10.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, comment:="testComment")
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestString("Shortcuts10", WalkmanLib.GetShortcutInfo(shortcutPath).Description, "testComment")
            End Using
        End Function

        Function Test_Shortcuts11(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts11.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, shortcutKey:="CTRL+ALT+F")
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestString("Shortcuts11", WalkmanLib.GetShortcutInfo(shortcutPath).Hotkey, "Alt+Ctrl+F")
            End Using
        End Function

        Function Test_Shortcuts12(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts12.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, windowStyle:=Windows.Forms.FormWindowState.Maximized)
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestNumber("Shortcuts12", WalkmanLib.GetShortcutInfo(shortcutPath).WindowStyle, 3) ' 3 = Maximised. explained in the interface commentDoc
            End Using
        End Function

        Function Test_Shortcuts13(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts13.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath)
            Using testFile As New DisposableFile(shortcutPath, False)
                Return TestBoolean("Shortcuts13", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), False)
            End Using
        End Function

        Function Test_Shortcuts14(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts14.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath)
            Using testFile As New DisposableFile(shortcutPath, False)
                WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, True)
                Return TestBoolean("Shortcuts14", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), True)
            End Using
        End Function

        Function Test_Shortcuts15(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts15.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, "C:\Windows\notepad.exe", "testArgument",
                                                     "C:\Windows", "C:\Windows\regedit.exe,0", "testComment",
                                                     "CTRL+ALT+F", Windows.Forms.FormWindowState.Maximized)

            Using testFile As New DisposableFile(shortcutPath, False)
                WalkmanLib.CreateShortcut(shortcutPath, workingDirectory:="%UserProfile%")
                Return TestString("Shortcuts15", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "%UserProfile%")
            End Using
        End Function

        Function Test_Shortcuts16(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts16.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, "C:\Windows\notepad.exe", "testArgument",
                                                     "C:\Windows", "C:\Windows\regedit.exe,0", "testComment",
                                                     "CTRL+ALT+F", Windows.Forms.FormWindowState.Maximized)

            Using testFile As New DisposableFile(shortcutPath, False)
                WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, True)
                Return TestString("Shortcuts16", WalkmanLib.GetShortcutInfo(shortcutPath).IconLocation, "C:\Windows\regedit.exe,0")
            End Using
        End Function

        Function Test_Shortcuts17(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts17.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, "C:\Windows\notepad.exe", "testArgument",
                                                     "C:\Windows", "C:\Windows\regedit.exe,0", "testComment",
                                                     "CTRL+ALT+F", Windows.Forms.FormWindowState.Maximized)

            Using testFile As New DisposableFile(shortcutPath, False)
                WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, True)
                WalkmanLib.CreateShortcut(shortcutPath, workingDirectory:="%UserProfile%", iconPath:="C:\Windows\explorer.exe")
                Return TestBoolean("Shortcuts17", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), True)
            End Using
        End Function
    End Module
End Namespace
