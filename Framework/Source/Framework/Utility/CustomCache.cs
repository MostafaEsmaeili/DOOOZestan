
#region Using

using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

#endregion

#region


#endregion

namespace Framework.Utility
{
    /// <summary>
    /// Summary description for cache.
    /// </summary>
    public class CustomCache
    {
        private const int HourFactor = 720;
        private static readonly System.Web.Caching.Cache _cache;
        public static readonly int DayFactor = 17280;
        public static readonly int MinuteFactor = 12;
        public static readonly double SecondFactor = 0.2;


        private static int Factor = 5;

        /// <summary>
        /// Static initializer should ensure we only have to look up the current cache
        /// instance once.
        /// </summary>
        static CustomCache()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                _cache = context.Cache;
            }
            else
            {
                _cache = HttpRuntime.Cache;
            }
        }

        private CustomCache()
        {
        }

        public static void ReSetFactor(int cacheFactor)
        {
            Factor = cacheFactor;
        }

        /// <summary>
        /// Removes all items from the Cache
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
                _cache.Remove(cacheEnum.Key.ToString());
        }

        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator cacheEnum = _cache.GetEnumerator();
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            while (cacheEnum.MoveNext())
            {
                if (regex.IsMatch(cacheEnum.Key.ToString()))
                    _cache.Remove(cacheEnum.Key.ToString());
            }
        }

        /// <summary>
        /// Removes the specified key from the cache
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            _cache.Remove(key);
        }

        /// <summary>
        /// Insert the current "obj" into the cache. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Insert(string key, object obj)
        {
            Insert(key, obj, null, HourFactor);
        }

        public static void Insert(string key, object obj, CacheDependency dep)
        {
            Insert(key, obj, dep, HourFactor * 12);
        }

        public static void Insert(string key, object obj, int seconds)
        {
            Insert(key, obj, null, seconds);
        }

        public static void Insert(string key, object obj, int seconds, CacheItemPriority priority)
        {
            Insert(key, obj, null, seconds, priority);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds)
        {
            Insert(key, obj, dep, seconds, CacheItemPriority.Normal);
        }

        public static void Insert(string key, object obj, CacheDependency dep, int seconds, CacheItemPriority priority)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.Now.AddSeconds(Factor * seconds), TimeSpan.Zero, priority, null);
            }
        }

        public static void MicroInsert(string key, object obj, int secondFactor)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, null, DateTime.Now.AddSeconds(Factor * secondFactor), TimeSpan.Zero);
            }
        }

        /// <summary>
        /// Insert an item into the cache for the Maximum allowed time
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public static void Max(string key, object obj)
        {
            Max(key, obj, null);
        }

        public static void Max(string key, object obj, CacheDependency dep)
        {
            if (obj != null)
            {
                _cache.Insert(key, obj, dep, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.AboveNormal, null);
            }
        }

        public static object Get(string key)
        {
            return _cache[key];
        }

        public static int SecondFactorCalculate(int cacheTime)
        {
            return Convert.ToInt32(Math.Round((cacheTime * SecondFactor)));
        }
    }
}