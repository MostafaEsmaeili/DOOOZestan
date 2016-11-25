using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Framework.Crawler
{
    public class HtmlTag
    {
        public string Name { get; set; }

        private string _html;
        public string Html
        {
            get { return _html; }
            set
            {
                _html = value;
                _innerHtml = null;
                _attributes = null;
                _children = null;
            }
        }

        private List<HtmlTag> _children;
        public List<HtmlTag> Children
        {
            get { return _children ?? (_children = GetChildNodes()); }
        }

        private string _innerText;
        public string InnerText
        {
            get { return _innerText ?? (_innerText = GetInnerText()); }
        }

        private Dictionary<string, string> _attributes;
        public Dictionary<string, string> Attributes
        {
            get { return _attributes ?? (_attributes = GetAttributes()); }
        }

        private string _innerHtml;
        public string InnerHtml
        {
            get
            {
                return _innerHtml ?? (_innerHtml = GetInnerHtml());
            }
        }
        public EndTag EndTagType { get; set; }

        private List<HtmlTag> GetChildNodes()
        {
            List<HtmlTag> nodes = new List<HtmlTag>();
            if (EndTagType != EndTag.EndTag)
            {
                return nodes;
            }
            string tempInner = InnerHtml.Replace('\n', ' ').Replace('\0', ' ');
            var temp = Regex.Matches(tempInner, @"(?!</)(<(\w+).*?>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            while (temp.Count > 0)
            {
                EndTag endType = EndTag.SelfEnd;
                var node = new HtmlTag();
                node.Name = temp[0].Groups[2].Value.ToUpper();
                var s = Regex.Matches(temp[0].Value, @"\w+");
                if (s.Count > 0)
                {
                    node.Html = TagReader.ReadToEndTag(tempInner, node.Name, temp[0], out endType);
                }
                tempInner = tempInner.Substring(node.Html.Length);
                temp = Regex.Matches(tempInner, @"(?!</)(<(\w+).*?>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                node.EndTagType = endType;
                nodes.Add(node);
            }
            return nodes;
        }

        private Dictionary<string, string> GetAttributes()
        {
            var dic = new Dictionary<string, string>();
            var temp = Regex.Matches(Html, string.Format("(<{0}.*?>)", Name), RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (temp.Count > 0)
            {
                string input = temp[0].Value;
                var attrs = Regex.Matches(input.Replace('\n', '\0'), @"(\S+)=[" + "\"']?((?:.(?![\"']?" + @"\s+(?:\S+['" + "\"])=|[>" + "\"']))+.)[\"']?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                foreach (Match attr in attrs)
                {
                    int i = attr.Value.IndexOf("=");
                    var substring = attr.Value.Substring(i + 1);
                    if ((substring.StartsWith("'") && substring.EndsWith("'")) || (substring.StartsWith("\"") && substring.EndsWith("\"")))
                    {
                        substring = substring.Substring(1, substring.Length - 2);
                    }
                    dic.Add(attr.Value.Substring(0, i), substring);
                }
            }
            return dic;
        }
        private string GetInnerHtml()
        {
            string temp;
            if (EndTagType == EndTag.EndTag)
            {
                temp = Regex.Replace(Html.Replace('\n', ' ').Replace('\0', ' '), string.Format("(<{0}.*?>)", Name), string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                return Regex.Replace(temp.Replace('\n', ' ').Replace('\0', ' '), string.Format(@"</{0}>$", Name), string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            }
            return string.Empty;
        }
        private string GetInnerText()
        {
            return Regex.Replace(InnerHtml, "<.*?>", string.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }
}
