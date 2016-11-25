#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#endregion

namespace Framework.Localizer
{
    public static class Numbering
    {
        #region " Private Properties "

        private static readonly string[] Harseragham =
            {
                "", "هزار", "میلیون", "میلیارد", "تریلیون", "کوادریلیون", "کنتیلیون",
                "سیکستیلیون", "سپتیلیون", "اکتیلیون", "نونیلیون", "دسیلیون"
                /*
                   Name                               10^
                Million                                  6 
                Billion                                  9 
                Trillion                                 12 
                Quadrillion                              15 
                Quintillion                              18 
                Sextillion                               21 
                Septillion                               24 
                Octillion                                27 
                Nonillion                                30 
                Decillion                                33 
                Undecillion                              36 
                Duodecillion                             39 
                Tredecillion                             42 
                Quattuordecillion                        45 
                Quindecillion (Quinquadecillion)         48 
                Sexdecillion (Sedecillion)               51 
                Septendecillion                          54 
                Octodecillion                            57 
                Novemdecillion (Novendecillion)          60 
                Vigintillion                             63
                - - - - - - - - - - - - - - - - - - - - - -
                Centillion                              303
                    */
            };

        private static Dictionary<string, long> numberTable = new Dictionary<string, long>
        {
            {"zero", 0},
            {"one", 1},
            {"two", 2},
            {"three", 3},
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9},
            {"ten", 10},
            {"eleven", 11},
            {"twelve", 12},
            {"thirteen", 13},
            {"fourteen", 14},
            {"fifteen", 15},
            {"sixteen", 16},
            {"seventeen", 17},
            {"eighteen", 18},
            {"nineteen", 19},
            {"twenty", 20},
            {"thirty", 30},
            {"forty", 40},
            {"fifty", 50},
            {"sixty", 60},
            {"seventy", 70},
            {"eighty", 80},
            {"ninety", 90},
            {"hundred", 100},
            {"thousand", 1000},
            {"million", 1000000},
            {"billion", 1000000000},
            {"trillion", 1000000000000},
            {"quadrillion", 1000000000000000},
            {"quintillion", 1000000000000000000}
        };

        #endregion

        #region " Private Methods "

        private static String NumberToEnglishExpr(this String numb)
        {
            return ToEnglishExpr(numb, false);
        }

        private static string NumberToPersianExpr(this string number)
        {
            if (number == "") return "";
            bool onlyzero = number.All(t => t == '0');
            if (onlyzero) return "صفر";
            string returnstr = "";
            int indexadd = 0;
            if (number.Length % 3 != 0) indexadd = 1;
            string[] sper3 = new string[(number.Length / 3) + indexadd];
            int index = 0;
            while ((number.Length >= 3) && (number != ""))
            {
                sper3[index++] = number.Substring(number.Length - 3, 3);
                if (number.Length == 3)
                {
                    number = "";
                    break;
                }
                number = number.Substring(0, number.Length - 3);
            }
            if (number != "") sper3[index] = number;
            else index--;
            for (int i = 0; i <= index; i++)
            {
                string tempstr = ConvertNumberToPersianExpr3Digits(sper3[i]);
                if (tempstr.Trim() != "")
                {
                    if (i < Harseragham.Length) tempstr = tempstr + " " + Harseragham[i];
                    else tempstr = tempstr + " " + "؟؟؟؟؟؟";
                }
                if ((returnstr != "") && (!returnstr.StartsWith(" و"))) tempstr = tempstr + " و ";
                returnstr = tempstr + returnstr;
            }
            while (returnstr.Contains("  "))
            {
                int k = returnstr.IndexOf("  ");
                returnstr = returnstr.Substring(0, k) + returnstr.Substring(k + 1);
            }
            return returnstr.Trim();
        }
        
        private static Int64 NumberFromPersianExpr(this string s)
        {
            return (Int64.Parse(NumberStringFromPersianExpr(s)));
        }

        private static string NumberStringFromPersianExpr(this string s)
        {
            if (s.Length < 1) return "";
            string[] harseragham2 = new string[Harseragham.Length - 1];
            for (int i = 1; i < Harseragham.Length; i++)
                harseragham2[i - 1] = Harseragham[i];
            string[] mys = s.Split(harseragham2, StringSplitOptions.None);
            string returnstr = ""; // new string('0', mys.Length);
            returnstr = mys.Length > 0
                            ? mys.Aggregate(returnstr,
                                            (current, t) =>
                                            current + ConvertNumberFromPersianExpr3Digits(t).ToString("000"))
                            : ConvertNumberFromPersianExpr3Digits(s).ToString("000");

            returnstr = returnstr.TrimStart('0').Trim();
            if (returnstr.Length < 1) returnstr = "0";
            if ((returnstr == "0") && (!s.Contains("صفر"))) returnstr = "";
            return returnstr;
        }

