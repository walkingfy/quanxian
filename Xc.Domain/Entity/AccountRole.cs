﻿/*
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
    /// 表示账号角色聚合根
    /// </summary>
    public class AccountRole : AggregateRoot
    {
        public AccountRole()
        {
            
        }
        public AccountRole(Guid accountId, Guid roleId)
        {
            this.AccountId = accountId;
            this.RoleId = roleId;
        }

        #region Public Properties

        public Account Account { get; set; }
        public Guid AccountId { get; set; }
        public Role Role { get; set; }
        public Guid RoleId { get; set; }

        #endregion
    }
}
