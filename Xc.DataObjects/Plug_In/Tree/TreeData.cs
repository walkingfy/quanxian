using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xc.DataObjects.Plug_In.Tree
{
    public class TreeData
    {
        public TreeData(string id, string name, string type)
        {
            this.id = id;
            this.name = name;
            this.type = type;
        }
        public string name { get; set; }

        public string id { get; set; }
        public string type { get; set; }
    }
}
