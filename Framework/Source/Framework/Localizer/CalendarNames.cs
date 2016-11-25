#region

using System;
using System.Globalization;

#endregion

namespace Framework.Localizer
{
    internal static class CalendarNames
    {
        private static readonly string[] GMonthEn =
            {
                "January", "February", "March", "April", "May", "June", "July",
                "August", "September", "October", "November", "December"
            };

        //private static  readonly string[] GMonthPe = { "ژانویه", "فبریه", "مارچ", "آپریل", "می", "ژون", "جولای", "آگوست", "سپتامبر", "اکتبر", "نوامبر", "دسامبر" };
        private static readonly string[] GMonthPe =
            {
                "ژانویه", "فوریه", "مارس", "آوریل", "مه", "ژوئن", "ژوئیه", "اوت",
                "سپتامبر", "اکتبر", "نوامبر", "دسامبر"
            };

        private static readonly string[] GMonthAr =
            {
                "يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو", "يوليو",
                "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"
            };

        private static readonly string[] PMonthPe =
            {
                "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر",
                "آبان", "آذر", "دی", "بهمن", "اسفند"
            };

        private static readonly string[] PMonthEn =
            {
                "Farvardin", "Ordibehesht", "Khordad", "Tir", "Mordad", "Shahrivar"
                , "Mehr", "Aban", "Azar", "Dey", "Bahman", "Esfand"
            };

        private static readonly string[] HMonthHe =
            {
                "נִיסָן", "אִיָּר", "סִיוָן", "תַּמּוּז", "אָב", "אֱלוּל",
                "תִּשׁרִי", "מַרְחֶשְׁוָן", "כִּסְלֵו", "טֵבֵת", "שְׁבָט", "אֲדָר א׳", "אדר"
            };

        private static readonly string[] HMonthEn =
            {
                "Nisan", "Iyyar", "Siwan", "Tammuz", "Av", "Elul", "Tishri",
                "Marcheshvan", "Kislew", "Tebeth", "Shevat", "Adar I", "Adar II"
            };

        private static readonly string[] HMonthAr =
            {
                "نِيسَان", "إِيَّار", "سِيوَان", "تَمُّوز", "آب", "أَيْلُول",
                "تِشريه", "حِشْوان", "كِسْلِو", "طِيبِيت", "شبَاط", "اَذَارُ الأَوَّل", "أذار الثاني"
            };

        private static readonly string[] HMonthPe =
            {
                "نیسان", "ایار", "سیوان", "تموز", "آو", "ایلول", "تیشری", "حشوان",
                "كیسلو", "طوت", "شواط", "آدار اول", "آدار دوم"
            };

        private static readonly string[] CMonthCh =
            {
                "正月", "杏月", "桃月", "梅月", "榴月", "荷月", "蘭月", "桂月", "菊月", "良月", "冬月",
                "臘月"
            };

        private static readonly string[] CMonthEN =
            {
                "Primens", "Apricomens", "Peacimens", "Plumens", "Guavamens",
                "Lotumens", "Orchimens", "Osmanthumens", "Chrysanthemens", "Benimens", "Hiemens", "Lamens"
            };

        private static readonly string[] IMonthArShort =
            {
                "محرّم", "صفر", "ربيع الأول", "ربيع الثاني", "جمادى الأول",
                "جمادى الثاني", "رجب", "شعبان", "رمضان", "شوّال", "ذو القعدة", "ذو الحجة"
            };

        private static readonly string[] IMonthArLong =
            {
                "محرّم الحرام", "صفر المظفر", "ربيع الأول", "ربيع الثاني",
                "جمادى الأول", "جمادى الثاني", "رجب المرجب", "شعبان المعظم", "رمضان المبارک", "شوّال المکرم",
                "ذو القعدة",
                "ذو الحجة"
            };

        private static readonly string[] IMonthEnShort =
            {
                "Muharram", "Safar", "Rabī' I", "Rabī' II", "Jumādā I",
                "Jumādā II", "Rajab", "Sha'aban", "Ramadan", "Shawwal", "Dhu al-Qi'dah", "Dhu al-Hijjah"
            };

