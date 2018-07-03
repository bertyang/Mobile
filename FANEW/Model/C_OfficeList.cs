using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_OfficeList
    {
        public string OfficeType { get; set; }
        public int ID { get; set; }
        public string Title { get; set; }
        public string Writer { get; set; }
        public DateTime CreateTime { get; set; }
        public string SendType { get; set; }
        public int AnnexCount { get; set; }
        public int? ReadCount { get; set; }
        public bool IsSelf { get; set; }
    }
}
