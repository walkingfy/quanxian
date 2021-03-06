﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xc.Core;
using Xc.Core.Exceptions;

namespace Xc.DataObjects
{
    /// <summary>分页信息
    /// </summary>
    public class PageInfo
    {
        /// <summary>当前页（从1开始）
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>每页大小
        /// </summary>
        public int PageSize { get; set; }

        public PageInfo(int pageIndex = 1, int pageSize = 20)
        {
            if (pageIndex < 1)
                throw new CustomException("当前页不能小于1", "0500", LogLevel.Warning);
            if (pageSize < 1)
                throw new CustomException("每页显示数量不能小于1", "0500", LogLevel.Warning);
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
