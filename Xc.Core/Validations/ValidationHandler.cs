using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Exceptions;

namespace Xc.Core.Validations
{
    /// <summary>
    /// 默认验证处理器，直接抛出异常
    /// </summary>
    public class ValidationHandler : IValidationHandler
    {
        /// <summary>
        /// 处理验证错误
        /// </summary>
        /// <param name="results">验证结果集合</param>
        public void Handle(ValidationResultCollection results)
        {
            if (results.IsValid)
                return;
            throw new CustomException(results.First().ErrorMessage);
        }
    }
}
