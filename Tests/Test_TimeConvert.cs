namespace Tests {
    static class Tests_TimeConvert {
        public static bool Test_TimeConvert1() {
            return GeneralFunctions.TestNumber("TimeConvert1", WalkmanLib.TimeConvert.GetSeconds(new WalkmanLib.TimeInfo() {Weeks = 1}), 604800);
        }

        public static bool Test_TimeConvert2() {
            return GeneralFunctions.TestNumber("TimeConvert2", WalkmanLib.TimeConvert.GetMinutes(new WalkmanLib.TimeInfo() {Weeks = 1}), 10080);
        }

        public static bool Test_TimeConvert3() {
            return GeneralFunctions.TestNumber("TimeConvert3", WalkmanLib.TimeConvert.GetHours(new WalkmanLib.TimeInfo() {Weeks = 1}), 168);
        }

        public static bool Test_TimeConvert4() {
            return GeneralFunctions.TestNumber("TimeConvert4", WalkmanLib.TimeConvert.GetDays(new WalkmanLib.TimeInfo() {Weeks = 1, Days = 1, Hours = 12}), 8.5);
        }

        public static bool Test_TimeConvert5() {
            return GeneralFunctions.TestNumber("TimeConvert5", WalkmanLib.TimeConvert.GetWeeks(new WalkmanLib.TimeInfo() {Weeks = 1, Days = 3, Hours = 12}), 1.5);
        }

        public static bool Test_TimeConvert6() {
            return GeneralFunctions.TestNumber("TimeConvert6", WalkmanLib.TimeConvert.GetDays(new WalkmanLib.TimeInfo() {Days = 1, Hours = 12}), 1.5);
        }

        public static bool Test_TimeConvert7() {
            return GeneralFunctions.TestNumber("TimeConvert7", WalkmanLib.TimeConvert.GetHours(new WalkmanLib.TimeInfo() {Hours = 1, Minutes = 30}), 1.5);
        }

        public static bool Test_TimeConvert8() {
            return GeneralFunctions.TestNumber("TimeConvert8", WalkmanLib.TimeConvert.GetMinutes(new WalkmanLib.TimeInfo() {Minutes = 1, Seconds = 30}), 1.5);
        }

        public static bool Test_TimeConvert9() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvSeconds(604800 + 86400 + 3600 + 60 + 1);

            if (!GeneralFunctions.TestNumber("TimeConvert9.1", ti.Weeks, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert9.2", ti.Days, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert9.3", ti.Hours, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert9.4", ti.Minutes, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert9.5", ti.Seconds, 1))
                returnVal = false;
            return returnVal;
        }

        public static bool Test_TimeConvert10() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvMinutes(10080 + 1440 + 60 + 1);

            if (!GeneralFunctions.TestNumber("TimeConvert10.1", ti.Weeks, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert10.2", ti.Days, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert10.3", ti.Hours, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert10.4", ti.Minutes, 1))
                returnVal = false;
            return returnVal;
        }

        public static bool Test_TimeConvert11() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvHours(168 + 24 + 1);

            if (!GeneralFunctions.TestNumber("TimeConvert11.1", ti.Weeks, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert11.2", ti.Days, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert11.3", ti.Hours, 1))
                returnVal = false;
            return returnVal;
        }

        public static bool Test_TimeConvert12() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvDays(7 + 1);

            if (!GeneralFunctions.TestNumber("TimeConvert12.1", ti.Weeks, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert12.2", ti.Days, 1))
                returnVal = false;
            return returnVal;
        }

        public static bool Test_TimeConvert13() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvMinutesD(140.5);

            if (!GeneralFunctions.TestNumber("TimeConvert13.1", ti.Weeks, 0))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert13.2", ti.Days, 0))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert13.3", ti.Hours, 2))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert13.4", ti.Minutes, 20))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert13.5", ti.Seconds, 30))
                returnVal = false;
            return returnVal;
        }

        public static bool Test_TimeConvert14() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvHoursD(140.5125);

            if (!GeneralFunctions.TestNumber("TimeConvert14.1", ti.Weeks, 0))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert14.2", ti.Days, 5))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert14.3", ti.Hours, 20))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert14.4", ti.Minutes, 30))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert14.5", ti.Seconds, 45))
                returnVal = false;
            return returnVal;
        }

        public static bool Test_TimeConvert15() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvDaysD(140.525);

            if (!GeneralFunctions.TestNumber("TimeConvert15.1", ti.Weeks, 20))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert15.2", ti.Days, 0))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert15.3", ti.Hours, 12))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert15.4", ti.Minutes, 36))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert15.5", ti.Seconds, 0))
                returnVal = false;
            return returnVal;
        }

        public static bool Test_TimeConvert16() {
            bool returnVal = true;
            WalkmanLib.TimeInfo ti = WalkmanLib.TimeConvert.ConvWeeksD(1.5055555555555558);

            if (!GeneralFunctions.TestNumber("TimeConvert16.1", ti.Weeks, 1))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert16.2", ti.Days, 3))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert16.3", ti.Hours, 12))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert16.4", ti.Minutes, 56))
                returnVal = false;
            if (!GeneralFunctions.TestNumber("TimeConvert16.5", ti.Seconds, 0))
                returnVal = false;
            return returnVal;
        }
    }
}
