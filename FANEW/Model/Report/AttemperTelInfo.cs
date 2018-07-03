using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    public class AttemperTelInfo
    {
        private string m_TelNumber;
        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelNumber
        {
            get { return m_TelNumber; }
            set { m_TelNumber = value; }
        }
        private string m_CallTime;
        /// <summary>
        /// 通话时刻
        /// </summary>
        public string CallTime
        {
            get { return m_CallTime; }
            set { m_CallTime = value; }
        }
        private string m_EndTime;
        /// <summary>
        /// 结束时刻
        /// </summary>
        public string EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }
        private string m_DaskNumber;
        /// <summary>
        /// 台号
        /// </summary>
        public string DaskNumber
        {
            get { return m_DaskNumber; }
            set { m_DaskNumber = value; }
        }
        private string m_Attemper;
        /// <summary>
        /// 调度员
        /// </summary>
        public string Attemper
        {
            get { return m_Attemper; }
            set { m_Attemper = value; }
        }
        private string m_CallType;
        /// <summary>
        /// 通话类型
        /// </summary>
        public string CallType
        {
            get { return m_CallType; }
            set { m_CallType = value; }
        }
    }
}
