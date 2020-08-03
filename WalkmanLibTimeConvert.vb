Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Math

Partial Public Class WalkmanLib
    Public Structure TimeInfo
        Public Seconds As ULong
        Public Minutes As ULong
        Public Hours As ULong
        Public Days As ULong
        Public Weeks As ULong
    End Structure

    Public Class TimeConvert
        ''' <summary>Amount of Seconds in a Minute</summary>
        Private Const _SecondsInMinute As ULong = 60
        ''' <summary>Amount of Minutes in an Hour</summary>
        Private Const _MinutesInHour As ULong = 60
        ''' <summary>Amount of Hours in a Day</summary>
        Private Const _HoursInDay As ULong = 24
        ''' <summary>Amount of Days in a Week</summary>
        Private Const _DaysInWeek As ULong = 7

        ''' <summary>Amount of Seconds in an Hour</summary>
        Private Const _SecondsInHour As ULong = _SecondsInMinute * _MinutesInHour
        ''' <summary>Amount of Seconds in a Day</summary>
        Private Const _SecondsInDay As ULong = _SecondsInHour * _HoursInDay
        ''' <summary>Amount of Seconds in a Week</summary>
        Private Const _SecondsInWeek As ULong = _SecondsInDay * _DaysInWeek

        ''' <summary>Amount of Minutes in a Day</summary>
        Private Const _MinutesInDay As ULong = _MinutesInHour * _HoursInDay
        ''' <summary>Amount of Minutes in a Week</summary>
        Private Const _MinutesInWeek As ULong = _MinutesInDay * _DaysInWeek

        ''' <summary>Amount of Hours in a Week</summary>
        Private Const _HoursInWeek As ULong = _HoursInDay * _DaysInWeek

#Region "Single value to multiple"
        Public Shared Function ConvSeconds(seconds As ULong) As TimeInfo
            Dim rtn As TimeInfo
            If seconds >= _SecondsInWeek Then
                rtn.Weeks = seconds \ _SecondsInWeek
                seconds = seconds Mod _SecondsInWeek
            End If
            If seconds >= _SecondsInDay Then
                rtn.Days = seconds \ _SecondsInDay
                seconds = seconds Mod _SecondsInDay
            End If
            If seconds >= _SecondsInHour Then
                rtn.Hours = seconds \ _SecondsInHour
                seconds = seconds Mod _SecondsInHour
            End If
            If seconds >= _SecondsInMinute Then
                rtn.Minutes = seconds \ _SecondsInMinute
                seconds = seconds Mod _SecondsInMinute
            End If
            rtn.Seconds = seconds
            Return rtn
        End Function

        Public Shared Function ConvMinutes(minutes As ULong) As TimeInfo
            Dim rtn As TimeInfo
            If minutes >= _MinutesInWeek Then
                rtn.Weeks = minutes \ _MinutesInWeek
                minutes = minutes Mod _MinutesInWeek
            End If
            If minutes >= _MinutesInDay Then
                rtn.Days = minutes \ _MinutesInDay
                minutes = minutes Mod _MinutesInDay
            End If
            If minutes >= _MinutesInHour Then
                rtn.Hours = minutes \ _MinutesInHour
                minutes = minutes Mod _MinutesInHour
            End If
            rtn.Minutes = minutes
            Return rtn
        End Function
        Public Shared Function ConvMinutesD(minutes As Double) As TimeInfo
            minutes = Abs(minutes)

            Dim minutesUL As ULong = CType(Truncate(minutes), ULong)
            Dim rtn As TimeInfo = ConvMinutes(minutesUL)

            minutes = minutes Mod 1 ' remove all the digits before the decimal point
            minutes *= _SecondsInMinute
            rtn.Seconds = CType(Truncate(minutes), ULong)

            Return rtn
        End Function

        Public Shared Function ConvHours(hours As ULong) As TimeInfo
            Dim rtn As TimeInfo
            If hours >= _HoursInWeek Then
                rtn.Weeks = hours \ _HoursInWeek
                hours = hours Mod _HoursInWeek
            End If
            If hours >= _HoursInDay Then
                rtn.Days = hours \ _HoursInDay
                hours = hours Mod _HoursInDay
            End If
            rtn.Hours = hours
            Return rtn
        End Function
        Public Shared Function ConvHoursD(hours As Double) As TimeInfo
            hours = Abs(hours)

            Dim hoursUL As ULong = CType(Truncate(hours), ULong)
            Dim rtn As TimeInfo = ConvHours(hoursUL)

            hours = hours Mod 1 ' remove all the digits before the decimal point
            hours *= _MinutesInHour
            rtn.Minutes = CType(Truncate(hours), ULong)

            hours = hours Mod 1
            hours *= _SecondsInMinute
            rtn.Seconds = CType(Truncate(hours), ULong)
            Return rtn
        End Function

        Public Shared Function ConvDays(days As ULong) As TimeInfo
            Dim rtn As TimeInfo
            If days >= _DaysInWeek Then
                rtn.Weeks = days \ _DaysInWeek
                days = days Mod _DaysInWeek
            End If
            rtn.Days = days
            Return rtn
        End Function
        Public Shared Function ConvDaysD(days As Double) As TimeInfo
            days = Abs(days)

            Dim daysUL As ULong = CType(Truncate(days), ULong)
            Dim rtn As TimeInfo = ConvDays(daysUL)

            days = days Mod 1 ' remove all the digits before the decimal point
            days *= _HoursInDay
            rtn.Hours = CType(Truncate(days), ULong)

            days = days Mod 1
            days *= _MinutesInHour
            rtn.Minutes = CType(Truncate(days), ULong)

            days = days Mod 1
            days *= _SecondsInMinute
            rtn.Seconds = CType(Truncate(days), ULong)
            Return rtn
        End Function

        Public Shared Function ConvWeeksD(weeks As Double) As TimeInfo
            weeks = Abs(weeks)

            Dim rtn As TimeInfo
            rtn.Weeks = CType(Truncate(weeks), ULong)

            weeks = weeks Mod 1 ' remove all the digits before the decimal point
            weeks *= _DaysInWeek
            rtn.Days = CType(Truncate(weeks), ULong)

            weeks = weeks Mod 1
            weeks *= _HoursInDay
            rtn.Hours = CType(Truncate(weeks), ULong)

            weeks = weeks Mod 1
            weeks *= _MinutesInHour
            rtn.Minutes = CType(Truncate(weeks), ULong)

            weeks = weeks Mod 1
            weeks *= _SecondsInMinute
            rtn.Seconds = CType(Truncate(weeks), ULong)
            Return rtn
        End Function
