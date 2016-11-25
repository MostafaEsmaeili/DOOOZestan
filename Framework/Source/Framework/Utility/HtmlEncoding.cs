using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Framework.Utility
{
    public  static class HtmlEncoding
    {
        private static readonly Regex EncodedCharRegex = new Regex(@"&#[X]?[0-9|A-F]{1,12};",
                                                                   RegexOptions.Compiled |
                                                                   RegexOptions.IgnoreCase |
                                                                   RegexOptions.CultureInvariant);


        public static string ToUtfCharacters(string input)
        {
            return ConvertInnerText(input, ReplaceWithCharacter);
        }

        private static string ReplaceWithCharacter(string original)
        {
            return EncodedCharRegex.Replace(original, DecodeCharacter);
        }

        private static string DecodeCharacter(Match match)
        {
            string digits = match.ToString().TrimStart(new[] { '#', '&' }).TrimEnd(';').ToUpperInvariant();
            return digits.StartsWith("X") ? HexToString(digits) : DecToString(digits);
        }

        private static string DecToString(string digits)
        {
            return ((char)int.Parse(digits)).ToString();
        }

        private static string HexToString(string digits)
        {
            return ((char)int.Parse(
                digits.Substring(1),
                NumberStyles.HexNumber,
                CultureInfo.InvariantCulture)).ToString();
        }

        private static string ConvertInnerText(string original, Func<string, string> converter)
        {
            var convertedQueue = new Queue<char>(original.Length);
            var innerQueue = new Queue<char>();
            int tagCount = 0;
            bool hasFoundHtml = false;
            foreach (char character in original)
            {
                if (character.Equals('<'))
                {
                    hasFoundHtml = true;
                    if (tagCount == 0 && innerQueue.Count > 0)
                    {
                        var innerString = new string(innerQueue.ToArray());
                        string convertedString = converter.Invoke(innerString);
                        foreach (char convertedCharacter in convertedString)
                        {
                            convertedQueue.Enqueue(convertedCharacter);
                        }
                        innerQueue.Clear();
                    }
                    tagCount += 1;
                    convertedQueue.Enqueue(character);
                    continue;
                }
                if (character.Equals('>'))
                {
                    tagCount -= 1;
                    convertedQueue.Enqueue(character);
                    continue;
                }
                if (tagCount == 0 && hasFoundHtml)
                {
                    innerQueue.Enqueue(character);
                }
                else
                {
                    convertedQueue.Enqueue(character);
                }
            }
            return new string(convertedQueue.ToArray());
        }
    }
}
