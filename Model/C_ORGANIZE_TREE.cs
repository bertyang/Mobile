using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 组织机构树实体类
    /// </summary>
    public class C_ORGANIZE_TREE
    {
        public List<C_ORGANIZE_TREE> children { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string url { get; set; }
        public string iconCls { get; set; }
        public string ParentID { get; set; }

        public string Type { get; set; }
        public string attributes { get; set; }
    }
}
