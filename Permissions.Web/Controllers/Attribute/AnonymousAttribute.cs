using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Permissions.Web.Controllers
{
    /// <summary>
    /// 匿名访问标记
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AnonymousAttribute : Attribute
    {
    }
}