/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Tools;
using System.Threading;

namespace Xc.UnitTests.Core.Tools.Tests
{
    [TestFixture]
    public class CacheHelperTests
    {
        [Test]
        public void SetAndGetCache_Normal_ReturnCache()
        {
            CacheHelper.SetCache("Name", "小聪");
            Assert.AreEqual("小聪", CacheHelper.GetCache("Name"));
        }

        [Test]
        public void SetCacheSlidingExpirationParam_PastDue_ReturnNull()
        {
            CacheHelper.SetCache("Name", "小聪", new TimeSpan(1));
            Thread.Sleep(200);
            Assert.IsNull(CacheHelper.GetCache("Name"));
        }

        [Test]
        //[Explicit]
        public void SetCacheAbsoluteExpirationParam_PastDue_ReturnNull()
        {
            CacheHelper.SetCache("Name", "小聪", DateTime.Now.AddMilliseconds(10));
            Thread.Sleep(150);
            Assert.IsNull(CacheHelper.GetCache("Name"));
        }

        [Test]
        public void RemoveCache_Normal_ReturnNull()
        {
            CacheHelper.SetCache("Name", "小聪");
            CacheHelper.RemoveCache("Name");
            Assert.IsNull(CacheHelper.GetCache("Name"));
        }

        public void RemoveAllCache_Normal_ReturnNull()
        {
            CacheHelper.SetCache("Name", "小聪");
            CacheHelper.SetCache("Name12", "小聪123");
            CacheHelper.RemoveAllCache();
            Assert.IsTrue(CacheHelper.GetCache("Name") == null && CacheHelper.GetCache("Name12") == null);
        }
    }
}
