using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 受理单打印-受理信息
    /// 刘爱青
    /// 2011.4.7
    /// </summary>
    public class AttemperAcceptInfo
    {
        private string m_Order;
        /// <summary>
        /// 受理序号
        /// </summary>
        public string Order
        {
            get { return m_Order; }
            set { m_Order = value; }
        }
        private string m_AcceptType;
        /// <summary>
        /// 受理类型
        /// </summary>
        public string AcceptType
        {
            get { return m_AcceptType; }
            set { m_AcceptType = value; }
        }
        private string m_AttemperPerson;
        /// <summary>
        /// 调度员
        /// </summary>
        public string AttemperPerson
        {
            get { return m_AttemperPerson; }
            set { m_AttemperPerson = value; }
        }

        private string m_ShakeTime;
        /// <summary>
        /// 电话振铃时刻
        /// </summary>
        public string ShakeTime
        {
            get { return m_ShakeTime; }
            set { m_ShakeTime = value; }
        }
        private string m_StartTime;
        /// <summary>
        /// 开始受理时刻
        /// </summary>
        public string StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }
        private string m_EndTime;
        /// <summary>
        /// 结束受理时刻
        /// </summary>
        public string EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }
        private string m_DispatchTime;
        /// <summary>
        /// 发送指令时刻
        /// </summary>
        public string DispatchTime
        {
            get { return m_DispatchTime; }
            set { m_DispatchTime = value; }
        }
        private string m_Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return m_Remark; }
            set { m_Remark = value; }
        }
        private string m_MPDSRemark;
        /// <summary>
        /// MPDS备注
        /// </summary>
        public string MPDSRemark
        {
            get { return m_MPDSRemark; }
            set { m_MPDSRemark = value; }
        }
    }
}
