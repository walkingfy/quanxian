using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.DataObjects.DTO
{
    /// <summary>
    /// 控制器传输对象
    /// </summary>
    public class MvcControllerDataObject
    {
        /// <summary>
        /// 控制器
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

}
