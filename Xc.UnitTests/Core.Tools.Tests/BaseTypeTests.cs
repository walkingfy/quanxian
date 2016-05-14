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
    public class BaseTypeTests
    {
        [Test]
        public void ToInt_Bad_Extension_Return0()
        {
            //准备
            String str = "为恶";
            //操作
            var result = str.ToInt();
            //断言
            Assert.True(result == 0);
        }

        [TestCase("10", 10)]
        [TestCase("sdfsdf", 0)]
        public void ToInt_VariousExtension_CheckThem(String str, int expected)
        {
            //操作
            var result = str.ToInt();
            //断言
            Assert.AreEqual(expected, result);
        }

        //[Test]
        [Ignore]
        public void ToInt_EmptyValue_ThrowsException()
        {
            Object obj = null;
            var ex = Assert.Catch<Exception>(() => obj.ToInt());
            StringAssert.Contains("未将对象引用设置到对象的实例", ex.Message);
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(-123.04, -123)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04", -123)]
        public void ToInt_VariousExtension_CheckThem(Object obj, int expected)
        {
            var result = obj.ToInt();
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(-123.04, null)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04", null)]
        public void ToIntOrNull_VariousExtension_CheckThem(Object obj, int? expected)
        {
            var result = obj.ToIntOrNull();
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, -1)]
        [TestCase("", -1)]
        [TestCase(-123.04, -1)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04", -1)]
        public void ToIntDefaultValue_VariousExtension_CheckThem(Object obj, int? expected)
        {
            var result = obj.ToInt(-1);
            Assert.AreEqual(expected, result);
        }


        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(-123.04, -123.04)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04", -123.04)]
        [TestCase("-123.0453", -123.0453)]
        public void ToDouble_VariousExtension_CheckThem(Object obj, double expected)
        {
            var result = obj.ToDouble();
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(-123.04, -123.0)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04", -123.0)]
        [TestCase("-123.0553", -123.1)]
        public void ToDoubleByDigits_VariousExtension_CheckThem(Object obj, double expected)
        {
            var result = obj.ToDouble(1);
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(-123.04, -123.04)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04", -123.04)]
        [TestCase("-123.0453", -123.0453)]
        public void ToDoubleOrNull_VariousExtension_CheckThem(Object obj, object expected)
        {
            var result = obj.ToDoubleOrNull();
            if (expected == null)
                Assert.AreEqual(expected, null);
            else
                Assert.AreEqual(expected, result);
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(-123.04, -123.04)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04", -123.04)]
        [TestCase("-123.0453", -123.0453)]
        public void ToDoubleDefaultValue_VariousExtension_CheckThem(Object obj, object expected)
        {
            var result = obj.ToDouble(0);
            if (expected == null)
                Assert.AreEqual(expected, null);
            else
                Assert.AreEqual(expected, result);
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(-123.0453, -123.0453)]
        [TestCase(-123, -123)]
        [TestCase("-123", -123)]
        [TestCase("-123.04523", -123.04523)]
        public void ToDecimal_VariousExtension_CheckThem(Object obj, Decimal expected)
        {
            var result = obj.ToDecimal();
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(-123.0453, -123.0453d)]
        [TestCase(-123, -123d)]
        [TestCase("-123", -123d)]
        [TestCase("-123.04523", -123.04523d)]
        public void ToDecimalOrNull_VariousExtension_CheckThem(Object obj, object expected)
        {
            var result = obj.ToDecimalOrNull();
            if (expected == null)
                Assert.AreEqual(expected, null);
            else
                Assert.AreEqual(expected, result);
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase(-123.0453, -123.0453d)]
        [TestCase(-123, -123d)]
        [TestCase("-123", -123d)]
        [TestCase("-123.04523", -123.04523d)]
        public void ToDecimalDefaultValue_VariousExtension_CheckThem(Object obj, object expected)
        {
            var result = obj.ToDecimal(0);
            if (expected == null)
                Assert.AreEqual(expected, null);
            else
                Assert.AreEqual(expected, result);
        }


        [TestCase(null, "00000000-0000-0000-0000-000000000000")]
        [TestCase("", "00000000-0000-0000-0000-000000000000")]
        [TestCase(-123.04, "00000000-0000-0000-0000-000000000000")]
        [TestCase("DCA8A469-A84E-DCC6-BFF2-5DA156860CE1", "dca8a469-a84e-dcc6-bff2-5da156860ce1")]
        [TestCase("DCA8A469-A84E-DCC6-BFF2-5DA156860CE112", "00000000-0000-0000-0000-000000000000")]
        [TestCase("DCA8A469-A84DCC6-BFF2-5DA15686012CE112", "00000000-0000-0000-0000-000000000000")]
        public void ToGuid_VariousExtension_CheckThem(Object obj, string expected)
        {
            var result = obj.ToGuid();
            Assert.AreEqual(expected, result.ToString());
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase(-123.04, null)]
        [TestCase("DCA8A469-A84E-DCC6-BFF2-5DA156860CE1", "dca8a469-a84e-dcc6-bff2-5da156860ce1")]
        [TestCase("DCA8A469-A84E-DCC6-BFF2-5DA156860CE112", null)]
        [TestCase("DCA8A469-A84DCC6-BFF2-5DA15686012CE112", null)]
        public void ToGuidOrNull_VariousExtension_CheckThem(Object obj, string expected)
        {
            var result = obj.ToGuidOrNull();
            if (expected == null)
                Assert.AreEqual(expected, null);
            else
                Assert.AreEqual(expected, result.ToString());
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("False", false)]
        [TestCase("True", true)]
        [TestCase(false, false)]
        public void ToBoolean_VariousExtension_CheckThem(Object obj, bool expected)
        {
            var result = obj.ToBoolean();
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, null)]
        [TestCase("", null)]
        [TestCase("False", false)]
        [TestCase("True", true)]
        [TestCase(false, false)]
        public void ToBooleanOrNull_VariousExtension_CheckThem(Object obj, bool? expected)
        {
            var result = obj.ToBooleanOrNull();
            Assert.AreEqual(expected, result);
        }

        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase("False", false)]
        [TestCase("True", true)]
        [TestCase(false, false)]
        public void ToBooleanDeaultValue_VariousExtension_CheckThem(Object obj, bool? expected)
        {
            var result = obj.ToBoolean(true);
            Assert.AreEqual(expected, result);
        }

        [TestCase("", false)]
        [TestCase("是", true)]
        [TestCase("否", false)]
        [TestCase("0", false)]
        [TestCase("1", true)]
        [TestCase("yes", true)]
        [TestCase("no", false)]
        [TestCase("true", true)]
        [TestCase("false", false)]
        public void ToBooleanBySpecicalString_VariousExtension_CheckThem(String obj, bool expected)
        {
            var result = obj.ToBooleanBySpecicalString();
            Assert.AreEqual(expected, result);
        }
    }
}
