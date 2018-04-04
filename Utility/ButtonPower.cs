using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;
namespace Anchor.FA.Utility
{
    /// <summary>
    /// 权限获取类
    /// 例如：Anchor.FA.Utility.ButtonPower p = new ButtonPower();
    /// p.ActionIDRang = UserInfo.GetRange(ActionId);//新方法
    /// string Search = p.GetGroupRangePower("searchBound");
    /// if (Search == "SearchControlMe")//查找受理中的本人。。。。。
    /// p.IsHaveRangePower("OnDutyAll")
    /// </summary>
    public class ButtonPower
    {
        //public int ActionID;
        //public int WorkerID;
        public List<B_Range> ActionIDRang { get; set; }
        public bool IsHaveRangePower(string Range)
        {
            return ActionIDRang.Any(t => t.Range == Range);
        }
        public string GetGroupRangePower(string GroupRange)
        {
            if (!ActionIDRang.Any(t => t.GroupRange == GroupRange))
                return null;
            var gr = from t in ActionIDRang
                     where t.GroupRange == GroupRange
                     select t;
            string g = (from l in gr
                        where l.Weight == gr.Max(t => t.Weight)
                        select l).First().Range;
            return g;
        }
    }
}
