using static System.Math;

public partial class WalkmanLib {
    public struct TimeInfo {
        public ulong Seconds;
        public ulong Minutes;
        public ulong Hours;
        public ulong Days;
        public ulong Weeks;
    }

    public class TimeConvert {
        /// <summary>Amount of Seconds in a Minute</summary>
        private const ulong _SecondsInMinute = 60;
        /// <summary>Amount of Minutes in an Hour</summary>
        private const ulong _MinutesInHour = 60;
        /// <summary>Amount of Hours in a Day</summary>
        private const ulong _HoursInDay = 24;
        /// <summary>Amount of Days in a Week</summary>
        private const ulong _DaysInWeek = 7;

        /// <summary>Amount of Seconds in an Hour</summary>
        private const ulong _SecondsInHour = _SecondsInMinute * _MinutesInHour;
        /// <summary>Amount of Seconds in a Day</summary>
        private const ulong _SecondsInDay = _SecondsInHour * _HoursInDay;
        /// <summary>Amount of Seconds in a Week</summary>
        private const ulong _SecondsInWeek = _SecondsInDay * _DaysInWeek;

        /// <summary>Amount of Minutes in a Day</summary>
        private const ulong _MinutesInDay = _MinutesInHour * _HoursInDay;
        /// <summary>Amount of Minutes in a Week</summary>
        private const ulong _MinutesInWeek = _MinutesInDay * _DaysInWeek;

        /// <summary>Amount of Hours in a Week</summary>
        private const ulong _HoursInWeek = _HoursInDay * _DaysInWeek;

        #region Single value to multiple
        /// <summary>Truncates and casts a <see cref="double"/> to <see cref="ulong"/></summary>
        /// <param name="value"><see cref="double"/> value to truncate</param>
        private static ulong Tr(double value) {
            value = Truncate(value);
            return (ulong)value;
        }
        /// <summary>Removes all the digits before the decimal point, using <see cref="decimal"/> to do the calculation so no precision is lost</summary>
        /// <param name="value"><see cref="Double"/> value to get decimals</param>
        private static decimal Digits(double value) {
            return (decimal)value % 1;
        }

        /// <summary>Converts seconds to a <see cref="TimeInfo"/> struct. Any amount that can convert will be put into higher units</summary>
        /// <param name="seconds">Seconds to convert</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvSeconds(ulong seconds) {
            var rtn = new TimeInfo();
            if (seconds >= _SecondsInWeek) {
                rtn.Weeks = seconds / _SecondsInWeek;
                seconds = seconds % _SecondsInWeek;
            }
            if (seconds >= _SecondsInDay) {
                rtn.Days = seconds / _SecondsInDay;
                seconds = seconds % _SecondsInDay;
            }
            if (seconds >= _SecondsInHour) {
                rtn.Hours = seconds / _SecondsInHour;
                seconds = seconds % _SecondsInHour;
            }
            if (seconds >= _SecondsInMinute) {
                rtn.Minutes = seconds / _SecondsInMinute;
                seconds = seconds % _SecondsInMinute;
            }
            rtn.Seconds = seconds;
            return rtn;
        }

