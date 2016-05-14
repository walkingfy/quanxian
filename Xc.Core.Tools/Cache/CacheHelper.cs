
/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections;
using System.Web;

namespace Xc.Core.Tools
{
    /// <summary>
    /// CacheHelper帮助类
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// 获取数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public static object GetCache(string cacheKey)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            return objCache[cacheKey];
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="objObject">值</param>
        public static void SetCache(string cacheKey, object objObject)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <param name="objObject">值</param>
        /// <param name="timeout">可调过期时间，设置距离上一次访问之后间隔多长过期</param>
        public static void SetCache(string cacheKey, object objObject, TimeSpan timeout)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, DateTime.MaxValue, timeout, System.Web.Caching.CacheItemPriority.NotRemovable, null);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="absoluteExpiration">绝对过期时间，设置到某个时间过期</param>
        /// <param name="cacheKey">键</param>
        /// <param name="objObject">值</param>
        ///// <param name="slidingExpiration">可调过期时间，设置距离上一次访问之后间隔多长过期，如果需要设置绝对日期，必须为</param>
        public static void SetCache(string cacheKey, object objObject, DateTime absoluteExpiration)
        {
            System.Web.Caching.Cache objCache = HttpRuntime.Cache;
            objCache.Insert(cacheKey, objObject, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">移除指定键的值</param>
        public static void RemoveCache(string cacheKey)
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            cache.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache cache = HttpRuntime.Cache;
            IDictionaryEnumerator cacheEnum = cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cache.Remove(cacheEnum.Key.ToString());
            }
        }
    }
}
