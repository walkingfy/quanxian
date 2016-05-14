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
using Xc.Domain;

namespace Xc.UnitTests.Domain.Base.Tests
{
    public class Student : EntityBase
    {

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Required(ErrorMessage = "性别不能为空")]
        public string Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Range(18, 50, ErrorMessage = "年龄范围为18岁到30岁")]
        public int Age { get; set; }
        /// <summary>
        /// 职业
        /// </summary>
        [Required(ErrorMessage = "职业不能为空")]
        public string Job { get; set; }

        /// <summary>
        /// 是否老的男学生
        /// </summary>
        public bool IsOldStudent()
        {
            return Job == "学生" && Gender == "男";
        }
    }
}
