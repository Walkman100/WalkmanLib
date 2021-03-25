Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_Junctions
        Function Test_Junctions1(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "junctions1Source"))
                Dim junctionPath As String = Path.Combine(rootTestFolder, "junctions1")

                Dim mklinkOutput As String = WalkmanLib.RunAndGetOutput("cmd", arguments:="/c mklink /J " & junctionPath & " junctions1Source", workingDirectory:=rootTestFolder)

                Using New DisposableDirectory(junctionPath, False)
                    Return TestString("Junctions1", WalkmanLib.GetSymlinkTarget(junctionPath), testDirSource)
                End Using
            End Using
        End Function

        Function Test_Junctions2(rootTestFolder As String) As Boolean
            Using testDirSource As New DisposableDirectory(Path.Combine(rootTestFolder, "junctions2Source"))
                Dim junctionPath As String = Path.Combine(rootTestFolder, "junctions2")

                WalkmanLib.CreateJunction(junctionPath, testDirSource)

                Using New DisposableDirectory(junctionPath, False)
                    Return TestString("Junctions2", WalkmanLib.GetSymlinkTarget(junctionPath), testDirSource)
                End Using
            End Using
        End Function

        Function Test_Junctions3(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.GetPathRoot(rootTestFolder)
            Dim junctionPath As String = Path.Combine(rootTestFolder, "junctions3")

            WalkmanLib.CreateJunction(junctionPath, testDirSource)

            Using New DisposableDirectory(junctionPath, False)
                Return TestString("Junctions3", WalkmanLib.GetSymlinkTarget(junctionPath), testDirSource)
            End Using
        End Function

        Function Test_Junctions4(rootTestFolder As String) As Boolean
            Dim testDirSource As String = Path.Combine("..", Path.GetFileName(rootTestFolder))
            Dim junctionPath As String = Path.Combine(rootTestFolder, "junctions4")
            Environment.CurrentDirectory = rootTestFolder

            WalkmanLib.CreateJunction(junctionPath, testDirSource)

            Using New DisposableDirectory(junctionPath, False)
                Return TestString("Junctions4", WalkmanLib.GetSymlinkTarget(junctionPath), rootTestFolder)
            End Using
        End Function

        Function Test_Junctions5(rootTestFolder As String) As Boolean
            Using testDir As New DisposableDirectory(Path.Combine(rootTestFolder, "junctions5Source")),
                  parentDir As New DisposableDirectory(Path.Combine(rootTestFolder, "nonExistantDirectory"), False),
                  junctionPath As New DisposableDirectory(Path.Combine(parentDir, "junction5Target"), False)

                WalkmanLib.CreateJunction(junctionPath, testDir)

                Return TestString("Junctions5", WalkmanLib.GetSymlinkTarget(junctionPath), testDir)
            End Using
        End Function

        Function Test_JunctionThrows1(rootTestFolder As String) As Boolean
            Using junctionPath As New DisposableDirectory(Path.Combine(rootTestFolder, "junctionThrows1")),
                  testDir As New DisposableDirectory(Path.Combine(rootTestFolder, "junctionThrows1Source"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateJunction(junctionPath, testDir)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("JunctionThrows1", ex.GetType, GetType(IOException))
            End Using
        End Function

        Function Test_JunctionThrows2(rootTestFolder As String) As Boolean
            Using testDir As New DisposableDirectory(Path.Combine(rootTestFolder, "junctionThrows2"))
                Dim ex As Exception = New NoException
                Try
                    WalkmanLib.CreateJunction(Path.Combine(Environment.SystemDirectory, "junctionThrows2"), testDir)
                Catch ex2 As Exception
                    ex = ex2
                End Try
                Return TestType("JunctionThrows2", ex.GetType, GetType(UnauthorizedAccessException))
            End Using
        End Function
    End Module
End Namespace
