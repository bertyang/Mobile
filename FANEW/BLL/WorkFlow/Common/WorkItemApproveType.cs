using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow.Common
{
    internal class WorkItemAssignType
    {
        /// <summary>
        /// 系统分配
        /// </summary>
        public static string Normal = "N";

        /// <summary>
        /// 加签
        /// </summary>
        public static string Add = "A";

        /// <summary>
        /// 转签
        /// </summary>
        public static string Transafer = "T";

        /// <summary>
        /// 退回
        /// </summary>
        public static string Reject = "R";
    }
}
