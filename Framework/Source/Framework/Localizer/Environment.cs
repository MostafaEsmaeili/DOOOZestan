using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global


namespace Framework.Localizer
{

    #region " Environment "

    /// <summary>
    ///     Containing SpecialCharacters (in str type) in Persian, English and Arabic Languages and also contains Hard-to-find common symbols!
    /// </summary>
    public static class Environment
    {
        public const string Char255 = " ";
        public const string ArabicTripleDot = "؞";
        public const string EmDash = "—";
        public const string EnDash = "–";
        public const string NonbreakingHyphen = " ";
        public const string OptionalHyphen = "¬";
        public const string EmSpace = " ";
        public const string EnSpace = " ";
        public const string QuarterEmSpace = " ";
        public const string NonBreakingSpace = " ";
        public const string CopyRight = "©";
        public const string Registered = "®";
        public const string TradeMArk = "™";
        public const string Section = "§";
        public const string Paragraph = "¶";
        public const string Ellipsis = "…";
        public const string SingleOpenningQuote = "‘";
        public const string SingleClosingQuote = "’";
        public const string DoubleOpenningQuote = "“";
        public const string DoubleClosingQuote = "”";
        public const string RightToLeftMark = "‏";
        public const string LeftToRightMark = "‎";
        public const string InhibitSymetricSwapping = "⁪";
        public const string ZeroWidthNonJoiner = "‌";
        public const string ZeroWidthJoiner = "‍";
        public const string HorizontalBar = "―";
        public const string DoubleLowLine = "‗";
        public const string Bullet = "•";
        public const string LeftToRightEmbeding = "‪";
        public const string RightToLeftEmbeding = "‫";
        public const string PopDirectionalFormating = "‬";
        public const string LeftToRightOverride = "‭";
        public const string RightToLeftOverride = "‮";
        public const string SingleLeftPointingAngleQuotionMark = "‹";
        public const string SingleRightPointingAngleQuotionMark = "›";
        public const string DoubleLeftPointingAngleQuotionMark = "«";
        public const string DoubleRightPointingAngleQuotionMark = "»";
        public const string DoubleExclamationMark = "‼";
        public const string OverLine = "‾";
        //{0:N0}
        //º°¹²³Μµ¼½¾ªΘΛΩζεδαβγηθΙικΚλπΠΡρΣςστΤΦφυΥω
        public const string FractionSlash = Slash;
        public const string ActivateSymetricSwapping = "⁫";
        public const string InhibitArabicFormShaping = "⁬";
        public const string ActivateArabicFormShaping = "⁭";
        public const string NationalDigitShapes = "⁮";
        public const string NominalDigitShapes = "⁯";
        public const string SuperScriptLatingSmallLetterN = "ⁿ";
        public const string MoneyFrenchFranc = "₣";
        public const string MoneyLiraSign = "₤";
        public const string MoneyPesetaSign = "₧";
        public const string MoneyNewSheqelSign = "₪";
        public const string MoneyDongSign = "₫";
        public const string MoneyIranRial = "ریال";
        public const string MoneyIranTooman = "تومان";
        public const string MoneyEuroSign = "€";
        public const string MoneyCentSign = "¢";
        public const string MoneyDollarSign = "$";
        public const string MoneyPondSign = "£";
        public const string MoneyYenSign = "¥";
        public const string MoneyCurrencySign = "¤";
        public const string NumeroSign = "№";
        public const string PlusMinusSign = "±";
        public const string DegreeSign = "°";
        public const string InvertedQuestionMark = "¿";
        public const string InvertedExclamationMark = "¡";
        public const string NonBreakSpace = " ";
        public const string PersianQuestionMark = "؟";
        public const string FivePointedStar = "٭";
        public const string EnglishNumbers0To9 = "0123456789";
        /// <summary>
        ///     <para> </para>
        ///     *** Do NOT forget! "۴۵۶" are Persian, "٤٥٦" are Arabic! ***
        ///     <para> </para>
        /// </summary>
        public const string PersianNumbers0To9 = "۰۱۲۳۴۵۶۷۸۹";

        /// <summary>
        ///     <para> </para>
        ///     *** Do NOT forget! "۴۵۶" are Persian, "٤٥٦" are Arabic! ***
        ///     <para> </para>
        /// </summary>
        public const string ArabicNumbers0To9 = "٠١٢٣٤٥٦٧٨٩";

