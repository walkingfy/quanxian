using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;
using Xc.Core.Exceptions;
using Xc.DataObjects;
using Xc.DataObjects.Enums;
using Xc.Domain.Entity;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;

namespace Xc.Application
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public class RoleAppService : ApplicationService
    {
        private IRoleRepository _roleRepository;
        public RoleAppService()
        {
            _roleRepository = AutofacInstace.Resolve<IRoleRepository>();
        }

        /// <summary>
        /// 分页获取所有角色
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JqGrid GetAllRole(int pageIndex, int pageSize)
        {
            return OperationBaseService.GetPageResultToJqGrid<RoleDataObject, Role, IRoleRepository>(_roleRepository, pageIndex, pageSize, t => t.Id);
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        public List<RoleDataObject> GetAllRole()
        {
            var roles = _roleRepository.GetAll(Specification<Role>.Eval(t => t.IsVisible == EnumIsVisible.Can)).ToList();
            var data = OperationBaseService.GetMapperData<List<Role>, List<RoleDataObject>>(roles);
            return data;
        }


        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationResult AddRole(RoleDataObject entity)
        {
            return OperationRole(entity, OperationType.Add);
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationResult UpdateRole(RoleDataObject entity)
        {
            return OperationRole(entity, OperationType.Update);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationResult DeleteRole(RoleDataObject entity)
        {
            return OperationRole(entity, OperationType.Delete);
        }

        /// <summary>
        /// 私有方法
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="operationType">操作类型，OperationType枚举值</param>
        /// <returns></returns>
        private OperationResult OperationRole(RoleDataObject entity, OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.Add:
                    return OperationBaseService.Save<RoleDataObject, Role, IRoleRepository>(_roleRepository, entity);
                case OperationType.Update:
                    return OperationBaseService.Update<RoleDataObject, Role, IRoleRepository>(_roleRepository, entity);
                case OperationType.Delete:
                    return OperationBaseService.Delete<RoleDataObject, Role, IRoleRepository>(_roleRepository, entity);
                default:
                    return new OperationResult(OperationResultType.Success);
            }
        }
    }
}
