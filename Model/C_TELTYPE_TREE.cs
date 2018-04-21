using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 电话类型树
    /// </summary>
    public class C_TELTYPE_TREE
    {
        public List<C_TELTYPE_TREE> children { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string url { get; set; }
        public string iconCls { get; set; }
        public string ParentID { get; set; }
    }
}
