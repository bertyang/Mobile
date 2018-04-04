using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Anchor.FA.Model;

namespace Anchor.FA.DAL.WorkFlow
{
    public class Transation
    {
        public static List<F_TRANSITION> GetSplitTransation(int activityid)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_TRANSITION.Where(t => t.StartActivtyID == activityid).ToList();
            }
        }

        public static List<F_TRANSITION> GetJoinTransation(int activityid)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_TRANSITION.Where(t => t.EndActivityID == activityid).ToList();
            }
        }

        public static F_TRANSITION Get(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                return dbContext.F_TRANSITION.FirstOrDefault(t => t.ID == id);
            }
        }


        public static int Insert(F_TRANSITION entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var list = from a in dbContext.F_FLOW select a.ID;

                long total = list.LongCount();

                if (total == 0)
                {
                    entity.ID = 1;
                }
                else
                {
                    entity.ID = dbContext.F_FLOW.Max(a => a.ID) + 1;
                }

                dbContext.F_TRANSITION.InsertOnSubmit(entity);

                dbContext.SubmitChanges();

                return entity.ID;
            }
        }

        public static bool Save(F_TRANSITION entity)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {

                var model = dbContext.F_TRANSITION.FirstOrDefault(t => t.ID == entity.ID);

                model.ConditionJoin = entity.ConditionJoin;
                model.Description = entity.Description;
                model.EndActivityID = entity.EndActivityID;
                model.StartActivtyID = entity.StartActivtyID;

                dbContext.SubmitChanges();
            }

            return true;
        }

        public static bool Delete(int id)
        {
            using (MainDataContext dbContext = new MainDataContext())
            {
                var model = dbContext.F_TRANSITION.SingleOrDefault(t => t.ID == id);

                if (model != null)
                {
                    //dbContext.F_TRANSITION.Load();
                    dbContext.F_TRANSITION.DeleteOnSubmit(model);

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
