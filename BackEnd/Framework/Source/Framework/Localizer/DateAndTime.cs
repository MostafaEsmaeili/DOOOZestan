#region usings

using System;
using Persian;

#endregion

namespace Framework.Localizer
{
    public static class DateAndTime
    {
        private static DateTime? _min = null, _max = null;

        public static DateTime MinDateSupported
        {
            get
            {
                if (_min == null) _min = MoveToSupportRange(DateTime.MinValue);
                return (DateTime)_min;
            }
        }

        public static DateTime MaxDateSupported
        {
            get
            {
                if (_max == null) _max = MoveToSupportRange(DateTime.MaxValue);
                return (DateTime)_max;
            }
        }

        internal static void AdjustDateTimeOverFlow(ref int day, ref int month, ref int year, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            while (month > GetMonthsInYear(year, cul) || day > GetDaysInMonth(month, year, cul))
            {
                while (month > GetMonthsInYear(year, cul))
                {
                    month = month - GetMonthsInYear(year, cul);
                    year++;
                }
                while (day > GetDaysInMonth(month, year, cul))
                {
                    day = day - GetDaysInMonth(month, year, cul);
                    month++;
                }
            }
        }

        public static DayOfWeek FirstDayOfWeek(Culture inculture = null)
        {
            if (inculture == null) inculture = Globals.CurrentCulture;
            return inculture.FirstDayOfWeek;
        }

        public static void ConvertToCalendar(this DateTime dt,
            out int year,
            out int month,
            out int day,
            out int hour,
            out int min,
            out int sec,
            out int milli,
            CalendarTypes? caltype = null)
        {
            if (caltype == null) caltype = Globals.CurrentCulture.Calendar;
            CalendarNames.FromDateTime(dt,
                (CalendarTypes)caltype,
                out year,
                out month,
                out day,
                out hour,
                out min,
                out sec,
                out milli);
        }

        public static void ConvertToCalendar(this DateTime dt,
            out int year,
            out int month,
            out int day,
            CalendarTypes? caltype = null)
        {
            int t;
            ConvertToCalendar(dt, out year, out month, out day, out t, out t, out t, out t, caltype);
        }

        public static string GetAMDesignator(this Culture inculture)
        {
            if (inculture == null) inculture = Globals.CurrentCulture;
            return CalendarNames.GetAMPM(true, inculture.BaseLanguage);
        }
        public static string GetAMDesignator()
        {
            return GetAMDesignator(null);
        }

        public static string GetDateString(this DateTime dt,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            return GetDateString(dt as DateTime?, cul, cal);
        }

        public static string GetDateString(this DateTime? dt,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            return GetDateString("", dt, cul, cal);
        }

        public static string GetDateString(this DateTime? dt,
            string format,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            return GetDateString(format, dt, cul, cal);
        }

        public static string GetDateString(this DateTime dt,
            string format,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            return GetDateString(format, dt, cul, cal);
        }

        public static string GetDateString(string format = "",
            DateTime? dt = null,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            if (dt == null) dt = DateTime.Now;
            dt = MoveToSupportRange((DateTime)dt);
            if (cul == null) cul = Globals.CurrentCulture;
            if (cal == null) cal = cul.Calendar;
            if (format == "") format = cul.DateFormat;
            return GetDateTimeStr(format, dt, cul, cal);
        }

        public static DateTime ConvertFromCalendar(int year, int month, int day, CalendarTypes? caltype = null)
        {
            return ConvertFromCalendar(year, month, day, 1, 1, 1, 1, caltype);
        }

        public static DateTime ConvertFromCalendar(int year,
            int month,
            int day,
            int hour,
            int min,
            int sec,
            int milli,
            CalendarTypes? caltype = null)
        {
            if (caltype == null) caltype = Globals.CurrentCulture.Calendar;
            return CalendarNames.GetDateTime(year, month, day, hour, min, sec, milli, (CalendarTypes)caltype);
        }

