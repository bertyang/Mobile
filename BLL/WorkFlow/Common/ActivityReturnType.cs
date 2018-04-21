using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow.Common
{
    /// <summary>
    /// 关卡完成类型
    /// </summary>
    internal class ActivityReturnType
    {
        /// <summary>
        /// 之前所有关卡
        /// </summary>
        public static string All = "all";

        /// <summary>
        /// 前一关卡
        /// </summary>
        public static string One = "one";

        /// <summary>
        /// 定制
        /// </summary>
        public static string Custom = "custom";

        /// <summary>
        /// 不能退回
        /// </summary>
        public static string Not = "not";


    }
}
