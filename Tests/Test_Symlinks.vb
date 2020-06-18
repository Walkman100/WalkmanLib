Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

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

        Function Test_Symlinks2(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks2Source.txt"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks2.txt")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks2Source.txt", SymbolicLinkType.File)

                Using testFile As New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks2", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks3(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks3Source"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks3")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3Source", SymbolicLinkType.Directory)

                Using testSymlink As New DisposableDirectory(symlinkPath, False)
                    Return TestString("Symlinks3", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource.dirPath)
                End Using
            End Using
        End Function

        Function Test_Symlinks4(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.GetPathRoot(rootTestFolder)
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks4")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, SymbolicLinkType.Directory)

            Using testSymlink As New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks4", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource)
            End Using
        End Function

        Function Test_Symlinks5(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.Combine("..", Path.GetFileName(rootTestFolder))
            Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks5")

            WalkmanLib.CreateSymLink(symlinkPath, testDirSource, SymbolicLinkType.Directory)

            Using testSymlink As New DisposableDirectory(symlinkPath, False)
                Return TestString("Symlinks5", WalkmanLib.GetSymlinkTarget(symlinkPath), rootTestFolder)
            End Using
        End Function

        Function Test_Symlinks6(rootTestFolder As String) As Boolean
            Using testDirRoot As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks6Root")),
                  testFileSource As New DisposableFile(Path.Combine(testDirRoot.dirPath, "symlinks6Source.txt"))

                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks6.txt")
                Dim symlinkTarget As String = Path.Combine(Path.GetFileName(testDirRoot.dirPath), Path.GetFileName(testFileSource.filePath))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, SymbolicLinkType.File)

                Using testSymlink As New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks6", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks7(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7Source")),
                  testFileSource As New DisposableFile(Path.Combine(testDirSource.dirPath, "symlinks7Source.txt")),
                  testDirTarget As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks7Target"))

                Dim symlinkPath As String = Path.Combine(testDirTarget.dirPath, "symlinks7.txt")
                Dim symlinkTarget As String = Path.Combine("..", Path.GetFileName(testDirSource.dirPath), Path.GetFileName(testFileSource.filePath))

                WalkmanLib.CreateSymLink(symlinkPath, symlinkTarget, SymbolicLinkType.File)

                Using testSymlink As New DisposableFile(symlinkPath, False)
                    Return TestString("Symlinks7", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function
    End Module
End Namespace
