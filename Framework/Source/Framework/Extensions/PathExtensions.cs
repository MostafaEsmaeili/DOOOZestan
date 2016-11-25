using System;

namespace Framework.Extensions
{
    public static class PathExtensions
    {
        public static string ApplicationPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\";
            }
        }
        public static string MapPath(this string relative)
        {
            relative = relative.Replace("/", @"\");
            relative = relative.TrimStart('~');
            relative = relative.TrimStart('\\');
            return AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\" + relative;
            //System.Web.Hosting.HostingEnvironment.MapPath
        }
    }
}
