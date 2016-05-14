using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;
using Xc.Core;
using Xc.Core.Exceptions;
using Xc.Core.Tools;
using Xc.DataObjects;
using Xc.DataObjects.Enums;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Services;

namespace Xc.Application
{
    public class AccountAppService : ApplicationService
    {
        private IAccountRepository _accountRepository;
        private AccountService _accountService;
        private AccountRolesService _accountRolesService;

        public AccountAppService()
        {
            //IRepositoryContext context, IAccountRepository accountRepository, AccountService accountService
            _accountRepository = AutofacInstace.Resolve<IAccountRepository>();
            _accountService = AutofacInstace.Resolve<AccountService>();
            _accountRolesService = AutofacInstace.Resolve<AccountRolesService>();
        }

        /// <summary>
        /// 判断账户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckAccoutIsExists(string userName, Guid id)
        {
            return _accountService.CheckUserNameIsExists(new Account() { Name = userName, Id = id });
        }

        /// <summary>
        /// 判断帐号密码是否正确
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public OperationResult CheckAccount(string userName, string passWord)
        {
            try
            {
                var result =
                    _accountService.CheckAccountPassword(new Account() { Name = userName, Password = passWord.ToMd5String() });
                if (result != null)
                {
                    var domainObject = ObjectMapperManager.DefaultInstance.GetMapper<Account, AccountDataObject>().Map(result);
                    return new OperationResult(OperationResultType.Success, "登录成功", domainObject);
                }
                else
                {
                    return new OperationResult(OperationResultType.Error, "登录失败");
                }

            }
            catch (CustomException exception)
            {
                return new OperationResult(OperationResultType.Error, exception.Level == LogLevel.Information ? exception.Message : "出现错误");
            }
        }

        /// <summary>
        /// 分页获取所有用户
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public JqGrid GetAllAccount(int pageIndex, int pageSize)
        {
            return OperationBaseService.GetPageResultToJqGrid<AccountDataObject, Account, IAccountRepository>(_accountRepository, pageIndex, pageSize, t => t.Id);
        }


        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationResult AddAccount(AccountDataObject entity)
        {
            var account = OperationBaseService.ProcessMapper<AccountDataObject, Account>(entity, OperationType.Add);
            _accountRolesService.ModifyAccountRole(account, entity.RoleIds);
            return OperationAccount(entity, OperationType.Add);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationResult UpdateAccount(AccountDataObject entity)
        {
            var account = OperationBaseService.ProcessMapper<AccountDataObject, Account>(entity, OperationType.Add);
            //修改用户角色
            _accountRolesService.ModifyAccountRole(account, entity.RoleIds);
            return OperationAccount(entity, OperationType.Update);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationResult DeleteAccount(AccountDataObject entity)
        {
            return OperationAccount(entity, OperationType.Delete);
        }

        /// <summary>
        /// 获取账号角色
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public IEnumerable<Guid> GetAccountRoles(Guid accountId)
        {
            List<Guid> guids = new List<Guid>();
            _accountRolesService.GetAccountRole(accountId).ToList().ForEach(t => guids.Add(t.RoleId));
            return guids;
        }

        /// <summary>
        /// 私有方法
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="operationType">操作类型，OperationType枚举值</param>
        /// <returns></returns>
        private OperationResult OperationAccount(AccountDataObject entity, OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.Add:
                    return OperationBaseService.Save<AccountDataObject, Account, IAccountRepository>(_accountRepository, entity);
                case OperationType.Update:
                    return OperationBaseService.Update<AccountDataObject, Account, IAccountRepository>(_accountRepository, entity);
                case OperationType.Delete:
                    return OperationBaseService.Delete<AccountDataObject, Account, IAccountRepository>(_accountRepository, entity);
                default:
                    return new OperationResult(OperationResultType.Success);
            }
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public OperationResult ResetPassword(AccountDataObject entity)
        {
            var account = OperationBaseService.ProcessMapper<AccountDataObject, Account>(entity, OperationType.Add);
            var result = _accountService.ResetPassword(account);
            if (result)
                return new OperationResult(OperationResultType.Success, "操作成功", entity);
            else
                return new OperationResult(OperationResultType.Error, "操作失败");
        }
    }
}
