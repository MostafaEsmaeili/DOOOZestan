using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;
using Framework.Globals;

namespace Framework.Utility
{
    

    public class SiteUrls
    {
        private static SiteUrls _instance;
        public static SiteUrls Instance
        {
            get
            {
                return _instance ?? (_instance = new SiteUrls());
            }
        }

        public SiteUrls()
        {
            
        }
        
        public Dictionary<string, string> Paths
        {
            get
            {
                var urls = (Dictionary<string, string>) CustomCache.Get("Core.SiteUrls");
                if (urls==null)
                {
                    urls = LoadUrls();
                    CustomCache.Insert("Core.SiteUrls", urls);
                    return urls;
                }
                return urls;
            }
        }
        public string GetUrls(string name)
        {
            return GetUrls(name, null);
        }
        public string GetUrls(string name, params object[] parameters)
        {
            return GetUrls(name, false, parameters);
        }
        public virtual string GetUrls(string name, bool isPhysicalAddress, params object[] parameters)
        {
            string result;
            if (parameters == null)

                result = Paths[name];

            else
                result = string.Format(Paths[name], parameters);
            if (result.StartsWith("~") && !isPhysicalAddress)
            {
                result = AppPath.ApplicationPath + "/" + result.Remove(0, 2);
            }
            return result;
        }
        protected XmlDocument CreateDoc(string siteUrlsXmlFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(siteUrlsXmlFile);
            return doc;
        }
        protected  Dictionary<string, string> LoadUrls()
        {
            Dictionary<string,string> dic = new Dictionary<string, string>();
            var fileName = AppPath.CorePath("SiteUrls");
            XmlDocument doc = CreateDoc(fileName);
            XmlNode urls = doc.SelectSingleNode("SiteUrls/urls");
            foreach (XmlNode n in urls.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment)
                {
                    if (n.Attributes != null)
                    {
                        string name = n.Attributes["Name"].Value;
                        string path = n.Attributes["Path"].Value.Replace("^", "&");
                        dic.Add(name,path);
                    }
                }
            }
            return dic;

        }

        
        public virtual string AdminHome
        {
            get { return GetUrls("Adminhome"); }
        }

    

        public string JavaScript(string url)
        {
            const string format = @"{0}/script/{1}";
            return string.Format(format, WebApplicationPath, url);
        }


        public string WebApplicationPath
        {
            get
            {
                string applicationPath = "/";

                if (HttpContext.Current != null)
                    applicationPath = HttpContext.Current.Request.ApplicationPath;

                // Are we in an application?
                //
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                else
                {
                    return applicationPath;
                }
            }
        }

        public void AddStylesheetToPage(Page page, string cssName)
        {
            HtmlLink link = new HtmlLink();
            link.Attributes["type"] = "text/css";
            link.Attributes["href"] = string.Format("{0}/Themes/Fa/Style/CSS/{1}", WebApplicationPath, cssName);
            link.Attributes["rel"] = "stylesheet";
            page.Header.Controls.Add(link);
        }

    }
      


    #region ReWrittenUrl

    public class ReWrittenUrl
    {

        #region Field Members

        private string _path;
        private Regex _regex = null;

        #endregion

        #region Ctors

        public ReWrittenUrl(string name, string pattern, string path)
        {
            _path = path;
            _regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        #endregion

        #region Method Members

        public virtual string Convert(string url, string qs)
        {
            if (qs != null && qs.StartsWith("?"))
            {
                qs = qs.Replace("?", "&");
            }
            return string.Format("{0}{1}", _regex.Replace(url, _path), qs);
        }

        public bool IsMatch(string url)
        {
            return _regex.IsMatch(url);
        }

        #endregion

    }

    #endregion

    
}