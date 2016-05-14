using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.DataObjects
{
    public class LoginModel : DataObjectBase
    {
        [MaxLength(50, ErrorMessage = "账号长度超过限制")]
        [Required(ErrorMessage = "账号不能为空")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool IsRememberMe { get; set; }
    }
}