        private static readonly string[] IMonthEnLong =
            {
                "Muḥarram al Ḥaram", "Ṣafar al Muzaffar", "Rabi' al-awwal",
                "Rabi' al-thani", "Jumada al-awwal", "Jumada al-thani", "Rajab al Murajab", "Sha'abān al Moazam",
                "Ramaḍān al Mubarak", "Shawwal al Mukarram", "Dhu al-Qi'dah", "Dhu al-Hijjah"
            };

        private static readonly string[] IMonthPe =
            {
                "محرم", "صفر", "ربیع الاول", "ربیع الثانی", "جمادی الاول",
                "جمادی الثانی", "رجب", "شعبان", "رمضان", "شوال", "ذی القعده", "ذی الحجه"
            };

        private static readonly string[] pdays = {"یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"};

        private static readonly string[] hdays =
            {
                "יום רִאשׁוֹן", "יום שֵׁנִי", "יום שְׁלִישִׁי", "יום רְבִיעִי",
                "יום חֲמִישִׁי", "יום שִׁשִּׁי", "שבת"
            };

        private static readonly string[] hdayssh = {"ום א", "יום ב׳", "יום ג׳", "יום ד׳", "יום ה׳", "יום ו׳", "יום ש׳"};

        private static readonly string[] idays =
            {
                "الأحد", "الإثنين", "الثُّلَاثاء", "الأَرْبِعاء", "الخَمِيس",
                "الجُمُعَة", "السَّبْت"
            };

        private static readonly string[] gdays =
            {
                "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday",
                "Saturday"
            };

        private static readonly string[] gampm =
            {
                "AM", "PM"
            };

        private static readonly string[] pampm =
            {
                "صبح", "عصر"
            };

        private static readonly string[] aampm =
            {
                "صباح", "مساء"
            };

        private static readonly string[] hampm =
            {
                "AM", "PM"
            };

        private static readonly string[] campm =
            {
                "AM", "PM"
            };

