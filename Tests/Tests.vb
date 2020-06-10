Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

Namespace Tests
    Module Main
        Function RunAllTests() As Boolean
            Dim returnVal As Boolean = True

            If Not TestString("testfunc", "input1", "input1") Then returnVal = False
            If Not TestString("testfunc", "input2", "input3") Then returnVal = False
            If Not TestBoolean("testfunc", True, True) Then returnVal = False
            If Not TestBoolean("testfunc", True, False) Then returnVal = False
            If Not TestNumber("testfunc", 34, 34) Then returnVal = False
            If Not TestNumber("testfunc", 3.141592693, 34) Then returnVal = False
            If Not TestNumber("testfunc", 0.000000000001234, 1234000) Then returnVal = False

            Return returnVal
        End Function
    End Module

    Module GeneralFunctions
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
