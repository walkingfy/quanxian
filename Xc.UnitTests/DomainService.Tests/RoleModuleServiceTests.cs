/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.UI.WebControls;
using NUnit.Framework;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Services;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;
using Xc.Repositories;

namespace Xc.UnitTests.DomainService.Tests
{
    [TestFixture]
    public class RolePermissionServiceTests
    {
        private RolePermissionRepository rolePermissionRepository;
        private ModuleRepository moduleRepository;
        private RoleRepository roleRepository;
        private RolePermissionService service;
        private TransactionScope transaction;
        [TestFixtureSetUp]
        public void InitRepository()
        {
            IRepositoryContext context = new EntityFrameworkRepositoryContext();
            rolePermissionRepository = new RolePermissionRepository(context);
            moduleRepository = new ModuleRepository(context);
            roleRepository = new RoleRepository(context);
            service = new RolePermissionService(context, rolePermissionRepository, moduleRepository);
            transaction = new TransactionScope(TransactionScopeOption.Required);
            var role = new Role("管理员", "测试的", EnumIsVisible.Can);
            roleRepository.Add(role);
            roleRepository.Context.Commit();
            _role = role;
            var entity = new Xc.Domain.Entity.Module() { Name = "Test", ParentId = null, Type = EnumModuleType.Menu, Sort = 1, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            var entity1 = new Xc.Domain.Entity.Module() { Name = "Test1", ParentId = null, Type = EnumModuleType.Menu, Sort = 2, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            moduleRepository.Add(entity);
            moduleRepository.Add(entity1);
            roleRepository.Context.Commit();
            var entity2 = new Xc.Domain.Entity.Module() { Name = "Test2", ParentId = entity.Id, Type = EnumModuleType.Menu, Sort = 3, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            var entity3 = new Xc.Domain.Entity.Module() { Name = "Test3", ParentId = entity1.Id, Type = EnumModuleType.Button, Sort = 4, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            moduleRepository.Add(entity2);
            moduleRepository.Add(entity3);
            roleRepository.Context.Commit();
            _module = entity;
            _module1 = entity1;
            _module2 = entity2;
            _module3 = entity3;
        }

        private Role _role { get; set; }
        private Module _module { get; set; }
        private Module _module1 { get; set; }
        private Module _module2 { get; set; }
        private Module _module3 { get; set; }

        [TestFixtureTearDown]
        public void Disponse()
        {
            transaction.Dispose();
        }

        [Test]
        public void ModifyRoleModule_VirtualData_ReturnNewRoleModule()
        {
            //初始化数据
            var rolemodules = new RolePermission(_role.Id, _module.Id);
            rolePermissionRepository.Add(rolemodules);
            rolePermissionRepository.Context.Commit();
            Assert.IsNotNull(rolemodules.Id);
            var oneCount =
                rolePermissionRepository.FindAll(Specification<RolePermission>.Eval(t => t.RoleId == _role.Id)).Count();
            Assert.AreEqual(1, oneCount);
            //开始测试
            var moduleList = new List<Module> { _module1, _module2, _module3 };
            service.ModifyRolePermissions(_role, moduleList);
            var threeCont =
                rolePermissionRepository.FindAll(Specification<RolePermission>.Eval(t => t.RoleId == _role.Id)).Count();
            Assert.AreEqual(3, threeCont);

            var menus = service.GetModuleMenusByRole(_role);
            Assert.AreEqual(2, menus.Count());

            var childCount = service.GetModuleButtonsByRole(_role, _module1);
            Assert.AreEqual(1, childCount.Count());

            var allCount = service.GetRolePermission(new List<Guid>(){_role.Id});
            Assert.AreEqual(3, allCount.Count());
        }
    }
}
