Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

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

        Function Test_TimeConvert9() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvSeconds(604800 + 86400 + 3600 + 60 + 1)

            If Not TestNumber("TimeConvert9.1", ti.Weeks, 1) Then returnVal = False
            If Not TestNumber("TimeConvert9.2", ti.Days, 1) Then returnVal = False
            If Not TestNumber("TimeConvert9.3", ti.Hours, 1) Then returnVal = False
            If Not TestNumber("TimeConvert9.4", ti.Minutes, 1) Then returnVal = False
            If Not TestNumber("TimeConvert9.5", ti.Seconds, 1) Then returnVal = False

            Return returnVal
        End Function

        Function Test_TimeConvert10() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvMinutes(10080 + 1440 + 60 + 1)

            If Not TestNumber("TimeConvert10.1", ti.Weeks, 1) Then returnVal = False
            If Not TestNumber("TimeConvert10.2", ti.Days, 1) Then returnVal = False
            If Not TestNumber("TimeConvert10.3", ti.Hours, 1) Then returnVal = False
            If Not TestNumber("TimeConvert10.4", ti.Minutes, 1) Then returnVal = False

            Return returnVal
        End Function

        Function Test_TimeConvert11() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvHours(168 + 24 + 1)

            If Not TestNumber("TimeConvert11.1", ti.Weeks, 1) Then returnVal = False
            If Not TestNumber("TimeConvert11.2", ti.Days, 1) Then returnVal = False
            If Not TestNumber("TimeConvert11.3", ti.Hours, 1) Then returnVal = False

            Return returnVal
        End Function

        Function Test_TimeConvert12() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvDays(7 + 1)

            If Not TestNumber("TimeConvert12.1", ti.Weeks, 1) Then returnVal = False
            If Not TestNumber("TimeConvert12.2", ti.Days, 1) Then returnVal = False

            Return returnVal
        End Function

        Function Test_TimeConvert13() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvMinutesD(140.5)

            If Not TestNumber("TimeConvert13.1", ti.Weeks, 0) Then returnVal = False
            If Not TestNumber("TimeConvert13.2", ti.Days, 0) Then returnVal = False
            If Not TestNumber("TimeConvert13.3", ti.Hours, 2) Then returnVal = False
            If Not TestNumber("TimeConvert13.4", ti.Minutes, 20) Then returnVal = False
            If Not TestNumber("TimeConvert13.5", ti.Seconds, 30) Then returnVal = False

            Return returnVal
        End Function

        Function Test_TimeConvert14() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvHoursD(140.5125)

            If Not TestNumber("TimeConvert14.1", ti.Weeks, 0) Then returnVal = False
            If Not TestNumber("TimeConvert14.2", ti.Days, 5) Then returnVal = False
            If Not TestNumber("TimeConvert14.3", ti.Hours, 20) Then returnVal = False
            If Not TestNumber("TimeConvert14.4", ti.Minutes, 30) Then returnVal = False
            If Not TestNumber("TimeConvert14.5", ti.Seconds, 45) Then returnVal = False

            Return returnVal
        End Function

        Function Test_TimeConvert15() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvDaysD(140.525)

            If Not TestNumber("TimeConvert15.1", ti.Weeks, 20) Then returnVal = False
            If Not TestNumber("TimeConvert15.2", ti.Days, 0) Then returnVal = False
            If Not TestNumber("TimeConvert15.3", ti.Hours, 12) Then returnVal = False
            If Not TestNumber("TimeConvert15.4", ti.Minutes, 36) Then returnVal = False
            If Not TestNumber("TimeConvert15.5", ti.Seconds, 0) Then returnVal = False

            Return returnVal
        End Function

        Function Test_TimeConvert16() As Boolean
            Dim returnVal As Boolean = True
            Dim ti As WalkmanLib.TimeInfo = WalkmanLib.TimeConvert.ConvWeeksD(1.5055555555555558)

            If Not TestNumber("TimeConvert16.1", ti.Weeks, 1) Then returnVal = False
            If Not TestNumber("TimeConvert16.2", ti.Days, 3) Then returnVal = False
            If Not TestNumber("TimeConvert16.3", ti.Hours, 12) Then returnVal = False
            If Not TestNumber("TimeConvert16.4", ti.Minutes, 56) Then returnVal = False
            If Not TestNumber("TimeConvert16.5", ti.Seconds, 0) Then returnVal = False

            Return returnVal
        End Function
    End Module
End Namespace