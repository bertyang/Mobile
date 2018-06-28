using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow.Common
{
    /// <summary>
    /// 关卡实例状态
    /// </summary>
    internal class ActivityInstanceStatus
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
        /// 搁置
        /// </summary>
        public static string Pending = "P";

        /// <summary>
        /// 完成
        /// </summary>
        public static string Completed = "C";
    }
}
