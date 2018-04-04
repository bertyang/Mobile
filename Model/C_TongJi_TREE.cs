using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
  public  class C_TongJi_TREE
    {
      public List<C_TongJi_TREE> children { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string treeUrl { get; set; }
        public string iconCls { get; set; }
        public string ParentID { get; set; }

        public string Type { get; set; }
    }
}
