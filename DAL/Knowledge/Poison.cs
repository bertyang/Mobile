using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anchor.FA.Model;
using System.Windows.Forms;

namespace Anchor.FA.DAL.Knowledge
{
    public class Poison
    {
        public static IList<TPoison> GetPoison(string chineseName, string englishName)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                //查询包含中文名称的集合并排序
                if (!string.IsNullOrEmpty(chineseName))
                {
                    return dbContext.TPoison.Where(t => t.中文名称.Contains(chineseName)).OrderBy(t => t.序号).ToList();
                }
                //查询包含英文名称的集合并排序
                if (!string.IsNullOrEmpty(chineseName))
                {
                    return dbContext.TPoison.Where(t => t.英文名称.Contains(englishName)).OrderBy(t => t.序号).ToList();
                }
                else
                {
                    return dbContext.TPoison.ToList();
                }
            }
        }

        public static TPoison GetPoisonByName(string id) 
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.TPoison.FirstOrDefault(t => t.序号 == id);
            }
        }

        /// <summary>
        /// 将RTf转成Text
        /// </summary>
        /// <param name="rtfCode"></param>
        /// <returns></returns>
        public static string RtfToText(string rtfStr)
        {
            RichTextBox rtfObj = new RichTextBox();
            rtfObj.Rtf = rtfStr;
            return rtfObj.Text;
        }
    }
}
