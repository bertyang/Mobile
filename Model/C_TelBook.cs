using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_TelBook
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string Tel1 { get; set; }
        public string Exten1 { get; set; }
        public string Tel2 { get; set; }
        public string Exten2 { get; set; }
        public int OwnerID { get; set; }
        public string Remark { get; set; }
        public int OrderNo { get; set; }
        public string IsEffect { get; set; }
    }
}
