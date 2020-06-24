Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_Compression
        Function Test_Compression1(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression1.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.SetCompression(testFile.filePath, False)

                If File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression1", "File is compressed", "File is not compressed")
                End If

                If Not WalkmanLib.SetCompression(testFile.filePath, True) Then
                    Return TestString("Compression1", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression1", File.GetAttributes(testFile.filePath), FileAttributes.Compressed)
            End Using
        End Function

        Function Test_Compression2(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression2.txt"))
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
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression3.txt"))
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFile.filePath)

                If File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression3", "File is compressed", "File is not compressed")
                End If

                If Not WalkmanLib.CompressFile(testFile.filePath) Then
                    Return TestString("Compression3", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression3", File.GetAttributes(testFile.filePath), FileAttributes.Compressed)
            End Using
        End Function

        Function Test_Compression4(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression4.txt"))
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

        Function Test_Compression5(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression5.txt"))
                Dim fileLength As Long = New FileInfo(testFile.filePath).Length
                If fileLength <> 0 Then
                    Return TestString("Compression5", "FileSize:" & fileLength, "FileSize:0")
                End If

                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.CompressFile(testFile.filePath)
                If Not File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression5", "File is not compressed", "File is compressed")
                End If

                Return TestNumber("Compression5", WalkmanLib.GetCompressedSize(testFile.filePath), 0)
            End Using
        End Function

        Function Test_Compression6(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression6.txt"))
                Dim fileLength As Long = New FileInfo(testFile.filePath).Length
                If fileLength <> 0 Then
                    Return TestString("Compression6", "FileSize:" & fileLength, "FileSize:0")
                End If

                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.CompressFile(testFile.filePath)
                If Not File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression6", "File is not compressed", "File is compressed")
                End If

                File.WriteAllText(testFile.filePath, String.Empty, Text.Encoding.UTF8)
                fileLength = New FileInfo(testFile.filePath).Length
                If fileLength <> 3 Then
                    Return TestString("Compression6", "FileSize:" & fileLength, "FileSize:3")
                End If

                ' currently GetCompressedSize returns the filesize for 0 compressed size...
                Return TestNumber("Compression6", WalkmanLib.GetCompressedSize(testFile.filePath), 3)
            End Using
        End Function

        Function Test_Compression7(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression7.txt"))
                Dim fileLength As Long = New FileInfo(testFile.filePath).Length
                If fileLength <> 0 Then
                    Return TestString("Compression7", "FileSize:" & fileLength, "FileSize:0")
                End If

                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFile.filePath)
                If File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression7", "File is compressed", "File is not compressed")
                End If

                File.WriteAllText(testFile.filePath, String.Empty, Text.Encoding.UTF8)
                fileLength = New FileInfo(testFile.filePath).Length
                If fileLength <> 3 Then
                    Return TestString("Compression7", "FileSize:" & fileLength, "FileSize:3")
                End If

                Return TestNumber("Compression7", WalkmanLib.GetCompressedSize(testFile.filePath), 3)
            End Using
        End Function

        Function Test_Compression8(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression8.txt"))
                Dim fileLength As Long = New FileInfo(testFile.filePath).Length
                If fileLength <> 0 Then
                    Return TestString("Compression8", "FileSize:" & fileLength, "FileSize:0")
                End If
                WalkmanLib.SetAttribute(testFile.filePath, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFile.filePath)
                If File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression8", "File is compressed", "File is not compressed")
                End If

                Dim newFileSize As Integer = 1024 * 64
                Dim randBytes(newFileSize) As Byte
                Dim rand As New Random()
                rand.NextBytes(randBytes)

                Dim randChars(newFileSize) As Char
                randBytes.CopyTo(randChars, 0)
                File.WriteAllText(testFile.filePath, randChars, Text.Encoding.ASCII)

                newFileSize += 1 ' seems to be an extra byte added somewhere
                fileLength = New FileInfo(testFile.filePath).Length
                If fileLength <> newFileSize Then
                    Return TestString("Compression8", "FileSize:" & fileLength, "FileSize:" & newFileSize)
                End If

                WalkmanLib.CompressFile(testFile.filePath)
                If Not File.GetAttributes(testFile.filePath).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression8", "File is not compressed", "File is compressed")
                End If

                Return TestNumber("Compression8", WalkmanLib.GetCompressedSize(testFile.filePath), 1024 * 60)
            End Using
        End Function

        Function Test_Compression9(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "compressionThrows1.txt")
            Return TestBoolean("Compression9", WalkmanLib.SetCompression(testPath, True), False)
        End Function

        Function Test_Compression10() As Boolean
            Dim testPath As String = Path.Combine(Environment.SystemDirectory, "shell32.dll")
            Return TestBoolean("Compression10", WalkmanLib.SetCompression(testPath, True), False)
        End Function
    End Module
End Namespace
