using System.Text;

namespace Framework.Crawler
{
    public class HtmlDocument
    {
        public HtmlDocument(string url, string postData, BrowserType browserType, Encoding encoding)
        {

        }

        public HtmlDocument(string html)
        {
            InnerDocument = html;
        }

        public string InnerDocument { get; set; }
    }
}
