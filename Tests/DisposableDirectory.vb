Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Public Class DisposableDirectory
        Implements IDisposable

        Private _dirPath As String
        Public ReadOnly Property dirPath As String
            Get
                Return _dirPath
            End Get
        End Property
        Private disposed As Boolean = False

        Public Sub New(path As String, Optional createDir As Boolean = True)
            If createDir Then
                If Directory.Exists(path) Or File.Exists(path) Then
                    Throw New IOException("Path already exists!")
                End If
                _dirPath = Directory.CreateDirectory(path).FullName
            Else
                _dirPath = New DirectoryInfo(path).FullName
            End If
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
End Namespace