        internal static string GetDateTimeStr(string format = "",
            DateTime? dt = null,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            if (!dt.HasValue) return "";
            if ((dt > _max) || (dt < _min))
            {
                throw new ArgumentOutOfRangeException("dt",
                    "Your DateTime (" + dt.Value.Year + "-" + dt.Value.Month + "-" +
                    dt.Value.Day +
                    ") exceeds the supported range, max=" + MaxDateSupported.Year +
                    "-" +
                    MaxDateSupported.Month + "-" + MaxDateSupported.Day + " min=" +
                    MinDateSupported.Year + "-" + MinDateSupported.Month + "-" +
                    MinDateSupported.Day);
            }
            //if (caltype == null) caltype = this.Calendar;
            if (cul == null) cul = Globals.CurrentCulture;
            if (cal == null) cal = cul.Calendar;
            if (format == "") format = cul.DateTimeFormat;
            int year, month, day, hour, min, sec, mil;
            ConvertToCalendar((DateTime)dt, out year, out month, out day, out hour, out min, out sec, out mil, cal);
            DayOfWeek dow = ((DateTime)dt).DayOfWeek;
            string dwname = GetDayName(dow, false, cul);
            string dwshort = GetDayName(dow, true, cul);
            string mnname = GetMonthName(month, false, cal, cul);
            string mnshort = GetMonthName(month, true, cal, cul);
            string ampm = (((hour >= 12) || (hour == 24)) ? GetPMDesignator(cul) : GetAMDesignator(cul));
            string str = CalendarNames.FormattedString(year,
                month,
                day,
                hour,
                min,
                sec,
                mil,
                dow,
                mnname,
                mnshort,
                dwname,
                dwshort,
                ampm,
                format);
            return str;
        }

        public static string GetDateTimeString(this DateTime dt, Culture cul, CalendarTypes? cal = null)
        {
            return GetDateTimeString(dt as DateTime?, cul, cal);
        }

        public static string GetDateTimeString(this DateTime? dt, Culture cul = null, CalendarTypes? cal = null)
        {
            return GetDateTimeStr("", dt, cul, cal);
        }

        public static string GetDateTimeString(this DateTime? dt, string format = "", Culture cul = null, CalendarTypes? cal = null)
        {
            return GetDateTimeString(format, dt, cul, cal);
        }

        public static string GetDateTimeString(this DateTime dt, string format = "", Culture cul = null, CalendarTypes? cal = null)
        {
            return GetDateTimeString(format, dt, cul, cal);
        }

        public static string GetDateTimeString(string format = "",
            DateTime? dt = null,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            //if (!dt.HasValue) return "";
            if (dt == null) dt = DateTime.Now;
            //if (caltype == null) caltype = this.Calendar;
            if (cul == null) cul = Globals.CurrentCulture;
            if (cal == null) cal = cul.Calendar;
            if (format == "") format = cul.DateTimeFormat;
            return GetDateTimeStr(format, dt, cul, cal);
        }

        public static string GetDayName(this DayOfWeek dow, bool shorten = false, Culture inculture = null)
        {
            if (inculture == null) inculture = Globals.CurrentCulture;
            return CalendarNames.GetDayOfWeekName(dow, inculture.BaseLanguage, shorten);
        }

        public static int GetDaysInMonth(this DateTime dt, Culture cul = null)
        {
            int year, month, day;
            if (cul == null) cul = Globals.CurrentCulture;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            return GetDaysInMonth(month, year, cul);
        }

        public static int GetDaysInMonth(this DateTime dt, CalendarTypes calendar)
        {
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, calendar);
            return GetDaysInMonth(month, year, calendar);
        }

        public static int GetDaysInMonth(int month, int year, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            return GetDaysInMonth(month, year, cul.Calendar);
        }

        public static int GetDaysInMonth(int month, int year, CalendarTypes calendar)
        {
            return CalendarNames.GetDaysInMonth(month, year, calendar);
        }

        public static int GetDaysInYear(this DateTime dt, Culture cul = null)
        {
            int year, month, day;
            if (cul == null) cul = Globals.CurrentCulture;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            return GetDaysInYear(year, cul);
        }

