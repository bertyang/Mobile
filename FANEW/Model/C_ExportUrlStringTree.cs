using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{    
    /// <summary>
    /// url传参树形查询条件 包含Null值的才不影响旧方法
    /// 张炜
    /// </summary>
    public class C_ExportUrlStringTree
    {
        public string ID { get; set; }
        public string radio1 { get; set; }
        public string columnname { get; set; }
        public string radio2 { get; set; }
        public string casetext { get; set; }
        public string ParentID { get; set; }



    }
}
