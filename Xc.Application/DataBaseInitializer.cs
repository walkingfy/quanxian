using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Repositories.EntityFramework;

namespace Xc.Application
{
    public class DataBaseInitializer
    {
        /// <summary>
        /// 使用UseXcDbContextInitializer初始化数据库
        /// </summary>
        public static void UseXcDbContextInitializer()
        {
            Database.SetInitializer(new XcDbContextInitializer());
        }
    }
}
