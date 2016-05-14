using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Validations;

namespace Xc.DataObjects
{
    public class DataObjectBase
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 验证失败字符串
        /// </summary>
        public string ErrorString { get; set; }
        /// <summary>
        /// 验证
        /// </summary>
        public virtual bool Validate()
        {
            var result = GetValidationResult();
            var resultBuilder = new StringBuilder();
            foreach (var item in result)
            {
                resultBuilder.Append("● " + item.ErrorMessage + "<br/>");
            }
            ErrorString = resultBuilder.ToString();
            return result.IsValid;
        }

        /// <summary>
        /// 获取验证结果
        /// </summary>
        private ValidationResultCollection GetValidationResult()
        {
            return ValidationFactory.Create().Validate(this);
        }
    }
}