        private static long NumberFromEnglishExpr(this string numberString)
        {
            var numbers = Regex.Matches(numberString, @"\w+").Cast<Match>()
                 .Select(m => m.Value.ToLowerInvariant())
                 .Where(v => numberTable.ContainsKey(v))
                 .Select(v => numberTable[v]);
            long acc = 0, total = 0L;
            foreach (var n in numbers)
            {
                if (n >= 1000)
                {
                    total += (acc * n);
                    acc = 0;
                }
                else if (n >= 100)
                {
                    acc *= n;
                }
                else acc += n;
            }
            return (total + acc) * (numberString.StartsWith("minus",
                  StringComparison.InvariantCultureIgnoreCase) ? -1 : 1);
        }
        
        private static int ConvertNumberFromPersianExpr3Digits(string s)
        {
            if (s == "") return 0;
            int i = 0;
            if (s.Contains("نهصد"))
            {
                i = i + 900;
                s = s.Replace("نهصد", "---");
            }
            else if (s.Contains("هشتصد"))
            {
                i = i + 800;
                s = s.Replace("هشتصد", "---");
            }
            else if (s.Contains("هفتصد"))
            {
                i = i + 700;
                s = s.Replace("هفتصد", "---");
            }
            else if (s.Contains("ششصد"))
            {
                i = i + 600;
                s = s.Replace("ششصد", "---");
            }
            else if (s.Contains("پانصد"))
            {
                i = i + 500;
                s = s.Replace("پانصد", "---");
            }
            else if (s.Contains("چهارصد"))
            {
                i = i + 400;
                s = s.Replace("چهارصد", "---");
            }
            else if (s.Contains("سیصد"))
            {
                i = i + 300;
                s = s.Replace("سیصد", "---");
            }
            else if (s.Contains("دویست"))
            {
                i = i + 200;
                s = s.Replace("دویست", "---");
            }
            else if (s.Contains("صد"))
            {
                i = i + 100;
                s = s.Replace("صد", "---");
            }

            if (s.Contains("ده"))
            {
                if (s.Contains("نوزده")) i += 19;
                else if (s.Contains("هجده")) i += 18;
                else if (s.Contains("هفده")) i += 17;
                else if (s.Contains("شانزده")) i += 16;
                else if (s.Contains("پانزده")) i += 15;
                else if (s.Contains("چهارده")) i += 14;
                else if (s.Contains("سیزده")) i += 13;
                else if (s.Contains("دوازده")) i += 12;
                else if (s.Contains("یازده")) i += 11;
                else if (s.Contains("ده")) i += 10;
                return i;
            }
            if (s.Contains("نود"))
            {
                i += 90;
                s = s.Replace("نود", "---");
            }
            else if (s.Contains("هشتاد"))
            {
                i += 80;
                s = s.Replace("هشتاد", "---");
            }
            else if (s.Contains("هفتاد"))
            {
                i += 70;
                s = s.Replace("هفتاد", "---");
            }
            else if (s.Contains("شصت"))
            {
                i += 60;
                s = s.Replace("شصت", "---");
            }
            else if (s.Contains("پنجاه"))
            {
                i += 50;
                s = s.Replace("پنجاه", "---");
            }
            else if (s.Contains("چهل"))
            {
                i += 40;
                s = s.Replace("چهل", "---");
            }
            else if (s.Contains("سی"))
            {
                i += 30;
                s = s.Replace("سی", "---");
            }
            else if (s.Contains("بیست"))
            {
                i += 20;
                s = s.Replace("بیست", "---");
            }
            if (s.Contains("نه")) i += 9;
            else if (s.Contains("هشت")) i += 8;
            else if (s.Contains("هفت")) i += 7;
            else if (s.Contains("شش")) i += 6;
            else if (s.Contains("پنج")) i += 5;
            else if (s.Contains("چهار")) i += 4;
            else if (s.Contains("سه")) i += 3;
            else if (s.Contains("دو")) i += 2;
            else if (s.Contains("یک")) i += 1;
            return i;
        }

