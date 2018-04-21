using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;

namespace Anchor.FA.DAL.Knowledge
{
    public class Danger
    {
        public static IList<TDanger> GetDanger(string chineseName, string englishName)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //查询包含中文名称的集合并排序
                if (!string.IsNullOrEmpty(chineseName))
                {
                    return dbContext.TDanger.Where(t => t.中文名称.Contains(chineseName)).OrderBy(t => t.序号).ToList();
                }
                //查询包含英文名称的集合并排序
                if (!string.IsNullOrEmpty(englishName))
                {
                    return dbContext.TDanger.Where(t => t.英文名称.Contains(englishName)).OrderBy(t => t.序号).ToList();
                }
                else
                {
                    return dbContext.TDanger.ToList();
                }
            }
        }

        public static TDanger GetDangerById(string id) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.TDanger.FirstOrDefault(t => t.序号 == id);
            }
        }
    }
}
