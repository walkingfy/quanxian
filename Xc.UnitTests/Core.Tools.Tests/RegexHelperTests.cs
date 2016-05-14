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

namespace Xc.UnitTests.Core.Tools.Tests
{
    [TestFixture]
    public class RegexHelperTests
    {
        [Test]
        public void IsMatch_RightEmail_ReturnTrue()
        {
            string regx = "^\\s*\\w+(?:\\.{0,1}[\\w-]+)*@[a-zA-Z0-9]+(?:[-.][a-zA-Z0-9]+)*\\.[a-zA-Z]+\\s*$";
            string email = "xiaocong_soft@163.com";
            var result = RegexHelper.IsMatch(email, regx, true);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsMatch_ErrorEmail_ReturnFalse()
        {
            string regx = "^\\s*\\w+(?:\\.{0,1}[\\w-]+)*@[a-zA-Z0-9]+(?:[-.][a-zA-Z0-9]+)*\\.[a-zA-Z]+\\s*$";
            string email = "xiaocong_soft@@s@d@163.com";
            var result = RegexHelper.IsMatch(email, regx, true);
            Assert.IsFalse(result);
        }
    }
}
