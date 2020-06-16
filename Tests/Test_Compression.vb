Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System.IO

Namespace Tests
    Module Tests_Compression
        Function Test_Compression1(rootTestFolder As String) As Boolean
            Using testFile As DisposableFile = New DisposableFile(Path.Combine(rootTestFolder, "compression1.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.SetCompression(testFile.filePath, False)

                If File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression1", "File is compressed", "File not compressed")
                End If

                If Not WalkmanLib.SetCompression(testFile.filePath, True) Then
                    Return TestString("Compression1", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression1", File.GetAttributes(testFile.filePath), FileAttributes.Compressed)
            End Using
        End Function

        Function Test_Compression2(rootTestFolder As String) As Boolean
            Using testFile As DisposableFile = New DisposableFile(Path.Combine(rootTestFolder, "compression2.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.SetCompression(testFile.filePath, True)

                If Not File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression2", "File is not compressed", "File is compressed")
                End If

                If Not WalkmanLib.SetCompression(testFile.filePath, False) Then
                    Return TestString("Compression2", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression2", File.GetAttributes(testFile.filePath), FileAttributes.Normal)
            End Using
        End Function
        Function Test_Compression3(rootTestFolder As String) As Boolean
            Using testFile As DisposableFile = New DisposableFile(Path.Combine(rootTestFolder, "compression3.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFile.filePath)

                If File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression3", "File is compressed", "File not compressed")
                End If

                If Not WalkmanLib.CompressFile(testFile.filePath) Then
                    Return TestString("Compression3", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression3", File.GetAttributes(testFile.filePath), FileAttributes.Compressed)
            End Using
        End Function

        Function Test_Compression4(rootTestFolder As String) As Boolean
            Using testFile As DisposableFile = New DisposableFile(Path.Combine(rootTestFolder, "compression4.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.CompressFile(testFile.filePath)

                If Not File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression4", "File is not compressed", "File is compressed")
                End If

                If Not WalkmanLib.UncompressFile(testFile.filePath) Then
                    Return TestString("Compression4", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression4", File.GetAttributes(testFile.filePath), FileAttributes.Normal)
            End Using
        End Function
    End Module
End Namespace
