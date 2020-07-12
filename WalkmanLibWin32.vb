Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports Microsoft.Win32.SafeHandles

Public Enum SymbolicLinkType ' used for CreateSymLink
    ''' <summary>The target is a file.</summary>
    File = 0
    ''' <summary>The target is a directory.</summary>
    Directory = 1
End Enum

Public Enum MouseButton ' used for MouseClick
    ''' <summary>Performs a LeftClick by running LeftDown and LeftUp.</summary>
    LeftClick = LeftDown Or LeftUp
    ''' <summary>Holds the left mouse button.</summary>
    LeftDown = &H2
    ''' <summary>Releases the left mouse button.</summary>
    LeftUp = &H4
    ''' <summary>Performs a RightClick by running RightDown and RightUp.</summary>
    RightClick = RightDown Or RightUp
    ''' <summary>Holds the right mouse button.</summary>
    RightDown = &H8
    ''' <summary>Releases the right mouse button.</summary>
    RightUp = &H10
    ''' <summary>Performs a MiddleClick by running MiddleDown and MiddleUp.</summary>
    MiddleClick = MiddleDown Or MiddleUp
    ''' <summary>Holds the right mouse button.</summary>
    MiddleDown = &H20
    ''' <summary>Releases the right mouse button.</summary>
    MiddleUp = &H40
    ''' <summary>Performs a XClick by running XDown and XUp.</summary>
    XClick = XDown Or XUp
    ''' <summary>Holds the X mouse button. (?)</summary>
    XDown = &H80
    ''' <summary>Releases the X mouse button. (?)</summary>
    XUp = &H100
End Enum

Partial Public Class WalkmanLib
    Const MAX_FILE_PATH As Integer = 32767 ' Maximum LongFileName length

