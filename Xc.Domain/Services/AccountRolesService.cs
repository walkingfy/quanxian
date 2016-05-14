using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xc.Core;
using Xc.Core.Exceptions;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;

namespace Xc.Domain.Services
{
    public class AccountRolesService
    {
        private readonly IAccountRoleRepository _accountRoleReposity;
        private readonly IRepositoryContext _repositoryContext;
        public AccountRolesService(IRepositoryContext repositoryContext, IAccountRoleRepository reposity)
        {
            this._repositoryContext = repositoryContext;
            this._accountRoleReposity = reposity;
        }
        /// <summary>
        ///修改账户角色 
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="roles">多个角色</param>
        /// <returns></returns>
        public void ModifyAccountRole(Account account, IList<Guid> roles)
        {
            if (account == null)
                throw new CustomException("account不能为空", "0500", LogLevel.Warning);
            if (roles == null)
                throw new CustomException("roles不能为空", "0500", LogLevel.Warning);

            //先删除原有角色
            _accountRoleReposity.RemoveAllRoleByAccount(account);

            //添加新角色
            foreach (var roleId in roles)
            {
                _accountRoleReposity.Add(new AccountRole(account.Id, roleId)
                {
                    CreateTime = DateTime.Now
                });
            }
            _repositoryContext.Commit();
        }


        public IEnumerable<AccountRole> GetAccountRole(Guid accountId)
        {
            return _accountRoleReposity.GetAll(Specification<AccountRole>.Eval(t => t.AccountId == accountId));
        }

    }
}
