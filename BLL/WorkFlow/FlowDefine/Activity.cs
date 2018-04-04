using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Anchor.FA.Model;
using Anchor.FA.DAL.WorkFlow;

namespace Anchor.FA.BLL.WorkFlow
{ 
    #region 关卡
    /// <summary>
    /// 关卡
    /// </summary>
    internal class Activity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        private List<Participant> m_ListParticipant = new List<Participant>();    
        private List<Transation> m_ListSplitTransation = new List<Transation>();
        private List<Transation> m_ListJoinTransation = new List<Transation>();
        private FlowDefine m_FlowDefine = null;
        private string m_Type;
        private string m_JoinType;
        private string m_SplitType;
        private string m_CompleteType;
        private string m_ReturnType;
        private string m_IsSign;
        private string m_IsSms;
        private int m_ActivityId = 0;
        private string m_ActivityName = string.Empty;

        public int ID
        {
            get { return m_ActivityId; }
            set { m_ActivityId = value; }
        }

        public string Name
        {
            get { return m_ActivityName; }
            set { m_ActivityName = value; }
        }

        public string Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public string IsSign
        {
            get { return m_IsSign; }
            set { m_IsSign = value; }
        }

        public string IsSms
        {
            get { return m_IsSms; }
            set { m_IsSms = value; }
        }

        public string CompleteType
        {
            get { return m_CompleteType; }
            set { m_CompleteType = value; }
        }

        public string JoinType
        {
            get { return m_JoinType; }
            set { m_JoinType = value; }
        }

        public string SplitType
        {
            get { return m_SplitType; }
            set { m_SplitType = value; }
        }

        public string ReturnType
        {
            get { return m_ReturnType; }
            set { m_ReturnType = value; }
        }

        public List<Transation> SplitTransations
        {
            get 
            {
                if (m_ListSplitTransation.Count == 0)
                {
                    List<F_TRANSITION> listSplitTransation = DAL.WorkFlow.Transation.GetSplitTransation(this.ID);

                    foreach (F_TRANSITION transation in listSplitTransation)
                    {
                        Transation trans = new Transation(transation.ID);

                        this.m_ListSplitTransation.Add(trans);
                    }
                }

                return m_ListSplitTransation;
            }
            set 
            { 
                m_ListSplitTransation = value; 
            }
        }

        public List<Transation> JoinTransations
        {
            get
            {
                if (m_ListJoinTransation.Count == 0)
                {
                    List<F_TRANSITION> listJoinTransation = DAL.WorkFlow.Transation.GetJoinTransation(this.ID);

                    foreach (F_TRANSITION transation in listJoinTransation)
                    {
                        Transation trans = new Transation(transation.ID);

                        this.m_ListJoinTransation.Add(trans);
                    }
                }

                return m_ListJoinTransation;
            }
            set 
            { 
                m_ListJoinTransation = value; 
            }
        }

        public FlowDefine FlowDefine
        {
            get { return m_FlowDefine; }
            set { m_FlowDefine = value; }
        }

        public List<Participant> Participants
        {
            get
            {
                if (m_ListParticipant.Count == 0)
                {
                    List<F_PARTICIPANT> listParticipant = DAL.WorkFlow.Participant.GetListParticipantByActivityId(this.ID);

                    foreach (F_PARTICIPANT participant in listParticipant)
                    {
                        this.m_ListParticipant.Add(new Participant(participant.ID, participant.Type));
                    }
                }

                return m_ListParticipant;
            }
            set
            {
                m_ListParticipant = value;
            }
        }

        #endregion

        #region 构造函数
        public Activity()
        {

        }

        public Activity(int id)
        {
            m_ActivityId = id;

            F_ACTIVITY activity = DAL.WorkFlow.Activity.Get(m_ActivityId);

            this.Type = activity.Type;
            this.JoinType = activity.JoinType;
            this.SplitType = activity.SplitType;
            this.FlowDefine = new FlowDefine(activity.FlowID);
            this.CompleteType = activity.CompleteType;
            this.ReturnType = activity.ReturnType;
            this.Name = activity.Name;
            this.IsSign = activity.IsSignature;
            this.IsSms = activity.IsSMS;

        }
        #endregion

        public List<int> ParseParticipant(int flowId,int flowNo)
        {
            List<int> listWorkerId = new List<int>();

            List<F_PARTICIPANT> listParticipant = DAL.WorkFlow.Participant.GetListParticipantByActivityId(this.ID);

            foreach (F_PARTICIPANT participant in listParticipant)
            {
                Participant p=new Participant(participant.ID, participant.Type);

                IList<int> listId = p.Parse(flowId, flowNo);

                if (listId != null)
                {
                    listWorkerId.AddRange(listId);
                }
            }

            return listWorkerId;
        }

    }
    #endregion
}
