using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Permissions.Web.Config;
using Xc.Application;
using Xc.Core.Tools;
using Xc.DataObjects;

namespace Permissions.Web.Controllers
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //权限拦截是否忽略
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var path = filterContext.HttpContext.Request.Path.ToLower();
            //获取当前配置保存起来的允许页面
            IList<string> allowPages = ConfigSettingHelper.GetAllAllowPage();
            bool isIgnored = allowPages.Any(page => page.ToLower() == path);
            if (isIgnored)
                return;

            //接下来进行权限拦截与验证
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ViewPageAttribute), true);
            var isViewPage = attrs.Length == 1;//当前Action请求是否为具体的功能页

            if (this.AuthorizeCore(filterContext) == false)//根据验证判断进行处理
            {
                //注：如果未登录直接在URL输入功能权限地址提示不是很友好；如果登录后输入未维护的功能权限地址，那么也可以访问，这个可能会有安全问题
                if (isViewPage == true)
                {
                    //跳转到登录页面
                    filterContext.RequestContext.HttpContext.Response.Redirect("~/Home/Login");
                }
                else
                {
                    filterContext.Result = new JsonResult { Data = new OperationResult(OperationResultType.Error, "您没有权限执行此操作！") };
                    filterContext.RequestContext.HttpContext.Response.Redirect("~/Admin/Manage/Error");
                }
            }
        }

        /// <summary>
        /// //权限判断业务逻辑
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        protected virtual bool AuthorizeCore(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            //验证当前Action是否是匿名访问Action
            if (CheckAnonymous(filterContext))
                return true;
            //未登录验证
            if (SessionHelper.GetSession("UserId") == null)
                return false;
            //验证当前Action是否是登录就可以访问的Action
            if (CheckLoginAllowView(filterContext))
                return true;

            //下面开始用户权限验证
            //var user = new UserService();
            //SysCurrentUser CurrentUser = new SysCurrentUser();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            //如果是超级管理员,直接允许
            if (CurrentUserHelper.GetUserId() == ConfigSettingHelper.GetAdminUserId())
            {
                return true;
            }
            //如果拥有超级管理员的角色就默认全部允许
            Guid adminUserRoleId = ConfigSettingHelper.GetAdminUserRoleId();
            //检查当前角色组有没有超级角色
            if (CurrentUserHelper.GetUserRoles().Contains(adminUserRoleId))
            {
                return true;
            }

            //Action权限验证
            if (controllerName.ToLower() != "manage")//如果当前Action请求为具体的功能页并且不是Manage中 Index页和Welcome页
            {
                var service = new RolePermissionAppService();
                //验证
                if (!service.GetUserRoleIsHavePermission(CurrentUserHelper.GetUserRoles(), controllerName, actionName))//如果验证该操作是否拥有权限
                {
                    return false;
                }
            }
            //管理页面直接允许
            return true;
        }

        /// <summary>
        /// [Anonymous标记]验证是否匿名访问
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckAnonymous(ActionExecutingContext filterContext)
        {
            //验证是否是匿名访问的Action
            object[] attrsAnonymous = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AnonymousAttribute), true);
            //是否是Anonymous
            return attrsAnonymous.Length == 1;
        }
        /// <summary>
        /// [LoginAllowView标记]验证是否登录就可以访问(如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了)
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        public bool CheckLoginAllowView(ActionExecutingContext filterContext)
        {
            //在这里允许一种情况,如果已经登陆,那么不对于标识了LoginAllowView的方法就不需要验证了
            object[] attrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(LoginAllowViewAttribute), true);
            //是否是LoginAllowView
            return attrs.Length == 1;
        }
    }
}