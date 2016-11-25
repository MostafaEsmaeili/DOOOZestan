using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using Framework.Utility;

namespace Framework.Extensions
{
    //public static class ConversionExtensions
    //{
    //    public static bool SafeBool(this object i)
    //    {
    //        var b = false;
    //        if (i != null)
    //        {
    //            if (!bool.TryParse(i.SafeString(), out b))
    //            {
    //                if (i.ToString() == "1") b = true;
    //                //else if (i.ToString() == "0") b = false;
    //            }
    //        }
    //        return b;
    //    }

    //    public static int SafeBoolToInt(this object i)
    //    {
    //        var b = SafeBool(i);
    //        return b ? 1 : 0;
    //    }

    //    public static byte SafeByte(this object i)
    //    {
    //        byte val = 0;
    //        if (i != null)
    //        {
    //            byte.TryParse(i.SafeString().Split('.')[0], out val);
    //        }

    //        return val;
    //    }

    //    public static string SafeInjection(this string searchString)
    //    {

    //        try
    //        {
    //            //if (string.IsNullOrEmpty(searchString))
    //            //    return null;

    //            //// Do wild card replacements
    //            searchString = searchString.Replace("*", "%");

    //            //// Strip any markup characters
    //            //searchString = Formatter.RemoveHtml(searchString);

    //            //// Remove known bad SQL characters
    //            //searchString = OMSRegex.SqlGeneratorSearchRegex().Replace(searchString, " ");

    //            //// Finally remove any extra spaces from the string
    //            //searchString = OMSRegex.SearchSpaceRegex().Replace(searchString, " ");
    //            searchString = searchString.Replace("'", "''");

    //        }
    //        catch (Exception)
    //        {
    //            //BaseEventLogs.Write(ex);
    //            return string.Empty;
    //        }
    //        return searchString;
    //    }

    //    public static int? SafeNullableInt(this object i)
    //    {
    //        if (i == null || i is DBNull)
    //        {
    //            return null;
    //        }

    //        return (int)i.SafeDecimal();
    //    }

    //    public static int SafeInt(this object i, int exceptionValue)
    //    {
    //        if (i == null)
    //        {
    //            return exceptionValue;
    //        }

    //        return (int)i.SafeDecimal();
    //    }

    //    public static int SafeInt(this object i)
    //    {
    //        return SafeInt(i, -1);
    //    }

    //    public static T SafeEnum<T>(this object e)
    //    {
    //        if (string.IsNullOrEmpty(e.SafeString()))
    //            return default(T);
    //        return (T) Enum.Parse(typeof (T), e.SafeString());
    //    }

    //    public static T? SafeNullableEnum<T>(this object e) where T : struct
    //    {
    //        if (e == null || e is DBNull)
    //            return null;
    //        if (string.IsNullOrEmpty(e.SafeString()))
    //            return null;
    //        return (T) Enum.Parse(typeof (T), e.SafeString());
    //    }

    //    public static string SafeString(this object i)
    //    {

    //        if (i != null)
    //        {
    //            return i.ToString();
    //        }

    //        return null;
    //    }

    //    public static DateTime SafeDateTime(this object d)
    //    {
    //        if (d == null)
    //            return DateTime.MinValue;
    //        DateTime dt;
    //        DateTime.TryParse(d.SafeString(), out dt);
    //        return dt;
    //    }

    //    public static DateTime? SafeNullableDateTime(this object d)
    //    {
    //        if (d == null)
    //            return null;
    //        DateTime dt;
    //        if (DateTime.TryParse(d.SafeString(), out dt))
    //            return dt;
    //        return null;
    //    }

    //    public static Guid SafeGuid(this object d)
    //    {
    //        if (d == null)
    //            return Guid.Empty;
    //        Guid g;
    //        Guid.TryParse(d.ToString(), out g);
    //        return g;
    //    }

    //    //public static string SafePersianEncode(this string str)
    //    //{
    //    //    //if (string.IsNullOrEmpty(str))
    //    //    //{
    //    //    //    return string.Empty;
    //    //    //}
    //    //    //str = str.Replace("ي", "ی");
    //    //    //str = str.Replace("ك", "ک");
    //    //    return str.FixPersianYaKaf();
    //    //}

    //    public static string ToSqlDateTime(this DateTime dateTime)
    //    {
    //        return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
    //    }

    //    public static string SafeSqlDateTime(this object dateTime)
    //    {
    //        if (dateTime is DateTime)
    //            return ToSqlDateTime((DateTime)dateTime);
    //        return null;
    //    }

    //    public static long SafeLong(this object i)
    //    {
    //        return SafeLong(i, 0);
    //    }

    //    public static long SafeLong(this object i, long exceptionValue)
    //    {
    //        if (i == null)
    //        {
    //            return exceptionValue;
    //        }

    //        return (long)i.SafeDecimal();
    //    }

