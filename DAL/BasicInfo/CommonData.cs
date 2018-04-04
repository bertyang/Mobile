using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

using Anchor.FA.Model;


namespace Anchor.FA.DAL.BasicInfo
{
    public class CommonData
    {
        public static IList<G_DATA> GetDataByType(string type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.G_DATA.Where(t => t.Type == type).OrderBy(t => t.Sequence).ToList();

            }
        }

        public static object SearchLoadAll(string type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from t in dbContext.G_DATA
                           select t;
                long total = list.LongCount();

                if (!string.IsNullOrEmpty(type))
                {
                    list = list.Where(t => t.Type == type);
                }
                var result = new { total = total, rows = list.ToList() };
                return result;
            }
        }

        public static object LoadAllDataByPage(int page, int rows, string order, string sort, string type)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from t in dbContext.G_DATA select t;
                long total = list.LongCount();

                if (!string.IsNullOrEmpty(type))
                {
                    list = list.Where(t => t.Type == type);
                }
                list = list.OrderBy(t => t.ID);
                list = list.Skip((page - 1) * rows).Take(rows);

                var result = new { total = total, rows = list.ToList() };

                return result;
            }
        }

        public static object Edit(int? id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                G_DATA entity = null;
                if (id != null)
                {
                    entity = dbContext.G_DATA.FirstOrDefault(g => g.ID == id);
                }
                entity = entity ?? new G_DATA
                {
                    ID = 0,
                    Name = string.Empty,
                    Type = string.Empty,
                    Value = string.Empty,
                    Sequence = 0,
                    Remark = string.Empty,
                };
                return entity;
            }
        }

        public static bool Save(G_DATA entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                if (entity.ID == 0)
                {
                    var list = from p in dbContext.G_DATA select p.ID;
                    long total = list.LongCount();
                    if (total == 0)
                    {
                        entity.ID = 1;
                    }
                    else
                    {
                        entity.ID = dbContext.G_DATA.Max(t => t.ID) + 1;
                    }

                    dbContext.G_DATA.InsertOnSubmit(entity);
                    dbContext.SubmitChanges();
                    return true;
                }
                else
                {
                    var model = dbContext.G_DATA.FirstOrDefault(t => t.ID == entity.ID);
                    model.ID = entity.ID;
                    model.Name = entity.Name;
                    model.Remark = entity.Remark;
                    model.Sequence = entity.Sequence;
                    model.Type = entity.Type;
                    model.Value = entity.Value;
                    dbContext.SubmitChanges();
                    return true;
                }
            }
        }

        public static bool Delete(IList<int> idList)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                foreach (int g in idList)
                {
                    var model = dbContext.G_DATA.Single(t => t.ID == g);

                    dbContext.G_DATA.DeleteOnSubmit(model);
                }
                dbContext.SubmitChanges();
                return true;
            }
        }

        public static object DataType()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = (from g in dbContext.G_DATA select new { Type = g.Type }).Distinct();
                return list.ToList();
            }
        }

        /// <summary>
        /// 加载护士列表
        /// </summary>
        /// <returns></returns>
        public static object LoadNurse()
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from r in dbContext.B_WORKER_ROLE
                           join w in dbContext.B_WORKER on r.WorkerID equals w.ID
                           where r.RoleID == 5 && w.Name != "测试护士"
                           select new
                           {
                               w.ID,
                               w.Name,
                           };
                return list.ToList();
            }
        }

        public static G_DATA GetData(string type, string value)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.G_DATA.SingleOrDefault(t => t.Type == type && t.Value == value);

            }
        }
    }
}
