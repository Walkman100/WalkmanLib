Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_StreamCopy
        Function Test_StreamCopy1(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy1"))
                Dim randByteAmount As Integer = 1024
                Dim randBytes(randByteAmount) As Byte
                Call New Random().NextBytes(randBytes)

                Dim fileText As String = Convert.ToBase64String(randBytes)
                File.WriteAllText(testFileSource, fileText, Text.Encoding.ASCII)

                Using testFileTarget As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy1Target"))
                    Dim source As FileStream = New FileStream(testFileSource, FileMode.Open)
                    Dim target As FileStream = New FileStream(testFileTarget, FileMode.Truncate)

                    Threading.Tasks.Task.Run(Sub()
                                                 WalkmanLib.StreamCopy(source, target)
                                             End Sub)
                    Threading.Thread.Sleep(200)

                    Return TestString("StreamCopy1", File.ReadAllText(testFileTarget), fileText)
                End Using
            End Using
        End Function

        Function Test_StreamCopy2(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy2Source")),
                    testFileTarget As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy2Target"))

                Dim randBytes(8) As Byte
                Call New Random().NextBytes(randBytes)
                File.WriteAllText(testFileTarget, Convert.ToBase64String(randBytes), Text.Encoding.ASCII)

                WalkmanLib.StreamCopy(File.OpenRead(testFileSource), File.OpenWrite(testFileTarget))

                Return TestString("StreamCopy2", File.ReadAllText(testFileTarget), "")
            End Using
        End Function

        Function Test_StreamCopyThrows1() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.StreamCopy(Nothing, Nothing)
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("StreamCopyThrows1", ex.GetType(), GetType(ArgumentNullException))
        End Function

        Function Test_StreamCopyThrows2(rootTestFolder As String) As Boolean
            Dim ex As Exception = New NoException
            Try
                Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "streamCopyThrows2Source")),
                        testFileTarget As New DisposableFile(Path.Combine(rootTestFolder, "streamCopyThrows2Target")),
                        source As FileStream = File.OpenWrite(testFileSource),
                        target As FileStream = File.OpenRead(testFileTarget)
                    WalkmanLib.StreamCopy(source, target)
                End Using
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("StreamCopyThrows2", ex.GetType(), GetType(InvalidOperationException))
        End Function
    End Module
End Namespace
