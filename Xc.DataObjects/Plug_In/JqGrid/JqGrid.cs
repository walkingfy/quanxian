using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.DataObjects
{
    public class JqGrid
    {
        public JqGrid(int page, object rowData, int rowCount, int totalPage)
        {
            this.page = page;
            this.records = rowCount;
            this.rows = rowData;
            this.total = totalPage;
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }

        /// <summary>
        /// 查询出的记录数
        /// </summary>
        public int records { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object rows { get; set; }
    }
}
