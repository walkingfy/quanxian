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
using Xc.Core.Validations;

namespace Xc.UnitTests.Domain.Base.Tests
{ /// <summary>
    /// 程序员老男人的工资验证规则
    /// </summary>
    public class OldProgrammerSalaryRule : IValidationRule
    {
        /// <summary>
        /// 初始化程序员老男人的工资验证规则
        /// </summary>
        /// <param name="student">员工</param>
        public OldProgrammerSalaryRule(Student student)
        {
            _student = student;
        }

        /// <summary>
        /// 学生
        /// </summary>
        private readonly Student _student;

        /// <summary>
        /// 验证
        /// </summary>
        public ValidationResult Validate()
        {
            if (_student.IsOldStudent() && _student.Age < 24)
                return new ValidationResult("低于24岁的不是老学生");
            return ValidationResult.Success;
        }
    }
}
