Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports Microsoft.VisualBasic

Namespace Tests
    Module Tests_ArgHandler
        Dim flagDict As New Dictionary(Of String, WalkmanLib.FlagInfo) From {
            {"test", New WalkmanLib.FlagInfo With {
                .shortFlag = "t"c,
                .hasArgs = True,
                .argsInfo = "<string>",
                .description = "test",
                .action = Sub(arg As String) Console.WriteLine("1{0}", arg)
            }},
            {"test2", New WalkmanLib.FlagInfo With {
                .hasArgs = False,
                .description = "test2",
                .action = Sub() Console.WriteLine("test2 called")
            }},
            {"test3", New WalkmanLib.FlagInfo With {
                .hasArgs = True,
                .argsInfo = "<string>",
                .description = "test3",
                .action = Sub(arg As String) Console.WriteLine("3{0}", arg)
            }},
            {"test4", New WalkmanLib.FlagInfo With {
                .shortFlag = "k"c,
                .description = "test4",
                .action = Sub() Console.WriteLine("test4 called")
            }},
            {"test5", New WalkmanLib.FlagInfo With {
                .shortFlag = "T"c,
                .description = "test5",
                .action = Sub() Console.WriteLine("test5 called")
            }},
            {"TEST", New WalkmanLib.FlagInfo With {
                .hasArgs = True,
                .argsInfo = "<string>",
                .description = "TEST",
                .action = Sub(arg As String) Console.WriteLine("TEST{0}", arg)
            }},
            {"help", New WalkmanLib.FlagInfo With {
                .shortFlag = "h"c,
                .description = "Show Help",
                .action = Sub() WalkmanLib.EchoHelp(flagDict)
            }}
        }

        Function Test_ArgHandler1() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    WalkmanLib.EchoHelp(flagDict)
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "Option        Long Option       Description" & vbNewLine &
                                           " -t <string>  --test=<string>   test" & vbNewLine &
                                           "              --test2           test2" & vbNewLine &
                                           "              --test3=<string>  test3" & vbNewLine &
                                           " -k           --test4           test4" & vbNewLine &
                                           " -T           --test5           test5" & vbNewLine &
                                           "              --TEST=<string>   TEST" & vbNewLine &
                                           " -h           --help            Show Help" & vbNewLine

            Return TestString("ArgHandler1", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler2() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"-t", "-k", "-T"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "Short Flag ""t"" requires arguments!" & vbNewLine

            Return TestString("ArgHandler2", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler3() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"-t", "TEST", "--test2", "--test3=TEST"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "1TEST" & vbNewLine & "test2 called" & vbNewLine & "3TEST" & vbNewLine

            Return TestString("ArgHandler3", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler4() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"-T"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "test5 called" & vbNewLine

            Return TestString("ArgHandler4", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler5() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"-t", "TEST", "--", "--test3=TEST"}, flagDict, True)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    Else
                        Console.WriteLine(rtn.extraParams.First())
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "1TEST" & vbNewLine & "--test3=TEST" & vbNewLine

            Return TestString("ArgHandler5", sw.ToString(), expectedOutput)
        End Function
    End Module
End Namespace