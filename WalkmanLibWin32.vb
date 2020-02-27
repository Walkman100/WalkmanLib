Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

Public Enum SymbolicLinkType ' used for CreateSymLink
    ''' <summary>The target is a file.</summary>
    File = 0
    ''' <summary>The target is a directory.</summary>
    Directory = 1
End Enum

Public Enum MouseButton ' used for MouseClick
    ''' <summary>Performs a LeftClick by running LeftDown and LeftUp.</summary>
    LeftClick
    ''' <summary>Holds the left mouse button.</summary>
    LeftDown = &H2
    ''' <summary>Releases the left mouse button.</summary>
    LeftUp = &H4
    ''' <summary>Performs a RightClick by running RightDown and RightUp.</summary>
    RightClick
    ''' <summary>Holds the right mouse button.</summary>
    RightDown = &H8
    ''' <summary>Releases the right mouse button.</summary>
    RightUp = &H10
    ''' <summary>Performs a MiddleClick by running MiddleDown and MiddleUp.</summary>
    MiddleClick
    ''' <summary>Holds the right mouse button.</summary>
    MiddleDown = &H20
    ''' <summary>Releases the right mouse button.</summary>
    MiddleUp = &H40
    ''' <summary>Performs a XClick by running XDown and XUp.</summary>
    XClick
    ''' <summary>Holds the X mouse button. (?)</summary>
    XDown = &H80
    ''' <summary>Releases the X mouse button. (?)</summary>
    XUp = &H100
End Enum

