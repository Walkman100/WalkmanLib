Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Management
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic

' Credits:
'  Main code: https://code.msdn.microsoft.com/windowsapps/Sample-to-demonstrate-how-495e69db
'  Additional version tables:
'   http://www.nirmaltv.com/2009/08/17/windows-os-version-numbers/
'    which links to https://www.msigeek.com/442/windows-os-version-numbers
'   https://stackoverflow.com/a/2819962/2999220
'   https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_osversioninfoexa
'   https://docs.microsoft.com/en-us/windows/desktop/SysInfo/operating-system-version

Public Enum WindowsVersion
    Windows1Point0
    Windows2Point0
    Windows3Point0
    WindowsNT3Point1
    WindowsNT3Point11
    WindowsNT3Point5
    WindowsNT3Point51
    Windows95
    WindowsNT4Point0
    Windows98
    Windows98SE
    WindowsME
    Windows2000
    WindowsXP
    
    WindowsXPProX64
    WindowsServer2003
    WindowsServer2003R2
    
    WindowsVista
    WindowsServer2008
    
    Windows7
    WindowsServer2008R2
    
    Windows8
    WindowsServer2012
    
    Windows8Point1
    WindowsServer2012R2
    
    Windows10
    WindowsServer2016
End Enum

Public Partial Class WalkmanLib
    
    ''' <summary>Gets whether the current Operating System is a Windows Server version or not</summary>
    ''' <returns>True if current environment is a Server version, and False for a standard Workstation version</returns>
    Shared Function IsWindowsServer As Boolean
                               ' add a reference to System.Management.dll
        Using searcher As New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem")
            For Each managementObject As ManagementObject In searcher.[Get]()
                ' ProductType will be one of:
                ' 1: Workstation
                ' 2: Domain Controller
                ' 3: Server
                Dim productType As UInteger = CUInt(managementObject.GetPropertyValue("ProductType"))
                Return productType <> 1
            Next
        End Using
        
        Return False
    End Function
    
    ' this helps distinguish between Windows Server 2003 and Windows Server 2003 R2
    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)> _
    Private Shared Function GetSystemMetrics(smIndex As Integer) As Integer
    End Function
    
    ''' <summary>Gets the current Windows version. NOTE: To get an accurate version on Windows versions above 8, 
    ''' you will need to embed a manifest as per https://msdn.microsoft.com/E7A1A16A-95B3-4B45-81AD-A19E33F15AE4
    ''' (https://docs.microsoft.com/en-us/windows/desktop/SysInfo/targeting-your-application-at-windows-8-1)</summary>
    ''' <returns>A Windows version of type (currentNamespace).WindowsVersion</returns>
    Shared Function GetWindowsVersion As WindowsVersion
        Dim currentVersion As Version = Environment.OSVersion.Version
        
        Select Case currentVersion.Major
            Case 1
                Return WindowsVersion.Windows1Point0
            Case 2
                Return WindowsVersion.Windows2Point0
            Case 3
                If currentVersion.Minor = 0 Then
                    Return WindowsVersion.Windows3Point0
                ElseIf currentVersion.Minor = 10
                    Return WindowsVersion.WindowsNT3Point1
                ElseIf currentVersion.Minor = 11
                    Return WindowsVersion.WindowsNT3Point11
                ElseIf currentVersion.Minor = 5
                    Return WindowsVersion.WindowsNT3Point5
                ElseIf currentVersion.Minor = 51
                    Return WindowsVersion.WindowsNT3Point51
                End If
                
            Case 4
                If currentVersion.Minor = 0 Then
                    If currentVersion.MinorRevision = 950 Then
                        Return WindowsVersion.Windows95
                    ElseIf currentVersion.MinorRevision = 1381 Then
                        Return WindowsVersion.WindowsNT4Point0
                    End If
                ElseIf currentVersion.Minor = 1 Or currentVersion.Minor = 10 Then
                    If currentVersion.MinorRevision = 1998 Then
                        Return WindowsVersion.Windows98
                    ElseIf currentVersion.MinorRevision = 2222 Then
                        Return WindowsVersion.Windows98SE
                    End If
                ElseIf currentVersion.Minor = 90
                    Return WindowsVersion.WindowsME
                End If
                
            Case 5
                If currentVersion.Minor = 0 Then
                    Return WindowsVersion.Windows2000
                ElseIf currentVersion.Minor = 1
                    Return WindowsVersion.WindowsXP
                ElseIf currentVersion.Minor = 2
                    If IsWindowsServer() Then
                        If GetSystemMetrics(89) = 0 Then
                            Return WindowsVersion.WindowsServer2003
                        Else
                            Return WindowsVersion.WindowsServer2003R2
                        End If
                        
                        ' Possibly also Windows Home Server - see https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_osversioninfoexa
                    Else
                        Return WindowsVersion.WindowsXPProX64
                    End If
                End If
                
            Case 6
                If currentVersion.Minor = 0 Then
                    If IsWindowsServer() Then
                        Return WindowsVersion.WindowsServer2008
                    Else
                        Return WindowsVersion.WindowsVista
                    End If
                ElseIf currentVersion.Minor = 1 Then
                    If IsWindowsServer() Then
                        Return WindowsVersion.WindowsServer2008R2
                    Else
                        Return WindowsVersion.Windows7
                    End If
                ElseIf currentVersion.Minor = 2 Then
                    If IsWindowsServer() Then
                        Return WindowsVersion.WindowsServer2012
                    Else
                        Return WindowsVersion.Windows8
                    End If
                ElseIf currentVersion.Minor = 3 Then
                    If IsWindowsServer() Then
                        Return WindowsVersion.WindowsServer2012R2
                    Else
                        Return WindowsVersion.Windows8Point1
                    End If
                End If
                
            Case 10
                If IsWindowsServer() Then
                    Return WindowsVersion.WindowsServer2016
                Else
                    Return WindowsVersion.Windows10
                End If
        End Select
        
        Throw New InvalidOperationException("Unrecognised Windows Version!" & vbNewLine & vbNewLine & _
            "VersionString: " & Environment.OSVersion.VersionString & vbNewLine & _
            "Version.ToString: " & currentVersion.ToString())
    End Function
End Class