        public const string PersianThousandsSeparator = "٫";
        public const string PersianDecimalSeparator = ".";
        public const string PersianDecimalSeparator2 = Slash;
        public const string ArabicDecimalSeparator = "٫";
        public const string ArabicThousandsSeparator = "٬";
        public const string ArabicYa = "ي";
        public const string ArabicYaWithHamza = "ئ";
        public const string PersianYa = "ی";
        public const string ArabicWavWithHamzaAbove = "ؤ";
        public const string PersianWav = "و";
        public const string ArabicKaf = "ك";
        public const string PersianKaf = "ک";
        public const string ArabicAlefMaqsura = "ى";
        public const string PersianGaf = "گ";
        public const string PersianCh = "چ";
        public const string PersianP = "پ";
        public const string PersianZh = "ژ";
        public const string ArabicHa = "ة";
        public const string PersianHa = "ه";
        public const string ArabicSwashKaf = "ڪ";
        public const string MaleSign = "♂";
        public const string FemailSign = "♀";
        public const string SmilingFace = "☺";
        public const string SmilingFaceBlack = "☻";
        public const string UpPointingRectangle = "▲";
        public const string RightPointingRectangle = "►";
        public const string DownPointingRectangle = "▼";
        public const string LeftPointingRectangle = "◄";
        public const string BlackRectabgle = "▬";
        public const string Heart = "♥";
        public const string Lozenge = "◊";
        public const string WhileCircle = "○";
        public const string BlackCircle = "●";
        public const string InverseBullet = "◘";
        public const string InverseWhiteCircle = "◙";
        public const string WhiteBullet = "◦";
        public const string EighthNote = "♪";
        public const string EighthNoteBeamed = "♫";
        public const string BlackSquar = "■";
        public const string WhiteSquar = "□";
        public const string Infinity = "∞";
        public const string LeftArrow = "←";
        public const string UpArrow = "↑";
        public const string RightArrow = "→";
        public const string DownArrow = "↓";
        public const string UpDownArrow = "↕";
        public const string BlackDiamondSuit = "♦";
        public const string BlackSpadeSuit = "♠";
        public const string BlackClubSuit = "♣";
        public const string BlockFull = "█";
        public const string BlockLeftHalf = "▌";
        public const string BlockRightHalf = "▐";
        public const string BlockUpHalf = "▀";
        public const string BlockDownHalf = "▄";
        public const string BlockShadeLight = "░";
        public const string BlockShadeMedium = "▒";
        public const string BlockShadeDark = "▓";
        public const string UpDownArrowWithBase = "↨";
        public const string LeftRightArrow = "↔";
        public const string AlmostEqualTo = "≈";
        public const string NotEqualTo = "≠";
        public const string IdenticalTo = "≡";
        public const string Division = "÷";
        public const string PersianAlefWithMad = "آ";
        public const string ArabicAlefWithHamzaAbove = "أ";
        public const string ArabicAlefWithHamzaBelow = "إ";
        public const string PersianAlef = "ا";
        public const string ArabicTatweel = "ـ";
        public const string ArabicFatha = "َ";
        public const string ArabicKasra = "ِ";
        public const string ArabicZamma = "ُ";
        public const string ArabicFathaTanvin = "ً";
        public const string ArabicKasraTanvin = "ٍ";
        public const string ArabicZammaTanvin = "ٌ";
        public const string ArabicTashdid = "ّ";
        public const string ArabicSokun = "ْ";
        public const string ArabicMaddAbove = "ٓ";
        public const string ArabicHamzaAbove = "ٔ";
        public const string ArabicHamzaBelow = "ٕ";
        public const string ArabicHamzaAlone = "ء";
        public const string PersianPercent = "٪";
        public const string Percent = "%";
        public const string LessThanOrEqualTo = "≤";
        public const string MoreThanOrEqualTo = "≥";
        public const string SquarRoot = "√";
        public const string Tilde = "~";
        public const string PersianComma = "،";
        public const string PersianSemiColon = "؛";
        public const string BasicTurningDashClockwise = EnDash + BackSlash + Pipe + Slash;
        public const string BasicTurningDashCounterClockwise = EnDash + Slash + Pipe + BackSlash;
        public const string Slash = "/";
        public const string BackSlash = "\\";
        public const string Pipe = "|";
        public const string DoubleQuote = "\"";
        public const string SingleQuote = "'";
        public const string Apostrophe = "'";
        public const string CircumflexAccent = "^";
        public const string Multiply = "×";
        public const string Asterisk = Star;
        public const string Star = "*";

