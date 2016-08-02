using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;

namespace Umbraco7.Helpers
{
    public class CacheHelper : BaseCacheHelper
    {
        private static CacheHelper _instance;

        public static CacheHelper Instance
        {
            get { return _instance ?? (_instance = new CacheHelper()); }
        }

        public static object Get<T>(string key)
        {
            return Instance.GetItem(key, false);
        }

        public static void Remove(string key)
        {
            Instance.RemoveItem(key);
        }

        public static void Set(string key, object value, DateTimeOffset cacheDuration)
        {
            Instance.AddItem(key, value, cacheDuration);
        }

    }
}