using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Framework.Crawler
{
    public class TagReader
    {


        public static string ReadToEndTag(string html, string tagName, Match startTag, out EndTag endType)
        {

            string temp = html.Substring(startTag.Index);
            MatchCollection endMatches = Regex.Matches(temp, string.Format("</{0}>", tagName), RegexOptions.IgnoreCase);
            MatchCollection startMatches = Regex.Matches(temp, string.Format("(<{0}.*?>)", tagName), RegexOptions.IgnoreCase);
            var stack = new Stack<int>();
            if (endMatches.Count == 0)
            {
                if (startTag.Value.EndsWith("/>"))
                {
                    endType = EndTag.SelfEnd;
                }
                else
                {
                    endType = EndTag.NoEnd;
                }
                return startTag.Value;
            }
            if (endMatches.Count > 0 || startMatches.Count > 0)
            {
                int i = 0;
                int j = 0;
                int startIndex = startMatches[i].Index;
                int endIndex = endMatches[j].Index;
                while (j < endMatches.Count || i < startMatches.Count)
                {
                    if (startIndex < endIndex)
                    {
                        stack.Push(startIndex);
                        if (startMatches.Count > ++i)
                        {
                            startIndex = startMatches[i].Index;
                        }
                        else
                        {
                            startIndex = temp.Length + 1;
                        }

                    }
                    else
                    {
                        if (stack.Count > 0)
                        {
                            stack.Pop();
                        }
                        else
                        {
                            int h = 0;
                            //TODO
                        }
                        if (endMatches.Count > ++j)
                        {
                            endIndex = endMatches[j].Index;
                        }


                    }
                    if (stack.Count == 0)
                    {
                        endType = EndTag.EndTag;
                        return temp.Substring(0, endMatches[j - 1].Index + endMatches[0].Length);
                    }
                }
            }
            endType = EndTag.NoEnd;
            return string.Empty;
        }


        public static string ReadToEndTag(string html, string tagName, Match startTag)
        {
            EndTag endTag;
            return ReadToEndTag(html, tagName, startTag, out endTag);
        }

    }
}
