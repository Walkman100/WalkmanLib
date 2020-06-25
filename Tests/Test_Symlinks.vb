Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.ComponentModel
Imports System.IO

Namespace Tests
    Module Tests_Symlinks
        Function Test_Symlinks1(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks1Source"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks1")

                Dim mklinkOutput As String = WalkmanLib.RunAndGetOutput("cmd", "/c mklink " & symlinkPath & " symlinks1Source", rootTestFolder)

                Using testFile As New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks1", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks2() As Boolean
            Dim symlinkPath As String = Path.Combine(Environment.GetEnvironmentVariable("ProgramData"), "Documents")
            Return TestString("Symlinks2", WalkmanLib.GetSymlinkTarget(symlinkPath), Path.Combine(Environment.GetEnvironmentVariable("PUBLIC"), "Documents"))
        End Function

        Function Test_Symlinks3(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks3Source.txt"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks3.txt")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3Source.txt", SymbolicLinkType.File)

                Using testFile As New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks3", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks4(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks4Source"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks4")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks4Source", SymbolicLinkType.Directory)

                Using testSymlink As New DisposableDirectory(symlinkPath, False)
                    Return TestString("Symlinks4", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource.dirPath)
                End Using
            End Using
        End Function

        Function Test_Symlinks5(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.GetPathRoot(rootTestFolder)
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks5")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, SymbolicLinkType.Directory)

            Using testSymlink As New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks5", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource)
            End Using
        End Function

        Function Test_Symlinks6(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.Combine("..", Path.GetFileName(rootTestFolder))
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks6")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, SymbolicLinkType.Directory)

            Using testSymlink As New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks6", WalkmanLib.GetSymlinkTarget(symlinkPath), rootTestFolder)
            End Using
        End Function

        Function Test_Symlinks7(rootTestFolder As String) As Boolean
            Using testDirRoot As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7Root")),
                  testFileSource As New DisposableFile(Path.Combine(testDirRoot.dirPath, "symlinks7Source.txt"))

                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks7.txt")
                Dim symlinkTarget As String = Path.Combine(Path.GetFileName(testDirRoot.dirPath), Path.GetFileName(testFileSource.filePath))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, SymbolicLinkType.File)

                Using testSymlink As New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks7", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks8(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8Source")),
                  testFileSource As New DisposableFile(Path.Combine(testDirSource.dirPath, "symlinks8Source.txt")),
                  testDirTarget As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks8Target"))

                Dim symlinkPath As String = Path.Combine(testDirTarget.dirPath, "symlinks8.txt")
                Dim symlinkTarget As String = Path.Combine("..", Path.GetFileName(testDirSource.dirPath), Path.GetFileName(testFileSource.filePath))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, SymbolicLinkType.File)

                Using testSymlink As New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks8", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks9() As Boolean
            Dim testPath As String = Environment.GetEnvironmentVariable("WinDir").ToLower()
            Return TestString("Symlinks9", WalkmanLib.GetSymlinkTarget(testPath).ToLower(), testPath)
        End Function

        Function Test_Symlinks10() As Boolean
            Dim testPath As String = Path.Combine(Environment.SystemDirectory, "shell32.dll").ToLower()
            Return TestString("Symlinks10", WalkmanLib.GetSymlinkTarget(testPath).ToLower(), testPath)
        End Function

        Function Test_SymlinkThrows1() As Boolean
            Try
                WalkmanLib.GetSymlinkTarget("nonExistantFile")
                Return TestType("SymlinkThrows1", GetType(NoException), GetType(Win32Exception))
            Catch ex As Win32Exception ' expected. none of the other Test* should run.
                Return TestNumber("SymlinkThrows1", ex.NativeErrorCode, 2) '0x2: ERROR_FILE_NOT_FOUND: The system cannot find the file specified.
            Catch ex As Exception
                Return TestType("SymlinkThrows1", ex.GetType, GetType(Win32Exception))
            End Try
        End Function

        Function Test_SymlinkThrows2(rootTestFolder As String) As Boolean
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinkThrows2.txt")
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinkThrows2Source.txt"))
                WalkmanLib.CreateSymLink(symlinkPath, testFileSource.filePath, SymbolicLinkType.File)
            End Using

            If Not File.Exists(symlinkPath) Then
                Return TestString("SymlinkThrows2", "Test symlink doesn't exist", "Test symlink exists")
            End If

            Try
                WalkmanLib.GetSymlinkTarget(symlinkPath)
                Return TestType("SymlinkThrows2", GetType(NoException), GetType(Win32Exception))
            Catch ex As Win32Exception
                Return TestNumber("SymlinkThrows2", ex.NativeErrorCode, 2) '0x2: ERROR_FILE_NOT_FOUND: The system cannot find the file specified.
            Catch ex As Exception
                Return TestType("SymlinkThrows2", ex.GetType, GetType(Win32Exception))
            Finally
                File.Delete(symlinkPath)
            End Try
        End Function
    End Module
End Namespace
