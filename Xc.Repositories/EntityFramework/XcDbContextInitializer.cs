/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Tools;

namespace Xc.Repositories.EntityFramework
{
    public class XcDbContextInitializer : DropCreateDatabaseIfModelChanges<XcDbContext>
    {
        //请在使用XcDbContextInitailizer作为数据库初始化器（Database Initializer）时，去除以下代码行
        //的注释，以便在数据库重建时，相应的SQL脚本会被执行。对于已有数据库的情况，请直接注释掉以下代码行。
        protected override void Seed(XcDbContext context)
        {
            context.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IDX_Users_NAME ON Users(Name)");
            context.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IDX_Role_NAME ON Roles(Name)");
            context.Database.ExecuteSqlCommand("CREATE UNIQUE INDEX IDX_Module_NAME ON Modules(Name)");
            context.Database.ExecuteSqlCommand(
                "insert into Users(Name,Password,IsVisible,CreateTime) values('sa','" + ("ok".ToMd5String()) + "',1,GETDATE())");

            base.Seed(context);
        }

        /// <summary>
        /// 执行对数据库的初始化操作。
        /// </summary>
        public static void Initialize()
        {
            Database.SetInitializer<XcDbContext>(null);
        }
    }
}
