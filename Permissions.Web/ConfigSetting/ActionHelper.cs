using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Xc.DataObjects.DTO;

namespace Permissions.Web.ConfigSetting
{
    public class ActionHelper
    {
        /// <summary>
        ///  获取所有的动作信息(out 控制器信息)
        /// </summary>
        /// <param name="controllers">控制器信息</param>
        /// <returns></returns>
        public static IList<MvcActionDataObject> GetAllActionByAssembly(out IList<MvcControllerDataObject> controllers)
        {
            controllers = new List<MvcControllerDataObject>();

            var result = new List<MvcActionDataObject>();

            var types = Assembly.Load("Permissions.Web").GetTypes();

            foreach (var type in types)
            {
                if (type.BaseType.Name == "BaseController")//如果是Controller
                {
                    var rs = GetActions(type);
                    result.AddRange(rs);

                    var controller = new MvcControllerDataObject();
                    controller.ControllerName = type.Name.Replace("Controller", "");//去除Controller的后缀
                    //设置Controller数组
                    object[] attrs = type.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (attrs.Length > 0)
                        controller.Description = (attrs[0] as System.ComponentModel.DescriptionAttribute).Description;
                    controllers.Add(controller);
                }
            }
            return result;
        }
        /// <summary>
        /// 获取所有的动作(根据控制器的Type)
        /// </summary>
        /// <param name="type">类别</param>
        /// <param name="DefaultPage">默认的控制器首页(Action)</param>
        /// <returns></returns>
        public static IList<MvcActionDataObject> GetActions(Type type)
        {
            var members = type.GetMethods();
            var result = new List<MvcActionDataObject>();
            foreach (var member in members)
            {
                if (member.ReturnType.Name == "ActionResult" || member.ReturnType.Name =="JsonResult")//如果是Action
                {
                    var item = new MvcActionDataObject();
                    item.ActionName = member.Name;
                    item.ControllerName = member.DeclaringType.Name.Substring(0, member.DeclaringType.Name.Length - 10); // 去掉“Controller”后缀

                    object[] attrs = member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (attrs.Length > 0)
                        item.Description = (attrs[0] as System.ComponentModel.DescriptionAttribute).Description;
                    item.LinkUrl = "/" + item.ControllerName + "/" + item.ActionName;
                    object[] Defaultpages = member.GetCustomAttributes(typeof(ViewPage), true);
                    if (Defaultpages.Length > 0)
                        item.IsPage = true;
                    result.Add(item);
                }

            }
            return result;
        }
    }
}