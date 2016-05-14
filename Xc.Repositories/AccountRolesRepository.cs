/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core;
using Xc.Core.Exceptions;
using Xc.Core.Tools;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Repositories.EntityFramework;

namespace Xc.Repositories
{
    public class AccountRolesRepository : EntityFrameworkRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRolesRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 根据账号删除账号所属角色，生成SQL语句删除，无需调用Content.Commit()
        /// </summary>
        /// <param name="account"></param>
        public void RemoveAllRoleByAccount(Account account)
        {
            if (account == null) throw new CustomException("account不能为null", "0500", LogLevel.Warning);
            string sql = string.Format("Delete from {0} where AccountId = @AccountId",EnumTableName.UserRoles.ToString());
            var param = new object[] { new SqlParameter("@AccountId", account.Id), };
            EFContext.Context.Database.ExecuteSqlCommand(sql, param);
        }
    }
}
