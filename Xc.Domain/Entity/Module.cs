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
using Xc.Domain.ValueObject;

namespace Xc.Domain.Entity
{
    /// <summary>
    /// 模块聚合根
    /// </summary>
    public sealed class Module : AggregateRoot
    {
        public Module()
        {
            
        }
        public Module(string name, string icon, string controller, string action, string linkAddress, string remark, EnumModuleType type, int? sort, EnumIsVisible isVisible)
        {
            this.Name = name;
            this.Icon = icon;
            this.Controller = controller;
            this.Action = action;
            this.LinkAddress = linkAddress;
            this.Remark = remark;
            this.Type = type;
            this.Sort = sort;
            this.IsVisible = isVisible;
        }

        #region Public Properties
        [MaxLength(100, ErrorMessage = "超出长度限制")]
        [Required(ErrorMessage = "名称不能为空")]
        public string Name { get; set; }
        [MaxLength(200, ErrorMessage = "超出长度限制")]
        public string Icon { get; set; }
        public Guid? ParentId { get; set; }
        [MaxLength(200, ErrorMessage = "超出长度限制")]
        public string Controller { get; set; }
        [MaxLength(200, ErrorMessage = "超出长度限制")]
        public string Action { get; set; }

        public string LinkAddress { get; set; }
        [MaxLength(200, ErrorMessage = "超出长度限制")]
        public string Remark { get; set; }
        public EnumModuleType Type { get; set; }
        public int? Sort { get; set; }

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
