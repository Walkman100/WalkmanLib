Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_Symlinks
        Function Test_Symlinks1_Final(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks1fSource"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks1f")

                Dim mklinkOutput As String = WalkmanLib.RunAndGetOutput("cmd", arguments:="/c mklink " & symlinkPath & " symlinks1fSource", workingDirectory:=rootTestFolder)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks1_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource)
                End Using
            End Using
        End Function
        Function Test_Symlinks1_Target(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks1tSource"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks1t")

                Dim mklinkOutput As String = WalkmanLib.RunAndGetOutput("cmd", arguments:="/c mklink " & symlinkPath & " symlinks1tSource", workingDirectory:=rootTestFolder)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks1_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), "symlinks1tSource")
                End Using
            End Using
        End Function

        Function Test_Symlinks2_Final() As Boolean
            Dim symlinkPath As String = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Documents")
            Return TestString("Symlinks2_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), "Documents"))
        End Function
        Function Test_Symlinks2_Target() As Boolean
            Dim symlinkPath As String = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Documents")
            Return TestString("Symlinks2_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), "Documents"))
        End Function

        Function Test_Symlinks3_Final(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks3fSource.txt"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks3f.txt")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3fSource.txt", False)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks3_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource)
                End Using
            End Using
        End Function
        Function Test_Symlinks3_Target(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks3tSource.txt"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks3t.txt")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3tSource.txt", False)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks3_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), "symlinks3tSource.txt")
                End Using
            End Using
        End Function

        Function Test_Symlinks4_Final(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks4fSource"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks4f")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks4fSource", True)

                Using New DisposableDirectory(symlinkPath, False)
                    Return TestString("Symlinks4_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testDirSource)
                End Using
            End Using
        End Function
        Function Test_Symlinks4_Target(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks4tSource"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks4t")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks4tSource", True)

                Using New DisposableDirectory(symlinkPath, False)
                    Return TestString("Symlinks4_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), "symlinks4tSource")
                End Using
            End Using
        End Function

        Function Test_Symlinks5_Final(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.GetPathRoot(rootTestFolder)
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks5f")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, True)

            Using New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks5_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testDirSource)
            End Using
        End Function
        Function Test_Symlinks5_Target(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.GetPathRoot(rootTestFolder)
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks5t")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, True)

            Using New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks5_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource)
            End Using
        End Function

        Function Test_Symlinks6_Final(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.Combine("..", Path.GetFileName(rootTestFolder))
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks6f")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, True)

            Using New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks6_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), rootTestFolder)
            End Using
        End Function
        Function Test_Symlinks6_Target(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.Combine("..", Path.GetFileName(rootTestFolder))
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks6t")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, True)

            Using New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks6_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource)
            End Using
        End Function

        Function Test_Symlinks7_Final(rootTestFolder As String) As Boolean
            Using testDirRoot As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7fRoot")),
                  testFileSource As New DisposableFile(Path.Combine(testDirRoot, "symlinks7fSource.txt"))

                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks7f.txt")
                Dim symlinkTarget As String = Path.Combine(Path.GetFileName(testDirRoot), Path.GetFileName(testFileSource))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, False)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks7_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource)
                End Using
            End Using
        End Function
        Function Test_Symlinks7_Target(rootTestFolder As String) As Boolean
            Using testDirRoot As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7tRoot")),
                  testFileSource As New DisposableFile(Path.Combine(testDirRoot, "symlinks7tSource.txt"))

                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks7t.txt")
                Dim symlinkTarget As String = Path.Combine(Path.GetFileName(testDirRoot), Path.GetFileName(testFileSource))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, False)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks7_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), symlinkTarget)
                End Using
            End Using
        End Function

        Function Test_Symlinks8_Final(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8fSource")),
                  testFileSource As New DisposableFile(Path.Combine(testDirSource, "symlinks8fSource.txt")),
                  testDirTarget As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8fTarget"))

                Dim symlinkPath As String = Path.Combine(testDirTarget, "symlinks8f.txt")
                Dim symlinkTarget As String = Path.Combine("..", Path.GetFileName(testDirSource), Path.GetFileName(testFileSource))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, False)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks8_Final", WalkmanLib.GetSymlinkFinalPath(symlinkPath), testFileSource)
                End Using
            End Using
        End Function
        Function Test_Symlinks8_Target(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8tSource")),
                  testFileSource As New DisposableFile(Path.Combine(testDirSource, "symlinks8tSource.txt")),
                  testDirTarget As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8tTarget"))

                Dim symlinkPath As String = Path.Combine(testDirTarget, "symlinks8t.txt")
                Dim symlinkTarget As String = Path.Combine("..", Path.GetFileName(testDirSource), Path.GetFileName(testFileSource))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, False)

                Using New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks8_Target", WalkmanLib.GetSymlinkTarget(symlinkPath), symlinkTarget)
                End Using
            End Using
        End Function

        Function Test_Symlinks9() As Boolean
            Dim testPath As String = Environment.GetEnvironmentVariable("WinDir").ToLower()
            Return TestString("Symlinks9", WalkmanLib.GetSymlinkFinalPath(testPath).ToLower(), testPath)
        End Function

        Function Test_Symlinks10() As Boolean
            Dim testPath As String = Path.Combine(Environment.SystemDirectory, "shell32.dll").ToLower()
            Return TestString("Symlinks10", WalkmanLib.GetSymlinkFinalPath(testPath).ToLower(), testPath)
        End Function

        Function Test_Symlinks11(rootTestFolder As String) As Boolean
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks11.txt")
            Dim symlinkTarget As String = Path.Combine(rootTestFolder, "symlinks11Source.txt")
            Using testFileSource As New DisposableFile(symlinkTarget)
                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, False)
            End Using

            Using New DisposableFile(symlinkPath, False)
                Return TestString("Symlinks11", WalkmanLib.GetSymlinkTarget(symlinkPath), symlinkTarget)
            End Using
        End Function

        Function Test_SymlinkThrows1(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows1Source.txt"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateSymLink(Path.Combine(rootTestFolder, "nonExistantFolder", "symlinkThrows1Source.txt"), testFile, False)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("SymlinkThrows1", ex.GetType, GetType(DirectoryNotFoundException))
            End Using
        End Function

        Function Test_SymlinkThrows2(rootTestFolder As String) As Boolean
            Using symlinkPath As New DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows2.txt")),
                  testFile As New DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows2Source.txt"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateSymLink(symlinkPath, testFile, False)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("SymlinkThrows2", ex.GetType, GetType(IOException))
            End Using
        End Function

        Function Test_SymlinkThrows3(rootTestFolder As String) As Boolean
            Using testFile As New DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows3.txt"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateSymLink(Path.Combine(Environment.SystemDirectory, "symlinkThrows3.txt"), testFile, False)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("SymlinkThrows3", ex.GetType, GetType(UnauthorizedAccessException))
            End Using
        End Function

        Function Test_SymlinkThrows4() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetSymlinkFinalPath("nonExistantFile")
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("SymlinkThrows4", ex.GetType(), GetType(FileNotFoundException))
        End Function

        Function Test_SymlinkThrows5() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetSymlinkTarget("nonExistantFile")
            Catch ex2 As Exception
                ex = ex2
            End Try
            Return TestType("SymlinkThrows5", ex.GetType(), GetType(FileNotFoundException))
        End Function

        Function Test_SymlinkThrows6() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetSymlinkTarget(Environment.GetEnvironmentVariable("WinDir"))
            Catch ex2 As Exception
                ex = ex2
            End Try

            If Not TypeOf ex Is ComponentModel.Win32Exception Then Return TestType("SymlinkThrows6", ex.GetType(), GetType(ComponentModel.Win32Exception))
            Return TestNumber("SymlinkThrows6", DirectCast(ex, ComponentModel.Win32Exception).NativeErrorCode, WalkmanLib.NativeErrorCode.ERROR_NOT_A_REPARSE_POINT)
        End Function

        Function Test_SymlinkThrows7() As Boolean
            Dim ex As Exception = New NoException
            Try
                WalkmanLib.GetSymlinkTarget(Path.Combine(Environment.SystemDirectory, "shell32.dll"))
            Catch ex2 As Exception
                ex = ex2
            End Try

            If Not TypeOf ex Is ComponentModel.Win32Exception Then Return TestType("SymlinkThrows7", ex.GetType(), GetType(ComponentModel.Win32Exception))
            Return TestNumber("SymlinkThrows7", DirectCast(ex, ComponentModel.Win32Exception).NativeErrorCode, WalkmanLib.NativeErrorCode.ERROR_NOT_A_REPARSE_POINT)
        End Function

        Function Test_SymlinkThrows8(rootTestFolder As String) As Boolean
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinkThrows8.txt")
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows8Source.txt"))
                WalkmanLib.CreateSymLink(symlinkPath, testFileSource, False)
            End Using

            Using New DisposableFile(symlinkPath, False)
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.GetSymlinkFinalPath(symlinkPath)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("SymlinkThrows8", ex.GetType, GetType(FileNotFoundException))
            End Using
        End Function
    End Module
End Namespace
