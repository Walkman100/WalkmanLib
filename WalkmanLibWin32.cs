using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

public enum MouseButton { // used for MouseClick
    /// <summary>Performs a LeftClick by running LeftDown and LeftUp.</summary>
    LeftClick = LeftDown | LeftUp,
    /// <summary>Holds the left mouse button.</summary>
    LeftDown = 0x2,
    /// <summary>Releases the left mouse button.</summary>
    LeftUp = 0x4,
    /// <summary>Performs a RightClick by running RightDown and RightUp.</summary>
    RightClick = RightDown | RightUp,
    /// <summary>Holds the right mouse button.</summary>
    RightDown = 0x8,
    /// <summary>Releases the right mouse button.</summary>
    RightUp = 0x10,
    /// <summary>Performs a MiddleClick by running MiddleDown and MiddleUp.</summary>
    MiddleClick = MiddleDown | MiddleUp,
    /// <summary>Holds the right mouse button.</summary>
    MiddleDown = 0x20,
    /// <summary>Releases the right mouse button.</summary>
    MiddleUp = 0x40,
    /// <summary>Performs a XClick by running XDown and XUp.</summary>
    XClick = XDown | XUp,
    /// <summary>Holds the X mouse button. (?)</summary>
    XDown = 0x80,
    /// <summary>Releases the X mouse button. (?)</summary>
    XUp = 0x100
}

public partial class WalkmanLib {
    /// <summary>Maximum LongFileName length</summary>
    public const int MAX_FILE_PATH = 32767;

    #region CreateHardLink
    // Link: http://pinvoke.net/default.aspx/kernel32.CreateHardLink
    // Link (native error codes): https://docs.microsoft.com/en-us/windows/win32/debug/system-error-codes#system-error-codes
    /// <summary>Creates a hardlink to an existing file.</summary>
    /// <param name="symlinkPath">Path to the hardlink file to create.</param>
    /// <param name="targetPath">Absolute or relative path to the existing file to link to. If relative, target is relative to current directory.</param>
    public static void CreateHardLink(string hardlinkPath, string existingFilePath) {
        if (CreateHardLink(hardlinkPath, existingFilePath, IntPtr.Zero) == false) {

            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode == 2) {
                // ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                if (!File.Exists(existingFilePath))
                    throw new FileNotFoundException("The hardlink target does not exist", existingFilePath, errorException);
            } else if (errorException.NativeErrorCode == 3) {
                // ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                if (!Directory.Exists(new FileInfo(hardlinkPath).DirectoryName))  // "New FileInfo(hardlinkPath)" throws an exception on invalid characters in path - perfect!
                    throw new DirectoryNotFoundException("The path to the new hardlink does not exist", errorException);
            } else if (errorException.NativeErrorCode == 183) {
                // ERROR_ALREADY_EXISTS: Cannot create a file when that file already exists
                if (File.Exists(hardlinkPath) | Directory.Exists(hardlinkPath))
                    throw new IOException("The hardlink path already exists", errorException);
            } else if (errorException.NativeErrorCode == 5) {
                // ERROR_ACCESS_DENIED: Access is denied
                throw new UnauthorizedAccessException("Access to the new hardlink path is denied", errorException);
            }
            throw errorException;
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createhardlinkw
    // https://www.pinvoke.net/default.aspx/kernel32/CreateHardLink.html
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);
    #endregion

    #region CreateSymLink
    // Link: https://stackoverflow.com/a/11156870/2999220
    /// <summary>Creates a file or directory symbolic link.</summary>
    /// <param name="symlinkPath">Path to the symbolic link file to create.</param>
    /// <param name="targetPath">Absolute or relative path to the target of the shortcut. If relative, target is relative to the symbolic link file.</param>
    /// <param name="targetIsDirectory">Type of the target. If incorrect target type is supplied, the system will act as if the target doesn't exist.</param>
    public static void CreateSymLink(string symlinkPath, string targetPath, bool targetIsDirectory = false) {
        // https://blogs.windows.com/windowsdeveloper/2016/12/02/symlinks-windows-10/
        SymbolicLinkFlags flags = SymbolicLinkFlags.AllowUnprivilegedCreate | (targetIsDirectory ? SymbolicLinkFlags.Directory : SymbolicLinkFlags.File);

        if (CreateSymbolicLink(symlinkPath, targetPath, flags) == false) {
            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode == 0x03) {
                // 0x03: ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                if (!Directory.Exists(new FileInfo(symlinkPath).DirectoryName))  // "New FileInfo(symlinkPath)" throws an exception on invalid characters in path - perfect!
                    throw new DirectoryNotFoundException("The path to the symbolic link does not exist", errorException);
            } else if (errorException.NativeErrorCode == 0xB7) {
                // 0xB7: ERROR_ALREADY_EXISTS: Cannot create a file when that file already exists
                if (File.Exists(symlinkPath) | Directory.Exists(symlinkPath))
                    throw new IOException("The symbolic link path already exists", errorException);
            } else if (errorException.NativeErrorCode == 0x05) {
                // 0x05: ERROR_ACCESS_DENIED: Access is denied
                throw new UnauthorizedAccessException("Access to the symbolic link path is denied", errorException);
            } else if (errorException.NativeErrorCode == 0x522) {
                // 0x522: ERROR_PRIVILEGE_NOT_HELD: A required privilege is not held by the client.
                //    ^ this occurs when Developer Mode is not enabled, or on below Windows 10
                throw new UnauthorizedAccessException("Symbolic link creation requires Admin privileges, or enabling developer mode", errorException);
            }
            throw errorException;
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createsymboliclinkw
    // https://www.pinvoke.net/default.aspx/kernel32/CreateSymbolicLink.html
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLinkFlags dwFlags);

    private enum SymbolicLinkFlags { // used for CreateSymLink
        /// <summary>The link target is a file.</summary>
        File = 0x00,
        /// <summary>The link target is a directory.</summary>
        Directory = 0x01,
        /// <summary>
        /// Specify this flag to allow creation of symbolic links when the process is not elevated.
        /// <br />Developer Mode must first be enabled on the machine before this option will function.
        /// </summary>
        AllowUnprivilegedCreate = 0x02
    }
    #endregion

