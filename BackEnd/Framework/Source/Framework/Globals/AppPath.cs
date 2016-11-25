using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Xml;
using Framework.Utility;

namespace Framework.Globals
{
    public class AppPath
    {
        private static string _rootPath;
        public static string RootPath
        {
            get
            {
                return _rootPath ?? (_rootPath = GetRootPath());
            }
        }
        public static string ApplicationPath
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
                return applicationPath;
            }
        }

        public static string CorePath(string name)
        {
            var address = (Dictionary<string, string>)CustomCache.Get("Core.Addresses");
            if (address != null)
            {
                return address[name];
            }
            address = new Dictionary<string, string>();
            var doc = GetCoreXml();
            var selectSingleNode = doc.SelectSingleNode("Address");
            if (selectSingleNode != null)
                foreach (XmlNode n in selectSingleNode.ChildNodes)
                {
                    var physicalPath = PhysicalPath(n.InnerText);
                    address.Add(n.Attributes["name"].Value, physicalPath);
                }
            CustomCache.Insert("Core.Addresses", address);
            return address[name];
        }

        private static XmlDocument GetCoreXml()
        {
            var doc = new XmlDocument();
            var physicalPath = PhysicalPath("Core.Xml");
            doc.Load(physicalPath);

            return doc;
        }

        public static string GetVirtualPath(string physicalPath)
        {
            if (HttpContext.Current == null)
            {
                throw new Exception("HttpContextIsNull");
            }
            if (!physicalPath.StartsWith(HttpContext.Current.Request.PhysicalApplicationPath))
            {
                throw new InvalidOperationException("Physical path is not within the application root");
            }

            return "~/" + physicalPath.Substring(HttpContext.Current.Request.PhysicalApplicationPath.Length)
                  .Replace("\\", "/");
        }
        public static string MapPath(string path)
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server.MapPath(path);
            return PhysicalPath(path.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("~", ""));
        }

        public static string PhysicalPath(string path)
        {
            return
                string.Concat(GetRootPath().TrimEnd(Path.DirectorySeparatorChar), Path.DirectorySeparatorChar.ToString(),
                              path.TrimStart(Path.DirectorySeparatorChar)).Replace("\\", "/");
        }
        private static string GetRootPath()
        {
            if (_rootPath == null)
            {
                _rootPath = AppDomain.CurrentDomain.BaseDirectory;
                string dirSep = Path.DirectorySeparatorChar.ToString();

                _rootPath = _rootPath.Replace("/", dirSep);

                string filePath = dirSep;

                if (filePath.Length > 0 && filePath.StartsWith(dirSep) && _rootPath.EndsWith(dirSep))
                {
                    _rootPath = _rootPath + filePath.Substring(1);
                }
                else
                {
                    _rootPath = _rootPath + filePath;
                }
            }
            return _rootPath;
        }
    }
}
