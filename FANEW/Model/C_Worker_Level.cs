using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_Worker_Level
    {
        public int ID { get; set; }
        public int Level { get; set; }
        public int ParentID { get; set; }
        public int DepartID { get; set; }
    }
}
