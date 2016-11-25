#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#endregion

namespace Framework.Localizer
{

    [Serializable]
    [AttributeUsage(AttributeTargets.Field)]
    public class Info : Attribute
    {
        public Info(string baseculturename, CalendarTypes defaultcalendar)
        {
            if (baseculturename.Length > 2) baseculturename = baseculturename.Substring(0, 2);
            Culture = new CultureInfo(baseculturename);
            Calendar = defaultcalendar;
        }

        public CultureInfo Culture { get; private set; }
        public CalendarTypes Calendar { get; private set; }
    }

    [Serializable]
    public enum CalendarTypes
    {
        Persian,
        Gregorian,
        Islamic,
        Hebrew,
        Chinese
    }

    [Serializable]
    public enum DateTimeParseOrder
    {
        YMD,
        YDM,
        DMY,
        DYM,
        MYD,
        MDY,
        UnknownFormat
    }

    [Serializable]
    public enum Languages
    {
        [Info("fa", CalendarTypes.Persian)]
        Persian,
        [Info("en", CalendarTypes.Gregorian)]
        English,
        [Info("ar", CalendarTypes.Islamic)]
        Arabic,
        [Info("he", CalendarTypes.Hebrew)]
        Hebrew,
        [Info("zh", CalendarTypes.Chinese)]
        Chinese
    }


    [Serializable]
    public enum Direction
    {
        Invariant = 0,
        RTL = 1,
        LTR = 2
    }



    [Serializable]
    public enum GetCultureMode
    {
        Web = 0,
        Windows = 1
    }

    [Serializable]
    public enum MessageType
    {
        General,
        Info,
        Warning,
        Error,
        UnhandledException
    }

    [Serializable]
    public class Culture : IComparable
    {
        private readonly SortedList<string, Culture> _allInstances = new SortedList<string, Culture>();
        private readonly Languages _baselang;
        private readonly string _key;
        private Info _info = null;

        public Culture(string key, Languages baseLanguage)
        {
            this._key = key.ToLower().Trim();
            this._baselang = baseLanguage;
            if (!this._allInstances.ContainsKey(_key)) this._allInstances.Add(_key, this);
            else
            {
                throw new Exception("Cannot instatiate two cultures with same key!");
                //Check for only base language and base culture match?
            }
            FirstDayOfWeek = Baseculture.DateTimeFormat.FirstDayOfWeek;
            NumberFormat = (NumberFormatInfo)Baseculture.NumberFormat.Clone();
            DateTimeFormat = Baseculture.DateTimeFormat.FullDateTimePattern;
            DateFormat = Baseculture.DateTimeFormat.ShortDatePattern;
            TimeFormat = Baseculture.DateTimeFormat.ShortTimePattern;
            KeyboardLayoutId = Baseculture.KeyboardLayoutId;
            BaseName = Baseculture.Name;
            BaseEnglishName = Baseculture.EnglishName;
            BaseIsoKey = Baseculture.TwoLetterISOLanguageName;
            Calendar = Info.Calendar;
        }

        public NumberFormatInfo NumberFormat { get; set; }

        private Info Info
        {
            get
            {
                if (_info == null)
                {
                    var type = BaseLanguage.GetType();
                    var memInfo = type.GetMember(BaseLanguage.ToString()); //Danger!
                    var attributes = memInfo[0].GetCustomAttributes(typeof(Info), false);
                    _info = ((Info)attributes[0]);
                }
                return _info;
            }
        }

        private CultureInfo Baseculture
        {
            get
            {
                return Info.Culture;
            }
        }

