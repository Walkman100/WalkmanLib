Imports System
Imports System.IO
Imports Microsoft.VisualBasic
Imports System.Security.Principal
Imports System.Windows.Forms
Public Partial Class WalkmanLib
    ''' <summary></summary>
    ''' <param name="path"></param>
    Shared Sub TakeOwnership(path As String)
        If File.Exists(path) Then
            RunAsAdmin("cmd.exe", "/c takeown /f " & path & " && icacls " & path & " /grant administrators:F && pause")
        ElseIf Directory.Exists(path)
            RunAsAdmin("cmd.exe", "/c takeown /f " & path & " /r /d y && icacls " & path & " /grant administrators:F /t && pause")
        End If
    End Sub
    
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Shared Function GetFolderIconPath(path As String) As String
        
    End Function
    
    ''' <summary>Sets the specified System.IO.FileAttributes of the file on the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttributes">A bitwise combination of the enumeration values.</param>
    ''' <returns>True if setting the attribute was successful, False if not.</returns>
    Shared Function SetAttribute(path As String, fileAttributes As FileAttributes) As Boolean
        Try
            File.SetAttributes(path, fileAttributes)
            Return True
        Catch ex As exception
            ErrorDialog(ex)
            Return False
        End Try
    End Function
    
    ''' <summary>Adds the specified System.IO.FileAttributes to the file on the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttributes">The FileAttributes to add.</param>
    ''' <returns>True if adding the attribute was successful, False if not.</returns>
    Shared Function AddAttribute(path As String, fileAttributes As FileAttributes) As Boolean
        Try
            File.SetAttributes(path, File.GetAttributes(path) + fileAttributes)
            Return True
        Catch ex As exception
            ErrorDialog(ex)
            Return False
        End Try
    End Function
    
    ''' <summary>Removes the specified System.IO.FileAttributes from the file on the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttributes">The FileAttributes to remove.</param>
    ''' <returns>True if removing the attribute was successful, False if not.</returns>
    Shared Function RemoveAttrubute(path As String, fileAttributes As FileAttributes) As Boolean
        Try
            File.SetAttributes(path, File.GetAttributes(path) - fileAttributes)
            Return True
        Catch ex As exception
            ErrorDialog(ex)
            Return False
        End Try
    End Function
    
    ''' <summary>Starts a program with a set of command-line arguments as an administrator.</summary>
    ''' <param name="programPath">Path of the program to run as administrator.</param>
    ''' <param name="arguments">Optional. Command-line arguments to pass when starting the process.</param>
    Shared Sub RunAsAdmin(programPath As String, Optional arguments As String = Nothing)
        If arguments = Nothing Then
            CreateObject("Shell.Application").ShellExecute(programPath, "", "", "runas")
        Else
            arguments = arguments.Trim("""")
            CreateObject("Shell.Application").ShellExecute(programPath, """" & arguments & """", "", "runas")
        End If
    End Sub
    
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
    
    ''' <summary></summary>
    ''' <param name="text"></param>
    ''' <param name="successMessage"></param>
    ''' <param name="showErrors"></param>
    ''' <returns></returns>
    Shared Function SafeSetText(text As String, Optional successMessage As String = Nothing, Optional showErrors As Boolean = True) As Boolean
        Try
            Clipboard.SetText(text, TextDataFormat.UnicodeText)
            If successMessage <> Nothing Then
                If successMessage = "default" Then
                    MsgBox(text & vbNewLine & "Succesfully copied!", MsgBoxStyle.Information, "Succesfully copied!")
                Else
                    MsgBox(successMessage, MsgBoxStyle.Information, "Succesfully copied!")
                End If
            End If
            Return True
        Catch ex As Exception
            If showErrors Then
                MsgBox("Copy failed!" & vbNewLine & "Error: """ & ex.ToString & """", MsgBoxStyle.Critical, "Copy failed!")
            End If
            Return False
        End Try
    End Function
    
    ''' <summary></summary>
    ''' <param name="ex"></param>
    Shared Sub ErrorDialog(ex As Exception)
        If MsgBox("There was an error! Error message: " & ex.Message & vbNewLine & "Show full stacktrace? (For sending to developer/making bugreport)", _
          MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Error!") = MsgBoxresult.Yes Then
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
        End If
    End Sub
End Class