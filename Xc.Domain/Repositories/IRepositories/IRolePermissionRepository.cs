using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.Entity;

namespace Xc.Domain.Repositories.IRepositories
{
    /// <summary>
    /// 角色模块仓储
    /// </summary>
    public interface IRolePermissionRepository : IRepository<RolePermission>
    {
        /// <summary>
        /// 根据角色删除该角色的所有权限
        /// </summary>
        /// <param name="role"></param>
        void RemoveRolePermissionByRole(Role role);
    }
}
