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
using Xc.Core.Exceptions;
using Xc.Core;

namespace Xc.UnitTests.Core.Tests
{
    [TestFixture]
    public class CustomExceptionTests
    {
        #region 常量

        /// <summary>
        /// 异常消息
        /// </summary>
        public const string Message = "A";

        /// <summary>
        /// 异常消息2
        /// </summary>
        public const string Message2 = "B";

        /// <summary>
        /// 异常消息3
        /// </summary>
        public const string Message3 = "C";

        /// <summary>
        /// 异常消息4
        /// </summary>
        public const string Message4 = "D";

        #endregion

        #region TestValidate_MessageIsNull(验证消息为空)

        /// <summary>
        /// 验证消息为空
        /// </summary>
        [Test]
        public void Validate_MessageIsNullTest()
        {
            var customException = new CustomException(null, "P1");
            Assert.AreEqual(string.Empty, customException.Message);
        }

        #endregion

        #region TestCode(设置错误码)

        /// <summary>
        /// 设置错误码
        /// </summary>
        [Test]
        public void TestCode()
        {
            var customException = new CustomException(Message, "P1");
            Assert.AreEqual("P1", customException.Code);
        }

        #endregion

        #region TestLogLevel(测试日志级别)

        /// <summary>
        /// 测试日志级别
        /// </summary>
        [Test]
        public void TestLogLevel()
        {
            var customException = new CustomException(Message, "P1", LogLevel.Fatal);
            Assert.AreEqual(LogLevel.Fatal, customException.Level);
        }

        #endregion

        #region TestMessage_OnlyMessage(仅设置消息)

        /// <summary>
        /// 仅设置消息
        /// </summary>
        [Test]
        public void TestMessage_OnlyMessage()
        {
            var customException = new CustomException(Message);
            Assert.AreEqual(Message, customException.Message);
        }

        #endregion

        #region TestMessage_OnlyException(仅设置异常)

        /// <summary>
        /// 仅设置异常
        /// </summary>
        [Test]
        public void TestMessage_OnlyException()
        {
            var customException = new CustomException(GetException());
            Assert.AreEqual(Message, customException.Message);
        }

        /// <summary>
        /// 获取异常
        /// </summary>
        private Exception GetException()
        {
            return new Exception(Message);
        }

        #endregion

        #region TestMessage_Message_Exception(设置错误消息和异常)

        /// <summary>
        /// 设置错误消息和异常
        /// </summary>
        [Test]
        public void TestMessage_Message_Exception()
        {
            var customException = new CustomException(Message2, "P1", LogLevel.Fatal, GetException());
            Assert.AreEqual(string.Format("{0}\r\n{1}", Message2, Message), customException.Message);
        }

        #endregion

        #region TestMessage_2LayerException(设置2层异常)

        /// <summary>
        /// 设置2层异常
        /// </summary>
        [Test]
        public void TestMessage_2LayerException()
        {
            var customException = new CustomException(Message3, "P1", LogLevel.Fatal, Get2LayerException());
            Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}", Message3, Message2, Message), customException.Message);
        }

        /// <summary>
        /// 获取2层异常
        /// </summary>
        private Exception Get2LayerException()
        {
            return new Exception(Message2, new Exception(Message));
        }

        #endregion

        #region TestMessage_CustomException(设置CustomException异常)

        /// <summary>
        /// 设置CustomException异常
        /// </summary>
        [Test]
        public void TestMessage_CustomException()
        {
            var customException = new CustomException(GetCustomException());
            Assert.AreEqual(Message, customException.Message);
        }

        /// <summary>
        /// 获取异常
        /// </summary>
        private CustomException GetCustomException()
        {
            return new CustomException(Message);
        }

        #endregion

        #region TestMessage_2LayerCustomException(设置2层CustomException异常)

        /// <summary>
        /// 设置2层CustomException异常
        /// </summary>
        [Test]
        public void TestMessage_2LayerCustomException()
        {
            var customException = new CustomException(Message3, "", Get2LayerCustomException());
            Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}", Message3, Message2, Message), customException.Message);
        }

        /// <summary>
        /// 获取2层异常
        /// </summary>
        private CustomException Get2LayerCustomException()
        {
            return new CustomException(Message2, "", new CustomException(Message));
        }

        #endregion

        #region TestMessage_3LayerCustomException(设置3层CustomException异常)

        /// <summary>
        /// 设置3层CustomException异常
        /// </summary>
        [Test]
        public void TestMessage_3LayerCustomException()
        {
            var customException = new CustomException(Message4, "", Get3LayerCustomException());
            Assert.AreEqual(string.Format("{0}\r\n{1}\r\n{2}\r\n{3}", Message4, Message3, Message2, Message), customException.Message);
        }

        /// <summary>
        /// 获取3层异常
        /// </summary>
        private CustomException Get3LayerCustomException()
        {
            return new CustomException(Message3, "", new Exception(Message2, new CustomException(Message)));
        }

        #endregion

        #region 添加异常数据

        /// <summary>
        /// 添加异常数据
        /// </summary>
        [Test]
        public void TestAdd_1Layer()
        {
            var customException = new CustomException(Message);
            customException.Data.Add("key1", "value1");
            customException.Data.Add("key2", "value2");

            var expected = new StringBuilder();
            expected.AppendLine(Message);
            expected.AppendLine("key1:value1");
            expected.AppendLine("key2:value2");
            Assert.AreEqual(expected.ToString(), customException.Message);
        }

        /// <summary>
        /// 添加异常数据
        /// </summary>
        [Test]
        public void TestAdd_2Layer()
        {
            var exception = new Exception(Message);
            exception.Data.Add("a", "a1");
            exception.Data.Add("b", "b1");

            var customException = new CustomException(exception);
            customException.Data.Add("key1", "value1");
            customException.Data.Add("key2", "value2");

            var expected = new StringBuilder();
            expected.AppendLine(Message);
            expected.AppendLine("a:a1");
            expected.AppendLine("b:b1");
            expected.AppendLine("key1:value1");
            expected.AppendLine("key2:value2");
            Assert.AreEqual(expected.ToString(), customException.Message);
        }

        #endregion

    }
}
