Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq

Namespace Tests
    Module Tests_Hardlinks
        Function Test_Hardlinks1(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks1Source.txt"))
                File.WriteAllText(testFileSource, "testText")
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks1.txt")
                Environment.CurrentDirectory = rootTestFolder

                WalkmanLib.CreateHardLink(hardlinkPath, "hardlinks1Source.txt")

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks1", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function

        Function Test_Hardlinks2(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks2Source.txt"))
                File.WriteAllText(testFileSource, "testText")
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks2.txt")

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks2", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function

        Function Test_Hardlinks3(rootTestFolder As String) As Boolean
            Using testDirRoot As New DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks3Root")),
                  testFileSource As New DisposableFile(Path.Combine(testDirRoot, "hardlinks3Source.txt"))

                File.WriteAllText(testFileSource, "testText")
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks3.txt")
                Dim hardlinkTarget As String = Path.Combine(Path.GetFileName(testDirRoot), Path.GetFileName(testFileSource))
                Environment.CurrentDirectory = rootTestFolder

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks3", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function

        Function Test_Hardlinks4(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks4Source.txt")),
                  testDirTarget As New DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks4Target"))

                File.WriteAllText(testFileSource, "testText")
                Dim hardlinkPath As String = Path.Combine(testDirTarget, "hardlinks4.txt")
                Dim hardlinkTarget As String = Path.Combine("..", Path.GetFileName(testFileSource))
                Environment.CurrentDirectory = testDirTarget

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget)

                Environment.CurrentDirectory = rootTestFolder ' allow testDirTarget to be deleted
                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks4", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function

        Function Test_Hardlinks5(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks5Source")),
                  testFileSource As New DisposableFile(Path.Combine(testDirSource, "hardlinks5Source.txt")),
                  testDirTarget As New DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks5Target"))

                File.WriteAllText(testFileSource, "testText")
                Dim hardlinkPath As String = Path.Combine(testDirTarget, "hardlinks5.txt")
                Dim hardlinkTarget As String = Path.Combine("..", Path.GetFileName(testDirSource), Path.GetFileName(testFileSource))
                Environment.CurrentDirectory = testDirTarget

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget)

                Environment.CurrentDirectory = rootTestFolder ' allow testDirTarget to be deleted
                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks5", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function

        Function Test_HardlinkThrows1(rootTestFolder As String) As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.CreateHardLink(Path.Combine(rootTestFolder, "hardlinkThrows1.txt"), "nonExistantFile.txt")
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows1", ex.GetType, GetType(FileNotFoundException))
        End Function

        Function Test_HardlinkThrows2(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows2Source.txt"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateHardLink(Path.Combine(rootTestFolder, "nonExistantFolder", "hardlinkThrows2.txt"), testFile)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("HardlinkThrows2", ex.GetType, GetType(DirectoryNotFoundException))
            End Using
        End Function

        Function Test_HardlinkThrows3(rootTestFolder As String) As Boolean
            Using testSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows3Source.txt")),
                  testFile As New DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows3.txt"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateHardLink(testFile, testSource)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("HardlinkThrows3", ex.GetType, GetType(IOException))
            End Using
        End Function

        Function Test_HardlinkThrows4(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "hardlinkThrows4Source.txt"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateHardLink(Path.Combine(Environment.SystemDirectory, "symlinkThrows3.txt"), testFile)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("HardlinkThrows4", ex.GetType, GetType(UnauthorizedAccessException))
            End Using
        End Function

        Function Test_Hardlink6(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks6Source.txt"))
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks6.txt")

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestNumber("Hardlinks6", WalkmanLib.GetHardlinkCount(hardlinkPath), 2)
                End Using
            End Using
        End Function

        Function Test_Hardlink7(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks7Source.txt"))
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks7.txt")

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestNumber("Hardlinks7", WalkmanLib.GetHardlinkCount(testFileSource), 2)
                End Using
            End Using
        End Function

        Function Test_Hardlink8(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks8Source.txt"))
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks8.txt")

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Dim lstLinks As List(Of String) = WalkmanLib.GetHardlinkLinks(hardlinkPath).ToList

                    Return TestNumber("Hardlinks8", lstLinks.Count, 2)
                End Using
            End Using
        End Function

        Function Test_Hardlink9(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks9Source.txt"))
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks9.txt")

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Dim lstLinks As List(Of String) = WalkmanLib.GetHardlinkLinks(hardlinkPath).ToList
                    Dim linkPath As String = lstLinks.First(Function(x As String)
                                                                Return x.EndsWith("Source.txt")
                                                            End Function)

                    Return TestString("Hardlinks9", linkPath, testFileSource)
                End Using
            End Using
        End Function

        Function Test_Hardlink10(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks10Source.txt"))
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks10.txt")

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Dim lstLinks As List(Of String) = WalkmanLib.GetHardlinkLinks(testFileSource).ToList
                    Dim linkPath As String = lstLinks.First(Function(x As String)
                                                                Return x.EndsWith("Source.txt")
                                                            End Function)

                    Return TestString("Hardlinks10", linkPath, testFileSource)
                End Using
            End Using
        End Function

        Function Test_HardlinkThrows5(rootTestFolder As String) As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkCount(Path.Combine(rootTestFolder, "nonExistantFile.txt"))
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows5", ex.GetType, GetType(FileNotFoundException))
        End Function

        Function Test_HardlinkThrows6(rootTestFolder As String) As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkCount(Path.Combine(rootTestFolder, "nonExistantFolder", "nonExistantFile.txt"))
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows6", ex.GetType, GetType(DirectoryNotFoundException))
        End Function

        Function Test_HardlinkThrows7() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkCount(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "MEMORY.DMP"))
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows7", ex.GetType, GetType(UnauthorizedAccessException))
        End Function

        Function Test_HardlinkThrows8() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkCount(Path.Combine(Environment.GetEnvironmentVariable("SystemDrive") & Path.DirectorySeparatorChar, "pagefile.sys"))
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows8", ex.GetType, GetType(IOException))
        End Function

        Function Test_HardlinkThrows9(rootTestFolder As String) As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkLinks(Path.Combine(rootTestFolder, "nonExistantFile.txt")).ToList
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows9", ex.GetType, GetType(FileNotFoundException))
        End Function

        Function Test_HardlinkThrows10(rootTestFolder As String) As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkLinks(Path.Combine(rootTestFolder, "nonExistantFolder", "nonExistantFile.txt")).ToList
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows10", ex.GetType, GetType(DirectoryNotFoundException))
        End Function

        Function Test_HardlinkThrows11() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkLinks(Path.Combine(Environment.GetEnvironmentVariable("WinDir"), "MEMORY.DMP")).ToList
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows11", ex.GetType, GetType(UnauthorizedAccessException))
        End Function

        Function Test_HardlinkThrows12() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetHardlinkLinks(Path.Combine(Environment.GetEnvironmentVariable("SystemDrive") & Path.DirectorySeparatorChar, "pagefile.sys")).ToList
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("HardlinkThrows12", ex.GetType, GetType(IOException))
        End Function
    End Module
End Namespace
