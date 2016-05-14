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

namespace Xc.Domain
{
    /// <summary>
    /// 聚合根
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public abstract class AggregateRoot : EntityBase, IAggregateRoot
    {
        ///// <summary>
        ///// 初始化聚合根
        ///// </summary>
        ///// <param name="id">标识</param>
        //protected AggregateRoot(Guid id)
        //    : base(id)
        //{
        //}

        /// <summary>
        /// 版本号(乐观锁)
        /// </summary>
        public byte[] Version { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        protected override void Validate(ValidationResultCollection results)
        {
            if (Id == Guid.Empty)
                results.Add(new ValidationResult("Id不能为空"));
        }
    }
}
