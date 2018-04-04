using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class C_ExportTree
    {
        public List<C_ExportTree> children { get; set; }
        public string radio1 { get; set; }
        public string columnname { get; set; }
        public string radio2 { get; set; }
        public string casetext { get; set; }
    }
}
