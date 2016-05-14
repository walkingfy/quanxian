using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.DataObjects.DTO
{
    public class MvcActionDataObject
    {
        /// <summary>
        /// 动作
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 是否是页面
        /// </summary>
        public bool IsPage { get; set; }
    }
}
