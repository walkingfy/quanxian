﻿using System.Web.UI.WebControls;
using Permissions.Web.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Xc.Application;
using Xc.Core.Tools;
using Xc.DataObjects;

namespace Permissions.Web.Controllers
{
    [Description("主页")]
    public class HomeController : BaseController
    {
        [Description("主页")]
        [LoginAllowView]
        [ViewPage]
        public ActionResult Index()
        {
            return View();
        }

        [Description("【主页】登陆")]
        [ViewPage]
        public ActionResult Login()
        {
            return View();
        }

        [Description("【主页】登陆")]
        [ViewPage]
        public ActionResult LogOut()
        {
            CurrentUserHelper.ClearUserInfo();
            return Redirect("Login");
        }

        [Description("【主页】获取用户菜单")]
        [LoginAllowView]
        public JsonResult GetMenus()
        {
            var appService = new RolePermissionAppService();
            var modules = new List<ModuleDataObject>();
            if (CurrentUserHelper.GetUserId() == ConfigSettingHelper.GetAdminUserId())
            {
                modules = appService.GetMenusByAdmin();
            }
            else
            {
                var roleIds = CurrentUserHelper.GetUserRoles();
                if (roleIds != null)
                {
                    modules = roleIds.Contains(ConfigSettingHelper.GetAdminUserRoleId()) ? appService.GetMenusByAdmin() : appService.GetMenus(roleIds);
                }
            }
            return this.Json(modules);
        }
    }
}