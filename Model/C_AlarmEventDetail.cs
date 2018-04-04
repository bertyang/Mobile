using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.Model
{
    /// <summary>
    /// 事件 等
    /// </summary>
    public class C_AlarmEventDetail
    {
        public C_AlarmEventDetail(C_AlarmEventInfo ae, List<C_AccInfo> acl, List<C_TaskInfoDetail> tl, List<C_AmbulanceStateTimeInfo> al)
        {
            m_tae = ae;
            if (acl != null)
            {
                foreach (C_AccInfo ac in acl)
                {
                    TACLS.Add(new C_AcceptEvent(ac, tl, al));
                }
            }
        }
        private C_AlarmEventInfo m_tae = new C_AlarmEventInfo();//事件
        /// <summary>
        /// 事件
        /// </summary>
        public C_AlarmEventInfo tae { get { return m_tae; } set { m_tae = value; } }

        private List<C_AcceptEvent> m_TACLS = new List<C_AcceptEvent>();//受理列表 等
        /// <summary>
        /// 受理列表 等
        /// </summary>
        public List<C_AcceptEvent> TACLS { get { return m_TACLS; } set { m_TACLS = value; } }
        //--------------------------
        //以下这几个对象 不太重要
        //public List<TAlarmCall> m_acLs = new List<TAlarmCall>();//录音
        ///// <summary>
        ///// 录音
        ///// </summary>
        //public List<TAlarmCall> acLs { get { return m_acLs; } set { m_acLs = value; } }

    }
    /// <summary>
    /// 受理 等
    /// </summary>
    public class C_AcceptEvent
    {
        public C_AcceptEvent(C_AccInfo ac, List<C_TaskInfoDetail> tl, List<C_AmbulanceStateTimeInfo> al)
        {
            m_tac = ac;
            if (tl != null)
            {
                List<C_TaskInfoDetail> m_tl=tl.Where(t => t.EventCode == ac.EventCode && t.AcceptOrder == ac.AcceptOrder).ToList();
                foreach (C_TaskInfoDetail t in m_tl)
                {
                    m_TT.Add(new C_Task(t, al));
                }
            }
        }

        private C_AccInfo m_tac = new C_AccInfo();//受理
        /// <summary>
        /// 受理
        /// </summary>
        public C_AccInfo tac { get { return m_tac; } set { m_tac = value; } }

        private List<C_Task> m_TT = new List<C_Task>();//任务 等
        /// <summary>
        /// 任务 等
        /// </summary>
        public List<C_Task> TT { get { return m_TT; } set { m_TT = value; } }

    }


    /// <summary>
    /// 任务 等
    /// </summary>
    public class C_Task
    {
        public C_Task(C_TaskInfoDetail t, List<C_AmbulanceStateTimeInfo> al)
        {
            m_tt = t;
            if (al != null)
                m_tastLs = al.Where(q => q.TaskCode == t.Code).ToList();
        }

        private C_TaskInfoDetail m_tt = new C_TaskInfoDetail();//任务
        /// <summary>
        /// 任务
        /// </summary>
        public C_TaskInfoDetail tt { get { return m_tt; } set { m_tt = value; } }

        private List<C_AmbulanceStateTimeInfo> m_tastLs = new List<C_AmbulanceStateTimeInfo>();//车辆状态变化标识
        /// <summary>
        /// 车辆状态变化标识
        /// </summary>
        public List<C_AmbulanceStateTimeInfo> tastLs { get { return m_tastLs; } set { m_tastLs = value; } }
    }

}
