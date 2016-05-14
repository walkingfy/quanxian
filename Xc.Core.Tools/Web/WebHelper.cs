/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Xc.Core.Tools
{
    /// <summary>
    /// WebHelper帮助类
    /// </summary>
    public class WebHelper
    {
        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <returns></returns>
        public static string GetUrlRequestContent(string url)
        {
            Uri uri = new Uri(url);
            WebRequest webreq = WebRequest.Create(uri);
            Stream s = webreq.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.Default);
            string all = sr.ReadToEnd();
            return all;
        }
    }
}
