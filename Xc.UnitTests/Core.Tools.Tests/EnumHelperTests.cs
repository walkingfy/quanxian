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
    class EnumHelperTests
    {

        [Test]
        public void GetDescription_Nomal_ReturnDescription()
        {
            var obj = TestEnum.Me;
            //Assert.AreEqual("我", obj.GetDescription());
            Assert.AreEqual(String.Empty, obj.GetDescription(true));
        }

        [Test]
        public void GetDescription_NoDescription_ReturnEmpty()
        {
            var obj = TestEnum.His;
            //Assert.AreEqual(String.Empty, obj.GetDescription());
            Assert.AreEqual(String.Empty, obj.GetDescription(true));
        }

        [Test]
        public void GetDescription_OtherType_ReturnEmpty()
        {
            var obj = "wlsjdfsdf";
            //Assert.AreEqual(String.Empty, obj.GetDescription());
            Assert.AreEqual(String.Empty, obj.GetDescription(true));
        }


        [Test]
        public void GetEnumItems_Normal_ReturnFourDictionary()
        {
            var result = typeof(TestEnum).GetEnumItems();
            var expected = new Dictionary<string, string>();
            expected.Add("1", "我");
            expected.Add("2", "你");
            expected.Add("3", "她");
            expected.Add("4", "His");
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetEnumItems_SystemEnum_ReturnFourDictionary()
        {
            var result = typeof(ConsoleColor).GetEnumItems();
            Assert.AreEqual("Black", result["0"]);
        }

        [Test]
        public void GetEnumDescription_Normal_ReturnString()
        {
            var obj = typeof(TestEnum);
            Assert.AreEqual("我", obj.GetEnumValueDescription("1"));
        }

        [Test]
        public void GetEnumDescription_NoDescription_ReturnString()
        {
            var obj = typeof(TestEnum);
            Assert.AreEqual("His", obj.GetEnumValueDescription("4"));
        }

        [Test]
        public void GetEnumDescription_NoExistsEnumValue_ReturnString()
        {
            var obj = typeof(TestEnum);
            Assert.AreEqual(String.Empty, obj.GetEnumValueDescription("Mes"));
        }

    }

    public enum TestEnum
    {
        [System.ComponentModel.Description("我")]
        Me = 1,
        [System.ComponentModel.Description("你")]
        You = 2,
        [System.ComponentModel.Description("她")]
        Her = 3,
        His = 4,
    }
}
