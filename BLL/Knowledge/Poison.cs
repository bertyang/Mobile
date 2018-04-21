using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.BLL.Knowledge
{
    internal class Poison
    {
        public IList<TPoison> GetPoison(string chineseName, string englishName)
        {
            return DAL.Knowledge.Poison.GetPoison(chineseName, englishName);
        }
        public TPoison GetPoisonById(string id)
        {
            TPoison entity = DAL.Knowledge.Poison.GetPoisonByName(id);
            if (entity != null)
            {
                entity.性质 = RtfToText(entity.性质);
                entity.毒性 = RtfToText(entity.毒性);
                entity.特点 = RtfToText(entity.特点);
                entity.毒理作用 = RtfToText(entity.毒理作用);
                entity.中毒表现 = RtfToText(entity.中毒表现);
                entity.诊断要点 = RtfToText(entity.诊断要点);
                entity.救治要点 = RtfToText(entity.救治要点);
                entity.毒理 = RtfToText(entity.毒理);
                entity.药动学 = RtfToText(entity.药动学);
                entity.实验室检查 = RtfToText(entity.实验室检查);
                entity.药理 = RtfToText(entity.药理);
                entity.病原学 = RtfToText(entity.病原学);
                entity.流行病学 = RtfToText(entity.流行病学);
                entity.备注 = RtfToText(entity.备注);
            }
            
            return entity;
        }
        public string RtfToText(string rtfStr)
        {
            return DAL.Knowledge.Poison.RtfToText(rtfStr);
        }
    }
}
