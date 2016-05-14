/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.Entity;

namespace Xc.Repositories.EntityFramework
{
    public sealed class XcDbContext : DbContext
    {
        #region Ctor
        /// <summary>
        /// 构造函数，初始化一个新的<c>XcDbContext</c>实例。
        /// </summary>
        public XcDbContext()
            : base("XcDb")
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        #endregion

        #region Public Properties

        public DbSet<Account> Accounts
        {
            get { return Set<Account>(); }
        }

        public DbSet<Role> Roles
        {
            get { return Set<Role>(); }
        }


        public DbSet<Module> Modules
        {
            get { return Set<Module>(); }
        }

        public DbSet<AccountRole> AccountRoles
        {
            get { return Set<AccountRole>(); }
        }

        public DbSet<RolePermission> RolePermissions
        {
            get { return Set<RolePermission>(); }
        }
        #endregion

        #region Protected Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Configurations
                .Add(new AccountTypeConfiguration())
                .Add(new RoleTypeConfiguration())
                .Add(new ModuleTypeConfiguration())
                .Add(new AccountRoleTypeConfiguration())
                .Add(new RolePermissionTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
