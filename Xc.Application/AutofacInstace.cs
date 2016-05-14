using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Xc.Domain.Repositories;
using Xc.Domain.Services;
using Xc.Repositories;

namespace Xc.Application
{
    public class AutofacInstace
    {
        public static IContainer Container { get; set; }
        public static void InitServiceInstance()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EntityFrameworkRepositoryContext>().As<IRepositoryContext>().InstancePerLifetimeScope();
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            builder.RegisterType<AccountService>();
            builder.RegisterType<AccountRolesService>();
            builder.RegisterType<RoleService>();
            builder.RegisterType<RolePermissionService>();
            Container = builder.Build();
        }

        /// <summary>
        /// 解析值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
