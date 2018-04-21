using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow.Common
{
    /// <summary>
    /// 流程实例状态类型
    /// </summary>
    internal class FlowInstanceState
    {
        /// <summary>
        /// 未激活
        /// </summary>
        public static string InActive = "I";

        /// <summary>
        /// 激活
        /// </summary>
        public static string Active = "A";

        /// <summary>
        /// 撤回
        /// </summary>
        public static string Recall = "R";

        /// <summary>
        /// 通过
        /// </summary>
        public static string Pass = "P";

        /// <summary>
        /// 否决
        /// </summary>
        public static string Reject = "J";

    }
}
