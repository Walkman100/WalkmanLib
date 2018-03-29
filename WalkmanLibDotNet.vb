Option Explicit Off

Imports System
Imports System.IO
Imports System.IO.File
Imports Microsoft.VisualBasic
Imports System.Security.Principal
Imports System.Windows.Forms
Public Partial Class WalkmanLib
    
    ''' <summary>Opens the Open With dialog box for a file path.</summary>
    ''' <param name="path">The file to open with a program.</param>
    Shared Sub OpenWith(path As String)
        Shell("rundll32 shell32.dll,OpenAs_RunDLL " & path, AppWinStyle.NormalFocus, True, 500)
    End Sub
    
    ''' <summary>Checks whether the current process is elevated (running with administrator permissions)</summary>
    ''' <returns>True if running with administrator permissions, False if not</returns>
    Shared Function IsAdmin() As Boolean
        Return New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)
    End Function
    
    ''' <summary>Shows a custom messagebox</summary>
    ''' <param name="Prompt">The text to display in the messagebox window</param>
    ''' <param name="Buttons">Buttons and style of messagebox to show. A bitwise combination of the enumeration values. Default: OkOnly</param>
    ''' <param name="Title">Title of the messagebox window. If left out or set to Nothing, then title will be set to the owner form title or CustomMsgBox.</param>
    ''' <param name="WinVersion">Windows version to use style icons from. Default: CustomMsgBoxForm.WinVersionStyle.Win10</param>
    ''' <returns>The button the user clicked on.</returns>
    Shared Function CustomMsgBox(Prompt As String, Optional Buttons As MsgBoxStyle = 0, _
      Optional Title As String = Nothing, Optional WinVersion As CustomMsgBoxForm.WinVersionStyle = CustomMsgBoxForm.WinVersionStyle.Win10) As DialogResult
        Dim formToShow As New CustomMsgBoxForm
        formToShow.Prompt = Prompt
        formToShow.Buttons = Buttons
        formToShow.Title = Title
        formToShow.WinVersion = WinVersion
        Return formToShow.ShowDialog
    End Function
    
    ''' <summary>Shows a custom messagebox with custom buttons</summary>
    ''' <param name="Prompt">The text to display in the messagebox window</param>
    ''' <param name="customButton1">Text to show on the first button</param>
    ''' <param name="customButton2">Text to show on the second button. If left out or set to Nothing, this button will be hidden.</param>
    ''' <param name="customButton3">Text to show on the third button. If left out or set to Nothing, this button will be hidden.</param>
    ''' <param name="Title">Title of the messagebox window. If left out or set to Nothing, then title will be set to the owner form title or CustomMsgBox.</param>
    ''' <param name="WinVersion">Windows version to use style icons from. Default: CustomMsgBoxForm.WinVersionStyle.Win10</param>
    ''' <returns>Text of the button the user clicked on.</returns>
    Shared Function CustomMsgBox(Prompt As String, customButton1 As String, Optional customButton2 As String = Nothing, Optional customButton3 As String = Nothing, _
      Optional Title As String = Nothing, Optional WinVersion As CustomMsgBoxForm.WinVersionStyle = CustomMsgBoxForm.WinVersionStyle.Win10) As String
        Dim formToShow As New CustomMsgBoxForm
        formToShow.Prompt = Prompt
        formToShow.Button1Text = customButton1
        formToShow.Button2Text = customButton2
        formToShow.Button3Text = customButton3
        formToShow.Title = Title
        formToShow.WinVersion = WinVersion
        formToShow.ShowDialog
        Return formToShow.DialogResultString
    End Function
    
    ' Link: https://www.howtogeek.com/howto/windows-vista/add-take-ownership-to-explorer-right-click-menu-in-vista/
    ''' <summary>Runs the Take Ownership commands for a path.</summary>
    ''' <param name="path">Path of file to take ownership of, or directory to recursively take ownership of.</param>
    Shared Sub TakeOwnership(path As String)
        If File.Exists(path) Then
            RunAsAdmin("cmd.exe", "/c takeown /f " & path & " && icacls " & path & " /grant administrators:F && pause")
        ElseIf Directory.Exists(path)
            RunAsAdmin("cmd.exe", "/c takeown /f " & path & " /r /d y && icacls " & path & " /grant administrators:F /t && pause")
        End If
    End Sub
    
    ''' <summary>Starts a program with a set of command-line arguments as an administrator.</summary>
    ''' <param name="programPath">Path of the program to run as administrator.</param>
    ''' <param name="arguments">Optional. Command-line arguments to pass when starting the process. Do not surround the whole variable in quotes.</param>
    Shared Sub RunAsAdmin(programPath As String, Optional arguments As String = Nothing)
        If arguments = Nothing Then
            CreateObject("Shell.Application").ShellExecute(programPath, "", "", "runas")
        Else
            CreateObject("Shell.Application").ShellExecute(programPath, """" & arguments & """", "", "runas")
        End If
    End Sub
    
    ''' <summary>Sets the specified System.IO.FileAttributes of the file on the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttributes">A bitwise combination of the enumeration values.</param>
    ''' <returns>Whether setting the attribute was successful or not.</returns>
    Shared Function SetAttribute(path As String, fileAttributes As FileAttributes) As Boolean
        Try
            SetAttributes(path, fileAttributes)
            Return True
        Catch ex As exception
            ErrorDialog(ex)
            Return False
        End Try
    End Function
    
    ''' <summary>Adds or removes the specified System.IO.FileAttributes to the file at the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttribute">The FileAttributes to add or remove.</param>
    ''' <param name="addOrRemoveAttribute">True to add the specified attribute, False to remove it.</param>
    ''' <returns>Whether setting the attribute was successful or not.</returns>
    Shared Function ChangeAttribute(path As String, fileAttribute As FileAttributes, addOrRemoveAttribute As Boolean) As Boolean
        If addOrRemoveAttribute Then
            Return AddAttribute(path, fileAttribute)
        Else
            Return RemoveAttribute(path, fileAttribute)
        End If
    End Function
    
    ''' <summary>Adds the specified System.IO.FileAttributes to the file at the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttribute">The FileAttributes to add.</param>
    ''' <returns>Whether adding the attribute was successful or not.</returns>
    Shared Function AddAttribute(path As String, fileAttribute As FileAttributes) As Boolean
        Try
            SetAttributes(path, GetAttributes(path) Or fileAttribute)
            Return True
        Catch ex As exception
            ErrorDialog(ex)
            Return False
        End Try
    End Function
    
    ''' <summary>Removes the specified System.IO.FileAttributes from the file at the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttribute">The FileAttributes to remove.</param>
    ''' <returns>Whether removing the attribute was successful or not.</returns>
    Shared Function RemoveAttribute(path As String, fileAttribute As FileAttributes) As Boolean
        Try
            SetAttributes(path, GetAttributes(path) - fileAttribute)
            Return True
        Catch ex As exception
            ErrorDialog(ex)
            Return False
        End Try
    End Function
    
    ''' <summary>Sets clipboard to specified text, with optional success message and checks for errors.</summary>
    ''' <param name="text">Text to copy.</param>
    ''' <param name="successMessage">Message to show on success. If left out no message will be shown, if "default" is supplied then the default message will be shown.</param>
    ''' <param name="showErrors">Whether to show a message on copy error or not. Default: True</param>
    ''' <returns>Whether setting the clipboard was successful or not.</returns>
    Shared Function SafeSetText(text As String, Optional successMessage As String = Nothing, Optional showErrors As Boolean = True) As Boolean
        Try
            Clipboard.SetText(text, TextDataFormat.UnicodeText)
            If successMessage <> Nothing Then
                System.Windows.Forms.Application.EnableVisualStyles() ' affects when in a console app
                If successMessage = "default" Then
                    MsgBox(text & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
                Else
                    MsgBox(successMessage, MsgBoxStyle.Information, "Succesfully copied!")
                End If
            End If
            Return True
        Catch ex As Exception
            If showErrors Then
                System.Windows.Forms.Application.EnableVisualStyles() ' affects when in a console app
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString & """", MsgBoxStyle.Critical, "Copy failed!")
            End If
            Return False
        End Try
    End Function
    
    ''' <summary>Shows an error message for an exception, and asks the user if they want to display the full error in a copyable window.</summary>
    ''' <param name="ex">The System.Exception to show details about.</param>
    Shared Sub ErrorDialog(ex As Exception)
        System.Windows.Forms.Application.EnableVisualStyles() ' affects when in a console app
        If MsgBox("There was an error! Error message: " & ex.Message & vbNewLine & "Show full stacktrace? (For sending to developer/making bugreport)", _
          MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Error!") = MsgBoxresult.Yes Then
            Dim frmBugReport As New Form()
            frmBugReport.Width = 600
            frmBugReport.Height = 525
            frmBugReport.StartPosition = FormStartPosition.CenterParent
            frmBugReport.WindowState = FormWindowState.Normal
            frmBugReport.Show()
            frmBugReport.ShowIcon = False
            frmBugReport.ShowInTaskbar = True
            frmBugReport.Text = "Full error trace"
            Dim txtBugReport As New TextBox()
            txtBugReport.Multiline = True
            txtBugReport.ScrollBars = ScrollBars.Vertical
            frmBugReport.Controls.Add(txtBugReport)
            txtBugReport.Dock = DockStyle.Fill
            Try
                txtBugReport.Text = "ToString:" & vbNewLine & ex.ToString & vbNewLine & vbNewLine
                txtBugReport.Text &= "BaseException:" & vbNewLine & ex.GetBaseException.ToString & vbNewLine & vbNewLine
                txtBugReport.Text &= "Type: " & ex.GetType.ToString & vbNewLine
                txtBugReport.Text &= "Message: " & ex.Message.ToString & vbNewLine & vbNewLine
                txtBugReport.Text &= "StackTrace:" & vbNewLine & ex.StackTrace.ToString & vbNewLine & vbNewLine
                txtBugReport.Text &= "Source: " & ex.Source.ToString & vbNewLine
                txtBugReport.Text &= "TargetSite: " & ex.TargetSite.ToString & vbNewLine
                txtBugReport.Text &= "HashCode: " & ex.GetHashCode.ToString & vbNewLine
                txtBugReport.Text &= "HResult: " & ex.HResult.ToString & vbNewLine & vbNewLine
                For i = 0 To 100 'Integer.MaxValue no reason to go that high
                    Try
                        txtBugReport.Text &= "Data:" & vbNewLine & ex.Data(i).ToString & vbNewLine & vbNewLine
                    Catch
                        Exit For
                    End Try
                Next
            Catch ex2 As Exception
                txtBugReport.Text = "Error getting exception data!" & vbNewLine & vbNewLine & ex2.ToString()
            End Try
        End If
    End Sub
    
    ''' <summary>Gets path to the folder icon, or "no icon found" if none is set.</summary>
    ''' <param name="folderPath">The folder path to get the icon path for.</param>
    ''' <returns>The icon path.</returns>
    Shared Function GetFolderIconPath(folderPath As String) As String
        Dim gotIcon, lookingForIconIndex, isAbsolute As Boolean
        Dim parsedIconPath As String = folderPath
        
        If folderPath.EndsWith(Path.VolumeSeparatorChar & Path.DirectorySeparatorChar) Then
            If Exists(Path.Combine(folderPath, "Autorun.inf")) Then
                For Each line In ReadLines(Path.Combine(folderPath, "Autorun.inf"))
                    If line.StartsWith("Icon=", True, Nothing) Then
                        parsedIconPath = line.Substring(5)
                        gotIcon = True
                    End If
                Next
            End If
        Else
            If Exists(Path.Combine(folderPath, "desktop.ini")) Then
                gotIcon = False
                lookingForIconIndex = False
                For Each line In ReadLines(Path.Combine(folderPath, "desktop.ini"))
                    If line.StartsWith("IconResource=", True, Nothing) Then
                        parsedIconPath = line.Substring(13)
                        gotIcon = True
                    ElseIf line.StartsWith("IconFile=", True, Nothing) And gotIcon = False Then
                        parsedIconPath = line.Substring(9)
                        lookingForIconIndex = True
                        gotIcon = True
                    ElseIf line.StartsWith("IconIndex=", True, Nothing) And lookingForIconIndex Then
                        parsedIconPath = parsedIconPath & "," & line.Substring(10)
                        lookingForIconIndex = False
                    End If
                Next
            End If
        End If
        
        If gotIcon Then
            isAbsolute = False
            If parsedIconPath.StartsWith("%") Then
                isAbsolute = True
                parsedIconPath = parsedIconPath.Substring(1)
                If parsedIconPath.Contains("%") Then
                    parsedIconPath = Environment.GetEnvironmentVariable(parsedIconPath.Remove(parsedIconPath.IndexOf("%"))) & parsedIconPath.Substring(parsedIconPath.IndexOf("%") + 1)
                End If
            Else
                For i = 1 To 26 ' The Chr() below will give all letters from A to Z
                    If parsedIconPath.StartsWith( Chr(i+64) & Path.VolumeSeparatorChar & Path.DirectorySeparatorChar, True, Nothing ) Then
                        isAbsolute = True
                        Exit For
                    End If
                Next
            End If
            
            If parsedIconPath.EndsWith(",0") Then
                parsedIconPath = parsedIconPath.Remove(parsedIconPath.Length - 2)
            End If
            
            If isAbsolute Then
                Return parsedIconPath
            Else
                Return Path.Combine(folderPath, parsedIconPath)
            End If
        Else
            Return "no icon found"
        End If
    End Function
End Class
