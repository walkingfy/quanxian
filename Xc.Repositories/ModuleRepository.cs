/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Infrastructure;
using Xc.Repositories.EntityFramework;

namespace Xc.Repositories
{
    public class ModuleRepository : EntityFrameworkRepository<Module>, IModuleRepository
    {
        public ModuleRepository(IRepositoryContext context)
            : base(context)
        { }
    }
}
