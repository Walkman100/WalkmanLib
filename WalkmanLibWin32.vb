Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Public Partial Class WalkmanLib
    
    ''' Link: http://www.thescarms.com/dotnet/NTFSCompress.aspx
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <param name="showWindow"></param>
    ''' <returns></returns>
    Shared Function CompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        
    End Function
    
    ''' Link: https://msdn.microsoft.com/en-us/library/windows/desktop/aa364592(v=vs.85).aspx
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <param name="showWindow"></param>
    ''' <returns></returns>
    Shared Function UncompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        
    End Function
    
    ''' Link: http://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    ''' Link: https://stackoverflow.com/a/22508299/2999220
    ''' <summary>Gets the compressed size of a specified file. Throws IOException on failure.</summary>
    ''' <param name="path">Path to the file to get size for.</param>
    ''' <returns>The compressed size of the file or the size of the file if file isn't compressed.</returns>
    Shared Function GetCompressedSize(path As String) As Double
        Dim sizeMultiplier As IntPtr
        Dim fileLength As Long = Convert.ToInt64(GetCompressedFileSize(path, sizeMultiplier))
        If fileLength = 4294967295 Then ' decimal representation of &HFFFFFFFF
            Dim Err As Long = Marshal.GetLastWin32Error()
            If Err <> 0 Then Throw New IOException("Exception getting compressed size: " & Err.ToString)
        End If
        Dim size As Double = (UInteger.MaxValue + 1) * CLng(sizeMultiplier) + fileLength
        Return size
    End Function
    
    Private Declare Function GetCompressedFileSize Lib "kernel32" Alias "GetCompressedFileSizeA"(ByVal lpFileName As String, ByRef lpFileSizeHigh As IntPtr) As UInteger
    
    ''' Link: http://www.vb-helper.com/howto_get_associated_program.html
    ''' <summary>Gets the path to the program specified to open a file.</summary>
    ''' <param name="path">The file to get the OpenWith program for.</param>
    ''' <returns>OpenWith program path, "Filetype not associated!" if none, or "File not found!"</returns>
    Shared Function GetOpenWith(path As String) As String
        If Not File.Exists(path) Then
            Return "File not found!"
        End If
        
        Dim pathDirectory As String = New IO.FileInfo(path).DirectoryName
        
        Dim result As String = Space$(1024)
        FindExecutable(path, pathDirectory & "\", result)
        
        Dim returnString As String = Strings.Left$(result, InStr(result, Chr(0)) - 1)
        If returnString = "" Then 
            Return "Filetype not associated!"
        Else
            Return returnString
        End If
    End Function
    
    Private Declare Function FindExecutable Lib "shell32.dll" Alias "FindExecutableA"(lpFile As String, lpDirectory As String, lpResult As String) As Long
    
    ''' Link: https://stackoverflow.com/a/1936957/2999220
    ''' <summary>Opens the Windows properties window for a path.</summary>
    ''' <param name="path">The path to show the window for.</param>
    ''' <returns>Whether the properties window was shown successfully or not.</returns>
    Shared Function ShowProperties(path As String) As Boolean
        Dim info As New ShellExecuteInfo
        info.cbSize = Marshal.SizeOf(info)
        info.lpVerb = "properties"
        info.lpFile = path
        info.fMask = 12
        Return ShellExecuteEx(info)
    End Function
    
    <DllImport("shell32.dll", CharSet := CharSet.Auto)> _
    Private Shared Function ShellExecuteEx(ByRef lpExecInfo As ShellExecuteInfo) As Boolean
    End Function
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
