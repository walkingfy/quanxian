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
    public class AccountRolesRepositoryTests
    {
        private AccountRolesRepository repository;
        private AccountRepository accountRepository;
        private RoleRepository roleRepository;
        private TransactionScope transaction;
        [TestFixtureSetUp]
        public void InitRepository()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new EntityFrameworkRepositoryContext()).As<IRepositoryContext>();
            builder.RegisterType<AccountRolesRepository>();
            builder.RegisterType<AccountRepository>();
            builder.RegisterType<RoleRepository>();
            IContainer container = builder.Build();
            repository = container.Resolve<AccountRolesRepository>();
            accountRepository = container.Resolve<AccountRepository>();
            roleRepository = container.Resolve<RoleRepository>();
            transaction = new TransactionScope(TransactionScopeOption.Required);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            transaction.Dispose();
        }

        private Account _account { get; set; }
        [Test]
        public void Add_VritualEntity_ReturnGUID()
        {
            var account = new Account { Name = "test", Password = "123", CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            accountRepository.Add(account);
            accountRepository.Context.Commit();
            var role = new Role("Test", "Test", EnumIsVisible.Can);
            roleRepository.Add(role);
            roleRepository.Context.Commit();
            var accountrole = new AccountRole(account.Id, role.Id);
            repository.Add(accountrole);
            repository.Context.Commit();
            _account = account;
            Assert.IsNotNull(accountrole.Id);
        }

        [Test]
        public void RemoveAll_Account_ReturnCount()
        {
            var oldCount = repository.FindAll(Specification<AccountRole>.Eval(t => t.AccountId == _account.Id)).Count();
            repository.RemoveAllRoleByAccount(_account);
            var count = repository.FindAll(Specification<AccountRole>.Eval(t => t.AccountId == _account.Id)).Count();
            Assert.AreEqual(0, count);
            Assert.AreNotEqual(oldCount,count);
        }

    }
}
