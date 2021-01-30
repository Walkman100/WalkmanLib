Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Security.Cryptography

Namespace Tests
    Public Module Tests_GetFileIcon
        Function Test_GetFileIcon1(rootTestFolder As String) As Boolean
            Dim ico As Icon = WalkmanLib.GetFileIcon(Environment.GetEnvironmentVariable("WinDir"))

            Using savedImage As New DisposableFile(Path.Combine(rootTestFolder, "getFileIcon1.png"), False, False)
                ico.ToBitmap().Save(savedImage)

                Return TestBoolean("GetFileIcon1", File.Exists(savedImage), True)
            End Using
        End Function

        Function Test_GetFileIcon2(rootTestFolder As String) As Boolean
            Dim ico As Icon = WalkmanLib.GetFileIcon(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "explorer.exe"))

            Using savedImage As New DisposableFile(Path.Combine(rootTestFolder, "getFileIcon2.png"), False, False)
                ico.ToBitmap().Save(savedImage)

                Return TestBoolean("GetFileIcon2", File.Exists(savedImage), True)
            End Using
        End Function

        Function Test_GetFileIcon3() As Boolean
            Dim ex As Exception = New NoException
            Try
                Dim ico As Icon = WalkmanLib.GetFileIcon("nonexistantfile.txt")
            Catch ex2 As Exception
                ex = ex2
            End Try

            Return TestType("GetFileIcon3", ex.GetType(), GetType(FileNotFoundException))
        End Function

        Function Test_GetFileIcon4() As Boolean
            Dim ex As Exception = New NoException
            Try
                Dim ico As Icon = WalkmanLib.GetFileIcon("nonexistantfile.txt", False)
            Catch ex2 As Exception
                ex = ex2
            End Try

            Return TestType("GetFileIcon4", ex.GetType(), GetType(NoException))
        End Function

        Private Function ImageIsEqual(bmp1 As Bitmap, bmp2 As Bitmap) As Boolean
            If bmp1.PixelFormat <> bmp2.PixelFormat Then Return False
            If Not bmp1.RawFormat.Equals(bmp2.RawFormat) Then Return False
            If bmp1.Size <> bmp2.Size Then Return False

            For x As Integer = 0 To bmp1.Width - 1
                For y As Integer = 0 To bmp1.Height - 1
                    If bmp1.GetPixel(x, y) <> bmp2.GetPixel(x, y) Then
                        Return False
                    End If
                Next
            Next

            Return True
        End Function

        Function Test_GetFileIcon5() As Boolean
            Dim ico1 As Icon = WalkmanLib.GetFileIcon(".txt", False)
            Dim ico2 As Icon = WalkmanLib.GetFileIcon(".txt", False)

            Return TestBoolean("GetFileIcon5", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), True)
        End Function

        Function Test_GetFileIcon6() As Boolean
            Dim ico1 As Icon = WalkmanLib.GetFileIcon(".txt", False, linkOverlay:=False)
            Dim ico2 As Icon = WalkmanLib.GetFileIcon(".txt", False, linkOverlay:=True)

            Return TestBoolean("GetFileIcon6", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), False)
        End Function

        Function Test_GetFileIcon7() As Boolean
            Dim filePath As String = Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "hh.exe")
            Dim ico1 As Icon = WalkmanLib.GetFileIcon(filePath)
            Dim ico2 As Icon = WalkmanLib.ExtractIconByIndex(filePath, 0, 16)

            Return TestBoolean("GetFileIcon7", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), True)
        End Function

        Function Test_GetFileIcon8() As Boolean
            Dim filePath As String = Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "hh.exe")
            Dim ico1 As Icon = WalkmanLib.GetFileIcon(filePath, smallIcon:=False)
            Dim ico2 As Icon = WalkmanLib.ExtractIconByIndex(filePath, 0, 32)

            Return TestBoolean("GetFileIcon8", ImageIsEqual(ico1.ToBitmap(), ico2.ToBitmap()), True)
        End Function

        Private Function Sha1Image(img As Image) As String
            Using ms As New MemoryStream()
                ' ImageFormat.MemoryBmp has no encoder, but it's still a BMP...
                img.Save(ms, If(img.RawFormat.Equals(ImageFormat.MemoryBmp), ImageFormat.Bmp, img.RawFormat))

                ms.Position = 0
                Dim hash As Byte() = New SHA1Managed().ComputeHash(ms)
                Return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant()
            End Using
        End Function

        Function Test_GetFileIcon9() As Boolean
            ' the icon for %WinDir%\hh.exe hasn't changed since XP, should be pretty safe
            Using ico As Icon = WalkmanLib.GetFileIcon(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "hh.exe")),
                    bmp As Bitmap = ico.ToBitmap()

                Dim hash As String = Sha1Image(bmp)

                Return TestString("GetFileIcon9", hash, "f805d57e484d7f00298fe32b072b8c57928b3edb")
            End Using
        End Function
    End Module
End Namespace
