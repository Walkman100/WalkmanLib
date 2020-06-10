Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Public Class DisposableDirectory
    Implements IDisposable

    Public ReadOnly Property dirPath As String
    Private disposed As Boolean = False

    Public Sub New(path As String)
        If Directory.Exists(path) Or File.Exists(path) Then
            Throw New IOException("Path already exists!")
        End If

        dirPath = Directory.CreateDirectory(path).FullName
    End Sub

    Public Overrides Function ToString() As String
        Return dirPath
    End Function

    Protected Overridable Overloads Sub Dispose(disposing As Boolean)
        If Not disposed Then
            Try
                If disposing Then Directory.Delete(dirPath, True)
            Catch ex As DirectoryNotFoundException
            End Try
            disposed = True
        End If
    End Sub

    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub
End Class
