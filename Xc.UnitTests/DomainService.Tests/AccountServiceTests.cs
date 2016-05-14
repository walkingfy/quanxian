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
    public class AccountServiceTests
    {
        private IAccountRepository accountRepository;
        private AccountService service;
        private TransactionScope transaction;
        [TestFixtureSetUp]
        public void InitRepository()
        {
            IRepositoryContext content = new EntityFrameworkRepositoryContext();
            accountRepository = new AccountRepository(content);
            service = new AccountService(accountRepository.Context, accountRepository);
            transaction = new TransactionScope(TransactionScopeOption.Required);

            var entity = new Account() { Name = "test", Password = "123", CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            accountRepository.Add(entity);
            accountRepository.Context.Commit();
            _account = entity;
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            transaction.Dispose();
        }

        private Account _account { get; set; }
        [Test]
        public void ExistsUserName_CheckTest_ReturnTrue()
        {
            Assert.IsTrue(service.CheckUserNameIsExists(_account));
        }

        [Test]
        public void CheckAccountPwd_CheckSelfAccount_ReturnTrue()
        {
            Assert.IsNotNull(service.CheckAccountPassword(_account));
        }

        [Test]
        public void ChangeAccountPwd_UpdateNewPwd_ReturnAccount()
        {
            var entity = service.ChangeAccountPassword(_account, "456");
            Assert.AreEqual("456", entity.Password);
            var myt = accountRepository.Find(Specification<Account>.Eval(t => t.Id == entity.Id));
            Assert.AreEqual("456", myt.Password);
        }
    }
}
