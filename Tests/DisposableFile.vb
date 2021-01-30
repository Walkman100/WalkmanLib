Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Public Class DisposableFile
        Implements IDisposable

        Private _filePath As String
        Public ReadOnly Property filePath As String
            Get
                Return _filePath
            End Get
        End Property
        Private disposed As Boolean = False

        Public Sub New(path As String, Optional createFile As Boolean = True, Optional checkFileExistence As Boolean = True)
            If createFile Then
                If Directory.Exists(path) Or File.Exists(path) Then
                    Throw New IOException("Path already exists!")
                End If
                File.Create(path).Dispose()
            ElseIf checkFileExistence Then
                If Not File.Exists(path) Then
                    Throw New FileNotFoundException("File doesn't exist, and createFile specified as false!", path)
                End If
            End If

            _filePath = New FileInfo(path).FullName
        End Sub

        Public Overrides Function ToString() As String
            Return filePath
        End Function

        Public Shared Widening Operator CType(v As DisposableFile) As String
            Return v.filePath
        End Operator

        Protected Overridable Overloads Sub Dispose(disposing As Boolean)
            If Not disposed Then
                If disposing Then DeleteFileIfExists(filePath)
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
