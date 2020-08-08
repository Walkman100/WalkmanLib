Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.Linq

Namespace Tests
    Module Tests_ArgHandler
        Function WriteAndReturn(text As String, ParamArray toFormat() As String) As Boolean
            Console.WriteLine(text, toFormat)
            Return True
        End Function

        Dim flagDict As New Dictionary(Of String, WalkmanLib.FlagInfo) From {
            {"test", New WalkmanLib.FlagInfo With {
                .shortFlag = "t"c,
                .hasArgs = True,
                .argsInfo = "<string>",
                .description = "test",
                .action = Function(arg As String) WriteAndReturn("1{0}", arg)
            }},
            {"test2", New WalkmanLib.FlagInfo With {
                .hasArgs = False,
                .description = "test2",
                .action = Function() WriteAndReturn("test2 called")
            }},
            {"test3", New WalkmanLib.FlagInfo With {
                .hasArgs = True,
                .argsInfo = "<string>",
                .description = "test3",
                .action = Function(arg As String) WriteAndReturn("3{0}", arg)
            }},
            {"test4", New WalkmanLib.FlagInfo With {
                .shortFlag = "k"c,
                .hasArgs = True,
                .optionalArgs = True,
                .argsInfo = "[string]",
                .description = "test4",
                .action = Function(arg As String) WriteAndReturn("test4 called: {0}", arg)
            }},
            {"test5", New WalkmanLib.FlagInfo With {
                .shortFlag = "T"c,
                .description = "test5",
                .action = Function() WriteAndReturn("test5 called")
            }},
            {"TEST", New WalkmanLib.FlagInfo With {
                .hasArgs = True,
                .argsInfo = "<string>",
                .description = "TEST",
                .action = Function(arg As String) WriteAndReturn("TEST{0}", arg)
            }},
            {"TeSt7", New WalkmanLib.FlagInfo With {
                .hasArgs = False,
                .description = "TeSt7",
                .action = Function() WriteAndReturn("TeSt7 called")
            }},
            {"help", New WalkmanLib.FlagInfo With {
                .shortFlag = "h"c,
                .description = "Show Help",
                .action = Function()
                              WalkmanLib.EchoHelp(flagDict)
                              Return True
                          End Function
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

            Dim expectedOutput As String = "Option        Long Option       Description" & Environment.NewLine &
                                           " -t <string>  --test=<string>   test" & Environment.NewLine &
                                           "              --test2           test2" & Environment.NewLine &
                                           "              --test3=<string>  test3" & Environment.NewLine &
                                           " -k [string]  --test4=[string]  test4" & Environment.NewLine &
                                           " -T           --test5           test5" & Environment.NewLine &
                                           "              --TEST=<string>   TEST" & Environment.NewLine &
                                           "              --TeSt7           TeSt7" & Environment.NewLine &
                                           " -h           --help            Show Help" & Environment.NewLine

            Return TestString("ArgHandler1", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler2() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    WalkmanLib.EchoHelp(flagDict, "t")
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "Option        Long Option      Description" & Environment.NewLine &
                                           " -t <string>  --test=<string>  test" & Environment.NewLine

            Return TestString("ArgHandler2", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler3() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    WalkmanLib.EchoHelp(flagDict, "test7")
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "Long Option  Description" & Environment.NewLine &
                                           "--TeSt7      TeSt7" & Environment.NewLine

            Return TestString("ArgHandler3", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler4() As Boolean
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

            Dim expectedOutput As String = "Short Flag ""t"" requires arguments!" & Environment.NewLine

            Return TestString("ArgHandler4", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler5() As Boolean
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

            Dim expectedOutput As String = "1TEST" & Environment.NewLine & "test2 called" & Environment.NewLine & "3TEST" & Environment.NewLine

            Return TestString("ArgHandler5", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler6() As Boolean
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

            Dim expectedOutput As String = "test5 called" & Environment.NewLine

            Return TestString("ArgHandler6", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler7() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"--test7"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "TeSt7 called" & Environment.NewLine

            Return TestString("ArgHandler7", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler8() As Boolean
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

            Dim expectedOutput As String = "1TEST" & Environment.NewLine & "--test3=TEST" & Environment.NewLine

            Return TestString("ArgHandler8", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler9() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"-k"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "test4 called: " & Environment.NewLine

            Return TestString("ArgHandler9", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler10() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"-k", "shorttest"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "test4 called: shorttest" & Environment.NewLine

            Return TestString("ArgHandler10", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler11() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"--test4"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "test4 called: " & Environment.NewLine

            Return TestString("ArgHandler11", sw.ToString(), expectedOutput)
        End Function

        Function Test_ArgHandler12() As Boolean
            Dim sw As New IO.StringWriter

            Using New RedirectConsole(sw)
                Try
                    Dim rtn As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs({"--test4=longtest"}, flagDict)
                    If rtn.gotError Then
                        Console.WriteLine(rtn.errorInfo)
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.ToString())
                End Try
            End Using

            Dim expectedOutput As String = "test4 called: longtest" & Environment.NewLine

            Return TestString("ArgHandler12", sw.ToString(), expectedOutput)
        End Function
    End Module
End Namespace
