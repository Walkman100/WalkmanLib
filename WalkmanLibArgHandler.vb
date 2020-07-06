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
End Class
