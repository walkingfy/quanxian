/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xc.Core.Tools;

namespace Xc.UnitTests.Core.Tools.Tests
{
    [TestFixture]
    public class LogHelperTests
    {
        [SetUp]
        public void Setup()
        {
            LogHelper.SetConfig();
        }

        [Test]
        [Explicit]
        public void WriteLog_Info_WriteTXTFile()
        {
            LogHelper.WriteLog("测试正常信息");
            var str = ReadTXTFile("C:\\Log.txt");
            Assert.IsTrue(str.Contains("测试正常信息"), str);
        }

        [Test]
        [Explicit]
        public void WriteLog_Erroe_WriteTXTFile()
        {
            var ex = new NullReferenceException("未将对象引用设置到对象的实例");
            LogHelper.WriteLog("出现异常", ex);
            var str = ReadTXTFile("C:\\Log.txt");
            Assert.IsTrue(str.Contains("未将对象引用设置到对象的实例"), str);
        }

        private string ReadTXTFile(string path)
        {
            string strLine = null;
            string result = "";
            var file = new FileStream(path, FileMode.Open, System.IO.FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(file, Encoding.Default);//用FileStream对象实例化一个StreamReader对象
            //strLine = sr.ReadToEnd();//读取完整的文件，如果用这个方法，就可以不用下面的while循环
            strLine = sr.ReadLine();//   读取一行字符并返回
            while (strLine != null)
            {
                strLine = sr.ReadLine();
                result += strLine;
            }
            sr.Close();
            return result;
        }
    }
}