        private static readonly string[] cdays = {"星期天", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"};

        public static string GetMonthName(int MonthNumber,
                                          CalendarTypes CalendarType,
                                          Languages Language,
                                          bool Shorten = false)
        {
            switch (CalendarType)
            {
                case CalendarTypes.Persian:
                    switch (Language)
                    {
                        case Languages.Persian:
                        case Languages.Arabic:
                            return ReturnPersianMonthNamePersian(MonthNumber);
                        default:
                            return ReturnPersianMonthNameEnglish(MonthNumber, Shorten);
                    }
                case CalendarTypes.Gregorian:
                    switch (Language)
                    {
                        case Languages.Persian:
                            return ReturnGregorianMonthNamePersian(MonthNumber, false);
                        case Languages.Arabic:
                            return GMonthAr[MonthNumber - 1];
                        default:
                            return ReturnGregorianMonthNameEnglish(MonthNumber, Shorten);
                    }
                case CalendarTypes.Islamic:
                    switch (Language)
                    {
                        case Languages.Persian:
                            return IMonthPe[MonthNumber - 1];
                        case Languages.Arabic:
                            if (Shorten)
                            {
                                return IMonthArShort[MonthNumber - 1];
                            }
                            else
                            {
                                return IMonthArLong[MonthNumber - 1];
                            }
                        default:
                            if (Shorten)
                            {
                                return IMonthEnShort[MonthNumber - 1];
                            }
                            else
                            {
                                return IMonthEnLong[MonthNumber - 1];
                            }
                    }
                case CalendarTypes.Hebrew:
                    switch (Language)
                    {
                        case Languages.Persian:
                            return HMonthPe[MonthNumber - 1];
                        case Languages.Arabic:
                            return HMonthAr[MonthNumber - 1];
                        case Languages.Hebrew:
                            return HMonthHe[MonthNumber - 1];
                        default:
                            return HMonthEn[MonthNumber - 1];
                    }
                case CalendarTypes.Chinese:
                    switch (Language)
                    {
                        case Languages.Chinese:
                            return CMonthCh[MonthNumber - 1];
                        default:
                            return CMonthEN[MonthNumber - 1];
                    }
            }
            return "";
        }

        public static string GetDayOfWeekName(DayOfWeek DayOfTheWeek, Languages Language, bool Shorten = false)
        {
            int dnumber = Int32.Parse(DayOfTheWeek.ToString("d")) + 1;
            return GetDayOfWeekName(dnumber, Language, Shorten);
        }

        public static string GetAMPM(bool isAM, Languages language)
        {
            switch (language)
            {
                case Languages.Arabic:
                    return isAM ? aampm[0] : aampm[1];
                case Languages.English:
                    return isAM ? gampm[0] : gampm[1];
                case Languages.Persian:
                    return isAM ? pampm[0] : pampm[1];
                case Languages.Hebrew:
                    return isAM ? hampm[0] : hampm[1];
                case Languages.Chinese:
                    return isAM ? campm[0] : campm[1];
            }
            return isAM ? gampm[0] : gampm[1];
        }

        public static string GetDayOfWeekName(int GregorianDayNumber, Languages Language, bool shorten = false)
        {
            int dnumber = GregorianDayNumber - 1;
            switch (Language)
            {
                case Languages.Arabic:
                    return idays[dnumber];
                case Languages.Chinese:
                    return cdays[dnumber];
                case Languages.Hebrew:
                    if (shorten)
                    {
                        return hdayssh[dnumber];
                    }
                    else
                    {
                        return hdays[dnumber];
                    }
                case Languages.Persian:
                    if (shorten)
                    {
                        return pdays[dnumber][0].ToString();
                    }
                    else
                    {
                        return pdays[dnumber];
                    }
                default:
                    if (shorten)
                    {
                        return gdays[dnumber].Substring(0, 3);
                    }
                    else
                    {
                        return gdays[dnumber];
                    }
            }
        }


        public static DateTime GetDateTime(int year,
                                           int month,
                                           int day,
                                           int hour,
                                           int minute,
                                           int second,
                                           int millisecond,
                                           CalendarTypes CalendarType)
        {
            return GetCalendar(CalendarType).ToDateTime(year, month, day, hour, minute, second, millisecond);
        }

        public static Calendar GetCalendar(CalendarTypes calendartype)
        {
            Calendar cal;
            switch (calendartype)
            {
                case CalendarTypes.Chinese:
                    cal = new ChineseLunisolarCalendar();
                    break;
                case CalendarTypes.Gregorian:
                    cal = new GregorianCalendar();
                    break;
                case CalendarTypes.Hebrew:
                    cal = new HebrewCalendar();
                    break;
                case CalendarTypes.Islamic:
                    cal = new HijriCalendar();
                    break;
                case CalendarTypes.Persian:
                    cal = new PersianCalendar();
                    break;
                default:
                    throw new Exception("Calendar not Supported!");
            }
            return cal;
        }

        public static void FromDateTime(DateTime dt,
                                        CalendarTypes CalendarType,
                                        out int year,
                                        out int month,
                                        out int day)
        {
            int t;
            FromDateTime(dt, CalendarType, out year, out month, out day, out t, out t, out t, out t);
        }

        public static void FromDateTime(DateTime dt,
                                        CalendarTypes CalendarType,
                                        out int year,
                                        out int month,
                                        out int day,
                                        out int hour,
                                        out int minute,
                                        out int second,
                                        out int millisec)
        {
            var cal = GetCalendar(CalendarType);
            year = cal.GetYear(dt);
            month = cal.GetMonth(dt);
            day = cal.GetDayOfMonth(dt);
            hour = cal.GetHour(dt);
            minute = cal.GetMinute(dt);
            second = cal.GetSecond(dt);
            millisec = (int)cal.GetMilliseconds(dt);
        }

        public static string FormattedString(int iyear,
                                             int imonth,
                                             int iday,
                                             int ihour,
                                             int imin,
                                             int isec,
                                             int imilli,
                                             DayOfWeek idow,
                                             string imonthname,
                                             string imonthshort,
                                             string idowname,
                                             string idowshort,
                                             string iamppm,
                                             string format)
        {
            string year4 = iyear.ToString("0000");
            string year2 = year4.Substring(2, 2);
            string year1 = iyear.ToString();
            string month2 = imonth.ToString("00");
            string month1 = imonth.ToString();
            string day2 = iday.ToString("00");
            string day1 = iday.ToString();
            string monthname = imonthname;
            string dayname = idowname;
            string houre2 = ihour.ToString("00");
            string houre1 = ihour.ToString();
            int h = ihour;
            // ReSharper disable NotAccessedVariable
            // ReSharper disable RedundantAssignment
            bool ispm;
            if ((h >= 12) || (h == 0))
            {
                switch (h)
                {
                    case 24:
                        h = 12;
                        ispm = false;
                        break;
                    case 12:
                        h = 12;
                        ispm = true;
                        break;
                    case 0:
                        h = 12;
                        ispm = false;
                        break;
                    default:
                        h = h - 12;
                        ispm = true;
                        break;
                }
            }
            else ispm = false;
            // ReSharper restore RedundantAssignment
            // ReSharper restore NotAccessedVariable
            string hour2 = h.ToString("00");
            string hour1 = h.ToString();
            string minute2 = imin.ToString("00");
            string minute1 = imin.ToString();
            string second2 = isec.ToString("00");
            string second1 = isec.ToString();
            string milli3 = imilli.ToString("000");
            string milli1 = imilli.ToString();
            // ReSharper disable NotAccessedVariable
            // ReSharper disable JoinDeclarationAndInitializer
            string ttE, tE, tte, te, ttp2, ttp, tp2, tp, tt, t, ttp3, tp3;
            // ReSharper restore JoinDeclarationAndInitializer
            // ReSharper restore NotAccessedVariable
            // ReSharper disable RedundantAssignment
            ttE = iamppm;
            tE = iamppm;
            tte = iamppm;
            te = iamppm;
            ttp3 = iamppm;
            tp3 = iamppm;
            ttp2 = iamppm;
            ttp = iamppm;
            tp2 = iamppm;
            tp = iamppm;
            tt = iamppm;
            t = iamppm;
            // ReSharper restore RedundantAssignment
            /* ddd, dd, d
             * MMMM, MM, M
             * yyyy, yy, y
             * HH, H
             * hh, h
             * mm, m
             * ss, s
             * iii, i
             * ttE, tE, tte, te
             * ttP3, ttP2, tP3, tP2, ttP, tP, tt, t
             */
            format = format.Replace("dddd", "dddd");
            format = format.Replace("DDDD", "dddd");
            format = format.Replace("DDD", "ddd");
            format = format.Replace("DD", "dd");

            format = format.Replace("Y", "y");
            format = format.Replace("S", "s");
            format = format.Replace("I", "i");
            format = format.Replace("T", "t");
            format = format.Replace("tp", "tP");
            format = format.Replace("tP1", "tP");
            //--------------------------------------------------------
            format = format.Replace("dddd", "*1");
            format = format.Replace("ddd","*2");
            format = format.Replace("dd","*3");
            format = format.Replace("d","*4");
            format = format.Replace("D","*5");
            format = format.Replace("MMMM","*6");
            format = format.Replace("MMM","*7");
            format = format.Replace("MM","*8");
            format = format.Replace("M","*9");
            format = format.Replace("yyyy","*a");
            format = format.Replace("yy","*b");
            format = format.Replace("y","*c");
            format = format.Replace("HH","*d");
            format = format.Replace("H","*e");
            format = format.Replace("hh","*f");
            format = format.Replace("h","*g");
            format = format.Replace("mm","*h");
            format = format.Replace("m","*i");
            format = format.Replace("ss","*j");
            format = format.Replace("s","*k");
            format = format.Replace("iii","*l");
            format = format.Replace("ii","*m");
            format = format.Replace("i","*n");
            format = format.Replace("ttE","*o");
            format = format.Replace("tte","*p");
            format = format.Replace("tE","*q");
            format = format.Replace("te","*r");
            format = format.Replace("ttP3","*s");
            format = format.Replace("ttP2","*t");
            format = format.Replace("tP3","*u");
            format = format.Replace("tP2","*v");
            format = format.Replace("ttP","*w");
            format = format.Replace("tP","*x");
            format = format.Replace("tt","*y");
            format = format.Replace("t","*z");
            //------------------------------------------------
            format = format.Replace("*1", dayname);
            format = format.Replace("*2", idowshort);
            format = format.Replace("*3", day2);
            format = format.Replace("*4", day1);
            format = format.Replace("*5", idowshort.Substring(0, 1));
            format = format.Replace("*6", monthname);
            format = format.Replace("*7", imonthshort);
            format = format.Replace("*8", month2);
            format = format.Replace("*9", month1);
            format = format.Replace("*a", year4);
            format = format.Replace("*b", year2);
            format = format.Replace("*c", year1);
            format = format.Replace("*d", houre2);
            format = format.Replace("*e", houre1);
            format = format.Replace("*f", hour2);
            format = format.Replace("*g", hour1);
            format = format.Replace("*h", minute2);
            format = format.Replace("*i", minute1);
            format = format.Replace("*j", second2);
            format = format.Replace("*k", second1);
            format = format.Replace("*l", milli3);
            format = format.Replace("*m", milli3);
            format = format.Replace("*n", milli1);
            format = format.Replace("*o", ttE);
            format = format.Replace("*p", tte);
            format = format.Replace("*q", ttE.Substring(0, 1));
            format = format.Replace("*r", tte.Substring(0, 1));
            format = format.Replace("*s", ttp3);
            format = format.Replace("*t", ttp2);
            format = format.Replace("*u", tp3);
            format = format.Replace("*v", tp2);
            format = format.Replace("*w", ttp);
            format = format.Replace("*x", tp);
            format = format.Replace("*y", tt);
            format = format.Replace("*z", t);
            return format;
        }

        #region " Private Functions for Names "

        private static string ReturnGregorianMonthNameEnglish(int Month, bool Short)
        {
            if ((Month < 1) || (Month > 12)) return "";
            string str = GMonthEn[Month - 1];
            if (Short) str = str.Substring(0, 3);
            return str;
        }

        private static string ReturnPersianMonthNameEnglish(int Month, bool Short)
        {
            if ((Month < 1) || (Month > 12)) return "";
            string str = PMonthEn[Month - 1];
            if (Short) str = str.Substring(0, 3);
            return str;
        }

        private static string ReturnGregorianMonthNamePersian(int Month, bool Short)
        {
            if ((Month < 1) || (Month > 12)) return "";
            string str = GMonthPe[Month - 1];
            if (Short) str = str.Substring(0, 3);
            return str;
        }

        private static string ReturnPersianMonthNamePersian(int Month)
        {
            if ((Month < 1) || (Month > 12)) return "";
            string str = PMonthPe[Month - 1];
            return str;
        }

        #endregion

        public static int GetDaysInMonth(int month, int year, CalendarTypes calendarType)
        {
            var cal = GetCalendar(calendarType);
            return cal.GetDaysInMonth(year, month);
        }
        public static int GetMonthsInYear(int year, CalendarTypes calendarType)
        {
            var cal = GetCalendar(calendarType);
            return cal.GetMonthsInYear(year);
        }

        public static int GetDaysInYear(int year, CalendarTypes calendarType)
        {
            var cal = GetCalendar(calendarType);
            return cal.GetDaysInYear(year);
        }

        public static bool GetLeapYear(int year, CalendarTypes calendarType)
        {
            var cal = GetCalendar(calendarType);
            return cal.IsLeapYear(year);
        }
    }
}
