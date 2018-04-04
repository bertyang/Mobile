using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow.Common
{
    internal class WorkItemApproveType
    {
        /// <summary>
        /// 签核
        /// </summary>
        public static string Normal = "A";

        /// <summary>
        /// 手工签核
        /// </summary>
        public static string Manual = "M";

        /// <summary>
        /// 无签核人
        /// </summary>
        public static string Blank = "B";

    }
}
