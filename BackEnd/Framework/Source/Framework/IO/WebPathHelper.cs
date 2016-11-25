using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Framework.IO
{
    /// <summary>
    /// Represents a common helper
    /// </summary>
    public class WebPathHelper : IPathHelper
    {
        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string MapPath(string path)
        {
            if (HttpContext.Current != null)
            {
                return HostingEnvironment.MapPath(path);
            }
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            int binIndex = baseDirectory.IndexOf("\\bin\\");
            if (binIndex >= 0)
                baseDirectory = baseDirectory.Substring(0, binIndex);
            else if (baseDirectory.EndsWith("\\bin"))
                baseDirectory = baseDirectory.Substring(0, baseDirectory.Length - 4);

            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }
    }


}
