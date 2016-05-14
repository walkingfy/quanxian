using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.Entity;
using Xc.Infrastructure;

namespace Xc.Domain.Repositories.IRepositories
{
    /// <summary>
    /// 帐号角色仓储
    /// </summary>
    public interface IAccountRoleRepository : IRepository<AccountRole>
    {
        void RemoveAllRoleByAccount(Account account);
    }
}
