using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 菜单树实体类
    /// 贾明
    /// </summary>
    public class C_MENU_TREE
    {
        public List<C_MENU_TREE> children { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string iconCls { get; set; }
        public string url { get; set; }
        public string ParentID { get; set; }
    }
}
