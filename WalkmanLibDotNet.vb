Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO
Imports System.IO.File
Imports System.Security.Principal
Imports System.Windows.Forms

' used for IsFileOrDirectory
<Flags>
Public Enum PathEnum
    NotFound = 0
    Exists = 1
    IsDirectory = 2
    IsFile = 4
    IsDrive = 8
End Enum

Partial Public Class WalkmanLib
    Public Enum OS
        Other
        Windows
        Linux
        MacOS
    End Enum

    Shared Function GetOS() As OS
        ' ideally, the following would be used, but it was only added in .Net Framework v4.7.1:
        'If Runtime.InteropServices.RuntimeInformation.IsOSPlatform(Runtime.InteropServices.OSPlatform.Windows) Then
        '    Return "Windows"
        'ElseIf Runtime.InteropServices.RuntimeInformation.IsOSPlatform(Runtime.InteropServices.OSPlatform.Linux) Then
        '    Return "Linux"
        'ElseIf Runtime.InteropServices.RuntimeInformation.IsOSPlatform(Runtime.InteropServices.OSPlatform.OSX) Then
        '    Return "MacOS"
        'End If

        If Environment.OSVersion.Platform = PlatformID.Win32NT Then
            Return OS.Windows
        ElseIf Environment.OSVersion.Platform = PlatformID.Unix Then
            Dim unameOutput As String = RunAndGetOutput("uname")
            If unameOutput = "Linux" Then
                Return OS.Linux
            ElseIf unameOutput = "Darwin" Then
                Return OS.MacOS
            End If
        End If

        Return OS.Other
    End Function

    ''' <summary>Opens the Open With dialog box for a file path.</summary>
    ''' <param name="path">The file to open with a program.</param>
    Shared Sub OpenWith(path As String)
        Microsoft.VisualBasic.Shell("rundll32 shell32.dll,OpenAs_RunDLL " & path, Microsoft.VisualBasic.AppWinStyle.NormalFocus, True, 500)
    End Sub

    ''' <summary>Checks whether the current process is elevated (running with administrator permissions)</summary>
    ''' <returns>True if running with administrator permissions, False if not</returns>
    Shared Function IsAdmin() As Boolean
        Return New WindowsPrincipal(WindowsIdentity.GetCurrent).IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    ' Link: https://www.howtogeek.com/howto/windows-vista/add-take-ownership-to-explorer-right-click-menu-in-vista/
    ''' <summary>Runs the Take Ownership commands for a path.</summary>
    ''' <param name="path">Path of file to take ownership of, or directory to recursively take ownership of.</param>
    Shared Sub TakeOwnership(path As String)
        Dim pathInfo As PathEnum = IsFileOrDirectory(path)
        If pathInfo.HasFlag(PathEnum.IsFile) Then
            RunAsAdmin("cmd.exe", "/c takeown /f """ & path & """ & icacls """ & path & """ /grant administrators:F & pause")
        ElseIf pathInfo.HasFlag(PathEnum.IsDirectory) Then
            RunAsAdmin("cmd.exe", "/c takeown /f """ & path & """ /r /d y & icacls """ & path & """ /grant administrators:F /t & pause")
        Else
            Throw New ArgumentException("File or Directory at specified path does not exist!", "path")
        End If
    End Sub

    ''' <summary>Starts a program with a set of command-line arguments as an administrator.</summary>
    ''' <param name="programPath">Path of the program to run as administrator.</param>
    ''' <param name="arguments">Optional. Command-line arguments to pass when starting the process. Surround whitespaces with quotes as usual.</param>
    Shared Sub RunAsAdmin(programPath As String, Optional arguments As String = Nothing)
        Dim WSH_Type As Type = Type.GetTypeFromProgID("Shell.Application")
        Dim WSH_Activated As Object = Activator.CreateInstance(WSH_Type)

        WSH_Type.InvokeMember("ShellExecute", Reflection.BindingFlags.InvokeMethod, Nothing, WSH_Activated, New Object() {programPath, arguments, "", "runas"})
    End Sub

    ''' <summary>Adds or removes the specified System.IO.FileAttributes to the file at the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttribute">The FileAttributes to add or remove.</param>
    ''' <param name="addOrRemoveAttribute">True to add the specified attribute, False to remove it.</param>
    ''' <param name="accessDeniedSub">Create a sub with the signature (ex As Exception) and pass it with AddressOf to run it when access is denied and the program can be elevated.</param>
    ''' <returns>Whether setting the attribute was successful or not.</returns>
    Shared Function ChangeAttribute(path As String, fileAttribute As FileAttributes, addOrRemoveAttribute As Boolean, Optional accessDeniedSub As AccessDeniedDelegate = Nothing) As Boolean
        If addOrRemoveAttribute Then
            Return AddAttribute(path, fileAttribute, accessDeniedSub)
        Else
            Return RemoveAttribute(path, fileAttribute, accessDeniedSub)
        End If
    End Function

    ''' <summary>Adds the specified System.IO.FileAttributes to the file at the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttribute">The FileAttributes to add.</param>
    ''' <param name="accessDeniedSub">Create a sub with the signature (ex As Exception) and pass it with AddressOf to run it when access is denied and the program can be elevated.</param>
    ''' <returns>Whether adding the attribute was successful or not.</returns>
    Shared Function AddAttribute(path As String, fileAttribute As FileAttributes, Optional accessDeniedSub As AccessDeniedDelegate = Nothing) As Boolean
        Return SetAttribute(path, GetAttributes(path) Or fileAttribute, accessDeniedSub)
    End Function

    ''' <summary>Removes the specified System.IO.FileAttributes from the file at the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttribute">The FileAttributes to remove.</param>
    ''' <param name="accessDeniedSub">Create a sub with the signature (ex As Exception) and pass it with AddressOf to run it when access is denied and the program can be elevated.</param>
    ''' <returns>Whether removing the attribute was successful or not.</returns>
    Shared Function RemoveAttribute(path As String, fileAttribute As FileAttributes, Optional accessDeniedSub As AccessDeniedDelegate = Nothing) As Boolean
        Return SetAttribute(path, GetAttributes(path) And Not fileAttribute, accessDeniedSub)
    End Function

    ''' <summary>Sets the specified System.IO.FileAttributes of the file on the specified path, with a try..catch block.</summary>
    ''' <param name="path">The path to the file.</param>
    ''' <param name="fileAttributes">A bitwise combination of the enumeration values.</param>
    ''' <param name="accessDeniedSub">Create a sub with the signature (ex As Exception) and pass it with AddressOf to run it when access is denied and the program can be elevated.</param>
    ''' <returns>Whether setting the attribute was successful or not.</returns>
    Shared Function SetAttribute(path As String, fileAttributes As FileAttributes, Optional accessDeniedSub As AccessDeniedDelegate = Nothing) As Boolean
        Try
            SetAttributes(path, fileAttributes)
            Return True
        Catch ex As UnauthorizedAccessException When Not IsAdmin() AndAlso accessDeniedSub IsNot Nothing
            accessDeniedSub.Invoke(ex)
            Return False
        Catch ex As Exception
            ErrorDialog(ex)
            Return False
        End Try
    End Function
    Delegate Sub AccessDeniedDelegate(ByVal ex As Exception)

    ' Example code to use the access denied sub return:
    'Sub Main()
    '    WalkmanLib.SetAttribute("C:\ProgramData", FileAttributes.Hidden, AddressOf accessDeniedSub)
    'End Sub
    'Sub accessDeniedSub(ex As Exception)
    '    Application.EnableVisualStyles() ' affects when in a console app
    '    If MsgBox(ex.Message & Environment.NewLine & Environment.NewLine & "Try launching <programName> As Administrator?",
    '      MsgBoxStyle.YesNo Or MsgBoxStyle.Exclamation, "Access denied!") = MsgBoxResult.Yes Then
    '        WalkmanLib.RunAsAdmin(Application.StartupPath & "\" & Diagnostics.Process.GetCurrentProcess.ProcessName & ".exe", "<arguments>")
    '        Environment.Exit(0) / Application.Exit() ' depending on whether running in Console or WinForms app, respectively
    '    End If
    'End Sub

    ' Link: https://stackoverflow.com/a/25958432/2999220
    ' Link: https://stackoverflow.com/a/11166160/2999220
    Private Const RunAsAdminByte  As Byte = &H15 ' Decimal 21
    Private Const RunAsAdminOnBit As Byte = &H20 ' Decimal 32

    ''' <summary>Gets whether a shortcut's "Run as Administrator" checkbox is checked.</summary>
    ''' <param name="shortcutPath">Path to the shortcut file. Shortcuts end in ".lnk".</param>
    ''' <returns>State of the Admin flag. True = Set, i.e. will attempt to run as admin.</returns>
    Shared Function GetShortcutRunAsAdmin(shortcutPath As String) As Boolean
        Dim shortcutBytes As Byte() = ReadAllBytes(shortcutPath)

        If (shortcutBytes(RunAsAdminByte) And RunAsAdminOnBit) = RunAsAdminOnBit Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Sets a shortcut's "Run as Administrator" checkbox state.
    ''' Note that this uses bit-flipping to change the documented RunAsAdmin bit.
    ''' If the Shortcut (.lnk) specification ever changes, this will corrupt shortcuts.
    ''' </summary>
    ''' <param name="shortcutPath">Path to the shortcut file. Shortcuts end in ".lnk".</param>
    ''' <param name="flagState">State to set the Admin flag to. True = Set, i.e. will attempt to run as admin.</param>
    Shared Sub SetShortcutRunAsAdmin(shortcutPath As String, flagState As Boolean)
        Dim shortcutBytes As Byte() = ReadAllBytes(shortcutPath)
        If flagState Then
            shortcutBytes(RunAsAdminByte) = shortcutBytes(RunAsAdminByte) Or      RunAsAdminOnBit
        Else
            shortcutBytes(RunAsAdminByte) = shortcutBytes(RunAsAdminByte) And Not RunAsAdminOnBit
        End If
        WriteAllBytes(shortcutPath, shortcutBytes)
    End Sub

    ''' <summary>Writes text to console to report a progress count</summary>
    ''' <param name="start">Number to start at. Must be less than <paramref name="end"/>.</param>
    ''' <param name="end">Number to end at. Must be more than <paramref name="start"/>.</param>
    ''' <param name="stop">ByRef boolean used to stop the count from an external method.</param>
    ''' <param name="stopped">ByRef boolean used to signal that counting has stopped.</param>
    ''' <returns><see langword="True"/> if the count reached <paramref name="end"/>, <see langword="False"/> if <paramref name="stop"/> was used to cancel.</returns>
    Shared Function ConsoleProgress(start As Integer, [end] As Integer, ByRef [stop] As Boolean, ByRef stopped As Boolean) As Boolean
        Dim currentCount As Integer = start
        Dim stopLength As Integer = [end].ToString().Length ' could also use: CType(Math.Floor(Math.Log10([end]) + 1), Integer)

        Console.Write(currentCount.ToString().PadLeft(stopLength) & " / " & [end])
        Console.CursorLeft -= stopLength + 3

        Do Until [stop] OrElse currentCount = [end]
            Threading.Thread.Sleep(1000)
            currentCount += 1

            Console.CursorLeft -= stopLength
            Console.Write(currentCount.ToString().PadLeft(stopLength))
        Loop

        stopped = True
        Return currentCount = [end]
    End Function

    ''' <summary>
    ''' Gets whether a specified <paramref name="path"/> exists. `NotFound` is returned on invalid chars.
    ''' Return value will contain `Exists` if <paramref name="path"/> refers to a valid drive,
    ''' and `IsDirectory` will be set if the drive is accessible (ready).
    ''' </summary>
    ''' <param name="path">Path to the item to get exists info for</param>
    ''' <returns>PathEnum: either NotFound, or Exists with IsFile | IsDirectory, and IsDrive if <paramref name="path"/> points to a drive mountpoint</returns>
    Shared Function IsFileOrDirectory(path As String) As PathEnum
        If path.IndexOfAny(IO.Path.GetInvalidPathChars()) <> -1 Then
            ' invalid chars
            Return PathEnum.NotFound
        End If

        Dim rtn As PathEnum
        If File.Exists(path) Then
            rtn = PathEnum.Exists Or PathEnum.IsFile
        ElseIf Directory.Exists(path) Then
            rtn = PathEnum.Exists Or PathEnum.IsDirectory
        End If

        Try
            ' path can be a Directory and a Drive, or just a Drive...
            ' will have IsDirectory if the drive can be accessed
            If New DriveInfo(path).Name = New FileInfo(path).FullName Then
                rtn = rtn Or PathEnum.Exists Or PathEnum.IsDrive
            End If
        Catch ex As Exception When _
                TypeOf ex Is ArgumentException OrElse
                TypeOf ex Is NotSupportedException
            ' New DriveInfo() and New FileInfo() throw ArgumentException on invalid path sequence
            ' NotSupportedException is thrown for AlternateDataStreams
        End Try

        Return rtn
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
                Application.EnableVisualStyles() ' affects when in a console app
                If successMessage = "default" Then
                    MessageBox.Show(text & Environment.NewLine & "Succesfully copied!", "Succesfully copied!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show(successMessage, "Succesfully copied!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
            Return True
        Catch ex As Exception
            If showErrors Then
                Application.EnableVisualStyles() ' affects when in a console app
                MessageBox.Show("Copy failed!" & Environment.NewLine & "Error: """ & ex.ToString & """", "Copy failed!", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets path to a WalkmanUtils install. First tries in the same folder as the running application,
    ''' then the location saved in the registry, then a default fallback in Program Files.
    ''' <br />Throws <see cref="DirectoryNotFoundException"/> on no install found.
    ''' </summary>
    ''' <param name="minimumVersion">
    ''' Optional version to check against installed version.
    ''' Only checked with installed WalkmanUtils, and only presents a Continue message to the user. Throws <see cref="OperationCanceledException"/> on user abort.
    ''' </param>
    ''' <returns>Directory containing a WalkmanUtils installation. Module presence is not checked.</returns>
    Shared Function GetWalkmanUtilsPath(Optional minimumVersion As Version = Nothing) As String
        'first check startup path
        Dim rtn As String = Path.Combine(Application.StartupPath, "WalkmanUtils")
        If Directory.Exists(rtn) Then
            Return rtn
        End If

        'then registry
        'if 64-bit app: HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\WalkmanUtils InstallLocation
        'if 32-bit app: HKLM\Software            \Microsoft\Windows\CurrentVersion\Uninstall\WalkmanUtils InstallLocation
        Dim keyPath As String = "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\WalkmanUtils\"

        ' always use 32-bit view
        Dim localKey As Microsoft.Win32.RegistryKey = Microsoft.Win32.RegistryKey.OpenBaseKey(
                Microsoft.Win32.RegistryHive.LocalMachine, Microsoft.Win32.RegistryView.Registry32)
        localKey = localKey.OpenSubKey(keyPath)

        If localKey IsNot Nothing AndAlso localKey.GetValue("InstallLocation") IsNot Nothing Then
            rtn = localKey.GetValue("InstallLocation").ToString()
            If Directory.Exists(rtn) Then

                If minimumVersion IsNot Nothing Then
                    Dim gotVersion As Version = Nothing
                    If Version.TryParse(localKey.GetValue("DisplayVersion").ToString(), gotVersion) Then
                        If gotVersion < minimumVersion AndAlso MessageBox.Show("Currently Installed WalkmanUtils version is out of date! Use anyway?",
                                                                           "WalkmanUtils Version", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                            Throw New OperationCanceledException
                        End If
                    ElseIf MessageBox.Show("Got Invalid Version from WalkmanUtils install info! Continue anyway?",
                                           "WalkmanUtils Version", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                        Throw New OperationCanceledException
                    End If
                End If

                Return rtn.TrimEnd(Path.DirectorySeparatorChar)
            End If
        End If

        'fall back to check %ProgramFiles%\WalkmanOSS\WalkmanUtils
        rtn = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles"), "WalkmanOSS", "WalkmanUtils")
        If Directory.Exists(rtn) Then
            Return rtn
        End If '        and %ProgramFiles(x86)%\WalkmanOSS\WalkmanUtils
        rtn = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles(x86)"), "WalkmanOSS", "WalkmanUtils")
        If Directory.Exists(rtn) Then
            Return rtn
        End If

        Throw New DirectoryNotFoundException("WalkmanUtils path not found in application's path, Installed location or default folder in Program Files")
    End Function

    ''' <summary>Shows an error message for an exception, and asks the user if they want to display the full error in a copyable window.</summary>
    ''' <param name="ex">The System.Exception to show details about.</param>
    ''' <param name="errorMessage">Optional error message to show instead of the default "There was an error! Error message: "</param>
    ''' <param name="showMsgBox">True to show the error message prompt to show the full stacktrace or not, False to just show the window immediately</param>
    Shared Sub ErrorDialog(ex As Exception, Optional errorMessage As String = "There was an error! Error message: ", Optional showMsgBox As Boolean = True)
        Application.EnableVisualStyles() ' affects when in a console app
        If showMsgBox Then
            If MessageBox.Show(errorMessage & ex.Message & Environment.NewLine & "Show full stacktrace? (For sending to developer/making bugreport)",
                               "Error!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) <> DialogResult.Yes Then Exit Sub
        End If

        Dim frmBugReport As New Form With {
            .Width = 600,
            .Height = 525,
            .StartPosition = FormStartPosition.CenterParent,
            .WindowState = FormWindowState.Normal,
            .ShowIcon = False,
            .ShowInTaskbar = True,
            .Text = "Full error trace"
        }
        Dim txtBugReport As New TextBox With {
            .Multiline = True,
            .ScrollBars = ScrollBars.Vertical,
            .Dock = DockStyle.Fill
        }
        frmBugReport.Controls.Add(txtBugReport)
        Try
            txtBugReport.Text = ""
            While ex IsNot Nothing
                If ex.ToString IsNot Nothing Then           txtBugReport.Text &= "ToString:" & Environment.NewLine & ex.ToString & Environment.NewLine & Environment.NewLine
                If ex.GetBaseException IsNot Nothing Then   txtBugReport.Text &= "BaseException:" & Environment.NewLine & ex.GetBaseException.ToString & Environment.NewLine & Environment.NewLine
                If ex.GetType IsNot Nothing Then            txtBugReport.Text &= "Type: " & ex.GetType.ToString & Environment.NewLine
                If ex.Message IsNot Nothing Then            txtBugReport.Text &= "Message: " & ex.Message.ToString & Environment.NewLine & Environment.NewLine
                If ex.StackTrace IsNot Nothing Then         txtBugReport.Text &= "StackTrace:" & Environment.NewLine & ex.StackTrace.ToString & Environment.NewLine & Environment.NewLine
                If TypeOf ex Is System.ComponentModel.Win32Exception Then
                                                            txtBugReport.Text &= "ErrorCode: 0x" & DirectCast(ex, System.ComponentModel.Win32Exception).ErrorCode.ToString("X") & Environment.NewLine
                                                            txtBugReport.Text &= "NativeErrorCode: 0x" & DirectCast(ex, System.ComponentModel.Win32Exception).NativeErrorCode.ToString("X") & Environment.NewLine
                End If
                If TypeOf ex Is FileNotFoundException Then
                                                            txtBugReport.Text &= "FileName: " & DirectCast(ex, FileNotFoundException).FileName & Environment.NewLine
                                                            txtBugReport.Text &= "FusionLog: " & DirectCast(ex, FileNotFoundException).FusionLog & Environment.NewLine
                End If
                If ex.Source IsNot Nothing Then             txtBugReport.Text &= "Source: " & ex.Source.ToString & Environment.NewLine
                If ex.TargetSite IsNot Nothing Then         txtBugReport.Text &= "TargetSite: " & ex.TargetSite.ToString & Environment.NewLine
                                                            txtBugReport.Text &= "HashCode: 0x" & ex.GetHashCode.ToString("X") & Environment.NewLine
                                                            txtBugReport.Text &= "HResult: 0x" & ex.HResult.ToString("X") & Environment.NewLine & Environment.NewLine
                For Each key As Object In ex.Data.Keys
                                                            txtBugReport.Text &= "Data(" & key.ToString() & "): " & ex.Data(key).ToString() & Environment.NewLine
                Next
                If ex.InnerException IsNot Nothing Then     txtBugReport.Text &= Environment.NewLine & "InnerException:" & Environment.NewLine
                ex = ex.InnerException
            End While
        Catch ex2 As Exception
            txtBugReport.Text &= "Error getting exception data!" & Environment.NewLine & Environment.NewLine & ex2.ToString()
        End Try

        Try
            ' show the dialog in a different thread with an unassociated MessagePump
            Threading.Tasks.Task.Run(Sub() frmBugReport.ShowDialog())

            'If messagePumpForm Is Nothing Then
            '    ' Thanks to https://stackoverflow.com/a/661662/2999220
            '    If frmBugReport.InvokeRequired Then
            '        frmBugReport.Invoke(DirectCast(Sub() frmBugReport.Show(), MethodInvoker))
            '    Else
            '        frmBugReport.Show()
            '    End If
            'Else
            '    messagePumpForm.Invoke(DirectCast(Sub() frmBugReport.Show(), MethodInvoker))
            'End If
        Catch ex2 As Exception
            MessageBox.Show("Error showing window: " & ex2.ToString, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    ' Link: https://stackoverflow.com/a/10072082/2999220
    ''' <summary>Runs an executable without showing a window and returns it's output.</summary>
    ''' <param name="fileName">The path to the program executable.</param>
    ''' <param name="arguments">Any arguments to run the program with. Default: Nothing</param>
    ''' <param name="workingDirectory">Working directory for the process to be started. Default: Nothing</param>
    ''' <param name="mergeStdErr">Whether to merge StdErr with StdOut in the function's result (Return String). Default: True</param>
    ''' <param name="StdErrReturn">Reference to a System.String variable to populate with StdErr, if any.</param>
    ''' <param name="ExitCode">Reference to a Integer variable to populate with the program's Exit Code.</param>
    ''' <returns>If mergeStdErr is False, Returns StdOut. If mergeStdErr is True and the process outputs data to StdErr, Returns StdOut (if not empty) appended with "StdErr:", StdErr, "ExitCode:", and the process's Exit Code.
    ''' To merge StdOut and StdErr in the order they are output, use "cmd.exe" as the fileName, "/c actual_program.exe actual_arguments 2>&amp;1" as the arguments (replace actual_* with real values), and set mergeStdErr to False.</returns>
    Shared Function RunAndGetOutput(fileName As String, Optional arguments As String = Nothing, Optional workingDirectory As String = Nothing,
      Optional mergeStdErr As Boolean = True, Optional ByRef stdErrReturn As String = "", Optional ByRef exitCode As Integer = -1) As String
        Dim process As New Diagnostics.Process()
        process.StartInfo.FileName = fileName
        If Not String.IsNullOrEmpty(arguments) Then
            process.StartInfo.Arguments = arguments
        End If
        If Not String.IsNullOrEmpty(workingDirectory) Then
            process.StartInfo.WorkingDirectory = workingDirectory
        End If

        process.StartInfo.CreateNoWindow = True
        process.StartInfo.WindowStyle = Diagnostics.ProcessWindowStyle.Hidden
        process.StartInfo.UseShellExecute = False
        process.StartInfo.RedirectStandardError = True
        process.StartInfo.RedirectStandardOutput = True
        Dim stdOutput As Text.StringBuilder = New Text.StringBuilder()
        AddHandler process.OutputDataReceived, Sub(sender, args) stdOutput.AppendLine(args.Data)
        ' Use AppendLine rather than Append since args.Data is one line of output, not including the newline character.

        Dim stdError As String
        process.Start()
        process.BeginOutputReadLine()
        stdError = process.StandardError.ReadToEnd()
        process.WaitForExit()

        Dim returnString As String = stdOutput.ToString.Trim()
        If mergeStdErr Then
            If Not String.IsNullOrEmpty(stdError) Then
                If Not String.IsNullOrEmpty(returnString) Then
                    returnString &= Environment.NewLine
                End If

                returnString &= "StdErr: " & stdError.Trim()
                returnString &= Environment.NewLine & "ExitCode: " & process.ExitCode
            End If
        End If

        stdErrReturn = stdError.Trim()
        exitCode = process.ExitCode
        Return returnString
    End Function

    ''' <summary>Gets path to the folder icon, or "no icon found" if none is set.</summary>
    ''' <param name="folderPath">The folder path to get the icon path for.</param>
    ''' <returns>The icon path.</returns>
    Shared Function GetFolderIconPath(folderPath As String) As String
        Dim gotIcon, lookingForIconIndex, isAbsolute As Boolean
        Dim parsedIconPath As String = folderPath

        If folderPath.EndsWith(Path.VolumeSeparatorChar & Path.DirectorySeparatorChar) Then
            If Exists(Path.Combine(folderPath, "Autorun.inf")) Then
                For Each line As String In ReadLines(Path.Combine(folderPath, "Autorun.inf"))
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
                For Each line As String In ReadLines(Path.Combine(folderPath, "desktop.ini"))
                    If line.StartsWith("IconResource=", True, Nothing) Then
                        parsedIconPath = line.Substring(13)
                        gotIcon = True
                    ElseIf line.StartsWith("IconFile=", True, Nothing) AndAlso gotIcon = False Then
                        parsedIconPath = line.Substring(9)
                        lookingForIconIndex = True
                        gotIcon = True
                    ElseIf line.StartsWith("IconIndex=", True, Nothing) AndAlso lookingForIconIndex Then
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
                parsedIconPath = Environment.ExpandEnvironmentVariables(parsedIconPath)
            Else
                For i As Integer = 1 To 26 ' The Convert.ToChar() below will give all letters from A to Z
                    If parsedIconPath.StartsWith(Convert.ToChar(i + 64) & Path.VolumeSeparatorChar & Path.DirectorySeparatorChar, True, Nothing) Then
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
