using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Anchor.FA.Model;

namespace Anchor.FA.DAL.BasicInfo
{
    public class Config
    {
        #region 功能项管理
        public static object LoadAllActionByPage(int page, int rows, string order, string sort)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = (from a in dbContext.G_CONFIG select a);
                long total = list.LongCount();

                list = list.OrderBy(p => p.Type);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };

                return result;
            }

        }

        public static object Edit(string key)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {

                return dbContext.G_CONFIG.FirstOrDefault(a => a.Key == key);
                
            }
        }
        public static bool Save(G_CONFIG entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.G_CONFIG.FirstOrDefault(a => a.Key == entity.Key);
                model.Value = entity.Value;

                try
                {
                    dbContext.SubmitChanges();
                }
                catch (Exception e)
                {

                    throw;
                }
                return true;

            }
        }



        #endregion
    }
}
