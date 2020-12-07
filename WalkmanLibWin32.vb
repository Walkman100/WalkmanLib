Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.Win32.SafeHandles

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
    ''' <param name="targetIsDirectory">Type of the target. If incorrect target type is supplied, the system will act as if the target doesn't exist.</param>
    Shared Sub CreateSymLink(symlinkPath As String, targetPath As String, Optional targetIsDirectory As Boolean = False)
        ' https://blogs.windows.com/windowsdeveloper/2016/12/02/symlinks-windows-10/
        Dim flags As SymbolicLinkFlags = SymbolicLinkFlags.AllowUnprivilegedCreate Or
                If(targetIsDirectory, SymbolicLinkFlags.Directory, SymbolicLinkFlags.File)

        If CreateSymbolicLink(symlinkPath, targetPath, flags) = False Then
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
    Private Shared Function CreateSymbolicLink(lpSymlinkFileName As String, lpTargetFileName As String, dwFlags As SymbolicLinkFlags) As Boolean
    End Function

    Private Enum SymbolicLinkFlags ' used for CreateSymLink
        ''' <summary>The link target is a file.</summary>
        File = &H00
        ''' <summary>The link target is a directory.</summary>
        Directory = &H01
        ''' <summary>
        ''' Specify this flag to allow creation of symbolic links when the process is not elevated.
        ''' <br />Developer Mode must first be enabled on the machine before this option will function.
        ''' </summary>
        AllowUnprivilegedCreate = &H02
    End Enum
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
            ElseIf errorException.NativeErrorCode = 32 Then
                'ERROR_SHARING_VIOLATION: The process cannot access the file because it is being used by another process
                Throw New IOException(errorException.Message, errorException)
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

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew#FILE_ATTRIBUTE_ARCHIVE
    <Flags>
    Public Enum Win32FileAttribute As UInteger
        None =                          0
        AttributeReadOnly =             &H00000001
        AttributeHidden =               &H00000002
        AttributeSystem =               &H00000004
        AttributeDirectory =            &H00000010
        AttributeArchive =              &H00000020
        AttributeDevice =               &H00000040
        AttributNormal =                &H00000080
        AttributeTemporary =            &H00000100
        AttributeSparseFile =           &H00000200
        AttributeReparsePoint =         &H00000400
        AttributeCompressed =           &H00000800
        AttributeOffline =              &H00001000
        AttributeNotContentIndexed =    &H00002000
        AttributeEncrypted =            &H00004000
        AttributeIntegrityStream =      &H00008000
        AttributeVirtual =              &H00010000
        AttributeNoScrubData =          &H00020000
        AttributeEA =                   &H00040000
        AttributeRecallOnOpen =         &H00040000
        AttributePinned =               &H00080000
        AttributeUnpinned =             &H00100000
        AttributeRecallOnDataAccess =   &H00400000
        ''' <summary>The file data is requested, but it should continue to be located in remote storage. It should not be transported back to
        ''' local storage. This flag is for use by remote storage systems.</summary>
        FlagOpenNoRecall =              &H00100000
        ''' <summary>
        ''' Normal reparse point processing will not occur; CreateFile will attempt to open the reparse point. When a file is opened, a file handle is returned,
        ''' whether or not the filter that controls the reparse point is operational.
        ''' <br />This flag cannot be used with the <see cref="FileMode.Create"/> flag.
        ''' <br />If the file is not a reparse point, then this flag is ignored.
        ''' </summary>
        FlagOpenReparsePoint =          &H00200000
        ''' <summary>
        ''' The file or device is being opened with session awareness. If this flag is not specified, then per-session devices (such as a device using RemoteFX
        ''' USB Redirection) cannot be opened by processes running in session 0. This flag has no effect for callers not in session 0.
        ''' This flag Is supported only on server editions of Windows.
        ''' <br />Windows Server 2008 R2 And Windows Server 2008: This flag Is Not supported before Windows Server 2012.
        ''' </summary>
        FlagSessionAware =              &H00800000
        ''' <summary>
        ''' Access will occur according to POSIX rules. This includes allowing multiple files with names, differing only in case, for file systems that support that naming.
        ''' Use care when using this option, because files created with this flag may not be accessible by applications that are written for MS-DOS or 16-bit Windows.
        ''' </summary>
        FlagPosixSemantics =            &H01000000
        ''' <summary>
        ''' The file is being opened or created for a backup or restore operation. The system ensures that the calling process overrides file security checks when the
        ''' process has SE_BACKUP_NAME And SE_RESTORE_NAME privileges.
        ''' <br />You must set this flag to obtain a handle to a directory. A directory handle can be passed to some functions instead of a file handle.
        ''' </summary>
        FlagBackupSemantics =           &H02000000
        ''' <summary>
        ''' The file is to be deleted immediately after all of its handles are closed, which includes the specified handle and any other open or duplicated handles.
        ''' If there are existing open Handles to a file, the call fails unless they were all opened with the <see cref="FileShare.Delete"/> share mode.
        ''' <br />Subsequent open requests for the file fail, unless the <see cref="FileShare.Delete"/> share mode is specified.
        ''' </summary>
        FlagDeleteOnClose =             &H04000000
        ''' <summary>
        ''' Access is intended to be sequential from beginning to end. The system can use this as a hint to optimize file caching.
        ''' <br />This flag should not be used if read-behind (that is, reverse scans) will be used.
        ''' <br />This flag has no effect if the file system does not support cached I/O And <see cref="FlagNoBuffering"/>.
        ''' </summary>
        FlagSequentialScan =            &H08000000
        ''' <summary>
        ''' Access is intended to be random. The system can use this as a hint to optimize file caching.
        ''' <br />This flag has no effect if the file system does not support cached I/O And <see cref="FlagNoBuffering"/>.
        ''' </summary>
        FlagRandomAccess =              &H10000000
        ''' <summary>
        ''' The file or device is being opened with no system caching for data reads and writes. This flag does not affect hard disk caching or memory mapped files.
        ''' <br />There are strict requirements for successfully working with files opened with CreateFile Using the <see cref="FlagNoBuffering"/> flag.
        ''' </summary>
        FlagNoBuffering =               &H20000000
        ''' <summary>
        ''' The file or device is being opened or created for asynchronous I/O.
        ''' When subsequent I/O operations are completed on this handle, the event specified in the OVERLAPPED structure will be set to the signaled state.
        ''' <br />If this flag is specified, the file can be used for simultaneous read and write operations.
        ''' <br />If this flag is not specified, then I/O operations are serialized, even if the calls to the read and write functions specify an OVERLAPPED Structure.
        ''' </summary>
        FlagOverlapped =                &H40000000
        ''' <summary>Write operations will not go through any intermediate cache, they will go directly to disk.</summary>
        FlagWriteThrough =              &H80000000UI
    End Enum

    'https://docs.microsoft.com/en-us/windows/win32/fileio/file-access-rights-constants
    'https://docs.microsoft.com/en-us/windows/win32/fileio/file-security-and-access-rights
    'https://docs.microsoft.com/en-us/windows/win32/secauthz/access-mask#code-try-1
    <Flags>
    Public Enum Win32FileAccess As UInteger
        None =                      0
        ''' <summary>[File &amp; Pipe]</summary>
        FileReadData =              &H00000001
        ''' <summary>[Directory]</summary>
        DirListDirectory =          &H00000001
        ''' <summary>[File &amp; Pipe]</summary>
        FileWriteData =             &H00000002
        ''' <summary>[Directory]</summary>
        DirAddFile =                &H00000002
        ''' <summary>[File] The right to append data to the file. (For local files, write operations will not overwrite
        ''' existing data if this flag is specified without <see cref="FileWriteData"/>.)
        ''' </summary>
        FileAppendData =            &H00000004
        ''' <summary>[Directory]</summary>
        DirAddSubdirectory =        &H00000004
        ''' <summary>[Pipe]</summary>
        PipeCreateInstance =        &H00000004
        ''' <summary>[File &amp; Directory]</summary>
        ReadEA =                    &H00000008
        ''' <summary>[File &amp; Directory]</summary>
        WriteEA =                   &H00000010
        ''' <summary>[File]</summary>
        FileExecute =               &H00000020
        ''' <summary>[Directory] The right to traverse the directory. By default, users are assigned the BYPASS_TRAVERSE_CHECKING privilege, which ignores the <see cref="Traverse"/> access right.</summary>
        DirTraverse =               &H00000020
        ''' <summary>[Directory] The right to delete a directory and all the files it contains, including read-only files.</summary>
        DirDeleteChild =            &H00000040
        ''' <summary>[All]</summary>
        ReadAttributes =            &H00000080
        ''' <summary>[All]</summary>
        WriteAttributes =           &H00000100
        ''' <summary>[All]</summary>
        Delete =                    &H00010000
        ''' <summary>Read access to the owner, group, and discretionary access control list (DACL) of the security descriptor.</summary>
        ReadControl =               &H00020000
        ''' <summary>Write access to the discretionary access control list (DACL).</summary>
        WriteDac =                  &H00040000
        ''' <summary>Write access to owner. Required to change the owner in the security descriptor for the object.</summary>
        WriteOwner =                &H00080000
        ''' <summary>Synchronize access. This enables a thread to wait until the object is in the signaled state.</summary>
        Synchronize =               &H00100000
        StandardRightsRequired =    &H000F0000
        StandardRightsRead =        ReadControl
        StandardRightsWrite =       ReadControl
        StandardRightsExecute =     ReadControl
        StandardRightsAll =         &H001F0000
        SpecificRightsAll =         &H0000FFFF
        ''' <summary>
        ''' Used to indicate access to a system access control list (SACL). This type of access requires the calling process to
        ''' have the SE_SECURITY_NAME (Manage auditing and security log) privilege. If this flag is set in the access mask of
        ''' an audit access ACE (successful or unsuccessful access), the SACL access will be audited.
        ''' </summary>
        AccessSystemSecurity =      &H01000000
        MaximumAllowed =            &H02000000
        GenericAll =                &H10000000
        GenericExecute =            &H20000000
        GenericWrite =              &H40000000
        GenericRead =               &H80000000UI
        FileGenericRead =           StandardRightsRead Or FileReadData Or ReadAttributes Or ReadEA Or Synchronize
        FileGenericWrite =          StandardRightsWrite Or FileWriteData Or WriteAttributes Or WriteEA Or FileAppendData Or Synchronize
        FileGenericExecute =        StandardRightsExecute Or ReadAttributes Or FileExecute Or Synchronize
        FileAllAccess =             StandardRightsRequired Or Synchronize Or &H1FF ' 1FF is everything before Delete (not including)
    End Enum
#End Region

#Region "GetHardlinkCount"
    ' Link: https://serverfault.com/questions/758496/get-hardlink-count-for-a-file-on-windows-without-fsutil-which-requires-admin
    ' Link: https://devblogs.microsoft.com/vbteam/to-compare-two-filenames-lucian-wischik/
    ''' <summary>Gets the count of hardlinks of a file.</summary>
    ''' <param name="path">Path to the file to get the hardlink count of.</param>
    ''' <returns>Count of links to a file. The count includes the current link (0 would be a deleted file)</returns>
    Shared Function GetHardlinkCount(path As String) As UInteger
        ' don't need to use Win32CreateFile as that's needed for directories
        Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim fileInfo As ByHandleFileInformation
            If GetFileInformationByHandle(fs.SafeFileHandle, fileInfo) = False Then
                Throw New Win32Exception()
            End If

            Return fileInfo.nNumberOfLinks
        End Using
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfileinformationbyhandle
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function GetFileInformationByHandle(hFile As SafeFileHandle, ByRef lpFileInformation As ByHandleFileInformation) As Boolean
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/ns-fileapi-by_handle_file_information
    <StructLayout(LayoutKind.Sequential)>
    Private Structure ByHandleFileInformation
        Public dwFileAttributes As UInteger
        Public ftCreationTime As ComTypes.FILETIME
        Public ftLastAccessTime As ComTypes.FILETIME
        Public ftLastWriteTime As ComTypes.FILETIME
        Public dwVolumeSerialNumber As UInteger
        Public nFileSizeHigh As UInteger
        Public nFileSizeLow As UInteger
        Public nNumberOfLinks As UInteger
        Public nFileIndexHigh As UInteger
        Public nFileIndexLow As UInteger
    End Structure
#End Region

#Region "GetHardlinkLinks"
    ' Link: https://stackoverflow.com/a/4511075/2999220
    ' Link: https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findfirstfilenamew
    ''' <summary>Gets all the links to a specified file.
    ''' Warning: A handle is used to get the links, and is only closed when all the links have been retrieved.
    ''' To ensure this handle is closed, ensure all the links are retrieved.</summary>
    ''' <param name="path">Path to the file to get links for. This path does not include the drive identifier.</param>
    ''' <returns>All a file's links.</returns>
    Shared Iterator Function GetHardlinkLinks(path As String) As IEnumerable(Of String)
        Dim INVALID_HANDLE_VALUE As IntPtr = New IntPtr(&HFFFFFFFF)
        Dim stringBuilderTarget As New Text.StringBuilder(MAX_FILE_PATH)
        Dim hFind As IntPtr = FindFirstFileName(path, 0, MAX_FILE_PATH, stringBuilderTarget)

        If hFind = INVALID_HANDLE_VALUE Then
            Dim errorException As New Win32Exception
            If errorException.NativeErrorCode = 2 Then
                'ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                If Not File.Exists(path) Then
                    Throw New FileNotFoundException(errorException.Message, path, errorException)
                End If
            ElseIf errorException.NativeErrorCode = 3 Then
                'ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                If Not Directory.Exists(New FileInfo(path).DirectoryName) Then
                    Throw New DirectoryNotFoundException(errorException.Message, errorException)
                End If
            ElseIf errorException.NativeErrorCode = 5 Then
                'ERROR_ACCESS_DENIED: Access is denied
                Throw New UnauthorizedAccessException("Access to the file path is denied", errorException)
            ElseIf errorException.NativeErrorCode = 32 Then
                'ERROR_SHARING_VIOLATION: The process cannot access the file because it is being used by another process
                Throw New IOException(errorException.Message, errorException)
            End If
            Throw errorException
        End If

        Try
            ' link path doesn't include drive letter by default
            Dim pathRoot As String = IO.Path.GetPathRoot(path)
            Yield If(stringBuilderTarget.ToString()(0) = IO.Path.DirectorySeparatorChar,
                IO.Path.Combine(pathRoot, stringBuilderTarget.ToString().Substring(1)),
                stringBuilderTarget.ToString())

            While FindNextFileName(hFind, MAX_FILE_PATH, stringBuilderTarget)
                If stringBuilderTarget.ToString()(0) = IO.Path.DirectorySeparatorChar Then
                    Yield IO.Path.Combine(pathRoot, stringBuilderTarget.ToString().Substring(1))
                Else
                    Yield stringBuilderTarget.ToString()
                End If
            End While

            Dim errorException As Win32Exception = New Win32Exception
            '                                    ERROR_HANDLE_EOF: Reached the end of the file.
            If errorException.NativeErrorCode <> &H26 Then Throw errorException
        Finally
            FindClose(hFind)
        End Try
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findfirstfilenamew
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindFirstFileName(lpFileName As String, dwFlags As UInteger, ByRef lpdwStringLength As UInteger, pLinkName As Text.StringBuilder) As IntPtr
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findnextfilenamew
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindNextFileName(hFindStream As IntPtr, ByRef lpdwStringLength As UInteger, pLinkName As Text.StringBuilder) As Boolean
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findclose
    <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindClose(hFindFile As IntPtr) As Boolean
    End Function
#End Region

#Region "GetSymlinkTarget"
    ' Link: https://stackoverflow.com/a/33487494/2999220
    ''' <summary>Gets the target of a symbolic link, directory junction or volume mountpoint. Throws ComponentModel.Win32Exception on error.</summary>
    ''' <param name="path">Path to the symlink to get the target of.</param>
    ''' <returns>The fully qualified path to the target.</returns>
    Shared Function GetSymlinkTarget(path As String) As String
        Dim returnString As String = ""

        Using hFile As SafeFileHandle = Win32CreateFile(path, Win32FileAccess.ReadEA,
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

    ''' <summary>Interface for handling WScript.Shell shortcut objects. Use with <see cref="GetShortcutInfo(String)"/> As IWshShortcut</summary>
    <ComImport, TypeLibType(&H1040S), Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")>
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
        <TypeLibFunc(&H40S), DispId(&H7D0)>
        Sub Load(<[In], MarshalAs(UnmanagedType.BStr)> PathLink As String)
        <DispId(&H7D1)>
        Sub Save()
    End Interface
#End Region

#Region "GetFileIcon"
    ' Link: https://stackoverflow.com/a/24146599/2999220
    ''' <summary>Retrieves the system icon associated with a file or filetype.</summary>
    ''' <param name="filePath">Path to the item to get icon for. Set <paramref name="checkFile"/> to <see langword="False"/> to get the filetype icon.</param>
    ''' <param name="checkFile">Specifies whether to get the icon associated with a specific file, or just for the filetype.</param>
    ''' <param name="smallIcon"><see langword="False"/> to get a 32x32 icon, <see langword="True"/> to get a 16x16 icon</param>
    ''' <param name="linkOverlay">Add the system "link" overlay to the icon</param>
    Public Shared Function GetFileIcon(filePath As String, Optional checkFile As Boolean = True, Optional smallIcon As Boolean = True, Optional linkOverlay As Boolean = False) As Drawing.Icon
        Dim flags As SHGetFileInfoFlags = SHGetFileInfoFlags.Icon
        If Not checkFile Then flags = flags Or SHGetFileInfoFlags.UseFileAttributes
        flags = flags Or If(smallIcon, SHGetFileInfoFlags.SmallIcon, SHGetFileInfoFlags.LargeIcon)
        If linkOverlay Then flags = flags Or SHGetFileInfoFlags.LinkOverlay

        Dim shInfo As New SHFILEINFO()
        If SHGetFileInfo(filePath, 0, shInfo, CType(Marshal.SizeOf(shInfo), UInteger), flags) = 0 _
                OrElse shInfo.hIcon = IntPtr.Zero Then

            Dim errorException As New Win32Exception()
            If errorException.NativeErrorCode = 2 Then
                'ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                If Not File.Exists(filePath) Then
                    Throw New FileNotFoundException("The file does not exist", filePath, errorException)
                End If
            ElseIf errorException.NativeErrorCode = 3 Then
                'ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                If Not Directory.Exists(New FileInfo(filePath).DirectoryName) Then
                    Throw New DirectoryNotFoundException("The path to the file does not exist", errorException)
                End If
            ElseIf errorException.NativeErrorCode = 5 Then
                'ERROR_ACCESS_DENIED: Access is denied
                Throw New UnauthorizedAccessException("Access to the file is denied", errorException)
            End If
            Throw errorException
        End If

        Return Drawing.Icon.FromHandle(shInfo.hIcon)
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetfileinfow
    'https://www.pinvoke.net/default.aspx/shell32/SHGetFileInfo.html
    <DllImport("shell32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function SHGetFileInfo(pszPath As String, dwFileAttributes As FileAttributes, ByRef psfi As SHFILEINFO, cbFileInfo As UInteger, uFlags As SHGetFileInfoFlags) As UInteger
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shfileinfow
    'https://www.pinvoke.net/default.aspx/Structures/SHFILEINFO.html
    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        ''' <summary>See <see cref="ContextMenu.SFGAO"/> enum for all values</summary>
        Public dwAttributes As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
        Public szTypeName As String
    End Structure

    'https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetfileinfow#shgfi_addoverlays-0x000000020
    Private Enum SHGetFileInfoFlags As UInteger
        ''' <summary>Modify <see cref="Icon"/>, causing the function to retrieve the file's large icon. The <see cref="Icon"/> flag must also be set.</summary>
        LargeIcon = &H0
        ''' <summary>
        ''' Modify <see cref="Icon"/>, causing the function to retrieve the file's small icon.
        ''' <br/>Also used to modify <see cref="SysIconIndex"/>, causing the function to return the handle to the system image list that contains small icon images.
        ''' <br/>The <see cref="Icon"/> and/or <see cref="SysIconIndex"/> flag must also be set.
        ''' </summary>
        SmallIcon = &H1
        ''' <summary>
        ''' Modify <see cref="Icon"/>, causing the function to retrieve the file's open icon.
        ''' <br/>Also used to modify <see cref="SysIconIndex"/>, causing the function to return the handle to the system image list that contains the file's small open icon.
        ''' A container object displays an open icon to indicate that the container is open.
        ''' <br/>The <see cref="Icon"/> and/or <see cref="SysIconIndex"/> flag must also be set.
        ''' </summary>
        OpenIcon = &H2
        ''' <summary>
        ''' Modify <see cref="Icon"/>, causing the function to retrieve a Shell-sized icon. If this flag is not specified the function
        ''' sizes the icon according to the system metric values. The <see cref="Icon"/> flag must also be set.
        ''' </summary>
        ShellIconSize = &H4
        ''' <summary>Indicate that <paramref name="SHGetFileInfo.pszPath"/> is the address of an ITEMIDLIST structure rather than a path name.</summary>
        PIDL = &H8
        ''' <summary>
        ''' Indicates that the function should not attempt to access the file specified by <paramref name="SHGetFileInfo.pszPath"/>. Rather, it should act as if the
        ''' file specified by <paramref name="SHGetFileInfo.pszPath"/> exists with the file attributes passed in <paramref name="SHGetFileInfo.dwFileAttributes"/>.
        ''' This flag cannot be combined with the <see cref="Attributes"/>, <see cref="ExeType"/>, or <see cref="PIDL"/> flags.
        ''' </summary>
        UseFileAttributes = &H10
        ''' <summary>Apply the appropriate overlays to the file's icon. The <see cref="Icon"/> flag must also be set.</summary>
        AddOverlays = &H20
        ''' <summary>
        ''' Return the index of the overlay icon. The value of the overlay index is returned in the upper eight bits of <see cref="SHFILEINFO.iIcon"/>.
        ''' This flag requires that the <see cref="Icon"/> be set as well.
        ''' </summary>
        OverlayIndex = &H40
        ''' <summary>
        ''' Retrieve the handle to the icon that represents the file and the index of the icon within the system image list.
        ''' The handle is copied to <see cref="SHFILEINFO.hIcon"/>, and the index is copied to <see cref="SHFILEINFO.iIcon"/>.
        ''' </summary>
        Icon = &H100
        ''' <summary>
        ''' Retrieve the display name for the file, which is the name as it appears in Windows Explorer. The name is copied to <see cref="SHFILEINFO.szDisplayName"/>.
        ''' The returned display name uses the long file name, if there is one, rather than the 8.3 form of the file name.
        ''' <br/> Note that the display name can be affected by settings such as whether extensions are shown.
        ''' </summary>
        DisplayName = &H200
        ''' <summary>Retrieve the string that describes the file's type. The string is copied to <see cref="SHFILEINFO.szTypeName"/>.</summary>
        TypeName = &H400
        ''' <summary>
        ''' Retrieve the item attributes. The attributes are copied to <see cref="SHFILEINFO.dwAttributes"/>.
        ''' These are the same attributes that are obtained from IShellFolder::GetAttributesOf.
        ''' </summary>
        Attributes = &H800
        ''' <summary>
        ''' Retrieve the name of the file that contains the icon representing the file specified by <paramref name="SHGetFileInfo.pszPath"/>, as returned by the
        ''' IExtractIcon::GetIconLocation method of the file's icon handler. Also retrieve the icon index within that file.
        ''' <br/>The name of the file containing the icon is copied to <see cref="SHFILEINFO.szDisplayName"/>. The icon's index is copied to <see cref="SHFILEINFO.iIcon"/>.
        ''' </summary>
        IconLocation = &H1000
        ''' <summary>
        ''' Retrieve the type of the executable file if <paramref name="SHGetFileInfo.pszPath"/> identifies an executable file.
        ''' The information is packed into the return value. This flag cannot be specified with any other flags.
        ''' </summary>
        ExeType = &H2000
        ''' <summary>
        ''' Retrieve the index of a system image list icon. If successful, the index is copied to <see cref="SHFILEINFO.iIcon"/>.
        ''' The return value is a handle to the system image list. Only those images whose indices are successfully copied to <see cref="SHFILEINFO.iIcon"/> are valid.
        ''' Attempting to access other images in the system image list will result in undefined behavior.
        ''' </summary>
        SysIconIndex = &H4000
        ''' <summary>Modify <see cref="Icon"/>, causing the function to add the link overlay to the file's icon. The <see cref="Icon"/> flag must also be set.</summary>
        LinkOverlay = &H8000
        ''' <summary>Modify <see cref="Icon"/>, causing the function to blend the file's icon with the system highlight color. The <see cref="Icon"/> flag must also be set.</summary>
        Selected = &H10000
        ''' <summary>
        ''' Modify <see cref="Attributes"/> to indicate that <see cref="SHFILEINFO.dwAttributes"/> contains the specific attributes that are desired.
        ''' These attributes are passed to IShellFolder::GetAttributesOf. If this flag is not specified, 0xFFFFFFFF is passed to IShellFolder::GetAttributesOf,
        ''' requesting all attributes. This flag cannot be specified with the <see cref="Icon"/> flag.
        ''' </summary>
        AttrSpecified = &H20000
    End Enum
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
                Environment.NewLine & "filePath: " & filePath & Environment.NewLine & "iconIndex: " & iconIndex)
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
    ' Link: https://docs.microsoft.com/en-us/windows/win32/api/winioctl/ni-winioctl-fsctl_set_compression
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

#Region "WaitForWindow"
    ''' <summary>Waits until a window matching search parameters closes</summary>
    ''' <param name="windowName">The window name (the window's title). If this parameter is <see langword="Nothing"/>, all window names match.</param>
    ''' <param name="windowClass">
    ''' Specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
    ''' <br/>If <paramref name="windowClass"/> is <see langword="Nothing"/>, it finds any window whose title matches the <paramref name="windowName"/> parameter.
    ''' </param>
    ''' <param name="timeout">Seconds to wait before returning</param>
    ''' <returns><see langword="True"/> if the timeout expired, <see langword="False"/> if the window was closed.</returns>
    Shared Function WaitForWindow(windowName As String, Optional windowClass As String = Nothing, Optional timeout As Integer = -1) As Boolean
        Dim hWnd As IntPtr = FindWindow(windowClass, windowName)
        If hWnd = IntPtr.Zero Then
            Dim errorException As New Win32Exception()
            If errorException.NativeErrorCode = 0 Then
                'ERROR_SUCCESS: The operation completed successfully.
                Throw New ArgumentException("Window matching the specified parameters not found!", "windowName / windowClass", errorException)
            End If
            Throw errorException
        End If

        Do While IsWindow(hWnd) AndAlso timeout <> 0
            Threading.Thread.Sleep(1000)
            If timeout > 0 Then timeout -= 1
        Loop

        Return timeout = 0
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindoww
    'https://www.pinvoke.net/default.aspx/user32/FindWindow.html
    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindow
    'https://www.pinvoke.net/default.aspx/user32/IsWindow.html
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function IsWindow(hWnd As IntPtr) As Boolean
    End Function
#End Region

#Region "WaitForWindowByThread"
    ''' <summary>
    ''' Waits until the thread hosting a window matching the specified parameters exits.
    ''' This is more efficient than <see cref="WaitForWindow"/>, but requires the Window/Thread to be running in the current process.
    ''' </summary>
    ''' <param name="windowName">The window name (the window's title). If this parameter is <see langword="Nothing"/>, all window names match.</param>
    ''' <param name="windowClass">
    ''' Specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
    ''' <br/>If <paramref name="windowClass"/> is <see langword="Nothing"/>, it finds any window whose title matches the <paramref name="windowName"/> parameter.
    ''' </param>
    ''' <param name="timeout">Miliseconds to wait before returning. The Default value is Infinite.</param>
    ''' <returns><see langword="True"/> if the timeout expired, <see langword="False"/> if the thread exited.</returns>
    Shared Function WaitForWindowByThread(windowName As String, Optional windowClass As String = Nothing, Optional timeout As UInteger = WFSO_INFINITE) As Boolean
        ' Get window handle
        Dim hWnd As IntPtr = FindWindow(windowClass, windowName)
        If hWnd = IntPtr.Zero Then
            Dim errorException As New Win32Exception()
            If errorException.NativeErrorCode = 0 Then
                'ERROR_SUCCESS: The operation completed successfully.
                Throw New ArgumentException("Window matching the specified parameters not found!", "windowName / windowClass", errorException)
            End If
            Throw errorException
        End If

        ' Get threadID for window handle
        Dim tID As UInteger = GetWindowThreadProcessId(hWnd, Nothing)
        If Not tID > 0 Then
            Throw New Win32Exception()
        End If

        ' Get thread handle for threadID
        Using handle As SafeFileHandle = OpenThread(ThreadAccess.Synchronize, False, tID)
            If handle.IsInvalid Then
                Throw New Win32Exception()
            Else

                ' Wait for handle with specified timeout
                Select Case WaitForSingleObject(handle.DangerousGetHandle, timeout)
                    Case WFSO_Val.WAIT_OBJECT_0
                        ' success condition
                    Case WFSO_Val.WAIT_ABANDONED
                        ' thread exited without releasing mutex object
                    Case WFSO_Val.WAIT_TIMEOUT
                        Return True
                    Case WFSO_Val.WAIT_FAILED
                        Throw New Win32Exception()
                End Select

            End If
        End Using

        Return False
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid
    'https://www.pinvoke.net/default.aspx/user32/GetWindowThreadProcessId.html
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function GetWindowThreadProcessId(hWnd As IntPtr, ByRef lpdwProcessId As UInteger) As UInteger
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openthread
    'https://www.pinvoke.net/default.aspx/kernel32/OpenThread.html
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function OpenThread(dwDesiredAccess As ThreadAccess, bInheritHandle As Boolean, dwThreadId As UInteger) As SafeFileHandle
    End Function
    ' SafeFileHandle has the same IsInvalid() and ReleaseHandle() members as SafeAccessTokenHandle, which is only available in .Net 4.6

    'https://docs.microsoft.com/en-us/windows/win32/procthread/thread-security-and-access-rights
    <Flags>
    Private Enum ThreadAccess As UInteger
        ''' <summary>Required to terminate a thread using TerminateThread.</summary>
        Terminate = &H1
        ''' <summary>Required to suspend or resume a thread (see SuspendThread and ResumeThread)</summary>
        SuspendResume = &H2
        ''' <summary>Required to read the context of a thread using GetThreadContext.</summary>
        GetContext = &H8
        ''' <summary>Required to write the context of a thread using SetThreadContext.</summary>
        SetContext = &H10
        ''' <summary>Required to set certain information in the thread object.</summary>
        SetInformation = &H20
        ''' <summary>Required to read certain information from the thread object, such as the exit code.</summary>
        QueryInformation = &H40
        ''' <summary>Required to set the impersonation token for a thread using SetThreadToken.</summary>
        SetThreadToken = &H80
        ''' <summary>Required to use a thread's security information directly without calling it by using a communication mechanism that provides impersonation services.</summary>
        Impersonate = &H100
        ''' <summary>Required for a server thread that impersonates a client.</summary>
        DirectImpersonation = &H200
        ''' <summary>Required to set certain information in the thread object. A handle that has the <see cref="SetInformation"/> access right is automatically granted <see cref="SetLimitedInformation"/>.</summary>
        SetLimitedInformation = &H400
        ''' <summary>Required to read certain information from the thread objects. A handle that has the <see cref="QueryInformation"/> access right is automatically granted <see cref="QueryLimitedInformation"/>.</summary>
        QueryLimitedInformation = &H800
        ''' <summary>(Found in WINNT.H header file)</summary>
        [Resume] = &H1000
        ''' <summary>Required to delete the object.</summary>
        Delete = &H10000
        ''' <summary>Read access to the owner, group, and discretionary access control list (DACL) of the security descriptor.</summary>
        ReadControl = &H20000
        ''' <summary>Write access to the discretionary access control list (DACL).</summary>
        WriteDac = &H40000
        ''' <summary>Write access to owner. Required to change the owner in the security descriptor for the object.</summary>
        WriteOwner = &H80000
        ''' <summary>Synchronize access. This enables a thread to wait until the object is in the signaled state.</summary>
        Synchronize = Win32FileAccess.Synchronize
        ''' <summary>
        ''' All possible access rights for a thread object. For Windows Server 2008/Windows Vista and up.
        ''' If this flag is specified on Windows Server 2003/Windows XP or below, the function specifying this flag fails with ERROR_ACCESS_DENIED.
        ''' </summary>
        AllAccess_VistaAndUp = Win32FileAccess.StandardRightsRequired Or Win32FileAccess.Synchronize Or &HFFFF
        ''' <summary>
        ''' All possible access rights for a thread object. For Windows Server 2003/Windows XP and below.
        ''' If this flag is specified on Windows Server 2008/Windows Vista and up, every possible access right is not granted.
        ''' </summary>
        AllAccess_XPAndBelow = Win32FileAccess.StandardRightsRequired Or Win32FileAccess.Synchronize Or &H3FF
    End Enum


    'https://docs.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-waitforsingleobject
    'https://www.pinvoke.net/default.aspx/kernel32.waitforsingleobject
    <DllImport("kernel32.dll", SetLastError:=True)>
    Private Shared Function WaitForSingleObject(handle As IntPtr, milliseconds As UInteger) As WFSO_Val
    End Function

    'https://docs.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-waitforsingleobject#return-value
    Private Enum WFSO_Val As UInteger
        ''' <summary>The state of the specified object is signaled.</summary>
        WAIT_OBJECT_0 = &H0UI
        ''' <summary>
        ''' The specified object is a mutex object that was not released by the thread that owned the mutex object before the owning thread terminated.
        ''' Ownership of the mutex object is granted to the calling thread and the mutex state is set to nonsignaled.
        ''' If the mutex was protecting persistent state information, you should check it for consistency.
        ''' </summary>
        WAIT_ABANDONED = &H80UI
        ''' <summary>The time-out interval elapsed, and the object's state is nonsignaled.</summary>
        WAIT_TIMEOUT = &H102UI
        ''' <summary>The function has failed. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</summary>
        WAIT_FAILED = &HFFFFFFFFUI
    End Enum
    Private Const WFSO_INFINITE As UInteger = &HFFFFFFFFUI
#End Region
End Class
