using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.Knowledge
{
    internal class Danger
    {
        public IList<TDanger> GetDanger(string chineseName, string englishName)
        {
            return DAL.Knowledge.Danger.GetDanger(chineseName,englishName);
        }
        public TDanger GetDangerById(string id) 
        {
            return DAL.Knowledge.Danger.GetDangerById(id);
        }
    }
}
