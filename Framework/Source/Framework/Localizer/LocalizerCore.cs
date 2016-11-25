#region usings

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web;
using Castle.Core.Logging;
using Framework.IoC;

#endregion

namespace Framework.Localizer
{
    [Serializable]
    public class DictionaryAccessTime
    {
        public DictionaryAccessTime(ConcurrentDictionary<string, Message> key)
        {
            Key = key;
            Value = DateTime.Now;
        }
        public ConcurrentDictionary<string, Message> Key { get; set; }
        public DateTime Value { get; set; }
    }
    internal static class LocalizerCore
    {
        private const int MaxMB = 15;

        internal const int Maxbytes = MaxMB * 1024 * 1024, MinSeconds = 120;
        internal const string CookieName = "LocalizerCulture", TempFilesExts = ".localizer.tmp";
        internal const string NotFoundBeginString = "";
        internal const string NotFoundEndString = "";
        internal const char NotFoundTitleKey = '-';

        internal static readonly bool ThrowOnNotFound = false;
        private static bool _inited = false, _shouldberemoved = false;
        private static readonly Culture DefaultCulture = SupportedCultures.FaIr;

        private static readonly
            ConcurrentDictionary
                <string,
                    ConcurrentDictionary
                        <Culture,
                            ConcurrentDictionary
                                <MessageType, DictionaryAccessTime>>>
            Dic =
                new ConcurrentDictionary
                    <string,
                        ConcurrentDictionary
                            <Culture,
                                ConcurrentDictionary
                                    <MessageType, DictionaryAccessTime>>>();

        private static GetCultureMode _mode = GetCultureMode.Web;

        private static string _cacheFolder = "";

        private static Int64 _mem = -1;

        private static Guid? _guid = null;

