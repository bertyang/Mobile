using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_TelType
    {
        public int 编码 { get; set; }
        public string 名称 { get; set; }
        public int 上级编码 { get; set; }
        public int 顺序号 { get; set; }
        public bool 是否有效 { get; set; }
        public int 归属人ID { get; set; }
    }
}
