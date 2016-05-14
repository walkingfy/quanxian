﻿/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.Repositories;
using System.Data.Entity;

namespace Xc.Repositories
{
    /// <summary>
    /// 表示继承于该接口的类型，是由Microsoft Entity Framework支持的一种仓储上下文的实现。
    /// </summary>
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        #region Properties
        /// <summary>
        /// 获取当前仓储上下文所使用的Entity Framework的<see cref="DbContext"/>实例。
        /// </summary>
        DbContext Context { get; }
        #endregion
    }
}
