using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Permissions.Web.Controllers
{
    /// <summary>
    /// 登陆即允许
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class LoginAllowViewAttribute : Attribute
    {
    }
}