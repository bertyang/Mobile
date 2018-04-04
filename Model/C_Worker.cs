using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_Worker
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string EmpNo { get; set; }
        public string Active { get; set; }
        public string Post { get; set; }
        public string Unit { get; set; } 
        public string ParentName { get; set; }
        public string Mobile { get; set; }
        public string Tel { get; set; }
        public Nullable<int> RoleID { get; set; }
    }
}
