/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ModuleService
    {
        private readonly IModuleRepository _moduleReposity;
        private readonly IRepositoryContext _repositoryContext;

        public ModuleService(IRepositoryContext repositoryContext, IModuleRepository moduleReposity)
        {
            this._repositoryContext = repositoryContext;
            this._moduleReposity = moduleReposity;
        }
        /// <summary>
        /// 上移/下移模块
        /// </summary>
        /// <param name="fromModule"></param>
        /// <param name="toModule"></param>
        public void MoveModule(Module fromModule, Module toModule)
        {
            var sortTemp = fromModule.Sort;
            fromModule.Sort = toModule.Sort;
            toModule.Sort = sortTemp;
            _moduleReposity.Update(fromModule);
            _moduleReposity.Update(toModule);
            _repositoryContext.Commit();
        }

        /// <summary>
        /// 获取所有父级菜单
        /// </summary>
        /// <returns>返回所有父级菜单</returns>
        public IEnumerable<Module> GetAllParentModuleMenus()
        {
            return
                _moduleReposity.FindAll(
                    Specification<Module>.Eval(t => t.Type == EnumModuleType.Menu && t.ParentId == null));
        }

        /// <summary>
        /// 获取子级模块
        /// </summary>
        /// <param name="parentModule">父级模块，ID不能为空</param>
        /// <returns>返回该模块下所有模块</returns>
        public IEnumerable<Module> GetChildModuls(Module parentModule)
        {
            if (parentModule == null)
            {
                throw new CustomException("parentModule不能为空", "0500", LogLevel.Warning);
            }
            return _moduleReposity.FindAll(Specification<Module>.Eval(t => t.ParentId == parentModule.Id));
        }

        ///// <summary>
        ///// 添加模块
        ///// </summary>
        ///// <param name="module"></param>
        //public void AddModule(Module module)
        //{
        //    //调用仓储存储模块
        //}

        ///// <summary>
        ///// 修改模块
        ///// </summary>
        ///// <param name="module"></param>
        //public void UpdateModule(Module module)
        //{
        //    //调用仓储修改
        //}
    }
}
