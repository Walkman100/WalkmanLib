Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Public Partial Class WalkmanLib
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Shared Function ShowProperties(path As String) As Boolean
        Dim info As New ShellExecuteInfo
        info.cbSize = Marshal.SizeOf(info)
        info.lpVerb = "porperties"
        info.lpFile = path
        info.fMask = 12
        Return ShellExecuteEx(info)
    End Function
    ' https://stackoverflow.com/a/1936957/2999220
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
    
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <param name="showWindow"></param>
    ''' <returns></returns>
    Shared Function CompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        
    End Function
    
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <param name="showWindow"></param>
    ''' <returns></returns>
    Shared Function UncompressFile(path As String, Optional showWindow As Boolean = True) As Boolean
        
    End Function
    
        ' usage:
        'Try
        '    compressedSizeOrError = CompressedFileSize(lblFullPath.Text)
        'Catch ex As Exception
        '    compressedSizeOrError = ex.Message
        'End Try
        'chkCompressed.Text = "Compressed"
    
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
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
    ' http://www.pinvoke.net/default.aspx/kernel32/GetCompressedFileSize.html
    ' https://stackoverflow.com/a/22508299/2999220
    Private Declare Function GetCompressedFileSize Lib "kernel32" Alias "GetCompressedFileSizeA"(ByVal lpFileName As String, ByRef lpFileSizeHigh As IntPtr) As UInteger
    
    ''' <summary></summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Shared Function GetOpenWith(path As String) As String
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
    ' http://www.vb-helper.com/howto_get_associated_program.html
    Private Declare Function FindExecutable Lib "shell32.dll" Alias "FindExecutableA"(lpFile As String, lpDirectory As String, lpResult As String) As Long
End Class