#End Region

#Region "Multiple to single value"
        Public Shared Function GetSeconds(ti As TimeInfo) As ULong
            Dim rtn As ULong = ti.Seconds

            rtn += ti.Minutes * _SecondsInMinute
            rtn += ti.Hours * _SecondsInHour
            rtn += ti.Days * _SecondsInDay
            rtn += ti.Weeks * _SecondsInWeek

            Return rtn
        End Function

        Public Shared Function GetMinutes(ti As TimeInfo) As Double
            Dim rtn As Double = ti.Minutes

            rtn += ti.Seconds / _SecondsInMinute
            rtn += ti.Hours * _MinutesInHour
            rtn += ti.Days * _MinutesInDay
            rtn += ti.Weeks * _MinutesInWeek

            Return rtn
        End Function

        Public Shared Function GetHours(ti As TimeInfo) As Double
            Dim rtn As Double = ti.Hours

            rtn += ti.Seconds / _SecondsInHour
            rtn += ti.Minutes / _MinutesInHour
            rtn += ti.Days * _HoursInDay
            rtn += ti.Weeks * _HoursInWeek

            Return rtn
        End Function

        Public Shared Function GetDays(ti As TimeInfo) As Double
            Dim rtn As Double = ti.Days

            rtn += ti.Seconds / _SecondsInDay
            rtn += ti.Minutes / _MinutesInDay
            rtn += ti.Hours / _HoursInDay
            rtn += ti.Weeks * _DaysInWeek

            Return rtn
        End Function

        Public Shared Function GetWeeks(ti As TimeInfo) As Double
            Dim rtn As Double = ti.Weeks

            rtn += ti.Seconds / _SecondsInWeek
            rtn += ti.Minutes / _MinutesInWeek
            rtn += ti.Hours / _HoursInWeek
            rtn += ti.Days / _DaysInWeek

            Return rtn
        End Function
#End Region
    End Class
End Class