        /// <summary>Converts minutes to a <see cref="TimeInfo"/> struct, without any lower units. Any amount that can convert will be put into higher units</summary>
        /// <param name="minutes">Minutes to convert</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvMinutes(ulong minutes) {
            var rtn = new TimeInfo();
            if (minutes >= _MinutesInWeek) {
                rtn.Weeks = minutes / _MinutesInWeek;
                minutes = minutes % _MinutesInWeek;
            }
            if (minutes >= _MinutesInDay) {
                rtn.Days = minutes / _MinutesInDay;
                minutes = minutes % _MinutesInDay;
            }
            if (minutes >= _MinutesInHour) {
                rtn.Hours = minutes / _MinutesInHour;
                minutes = minutes % _MinutesInHour;
            }
            rtn.Minutes = minutes;
            return rtn;
        }
        /// <summary>Converts minutes to a <see cref="TimeInfo"/> struct, with decimal places converted to seconds. Any amount that can convert will be put into higher units</summary>
        /// <param name="minutes">Minutes to convert. This is Abs()'d, so negativity doesn't make a difference</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvMinutesD(double minutes) {
            minutes = Abs(minutes);

            ulong minutesUL = Tr(minutes);
            TimeInfo rtn = ConvMinutes(minutesUL);

            minutes = (double)Digits(minutes);
            minutes *= _SecondsInMinute;
            rtn.Seconds = Tr(minutes);

            return rtn;
        }

        /// <summary>Converts hours to a <see cref="TimeInfo"/> struct, without any lower units. Any amount that can convert will be put into higher units</summary>
        /// <param name="hours">Hours to convert</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvHours(ulong hours) {
            var rtn = new TimeInfo();
            if (hours >= _HoursInWeek) {
                rtn.Weeks = hours / _HoursInWeek;
                hours = hours % _HoursInWeek;
            }

            if (hours >= _HoursInDay) {
                rtn.Days = hours / _HoursInDay;
                hours = hours % _HoursInDay;
            }
            rtn.Hours = hours;
            return rtn;
        }
        /// <summary>Converts hours to a <see cref="TimeInfo"/> struct, with decimal places converted to lower units. Any amount that can convert will be put into higher units</summary>
        /// <param name="hours">Hours to convert. This is Abs()'d, so negativity doesn't make a difference</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvHoursD(double hours) {
            hours = Abs(hours);

            ulong hoursUL = Tr(hours);
            TimeInfo rtn = ConvHours(hoursUL);

            hours = (double)Digits(hours);
            hours *= _MinutesInHour;
            rtn.Minutes = Tr(hours);

            hours = (double)Digits(hours);
            hours *= _SecondsInMinute;
            rtn.Seconds = Tr(hours);
            return rtn;
        }

        /// <summary>Converts days to a <see cref="TimeInfo"/> struct, without any lower units. Any amount that can convert will be put into higher units (Weeks only)</summary>
        /// <param name="days">Days to convert</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvDays(ulong days) {
            var rtn = new TimeInfo();
            if (days >= _DaysInWeek) {
                rtn.Weeks = days / _DaysInWeek;
                days = days % _DaysInWeek;
            }
            rtn.Days = days;
            return rtn;
        }
        /// <summary>Converts days to a <see cref="TimeInfo"/> struct, with decimal places converted to lower units. Any amount that can convert will be put into higher units (Weeks only)</summary>
        /// <param name="days">Days to convert. This is Abs()'d, so negativity doesn't make a difference</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvDaysD(double days) {
            days = Abs(days);

            ulong daysUL = Tr(days);
            TimeInfo rtn = ConvDays(daysUL);

            days = (double)Digits(days);
            days *= _HoursInDay;
            rtn.Hours = Tr(days);

            days = (double)Digits(days);
            days *= _MinutesInHour;
            rtn.Minutes = Tr(days);

            days = (double)Digits(days);
            days *= _SecondsInMinute;
            rtn.Seconds = Tr(days);
            return rtn;
        }

        /// <summary>Converts Weeks to a <see cref="TimeInfo"/> struct, with decimal places converted to lower units</summary>
        /// <param name="weeks">Weeks to convert. This is Abs()'d, so negativity doesn't make a difference</param>
        /// <returns><see cref="TimeInfo"/> struct with values filled in</returns>
        public static TimeInfo ConvWeeksD(double weeks) {
            weeks = Abs(weeks);

            TimeInfo rtn;
            rtn.Weeks = Tr(weeks);

            weeks = (double)Digits(weeks);
            weeks *= _DaysInWeek;
            rtn.Days = Tr(weeks);

            weeks = (double)Digits(weeks);
            weeks *= _HoursInDay;
            rtn.Hours = Tr(weeks);

            weeks = (double)Digits(weeks);
            weeks *= _MinutesInHour;
            rtn.Minutes = Tr(weeks);

            weeks = (double)Digits(weeks);
            weeks *= _SecondsInMinute;
            rtn.Seconds = Tr(weeks);
            return rtn;
        }
        #endregion

        #region Multiple to single value
        /// <summary>Converts a <see cref="TimeInfo"/> struct to seconds</summary>
        /// <param name="ti">The <see cref="TimeInfo"/> to convert</param>
        /// <returns>Result in seconds</returns>
        public static ulong GetSeconds(TimeInfo ti) {
            ulong rtn = ti.Seconds;

            rtn += ti.Minutes * _SecondsInMinute;
            rtn += ti.Hours * _SecondsInHour;
            rtn += ti.Days * _SecondsInDay;
            rtn += ti.Weeks * _SecondsInWeek;

            return rtn;
        }

        /// <summary>Converts a <see cref="TimeInfo"/> struct to minutes, with seconds as decimal places</summary>
        /// <param name="ti">The <see cref="TimeInfo"/> to convert</param>
        /// <returns>Result in minutes</returns>
        public static double GetMinutes(TimeInfo ti) {
            double rtn = ti.Minutes;

            rtn += ti.Seconds / (double)_SecondsInMinute;
            rtn += ti.Hours * _MinutesInHour;
            rtn += ti.Days * _MinutesInDay;
            rtn += ti.Weeks * _MinutesInWeek;

            return rtn;
        }

        /// <summary>Converts a <see cref="TimeInfo"/> struct to hours, with smaller units as decimal places</summary>
        /// <param name="ti">The <see cref="TimeInfo"/> to convert</param>
        /// <returns>Result in hours</returns>
        public static double GetHours(TimeInfo ti) {
            double rtn = ti.Hours;

            rtn += ti.Seconds / (double)_SecondsInHour;
            rtn += ti.Minutes / (double)_MinutesInHour;
            rtn += ti.Days * _HoursInDay;
            rtn += ti.Weeks * _HoursInWeek;

            return rtn;
        }

        /// <summary>Converts a <see cref="TimeInfo"/> struct to days, with smaller units as decimal places</summary>
        /// <param name="ti">The <see cref="TimeInfo"/> to convert</param>
        /// <returns>Result in days</returns>
        public static double GetDays(TimeInfo ti) {
            double rtn = ti.Days;

            rtn += ti.Seconds / (double)_SecondsInDay;
            rtn += ti.Minutes / (double)_MinutesInDay;
            rtn += ti.Hours / (double)_HoursInDay;
            rtn += ti.Weeks * _DaysInWeek;

            return rtn;
        }

        /// <summary>Converts a <see cref="TimeInfo"/> struct to weeks, with smaller units as decimal places</summary>
        /// <param name="ti">The <see cref="TimeInfo"/> to convert</param>
        /// <returns>Result in weeks</returns>
        public static double GetWeeks(TimeInfo ti) {
            double rtn = ti.Weeks;

            rtn += ti.Seconds / (double)_SecondsInWeek;
            rtn += ti.Minutes / (double)_MinutesInWeek;
            rtn += ti.Hours / (double)_HoursInWeek;
            rtn += ti.Days / (double)_DaysInWeek;

            return rtn;
        }
        #endregion
    }
}
