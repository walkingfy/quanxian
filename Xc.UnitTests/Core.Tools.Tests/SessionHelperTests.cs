/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xc.Core.Tools;

namespace Xc.UnitTests.Core.Tools.Tests
{
    [TestFixture]
    [Ignore]
    public class SessionHelperTests
    {

        [Test]
        public void SetSession_NoExists()
        {
            SessionHelper.SetSession("Name", "xiaocong_soft");
            Assert.AreEqual("xiaocong_soft", SessionHelper.GetSessionToString("Name"));
        }

        [Test]
        public void SetSession_AddMoreSession()
        {
            SessionHelper.AddSession("Name", "xiaocong_soft1");
            SessionHelper.AddSession("Name", "xiaocong_soft2");
            SessionHelper.AddSession("Name", "xiaocong_soft3");
            var arr = SessionHelper.GetSessions("Name");
            Assert.AreEqual(3, arr.Count());
        }

        [Test]
        public void DeleteSession()
        {
            SessionHelper.AddSession("Name", "xiaocong_soft1");
            SessionHelper.AddSession("Name", "xiaocong_soft2");
            SessionHelper.AddSession("Name", "xiaocong_soft3");
            SessionHelper.DeleteSession("Name");
            Assert.AreEqual(null, SessionHelper.GetSession("Name"));
        }
    }
}
