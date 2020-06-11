Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Tests_IsAdmin
        Function Test_IsAdmin1() As Boolean
            Return TestBoolean("IsAdmin1", WalkmanLib.IsAdmin(), False)
        End Function

        Function Test_IsAdmin2(rootTestFolder As String) As Boolean
            Dim programPath As String = Reflection.Assembly.GetExecutingAssembly().CodeBase
            programPath = New Uri(programPath).LocalPath
            Dim tmpOutPath As String = Path.Combine(rootTestFolder, "outTmp.txt")

            WalkmanLib.RunAsAdmin("cmd.exe", "/c """ & programPath & """ getAdmin > " & tmpOutPath)
            Threading.Thread.Sleep(1000)

            Dim runAsAdminOutput As String
            Try
                runAsAdminOutput = File.ReadAllText(tmpOutPath)
            Catch ex As Exception
                runAsAdminOutput = "Error: " & ex.Message
            End Try
            Try
                File.Delete(tmpOutPath)
            Catch
            End Try

            Return TestString("IsAdmin2", runAsAdminOutput, "True" & Microsoft.VisualBasic.vbNewLine)
        End Function
    End Module
End Namespace
