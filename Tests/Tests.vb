Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection

Namespace Tests
    Module Main
        Function RunTests(Optional haveGUIAccess As Boolean = True,
                          Optional runCustomMsgBoxTests As Boolean = True,
                          Optional runArgHandlerTests As Boolean = True,
                          Optional runUpdatesTests As Boolean = True,
                          Optional runWin32Tests As Boolean = True,
                          Optional runDotNetTests As Boolean = True) As Boolean
            Dim returnVal As Boolean = True

            Dim rootTestFolder As String = Path.Combine(Directory.GetCurrentDirectory, "tests")
            Directory.CreateDirectory(rootTestFolder)
            If Not TestBoolean("CreateDirectory", Directory.Exists(rootTestFolder), True) Then returnVal = False

            If runDotNetTests Then
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
                If Not Test_RunAndGetOutputThrows1() Then returnVal = False
                If Not Test_RunAndGetOutputThrows2() Then returnVal = False
                If Not Test_RunAndGetOutputThrows3() Then returnVal = False
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
            End If

            If runWin32Tests Then
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
                If Not Test_ShortcutThrows1(rootTestFolder) Then returnVal = False
                If Not Test_ShortcutThrows2() Then returnVal = False
                If Not Test_Compression1(rootTestFolder) Then returnVal = False
                If Not Test_Compression2(rootTestFolder) Then returnVal = False
                If Not Test_Compression3(rootTestFolder) Then returnVal = False
                If Not Test_Compression4(rootTestFolder) Then returnVal = False
                If Not Test_Compression5(rootTestFolder) Then returnVal = False
                If Not Test_Compression6(rootTestFolder) Then returnVal = False
                If Not Test_Compression7(rootTestFolder) Then returnVal = False
                If Not Test_Compression8(rootTestFolder) Then returnVal = False
                If Not Test_Compression9(rootTestFolder) Then returnVal = False
                If Not Test_Compression10(rootTestFolder) Then returnVal = False
                If Not Test_Compression11(rootTestFolder) Then returnVal = False
                If Not Test_Compression12(rootTestFolder) Then returnVal = False
                If Not Test_Compression13(rootTestFolder) Then returnVal = False
                If Not Test_Compression14() Then returnVal = False
                If Not Test_GetOpenWith1() Then returnVal = False
                If Not Test_GetOpenWith2(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith3(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith4(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith5(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith6(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith7(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith8(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith9(rootTestFolder) Then returnVal = False
                If Not Test_GetOpenWith10(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks1(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks2() Then returnVal = False
                If Not Test_Symlinks3(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks4(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks5(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks6(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks7(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks8(rootTestFolder) Then returnVal = False
                If Not Test_Symlinks9() Then returnVal = False
                If Not Test_Symlinks10() Then returnVal = False
                If Not Test_SymlinkThrows1() Then returnVal = False
                If Not Test_SymlinkThrows2(rootTestFolder) Then returnVal = False
                If Not Test_SymlinkThrows3(rootTestFolder) Then returnVal = False
                If Not Test_SymlinkThrows4(rootTestFolder) Then returnVal = False
                If Not Test_SymlinkThrows5(rootTestFolder) Then returnVal = False
                If Not Test_Hardlinks1(rootTestFolder) Then returnVal = False
                If Not Test_Hardlinks2(rootTestFolder) Then returnVal = False
                If Not Test_Hardlinks3(rootTestFolder) Then returnVal = False
                If Not Test_Hardlinks4(rootTestFolder) Then returnVal = False
                If Not Test_Hardlinks5(rootTestFolder) Then returnVal = False
                If Not Test_HardlinkThrows1(rootTestFolder) Then returnVal = False
                If Not Test_HardlinkThrows2(rootTestFolder) Then returnVal = False
                If Not Test_HardlinkThrows3(rootTestFolder) Then returnVal = False
                If Not Test_HardlinkThrows4(rootTestFolder) Then returnVal = False
                If Not Test_Hardlink6(rootTestFolder) Then returnVal = False
                If Not Test_Hardlink7(rootTestFolder) Then returnVal = False
                If Not Test_Hardlink8(rootTestFolder) Then returnVal = False
                If Not Test_Hardlink9(rootTestFolder) Then returnVal = False
                If Not Test_Hardlink10(rootTestFolder) Then returnVal = False

                Dim projectRoot As String = New Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath
                projectRoot = New FileInfo(projectRoot).Directory.Parent.Parent.FullName

                If Not Test_ExtractIcon1(rootTestFolder, projectRoot) Then returnVal = False
                If Not Test_ExtractIcon2(rootTestFolder, projectRoot) Then returnVal = False
                If Not Test_ExtractIcon3(rootTestFolder) Then returnVal = False

                If haveGUIAccess Then
                    If Not Test_PickIconDialog1() Then returnVal = False
                    If Not Test_PickIconDialog2() Then returnVal = False
                    If Not Test_PickIconDialog3() Then returnVal = False
                    If Not Test_ShowProperties1() Then returnVal = False
                    If Not Test_ShowProperties2() Then returnVal = False
                    If Not Test_ShowProperties3() Then returnVal = False
                    If Not Test_ShowProperties4() Then returnVal = False
                    If Not Test_Mouse1() Then returnVal = False
                    If Not Test_Mouse2() Then returnVal = False
                Else
                    WriteTestSkipped(New List(Of String)({
                        "PickIconDialog1", "PickIconDialog2", "PickIconDialog3",
                        "ShowProperties1", "ShowProperties2", "ShowProperties3", "ShowProperties4",
                        "Mouse1", "Mouse2"
                        }), "No Graphical Session Access")
                End If
            End If

            If runUpdatesTests Then
                If Not Test_Updates1() Then returnVal = False
                If Not Test_Updates2() Then returnVal = False
                If Not Test_Updates3() Then returnVal = False
                If Not Test_Updates4() Then returnVal = False
                If Not Test_Updates5() Then returnVal = False
                If Not Test_Updates6() Then returnVal = False
                If Not Test_Updates7() Then returnVal = False
                If Not Test_Updates8() Then returnVal = False
                If Not Test_UpdateThrows1() Then returnVal = False
                If Not Test_UpdateThrows2() Then returnVal = False
                If Not Test_UpdateThrows3() Then returnVal = False
            End If

            If runArgHandlerTests Then
                If Not Test_ArgHandler1() Then returnVal = False
                If Not Test_ArgHandler2() Then returnVal = False
                If Not Test_ArgHandler3() Then returnVal = False
                If Not Test_ArgHandler4() Then returnVal = False
                If Not Test_ArgHandler5() Then returnVal = False
            End If

            If runCustomMsgBoxTests Then
                If haveGUIAccess Then
                    If Not Test_CustomMsgBox1() Then returnVal = False
                    If Not Test_CustomMsgBox2() Then returnVal = False
                    If Not Test_CustomMsgBox3() Then returnVal = False
                    If Not Test_CustomMsgBox4() Then returnVal = False
                    If Not Test_CustomMsgBox5() Then returnVal = False
                    If Not Test_CustomMsgBox6() Then returnVal = False
                    If Not Test_CustomMsgBox7() Then returnVal = False
                    If Not Test_CustomMsgBox8() Then returnVal = False
                Else
                    WriteTestSkipped(New List(Of String)({
                        "CustomMsgBox1", "CustomMsgBox2", "CustomMsgBox3", "CustomMsgBox4",
                        "CustomMsgBox5", "CustomMsgBox6", "CustomMsgBox7", "CustomMsgBox8"
                        }), "No Graphical Session Access")
                End If
            End If

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

            Console.Write(": [")
            If succeeded Then
                WriteColour(ConsoleColor.Green, "Y")
                Console.Write("]")
            Else
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
            End If
            Console.WriteLine()
        End Sub

        Sub WriteTestSkipped(functionNames As List(Of String), reason As String)
            For Each functionName As String In functionNames
                WriteColour(ConsoleColor.White, functionName)

                Console.Write(": ")
                WriteColour(ConsoleColor.DarkGray, String.Format("Skipped ({0})", reason))
                Console.WriteLine()
            Next
        End Sub

        Private Sub WriteColour(colour As ConsoleColor, input As String)
            If Not Console.IsOutputRedirected Then Console.ForegroundColor = colour
            Console.Write(input)
            If Not Console.IsOutputRedirected Then Console.ResetColor()
        End Sub
    End Module
End Namespace
