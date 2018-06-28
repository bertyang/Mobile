using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_AlarmCallInfo
    {
        /// <summary>
        /// 通话时刻--
        /// </summary>
        public System.DateTime CallTime { get; set; }
        /// <summary>
        /// 振铃时刻
        /// </summary>
        public Nullable<System.DateTime> ShakeBellTime { get; set; }
        /// <summary>
        /// 结束时刻
        /// </summary>
        public Nullable<System.DateTime> FinishTime { get; set; }
        /// <summary>
        /// 台号
        /// </summary>
        public string DeskCode { get; set; }
        /// <summary>
        /// 调度员编码
        /// </summary>
        public string DispatcherCode { get; set; }
        /// <summary>
        /// 通话类型编码
        /// </summary>
        public int CallTypeCode { get; set; }
        /// <summary>
        /// 事件编码--
        /// </summary>
        public string EventCode { get; set; }
        /// <summary>
        /// 主叫号码--
        /// </summary>
        public string TelNumber { get; set; }
        /// <summary>
        /// 录音号
        /// </summary>
        public string RecordCode { get; set; }
        /// <summary>
        /// 是否呼出
        /// </summary>
        public bool IsOut { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 中心编码
        /// </summary>
        public int CenterId { get; set; }
        //-----------------------------以上为实际表格TAlarmCall中的 字段名
        /// <summary>
        /// 台名称--
        /// </summary>
        public string DeskName { get; set; }
        /// <summary>
        /// 调度员--
        /// </summary>
        public string DispatcherName { get; set; }
        /// <summary>
        /// 通话类型
        /// </summary>
        public string CallTypeName { get; set; }
        /// <summary>
        /// 中心名称
        /// </summary>
        public string CenterName { get; set; }
    }
}
