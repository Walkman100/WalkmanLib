Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.IO

Namespace Tests
    Module Main
        Function RunAllTests() As Boolean
            Dim returnVal As Boolean = True

            Dim rootTestFolder As String = Path.Combine(Directory.GetCurrentDirectory, "tests")
            Directory.CreateDirectory(rootTestFolder)
            If Not TestBoolean("CreateDirectory", Directory.Exists(rootTestFolder), True) Then returnVal = False

            If Not Test_GetFolderIconPath1(rootTestFolder) Then returnVal = False
            If Not Test_GetFolderIconPath2(rootTestFolder) Then returnVal = False
            If Not Test_GetFolderIconPath3(rootTestFolder) Then returnVal = False
            If Not Test_GetFolderIconPath4(rootTestFolder) Then returnVal = False
            If Not Test_GetFolderIconPath5(rootTestFolder) Then returnVal = False
            If Not Test_RunAndGetOutput1() Then returnVal = False
            If Not Test_RunAndGetOutput2() Then returnVal = False
            If Not Test_RunAndGetOutput3() Then returnVal = False
            If Not Test_RunAndGetOutput4() Then returnVal = False
            If Not Test_RunAndGetOutput5() Then returnVal = False
            If Not Test_RunAndGetOutput6() Then returnVal = False
            If Not Test_IsAdmin1() Then returnVal = False
            If Not Test_IsAdmin2(rootTestFolder) Then returnVal = False
            If Not Test_IsFileOrDirectory1(rootTestFolder) Then returnVal = False
            If Not Test_IsFileOrDirectory2(rootTestFolder) Then returnVal = False
            If Not Test_IsFileOrDirectory3() Then returnVal = False
            If Not Test_IsFileOrDirectory4() Then returnVal = False
            If Not Test_IsFileOrDirectory5() Then returnVal = False
            If Not Test_IsFileOrDirectory6() Then returnVal = False
            If Not Test_IsFileOrDirectory7() Then returnVal = False
            If Not Test_Attributes1(rootTestFolder) Then returnVal = False
            If Not Test_Attributes2(rootTestFolder) Then returnVal = False
            If Not Test_Attributes3(rootTestFolder) Then returnVal = False
            If Not Test_Attributes4(rootTestFolder) Then returnVal = False
            If Not Test_Attributes5() Then returnVal = False
            If Not Test_Attributes6() Then returnVal = False
            If Not Test_Shortcuts1() Then returnVal = False
            If Not Test_Shortcuts2() Then returnVal = False
            If Not Test_Shortcuts3() Then returnVal = False
            If Not Test_Shortcuts4(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts5(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts6(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts7(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts8(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts9(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts10(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts11(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts12(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts13(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts14(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts15(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts16(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts17(rootTestFolder) Then returnVal = False
            If Not Test_Shortcuts18(rootTestFolder) Then returnVal = False
            If Not Test_Compression1(rootTestFolder) Then returnVal = False
            If Not Test_Compression2(rootTestFolder) Then returnVal = False
            If Not Test_Compression3(rootTestFolder) Then returnVal = False
            If Not Test_Compression4(rootTestFolder) Then returnVal = False
            If Not Test_Compression5(rootTestFolder) Then returnVal = False
            If Not Test_Compression6(rootTestFolder) Then returnVal = False
            If Not Test_Compression7(rootTestFolder) Then returnVal = False
            If Not Test_Compression8(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith1() Then returnVal = False
            If Not Test_GetOpenWith2(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith3(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith4(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith5(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith6(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith7(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith8(rootTestFolder) Then returnVal = False
            If Not Test_GetOpenWith9(rootTestFolder) Then returnVal = False
            If Not Test_Symlinks1(rootTestFolder) Then returnVal = False
            If Not Test_Symlinks2(rootTestFolder) Then returnVal = False
            If Not Test_Symlinks3(rootTestFolder) Then returnVal = False

            Return returnVal
        End Function
    End Module

    Module GeneralFunctions
        Sub DeleteFileIfExists(path As String)
            If File.Exists(path) Then
                File.Delete(path)
            Else
                Console.Write("[")
                WriteColour(ConsoleColor.DarkRed, "Warning")
                Console.WriteLine("] File " & path & " does not exist")
            End If
        End Sub

        Function TestString(functionName As String, input As String, expected As String) As Boolean
            If input = expected Then
                WriteTestOutput(functionName, True)
                Return True
            Else
                WriteTestOutput(functionName, False, input, expected)
                Return False
            End If
        End Function

        Function TestBoolean(functionName As String, input As Boolean, expected As Boolean) As Boolean
            If input = expected Then
                WriteTestOutput(functionName, True)
                Return True
            Else
                WriteTestOutput(functionName, False, input.ToString(), expected.ToString())
                Return False
            End If
        End Function

        Function TestNumber(functionName As String, input As Double, expected As Double) As Boolean
            If input = expected Then
                WriteTestOutput(functionName, True)
                Return True
            Else
                WriteTestOutput(functionName, False, ConvertDouble(input), ConvertDouble(expected))
                Return False
            End If
        End Function

        Private Const formatString As String = "0.############################"
        'https://stackoverflow.com/a/9391762/2999220
        Private Function ConvertDouble(input As Double) As String
            Return input.ToString(formatString)
        End Function

        Function TestType(functionName As String, input As Type, expected As Type) As Boolean
            If input = expected Then
                WriteTestOutput(functionName, True)
                Return True
            Else
                WriteTestOutput(functionName, False, input.FullName, expected.FullName)
                Return False
            End If
        End Function

        Private Sub WriteTestOutput(functionName As String, succeeded As Boolean, Optional input As String = Nothing, Optional expected As String = Nothing)
            WriteColour(ConsoleColor.White, functionName)

            If succeeded Then
                Console.Write(": [")
                WriteColour(ConsoleColor.Green, "Y")
                Console.WriteLine("]")
            Else
                Console.Write(": [")
                WriteColour(ConsoleColor.Red, "N")

                Console.Write("]: in:")
                If input Is Nothing Then
                    WriteColour(ConsoleColor.Magenta, "NULL")
                Else
                    WriteColour(ConsoleColor.Magenta, input)
                End If

                Console.Write(" expected:")
                If expected Is Nothing Then
                    WriteColour(ConsoleColor.Cyan, "NULL")
                Else
                    WriteColour(ConsoleColor.Cyan, expected)
                End If

                Console.WriteLine()
            End If
        End Sub

        Private Sub WriteColour(colour As ConsoleColor, input As String)
            If Not Console.IsOutputRedirected Then Console.ForegroundColor = colour
            Console.Write(input)
            If Not Console.IsOutputRedirected Then Console.ResetColor()
        End Sub
    End Module
End Namespace
