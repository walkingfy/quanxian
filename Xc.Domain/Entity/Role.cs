﻿/*
 * Author:xiaocong_soft
 * Blog:http://xcong.cnblogs.com
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xc.Domain.ValueObject;

namespace Xc.Domain.Entity
{
    /// <summary>
    /// 角色聚合根
    /// </summary>
    public sealed class Role : AggregateRoot
    {
        public Role()
        {
            
        }
        public Role(string name, string remark, EnumIsVisible isVisible)
        {
            this.Name = name;
            this.Remark = remark;
            this.IsVisible = isVisible;
        }

        #region Public Properties
        [MaxLength(50, ErrorMessage = "长度超出限制")]
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "长度超出限制")]
        public string Remark { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public EnumIsVisible IsVisible { get; set; }
        #endregion

        #region Public Method
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
