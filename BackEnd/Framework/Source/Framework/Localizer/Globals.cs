using System;

namespace Framework.Localizer
{
    public static class Globals
    {
        public static bool Inited
        {
            get { return LocalizerCore.Inited; }
        }

        public static Int64 CurrentMem
        {
            get { return LocalizerCore.CurrentMem; }
        }

        public static Int64 MaxMem
        { 
            get { return LocalizerCore.Maxbytes; }
        }

        public static Culture CurrentCulture
        {
            get { return LocalizerCore.CurrentCulture; }
        }

        public static void Init(string cachefolder,
            Guid guid,
            GetCultureMode mode = GetCultureMode.Web,
            Culture preferedculture = null)
        {
            LocalizerCore.Init(cachefolder, guid, mode, preferedculture);
        }

        public static void Init(string cachefolder, Guid guid, Culture culture)
        {
            Init(cachefolder, guid, GetCultureMode.Web, culture);
        }

        public static bool SetCurrentCulture(Guid guid, Culture culture)
        {
            return LocalizerCore.SetCurrentCulture(guid, culture);
        }

    }
}
