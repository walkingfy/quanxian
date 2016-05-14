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
using Autofac;
using NUnit.Framework;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;
using Xc.Repositories;

namespace Xc.UnitTests.Repository.Tests
{
    [TestFixture]
    public class RolePermissionRepositoryTests
    {
        private RolePermissionRepository repository;
        private ModuleRepository moduleRepository;
        private RoleRepository roleRepository;
        private TransactionScope transaction;
        [TestFixtureSetUp]
        public void InitRepository()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new EntityFrameworkRepositoryContext()).As<IRepositoryContext>();
            builder.RegisterType<RoleRepository>();
            builder.RegisterType<ModuleRepository>();
            builder.RegisterType<RolePermissionRepository>();
            IContainer container = builder.Build();
            repository = container.Resolve<RolePermissionRepository>();
            moduleRepository = container.Resolve<ModuleRepository>();
            roleRepository = container.Resolve<RoleRepository>();
            transaction = new TransactionScope(TransactionScopeOption.Required);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            transaction.Dispose();
        }

        public Role _role { get; set; }

        [Test]
        public void Add_VirtualEnity_ReturnGUID()
        {
            var role = new Role("Test", "Test", EnumIsVisible.Can) { };
            roleRepository.Add(role);
            roleRepository.Context.Commit();
            var module = new Xc.Domain.Entity.Module()
            {
                Name = "Test",
                CreateTime = DateTime.Now,
                IsVisible = EnumIsVisible.Can
            };
            moduleRepository.Add(module);
            moduleRepository.Context.Commit();
            var entity = new RolePermission(role.Id, module.Id);
            repository.Add(entity);
            repository.Context.Commit();
            _role = role;
            Assert.IsNotNull(entity.Id);
        }

        [Test]
        public void RemoveAll_Role_ReturnCount()
        {
            var oldCount = repository.FindAll(Specification<RolePermission>.Eval(t => t.RoleId == _role.Id)).Count();
            repository.RemoveRolePermissionByRole(_role);
            var count = repository.FindAll(Specification<RolePermission>.Eval(t => t.RoleId == _role.Id)).Count();
            Assert.AreEqual(0, count);
            Assert.AreNotEqual(oldCount, count);
        }
    }
}
