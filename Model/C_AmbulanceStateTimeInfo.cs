using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class C_AmbulanceStateTimeInfo
    {
        private int m_Code;//编码
        /// <summary>
        /// 编码--
        /// </summary>
        public int Code
        {
            get { return m_Code; }
            set { m_Code = value; }
        }

        private string m_AmbuCode;//车辆编码
        /// <summary>
        /// 车辆编码--
        /// </summary>
        public string AmbuCode
        {
            get { return m_AmbuCode; }
            set { m_AmbuCode = value; }
        }

        private string m_TaskCode;//任务编码
        /// <summary>
        /// 任务编码--
        /// </summary>
        public string TaskCode
        {
            get { return m_TaskCode; }
            set { m_TaskCode = value; }
        }

        private int m_WorkStateId;//车辆状态编码
        /// <summary>
        /// 车辆状态编码--
        /// </summary>
        public int WorkStateId
        {
            get { return m_WorkStateId; }
            set { m_WorkStateId = value; }
        }

        private Nullable<DateTime> m_KeyPressTime;//时刻值
        /// <summary>
        /// 时刻值--
        /// </summary>
        public Nullable<DateTime> KeyPressTime
        {
            get { return m_KeyPressTime; }
            set { m_KeyPressTime = value; }
        }

        private Nullable<DateTime> m_SaveTime;//记录时刻
        /// <summary>
        /// 记录时刻--
        /// </summary>
        public Nullable<DateTime> SaveTime
        {
            get { return m_SaveTime; }
            set { m_SaveTime = value; }
        }

        private int m_SourceCode;//操作来源编码
        /// <summary>
        /// 操作来源编码--
        /// </summary>
        public int SourceCode
        {
            get { return m_SourceCode; }
            set { m_SourceCode = value; }
        }

        private string m_OperationCode;//操作员编码
        /// <summary>
        /// 操作员编码--
        /// </summary>
        public string OperationCode
        {
            get { return m_OperationCode; }
            set { m_OperationCode = value; }
        }

        private bool m_IsOnline;//车辆是否在线
        /// <summary>
        /// 车辆是否在线--
        /// </summary>
        public bool IsOnline
        {
            get { return m_IsOnline; }
            set { m_IsOnline = value; }
        }
        //-----------------------------以上为实际表格TAmbulanceStateTime中的 字段名
        private string m_RealSign;//实际标识
        /// <summary>
        /// 实际标识
        /// </summary>
        public string RealSign
        {
            get { return m_RealSign; }
            set { m_RealSign = value; }
        }
        /// <summary>
        /// 车辆状态
        /// </summary>
        public string WorkStateName { get; set; }
        
        /// <summary>
        /// 操作来源
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string JobCode { get; set; }

        
    }
}
