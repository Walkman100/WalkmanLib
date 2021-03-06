Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_StreamCopy
#If NoOokii Then

        Function Test_StreamCopy1(rootTestFolder As String) As Boolean
            WriteTestSkipped(New Collections.Generic.List(Of String)({"StreamCopy1",
                                                                      "StreamCopy2",
                                                                      "StreamCopy3",
                                                                      "StreamCopyThrows1",
                                                                      "StreamCopyThrows2"}), "No Ookii.Dialogs available")
            Return True
        End Function
        Function Test_StreamCopy2(rootTestFolder As String) As Boolean
            Return True
        End Function
        Function Test_StreamCopy3(rootTestFolder As String) As Boolean
            Return True
        End Function
        Function Test_StreamCopyThrows1() As Boolean
            Return True
        End Function
        Function Test_StreamCopyThrows2(rootTestFolder As String) As Boolean
            Return True
        End Function

#Else     ' disable if NoOokii is defined (default for tests project)

        Function Test_StreamCopy1(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy1Source"))
                Dim randByteAmount As Integer = 1024
                Dim randBytes(randByteAmount) As Byte
                Call New Random().NextBytes(randBytes)

                Dim fileText As String = Convert.ToBase64String(randBytes)
                File.WriteAllText(testFileSource, fileText, Text.Encoding.ASCII)

                Using testFileTarget As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy1Target"))
                    Dim source As FileStream = New FileStream(testFileSource, FileMode.Open)
                    Dim target As FileStream = New FileStream(testFileTarget, FileMode.Truncate)

                    Threading.Tasks.Task.Run(Sub()
                                                 Try
                                                     WalkmanLib.StreamCopy(source, target)
                                                 Catch ' StreamCopy Disposes the streams if copy started successfully, if it didn't then we manually close them
                                                     source.Dispose()
                                                     target.Dispose()
                                                     Throw
                                                 End Try
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
                File.WriteAllText(testFileSource, Convert.ToBase64String(randBytes), Text.Encoding.ASCII)

                Dim returned As Boolean = False

                Dim source As FileStream = File.OpenRead(testFileSource)
                Dim target As FileStream = File.OpenWrite(testFileTarget)

                Threading.Tasks.Task.Run(Sub()
                                             WalkmanLib.StreamCopy(source, target, onComplete:=Sub() returned = True)
                                         End Sub)
                Threading.Thread.Sleep(100)

                Return TestBoolean("StreamCopy2", returned, True)
            End Using
        End Function

        Function Test_StreamCopy3(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy3Source")),
                    testFileTarget As New DisposableFile(Path.Combine(rootTestFolder, "streamCopy3Target"))

                Dim randBytes(8) As Byte
                Call New Random().NextBytes(randBytes)
                File.WriteAllText(testFileTarget, Convert.ToBase64String(randBytes), Text.Encoding.ASCII)

                WalkmanLib.StreamCopy(File.OpenRead(testFileSource), File.OpenWrite(testFileTarget))

                Return TestString("StreamCopy3", File.ReadAllText(testFileTarget), "")
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

#End If
    End Module
End Namespace
