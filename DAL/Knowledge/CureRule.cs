using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.DAL.Knowledge
{
    public class CureRule
    {
        public static IList<TCureRule> GetCureRule(string Name) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //查询包含疾病名称的集合并排序
                if (!string.IsNullOrEmpty(Name))
                {
                    return dbContext.TCureRule.Where(t => t.疾病名称.Contains(Name)).OrderBy(t => t.编码).ToList();
                }
                else
                {
                    return dbContext.TCureRule.ToList();
                }
            }
        }

        public static TCureRule GetCureRuleById(string id)  
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.TCureRule.FirstOrDefault(t => t.编码 == id);
            }
        }
    }
}
