﻿/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using Xc.DataObjects;
using Xc.Domain.Entity;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Services;

namespace Xc.Application
{
    public class RolePermissionAppService : ApplicationService
    {
        private IRolePermissionRepository _accountModuleRepository;
        private RolePermissionService _rolePermissionService;

        public RolePermissionAppService()
        {
            _accountModuleRepository = AutofacInstace.Resolve<IRolePermissionRepository>();
            _rolePermissionService = AutofacInstace.Resolve<RolePermissionService>();
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="roleIdGuids">角色Id</param>
        /// <returns></returns>
        public List<ModuleDataObject> GetMenus(List<Guid> roleIdGuids)
        {
            var list = _rolePermissionService.GetRolePermission(roleIdGuids).ToList();
            var data = OperationBaseService.GetMapperData<List<Module>, List<ModuleDataObject>>(list);
            return ProcessMoreLevelMenus(data);
        }


        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public List<ModuleDataObject> GetMenusByAdmin()
        {
            var list = _rolePermissionService.GetAllPermission().ToList();
            var data = OperationBaseService.GetMapperData<List<Module>, List<ModuleDataObject>>(list);
            return ProcessMoreLevelMenus(data);
        }

        /// <summary>
        /// 处理父级与子级关系
        /// </summary>
        /// <param name="moduleList"></param>
        /// <returns></returns>
        private List<ModuleDataObject> ProcessMoreLevelMenus(List<ModuleDataObject> moduleList)
        {
            var notParentList = moduleList.Where(t => t.ParentId != null);
            var parentList = moduleList.Where(t => t.ParentId == null).ToList();
            foreach (var item in notParentList)
            {
                var parentId = parentList.SingleOrDefault(t => t.Id == item.ParentId);
                if (parentId != null)
                {
                    if (parentId.Children == null) parentId.Children = new List<ModuleDataObject>();
                    parentId.Children.Add(item);
                }
            }
            return parentList;
        }


        /// <summary>
        /// 根据角色Id获取模块IDs
        /// </summary>
        /// <param name="roleIdGuid"></param>
        /// <returns></returns>
        public IList<Guid> GetRolePermissionsById(Guid roleIdGuid)
        {
            return _rolePermissionService.GetRolePermissionsById(roleIdGuid).ToList();
        }

        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="moduleIds"></param>
        /// <returns></returns>
        public int ModifyRolePermissions(Guid roleId, List<Guid> moduleIds)
        {
            var modules = moduleIds.Select(item => new Module() { Id = item }).ToList();
            //调用执行权限
            return _rolePermissionService.ModifyRolePermissions(new Role() { Id = roleId }, modules);
        }

        /// <summary>
        /// 判断角色是否拥有权限
        /// </summary>
        /// <param name="roleIds"></param>
        /// <param name="controllName"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public bool GetUserRoleIsHavePermission(List<Guid> roleIds, string controllName, string action)
        {
            return _rolePermissionService.GetRoleIsHavePermission(roleIds, controllName, action);
        }
    }
}
