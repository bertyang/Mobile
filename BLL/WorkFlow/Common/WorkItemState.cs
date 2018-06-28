using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow.Common
{
    internal class WorkItemState
    {
        /// <summary>
        /// 等待
        /// </summary>
        public static string Waiting = "W";

        /// <summary>
        /// 签核完成
        /// </summary>
        public static string Completed = "C";

        /// <summary>
        /// 自动完成
        /// </summary>
        public static string Pass = "P";

        /// <summary>
        /// 保留
        /// </summary>
        public static string Reserve = "R";


        /// <summary>
        /// 撤回
        /// </summary>
        //public static string Transfer = "T";

    }
}