        public const string Sigma = "∑";
        public const string Product = "∏";
        public const string Delta = "∆";
        public const string Diacritizations = "ًٌٍَُِْٓٔ";

        /// <summary>
        ///     System's Regular NewLine Chars.
        /// </summary>
        public static readonly string NewLine = System.Environment.NewLine;

        public static string PersianNumberToEnglish(this string number)
        {
            if (string.IsNullOrEmpty(number)) return number;
            string temptext = string.Empty;
            foreach (char cc in number)
            {
                if (ArabicNumbers0To9.IndexOf(cc) >= 0)
                    temptext = temptext + EnglishNumbers0To9[ArabicNumbers0To9.IndexOf(cc)].ToString();
                else if (PersianNumbers0To9.IndexOf(cc) >= 0)
                    temptext = temptext + EnglishNumbers0To9[PersianNumbers0To9.IndexOf(cc)].ToString();
                else temptext = temptext + cc.ToString();
            }
            return temptext;
        }

        public static string ArabicNumberToPersian(this string number)
        {
            if (string.IsNullOrEmpty(number)) return number;
            string temptext = string.Empty;
            foreach (char cc in number)
            {
                if (ArabicNumbers0To9.IndexOf(cc) >= 0)
                    temptext = temptext + PersianNumbers0To9[ArabicNumbers0To9.IndexOf(cc)].ToString();
                else temptext = temptext + cc.ToString();
            }
            return temptext;
        }

        public static string EnglishAndArabicNumberToPersian(this string number)
        {
            if (string.IsNullOrEmpty(number)) return number;
            string temptext = string.Empty;
            foreach (char cc in number)
            {
                if (EnglishNumbers0To9.IndexOf(cc) >= 0)
                    temptext = temptext + PersianNumbers0To9[EnglishNumbers0To9.IndexOf(cc)].ToString();
                else if (ArabicNumbers0To9.IndexOf(cc) >= 0)
                    temptext = temptext + PersianNumbers0To9[ArabicNumbers0To9.IndexOf(cc)].ToString();
                else temptext = temptext + cc.ToString();
            }
            return temptext;
        }