#Region "CreateHardLink"
    ' Link: http://pinvoke.net/default.aspx/kernel32.CreateHardLink
    ' Link (native error codes): https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes#system-error-codes
    ''' <summary>Creates a hardlink to an existing file.</summary>
    ''' <param name="symlinkPath">Path to the hardlink file to create.</param>
    ''' <param name="targetPath">Absolute or relative path to the existing file to link to. If relative, target is relative to current directory.</param>
    Shared Sub CreateHardLink(hardlinkPath As String, existingFilePath As String)
        If CreateHardLink(hardlinkPath, existingFilePath, IntPtr.Zero) = False Then

            Dim errorException As New Win32Exception
            If errorException.NativeErrorCode = 2 Then
                'ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                If Not File.Exists(existingFilePath) Then
                    Throw New FileNotFoundException("The hardlink target does not exist", existingFilePath, errorException)
                End If
            ElseIf errorException.NativeErrorCode = 3 Then
                'ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                If Not Directory.Exists(New FileInfo(hardlinkPath).DirectoryName) Then ' "New FileInfo(hardlinkPath)" throws an exception on invalid characters in path - perfect!
                    Throw New DirectoryNotFoundException("The path to the new hardlink does not exist", errorException)
                End If
            ElseIf errorException.NativeErrorCode = 183 Then
                'ERROR_ALREADY_EXISTS: Cannot create a file when that file already exists
                If File.Exists(hardlinkPath) Or Directory.Exists(hardlinkPath) Then
                    Throw New IOException("The hardlink path already exists", errorException)
                End If
            ElseIf errorException.NativeErrorCode = 5 Then
                'ERROR_ACCESS_DENIED: Access is denied
                Throw New UnauthorizedAccessException("Access to the new hardlink path is denied", errorException)
            End If
            Throw errorException
        End If
    End Sub

    'https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createhardlinkw
    'https://www.pinvoke.net/default.aspx/kernel32/CreateHardLink.html
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function CreateHardLink(lpFileName As String, lpExistingFileName As String, lpSecurityAttributes As IntPtr) As Boolean
    End Function
#End Region

#Region "CreateSymLink"
    ' Link: https://stackoverflow.com/a/11156870/2999220
    ''' <summary>Creates a file or directory symbolic link.</summary>
    ''' <param name="symlinkPath">Path to the symbolic link file to create.</param>
    ''' <param name="targetPath">Absolute or relative path to the target of the shortcut. If relative, target is relative to the symbolic link file.</param>
    ''' <param name="targetType">Type of the target. If incorrect target type is supplied, the system will act as if the target doesn't exist.</param>
    Shared Sub CreateSymLink(symlinkPath As String, targetPath As String, targetType As SymbolicLinkType)
        ' https://blogs.windows.com/windowsdeveloper/2016/12/02/symlinks-windows-10/
        'SYMBOLIC_LINK_FLAG_ALLOW_UNPRIVILEGED_CREATE = 0x02
        targetType = targetType Or DirectCast(2, SymbolicLinkType)

        If CreateSymbolicLink(symlinkPath, targetPath, targetType) = False Then
            Dim errorException As New Win32Exception
            If errorException.NativeErrorCode = &H03 Then
                '0x03: ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                If Not Directory.Exists(New FileInfo(symlinkPath).DirectoryName) Then ' "New FileInfo(symlinkPath)" throws an exception on invalid characters in path - perfect!
                    Throw New DirectoryNotFoundException("The path to the symbolic link does not exist", errorException)
                End If
            ElseIf errorException.NativeErrorCode = &HB7 Then
                '0xB7: ERROR_ALREADY_EXISTS: Cannot create a file when that file already exists
                If File.Exists(symlinkPath) Or Directory.Exists(symlinkPath) Then
                    Throw New IOException("The symbolic link path already exists", errorException)
                End If
            ElseIf errorException.NativeErrorCode = &H05 Then
                '0x05: ERROR_ACCESS_DENIED: Access is denied
                Throw New UnauthorizedAccessException("Access to the symbolic link path is denied", errorException)
            ElseIf errorException.NativeErrorCode = &H522 Then
                '0x522: ERROR_PRIVILEGE_NOT_HELD: A required privilege is not held by the client.
                '   ^ this occurs when Developer Mode is not enabled, or on below Windows 10
                Throw New UnauthorizedAccessException("Symbolic link creation requires Admin privileges, or enabling developer mode", errorException)
            End If
            Throw errorException
        End If
    End Sub

    'https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createsymboliclinkw
    'https://www.pinvoke.net/default.aspx/kernel32/CreateSymbolicLink.html
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function CreateSymbolicLink(lpSymlinkFileName As String, lpTargetFileName As String, dwFlags As SymbolicLinkType) As Boolean
    End Function
#End Region

#Region "Win32CreateFile"
    Shared Function Win32CreateFile(fileName As String, fileAccess As Win32FileAccess, shareMode As FileShare,
                                    fileMode As FileMode, flagsAndAttributes As Win32FileAttribute) As SafeFileHandle
        Dim handle As SafeFileHandle = CreateFile(fileName, fileAccess, shareMode, IntPtr.Zero, fileMode, flagsAndAttributes, IntPtr.Zero)

        If handle.IsInvalid Then
            Dim errorException As New Win32Exception
            If errorException.NativeErrorCode = 2 Then
                'ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                Throw New FileNotFoundException(errorException.Message, fileName, errorException)
            ElseIf errorException.NativeErrorCode = 3 Then
                'ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                Throw New DirectoryNotFoundException("The path to the file does not exist", errorException)
            ElseIf errorException.NativeErrorCode = 5 Then
                'ERROR_ACCESS_DENIED: Access is denied
                Throw New UnauthorizedAccessException("Access to the file path is denied", errorException)
            ElseIf errorException.NativeErrorCode = 183 Then
                'ERROR_ALREADY_EXISTS: Cannot create a file when that file already exists
                Throw New IOException("The target path already exists", errorException)
            End If
            Throw errorException
        Else
            Return handle
        End If
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew
    'https://www.pinvoke.net/default.aspx/kernel32/CreateFile.html
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function CreateFile(lpFileName As String, dwDesiredAccess As Win32FileAccess,
                                       dwShareMode As FileShare, lpSecurityAttributes As IntPtr,
                                       dwCreationDisposition As FileMode, dwFlagsAndAttributes As Win32FileAttribute,
                                       hTemplateFile As IntPtr) As SafeFileHandle
    End Function

    <Flags>
    Public Enum Win32FileAttribute As UInteger
        AttributeReadOnly = &H1
        AttributeHidden = &H2
        AttributeSystem = &H4
        AttributeDirectory = &H10
        AttributeArchive = &H20
        AttributeDevice = &H40
        AttributNormal = &H80
        AttributeTemporary = &H100
        AttributeSparseFile = &H200
        AttributeReparsePoint = &H400
        AttributeCompressed = &H800
        AttributeOffline = &H1000
        AttributeNotContentIndexed = &H2000
        AttributeEncrypted = &H4000
        AttributeIntegrityStream = &H8000
        AttributeVirtual = &H1_0000
        AttributeNoScrubData = &H2_0000
        AttributeEA = &H4_0000
        AttributeRecallOnOpen = &H4_0000
        AttributePinned = &H8_0000
        AttributeUnpinned = &H10_0000
        AttributeRecallOnDataAccess = &H40_0000
        ''' <summary>The file data is requested, but it should continue to be located in remote storage. It should not be transported back to
        ''' local storage. This flag is for use by remote storage systems.</summary>
        FlagOpenNoRecall = &H10_0000
        ''' <summary>
        ''' Normal reparse point processing will not occur; CreateFile will attempt to open the reparse point. When a file is opened, a file handle is returned,
        ''' whether or not the filter that controls the reparse point is operational.
        ''' <br />This flag cannot be used with the <see cref="FileMode.Create"/> flag.
        ''' <br />If the file is not a reparse point, then this flag is ignored.
        ''' </summary>
        FlagOpenReparsePoint = &H20_0000
        ''' <summary>
        ''' The file or device is being opened with session awareness. If this flag is not specified, then per-session devices (such as a device using RemoteFX
        ''' USB Redirection) cannot be opened by processes running in session 0. This flag has no effect for callers not in session 0.
        ''' This flag Is supported only on server editions of Windows.
        ''' <br />Windows Server 2008 R2 And Windows Server 2008: This flag Is Not supported before Windows Server 2012.
        ''' </summary>
        FlagSessionAware = &H80_0000
        ''' <summary>
        ''' Access will occur according to POSIX rules. This includes allowing multiple files with names, differing only in case, for file systems that support that naming.
        ''' Use care when using this option, because files created with this flag may not be accessible by applications that are written for MS-DOS or 16-bit Windows.
        ''' </summary>
        FlagPosixSemantics = &H100_0000
        ''' <summary>
        ''' The file is being opened or created for a backup or restore operation. The system ensures that the calling process overrides file security checks when the
        ''' process has SE_BACKUP_NAME And SE_RESTORE_NAME privileges.
        ''' <br />You must set this flag to obtain a handle to a directory. A directory handle can be passed to some functions instead of a file handle.
        ''' </summary>
        FlagBackupSemantics = &H200_0000
        ''' <summary>
        ''' The file is to be deleted immediately after all of its handles are closed, which includes the specified handle and any other open or duplicated handles.
        ''' If there are existing open Handles to a file, the call fails unless they were all opened with the <see cref="FileShare.Delete"/> share mode.
        ''' <br />Subsequent open requests for the file fail, unless the <see cref="FileShare.Delete"/> share mode is specified.
        ''' </summary>
        FlagDeleteOnClose = &H400_0000
        ''' <summary>
        ''' Access is intended to be sequential from beginning to end. The system can use this as a hint to optimize file caching.
        ''' <br />This flag should not be used if read-behind (that is, reverse scans) will be used.
        ''' <br />This flag has no effect if the file system does not support cached I/O And <see cref="NoBuffering"/>.
        ''' </summary>
        FlagSequentialScan = &H800_0000
        ''' <summary>
        ''' Access is intended to be random. The system can use this as a hint to optimize file caching.
        ''' <br />This flag has no effect if the file system does not support cached I/O And <see cref="NoBuffering"/>.
        ''' </summary>
        FlagRandomAccess = &H1000_0000
        ''' <summary>
        ''' The file or device is being opened with no system caching for data reads and writes. This flag does not affect hard disk caching or memory mapped files.
        ''' <br />There are strict requirements for successfully working with files opened with CreateFile Using the <see cref="NoBuffering"/> flag.
        ''' </summary>
        FlagNoBuffering = &H2000_0000
        ''' <summary>
        ''' The file or device is being opened or created for asynchronous I/O.
        ''' When subsequent I/O operations are completed on this handle, the event specified in the OVERLAPPED structure will be set to the signaled state.
        ''' <br />If this flag is specified, the file can be used for simultaneous read and write operations.
        ''' <br />If this flag is not specified, then I/O operations are serialized, even if the calls to the read and write functions specify an OVERLAPPED Structure.
        ''' </summary>
        FlagOverlapped = &H4000_0000
        ''' <summary>Write operations will not go through any intermediate cache, they will go directly to disk.</summary>
        FlagWriteThrough = &H8000_0000UI
    End Enum

    <Flags>
    Public Enum Win32FileAccess As UInteger
        ''' <summary>[File & Pipe]</summary>
        FileReadData = &H1
        ''' <summary>[Directory]</summary>
        FileListDirectory = &H1
        ''' <summary>[File & Pipe]</summary>
        FileWriteData = &H2
        ''' <summary>[Directory]</summary>
        FileAddFile = &H2
        ''' <summary>[File] The right to append data to the file. (For local files, write operations will not overwrite
        ''' existing data if this flag is specified without <see cref="FileWriteData"/>.)
        ''' </summary>
        FileAppendData = &H4
        ''' <summary>[Directory]</summary>
        FileAddSubdirectory = &H4
        ''' <summary>[Pipe]</summary>
        FileCreatePipeInstance = &H4
        ''' <summary>[File & Directory]</summary>
        FileReadEA = &H8
        ''' <summary>[File & Directory]</summary>
        FileWriteEA = &H10
        ''' <summary>[File]</summary>
        FileExecute = &H20
        ''' <summary>[Directory] The right to traverse the directory. By default, users are assigned the BYPASS_TRAVERSE_CHECKING privilege, which ignores the <see cref="Traverse"/> access right.</summary>
        FileTraverse = &H20
        ''' <summary>[Directory] The right to delete a directory and all the files it contains, including read-only files.</summary>
        FileDeleteChild = &H40
        ''' <summary>[All]</summary>
        FileReadAttributes = &H80
        ''' <summary>[All]</summary>
        FileWriteAttributes = &H100
        Delete = &H1_0000
        ''' <summary>Read access to the owner, group, and discretionary access control list (DACL) of the security descriptor.</summary>
        ReadControl = &H2_0000
        ''' <summary>Write access to the discretionary access control list (DACL).</summary>
        WriteDac = &H4_0000
        ''' <summary>Write access to owner.</summary>
        WriteOwner = &H8_0000
        ''' <summary>Synchronize access.</summary>
        Synchronize = &H10_0000
        StandardRightsRequired = &HF_0000
        StandardRightsRead = ReadControl
        StandardRightsWrite = ReadControl
        StandardRightsExecute = ReadControl
        StandardRightsAll = &H1F_0000
        SpecificRightsAll = &HFFFF
        ''' <summary>
        ''' Used to indicate access to a system access control list (SACL). This type of access requires the calling process to
        ''' have the SE_SECURITY_NAME (Manage auditing and security log) privilege. If this flag is set in the access mask of
        ''' an audit access ACE (successful or unsuccessful access), the SACL access will be audited.
        ''' </summary>
        AccessSystemSecurity = &H100_0000
        MaximumAllowed = &H200_0000
        GenericAll = &H1000_0000
        GenericExecute = &H2000_0000
        GenericWrite = &H4000_0000
        GenericRead = &H8000_0000UI
        FileGenericRead = StandardRightsRead Or FileReadData Or FileReadAttributes Or FileReadEA Or Synchronize
        FileGenericWrite = StandardRightsWrite Or FileWriteData Or FileWriteAttributes Or FileWriteEA Or FileAppendData Or Synchronize
        FileGenericExecute = StandardRightsExecute Or FileReadAttributes Or FileExecute Or Synchronize
        FileAllAccess = StandardRightsRequired Or Synchronize Or &H1FF
    End Enum
#End Region

#Region "GetSymlinkTarget"
    ' Link: https://stackoverflow.com/a/33487494/2999220
    ''' <summary>Gets the target of a symbolic link, directory junction or volume mountpoint. Throws ComponentModel.Win32Exception on error.</summary>
    ''' <param name="path">Path to the symlink to get the target of.</param>
    ''' <returns>The fully qualified path to the target.</returns>
    Shared Function GetSymlinkTarget(path As String) As String
        Dim returnString As String = ""

        Using hFile As SafeFileHandle = Win32CreateFile(path, Win32FileAccess.FileReadEA,
                                                        FileShare.ReadWrite Or FileShare.Delete, FileMode.Open,
                                                        Win32FileAttribute.FlagBackupSemantics)
            Dim stringBuilderTarget As New Text.StringBuilder(MAX_FILE_PATH)
            Dim result As UInteger = GetFinalPathNameByHandle(hFile.DangerousGetHandle, stringBuilderTarget, MAX_FILE_PATH, 0)

            If result = 0 Then Throw New Win32Exception()
            returnString = stringBuilderTarget.ToString()
        End Using

        returnString = returnString.Substring(4) ' remove "\\?\" at the beginning
        If returnString.StartsWith("UNC\") Then  ' change "UNC\[IP]\" to proper "\\[IP]\"
            returnString = "\" & returnString.Substring(3)
        End If

        Return returnString
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfinalpathnamebyhandlew
    'https://www.pinvoke.net/default.aspx/shell32/GetFinalPathNameByHandle.html
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function GetFinalPathNameByHandle(hFile As IntPtr, lpszFilePath As Text.StringBuilder,
                                                     cchFilePath As UInteger, dwFlags As UInteger) As UInteger
    End Function
#End Region

#Region "Shortcut Management"
    ' Link: https://stackoverflow.com/a/14141782/2999220
    ' Link: https://www.tek-tips.com/viewthread.cfm?qid=850335
    ''' <summary>Gets a shortcut property object to retrieve info about a shortcut.</summary>
    ''' <param name="shortcutPath">Path to the shortcut file.</param>
    ''' <returns>Shortcut object of type IWshShortcut - either use WalkmanLib.IWshShortcut or ComImport your own interface.</returns>
    Shared Function GetShortcutInfo(shortcutPath As String) As IWshShortcut
        Dim WSH_Type As Type = Type.GetTypeFromProgID("WScript.Shell")
        Dim WSH_Activated As Object = Activator.CreateInstance(WSH_Type)

        If Not shortcutPath.EndsWith(".lnk", True, Nothing) Then shortcutPath &= ".lnk"
        Dim WSH_InvokeMember As Object = WSH_Type.InvokeMember("CreateShortcut", Reflection.BindingFlags.InvokeMethod, Nothing, WSH_Activated, New Object() {shortcutPath})

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
    Shared Function CreateShortcut(shortcutPath As String, Optional targetPath As String = Nothing, Optional arguments As String = Nothing, Optional workingDirectory As String = Nothing,
        Optional iconPath As String = Nothing, Optional comment As String = Nothing, Optional shortcutKey As String = Nothing, Optional windowStyle As Windows.Forms.FormWindowState = Windows.Forms.FormWindowState.Normal) As String
        Dim shortcutObject As IWshShortcut = GetShortcutInfo(shortcutPath)

        If targetPath <> Nothing Then       shortcutObject.TargetPath       = targetPath
        If arguments <> Nothing Then        shortcutObject.Arguments        = arguments
        If workingDirectory <> Nothing Then shortcutObject.WorkingDirectory = workingDirectory
        If iconPath <> Nothing Then         shortcutObject.IconLocation     = iconPath
        If comment <> Nothing Then          shortcutObject.Description      = comment
        If shortcutKey <> Nothing Then      shortcutObject.Hotkey           = shortcutKey

        If windowStyle = Windows.Forms.FormWindowState.Normal Then
            shortcutObject.WindowStyle = 1
        ElseIf windowStyle = Windows.Forms.FormWindowState.Minimized Then
            shortcutObject.WindowStyle = 7
        ElseIf windowStyle = Windows.Forms.FormWindowState.Maximized Then
            shortcutObject.WindowStyle = 3
        End If

        shortcutObject.Save()

        Return shortcutObject.FullName
    End Function

    ''' <summary>Interface for handling WScript.Shell shortcut objects. Use with GetShortcutInfo(shortcutPath) As IWshShortcut</summary>
    <ComImport, TypeLibType(CShort(&H1040)), Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")>
    Interface IWshShortcut
        <DispId(0)>
        ReadOnly Property FullName() As String
        <DispId(&H3E8)>
        Property Arguments() As String
        ''' <summary>Shortcut Comment.</summary>
        <DispId(&H3E9)>
        Property Description() As String
        <DispId(&H3EA)>
        Property Hotkey() As String
        <DispId(&H3EB)>
        Property IconLocation() As String
        <DispId(&H3EC)>
        WriteOnly Property RelativePath() As String
        <DispId(&H3ED)>
        Property TargetPath() As String
        ''' <summary>Shortcut "Run" combobox. 1=Normal, 3=Maximized, 7=Minimized.</summary>
        <DispId(&H3EE)>
        Property WindowStyle() As Integer
        <DispId(&H3EF)>
        Property WorkingDirectory() As String
        <TypeLibFunc(CShort(&H40)), DispId(&H7D0)>
        Sub Load(<[In], MarshalAs(UnmanagedType.BStr)> PathLink As String)
        <DispId(&H7D1)>
        Sub Save()
    End Interface
#End Region

#Region "PickIconDialogShow"
    ' Link: https://www.pinvoke.net/default.aspx/shell32.pickicondlg
    ' Link: https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-pickicondlg
    ''' <summary>Shows a dialog for the user to choose an icon file and index.</summary>
    ''' <param name="filePath">Path of the initial file to be loaded. Use the same variable to get the selected file.</param>
    ''' <param name="iconIndex">Initial Index to be preselected. Use the same variable to get the selected index.</param>
    ''' <param name="OwnerHandle">Use Me.Handle to make the PickIconDialog show as a Dialog - i.e. blocking your applications interface until dialog is closed.</param>
    ''' <returns>True if accepted, False if cancelled.</returns>
    Shared Function PickIconDialogShow(ByRef filePath As String, ByRef iconIndex As Integer, Optional OwnerHandle As IntPtr = Nothing) As Boolean
        Dim stringBuilderTarget As New Text.StringBuilder(filePath, MAX_FILE_PATH)
        Dim result As Integer = PickIconDlg(OwnerHandle, stringBuilderTarget, MAX_FILE_PATH, iconIndex)

        filePath = stringBuilderTarget.ToString()
        If result = 1 Then
            Return True
        ElseIf result = 0 Then
            Return False
        Else
            Throw New Exception("Unknown error! PickIconDlg return value: " & result &
                vbNewLine & "filePath: " & filePath & vbNewLine & "iconIndex: " & iconIndex)
        End If
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-pickicondlg
    'https://www.pinvoke.net/default.aspx/shell32/PickIconDlg.html
    <DllImport("shell32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function PickIconDlg(hwndOwner As IntPtr, pszIconPath As Text.StringBuilder, cchIconPath As UInteger, <[In], Out> ByRef piIconIndex As Integer) As Integer
    End Function
#End Region

#Region "ExtractIconByIndex"
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

    'https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shdefextracticonw
    <DllImport("shell32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function SHDefExtractIcon(pszIconFile As String, iconIndex As Integer, flags As UInteger,
                                             <Out> ByRef hiconLarge As IntPtr, <Out> ByRef hiconSmall As IntPtr,
                                             iconSize As UInteger) As Integer
    End Function
#End Region

#Region "File Compression"
    ''' <summary>Compresses the specified file or directory using NTFS compression.</summary>
    ''' <param name="path">Path to the file or directory to compress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not.</param>
    ''' <returns>Whether the file or directory was compressed successfully or not.</returns>
    Shared Function CompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        Return SetCompression(path, True, showWindow)
    End Function

    ''' <summary>Decompresses the specified file or directory using NTFS compression.</summary>
    ''' <param name="path">Path to the file or directory to decompress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not.</param>
    ''' <returns>Whether the file or directory was decompressed successfully or not.</returns>
    Shared Function UncompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        Return SetCompression(path, False, showWindow)
    End Function

    ' Link: http://www.thescarms.com/dotnet/NTFSCompress.aspx
    ' Link: https://docs.microsoft.com/en-za/windows/win32/api/winioctl/ni-winioctl-fsctl_set_compression
    ''' <summary>Compress or decompress the specified file or directory using NTFS compression.</summary>
    ''' <param name="path">Path to the file or directory to (de)compress.</param>
    ''' <param name="compress">True to compress, False to decompress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not (TODO).</param>
    ''' <returns>Whether the file or directory was (de)compressed successfully or not.</returns>
    Shared Function SetCompression(path As String, compress As Boolean, Optional showWindow As Boolean = True) As Boolean
        Dim lpInBuffer As Short
        If compress Then
            lpInBuffer = 1
        Else
            lpInBuffer = 0
        End If

        Try
            Using hFile As SafeFileHandle = Win32CreateFile(path,
                                                            Win32FileAccess.FileGenericRead Or Win32FileAccess.FileGenericWrite,
                                                            FileShare.None, FileMode.Open, Win32FileAttribute.FlagBackupSemantics)
                Return DeviceIoControl(hFile.DangerousGetHandle, &H9C040, lpInBuffer, 2, IntPtr.Zero, 0, 0, IntPtr.Zero)
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/ioapiset/nf-ioapiset-deviceiocontrol
    'https://www.pinvoke.net/default.aspx/kernel32/DeviceIoControl.html
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function DeviceIoControl(hDevice As IntPtr, dwIoControlCode As UInteger,
                                            <[In]> ByRef lpInBuffer As Short, nInBufferSize As UInteger,
                                            <Out> ByRef lpOutBuffer As IntPtr, nOutBufferSize As UInteger,
                                            ByRef lpBytesReturned As UInteger, lpOverlapped As IntPtr) As Boolean
    End Function
#End Region

#Region "GetCompressedSize"
    ' Link: http://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    ' Link: https://stackoverflow.com/a/22508299/2999220
    ' Link: https://stackoverflow.com/a/1650868/2999220 (Win32Exception handling)
    ''' <summary>Gets the compressed size of a specified file. Throws IOException on failure.</summary>
    ''' <param name="path">Path to the file to get size for.</param>
    ''' <returns>The compressed size of the file or the size of the file if file isn't compressed.</returns>
    Shared Function GetCompressedSize(path As String) As Double
        Dim sizeMultiplier As UInteger
        Dim fileLength As Long = Convert.ToInt64(GetCompressedFileSize(path, sizeMultiplier))
        If fileLength = &HFFFFFFFF Then
            Dim errorException As New Win32Exception
            If errorException.NativeErrorCode() <> 0 Then Throw New IOException(errorException.Message, errorException)
        End If
        Dim size As Double = ((UInteger.MaxValue + 1) * sizeMultiplier) + fileLength
        Return size
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getcompressedfilesizew
    'https://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function GetCompressedFileSize(lpFileName As String, <Out> ByRef lpFileSizeHigh As UInteger) As UInteger
    End Function
#End Region

#Region "GetOpenWith"
    ' Link: http://www.vb-helper.com/howto_get_associated_program.html
    ''' <summary>Gets the path to the program specified to open a file.</summary>
    ''' <param name="filePath">The file to get the OpenWith program for.</param>
    ''' <returns>OpenWith program path, "Filetype not associated!" if none, or "File not found!"</returns>
    Shared Function GetOpenWith(filePath As String) As String
        If Not File.Exists(filePath) Then
            Return "File not found!"
        End If

        Dim FileProperties As New FileInfo(filePath)
        Dim stringBuilderTarget As New Text.StringBuilder(MAX_FILE_PATH)

        FindExecutable(FileProperties.Name, FileProperties.DirectoryName & Path.DirectorySeparatorChar, stringBuilderTarget)
        Dim returnString As String = stringBuilderTarget.ToString()

        If returnString = "" Then
            Return "Filetype not associated!"
        Else
            Return returnString
        End If
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-findexecutablew
    'https://www.pinvoke.net/default.aspx/shell32/FindExecutable.html
    <DllImport("shell32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindExecutable(lpFile As String, lpDirectory As String, lpResult As Text.StringBuilder) As Long
    End Function
#End Region

#Region "MouseClick"
    ' Link: https://stackoverflow.com/a/2416762/2999220
    ' Link: http://pinvoke.net/default.aspx/user32.mouse_event (Additional buttons)
    ''' <summary>Performs a mouse click at the current cursor position.</summary>
    ''' <param name="button">MouseButton to press.</param>
    Shared Sub MouseClick(button As MouseButton)
        mouse_event(button, 0, 0, 0, 0)

        'Const MOUSEEVENTF_MOVE = &H1
        'Const MOUSEEVENTF_LEFTDOWN = &H2
        'Const MOUSEEVENTF_LEFTUP = &H4
        'Const MOUSEEVENTF_RIGHTDOWN = &H8
        'Const MOUSEEVENTF_RIGHTUP = &H10
        'Const MOUSEEVENTF_MIDDLEDOWN = &H20
        'Const MOUSEEVENTF_MIDDLEUP = &H40
        'Const MOUSEEVENTF_XDOWN = &H80
        'Const MOUSEEVENTF_XUP = &H100
        'Const MOUSEEVENTF_WHEEL = &H800
        'Const MOUSEEVENTF_HWHEEL = &H1000
        'Const MOUSEEVENTF_ABSOLUTE = &H8000
    End Sub

    'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event
    'https://www.pinvoke.net/default.aspx/user32/mouse_event.html
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Sub mouse_event(dwFlags As MouseButton, dx As UInteger, dy As UInteger, dwData As UInteger, dwExtraInfo As UInteger)
    End Sub
#End Region

#Region "ShowProperties"
    ' Link: https://stackoverflow.com/a/1936957/2999220
    ''' <summary>Opens the Windows properties window for a path.</summary>
    ''' <param name="path">The path to show the window for.</param>
    ''' <param name="tab">Optional tab to open to. Beware, this name is Windows version-specific!</param>
    ''' <returns>Whether the properties window was shown successfully or not.</returns>
    Shared Function ShowProperties(path As String, Optional tab As String = Nothing) As Boolean
        Dim info As New ShellExecuteInfo
        info.cbSize = CType(Marshal.SizeOf(info), UInteger)
        info.lpVerb = "properties"
        info.lpFile = path
        If tab <> Nothing Then info.lpParameters = tab
        info.nShow = 5  'SW_SHOW
        info.fMask = 12 'SEE_MASK_INVOKEIDLIST
        Return ShellExecuteEx(info)
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shellexecuteexw
    'https://www.pinvoke.net/default.aspx/shell32/ShellExecuteEx.html
    <DllImport("shell32", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function ShellExecuteEx(ByRef lpExecInfo As ShellExecuteInfo) As Boolean
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shellexecuteinfow
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Private Structure ShellExecuteInfo
        Public cbSize As UInteger ' cbSize is specified as a DWORD, and "A DWORD is a 32-bit unsigned integer"
        Public fMask As UInteger
        Public hwnd As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)>
        Public lpVerb As String
        <MarshalAs(UnmanagedType.LPTStr)>
        Public lpFile As String
        <MarshalAs(UnmanagedType.LPTStr)>
        Public lpParameters As String
        <MarshalAs(UnmanagedType.LPTStr)>
        Public lpDirectory As String
        Public nShow As Integer
        Public hInstApp As IntPtr
        Public lpIDList As IntPtr
        <MarshalAs(UnmanagedType.LPTStr)>
        Public lpClass As String
        Public hkeyClass As IntPtr
        Public dwHotKey As UInteger
        Public hIcon As IntPtr
        Public hProcess As IntPtr
    End Structure
#End Region
End Class
