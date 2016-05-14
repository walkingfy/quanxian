/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Core.Exceptions;
using Xc.Domain.ValueObject;

namespace Xc.Domain.Entity
{
    /// <summary>
    /// 账号聚合根
    /// </summary>
    public sealed class Account : AggregateRoot
    {
        public Account()
        {

        }
        public Account(string name, string password, string email, string phone, string realName, string remark, EnumIsVisible isVisible)
        {
            this.Name = name;
            this.Password = password;
            this.Email = email;
            this.Phone = phone;
            this.RealName = realName;
            this.Remark = remark;
            this.IsVisible = isVisible;
            this.CreateTime = DateTime.Now;
        }

        #region Public Properties

        [MaxLength(50, ErrorMessage = "账号长度超过限制")]
        [Required(ErrorMessage = "账号不能为空")]
        public string Name { get; set; }
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
        [EmailAddress(ErrorMessage = "邮箱地址错误")]
        public string Email { get; set; }
        [MaxLength(14, ErrorMessage = "电话号码超出长度限制")]
        public string Phone { get; set; }
        [MaxLength(20, ErrorMessage = "真实姓名超出长度限制")]
        public string RealName { get; set; }
        [MaxLength(200, ErrorMessage = "备注超出长度限制")]
        public string Remark { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public EnumIsVisible IsVisible { get; set; }
        #endregion

        #region Public Method
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (oldPassword == Password)
            {
                Password = newPassword;
            }
            else
            {
                throw new CustomException("原密码与现密码不一致");
            }
        }

        /// <summary>
        /// 启用账户
        /// </summary>
        public void Enable()
        {
            this.IsVisible = EnumIsVisible.Can;
        }

        /// <summary>
        /// 禁用账户
        /// </summary>
        public void Disable()
        {
            this.IsVisible = EnumIsVisible.Not;
        }
        #endregion

    }
}
