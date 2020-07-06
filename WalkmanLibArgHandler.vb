Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports Microsoft.VisualBasic

Partial Public Class WalkmanLib
    Public Class FlagInfo
        Public shortFlag As Char = Chr(0)
        Public hasArgs As Boolean = False
        Public argsInfo As String = Nothing
        Public description As String
        Public action As Action(Of String)
    End Class

    Private Class ShortFlagInfo
        Public usesShortFlags As Boolean = False
        Public maxLength As Integer = 0
    End Class

    Private Shared Function getShortFlagInfo(flagDict As Dictionary(Of String, FlagInfo)) As ShortFlagInfo
        Dim retVal As New ShortFlagInfo

        For Each flagInfo As KeyValuePair(Of String, FlagInfo) In flagDict
            If flagInfo.Value.shortFlag <> Chr(0) Then
                retVal.usesShortFlags = True

                Dim flagLength As Integer = 1
                If flagInfo.Value.hasArgs Then
                    flagLength += flagInfo.Value.argsInfo.Length + 1
                End If

                If flagLength > retVal.maxLength Then
                    retVal.maxLength = flagLength
                End If
            End If
        Next

        Return retVal
    End Function

    Private Shared Function getMaxFlagLength(flagDict As Dictionary(Of String, FlagInfo)) As Integer
        Dim retVal As Integer = 0

        For Each flagInfo As KeyValuePair(Of String, FlagInfo) In flagDict
            Dim flagLength As Integer = flagInfo.Key.Length
            If flagInfo.Value.hasArgs Then
                flagLength += flagInfo.Value.argsInfo.Length + 1
            End If

            If flagLength > retVal Then
                retVal = flagLength
            End If
        Next

        Return retVal
    End Function

    Public Shared Sub EchoHelp(flagDict As Dictionary(Of String, FlagInfo), Optional flag As String = Nothing)
        If flag Is Nothing Then
            Dim shortFlagInfo As ShortFlagInfo = getShortFlagInfo(flagDict)
            Dim maxFlagLength As Integer = getMaxFlagLength(flagDict)

            If shortFlagInfo.usesShortFlags Then
                Console.WriteLine("{0}  {1}  {2}", "Option".PadRight(shortFlagInfo.maxLength + 2), "Long Option".PadRight(maxFlagLength + 2), "Description")
            Else
                Console.WriteLine("{0}  {1}", "Long Option".PadRight(maxFlagLength + 2), "Description")
            End If

            For Each flagInfo As KeyValuePair(Of String, FlagInfo) In flagDict
                If flagInfo.Value.shortFlag <> Chr(0) Then
                    Dim shortFlag As String
                    If flagInfo.Value.hasArgs Then
                        shortFlag = (flagInfo.Value.shortFlag & " " & flagInfo.Value.argsInfo).PadRight(shortFlagInfo.maxLength)
                    Else
                        shortFlag = flagInfo.Value.shortFlag.ToString().PadRight(shortFlagInfo.maxLength)
                    End If
                    Console.Write(" -{0}  ", shortFlag)
                ElseIf shortFlagInfo.usesShortFlags Then
                    Console.Write(" ".PadRight(shortFlagInfo.maxLength + 2) & "  ")
                End If

                Dim longFlag As String
                If flagInfo.Value.hasArgs Then
                    longFlag = flagInfo.Key & "=" & flagInfo.Value.argsInfo
                Else
                    longFlag = flagInfo.Key
                End If
                Console.WriteLine("--{0}  {1}", longFlag.PadRight(maxFlagLength), flagInfo.Value.description)
            Next
        Else
            If flagDict.ContainsKey(flag.ToLowerInvariant()) Then
                Dim flagInfo As FlagInfo = flagDict.Item(flag.ToLowerInvariant())
                Dim longFlag As String = flag.ToLowerInvariant()
                If flagInfo.hasArgs Then
                    longFlag &= "=" & flagInfo.argsInfo
                End If

                If flagInfo.shortFlag <> Chr(0) Then
                    Dim optionPad As Integer = 4 + If(flagInfo.argsInfo, "").Length
                    Console.WriteLine("{0}  {1}  {2}", "Option".PadRight(optionPad), "Long Option".PadRight(longFlag.Length + 2), "Description")

                    Console.Write(" -{0} {1}  ", flagInfo.shortFlag, If(flagInfo.argsInfo, "").PadRight(2))
                Else
                    Console.WriteLine("{0}  {1}", "Long Option".PadRight(longFlag.Length + 2), "Description")
                End If

                Console.WriteLine("--{0}  {1}", longFlag.PadRight(9), flagInfo.description)
            Else
                Console.WriteLine("Flag ""{0}"" not found!", flag)
            End If
        End If
    End Sub

    Public Class ResultInfo
        Public gotError As Boolean = False
        Public errorInfo As String = Nothing
    End Class

    Public Shared Function ProcessArgs(args As String(), flagDict As Dictionary(Of String, FlagInfo)) As ResultInfo
        Dim processingShortFlags As Boolean = True
        Dim finishedProcessingArgs As Boolean = False
        Dim gettingArg As Boolean = False
        Dim gettingArgFor As New FlagInfo
        Dim gettingArgForLong As String = Nothing
        Dim resultInfo As New ResultInfo

        For Each arg As String In args

            If processingShortFlags AndAlso arg.StartsWith("-") AndAlso arg(1) <> "-"c Then
                For Each chr As Char In arg.Substring(1)
                    If gettingArg Then
                        resultInfo.gotError = True
                        resultInfo.errorInfo = String.Format("Short Flag ""{0}"" requires arguments!", gettingArgFor.shortFlag)
                        Return resultInfo
                    End If

                    Dim gotShortFlag As Boolean = False
                    For Each flagInfo As KeyValuePair(Of String, FlagInfo) In flagDict
                        If flagInfo.Value.shortFlag = chr Then
                            If flagInfo.Value.hasArgs Then
                                gettingArg = True
                                gettingArgFor = flagInfo.Value
                            Else
                                flagInfo.Value.action(Nothing)
                            End If
                            gotShortFlag = True
                            Exit For
                        End If
                    Next
                    If Not gotShortFlag Then
                        resultInfo.gotError = True
                        resultInfo.errorInfo = String.Format("Unknown Short Flag ""{0}""!", chr)
                        Return resultInfo
                    End If

                Next
            ElseIf Not arg.StartsWith("--") AndAlso gettingArg Then
                gettingArgFor.action(arg)

                gettingArg = False
                gettingArgForLong = Nothing
            ElseIf arg = "--" Then
                If gettingArg Then
                    resultInfo.gotError = True
                    resultInfo.errorInfo = String.Format("Flag ""{0}"" requires arguments!", If(gettingArgForLong, gettingArgFor.shortFlag))
                    Return resultInfo
                End If

                processingShortFlags = False
                finishedProcessingArgs = True
            ElseIf Not finishedProcessingArgs AndAlso arg.StartsWith("--") Then
                If gettingArg Then
                    resultInfo.gotError = True
                    resultInfo.errorInfo = String.Format("Flag ""{0}"" requires arguments!", If(gettingArgForLong, gettingArgFor.shortFlag))
                    Return resultInfo
                End If

                processingShortFlags = False

                arg = arg.Substring(2)
                Dim flagArg As String = Nothing
                If arg.Contains("="c) Then
                    flagArg = arg.Substring(arg.LastIndexOf("="c) + 1)
                    arg = arg.Remove(arg.IndexOf("="c))
                End If

                If flagDict.ContainsKey(arg.ToLowerInvariant()) Then
                    gettingArgFor = flagDict.Item(arg.ToLowerInvariant())
                    If gettingArgFor.hasArgs AndAlso flagArg Is Nothing Then
                        resultInfo.gotError = True
                        resultInfo.errorInfo = String.Format("Flag ""{0}"" requires arguments!", arg.ToLowerInvariant())
                        Return resultInfo
                    ElseIf gettingArgFor.hasArgs Then
                        gettingArgFor.action(flagArg)
                    Else
                        gettingArgFor.action(Nothing)
                    End If
                Else
                    resultInfo.gotError = True
                    resultInfo.errorInfo = String.Format("Unknown Flag ""{0}""!", arg)
                    Return resultInfo
                End If
            Else
                ' extra info
            End If
        Next

        If gettingArg Then
            resultInfo.gotError = True
            resultInfo.errorInfo = String.Format("Flag ""{0}"" requires arguments!", If(gettingArgForLong, gettingArgFor.shortFlag))
            Return resultInfo
        End If

        Return resultInfo
    End Function
End Class
