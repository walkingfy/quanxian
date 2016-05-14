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
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;
using Xc.Repositories;
using Module = Xc.Domain.Entity.Module;

namespace Xc.UnitTests.Repository.Tests
{
    [TestFixture]
    public class ModuleRepositoryTests
    {
        private ModuleRepository moduleRepository;
        private TransactionScope transaction;

        [TestFixtureSetUp]
        public void InitRepository()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new EntityFrameworkRepositoryContext()).As<IRepositoryContext>();
            builder.RegisterType<ModuleRepository>();
            IContainer container = builder.Build();
            moduleRepository = container.Resolve<ModuleRepository>();
            transaction = new TransactionScope(TransactionScopeOption.Required);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            transaction.Dispose();
        }


        [Test]
        public void Add_VritualEntity_ReturnGUID()
        {
            var entity = new Xc.Domain.Entity.Module()
            {
                Name = "Test",
                CreateTime = DateTime.Now,
                IsVisible = EnumIsVisible.Can
            };
            moduleRepository.Add(entity);
            moduleRepository.Context.Commit();
            Assert.IsNotNull(entity.Id);
        }

        [Test]
        public void Update_NameEqualsTestEntity_ReturnNewEntity()
        {
            var entity = moduleRepository.Find(Specification<Module>.Eval(t => t.Name == "Test"));
            entity.Name = "NewTest";
            moduleRepository.Update(entity);
            moduleRepository.Context.Commit();
            var newEntity = moduleRepository.Find(Specification<Module>.Eval(t => t.Id == entity.Id));
            Assert.AreEqual(entity.Name, newEntity.Name);
        }

        [Test]
        public void Remove_NameEqualsTestEntity_ReturnCountMinish()
        {
            var count1 = moduleRepository.FindAll().Count();
            var entity = moduleRepository.Find(Specification<Module>.Eval(t => t.Name == "Test"));
            moduleRepository.Remove(entity);
            moduleRepository.Context.Commit();
            var count2 = moduleRepository.FindAll().Count();
            Assert.AreEqual(count1 - 1, count2);
        }
    }
}