    #region Win32CreateFile
    public static SafeFileHandle Win32CreateFile(string fileName, Win32FileAccess fileAccess, FileShare shareMode,
                                                 FileMode fileMode, Win32FileAttribute flagsAndAttributes) {
        SafeFileHandle handle = CreateFile(fileName, fileAccess, shareMode, IntPtr.Zero, fileMode, flagsAndAttributes, IntPtr.Zero);

        if (handle.IsInvalid) {
            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode == 2) {
                // ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                throw new FileNotFoundException(errorException.Message, fileName, errorException);
            } else if (errorException.NativeErrorCode == 3) {
                // ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                throw new DirectoryNotFoundException("The path to the file does not exist", errorException);
            } else if (errorException.NativeErrorCode == 5) {
                // ERROR_ACCESS_DENIED: Access is denied
                throw new UnauthorizedAccessException("Access to the file path is denied", errorException);
            } else if (errorException.NativeErrorCode == 32) {
                // ERROR_SHARING_VIOLATION: The process cannot access the file because it is being used by another process
                throw new IOException(errorException.Message, errorException);
            } else if (errorException.NativeErrorCode == 183) {
                // ERROR_ALREADY_EXISTS: Cannot create a file when that file already exists
                throw new IOException("The target path already exists", errorException);
            }
            throw errorException;
        } else {
            return handle;
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew
    // https://www.pinvoke.net/default.aspx/kernel32/CreateFile.html
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern SafeFileHandle CreateFile(string lpFileName, Win32FileAccess dwDesiredAccess,
                                                    FileShare dwShareMode, IntPtr lpSecurityAttributes,
                                                    FileMode dwCreationDisposition, Win32FileAttribute dwFlagsAndAttributes,
                                                    IntPtr hTemplateFile);

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-createfilew#FILE_ATTRIBUTE_ARCHIVE
    [Flags]
    public enum Win32FileAttribute : uint {
        None =                          0,
        AttributeReadOnly =             0x00000001,
        AttributeHidden =               0x00000002,
        AttributeSystem =               0x00000004,
        AttributeDirectory =            0x00000010,
        AttributeArchive =              0x00000020,
        AttributeDevice =               0x00000040,
        AttributeNormal =               0x00000080,
        AttributeTemporary =            0x00000100,
        AttributeSparseFile =           0x00000200,
        AttributeReparsePoint =         0x00000400,
        AttributeCompressed =           0x00000800,
        AttributeOffline =              0x00001000,
        AttributeNotContentIndexed =    0x00002000,
        AttributeEncrypted =            0x00004000,
        AttributeIntegrityStream =      0x00008000,
        AttributeVirtual =              0x00010000,
        AttributeNoScrubData =          0x00020000,
        AttributeEA =                   0x00040000,
        AttributeRecallOnOpen =         0x00040000,
        AttributePinned =               0x00080000,
        AttributeUnpinned =             0x00100000,
        AttributeRecallOnDataAccess =   0x00400000,
        /// <summary>The file data is requested, but it should continue to be located in remote storage. It should not be transported back to
        /// local storage. This flag is for use by remote storage systems.</summary>
        FlagOpenNoRecall =              0x00100000,
        /// <summary>
        /// Normal reparse point processing will not occur; CreateFile will attempt to open the reparse point. When a file is opened, a file handle is returned,
        /// whether or not the filter that controls the reparse point is operational.
        /// <br />This flag cannot be used with the <see cref="FileMode.Create"/> flag.
        /// <br />If the file is not a reparse point, then this flag is ignored.
        /// </summary>
        FlagOpenReparsePoint =          0x00200000,
        /// <summary>
        /// The file or device is being opened with session awareness. If this flag is not specified, then per-session devices (such as a device using RemoteFX
        /// USB Redirection) cannot be opened by processes running in session 0. This flag has no effect for callers not in session 0.
        /// This flag Is supported only on server editions of Windows.
        /// <br />Windows Server 2008 R2 And Windows Server 2008: This flag Is Not supported before Windows Server 2012.
        /// </summary>
        FlagSessionAware =              0x00800000,
        /// <summary>
        /// Access will occur according to POSIX rules. This includes allowing multiple files with names, differing only in case, for file systems that support that naming.
        /// Use care when using this option, because files created with this flag may not be accessible by applications that are written for MS-DOS or 16-bit Windows.
        /// </summary>
        FlagPosixSemantics =            0x01000000,
        /// <summary>
        /// The file is being opened or created for a backup or restore operation. The system ensures that the calling process overrides file security checks when the
        /// process has SE_BACKUP_NAME And SE_RESTORE_NAME privileges.
        /// <br />You must set this flag to obtain a handle to a directory. A directory handle can be passed to some functions instead of a file handle.
        /// </summary>
        FlagBackupSemantics =           0x02000000,
        /// <summary>
        /// The file is to be deleted immediately after all of its handles are closed, which includes the specified handle and any other open or duplicated handles.
        /// If there are existing open Handles to a file, the call fails unless they were all opened with the <see cref="FileShare.Delete"/> share mode.
        /// <br />Subsequent open requests for the file fail, unless the <see cref="FileShare.Delete"/> share mode is specified.
        /// </summary>
        FlagDeleteOnClose =             0x04000000,
        /// <summary>
        /// Access is intended to be sequential from beginning to end. The system can use this as a hint to optimize file caching.
        /// <br />This flag should not be used if read-behind (that is, reverse scans) will be used.
        /// <br />This flag has no effect if the file system does not support cached I/O And <see cref="FlagNoBuffering"/>.
        /// </summary>
        FlagSequentialScan =            0x08000000,
        /// <summary>
        /// Access is intended to be random. The system can use this as a hint to optimize file caching.
        /// <br />This flag has no effect if the file system does not support cached I/O And <see cref="FlagNoBuffering"/>.
        /// </summary>
        FlagRandomAccess =              0x10000000,
        /// <summary>
        /// The file or device is being opened with no system caching for data reads and writes. This flag does not affect hard disk caching or memory mapped files.
        /// <br />There are strict requirements for successfully working with files opened with CreateFile Using the <see cref="FlagNoBuffering"/> flag.
        /// </summary>
        FlagNoBuffering =               0x20000000,
        /// <summary>
        /// The file or device is being opened or created for asynchronous I/O.
        /// When subsequent I/O operations are completed on this handle, the event specified in the OVERLAPPED structure will be set to the signaled state.
        /// <br />If this flag is specified, the file can be used for simultaneous read and write operations.
        /// <br />If this flag is not specified, then I/O operations are serialized, even if the calls to the read and write functions specify an OVERLAPPED Structure.
        /// </summary>
        FlagOverlapped =                0x40000000,
        /// <summary>Write operations will not go through any intermediate cache, they will go directly to disk.</summary>
        FlagWriteThrough =              0x80000000
    }

    // https://docs.microsoft.com/en-us/windows/win32/fileio/file-access-rights-constants
    // https://docs.microsoft.com/en-us/windows/win32/fileio/file-security-and-access-rights
    // https://docs.microsoft.com/en-us/windows/win32/secauthz/access-mask#code-try-1
    [Flags]
    public enum Win32FileAccess : uint {
        None =                      0,
        /// <summary>[File &amp; Pipe]</summary>
        FileReadData =              0x00000001,
        /// <summary>[Directory]</summary>
        DirListDirectory =          0x00000001,
        /// <summary>[File &amp; Pipe]</summary>
        FileWriteData =             0x00000002,
        /// <summary>[Directory]</summary>
        DirAddFile =                0x00000002,
        /// <summary>[File] The right to append data to the file. (For local files, write operations will not overwrite
        /// existing data if this flag is specified without <see cref="FileWriteData"/>.)
        /// </summary>
        FileAppendData =            0x00000004,
        /// <summary>[Directory]</summary>
        DirAddSubdirectory =        0x00000004,
        /// <summary>[Pipe]</summary>
        PipeCreateInstance =        0x00000004,
        /// <summary>[File &amp; Directory]</summary>
        ReadEA =                    0x00000008,
        /// <summary>[File &amp; Directory]</summary>
        WriteEA =                   0x00000010,
        /// <summary>[File]</summary>
        FileExecute =               0x00000020,
        /// <summary>[Directory] The right to traverse the directory. By default, users are assigned the BYPASS_TRAVERSE_CHECKING privilege, which ignores the <see cref="Traverse"/> access right.</summary>
        DirTraverse =               0x00000020,
        /// <summary>[Directory] The right to delete a directory and all the files it contains, including read-only files.</summary>
        DirDeleteChild =            0x00000040,
        /// <summary>[All]</summary>
        ReadAttributes =            0x00000080,
        /// <summary>[All]</summary>
        WriteAttributes =           0x00000100,
        /// <summary>[All]</summary>
        Delete =                    0x00010000,
        /// <summary>Read access to the owner, group, and discretionary access control list (DACL) of the security descriptor.</summary>
        ReadControl =               0x00020000,
        /// <summary>Write access to the discretionary access control list (DACL).</summary>
        WriteDac =                  0x00040000,
        /// <summary>Write access to owner. Required to change the owner in the security descriptor for the object.</summary>
        WriteOwner =                0x00080000,
        /// <summary>Synchronize access. This enables a thread to wait until the object is in the signaled state.</summary>
        Synchronize =               0x00100000,
        StandardRightsRequired =    0x000F0000,
        StandardRightsRead =        ReadControl,
        StandardRightsWrite =       ReadControl,
        StandardRightsExecute =     ReadControl,
        StandardRightsAll =         0x001F0000,
        SpecificRightsAll =         0x0000FFFF,
        /// <summary>
        /// Used to indicate access to a system access control list (SACL). This type of access requires the calling process to
        /// have the SE_SECURITY_NAME (Manage auditing and security log) privilege. If this flag is set in the access mask of
        /// an audit access ACE (successful or unsuccessful access), the SACL access will be audited.
        /// </summary>
        AccessSystemSecurity =      0x01000000,
        MaximumAllowed =            0x02000000,
        GenericAll =                0x10000000,
        GenericExecute =            0x20000000,
        GenericWrite =              0x40000000,
        GenericRead =               0x80000000,
        FileGenericRead =           StandardRightsRead | FileReadData | ReadAttributes | ReadEA | Synchronize,
        FileGenericWrite =          StandardRightsWrite | FileWriteData | WriteAttributes | WriteEA | FileAppendData | Synchronize,
        FileGenericExecute =        StandardRightsExecute | ReadAttributes | FileExecute | Synchronize,
        FileAllAccess =             StandardRightsRequired | Synchronize | 0x1FF // 1FF is everything before Delete (not including)
    }
    #endregion

    #region CreateJunction
    // Link: https://www.codeproject.com/Articles/15633/Manipulating-NTFS-Junction-Points-in-NET
    public static void CreateJunction(string junctionPath, string targetPath, bool replace = false) {
        // https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-fscc/c8e77b37-3909-4fe6-a4ea-2b9d423b1ee4
        const uint IO_REPARSE_TAG_MOUNT_POINT = 0xA0000003;
        // https://docs.microsoft.com/en-us/windows/win32/api/winioctl/ni-winioctl-fsctl_set_reparse_point
        const uint FSCTL_SET_REPARSE_POINT = 0x900A4;
        // This prefix indicates to NTFS that the path is to be treated as a non-interpreted path in the virtual file system.
        const string NonInterpretedPathPrefix = @"\??\";

        if (Directory.Exists(junctionPath)) {
            if (!replace)
                throw new IOException("Directory already exists and overwrite parameter is false.");
        } else {
            Directory.CreateDirectory(junctionPath);
        }
        targetPath = NonInterpretedPathPrefix + Path.GetFullPath(targetPath);

        using (SafeFileHandle reparsePointHandle = Win32CreateFile(junctionPath, Win32FileAccess.GenericWrite, FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open, Win32FileAttribute.FlagBackupSemantics | Win32FileAttribute.FlagOpenReparsePoint)) {
            if (Marshal.GetLastWin32Error() != 0)
                throw new IOException("Unable to open reparse point.", new Win32Exception());

            // unicode string is 2 bytes per character, so *2 to get byte length
            ushort byteLength = (ushort)(targetPath.Length * 2);
            var reparseDataBuffer = new ReparseDataBuffer() {
                ReparseTag = IO_REPARSE_TAG_MOUNT_POINT,
                ReparseDataLength = (ushort)(byteLength + 12),
                SubstituteNameOffset = 0,
                SubstituteNameLength = byteLength,
                PrintNameOffset = (ushort)(byteLength + 2),
                PrintNameLength = 0,
                PathBuffer = targetPath
            };

            bool result = DeviceIoControl(reparsePointHandle, FSCTL_SET_REPARSE_POINT, ref reparseDataBuffer, (uint)(byteLength + 20), IntPtr.Zero, 0, out _, IntPtr.Zero);
            if (!result)
                throw new IOException("Unable to create junction point.", new Win32Exception());
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/ioapiset/nf-ioapiset-deviceiocontrol
    // https://www.pinvoke.net/default.aspx/kernel32/DeviceIoControl.html
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool DeviceIoControl(SafeFileHandle hDevice, uint dwIoControlCode,
                                            [In] ref ReparseDataBuffer lpInBuffer, uint nInBufferSize,
                                            IntPtr lpOutBuffer, uint nOutBufferSize,
                                            out uint lpBytesReturned, IntPtr lpOverlapped);

    // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/ntifs/ns-ntifs-_reparse_data_buffer
    //  Because the tag we're using is IO_REPARSE_TAG_MOUNT_POINT, we use the MountPointReparseBuffer struct in the DUMMYUNIONNAME union.
    // https://www.pinvoke.net/default.aspx/Structures/REPARSE_DATA_BUFFER.html
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct ReparseDataBuffer {
        /// <summary>Reparse point tag. Must be a Microsoft reparse point tag.</summary>
        public uint ReparseTag;
        /// <summary>Size, in bytes, of the reparse data in the buffer that <see cref="PathBuffer"/> points to.</summary>
        public ushort ReparseDataLength;
        /// <summary>Reserved; do not use.</summary>
        private ushort Reserved;
        /// <summary>Offset, in bytes, of the substitute name string in the <see cref="PathBuffer"/> array.</summary>
        public ushort SubstituteNameOffset;
        /// <summary>Length, in bytes, of the substitute name string. If this string is null-terminated, <see cref="SubstituteNameLength"/> does not include space for the null character.</summary>
        public ushort SubstituteNameLength;
        /// <summary>Offset, in bytes, of the print name string in the <see cref="PathBuffer"/> array.</summary>
        public ushort PrintNameOffset;
        /// <summary>Length, in bytes, of the print name string. If this string is null-terminated, <see cref="PrintNameLength"/> does not include space for the null character.</summary>
        public ushort PrintNameLength;
        /// <summary>
        /// A buffer containing the unicode-encoded path string. The path string contains the substitute name
        /// string and print name string. The substitute name and print name strings can appear in any order.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8184)]
        internal string PathBuffer;
        // with [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16368)] public byte[] PathBuffer;
        // 16368 is the amount of bytes. since a unicode string uses 2 bytes per character, constrain to 16368/2 = 8184 characters.
    }
    #endregion

    #region GetHardlinkCount
    // Link: https://serverfault.com/questions/758496/get-hardlink-count-for-a-file-on-windows-without-fsutil-which-requires-admin
    // Link: https://devblogs.microsoft.com/vbteam/to-compare-two-filenames-lucian-wischik/
    /// <summary>Gets the count of hardlinks of a file.</summary>
    /// <param name="path">Path to the file to get the hardlink count of.</param>
    /// <returns>Count of links to a file. The count includes the current link (0 would be a deleted file)</returns>
    public static uint GetHardlinkCount(string path) {
        // don't need to use Win32CreateFile as that's needed for directories
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
            var fileInfo = default(ByHandleFileInformation);
            if (GetFileInformationByHandle(fs.SafeFileHandle, ref fileInfo) == false)
                throw new Win32Exception();

            return fileInfo.nNumberOfLinks;
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfileinformationbyhandle
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool GetFileInformationByHandle(SafeFileHandle hFile, ref ByHandleFileInformation lpFileInformation);

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/ns-fileapi-by_handle_file_information
    [StructLayout(LayoutKind.Sequential)]
    private struct ByHandleFileInformation {
        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint dwVolumeSerialNumber;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint nNumberOfLinks;
        public uint nFileIndexHigh;
        public uint nFileIndexLow;
    }
    #endregion

    #region GetHardlinkLinks
    // Link: https://stackoverflow.com/a/4511075/2999220
    // Link: https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findfirstfilenamew
    /// <summary>Gets all the links to a specified file.
    /// Warning: A handle is used to get the links, and is only closed when all the links have been retrieved.
    /// To ensure this handle is closed, ensure all the links are retrieved.</summary>
    /// <param name="path">Path to the file to get links for. This path does not include the drive identifier.</param>
    /// <returns>All a file's links.</returns>
    public static IEnumerable<string> GetHardlinkLinks(string path) {
        var INVALID_HANDLE_VALUE = new IntPtr(unchecked((int)0xFFFFFFFF));
        var stringBuilderTarget = new StringBuilder(MAX_FILE_PATH);
        uint lpdwStringLength = MAX_FILE_PATH;
        IntPtr hFind = FindFirstFileName(path, 0, ref lpdwStringLength, stringBuilderTarget);

        if (hFind == INVALID_HANDLE_VALUE) {
            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode == 2) {
                // ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                if (!File.Exists(path))
                    throw new FileNotFoundException(errorException.Message, path, errorException);
            } else if (errorException.NativeErrorCode == 3) {
                // ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                if (!Directory.Exists(new FileInfo(path).DirectoryName))
                    throw new DirectoryNotFoundException(errorException.Message, errorException);
            } else if (errorException.NativeErrorCode == 5) {
                // ERROR_ACCESS_DENIED: Access is denied
                throw new UnauthorizedAccessException("Access to the file path is denied", errorException);
            } else if (errorException.NativeErrorCode == 32) {
                // ERROR_SHARING_VIOLATION: The process cannot access the file because it is being used by another process
                throw new IOException(errorException.Message, errorException);
            }
            throw errorException;
        }

        try {
            // link path doesn't include drive letter by default
            string pathRoot = Path.GetPathRoot(path);
            string getFullPath(string path) {
                if (path[0] == Path.DirectorySeparatorChar)
                    return Path.Combine(pathRoot, path.Substring(1));
                else
                    return path;
            };

            yield return getFullPath(stringBuilderTarget.ToString());

            lpdwStringLength = MAX_FILE_PATH;
            while (FindNextFileName(hFind, ref lpdwStringLength, stringBuilderTarget))
                yield return getFullPath(stringBuilderTarget.ToString());

            var errorException = new Win32Exception();
            //                                    ERROR_HANDLE_EOF: Reached the end of the file.
            if (errorException.NativeErrorCode != 0x26)
                throw errorException;
        } finally {
            FindClose(hFind);
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findfirstfilenamew
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr FindFirstFileName(string lpFileName, uint dwFlags, [In, Out] ref uint lpdwStringLength, StringBuilder pLinkName);

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findnextfilenamew
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool FindNextFileName(IntPtr hFindStream, ref uint lpdwStringLength, StringBuilder pLinkName);

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-findclose
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool FindClose(IntPtr hFindFile);
    #endregion

    #region GetSymlinkTarget
    // Link: https://stackoverflow.com/a/33487494/2999220
    /// <summary>Gets the target of a symbolic link, directory junction or volume mountpoint. Throws ComponentModel.Win32Exception on error.</summary>
    /// <param name="path">Path to the symlink to get the target of.</param>
    /// <returns>The fully qualified path to the target.</returns>
    public static string GetSymlinkTarget(string path) {
        string returnString = "";

        using (SafeFileHandle hFile = Win32CreateFile(path, Win32FileAccess.ReadEA,
                                                      FileShare.Read | FileShare.Write | FileShare.Delete, FileMode.Open,
                                                      Win32FileAttribute.FlagBackupSemantics)) {
            var stringBuilderTarget = new StringBuilder(MAX_FILE_PATH);
            uint result = GetFinalPathNameByHandle(hFile, stringBuilderTarget, MAX_FILE_PATH, 0);

            if (result == 0)
                throw new Win32Exception();
            returnString = stringBuilderTarget.ToString();
        }

        returnString = returnString.Substring(4); // remove "\\?\" at the beginning
        if (returnString.StartsWith(@"UNC\")) // change "UNC\[IP]\" to proper "\\[IP]\"
            returnString = @"\" + returnString.Substring(3);

        return returnString;
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getfinalpathnamebyhandlew
    // https://www.pinvoke.net/default.aspx/shell32/GetFinalPathNameByHandle.html
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern uint GetFinalPathNameByHandle(SafeFileHandle hFile, StringBuilder lpszFilePath,
                                                        uint cchFilePath, uint dwFlags);
    #endregion

    #region Shortcut Management
    // Link: https://stackoverflow.com/a/14141782/2999220
    // Link: https://www.tek-tips.com/viewthread.cfm?qid=850335
    /// <summary>Gets a shortcut property object to retrieve info about a shortcut.</summary>
    /// <param name="shortcutPath">Path to the shortcut file.</param>
    /// <returns>Shortcut object of type IWshShortcut - either use WalkmanLib.IWshShortcut or ComImport your own interface.</returns>
    public static IWshShortcut GetShortcutInfo(string shortcutPath) {
        if (!shortcutPath.EndsWith(".lnk", true, System.Globalization.CultureInfo.InvariantCulture))
            shortcutPath += ".lnk";

        dynamic WScript_Shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
        return WScript_Shell.CreateShortcut(shortcutPath);
    }

    // Link: https://ss64.com/vb/shortcut.html
    // HotKey: https://docs.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/windows-scripting/3zb1shc6(v=vs.84)#arguments
    /// <summary>Creates or modifies an existing shortcut. When modifying, set a parameter to "" to clear it. Defaults are 'Nothing'.</summary>
    /// <param name="shortcutPath">Path to the shortcut file.</param>
    /// <param name="targetPath">Full path to the target of the shortcut.</param>
    /// <param name="arguments">Arguments to the target.</param>
    /// <param name="workingDirectory">Directory to start the target in.</param>
    /// <param name="iconPath">Path to the shortcut icon. Append ", iconIndex" to specify an index.</param>
    /// <param name="comment">Shortcut comment. Shown in the Shortcut's tooltip.</param>
    /// <param name="shortcutKey">Hotkey used to launch the shortcut - see the end of https://ss64.com/vb/shortcut.html.</param>
    /// <param name="windowStyle">System.Windows.Forms.FormWindowState to show the launched program in.</param>
    /// <returns>Full path to the created shortcut.</returns>
    public static string CreateShortcut(string shortcutPath, string targetPath = null, string arguments = null, string workingDirectory = null,
            string iconPath = null, string comment = null, string shortcutKey = null, System.Windows.Forms.FormWindowState windowStyle = System.Windows.Forms.FormWindowState.Normal) {
        IWshShortcut shortcutObject = GetShortcutInfo(shortcutPath);

        if (!string.IsNullOrEmpty(targetPath))
            shortcutObject.TargetPath = targetPath;
        if (!string.IsNullOrEmpty(arguments))
            shortcutObject.Arguments = arguments;
        if (!string.IsNullOrEmpty(workingDirectory))
            shortcutObject.WorkingDirectory = workingDirectory;
        if (!string.IsNullOrEmpty(iconPath))
            shortcutObject.IconLocation = iconPath;
        if (!string.IsNullOrEmpty(comment))
            shortcutObject.Description = comment;
        if (!string.IsNullOrEmpty(shortcutKey))
            shortcutObject.Hotkey = shortcutKey;

        if (windowStyle == System.Windows.Forms.FormWindowState.Normal)
            shortcutObject.WindowStyle = 1;
        else if (windowStyle == System.Windows.Forms.FormWindowState.Minimized)
            shortcutObject.WindowStyle = 7;
        else if (windowStyle == System.Windows.Forms.FormWindowState.Maximized)
            shortcutObject.WindowStyle = 3;

        shortcutObject.Save();

        return shortcutObject.FullName;
    }

    /// <summary>Interface for handling WScript.Shell shortcut objects. Use with IWshShortcut <see cref="GetShortcutInfo(string)"/></summary>
    [ComImport, TypeLibType(0x1040), Guid("F935DC23-1CF0-11D0-ADB9-00C04FD58A0B")]
    public interface IWshShortcut {
        [DispId(0)]
        string FullName { get; }
        [DispId(0x3E8)]
        string Arguments { get; set; }
        /// <summary>Shortcut Comment.</summary>
        [DispId(0x3E9)]
        string Description { get; set; }
        [DispId(0x3EA)]
        string Hotkey { get; set; }
        [DispId(0x3EB)]
        string IconLocation { get; set; }
        [DispId(0x3EC)]
        string RelativePath { set; }
        [DispId(0x3ED)]
        string TargetPath { get; set; }
        /// <summary>Shortcut "Run" combobox. 1=Normal, 3=Maximized, 7=Minimized.</summary>
        [DispId(0x3EE)]
        int WindowStyle { get; set; }
        [DispId(0x3EF)]
        string WorkingDirectory { get; set; }
        [TypeLibFunc(0x40), DispId(0x7D0)]
        void Load([In, MarshalAs(UnmanagedType.BStr)] string PathLink);
        [DispId(0x7D1)]
        void Save();
    }
    #endregion

    #region GetFileIcon
    // Link: https://stackoverflow.com/a/24146599/2999220
    /// <summary>Retrieves the system icon associated with a file or filetype.</summary>
    /// <param name="filePath">Path to the item to get icon for. Set <paramref name="checkFile"/> to <see langword="False"/> to get the filetype icon.</param>
    /// <param name="checkFile">Specifies whether to get the icon associated with a specific file, or just for the filetype.</param>
    /// <param name="smallIcon"><see langword="false"/> to get a 32x32 icon, <see langword="true"/> to get a 16x16 icon</param>
    /// <param name="linkOverlay">Add the system "link" overlay to the icon</param>
    public static System.Drawing.Icon GetFileIcon(string filePath, bool checkFile = true, bool smallIcon = true, bool linkOverlay = false) {
        SHGetFileInfoFlags flags = SHGetFileInfoFlags.Icon;
        if (!checkFile)
            flags |= SHGetFileInfoFlags.UseFileAttributes;
        flags |= (smallIcon ? SHGetFileInfoFlags.SmallIcon : SHGetFileInfoFlags.LargeIcon);
        if (linkOverlay)
            flags |= SHGetFileInfoFlags.LinkOverlay;

        var shInfo = new SHFILEINFO();
        if (SHGetFileInfo(filePath, 0, ref shInfo, (uint)Marshal.SizeOf(shInfo), flags) == 0 || shInfo.hIcon == IntPtr.Zero) {

            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode == 2) {
                // ERROR_FILE_NOT_FOUND: The system cannot find the file specified
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("The file does not exist", filePath, errorException);
            } else if (errorException.NativeErrorCode == 3) {
                // ERROR_PATH_NOT_FOUND: The system cannot find the path specified
                if (!Directory.Exists(new FileInfo(filePath).DirectoryName))
                    throw new DirectoryNotFoundException("The path to the file does not exist", errorException);
            } else if (errorException.NativeErrorCode == 5) {
                // ERROR_ACCESS_DENIED: Access is denied
                throw new UnauthorizedAccessException("Access to the file is denied", errorException);
            }
            throw errorException;
        }

        return System.Drawing.Icon.FromHandle(shInfo.hIcon);
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetfileinfow
    // https://www.pinvoke.net/default.aspx/shell32/SHGetFileInfo.html
    [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern uint SHGetFileInfo(string pszPath, FileAttributes dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, SHGetFileInfoFlags uFlags);

    // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shfileinfow
    // https://www.pinvoke.net/default.aspx/Structures/SHFILEINFO.html
    private struct SHFILEINFO {
        public IntPtr hIcon;
        public int iIcon;
        /// <summary>See <see cref="ContextMenu.SFGAO"/> enum for all values</summary>
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shgetfileinfow#shgfi_addoverlays-0x000000020
    private enum SHGetFileInfoFlags : uint {
        /// <summary>Modify <see cref="Icon"/>, causing the function to retrieve the file's large icon. The <see cref="Icon"/> flag must also be set.</summary>
        LargeIcon = 0x0,
        /// <summary>
        /// Modify <see cref="Icon"/>, causing the function to retrieve the file's small icon.
        /// <br/>Also used to modify <see cref="SysIconIndex"/>, causing the function to return the handle to the system image list that contains small icon images.
        /// <br/>The <see cref="Icon"/> and/or <see cref="SysIconIndex"/> flag must also be set.
        /// </summary>
        SmallIcon = 0x1,
        /// <summary>
        /// Modify <see cref="Icon"/>, causing the function to retrieve the file's open icon.
        /// <br/>Also used to modify <see cref="SysIconIndex"/>, causing the function to return the handle to the system image list that contains the file's small open icon.
        /// A container object displays an open icon to indicate that the container is open.
        /// <br/>The <see cref="Icon"/> and/or <see cref="SysIconIndex"/> flag must also be set.
        /// </summary>
        OpenIcon = 0x2,
        /// <summary>
        /// Modify <see cref="Icon"/>, causing the function to retrieve a Shell-sized icon. If this flag is not specified the function
        /// sizes the icon according to the system metric values. The <see cref="Icon"/> flag must also be set.
        /// </summary>
        ShellIconSize = 0x4,
        /// <summary>Indicate that <paramref name="SHGetFileInfo.pszPath"/> is the address of an ITEMIDLIST structure rather than a path name.</summary>
        PIDL = 0x8,
        /// <summary>
        /// Indicates that the function should not attempt to access the file specified by <paramref name="SHGetFileInfo.pszPath"/>. Rather, it should act as if the
        /// file specified by <paramref name="SHGetFileInfo.pszPath"/> exists with the file attributes passed in <paramref name="SHGetFileInfo.dwFileAttributes"/>.
        /// This flag cannot be combined with the <see cref="Attributes"/>, <see cref="ExeType"/>, or <see cref="PIDL"/> flags.
        /// </summary>
        UseFileAttributes = 0x10,
        /// <summary>Apply the appropriate overlays to the file's icon. The <see cref="Icon"/> flag must also be set.</summary>
        AddOverlays = 0x20,
        /// <summary>
        /// Return the index of the overlay icon. The value of the overlay index is returned in the upper eight bits of <see cref="SHFILEINFO.iIcon"/>.
        /// This flag requires that the <see cref="Icon"/> be set as well.
        /// </summary>
        OverlayIndex = 0x40,
        /// <summary>
        /// Retrieve the handle to the icon that represents the file and the index of the icon within the system image list.
        /// The handle is copied to <see cref="SHFILEINFO.hIcon"/>, and the index is copied to <see cref="SHFILEINFO.iIcon"/>.
        /// </summary>
        Icon = 0x100,
        /// <summary>
        /// Retrieve the display name for the file, which is the name as it appears in Windows Explorer. The name is copied to <see cref="SHFILEINFO.szDisplayName"/>.
        /// The returned display name uses the long file name, if there is one, rather than the 8.3 form of the file name.
        /// <br/> Note that the display name can be affected by settings such as whether extensions are shown.
        /// </summary>
        DisplayName = 0x200,
        /// <summary>Retrieve the string that describes the file's type. The string is copied to <see cref="SHFILEINFO.szTypeName"/>.</summary>
        TypeName = 0x400,
        /// <summary>
        /// Retrieve the item attributes. The attributes are copied to <see cref="SHFILEINFO.dwAttributes"/>.
        /// These are the same attributes that are obtained from IShellFolder::GetAttributesOf.
        /// </summary>
        Attributes = 0x800,
        /// <summary>
        /// Retrieve the name of the file that contains the icon representing the file specified by <paramref name="SHGetFileInfo.pszPath"/>, as returned by the
        /// IExtractIcon::GetIconLocation method of the file's icon handler. Also retrieve the icon index within that file.
        /// <br/>The name of the file containing the icon is copied to <see cref="SHFILEINFO.szDisplayName"/>. The icon's index is copied to <see cref="SHFILEINFO.iIcon"/>.
        /// </summary>
        IconLocation = 0x1000,
        /// <summary>
        /// Retrieve the type of the executable file if <paramref name="SHGetFileInfo.pszPath"/> identifies an executable file.
        /// The information is packed into the return value. This flag cannot be specified with any other flags.
        /// </summary>
        ExeType = 0x2000,
        /// <summary>
        /// Retrieve the index of a system image list icon. If successful, the index is copied to <see cref="SHFILEINFO.iIcon"/>.
        /// The return value is a handle to the system image list. Only those images whose indices are successfully copied to <see cref="SHFILEINFO.iIcon"/> are valid.
        /// Attempting to access other images in the system image list will result in undefined behavior.
        /// </summary>
        SysIconIndex = 0x4000,
        /// <summary>Modify <see cref="Icon"/>, causing the function to add the link overlay to the file's icon. The <see cref="Icon"/> flag must also be set.</summary>
        LinkOverlay = 0x8000,
        /// <summary>Modify <see cref="Icon"/>, causing the function to blend the file's icon with the system highlight color. The <see cref="Icon"/> flag must also be set.</summary>
        Selected = 0x10000,
        /// <summary>
        /// Modify <see cref="Attributes"/> to indicate that <see cref="SHFILEINFO.dwAttributes"/> contains the specific attributes that are desired.
        /// These attributes are passed to IShellFolder::GetAttributesOf. If this flag is not specified, 0xFFFFFFFF is passed to IShellFolder::GetAttributesOf,
        /// requesting all attributes. This flag cannot be specified with the <see cref="Icon"/> flag.
        /// </summary>
        AttrSpecified = 0x20000
    }
    #endregion

    #region PickIconDialogShow
    // Link: https://www.pinvoke.net/default.aspx/shell32.pickicondlg
    // Link: https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-pickicondlg
    /// <summary>Shows a dialog for the user to choose an icon file and index.</summary>
    /// <param name="filePath">Path of the initial file to be loaded. Use the same variable to get the selected file.</param>
    /// <param name="iconIndex">Initial Index to be preselected. Use the same variable to get the selected index.</param>
    /// <param name="OwnerHandle">Use this.Handle to make the PickIconDialog show as a Dialog - i.e. blocking your applications interface until dialog is closed.</param>
    /// <returns>true if accepted, false if cancelled.</returns>
    public static bool PickIconDialogShow(ref string filePath, ref int iconIndex, IntPtr OwnerHandle = default) {
        var stringBuilderTarget = new StringBuilder(filePath, MAX_FILE_PATH);
        int result = PickIconDlg(OwnerHandle, stringBuilderTarget, MAX_FILE_PATH, ref iconIndex);

        filePath = stringBuilderTarget.ToString();
        if (result == 1)
            return true;
        else if (result == 0)
            return false;
        else
            throw new ApplicationException($"Unknown error! PickIconDlg return value: {result}" +
                $"{Environment.NewLine}filePath: {filePath}{Environment.NewLine}iconIndex: {iconIndex}");
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-pickicondlg
    // https://www.pinvoke.net/default.aspx/shell32/PickIconDlg.html
    [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int PickIconDlg(IntPtr hwndOwner, StringBuilder pszIconPath, uint cchIconPath, [In, Out] ref int piIconIndex);
    #endregion

    #region ExtractIconByIndex
    // Link: https://stackoverflow.com/q/37261353/2999220 (last half)
    /// <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
    /// <param name="filePath">The path to the file that contains an image.</param>
    /// <param name="iconIndex">Index to extract the icon from. If this is a positive number, it refers to the zero-based position of the icon in the file. If this is a negative number, it refers to the icon's resource ID.</param>
    /// <param name="iconSize">Size of icon to extract. Size is measured in pixels. Pass 0 to specify default icon size. Default: 0.</param>
    /// <returns>The <see cref="System.Drawing.Icon"/> representation of the image that is contained in the specified file.</returns>
    public static System.Drawing.Icon ExtractIconByIndex(string filePath, int iconIndex, uint iconSize = 0) {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found!", filePath);

        int result = SHDefExtractIcon(filePath, iconIndex, 0, out IntPtr hiconLarge, out _, iconSize);

        if (result == 0) {           // S_OK: Success
            return System.Drawing.Icon.FromHandle(hiconLarge);
        } else if (result == 1) {    // S_FALSE: The requested icon is not present
            throw new ArgumentOutOfRangeException(nameof(iconIndex), "The requested icon index is not present in the specified file.");
        } else { //if (HRESULT = 2) { // E_FAIL: The file cannot be accessed, or is being accessed through a slow link
            throw new Win32Exception();
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/shlobj_core/nf-shlobj_core-shdefextracticonw
    [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern int SHDefExtractIcon(string pszIconFile, int iconIndex, uint flags,
                                               out IntPtr hiconLarge, out IntPtr hiconSmall,
                                               uint iconSize);
    #endregion

    #region File Compression
    /// <summary>Compresses the specified file or directory using NTFS compression.</summary>
    /// <param name="path">Path to the file or directory to compress.</param>
    /// <param name="showWindow">Whether to show the compression status window or not.</param>
    /// <returns>Whether the file or directory was compressed successfully or not.</returns>
    public static bool CompressFile(string path, bool showWindow = true) {
        return SetCompression(path, true, showWindow);
    }

    /// <summary>Decompresses the specified file or directory using NTFS compression.</summary>
    /// <param name="path">Path to the file or directory to decompress.</param>
    /// <param name="showWindow">Whether to show the compression status window or not.</param>
    /// <returns>Whether the file or directory was decompressed successfully or not.</returns>
    public static bool UncompressFile(string path, bool showWindow = true) {
        return SetCompression(path, false, showWindow);
    }

    // Link: http://www.thescarms.com/dotnet/NTFSCompress.aspx
    // Link: https://docs.microsoft.com/en-us/windows/win32/api/winioctl/ni-winioctl-fsctl_set_compression
    /// <summary>Compress or decompress the specified file or directory using NTFS compression.</summary>
    /// <param name="path">Path to the file or directory to (de)compress.</param>
    /// <param name="compress">True to compress, False to decompress.</param>
    /// <param name="showWindow">Whether to show the compression status window or not (TODO).</param>
    /// <returns>Whether the file or directory was (de)compressed successfully or not.</returns>
    public static bool SetCompression(string path, bool compress, bool showWindow = true) {
        // https://docs.microsoft.com/en-us/windows/win32/api/winioctl/ni-winioctl-fsctl_set_compression
        const uint FSCTL_SET_COMPRESSION = 0x9C040;
        ushort lpInBuffer = compress ? (ushort)1 : (ushort)0;

        using (SafeFileHandle hFile = Win32CreateFile(path, Win32FileAccess.FileGenericRead | Win32FileAccess.FileGenericWrite,
                                                      FileShare.ReadWrite | FileShare.Delete,
                                                      FileMode.Open, Win32FileAttribute.FlagBackupSemantics)) {
            return DeviceIoControl(hFile, FSCTL_SET_COMPRESSION, ref lpInBuffer, 2, IntPtr.Zero, 0, out _, IntPtr.Zero);
        }
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/ioapiset/nf-ioapiset-deviceiocontrol
    // https://www.pinvoke.net/default.aspx/kernel32/DeviceIoControl.html
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool DeviceIoControl(SafeFileHandle hDevice, uint dwIoControlCode,
                                               [In] ref ushort lpInBuffer, uint nInBufferSize,
                                               IntPtr lpOutBuffer, uint nOutBufferSize,
                                               out uint lpBytesReturned, IntPtr lpOverlapped);
    #endregion

    #region GetCompressedSize
    // Link: http://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    // Link: https://stackoverflow.com/a/22508299/2999220
    // Link: https://stackoverflow.com/a/1650868/2999220 (Win32Exception handling)
    /// <summary>Gets the compressed size of a specified file. Throws IOException on failure.</summary>
    /// <param name="path">Path to the file to get size for.</param>
    /// <returns>The compressed size of the file or the size of the file if file isn't compressed.</returns>
    public static long GetCompressedSize(string path) {
        long fileLength = Convert.ToInt64(GetCompressedFileSize(path, out uint sizeMultiplier));
        if (fileLength == 0xFFFFFFFFL) {
            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode != 0)
                throw new IOException(errorException.Message, errorException);
        }
        return ((uint.MaxValue + 1L) * sizeMultiplier) + fileLength;
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/fileapi/nf-fileapi-getcompressedfilesizew
    // https://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern uint GetCompressedFileSize(string lpFileName, out uint lpFileSizeHigh);
    #endregion

    #region GetOpenWith
    // Link: http://www.vb-helper.com/howto_get_associated_program.html
    /// <summary>Gets the path to the program specified to open a file.</summary>
    /// <param name="filePath">The file to get the OpenWith program for.</param>
    /// <returns>OpenWith program path, "Filetype not associated!" if none, or "File not found!"</returns>
    public static string GetOpenWith(string filePath) {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found!", filePath);

        var stringBuilderTarget = new StringBuilder(MAX_FILE_PATH);
        FindExecutable(Path.GetFileName(filePath), Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar, stringBuilderTarget);
        return stringBuilderTarget.ToString();
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-findexecutablew
    // https://www.pinvoke.net/default.aspx/shell32/FindExecutable.html
    [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern long FindExecutable(string lpFile, string lpDirectory, StringBuilder lpResult);
    #endregion

    #region MouseClick
    // Link: https://stackoverflow.com/a/2416762/2999220
    // Link: http://pinvoke.net/default.aspx/user32.mouse_event (Additional buttons)
    /// <summary>Performs a mouse click at the current cursor position.</summary>
    /// <param name="button">MouseButton to press.</param>
    public static void MouseClick(MouseButton button) {
        mouse_event(button, 0, 0, 0, 0);

        // const int MOUSEEVENTF_MOVE = 0x1
        // const int MOUSEEVENTF_LEFTDOWN = 0x2
        // const int MOUSEEVENTF_LEFTUP = 0x4
        // const int MOUSEEVENTF_RIGHTDOWN = 0x8
        // const int MOUSEEVENTF_RIGHTUP = 0x10
        // const int MOUSEEVENTF_MIDDLEDOWN = 0x20
        // const int MOUSEEVENTF_MIDDLEUP = 0x40
        // const int MOUSEEVENTF_XDOWN = 0x80
        // const int MOUSEEVENTF_XUP = 0x100
        // const int MOUSEEVENTF_WHEEL = 0x800
        // const int MOUSEEVENTF_HWHEEL = 0x1000
        // const int MOUSEEVENTF_ABSOLUTE = 0x8000
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mouse_event
    // https://www.pinvoke.net/default.aspx/user32/mouse_event.html
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(MouseButton dwFlags, uint dx, uint dy, uint dwData, uint dwExtraInfo);
    #endregion

    #region ShowProperties
    // Link: https://stackoverflow.com/a/1936957/2999220
    /// <summary>Opens the Windows properties window for a path.</summary>
    /// <param name="path">The path to show the window for.</param>
    /// <param name="tab">Optional tab to open to. Beware, this name is Windows version-specific!</param>
    /// <returns>Whether the properties window was shown successfully or not.</returns>
    public static bool ShowProperties(string path, string tab = null) {
        var info = new ShellExecuteInfo() {
            cbSize = (uint)Marshal.SizeOf<ShellExecuteInfo>(),
            lpVerb = "properties",
            lpFile = path,
            lpParameters = tab,
            nShow = 5, // SW_SHOW
            fMask = 12 // SEE_MASK_INVOKEIDLIST
        };
        return ShellExecuteEx(ref info);
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/nf-shellapi-shellexecuteexw
    // https://www.pinvoke.net/default.aspx/shell32/ShellExecuteEx.html
    [DllImport("shell32", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);

    // https://docs.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shellexecuteinfow
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct ShellExecuteInfo {
        public uint cbSize; // cbSize is specified as a DWORD, and "A DWORD is a 32-bit unsigned integer"
        public uint fMask;
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpVerb;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpParameters;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpDirectory;
        public int nShow;
        public IntPtr hInstApp;
        public IntPtr lpIDList;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpClass;
        public IntPtr hkeyClass;
        public uint dwHotKey;
        public IntPtr hIcon;
        public IntPtr hProcess;
    }
    #endregion

    #region WaitForWindow
    /// <summary>Waits until a window matching search parameters closes</summary>
    /// <param name="windowName">The window name (the window's title). If this parameter is <see langword="null"/>, all window names match.</param>
    /// <param name="windowClass">
    /// Specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
    /// <br/>If <paramref name="windowClass"/> is <see langword="null"/>, it finds any window whose title matches the <paramref name="windowName"/> parameter.
    /// </param>
    /// <param name="timeout">Seconds to wait before returning.</param>
    /// <returns><see langword="true"/> if the timeout expired, <see langword="false"/> if the window was closed.</returns>
    public static bool WaitForWindow(string windowName, string windowClass = null, int timeout = -1) {
        IntPtr hWnd = FindWindow(windowClass, windowName);

        if (hWnd == IntPtr.Zero) {
            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode == 0 || errorException.NativeErrorCode == 1400) {
                // 0: ERROR_SUCCESS: The operation completed successfully.
                // 1400: ERROR_INVALID_WINDOW_HANDLE: Invalid window handle.
                throw new ArgumentException("Window matching the specified parameters not found!", "windowName / windowClass", errorException);
            }
            throw errorException;
        }

        while (IsWindow(hWnd) && timeout != 0) {
            System.Threading.Thread.Sleep(1000);
            if (timeout > 0)
                timeout -= 1;
        }

        return timeout == 0;
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindoww
    // https://www.pinvoke.net/default.aspx/user32/FindWindow.html
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-iswindow
    // https://www.pinvoke.net/default.aspx/user32/IsWindow.html
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool IsWindow(IntPtr hWnd);
    #endregion

    #region WaitForWindowByThread
    /// <summary>
    /// Waits until the thread hosting a window matching the specified parameters exits.
    /// This is more efficient than <see cref="WaitForWindow"/>, but requires the Window/Thread to be running in the current process.
    /// </summary>
    /// <param name="windowName">The window name (the window's title). If this parameter is <see langword="null"/>, all window names match.</param>
    /// <param name="windowClass">
    /// Specifies the window class name. The class name can be any name registered with RegisterClass or RegisterClassEx, or any of the predefined control-class names.
    /// <br/>If <paramref name="windowClass"/> is <see langword="null"/>, it finds any window whose title matches the <paramref name="windowName"/> parameter.
    /// </param>
    /// <param name="timeout">Miliseconds to wait before returning. The Default value is Infinite.</param>
    /// <returns><see langword="true"/> if the timeout expired, <see langword="false"/> if the thread exited.</returns>
    public static bool WaitForWindowByThread(string windowName, string windowClass = null, uint timeout = WFSO_INFINITE) {
        // Get window handle
        IntPtr hWnd = FindWindow(windowClass, windowName);

        if (hWnd == IntPtr.Zero) {
            var errorException = new Win32Exception();
            if (errorException.NativeErrorCode == 0 || errorException.NativeErrorCode == 1400) {
                // 0: ERROR_SUCCESS: The operation completed successfully.
                // 1400: ERROR_INVALID_WINDOW_HANDLE: Invalid window handle.
                throw new ArgumentException("Window matching the specified parameters not found!", "windowName / windowClass", errorException);
            }
            throw errorException;
        }

        // Get threadID for window handle
        uint tID = GetWindowThreadProcessId(hWnd, out _);
        if (tID == 0) {
            throw new Win32Exception();
        }

        // Get thread handle for threadID
        using (SafeFileHandle handle = OpenThread(ThreadAccess.Synchronize, false, tID)) {
            if (handle.IsInvalid) {
                throw new Win32Exception();
            } else {

                // Wait for handle with specified timeout
                switch (WaitForSingleObject(handle, timeout)) {
                    case WFSO_Val.WAIT_OBJECT_0: // success condition
                        break;
                    case WFSO_Val.WAIT_ABANDONED: // thread exited without releasing mutex object
                        break;
                    case WFSO_Val.WAIT_TIMEOUT:
                        return true;
                    case WFSO_Val.WAIT_FAILED:
                        throw new Win32Exception();
                }

            }
        }

        return false;
    }

    // https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid
    // https://www.pinvoke.net/default.aspx/user32/GetWindowThreadProcessId.html
    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    // https://docs.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-openthread
    // https://www.pinvoke.net/default.aspx/kernel32/OpenThread.html
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern SafeFileHandle OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
    // SafeFileHandle has the same IsInvalid() and ReleaseHandle() members as SafeAccessTokenHandle, which is only available in .Net 4.6

    // https://docs.microsoft.com/en-us/windows/win32/procthread/thread-security-and-access-rights
    [Flags]
    private enum ThreadAccess : uint {
        /// <summary>Required to terminate a thread using TerminateThread.</summary>
        Terminate = 0x1,
        /// <summary>Required to suspend or resume a thread (see SuspendThread and ResumeThread)</summary>
        SuspendResume = 0x2,
        /// <summary>Required to read the context of a thread using GetThreadContext.</summary>
        GetContext = 0x8,
        /// <summary>Required to write the context of a thread using SetThreadContext.</summary>
        SetContext = 0x10,
        /// <summary>Required to set certain information in the thread object.</summary>
        SetInformation = 0x20,
        /// <summary>Required to read certain information from the thread object, such as the exit code.</summary>
        QueryInformation = 0x40,
        /// <summary>Required to set the impersonation token for a thread using SetThreadToken.</summary>
        SetThreadToken = 0x80,
        /// <summary>Required to use a thread's security information directly without calling it by using a communication mechanism that provides impersonation services.</summary>
        Impersonate = 0x100,
        /// <summary>Required for a server thread that impersonates a client.</summary>
        DirectImpersonation = 0x200,
        /// <summary>Required to set certain information in the thread object. A handle that has the <see cref="SetInformation"/> access right is automatically granted <see cref="SetLimitedInformation"/>.</summary>
        SetLimitedInformation = 0x400,
        /// <summary>Required to read certain information from the thread objects. A handle that has the <see cref="QueryInformation"/> access right is automatically granted <see cref="QueryLimitedInformation"/>.</summary>
        QueryLimitedInformation = 0x800,
        /// <summary>(Found in WINNT.H header file)</summary>
        Resume = 0x1000,
        /// <summary>Required to delete the object.</summary>
        Delete = 0x10000,
        /// <summary>Read access to the owner, group, and discretionary access control list (DACL) of the security descriptor.</summary>
        ReadControl = 0x20000,
        /// <summary>Write access to the discretionary access control list (DACL).</summary>
        WriteDac = 0x40000,
        /// <summary>Write access to owner. Required to change the owner in the security descriptor for the object.</summary>
        WriteOwner = 0x80000,
        /// <summary>Synchronize access. This enables a thread to wait until the object is in the signaled state.</summary>
        Synchronize = Win32FileAccess.Synchronize,
        /// <summary>
        /// All possible access rights for a thread object. For Windows Server 2008/Windows Vista and up.
        /// If this flag is specified on Windows Server 2003/Windows XP or below, the function specifying this flag fails with ERROR_ACCESS_DENIED.
        /// </summary>
        AllAccess_VistaAndUp = Win32FileAccess.StandardRightsRequired | Win32FileAccess.Synchronize | 0xFFFF,
        /// <summary>
        /// All possible access rights for a thread object. For Windows Server 2003/Windows XP and below.
        /// If this flag is specified on Windows Server 2008/Windows Vista and up, every possible access right is not granted.
        /// </summary>
        AllAccess_XPAndBelow = Win32FileAccess.StandardRightsRequired | Win32FileAccess.Synchronize | 0x3FF
    }


    // https://docs.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-waitforsingleobject
    // https://www.pinvoke.net/default.aspx/kernel32.waitforsingleobject
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern WFSO_Val WaitForSingleObject(SafeFileHandle handle, uint milliseconds);

    // https://docs.microsoft.com/en-us/windows/win32/api/synchapi/nf-synchapi-waitforsingleobject#return-value
    private enum WFSO_Val : uint {
        /// <summary>The state of the specified object is signaled.</summary>
        WAIT_OBJECT_0 = 0x0,
        /// <summary>
        /// The specified object is a mutex object that was not released by the thread that owned the mutex object before the owning thread terminated.
        /// Ownership of the mutex object is granted to the calling thread and the mutex state is set to nonsignaled.
        /// If the mutex was protecting persistent state information, you should check it for consistency.
        /// </summary>
        WAIT_ABANDONED = 0x80,
        /// <summary>The time-out interval elapsed, and the object's state is nonsignaled.</summary>
        WAIT_TIMEOUT = 0x102,
        /// <summary>The function has failed. To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>.</summary>
        WAIT_FAILED = 0xFFFFFFFF
    }

    private const uint WFSO_INFINITE = 0xFFFFFFFF;
    #endregion
}
