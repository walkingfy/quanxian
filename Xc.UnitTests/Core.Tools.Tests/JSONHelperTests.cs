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
    public class JsonHelperTests
    {
        public MyTestJson MyTest { get; set; }
        [Test]
        [SetUp]
        public void Setup()
        {
            MyTest = new MyTestJson("呵呵", 2, 2.50);
        }

        [Test]
        public void ToJsonString_Normal_ReturnString()
        {
            var returnStr = JsonHelper.ToJsonString(MyTest);
            Assert.AreEqual("{\"P1\":\"呵呵\",\"P2\":2,\"P3\":2.5}", returnStr);
        }

        [Test]
        public void ToEntity_Normal_ReturnMyTestJson()
        {
            var str = "{\"P1\":\"呵呵\",\"P2\":2,\"P3\":2.5}";
            var result = JsonHelper.ToEntity<MyTestJson>(str);
            Assert.AreEqual(MyTest.P1, result.P1);
        }
    }

    public class MyTestJson
    {
        public MyTestJson(string p1, int p2, double p3)
        {
            P1 = p1; P2 = p2; P3 = p3;
        }
        public string P1 { get; set; }
        public int P2 { get; set; }
        public double P3 { get; set; }
    }
}
