using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anchor.FA.BLL.WorkFlow
{
    internal class Compare
    {
        protected object m_VariableA;
        protected object m_VariableB;
        protected string m_Operator;

        public object VariableA
        {
            set { m_VariableA = value; }
        }

        public object VariableB
        {
            set { m_VariableB = value; }
        }

        public string Operator
        {
            set { m_Operator = value; }
        }

        public virtual bool GetResult()
        {
            return false;
        }
    }

    internal class CompareINT : Compare
    {
        public override bool GetResult()
        {
            int A = (int)m_VariableA;
            int B = Convert.ToInt32(m_VariableB);

            switch (m_Operator)
            {
                case "==":
                    if (A == B) return true;
                    break;
                case ">=":
                    if (A >= B) return true;
                    break;
                case "<=":
                    if (A <= B) return true;
                    break;
                case ">":
                    if (A > B) return true;
                    break;
                case "<":
                    if (A < B) return true;
                    break;
                case "!=":
                    if (A != B) return true;
                    break;
            }

            return false;
        }
    }

    internal class CompareSTRING : Compare
    {
        public override bool GetResult()
        {
            string A = (string)m_VariableA;
            string B = (string)m_VariableB;

            switch (m_Operator)
            {
                case "==":
                    if (A.Equals(B)) return true;
                    break;
                case "!=":
                    if (!A.Equals(B)) return true;
                    break;
            }

            return false;
        }
    }

    internal class CompareDECIMAL : Compare
    {
        public override bool GetResult()
        {
            Decimal A = (Decimal)m_VariableA;
            Decimal B = Convert.ToDecimal(m_VariableB);

            switch (m_Operator)
            {
                case "==":
                    if (A == B) return true;
                    break;
                case ">=":
                    if (A >= B) return true;
                    break;
                case "<=":
                    if (A <= B) return true;
                    break;
                case ">":
                    if (A > B) return true;
                    break;
                case "<":
                    if (A < B) return true;
                    break;
                case "!=":
                    if (A != B) return true;
                    break;
            }

            return false;
        }
    }

    internal class CompareDOUBLE : Compare
    {
        public override bool GetResult()
        {
            Double A = (Double)m_VariableA;
            Double B = Convert.ToDouble(m_VariableB);

            switch (m_Operator)
            {
                case "==":
                    if (A == B) return true;
                    break;
                case ">=":
                    if (A >= B) return true;
                    break;
                case "<=":
                    if (A <= B) return true;
                    break;
                case ">":
                    if (A > B) return true;
                    break;
                case "<":
                    if (A < B) return true;
                    break;
                case "!=":
                    if (A != B) return true;
                    break;
            }

            return false;
        }
    }
}
