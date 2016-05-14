using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Permissions.Web.Controllers
{
    /// <summary>
    /// 普通页面
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ViewPageAttribute : Attribute
    {
    }
}