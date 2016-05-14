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
    public class RolePermissionService
    {
        private readonly IRolePermissionRepository _rolePermissionReposity;
        private readonly IRepositoryContext _repositoryContext;
        private readonly IModuleRepository _moduleReposity;

        public RolePermissionService(IRepositoryContext repositoryContext, IRolePermissionRepository rolePermissionsReposity, IModuleRepository moduleReposity)
        {
            this._rolePermissionReposity = rolePermissionsReposity;
            this._repositoryContext = repositoryContext;
            this._moduleReposity = moduleReposity;
        }
        public RolePermissionService(IRepositoryContext repositoryContext, IRolePermissionRepository rolePermissionsReposity)
        {
            this._rolePermissionReposity = rolePermissionsReposity;
            this._repositoryContext = repositoryContext;
        }

        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="modules">多个模块</param>
        public int ModifyRolePermissions(Role role, IEnumerable<Module> modules)
        {
            if (role == null)
                throw new CustomException("role不能为空", "0500", LogLevel.Warning);
            if (modules == null)
                throw new CustomException("modules不能为空", "0500", LogLevel.Warning);

            //先删除原有权限
            _rolePermissionReposity.RemoveRolePermissionByRole(role);
            //添加新权限
            foreach (var module in modules)
            {
                _rolePermissionReposity.Add(new RolePermission(role.Id, module.Id) { CreateTime = DateTime.Now });
            }
            return _repositoryContext.Commit();
        }



        /// <summary>
        /// 根据角色权限获取所有菜单
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public IEnumerable<Module> GetModuleMenusByRole(Role role)
        {
            if (role == null)
                throw new CustomException("role不能为空", "0500", LogLevel.Warning);
            var result = from rolePermission in _rolePermissionReposity.FindAll(new ExpressionSpecification<RolePermission>(t => t.RoleId == role.Id))
                         join modules in _moduleReposity.FindAll(new ExpressionSpecification<Module>(t => t.IsVisible == EnumIsVisible.Can && t.Type == EnumModuleType.Menu)) on rolePermission.ModuleId
                             equals modules.Id
                         select modules;
            return result;
        }

        /// <summary>
        /// 根据角色权限获取父级模块中按钮
        /// </summary>
        /// <param name="role">角色</param>
        /// <param name="parentModule">父级模块</param>
        /// <returns></returns>
        public IEnumerable<Module> GetModuleButtonsByRole(Role role, Module parentModule)
        {
            if (role == null)
            {
                throw new CustomException("role不能为空", "0500", LogLevel.Warning);
            }
            if (parentModule == null)
            {
                throw new CustomException("parentModule不能为空", "0500", LogLevel.Warning);
            }
            var result = from rolePermissions in _rolePermissionReposity.FindAll(new ExpressionSpecification<RolePermission>(t => t.RoleId == role.Id))
                         join modules in _moduleReposity.FindAll(new ExpressionSpecification<Module>(t => t.IsVisible == EnumIsVisible.Can && t.Type == EnumModuleType.Button && t.ParentId == parentModule.Id)) on rolePermissions.ModuleId
                             equals modules.Id
                         select modules;
            return result;
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public IEnumerable<Module> GetRolePermission(List<Guid> roleIds)
        {
            if (roleIds == null)
            {
                throw new CustomException("roleId不能为空", "0500", LogLevel.Warning);
            }
            var result = from rolePermissions in _rolePermissionReposity.FindAll(new ExpressionSpecification<RolePermission>(t => roleIds.Contains(t.RoleId)))
                         join modules in _moduleReposity.FindAll(new ExpressionSpecification<Module>(t => t.IsVisible == EnumIsVisible.Can 
                             &&t.Type==EnumModuleType.Menu)) on rolePermissions.ModuleId
                             equals modules.Id
                         select modules;
            return result;
        }

        /// <summary>
        /// 获取所有角色权限
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Module> GetAllPermission()
        {
            return _moduleReposity.FindAll(new ExpressionSpecification<Module>(t => t.IsVisible == EnumIsVisible.Can && t.Type == EnumModuleType.Menu));
        }

        /// <summary>
        /// 获取角色对应的模块Id
        /// </summary>
        /// <param name="roleGuid"></param>
        /// <returns></returns>
        public IEnumerable<Guid> GetRolePermissionsById(Guid roleGuid)
        {
            return
                _rolePermissionReposity.FindAll(new ExpressionSpecification<RolePermission>(t => t.RoleId == roleGuid))
                    .Select(t => t.ModuleId);
        }

        /// <summary>
        /// 判断角色是否拥有权限
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="controllName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public bool GetRoleIsHavePermission(List<Guid> roleIds, string controllName, string actionName)
        {
            if (roleIds == null)
            {
                throw new CustomException("roleId不能为空", "0500", LogLevel.Warning);
            }
            var result = from rolePermissions in _rolePermissionReposity.FindAll(new ExpressionSpecification<RolePermission>(t => roleIds.Contains(t.RoleId)))
                         join modules in _moduleReposity.FindAll(new ExpressionSpecification<Module>(t => t.IsVisible == EnumIsVisible.Can
                             && t.Controller == controllName && t.Action == actionName)) on rolePermissions.ModuleId
                             equals modules.Id
                         select modules;
            return result.Any();
        }
    }
}
