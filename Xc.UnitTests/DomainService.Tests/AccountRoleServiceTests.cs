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
using Xc.Core.Tools;
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
    public class AccountRoleServiceTests
    {
        private AccountRolesRepository accountRoleRepository;
        private AccountRepository accountRepository;
        private RoleRepository roleRepository;
        private TransactionScope transaction;

        [TestFixtureSetUp]
        public void InitRepositroy()
        {
            var builder = new ContainerBuilder();
            builder.RegisterInstance(new EntityFrameworkRepositoryContext()).As<IRepositoryContext>();
            builder.RegisterType<AccountRolesRepository>();
            builder.RegisterType<AccountRepository>();
            builder.RegisterType<RoleRepository>();
            IContainer container = builder.Build();
            accountRoleRepository = container.Resolve<AccountRolesRepository>();
            accountRepository = container.Resolve<AccountRepository>();
            roleRepository = container.Resolve<RoleRepository>();
            transaction = new TransactionScope(TransactionScopeOption.Required);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            transaction.Dispose();
        }

        [Test]
        public void ModifyAccountRole_VirtualData_ReturnNewAccountRole()
        {
            //初始化数据
            var service = new AccountRolesService(accountRoleRepository.Context, accountRoleRepository);
            var account = new Account { Name = "test", Password = "123", CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            accountRepository.Add(account);
            accountRepository.Context.Commit();
            var role = new Role("Test", "Test", EnumIsVisible.Can);
            var role1 = new Role("Test1", "Test", EnumIsVisible.Can);
            var role2 = new Role("Test2", "Test", EnumIsVisible.Can);
            var role3 = new Role("Test3", "Test", EnumIsVisible.Can);
            roleRepository.Add(role);
            roleRepository.Add(role1);
            roleRepository.Add(role2);
            roleRepository.Add(role3);
            roleRepository.Context.Commit();
            var accountrole = new AccountRole(account.Id, role.Id);
            accountRoleRepository.Add(accountrole);
            accountRoleRepository.Context.Commit();
            Assert.IsNotNull(accountrole.Id);
            var oneCount =
                accountRoleRepository.FindAll(Specification<AccountRole>.Eval(t => t.AccountId == account.Id)).Count();
            Assert.AreEqual(1, oneCount);
            //开始测试
            var roleList = new List<Guid> { role1.Id, role2.Id, role3.Id };
            service.ModifyAccountRole(account, roleList);
            var threeCont =
                accountRoleRepository.FindAll(Specification<AccountRole>.Eval(t => t.AccountId == account.Id)).Count();
            Assert.AreEqual(3, threeCont);
        }
    }
}
