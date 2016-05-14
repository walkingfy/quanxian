/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Validations;

namespace Xc.UnitTests.Domain.Base.Tests
{ 
    /// <summary>
    /// 异常验证处理 - 什么也不做
    /// </summary>
    public class NothingValidationHandler : IValidationHandler
    {
        /// <summary>
        /// 处理错误
        /// </summary>
        /// <param name="results">验证结果集合</param>
        public void Handle(ValidationResultCollection results)
        {
        }
    }
}