        private static string UniqueSessionIdentifier
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_uSI))
                {
                    var guid = _guid.Value.ToString("N");

                    _uSI = _timeInited.Year.ToString("0000") + _timeInited.Month.ToString("00") +
                           _timeInited.Day.ToString("00") + _timeInited.Hour.ToString("00") +
                           _timeInited.Minute.ToString("00") +
                           _timeInited.Second.ToString("00") + _timeInited.Millisecond.ToString("000") +
                           guid.Substring(guid.Length - 4);

                }
                return _uSI;
            }
        }

        private static string _uSI;

        private static DateTime _timeInited;

        #region Private --------------------------------------------------------------------------------

        private static Culture _preferedCulture = null;

        private static Culture GetCultureByName(string abr)
        {
            var successfull = SupportedCultures.Parse(abr);
            if (successfull != null) return successfull;
            abr = abr.ToLower().Trim();
            if (abr.Length > 2) abr = abr.Substring(0, 2);
            if ((abr.StartsWith("pe")) || (abr.StartsWith("fa"))) return SupportedCultures.FaIr;
            if (abr.StartsWith("en")) return SupportedCultures.EnUs;
            return null;
        }

        private static string SafeCollectionName(string str)
        {
            bool btemp;
            return SafeCollectionName(str, out btemp);
        }

        private static ConcurrentDictionary<string, KeyValuePair<string, bool>> SafeNames = new ConcurrentDictionary<string, KeyValuePair<string, bool>>(StringComparer.InvariantCultureIgnoreCase);
        private static string SafeCollectionName(string str, out bool containedinvalidchars)
        {
            if (SafeNames.ContainsKey(str))
            {
                var val = SafeNames[str];
                containedinvalidchars = val.Value;
                return val.Key;
            }
            string name = "";
            const string allowed = "_";
            containedinvalidchars = false;
            for (int i = 0; i < str.Length; i++)
            {
                if (((char.IsLetterOrDigit(str[i]))) || (allowed.Contains(str[i].ToString())))
                    name = name + str[i].ToString().ToLower();
                else containedinvalidchars = true;
            }
            if (name.Length < 0)
                throw new Exception("Localizer: Invalid collection name, please use letters or numbers!");
            SafeNames.TryAdd(str, new KeyValuePair<string, bool>(name, containedinvalidchars));
            return name;
        }

        private static string SolveCacheFolder(string cachefolder, Guid gu)
        {
            if ((cachefolder.StartsWith("~")) || (cachefolder.StartsWith("/")))
                cachefolder = IO.PathHelper.Helper.MapPath(cachefolder);

            Debug.Assert(cachefolder != null, "cachefolder != null");

            cachefolder = cachefolder.Replace("/", "\\").TrimEnd('\\');
            if (!Directory.Exists(cachefolder))
                throw new Exception("Localizer: Cannot find Cache folder on: " + cachefolder);
            string tempfile = cachefolder + "\\Localizer_" + gu.ToString() + ".init" + TempFilesExts;
            bool create = true;
            try
            {
                if (File.Exists(tempfile)) create = false;
                File.WriteAllText(tempfile, DateTime.Now.ToString());
                create = false;
                File.AppendAllText(tempfile, DateTime.Now.ToString());
                //var files = Directory.GetFiles(cachefolder + "\\", "*.init" + TempFilesExts);
                //foreach (var file in files)
                //{
                File.Delete(tempfile);
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Localizer: Cannot " + (create ? "Create" : "Modify") + " files inside cache folder! " + ex.Message,
                    ex);
            }
            return cachefolder;
        }

        private static string GetCacheFileName(string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            Checkforinit();
            return (_cacheFolder + "\\" + UniqueSessionIdentifier + "." + (SafeCollectionName(collectionName) + "." + culture + "." +
                                                              msgmode.ToString() + ".resx" +
                                                              TempFilesExts).TrimStart('.'));
        }

        private static bool DictionaryExistsInMem(string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            Checkforinit();
            collectionName = SafeCollectionName(collectionName);
            if (!Dic.ContainsKey(collectionName))
            {
                lock (Dic)
                {
                    if (!Dic.ContainsKey(collectionName))
                    {
                        Dic.TryAdd(collectionName,
                            new ConcurrentDictionary
                                <Culture,
                                    ConcurrentDictionary
                                        <MessageType, DictionaryAccessTime>
                                    >());

                    }
                }
            }
            if (!Dic[collectionName].Keys.Contains(culture))
            {
                lock (Dic)
                {
                    if (!Dic[collectionName].Keys.Contains(culture))
                    {
                        Dic[collectionName].TryAdd(culture,
                            new ConcurrentDictionary
                                <MessageType, DictionaryAccessTime>
                                ());
                    }
                }
            }
            return Dic[collectionName][culture].ContainsKey(msgmode);

        }


        private static ConcurrentDictionary<string, Message> GetOrFetch(string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            Checkforinit();
            collectionName = SafeCollectionName(collectionName);
            string cachename = GetCacheFileName(collectionName, culture, msgmode);

            if (!DictionaryExistsInMem(collectionName, culture, msgmode))
            {
                if (File.Exists(cachename))
                {

                    FileStream stream = null;
                    try
                    {
                        stream = SafeOpenAndLockForRead(cachename);

                        AddDictionaryFromResX(false, stream
                            ,
                            collectionName,
                            culture,
                            msgmode);
                    }
                    finally
                    {
                        if (stream != null) stream.Close();
                    }
                }
                else return null;
            }
            CheckForCaches(collectionName, culture, msgmode);
            return Dic[collectionName][culture][msgmode].Key;
        }

        private static void AddDictionaryFromResX(bool cacheit,
            Stream stream,
            string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            ResXResourceReader rsxr = new ResXResourceReader(stream);
            Dictionary<string, string> dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (DictionaryEntry d in rsxr)
            {
                if (d.Key as string == null || d.Value as string == null) continue;
                dic.Add(d.Key.ToString(), d.Value.ToString());
            }
            rsxr.Close();
            if (stream.CanRead || stream.CanSeek || stream.CanWrite) stream.Close();
            AddDictionary(cacheit, dic, collectionName, culture, msgmode);
        }

        private static Culture GetCulture()
        {
            Checkforinit();
            if (_mode == GetCultureMode.Web)
            {
                var httpcont = HttpContext.Current;
                HttpRequest req = null;
                if (httpcont != null)
                {
                    try
                    {
                        req = httpcont.Request;
                    }
                    catch
                    {
                        req = null;
                    }
                }
                if (req != null)
                {
                    if (httpcont.Request.Cookies[CookieName] != null)
                    {
                        var detlang = GetCultureByName(httpcont.Request.Cookies[CookieName].Value);
                        if (detlang != null) return detlang;
                    }
                    if (_preferedCulture != null)
                    {
                        if (_guid != null) SetCurrentCulture(_guid.Value, _preferedCulture);
                        return _preferedCulture;
                    }
                    var langs = httpcont.Request.UserLanguages;
                    if ((langs != null) && (langs.Length > 0))
                    {
                        Culture detectedlang = null;
                        foreach (var lang in langs)
                        {
                            detectedlang = GetCultureByName(lang);
                            if (detectedlang != null) break;
                        }
                        if (detectedlang != null) return detectedlang;
                    }
                }
                return null;
            }
            //else
            {
                //return GetCultureByName(CultureInfo.CurrentCulture.Name);
                return CurrentStaticCulture;
            }
        }

        private static Culture CurrentStaticCulture { get; set; }

        private static void Checkforinit()
        {
            if (!_inited) throw new Exception("Localizer: Not Initialized!");
        }

        private static DateTime _lastCleanUp;
        private static void CheckForCaches(string collectioName = "", Culture culture = null, MessageType msgMode = MessageType.General)
        {
            DateTime dt = DateTime.Now;
            if (culture != null)
            {
                DictionaryAccessTime dic;
                if (Dic[collectioName][culture].TryGetValue(msgMode, out dic)) dic.Value = dt;
            }
            if (_shouldberemoved && (dt - _lastCleanUp).TotalSeconds > MinSeconds)
            {
                _lastCleanUp = dt;
                foreach (var kpp in Dic.SelectMany(kp => kp.Value))
                {
                    foreach (MessageType messageType in kpp.Value.Keys)
                    {
                        if (((dt - kpp.Value[messageType].Value).TotalSeconds > MinSeconds))
                        {
                            DictionaryAccessTime obj;
                            kpp.Value.TryRemove(messageType, out obj);
                        }
                    }
                }
            }

        }

        private static void GetAndSetCurrentCulture(Guid guid)
        {
            if ((_guid == null) ||
                ((_guid != null) && (((Guid)_guid).ToString("d") == guid.ToString("d"))))
            {
                Culture culturedetected;
                try
                {
                    culturedetected = GetCulture();
                }
                catch
                {
                    culturedetected = null;
                }
                if (culturedetected == null)
                {
                    culturedetected = _preferedCulture;
                    //_defaultCulture = preferedculture;
                }
                if (culturedetected == null) culturedetected = DefaultCulture;
                SetCurrentCulture(guid, culturedetected);
            }
            else
                throw new ArgumentException(
                    "Localizer: Invalid guid, please pass last guid you've used to set active culture, and then you will be able to change it again.",
                    "guid");
        }

        #endregion Private -------------------------------------------------------------------------

        #region internal -----------------------------------------------------------------------------

        internal static bool Inited
        {
            get { return _inited; }
        }

        internal static Int64 CurrentMem
        {
            get
            {
                if (_mem < 0)
                {
                    BinaryFormatter b = new BinaryFormatter();
                    MemoryStream m = new MemoryStream();
                    b.Serialize(m, Dic);
                    _mem = m.Length;
                }
                return _mem;
            }
        }


        internal static Culture CurrentCulture
        {
            get
            {
                Checkforinit();
                var cul = GetCulture();
                if (cul == null) return DefaultCulture;
                return cul;
            }
        }

        internal static void Init(string cachefolder,
            Guid guid,
            GetCultureMode mode = GetCultureMode.Web,
            Culture preferedculture = null)
        {
            if (_inited) return;
            _inited = true;
            _guid = guid;
            _cacheFolder = SolveCacheFolder(cachefolder, guid);
            _mode = mode;
            _timeInited = DateTime.Now;
            _preferedCulture = preferedculture;
            try
            {
                CleanCache();
            }
            catch
            {
                //NOOP!
            }
            GetAndSetCurrentCulture(guid);
        }

        private static void CleanCache()
        {
            var files = System.IO.Directory.GetFiles(_cacheFolder,  "*.resx" + TempFilesExts);
            foreach (string file in files)
            {
                var fi = new FileInfo(file);
                if (fi.Name.Length < 8) continue;
                int year = 0, month = 0, day = 0;
                int.TryParse(fi.Name.Substring(0, 4), out year);
                int.TryParse(fi.Name.Substring(4, 2), out month);
                int.TryParse(fi.Name.Substring(6, 2), out day);
                if (year < 2000 || year > 3000 || month < 1 || month > 12 || day < 1 || day > 31) continue;
                if (day > DateTime.DaysInMonth(year, month)) continue;
                var dt = new DateTime(year, month, day);
                if ((DateTime.Now - dt).TotalDays >= 3 && fi.Exists)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        //NOOP
                    }
                }

                //_timeInited.Year.ToString("0000") + _timeInited.Month.ToString("00") +
                //           _timeInited.Day.ToString("00")
            }
        }


        internal static bool SetCurrentCulture(Guid guid, Culture culture)
        {
            Checkforinit();
            if ((_guid == null) ||
                ((_guid != null) && (((Guid)_guid).ToString("d") == guid.ToString("d"))))
            {
                //CurrentCulture = culture;
                _guid = guid;
                if (_mode == GetCultureMode.Web)
                {
                    if (HttpContext.Current != null)
                    {
                        var httpcont = HttpContext.Current;

                        if (httpcont == null)
                            return false; //throw new Exception("Localizer: Cannot find Current httpcontext!");
                        HttpResponse resp;
                        try
                        {
                            resp = httpcont.Response;
                        }
                        catch
                        {
                            resp = null;
                        }
                        if (resp != null)
                        {
                            if (httpcont.Response.Cookies[CookieName] != null)
                                httpcont.Response.Cookies.Remove(CookieName);
                            httpcont.Response.Cookies.Add(new HttpCookie(CookieName, culture.ToString()));

                            //Just if Called before writing response and get a new request.
                            if (httpcont.Request.Cookies[CookieName] != null)
                                httpcont.Request.Cookies.Remove(CookieName);
                            httpcont.Request.Cookies.Add(new HttpCookie(CookieName, culture.ToString())
                            {
                                Expires = DateTime.Now.AddYears(5)
                            });
                        }
                        else return false;
                    }
                }
                CurrentStaticCulture = culture;
                return true;
            }
            throw new ArgumentException(
                "Localizer: Invalid guid, please pass last guid yo've used to set active culture, and then you will be able to change it again.",
                "guid");
            //Remove exception?
            //return false;
        }

        internal static void AddDictionary(bool cacheit,
            IEnumerable<KeyValuePair<string, string>> idic,
            string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            Checkforinit();
            collectionName = SafeCollectionName(collectionName);
            if (!DictionaryExistsInMem(collectionName, culture, msgmode))
            {
                lock (Dic)
                {
                    Dic[collectionName][culture].TryAdd(msgmode,
                        new DictionaryAccessTime(
                            new ConcurrentDictionary<string, Message>(
                                StringComparer.OrdinalIgnoreCase)));
                }
            }
            string cachename = GetCacheFileName(collectionName, culture, msgmode);
            ResXResourceWriter rw = null;
            lock (Dic)
            {
                if (cacheit) rw = new ResXResourceWriter(File.Open(cachename, FileMode.Create));

                foreach (var entry in idic)
                {
                    string bdy = entry.Value;
                    string tit = "";
                    if (bdy.Contains(";"))
                    {
                        tit = !bdy.StartsWith(";") ? bdy.Substring(0, bdy.IndexOf(';')) : "";
                        bdy = !bdy.EndsWith(";") ? bdy.Substring(bdy.IndexOf(';') + 1) : "";
                    }
                    Dic[collectionName][culture][msgmode].Key[entry.Key] = new Message(entry.Key)
                    {
                        Body = bdy,
                        Title = tit,
                        MessageType = msgmode
                    };
                    if (rw != null) rw.AddResource(entry.Key, entry.Value);
                }

                if (rw != null) rw.Close();
            }
            _mem = -1;
            _shouldberemoved = CurrentMem > Maxbytes;
            CheckForCaches();
        }

        internal static ConcurrentDictionary<string, Message> GetDictionary(string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            Checkforinit();
            return GetOrFetch(collectionName, culture, msgmode);
        }

        internal static Message GetByKey(
            string key,
            string collectionName = "",
            Culture culture = null,
            MessageType msgmode = MessageType.General,
            bool checkExistanceOnly = false)
        {
            if (string.IsNullOrWhiteSpace(key))
                return new Message(new string(NotFoundTitleKey, 2))
                {
                    Body = NotFoundBeginString + new string(NotFoundTitleKey, 4) + NotFoundEndString,
                    Title = NotFoundBeginString + new string(NotFoundTitleKey, 3) + NotFoundEndString
                };
            Checkforinit();
            Message msg;
            if (culture == null) culture = CurrentCulture;
            var dic = GetOrFetch(collectionName, culture, msgmode);
            var found = dic.TryGetValue(key, out msg);
            if (!found) msg = null;

            //foreach (
            //    var kp in
            //        Dic.Where(
            //            lst =>
            //                (SafeCollectionName(collectionName) == "") ||
            //                (SafeCollectionName(lst.Key) == SafeCollectionName(collectionName))).
            //            SelectMany(
            //                lst =>
            //                    lst.Value.Where(
            //                        kp =>
            //                            ((culture == null) && (Equals(kp.Key, CurrentCulture))) ||
            //                            ((culture != null) &&
            //                             (culture.Key.ToLower() == kp.Key.Key.ToLower())))))
            //{
            //    GetOrFetch(collectionName, kp.Key, msgmode);
            //    foreach (var lt in kp.Value.Where(lt => (lt.Key == msgmode)))
            //    {
            //        try
            //        {
            //            msg =
            //                lt.Value.Key.FirstOrDefault(
            //                    p =>
            //                        p.Key.Replace(" ", "").FixForPersian() ==
            //                        key.Replace(" ", "").FixForPersian()).Value;
            //            if (msg == default(Message))
            //                msg = lt.Value.Key.ContainsKey(key) ? lt.Value.Key[key] : default(Message);
            //        }
            //        catch
            //        {
            //            msg = null;
            //        }

            //        break;
            //    }
            //}

            //if (Dic.ContainsKey(collectionName) && Dic[collectionName].ContainsKey(culture) &&
            //    Dic[collectionName][culture].ContainsKey(msgmode))
            //{
            //    var theDic = Dic[collectionName][culture][msgmode];
            //    theDic.Key.TryGetValue(key, out msg);
            //}
            if (msg != null) return msg;
            var errmsg = "Localizer cannot find Key '" + key + "' in selected area: '" +
                         collectionName + "/" +
                         (culture != null ? culture.Key : ".") + "/" + msgmode + "'";

            if (checkExistanceOnly)
            {
                errmsg += "(HasKey)";
            }
            LogError(errmsg);
            if (checkExistanceOnly)
            {
                return null;
            }

            if (ThrowOnNotFound)
            {
                throw new KeyNotFoundException(errmsg);
            }
            return new Message(key) { Body = NotFoundBeginString + key + NotFoundEndString };
        }

        private static void LogError(string errmsg)
        {
            try
            {
                var logger = CoreContainer.Container.Resolve<ILogger>();
                if (logger != null) logger.Error(errmsg);
            }
            catch
            {
                //NOOP - logging is not so important
                System.Diagnostics.Debug.WriteLine(errmsg);
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine(errmsg);
            }
        }

        internal static bool HasKey(string key,
            string collectionName = "",
            Culture culture = null,
            MessageType msgmode = MessageType.General)
        {
            return (GetByKey(key, collectionName, culture, msgmode, true) != null);
        }

        internal static void AddDictionaryFromResX(Stream stream,
            string collectionName,
            Culture culture,
            MessageType msgmode = MessageType.General)
        {
            GetOrFetch(collectionName, culture, msgmode);
            AddDictionaryFromResX(true, stream, collectionName, culture, msgmode);
        }

        #endregion internal --------------------------------------------------------------------------------

        private static Random rnd = new Random();
        public static FileStream SafeOpenAndLockForRead(string filePath)
        {
            FileStream stream = null;
            var fi = new FileInfo(filePath);
            var tryCount = 0;
            var lastErrorMessage = "";
            while (stream == null)
            {

                tryCount++;
                if (tryCount > 30)
                {
                    throw new Exception("Cannot open file: " + filePath + " " + lastErrorMessage);
                }
                //try
                //{
                while (IsFileLockedOpenIfNotLocked(fi, out stream, out lastErrorMessage))
                {
                    Thread.Sleep(rnd.Next(10, 50));
                }

                //stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                //stream.Lock(0, stream.Length);
                //}
                //catch (Exception ex)
                //{
                //    lastErrorMessage = ex.Message;
                //    Thread.Sleep(rnd.Next(10, 20));
                //    if (stream != null) stream.Close();
                //    stream = null;
                //}
            }

            return stream;
        }
        private static bool IsFileLockedOpenIfNotLocked(FileInfo file, out FileStream stream, out string errorMessage)
        {
            //FileStream
            stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
                errorMessage = "";
            }
            catch (IOException ex)
            {
                if (stream != null) stream.Close();
                stream = null;
                errorMessage = ex.Message;
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                if (stream != null) stream.Close();
                stream = null;
                return true;
            }
            //finally
            //{
            //    if (stream != null)
            //        stream.Close();
            //}

            //file is not locked
            return false;
        }
    }
}