    //    public static float SafeFloat(this object i)
    //    {
    //        float id;
    //        float.TryParse(i.SafeString(), out id);
    //        return id;
    //    }

    //    public static double SafeDouble(this object i)
    //    {
    //        double id;
    //        double.TryParse(i.SafeString(), out id);
    //        return id;
    //    }

    //    public static double SafeDouble(this object i, double exceptionValue)
    //    {
    //        if (i != null)
    //        {
    //            double.TryParse(i.SafeString().Split('.')[0], out exceptionValue);
    //        }
    //        return exceptionValue;
    //    }

    //    public static Int16 SafeInt16(this object i)
    //    {
    //        if (i == null)
    //        {
    //            return 0;
    //        }

    //        return (Int16)i.SafeDecimal();
    //    }

    //    public static decimal SafeDecimal(this object i)
    //    {
    //        decimal id;
    //        decimal.TryParse(i.SafeString(), out id);
    //        return id;
    //    }

    //    public static object SafeReader(this IDataReader reader, string name)
    //    {
    //        if (reader == null)
    //            return null;
    //        for (var i = 0; i < reader.FieldCount; i++)
    //        {
    //            if (reader.GetName(i).Equals(name, StringComparison.InvariantCultureIgnoreCase))
    //                return reader[name];
    //        }

    //        return null;
    //    }

    //    public static object SafeReader(this IDataRecord reader, string name)
    //    {
    //        if (reader == null)
    //            return null;
    //        for (var i = 0; i < reader.FieldCount; ++i)
    //        {
    //            if (reader.GetName(i).Equals(name, StringComparison.InvariantCultureIgnoreCase))
    //                return reader[name];
    //        }
    //        return null;
    //    }

    //    public static string SafeTrim(this string str)
    //    {
    //        if (!string.IsNullOrEmpty(str))
    //        {
    //            return str.Trim();
    //        }

    //        return null;
    //    }

    //    public static Dictionary<string, string> ToDictionaryUDT(this IEnumerable<string> source)
    //    {
    //        return source == null ? null : source.ToDictionary(x => x);
    //    }

    //    public static DateTime SafeMinDate(this DateTime date)
    //    {
    //        if (date == DateTime.MinValue)
    //        {
    //            date = new DateTime(1907, 1, 1);
    //        }
    //        return date;
    //    }

    //    public static string SafeLine(this string str)
    //    {
    //        if (string.IsNullOrEmpty(str))
    //            return str;
    //        str = str.Replace(System.Environment.NewLine, string.Empty);
    //        str = str.Replace("\r", string.Empty);
    //        return str;
    //    }

    //    public static T ConvertToValue<T>(Type type, string value)
    //    {
    //        return (T)ConvertToValue(type, value);
    //    }

    //    public static object ConvertToValue(Type type, string value)
    //    {
    //        if (type == typeof(bool) || type == typeof(bool?))
    //            return value.SafeString().SafeBool();

    //        if (type == typeof(float) || type == typeof(float?))
    //            return value.SafeString().SafeFloat();

    //        if (type == typeof(long) || type == typeof(long?))
    //            return value.SafeString().SafeLong();

    //        if (type == typeof(double) || type == typeof(double?))
    //            return value.SafeString().SafeDouble();

    //        if (type == typeof(int) || type == typeof(int?))
    //            return value.SafeString().SafeInt();

    //        if (type == typeof(short) || type == typeof(short?))
    //            return value.SafeString().SafeInt16();

    //        if (type == typeof(byte) || type == typeof(byte?))
    //            return value.SafeString().SafeByte();

    //        if (type == typeof(string))
    //            return value.SafeString().SafePersianEncode().Trim();

    //        if (type == typeof(decimal) || type == typeof(decimal?))
    //            return value.SafeString().SafeDecimal();

    //        return value;
    //    }

    //    public static string GetDescription(this Enum value)
    //    {
    //        FieldInfo field = value.GetType().GetField(value.ToString());

    //        DescriptionAttribute attribute
    //            = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
    //              as DescriptionAttribute;

    //        return attribute == null ? value.ToString() : attribute.Description;
    //    }

    //    public static T GetValueFromDescription<T>(string description)
    //    {
    //        var type = typeof(T);
    //        if (!type.IsEnum) throw new InvalidOperationException();
    //        foreach (var field in type.GetFields())
    //        {
    //            var attribute = Attribute.GetCustomAttribute(field,
    //                                                         typeof(DescriptionAttribute)) as DescriptionAttribute;
    //            if (attribute != null)
    //            {
    //                if (attribute.Description == description)
    //                    return (T)field.GetValue(null);
    //            }
    //            else
    //            {
    //                if (field.Name == description)
    //                    return (T)field.GetValue(null);
    //            }
    //        }
    //        throw new ArgumentException("Value is not valid", "description");
    //    }
    //}
}