        public string DateTimeFormat { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string DateSeparator
        {
            get
            {
                var dti = new DateTimeFormatInfo() { FullDateTimePattern = DateFormat };
                return dti.DateSeparator;
            }
            set
            {
                var dti = new DateTimeFormatInfo { FullDateTimePattern = DateTimeFormat, DateSeparator = value };
                DateTimeFormat = dti.FullDateTimePattern;

                dti = new DateTimeFormatInfo { LongDatePattern = DateFormat, DateSeparator = value };
                DateFormat = dti.LongDatePattern;

                dti = new DateTimeFormatInfo { LongTimePattern = TimeFormat, DateSeparator = value };
                TimeFormat = dti.LongTimePattern;
            }
        }
        public DayOfWeek FirstDayOfWeek { get; set; }

        public string Key
        {
            get
            {
                return _key;
            }
        }

        public int KeyboardLayoutId { get; set; }
        public string BaseName { get; private set; }
        public string BaseEnglishName { get; private set; }
        public string BaseIsoKey { get; private set; }


        public Direction Dir
        {
            get
            {
                return Baseculture.TextInfo.IsRightToLeft ? Direction.RTL : Direction.LTR;
            }
        }
        public string Align
        {
            get
            {
                return Baseculture.TextInfo.IsRightToLeft ? "Right" : "Left";
            }

        }
        public string InvAlign
        {
            get
            {
                return Baseculture.TextInfo.IsRightToLeft ? "Left" : "Right";
            }
        }
        public Direction InvDir
        {
            get
            {
                return Baseculture.TextInfo.IsRightToLeft ? Direction.LTR : Direction.RTL;
            }
        }
        public CalendarTypes Calendar { get; set; }

        public Languages BaseLanguage
        {
            get
            {
                return this._baselang;
            }
        }

        #region IComparable Members

        int IComparable.CompareTo(object obj)
        {
            var c = (Culture)obj;
            return Equals(this, obj as Culture) ? 0 : String.CompareOrdinal(this._key, c._key);
        }

        #endregion

        protected bool Equals(Culture other)
        {
            return string.Equals(this._key.ToLower(), other._key.ToLower()) && BaseLanguage == other.BaseLanguage;
            //&& Equals(this.BaseCulture.Name, other.BaseCulture.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Culture)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable BaseObjectGetHashCodeCallInGetHashCode
                int hashCode = base.GetHashCode();
                // ReSharper restore BaseObjectGetHashCodeCallInGetHashCode
                hashCode = (hashCode * 397) ^ this.Dir.GetHashCode();
                hashCode = (hashCode * 397) ^ this._baselang.GetHashCode();
                hashCode = (hashCode * 397) ^ (this._key != null ? this._key.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static implicit operator Languages(Culture m)
        {
            return m.BaseLanguage;
        }

        public static implicit operator string(Culture m)
        {
            return m.ToString();
        }

        public override string ToString()
        {
            return this._key;
        }

        public static Culture Parse(string str)
        {
            return SupportedCultures.Parse(str);
        }
    }

    public static class SupportedCultures
    {
        public static readonly Culture FaIr = new Culture("fa-ir", Languages.Persian) { DateFormat = "yyyy/MM/dd", DateTimeFormat = "yyyy/MM/dd HH:mm:ss", FirstDayOfWeek = DayOfWeek.Saturday, TimeFormat = "HH:mm:ss" };

        public static readonly Culture EnUs = new Culture("en-us", Languages.English) { DateFormat = "yyyy-MM-dd", DateTimeFormat = "yyyy-MM-dd HH:mm:ss", TimeFormat = "HH:mm:ss" };

        public static readonly Culture ArSa = new Culture("ar-sa", Languages.Arabic) { DateFormat = "yyyy MM dd", DateTimeFormat = "yyyy MM dd HH:mm:ss", TimeFormat = "HH:mm:ss" };

        public static Culture Parse(string str)
        {
            str = str.ToLower();
            Type type = typeof(SupportedCultures);

            foreach (
                var cul in
                    type.GetFields().Where(p => p.GetValue(null).GetType() == typeof(Culture)).Select(
                                                                                                       p =>
                                                                                                       (Culture)
                                                                                                       p.GetValue(null))
                        .Where(cul => cul.ToString().ToLower() == str))
            {
                return cul;
            }
            foreach (var p in type.GetFields().Where(p => p.Name.ToLower() == str))
            {
                return (Culture)p.GetValue(null);
            }
            if (str.Length > 2) str = str.Substring(0, 2);
            foreach (var p in type.GetFields())
            {
                if (p.GetValue(null).GetType() != typeof(Culture)) continue;
                var cul = (Culture)p.GetValue(null);
                var nm = cul.ToString().ToLower();
                if (nm.Length > 2) nm = nm.Substring(0, 2);
                if (nm == str) return cul;
            }
            foreach (var p in type.GetFields())
            {
                var nm = p.Name.ToLower();
                if (nm.Length > 2) nm = nm.Substring(0, 2);
                if (nm == str)
                    return (Culture)p.GetValue(null);
            }
            return null;
        }
    }

    [Serializable]
    public class Message
    {
        public string Body = "", Key;
        public MessageType MessageType = MessageType.General;
        public string Title = "";

        public Message(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("Key Cannot be empty!", "Key");
            this.Key = key;
        }
    }
}
