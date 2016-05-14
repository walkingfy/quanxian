/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Text.RegularExpressions;

namespace Xc.Core.Tools
{
    /// <summary>
    /// RegexHelper帮助类
    /// </summary>
    public static class RegexHelper
    {
        /// <summary>
        /// 判断字符串是否匹配正则表达式
        /// </summary>
        /// <param name="input">输入内容</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="isIgnoreCase">是否忽略大小写</param>
        /// <returns>返回是否匹配，出现异常return false;</returns>
        public static bool IsMatch(string input, string regex, bool isIgnoreCase = true)
        {
            return IsMatch(input, regex, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断字符串是否匹配正则表达式
        /// </summary>
        /// <param name="input">输入内容</param>
        /// <param name="regex">正则表达式</param>
        /// <param name="regexOptions">正则表达式设置</param>
        /// <returns>返回是否匹配，出现异常return false;</returns>
        public static bool IsMatch(string input, string regex, RegexOptions regexOptions)
        {
            return Regex.IsMatch(input, regex, regexOptions);
        }
    }
}
