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
using Xc.Domain.ValueObject;
using Xc.Repositories;

namespace Xc.UnitTests.DomainService.Tests
{
    [TestFixture]
    public class ModuleServiceTests
    {
        private IModuleRepository moduleRepository;
        private ModuleService service;
        private TransactionScope transaction;

        public Module MEntity { get; set; }
        public Module MEntity1 { get; set; }
        public Module MEntity2 { get; set; }
        public Module MEntity3 { get; set; }

        [TestFixtureSetUp]
        public void InitRespository()
        {
            IRepositoryContext context = new EntityFrameworkRepositoryContext();
            moduleRepository = new ModuleRepository(context);
            service = new ModuleService(context, moduleRepository);
            transaction = new TransactionScope(TransactionScopeOption.Required);

            //初始化数据

            var entity = new Xc.Domain.Entity.Module() { Name = "Test", ParentId = null, Type = EnumModuleType.Menu, Sort = 1, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            var entity1 = new Xc.Domain.Entity.Module() { Name = "Test1", ParentId = null, Type = EnumModuleType.Menu, Sort = 2, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            moduleRepository.Add(entity);
            moduleRepository.Add(entity1);
            moduleRepository.Context.Commit();
            var entity2 = new Xc.Domain.Entity.Module() { Name = "Test2", ParentId = entity.Id, Type = EnumModuleType.Menu, Sort = 3, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            var entity3 = new Xc.Domain.Entity.Module() { Name = "Test3", ParentId = entity1.Id, Type = EnumModuleType.Menu, Sort = 4, CreateTime = DateTime.Now, IsVisible = EnumIsVisible.Can };
            moduleRepository.Add(entity2);
            moduleRepository.Add(entity3);
            moduleRepository.Context.Commit();
            MEntity = entity;
            MEntity1 = entity1;
            MEntity2 = entity2;
            MEntity3 = entity3;
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            transaction.Dispose();
        }

        [Test]
        public void MoveModule_VirtualEntity_ReturnSort()
        {
            var sort = MEntity.Sort;
            var sort1 = MEntity1.Sort;
            service.MoveModule(MEntity, MEntity1);
            Assert.AreEqual(sort, MEntity1.Sort);
            Assert.AreEqual(sort1, MEntity.Sort);
        }

        [Test]
        public void GetALLParent_VirtualEntity_ReturnCount()
        {
            var parentList = service.GetAllParentModuleMenus();
            Assert.IsTrue(parentList.Count() >= 2);
        }

        [Test]
        public void GetChildParent_ByParent_ReturnCount()
        {
            var child = service.GetChildModuls(MEntity).Single();
            Assert.AreEqual(MEntity.Id, child.ParentId);
        }
    }
}
