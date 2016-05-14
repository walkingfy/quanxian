
/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.IO;

namespace Xc.Core.Tools
{
    /// <summary>
    /// LogHelper帮助类
    /// </summary>
    public class LogHelper
    {
        public static readonly log4net.ILog Loginfo = log4net.LogManager.GetLogger("loginfo");   //选择<logger name="loginfo">的配置 

        public static readonly log4net.ILog Logerror = log4net.LogManager.GetLogger("logerror");   //选择<logger name="logerror">的配置 

        public static void SetConfig()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
        /// <summary>
        /// 设置文件路径
        /// </summary>
        /// <param name="configFile"></param>
        public static void SetConfig(FileInfo configFile)
        {
            log4net.Config.XmlConfigurator.Configure(configFile);
        }
        /// <summary>
        /// 写系统信息日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            if (Loginfo.IsInfoEnabled)
            {
                Loginfo.Info(info);
            }
        }
        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="error"></param>
        /// <param name="se"></param>
        public static void WriteLog(string error, Exception se)
        {
            if (Logerror.IsErrorEnabled)
            {
                Logerror.Error(error, se);
            }
        }
    }
}
