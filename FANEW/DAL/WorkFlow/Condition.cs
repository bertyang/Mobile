using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class Condition
    {
        public static List<F_CONDITION> GetListConditionByTransationId(int transationId)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_CONDITION.Where(t => t.TransationID == transationId).ToList();
            }
        }

        public static F_CONDITION Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_CONDITION.FirstOrDefault(t => t.ID == id);
            }
        }


        public static int Insert(F_CONDITION entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_CONDITION select a.ID;

                long total = list.LongCount();

                if (total == 0)
                {
                    entity.ID = 1;
                }
                else
                {
                    entity.ID = dbContext.F_CONDITION.Max(a => a.ID) + 1;
                }

                dbContext.F_CONDITION.InsertOnSubmit(entity);

                dbContext.SubmitChanges();

                return entity.ID;
            }
        }

        public static bool Save(F_CONDITION entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {

                var model = dbContext.F_CONDITION.FirstOrDefault(t => t.ID == entity.ID);

                model.CreateTime = entity.CreateTime;
                model.Sql = entity.Sql;
                model.TransationID = entity.TransationID;
                model.Operator = entity.Operator;
                model.Type = entity.Type;
                model.Value = entity.Value;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public static bool Delete(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_CONDITION.SingleOrDefault(t => t.ID == id);

                if (model != null)
                {
                    //dbContext.F_CONDITION.Load();
                    dbContext.F_CONDITION.DeleteOnSubmit(model);

                    dbContext.SubmitChanges();

                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
