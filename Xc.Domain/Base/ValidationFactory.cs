using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Validations;

namespace Xc.Domain
{
    public class ValidationFactory
    {
        /// <summary>
        /// 创建验证操作
        /// </summary>
        public static IValidation Create()
        {
            return new Validation();
        }
    }
}