        private static string ConvertNumberToPersianExpr3Digits(string s)
        {
            if (s == "") return "";
            string returnstr = "";
            string blast = "";
            string bblast = "";
            if (s.Length > 1) blast = s.Substring(s.Length - 2, 1);
            if (s.Length > 2) bblast = s.Substring(s.Length - 3, 1);
            string last = s.Substring(s.Length - 1, 1);
            if ((bblast.Length > 0) && (bblast != "0"))
            {
                switch (bblast)
                {
                    case "1":
                        returnstr = "صد" + returnstr;
                        break;
                    case "2":
                        returnstr = "دویست" + returnstr;
                        break;
                    case "3":
                        returnstr = "سیصد" + returnstr;
                        break;
                    case "4":
                        returnstr = "چهارصد" + returnstr;
                        break;
                    case "5":
                        returnstr = "پانصد" + returnstr;
                        break;
                    case "6":
                        returnstr = "ششصد" + returnstr;
                        break;
                    case "7":
                        returnstr = "هفتصد" + returnstr;
                        break;
                    case "8":
                        returnstr = "هشتصد" + returnstr;
                        break;
                    case "9":
                        returnstr = "نهصد" + returnstr;
                        break;
                }
                if (blast + last != "00") returnstr = returnstr + " و ";
            }

            if ((s.Length > 1) && (blast == "1"))
            {
                if (returnstr.Length > 0) returnstr = returnstr + " ";
                switch (last)
                {
                    case "0":
                        break;
                    case "1":
                        returnstr = returnstr + "یاز";
                        break;
                    case "2":
                        returnstr = returnstr + "دواز";
                        break;
                    case "3":
                        returnstr = returnstr + "سیز";
                        break;
                    case "4":
                        returnstr = returnstr + "چهار";
                        break;
                    case "5":
                        returnstr = returnstr + "پانز";
                        break;
                    case "6":
                        returnstr = returnstr + "شانز";
                        break;
                    case "7":
                        returnstr = returnstr + "هف";
                        break;
                    case "8":
                        returnstr = returnstr + "هج";
                        break;
                    case "9":
                        returnstr = returnstr + "نوز";
                        break;
                }
                returnstr = returnstr + "ده";
            }
            else
            {
                switch (blast)
                {
                    case "2":
                        returnstr = returnstr + "بیست";
                        break;
                    case "3":
                        returnstr = returnstr + "سی";
                        break;
                    case "4":
                        returnstr = returnstr + "چهل";
                        break;
                    case "5":
                        returnstr = returnstr + "پنجاه";
                        break;
                    case "6":
                        returnstr = returnstr + "شصت";
                        break;
                    case "7":
                        returnstr = returnstr + "هفتاد";
                        break;
                    case "8":
                        returnstr = returnstr + "هشتاد";
                        break;
                    case "9":
                        returnstr = returnstr + "نود";
                        break;
                }

                if ((blast.Length > 0) && (last != "0") && (blast != "0")) returnstr = returnstr + " و ";
                switch (last)
                {
                    case "1":
                        returnstr = returnstr + "یک";
                        break;
                    case "2":
                        returnstr = returnstr + "دو";
                        break;
                    case "3":
                        returnstr = returnstr + "سه";
                        break;
                    case "4":
                        returnstr = returnstr + "چهار";
                        break;
                    case "5":
                        returnstr = returnstr + "پنج";
                        break;
                    case "6":
                        returnstr = returnstr + "شش";
                        break;
                    case "7":
                        returnstr = returnstr + "هفت";
                        break;
                    case "8":
                        returnstr = returnstr + "هشت";
                        break;
                    case "9":
                        returnstr = returnstr + "نه";
                        break;
                }
            }
            return returnstr;
        }

