using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Framework.DBFFactory
{
    public sealed class Conversions
    {
        public static int ToInteger(string value)
        {
            int num;
            if (value == null)
            {
                return 0;
            }
            try
            {
                long num2 = 0L;
                if (IsHexOrOctValue(value, ref num2))
                {
                    return (int)num2;
                }
                num = (int)Math.Round(Convert.ToDouble(value));
            }
            catch (FormatException exception)
            {
                throw new Exception("s");
            }
            return num;
        }

        internal static bool IsHexOrOctValue(string value, ref long i64Value)
        {
            int num = 0;
            int length = value.Length;
            while (num < length)
            {
                char ch = value[num];
                if ((ch == '&') && ((num + 2) < length))
                {
                    ch = char.ToLower(value[num + 1], CultureInfo.InvariantCulture);
                    string str = ToHalfwidthNumbers(value.Substring(num + 2), GetCultureInfo());
                    switch (ch)
                    {
                        case 'h':
                            i64Value = Convert.ToInt64(str, 0x10);
                            goto Label_0087;

                        case 'o':
                            i64Value = Convert.ToInt64(str, 8);
                            goto Label_0087;
                    }
                    throw new FormatException();
                }
                if ((ch != ' ') && (ch != '　'))
                {
                    return false;
                }
                num++;
            }
            return false;
        Label_0087:
            return true;
        }

        internal static CultureInfo GetCultureInfo()
        {
            return Thread.CurrentThread.CurrentCulture;
        }

        internal static string ToHalfwidthNumbers(string s, CultureInfo culture)
        {
            int num = culture.LCID & 0x3ff;
            if (((num != 4) && (num != 0x11)) && (num != 0x12))
            {
                return s;
            }
            return vbLCMapString(culture, 0x400000, s);
        }

        internal static string vbLCMapString(CultureInfo loc, int dwMapFlags, string sSrc)
        {
            int num2 = 0;
            int length;
            if (sSrc == null)
            {
                length = 0;
            }
            else
            {
                length = sSrc.Length;
            }
            if (length == 0)
            {
                return "";
            }

            Encoding encoding = Encoding.UTF8;
            if (!encoding.IsSingleByte)
            {
                string s = sSrc;
                if (s != null)
                {
                    byte[] bytes = encoding.GetBytes(s);
                    num2 = bytes.Length;
                }
                byte[] buffer = new byte[(num2 - 1) + 1];
                return encoding.GetString(buffer);
            }
            string lpDestStr = new string(' ', length);
            return lpDestStr;
        }

        public static string ToString(byte value)
        {
            return value.ToString(null, null);
        }

        public static string ToString(char value)
        {
            return value.ToString();
        }

        public static string ToString(DateTime value)
        {
            long ticks = value.TimeOfDay.Ticks;
            if ((ticks == value.Ticks) || (((value.Year == 0x76b) && (value.Month == 12)) && (value.Day == 30)))
            {
                return value.ToString("T", null);
            }
            if (ticks == 0L)
            {
                return value.ToString("d", null);
            }
            return value.ToString("G", null);
        }

        public static string ToString(decimal value)
        {
            return ToString(value, null);
        }

        public static string ToString(double value)
        {
            return ToString(value, null);
        }

        public static string ToString(short value)
        {
            return value.ToString(null, (IFormatProvider)null);
        }

        public static string ToString(int value)
        {
            return value.ToString(null, null);
        }

        public static string ToString(long value)
        {
            return value.ToString(null, null);
        }

        public static string ToString(object value)
        {
            if (value == null)
            {
                return null;
            }
            string str2 = value as string;
            if (str2 != null)
            {
                return str2;
            }
            IConvertible convertible = value as IConvertible;
            if (convertible != null)
            {
                switch (convertible.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        return ToString(convertible.ToBoolean(null));

                    case TypeCode.Char:
                        return ToString(convertible.ToChar(null));

                    case TypeCode.SByte:
                        return ToString((int)convertible.ToSByte(null));

                    case TypeCode.Byte:
                        return ToString(convertible.ToByte(null));

                    case TypeCode.Int16:
                        return ToString((int)convertible.ToInt16(null));

                    case TypeCode.UInt16:
                        return ToString((uint)convertible.ToUInt16(null));

                    case TypeCode.Int32:
                        return ToString(convertible.ToInt32(null));

                    case TypeCode.UInt32:
                        return ToString(convertible.ToUInt32(null));

                    case TypeCode.Int64:
                        return ToString(convertible.ToInt64(null));

                    case TypeCode.UInt64:
                        return ToString(convertible.ToUInt64(null));

                    case TypeCode.Single:
                        return ToString(convertible.ToSingle(null));

                    case TypeCode.Double:
                        return ToString(convertible.ToDouble(null));

                    case TypeCode.Decimal:
                        return ToString(convertible.ToDecimal(null));

                    case TypeCode.DateTime:
                        return ToString(convertible.ToDateTime(null));

                    case (TypeCode.DateTime | TypeCode.Object):
                        return null;

                    case TypeCode.String:
                        return convertible.ToString(null);
                }
            }
            else
            {
                char[] chArray = value as char[];
                if (chArray != null)
                {
                    return new string(chArray);
                }
            }

            return convertible.ToString();
        }

        public static string ToString(float value)
        {
            return ToString(value, null);
        }

        [CLSCompliant(false)]
        public static string ToString(uint value)
        {
            return value.ToString(null, null);
        }

        [CLSCompliant(false)]
        public static string ToString(ulong value)
        {
            return value.ToString(null, null);
        }

        public static string ToString(decimal value, NumberFormatInfo numberFormat)
        {
            return value.ToString("G", numberFormat);
        }

        public static string ToString(double value, NumberFormatInfo numberFormat)
        {
            return value.ToString("G", numberFormat);
        }

        public static string ToString(float value, NumberFormatInfo numberFormat)
        {
            return value.ToString(null, numberFormat);
        }



    }
}
