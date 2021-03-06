Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace Tests
    Friend Class ShowPropertiesTestsHelper
        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
        'https://www.pinvoke.net/default.aspx/user32/GetForegroundWindow.html
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function GetForegroundWindow() As IntPtr
        End Function

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextw
        'https://www.pinvoke.net/default.aspx/user32/GetWindowText.html
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function GetWindowText(hWnd As IntPtr, lpString As Text.StringBuilder, nMaxCount As Integer) As Integer
        End Function

        'https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindow
        'https://www.pinvoke.net/default.aspx/user32/GetWindow.html
        <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
        Private Shared Function GetWindow(hWnd As IntPtr, uCmd As UInteger) As IntPtr
        End Function

        Friend Shared Function GetActiveWindowText(Optional getChildText As Boolean = False) As String
            Dim windowHandle As IntPtr
            windowHandle = GetForegroundWindow()
            If Marshal.GetLastWin32Error <> 0 Then Throw New System.ComponentModel.Win32Exception()

            If getChildText Then
                windowHandle = GetWindow(windowHandle, 5) 'GW_CHILD = 5
                If Marshal.GetLastWin32Error <> 0 Then Throw New System.ComponentModel.Win32Exception()
            End If

            Dim stringBuilderTarget As New Text.StringBuilder(1024)
            Dim result As Integer = GetWindowText(windowHandle, stringBuilderTarget, 1024)
            If result = 0 Then Throw New System.ComponentModel.Win32Exception()
            Return stringBuilderTarget.ToString()
        End Function
    End Class

    Module Tests_ShowProperties
        Function Test_ShowProperties1() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(600)
                         SendKeys.SendWait("{ENTER}")
                     End Sub)

            Dim result As Boolean = WalkmanLib.ShowProperties("nonExistantFile.txt")

            Return TestBoolean("ShowProperties1", result, False)
        End Function

        Function Test_ShowProperties2() As Boolean
            Dim result As Boolean = WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll"))

            Thread.Sleep(600) ' ShowProperties is Async when it succeeds
            SendKeys.SendWait("{ESC}")
            Thread.Sleep(10)  ' wait for window to close else next functions don't work

            Return TestBoolean("ShowProperties2", result, True)
        End Function

        Function Test_ShowProperties3() As Boolean
            Try
                Dim result As Boolean = WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll"))
                If Not result Then
                    Return TestBoolean("ShowProperties3", result, True)
                End If

                Thread.Sleep(700) ' wait for window to show
                Return TestString("ShowProperties3", ShowPropertiesTestsHelper.GetActiveWindowText(), "shell32.dll Properties")
            Finally
                SendKeys.SendWait("{ESC}")
                Thread.Sleep(10)
            End Try
        End Function

        Function Test_ShowProperties4() As Boolean
            Try
                Dim result As Boolean = WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll"), "Details")
                If Not result Then
                    Return TestBoolean("ShowProperties4", result, True)
                End If

                Thread.Sleep(700)

                Dim tabName As String
                Try
                    tabName = ShowPropertiesTestsHelper.GetActiveWindowText(True)
                Catch ex As Exception
                    tabName = ex.ToString
                End Try

                Return TestString("ShowProperties4", tabName, "Details")
            Finally
                SendKeys.SendWait("{ESC}")
                Thread.Sleep(10)
            End Try
        End Function
    End Module
End Namespace
