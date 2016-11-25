using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Framework.IO
{
    public class PathHelper
    {
        private static IPathHelper _pathHelper;

        public static IPathHelper Helper
        {
            get
            {
                if (_pathHelper == null)
                    if (HostingEnvironment.IsHosted)
                        _pathHelper = new WebPathHelper();
                    else
                        _pathHelper = new ExeDomainPathHelper();

                return _pathHelper;
            }
        }
        public static string GetDllPath(string dllFileName)
        {
            if (!dllFileName.ToLower().EndsWith(".dll"))
                dllFileName = string.Format("{0}{1}", dllFileName, ".dll");
            return Helper.MapPath(dllFileName);
        }

        public static string BinPath()
        {
            if (HostingEnvironment.IsHosted)
                return HttpRuntime.BinDirectory;

            return string.Empty;
        }
        public static string FindFile(string fileName, string startPath = "")
        {
            var dirpath = "";
            if (!File.Exists(dirpath + fileName))
            {
                if (AppDomain.CurrentDomain != null &&
                    !string.IsNullOrWhiteSpace(AppDomain.CurrentDomain.RelativeSearchPath))
                {
                    dirpath = AppDomain.CurrentDomain.RelativeSearchPath.TrimEnd('\\') + "\\";
                }
                if (!File.Exists(dirpath + fileName))
                {
                    dirpath = Assembly.GetExecutingAssembly().Location.TrimEnd('\\') + "\\";
                }
                if (!File.Exists(dirpath + fileName))
                {
                    dirpath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\";
                }
            }
            if (!File.Exists(dirpath + fileName))
            {
                throw new Exception(fileName + " not found!");
            }
            return dirpath + fileName;
        }
    }
}
