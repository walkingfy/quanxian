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

namespace Xc.Repositories
{
    public class AccountRepository : EntityFrameworkRepository<Account>, IAccountRepository
    {
        public AccountRepository(IRepositoryContext context)
            : base(context)
        { }
    }
}
