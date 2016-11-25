using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Framework.Utility;

namespace Doozestan.Common.Util
{
    public static class AmberUtil
    {
        public static string Beautify(this XmlDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
                NewLineHandling = NewLineHandling.Replace
            };
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                doc.Save(writer);
            }
            return sb.ToString();
        }

        public static string RemoveControlCharacters(string inString)
        {
            if (inString == null) return null;
            StringBuilder newString = new StringBuilder();
            char ch;
            foreach (char t in inString)
            {
                ch = t;
                if (!char.IsControl(ch))
                {
                    newString.Append(ch);
                }
                else
                {
                    continue;
                }
            }
            return newString.ToString();
        }

        public static string SafeSpace(string str, string replacement = "")
        {
            if (String.IsNullOrEmpty(str))
                return str;
            str = str.Replace(" ", " ");
            str = str.Replace(" ", " ");
            str = str.Replace(" ", " ");
            str = str.Replace("¬", " ");
            str = str.Replace(" ", " ");
            str = str.Replace(" ", " ");
            str = str.Replace(" ", " ");
            str = str.Replace("\x200E", replacement);
            str = str.Replace("\x200F", replacement);
            str = str.Replace('\x0640'.ToString(), replacement);
            str = str.Replace(((char)65279).ToString(), replacement);
            string str1 = str;
            char ch = ' ';
            string oldValue1 = ch.ToString();
            string newValue1 = replacement;
            str = str1.Replace(oldValue1, newValue1);
            string str2 = str;
            ch = ' ';
            string oldValue2 = ch.ToString();
            string newValue2 = replacement;
            str = str2.Replace(oldValue2, newValue2);
            string str3 = str;
            ch = '\x200B';
            string oldValue3 = ch.ToString();
            string newValue3 = replacement;
            str = str3.Replace(oldValue3, newValue3);
            string str4 = str;
            ch = '\x200C';
            string oldValue4 = ch.ToString();
            string newValue4 = replacement;
            str = str4.Replace(oldValue4, newValue4);
            string str5 = str;
            ch = '\x200D';
            string oldValue5 = ch.ToString();
            string newValue5 = replacement;
            str = str5.Replace(oldValue5, newValue5);
            string str6 = str;
            ch = '\x200E';
            string oldValue6 = ch.ToString();
            string newValue6 = replacement;
            str = str6.Replace(oldValue6, newValue6);
            string str7 = str;
            ch = '\x200F';
            string oldValue7 = ch.ToString();
            string newValue7 = replacement;
            str = str7.Replace(oldValue7, newValue7);
            string str8 = str;
            ch = '‐';
            string oldValue8 = ch.ToString();
            string newValue8 = replacement;
            str = str8.Replace(oldValue8, newValue8);
            string str9 = str;
            ch = '‑';
            string oldValue9 = ch.ToString();
            string newValue9 = replacement;
            str = str9.Replace(oldValue9, newValue9);
            str = str.Replace("¬", replacement);
            str = str.Trim();

            return str;
        }

        public static string RmoveIllegalChars(this string illegal)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            illegal = r.Replace(illegal, "");

            return illegal;
        }

        public static string PadLeftString(string input, int totalWidth)
        {
            return input.PadLeft(totalWidth, '0');
        }

        public static string PadLeftString(int input, int totalWidth)
        {
            return input.ToString().PadLeft(totalWidth, '0');
        }
        public static string ToFirstMarketISIN(this string isin)
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
            if (isin.StartsWith("IRB") && isin.EndsWith("52"))
            {
                return isin.Replace("52", "51");
            }
            return isin;
        }


        public static string SafePersianPhrase(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return
                    str.SafePersianEncode().RemoveNoise()
                        .Replace((char)8204, (char)32)
                        .TrimStart()
                        .TrimEnd();
            }
            return str;
        }

        public static string ToRightISIN(this string isin)
        {
            return isin.Replace("IRO", "IRR").Replace("0001", "0101");
        }

        public static string ToStockISIN(this string isin)
        {
            return isin.Replace("IRR", "IRO").Replace("0101", "0001");
        }
        public static string RemoveNoise(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            return str.Replace("‏", "");
        }

        public static string ToSimpleSymbol(this string symbol)
        {
            char last = symbol[symbol.Length - 1];
            if (last == '1')
                return symbol.Remove(symbol.Length - 1);
            else
            {
                return symbol;
            }
        }


        public static decimal ToPercentage(this decimal value,int round)
        {
            return Math.Round(value * 100, round);
        }

        public static bool IsRight(this string isin)
        {
            return isin.StartsWith("IRR");
        }

        /// <summary>
        /// برای سلف متفاوت خواهد بود
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        ///
        public static bool IsProductBond(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.StartsWith("IRB") && !str.StartsWith("IRBE") && !str.StartsWith("IRBK");
            }
            return false;
        }

        /// <summary>
        ///آیا سلف می باشد؟
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsProductForward(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.StartsWith("IRBE") || str.StartsWith("IRBK");
            }
            return false;
        }



    }
}
