Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_ContextMenu
        Function Test_ContextMenu1(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu1.txt"))
                Using cm As New WalkmanLib.ContextMenu()
                    cm.BuildMenu(IntPtr.Zero, {testFile.filePath})

                    Return TestBoolean("ContextMenu1", cm.IsBuilt(), True)
                End Using
            End Using
        End Function

        Function Test_ContextMenu2(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu2.txt"))
                Using cm As New WalkmanLib.ContextMenu()
                    cm.BuildMenu(IntPtr.Zero, {testFile.filePath})
                    cm.DestroyMenu()

                    Return TestBoolean("ContextMenu2", cm.IsBuilt(), False)
                End Using
            End Using
        End Function

        Function Test_ContextMenu3(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu3.txt"))
                Using cm As New WalkmanLib.ContextMenu()
                    Dim ex As Exception = New NoException
                    Try
                        cm.AddItem(-1, "test", Sub() Console.WriteLine("test"))
                    Catch ex2 As Exception
                        ex = ex2
                    End Try

                    Return TestType("ContextMenu3", ex.GetType(), GetType(NotSupportedException))
                End Using
            End Using
        End Function

        Function Test_ContextMenu4(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu4.txt"))
                Using cm As New WalkmanLib.ContextMenu()
                    cm.BuildMenu(IntPtr.Zero, {testFile.filePath})

                    Dim ex As Exception = New NoException
                    Try
                        cm.AddItem(-1, "test", Sub() Console.WriteLine("test"))
                    Catch ex2 As Exception
                        ex = ex2
                    End Try

                    Return TestType("ContextMenu4", ex.GetType(), GetType(ArgumentOutOfRangeException))
                End Using
            End Using
        End Function

        Function Test_ContextMenu5(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "contextMenu5.txt"))
                Using cm As New WalkmanLib.ContextMenu()
                    cm.BuildMenu(IntPtr.Zero, {testFile.filePath}, 2)

                    Dim ex As Exception = New NoException
                    Try
                        cm.AddItem(-1, "test", Sub() Console.WriteLine("test"))
                    Catch ex2 As Exception
                        ex = ex2
                    End Try

                    Return TestType("ContextMenu5", ex.GetType(), GetType(NoException))
                End Using
            End Using
        End Function
    End Module
End Namespace