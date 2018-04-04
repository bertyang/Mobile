using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;
using Anchor.FA.BLL.WorkFlow.Common;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class FlowDefine
    {
        #region 属性
        private List<Activity> m_ListActivity = new List<Activity>();
        private int m_FlowId = 0;
        private string m_FlowName = string.Empty;
        private bool m_IsInner;
        private string m_Url;

        public int ID
        {
            get { return m_FlowId; }
            set { m_FlowId = value; }
        }

        public string Name
        {
            get { return m_FlowName; }
            set { m_FlowName = value; }
        }

        public string Url
        {
            get { return m_Url; }
            set { m_Url = value; }
        }

        public bool IsInner
        {
            get { return m_IsInner; }
            set { m_IsInner = value; }
        }

        public List<Activity> Activitys
        {
            get
            {
                if (m_ListActivity.Count == 0)
                {
                    List<F_ACTIVITY> listActivity = DAL.WorkFlow.Activity.GetListByFlowId(this.ID);

                    foreach (F_ACTIVITY entity in listActivity)
                    {
                        Activity activity = new Activity(entity.ID);

                        m_ListActivity.Add(activity);
                    }
                }

                return m_ListActivity;
            }

            set { m_ListActivity = value; }
        }
        #endregion

        public FlowDefine(int flowId)
        {
            m_FlowId = flowId;

            F_FLOW flow = DAL.WorkFlow.Flow.Get(flowId);

            m_FlowName = flow.Name;

            m_IsInner = flow.IsInner.ToUpper()=="Y"? true:false;

            m_Url = flow.Url;
        }

        public Activity GetStartActivity()
        {
            return this.Activitys.Find(t => t.Type == ActivityType.START);
        }

    }
}