        private static String ToEnglishExpr(String numb, bool isCurrency)
        {
            // ReSharper disable TooWideLocalVariableScope
            String wholeNo = numb, points, andStr = "", pointStr = "";
            // ReSharper restore TooWideLocalVariableScope
            String endStr = (isCurrency) ? ("Only") : ("");
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = (isCurrency) ? ("and") : ("point");
                    // just to separate whole numbers from points/cents
                    endStr = (isCurrency) ? ("Cents " + endStr) : ("");
                    pointStr = TranslateCents(points);
                }
            }
            string val = String.Format("{0} {1}{2} {3}", TranslateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);

            return val;
        }

        private static String TranslateWholeNumber(String number)
        {
            string word = "";

            bool isDone = false; //test if already translated
            double dblAmt = (Convert.ToDouble(number));
            //if ((dblAmt > 0) && number.StartsWith("0"))
            if (dblAmt > 0)
            {
                //test for zero or digit zero in a nuemric
                bool beginsZero = number.StartsWith("0"); //tests for 0XX

                int numDigits = number.Length;
                int pos = 0; //store digit grouping
                String place = ""; //digit grouping name:hundres,thousand,etc...
                switch (numDigits)
                {
                    case 1: //ones' range
                        word = Ones(number);
                        isDone = true;
                        break;
                    case 2: //tens' range
                        word = Tens(number);
                        isDone = true;
                        break;
                    case 3: //hundreds' range
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4: //thousands' range
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7: //millions' range
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10: //Billions's range
                    case 11: //Billions's range
                    case 12: //Billions's range
                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    case 13: //Billions's range
                    case 14: //Billions's range
                    case 15: //Billions's range
                        pos = (numDigits % 13) + 1;
                        place = " Trillion ";
                        break;
                    case 16: //Billions's range
                    case 17: //Billions's range
                    case 18: //Billions's range
                        pos = (numDigits % 16) + 1;
                        place = " Quadrillion ";
                        break;
                    case 19: //Billions's range
                    case 20: //Billions's range
                    case 21: //Billions's range
                        pos = (numDigits % 19) + 1;
                        place = " Quintillion ";
                        break;
                    case 22: //Billions's range
                    case 23: //Billions's range
                    case 24: //Billions's range
                        pos = (numDigits % 22) + 1;
                        place = " Sextillion ";
                        break;
                    case 25: //Billions's range
                    case 26: //Billions's range
                    case 27: //Billions's range
                        pos = (numDigits % 25) + 1;
                        place = " Septillion ";
                        break;
                    case 28: //Billions's range
                    case 29: //Billions's range
                    case 30: //Billions's range
                        pos = (numDigits % 28) + 1;
                        place = " Octillion ";
                        break;
                    case 31: //Billions's range
                    case 32: //Billions's range
                    case 33: //Billions's range
                        pos = (numDigits % 31) + 1;
                        place = " Nonillion ";
                        break;
                    case 34: //Billions's range
                    case 35: //Billions's range
                    case 36: //Billions's range
                        pos = (numDigits % 34) + 1;
                        place = " Decillion ";
                        break;
                    //add extra case options for anything above Billion...
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {
                    //if transalation is not done, continue...(Recursion comes in now!!)
                    word = TranslateWholeNumber(number.Substring(0, pos)) + place +
                           TranslateWholeNumber(number.Substring(pos));
                    //check for trailing zeros
                    if (beginsZero) word = " and " + word.Trim();
                }
                //ignore digit grouping names
                if (word.Trim().Equals(place.Trim())) word = "";
            }

            return word.Trim();
        }

        private static String Tens(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = null;
            switch (digt)
            {
                case 10:
                    name = "Ten";
                    break;
                case 11:
                    name = "Eleven";
                    break;
                case 12:
                    name = "Twelve";
                    break;
                case 13:
                    name = "Thirteen";
                    break;
                case 14:
                    name = "Fourteen";
                    break;
                case 15:
                    name = "Fifteen";
                    break;
                case 16:
                    name = "Sixteen";
                    break;
                case 17:
                    name = "Seventeen";
                    break;
                case 18:
                    name = "Eighteen";
                    break;
                case 19:
                    name = "Nineteen";
                    break;
                case 20:
                    name = "Twenty";
                    break;
                case 30:
                    name = "Thirty";
                    break;
                case 40:
                    name = "Fourty";
                    break;
                case 50:
                    name = "Fifty";
                    break;
                case 60:
                    name = "Sixty";
                    break;
                case 70:
                    name = "Seventy";
                    break;
                case 80:
                    name = "Eighty";
                    break;
                case 90:
                    name = "Ninety";
                    break;
                default:
                    if (digt > 0)
                    {
                        name = Tens(digit.Substring(0, 1) + "0") + " " + Ones(digit.Substring(1));
                    }
                    break;
            }
            return name;
        }

        private static String Ones(String digit)
        {
            int digt = Convert.ToInt32(digit);
            String name = "";
            switch (digt)
            {
                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
            return name;
        }

        private static String TranslateCents(String cents)
        {
            String cts = "";
            for (int i = 0; i < cents.Length; i++)
            {
                String digit = cents[i].ToString();
                string engOne = digit.Equals("0") ? "Zero" : Ones(digit);
                cts += " " + engOne;
            }
            return cts;
        }

        #endregion
        
        #region " Digit Grouping "
        public static string GroupDigits(this double number, int maxDigitsToZeroLead = 0)
        {
            return GroupDigits(number.ToString(), maxDigitsToZeroLead);
        }

        public static string GroupDigits(this float number, int maxDigitsToZeroLead = 0)
        {
            return GroupDigits(number.ToString(), maxDigitsToZeroLead);
        }

        public static string GroupDigits(this int number, int maxDigitsToZeroLead = 0)
        {
            return GroupDigits(number.ToString(), maxDigitsToZeroLead);
        }

        public static string GroupDigits(this Int64 number, int maxDigitsToZeroLead = 0)
        {
            return GroupDigits(number.ToString(), maxDigitsToZeroLead);
        }

        public static string GroupDigits(this String numberString, int maxDigitsToZeroLead = 0)
        {
            numberString = numberString.Replace(",", "");
            string pish = "";
            string pass = "";
            string num;
            if ((numberString[0] == '-') || (numberString[0] == '+'))
            {
                pish = numberString[0].ToString();
                numberString = numberString.Substring(1);
            }
            if (numberString.Contains("."))
            {
                if (numberString[0] == '.')
                {
                    num = "0";
                    if (numberString.Length > 1) pass = numberString.Substring(1);
                }
                else
                {
                    num = numberString.Substring(0, numberString.IndexOf('.'));
                    if (numberString.IndexOf('.') < numberString.Length - 1)
                        pass = numberString.Substring(numberString.IndexOf('.') + 1);
                }
            }
            else num = numberString;
            int j = 0;
            num = num.TrimStart('0');
            while (num.Length < maxDigitsToZeroLead)
            {
                num = "0" + num;
            }
            string newnum = "";
            for (int i = num.Length - 1; i >= 0; i--)
            {
                j++;
                newnum = num[i].ToString() + newnum;
                if (j % 3 == 0) newnum = ',' + newnum;
            }
            newnum = newnum.TrimStart(',');
            newnum = pish + newnum;
            pass = pass.Trim();
            if (pass.Length > 0) newnum = newnum + "." + pass;
            return newnum;
        }
        #endregion

        #region Currency And Number Formating
        
        public static string GetCurrency(this double d, string type = "c", Culture cul = null)
        {
            return GetCurrency((decimal)d, type, cul);
        }

        public static string GetCurrency(this decimal d, string type = "c", Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            return d.ToString(type, cul.NumberFormat);
        }
        
        public static string GetNumber(this double d, string type = "", Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            if (type == "") return d.ToString(cul.NumberFormat);
            return d.ToString(type, cul.NumberFormat);
        }

        public static string GetNumber(this decimal d, string type = "", Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            if (type == "") return d.ToString(cul.NumberFormat);
            return d.ToString(type, cul.NumberFormat);
        }

        public static string GetNumber(this Int64 i, string type = "", Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            if (type == "") return i.ToString(cul.NumberFormat);
            return i.ToString(type, cul.NumberFormat);
        }

        public static string NumberToExpression(this double num, Culture cul = null)
        {
            return num.ToString().NumberToExpression(cul);
        }

        public static string NumberToExpression(this Int64 num, Culture cul = null)
        {
            return num.ToString().NumberToExpression(cul);
        }

        public static string NumberToExpression(this string num, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            switch (cul.BaseLanguage)
            {
                case Languages.English:
                    return NumberToEnglishExpr(num);
                case Languages.Persian:
                    return NumberToPersianExpr(num.TrimEnd('.'));
                default:
                    throw new NotSupportedException("Localizer: Converting number to this language is Not Supported");
            }
        }

        public static Int64 ParseNumberFromExpression(this string s, Culture cul = null)
        {
            if (cul == null) cul = Globals.CurrentCulture;
            switch (cul.BaseLanguage)
            {
                case Languages.English:
                    return NumberFromEnglishExpr(s);
                case Languages.Persian:
                    return NumberFromPersianExpr(s.TrimEnd('.'));
                default:
                    throw new NotSupportedException("Localizer: Parsing number from this language is Not Supported");
            }
        }

        #endregion
    }
}
