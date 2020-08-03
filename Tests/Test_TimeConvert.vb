Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System

Namespace Tests
    Module Tests_TimeConvert
        Function Test_TimeConvert1() As Boolean
            Return TestNumber("TimeConvert1", WalkmanLib.TimeConvert.GetSeconds(New WalkmanLib.TimeInfo With {.Weeks = 1}), 604800)
        End Function

        Function Test_TimeConvert2() As Boolean
            Return TestNumber("TimeConvert2", WalkmanLib.TimeConvert.GetMinutes(New WalkmanLib.TimeInfo With {.Weeks = 1}), 10080)
        End Function

        Function Test_TimeConvert3() As Boolean
            Return TestNumber("TimeConvert3", WalkmanLib.TimeConvert.GetHours(New WalkmanLib.TimeInfo With {.Weeks = 1}), 168)
        End Function

        Function Test_TimeConvert4() As Boolean
            Return TestNumber("TimeConvert4", WalkmanLib.TimeConvert.GetDays(New WalkmanLib.TimeInfo With {.Weeks = 1, .Days = 1, .Hours = 12}), 8.5)
        End Function

        Function Test_TimeConvert5() As Boolean
            Return TestNumber("TimeConvert5", WalkmanLib.TimeConvert.GetWeeks(New WalkmanLib.TimeInfo With {.Weeks = 1, .Days = 3, .Hours = 12}), 1.5)
        End Function

        Function Test_TimeConvert6() As Boolean
            Return TestNumber("TimeConvert6", WalkmanLib.TimeConvert.GetDays(New WalkmanLib.TimeInfo With {.Days = 1, .Hours = 12}), 1.5)
        End Function

        Function Test_TimeConvert7() As Boolean
            Return TestNumber("TimeConvert7", WalkmanLib.TimeConvert.GetHours(New WalkmanLib.TimeInfo With {.Hours = 1, .Minutes = 30}), 1.5)
        End Function

        Function Test_TimeConvert8() As Boolean
            Return TestNumber("TimeConvert8", WalkmanLib.TimeConvert.GetMinutes(New WalkmanLib.TimeInfo With {.Minutes = 1, .Seconds = 30}), 1.5)
        End Function
    End Module
End Namespace