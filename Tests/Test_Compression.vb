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
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.SetCompression(testFile, False)

                If File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression1", "File is compressed", "File is not compressed")
                End If

                If Not WalkmanLib.SetCompression(testFile, True) Then
                    Return TestString("Compression1", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression1", File.GetAttributes(testFile), FileAttributes.Compressed)
            End Using
        End Function

        Function Test_Compression2(rootTestFolder As String) As Boolean
            Using testFolder As New DisposableDirectory(Path.Combine(rootTestFolder, "compression2"))
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal)
                WalkmanLib.SetCompression(testFolder, False)

                If File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression2", "Folder is compressed", "Folder is not compressed")
                End If

                If Not WalkmanLib.SetCompression(testFolder, True) Then
                    Return TestString("Compression2", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression2", File.GetAttributes(testFolder), FileAttributes.Compressed Or FileAttributes.Directory)
            End Using
        End Function

        Function Test_Compression3(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression3.txt"))
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.SetCompression(testFile, True)

                If Not File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression3", "File is not compressed", "File is compressed")
                End If

                If Not WalkmanLib.SetCompression(testFile, False) Then
                    Return TestString("Compression3", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression3", File.GetAttributes(testFile), FileAttributes.Normal)
            End Using
        End Function

        Function Test_Compression4(rootTestFolder As String) As Boolean
            Using testFolder As New DisposableDirectory(Path.Combine(rootTestFolder, "compression4"))
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal)
                WalkmanLib.SetCompression(testFolder, True)

                If Not File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression4", "Folder is not compressed", "Folder is compressed")
                End If

                If Not WalkmanLib.SetCompression(testFolder, False) Then
                    Return TestString("Compression4", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression4", File.GetAttributes(testFolder), FileAttributes.Directory)
            End Using
        End Function

        Function Test_Compression5(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression5.txt"))
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFile)

                If File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression5", "File is compressed", "File is not compressed")
                End If

                If Not WalkmanLib.CompressFile(testFile) Then
                    Return TestString("Compression5", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression5", File.GetAttributes(testFile), FileAttributes.Compressed)
            End Using
        End Function

        Function Test_Compression6(rootTestFolder As String) As Boolean
            Using testFolder As New DisposableDirectory(Path.Combine(rootTestFolder, "compression6"))
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFolder)

                If File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression6", "Folder is compressed", "Folder is not compressed")
                End If

                If Not WalkmanLib.CompressFile(testFolder) Then
                    Return TestString("Compression6", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression6", File.GetAttributes(testFolder), FileAttributes.Compressed Or FileAttributes.Directory)
            End Using
        End Function

        Function Test_Compression7(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression7.txt"))
                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.CompressFile(testFile)

                If Not File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression7", "File is not compressed", "File is compressed")
                End If

                If Not WalkmanLib.UncompressFile(testFile) Then
                    Return TestString("Compression7", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression7", File.GetAttributes(testFile), FileAttributes.Normal)
            End Using
        End Function

        Function Test_Compression8(rootTestFolder As String) As Boolean
            Using testFolder As New DisposableDirectory(Path.Combine(rootTestFolder, "compression8"))
                WalkmanLib.SetAttribute(testFolder, FileAttributes.Normal)
                WalkmanLib.CompressFile(testFolder)

                If Not File.GetAttributes(testFolder).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression8", "Folder is not compressed", "Folder is compressed")
                End If

                If Not WalkmanLib.UncompressFile(testFolder, False) Then
                    Return TestString("Compression8", "Return value: False", "Return value: True")
                End If

                Return TestNumber("Compression8", File.GetAttributes(testFolder), FileAttributes.Directory)
            End Using
        End Function

        Function Test_Compression9(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression9.txt"))
                Dim fileLength As Long = New FileInfo(testFile).Length
                If fileLength <> 0 Then
                    Return TestString("Compression9", "FileSize:" & fileLength, "FileSize:0")
                End If

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.CompressFile(testFile)
                If Not File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression9", "File is not compressed", "File is compressed")
                End If

                Return TestNumber("Compression9", WalkmanLib.GetCompressedSize(testFile), 0)
            End Using
        End Function

        Function Test_Compression10(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression10.txt"))
                Dim fileLength As Long = New FileInfo(testFile).Length
                If fileLength <> 0 Then
                    Return TestString("Compression10", "FileSize:" & fileLength, "FileSize:0")
                End If

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.CompressFile(testFile)
                If Not File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression10", "File is not compressed", "File is compressed")
                End If

                File.WriteAllText(testFile, String.Empty, Text.Encoding.UTF8)
                fileLength = New FileInfo(testFile).Length
                If fileLength <> 3 Then
                    Return TestString("Compression10", "FileSize:" & fileLength, "FileSize:3")
                End If

                ' currently GetCompressedSize returns the filesize for 0 compressed size...
                Return TestNumber("Compression10", WalkmanLib.GetCompressedSize(testFile), 3)
            End Using
        End Function

        Function Test_Compression11(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression11.txt"))
                Dim fileLength As Long = New FileInfo(testFile).Length
                If fileLength <> 0 Then
                    Return TestString("Compression11", "FileSize:" & fileLength, "FileSize:0")
                End If

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFile)
                If File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression11", "File is compressed", "File is not compressed")
                End If

                File.WriteAllText(testFile, String.Empty, Text.Encoding.UTF8)
                fileLength = New FileInfo(testFile).Length
                If fileLength <> 3 Then
                    Return TestString("Compression11", "FileSize:" & fileLength, "FileSize:3")
                End If

                Return TestNumber("Compression11", WalkmanLib.GetCompressedSize(testFile), 3)
            End Using
        End Function

        Function Test_Compression12(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "compression12.txt"))
                Dim fileLength As Long = New FileInfo(testFile).Length
                If fileLength <> 0 Then
                    Return TestString("Compression12", "FileSize:" & fileLength, "FileSize:0")
                End If

                WalkmanLib.SetAttribute(testFile, FileAttributes.Normal)
                WalkmanLib.UncompressFile(testFile)
                If File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression12", "File is compressed", "File is not compressed")
                End If

                Dim newFileSize As Integer = 1024 * 64
                Dim randBytes(newFileSize) As Byte
                Dim rand As New Random()
                rand.NextBytes(randBytes)

                Dim randChars(newFileSize) As Char
                randBytes.CopyTo(randChars, 0)
                File.WriteAllText(testFile, randChars, Text.Encoding.ASCII)

                newFileSize += 1 ' seems to be an extra byte added somewhere
                fileLength = New FileInfo(testFile).Length
                If fileLength <> newFileSize Then
                    Return TestString("Compression12", "FileSize:" & fileLength, "FileSize:" & newFileSize)
                End If

                WalkmanLib.CompressFile(testFile)
                If Not File.GetAttributes(testFile).HasFlag(FileAttributes.Compressed) Then
                    Return TestString("Compression12", "File is not compressed", "File is compressed")
                End If

                Return TestNumber("Compression12", WalkmanLib.GetCompressedSize(testFile), 1024 * 60)
            End Using
        End Function

        Function Test_Compression13(rootTestFolder As String) As Boolean
            Dim testPath As String = Path.Combine(rootTestFolder, "compression13.txt")
            Return TestBoolean("Compression13", WalkmanLib.SetCompression(testPath, True), False)
        End Function

        Function Test_Compression14() As Boolean
            Dim testPath As String = Path.Combine(Environment.SystemDirectory, "shell32.dll")
            Return TestBoolean("Compression14", WalkmanLib.SetCompression(testPath, True), False)
        End Function
    End Module
End Namespace
