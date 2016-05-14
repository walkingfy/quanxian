/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.Domain
{
    /// <summary>
    /// 聚合根
    /// </summary>
    public interface IAggregateRoot : IEntity
    {
        /// <summary>
        /// 版本号(乐观锁)
        /// </summary>
        byte[] Version { get; set; }
    }
}
