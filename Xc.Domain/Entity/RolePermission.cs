/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.Domain.Entity
{
    /// <summary>
    /// 表示角色权限聚合根
    /// </summary>
    public class RolePermission : AggregateRoot
    {
        public RolePermission()
        {

        }
        public RolePermission(Guid roleId, Guid moduleId)
        {
            this.RoleId = roleId;
            this.ModuleId = moduleId;
        }

        #region Public Properties

        public Role Role { get; set; }
        public Guid RoleId { get; set; }

        public Module Module { get; set; }
        public Guid ModuleId { get; set; }
        #endregion

        #region Public Method
        #endregion
    }
}