Public Partial Class WalkmanLib
    
    ' =================================== CreateHardLink ===================================
    
    ' Link: http://pinvoke.net/default.aspx/kernel32.CreateHardLink
    ''' <summary>Creates a hardlink to an existing file.</summary>
    ''' <param name="symlinkPath">Path to the hardlink file to create.</param>
    ''' <param name="targetPath">Absolute or relative path to the existing file to link to. If relative, target is relative to current directory.</param>
    Shared Sub CreateHardLink(hardlinkPath As String, existingFilePath As String)
        If CreateHardLink(hardlinkPath, existingFilePath, IntPtr.Zero) = False Then
            
            Dim errorException As Win32Exception = New Win32Exception
            If errorException.Message = "The system cannot find the file specified" Then
                If Not File.Exists(existingFilePath) Then
                    Throw New FileNotFoundException("The hardlink target does not exist", existingFilePath, errorException)
                End If
            ElseIf errorException.Message = "The system cannot find the path specified" Then
                If Not Directory.Exists(New FileInfo(hardlinkPath).DirectoryName) Then ' "New FileInfo(hardlinkPath)" throws an exception on invalid characters in path - perfect!
                    Throw New DirectoryNotFoundException("The path to the hardlink does not exist", errorException)
                End If
            End If
            Throw errorException
        End If
    End Sub
    
    <DllImport("kernel32.dll", SetLastError := True, CharSet := CharSet.Auto)> _
    Private Shared Function CreateHardLink(lpFileName As String, lpExistingFileName As String, lpSecurityAttributes As IntPtr) As Boolean
    End Function
    
    
    ' =================================== CreateSymLink ===================================
    
    ' Link: https://stackoverflow.com/a/11156870/2999220
    ''' <summary>Creates a file or directory symbolic link.</summary>
    ''' <param name="symlinkPath">Path to the symbolic link file to create.</param>
    ''' <param name="targetPath">Absolute or relative path to the target of the shortcut. If relative, target is relative to the symbolic link file.</param>
    ''' <param name="targetType">Type of the target. If incorrect target type is supplied, the system will act as if the target doesn't exist.</param>
    Shared Sub CreateSymLink(symlinkPath As String, targetPath As String, targetType As SymbolicLinkType)
        If CreateSymbolicLink(symlinkPath, targetPath, targetType) = False Then
            
            Dim errorException As Win32Exception = New Win32Exception
            If errorException.Message = "The system cannot find the file specified" Then
                If File.Exists(symlinkPath) Or Directory.Exists(symlinkPath) Then
                    Throw New IOException("The symbolic link path already exists", errorException)
                ElseIf Not Directory.Exists(New FileInfo(symlinkPath).DirectoryName) Then ' "New FileInfo(symlinkPath)" throws an exception on invalid characters in path - perfect!
                    Throw New DirectoryNotFoundException("The path to the symbolic link does not exist or is invalid", errorException)
                End If
            End If
            Throw errorException
        End If
    End Sub
    
    <DllImport("kernel32.dll")> _
    Private Shared Function CreateSymbolicLink(lpSymlinkFileName As String, lpTargetFileName As String, dwFlags As SymbolicLinkType) As Boolean
    End Function
    
    
    ' =================================== GetSymlinkTarget ===================================
    
    ' Link: https://stackoverflow.com/a/33487494/2999220
    ''' <summary>Gets the target of a symbolic link, directory junction or volume mountpoint. Throws ComponentModel.Win32Exception on error.</summary>
    ''' <param name="path">Path to the symlink to get the target of.</param>
    ''' <returns>The fully qualified path to the target.</returns>
    Shared Function GetSymlinkTarget(path As String) As String
        Dim fileHandle As IntPtr = CreateFile(path, &H8, FileShare.ReadWrite Or FileShare.Delete, IntPtr.Zero, FileMode.Open, &H2000000, IntPtr.Zero)
        If fileHandle = New IntPtr(-1) Then Throw New Win32Exception()
        
        Dim returnString As String = ""
        Try
            Dim stringBuilderTarget As Text.StringBuilder = New Text.StringBuilder(1024)
            Dim result As UInteger = GetFinalPathNameByHandle(fileHandle, stringBuilderTarget, 1024, 0)
            If result = 0 Then Throw New Win32Exception()
            returnString = stringBuilderTarget.ToString()
        Finally
            CloseHandle(fileHandle)
        End Try
        
        returnString = returnString.Substring(4) ' remove "\\?\" at the beginning
        If returnString.StartsWith("UNC\") Then  ' change "UNC\[IP]\" to proper "\\[IP]\"
            returnString = "\" & returnString.Substring(3)
        End If
        
        Return returnString
    End Function
    
    <DllImport("kernel32.dll", SetLastError := True, CharSet := CharSet.Auto)> _
    Private Shared Function CreateFile(filename As String, access As UInteger, share As FileShare, securityAttributes As IntPtr,
    creationDisposition As FileMode, flagsAndAttributes As UInteger, templateFile As IntPtr) As IntPtr
    End Function
    
    <DllImport("Kernel32.dll", SetLastError := True, CharSet := CharSet.Auto)> _
    Private Shared Function GetFinalPathNameByHandle(hFile As IntPtr, lpszFilePath As Text.StringBuilder,
    cchFilePath As UInteger, dwFlags As UInteger) As UInteger
    End Function
    
    <DllImport("kernel32.dll", SetLastError := True)> _
    Private Shared Function CloseHandle(hObject As IntPtr) As Boolean
    End Function
    
    
    ' =================================== Shortcut Management ===================================
    
    ' Link: https://stackoverflow.com/a/14141782/2999220
    ' Link: https://www.tek-tips.com/viewthread.cfm?qid=850335
    ''' <summary>Gets a shortcut property object to retrieve info about a shortcut.</summary>
    ''' <param name="shortcutPath">Path to the shortcut file.</param>
    ''' <returns>Shortcut object of type IWshShortcut - either use WalkmanLib.IWshShortcut or ComImport your own interface.</returns>
    Shared Function GetShortcutInfo(shortcutPath As String) As IWshShortcut
        Dim WSH_Type As Type = Type.GetTypeFromProgID("WScript.Shell")
        Dim WSH_Activated As Object = Activator.CreateInstance(WSH_Type)
        
        If Not shortcutPath.EndsWith(".lnk", True, Nothing) Then shortcutPath &= ".lnk"
        Dim WSH_InvokeMember As Object = WSH_Type.InvokeMember("CreateShortcut", System.Reflection.BindingFlags.InvokeMethod, Nothing, WSH_Activated, New Object() {shortcutPath})
        
        Return DirectCast(WSH_InvokeMember, IWshShortcut)
    End Function
    
    ' Link: https://ss64.com/vb/shortcut.html
    ' HotKey: https://docs.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/windows-scripting/3zb1shc6(v=vs.84)#arguments
    ''' <summary>Creates or modifies an existing shortcut. When modifying, set a parameter to "" to clear it. Defaults are 'Nothing'.</summary>
    ''' <param name="shortcutPath">Path to the shortcut file.</param>
    ''' <param name="targetPath">Full path to the target of the shortcut.</param>
    ''' <param name="arguments">Arguments to the target.</param>
    ''' <param name="workingDirectory">Directory to start the target in.</param>
    ''' <param name="iconPath">Path to the shortcut icon. Append ", iconIndex" to specify an index.</param>
    ''' <param name="comment">Shortcut comment. Shown in the Shortcut's tooltip.</param>
    ''' <param name="shortcutKey">Hotkey used to launch the shortcut - see the end of https://ss64.com/vb/shortcut.html.</param>
    ''' <param name="windowStyle">System.Windows.Forms.FormWindowState to show the launched program in.</param>
    ''' <returns>Full path to the created shortcut.</returns>
    Shared Function CreateShortcut(shortcutPath As String, Optional targetPath As String = Nothing, Optional arguments As String = Nothing, Optional workingDirectory As String = Nothing, _
        Optional iconPath As String = Nothing, Optional comment As String = Nothing, Optional shortcutKey As String = Nothing, Optional windowStyle As Windows.Forms.FormWindowState = Windows.Forms.FormWindowState.Normal) As String
        Dim shortcutObject As IWshShortcut = GetShortcutInfo(shortcutPath)
        
        If targetPath <> Nothing Then       shortcutObject.TargetPath       = targetPath
        If arguments <> Nothing Then        shortcutObject.Arguments        = arguments
        If workingDirectory <> Nothing Then shortcutObject.WorkingDirectory = workingDirectory
        If iconPath <> Nothing Then         shortcutObject.IconLocation     = iconPath
        If comment <> Nothing Then          shortcutObject.Description      = comment
        If shortcutKey <> Nothing Then      shortcutObject.HotKey           = shortcutKey
        
        If windowStyle = Windows.Forms.FormWindowState.Normal Then
            shortcutObject.WindowStyle = 1
        ElseIf windowStyle = Windows.Forms.FormWindowState.Minimized Then
            shortcutObject.WindowStyle = 7
        ElseIf windowStyle = Windows.Forms.FormWindowState.Maximized Then
            shortcutObject.WindowStyle = 3
        End If
        
        shortcutObject.Save
        
        Return shortcutObject.FullName
    End Function
    
    ''' <summary>Interface for handling WScript.Shell shortcut objects. Use with GetShortcutInfo(shortcutPath) As IWshShortcut</summary>
    <ComImport, TypeLibType(CShort(&H1040)), Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")> _
    Interface IWshShortcut
        <DispId(0)> _
        ReadOnly Property FullName() As String
        <DispId(&H3e8)> _
        Property Arguments() As String
        ''' <summary>Shortcut Comment.</summary>
        <DispId(&H3e9)> _
        Property Description() As String
        <DispId(&H3ea)> _
        Property Hotkey() As String
        <DispId(&H3eb)> _
        Property IconLocation() As String
        <DispId(&H3ec)> _
        WriteOnly Property RelativePath() As String
        <DispId(&H3ed)> _
        Property TargetPath() As String
        ''' <summary>Shortcut "Run" combobox. 1=Normal, 3=Maximized, 7=Minimized.</summary>
        <DispId(&H3ee)> _
        Property WindowStyle() As Integer
        <DispId(&H3ef)> _
        Property WorkingDirectory() As String
        <TypeLibFunc(CShort(&H40)), DispId(&H7d0)> _
        Sub Load(<[In], MarshalAs(UnmanagedType.BStr)> PathLink As String)
        <DispId(&H7d1)> _
        Sub Save()
    End Interface
    
    
    ' =================================== PickIconDialogShow ===================================
    
    ' Link: https://www.pinvoke.net/default.aspx/shell32.pickicondlg
    ' Link: https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-pickicondlg
    ''' <summary>Shows a dialog for the user to choose an icon file and index.</summary>
    ''' <param name="filePath">Path of the initial file to be loaded. Use the same variable to get the selected file.</param>
    ''' <param name="iconIndex">Initial Index to be preselected. Use the same variable to get the selected index.</param>
    ''' <param name="OwnerHandle">Use Me.Handle to make the PickIconDialog show as a Dialog - i.e. blocking your applications interface until dialog is closed.</param>
    ''' <returns>True if accepted, False if cancelled.</returns>
    Shared Function PickIconDialogShow(ByRef filePath As String, ByRef iconIndex As Integer, Optional OwnerHandle As IntPtr = Nothing) As Boolean
        Dim filePathBuffer As String = filePath.PadRight(1024, Chr(0))
        
        Dim result As Integer = PickIconDlg(OwnerHandle, filePathBuffer, filePathBuffer.Length, iconIndex)
        
        filePath = filePathBuffer.Remove(filePathBuffer.IndexOf(Chr(0)))
        
        If result = 1 Then
            Return True
        ElseIf result = 0
            Return False
        Else
            Throw New Exception("Unknown error! PickIconDlg return value: " & result & _
                vbNewLine & "filePath: " & filePath & vbNewLine & "iconIndex: " & iconIndex)
        End If
    End Function
    
    Private Declare Unicode Function PickIconDlg Lib "Shell32" Alias "PickIconDlg" (hwndOwner As IntPtr, lpstrFile As String, nMaxFile As Integer, ByRef lpdwIconIndex As Integer) As Integer
    
    
    ' =================================== ExtractIconByIndex ===================================
    
    ' Link: https://stackoverflow.com/q/37261353/2999220 (last half)
    ''' <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
    ''' <param name="filePath">The path to the file that contains an image.</param>
    ''' <param name="iconIndex">Index to extract the icon from. If this is a positive number, it refers to the zero-based position of the icon in the file. If this is a negative number, it refers to the icon's resource ID.</param>
    ''' <param name="iconSize">Size of icon to extract. Size is measured in pixels. Pass 0 to specify default icon size. Default: 0.</param>
    ''' <returns>The System.Drawing.Icon representation of the image that is contained in the specified file.</returns>
    Shared Function ExtractIconByIndex(filePath As String, iconIndex As Integer, Optional iconSize As UInteger = 0) As Drawing.Icon
        If Not File.Exists(filePath) Then Throw New FileNotFoundException("File """ & filePath & """ not found!")
        
        Dim hiconLarge As IntPtr
        Dim HRESULT As Integer = SHDefExtractIcon(filePath, iconIndex, 0, hiconLarge, Nothing, iconSize)
        
        If HRESULT = 0 Then     ' S_OK: Success
            Return Drawing.Icon.FromHandle(hiconLarge)
        ElseIf HRESULT = 1 Then ' S_FALSE: The requested icon is not present
            Throw New ArgumentOutOfRangeException("iconIndex", "The requested icon index is not present in the specified file.")
        Else 'If HRESULT = 2    ' E_FAIL: The file cannot be accessed, or is being accessed through a slow link
            Throw New Win32Exception
        End If
    End Function
    
    <DllImport("Shell32.dll", SetLastError := False)> _
    Private Shared Function SHDefExtractIcon(ByVal iconFile As String, ByVal iconIndex As Integer, ByVal flags As UInteger,
    ByRef hiconLarge As IntPtr, ByRef hiconSmall As IntPtr, ByVal iconSize As UInteger) As Integer
    End Function
    
    
    ' =================================== File Compression ===================================
    
    ''' <summary>Compresses the specified file using NTFS compression.</summary>
    ''' <param name="path">Path to the file to compress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not.</param>
    ''' <returns>Whether the file was compressed successfully or not.</returns>
    Shared Function CompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        Return SetCompression(path, True, showWindow)
    End Function
    
    ''' <summary>Decompresses the specified file using NTFS compression.</summary>
    ''' <param name="path">Path to the file to decompress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not.</param>
    ''' <returns>Whether the file was decompressed successfully or not.</returns>
    Shared Function UncompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        Return SetCompression(path, False, showWindow)
    End Function
    
    ' Link: http://www.thescarms.com/dotnet/NTFSCompress.aspx
    ' Link: https://msdn.microsoft.com/en-us/library/windows/desktop/aa364592(v=vs.85).aspx
    ''' <summary>Compress or decompress the specified file using NTFS compression.</summary>
    ''' <param name="path">Path to the file to (de)compress.</param>
    ''' <param name="compress">True to compress, False to decompress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not (TODO).</param>
    ''' <returns>Whether the file was (de)compressed successfully or not.</returns>
    Shared Function SetCompression(path As String, compress As Boolean, Optional showWindow As Boolean = True) As Boolean
        Dim lpInBuffer As Short
        If compress Then
            lpInBuffer = 1
        Else
            lpInBuffer = 0
        End If
        
        Try
            Dim FilePropertiesStream As FileStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            DeviceIoControl(FilePropertiesStream.SafeFileHandle.DangerousGetHandle, &H9c040, lpInBuffer, 2, IntPtr.Zero, 0, 0, IntPtr.Zero)
            
            FilePropertiesStream.Flush(True)
            FilePropertiesStream.SafeFileHandle.Dispose
            FilePropertiesStream.Dispose
            FilePropertiesStream.Close
            
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    
    <DllImport("Kernel32.dll")> _
    Private Shared Function DeviceIoControl(hDevice As IntPtr, dwIoControlCode As Integer, lpInBuffer As Short, nInBufferSize As Integer, _
    lpOutBuffer As IntPtr, nOutBufferSize As Integer, ByRef lpBytesReturned As Integer, lpOverlapped As IntPtr) As Integer
    End Function
    
    
    ' =================================== GetCompressedSize ===================================
    
    ' Link: http://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    ' Link: https://stackoverflow.com/a/22508299/2999220
    ' Link: https://stackoverflow.com/a/1650868/2999220 (Win32Exception handling)
    ''' <summary>Gets the compressed size of a specified file. Throws IOException on failure.</summary>
    ''' <param name="path">Path to the file to get size for.</param>
    ''' <returns>The compressed size of the file or the size of the file if file isn't compressed.</returns>
    Shared Function GetCompressedSize(path As String) As Double
        Dim sizeMultiplier As IntPtr
        Dim fileLength As Long = Convert.ToInt64(GetCompressedFileSize(path, sizeMultiplier))
        If fileLength = 4294967295 Then ' decimal representation of &HFFFFFFFF
            Dim Win32Error As Integer = Marshal.GetLastWin32Error()
            Dim errorException As Win32Exception = New Win32Exception(Win32Error)
            If Win32Error <> 0 Then Throw New IOException(errorException.Message, errorException)
        End If
        Dim size As Double = (UInteger.MaxValue + 1) * CLng(sizeMultiplier) + fileLength
        Return size
    End Function
    
    Private Declare Function GetCompressedFileSize Lib "kernel32" Alias "GetCompressedFileSizeA"(ByVal lpFileName As String, ByRef lpFileSizeHigh As IntPtr) As UInteger
    
    
    ' =================================== GetOpenWith ===================================
    
    ' Link: http://www.vb-helper.com/howto_get_associated_program.html
    ''' <summary>Gets the path to the program specified to open a file.</summary>
    ''' <param name="filePath">The file to get the OpenWith program for.</param>
    ''' <returns>OpenWith program path, "Filetype not associated!" if none, or "File not found!"</returns>
    Shared Function GetOpenWith(filePath As String) As String
        If Not File.Exists(filePath) Then
            Return "File not found!"
        End If
        
        Dim FileProperties As New FileInfo(filePath)
        
        Dim result As String = Space$(1024)
        FindExecutable(FileProperties.Name, FileProperties.DirectoryName & Path.DirectorySeparatorChar, result)
        
        Dim returnString As String = Strings.Left$(result, InStr(result, Chr(0)) - 1)
        If returnString = "" Then
            Return "Filetype not associated!"
        Else
            Return returnString
        End If
    End Function
    
    Private Declare Function FindExecutable Lib "shell32.dll" Alias "FindExecutableA"(lpFile As String, lpDirectory As String, lpResult As String) As Long
    
    
    ' =================================== MouseClick ===================================
    
    ' Link: https://stackoverflow.com/a/2416762/2999220
    ' Link: http://pinvoke.net/default.aspx/user32.mouse_event (Additional buttons)
    ''' <summary>Performs a mouse click at the current cursor position.</summary>
    ''' <param name="button">MouseButton to press.</param>
    Shared Sub MouseClick(button As MouseButton)
        Select Case button
            Case MouseButton.LeftClick
                mouse_event(MouseButton.LeftDown Or MouseButton.LeftUp, 0, 0, 0, 0)
            Case MouseButton.RightClick
                mouse_event(MouseButton.RightDown Or MouseButton.RightUp, 0, 0, 0, 0)
            Case MouseButton.MiddleClick
                mouse_event(MouseButton.MiddleDown Or MouseButton.MiddleUp, 0, 0, 0, 0)
            Case MouseButton.XClick
                mouse_event(MouseButton.XDown Or MouseButton.XUp, 0, 0, 0, 0)
            Case Else
                mouse_event(button, 0, 0, 0, 0)
        End Select
        
'        Const MOUSEEVENTF_MOVE = &H1
'        Const MOUSEEVENTF_LEFTDOWN = &H2
'        Const MOUSEEVENTF_LEFTUP = &H4
'        Const MOUSEEVENTF_RIGHTDOWN = &H8
'        Const MOUSEEVENTF_RIGHTUP = &H10
'        Const MOUSEEVENTF_MIDDLEDOWN = &H20
'        Const MOUSEEVENTF_MIDDLEUP = &H40
'        Const MOUSEEVENTF_XDOWN = &H80
'        Const MOUSEEVENTF_XUP = &H100
'        Const MOUSEEVENTF_WHEEL = &H800
'        Const MOUSEEVENTF_HWHEEL = &H1000
'        Const MOUSEEVENTF_ABSOLUTE = &H8000
    End Sub
    
    <DllImport("user32.dll", CharSet := CharSet.Auto, CallingConvention := CallingConvention.StdCall)> _
    Private Shared Sub mouse_event(dwFlags As MouseButton, dx As UInteger, dy As UInteger, cButtons As UInteger, dwExtraInfo As UInteger)
    End Sub
    
    
    ' =================================== ShowProperties ===================================
    
    ' Link: https://stackoverflow.com/a/1936957/2999220
    ''' <summary>Opens the Windows properties window for a path.</summary>
    ''' <param name="path">The path to show the window for.</param>
    ''' <param name="tab">Optional tab to open to. Beware, this name is Windows version-specific!</param>
    ''' <returns>Whether the properties window was shown successfully or not.</returns>
    Shared Function ShowProperties(path As String, Optional tab As String = Nothing) As Boolean
        Dim info As New ShellExecuteInfo
        info.cbSize = Marshal.SizeOf(info)
        info.lpVerb = "properties"
        info.lpFile = path
        If tab <> Nothing Then info.lpParameters = tab
        info.nShow = 5  'SW_SHOW
        info.fMask = 12 'SEE_MASK_INVOKEIDLIST
        Return ShellExecuteEx(info)
    End Function
    
    Private Declare Auto Function ShellExecuteEx Lib "shell32.dll"(ByRef lpExecInfo As ShellExecuteInfo) As Boolean
    
    <StructLayout(LayoutKind.Sequential, CharSet := CharSet.Auto)> _
    Private Structure ShellExecuteInfo
        Public cbSize As Integer
        Public fMask As UInteger
        Public hwnd As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpVerb As String
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpFile As String
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpParameters As String
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpDirectory As String
        Public nShow As Integer
        Public hInstApp As IntPtr
        Public lpIDList As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)> _
        Public lpClass As String
        Public hkeyClass As IntPtr
        Public dwHotKey As UInteger
        Public hIcon As IntPtr
        Public hProcess As IntPtr
    End Structure
End Class
