using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Anchor.FA.Utility
{
    /// <summary>
    /// 缓存工具类
    /// </summary>
    public class CacheUtility
    {
        public static void SetCacheObject(string key, object obj)
        {
            HttpContext.Current.Cache.Insert(key, obj, null, Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(AppConfig.CacheTimeSeconds));
        }

        public static object GetCacheObject(string key)
        {
            return HttpContext.Current.Cache[key];
        }
    }
}
