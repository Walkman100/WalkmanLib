Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.Threading
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Namespace Tests
    Module Tests_Mouse
        Function Test_Mouse1() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(400)

                         Cursor.Position = New System.Drawing.Point(325, 55)
                         WalkmanLib.MouseClick(MouseButton.LeftClick)
                     End Sub)

            Dim result As String = InputBox("Press Esc if this window doesn't disappear", DefaultResponse:="test", XPos:=0, YPos:=0)

            Return TestString("Mouse1", result, "test")
        End Function
        Function Test_Mouse2() As Boolean
            Task.Run(Sub()
                         Thread.Sleep(400)

                         Cursor.Position = New System.Drawing.Point(100, 10)
                         WalkmanLib.MouseClick(MouseButton.RightClick)
                         Thread.Sleep(10)
                         Cursor.Position = New System.Drawing.Point(150, 140)
                         WalkmanLib.MouseClick(MouseButton.LeftClick)
                     End Sub)

            Dim result As String = InputBox("Click OK if this window doesn't get closed", DefaultResponse:="test", XPos:=0, YPos:=0)

            Return TestString("Mouse2", result, "")
        End Function
    End Module
End Namespace