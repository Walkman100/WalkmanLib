Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.Linq

Partial Public Class WalkmanLib
    Public Const NullChar As Char = Microsoft.VisualBasic.ControlChars.NullChar

    ''' <summary>Create a dictionary of these to use with the ArgHandler methods</summary>
    Public Class FlagInfo
        ''' <summary>Optional single character to activate this flag</summary>
        Public shortFlag As Char = NullChar
        ''' <summary>Set to <see langword="True"/> if this flag requires arguments</summary>
        Public hasArgs As Boolean = False
        ''' <summary>Set to <see langword="True"/> if this flag's arguments are optional</summary>
        Public optionalArgs As Boolean = False
        ''' <summary>Set to text to describe the flag's argument, if <see cref="hasArgs"/> is <see langword="True"/>. Used in <see cref="EchoHelp"/></summary>
        Public argsInfo As String = Nothing
        ''' <summary>Description of the flag used in <see cref="EchoHelp"/></summary>
        Public description As String
        ''' <summary>
        ''' Routine to run when the command is called. String argument will be <see langword="Nothing"/> if <see cref="hasArgs"/> is <see langword="False"/>,
        ''' or <see cref="optionalArgs"/> is <see langword="True"/> and no argument is supplied.
        ''' <br />This function should return a <see cref="Boolean"/>, return <see langword="False"/> to stop ProcessArgs args processing.
        ''' <see cref="ResultInfo.errorInfo"/> will be set to <see langword="Nothing"/>.
        ''' </summary>
        Public action As Func(Of String, Boolean)
    End Class

