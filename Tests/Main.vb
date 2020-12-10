Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.Reflection

Module Program
    Private Function DoAndReturn(func As Action) As Boolean
        func()
        Return True
    End Function

    Private flagDict As New Dictionary(Of String, WalkmanLib.FlagInfo) From {
        {"help", New WalkmanLib.FlagInfo With {
            .shortFlag = "h"c,
            .description = "Show Help",
            .hasArgs = True,
            .optionalArgs = True,
            .argsInfo = "[flag]",
            .action = Function(input As String)
                          Console.WriteLine("WalkmanLib Tests. Long options are case-insensitive. All options are True by default, except GUI access." & Environment.NewLine)
                          WalkmanLib.EchoHelp(flagDict, input)
                          Environment.Exit(0)
                          Return True
                      End Function
        }},
        {"TestDir", New WalkmanLib.FlagInfo With {
            .shortFlag = "f"c,
            .description = "Folder to use to write test files to. Defaults to the current directory",
            .hasArgs = True,
            .argsInfo = "<folderPath>",
            .action = Function(input As String) DoAndReturn(Sub() rootTestFolder = input)
        }},
        {"GUI", New WalkmanLib.FlagInfo With {
            .shortFlag = "g"c,
            .description = "Set to true if GUI access is available",
            .hasArgs = True,
            .optionalArgs = True,
            .argsInfo = "[true|false]",
            .action = Function(input As String) DoAndReturn(Sub() haveGUIAccess = getBool(input, True))
        }},
        {"DotNet", New WalkmanLib.FlagInfo With {
            .shortFlag = "d"c,
            .description = "Set to false to skip running DotNet tests",
            .hasArgs = True,
            .optionalArgs = True,
            .argsInfo = "[true|false]",
            .action = Function(input As String) DoAndReturn(Sub() runDotNetTests = getBool(input))
        }},
        {"Win32", New WalkmanLib.FlagInfo With {
            .shortFlag = "w"c,
            .description = "Set to false to skip running Win32 tests",
            .hasArgs = True,
            .optionalArgs = True,
            .argsInfo = "[true|false]",
            .action = Function(input As String) DoAndReturn(Sub() runWin32Tests = getBool(input))
        }},
        {"Updates", New WalkmanLib.FlagInfo With {
            .shortFlag = "u"c,
            .description = "Set to false to skip running Updates tests",
            .hasArgs = True,
            .optionalArgs = True,
            .argsInfo = "[true|false]",
            .action = Function(input As String) DoAndReturn(Sub() runUpdatesTests = getBool(input))
        }},
        {"ArgHandler", New WalkmanLib.FlagInfo With {
            .shortFlag = "a"c,
            .description = "Set to false to skip running ArgHandler tests",
            .hasArgs = True,
            .optionalArgs = True,
            .argsInfo = "[true|false]",
            .action = Function(input As String) DoAndReturn(Sub() runArgHandlerTests = getBool(input))
        }},
        {"CustomMsgBox", New WalkmanLib.FlagInfo With {
            .shortFlag = "c"c,
            .description = "Set to false to skip running CustomMsgBox tests",
            .hasArgs = True,
            .optionalArgs = True,
            .argsInfo = "[true|false]",
            .action = Function(input As String) DoAndReturn(Sub() runCustomMsgBoxTests = getBool(input))
        }}
    }

    Private rootTestFolder As String = Nothing
    Private haveGUIAccess As Boolean = False
    Private runDotNetTests As Boolean = True
    Private runWin32Tests As Boolean = True
    Private runUpdatesTests As Boolean = True
    Private runArgHandlerTests As Boolean = True
    Private runCustomMsgBoxTests As Boolean = True

    Private Function getBool(input As String, Optional [default] As Boolean = False) As Boolean
        If String.IsNullOrEmpty(input) Then Return [default]
        Dim rtn As Boolean
        If Boolean.TryParse(input, rtn) Then
            Return rtn
        Else
            ExitE("""{0}"" is not True or False!", input)
            Environment.Exit(1)
            End
        End If
    End Function

    Sub Main(args As String())
        Dim res As WalkmanLib.ResultInfo = WalkmanLib.ProcessArgs(args, flagDict, True)

        If res.gotError Then
            ExitE(res.errorInfo)
        ElseIf res.extraParams.Count = 1 AndAlso res.extraParams.Item(0) = "getAdmin" Then
            Console.WriteLine(WalkmanLib.IsAdmin)
            Environment.Exit(0)
        ElseIf res.extraParams.Count > 0 Then
            ExitE("Unknown parameter(s) supplied!")
        End If

        ' folder where CustomMsgBox.resx is in, used for Test_Icons
        Dim projectRoot As String = New Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath
        projectRoot = New IO.FileInfo(projectRoot).Directory.Parent.Parent.Parent.FullName

        Console.WriteLine(Environment.NewLine & "All tests completed successfully: " & Tests.RunTests(projectRoot, rootTestFolder,
            haveGUIAccess, runCustomMsgBoxTests, runArgHandlerTests, runUpdatesTests, runWin32Tests, runDotNetTests))

        If Not Console.IsOutputRedirected Then
            Console.Write("Press any key to continue . . . ")
            Console.ReadKey(True)
            Console.WriteLine()
        End If
    End Sub

    Sub ExitE(msg As String, Optional formatItem As String = Nothing)
        If formatItem IsNot Nothing Then
            msg = String.Format(msg, formatItem)
        End If
        Console.WriteLine(msg)
        Environment.Exit(1)
    End Sub
End Module
