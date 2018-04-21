using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_OfficeReceive
    {
        public int 编码 { get; set; }
        public bool 是否已阅 { get; set; }
        public DateTime? 查阅时间 { get; set; }
        public string 接收人 { get; set; }
    }
}
