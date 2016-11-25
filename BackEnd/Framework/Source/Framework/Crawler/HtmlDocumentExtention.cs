using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Framework.Crawler
{
    public static class HtmlDocumentExtention
    {
        public static List<HtmlTag> GetElementsByClassName(this List<HtmlTag> tags, string className)
        {

            return tags.GetElementsByAttributeValue("class", className);
        }
        public static List<HtmlTag> GetElementsById(this List<HtmlTag> tags, string id)
        {
            return tags.GetElementsByAttributeValue("id", id);
        }
        public static List<HtmlTag> GetElementsByAttributeValue(this List<HtmlTag> tags, string attribute, string value)
        {
            List<HtmlTag> htmlTags = new List<HtmlTag>();
            if (tags != null)
            {

                foreach (HtmlTag tag in tags)
                {
                    string attrValue;
                    if (tag.Attributes.TryGetValue(attribute, out attrValue))
                    {
                        if (attrValue.ToLower() == value.ToLower())
                        {
                            htmlTags.Add(tag);
                        }
                    }
                }

            }
            return htmlTags;
        }

        public static List<HtmlTag> GetElementsByTagName(this HtmlDocument document, string tagName, EndTag endTag)
        {
            string html = document.InnerDocument.Replace('\n', ' ').Replace('\0', ' ');
            MatchCollection matches = Regex.Matches(html, string.Format(@"(<{0}\s.*?>)|(<{0}>)", tagName), RegexOptions.IgnoreCase| RegexOptions.Compiled);
            List<HtmlTag> tags = new List<HtmlTag>();
            foreach (Match match in matches)
            {
                HtmlTag tag = new HtmlTag();
                tag.EndTagType = endTag;
                tag.Name = tagName.ToUpper();
                if (endTag == EndTag.EndTag)
                {
                    var s = Regex.Matches(match.Value, @"\w+");
                    if (s.Count > 0)
                    {
                        tag.Html = TagReader.ReadToEndTag(html, tagName, match);
                    }
                }
                else
                {
                    tag.Html = match.Value;
                }
                tags.Add(tag);
            }
            return tags;
        }




    }
}
