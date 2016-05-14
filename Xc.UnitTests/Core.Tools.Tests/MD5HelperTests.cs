/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xc.Core.Tools;

namespace Xc.UnitTests.Core.Tools.Tests
{
    [TestFixture]
    public class MD5HelperTests
    {
        [TestCase("123456", "e10adc3949ba59abbe56e057f20f883e")]
        [TestCase(123456, "e10adc3949ba59abbe56e057f20f883e")]
        [TestCase(123.080, "4923ea2cedc2ce90da8d30cd4fd9c567")]
        [TestCase(null, "")]
        public void ToMD5String_VariousExtension_CheckThm(Object obj,String expected)
        {
            var result = obj.ToMd5String();
            Assert.AreEqual(expected, result);
        }

    }
}