        public static int GetDaysInYear(this DateTime dt, CalendarTypes calendar)
        {
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, calendar);
            return GetDaysInYear(year, calendar);
        }

        public static int GetDaysInYear(int year, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            return GetDaysInYear(year, cul.Calendar);
        }

        public static int GetDaysInYear(int year, CalendarTypes calendar)
        {
            return CalendarNames.GetDaysInYear(year, calendar);
        }

        public static DateTime GetDaysLater(this DateTime dt, int numberOfDaysLater = 1, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            day = day + numberOfDaysLater;
            AdjustDateTimeOverFlow(ref day, ref month, ref year, cul);
            DateTime enddate = ConvertFromCalendar(year, month, day);
            return enddate;
        }

        internal static bool GetDefaultCalendarDateFromString(this string input,
            out int year,
            out int month,
            out int day,
            char separator = (char) 0,
            Culture inputculture = null,
            DateTimeParseOrder parseOrder = DateTimeParseOrder.DMY)
        {
            if (inputculture == null) inputculture = Globals.CurrentCulture;
            if (separator == (char)0) separator = Globals.CurrentCulture.DateSeparator[0];
            if (inputculture.Equals(SupportedCultures.FaIr))
            {
                PersianCalendar.DateFormatType df;
                switch (parseOrder)
                {
                    case DateTimeParseOrder.YMD:
                        df = PersianCalendar.DateFormatType.YMD;
                        break;
                    case DateTimeParseOrder.YDM:
                        df = PersianCalendar.DateFormatType.YDM;
                        break;
                    case DateTimeParseOrder.DMY:
                        df = PersianCalendar.DateFormatType.DMY;
                        break;
                    case DateTimeParseOrder.DYM:
                        df = PersianCalendar.DateFormatType.DYM;
                        break;
                    case DateTimeParseOrder.MYD:
                        df = PersianCalendar.DateFormatType.MYD;
                        break;
                    case DateTimeParseOrder.MDY:
                        df = PersianCalendar.DateFormatType.MDY;
                        break;
                    default:
                        df = PersianCalendar.DateFormatType.UnknownFormat;
                        break;
                }
                if (input.Contains(":") && input.Contains(" "))
                {
                    var inputs = input.Split(' ');
                    if (inputs.Length == 2)
                    {
                        if (inputs[0].Contains(":") ^ inputs[1].Contains(":"))
                        {
                            if (inputs[0].Contains(":"))
                            {
                                input = inputs[1];
                            }
                            else
                            {
                                input = inputs[0];
                            }
                        }
                    }
                }
                var pdt = PersianCalendar.PersianDateTime.ParseDate(input, separator, ref df);
                year = pdt.Year;
                month = pdt.Month;
                day = pdt.Day;
                return true;
            }
            if (inputculture.Equals(SupportedCultures.EnUs))
            {
                var dt = DateTime.Parse(input);
                year = dt.Year;
                month = dt.Month;
                day = dt.Day;
                return true;
            }
            year = month = day = 0;
            return false;
        }

        public static DateTime GetEndOfMonth(this DateTime dt, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            DateTime enddate = ConvertFromCalendar(year, month, GetDaysInMonth(month, year, cul), cul.Calendar);
            return enddate;
        }

        public static DateTime GetEndOfWeek(this DateTime dt, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            while (dt.DayOfWeek != cul.FirstDayOfWeek - 1)
            {
                dt = dt.AddDays(1);
            }
            return dt;
        }

        public static DateTime GetEndOfYear(this DateTime dt, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            DateTime enddate = ConvertFromCalendar(year, GetMonthsInYear(year, cul), GetDaysInMonth(month, year, cul));
            return enddate;
        }

        public static DateTime GetFirstOfMonth(this DateTime dt, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            DateTime enddate = ConvertFromCalendar(year, month, 1, cul.Calendar);
            return enddate;
        }

        public static DateTime GetFirstOfWeek(this DateTime dt, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            while (dt.DayOfWeek != cul.FirstDayOfWeek)
            {
                dt = dt.AddDays(-1);
            }
            return dt;
        }

        public static DateTime GetFirstOfYear(this DateTime dt, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            DateTime enddate = ConvertFromCalendar(year, 1, 1, cul.Calendar);
            return enddate;
        }

        public static string GetMonthName(int month,
            bool shorten = false,
            CalendarTypes? caltype = null,
            Culture inculture = null)
        {
            if (inculture == null) inculture = Globals.CurrentCulture;
            if (caltype == null) caltype = inculture.Calendar;
            return CalendarNames.GetMonthName(month, (CalendarTypes)caltype, inculture.BaseLanguage, shorten);
        }

        public static int GetMonthsInYear(this DateTime dt, Culture cul = null)
        {
            int year, month, day;
            if (cul == null) cul = Globals.CurrentCulture;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            return GetMonthsInYear(year, cul);
        }

        public static int GetMonthsInYear(this DateTime dt, CalendarTypes calendar)
        {
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, calendar);
            return GetMonthsInYear(year, calendar);
        }

        public static int GetMonthsInYear(this int year, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            return GetMonthsInYear(year, cul.Calendar);
        }

        public static int GetMonthsInYear(this int year, CalendarTypes calendar)
        {
            return CalendarNames.GetMonthsInYear(year, calendar);
        }

        public static DateTime GetMonthsLater(this DateTime dt, int numberOfMonthsLater = 1, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            month = month + numberOfMonthsLater;
            AdjustDateTimeOverFlow(ref day, ref month, ref year, cul);
            DateTime enddate = ConvertFromCalendar(year, month, day, cul.Calendar);
            return enddate;
        }

        public static string GetPMDesignator(this Culture inculture)
        {
            if (inculture == null) inculture = Globals.CurrentCulture;
            return CalendarNames.GetAMPM(false, inculture.BaseLanguage);
        }

        public static string GetPMDesignator()
        {
            return GetPMDesignator(null);
        }

        public static string GetTimeString(this DateTime dt, Culture cul = null, CalendarTypes? cal = null)
        {
            return GetTimeString(dt as DateTime?, cul, cal);
        }

        public static string GetTimeString(this DateTime dt,
            string format,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            return GetTimeString(format, dt, cul, cal);
        }

        public static string GetTimeString(this DateTime? dt,
            string format,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            return GetTimeString(format, dt, cul, cal);
        }

        public static string GetTimeString(this DateTime? dt, Culture cul = null, CalendarTypes? cal = null)
        {
            return GetDateTimeStr("", dt, cul, cal);
        }

        public static string GetTimeString(string format = "",
            DateTime? dt = null,
            Culture cul = null,
            CalendarTypes? cal = null)
        {
            //if (!dt.HasValue) return "";
            if (dt == null) dt = DateTime.Now;
            if (cul == null) cul = Globals.CurrentCulture;
            if (cal == null) cal = cul.Calendar;
            if (format == "") format = cul.TimeFormat;
            return GetDateTimeStr(format, dt, cul, cal);
        }

        public static DateTime GetWeeksLater(this DateTime dt, int numberOfWeeksLater = 1, Culture cul = null)
        {
            return GetDaysLater(dt, numberOfWeeksLater * 7, cul);
        }

        public static DateTime GetYearsLater(this DateTime dt, int numberOfYearsLater = 1, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            year = year + numberOfYearsLater;
            AdjustDateTimeOverFlow(ref day, ref month, ref year, cul);
            DateTime enddate = ConvertFromCalendar(year, month, day);
            return enddate;
        }

        public static bool IsLeapYear(this DateTime dt, Culture cul = null)
        {
            int year, month, day;
            if (cul == null) cul = Globals.CurrentCulture;
            ConvertToCalendar(dt, out year, out month, out day, cul.Calendar);
            return IsLeapYear(year, cul);
        }

        public static bool IsLeapYear(this DateTime dt, CalendarTypes calendar)
        {
            int year, month, day;
            ConvertToCalendar(dt, out year, out month, out day, calendar);
            return IsLeapYear(year, calendar);
        }

        public static bool IsLeapYear(int year, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            return IsLeapYear(year, cul.Calendar);
        }

        public static bool IsLeapYear(int year, CalendarTypes calendar)
        {
            return CalendarNames.GetLeapYear(year, calendar);
        }

        public static DateTime MoveToSupportRange(this DateTime dt)
        {
            foreach (CalendarTypes calendarType in Enum.GetValues(typeof(CalendarTypes)))
            {
                if (dt < CalendarNames.GetCalendar(calendarType).MinSupportedDateTime)
                    dt = CalendarNames.GetCalendar(calendarType).MinSupportedDateTime;
                if (dt > CalendarNames.GetCalendar(calendarType).MaxSupportedDateTime)
                    dt = CalendarNames.GetCalendar(calendarType).MaxSupportedDateTime;
            }
            return dt;
        }

        public static DateTime? MoveToSupportRange(this DateTime? dt)
        {
            if (dt.HasValue) return MoveToSupportRange(dt.Value);
            return null;
        }

        public static DateTime ParseDateTime(this string input,
            Culture inputculture = null,
            DateTimeParseOrder parseOrder = DateTimeParseOrder.DMY)
        {
            DateTime dt;
            if (!TryParseDateTime(input, out dt, inputculture, parseOrder))
                throw new Exception("Localizer: Cannot parse datetime in this culture!");
            return dt;
        }

        public static bool TryParseDateTime(this string input,
            out DateTime dt,
            Culture inputculture = null,
            DateTimeParseOrder parseOrder = DateTimeParseOrder.DMY)
        {
            if (inputculture == null) inputculture = Globals.CurrentCulture;
            int year, month, day;
            char separator = '-';
            if (!input.Contains("-") && input.Contains("/")) separator = '/';
            bool result = GetDefaultCalendarDateFromString(input,
                out year,
                out month,
                out day,
                separator,
                inputculture,
                parseOrder);
            if (!result)
            {
                dt = default(DateTime);
                return false;
            }
            dt = ConvertFromCalendar(year, month, day, inputculture.Calendar);
            return true;
        }

        public static bool IsValidDate(string shamsiDate)
        {
            string[] strArray = shamsiDate.Split(new char[1]
            {
                '/'
            });
            if (strArray.Length != 3)
                return false;
            var num1 = Convert.ToInt32(strArray[0]);
            var num2 = Convert.ToInt32(strArray[1]);
            var num3 = Convert.ToInt32(strArray[2]);
            if (num1 < 0 || num1 > 2000 || (num2 < 0 || num2 > 12) || (num3 < 0 || num3 > 31))
                return false;
            if (num2 < 7)
                return true;
            if (num2 < 12 && num2 > 6)
                return num3 <= 30;
            return num2 == 12 && num3 <= 29;
        }
    }
}