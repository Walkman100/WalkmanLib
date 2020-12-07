Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace Tests
    Module Tests_WaitFor
        Private Const shell32WindowName As String = "shell32.dll Properties"
        ' https://gist.github.com/Memphizzz/ed69d2500c422019609c
        Private Const shell32WindowClass As String = "#32770"

        Function Test_WaitForWindow1() As Boolean
            If WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll")) = False Then
                Return TestString("WaitForWindow1", "ShowProperties returned False", "ShowProperties returned True")
            End If

            Task.Run(Sub()
                         Thread.Sleep(700)
                         SendKeys.SendWait("{ESC}")
                     End Sub)

            Thread.Sleep(400) ' give the window time to show

            Dim result As Boolean = WalkmanLib.WaitForWindow(shell32WindowName, shell32WindowClass, 10)

            Return TestBoolean("WaitForWindow1", result, False)
        End Function

        Function Test_WaitForWindow2() As Boolean
            If WalkmanLib.ShowProperties(Path.Combine(Environment.SystemDirectory, "shell32.dll")) = False Then
                Return TestString("WaitForWindow2", "ShowProperties returned False", "ShowProperties returned True")
            End If

            Task.Run(Sub()
                         Thread.Sleep(700)
                         SendKeys.SendWait("{ESC}")
                     End Sub)

            Thread.Sleep(400)

            Dim result As Boolean = WalkmanLib.WaitForWindowByThread(shell32WindowName, shell32WindowClass, 40 * 1000)

            Return TestBoolean("WaitForWindow2", result, False)
        End Function
    End Module
End Namespace
