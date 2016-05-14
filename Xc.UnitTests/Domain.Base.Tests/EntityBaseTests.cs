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

namespace Xc.UnitTests.Domain.Base.Tests
{
    /// <summary>
    /// 实体基类测试
    /// </summary>
    [TestFixture]
    public class EntityBaseTest
    {
        /// <summary>
        /// 测试实体1
        /// </summary>
        private Student _test1;
        /// <summary>
        /// 测试实体2
        /// </summary>
        private Student _test2;

        /// <summary>
        /// 测试初始化
        /// </summary>
        [SetUp]
        public void TestInit()
        {
            _test1 = new Student();
            _test2 = new Student();
        }

        /// <summary>
        /// 新创建的实体不相等
        /// </summary>
        [Test]
        public void TestNewEntityIsNotEquals()
        {
            Assert.IsFalse(_test1.Equals(_test2));
            Assert.IsFalse(_test1.Equals(null));

            Assert.IsFalse(_test1 == _test2);
            Assert.IsFalse(_test1 == null);
            Assert.IsFalse(null == _test2);

            Assert.IsTrue(_test1 != _test2);
            Assert.IsTrue(_test1 != null);
            Assert.IsTrue(null != _test2);
        }

        /// <summary>
        /// 测试验证
        /// </summary>
        [Test]
        [ExpectedException(typeof(CustomException))]
        public void TestValidate()
        {
            try
            {
                Student employee = new Student { Name = "张三", Gender = "男", Job = "学生", Age = 18 };
                employee.AddValidationRule(new OldProgrammerSalaryRule(employee));
                employee.Validate();
            }
            catch (CustomException ex)
            {
                Assert.AreEqual("低于24岁的不是老学生", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 设置空操作验证处理器,不进行任何操作，所以不会抛出异常
        /// </summary>
        [Test]
        public void TestSetValidationHandler()
        {
            Student employee = new Student();
            employee.SetValidationHandler(new NothingValidationHandler());
            employee.Validate();
        }
    }
}
