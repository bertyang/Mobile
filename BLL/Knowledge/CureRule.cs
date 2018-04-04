using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.Knowledge
{
    internal class CureRule
    {
        public IList<TCureRule> GetCureRule(string Name)
        {
            return DAL.Knowledge.CureRule.GetCureRule(Name);
        }
        public TCureRule GetCureRuleById(string id)
        {
            return DAL.Knowledge.CureRule.GetCureRuleById(id);
        }
    }
}
