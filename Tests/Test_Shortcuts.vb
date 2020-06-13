Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_Shortcuts
        Function Test_Shortcuts1(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts1.lnk")
            Try
                Return TestString("Shortcuts1", WalkmanLib.CreateShortcut(shortcutPath), shortcutPath)
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts2(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts2")
            Try
                Return TestString("Shortcuts2", WalkmanLib.CreateShortcut(shortcutPath), shortcutPath & ".lnk")
            Finally
                File.Delete(shortcutPath & ".lnk")
            End Try
        End Function

        Function Test_Shortcuts3(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts3.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, targetPath:="C:\Windows\notepad.exe")
            Try
                Return TestString("Shortcuts3", WalkmanLib.GetShortcutInfo(shortcutPath).TargetPath, "C:\Windows\notepad.exe")
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts4(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts4.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, arguments:="testArgument")
            Try
                Return TestString("Shortcuts4", WalkmanLib.GetShortcutInfo(shortcutPath).Arguments, "testArgument")
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts5(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts5.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, workingDirectory:="C:\Windows")
            Try
                Return TestString("Shortcuts5", WalkmanLib.GetShortcutInfo(shortcutPath).WorkingDirectory, "C:\Windows")
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts6(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts6.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, iconPath:="C:\Windows\regedit.exe,0")
            Try
                Return TestString("Shortcuts6", WalkmanLib.GetShortcutInfo(shortcutPath).IconLocation, "C:\Windows\regedit.exe,0")
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts7(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts7.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, comment:="testComment")
            Try
                Return TestString("Shortcuts7", WalkmanLib.GetShortcutInfo(shortcutPath).Description, "testComment")
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts8(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts8.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, shortcutKey:="CTRL+ALT+F")
            Try
                Return TestString("Shortcuts8", WalkmanLib.GetShortcutInfo(shortcutPath).Hotkey, "Alt+Ctrl+F")
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts9(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts9.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, windowStyle:=Windows.Forms.FormWindowState.Maximized)
            Try
                Return TestNumber("Shortcuts9", WalkmanLib.GetShortcutInfo(shortcutPath).WindowStyle, 3) ' 3 = Maximised. explained in the interface commentDoc
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts10(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts10.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, windowStyle:=Windows.Forms.FormWindowState.Maximized)
            Try
                Return TestBoolean("Shortcuts10", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), False)
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function

        Function Test_Shortcuts11(rootTestFolder As String) As Boolean
            Dim shortcutPath As String = Path.Combine(rootTestFolder, "shortcuts11.lnk")
            shortcutPath = WalkmanLib.CreateShortcut(shortcutPath, windowStyle:=Windows.Forms.FormWindowState.Maximized)
            WalkmanLib.SetShortcutRunAsAdmin(shortcutPath, True)
            Try
                Return TestBoolean("Shortcuts11", WalkmanLib.GetShortcutRunAsAdmin(shortcutPath), True)
            Finally
                File.Delete(shortcutPath)
            End Try
        End Function
    End Module
End Namespace
