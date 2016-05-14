/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xc.Core;
using Xc.Core.Exceptions;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Repositories.EntityFramework;

namespace Xc.Repositories
{
    public class RolePermissionRepository : EntityFrameworkRepository<RolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(IRepositoryContext context)
            : base(context)
        { }

        /// <summary>
        /// 根据角色删除角色权限
        /// </summary>
        /// <param name="role"></param>
        public void RemoveRolePermissionByRole(Role role)
        {
            if (role == null) throw new CustomException("role不能为null", "0500", LogLevel.Warning);
            string sql = string.Format("Delete from RolePermissions where RoleId = @RoleId",EnumTableName.RolePermissions.ToString());
            var param = new object[] { new SqlParameter("@RoleId", role.Id) };
            EFContext.Context.Database.ExecuteSqlCommand(sql, param);
        }


    }
}
