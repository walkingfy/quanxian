/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core;
using Xc.Core.Exceptions;
using Xc.Core.Tools;
using Xc.Domain.Entity;
using Xc.Domain.Repositories;
using Xc.Domain.Repositories.IRepositories;
using Xc.Domain.Specifications;
using Xc.Domain.ValueObject;

namespace Xc.Domain.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountReposity;
        private readonly IRepositoryContext _repositoryContext;

        public AccountService(IRepositoryContext repositoryContext, IAccountRepository accountReposity)
        {
            this._repositoryContext = repositoryContext;
            this._accountReposity = accountReposity;
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="account">用户资料，要求包含账户资料和Id。【Id可为空(Guid.Empty)】</param>
        /// <returns></returns>
        public bool CheckUserNameIsExists(Account account)
        {
            if (account == null)
            {
                throw new CustomException("account不能为空", "0500", LogLevel.Warning);
            }


            return
                _accountReposity.Exists(Specification<Account>.Eval(t => t.Name == account.Name && t.Id != account.Id));
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="newPwd">新密码</param>
        /// <returns></returns>
        public Account ChangeAccountPassword(Account account, string newPwd)
        {
            if (account == null)
                throw new CustomException("account不能为空", "0500", LogLevel.Warning);
            if (String.IsNullOrEmpty(newPwd))
                throw new CustomException("newPwd不能为空", "0500", LogLevel.Warning);
            account.ChangePassword(account.Password, newPwd);
            _accountReposity.Update(account);
            _accountReposity.Context.Commit();
            return account;
        }

        /// <summary>
        /// 检查用户密码是否正确
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Account CheckAccountPassword(Account account)
        {
            if (account == null)
            {
                throw new CustomException("account不能为空", "0500", LogLevel.Warning);
            }
            var entity =
                _accountReposity.Find(
                    Specification<Account>.Eval(t => t.Name == account.Name && t.Password == account.Password));
            if (entity == null)
            {
                throw new CustomException("用户名或密码不正确", "1404", LogLevel.Information);
            }
            else
            {
                if (entity.IsVisible == EnumIsVisible.Not)
                {
                    throw new CustomException("该用户未激活，请联系管理员", "1403", LogLevel.Information);
                }
                else
                {
                    return entity;
                }
            }
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Account AddAccount(Account account)
        {
            if (account == null)
            {
                throw new CustomException("account不能为空", "0500", LogLevel.Warning);
            }

            //加密密码,初始密码888888
            account.Password = ("888888").ToMd5String();

            _accountReposity.Add(account);
            _accountReposity.Context.Commit();
            return account;
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool ResetPassword(Account entity)
        {
            //加密密码,初始密码888888
            entity.Password = ("888888").ToMd5String();

            _accountReposity.Update(entity);
            var count = _accountReposity.Context.Commit();
            return count > 0;
        }
    }
}
