using System;
using System.IO;

namespace Framework.IO
{
    public class ExeDomainPathHelper : IPathHelper
    {
        public string MapPath(string path)
        {
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            var result = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            return result;
        }
    }
}
