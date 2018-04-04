using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_AlarmEventSearch
    {
        /// <summary>
        /// 事件编码
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 首次呼救电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 事件来源
        /// </summary>
        public string ori { get; set; }
        /// <summary>
        /// 受理次数
        /// </summary>
        public int accnum { get; set; }
        /// <summary>
        /// 事件名称
        /// </summary>
        public string alarmName { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 首次受理时刻
        /// </summary>
        public DateTime time { get; set; }
        /// <summary>
        /// 首次调度员
        /// </summary>
        public string diaoduyuan { get; set; }
        /// <summary>
        /// 派车次数
        /// </summary>
        public int chuche { get; set; }
        /// <summary>
        /// 正常完成
        /// </summary>
        public int 正常完成 { get; set; }
        /// <summary>
        /// 首次派车时刻
        /// </summary>
        public Nullable<System.DateTime> c_time { get; set; }
    }
}
