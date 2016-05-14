/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.Core
{
    public enum LogLevel
    {
        /// <summary>
        /// 致命错误
        /// </summary>
        [Description("致命错误")]
        Fatal,
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warning,
        /// <summary>
        /// 信息
        /// </summary>
        [Description("信息")]
        Information,
        /// <summary>
        /// 调试
        /// </summary>
        [Description("调试")]
        Debug
    }
}
