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
//using Autofac;
using System.Transactions;
using Autofac;
using NUnit.Framework;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;
using Xc.Repositories;
using Xc.Repositories.EntityFramework;

namespace Xc.UnitTests.Repository.Tests
{
    [TestFixture]
    public class AccountRepositoryTests
    {
        private TransactionScope trans = null;
        private AccountRepository repository = null;
        public Account AddEntity { get; set; }
        //[SetUp]
        [TestFixtureSetUp]
        public void InitTrans()
        {
            //Database.SetInitializer(new XcDbContextInitializer());
            trans = new TransactionScope(TransactionScopeOption.Required);
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new EntityFrameworkRepositoryContext()).As<IRepositoryContext>();
            builder.RegisterType<AccountRepository>();
            IContainer container = builder.Build();
            repository = container.Resolve<AccountRepository>();
        }

        [TestFixtureTearDown]
        public void DisposeTrans()
        {
            trans.Dispose();
        }


        [Test]
        public void Add_VritualEntity_ReturnGuid()
        {
            var entity = new Account() { Name = "test", Password = "123", CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            repository.Add(entity);
            repository.Context.Commit();
            Assert.IsNotNull(repository);
            Assert.IsNotNull(entity.Id);
            AddEntity = entity;
        }

        [Test]
        public void Update_NameEqualsTestEntity_ReturnNewEntity()
        {
            var entity = repository.Find(Specification<Account>.Eval(t => t.Name == "test"));
            entity.Name = "newtest";
            repository.Update(entity);
            repository.Context.Commit();
            var newentity = repository.Find(Specification<Account>.Eval(t => t.Id == entity.Id));
            Assert.AreEqual(entity.Name, newentity.Name);
        }

        [Test]
        public void Remove_NameEqualsTestEntity_ReturnCountMinish()
        {
            var count1 = repository.FindAll().Count();
            var entity = repository.Find(Specification<Account>.Eval(t => t.Name == "test"));
            repository.Remove(entity);
            repository.Context.Commit();
            var count2 = repository.FindAll().Count();
            Assert.AreEqual(count1 - 1, count2);
        }
    }
}
