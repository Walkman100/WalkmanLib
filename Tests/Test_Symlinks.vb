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
                    Return TestString("symlinks1", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks2(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "symlinks2Source"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks2")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks2Source", SymbolicLinkType.File)

                Using testFile As New DisposableFile(symlinkPath, False)
                    Return TestString("symlinks2", WalkmanLib.GetSymlinkTarget(symlinkPath), testFileSource.filePath)
                End Using
            End Using
        End Function

        Function Test_Symlinks3(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "symlinks3Source"))
                Dim symlinkPath As String = Path.Combine(rootTestFolder, "symlinks3")

                WalkmanLib.CreateSymLink(symlinkPath, "symlinks3Source", SymbolicLinkType.Directory)

                Using testSymlink As New DisposableDirectory(symlinkPath, False)
                    Return TestString("symlinks3", WalkmanLib.GetSymlinkTarget(symlinkPath), testDirSource.dirPath)
                End Using
            End Using
        End Function
    End Module
End Namespace
