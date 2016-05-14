/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xc.Core.Validations;

namespace Xc.UnitTests.Core.Tests
{
    [TestFixture]
    public class ValidationDomainTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            _test=new DomainTest();
            _validation=new Validation();
        }

        [Test]
        public void RequiredValidateTest()
        {
            var result = _validation.Validate(_test);
            Assert.AreEqual("Name不能为空",result.First().ErrorMessage);
        }

        [Test]
        public void StringLengthValidateTest()
        {
            _test.Remark = "asdfladsf;alkdjfl;ajkdl;fkjl;akjf";
            var result = _validation.Validate(_test);
            Assert.AreEqual(2,result.Count);
            Assert.AreEqual("超出长度限制", result.Last().ErrorMessage);
        }

        private DomainTest _test { get; set; }

        private IValidation _validation { get; set; }

    }

    public class DomainTest
    {

        [Required(ErrorMessage = "Name不能为空")]
        public string Name { get; set; }

        [StringLength(10, ErrorMessage = "超出长度限制")]
        public string Remark { get; set; }
    }
}
