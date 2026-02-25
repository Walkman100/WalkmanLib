Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports System.Windows.Forms

Namespace Tests
    Module Tests_ContextMenu
        Function Test_ContextMenu1(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu1.txt")),
                  cm As New WalkmanLib.ContextMenu()
                cm.BuildMenu(IntPtr.Zero, {testFile})

                Return TestBoolean("ContextMenu1", cm.IsBuilt(), True)
            End Using
        End Function

        Function Test_ContextMenu2(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu2.txt")),
                  cm As New WalkmanLib.ContextMenu()
                cm.BuildMenu(IntPtr.Zero, {testFile})
                cm.DestroyMenu()

                Return TestBoolean("ContextMenu2", cm.IsBuilt(), False)
            End Using
        End Function

        Function Test_ContextMenu3(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu3.txt")),
                  cm As New WalkmanLib.ContextMenu()
                Dim ex As Exception = New NoException
                Try
                    cm.AddItem(-1, "test", Sub() Console.WriteLine("test"))
                Catch ex2 As Exception
                    ex = ex2
                End Try

                Return TestType("ContextMenu3", ex.GetType(), GetType(NotSupportedException))
            End Using
        End Function

        Function Test_ContextMenu4(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu4.txt")),
                  cm As New WalkmanLib.ContextMenu()
                cm.BuildMenu(IntPtr.Zero, {testFile})

                Dim ex As Exception = New NoException
                Try
                    cm.AddItem(-1, "test", Sub() Console.WriteLine("test"))
                Catch ex2 As Exception
                    ex = ex2
                End Try

                Return TestType("ContextMenu4", ex.GetType(), GetType(ArgumentOutOfRangeException))
            End Using
        End Function

        Function Test_ContextMenu5(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu5.txt")),
                  cm As New WalkmanLib.ContextMenu()
                cm.BuildMenu(IntPtr.Zero, {testFile}, 2)

                Dim ex As Exception = New NoException
                Try
                    cm.AddItem(-1, "test", Sub() Console.WriteLine("test"))
                Catch ex2 As Exception
                    ex = ex2
                End Try

                Return TestType("ContextMenu5", ex.GetType(), GetType(NoException))
            End Using
        End Function

        Function Test_ContextMenu6(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu6.txt")),
                  cm As New WalkmanLib.ContextMenu()
                cm.BuildMenu(IntPtr.Zero, {testFile})

                Try
                    cm.ShowMenu(IntPtr.Zero, New Drawing.Point(0, 0))
                Catch ex As Win32Exception
                    Return TestNumber("ContextMenu6", ex.NativeErrorCode, WalkmanLib.NativeErrorCode.ERROR_INVALID_WINDOW_HANDLE)
                Catch ex As Exception
                    Return TestType("ContextMenu6", ex.GetType(), GetType(Win32Exception))
                End Try

                Return TestType("ContextMenu6", GetType(NoException), GetType(Win32Exception))
            End Using
        End Function

        Dim renameCalled As Boolean
        Sub RenameCallback()
            renameCalled = True
        End Sub
        Function Test_ContextMenuUI1(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenuUI1.txt")),
                  cm As New WalkmanLib.ContextMenu()
                Dim frm As New Form

                renameCalled = False
                AddHandler cm.ItemRenamed, AddressOf RenameCallback

                Tasks.Task.Run(Sub() Application.Run(frm))

                cm.BuildMenu(frm.Handle, {testFile}, flags:=WalkmanLib.ContextMenu.QueryContextMenuFlags.CanRename)
                frm.BringToFront()

                Tasks.Task.Run(Sub()
                                   Thread.Sleep(500)
                                   SendKeys.SendWait("{UP 2}")
                                   SendKeys.SendWait("{ENTER}")
                               End Sub)
                frm.Invoke(Sub() cm.ShowMenu(frm.Handle, frm.PointToScreen(New Drawing.Point(0, 0))))
                frm.Close()

                Return TestBoolean("ContextMenuUI1", renameCalled, True)
            End Using
        End Function

        Function Test_ContextMenuUI2(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenuUI2.txt")),
                  cm As New WalkmanLib.ContextMenu
                Dim frm As New Form

                Tasks.Task.Run(Sub() Application.Run(frm))

                cm.BuildMenu(frm.Handle, {testFile}, 10)
                frm.BringToFront()

                renameCalled = False
                cm.AddItem(-1, "test", AddressOf RenameCallback)

                Tasks.Task.Run(Sub()
                                   Thread.Sleep(500)
                                   SendKeys.SendWait("{UP}")
                                   SendKeys.SendWait("{ENTER}")
                               End Sub)
                frm.Invoke(Sub() cm.ShowMenu(frm.Handle, frm.PointToScreen(New Drawing.Point(0, 0))))
                frm.Close()

                Return TestBoolean("ContextMenuUI2", renameCalled, True)
            End Using
        End Function

        Dim helpText As String
        Sub HelpCallback(text As String, ex As Exception)
            If ex IsNot Nothing Then
                helpText = ex.Message
            ElseIf Not String.IsNullOrEmpty(text) Then
                helpText = text
            End If
        End Sub
        Class HandleWMForm
            Inherits Form
            Public cm As WalkmanLib.ContextMenu

            Protected Overrides Sub WndProc(ByRef m As Message)
                cm.HandleWindowMessage(m)
                MyBase.WndProc(m)
            End Sub
        End Class

        Function Test_ContextMenuUI3(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenuUI3.txt"))
                Dim frm As New HandleWMForm

                frm.cm = New WalkmanLib.ContextMenu
                helpText = Nothing
                AddHandler frm.cm.HelpTextChanged, AddressOf HelpCallback

                Tasks.Task.Run(Sub() Application.Run(frm))

                frm.cm.BuildMenu(frm.Handle, {testFile})
                frm.BringToFront()

                Tasks.Task.Run(Sub()
                                   Thread.Sleep(500)
                                   SendKeys.SendWait("{UP}")
                                   SendKeys.SendWait("{ESC}")
                               End Sub)
                frm.Invoke(Sub() frm.cm.ShowMenu(frm.Handle, frm.PointToScreen(New Drawing.Point(0, 0))))
                frm.Close()
                frm.cm.DestroyMenu()

                Return TestString("ContextMenuUI3", helpText, "Displays the properties of the selected items.")
            End Using
        End Function
    End Module
End Namespace