        public static string RemoveSigns(this string text, bool removeWhiteSpace = false)
        {
            if (string.IsNullOrEmpty(text)) return text;
            string returnstr = string.Empty;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetterOrDigit(text[i])) returnstr = returnstr + text[i].ToString();
                if ((!removeWhiteSpace) && (char.IsWhiteSpace(text[i]))) returnstr = returnstr + text[i].ToString();
            }
            return FixForPersian(returnstr);
        }

        public static string FixPersianYaKaf(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Replace(ArabicYa, PersianYa).Replace(ArabicKaf, PersianKaf);
        }

        public static string FixArabicYaKaf(this string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            return text.Replace(PersianYa, ArabicYa).Replace(PersianKaf, ArabicKaf);
        }

        public static string FixForPersian(this string text, bool removeZeroWidthSpace = true)
        {
            if (string.IsNullOrEmpty(text)) return text;
            text = text.Replace(OptionalHyphen, ZeroWidthJoiner);
            text = text.Replace(RightToLeftMark, ZeroWidthJoiner);
            text = text.Replace(LeftToRightMark, ZeroWidthJoiner);
            if (removeZeroWidthSpace) text = text.Replace(ZeroWidthJoiner, string.Empty).Replace(ZeroWidthNonJoiner, string.Empty);
            ////
            string temptext = string.Empty;
            foreach (char cc in text)
            {
                if (ArabicNumbers0To9.IndexOf(cc) >= 0)
                    temptext = temptext + PersianNumbers0To9[ArabicNumbers0To9.IndexOf(cc)].ToString();
                else temptext = temptext + cc.ToString();
            }
            text = temptext;
            ////
            text = text.Replace(EmSpace, EnSpace);
            text =
                text.Replace(ArabicYa, PersianYa).Replace(ArabicKaf, PersianKaf).Replace(ArabicHa, PersianHa).Replace(
                    ArabicHamzaAlone, string.Empty).Replace(ArabicTripleDot, string.Empty);
            text =
                text.Replace(PersianAlefWithMad, PersianAlef).Replace(ArabicYaWithHamza, PersianYa).Replace(
                    ArabicHamzaAlone, string.Empty).Replace(ArabicAlefWithHamzaAbove, PersianAlef).Replace(
                        ArabicTatweel, string.Empty);
            text =
                text.Replace(PersianAlefWithMad, PersianAlef).Replace(ArabicAlefWithHamzaAbove, PersianAlef).Replace(
                    ArabicWavWithHamzaAbove, PersianWav).
                    Replace(ArabicAlefWithHamzaBelow, PersianAlef).Replace(ArabicYaWithHamza, PersianYa).
                    Replace(ArabicYa, PersianYa);

            text = text.Replace(Char255, " ");
            /////////////////////// Non Real letters (non joinable)


            text =
                text.Replace(ArabicKaf, PersianKaf).Replace("ﺁ", PersianAlef).Replace("ﺂ", PersianAlef).
                    Replace("ﺃ", PersianAlef).Replace("ﺄ", PersianAlef);
            text = text.Replace("ﺅ", PersianWav).Replace("ﺆ", PersianWav).Replace("ﺇ", PersianAlef).Replace("ﺈ", PersianAlef);
            text = text.Replace("ﺉ", PersianYa).Replace("ﺊ", PersianYa).Replace("ﺋ", PersianYa).Replace("ﺌ", PersianYa);
            text = text.Replace("ﺎ", PersianAlef).Replace("ﺍ", PersianAlef);
            text = text.Replace("ﺔ", PersianHa).Replace("ﺓ", PersianHa);
            text = text.Replace("ﻲ", PersianYa).Replace("ﻴ", PersianYa).Replace("ﻰ", PersianYa).Replace("ﻱ", PersianYa).
                Replace("ﻴ", PersianYa).Replace("ﻳ", PersianYa);
            text = text.Replace("ۂ", PersianHa).Replace("ۃ", PersianHa).Replace("ۀ", PersianHa).Replace("ە", PersianHa);
            //////////////////////////////////////
            return RemoveDiacritizations(text);
        }

        public static string RemoveDiacritizations(this string text, bool removeSingleHamza = true, bool removePersianLineJoiner = true)
        {
            if (string.IsNullOrEmpty(text)) return text;
            string ret = string.Empty;
            string puncs = Diacritizations + (removeSingleHamza ? ArabicHamzaAlone : string.Empty) + (removePersianLineJoiner ? ArabicTatweel : string.Empty);
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsPunctuation(text[i]))
                {
                    if (!puncs.Contains(text[i].ToString())) ret = ret + text[i].ToString();
                }
            }
            const string invs = "ۀؤإأئ";
            const string reps = "هواای";
            for (int i = 0; i < invs.Length; i++)
            {
                ret = ret.Replace(invs[i], reps[i]);
            }
            return (ret.Trim());
        }

        public static string SafeSpace(this string str,string replacement="")
        {
            if (string.IsNullOrEmpty(str)) return str;
            str = str.Replace(EmSpace, " ");
            str = str.Replace(EnSpace, " ");
            str = str.Replace(NonbreakingHyphen, " ");
            str = str.Replace(OptionalHyphen, " ");
            str = str.Replace(NonBreakSpace, " ");
            str = str.Replace(NonBreakingSpace, " ");
            str = str.Replace(QuarterEmSpace, " ");
            str = str.Replace(LeftToRightMark, replacement);
            str = str.Replace(RightToLeftMark, replacement);
            str = str.Replace(((char) 1600).ToString(), replacement);
            str = str.Replace(((char) 8201).ToString(), replacement);
            str = str.Replace(((char) 8202).ToString(), replacement);
            str = str.Replace(((char) 8203).ToString(), replacement);
            str = str.Replace(((char) 8204).ToString(), replacement);
            str = str.Replace(((char) 8205).ToString(), replacement);
            str = str.Replace(((char) 8206).ToString(), replacement);
            str = str.Replace(((char) 8207).ToString(), replacement);
            str = str.Replace(((char) 8208).ToString(), replacement);
            str = str.Replace(((char) 8209).ToString(), replacement);
            str = str.Replace("¬", replacement);
            str = str.Trim();
            if (!string.IsNullOrEmpty(replacement)) str = str.RemoveDual(replacement[0]);
            return str;
        }

        public static string RemoveMultiple(this string str, char chr, int morechars = 2, int fewchars = 1)
        {
            if (string.IsNullOrEmpty(str)) return str;
            var more = new string(chr, morechars);
            var fewer = new string(chr, fewchars);
            if (str.IsBlank()) return "";
            while (str.Contains(more))
            {
                str = str.Replace(more, fewer);
            }
            return str;
        }

        public static string RemoveDualSpace(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str == null ? null : str.RemoveDual(' ');
        }

        public static string RemoveDash(this string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str == null ? null : str.Replace("-", "");
        }

        public static string RemoveDual(this string str, char chr)
        {
            if (string.IsNullOrEmpty(str)) return str;
            return str == null ? null : str.RemoveMultiple(chr, 2, 1);
        }

        public static string RemoveChars(this string s, params char[] chrs)
        {
            if (String.IsNullOrEmpty(s)) return s;
            if (chrs == null || chrs.Length < 1) return s;
            return chrs.Aggregate(s, (current, c) => current.Replace(c.ToString(), ""));
        }

        public static bool IsAllTheSame(this string s)
        {
            if (String.IsNullOrWhiteSpace(s)) return true;
            return s.All(c => c == s[0]);
        }

        public static bool IsPositiveInteger(this string s)
        {
            const string numberpattern = @"^\d+$";
            return (new Regex(numberpattern)).IsMatch(s);
        }

        public static bool IsInteger(this string s)
        {
            const string numberpattern = @"^((-{0,1}[1-9]\d*)|(\d+))$";
            return (new Regex(numberpattern)).IsMatch(s);
        }

        public static bool IsNumber(this string s)
        {
            const string numberpattern = @"^[-]?[0-9]*\.?[0-9]+([eE][-]?[0-9]+)?$";
            return (new Regex(numberpattern)).IsMatch(s);
        }

        public static string RemoveSpace(this string str)
        {
            return str == null ? null : str.Replace(" ", "");
        }

        public static string SuperTrim(this string str, params char[] additionalChars)
        {
            if (string.IsNullOrWhiteSpace(str)) return "";
            var mychars = new List<char> { ' ', '\n', '\r' };
            if (additionalChars != null && additionalChars.Length > 0) mychars.AddRange(additionalChars);
            return str.Trim(mychars.ToArray());
        }

        public static bool IsBlank(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        public static bool IsGentlyBlank(this string str)
        {
            return String.IsNullOrWhiteSpace(str) ||
                   String.IsNullOrWhiteSpace(SuperTrim(str).Replace(" ", "").Replace("-", ""));
        }

        public static string Gentled(this string str)
        {
            if (string.IsNullOrEmpty(str)) str=string.Empty;
            str = FixForPersian(str);
            var strtemp = "";
            const string removables = "ًٌٍَُِّْٕٓٔٴً  ";
            for (int i = 0; i < str.Length; i++)
            {
                if (removables.Contains(str[i].ToString(CultureInfo.InvariantCulture))) continue;
                if (char.IsLetterOrDigit(str[i])) strtemp += str[i].ToString(CultureInfo.InvariantCulture);
            }
            str = strtemp;
            const string abbr1 = "ئ ء آ ۀ أ ـ إ ؤ ة ي ك ٱ ٲ ٳ ٵ";
            const string abbr2 = "ی   ا ه ا   ا و ه ی ک ا ا ا ا";
            for (int i = 0; i < abbr1.Length; i++)
            {
                if (abbr1[i] == ' ') continue;
                if (str.Contains(abbr1[i].ToString(CultureInfo.InvariantCulture)))
                {
                    var strsubstitute = abbr2[i].ToString();
                    if (strsubstitute == " ") strsubstitute = "";
                    str = str.Replace(abbr1[i].ToString(CultureInfo.InvariantCulture), strsubstitute);
                }
            }
            return str;
        }

        public static bool GentlyEqual(this string str1, string str2)
        {
            return Gentled(str1) == Gentled(str2);
        }
        public static string LeadZero(this string str,int length)
        {
            if (string.IsNullOrEmpty(str)) str = "";
            if (str.Length < length && length > 0) str = new string('0', length - str.Length) + str;
            return str;
        }

        public static bool GentlyContains(this IEnumerable<string> strCol, string str1)
        {
            return strCol.Any(x => GentlyEqual(str1, x));
        }
    }

    #endregion
}
// ReSharper restore MemberCanBePrivate.Global
// ReSharper restore UnusedMember.Global
