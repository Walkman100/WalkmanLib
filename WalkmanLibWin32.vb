Option Explicit Off

Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports System.ComponentModel
Public Partial Class WalkmanLib
    
    ''' <summary>Compresses the specified file using NTFS compression.</summary>
    ''' <param name="path">Path to the file to compress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not (TO DO).</param>
    ''' <returns>Whether the file was compressed successfully or not.</returns>
    Shared Function CompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        Return SetCompression(path, True, showWindow)
    End Function
    
    ''' <summary>Decompresses the specified file using NTFS compression.</summary>
    ''' <param name="path">Path to the file to decompress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not (TO DO).</param>
    ''' <returns>Whether the file was decompressed successfully or not.</returns>
    Shared Function UncompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        Return SetCompression(path, False, showWindow)
    End Function
    
    ' Link: http://www.thescarms.com/dotnet/NTFSCompress.aspx
    ' Link: https://msdn.microsoft.com/en-us/library/windows/desktop/aa364592(v=vs.85).aspx
    ''' <summary>Compress or decompress the specified file using NTFS compression.</summary>
    ''' <param name="path">Path to the file to (de)compress.</param>
    ''' <param name="compress">True to compress, False to decompress.</param>
    ''' <param name="showWindow">Whether to show the compression status window or not (TO DO).</param>
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
    
    ' Link: http://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    ' Link: https://stackoverflow.com/a/22508299/2999220
    ''' <summary>Gets the compressed size of a specified file. Throws IOException on failure.</summary>
    ''' <param name="path">Path to the file to get size for.</param>
    ''' <returns>The compressed size of the file or the size of the file if file isn't compressed.</returns>
    Shared Function GetCompressedSize(path As String) As Double
        Dim sizeMultiplier As IntPtr
        Dim fileLength As Long = Convert.ToInt64(GetCompressedFileSize(path, sizeMultiplier))
        If fileLength = 4294967295 Then ' decimal representation of &HFFFFFFFF
            Dim Win32Error As Integer = Marshal.GetLastWin32Error()
            Dim errorException = New Win32Exception(Win32Error)
            If Win32Error <> 0 Then Throw New IOException(errorException.Message, errorException)
        End If
        Dim size As Double = (UInteger.MaxValue + 1) * CLng(sizeMultiplier) + fileLength
        Return size
    End Function
    
    Private Declare Function GetCompressedFileSize Lib "kernel32" Alias "GetCompressedFileSizeA"(ByVal lpFileName As String, ByRef lpFileSizeHigh As IntPtr) As UInteger
    
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
    
    ' Link: https://stackoverflow.com/a/33487494/2999220
    ''' <summary>Gets the target of a symbolic link or directory junction. Throws ComponentModel.Win32Exception on error.</summary>
    ''' <param name="path">Path to the symlink to get the target of.</param>
    ''' <returns>The fully qualified path to the target.</returns>
    Shared Function GetSymlinkTarget(path As String) As String
        Dim INVALID_HANDLE_VALUE As New IntPtr(-1)
        Const FILE_READ_EA As UInteger = &H8
        Const FILE_FLAG_BACKUP_SEMANTICS As UInteger = &H2000000
        
        Dim fileHandle = CreateFile(path, FILE_READ_EA, FileShare.ReadWrite Or FileShare.Delete, IntPtr.Zero, FileMode.Open, FILE_FLAG_BACKUP_SEMANTICS, IntPtr.Zero)
        If fileHandle = INVALID_HANDLE_VALUE Then Throw New Win32Exception()
        
        Dim returnString As String = ""
        Try
            Dim stringBuilderTarget = New Text.StringBuilder(1024)
            Dim result = GetFinalPathNameByHandle(fileHandle, stringBuilderTarget, 1024, 0)
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
    
    <DllImport("kernel32.dll", CharSet := CharSet.Auto, SetLastError := True)> _
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
    
    ' Link: https://stackoverflow.com/a/1936957/2999220
    ''' <summary>Opens the Windows properties window for a path.</summary>
    ''' <param name="path">The path to show the window for.</param>
    ''' <param name="tab">Optional tab to open to. Beware, this name is culture-specific!</param>
    ''' <returns>Whether the properties window was shown successfully or not.</returns>
    Shared Function ShowProperties(path As String, Optional tab As String = Nothing) As Boolean
        Dim info As New ShellExecuteInfo
        info.cbSize = Marshal.SizeOf(info)
        info.lpVerb = "properties"
        info.lpFile = path
        If tab <> Nothing Then info.lpParameters = tab
        info.fMask = 12
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
