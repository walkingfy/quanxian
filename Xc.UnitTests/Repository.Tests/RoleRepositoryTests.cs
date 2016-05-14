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
using Autofac.Core;
using NUnit.Framework;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;
using Xc.Repositories;

namespace Xc.UnitTests.Repository.Tests
{
    [TestFixture]
    public class RoleRepositoryTests
    {
        private RoleRepository repository;
        private TransactionScope transaction;

        public Role AddEntity { get; set; }
        [TestFixtureSetUp]
        public void InitRepository()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new EntityFrameworkRepositoryContext()).As<IRepositoryContext>();
            builder.RegisterType<RoleRepository>();
            IContainer container = builder.Build();
            repository = container.Resolve<RoleRepository>();
            transaction = new TransactionScope(TransactionScopeOption.Required);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            transaction.Dispose();
        }

        [Test]
        public void Add_VirtualEntity_ReturnGuid()
        {
            var entity = new Role("Test", "Test", EnumIsVisible.Can) { };
            repository.Add(entity);
            repository.Context.Commit();
            Assert.IsNotNull(entity.Id);
            AddEntity = entity;
        }

        [Test]
        public void Update_NameEqualsTest_ReturnNewEntity()
        {
            var entity = repository.Find(Specification<Role>.Eval(t => t.Name == "Test"));
            entity.Name = "NewTest";
            repository.Update(entity);
            repository.Context.Commit();
            var newentity = repository.Find(Specification<Role>.Eval(t => t.Id == entity.Id));
            Assert.AreEqual(entity.Name, newentity.Name);
        }

        [Test]
        public void Remove_NameEqualsTest_ReturnCountMinish()
        {
            var count1 = repository.FindAll().Count();
            var entity = repository.Find(Specification<Role>.Eval(t => t.Name == "Test"));
            repository.Remove(entity);
            repository.Context.Commit();
            var count2 = repository.FindAll().Count();
            Assert.AreEqual(count1 - 1, count2);
        }
    }
}
