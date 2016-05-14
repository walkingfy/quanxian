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

namespace Xc.DataObjects
{
    public class RoleDataObject : DataObjectBase
    {
        [Required(ErrorMessage = "请输入角色名称")]
        [MaxLength(20, ErrorMessage = "角色名称长度超出限制")]
        public string Name { get; set; }

        [MaxLength(200, ErrorMessage = "长度超出限制")]
        public string Remark { get; set; }
        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; }
    }
}
