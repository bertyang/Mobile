using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Anchor.FA.Model;
using Anchor.FA.DAL.WorkFlow;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Condition
    {
        private int m_ConditionId;
        private string m_Sql;
        private string m_Operator;
        private string m_Value;
        private string m_Type;
        private Transation m_Transation;

        public Transation Transation
        {
            get
            {
                return m_Transation;
            }
        }

        public Condition(int id)
        {
            m_ConditionId = id;

            F_CONDITION condition = DAL.WorkFlow.Condition.Get(m_ConditionId);

            m_Sql = condition.Sql;
            m_ConditionId = condition.ID;
            m_Operator = condition.Operator;
            m_Value = condition.Value;
            m_Type = condition.Type;
            m_Transation = new Transation(condition.TransationID);
        }

        public bool Parse(int flowId,int flowNo)
        {
            object colomnValue = DAL.WorkFlow.Column.GetColomnValue(m_Sql,flowNo,m_Type);

            if (colomnValue != null)
            {
                Compare compare = (Compare)Assembly.Load("Anchor.FA.BLL.WorkFlow").CreateInstance("Anchor.FA.BLL.WorkFlow.Compare" + m_Type.ToUpper());

                compare.VariableA = colomnValue;
                compare.VariableB = m_Value;
                compare.Operator = m_Operator;

                return compare.GetResult();
            }

            return false;
        }
    }
}