#Region "EchoHelp & helpers"
    ''' <summary>Info on whether the dictionary of items has any short flags, and the max length of those flags</summary>
    Private Class ShortFlagInfo
        Public usesShortFlags As Boolean = False
        Public maxLength As Integer = 0
    End Class

    ''' <summary>Gets whether the supplied dictionary of items has any short flags, and the max length of those flags</summary>
    ''' <param name="flagDict">Dictionary to operate on</param>
    Private Shared Function getShortFlagInfo(flagDict As Dictionary(Of String, FlagInfo)) As ShortFlagInfo
        Dim retVal As New ShortFlagInfo

        For Each flagInfo As KeyValuePair(Of String, FlagInfo) In flagDict
            If flagInfo.Value.shortFlag <> NullChar Then
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

    ''' <summary>Gets the maximum length of the flags in the supplied dictionary of items</summary>
    ''' <param name="flagDict">Dictionary to operate on</param>
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

    ''' <summary>Generates and writes help information about the items in the specified dictionary to the console</summary>
    ''' <param name="flagDict">Dictionary to operate on</param>
    ''' <param name="flag">Optional flag to show help for. Can be the long or short form</param>
    ''' <param name="output">Optional TextWriter to write the output to. Defaults to <see cref="Console.Error"/>.</param>
    Public Shared Sub EchoHelp(flagDict As Dictionary(Of String, FlagInfo), Optional flag As String = Nothing, Optional output As IO.TextWriter = Nothing)
        If output Is Nothing Then output = Console.Error
        If flag Is Nothing Then
            Dim shortFlagInfo As ShortFlagInfo = getShortFlagInfo(flagDict)
            Dim maxFlagLength As Integer = getMaxFlagLength(flagDict)

            If shortFlagInfo.usesShortFlags Then
                output.WriteLine("{0}  {1}  {2}", "Option".PadRight(shortFlagInfo.maxLength + 2), "Long Option".PadRight(maxFlagLength + 2), "Description")
            Else
                output.WriteLine("{0}  {1}", "Long Option".PadRight(maxFlagLength + 2), "Description")
            End If

            For Each flagInfo As KeyValuePair(Of String, FlagInfo) In flagDict
                If flagInfo.Value.shortFlag <> NullChar Then
                    Dim shortFlag As String
                    If flagInfo.Value.hasArgs Then
                        shortFlag = (flagInfo.Value.shortFlag & " " & flagInfo.Value.argsInfo).PadRight(shortFlagInfo.maxLength)
                    Else
                        shortFlag = flagInfo.Value.shortFlag.ToString().PadRight(shortFlagInfo.maxLength)
                    End If
                    output.Write(" -{0}  ", shortFlag)
                ElseIf shortFlagInfo.usesShortFlags Then
                    output.Write(" ".PadRight(shortFlagInfo.maxLength + 2) & "  ")
                End If

                Dim longFlag As String
                If flagInfo.Value.hasArgs Then
                    longFlag = flagInfo.Key & "=" & flagInfo.Value.argsInfo
                Else
                    longFlag = flagInfo.Key
                End If
                output.WriteLine("--{0}  {1}", longFlag.PadRight(maxFlagLength), flagInfo.Value.description)
            Next
        Else
            Dim flagKV As KeyValuePair(Of String, FlagInfo) = flagDict.FirstOrDefault(
                Function(x As KeyValuePair(Of String, FlagInfo))
                    If flag.Length = 1 AndAlso x.Value.shortFlag = flag.ToLowerInvariant()(0) Then
                        Return True
                    End If

                    Return x.Key.ToLowerInvariant = flag.ToLowerInvariant
                End Function)

            If flagKV.Key IsNot Nothing Then
                Dim flagInfo As FlagInfo = flagKV.Value
                Dim longFlag As String = flagKV.Key
                If flagInfo.hasArgs Then
                    longFlag &= "=" & flagInfo.argsInfo
                End If

                If flagInfo.shortFlag <> NullChar Then
                    Dim optionPad As Integer = 4 + If(flagInfo.argsInfo, "").Length
                    output.WriteLine("{0}  {1}  {2}", "Option".PadRight(optionPad), "Long Option".PadRight(longFlag.Length + 2), "Description")

                    output.Write(" -{0} {1}  ", flagInfo.shortFlag, If(flagInfo.argsInfo, "").PadRight(2))
                Else
                    output.WriteLine("{0}  {1}", "Long Option".PadRight(longFlag.Length + 2), "Description")
                End If

                output.WriteLine("--{0}  {1}", longFlag.PadRight(9), flagInfo.description)
            Else
                output.WriteLine("Flag ""{0}"" not found!", flag)
            End If
        End If
    End Sub
#End Region

#Region "ProcessArgs & helpers"
    ''' <summary>Return info for ProcessArgs</summary>
    Public Class ResultInfo
        ''' <summary>Will be <see langword="True"/> if there was an error. <see cref="errorInfo"/> contains the error message</summary>
        Public gotError As Boolean = False
        ''' <summary>Error message. <see langword="Nothing"/> if no error occurred - check <see cref="gotError"/></summary>
        Public errorInfo As String = Nothing
        ''' <summary>Parameters after processing arguments has completed. Empty list if there are no parameters or <c>paramsAfterFlags</c> is <see langword="False"/></summary>
        Public extraParams As New List(Of String)
    End Class

    ''' <summary>Internal helper to simplify creating a <see cref="ResultInfo"/> instance, with <c>errorInfo</c> set and <c>gotError = True</c></summary>
    ''' <param name="errorInfo">String to fill the <see cref="ResultInfo.errorInfo"/> field with</param>
    ''' <param name="flagName">Optional string to format <paramref name="errorInfo"/> - <c>String.Format</c> will be used if this flag is set</param>
    ''' <returns></returns>
    Private Shared Function GetErrorResult(errorInfo As String, Optional flagName As String = Nothing) As ResultInfo
        Dim resultInfo As New ResultInfo With {.gotError = True}
        If flagName Is Nothing Then
            resultInfo.errorInfo = errorInfo
        Else
            resultInfo.errorInfo = String.Format(errorInfo, flagName)
        End If
        Return resultInfo
    End Function

    Private Shared Sub DisableGettingArg(ByRef gettingArg As Boolean, ByRef gettingArgFor As FlagInfo, ByRef gettingArgForLong As String)
        gettingArg = False
        gettingArgFor = Nothing
        gettingArgForLong = Nothing
    End Sub

    ''' <summary>
    ''' Processes arguments supplied to a program using a supplied Dictionary of flags. If <paramref name="paramsAfterFlags"/> is <see langword="True"/>,
    ''' <see cref="ResultInfo.extraParams"/> will contain a list of arguments after processing is complete. If there is an incorrect parameter in the supplied string array,
    ''' <see cref="ResultInfo.gotError"/> will be <see langword="True"/> and <see cref="ResultInfo.errorInfo"/> will contain the error message to display to the user.
    ''' <br/>Short flags are processed first, and once a long flag is encountered short flag processing is skipped.
    ''' <br/>"<c>--</c>" can be used to force flag processing to end, and the remainder of the arguments will be considered as extra parameters.
    ''' <br/>Long flag matching is case-insensitive, while Short flag matching is case-sensitive - characters must match exactly.
    ''' </summary>
    ''' <param name="args">Array of argument strings to process</param>
    ''' <param name="flagDict">Dictionary to operate on</param>
    ''' <param name="paramsAfterFlags"><see langword="True"/> to allow extra parameters after arguments processing is complete</param>
    ''' <returns><see cref="ResultInfo"/> class with the result of processing</returns>
    Public Shared Function ProcessArgs(args As String(), flagDict As Dictionary(Of String, FlagInfo), Optional paramsAfterFlags As Boolean = False) As ResultInfo
        Dim processingShortFlags As Boolean = True
        Dim finishedProcessingArgs As Boolean = False
        Dim gettingArg As Boolean = False
        Dim gettingArgFor As New FlagInfo
        Dim gettingArgForLong As String = Nothing
        Dim extraParams As New List(Of String)

        For Each arg As String In args

            If processingShortFlags AndAlso arg.StartsWith("-") AndAlso arg(1) <> "-"c Then
                For Each chr As Char In arg.Substring(1)
                    If gettingArg AndAlso gettingArgFor.optionalArgs Then
                        If Not gettingArgFor.action(Nothing) Then Return GetErrorResult(Nothing)
                        DisableGettingArg(gettingArg, gettingArgFor, gettingArgForLong)
                    ElseIf gettingArg Then
                        Return GetErrorResult("Short Flag ""{0}"" requires arguments!", gettingArgFor.shortFlag)
                    End If

                    Dim flagKV As KeyValuePair(Of String, FlagInfo) = flagDict.FirstOrDefault(Function(x As KeyValuePair(Of String, FlagInfo))
                                                                                                  Return x.Value.shortFlag = chr
                                                                                              End Function)
                    If flagKV.Value IsNot Nothing Then
                        If flagKV.Value.hasArgs Then
                            gettingArg = True
                            gettingArgFor = flagKV.Value
                        Else
                            If Not flagKV.Value.action(Nothing) Then Return GetErrorResult(Nothing)
                        End If
                    Else
                        Return GetErrorResult("Unknown Short Flag ""{0}""!", chr)
                    End If
                Next
            ElseIf Not arg.StartsWith("--") AndAlso gettingArg Then
                If Not gettingArgFor.action(arg) Then Return GetErrorResult(Nothing)

                DisableGettingArg(gettingArg, gettingArgFor, gettingArgForLong)
            ElseIf arg = "--" Then
                If gettingArg AndAlso gettingArgFor.optionalArgs Then
                    If Not gettingArgFor.action(Nothing) Then Return GetErrorResult(Nothing)
                    DisableGettingArg(gettingArg, gettingArgFor, gettingArgForLong)
                ElseIf gettingArg Then
                    Return GetErrorResult("Flag ""{0}"" requires arguments!", If(gettingArgForLong, gettingArgFor.shortFlag))
                End If

                processingShortFlags = False
                finishedProcessingArgs = True
            ElseIf Not finishedProcessingArgs AndAlso arg.StartsWith("--") Then
                If gettingArg AndAlso gettingArgFor.optionalArgs Then
                    If Not gettingArgFor.action(Nothing) Then Return GetErrorResult(Nothing)
                    DisableGettingArg(gettingArg, gettingArgFor, gettingArgForLong)
                ElseIf gettingArg Then
                    Return GetErrorResult("Flag ""{0}"" requires arguments!", If(gettingArgForLong, gettingArgFor.shortFlag))
                End If

                processingShortFlags = False

                arg = arg.Substring(2)
                Dim flagArg As String = Nothing
                If arg.Contains("="c) Then
                    flagArg = arg.Substring(arg.LastIndexOf("="c) + 1)
                    arg = arg.Remove(arg.IndexOf("="c))
                End If

                Dim flagKV As KeyValuePair(Of String, FlagInfo) = flagDict.FirstOrDefault(Function(x As KeyValuePair(Of String, FlagInfo))
                                                                                              Return x.Key.ToLowerInvariant = arg.ToLowerInvariant
                                                                                          End Function)
                If flagKV.Key IsNot Nothing Then
                    gettingArgFor = flagKV.Value
                    If gettingArgFor.hasArgs = False Then
                        If Not gettingArgFor.action(Nothing) Then Return GetErrorResult(Nothing)
                    ElseIf flagArg Is Nothing AndAlso gettingArgFor.optionalArgs Then
                        If Not gettingArgFor.action(Nothing) Then Return GetErrorResult(Nothing)
                    ElseIf flagArg Is Nothing Then
                        Return GetErrorResult("Flag ""{0}"" requires arguments!", flagKV.Key)
                    Else
                        If Not gettingArgFor.action(flagArg) Then Return GetErrorResult(Nothing)
                    End If
                    gettingArgFor = Nothing
                Else
                    Return GetErrorResult("Unknown Flag ""{0}""!", arg)
                End If
            ElseIf paramsAfterFlags = True Then
                extraParams.Add(arg)
            Else
                Return GetErrorResult("Argument ""{0}"" is not a flag!", arg)
            End If
        Next

        If gettingArg AndAlso gettingArgFor.optionalArgs Then
            If Not gettingArgFor.action(Nothing) Then Return GetErrorResult(Nothing)
            DisableGettingArg(gettingArg, gettingArgFor, gettingArgForLong)
        ElseIf gettingArg Then
            Return GetErrorResult("Flag ""{0}"" requires arguments!", If(gettingArgForLong, gettingArgFor.shortFlag))
        End If

        Return New ResultInfo() With {
            .gotError = False,
            .extraParams = extraParams
        }
    End Function
#End Region
End Class
