using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Framework.Utility
{
    public static class Extention
    {
        public static bool ColumnExists(this IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i) == columnName)
                {
                    return true;
                }
            }

            return false;
        }

        public static string CalculateMD5Hash(this string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string EncodeTo64(this string toEncode)
        {

            byte[] toEncodeAsBytes

                  = Encoding.UTF8.GetBytes(toEncode);

            string returnValue

                  = Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }

        public static DateTime SafeMmtpDateTime(this object d)
        {
            try
            {
                if (d != null)
                {
                    var y = Convert.ToInt32(d.ToString().Substring(0, 4));
                    var m = Convert.ToInt32(d.ToString().Substring(4, 2));
                    var day = Convert.ToInt32(d.ToString().Substring(6, 2));
                    var hour = Convert.ToInt32(d.ToString().Substring(8, 2));
                    var min = Convert.ToInt32(d.ToString().Substring(10, 2));
                    var sec = Convert.ToInt32(d.ToString().Substring(12, 2));

                    DateTime dt = new DateTime(y, m, day, hour, min, sec);
                    //CultureInfo enUS = new CultureInfo("en-US");
                    ////d.ToString("MMMM dd, yyyy");
                    //DateTime.TryParseExact(d.ToString(), "yyyyMMddhhmmss", enUS, DateTimeStyles.None, out dt);
                    //DateTime.TryParse(d.ToString(), out dt);
                    return dt;
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
            return DateTime.MinValue;
        }

        public static TimeSpan SafeMmtpTime(this object d)
        {
            try
            {
                if (d != null)
                {
                    TimeSpan time;
                    string strTime;
                    if (d.ToString().Length < 6)
                    {
                        strTime = "0" + d.ToString();
                    }
                    else
                    {
                        strTime = d.ToString();
                    }
                    var hour = strTime.Substring(0, 2);
                    var min = strTime.Substring(2, 2);
                    var sec = strTime.Substring(4, 2);
                    time = TimeSpan.Parse(hour + ":" + min + ":" + sec);

                    return time;
                }
            }
            catch
            {
                return TimeSpan.MinValue;
            }
            return TimeSpan.MinValue;
        }


        public static DateTime SafeMmtpDate(this object d)
        {
            try
            {
                if (d != null)
                {
                    DateTime dt;
                    var y = Convert.ToInt32(d.ToString().Substring(0, 4));
                    var m = Convert.ToInt32(d.ToString().Substring(4, 2));
                    var day = Convert.ToInt32(d.ToString().Substring(6, 2));
                    dt = new DateTime(y, m, day);
                    return dt;
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
            return DateTime.MinValue;
        }

        public static DateTime RemoveMinutes(this DateTime dt)
        {
            return Convert.ToDateTime(dt.ToShortDateString());
        }

        public static DateTime ConvertJalaliToMiladi(this string persianDate)
        {
            try
            {
                persianDate = persianDate.PersianNumberToLatin();

                if (string.IsNullOrEmpty(persianDate))
                {
                    return DateTime.MinValue;
                }
                persianDate = persianDate.Trim();
                var s = persianDate.Split(' ');
                string date;
                TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0);
                if (s.Length == 2)
                {
                    timeSpan = TimeSpan.Parse(s[1]);
                }
                date = s[0];

                Match match = Regex.Match(date,
                                     @"(?'Year'(^[1-4]\d{3})|(\d{2}))[/-:](((?'Month'0?[1-6])\/((?'Day'(3[0-1])|([1-2][0-9])|(0?[1-9])))|((?'Month'1[0-2]|(0?[7-9]))\/(?'Day'30|([1-2][0-9])|(0?[1-9])))))$");
                if (!match.Success)
                {
                    throw new Exception("InvalidPersianDate");
                }
                var yearGroup = match.Groups["Year"].ToString();
                if (yearGroup.Length == 2)
                {
                    yearGroup = string.Format("13{0}", yearGroup);
                }
                int year = yearGroup.SafeInt(0);
                int month = match.Groups["Month"].SafeInt(0);
                int day = match.Groups["Day"].SafeInt(0);
                PersianCalendar calendar = new PersianCalendar();
                try
                {
                    return calendar.ToDateTime(year, month, day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                }
                catch (Exception exDate)
                {
                    if (exDate.Message == "Day must be between 1 and 29 for month 12.\r\nParameter name: day")
                        return calendar.ToDateTime(year, month, day - 1, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                    else
                        throw new Exception("InvalidPersianDate");
                }

            }
            catch (Exception)
            {
                throw new Exception("InvalidPersianDate");
            }
        }

        public static DateTime ConvertJalaliToMiladi(this string persianDate, string time)
        {
            try
            {
                if (string.IsNullOrEmpty(persianDate))
                {
                    return DateTime.MinValue;
                }
                persianDate = persianDate.Trim();
                persianDate = persianDate.PersianNumberToLatin();

                var s = persianDate.Split(' ');
                string date;
                TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0);
                if (s.Length == 2)
                {
                    timeSpan = TimeSpan.Parse(s[1]);
                }
                date = s[0];

                Match match = Regex.Match(date,
                                     @"(?'Year'(^[1-4]\d{3})|(\d{2}))[/-:](((?'Month'0?[1-6])\/((?'Day'(3[0-1])|([1-2][0-9])|(0?[1-9])))|((?'Month'1[0-2]|(0?[7-9]))\/(?'Day'30|([1-2][0-9])|(0?[1-9])))))$");
                if (!match.Success)
                {
                    throw new Exception("InvalidPersianDate");
                }
                var yearGroup = match.Groups["Year"].ToString();
                if (yearGroup.Length == 2)
                {
                    yearGroup = string.Format("13{0}", yearGroup);
                }
                int year = yearGroup.SafeInt(0);
                int month = match.Groups["Month"].SafeInt(0);
                int day = match.Groups["Day"].SafeInt(0);
                PersianCalendar calendar = new PersianCalendar();
                try
                {
                    return calendar.ToDateTime(year, month, day, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                }
                catch (Exception exDate)
                {
                    if (exDate.Message == "Day must be between 1 and 29 for month 12.\r\nParameter name: day")
                        return calendar.ToDateTime(year, month, day - 1, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                    else
                        return DateTime.MinValue;
                }

            }
            catch (Exception)
            {
                throw new Exception("InvalidPersianDate");
            }
        }

        public static string ConvertMiladiToJalali(this DateTime date, bool showTime)
        {
            if(date == null || (date <= DateTime.MinValue))
            {
                return "";
            }
            PersianCalendar obj = new PersianCalendar();
            //if (date <= DateTime.MinValue)
            //{
            //    date = new DateTime(622, 3, 21);
            //}
            string dayStr = "0";
            string monthStr = "0";
            int day = obj.GetDayOfMonth(date);
            int month = obj.GetMonth(date);
            int year = obj.GetYear(date);
            int hour = obj.GetHour(date);
            int minute = obj.GetMinute(date);
            int second = obj.GetSecond(date);
            dayStr = obj.GetDayOfMonth(date).CompareTo(10) >= 0 ? day.ToString() : "0" + day;
            monthStr = obj.GetMonth(date).CompareTo(10) >= 0 ? month.ToString() : "0" + month;
            if (showTime)
            {
                return string.Format("{0}/{1}/{2} {3}:{4}:{5}", year, monthStr, dayStr, hour, minute, second);
            }

            return string.Format("{0}/{1}/{2}", year, monthStr, dayStr);
        }
        public static string ConvertMiladiToJalali(this DateTime date)
        {
            return ConvertMiladiToJalali(date, false);
        }
        public static string ConvertMiladiToJalali(this DateTime? date)
        {
            return date == null ? "" : ConvertMiladiToJalali((DateTime)date, false);
        }

        public static bool IsValidPersianDate(this String shamsiDate)
        {
            const bool result = false;

            if (!string.IsNullOrEmpty(shamsiDate))
            {
                string[] Arr = shamsiDate.Split('/');
                int Year = Convert.ToInt16(Arr[0]);
                int Month = Convert.ToInt16(Arr[1]);
                int Day = Convert.ToInt16(Arr[2]);

                //Check Year
                if ((Year < 0) || (Year > 2000))
                {
                    //Result = false;
                    return false;
                }
                //Check Month
                if ((Month < 0) || (Month > 12))
                {
                    //Result = false;
                    return false;
                }
                //Check Day
                if ((Day < 0) || (Day > 31))
                {
                    // Result = false;
                    return false;
                }
                //Check Valid Day With Month
                if (Month < 7)
                {
                    //Result = true;
                    return true;
                }
                if ((Month < 12) && (Month > 6))
                {
                    if (Day > 30)
                    {
                        //Result = false;
                        return false;
                    }
                    else
                    {
                        // Result = true;
                        return true;
                    }
                }
                if (Month == 12)
                {
                    if (Day > 29)
                    {
                        //Result = false;
                        return false;
                    }
                    else
                    {
                        // Result = true;
                        return true;
                    }
                }
            }
            return result;

        }

        public static int GetPersianMonth(this DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetMonth(date);
        }

        public static int GetPersianDayOfMonth(this DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetDayOfMonth(date);
        }

        public static int GetPersianYear(this DateTime date)
        {
            PersianCalendar calendar = new PersianCalendar();
            return calendar.GetYear(date);
        }

        //TODO :Replace with regular expression
        public static DateTime AddMonths(this string jalaliDate, int period)
        {
            jalaliDate = jalaliDate.Replace(@"‏", string.Empty);
            string[] parts = jalaliDate.Split('/');
            if (parts.Length == 3)
            {
                int month = Convert.ToInt32(parts[1]);
                int newMonth = month + period;
                int newYear = Convert.ToInt32(parts[0]);
                if (newMonth <= 0)
                {
                    newYear--;
                    newMonth += 12;
                }
                else if (newMonth > 12)
                {
                    newYear++;
                    newMonth = ((newMonth % 12) == 0) ? 12 : (newMonth % 12);
                }
                if (newMonth > 6 && newMonth < 12 && parts[2] == "31")
                {
                    parts[2] = "30";
                }
                else if (newMonth == 12 && Convert.ToInt32(parts[2]) >= 30)
                {
                    parts[2] = "29";
                }
                return ConvertJalaliToMiladi(newYear + "/" + newMonth + "/" + parts[2]);
            }
            return DateTime.Now;
        }

        public static DateTime SafeMinDate(this DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                date = new DateTime(1907, 1, 1);
            }
            return date;
        }

        public static bool SafeBool(this object i)
        {
            bool b = false;
            if (i != null)
            {
                bool.TryParse(i.SafeString(), out b);
            }
            return b;
        }

        public static int SafeBoolToInt(this object i)
        {
            var b = SafeBool(i);
            return b ? 1 : 0;
        }

        public static T ConvertToValue<T>(Type type, string value)
        {
            return (T)ConvertToValue(type, value);
        }

        public static object ConvertToValue(Type type, string value)
        {
            if (type == typeof(DateTime))
                try
                {
                    return value.SafeString().ConvertJalaliToMiladi().SafeDateTime();
                }
                catch
                {
                    return value.SafeString().SafeDateTime();
                }

            if (type == typeof(bool) || type == typeof(bool?))
                return value.SafeString().SafeBool();

            if (type == typeof(float) || type == typeof(float?))
                return value.SafeString().SafeFloat();

            if (type == typeof(decimal) || type == typeof(decimal?))
                return value.SafeString().SafeDecimal();

            if (type == typeof(long) || type == typeof(long?))
                return value.SafeString().SafeLong();

            if (type == typeof(double) || type == typeof(double?))
                return value.SafeString().SafeDouble();

            if (type == typeof(int) || type == typeof(int?))
                return value.SafeString().SafeInt();

            if (type == typeof(short) || type == typeof(short?))
                return value.SafeString().SafeInt16();

            if (type == typeof(byte) || type == typeof(byte?))
                return value.SafeString().SafeByte();

            if (type == typeof(string))
                return value.SafeString().SafePersianEncode().Trim();

            return value;
        }

        public static byte SafeByte(this object i)
        {
            byte val = 0;
            if (i != null)
            {
                byte.TryParse(i.SafeString().Split('.')[0], out val);
            }

            return val;
        }

        public static int SafeInt(this object i, int exceptionValue)
        {
            if (i != null)
            {
                int.TryParse(i.SafeString().Split('.')[0], out exceptionValue);
            }

            return exceptionValue;
        }
        public static int SafeInt(this object i)
        {
            return SafeInt(i, -1);
        }

        public static string PersianNumberToLatin(this string number)
        {
            string s = number;
            s =
                s.Replace("\u06F0", "0").Replace("\u06F1", "1").Replace("\u06F2", "2").Replace("\u06F3", "3").Replace(
                    "\u06F4", "4").Replace("\u06F5", "5").Replace("\u06F6", "6").Replace("\u06F7", "7").Replace(
                    "\u06F8", "8").Replace("\u06F9", "9");
            return s;
        }

        private static string[] formats = new[]
            {
                "MM/dd/yyyy",
                "MM/dd/yyyy HH",
                "MM/dd/yyyy H:mm",
                "MM/dd/yyyy HH:mm",
                "MM/dd/yyyy H.mm",
                "MM/dd/yyyy HH.mm",
                "MM/dd/yyyy HH:mm:ss",
                "M/dd/yyyy H:mm:ss tt",
                "M/dd/yyyy H:mm:ss"        
            };

        public static DateTime ParseDate(string input)
        {
            return DateTime.ParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static string SafeTrim(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.Trim();
            }

            return null;
        }

        public static string SafeString(this object i)
        {

            if (i != null)
            {
                return i.ToString();
            }

            return null;
        }

        public static string SafeString(this object i, bool isEmpty)
        {

            if (i != null)
            {
                return i.ToString();
            }

            return string.Empty;
        }
        public static DateTime SafeDateTime(this object d)
        {
            if (d != null)
            {
                DateTime dt;
                DateTime.TryParse(d.SafeString(), out dt);
                return dt;
            }
            return new DateTime(1907, 1, 1);
        }

        public static TimeSpan SafeTime(this object d)
        {
            if (d != null)
            {
                TimeSpan dt;
                TimeSpan.TryParse(d.SafeString(), out dt);
                return dt;
            }
            return TimeSpan.MinValue;
        }

        public static bool SafeBoolean(this object d)
        {
            if (d != null)
            {
                bool dt;
                Boolean.TryParse(d.SafeString(), out dt);
                return dt;
            }
            return new bool();
        }

        public static Guid SafeGuid(this object d)
        {
            if (d != null)
            {
                Guid g;
                Guid.TryParse(d.ToString(), out g);
                return g;
            }
            return Guid.Empty;
        }

        public static string SafePersianEncode(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            str = str.Replace("ي", "ی");
            str = str.Replace("ك", "ک");
            return str.Trim();
        }
        public static string SafeSqlSingleQuotes(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            str = str.Replace("'", "''");
            return str;
        }

        public static string ReverseString(this string str)
        {
            return new string(str.ToCharArray().Reverse().ToArray());
        }

        public static string RemoveNoise(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return str.Replace("‏", "");

        }

        public static string SafeSqlDateTime(this object dateTime)
        {
            if (dateTime is DateTime)
                return ((DateTime)dateTime).ToString("yyyy/MM/dd HH:mm:ss");
            return null;
        }

        public static long SafeLong(this object i)
        {
            return SafeLong(i, 0);
        }

        public static long SafeLong(this object i, long exceptionValue)
        {

            if (i == null)
            {
                return exceptionValue;
            }
            Int64.TryParse(i.SafeDouble().SafeString(), out exceptionValue);
            return exceptionValue;
        }

        public static float SafeFloat(this object i)
        {
            float id;
            float.TryParse(i.SafeString(), out id);
            return id;
        }

        public static double SafeDouble(this object i)
        {
            double id;
            double.TryParse(i.SafeString(), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out id);
            return id;
        }

        public static double SafeDouble(this object i, double exceptionValue)
        {
            if (i != null)
            {
                double.TryParse(i.SafeString().Split('.')[0], out exceptionValue);
            }
            return exceptionValue;
        }

        public static Int16 SafeInt16(this object i)
        {
            Int16 id;
            Int16.TryParse(i.SafeString(), out id);
            return id;
        }
        public static decimal SafeDecimal(this object i)
        {
            decimal id;
            decimal.TryParse(i.SafeString(), out id);
            return id;
        }

        public static TValue SafeDictionary<TKey, TValue>(this Dictionary<TKey, TValue> input, TKey key, TValue ifNotFound = default(TValue))
        {
            TValue val;
            if (input.TryGetValue(key, out val))
            {
                return val;
            }

            return ifNotFound;
        }
        public static object SafeReader(this IDataReader reader, string name)
        {
            if (reader != null)
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.GetName(i).Equals(name, StringComparison.InvariantCultureIgnoreCase))
                        return reader[name];
                }
            }

            return null;
        }

        public static object SafeReader(this IDataRecord reader, string name)
        {
            if (reader == null)
                return null;
            for (var i = 0; i < reader.FieldCount; ++i)
            {
                if (reader.GetName(i).Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return reader[name];
            }
            return null;
        }

        public static XElement XmlElementToXelement(this XmlElement e)
        {
            return XElement.Parse(e.OuterXml);
        }


        public static IEnumerable<T[]> GetBlocks<T>(
              this IEnumerable<T> source, int blockSize)
        {
            List<T> list = new List<T>(blockSize);
            foreach (T item in source)
            {
                list.Add(item);
                if (list.Count == blockSize)
                {
                    yield return list.ToArray();
                    list.Clear();
                }
            }
            if (list.Count > 0)
            {
                yield return list.ToArray();
            }
        }

        public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(
                    this IEnumerable<TValue> values, Int32 chunkSize)
        {
            using (var enumerator = values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    yield return GetChunk(enumerator, chunkSize).ToList();
                }
            }
        }
        private static IEnumerable<T> GetChunk<T>(
                         IEnumerator<T> enumerator,
                         int chunkSize)
        {
            do
            {
                yield return enumerator.Current;
            } while (--chunkSize > 0 && enumerator.MoveNext());
        }


        public static string ToRightISIN(this string isin)
        {
            return isin.Replace("IRO", "IRR").Replace("0001", "0101");
        }

        public static string ToStockISIN(this string isin)
        {
            return isin.Replace("IRR", "IRO").Replace("0101", "0001");
        }


        public static string GetDateString(this DateTime dt)
        {
            return String.Format("{0:yyyy-MM-dd}", dt);
        }

        public static DataTable GetItemsAsDictionary<T>(this IEnumerable<T> source)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Key", typeof(string));
            table.Columns.Add("Value", typeof(string));
            foreach (var item in source)
            {
                if (item != null)
                {
                    table.Rows.Add(item.ToString(), item.ToString());
                }
            }
            return table;
        }
        public static string ToFirstMarketISIN(this string isin)
        {
            if (isin.EndsWith("02"))
            {
                return isin.Replace("02", "01");
            }
            if (isin.EndsWith("03"))
            {
                return isin.Replace("03", "01");
            }
            if (isin.EndsWith("A2"))
            {
                return isin.Replace("A2", "A1");
            }
            if (isin.EndsWith("C2"))
            {
                return isin.Replace("C2", "C1");
            }
            if (isin.StartsWith("IRB") && isin.EndsWith("12"))
            {
                return isin.Replace("12", "11");
            }
            return isin;
        }

        public static string ToBrief(this string text, int maximumLength = 50)
        {
            maximumLength -= 3;
            if (text.Length < maximumLength) return text;

            string firstString = string.Empty;
            string secondStrnig = string.Empty;

            var splitedString = text.Split(' ');

            foreach (var temp in splitedString)
            {
                firstString += " " + temp;
                if (firstString.Length < maximumLength)
                {
                    secondStrnig += " " + temp;
                }
                else
                {
                    break;
                }
            }

            secondStrnig += " ...";
            return secondStrnig;


        }
        public static string ToSqlDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

   
        public static T SafeEnum<T>(this object e)
        {
            if (string.IsNullOrEmpty(e.SafeString()))
                return default(T);
            return (T)Enum.Parse(typeof(T), e.SafeString());
        }

        public static T? SafeNullableEnum<T>(this object e) where T : struct
        {
            if (e == null || e is DBNull)
                return null;
            if (string.IsNullOrEmpty(e.SafeString()))
                return null;
            return (T)Enum.Parse(typeof(T), e.SafeString());
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                  as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Value is not valid", "description");
        }
       
    }
}
