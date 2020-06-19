Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Test_Hardlinks
        Function Test_Hardlinks1(rootTestFolder As String) As Boolean
            Using testFileSource As New DisposableFile(Path.Combine(rootTestFolder, "hardlinks1Source.txt"))
                File.WriteAllText(testFileSource.filePath, "testText")
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
                File.WriteAllText(testFileSource.filePath, "testText")
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks2.txt")

                WalkmanLib.CreateHardLink(hardlinkPath, testFileSource.filePath)

                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks2", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function

        Function Test_Hardlinks3(rootTestFolder As String) As Boolean
            Using testDirRoot As New DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks3Root")),
                  testFileSource As New DisposableFile(Path.Combine(testDirRoot.dirPath, "hardlinks3Source.txt"))

                File.WriteAllText(testFileSource.filePath, "testText")
                Dim hardlinkPath As String = Path.Combine(rootTestFolder, "hardlinks3.txt")
                Dim hardlinkTarget As String = Path.Combine(Path.GetFileName(testDirRoot.dirPath), Path.GetFileName(testFileSource.filePath))
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

                File.WriteAllText(testFileSource.filePath, "testText")
                Dim hardlinkPath As String = Path.Combine(testDirTarget.dirPath, "hardlinks4.txt")
                Dim hardlinkTarget As String = Path.Combine("..", Path.GetFileName(testFileSource.filePath))
                Environment.CurrentDirectory = testDirTarget.dirPath

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget)

                Environment.CurrentDirectory = rootTestFolder ' allow testDirTarget to be deleted
                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks4", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function

        Function Test_Hardlinks5(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks5Source")),
                  testFileSource As New DisposableFile(Path.Combine(testDirSource.dirPath, "hardlinks5Source.txt")),
                  testDirTarget As New DisposableDirectory(Path.Combine(rootTestFolder, "hardlinks5Target"))

                File.WriteAllText(testFileSource.filePath, "testText")
                Dim hardlinkPath As String = Path.Combine(testDirTarget.dirPath, "hardlinks5.txt")
                Dim hardlinkTarget As String = Path.Combine("..", Path.GetFileName(testDirSource.dirPath), Path.GetFileName(testFileSource.filePath))
                Environment.CurrentDirectory = testDirTarget.dirPath

                WalkmanLib.CreateHardLink(hardlinkPath, hardlinkTarget)

                Environment.CurrentDirectory = rootTestFolder ' allow testDirTarget to be deleted
                Using testHardlink As New DisposableFile(hardlinkPath, False)
                    Return TestString("Hardlinks5", File.ReadAllText(hardlinkPath), "testText")
                End Using
            End Using
        End Function
    End Module
End Namespace
