using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_Notice
    {
        public int ID { get; set; }
        public int Num { get; set; }
        public string content { get; set; }
        public string sendList { get; set; }
        public DateTime sendTime { get; set; }
        public int typeCode { get; set; }
        public string typeName { get; set; }
        public string worker { get; set; }
        public int backCount { get; set; }
    }
}
