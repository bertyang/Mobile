using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Transation
    {
        private Activity m_ToActivity = null;
        private Activity m_FromActivity = null;
        private string m_ConditionJoin = string.Empty;
        private List<Condition> m_ListCondition = new List<Condition>();        
        private int m_TransationId ;

        public int ID
        {
            get { return m_TransationId; }
            set { m_TransationId = value; }
        }

        public Activity ToActivity
        {
            get { return m_ToActivity; }
        }

        public Activity FromActivity
        {
            get { return m_FromActivity; }
        }

        public List<Condition> Conditions
        {
            get
            {
                if (m_ListCondition.Count == 0)
                {
                    List<F_CONDITION> listCondition = DAL.WorkFlow.Condition.GetListConditionByTransationId(this.ID);

                    foreach (F_CONDITION condition in listCondition)
                    {
                        this.m_ListCondition.Add(new Condition(condition.ID));
                    }
                }

                return m_ListCondition;
            }
            set 
            { 
                m_ListCondition = value; 
            }
        }

        public Transation(int id)
        {
            m_TransationId = id;

            F_TRANSITION transation = DAL.WorkFlow.Transation.Get(m_TransationId);

            m_FromActivity = new Activity(transation.StartActivtyID);
            m_ToActivity = new Activity(transation.EndActivityID);
            m_ConditionJoin = transation.ConditionJoin;
        }


        public bool Parse(int flowId, int flowNo)
        {
            if (this.Conditions.Count == 0) return true;
          
            StringBuilder ConditionExps = new StringBuilder();

            foreach (Condition cs in m_ListCondition)
            {
                ConditionExps.Append(cs.Parse(flowId, flowNo).ToString().ToUpper() + ";");
            }

            return Parse(ConditionExps.ToString().TrimEnd(';'));
        }

        private bool Parse(string exps)
        {
            string[] split = exps.Split(';');

            if (m_ConditionJoin == "OR")
            {
                for (int i = 0; i < split.Length; i++)
                {
                    if (split[i] == "TRUE")
                    {
                        return true;
                    }
                }

                return false;
            }
            else if (m_ConditionJoin == "AND")
            {
                for (int i = 0; i < split.Length; i++)
                {
                    if (split[i] == "FALSE")
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
